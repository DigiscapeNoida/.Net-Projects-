using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;
using ProcessNotification;
namespace LWWeProof
{
    class FreshEProofProcess : MessageEventArgs, IRValidation
    {
        bool _ValidationResult = false;
        bool _IsAuthorEMailWellForm = false;
        bool _isAlreadyProcessed = false;
        bool _IsAuthorNameExist = false;
        bool _IsAuthorEmailExist = false;
        bool _isValidJID = false;


        string _OPSConStr = string.Empty;
        string _MailBody  = string.Empty;
        string _PDFPath   = string.Empty;

        StringCollection _PDffiles = new StringCollection();
        OPSDetail   _OPSDetailObj = null;
        MNTInfo     _MNT = null;
        OPSDB       _OPSDBObj = null;
        OPSRevise   _OPSRvsObj = null;

        public string MailBody
        {
            get { return _MailBody; }
        }

        public MNTInfo MNT
        {
            get { return _MNT; }
        }

        public OPSDetail OPSDetailObj
        {
            get { return _OPSDetailObj; }
        }

        public OPSRevise OPSRvsObj
        {
            get { return _OPSRvsObj; }
        }

        
        public FreshEProofProcess(MNTInfo MNT, string PDFPath)
        { 
            _MNT       = MNT;
            _PDFPath = PDFPath;
            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj  = new OPSDB(_OPSConStr);
             
             
        }
        private void InitiallizeEditorInfo()
        {
            _OPSDetailObj = _OPSDBObj.GetOPSDetails(_MNT.JID, _MNT.Client);

            if (_OPSDetailObj != null)
            {
                _OPSRvsObj = _OPSDBObj.GetReviseDetails(_OPSDetailObj.OPSID);
                _MailBody = GetMailBody();
            }
        }
        public void StartValidation()
        {
            InitiallizeEditorInfo();
            AssignValidationResult();

        }
        private string GetMailBody()
        {

            string TemplatePath = Program.EXELoc + "\\ReviseTemplate\\" + _MNT.JID + ".txt";

            if (!File.Exists(TemplatePath))
                TemplatePath = Program.EXELoc + "\\ReviseTemplate\\Common.txt";

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

            _IsAuthorEmailExist    = string.IsNullOrEmpty(_OPSRvsObj.MailTo) ? false : true;
            _IsAuthorNameExist     = string.IsNullOrEmpty(_OPSRvsObj.CorrName) ? false : true;
            _IsAuthorEMailWellForm = CheckEmail(_OPSRvsObj.MailTo);

            
              _isAlreadyProcessed = _OPSDBObj.GetReviseHistoryCount(_OPSRvsObj.OPSID, _MNT.AID, _MNT.Stage) > 0 ? true : false;
            


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
        public bool InsertHistory()
        {

            try
            {
                usp_GetReviseHistoryResult RvsHstry = new usp_GetReviseHistoryResult();
                RvsHstry.AID = _MNT.AID;
                RvsHstry.CorrName = _OPSRvsObj.CorrName;
                RvsHstry.MailBCC = _OPSRvsObj.MailBCC;
                RvsHstry.MailCC = _OPSRvsObj.MailCC;
                RvsHstry.MailFrom = _OPSRvsObj.MailFrom;
                RvsHstry.MailTo = _OPSRvsObj.MailTo;
                RvsHstry.OPSID = _OPSRvsObj.OPSID;

                RvsHstry.RevisionType = MNT.Stage;
                _OPSDBObj.InsertReviseHistory(RvsHstry);

                ProcessEventHandler("Finish InsertHistory");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
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

        public bool IsPdfProcessError
        {
            get;
            set;
        }
        public string PDFPath
        {
            get { return _PDFPath; }
        }

        public bool IsValidTakName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsValidGOXML
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsValidStage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public bool IsValidAID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string Stage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
