using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Domino;

namespace FMSIntegrateUsingEOOLink
{
    class wileyProcess
    {
         public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        private string _MailSubject = string.Empty;
        private string _MailFrom = string.Empty;
        private string _MailBody = string.Empty;
        private string _Aid = string.Empty;
        private string _FileName = string.Empty;
        private string _Jid;

        NotesDocument Document;

        public wileyProcess(string subject, string from, string body, NotesDocument Document)
        {
            MailSubject = subject;
            this.Document = Document;
            MailFrom = from;
            MailBody = body;
        }
        public void StartProcess()
        {
            GetMailBodyDetail();
        }

     

        private string MailSubject
        {
            get 
            {
                return _MailSubject;
            }
            set
            {
                 _MailSubject = value;
            }
        }
        private string MailFrom
        {
            get
            {
                return _MailFrom;
            }
            set
            {
                _MailFrom = value;
            }
        }
        private string MailBody
        {
            get
            {
                return _MailBody;
            }
            set
            {
                _MailBody = value;
            }
        }        
        private string AID
        {
            get
            {
                return _Aid;
            }
            set
            {
                _Aid = value;
            }
        }
       
        private string Jid
        {
            get
            {
                return _Jid;
            }
            set
            {
                _Jid = value;
            }
        }
        
        private string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }
       
        private bool UpdateStatusDB(bool isIssue)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@Jid", Jid.Trim());
                param[1] = new SqlParameter("@Aid", AID.Trim());
                param[2] = new SqlParameter("@FileName", FileName.Trim());

                SqlHelper.ExecuteNonQuery(StaticInfo.OPSConfig, System.Data.CommandType.StoredProcedure, "[sp_InsertwileyMailInfo]", param);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
            return false;
        }
        private bool UpdateWipData()
        {
            try
            {
                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@JIDAid", JidAid.Trim());
                //SqlHelper.ExecuteNonQuery(StaticInfo.OPSConfig, System.Data.CommandType.StoredProcedure, "[usp_UpdateJQAWipS280ToResuply]", param);
                return true;
            }
            catch (Exception ex)
            {
                   ErrorMessage(ex);
            }

            return false;
        }

        private void GetMailBodyDetail()
        {
            string ebody = MailBody;
            ProcessMessage("Traverse each line of mail body.");

            string[] Lines = ebody.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Line in Lines)
            {
                if (Line.IndexOf("Manuscript number:", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    _Jid = Line.Split(':')[1];
                }
                if (Line.IndexOf("Journal:", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    _Aid = Line.Split(':')[1].Trim();
                }
                if (Line.IndexOf("Your ScholarOne manuscript", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    string SCRNum = Line.Split(new string[] { "Your ScholarOne manuscript" }, StringSplitOptions.None)[1].TrimStart();
                    _FileName = SCRNum.Split(' ')[0];
                }
            }

            UpdateStatusDB(true);
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
}
