using System;
using PdfProcess;
using System.Xml;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DatabaseLayer;
using MsgRcvr;
using ProcessNotification;
namespace AutoEproof
{
    class eProofArticleInfo : MessageEventArgs, IValidation
    {
        MNTInfo MNT = null;
        ArticleInfo _ArticleInfoOBJ = null;
        InputFiles _InputFiles = new InputFiles();

        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;

        bool _ValidationResult = false;
        bool _IsAuthorEMailWellForm = false;
        bool _IsACRJID = false;
        bool _isAlreadyProcessed = false;
        bool _IsAuthorNameExist = false;
        bool _IsMatchCorEmailXMLANDDB = false;
        bool _IsMatchDOI = false;
        bool _IsAuthorEmailExist = false;
        bool _IsArticleTitleExist = false;
        bool _isValidJID = false;
        bool _IsQueryIDExist = false;
        bool _IscPDFExist = false;
        bool _IsAIJMEditor = false;

        string _InputPDF = string.Empty;
        string _JID = string.Empty;
        string _AID = string.Empty;
        string _UploadPath = string.Empty;
        string _URI = string.Empty;
        string _EncryptedName = string.Empty;
        string _AuthorName = string.Empty;
        string _ACRAuthorName = string.Empty;
        string _ArticleTitle = string.Empty;
        string _ReviewPDFName = string.Empty;
        string _ReviewerEmail = string.Empty;
        string _PDFNAME = string.Empty;
        string _CorrEmail = string.Empty;
        string _CCEmail = string.Empty;
        string _ACRCorMail = string.Empty;
        string _PEName = string.Empty;
        string _PEMail = string.Empty;
        string _EditorName = string.Empty;
        OPSDB _OPSDB = new OPSDB();
        OPSDetail _OPSDetailOBJ = null;



        bool CheckOPSJID(string JID)
        {
            string _AEPSConStr = ConfigurationManager.ConnectionStrings["AEPSJWConnectionString"].ConnectionString;
            string SQLStr = "select JID from OPSJIDLIST where JID='" + JID + "'";

            System.Data.SqlClient.SqlDataReader DR = SqlHelper.ExecuteReader(_AEPSConStr, SQLStr);

            if (DR.HasRows)
            {
                DR.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ArticleInfo ArticleInfoOBJ
        {
            get { return _ArticleInfoOBJ; }
        }

        public OPSDetail JIDOPSDetail
        {
            get { return _OPSDetailOBJ; }
        }
        public int PdfSize
        {
            get;
            set;
        }

        public bool CheckAuthorEmail()
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
        public bool CheckEMailWellForm(string Email)
        {
            if (Email != null)
            {
                if (Email.Contains(","))
                {
                    return true;
                }
                else if (!(new Regex(@"^['a-zA-Z0-9_\-\.]+@[a-zA-Z0-9_\-\.]+\.[a-zA-Z]{2,}$")).IsMatch(_CorrEmail))
                {

                    return false;
                }
                else
                    return true;
            }
            else
                return false;

        }
        public string InputPDF
        {
            get { return _InputPDF; }
        }
        public string ReviewPDFName
        {
            get;
            set;
        }
        public string GetFileSize(string FilePath)
        {
            long ByteSize = new FileInfo(FilePath).Length;
            decimal DivResult;
            if (ByteSize < 1024)
                return ByteSize + " Bytes";
            else if (ByteSize >= 1024 && ByteSize < 1048576)
            {
                DivResult = (decimal)ByteSize / 1024;
                return Math.Round(DivResult, 2) + " KB";
            }
            else if (ByteSize >= 1048576)
            {
                DivResult = (decimal)(ByteSize / 1024) / 1024;
                return Math.Round(DivResult, 2) + " MB";
            }
            return ByteSize + " Bytes";
        }
        public string ACRCorMail
        {
            get { return _ACRCorMail; }
        }
        public string AuthorName
        {
            get { return _AuthorName; }
            set { _AuthorName = value; }
        }
        public string ArticleTitle
        {
            get { return _ArticleTitle; }
            set { _ArticleTitle = value; }
        }
        public string Stage
        {
            get;
            set;
        }
        public string AuthorEMail
        {
            set
            {
                _CorrEmail = value;
            }
            get
            {
                _CorrEmail = string.IsNullOrEmpty(_CorrEmail) ? "" : _CorrEmail.Trim();
                return _CorrEmail.Trim();
            }
        }
        public string MainReviewerRole
        {
            get;
            set;
        }
        public string MainReviewerQID
        {
            get;
            set;
        }
        public string JID
        {
            get
            {
                return _JID;
            }

            set { _JID = value; }
        }
        public string AID
        {
            get
            {
                return _AID;
            }
            set { _AID = value; }

        }
        public string SystemIPAddress
        {
            get;
            set;
        }
        public string Client
        {
            get;
            set;
        }

        public string CCMail
        {
            get
            {
                return _CCEmail;
            }
            set
            {
                _CCEmail = value;
            }
        }

        public string ReviewerEmail
        {
            get
            {
                return _ReviewerEmail;
            }
            set
            {
                _ReviewerEmail = value;
            }
        }
        public string PEName
        {
            get
            {
                return _PEName;
            }
            set
            {
                _PEName = value;
            }
        }
        public string PEMail
        {
            get
            {
                return _PEMail;
            }
            set
            {
                _PEMail = value;
            }
        }
        public string URI
        {
            get
            {
                return _URI;
            }
            set
            {
                _URI = value;
            }
        }
        public string UploadPath
        {
            get
            {
                if (_UploadPath.Equals(""))
                {
                    if (_UploadPath.Equals(""))
                    {

                        _UploadPath = "OPSProofs/" + ReviewPDFName;

                    }
                }
                return _UploadPath;
            }
            set
            {
                _UploadPath = value;
            }
        }
        public string EncryptedName
        {
            get
            {
                if (_EncryptedName.Equals(""))
                {
                    _EncryptedName = GetEncryptedString();
                }
                return _EncryptedName;
            }
            set
            {
                _EncryptedName = value;
            }
        }

        public string EditorName
        {
            get
            {
                return _EditorName;
            }
            set
            {
                _EditorName = value;
            }
        }
        public string ReviewrEncryptedString(string RvwrName)
        {
            string base64String = "";
            byte[] binaryData;
            try
            {
                string DateTimeStr = RvwrName + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString();

                DateTimeStr = DateTimeStr.Replace("/", "").Replace(" ", "");
                UTF8Encoding str = new UTF8Encoding();
                binaryData = str.GetBytes(DateTimeStr);
                base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                base64String = base64String.Replace("=", "");
            }
            catch (System.ArgumentNullException)
            {
                return "";
            }
            return base64String;
        }
        public string GetEncryptedString()
        {
            string base64String = "";
            byte[] binaryData;
            try
            {
                UTF8Encoding str = new UTF8Encoding();
                binaryData = str.GetBytes(Client + Stage + _JID + _AID);
                base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);

                string DateTimeStr = DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString();
                DateTimeStr = DateTimeStr.Replace("/", "").Replace(":", "").Replace(" ", "");

                binaryData = str.GetBytes(DateTimeStr);
                base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                base64String = base64String.Replace("=", "");
            }
            catch (System.ArgumentNullException)
            {
                return "";
            }
            return base64String;
        }


        public eProofArticleInfo(MNTInfo MNT, InputFiles InputFiles_)
        {
            string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDB = new OPSDB(_OPSConStr);


            _InputFiles = InputFiles_;
            _JID = MNT.JID;
            _AID = MNT.AID;
            Stage = MNT.Stage;
            Client = MNT.Client;
            _InputPDF = _InputFiles.PDFPath;

            this.MNT = MNT;
            _PDFPath = InputFiles_.PDFPath;
            _XMLPath = InputFiles_.XMLPath;

        }
        private void InitiallizeProcess()
        {
            InitiallizeAuthorInfo();
            InializeFileInfo();
        }
        private void InitiallizeAuthorInfo()
        {
            string _XMLPath = _InputFiles.XMLPath;
            ArticleXMLProcess ArticleXMLProcessOBJ = new ArticleXMLProcess(_XMLPath);
            _ArticleInfoOBJ = ArticleXMLProcessOBJ.ArticleInfoOBJ;


            _OPSDetailOBJ = _OPSDB.GetOPSDetails(_JID, Client);

            if (_OPSDetailOBJ == null)
                _isValidJID = false;
            else
            {

                MailFrom = _OPSDetailOBJ.FromMail;
                //MailCC         = _OPSDetailOBJ.CCMail;

                if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    MailCC = _ArticleInfoOBJ.CorEmailCC + "," + _OPSDetailOBJ.CCMail;
                else
                    MailCC = _OPSDetailOBJ.CCMail;


                if (!string.IsNullOrEmpty(_CCEmail) && CheckEMailWellForm(_CCEmail))
                {
                    if (string.IsNullOrEmpty(MailCC))
                        MailCC = _CCEmail;
                    else
                        MailCC = MailCC + "," + _CCEmail;
                }
                MailBCC = _OPSDetailOBJ.BccMail;
                Role = _OPSDetailOBJ.Role;
                _ReviewerEmail = _OPSDetailOBJ.ReviewerEmail;
                _PEName = _OPSDetailOBJ.Peditor;
                _PEMail = _OPSDetailOBJ.Pe_email;
            }

            if (_ArticleInfoOBJ != null)
            {
                _CorrEmail = _ArticleInfoOBJ.AuthorEmail;

                if (string.IsNullOrEmpty(_ArticleInfoOBJ.ArticleTitle) == false)
                    _ArticleTitle = _ArticleInfoOBJ.ArticleTitle;

                _AuthorName = _ArticleInfoOBJ.AuthorName;

                CorAuthorDetaill CorDetaill = _OPSDB.GetCorAuthorDetaill(MNT.JID, MNT.AID);

                if (CorDetaill != null)//////////////Over Write from database entry 
                {
                    // _ArticleInfoOBJ = new ArticleInfo();

                    if (string.IsNullOrEmpty(_ArticleTitle))
                    {
                        _ArticleInfoOBJ.ArticleTitle = CorDetaill.Title;
                        _ArticleTitle = CorDetaill.Title;
                    }

                    if (!string.IsNullOrEmpty(CorDetaill.CorMailCC))
                        _ArticleInfoOBJ.CorEmailCC = CorDetaill.CorMailCC;

                    if (!string.IsNullOrEmpty(CorDetaill.CorMail))
                        _ArticleInfoOBJ.AuthorEmail = CorDetaill.CorMail;

                    if (!string.IsNullOrEmpty(CorDetaill.CorName))
                        _ArticleInfoOBJ.AuthorName = CorDetaill.CorName;

                    //For Email Check
                    //if (_ArticleInfoOBJ.AuthorEmail != null)
                    //{
                    //    string[] emailIDofXML = _ArticleInfoOBJ.AuthorEmail.Split(',');
                    //    if (!string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorEmail) && emailIDofXML.Length > 0)
                    //    {
                    //        foreach (string email in emailIDofXML)
                    //        {
                    //            if (!string.IsNullOrEmpty(email))
                    //            {
                    //                if (CorDetaill.CorMail == email.Trim())
                    //                {
                    //                    _IsMatchCorEmailXMLANDDB = true;
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    _IsMatchCorEmailXMLANDDB = false;
                    //                    continue;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //        _IsMatchCorEmailXMLANDDB = true;
                    //}

                    //if(!string.IsNullOrEmpty(CorDetaill.CorMail))
                    //_CorrEmail = CorDetaill.CorMail;
                    _CorrEmail = _ArticleInfoOBJ.AuthorEmail;
                    _AuthorName = _ArticleInfoOBJ.AuthorName;
                    _CCEmail = CorDetaill.CorMailCC;
                    _ArticleInfoOBJ.AID = AID;

                }
            }

            if (!string.IsNullOrEmpty(_CorrEmail) && _CorrEmail.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
            {
                _CorrEmail = string.Empty;
            }

            string BlockedJID = ConfigurationManager.AppSettings["BlockedJID"];
            if (BlockedJID.IndexOf("#" + JID + "#", StringComparison.OrdinalIgnoreCase) != -1)
            {
                _CorrEmail = string.Empty;
            }

            PdfPages = MNT.PdfPages;
            AutoPdfPages = MNT.PgCountLog;


            if (PdfPages == AutoPdfPages)
                IsPdfPagesEqualAutopage = true;

            _IsAuthorEmailExist = string.IsNullOrEmpty(_CorrEmail) ? false : true;
            _IsArticleTitleExist = string.IsNullOrEmpty(_ArticleTitle) ? false : true;
            _IsAuthorNameExist = string.IsNullOrEmpty(_AuthorName) ? false : true;
            _IsAuthorEMailWellForm = CheckAuthorEmail();
            _isAlreadyProcessed = _OPSDB.CheckReViewExistence(_OPSDetailOBJ.Jid, AID) > 0 ? true : false;
            _isValidJID = _OPSDetailOBJ == null ? false : true;
            if (JID == "AJIM")
            {
                _IsAIJMEditor = string.IsNullOrEmpty(_ArticleInfoOBJ.AJIMEditor) ? false : true;
                _EditorName = _ArticleInfoOBJ.AJIMEditor;
            }
            if (ConfigurationManager.AppSettings["FMSJIDList"].IndexOf(JID) != -1)
            {
                if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + AID + ".xml"))
                {
                    IsQueryIDExist = true;
                    IscPDFExist = true;
                }
                else
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + "-" + AID + "Q.pdf"))
                    {
                        IsQueryIDExist = true;
                        IscPDFExist = true;  //C PDF not exist for these Journals 
                    }
                    else
                        IsQueryIDExist = false;
                }
            }
            else
            {
                if (_ArticleInfoOBJ.IsQuerypage)
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + "-" + AID + "Q.pdf"))
                        _IsQueryIDExist = true;
                    else
                    {
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + Client + "\\JOURNAL\\" + JID + "\\" + AID + "\\WorkArea\\" + MNT.MNTFolder + "\\" + AID + "Q.pdf";
                        if (File.Exists(Path))
                        {
                            File.Move(Path, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + "-" + AID + "Q.pdf");
                            _IscPDFExist = true;
                            System.Threading.Thread.Sleep(5000);
                        }
                        else
                            _IscPDFExist = false;
                    }
                }
                else
                    _IsQueryIDExist = true;

                if (_ArticleInfoOBJ.IsgraphicalAbs)
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + "-" + AID + "c.pdf"))
                        _IscPDFExist = true;
                    else
                    {
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + Client + "\\JOURNAL\\" + JID + "\\" + AID + "\\WorkArea\\" + MNT.MNTFolder + "\\" + AID + "c.pdf";
                        if (File.Exists(Path))
                        {
                            File.Move(Path, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + JID + AID + "\\" + JID + "-" + AID + "c.pdf");
                            _IscPDFExist = true;
                            System.Threading.Thread.Sleep(5000);
                        }
                        else
                            _IscPDFExist = false;
                    }
                }
                else
                    _IscPDFExist = true;
            }
            AssignACREmail();
        }
        private void AssignACREmail()
        {
            if (_OPSDB.CheckACRJIDExistence(JID) > 0)
            {
                _IsACRJID = true;
                ArticleContentReport ACR = _OPSDB.GetACRDetails(JID, AID);
                if (ACR != null)
                {
                    string[] ProdStr = "prod ed#proded#Ed , Prod#Prod , Ed".Split('#');
                    bool isProd = false;
                    foreach (string Prod in ProdStr)
                    {
                        if (ACR.CorAuthor.Contains(Prod))
                            isProd = true;
                    }
                    if (isProd)
                        _ACRCorMail = _OPSDetailOBJ.PRMaill;
                    else
                        _ACRCorMail = ACR.CorEmail.Trim();
                }
                else
                {
                    _ACRCorMail = _OPSDB.GetAuthorEmailFromArticleContentReport(JID, AID);
                }
            }
        }
        private void InializeFileInfo()
        {
            _PDFNAME = Path.GetFileName(_InputFiles.PDFPath);
            ReviewPDFName = _PDFNAME.Replace(".pdf", "_review.pdf");

            FileInfo Finfo = new FileInfo(_InputFiles.PDFPath);
            long _PdfSize = Finfo.Length;

            if (_PdfSize > 1024)
                _PdfSize = _PdfSize / 1024;

            PdfSize = (int)_PdfSize;
        }

        #region IEProofMailInfo Members

        public string MailFrom
        { get; set; }

        public string MailTo
        { get; set; }
        public string MailCC
        { get; set; }
        public string Role
        { get; set; }

        public string MailBCC
        { get; set; }
        public string PDFPath
        {
            get { return _PDFPath; }
        }
        public string XMLPath
        {
            get { return _XMLPath; }
        }

        #endregion

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

        public bool IsArticleTitleExist
        {
            get
            {
                return _IsArticleTitleExist;
            }
            set
            {
                _IsArticleTitleExist = value;
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

        public bool IsACRJID
        {
            get
            {
                return _IsACRJID;
            }
            set
            {
                _IsACRJID = value;
            }
        }


        public void StartValidation()
        {
            InitiallizeProcess();

            AssignValidationResult();
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

        private void AssignValidationResult()
        {

            _ValidationResult = _IsAuthorEmailExist;

            if (_ValidationResult)
                _ValidationResult = _isValidJID;

            if (_ValidationResult)
                _ValidationResult = _isAlreadyProcessed ? false : true;

            if (_ValidationResult)
                _ValidationResult = _IsArticleTitleExist;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorEmailExist;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorEMailWellForm;

            if (_ValidationResult)
                _ValidationResult = _IsAuthorNameExist;

            if (_ValidationResult)
                _ValidationResult = IsPdfPagesEqualAutopage;

            if (_ValidationResult)
                _ValidationResult = _IsQueryIDExist;

            if (_ValidationResult)
                _ValidationResult = _IscPDFExist;

            if (_ValidationResult && JID == "AJIM")
            {
                _ValidationResult = _IsAIJMEditor;
            }
            // For Email Check
            //if (_ValidationResult)
            //    _ValidationResult = _IsMatchCorEmailXMLANDDB;

            /////////////No need to eproof if AJt article's type is CDC
            PDF PDFObj = new PDF(_InputPDF, MNT);
            if (JID.Equals("AJT") && PDFObj.CheckCDC(_InputPDF))
            {
                IsCDCArticle = true;
                ValidationResult = false;
            }
            else
            {
                IsCDCArticle = false;
            }
        }

        public bool IsCDCArticle
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }


        public bool IsPdfProcessError
        {
            get;
            set;
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
    }
}
