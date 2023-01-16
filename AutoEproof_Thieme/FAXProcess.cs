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
using MsgRcvr;
using iTextSharp.text.pdf;

namespace AutoEproof
{
    class FAXProcess : MessageEventArgs,IRValidation
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
        bool _IsFTPArticle = false;
        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;

        string _OPSConStr = string.Empty;
        string _MailBody = string.Empty;
        OPSDetail         _OPSDetailObj          = null;
        MNTInfo           _MNT                   = null;
        OPSDB             _OPSDBObj              = null;
        OPSFAX            _OPSFAXObj             = null;

        public OPSDetail OPSDetailObj
        {
            get { return _OPSDetailObj; }
        }

        public OPSFAX OPSFAXObj
        {
            get { return _OPSFAXObj; }
        }

        public MNTInfo MNT
        {
            get { return _MNT; }
        }


        public FAXProcess(MNTInfo MNT, string PDFPath )
        {
            _MNT       = MNT;
            _PDFPath   = PDFPath;
            _XMLPath = string.Empty;
            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj  = new OPSDB(_OPSConStr);
            StartValidation();
        }
        
        private void InitiallizeEditorInfo()
        {
          _OPSDetailObj    = _OPSDBObj.GetOPSDetails(_MNT.JID, _MNT.Client);
          _OPSFAXObj       = _OPSDBObj.GetFAXDetails(_OPSDetailObj.OPSID);
          _MailBody        = GetMailBody();
        }
        private bool CommentEnablePDF()
        {
            try
            {
                string[] args = new string[1];
                args[0] = _PDFPath;
                PDFAnnotation.Program.Main(args);
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
                    ProcessEventHandler("Save as END");
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
        private void ProcessMail()
        {
            MailDetail MailDetailOBJ = new MailDetail();
            MailDetailOBJ.MailFrom   = _OPSFAXObj.MailFrom;
            MailDetailOBJ.MailTo     = _OPSFAXObj.MailTo;
            MailDetailOBJ.MailCC     = _OPSFAXObj.MailCC;
            MailDetailOBJ.MailBCC    = _OPSFAXObj.MailBCC;

            MailDetailOBJ.MailSubject =  _MNT.JID + " " + _MNT.AID + " Revised Proofs" ;
           

            int RevCount       = _OPSDBObj.GetReviseHistoryCount( _OPSFAXObj.OPSID, _MNT.AID);
            string  RevPDFPath = string.Empty;

            if (_OPSFAXObj.EarlyView.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                // RevCount++;   //Updated by Kumar Sonu for Fax PDF numbering
                RevPDFPath = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID + "_" + _MNT.AID + "_Rev" + RevCount.ToString() + "_EV.pdf";
            }
            else
            {
                if (_OPSFAXObj.OPSDetail.Jid == "AERE")   //Add  on 01 May 2018 by Pradeep as per mail 30/04/2018 by Rajina 
                    RevPDFPath = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID + "_" + _MNT.AID + "_Rev" + RevCount.ToString() + ".pdf";
                else
                RevPDFPath = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID + "_" + _MNT.AID + "_Rev" + ".pdf";
            }
            File.Copy(_PDFPath, RevPDFPath);


            ProcessEventHandler("File Name : " + Path.GetFileName(RevPDFPath));
            _MailBody = _MailBody.Replace("<FILENAME>", Path.GetFileName(RevPDFPath));
            _MailBody = _MailBody.Replace("<JID>", _MNT.JID);
            _MailBody = _MailBody.Replace("<AID>", _MNT.AID);

            //ProcessEventHandler("Mail Body: "+_MailBody.ToString());
            MailDetailOBJ.MailBody = _MailBody;
            MailDetailOBJ.Stage = _MNT.Stage;

            eMailProcess eMailProcessOBJ         = new eMailProcess();
            eMailProcessOBJ.ProcessNotification += this.ProcessEventHandler;
            eMailProcessOBJ.ErrorNotification   += this.ProcessErrorHandler;


            if (UploadArticleOnFtp(RevPDFPath))
            {
            }
            else
            {
                MailDetailOBJ.MailAtchmnt.Add(RevPDFPath);
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
                        return;
                    }
                }
            }

            //=============to stop ftp upload failed but mail sent
            if (_OPSFAXObj.FtpUpload != null && _OPSFAXObj.FtpUpload.Equals("YES"))
            {
                if (!string.IsNullOrEmpty(_OPSFAXObj.FtpPath) && !string.IsNullOrEmpty(_OPSFAXObj.FtpUID) && !string.IsNullOrEmpty(_OPSFAXObj.FtpUID))
                {
                    if (_IsFTPArticle)
                    {
                        eMailProcessOBJ.SendMailExternal(MailDetailOBJ);
                        ProcessEventHandler("File upload on FTP");
                    }
                    else
                        ProcessEventHandler("File uploadation failed on FTP");
                }
            }

            else if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
            {
                /////////Fortest
                MailDetailOBJ.MailTo    = _OPSDetailObj.FailEmail;
                MailDetailOBJ.MailCC    = string.Empty;
                MailDetailOBJ.MailBCC   = string.Empty;
                //eMailProcessOBJ.SendMailInternal(MailDetailOBJ);
            }
        }

        void eMailProcessOBJ_ErrorNotification(Exception Ex)
        {
            this.ProcessErrorHandler(Ex);
        }

        void eMailProcessOBJ_ProcessNotification(string NotificationMsg)
        {
            this.ProcessEventHandler(NotificationMsg);
        }
        private bool InsertReviseHistory()
        {
            try
            {
                //string AEPSJWConStr = ConfigurationManager.ConnectionStrings["AEPSConnectionString"].ConnectionString;
                //string InsertCmdString = "insert into File_History (Original,Customer,Stage,JID,AID,Final,Extras,MailTo,DateCreated,DateModified,Lock) values ('" + _MNT.JID + "-" + _MNT.AID + "','" + _MNT.Client + "','" + "FAX" + "','" + _MNT.JID + "','" + _MNT.AID + "','" + _MNT.AID + "','UPLOAD','" + _OPSFAXObj.MailTo + "','" + DateTime.Now + "','" + DateTime.Now + "','LOCK')";
                //SqlHelper.ExecuteNonQuery(AEPSJWConStr, System.Data.CommandType.Text, InsertCmdString);

                usp_GetReviseHistoryResult RvsHstry = new usp_GetReviseHistoryResult();
                RvsHstry.AID = _MNT.AID;
                RvsHstry.CorrName = _OPSFAXObj.CorrName;
                RvsHstry.MailBCC = _OPSFAXObj.MailBCC;
                RvsHstry.MailCC = _OPSFAXObj.MailCC;
                RvsHstry.MailFrom = _OPSFAXObj.MailFrom;
                RvsHstry.MailTo = _OPSFAXObj.MailTo;
                RvsHstry.OPSID = _OPSFAXObj.OPSID;
                RvsHstry.RevisionType = "FAX";
                _OPSDBObj.InsertReviseHistory(RvsHstry);
                return true;
            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }

            return false;
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
            if (_OPSFAXObj != null)
            {
                MailBody.Replace("<CorName>", _OPSFAXObj.CorrName);
            }

            return MailBody.ToString();
        }
        public void Start()
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
                    ProcessMail();
                }
            }
        }

        public void StartValidation()
        {
            InitiallizeEditorInfo();
            AssignValidationResult();
        }
        private void AssignValidationResult()
        {
            _isValidJID = _OPSFAXObj == null ? false : true;

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

             PdfPages     = MNT.PdfPages;
             AutoPdfPages = MNT.PgCountLog;



             if (AutoPdfPages == PdfPages)
                 IsPdfPagesEqualAutopage = true;
             else if(PdfPages > AutoPdfPages)
                 IsPdfPagesEqualAutopage = false;
             else if (AutoPdfPages > 0 && (PdfPages>=AutoPdfPages-2 ))
                 IsPdfPagesEqualAutopage = true;
             else if (AutoPdfPages == 0)
                 IsPdfPagesEqualAutopage = true;

            _IsAuthorEmailExist    = string.IsNullOrEmpty(_OPSFAXObj.MailTo) ? false : true;
            _IsAuthorNameExist     = string.IsNullOrEmpty(_OPSFAXObj.CorrName) ? false : true;
            _IsAuthorEMailWellForm = CheckEmail(_OPSFAXObj.MailTo);

            if (_MNT.Stage.Equals("S275RESUPPLY", StringComparison.OrdinalIgnoreCase))
                _isAlreadyProcessed = _OPSDBObj.GetReviseHistoryCount(_OPSFAXObj.OPSID, _MNT.AID) > 2 ? true : false;
            else
                _isAlreadyProcessed = _OPSDBObj.GetReviseHistoryCount(_OPSFAXObj.OPSID, _MNT.AID) > 6 ? true : false;

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

            PdfProcess.PDF PDFObj = new PdfProcess.PDF(_PDFPath);
            if (PDFObj.isPdfLineStartWithNumber())
            {
                _ValidationResult = false;
                Remark = "Revised's pdf is same as fresh pdf.";
            }


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
        private bool UploadArticleOnFtp(string _UploadFileName)
        {
            string FTPURL = string.Empty;
            string Uname = string.Empty;
            string PWD = string.Empty;

            if (_OPSFAXObj.FtpUpload != null && _OPSFAXObj.FtpUpload.Equals("YES"))
            {
                if (!string.IsNullOrEmpty(_OPSFAXObj.FtpPath) && !string.IsNullOrEmpty(_OPSFAXObj.FtpUID) && !string.IsNullOrEmpty(_OPSFAXObj.FtpUID))
                {
                    FTPURL = _OPSFAXObj.FtpPath;
                    Uname = _OPSFAXObj.FtpUID;
                    PWD = _OPSFAXObj.ftpPWD;
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
                _IsFTPArticle = true;
                //LogStr.Add("File moved to backup");
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                _IsFTPArticle = false;
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
            PdfReader reader;
            PdfStamper stamper;
            try
            {
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
