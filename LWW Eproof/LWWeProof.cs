using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using DatabaseLayer;
using System.Configuration;
using ProcessNotification;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using PdfProcess;
using iTextSharp.text.pdf;
using System.Threading;
namespace LWWeProof
{
    class LWWeProof : MessageEventArgs
    {
         DBProcess _DBObj       = DBProcess.DBProcessObj;
            string _ProcessPath = string.Empty;
            string _GoXmlPath   = string.Empty;
            string _ZipPath     = string.Empty;
        Validation _ValidationObj ;

        public LWWeProof()
        {
            DBProcess.DBConStr = ConfigDetails.LWWConStr;
            _DBObj = DBProcess.DBProcessObj;

            

        }
        private void InitiallizeEditorInfo()
        {
            
        }
        public LWWeProof(Validation ValidationObj):this()
        {
            _ValidationObj = ValidationObj;
            
        }

        public string GoXmlPath
        {
            get { return _GoXmlPath; }
        }
        public string ZipPath
        {
            get { return _ZipPath; }
        }

        public void StartProcess()
        {
            try
            {
                ProcessEventHandler("Start GetInprocessMessageDetail..");
                List<MessageDetail> _MsgList = _DBObj.GetInprocessMessageDetail();
                ProcessEventHandler("_MsgList Count :" + _MsgList.Count.ToString());
                bool CreateDS = false;
                foreach (MessageDetail MsgDtl in _MsgList)
                {
                    CreateDS = false;
                    MNTInfo MNT = new MNTInfo(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage);

                    MNT.Status = MsgDtl.Status;

                    // check jid if exists then check taskname and fail
                    ProcessEventHandler("Getting input files");

                    GetInput GetInputOBj = new GetInput(MNT);
                    GetInputOBj.ProcessNotification += ProcessEventHandler;
                    GetInputOBj.ErrorNotification += ProcessErrorHandler;

                    if (GetInputOBj.CopyXMLPDFFromServerOrFMS())
                    {
                        _ProcessPath = GetInputOBj.PrcsAIDFolder;


                        ProcessEventHandler("_ProcessPath : " + _ProcessPath);
                        ProcessEventHandler("ConfigDetails.PrfPath : " + ConfigDetails.PrfPath);
                        if (Directory.Exists(ConfigDetails.PrfPath))
                        {


                            string[] PrfFiles = Directory.GetFiles(ConfigDetails.PrfPath, MNT.AID + "*prf.pdf", SearchOption.AllDirectories);
                            ProcessEventHandler("PrfFiles : " + PrfFiles.Length);
                            if (PrfFiles.Length > 0)
                            {
                                string PrfFile = _ProcessPath + "\\" + Path.GetFileName(PrfFiles[0]);
                                File.Copy(PrfFiles[0], PrfFile, true);
                            }
                        }


                        InputFiles InputObj = new InputFiles(GetInputOBj.PDFPath, "", MsgDtl.Stage);

                        _ValidationObj = new Validation(InputObj);
                        _ValidationObj.ProcessNotification += ProcessEventHandler;
                        _ValidationObj.ErrorNotification += ProcessErrorHandler;

                        ProcessEventHandler("StartValidation");
                        _ValidationObj.StartValidation();
                        ProcessEventHandler("Validation Finished = _ValidationObj.IsValidJID : " + _ValidationObj.IsValidJID);
                        ProcessEventHandler("_ValidationObj.ValidationResult : " + _ValidationObj.ValidationResult);


                        if (_ValidationObj.IsValidJID == false)
                        {
                            _ValidationObj.ValidationResult = false;
                            ProcessEventHandler("Task Not found mail start");
                            eProofResultNotification.InternalValidationMail(this, _ValidationObj);
                            _DBObj.UpdateMessageStatus(MsgDtl.MsgID, _ValidationObj.AIDObj.SNO, _ValidationObj.AIDObj.PdfPages, _ValidationObj.ValidationResult.ToString());
                            ProcessEventHandler("UpdateMessageStatus end");
                            ProcessEventHandler("Task Not found mail end");
                        }
                        else
                        {
                            //=======================================================
                            _ValidationObj.ValidationResult = true;
                            //=======================================================
                            if (_ValidationObj.ValidationResult)
                            {
                                ProcessEventHandler("_ValidationObj.ValidationResult Start");
                                ProcessEventHandler("Dataset creation started.");
                                CreateDS = CreateDataset();
                                ProcessEventHandler("Dataset creation process finished.");
                            }
                            if (CreateDS)
                            {
                                ProcessEventHandler("InternalValidationMail start");
                                eProofResultNotification.InternalValidationMail(this, _ValidationObj);
                                ProcessEventHandler("InternalValidationMail end");
                            }
                            else
                            {
                                eProofResultNotification.InternalValidationMail(this, _ValidationObj);
                                ProcessEventHandler("Dataset not uploaded and external mail not sent.");
                                _DBObj.UpdateMessageStatus(MsgDtl.MsgID, _ValidationObj.AIDObj.SNO, _ValidationObj.AIDObj.PdfPages, _ValidationObj.ValidationResult.ToString());
                                ProcessEventHandler("UpdateMessageStatus end");
                            }
                        }
                    }

                    if (_ValidationObj != null && !_ValidationObj.IsPdfProcessError && CreateDS)
                    {
                        ProcessEventHandler("_ValidationObj.IsPdfProcessError start");
                        if (!string.IsNullOrEmpty(_ValidationObj.AIDObj.JID) && _ValidationObj.AIDObj.Stage.Equals("S100"))
                            InsertReminder();

                        _DBObj.UpdateMessageStatus(MsgDtl.MsgID, _ValidationObj.AIDObj.SNO, _ValidationObj.AIDObj.PdfPages, _ValidationObj.ValidationResult.ToString());
                        ProcessEventHandler("UpdateMessageStatus end");

                    }
                }
            }
            catch (Exception e)
            {
                ProcessEventHandler("ERROR : " + e.ToString());
            }
        }

        public void StartValidation()
        { 
        }

        public bool CreateDataset()
        {
            bool Rslt = false;
            try
            {
                string RcvdFileName = Path.GetFileNameWithoutExtension(  _ValidationObj.GoXMLObj.archivefile);

                _ZipPath    = _ProcessPath +"\\"+   RcvdFileName +".zip";
                _GoXmlPath  = _ProcessPath +"\\"+   RcvdFileName +".go.xml";

                StringCollection InPutFiles = new StringCollection();

                ProcessEventHandler("_ZipPath   " + _ZipPath);
                ProcessEventHandler("_GoXmlPath " + _GoXmlPath);

                if (!string.IsNullOrEmpty(_ValidationObj.AIDObj.SubmissionXML))
                {

                    string SubmissionXML= _ValidationObj.AIDObj.SubmissionXML;
                    string Srch = "<target-number-of-pages>0</target-number-of-pages>";
                    //string Rplc = "<target-number-of-pages>" + _ValidationObj.AIDObj.PdfPages+ "</target-number-of-pages>";
                    string Rplc = string.Empty;
                    try
                    {
                        var data = File.ReadAllLines(ConfigDetails.LWWPDFCountFile + @"\" + _ValidationObj.AIDObj.JID + @"\" + _ValidationObj.AIDObj.AID + @"\text\pageCount.txt").Select(x => x.Split('=')).Where(x => x.Length > 1).ToDictionary(x => x[0].Trim(), x => x[1]);
                        string str = data["\"Total Page count"].Replace("\"", string.Empty);
                        Rplc = "<target-number-of-pages>" + Convert.ToInt32(str) + "</target-number-of-pages>";
                        ProcessEventHandler("PageCount   " + str);
                    }
                    catch(Exception ex)
                    {
                        Rplc = "<target-number-of-pages>" + _ValidationObj.AIDObj.PdfPages + "</target-number-of-pages>";
                        ProcessEventHandler(ex.Message);
                        ProcessEventHandler("PDF Count   " + _ValidationObj.AIDObj.PdfPages);
                    }

                    SubmissionXML = SubmissionXML.Replace(Srch, Rplc);

                    //<manuscript-number>ANNSURG-D-15-01920</manuscript-number>

                    System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument ();
                    xDoc.XmlResolver= null;
                    xDoc.LoadXml(SubmissionXML);
                    System.Xml.XmlNodeList NL= xDoc.GetElementsByTagName("manuscript-number");
                    if (NL.Count>0)
                    {
                        string Saveto =_ProcessPath + "\\" + NL[0].InnerText +"_Import.xml";
                        File.WriteAllText(Saveto, SubmissionXML);
                        InPutFiles.Add(Saveto);
                    }
                }


                string[] pdfFiles = Directory.GetFiles(_ProcessPath, "*.pdf");
                InPutFiles.AddRange(pdfFiles);
                

                CreateGo CreateGoObj = new CreateGo(_ValidationObj.GoXMLObj.GoXMLString);
                CreateGoObj.ProcessNotification += ProcessEventHandler;
                CreateGoObj.ErrorNotification   += ProcessErrorHandler;

                CreateGoObj.SetFileGroup(pdfFiles);

                File.WriteAllText(_GoXmlPath, CreateGoObj.GoXMLString);



                ConfigDetails.MakeAnn(Directory.GetFiles(_ProcessPath, "*.pdf"));

                CreateZip(InPutFiles, _ZipPath);
                if (File.Exists(_ZipPath) && File.Exists(_GoXmlPath))
                {
                    ProcessEventHandler("To upload on ftp : " + _ZipPath);
                    if (UploadArticleOnFtp(_ZipPath))
                    {
                        ProcessEventHandler("To upload on ftp : " + _GoXmlPath);
                        if (UploadArticleOnFtp(_GoXmlPath))
                        {
                            ProcessEventHandler("insertDatasetHistory");
                            _DBObj.insertDatasetHistory(_ValidationObj.AIDObj.JID, _ValidationObj.AIDObj.AID, _ValidationObj.AIDObj.Stage);
                            Rslt = true;
                        }
                        else
                        {
                            _ValidationObj.IsPdfProcessError = true;
                            ProcessEventHandler("Uploading fail to ftp : " + _GoXmlPath);
                        }
                    }
                    else
                    {
                        ProcessEventHandler("Uploading fail to ftp : " + _ZipPath);
                        _ValidationObj.IsPdfProcessError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessEventHandler("Error in CreateDataset: "+ex.Message);
            }
                return Rslt;
        }


        private void CreateZip(StringCollection PdfFiles,string ZipFileName)
        {
            ProcessEventHandler("CreateZip Process start");
            ProcessEventHandler("ZipFileName : " +ZipFileName);

            ZipFile zipFile=null;
            try
            {
                zipFile = ZipFile.Create(ZipFileName);
                zipFile.BeginUpdate();
                foreach (string Pdf in PdfFiles)
                {
                   // PDF security is enable for do not edit text in PDF file on 21 April 2018
                    string ad = Path.GetExtension(Pdf);
                    if (ad == ".pdf")
                    {
//                        ProcessEventHandler("This is not a valid PDF file : " + Pdf);
                        ProcessEventHandler("PDF security Process Start");
                        try
                        {
                            if (PDFSecurity(Pdf, Pdf.Replace(".pdf", "1.pdf")))
                            {
                                ProcessEventHandler("PDF security is enable successfully");
                            }
                            else
                                ProcessEventHandler("PDF security enable is failed: ");
                        }
                        catch (Exception e)
                        {
                            ProcessEventHandler("Error in PDF security Process: " + e.Message);
                        }
                        ProcessEventHandler("PDF security Process END");
                    }
                    ProcessEventHandler("To add in zip : " + Pdf);
                    zipFile.Add(Pdf, Path.GetFileName(Pdf));
                }
                zipFile.CommitUpdate();
                zipFile.Close();

                ProcessEventHandler("zipFile.Close()");
            }
            catch (Exception Ex)
            {
                ProcessErrorHandler(Ex);
            }
            finally
            {
                if (zipFile!= null)
                    zipFile.Close();
            }
        }
        public bool CopyPDFToPrcsFolder()
        {
            _ProcessPath = ConfigDetails.ProcessPath + "\\" + _ValidationObj.AIDObj.JID + "-" + _ValidationObj.AIDObj.AID;
            string ProcessPdfPath = _ProcessPath + "\\" + Path.GetFileName(_ValidationObj.PDFPath);


            if (Directory.Exists(_ProcessPath))
                Directory.Delete(_ProcessPath,true);

            Directory.CreateDirectory(_ProcessPath);

            while(true)
            {
              if (Directory.Exists(_ProcessPath))
                  break;
            }
            File.Copy(_ValidationObj.PDFPath, ProcessPdfPath, true);

            return true;
        }
        private bool UploadArticleOnFtp(string UploadFileName)
        {

            string FTPURL  = ConfigDetails.FtpUrl;
            string Uname   = ConfigDetails.FtpUsr;
            string PWD     = ConfigDetails.FtpPwd;
            try
            {
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);
                FtpObj.ProcessNotification += ProcessEventHandler;
                FtpObj.ErrorNotification += ProcessErrorHandler;

                ProcessEventHandler(" FTPURL " + FTPURL);
                Console.WriteLine(" FTPURL " + FTPURL);
                if (FtpObj.UploadFileToFTP(UploadFileName) == false)
                {
                    Console.WriteLine("Not able to upload the file" + UploadFileName + ", Uploading second time ...");
                    ProcessEventHandler("Not able to upload the file" + UploadFileName + ", Uploading second time ...");
                    //FtpObj.UploadFileToFTP(UploadFileName);
                    if (FtpObj.UploadFileToFTP(UploadFileName) == false)
                    {
                        ProcessEventHandler("Not able to upload the file..." + UploadFileName);
                        Console.WriteLine(" FTPURL " + FTPURL);
                        //to be set as return false
                        return false;
                    }
                    else
                    {
                        ProcessEventHandler("Successfully Upload Article " + UploadFileName + " On Ftp ...");
                        Console.WriteLine("Successfully Upload Article " + UploadFileName + " On Ftp ...");
                        Thread.Sleep(10000);
                    }
                }
                else
                {
                    ProcessEventHandler("Successfully Upload Article " + UploadFileName + " On Ftp ...");
                    Console.WriteLine("Successfully Upload Article " + UploadFileName + " On Ftp ...");
                    Thread.Sleep(10000);
                }

            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                ProcessEventHandler("Error while uploading article on ftp ...");
                Console.WriteLine("Error while uploading article on ftp ...");
                return false;
            }

            return true;
        }

        private DateTime CalCulateReminderDate(DateTime dt, int Days)
        {
            DateTime ReminderDate;
            int DayIcr = 0;
            while (true)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                }
                else
                {
                    DayIcr = DayIcr + 1;
                    if (DayIcr == Days)
                    {
                        break;
                    }
                }
            }
            ReminderDate = dt;
            return ReminderDate;
        }

        private bool InsertReminder()
        {
            string AuthrMail = GetCorEmail();

            if (string.IsNullOrEmpty(AuthrMail))
            {
                return false;
            }

            ProcessEventHandler("InsertReminder Start");

            String Todaydate = string.Empty;

            Todaydate = System.DateTime.Now.Year.ToString().Trim() + "-" +
                             System.DateTime.Now.Month.ToString().Trim() + "-" +
                             System.DateTime.Now.Day.ToString().Trim() + " " +
                             System.DateTime.Now.Hour.ToString().Trim() + ":" +
                             System.DateTime.Now.Minute.ToString().Trim() + ":" +
                             System.DateTime.Now.Second.ToString().Trim() + "." +
                             System.DateTime.Now.Millisecond.ToString().Trim();


            DateTime FIRSTREMINDER;
            DateTime SECONDREMINDER;
            DateTime THIRDREMINDER;
            DateTime FOURTHREMINDER;

            FIRSTREMINDER = CalCulateReminderDate(DateTime.Today, 3);
            SECONDREMINDER = CalCulateReminderDate(FIRSTREMINDER, 3);
            THIRDREMINDER = CalCulateReminderDate(SECONDREMINDER, 3);
            FOURTHREMINDER = CalCulateReminderDate(THIRDREMINDER, 2);

            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@JID", _ValidationObj.AIDObj.JID);
            param[1] = new SqlParameter("@AID", _ValidationObj.AIDObj.AID);
            param[2] = new SqlParameter("@FIRSTREMINDER",  FIRSTREMINDER);
            param[3] = new SqlParameter("@SECONDREMINDER",SECONDREMINDER);
            param[4] = new SqlParameter("@THIRDREMINDER", THIRDREMINDER);
            param[5] = new SqlParameter("@FOURTHREMINDER", FOURTHREMINDER);
            param[6] = new SqlParameter("@AUTHOREmail", AuthrMail);
            
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigDetails.LWWConStr,System.Data.CommandType.StoredProcedure,"usp_InsertReminderDeatail",param);
                ProcessEventHandler("InsertReminder successfully");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
        }

        private string GetCorEmail()
        {
            try
            {
                string CorEmail = string.Empty;
                MetaDataProcess MetaDataObj = new MetaDataProcess();

                if (!string.IsNullOrEmpty(_ValidationObj.AIDObj.MetaDataXML))
                {
                    MetaDataObj.ProcessMetaXml(_ValidationObj.AIDObj.MetaDataXML);
                    CorEmail = string.IsNullOrEmpty(MetaDataObj.CorEmail) ? ConfigDetails.MailTo : MetaDataObj.CorEmail;
                }
                return CorEmail;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return string.Empty;
        }

        private bool PDFSecurity(string source, string destination)
        {
            bool result = false;
            PdfReader reader;
            PdfStamper stamper;
            try
            {
                try
                {
                    reader = new PdfReader(source);
                    if (File.Exists(destination))
                        File.Delete(destination);
                    //Do not change the order from 1-4 below
                    reader.RemoveUsageRights();     // Step - 1
                    stamper = new PdfStamper(reader, new System.IO.FileStream(destination, System.IO.FileMode.CreateNew)); // Step - 2
                    stamper.SetEncryption(null, System.Text.Encoding.UTF8.GetBytes("Th0MsOnD123"), PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_FILL_IN | PdfWriter.ALLOW_MODIFY_ANNOTATIONS | PdfWriter.ALLOW_COPY, PdfWriter.DO_NOT_ENCRYPT_METADATA);// Step - 3
                    stamper.FormFlattening = true;  // Step - 4
                    stamper.Close();
                    reader.Close();
                    if (File.Exists(source))
                    {
                        File.Delete(source);
                        System.Threading.Thread.Sleep(10000);
                        File.Move(destination, source);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);
                        File.Move(destination, source);
                    }
                    reader = new PdfReader(source);
                    if (!reader.IsEncrypted())
                    {
                        ProcessEventHandler("PDF is not encrypted ");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ProcessEventHandler("Error in PDF Security: " + ex.Message);
                    return false;
                }

                result = true;
            }
            catch (Exception ex)
            {
                ProcessEventHandler(ex.Message);
                result = false;
            }
            return result;
        }
    }
}
