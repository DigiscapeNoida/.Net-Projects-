using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JLEDATASET
{
    public delegate void ExceptionHandler(XmlException ex);
    public delegate void XmlChangeHandler(String xmlstr);

    class XmlInfo
    {
        XmlReader         Reader;
        XmlDocument       MyXmlDocument     = new XmlDocument();
        XmlReaderSettings ReaderSettings    = new XmlReaderSettings();

        string processFilePath    = "";
        string XmlFilePath        = "";
        string dtdPath            = "";

        string _JID = "";
        string _AID = "";

        bool isParsingRequired    = false;
        bool preserveSpace        = false;

        
        bool   _IsChangedDtdPath  = false;
        string _ChangedDtdPath    = "";

        public event ExceptionHandler ExceptionFired;
        public event XmlChangeHandler XmlChanged;

        public XmlInfo()
        {
            MyXmlDocument.NodeChanged += new XmlNodeChangedEventHandler(MyXmlDocument_NodeChanged);
        }
        public bool   IsChangedDtdPath 
        {

            get { return _IsChangedDtdPath; }
            set { _IsChangedDtdPath = value; }
        }
        public string JID
        {
            get
            {
                return _JID;
            }

        }
        public string AID
        {
            get
            {
                return _AID;
            }

        }
        public string ChangedDtdPath
        {
            get { return _ChangedDtdPath; }
            set { _ChangedDtdPath = value; }
        }
        void MyXmlDocument_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            if (XmlChanged!=null)
            {
                XmlChanged(MyXmlDocument.OuterXml);
            }
            //throw new Exception("The method or operation is not implemented.");
        }
        public void SaveXml()
        {
            if (!XmlFilePath.Equals(""))
            {
                MyXmlDocument.Save(XmlFilePath.Replace(".xml","_edt.xml"));
            }
        }
        public XmlInfo(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
        }
        public XmlDocument xmlDocument
        {
            get{return MyXmlDocument;}
        }
        public string InnerXml
        {
            get 
            {
                if (MyXmlDocument != null)
                    return MyXmlDocument.InnerXml;
                else
                    return "";
            }
            
        }
        public string FilePath
        {
            get{return XmlFilePath;}
            set
            {
                if (value.Equals(""))
                    throw new Exception ("Empty string");
                else if (!File.Exists(value))
                    throw new FileNotFoundException();
                else
                    XmlFilePath= value; 
            }  
        }
        public string ProcessFilePath
        {
            get { return processFilePath; }

        }
        public string DtdPath
        {
            get{return dtdPath;}
            set
            {
                if (value.Equals(""))
                    throw new Exception ("Empty string");
                else if (!File.Exists(dtdPath))
                    throw new FileNotFoundException();
                else
                    dtdPath= value; 
            }  
        }
        public bool   IsParsingRequired
        {
            set
            {
                try
                {
                    isParsingRequired = value;
                }
                catch
                {

                }
            }
            get { return isParsingRequired; }

        }
        public bool   PreserveSpace
        {
            set
            {
                try
                {
                    preserveSpace = value;
                }
                catch
                {

                }
            }
            get { return preserveSpace; }
        }
        private void  SetReaderSettings()
        {
            ReaderSettings.IgnoreComments   = false;
            ReaderSettings.IgnoreWhitespace = false;

            if (dtdPath.Equals(""))
            {
                ReaderSettings.XmlResolver = new XmlUrlResolver();
                ReaderSettings.ValidationType = ValidationType.None;
                //ReaderSettings.ProhibitDtd = false;
                ReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
            }
            else
            {
                ReaderSettings.XmlResolver = new XmlUrlResolver();
                ReaderSettings.DtdProcessing = DtdProcessing.Parse;
                ReaderSettings.ValidationType = ValidationType.DTD;
                //ReaderSettings.ProhibitDtd = true;
                //ReaderSettings.ValidationType = ValidationType.DTD;
            }
        }
        private bool  LoadXmlFile()
        {
            string tempXMlPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml";
            try
            {
                StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));


                //xmlStr.Replace("<td:cross-ref refid=\"cor1\"></td:cross-ref>", "");
                xmlStr.Replace("<td:bold>", "&lt;b&gt;");
                xmlStr.Replace("<td:italic>", "&lt;i&gt;");


                xmlStr.Replace("<td:inf>", "&lt;sub&gt;");
                xmlStr.Replace("<td:sup>", "&lt;sup&gt;");

                xmlStr.Replace("<td:bold loc=\"pre\">", "&lt;b&gt;");
                xmlStr.Replace("<td:italic loc=\"pre\">", "&lt;i&gt;");
                xmlStr.Replace("<td:inf loc=\"pre\">", "&lt;sub&gt;");
                xmlStr.Replace("<td:sup loc=\"pre\">", "&lt;sup&gt;");
                

                xmlStr.Replace("</td:bold>", "&lt;/b&gt;");
                xmlStr.Replace("</td:italic>", "&lt;/i&gt;");
                xmlStr.Replace("</td:inf>", "&lt;/sub&gt;");
                xmlStr.Replace("</td:sup>", "&lt;/sup&gt;");

                xmlStr.Replace("<td:cross-out>", "&lt;strike&gt;");
                xmlStr.Replace("</td:cross-out>", "&lt;/strike&gt;");
                

                xmlStr.Replace("&", "#$#");
                xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
                xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");

                if (IsChangedDtdPath)
                {
                    int ePos = xmlStr.ToString().IndexOf(".dtd");
                    if (ePos > 0)
                    {
                        int sPos = xmlStr.ToString().LastIndexOf("\"", ePos);
                        if (sPos > 0)
                        {
                            ePos = ePos + 4;
                            sPos++;
                            string str = xmlStr.ToString().Substring(sPos, ePos - sPos);
                            if (!str.Equals(""))
                            {
                                if (!File.Exists(ChangedDtdPath))
                                {
                                    Console.WriteLine("DTD could not be found");
                                    Console.WriteLine("Please check this path: " + ChangedDtdPath);
                                    Console.WriteLine("Press any key to exit");
                                    Console.ReadLine();
                                    Environment.Exit(0);
                                }
                                xmlStr.Replace(str, ChangedDtdPath);
                            }
                        }
                    }
                }

                File.WriteAllText(tempXMlPath, xmlStr.ToString());
                ReaderSettings.XmlResolver = new XmlUrlResolver();
                ReaderSettings.DtdProcessing = DtdProcessing.Parse;
                ReaderSettings.ValidationType = ValidationType.None;
                ReaderSettings.IgnoreComments = false;
                ReaderSettings.IgnoreWhitespace = false;
                Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
                
                MyXmlDocument = new XmlDocument();
                if (preserveSpace)
                    MyXmlDocument.PreserveWhitespace = true;

                

                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");
                
            }
            catch (XmlException ex)
            {
                if (ExceptionFired != null)
                {
                    ExceptionFired(ex);
                }
                Console.WriteLine("Error in xml file. Please check..");
                Console.WriteLine("Error :" + ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
                return false;
            }
            finally
            {
                Reader.Close();
                File.Delete(tempXMlPath);
            }
            return true;
        }
        public string GetDOI()
        {
            string DOI = "";
            XmlNodeList DoiNode = MyXmlDocument.GetElementsByTagName("td:title");
            if (DoiNode.Count > 0)
            {
                DOI = DoiNode[0].InnerText;
                //if (DoiNode[0].ParentNode.Name.Equals("item-info"))
                //{
                //    DOI = DoiNode[0].InnerText;
                //}
            }
            return DOI;
        }
        public  bool  MakeXHtml()
        {
            //try
            //{
                XmlDocument xmlDoc       = new XmlDocument();
                xmlDoc.InnerXml          = MyXmlDocument.InnerXml;

                string HtmlFileName =   Path.GetDirectoryName(XmlFilePath)    +"\\index.htm";
                Xml2HTML Xml2HTMLObj     = new Xml2HTML( xmlDoc,HtmlFileName);
                
                Xml2HTMLObj.IndexXMLName = Path.GetDirectoryName(XmlFilePath) + "\\index.xml";

                
                Xml2HTMLObj.MakeXhtml();

                //////////////////Get English title...
                Xml2HTMLObj.IndexInfo.ENTitle = GetArticleTitle();
                Xml2HTMLObj.IndexInfo.ArticleLanguage = GetArticleLanguage();
                Xml2HTMLObj.MakeIndexXml();

            
                string JIDAID = Xml2HTMLObj.jid + Xml2HTMLObj.aid;
                
                ////////////////////////////////////////////////////To make pubmed Xml for AOP
                if (Array.IndexOf(Program.PubmedNotRequired, JIDAID.ToUpper()) != -1)
                { 
                }
                else if (Xml2HTML.AOPArticle)
                {
                    xmlDoc.InnerXml = MyXmlDocument.InnerXml;


                    string PubmedFileName = Path.GetDirectoryName(Program.ProcessPath) + "\\aop_" + Xml2HTMLObj.jid.ToLower() + "_" + Xml2HTMLObj.aid + ".xml";
                    PubmedInfo PubmedInfoObj = new PubmedInfo(xmlDoc, PubmedFileName);

                    
                    AssignArticleID(PubmedInfoObj);


                    if (Xml2HTMLObj.IndexInfo.EnAbstract.Equals(""))
                        PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.FrAbstract;
                    else
                        PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.EnAbstract;

                    PubmedInfoObj.ArticleId = Xml2HTMLObj.aid;

                    string[] PubMedJID = { "" };
                    string Pubmed = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Pubmed.txt";
                    PubMedJID = File.ReadAllLines(Pubmed);
                    if (Array.IndexOf(PubMedJID, Xml2HTMLObj.jid.ToUpper()) != -1)
                        PubmedInfoObj.CreatePubmedXML();
                }
                else if (Program.PubMedIssueWithoutReplace)
                {
                    xmlDoc.InnerXml = MyXmlDocument.InnerXml;
                    string PubmedFileName = Path.GetDirectoryName(Program.ProcessPath) + "\\" + Xml2HTMLObj.jid.ToLower() + "_" + Program.VOL + "_" + Program.ISSUE + ".xml";

                    PubmedInfo PubmedInfoObj = new PubmedInfo(xmlDoc, PubmedFileName);


                    if (XmlFilePath.IndexOf("-") != -1)
                    {
                        string[] PageRange = Path.GetFileName(Path.GetDirectoryName(XmlFilePath)).Split('-');
                        if (PageRange.Length > 2)
                        {
                            PubmedInfoObj.FirstPage = PageRange[0].TrimStart('0');
                            PubmedInfoObj.LastPage = PageRange[1].TrimStart('0');
                        }
                    }

                    PubmedInfoObj.Volume = Program.VOL;
                    PubmedInfoObj.Issue = Program.ISSUE;
                    PubmedInfoObj.JID = Xml2HTMLObj.jid;
                    PubmedInfoObj.ArticleId = Xml2HTMLObj.aid;

                    if (Xml2HTMLObj.IndexInfo.EnAbstract.Equals(""))
                        PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.FrAbstract;
                    else
                        PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.EnAbstract;

                    PubmedInfoObj.CreatePubmedXML();

                    string[] PubmedXml = File.ReadAllLines(PubmedFileName);
                    for (int i = 3; i < PubmedXml.Length - 1; i++)
                    {
                        Program.IssuePubmedXML.AppendLine(PubmedXml[i]);
                    }
                }
                ////////////////////////////////////////////////////To make pubmed Xml for ISSUE
                else if (Program.PubMedIssue)
                {

                    string[] PubMedJID = { "" };
                    string Pubmed = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Pubmed.txt";
                    PubMedJID = File.ReadAllLines(Pubmed);
                    if (Array.IndexOf(PubMedJID, Xml2HTMLObj.jid.ToUpper()) == -1)
                    {

                    }
                    else
                    {
                        xmlDoc.InnerXml = MyXmlDocument.InnerXml;
                        string PubmedFileName = Path.GetDirectoryName(Program.ProcessPath) + "\\" + Xml2HTMLObj.jid.ToLower() + "_" + Program.VOL + "_" + Program.ISSUE + ".xml";

                        

                        PubmedInfo PubmedInfoObj = new PubmedInfo(xmlDoc, PubmedFileName);
                        string PMID;
                        string PubXML;
                        string ArticleID = "";
                        //if (PubmedInfo.PubMedID.ContainsKey(JIDAID))// hardcode by sunil
                        if (true)
                        {
                             PMID = GetDOI();
                            //http://www.ncbi.nlm.nih.gov/pubmed/?term=10.1684%2Fejd.2015.2550
                            
                            //PMID = PubmedInfo.PubMedID[JIDAID];

                            if (!string.IsNullOrEmpty(PMID))
                            {
                                //PMID = PMID.Replace("/", "%2F");

                                Console.WriteLine("410 Send request to get pubmed id using doi  : " + PMID);
                                PubXML = Program.Pubmed(PMID);
                                Pubmed PubmedOBJ = new Pubmed(PubXML);
                                Console.WriteLine("Response XML : " + PubXML);

                                ArticleID = PubmedOBJ.GetNodeValue("ArticleIdList");
                                int sPos = ArticleID.IndexOf("<ArticleId IdType=\"pubmed\">");
                                int ePos = 0;

                                if (sPos >= 0)
                                    ePos = ArticleID.IndexOf("</ArticleId>", sPos);

                                if (sPos >= 0 && ePos > 0)
                                {
                                    int LEN = (ePos - sPos) + "</ArticleId>".Length;
                                    string TempStr = ArticleID.Substring(sPos, LEN);
                                    ArticleID = ArticleID.Replace(TempStr, "");

                                    
                                    PubmedInfoObj.PMID = PubmedOBJ.GetNodeValue("PMID");
                                }

                                PubmedInfoObj.ArticleIdList = ArticleID;
                            }
                            //}
                            //else
                            //{
                            //    PubXML = Program.Pubmed(Xml2HTMLObj.Title.Replace("#$#","&"));
                            //}


                            if (XmlFilePath.IndexOf("-") != -1)
                            {
                                string[] PageRange = Path.GetFileName(Path.GetDirectoryName(XmlFilePath)).Split('-');
                                if (PageRange.Length > 2)
                                {
                                    PubmedInfoObj.FirstPage = PageRange[0].TrimStart('0');
                                    PubmedInfoObj.LastPage = PageRange[1].TrimStart('0');
                                }
                            }
                           // if (!string.IsNullOrEmpty(ArticleID))
                            if (true)
                            {
                                PubmedInfoObj.Volume = Program.VOL;
                                PubmedInfoObj.Issue = Program.ISSUE;
                                PubmedInfoObj.JID = Xml2HTMLObj.jid;
                                PubmedInfoObj.ArticleId = Xml2HTMLObj.aid;

                                if (Xml2HTMLObj.IndexInfo.EnAbstract.Equals(""))
                                    PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.FrAbstract;
                                else
                                    PubmedInfoObj.Abstract = Xml2HTMLObj.IndexInfo.EnAbstract;

                                PubmedInfoObj.CreatePubmedXML();

                                string[] PubmedXml = File.ReadAllLines(PubmedFileName);

                                for (int i = 3; i < PubmedXml.Length - 1; i++)
                                {
                                    Program.IssuePubmedXML.AppendLine(PubmedXml[i]);
                                }
                            }
                        }
                        else
                        { 
                            Console.WriteLine( JIDAID + " pubmed id could not found");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadLine();
                        }
                    }
                }
                //////////////////////////////////////////////////To make pubmed Xml
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("HTML could not be generated due to some exceptional cases.");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine("Press any key to ext...");
            //    Console.ReadLine();
            //    Environment.Exit(0);
            //    return false;
            //}

            //Xml2HTMLObj.IndexInfo.DateParu


                NLMXML.Program.ProcessStart(XmlFilePath, Xml2HTMLObj);
            return true;
        }
        private void AssignArticleID(PubmedInfo PubmedInfoObj)
        {
                         string PMID;
                        string PubXML;
                        string ArticleID = "";
                        //if (PubmedInfo.PubMedID.ContainsKey(JIDAID))// hardcode by sunil
                          PMID = GetDOI();
                            //http://www.ncbi.nlm.nih.gov/pubmed/?term=10.1684%2Fejd.2015.2550
                            
                            //PMID = PubmedInfo.PubMedID[JIDAID];

                            if (!string.IsNullOrEmpty(PMID))
                            {
                                //PMID = PMID.Replace("/", "%2F");

                                Console.WriteLine("Send request to get pubmed id using doi : " + PMID);
                                PubXML = Program.Pubmed(PMID);
                                Pubmed PubmedOBJ = new Pubmed(PubXML);
                                Console.WriteLine("Response XML : " + PubXML);

                                ArticleID = PubmedOBJ.GetNodeValue("ArticleIdList");
                                int sPos = ArticleID.IndexOf("<ArticleId IdType=\"pubmed\">");
                                int ePos = 0;

                                if (sPos >= 0)
                                    ePos = ArticleID.IndexOf("</ArticleId>", sPos);

                                if (sPos >= 0 && ePos > 0)
                                {
                                    int LEN = (ePos - sPos) + "</ArticleId>".Length;
                                    string TempStr = ArticleID.Substring(sPos, LEN);
                                    ArticleID = ArticleID.Replace(TempStr, "");

                                    
                                    PubmedInfoObj.PMID = PubmedOBJ.GetNodeValue("PMID");
                                }

                                PubmedInfoObj.ArticleIdList = ArticleID;
                            }
        }
        public  bool  LoadXml()
        {
            try
            {
                if (!XmlFilePath.Equals(""))
                {
                    SetReaderSettings();
                    if (LoadXmlFile())
                    {
                        XmlNode Node = MyXmlDocument.GetElementsByTagName("jid")[0];
                        if (Node != null)
                            _JID = Node.InnerText;

                        Node = MyXmlDocument.GetElementsByTagName("aid")[0];
                        if (Node != null)
                            _AID = Node.InnerText;

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (XmlException ex)
            {
                if (ExceptionFired != null)
                {
                    ExceptionFired(ex);
                }
                return false;
            }
        }
        public int CountElement(string EleName, out string ImgStr)
        {
            ImgStr = string.Empty;
            int count = 0;
            string Str = "";
            string AtrVal = "";
            if (EleName.Equals("td:link"))
            {
                XmlNodeList NL = MyXmlDocument.GetElementsByTagName(EleName);
                foreach (XmlNode chNode in NL)
                {
                    if (chNode.Attributes.GetNamedItem("locator") != null)
                    {
                        AtrVal = chNode.Attributes.GetNamedItem("locator").Value;
                        if (Str == "")
                        {
                            Str = "#" + AtrVal + "#";
                            count++;
                        }
                        else if (Str.IndexOf("#" + AtrVal + "#") == -1)
                        {
                            Str = Str + "#" + AtrVal + "#";
                            count++;
                        }
                    }
                }
                ImgStr = Str;
                return count;
            }
            else
                 return MyXmlDocument.GetElementsByTagName(EleName).Count;
        }
        public int CountElement(string EleName)
        {
         
            int count = 0;
            string Str = "";
            string AtrVal = "";
            if (EleName.Equals("td:link"))
            {
                XmlNodeList NL = MyXmlDocument.GetElementsByTagName(EleName);
                foreach (XmlNode chNode in NL)
                {
                    if (chNode.Attributes.GetNamedItem("locator") != null)
                    {
                        AtrVal = chNode.Attributes.GetNamedItem("locator").Value;
                        if (Str == "")
                        {
                            Str = "#" + AtrVal + "#";
                            count++;
                        }
                        else if (Str.IndexOf("#" + AtrVal + "#") == -1)
                        {
                            Str = Str + "#" + AtrVal + "#";
                            count++;
                        }
                    }
                }
             
                return count;
            }
            else
                return MyXmlDocument.GetElementsByTagName(EleName).Count;
        }

        public string GetArticleLanguage()
        {
            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                    return "en";

            }
            return "fr";
        }
        public string GetArticleTitle()
        {
            string AT = "";
            XmlNodeList NL;

            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                {
                    NL = MyXmlDocument.GetElementsByTagName("td:title");
                    if (NL.Count > 0)
                    {
                        AT = NL[0].InnerText;
                        return AT;
                    }
                }
                else
                {
                    string ENGJID = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ENGJID.txt";
                    if (File.Exists(ENGJID))
                    {
                        ENGJID = File.ReadAllText(ENGJID);
                        if (ENGJID.IndexOf(JID) != -1)
                        //if (true)
                        {
                            NL = MyXmlDocument.GetElementsByTagName("td:title");
                            if (NL.Count > 0)
                            {
                                AT = NL[0].InnerText;
                                return AT;
                            }
                        }
                    }
                }
            }

            NL = MyXmlDocument.GetElementsByTagName("td:alt-title");
            if (NL.Count > 0)
            {
                AT = NL[0].InnerText;
            }
            return AT;
        }

    }

    class Pubmed
    {
        //private string _ISSN;
        XmlDocument    _XmlDocument = new XmlDocument ();
        public Pubmed(string XmlStr)
        {
            XmlStr = XmlStr.Replace("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", "");
            _XmlDocument.LoadXml(XmlStr);
        }
        

        public string GetNodeValue(string NodeName)
        {
            XmlNodeList NL = _XmlDocument.GetElementsByTagName(NodeName);
            string NodeValue = "";
            if (NL.Count == 1)
            {
                NodeValue= NL[0].InnerXml;
            }

            return NodeValue;
        }
    }

    class ChekFolderSequence
    {
        StringBuilder LogStr = new StringBuilder();

        string _DirPath = "";

        /// <summary>
        /// Set directory path to be used.
        /// </summary>
        /// 

        public string DirPath
        {
            get { return _DirPath ;}
            set { _DirPath=value ;}
        }

        public string ProcessLog
        {
            get { return LogStr.ToString(); }
        }

        /// <summary>
        /// To check sequence of directory's name.
        /// </summary>
        
        public string[]    GetSubFolder()
        {
            string[] SubFolder = Directory.GetDirectories(DirPath,"*-*",SearchOption.AllDirectories);
            for (int i = 0; i < SubFolder.Length; i++ )
            {
                SubFolder.SetValue(Path.GetFileName(SubFolder[i]), i);
            }
            return SubFolder;
        }

        public void CheckFolderSeq()
        {
            string[] SubFolder        = GetSubFolder();
            
            List<DArray> PageRangeSeq = new List<DArray>();

            for(int i=0 ; i<SubFolder.Length ; i++)
            {
                if (SubFolder[i].IndexOf('-') == -1)
                    LogStr.AppendLine(SubFolder[i] + " folder name must be page range and have '-'.");
                else
                {
                    bool Numeric=true;
                    string[] PageRange = SubFolder[i].Split('-');
                    if (!IsNumeric(PageRange[0]))
                    {
                        LogStr.AppendLine(SubFolder[i] + " folder name must be start with numeric.");
                        Numeric = false;
                    }
                    if (!IsNumeric(PageRange[1]))
                    {
                        LogStr.AppendLine(SubFolder[i] + " folder name must be end with numeric.");
                        Numeric = false;
                    }
                    if (Numeric)
                    {
                        int sPage = Int32.Parse(PageRange[0]); //////////// Start Page no. 
                        int ePage = Int32.Parse(PageRange[1]); //////////// End   Page no. 
                        PageRangeSeq.Add(new DArray(sPage,ePage));
                        if (sPage > ePage)
                        {
                            LogStr.AppendLine(SubFolder[i] + " folder name must be like this 'StartPageNo-EndPageNo'.");
                        }
                    }
                }
            }

            SubFolder = Directory.GetDirectories(DirPath, "*-*", SearchOption.AllDirectories); 
            for (int i = 0; i < SubFolder.Length; i++)
            {
                if (Directory.GetFiles(SubFolder[i],"*.xml").Length!=1)
                    LogStr.AppendLine(SubFolder[i] + " must have article's xml file.");
                //$$$
                if (Directory.GetFiles(SubFolder[i], "*.pdf").Length < 0)
                    LogStr.AppendLine(SubFolder[i] + " must have article's pdf file.");
            }

            PageRangeSeq.Sort();
            for (int i = 0; i < PageRangeSeq.Count-1; ++i)
            {
                if ((PageRangeSeq[i].Col2 == PageRangeSeq[i + 1].Col1) || (PageRangeSeq[i].Col2 + 1 == PageRangeSeq[i + 1].Col1))
                { }
                else
                { LogStr.AppendLine(PageRangeSeq[i + 1].Col1 + " is not in squence."); }
            }

            if (LogStr.Length > 0)
            {
                Console.WriteLine(LogStr.ToString());
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();

                LogStr = new StringBuilder("");
                //Environment.Exit(0);
            }
        }

        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
       
        public void CheckImageInXML()
        {
            string  DoctypeFile = "D:\\Doctype.txt";
            StringBuilder DocTypeFromFolderName = new StringBuilder ("");
            if (File.Exists(DoctypeFile))
                File.Delete(DoctypeFile);


            Console.WriteLine("Process start to check images callout in xml.");
            Console.WriteLine("Please wait....");
            string[] SubFolder = Directory.GetDirectories(DirPath, "*-*", SearchOption.AllDirectories);//Directory.GetDirectories(DirPath);

            if (SubFolder.Length > 0)
            {
                XmlInfo XmlInfoObj = new XmlInfo();
                int ImageCountInXml = 0;
                int JPGCount = 0;
                for (int i = 0; i < SubFolder.Length; i++)
                {
                   if (Directory.GetFiles(SubFolder[i], "*.xml").Length == 1)
                   {
                        string XmlPath = Directory.GetFiles(SubFolder[i], "*.xml")[0];
                        XmlInfoObj = new XmlInfo(XmlPath);
                        XmlInfoObj.IsChangedDtdPath = true;
                        XmlInfoObj.ChangedDtdPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TD_JLE_Journal_v1.0.dtd";

                        XmlInfoObj.LoadXml();


                        string GrFxStr; 
                        ImageCountInXml = XmlInfoObj.CountElement("td:link", out GrFxStr);
                        JPGCount = Directory.GetFiles(SubFolder[i], "*.jpg").Length;

                        if (JPGCount != ImageCountInXml)
                        {
                            LogStr.AppendLine("--------------------------------------");
                            LogStr.AppendLine("To check folder: " + SubFolder[i]);
                            LogStr.AppendLine("No. of images callout in xml does not match with jpg files in folder.");
                            LogStr.AppendLine("No. of JPG files: " + JPGCount);
                            LogStr.AppendLine("No. of image call out in xml: " + ImageCountInXml);
                            LogStr.AppendLine("--------------------------------------");
                        }
                        if (!string.IsNullOrEmpty(GrFxStr))
                        {
                            string[] GrFx = GrFxStr.Split(new char[]{'#'},StringSplitOptions.RemoveEmptyEntries);
                            List<string> JPGFilesList = new List<string> ();
                            string [] JPGFiles = Directory.GetFiles(SubFolder[i], "*.jpg");
                            foreach (string JPGFile in JPGFiles)
                            {
                                JPGFilesList.Add(Path.GetFileNameWithoutExtension(JPGFile).ToUpper());
                            }

                            foreach(string Gr in GrFx)
                            {
                                if (JPGFilesList.IndexOf(Gr.ToUpper()) == -1)
                                {
                                        LogStr.AppendLine("--------------------------------------");
                                        LogStr.AppendLine(Gr + ".jpg is missing in  article folder" + SubFolder[i] );
                                        LogStr.AppendLine("image callout in xml does not match with jpg file in folder.");
                                        LogStr.AppendLine("image call out in xml: " +Gr);
                                        LogStr.AppendLine("--------------------------------------");
                                }
                            }
                        }

                        if (XmlInfoObj.CountElement("td:title")==0)
                        {
                            LogStr.AppendLine("--------------------------------------");
                            LogStr.AppendLine("To check folder: " + SubFolder[i]);
                            LogStr.AppendLine("Article title is missing in xml.");
                            LogStr.AppendLine("--------------------------------------");
                        }

                        if (XmlInfoObj.CountElement("td:dochead") == 0)
                        {
                            LogStr.AppendLine("--------------------------------------");
                            LogStr.AppendLine("To check folder: " + SubFolder[i]);
                            LogStr.AppendLine("Dochead is missing in xml.");
                            LogStr.AppendLine("--------------------------------------");
                        }


                        string JIDAID = XmlInfoObj.JID + XmlInfoObj.AID;
                        string TOCDocHead= Path.GetFileName( Path.GetDirectoryName(SubFolder[i]));
                        if (TOCDocHead.StartsWith("issue_") == false)
                        {
                            DocTypeFromFolderName.AppendLine(JIDAID + "\t" + TOCDocHead);
                        }

                        if (Program.PubMedIssue)
                        {
                            
                            string[] PubMedJID = { "" };
                            ////////////////////////////////////////////////////To make pubmed Xml for AOP
                            string Pubmed = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Pubmed.txt";
                            PubMedJID = File.ReadAllLines(Pubmed);
                            if (Array.IndexOf(PubMedJID, XmlInfoObj.JID) == -1)
                            { 
                            }
                            else if (Array.IndexOf(Program.PubmedNotRequired, JIDAID.ToUpper()) != -1)
                            {
                            }
                            else if (XmlInfoObj.GetArticleTitle().Equals(""))
                            {
                                LogStr.AppendLine("--------------------------------------");
                                LogStr.AppendLine("To check folder: " + SubFolder[i]);
                                LogStr.AppendLine("To check file: " + XmlInfoObj.FilePath);
                                LogStr.AppendLine("English title missing.");
                                LogStr.AppendLine("It is required for pubmed xml.");
                                LogStr.AppendLine("--------------------------------------");
                            }
                        }
                    }
                }


                File.WriteAllText(DoctypeFile, DocTypeFromFolderName.Replace("&colon;",":").ToString());
                CheckJunkChar(DoctypeFile);
                
            }
            else
            {
                XmlInfo XmlInfoObj = new XmlInfo();
                int ImageCountInXml = 0;
                int MathCountInXml = 0;
                int JPGCount = 0;
                int GifCount = 0;
                if (Directory.GetFiles(DirPath, "*.xml").Length == 1)
                {
                    string XmlPath = Directory.GetFiles(DirPath, "*.xml")[0];
                    XmlInfoObj = new XmlInfo(XmlPath);
                    XmlInfoObj.IsChangedDtdPath = true;
                    XmlInfoObj.ChangedDtdPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TD_JLE_Journal_v1.0.dtd";

                    XmlInfoObj.LoadXml();
                    ImageCountInXml = XmlInfoObj.CountElement("td:link");
                    MathCountInXml = XmlInfoObj.CountElement("mml:math");


                    JPGCount = Directory.GetFiles(DirPath, "*.jpg").Length;
                    GifCount = Directory.GetFiles(DirPath, "*.gif").Length;

                    if (JPGCount != ImageCountInXml)
                    {
                        LogStr.AppendLine("--------------------------------------");
                        LogStr.AppendLine("To check folder: " + DirPath);
                        LogStr.AppendLine("No. of images callout in xml does not match with jpg files in folder.");
                        LogStr.AppendLine("No. of JPG files: " + JPGCount);
                        LogStr.AppendLine("No. of image call out in xml: " + ImageCountInXml);
                        LogStr.AppendLine("--------------------------------------");
                    }


                    string GrFxStr;
                    ImageCountInXml = XmlInfoObj.CountElement("td:link", out GrFxStr);
                    JPGCount = Directory.GetFiles(DirPath, "*.jpg").Length;

                    if (JPGCount != ImageCountInXml)
                    {
                        LogStr.AppendLine("--------------------------------------");
                        LogStr.AppendLine("To check folder: " + DirPath);
                        LogStr.AppendLine("No. of images callout in xml does not match with jpg files in folder.");
                        LogStr.AppendLine("No. of JPG files: " + JPGCount);
                        LogStr.AppendLine("No. of image call out in xml: " + ImageCountInXml);
                        LogStr.AppendLine("--------------------------------------");
                    }
                    if (!string.IsNullOrEmpty(GrFxStr))
                    {
                        string[] GrFx = GrFxStr.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> JPGFilesList = new List<string>();
                        string[] JPGFiles = Directory.GetFiles(DirPath, "*.jpg");
                        foreach (string JPGFile in JPGFiles)
                        {
                            JPGFilesList.Add(Path.GetFileNameWithoutExtension(JPGFile).ToUpper());
                        }

                        foreach (string Gr in GrFx)
                        {
                            if (JPGFilesList.IndexOf(Gr.ToUpper()) == -1)
                            {
                                LogStr.AppendLine("--------------------------------------");
                                LogStr.AppendLine(Gr + ".jpg is missing in  article folder" + DirPath);
                                LogStr.AppendLine("image callout in xml does not match with jpg file in folder.");
                                LogStr.AppendLine("image call out in xml: " + Gr);
                                LogStr.AppendLine("--------------------------------------");
                            }
                        }
                    }

                    if (MathCountInXml>0)
                    {
                        string FMSHEStripnsPath = @"\\172.16.3.110\jlestripins\" + XmlInfoObj.JID + "\\" + XmlInfoObj.AID;
                        //string LocalStripnsPath =           DirPath +"\\" + XmlInfoObj.JID + "\\" + XmlInfoObj.AID;
                        string LocalStripnsPath = DirPath;

                        //if (Directory.Exists(StripnsPath))
                        //{
                            XmlNodeList NL = XmlInfoObj.xmlDocument.GetElementsByTagName("mml:math");
                            foreach (XmlNode Math in NL)
                            {
                                if (Math.Attributes.GetNamedItem("altimg") != null)
                                {
                                    string FMSHEGifFile = FMSHEStripnsPath + "\\" + Math.Attributes.GetNamedItem("altimg").Value;
                                    string LocalGifFile = LocalStripnsPath + "\\" + Math.Attributes.GetNamedItem("altimg").Value;

                                    if (File.Exists(FMSHEGifFile))
                                        File.Copy(FMSHEGifFile, LocalGifFile,true);
                                    else
                                        WriteStripinsError(Math);
                                }
                                else
                                    WriteStripinsError(Math);
                            }
                        //}
                    }

                    if (XmlInfoObj.CountElement("td:title") == 0)
                    {
                        LogStr.AppendLine("--------------------------------------");
                        LogStr.AppendLine("To check folder: " + DirPath);
                        LogStr.AppendLine("Article title is missing in xml.");
                        LogStr.AppendLine("--------------------------------------");
                    }

                    if (XmlInfoObj.CountElement("td:dochead") == 0)
                    {
                        LogStr.AppendLine("--------------------------------------");
                        LogStr.AppendLine("To check folder: " + DirPath);
                        LogStr.AppendLine("Dochead is missing in xml.");
                        LogStr.AppendLine("--------------------------------------");
                    }
                }
            }

            if (LogStr.Length > 0)
            {
                Console.WriteLine(LogStr.ToString());
                Console.WriteLine("Press any key to Exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        private void CheckJunkChar(string XMLFIlePath)
        {
             Dictionary<int, string> xmlEntity = new Dictionary<int, string>();
            string EntityPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\entity.txt";
            string[] Lines = File.ReadAllLines(EntityPath);

            foreach (string Line in Lines)
            {
                string[] Split = Line.Split(' ');
                int KeyCode = Int32.Parse(Split[0]);

                if (!xmlEntity.ContainsKey(KeyCode))
                    xmlEntity.Add(KeyCode, Split[1]);
            }

            StringBuilder Str = new StringBuilder("");
            StringBuilder value = new StringBuilder(File.ReadAllText(XMLFIlePath));

            for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] > 127)
                {
                    if (xmlEntity.ContainsKey(value[i]))
                        Str.Append(xmlEntity[value[i]]);
                    else
                        Console.WriteLine("XML entity does not exist for this character code : " + value[i]);
                }
                else if (value[i] < 0x20 && value[i] != '\t' & value[i] != '\n' & value[i] != '\r')
                {
                    if (xmlEntity.ContainsKey(value[i]))
                        Str.Append(xmlEntity[value[i]]);
                    else
                        Console.WriteLine("XML entity does not exist for this character code : " + value[i]);
                }
                else
                {
                    Str.Append(value[i]);
                }
            }
            File.WriteAllText(XMLFIlePath, Str.ToString().Replace("&","#$#"));

        }
      
        private void WriteStripinsError(XmlNode Math)
        {

            LogStr.AppendLine("--------------------------------------");
            LogStr.AppendLine("Errorr during stripns processing........");
            LogStr.AppendLine("altimg attribute missing in math coding");
            LogStr.AppendLine("Check below math coding in xml");
            LogStr.AppendLine(Math.OuterXml);
        }
    }

    public struct DArray : IComparable<DArray>
    {
        public int Col1;
        public int Col2;
        public DArray(int Col1, int Col2)
        {
            this.Col1 = Col1;
            this.Col2 = Col2;
        }
        public int CompareTo(DArray other)
        {
            return Col1.CompareTo(other.Col1);
        }
    } 

}

