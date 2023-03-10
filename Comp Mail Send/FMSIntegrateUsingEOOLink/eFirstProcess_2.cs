using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using DatabaseLayer;

namespace FMSIntegrateUsingEOOLink
{


    class eFirstDetail
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;
        string _Stage = string.Empty;
        string _JID = string.Empty;
        string _VOL = string.Empty;
        string _ISS = string.Empty;
        string _DOI = string.Empty;
        string _XMLFile = string.Empty;
        string _PDFFile = string.Empty;
        static string NotFoudFile = string.Empty;
        AuthorInfo _AuthorInfoOBJ = new AuthorInfo();

        public string PDFFile
        {
            get { return _PDFFile; }
        }

        public string FIZPath
        {
            set { _FIZPath = value; }
            get { return _FIZPath; }
        }
        public string eFirstPath
        {
            set { _eFirstPath = value; }
            get { return _eFirstPath; }
        }


        public AuthorInfo AuthorInfoOBJ
        {
            get { return _AuthorInfoOBJ; }
        }

        //const string _FIZPath    = @"\\wip\Thieme_3B2\For_Final_Thieme\online\FIZ2012-13\Uploaded\FIZ2014";
        //const string _eFirstPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2014";
        //=============================================================================
        //string _FIZPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\FIZ2020\uploaded";
        //string _eFirstPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2020";
        string _FIZPath = ConfigurationSettings.AppSettings["fizpath"];
        string _eFirstPath = ConfigurationSettings.AppSettings["efirstpath"];
        //=============================================================================                    
        //string _FIZPath    = @"\\wip\Thieme_3B2\For_Final_Thieme\online\FIZ2012-13\Uploaded\FIZ2014";
        //string _eFirstPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2014";

        static eFirstDetail()
        {
            NotFoudFile = "d:\\NotFoudFile.log";

            if (File.Exists(NotFoudFile))
            {
                File.Delete(NotFoudFile);
            }
        }

        public eFirstDetail(string JID, string DOI, string Stage)
        {
            _JID = JID;
            _DOI = DOI;
            _Stage = Stage;
            _AuthorInfoOBJ.ProcessNotification += ProcessMessage;
            _AuthorInfoOBJ.ErrorNotification += ErrorMessage;
        }

        public void AssignXMLPDFFiles()
        {
            string SrchPath = string.Empty;

            if (_Stage.Equals("Fiz", StringComparison.OrdinalIgnoreCase))
                SrchPath = _FIZPath + "\\" + _JID;
            else if (_Stage.Equals("EFirst", StringComparison.OrdinalIgnoreCase))
                SrchPath = _eFirstPath + "\\" + _JID;

            if (!Directory.Exists(SrchPath))
            {
                File.AppendAllText(NotFoudFile, SrchPath + "\\" + _DOI + "\r\n");
                return;
            }


            ProcessMessage("Stage     :: " + _Stage);
            ProcessMessage("Srch Path :: " + SrchPath);
            while (true)
             {
                try
                {
                    string[] XMLFiles = Directory.GetFiles(SrchPath, _DOI + "*.xml", SearchOption.AllDirectories);
                    string[] PDFFiles = Directory.GetFiles(SrchPath, _DOI + "*.pdf", SearchOption.AllDirectories);


                    if (XMLFiles.Length >0)
                    {
                        _XMLFile = XMLFiles[0];
                        ProcessMessage("XMLFile     :: " + _XMLFile);
                       ProcessMessage("Start to get author details");
                        _AuthorInfoOBJ.GetAuthorDetails(_XMLFile);

                        ProcessMessage("End to get author details");
                    }
                    else
                    {
                        File.AppendAllText(NotFoudFile, SrchPath + "\\" + _DOI + "\r\n");
                        ProcessMessage("XMLFile Not found    :: " + _XMLFile);
                    }

                    if (PDFFiles.Length > 0)
                    {
                        string xmlFileName = Path.GetFileNameWithoutExtension(_XMLFile);
                        if (PDFFiles.Length > 1)
                        {
                            foreach (string pdfFile in PDFFiles)
                            {
                                string PDFFileName = Path.GetFileNameWithoutExtension(pdfFile);
                                if (PDFFileName == xmlFileName)
                                {
                                    _PDFFile = pdfFile;
                                    break;
                                }
                            }

                        }
                        else
                            _PDFFile = PDFFiles[0];
                        ProcessMessage("PDFFile     :: " + PDFFile);
                        //Old Condition and change with=> If PDF File is more than one then pick the PDF file which Name exactly match with XML File Name except file extension 12/04/2017
                        //_PDFFile = PDFFiles[0];
                        //ProcessMessage("PDFFile     :: " + PDFFile);
                    }
                    else
                    {
                        ProcessMessage("PDFFile Not found    :: " + PDFFile);
                    }
                    //string[] XMLFiles = Directory.GetFiles(SrchPath, _DOI + "*.xml", SearchOption.AllDirectories);
                    //string[] PDFFiles = Directory.GetFiles(SrchPath, _DOI + "*.pdf", SearchOption.AllDirectories);


                    //if (XMLFiles.Length == 1)
                    //{
                  //  _XMLFile = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2020\jnlsa\202861oa\10-1055-s-0041-1725955_202861oa.xml";

                        //ProcessMessage("XMLFile     :: " + _XMLFile);

                        //ProcessMessage("Start to get author details");

                       // _AuthorInfoOBJ.GetAuthorDetails(_XMLFile);

                        //ProcessMessage("End to get author details");
                    //}
                    //else
                    //{
                    //    File.AppendAllText(NotFoudFile, SrchPath + "\\" + _DOI + "\r\n");
                    //    ProcessMessage("XMLFile Not found    :: " + _XMLFile);
                    //}

                    //if (PDFFiles.Length > 0)
                    //{
                        //string xmlFileName = Path.GetFileNameWithoutExtension(_XMLFile);
                        //if (PDFFiles.Length > 1)
                        //{
                        //    foreach (string pdfFile in PDFFiles)
                        //    {
                        //        string PDFFileName = Path.GetFileNameWithoutExtension(pdfFile);
                        //        if (PDFFileName == xmlFileName)
                        //        {
                        //            _PDFFile = pdfFile;
                        //            break;
                        //        }
                        //    }

                        //}
                        //else
                       // _PDFFile = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2020\jnlsa\202861oa\10-1055-s-0041-1725955_202861oa.pdf";
                    //    ProcessMessage("PDFFile     :: " + PDFFile);
                    //    //Old Condition and change with=> If PDF File is more than one then pick the PDF file which Name exactly match with XML File Name except file extension 12/04/2017
                    //    //_PDFFile = PDFFiles[0];
                    //    //ProcessMessage("PDFFile     :: " + PDFFile);
                    //}
                    //else
                    //{
                    //    ProcessMessage("PDFFile Not found    :: " + PDFFile);
                    //}
                    break;
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                    Console.WriteLine(ex.Message);
                }
                if (Directory.Exists(_FIZPath) || Directory.Exists(_eFirstPath))
                {
                    break;
                }
            }
        }


        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }

    }
    class eFirstPDFProcess
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;
        string _OutputPDF = string.Empty;
        string _JID = string.Empty;
        string _AID = string.Empty;
        string _PDFPath = string.Empty;
        string _BasePath = AppDomain.CurrentDomain.BaseDirectory;

        public string OutputPDF
        {
            get { return _OutputPDF; }
        }
        public eFirstPDFProcess(string PDFPath, string JID, string AID)
        {

            _PDFPath = PDFPath;
            _JID = JID;
            _AID = AID;


            ProcessMessage("_PDFPath : " + PDFPath);
            ProcessMessage("_JID  : " + JID);
            ProcessMessage("_AID  : " + AID);
        }
        public void StartProcess()
        {
            string ProcessPath = _BasePath + "\\Process";

            if (!Directory.Exists(ProcessPath))
            {
                Directory.CreateDirectory(ProcessPath);
            }


            _OutputPDF = _BasePath + "Process\\" + _JID + "_" + _AID + "_1.pdf";

            ProcessMessage("_OutputPDF  : " + _OutputPDF);

            try
            {
                if (File.Exists(_PDFPath))
                {
                    if (File.Exists(_OutputPDF))
                        File.Delete(_OutputPDF);

                    File.Copy(_PDFPath, _OutputPDF);

                    ProcessMessage("AddWatermark  : ");
                    AddWatermark(_OutputPDF, _OutputPDF.Replace("_1.pdf", ".pdf"));

                    if (File.Exists(_OutputPDF))
                        File.Delete(_OutputPDF);

                    _OutputPDF = _OutputPDF.Replace("_1.pdf", ".pdf");

                    ProcessMessage("Final OutputPDF  : " + _OutputPDF);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

        }
        private void AddWatermark(string _InputPDF, string _OutputPDF)
        {
            PdfReader pdfReader = new PdfReader(_InputPDF);
            using (Stream output = new FileStream(_OutputPDF, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (PdfStamper pdfStamper = new PdfStamper(pdfReader, output))
                {
                    for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
                    {
                        pdfStamper.FormFlattening = false;
                        iTextSharp.text.Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(pageIndex);
                        PdfContentByte pdfData = pdfStamper.GetUnderContent(pageIndex);
                        pdfData.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);
                        PdfGState graphicsState = new PdfGState();
                        graphicsState.FillOpacity = 1.8F;
                        pdfData.SetGState(graphicsState);
                        pdfData.BeginText();

                        iTextSharp.text.Image jpeg = iTextSharp.text.Image.GetInstance(_BasePath + "\\Pages from pssa_201300408_img_0.jpg");
                        jpeg.ScaleAbsoluteHeight(230);
                        jpeg.ScaleAbsoluteWidth(540);
                        jpeg.SetDpi(540, 230);
                        jpeg.SetAbsolutePosition(30, 300);

                        pdfData.AddImage(jpeg);

                        pdfData.EndText();
                    }
                    pdfStamper.Close();
                }
                output.Close();
                output.Dispose();

            }
        }


        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }
    }

    class eFirstProcess_2
    {
        public event NotifyMsg ProcessNotification;

        public event NotifyErrMsg ErrorNotification;
        ThiemeOffPrint _ThiemeOffPrintObj = null;
        OPSDB OPSDBobj = new OPSDB();
        string _ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
        public eFirstProcess_2(ThiemeOffPrint ThiemeOffPrintObj)
        {
            _ThiemeOffPrintObj = ThiemeOffPrintObj;
            OPSDBobj = new OPSDB(_ConnectionString);
        }

        public void StartProcess()
        {
            if (string.IsNullOrEmpty(_ThiemeOffPrintObj.JID))
            {
                ProcessNotification("JID:: Blank ");
                string NotFoudFile = "d:\\NotFoudFile.log";
                File.AppendAllText(NotFoudFile, "Alert for not to process mail of subject line : " + _ThiemeOffPrintObj.MailSubjectLine + Environment.NewLine);
                return;
            }

            ProcessMessage("eFirstDetail Object assigning");

            eFirstDetail eFirstDetailOBJ = new eFirstDetail(_ThiemeOffPrintObj.JID, _ThiemeOffPrintObj.DOI, _ThiemeOffPrintObj.STAGE);
            eFirstDetailOBJ.ProcessNotification += ProcessMessage;
            eFirstDetailOBJ.ErrorNotification += ErrorMessage;

            ProcessMessage("JID:: " + _ThiemeOffPrintObj.JID);
            ProcessMessage("To start assign XML and PDF File");

            eFirstDetailOBJ.AssignXMLPDFFiles();


            if (eFirstDetailOBJ.AuthorInfoOBJ.AID != null)
            {
                if (!IgnoreList(eFirstDetailOBJ.AuthorInfoOBJ.AID))
                {
                    _ThiemeOffPrintObj.AID = eFirstDetailOBJ.AuthorInfoOBJ.AID;

                    ProcessMessage("_ThiemeOffPrintObj.AID  : " + _ThiemeOffPrintObj.AID);

                    eFirstPDFProcess eFirstPDFProcessOBJ = new eFirstPDFProcess(eFirstDetailOBJ.PDFFile, _ThiemeOffPrintObj.JID, _ThiemeOffPrintObj.AID);
                    eFirstPDFProcessOBJ.ProcessNotification += ProcessMessage;
                    eFirstPDFProcessOBJ.ErrorNotification += ErrorMessage;

                    eFirstPDFProcessOBJ.StartProcess();

                    string[] Atch = new string[2];
                    if (File.Exists(eFirstPDFProcessOBJ.OutputPDF))
                    {
                        Atch[0] = eFirstPDFProcessOBJ.OutputPDF;
                        if ("#ACI#TH#VCOT#".IndexOf("#" + _ThiemeOffPrintObj.JID + "#") == -1)
                            Atch[1] = AppDomain.CurrentDomain.BaseDirectory + "Social Media Letter.docx";
                        DatabaseLayer.OPSDetail OPSobj = OPSDBobj.GetOPSDetails(_ThiemeOffPrintObj.JID, "Thieme");
                        MailDetail MailDetailObj = new MailDetail();
                        MailDetailObj.MailFrom = "thieme.j@thomsondigital.com";
                        MailDetailObj.MailTo = eFirstDetailOBJ.AuthorInfoOBJ.AuthorEmail;

                        if (MailDetailObj.MailTo == null)
                        {
                            UpdateThiemeOffPrintStatus(_ThiemeOffPrintObj.SNO, "MailToBlank");
                            return;
                        }

                        if ("#IJNT#IJNS#".IndexOf("#" + _ThiemeOffPrintObj.JID + "#") != -1)
                            MailDetailObj.MailCC = OPSobj.Pe_email;
                        else
                            MailDetailObj.MailCC = _ThiemeOffPrintObj.JID == "JAO" ? "David.Stewart@thieme.com,gac2126@cumc.columbia.edu," + OPSobj.Pe_email : "David.Stewart@thieme.com," + OPSobj.Pe_email;

                        //MailDetailObj.MailBCC = "thieme.j@thomsondigital.com,uditpandit21@gmail.com,Rohit.Singh@Digiscapetech.com";
                        MailDetailObj.MailBCC = "thieme.j@thomsondigital.com";

                        MailDetailObj.MailAtchmnt = Atch;

                        ProcessMessage("UpdateThiemeOffPrintStatus for SNO :" + _ThiemeOffPrintObj.SNO);

                        if (UpdateThiemeOffPrintStatus(_ThiemeOffPrintObj.SNO))////////Update status finished before sending the mail
                        {
                            if (_ThiemeOffPrintObj.STAGE.Equals("EFirst", StringComparison.OrdinalIgnoreCase))
                            {
                                ProcessMessage("EFirstMailProcess ");

                                if (EFirstMailProcess_test(MailDetailObj, OPSobj.Jname, _ThiemeOffPrintObj.JID) == false)
                                {
                                    SetThiemeOffPrintStatusInProgress(_ThiemeOffPrintObj.SNO);////////If mail send failed then change the status inprogress
                                    ProcessMessage("mail send failed then change the status inprogress ");
                                }
                            }
                            else if (_ThiemeOffPrintObj.STAGE.Equals("Fiz", StringComparison.OrdinalIgnoreCase))
                            {
                                ProcessMessage("FizMailProcess ");

                                if (FizMailProcess_test(MailDetailObj, OPSobj.Jname, eFirstDetailOBJ.AuthorInfoOBJ.VOL, eFirstDetailOBJ.AuthorInfoOBJ.ISS, _ThiemeOffPrintObj.JID) == false)
                                {
                                    SetThiemeOffPrintStatusInProgress(_ThiemeOffPrintObj.SNO);////////If mail send failed then change the status inprogress
                                    ProcessMessage("mail send failed then change the status inprogress ");
                                }
                            }
                        }
                    }
                    else
                    {
                        ProcessMessage("Not send mail as " + eFirstPDFProcessOBJ.OutputPDF + " not exist");
                        MailDetail MailDetailObj = new MailDetail();
                        MailDetailObj.MailFrom = "eproof@thomsondigital.com";
                        MailDetailObj.MailTo = "thieme.j@thomsondigital.com";
                        MailDetailObj.MailCC = "";
                        MailDetailObj.MailBCC = "";
                        MailDetailObj.MailBody = "Complimentary mail not sent. PDF file " + eFirstPDFProcessOBJ.OutputPDF + " not exists for JID = " + _ThiemeOffPrintObj.JID+" and DOI = "+_ThiemeOffPrintObj.DOI;

                        MailDetailObj.MailSubject = "PDF Not found (DOI : " + _ThiemeOffPrintObj.DOI+")";

                        UpdateThiemeOffPrintStatus(_ThiemeOffPrintObj.SNO, "OutputPdfBlank");
                        eMailProcess eMailProcessObj = new eMailProcess();
                      
                            eMailProcessObj.SendMailInternal(MailDetailObj);
                       
                    }
                }
                else
                    UpdateThiemeOffPrintStatus(_ThiemeOffPrintObj.SNO);

            }
            else
            {
                ProcessMessage("Not send mail as AID is blank.");
                MailDetail MailDetailObj = new MailDetail();
                MailDetailObj.MailFrom = "eproof@thomsondigital.com";
                MailDetailObj.MailTo = "thieme.j@thomsondigital.com";
                MailDetailObj.MailCC = "";
                MailDetailObj.MailBCC = "";
                MailDetailObj.MailBody = "Complimentary mail not sent. AID is blank for JID = " + _ThiemeOffPrintObj.JID + " and DOI = " + _ThiemeOffPrintObj.DOI;
                
                MailDetailObj.MailSubject = "AID is blank (DOI : "+ _ThiemeOffPrintObj.DOI + ")";

                UpdateThiemeOffPrintStatus(_ThiemeOffPrintObj.SNO, "AidBlank");


                eMailProcess eMailProcessObj = new eMailProcess();
                eMailProcessObj.SendMailInternal(MailDetailObj);
            }
        }


        private bool UpdateThiemeOffPrintStatus(int SNO, string status)
        {
            bool Result = false;
            SqlParameter[] Arg = new SqlParameter[2];
            Arg[0] = new SqlParameter("@SNO", SNO);
            Arg[1] = new SqlParameter("@Status", status);

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_UpdateThiemeOffPrintWithStatus", Arg);
                return true;
            }
            catch (SqlException Ex)
            {
                ErrorMessage(Ex);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
            return Result;
        }
        private bool UpdateThiemeOffPrintStatus(int SNO)
        {
            bool Result = false;
            SqlParameter[] Arg = new SqlParameter[1];
            Arg[0] = new SqlParameter("@SNO", SNO);

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_UpdateThiemeOffPrintStatus", Arg);
                return true;
            }
            catch (SqlException Ex)
            {
                ErrorMessage(Ex);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
            return Result;
        }

        private bool SetThiemeOffPrintStatusInProgress(int SNO)
        {
            bool Result = false;
            SqlParameter[] Arg = new SqlParameter[1];
            Arg[0] = new SqlParameter("@SNO", SNO);

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_SetThiemeOffPrintStatusInProgress", Arg);
                return true;
            }
            catch (SqlException Ex)
            {
                ErrorMessage(Ex);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
            return Result;
        }
        private bool EFirstMailProcess_test(MailDetail MailDetailOBJ, string JT, string JID = "")
        {
            try
            {
                string _Subject = "Complimentary PDF of your article published online in  " + JT;

                string template = System.Configuration.ConfigurationManager.AppSettings["EFirstMailTemplatePath"];
                StreamReader sr = new StreamReader(template);
                string mailBody = sr.ReadToEnd();
                string survey_txt = survey_text(JID);
                if (survey_txt != "")
                {
                    mailBody = mailBody.Replace("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.", "This PDF is for personal and educational purposes only and should not be distributed or printed commercially.</br></br>Help us enhance your publishing experience by taking this quick survey. Your feedback is very important to us.</br><a href=\"" + survey_txt + "\">" + survey_txt + "</a>");
                }
                mailBody = mailBody.Replace("[JT]", JT);
                if (JT == "Journal of Neurological Surgery Part B")
                    mailBody = mailBody.Replace("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.", "This PDF is for personal and educational purposes only and should not be distributed or printed commercially. </br>Please note that your article will get included in PubMed once published in an issue. Due to the increase in submissions to the journals, we are facing delays we are working hard to reduce the turnaround time and will include your paper as soon as possible.");

                MailDetailOBJ.MailBody = mailBody.ToString();
                MailDetailOBJ.MailSubject = _Subject;

                eMailProcess eMailProcessObj = new eMailProcess();

                //ForTest
                if (eMailProcessObj.SendMailExternal(MailDetailOBJ))
                {
                    ProcessMessage("SendMailExternal Successfully");
                    try
                    {
                        //Fortest
                        if (ConfigurationManager.AppSettings["AttachmentJID"].IndexOf(JID) != -1)
                        {
                            ProcessMessage("Process start to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                            MailDetailOBJ.MailFrom = "thieme.j@thomsondigital.com";
                            MailDetailOBJ.MailCC = MailDetailOBJ.MailCC + ",praveen.s@thomsondigital.com";
                            if (File.Exists(MailDetailOBJ.MailAtchmnt[0]))
                            {
                                eMailProcessObj.SendMailExternal(MailDetailOBJ);
                                ProcessMessage("Process completed to send attachment");

                            }
                            else
                                ProcessMessage(MailDetailOBJ.MailAtchmnt[0] + ": File does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessMessage("Process fail to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                        ErrorNotification(ex);
                    }

                    //eMailProcessObj.SendMailInternal(MailDetailOBJ);
                    return true;
                }


            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
            }
            return false;
        }
        private bool FizMailProcess_test(MailDetail MailDetailObj, string _JTitle, string Volume, string Issue, string JID = "")
        {
            try
            {
                string _Subject = "Complimentary PDF of your article published in " + _JTitle + ", Volume " + Volume + ", Number " + Issue;

                string template = System.Configuration.ConfigurationManager.AppSettings["FIZMailTemplatePath"];
                StreamReader sr = new StreamReader(template);
                string mailBody = sr.ReadToEnd();
                string survey_txt = survey_text(JID);
                if (survey_txt!="")
                {
                    mailBody = mailBody.Replace("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.", "This PDF is for personal and educational purposes only and should not be distributed or printed commercially.</br></br>Help us enhance your publishing experience by taking this quick survey. Your feedback is very important to us.</br><a href=\"" + survey_txt+"\">"+survey_txt+ "</a>");
                }
                mailBody = mailBody.Replace("[Volume]", Volume);
                mailBody = mailBody.Replace("[Issue]", Issue);
                mailBody = mailBody.Replace("[JT]", _JTitle);
                if (_JTitle == "Journal of Neurological Surgery Part B")
                    mailBody = mailBody.Replace("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.", "This PDF is for personal and educational purposes only and should not be distributed or printed commercially. <\br>Please note that your article will get included in PubMed once published in an issue. Due to the increase in submissions to the journals, we are facing delays we are working hard to reduce the turnaround time and will include your paper as soon as possible.");


                MailDetailObj.MailSubject = _Subject;
                MailDetailObj.MailBody = mailBody.ToString();

                eMailProcess eMailProcessObj = new eMailProcess();
                if (eMailProcessObj.SendMailExternal(MailDetailObj))
                {
                    try
                    {
                        //Fortest
                        if (ConfigurationManager.AppSettings["AttachmentJID"].IndexOf(JID) != -1)
                        {
                            ProcessMessage("Process start to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                            MailDetailObj.MailFrom = "thieme.j@thomsondigital.com";
                            MailDetailObj.MailCC = MailDetailObj.MailCC + ",praveen.s@thomsondigital.com";
                            if (File.Exists(MailDetailObj.MailAtchmnt[0]))
                            {
                                eMailProcessObj.SendMailExternal(MailDetailObj);
                                ProcessMessage("Process completed to send attachment");
                            }
                            else
                                ProcessMessage(MailDetailObj.MailAtchmnt[0] + ": File does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessMessage("Process fail to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                        ErrorNotification(ex);
                    }

                    return true;
                }


            }
            catch (Exception ex)
            {

            }
            return false;
        }
        private bool EFirstMailProcess(MailDetail MailDetailOBJ, string JT, string JID = "")
        {
            try
            {
                string _Subject = "Complimentary PDF of your article published online in  " + JT;

                StringBuilder _Body = new StringBuilder();
                _Body.AppendLine("Dear Author,");
                _Body.AppendLine("");
                _Body.AppendLine("Attached please find a complimentary PDF of your article published online in " + JT);
                _Body.AppendLine("");
                _Body.AppendLine("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.");
                _Body.AppendLine("");
                if (JT == "Journal of Neurological Surgery Part B")
                    _Body.AppendLine("Please note that your article will get included in PubMed once published in an issue. Due to the increase in submissions to the journals, we are facing delays we are working hard to reduce the turnaround time and will include your paper as soon as possible.");
                _Body.AppendLine("");
                _Body.AppendLine("Thank you for your contribution to the journal.");
                _Body.AppendLine("");
                _Body.AppendLine("Regards,");
                _Body.AppendLine("");
                if (JID == "TH" || JID == "VCOT" || JID == "ACI")
                    _Body.AppendLine("Thomson Digital (on behalf of Thieme Medical Publishers)");
                else
                    _Body.AppendLine("Thomson Digital (on behalf of Thieme Medical Publishers)");

                MailDetailOBJ.MailBody = _Body.ToString();
                MailDetailOBJ.MailSubject = _Subject;

                eMailProcess eMailProcessObj = new eMailProcess();

                //ForTest
                if (eMailProcessObj.SendMailExternal(MailDetailOBJ))
                {
                    ProcessMessage("SendMailExternal Successfully");
                    try
                    {
                        //Fortest
                        if (ConfigurationManager.AppSettings["AttachmentJID"].IndexOf(JID) != -1)
                        {
                            ProcessMessage("Process start to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                            MailDetailOBJ.MailFrom = "thieme.j@thomsondigital.com";
                            MailDetailOBJ.MailCC = MailDetailOBJ.MailCC + ",praveen.s@thomsondigital.com";
                            if (File.Exists(MailDetailOBJ.MailAtchmnt[0]))
                            {
                                eMailProcessObj.SendMailExternal(MailDetailOBJ);
                                ProcessMessage("Process completed to send attachment");

                            }
                            else
                                ProcessMessage(MailDetailOBJ.MailAtchmnt[0] + ": File does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessMessage("Process fail to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                        ErrorNotification(ex);
                    }

                    //eMailProcessObj.SendMailInternal(MailDetailOBJ);
                    return true;
                }


            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
            }
            return false;
        }

        private bool FizMailProcess(MailDetail MailDetailObj, string _JTitle, string Volume, string Issue, string JID = "")
        {
            try
            {
                string _Subject = "Complimentary PDF of your article published in " + _JTitle + ", Volume " + Volume + ", Number " + Issue;

                StringBuilder _Body = new StringBuilder();

                _Body.AppendLine("Dear Author,");
                _Body.AppendLine("");
                _Body.AppendLine("Attached please find a complimentary PDF of your article published in print in Volume " + Volume + ", Number " + Issue + " of " + _JTitle);
                _Body.AppendLine("");
                _Body.AppendLine("This PDF is for personal and educational purposes only and should not be distributed or printed commercially.");
                _Body.AppendLine("");
                if (_JTitle == "Journal of Neurological Surgery Part B")
                    _Body.AppendLine("Please note that your article will get included in PubMed once published in an issue. Due to the increase in submissions to the journals, we are facing delays we are working hard to reduce the turnaround time and will include your paper as soon as possible.");
                _Body.AppendLine("");
                _Body.AppendLine("Thank you for your contribution to the journal.");
                _Body.AppendLine("");
                _Body.AppendLine("Regards,");
                if (JID == "TH" || JID == "VCOT" || JID == "ACI")
                    _Body.AppendLine("Thomson Digital (on behalf of Thieme Medical Publishers)");
                else
                    _Body.AppendLine("Thomson Digital (on behalf of Thieme Medical Publishers)");
                _Body.AppendLine("");


                MailDetailObj.MailSubject = _Subject;
                MailDetailObj.MailBody = _Body.ToString();

                eMailProcess eMailProcessObj = new eMailProcess();
                if (eMailProcessObj.SendMailExternal(MailDetailObj))
                {
                    try
                    {
                        //Fortest
                        if (ConfigurationManager.AppSettings["AttachmentJID"].IndexOf(JID) != -1)
                        {
                            ProcessMessage("Process start to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                            MailDetailObj.MailFrom = "thieme.j@thomsondigital.com";
                            MailDetailObj.MailCC = MailDetailObj.MailCC + ",praveen.s@thomsondigital.com";
                            if (File.Exists(MailDetailObj.MailAtchmnt[0]))
                            {
                                eMailProcessObj.SendMailExternal(MailDetailObj);
                                ProcessMessage("Process completed to send attachment");
                            }
                            else
                                ProcessMessage(MailDetailObj.MailAtchmnt[0] + ": File does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessMessage("Process fail to send attachment ::JID " + JID + " Aid " + _ThiemeOffPrintObj.AID);
                        ErrorNotification(ex);
                    }

                    return true;
                }


            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private string survey_text(string jid)
        {
            string survey = "";
            string[] lines=File.ReadAllLines(System.Windows.Forms.Application.StartupPath+"\\survey.txt");
            foreach (string line in lines)
            {
                if (line.Split('|')[0].ToString().Trim().ToLower()==jid.Trim().ToLower())
                {
                    survey= line.Split('|')[1].ToString();
                }
            }
            return survey;
        }
        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }

        private bool IgnoreList(string aid)
        {
            // var containsAll = ignorelist.All(x => eFirstDetailOBJ.AuthorInfoOBJ.AID.Contains(x));
            bool exist = false;
            string[] ignorelist = File.ReadAllLines(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\IgnoreArticleList.txt");
            foreach (string str in ignorelist)
            {
                if (aid.Contains(str))
                {
                    exist = true;
                    break;
                }
                else
                    continue;
            }
            return exist;
        }

    }
}
