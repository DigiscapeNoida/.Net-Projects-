using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using DatabaseLayer;
using PdfProcess;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using MsgRcvr;
using iTextSharp.text.pdf;

namespace AutoEproof
{
    class ReviseProcess : MessageEventArgs,IRValidation
    {

        bool _ValidationResult = false;
        bool _IsAuthorEMailWellForm = false;
        bool _isAlreadyProcessed = false;
        bool _IsMatchCorEmailXMLANDDB = false;
        bool _IsAuthorNameExist = false;
        bool _IsAuthorEmailExist = false;
        bool _isValidJID = false;
        bool _IsMatchDOI = false;
        bool _IsQueryIDExist = false;
        bool _IscPDFExist = false;
        bool _IsFTPUpload = false;
        string _AuthrCrcnPDFPath = string.Empty;
        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;

        string _OPSConStr  = string.Empty;
        string _MailBody   = string.Empty;
        OPSDetail         _OPSDetailObj          = null;
        MNTInfo           _MNT                   = null;
        OPSDB             _OPSDBObj              = null;
        OPSRevise         _OPSRvsObj             = null;

        public OPSDetail OPSDetailObj
        {
            get { return _OPSDetailObj; }
        }

        public OPSRevise OPSRvsObj
        {
            get { return _OPSRvsObj; }
        }

        public MNTInfo MNT
        {
            get { return _MNT; }
        }

       
        public ReviseProcess(MNTInfo MNT, string PDFPath)
        { 
            _MNT       = MNT;
            _PDFPath   = PDFPath;
            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj  = new OPSDB(_OPSConStr);
             StartValidation();
             
        }
        private void InitiallizeEditorInfo()
        {
          _OPSDetailObj    = _OPSDBObj.GetOPSDetails(_MNT.JID, _MNT.Client);
          _OPSRvsObj       = _OPSDBObj.GetReviseDetails(_OPSDetailObj.OPSID);
          _MailBody        = GetMailBody();
        }
       
        private bool CommentEnablePDF()
        {
            try
            {
                string[] args = new string[1];
                args[0] = _PDFPath;
                PDFAnnotation.Program.Main(args);
                //PDF security for Revised proof
                ProcessEventHandler("PDF security Start");
                if (PDFSecurity(_PDFPath, _PDFPath.Replace(".pdf", "1.pdf")))
                {
                    //System.Threading.Thread.Sleep(1000);
                    //ProcessEventHandler("PDF security END");
                    //args = new string[2];
                    //args[0] = _PDFPath.Replace(".pdf", "1.pdf");
                    //args[1] = "SaveACopyAEPS";
                    //PDFAnnotation.Program.Main(args);
                    //System.Threading.Thread.Sleep(500);
                    ProcessEventHandler("Save as end");
                }

                if (File.Exists(_PDFPath))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                IsPdfProcessError = true;
                base.ProcessErrorHandler(ex);
                return false;
            }
        }
        public bool IsPdfProcessError
        {
            get;
            set;
        }

        private bool ProcessMail()
        {
            MailDetail MailDetailOBJ = new MailDetail();
            MailDetailOBJ.MailFrom   = _OPSRvsObj.MailFrom;
            MailDetailOBJ.MailTo     = _OPSRvsObj.MailTo;
            MailDetailOBJ.MailCC     = _OPSRvsObj.MailCC;
            MailDetailOBJ.MailBCC    = _OPSRvsObj.MailBCC;

            MailDetailOBJ.MailSubject =  _MNT.JID + " " + _MNT.AID + " Revised Proofs" ;

            

            string RevPDFPath = string.Empty;

           
           if (_OPSRvsObj.EarlyView.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            
               RevPDFPath = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID.ToLower() + "_" + _MNT.AID + "_Rev" + "_EV.pdf";
           else
               RevPDFPath = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID.ToLower() + "_" + _MNT.AID + "_Rev" + ".pdf";

             File.Copy(_PDFPath,RevPDFPath);
             ProcessEventHandler("File Name : " + Path.GetFileName(RevPDFPath));
            _MailBody = _MailBody.Replace("<FILENAME>",Path.GetFileName(RevPDFPath));
            _MailBody = _MailBody.Replace("<JID>",     _MNT.JID);
            _MailBody = _MailBody.Replace("<AID>",    _MNT.AID);

            //ProcessEventHandler("Mail Body : " + _MailBody);
            MailDetailOBJ.MailBody = _MailBody;
            
           if (UploadArticleOnFtp(RevPDFPath))
           { 
           }
           else
           {
               MailDetailOBJ.MailAtchmnt.Add(RevPDFPath);
           }
            

            eMailProcess eMailProcessOBJ = new eMailProcess();
            eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
            eMailProcessOBJ.ErrorNotification   += this.ProcessErrorHandler;

            MailDetailOBJ.Stage = _MNT.Stage ;

            if (File.Exists(RevPDFPath))
            {
                FileInfo FI = new FileInfo(RevPDFPath);
                if ((FI.Length / 1024) / 1024 > 10)
                {
                    MailDetailOBJ.MailAtchmnt.Clear();
                    if (!string.IsNullOrEmpty(ConfigDetails.HeavyPDF))
                    {
                        if (!Directory.Exists(ConfigDetails.HeavyPDF))
                        {
                            Directory.CreateDirectory(ConfigDetails.HeavyPDF);
                        }
                        string FromCopy = RevPDFPath;
                        string ToCopy = ConfigDetails.HeavyPDF + "\\" + Path.GetFileName(RevPDFPath);
                        File.Copy(FromCopy, ToCopy);
                    }
                    _ValidationResult = false;
                    Remark = "File size exceed from 10 MB so find pdf file in HeavyPDF folder in eProof server";
                    return false;
                }
            }

            if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
            {
                ///////////Fortest
                //MailDetailOBJ.MailTo = _OPSDetailObj.FailEmail;
                //MailDetailOBJ.MailCC = string.Empty;
                //MailDetailOBJ.MailBCC = string.Empty;
                //if (!eMailProcessOBJ.SendMailInternal(MailDetailOBJ))
                //{
                //    System.Threading.Thread.Sleep(5000);
                //    eMailProcessOBJ.SendMailInternal(MailDetailOBJ);
                //}

                return true;
            }
            else
            {
                return false;
            }
        }
        private bool InsertReviseHistory()
        {

            try
            {
                //string AEPSJWConStr = ConfigurationManager.ConnectionStrings["AEPSConnectionString"].ConnectionString;
                //string InsertCmdString = "insert into File_History (Original,Customer,Stage,JID,AID,Final,Extras,MailTo,DateCreated,DateModified,Lock) values ('" + _MNT.JID + "-" + _MNT.AID + "','" + _MNT.Client + "','" + "REVISED" + "','" + _MNT.JID + "','" + _MNT.AID + "','" + _MNT.AID + "','UPLOAD','" + _OPSRvsObj.MailTo + "','" + DateTime.Now + "','" + DateTime.Now + "','LOCK')";
                //SqlHelper.ExecuteNonQuery(AEPSJWConStr, System.Data.CommandType.Text, InsertCmdString);


                usp_GetReviseHistoryResult RvsHstry = new usp_GetReviseHistoryResult();
                RvsHstry.AID = _MNT.AID;
                RvsHstry.CorrName = _OPSRvsObj.CorrName;
                RvsHstry.MailBCC = _OPSRvsObj.MailBCC;
                RvsHstry.MailCC = _OPSRvsObj.MailCC;
                RvsHstry.MailFrom = _OPSRvsObj.MailFrom;
                RvsHstry.MailTo = _OPSRvsObj.MailTo;
                RvsHstry.OPSID = _OPSRvsObj.OPSID;
                RvsHstry.RevisionType = "Revise";
                _OPSDBObj.InsertReviseHistory(RvsHstry);
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                return false;
            }

            return true;
        }
        private string GetMailBody()
        {

            string TemplatePath = ConfigDetails.EXELoc + "\\ReviseTemplate\\" + _MNT.JID + ".txt";

            if (!File.Exists(TemplatePath))
                TemplatePath = ConfigDetails.EXELoc + "\\ReviseTemplate\\Common.txt";

            if (!File.Exists(TemplatePath))
            {
                return string.Empty;
            }

            StringBuilder MailBody = new StringBuilder("");
            MailBody = new StringBuilder(File.ReadAllText(TemplatePath));
            if (_OPSRvsObj != null)
            {
                MailBody.Replace("<CorName>", _OPSRvsObj.CorrName);
            }

            return MailBody.ToString();
        }
        public bool Start()
        {
            if (File.Exists(_PDFPath.Replace(".pdf", "c.pdf")) && MNT.JID != "AJP")
            {
                try
                {
                    ProcessEventHandler("C PDF Exist for Revise stage");
                    PdfProcess.PDF PDFObj = new PDF(_PDFPath, MNT);
                    PDFObj.MergePDF(new string[2] { _PDFPath, _PDFPath.Replace(".pdf", "c.pdf") });
                    ProcessEventHandler("C PDF merge for revise stage");
                    System.Threading.Thread.Sleep(10000);
                    File.Delete(_PDFPath.Replace(".pdf", "c.pdf"));
                    if (PDFObj != null)
                        PDFObj = null;
                }
                catch (Exception ex)
                {
                    ProcessEventHandler(ex.Message);
                }
            }
            if (CommentEnablePDF())
            {
                if (InsertReviseHistory())
                {
                    if (ProcessMail())
                        return true;
                }
            }
            return false;
        }

        public void StartValidation()
        {
            InitiallizeEditorInfo();
            AssignValidationResult();
          
        }
        private void AssignValidationResult()
        {
            _isValidJID = _OPSRvsObj == null ? false : true;

            if (_isValidJID)
                _ValidationResult = _isValidJID;

            if (_ValidationResult)
                _ValidationResult = string.IsNullOrEmpty(_MailBody) ? false : true;
            else
            {
                return;
                ///////No need to validate further as Editor information does not exit;
            }

            if (_ValidationResult)
                _ValidationResult = string.IsNullOrEmpty(_PDFPath) ? false : true;

            if (_ValidationResult)
                _ValidationResult = File.Exists(_PDFPath) ? true : false;

            PdfPages = MNT.PdfPages;
            AutoPdfPages = MNT.PgCountLog;

            if (AutoPdfPages == PdfPages)
                IsPdfPagesEqualAutopage = true;
            else if (PdfPages > AutoPdfPages)
                IsPdfPagesEqualAutopage = false;
            else if (AutoPdfPages > 0 && (PdfPages >= AutoPdfPages - 2))
                IsPdfPagesEqualAutopage = true;
            else if (AutoPdfPages == 0)
                IsPdfPagesEqualAutopage = true;

            _IsAuthorEmailExist    = string.IsNullOrEmpty(_OPSRvsObj.MailTo) ? false : true;
            _IsAuthorNameExist     = string.IsNullOrEmpty(_OPSRvsObj.CorrName) ? false : true;
            _IsAuthorEMailWellForm = CheckEmail(_OPSRvsObj.MailTo);
            _isAlreadyProcessed    = _OPSDBObj.GetReviseHistoryCount(_OPSRvsObj.OPSID, _MNT.AID) > 0 ? true : false;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorEmailExist;

            if (_ValidationResult)
                _ValidationResult = _isAlreadyProcessed ? false : true;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorEmailExist;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorEMailWellForm;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorNameExist;

            if (_ValidationResult)
                _ValidationResult = IsPdfPagesEqualAutopage;
            ////////////To check if pdf page has line no then failed it
            PdfProcess.PDF PDFObj = new PdfProcess.PDF(_PDFPath);
            if (PDFObj.isPdfLineStartWithNumber())
            {
                _ValidationResult = false;
                Remark = "Revised's pdf is same as fresh pdf.";
            }

            return;

            
        }
        private bool CheckEmail(string _CorrEmail)
        {
            if (_CorrEmail != null)
            {
                if (_CorrEmail.Contains(","))
                {
                    return true;
                }
                else if (!(new Regex(@"^['a-zA-Z0-9_\-\.]+@[a-zA-Z0-9_\-\.]+\.[a-zA-Z]{2,}$")).IsMatch(_CorrEmail))
                {
                    _IsAuthorEMailWellForm = false;
                    return false;
                }
                else
                    return true;
            }
            else
                return false;

        }

        private void DownloadArticleFromFMS()
        {
            string ExtractTo = "C:\\Temp\\" + _MNT.JID + _MNT.AID;

            if (Directory.Exists(ExtractTo))
            {
                Directory.Delete(ExtractTo, true);
            }
            Directory.CreateDirectory(ExtractTo);

            string LocalZip = ExtractTo + "\\Rvs.zip";

            byte[] b1 = null;
            OPSServise.OPSService SrvcObj = new OPSServise.OPSService();

            b1 = SrvcObj.GetRvsFile(_MNT.Client, _MNT.JID, _MNT.AID);

            FileStream fs1 = null;
            fs1 = new FileStream(LocalZip, FileMode.Create);

            fs1.Write(b1, 0, b1.Length);
            fs1.Close();
            fs1 = null;

            UNzipfile(LocalZip, ExtractTo);
        }

        private void UNzipfile(string ZipPath,string ExtractTo)
        {
            if (!string.IsNullOrEmpty(ZipPath))
            {
                FastZip fz = new FastZip();
                fz.ExtractZip(ZipPath, ExtractTo, null);
                string[] PdfFiles = Directory.GetFiles(ExtractTo, "*.pdf");
                if (PdfFiles.Length == 1)
                {
                    _AuthrCrcnPDFPath = PdfFiles[0];
                }
            }
        }
        private bool UploadArticleOnFtp(string _UploadFileName)
        {
            string FTPURL = string.Empty;
            string Uname = string.Empty;
            string PWD = string.Empty;

            if (_OPSRvsObj.FtpUpload!= null &&   _OPSRvsObj.FtpUpload.Equals("YES"))
            {
                if (!string.IsNullOrEmpty(_OPSRvsObj.FtpPath) && !string.IsNullOrEmpty(_OPSRvsObj.FtpUID) && !string.IsNullOrEmpty(_OPSRvsObj.FtpUID))
                {
                    FTPURL = _OPSRvsObj.FtpPath;
                    Uname = _OPSRvsObj.FtpUID;
                    PWD = _OPSRvsObj.ftpPWD;
                }
                else
                    return false;

            }
            else
                return false;

            try
            {
                //LogStr.Add("Upload to FTP");
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);
                FtpObj.ProcessNotification += ProcessEventHandler;
                FtpObj.ErrorNotification += ProcessErrorHandler;


                if (FtpObj.FtpDirectoryExists(FTPURL) == false)
                {
                    FtpObj.CreateFtpFolder(FTPURL);
                }
                if (FtpObj.UploadFileToFTP(_UploadFileName) == false)
                {
                    FtpObj.UploadFileToFTP(_UploadFileName);
                }
                _IsFTPUpload = true;
                //LogStr.Add("File moved to backup");
            }
            catch (Exception ex)
            {
                _IsFTPUpload = false;
                base.ProcessErrorHandler(ex);
                //LogStr.Add("Error while uploaing to FTP..");
                //LogStr.Add("Error Message :: " + ex.Message);
            }

            return true;
        }

        private bool UploadAuthorCorrectionOnFtp()
        {
            string UploadFileName = string.Empty;
            string FTPURL = string.Empty;
            string Uname = string.Empty;
            string PWD = string.Empty;

            if (_OPSRvsObj.FtpAuthCrcnt != null && _OPSRvsObj.FtpAuthCrcnt.Equals("YES"))
            {
                if (!string.IsNullOrEmpty(_OPSRvsObj.FtpAuthCrcntPath) && !string.IsNullOrEmpty(_OPSRvsObj.FtpUID) && !string.IsNullOrEmpty(_OPSRvsObj.FtpUID))
                {

                    DownloadArticleFromFMS();
                    if (string.IsNullOrEmpty(_AuthrCrcnPDFPath) && File.Exists(_AuthrCrcnPDFPath))
                    {
                        UploadFileName = _AuthrCrcnPDFPath;
                        FTPURL = _OPSRvsObj.FtpAuthCrcntPath;
                        Uname = _OPSRvsObj.FtpUID;
                        PWD = _OPSRvsObj.ftpPWD;
                    }
                }
                else
                    return false;

            }
            else
                return false;

            try
            {
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);
                FtpObj.ProcessNotification += ProcessEventHandler;
                FtpObj.ErrorNotification += ProcessErrorHandler;

                if (FtpObj.FtpDirectoryExists(FTPURL) == false)
                {
                    FtpObj.CreateFtpFolder(FTPURL);
                }
                FtpObj.UploadFileToFTP(UploadFileName);

            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                //LogStr.Add("Error while uploaing to FTP..");
                //LogStr.Add("Error Message :: " + ex.Message);
            }

            return true;
        }

        public bool IsValidJID
        {
            get
            {
                return _isValidJID;
            }
            set
            {
                _isValidJID = value;
            }
        }
        public bool IsAuthorNameExist
        {
            get
            {
                return _IsAuthorNameExist;
            }
            set
            {
                _IsAuthorNameExist = value;
            }
        }
        public bool IsAuthorEmailExist
        {
            get
            {
                return _IsAuthorEmailExist;
            }
            set
            {
                _IsAuthorEmailExist = value;
            }
        }
        public bool IsAlreadyProcessed
        {
            get
            {
                return _isAlreadyProcessed;
            }
            set
            {
                _isAlreadyProcessed = value;
            }
        }
        public bool IsMatchCorEmailXMLANDDB
        {
            get
            {
                return _IsMatchCorEmailXMLANDDB;
            }
            set
            {
                _IsMatchCorEmailXMLANDDB = value;
            }
        }
        public bool IsMatchDOI
        {
            get
            {
                return _IsMatchDOI;
            }
            set
            {
                _IsMatchDOI = value;
            }
        }
        public bool IsQueryIDExist
        {
            get
            {
                return _IsQueryIDExist;
            }
            set
            {
                _IsQueryIDExist = value;
            }
        }
        public bool IscPDFExist
        {
            get
            {
                return _IscPDFExist;
            }
            set
            {
                _IscPDFExist = value;
            }
        }
        
        public bool ValidationResult
        {
            get
            {
                return _ValidationResult;
            }
            set
            {
                _ValidationResult = value;
            }
        }
        public bool IsAuthorEMailWellForm
        {
            get
            {
                return _IsAuthorEMailWellForm;
            }
            set
            {
                _IsAuthorEMailWellForm = value;
            }
        }
        public string Remark
        {
            get;
            set;
        }
        public string PDFPath
        {
            get { return _PDFPath; }
        }
        public string XMLPath
        {
            get { return _XMLPath; }
        }


        public int PdfPages
        {
            get;
            set;
        }

        public int AutoPdfPages
        {
            get;
            set;
        }
        public bool IsPdfPagesEqualAutopage
        {
            get;
            set;
        }
        private bool PDFSecurity(string source, string destination)
        {
            bool result = false;
            PdfReader reader = null;
            PdfStamper stamper=null;
                try
                {
                    reader = new PdfReader(source);
                    reader.RemoveUsageRights();
                    stamper = new PdfStamper(reader, new System.IO.FileStream(destination, System.IO.FileMode.CreateNew));
                    stamper.FormFlattening = true;
                    stamper.SetEncryption(null, System.Text.Encoding.UTF8.GetBytes("Th0MsOnD123"), PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_FILL_IN | PdfWriter.ALLOW_MODIFY_ANNOTATIONS | PdfWriter.ALLOW_COPY, PdfWriter.DO_NOT_ENCRYPT_METADATA);
                    stamper.Close();
                    reader.Close();
                    if (File.Exists(source))
                    {
                        File.Delete(source);
                        System.Threading.Thread.Sleep(10000);
                    }
                    File.Move(destination, source);
                    System.Threading.Thread.Sleep(5000);
                    File.Delete(destination);
                    reader = new PdfReader(source);

                    if (!reader.IsEncrypted())
                    {
                        ProcessEventHandler("PDF is not encrypted ");
                        return false;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    ProcessEventHandler("Error in PDF Security: " + ex.Message);
                    return false;
                }
               
                result = true;
           
            return result;
        }
    }
}
