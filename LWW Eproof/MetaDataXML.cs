using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessNotification;

namespace LWWeProof
{
    class DatePart
    {
        string _Year = string.Empty;
        string _Month = string.Empty;
        string _Day = string.Empty;
        string _Hour = string.Empty;
        string _Minute = string.Empty;
        string _Second = string.Empty;

        string _Date = string.Empty;


        public DatePart()
        { }
        public DatePart(string Year, String Month, string Day)
        {
            _Year = Year;
            _Month = Month;
            _Day = Day;
        }

        public string Date
        {
            get { return (_Day + "-" + _Month + "-" + _Year).Trim('-') ; }
            
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }

        public string Hour
        {
            get { return _Hour; }
            set { _Hour = value; }
        }

        public string Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }

        public string Second
        {
            get { return _Second; }
            set { _Second = value; }
        }
    }
    class History
    {
        DatePart _ReceivedDate = new DatePart();
        DatePart _RevisedDate = new DatePart();
        DatePart _AcceptedDate = new DatePart();

        public DatePart ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }

        public DatePart RevisedDate
        {
            get { return _RevisedDate; }
            set { _RevisedDate = value; }
        }

        public DatePart AcceptedDate
        {
            get { return _AcceptedDate; }
            set { _AcceptedDate = value; }
        }
    }
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


    }



    class MetaDataProcess : MetaDataInfo
    {
        List<AuthorInfo> _Authors = new List<AuthorInfo>();
        public string CorEmail { get; set; }
        public string CorName { get; set; }
        public string CorDegree { get; set; }
        public List<AuthorInfo> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }
        public bool ProcessMetaXml(string _MetaXMLStr)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.XmlResolver = null;
                xDoc.LoadXml(_MetaXMLStr);


                if (xDoc.DocumentElement.Attributes.GetNamedItem("article-type") != null)
                {
                    ArticleCategory = xDoc.DocumentElement.Attributes.GetNamedItem("article-type").Value;
                }

                XmlNodeList ATCountList = xDoc.GetElementsByTagName("article-title");
                if (ATCountList.Count > 0)
                {
                    ArticleTitle = ATCountList[0].InnerText;
                }


                XmlNodeList contribList = xDoc.GetElementsByTagName("contrib");
                if (contribList.Count > 0)
                {
                    foreach (XmlNode contrib in contribList)
                    {
                        Authors.Add(ProcessAuNode(contrib));
                    }
                }



                AuthorInfo CorAu = Authors.Find(x => x.isCorr == true);
                if (CorAu != null)
                {
                    CorEmail = CorAu.eMail;
                    CorName = CorAu.FirstName + " " + CorAu.LastName;
                    CorDegree = CorAu.Degree;
                }


            }
            catch (XmlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }

            return true;
        }
        private AuthorInfo ProcessAuNode(XmlNode AuNode)
        {
            AuthorInfo Author = new AuthorInfo();

            if (AuNode.Attributes.GetNamedItem("corresp") != null)
            {
                string Corr = AuNode.Attributes.GetNamedItem("corresp").Value;
                if (Corr.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    Author.isCorr = true;
                }

            }

            XmlNodeList NL = AuNode.SelectNodes(".//*");
            foreach (XmlNode Node in NL)
                switch (Node.Name)
                {

                    case "attr_type":
                        {
                            break;
                        }
                    case "attribute":
                        {
                            break;
                        }
                    case "comments":
                        {
                            break;
                        }
                    case "degrees":
                        {
                            Author.Degree = Node.InnerText;
                            break;
                        }
                    case "email":
                        {
                            Author.eMail = Node.InnerText;
                            break;
                        }
                    case "given-names":
                        {
                            Author.FirstName = Node.InnerText;
                            break;
                        }
                    case "flags":
                        {
                            break;
                        }
                    case "surname":
                        {
                            Author.LastName = Node.InnerText;
                            break;
                        }
                    case "middle_name":
                        {
                            Author.MiddleName = Node.InnerText;
                            break;
                        }
                    case "orcid":
                        {
                            break;
                        }
                    case "researcher_id":
                        {
                            break;
                        }
                    case "prefix":
                        {
                            Author.Salutation = Node.InnerText;
                            break;
                        }
                    case "suffix":
                        {
                            Author.Suffix = Node.InnerText;
                            break;
                        }
                }
            return Author;
        }
    }
}
