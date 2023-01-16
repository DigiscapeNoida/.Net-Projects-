using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WileyIssueOrder
{
    class OrderInfo : MessageEventArgs
    {
        
        DateTime _RECIVEDDATE;
        DateTime _DUEDATE;

        public DateTime RECIVEDDATE
        {
            get
            {
                return _RECIVEDDATE;
            }
            set
            {
                _RECIVEDDATE = value;
            }
        }
        public DateTime DUEDATE
        {
            get
            {
                return _DUEDATE;
            }
            set
            {
                _DUEDATE = value;
            }
        }

        public string Client { get; set; }
        public string JID    { get; set; }
        public string JTITLE { get; set; }
        public string pISSN  { get; set; }
        public string Stage  { get; set; }
        public string Stage { get; set; }
        
    }

}