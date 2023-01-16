using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWAutoIntegrate
{
    enum Protocol 
    {
        HTTP=1,FTP,TCP
    }
    interface IUrlDtl
    {
        string Url      {get;set;}
        string UID      {get;set;}
        string PWD      {get;set;}
        string Port     {get;set;}
        string Protocol {get;set; }
    }

    class FtpUrl : IUrlDtl
    {

        public string Url
        {
            get;
            set;
        }

        public string UID
        {
            get;
            set;
        }

        public string PWD
        {
            get;
            set;
        }

        public string Port
        {
            get;
            set;
        }
        public string Protocol
        {
            get;
            set;
        }
    }

    class d
    {
        string _s = string.Empty;
        public d(string ss)
        {
            _s = ss;
        }
    }

}
