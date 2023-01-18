using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWeProof
{
    public class MNTInfo
    {


        public MNTInfo(string Client_, string JID_, string AID_, string Stage_)
        {
            Client = Client_;
            JID = JID_.ToUpper();
            AID = AID_;
            Stage = Stage_;
            this.MNTFolder = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110";
        }
        public MNTInfo(string MNTFolder, string Stage)
        {
            //MNT_JWUSA_JOURNAL_JCB_24664_110
            string[] MNTParts = MNTFolder.Split('_');
            if (MNTParts.Length == 6)
            {
                Client = MNTParts[1];
                JID = MNTParts[3].ToUpper();
                AID = MNTParts[4];
            }
            this.MNTFolder = MNTFolder;
            this.Stage = Stage;
        }

        public MNTInfo(string ItemCode, string Stage, string Status)
        {
            if (ItemCode.Contains("#"))
            {
                ItemCode = ItemCode.Substring(ItemCode.LastIndexOf("#")).Trim();
            }
            //MNT_JWUSA_JOURNAL_JCB_24664_110
            string[] MNTParts = ItemCode.Split('_');
            if (MNTParts.Length > 5)
            {
                Client = MNTParts[1];
                JID = MNTParts[3].ToUpper();
                AID = MNTParts[4];
            }
            this.MNTFolder = ItemCode.Replace("_PS", "");
            this.Stage = Stage;
            this.StrpnsStatus = Status;
        }

        public MNTInfo(string MNTFolder)
        {
            //MNT_JWUSA_JOURNAL_JCB_24664_110
            string[] MNTParts = MNTFolder.Split('_');
            if (MNTParts.Length == 6)
            {
                Client = MNTParts[1];
                JID = MNTParts[3];
                AID = MNTParts[4];
            }
            this.MNTFolder = MNTFolder;

        }
        public string MNTFolder { get; set; }
        public string JID { get; set; }
        public string AID { get; set; }
        public string Client { get; set; }
        public string Stage { get; set; }
        public string Status { get; set; }
        public string StrpnsStatus { get; set; }
        public int PgCountLog { get; set; }
        public int PdfPages { get; set; }
    }
}
