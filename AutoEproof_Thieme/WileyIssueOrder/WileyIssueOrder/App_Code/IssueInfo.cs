using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WileyIssueOrder
{
    public class IssueInfo : MessageEventArgs
    {
        public string Issue        { get; set; }
        public string Volume       { get; set; }
        public string IssSuppl     { get; set; }
        public string CoverMonth   { get; set; }
        public string CoverYear    { get; set; }
        public string DisplayCover { get; set; }
        public string SPage { get; set; }
        public string IssueRemarks { get; set; }
    }

    class ss
    {
        public ss()
        {
        }
        public void dd()
        {
            IssueInfo DD = new IssueInfo();

            List<AIDInfo> ListAIDInfo = new List<AIDInfo>();
            string[] AIDs = "".Split('\n');

            foreach (string AID in AIDs)
            {
                GetAIDInfo(AID);
            }

        }

        private AIDInfo GetAIDInfo(string AID)
        {
            
            return new AIDInfo();
        }
    }
}