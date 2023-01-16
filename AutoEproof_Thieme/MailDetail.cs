using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using ProcessNotification;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for MailDetail
/// </summary>
namespace AutoEproof
{
    public class MailDetail
    {

        StringCollection _MailAtchmnt = new StringCollection();

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

        public string MailCC
        {
            get;
            set;
        }

        public string MailBCC
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

        public StringCollection MailAtchmnt
        {
            get { return _MailAtchmnt; }
            set { _MailAtchmnt=value; }
            
        }

        public bool IsBodyHtml
        {
            get;
            set;
        }

        public string Stage
        {
            get;
            set;
        }
    }
}