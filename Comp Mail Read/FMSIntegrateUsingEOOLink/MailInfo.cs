using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class MailInfo
    {

        public MailInfo(string _MailID, string _MailFrom, string _MailTo, string _MailSubject, string _MailBody, string _MailTime)
        {
            NoteID      = _MailID;
            MailSubject = _MailSubject;
            MailTo      = _MailTo;
            MailBody    = _MailBody;
            MailFrom    = _MailFrom;
            MailTime   = DateTime.Parse( _MailTime);
            AssignEOOType();
        }

        private void AssignEOOType()
        {
            if (MailSubject.IndexOf("export", StringComparison.OrdinalIgnoreCase) != -1)
                ExportMailType = true;
            else if (MailSubject.IndexOf("Sent to production", StringComparison.OrdinalIgnoreCase) != -1)
                ExportMailType = true;
            else if (MailSubject.IndexOf("import", StringComparison.OrdinalIgnoreCase) != -1)
                ImportMailType = true;
            
        }
        public DateTime MailTime
        {
            get;
            set;
        }
        public bool ExportMailType
        {
            get;
            set;
        }
        public bool ImportMailType
        {
            get;
            set;
        }

        public string NoteID
        {
            get;
            set;
        }

        public string MailFrom
        {
            get;
            set;
        }

        public string MailTo
        {
            get;
            set;
        }

        public string MailSubject
        {
            get;
            set;
        }
        public string MailBody
        {
            get;
            set;
        }
    }
}
