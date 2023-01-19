using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Contrast4ElsBooks
{
    class FtpDetails
    {
      
        string _host = string.Empty;
        string _user = string.Empty;
        string _password = string.Empty;
        string _location = string.Empty;

        public FtpDetails()
        {
            Host = ConfigurationSettings.AppSettings["Host"];
            User = ConfigurationSettings.AppSettings["User"];
            password = ConfigurationSettings.AppSettings["Password"];
            Location = ConfigurationSettings.AppSettings["Location"];    
        }        
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
    }
}
