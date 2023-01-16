using System;
using System.Collections.Generic;
using System.Xml;
using ProcessNotification;
namespace LWWAutoIntegrate
{
    class MetaDataInfo : MessageEventArgs
    {
        History _HistoryDate = new History();
        List<AuthorInfo> _Authors = new List<AuthorInfo>();
        public List<AuthorInfo> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }
        public History HistoryDate
        {
            get { return _HistoryDate; }
        }

        public string SubmissionXML { get; set; }
        public string MetaDataXML { get; set; }
        public string MSS { get; set; }
        public string FigCount { get; set; }
        public string VOL { get; set; }
        public string Issue { get; set; }
        public string ArticleCategory { get; set; }
        public string ArticleTitle { get; set; }
        public string CorEmail { get; set; }
        public string CorName { get; set; }
        public string CorDegree { get; set; }
        public string RevisePdf { get; set; }
        
            
    }
}
