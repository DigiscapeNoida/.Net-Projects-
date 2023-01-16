using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for MailDetail
/// </summary>
namespace FMSIntegrateUsingEOOLink
{
    public class MailDetail
    {
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

        public string[] MailAtchmnt
        {
            get;
            set;
        }

        public bool IsBodyHtml
        {
            get;
            set;
        }
    }
}