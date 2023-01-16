using System;
using System.IO;
using System.Collections.Generic;
using WileyMetaData;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class ProcessMetaData
    {
        string _MetaDataXml = string.Empty;
       static List<RefCodeJIDAID> RefCodeJIDAIDList = new List<RefCodeJIDAID>();

        static  ProcessMetaData()
        {
            InitializeRefcode();
        }

        public ProcessMetaData(string  MetaDataXml)
        {
            _MetaDataXml = MetaDataXml;
        }

        public void ProcessMetaDataXML()
        {
             
            WileyMetaData.MetaData WileyMetaDataOBJ = new WileyMetaData.MetaData(_MetaDataXml);

            WileyMetaDataOBJ.ArticleDetail.RefCode = WileyMetaDataOBJ.ArticleDetail.RefCode.Replace(".", "-");
            RefCodeJIDAID RefCodeJIDAIDOBJ  = RefCodeJIDAIDList.Find(x=> x.RefCode== WileyMetaDataOBJ.ArticleDetail.RefCode);
            
            if (RefCodeJIDAIDOBJ!= null)
                WileyMetaDataOBJ.InsertMetaData("JWUSA",RefCodeJIDAIDOBJ.JID,RefCodeJIDAIDOBJ.AID);
        }
        public void GetMetaDataXML()
        {
            WileyMetaData.MetaData WileyMetaDataOBJ = new WileyMetaData.MetaData();
            WileyMetaData.ArticleInfo AI = WileyMetaDataOBJ.GetArticleMetaData("JWUSA", "TEST", "1234");

            //AuthorInfo s = AI.Authors.Find(x => x.isCorr);
        }
        private static void InitializeRefcode()
        { 

            string RefCodePath=@"D:\Input\JW\MetaData\refcode.txt";
            string[] Lines = File.ReadAllLines(RefCodePath);

            foreach (string Line in Lines)
            {
                string[] arr = Line.Split('\t');
                RefCodeJIDAIDList.Add(new RefCodeJIDAID(arr[0],arr[1],arr[2]));
            }
        }
    }

    class RefCodeJIDAID
    {

        public RefCodeJIDAID( string JID, string AID,string RefCode)
        {
            this.RefCode = RefCode;
            this.JID = JID;
            this.AID = AID;
        }
        public string  RefCode
        {
            get;set;
        }
        public string   JID
        {
            get;set;
        }
        public string   AID
        {
            get;set;
        }
    }
}
