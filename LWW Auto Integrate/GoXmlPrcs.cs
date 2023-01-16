using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using PdfProcess;
using MSWord;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
namespace LWWAutoIntegrate
{
    class GoXmlPrcs : GoXmlInfo
    {
        string _MetadataXMLPath = string.Empty;
        string _XMLPath = string.Empty;
        string _ZipPath = string.Empty;
        string _ExtractTo = string.Empty;
        string _MetaXMLFile = string.Empty;
        string _PrfPdfFile = string.Empty;
        string _RevisePdf = string.Empty;
        string _TOCINIFile = string.Empty;
        //GoXmlInfo _GoXmlInfoObj = new GoXmlInfo();
        public GoXmlPrcs(string XMLPath)
        {
            _XMLPath = XMLPath;
            if (File.Exists(_XMLPath))
            {
                //_GoXmlInfoObj = (GoXmlInfo)this;
                
                GoXML = File.ReadAllText(_XMLPath);
                ProcessGoXml();
            }
        }

        //public GoXmlInfo GoXmlInfoObj
        //{
        //    get { return _GoXmlInfoObj; }
        //}
        public GoXmlPrcs(string XMLPath, string ZipPath)
        {
            _XMLPath = XMLPath;
            _ZipPath = ZipPath;
            
            if (File.Exists(_XMLPath))
                GoXML = File.ReadAllText(_XMLPath);
        }
        private bool ProcessGoXml()
        {
            ProcessEventHandler("ProcessGoXml");

            if (string.IsNullOrEmpty( GoXML))
            {
                ProcessEventHandler(GoXML + " file does not exist" );
                return false;
            }
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.XmlResolver = null;
                xDoc.Load(_XMLPath);

                XmlNodeList ParamList = xDoc.GetElementsByTagName("parameter");
                foreach (XmlNode param in ParamList)
                {
                    if (param.Attributes.GetNamedItem("name") != null && param.Attributes.GetNamedItem("value") != null)
                    {
                        string ParamName = param.Attributes.GetNamedItem("name").Value;
                        string ParamValue = param.Attributes.GetNamedItem("value").Value;

                        SetParamValue(ParamName, ParamValue);
                    }
                }

                XmlNodeList JournalList = xDoc.GetElementsByTagName("journal");
                if (JournalList.Count > 0)
                {
                    if (JournalList[0].Attributes.GetNamedItem("code") != null)
                        JID = JournalList[0].Attributes.GetNamedItem("code").Value.ToUpper().Trim();
                }

                XmlNodeList MetaList = xDoc.GetElementsByTagName("metadata-file");
                if (MetaList.Count > 0)
                {
                    if (MetaList[0].Attributes.GetNamedItem("name") != null)
                        MetaFileName = MetaList[0].Attributes.GetNamedItem("name").Value;
                }

                string nm = "";
                int GrCount = 0;
                XmlNodeList FileList = xDoc.GetElementsByTagName("file");
                foreach (XmlNode FileNode in FileList)
                {
                    string asd = FileNode.BaseURI;
                    nm = Path.GetFileNameWithoutExtension(asd);
                    if (FileNode.Attributes.GetNamedItem("name") != null)
                    {
                        string FileName = FileNode.Attributes.GetNamedItem("name").Value;
                        string ext = Path.GetExtension(FileName);
                        if ("#.eps#.tiff#.tif#.png#.jpg#.bmp#.png#".IndexOf("#" + ext + "#") != -1)
                        {
                            GrCount++;
                        }
                        else if (FileName.IndexOf("Fig", StringComparison.OrdinalIgnoreCase) != -1)
                        { 
                            GrCount++;
                        }

                        if (string.IsNullOrEmpty(AID) && ext.EndsWith("pdf",StringComparison.OrdinalIgnoreCase))
                        {                                                                                      
                            string AIDPdf = Path.GetFileNameWithoutExtension(FileName);
                            if (string.IsNullOrEmpty(AID))
                                AID = AIDPdf;
                            else if (AIDPdf.Length < AID.Length)
                            {
                                AID = AIDPdf;
                            }
                        }
                    }
                }
                this.FigCount=GrCount.ToString();
                SqlParameter[] objParam = new SqlParameter[4];
                String _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
                objParam[0] = new SqlParameter("@GoXMLPath", nm.Replace(".go", ""));
                objParam[1] = new SqlParameter("@JID", JID);
                objParam[2] = new SqlParameter("@Aid", AID);
                objParam[3] = new SqlParameter("@FigCount", FigCount);

                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateFigCount]", objParam);
                                               
                //if jid is in list then return false duefor new jid on compostion task

                String TaskBasedJID = ConfigurationManager.AppSettings["TaskBasedJID"].ToString();
                string[] nJID = TaskBasedJID.Split('#');
                foreach (string j in nJID)
                {
                    if (j == JID)
                    {
                        if (TaskName == "Logging/Verification" || TaskName == "OPEN ACCESS: Logging/Verification")
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex1)
            {
                ProcessEventHandler("Exception" + ex1.ToString());
                ProcessErrorHandler(ex1);
                return false;
            }
            ProcessEventHandler("Successfully processed ProcessGoXml method");
            return true;
        }
        private void SetParamValue(string ParamName, string ParamValue)
        {
            switch (ParamName)
            {
                case "production-task-id":
                    {
                        Guid = ParamValue;
                        break;
                    }
                case "production-task-name":
                    {
                        TaskName = ParamValue;
                        break;
                    }
                case "production-task-due-date":
                    {
                        DueDate = ParamValue;
                        break;
                    }
                case "DOI":
                    {
                        DOI = ParamValue;
                        break;
                    }
                case "manuscript-number":
                    {
                        AID = ParamValue.Replace(" ","");
                        break;
                    }

            }
        }

        public bool StartToGetArticleInfo()
        {
            ProcessEventHandler("StartToGetArticleInfo");

            bool PrcsRslt = false;

            if (ProcessGoXml())
            {
                ProcessEventHandler("UnZipFile");

                if (UnZipFile())
                {
                   
                    if (!string.IsNullOrEmpty(_MetaXMLFile) && File.Exists(_MetaXMLFile)) 
                    {
                        MetaDataXML = File.ReadAllText(_MetaXMLFile);
                        if (ProcessMetaXml())
                        {
                            PrcsRslt = true;
                        }
                    }
                    else
                        PrcsRslt = true;

                    if (string.IsNullOrEmpty(MSS))
                    {
                        MSS = "0"; // GetMSSPages(_ExtractTo); rohit
                    }

                    if (string.IsNullOrEmpty(MSS))
                    {
                        ProcessEventHandler("Mss is missisng");
                        ProcessEventHandler("Trying to get prf pdf pages");

                        ProcessEventHandler("_PrfPdfFile :" +  _PrfPdfFile);

                        if (!string.IsNullOrEmpty(_PrfPdfFile) && File.Exists(_PrfPdfFile))
                        {
                            ProcessEventHandler("PDF.GetPdfPageCount");
                            MSS= PDF.GetPdfPageCount(_PrfPdfFile).ToString();
                        }
                    }

                    ProcessEventHandler("Directory Delete : " + _ExtractTo);
                    Directory.Delete(_ExtractTo, true);
                }
            }
            return PrcsRslt;
        }
        private bool UnZipFile()
        {
            _ExtractTo = "C:\\Temp\\" + System.Guid.NewGuid().ToString();

            ProcessEventHandler("_ExtractTo : " + _ExtractTo);
            ProcessEventHandler("_ZipPath : "   + _ZipPath);

            if (!string.IsNullOrEmpty(_ZipPath) && File.Exists(_ZipPath))
            {
                try
                {
                    FastZip fz = new FastZip();
                    fz.ExtractZip(_ZipPath, _ExtractTo, "xml|doc|docx|pdf");

                    if (!string.IsNullOrEmpty(MetaFileName))
                    {
                        string[] MetaXMLFiles = Directory.GetFiles(_ExtractTo, MetaFileName, SearchOption.AllDirectories);

                        if (MetaXMLFiles.Length == 1)
                        {
                            _MetaXMLFile = MetaXMLFiles[0];
                        }
                    }
                    ProcessEventHandler("MetaXMLFile : " + _MetaXMLFile);
                    
                    string[] PrfFiles = Directory.GetFiles(_ExtractTo, "*.pdf", SearchOption.AllDirectories);
                    Array.Sort(PrfFiles);
                    if (PrfFiles.Length > 0)
                    {
                                RevisePdf = "";
                        _PrfPdfFile = PrfFiles[0];
                        //Chnage in MDWorkflow
                        foreach (string s in PrfFiles)
                        {
                            if (s.Contains("_1stcrx"))
                            {
                                RevisePdf = "1";
                            }
                            if (s.Contains("_2ndcrx"))
                            {
                                RevisePdf = "2";
                            }
                            if (s.Contains("_3rdcrx"))
                            {
                                RevisePdf = "3";
                            }
                            if (s.Contains("_4thcrx"))
                            {
                                RevisePdf = "4";
                            }
                            if (s.Contains("_5thcrx"))
                            {
                                RevisePdf = "5";
                            }
                            if (s.Contains("_6thcrx"))
                            {
                                RevisePdf = "6";
                            }

                        }
                    }
                    ProcessEventHandler("_PrfPdfFile : " + _PrfPdfFile);

                    string[] SubmissionFiles = Directory.GetFiles(_ExtractTo, "*Import.xml", SearchOption.AllDirectories);
                    Array.Sort(SubmissionFiles);
                    if (SubmissionFiles.Length > 0)
                    {
                        SubmissionXML = File.ReadAllText(SubmissionFiles[0]);
                    }


                    List<string> file = new List<string>(Directory.EnumerateFiles(_ExtractTo, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith("TOC.doc") || s.EndsWith("TOC.docx") || s.EndsWith("INI.doc") || s.EndsWith("INI.docx")));
                   
                    if (file.Count > 0)
                        return false;
                   
                    fz = null;
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                    return false;
                }
            }
            else
            { 
                
            }

            return true;
        }
        //private string GetMSSPages(string Source)
        //{
        //    string MSSPages = "";
        //    StringBuilder _DocText = new StringBuilder();
        //    try
        //    {
                
        //        //string IgnoreFiles = System.Configuration.ConfigurationManager.AppSettings["IgnoreFiles"];
        //        //string[] IgnoreFilesArr = { "" };

        //        //if (!string.IsNullOrEmpty(IgnoreFiles))
        //        //{
        //        //    IgnoreFilesArr = IgnoreFiles.Split('#');
        //        //}


        //        StringCollection DocFiles = new StringCollection();
        //        ProcessEventHandler("Listing doc files in input files");
        //        DocFiles.AddRange(Directory.GetFiles(Source, "*.doc*", SearchOption.AllDirectories));

        //        //int Counter = 1;
        //        //int MaxWordCount = 0;
        //        //int TempFigCount = 0;
        //        //WordCount WordCountObj = new WordCount();
        //        //foreach (string DocFile in DocFiles)
        //        //{
        //        //    bool isIgnore = false;
        //        //    foreach (string IgnrFile in IgnoreFilesArr)
        //        //    {
        //        //        if (DocFile.IndexOf(IgnrFile, StringComparison.OrdinalIgnoreCase) != -1)
        //        //        {
        //        //            isIgnore = true;
        //        //        }
        //        //    }
        //        //    if (isIgnore == false)
        //        //    {
        //        //        ProcessEventHandler("Getting character count from " + DocFile);
        //        //       // int WordCount = WordCountObj.GetCharacterCount(DocFile);
        //        //        int WordCount = WordCountObj.GetWordCount(DocFile);
        //        //        //if (WordCount > MaxWordCount)
        //        //        //{
        //        //        //    MaxWordCount = WordCount;
        //        //       // MaxWordCount = MaxWordCount+WordCountObj.Pages;
        //        //        MaxWordCount = MaxWordCount +WordCount;
        //        //            _DocText = new StringBuilder(WordCountObj.DocText);
        //        //            string FigCount = GetFigCount(_DocText);
        //        //            int TempFigCount1 = 0;
        //        //            if (Int32.TryParse(FigCount, out TempFigCount1))
        //        //            {
        //        //                TempFigCount = TempFigCount+TempFigCount1;
        //        //            }
        //        //       // }
        //        //    }
        //        //    Counter++;
        //        //}
        //        //MSSPages = (MaxWordCount/250).ToString();
        //        //this.FigCount = TempFigCount.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ProcessErrorHandler(ex);
        //    }
            
        //    //string FigCount= GetFigCount(_DocText);
        //    //int TempFigCount =0;

        //    //if (Int32.TryParse(FigCount, out TempFigCount))
        //    //{
        //    //    int  TempFigCount1=0;
        //    //    if (Int32.TryParse( this.FigCount,out TempFigCount1))
        //    //    {
        //    //        if (TempFigCount > TempFigCount1)
        //    //        {
        //    //            this.FigCount = FigCount;
        //    //        }
        //    //    }
        //    //}

        //    return MSSPages;
        //}
        string GetFigCount(StringBuilder DocStr)
        {
            string FigCount = "0";
            try
            {
                int ePos = DocStr.ToString().LastIndexOf("\nFig");

                if (ePos == -1)
                {
                    ePos = DocStr.ToString().LastIndexOf("\rFig");
                    if (ePos == -1)
                    {
                        ePos = DocStr.ToString().LastIndexOf("Fig");
                    }
                }

                if (ePos > 0)
                {
                    DocStr.Remove(0, ePos);
                    FigCount = Regex.Match(DocStr.ToString(), "[0-9]+").Value;
                }
                else 
                { 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                 ProcessErrorHandler(ex);
            }
            return FigCount;
        }
        public bool ProcessMetaXml()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.XmlResolver = null;
                xDoc.LoadXml( File.ReadAllText(_MetaXMLFile).Replace("&","#$#"));


                if (xDoc.DocumentElement.Attributes.GetNamedItem("article-type") != null)
                {
                    ArticleCategory = xDoc.DocumentElement.Attributes.GetNamedItem("article-type").Value;
                    
                }

                XmlNodeList ATCountList = xDoc.GetElementsByTagName("article-title");
                if (ATCountList.Count > 0)
                {
                    ArticleTitle = ATCountList[0].InnerText;
                }

                XmlNodeList FigCountList = xDoc.GetElementsByTagName("fig-count");
                if (FigCountList.Count > 0)
                {
                    if (FigCountList[0].Attributes.GetNamedItem("count") != null)
                    {
                        if (!FigCountList[0].Attributes.GetNamedItem("count").Value.Equals("0"))
                            this.FigCount = FigCountList[0].Attributes.GetNamedItem("count").Value;
                    }
                }

                VOL = string.Empty;
                XmlNodeList VolumeList = xDoc.GetElementsByTagName("volume");
                if (VolumeList.Count > 0)
                {

                    VOL = VolumeList[0].InnerText;
                }

                Issue = string.Empty;
                XmlNodeList IssueList = xDoc.GetElementsByTagName("issue");
                if (IssueList.Count > 0)
                {

                    Issue = IssueList[0].InnerText;
                }

                XmlNodeList dayList = xDoc.GetElementsByTagName("day");
                if (dayList.Count > 0)
                {
                    foreach (XmlNode day in dayList)
                    {
                        ProcessHistory(day);
                    }

                }
                XmlNodeList monthList = xDoc.GetElementsByTagName("month");
                if (monthList.Count > 0)
                {
                    foreach (XmlNode month in monthList)
                    {
                        ProcessHistory(month);
                    }
                }
                XmlNodeList yearList = xDoc.GetElementsByTagName("year");
                if (yearList.Count > 0)
                {
                    foreach (XmlNode year in yearList)
                    {
                        ProcessHistory(year);
                    }
                }

                XmlNodeList contribList = xDoc.GetElementsByTagName("contrib");
                if (contribList.Count > 0)
                {
                    foreach (XmlNode contrib in contribList)
                    {
                        Authors.Add(ProcessAuNode(contrib));
                    }
                }

                XmlNodeList MetaNameList = xDoc.GetElementsByTagName("meta-name");
                if (MetaNameList.Count > 0)
                {
                    foreach (XmlNode MetaName in MetaNameList)
                    {
                        if (MetaName.InnerText.IndexOf("Open Access Payment Received") != -1)
                        { 
                            XmlNode metavalue =  MetaName.ParentNode.SelectSingleNode("meta-value");
                            if (metavalue != null && metavalue.InnerText.IndexOf("Yes",StringComparison.OrdinalIgnoreCase) != -1)
                            { 
                                Remarks = "Open Access";
                            }
                        }
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
        private void ProcessHistory(XmlNode Node)
        {
            string DateType = string.Empty;
            if (Node.ParentNode.Attributes.GetNamedItem("date-type") != null)
            {
                DateType = Node.ParentNode.Attributes.GetNamedItem("date-type").Value;
            }

            switch (Node.Name)
            {
                case "day":
                    {
                        if (DateType.Equals("accepted"))
                            HistoryDate.AcceptedDate.Day = Node.InnerText;
                        else if (DateType.Equals("received"))
                            HistoryDate.ReceivedDate.Day = Node.InnerText;
                        else if (DateType.Equals("rev-recd"))
                            HistoryDate.RevisedDate.Day = Node.InnerText;
                        break;
                    }
                case "month":
                    {
                        if (DateType.Equals("accepted"))
                            HistoryDate.AcceptedDate.Month = Node.InnerText;
                        else if (DateType.Equals("received"))
                            HistoryDate.ReceivedDate.Month = Node.InnerText;
                        else if (DateType.Equals("rev-recd"))
                            HistoryDate.RevisedDate.Month = Node.InnerText;
                        break;
                    }

                case "year":
                    {
                        if (DateType.Equals("accepted"))
                            HistoryDate.AcceptedDate.Year = Node.InnerText;
                        else if (DateType.Equals("received"))
                            HistoryDate.ReceivedDate.Year = Node.InnerText;
                        else if (DateType.Equals("rev-recd"))
                            HistoryDate.RevisedDate.Year = Node.InnerText;
                        break;
                    }
            }
        }
    }
}
