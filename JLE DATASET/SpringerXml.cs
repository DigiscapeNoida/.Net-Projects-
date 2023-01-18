    using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
namespace JLEDATASET
{
    class IssueDetails
    {
         static string _IssueType = "";
         static string _Volume    = "";
         static string _ISSUE     = "";
         static string _InputPath = "";
         static string _Year      = "";
         static string _JID = "";
         static string _Month     = "";
         static string _IssueArticleCount = "";


         public static string JID
         {
             set { _JID = value; }
             get { return _JID; }
         }

        public static string IssueArticleCount
        {
            set { _IssueArticleCount = value; }
            get { return _IssueArticleCount; }
        }

        public static string IssueType
        { 
            set{_IssueType = value;}
            get { return _IssueType; }              
        }
        public static string Volume
        {
            set { _Volume = value; }
            get { return _Volume; }              
        }
        public static string ISSUE
        {
            set { _ISSUE = value; }
            get { return _ISSUE; }
        }
        public static string InputPath
        {
            set { _InputPath = value; }
            get { return _InputPath; }
        }
        public static string    Year
        {
            set { _Year = value; }
            get { return _Year; }

        }
        public static string Month
        {
            set { _Month = value; }
            get { return _Month; }
        }
        static IssueDetails()
        { }
    }

    class ArticleDetails : IssueDetails
    {
        XmlNamespaceManager nsmgr = null;
        string XmlFilePath;

        string _ArticleCategory = "Original article";
        string _sPage = "";
        string _ePage = "";

        string _JID = "";
        string _AID = "";
        string _DOI = "";
        string _Language = "";
        string _DocHead = "";
        string _ENArticleTitle = "";
        string _FRArticleTitle = "";
        string _ENAbstract = "";
        string _FRAbstract = "";
        string _ENKeyWord = "";
        string _FRKeyWord = "";
        string _StartPage = "";
        string _EndPage = "";
        bool _PubmedRequired;
        bool _IsFree;
        static int _SequenceNo = 0;
        
        public static int AffID = 0;
        XmlDocument MyXmlDocument = new XmlDocument();

        int GrCallOut = 0;
        int _Images = 0;

        public string ArticleCategory
        {
            set { _ArticleCategory = value; }
            get { return _ArticleCategory; }
        }

        public ArticleDetails(string InputFile)
        {
            XmlFilePath = InputFile;
            GetDatails();
        }

        public int Images
        {
            get { return _Images; }
        }

        public int GraphicCallOut
        {
            get { return GrCallOut; }
        }

        public string PageRange
        {
            get { return _StartPage.ToString() + "-" + _EndPage.ToString(); }
        }

        public string JID
        {
            set { _JID = value; }
            get { return _JID; }
        }

        public string AID
        {
            set { _AID = value; }
            get { return _AID.TrimStart('0'); }
        }

        public string DOI
        {
            get { return _DOI; }
        }

        public static string SequenceNO
        {
            get
            {
                _SequenceNo++;
                return _SequenceNo.ToString();

            }
        }

        public string SPage
        {
            get { return _sPage; }
        }

        public string EPage
        {
            get { return _ePage; }
        }

        public string Language
        {
            set { _Language = value; }
            get { return _Language; }
        }

        public string DocHead
        {
            set { _DocHead = value; }
            get { return _DocHead.Replace("#$#", "&"); }
        }

        public string ExistENArticleTitle
        {
            get
            {
                if (!_ENArticleTitle.Equals(""))
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string ExistFRArticleTitle
        {
            get
            {
                if (!_FRArticleTitle.Equals(""))
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string ENArticleTitle
        {
            set { _ENArticleTitle = value; }
            get { return _ENArticleTitle; }
        }

        public string FRArticleTitle
        {
            set { _FRArticleTitle = value; }
            get { return _FRArticleTitle; }
        }

        public string ENAbstract
        {
            set { _ENAbstract = value; }
            get { return _ENAbstract; }
        }

        public string FRAbstract
        {
            set { _FRAbstract = value; }
            get { return _FRAbstract; }
        }

        public string ENKeyWord
        {
            set { _ENKeyWord = value; }
            get { return _ENKeyWord; }
        }

        public string FRKeyWord
        {
            set { _FRKeyWord = value; }
            get { return _FRKeyWord; }
        }

        public string StartPage
        {
            set { _sPage = value; }
            get { return _sPage; }
        }

        public string EndPage
        {
            set { _ePage = value; }
            get { return _ePage; }
        }

        public bool PubmedRequired
        {
            set { _PubmedRequired = value; }
            get { return _PubmedRequired; }
        }

        public bool IsFree
        {
            set { _IsFree = value; }
            get { return _IsFree; }
        }

        public void GetDatails()
        {
            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            nsmgr.AddNamespace("td", "http://www.thomsondigital.com/xml/common/dtd");
            nsmgr.AddNamespace("sb", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsmgr.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            nsmgr.AddNamespace("tb", "http://www.thomsondigital.com/xml/common/table/dtd");
            nsmgr.AddNamespace("tp", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");



            //////////////Count JPG file article folder

            string ArticleFolderPath = Path.GetFileName(Path.GetDirectoryName(XmlFilePath));
            _Images = Directory.GetFiles(Path.GetDirectoryName(XmlFilePath), "*.jpg").Length;



            string[] PageRange = ArticleFolderPath.Split('-');

            if (PageRange.Length == 2)
            {
                StartPage = PageRange[0].TrimStart('0');
                EndPage = PageRange[1].TrimStart('0');
            }
            else
            {
                 PageRange = ArticleFolderPath.Replace("&hyphen;","-").Split('-');
            }

            ////////////Do'nt change sequence
            XmlLoad(XmlFilePath);

            ///////////Count graphic call out in xml to check graphics existence in article folder.
            GrCallOut = CountElement("td:link");

            XmlNode Node = MyXmlDocument.GetElementsByTagName("aid")[0];
            if (Node != null)
                _AID = Node.InnerText;

            Node = MyXmlDocument.GetElementsByTagName("jid")[0];
            if (Node != null)
                 _JID = Node.InnerText;

            Node = MyXmlDocument.GetElementsByTagName("td:doi")[0];
            _DOI = Node.InnerText;

            GetDochead(nsmgr);
            GetArticleLanguage();
            GetArticleTitle();
            GetAbstract(nsmgr);
            GetKeyWords(nsmgr);
        }

        private bool XmlLoad(string InputFile)
        {
            string tempXMlPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml";
            StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));
            xmlStr.Replace("&", "#$#");
            xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
            xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");

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
                        string ChangedDtdPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TD_JLE_Journal_v1.0.dtd";
                        xmlStr.Replace(str, ChangedDtdPath);
                    }
                }
            }
            File.WriteAllText(tempXMlPath, xmlStr.ToString());


            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.ProhibitDtd = false;

            XmlReader Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            try
            {
                MyXmlDocument.PreserveWhitespace = true;
                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");
            }
            catch (XmlException ex)
            {
                //Console.WriteLine("Error message :" + ex.Message);
                return false;
            }
            finally
            {
                Reader.Close();
                File.Delete(tempXMlPath);
            }
            return true;
        }

        private int     CountElement(string EleName)
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

        private void GetArticleLanguage()
        {
            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                    _Language = "en";
                else
                    _Language = "fr";
            }
            else
            {
                _Language = "fr";
            }
        }

        private void GetArticleTitle()
        {
            XmlNodeList NL;
            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                {
                    _Language = "en";
                    NL = MyXmlDocument.GetElementsByTagName("td:title");
                    if (NL.Count > 0)
                        ENArticleTitle = NL[0].InnerText;

                }
                else if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("fr"))
                {
                    _Language = "fr";
                    NL = MyXmlDocument.GetElementsByTagName("td:title");
                    if (NL.Count > 0)
                        FRArticleTitle = NL[0].InnerText;
                }
            }
            else
            {
                string ENGJID = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ENGJID.txt";
                if (File.Exists(ENGJID))
                {
                    ENGJID = File.ReadAllText(ENGJID);
                    if (ENGJID.IndexOf(JID) != -1)
                    {
                        _Language = "en";
                        NL = MyXmlDocument.GetElementsByTagName("td:title");
                        if (NL.Count > 0)
                            ENArticleTitle = NL[0].InnerText;
                    }
                }
            }


            NL = MyXmlDocument.GetElementsByTagName("td:alt-title");
            if (NL.Count > 0)
            {
                if (_Language.Equals("fr"))
                    ENArticleTitle = NL[0].InnerText;
                else if (_Language.Equals("en"))
                    FRArticleTitle = NL[0].InnerText;
            }
        }

        private void GetAbstract(XmlNamespaceManager nsmgr)
        {
            string ABSLan = "";
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:abstract");
            string ProcessedAbstrat = "";
            for (int i = 0; i < nodeList.Count; i++)
            {

                if (nodeList[i].Attributes.GetNamedItem("xml:lang") != null)
                {
                    if (nodeList[i].Attributes.GetNamedItem("xml:lang").Value.Equals("fr"))
                        ABSLan = "fr";
                    else if (nodeList[i].Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                        ABSLan = "en";
                }

                XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);
                if (ParaList.Count == 1)
                {
                    ProcessAbstarct(ParaList[0]);
                    ProcessedAbstrat = ParaList[0].InnerXml;
                }
                else
                {
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        ProcessAbstarct(ParaList[j]);
                        ProcessedAbstrat += "<p>" + ParaList[j].InnerXml + "</p>";
                    }
                }
                ProcessedAbstrat = ProcessedAbstrat.Replace("<p></p>", "");
                if (ABSLan.Equals("fr") || nodeList[i].FirstChild.InnerXml.IndexOf("R#$#eacute;sum#$#eacute;", StringComparison.OrdinalIgnoreCase) != -1)
                    FRAbstract = ProcessedAbstrat;
                else if (ABSLan.Equals("en") || nodeList[i].FirstChild.InnerXml.IndexOf("Abstract", StringComparison.OrdinalIgnoreCase) != -1)
                    ENAbstract = ProcessedAbstrat;
            }
        }

        private void ProcessAbstarct(XmlNode node)
        {
            try
            {
                XmlNodeList TempNL = node.SelectNodes(".//td:footnote", nsmgr);
                foreach (XmlNode chNode in TempNL)
                {
                    chNode.ParentNode.RemoveChild(chNode);
                }
                XmlNodeList NL = node.SelectNodes(".//*", nsmgr);

                foreach (XmlNode chNode in NL)
                {
                    if (chNode.Name.Equals("td:list"))
                    { }
                    else if (chNode.Name.Equals("td:label"))
                    { }
                    else if (chNode.Name.Equals("td:list-item"))
                    { }
                    else if (chNode.Name.Equals("td:para"))
                    { }
                    else if (chNode.Name.StartsWith("td:cross-ref"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:inter-ref"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.NodeType == XmlNodeType.Comment)
                    {
                        node.ParentNode.RemoveChild(node);
                    }
                    else
                    {
                        //Console.WriteLine("************Warning.**************************");
                        //Console.WriteLine("Please check this element :: " + chNode.Name);
                        //Console.WriteLine("Application does not support this element in abstract node.");
                        //Console.WriteLine("Please specify this element in application.");
                        //Console.WriteLine("Press any key to exit.");
                        //Console.ReadLine();
                        //Environment.Exit(0);
                        //Console.WriteLine("**************************************");
                    }
                }

                ///////////Please dont change sequence
                StringBuilder XmlStr = new StringBuilder(node.InnerXml);

                XmlStr.Replace("<td:list-item><td:para>", "<li>");
                XmlStr.Replace("</td:para></td:list-item>", "</li>");
                XmlStr.Replace("</td:label><td:para>", " ");
                XmlStr.Replace("td:list-item", "li");
                XmlStr.Replace("td:list", "ul");
                XmlStr.Replace("td:para", "p");

                XmlStr.Replace("<td:label>", "");
                XmlStr.Replace("</td:label>", " ");
                node.InnerXml = XmlStr.ToString();
            }
            catch
            {
            }
        }

        public void GetKeyWords(XmlNamespaceManager nsmgr)
        {
            string KwdLan = "";
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:keywords");
            string ProcessedKeyWord = "";
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Attributes.GetNamedItem("xml:lang") != null)
                {
                    if (nodeList[i].Attributes.GetNamedItem("xml:lang").Value.Equals("fr"))
                        KwdLan = "fr";
                    else if (nodeList[i].Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                        KwdLan = "en";
                }


                XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                for (int j = 0; j < ParaList.Count; j++)
                    ProcessedKeyWord += ParaList[j].InnerXml + ", ";

                ProcessedKeyWord = ProcessedKeyWord.Trim(new char[] { ',', ' ' });

                if (KwdLan.Equals("fr") || nodeList[i].FirstChild.InnerXml.IndexOf("Mots cl", StringComparison.OrdinalIgnoreCase) != -1)
                    FRKeyWord = ProcessedKeyWord;
                else if (KwdLan.Equals("en") || nodeList[i].FirstChild.InnerXml.IndexOf("Key word", StringComparison.OrdinalIgnoreCase) != -1)
                    ENKeyWord = ProcessedKeyWord;
            }
        }

        private void GetDochead(XmlNamespaceManager nsmgr)
        {
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:dochead");
            if (nodeList.Count > 0)
            {
                XmlNodeList textfn = nodeList[0].SelectNodes(".//td:textfn", nsmgr);
                if (textfn != null && textfn.Count>0)
                {
                    //ProcessTitle(textfn[0]);
                    _DocHead = textfn[0].InnerXml;
                }
            }
            else
            {
                if (File.Exists("C:\\Doctype.txt"))
                {
                    string JIDAID = _JID + _AID;
                    string[] DocHead = File.ReadAllLines("C:\\Doctype.txt");
                    foreach (string DocType in DocHead)
                    {
                        if (DocType.StartsWith(JIDAID, StringComparison.OrdinalIgnoreCase))
                        {
                            string[] Arr = DocType.Split('\t');
                            _DocHead = Arr[1].Substring(0, 1).ToUpper() + Arr[1].Substring(1).ToLower();
                        }
                    }
                }
                // Sql
            }
        }

        public void WriteAuthor(XmlTextWriter textWriter)
        {
            string _FirstName = "";

            //string _FirstName = "";
            string _LastName  = "";
            string AffiliationIDS = "";
            //string TempStr        = "";
            string Prefix="", Suffix="", Degrees="" ,eMail="";
            //StringDictionary AFFID = new StringDictionary();
            StringCollection AFFID = new StringCollection  ();
            XmlNodeList AffNL = MyXmlDocument.GetElementsByTagName("td:affiliation");
            foreach (XmlNode node in AffNL)
            {
                if (node.Attributes.GetNamedItem("id") != null)
                {
                    AFFID.Add( node.Attributes.GetNamedItem("id").Value);
                }
            }
            for (int i = 0; i < AFFID.Count;i++ )
            {
                string RplcID = AFFID[i] + "_" + this.AID;

                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace(AFFID[i], RplcID);
            }

            if (AFFID.Count == 0 && AffNL.Count == 1)
            {
                ((XmlElement)AffNL[0]).SetAttribute("id", "aff1");
                AFFID.Add("aff1");
                string RplcID = AFFID[0] + "_" + this.AID;
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace(AFFID[0], RplcID);
            }

            XmlNodeList AuthorNL = MyXmlDocument.GetElementsByTagName("td:author");
            if (AuthorNL.Count == 1)
            {
                if (AuthorNL[0].NextSibling != null)
                {
                    if (AuthorNL[0].NextSibling.Name.Equals("td:affiliation"))
                    {
                        if (AuthorNL[0].NextSibling.Attributes.GetNamedItem("id") == null)
                        {
                            ((XmlElement)AuthorNL[0].NextSibling).SetAttribute("id", "AFF1_" + this.AID);
                        }
                    }
                }
            }

            for (int X = 0; X < AuthorNL.Count; X++)
            {
                 XmlNode node= AuthorNL[X];
                _FirstName = "";
                _LastName  = "";
                 AffiliationIDS = "";
                 Prefix   = "";
                 Suffix   = "";
                 Degrees  = "";

                //foreach (XmlNode chnode in node)
                for (int i=0; i<node.ChildNodes.Count;i++)
                {
                    XmlNode chnode = node.ChildNodes[i];
                    if (!chnode.Equals("") && chnode.NodeType == XmlNodeType.Element)
                        chnode.InnerXml = chnode.InnerXml.Replace("#$#lt;sup#$#gt;a#$#lt;/sup#$#gt;", "#$##x00AA;");
                    if (chnode.Name.Equals("td:e-address"))
                        eMail=chnode.InnerText;
                    else if (chnode.Name.Equals("td:suffix"))
                        Suffix = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:initials"))
                        Prefix = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:degrees"))
                        Degrees = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:given-name"))
                        _FirstName = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:surname"))
                        _LastName = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:cross-ref"))
                    {
                        if (chnode.Attributes.GetNamedItem("refid") != null)
                        {
                            string ID     =   chnode.Attributes.GetNamedItem("refid").Value;

                            if (!ID.StartsWith("cor") && !ID.StartsWith("fn"))
                               AffiliationIDS = AffiliationIDS + " " + ID;
                        }
                        AffiliationIDS=AffiliationIDS.Trim();
                    }
                }
                if (AffiliationIDS.Equals(""))
                {
                    if (node.NextSibling != null)
                    {
                        if (node.NextSibling.Attributes.GetNamedItem("id") != null)
                        {
                            AffiliationIDS = node.NextSibling.Attributes.GetNamedItem("id").Value;
                        }
                    }
                }
                if (_FirstName.Equals(""))
                {
                    Console.WriteLine("Author information missing.........");
                    Console.WriteLine("First Name  not define in td:author tag");
                    Console.WriteLine("Please First Name must be define in td:author tag");
                    Console.WriteLine("Please define in xml for further process : ");
                    Console.WriteLine("Please press any key to exit.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                if (_LastName.Equals(""))
                {
                    Console.WriteLine("Author information missing.........");
                    Console.WriteLine("Last Name  not define in td:author tag");
                    Console.WriteLine("Please Last Name must be define in td:author tag");
                    Console.WriteLine("Please define in xml for further process : ");
                    Console.WriteLine("Please press any key to exit.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                if (AffiliationIDS.StartsWith("fn"))
                { 
                }

                textWriter.WriteStartElement("Author");
                    if (!AffiliationIDS.Equals(""))
                    {
                        textWriter.WriteAttributeString("AffiliationIDS", AffiliationIDS);
                    }
                    textWriter.WriteStartElement("AuthorName");
                        
                        if (!Prefix.Equals(""))
                             textWriter.WriteElementString("Prefix", Prefix);

                             textWriter.WriteElementString("GivenName",_FirstName);
                             textWriter.WriteElementString("FamilyName", _LastName);

                        if (!Suffix.Equals(""))
                            textWriter.WriteStartElement("Suffix",Suffix);
                        if (!Degrees.Equals(""))
                            textWriter.WriteElementString("Degrees", Degrees);

                textWriter.WriteEndElement();//////////////////AuthorName Close
              //<Contact><Email>tobias.loddenkemper@childrens.harvard.edu</Email></Contact>
                if (!eMail.Equals(""))
                {
                   textWriter.WriteStartElement("Contact");
                   textWriter.WriteElementString("Email",eMail);
                   textWriter.WriteEndElement();//////////////////Contact Close
                }
                textWriter.WriteEndElement();//////////////////Author Close
            }
        }

        public void WriteAffilation(XmlTextWriter textWriter)
        {

            if (_AID.Equals("0411"))
            { 
            }
            string ID="";
            string OrgDivision="", OrgName="", City="", Country = "";
            XmlNodeList AuthorNL = MyXmlDocument.GetElementsByTagName("td:affiliation");
            foreach (XmlNode node in AuthorNL)
            {
                if (node.Attributes.GetNamedItem("id") != null)
                {
                    ID = node.Attributes.GetNamedItem("id").Value;
                    //AffID++;
                    //ID = "AFFID" + AffID.ToString();
                }
                else
                {
                    AffID++;
                    ID = "AFF" + AffID.ToString() +"_" + this.AID;
                }

                int CityPos=node.LastChild.InnerXml.IndexOf("<!--");
                string TempStr="";
                if (CityPos != -1)
                {
                    TempStr = node.LastChild.InnerXml.Substring(0, CityPos);
                    node.LastChild.InnerXml = node.LastChild.InnerXml.Substring(CityPos);

                    if (TempStr.IndexOf(',') != -1)
                    {
                        //string[] ARR = TempStr.Split(',');
                        //OrgDivision = ARR[0];
                        //OrgName = ARR[1];
                        int Pos = TempStr.IndexOf(',');
                        OrgDivision = TempStr.Substring(0, Pos);
                        OrgName = TempStr.Substring(Pos);
                        OrgName = OrgName.Trim(new char []{',',' '});
                    }
                    else
                    {
                        OrgDivision = TempStr;
                    }
                }
                else
                {
                    TempStr = node.LastChild.InnerXml;
                    if (TempStr.IndexOf(',') != -1)
                    {
                        int Pos = TempStr.IndexOf(',');
                        OrgDivision = TempStr.Substring(0, Pos);
                        OrgName = TempStr.Substring(Pos);
                        OrgName = OrgName.Trim(new char[] { ',', ' ' });
                    }
                    else
                    {
                        OrgDivision = node.LastChild.InnerXml;
                    }
                }


                node.InnerXml = node.InnerXml.Replace("<!--", "").Replace("-->", "");
                foreach (XmlNode chnode in node.LastChild)
                {
                    if (chnode.Name.Equals("city"))
                        City = chnode.InnerXml;
                    else if (chnode.Name.Equals("country"))
                        Country = chnode.InnerXml;
                }

                textWriter.WriteStartElement("Affiliation");
                    if (!ID.Equals(""))
                    textWriter.WriteAttributeString("ID", ID);

                    textWriter.WriteElementString("OrgDivision",OrgDivision);
                    textWriter.WriteElementString("OrgName",OrgName);
                    textWriter.WriteStartElement("OrgAddress"); 
                    textWriter.WriteElementString("City",City);
                    textWriter.WriteElementString("Country",Country);
                    textWriter.WriteEndElement();                           
                textWriter.WriteEndElement();
            }
        }
    }

    class SpringerXml
    {
        string _JournalID   = "";
        string _InPutPath   = "";

        string _SpringerXml = "";

        string JournalInfo = "";

        StringCollection PdfFileInfo = new StringCollection();

        ArticleDetails ArticleDetailsOBJ;

        XmlTextWriter  textWriter;

        public SpringerXml(string InPutPath)
        {
            InitialProcess(IssueDetails.JID);
            
           //string FileName  ="13315_" + IssueDetails.Volume + "_" + IssueDetails.ISSUE + "_jobsheet_500.xml";

            string FileName = _JournalID +  "_" + IssueDetails.Volume + "_" + IssueDetails.ISSUE + "_jobsheet_500.xml";
            

            //13315_12_4_jobsheet_500.xml
           _InPutPath = InPutPath;
           _SpringerXml = Path.GetDirectoryName(InPutPath) + "\\" + FileName;
            textWriter = new XmlTextWriter(_SpringerXml, Encoding.UTF8);
            
            textWriter.Indentation = 1;
            textWriter.IndentChar = '\t';
            textWriter.Formatting = Formatting.Indented;
        }

        public  void CreateSpringerXml()  
        {
            if (_InPutPath.Equals(""))
            {
                Console.WriteLine("Please provide the input path for process.");
                return ;
            }
            if (!Directory.Exists(_InPutPath))
            {
                Console.WriteLine( _InPutPath + " does not exist.");
                return ;
            }
            WriteRootElement();

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(_SpringerXml));

            XmlStr.Replace("<AuthorGroup />", "");
            XmlStr.Replace("<City />", "<City></City>");
            XmlStr.Replace("<Country />", "<Country></Country>");
            XmlStr.Replace("<OrgName />", "<OrgName></OrgName>");
            

            XmlStr.Replace("#$#", "&");

            XmlStr.Replace("</PublisherInfo>", "</PublisherInfo>" + JournalInfo);
	
            XmlStr = ReplacePubmedEntity(XmlStr.ToString());
            XmlStr = ReplaceEntity(XmlStr.ToString());
            File.WriteAllText(_SpringerXml, XmlStr.ToString());
            SpringerParsing(_SpringerXml);

        }

        public bool SpringerParsing(string str)
        {
            //DTD\PUBMED\PubMed.dtd
            //D:\NewProject\JLEDataSet\JLEDATASET\bin\Debug\DTD\PUBMED\PubMed.dtd
            try
            {
                string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string ValidationPath = ExeLoc + "\\validation";
                string SpringerDTDPath = ExeLoc + "\\DTD\\Springer\\A++V2.4JobSheetV2.4.1.dtd";

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = ValidationPath + "\\Parse.BAT";
                myProcess.StartInfo.Arguments = "\"" + str + "\"";
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                Console.WriteLine("Process strat to Parse Pubmed Xml file................");

                myProcess.Start();
                myProcess.WaitForExit();

                string LogFIle = str + ".err";

                FileInfo FInfo = new FileInfo(LogFIle);
                if (FInfo.Length == 0)
                {
                    File.Delete(LogFIle);

                    

                    string RplcStr = @"http://devel.springer.de/A++/V2.4/DTD/A++V2.4JobSheetV2.4.1.dtd";

                    File.WriteAllText(str, File.ReadAllText(str).Replace(SpringerDTDPath, RplcStr).Replace("dtd\"[]", "dtd\""));// .Replace("\t", ""));
                    Console.WriteLine("Result :: OK");
                    return true;
                }
                else
                {
                    StringBuilder LogStr = new StringBuilder(File.ReadAllText(LogFIle));
                    LogStr.Replace(ValidationPath + "\\nsgmls:", "");
                    LogStr.Replace(str + ":", "");
                    File.WriteAllText(LogFIle, LogStr.ToString());
                    Process.Start("notepad", LogFIle);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + Environment.NewLine);
            }
            return false;
        }

        private void WriteRootElement()   
        {
            string ExeLoc = ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string SpringerDTDPath = ExeLoc + "\\DTD\\Springer\\A++V2.4JobSheetV2.4.1.dtd";

            textWriter.WriteStartDocument();
            textWriter.WriteDocType("JobSheet",  "-//Springer-Verlag//DTD A++ V2.4//EN", SpringerDTDPath,"");
                textWriter.WriteStartElement("JobSheet");
                textWriter.WriteAttributeString("JobSheetDate", string.Format("{0:yyyy-MM-dd 12:00:00}", DateTime.Today));
                textWriter.WriteAttributeString("Supplier", "ExternalPublisher");
                textWriter.WriteAttributeString("Version", "2.4.1");
                        textWriter.WriteStartElement("IssueJobSheet");
                            WritePublisherInfo();
                            WriteJournalInfo();
                            WriteVolumeInfo();
                            WriteIssueInfo();
                            WriteProductionInfo();
                        textWriter.WriteEndElement();
                        textWriter.WriteEndElement();///////////////////IssueJobSheet close
            textWriter.WriteEndDocument();
            textWriter.Flush();
            textWriter.Close();

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(_SpringerXml));
            //XmlStr.Replace("\r", "");
            File.WriteAllText(_SpringerXml,XmlStr.ToString());
        
        }

        private void WritePublisherInfo() 
        { 
               textWriter.WriteStartElement("PublisherInfo");
	                 textWriter.WriteStartElement("PublisherName");
                     textWriter.WriteString("John Libbey eurotext");
                     textWriter.WriteEndElement();
                     textWriter.WriteStartElement("PublisherLocation");
                        textWriter.WriteString("Paris");
                     textWriter.WriteEndElement();
               textWriter.WriteEndElement();
        }

        private void WriteJournalInfo()   
        {
            ProcessJournalInfo(IssueDetails.JID);
           
        }

        private void WriteVolumeInfo()    
        {
            textWriter.WriteStartElement("VolumeInfo");
            textWriter.WriteAttributeString("OutputMedium","Online");
            textWriter.WriteAttributeString("TocLevels" , "0");
            textWriter.WriteAttributeString("VolumeType","Regular");
            
                textWriter.WriteStartElement("VolumeIDStart");
                textWriter.WriteString(IssueDetails.Volume);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VolumeIDEnd");
                textWriter.WriteString(IssueDetails.Volume);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VolumeIssueCount");
                textWriter.WriteString(IssueDetails.ISSUE);
                textWriter.WriteEndElement();

            textWriter.WriteEndElement();
        }

        private void WriteIssueInfo()     
        {
                textWriter.WriteStartElement("IssueInfo");
                    textWriter.WriteAttributeString("IssueType",    "Regular");
                    textWriter.WriteAttributeString("OutputMedium", "Online");
                    textWriter.WriteAttributeString("TocLevels" ,   "0");

                    textWriter.WriteElementString("IssueIDStart",     IssueDetails.ISSUE);
                    textWriter.WriteElementString("IssueIDEnd",       IssueDetails.ISSUE);
                    textWriter.WriteElementString("IssueArticleCount",IssueDetails.IssueArticleCount );

                    textWriter.WriteStartElement("IssueHistory");
                    textWriter.WriteStartElement("CoverDate");
                        textWriter.WriteElementString("Year",  IssueDetails.Year);
                        textWriter.WriteElementString("Month", IssueDetails.Month);
                    textWriter.WriteEndElement();
                    textWriter.WriteEndElement();


                    textWriter.WriteStartElement("IssueCopyright");
                        textWriter.WriteElementString("CopyrightHolderName", "JLE/Springer");
                        textWriter.WriteElementString("CopyrightYear",        IssueDetails.Year);
                    textWriter.WriteEndElement();
               textWriter.WriteEndElement();
        }

        private void WriteProductionInfo()
        {
            textWriter.WriteStartElement("ProductionInfo");
            DiscreteIssueObjectInfo();
            WriteWorkflowInfo();
            textWriter.WriteEndElement();/////////////ProductionInfo close
             
        }

        private void WriteArticleInfo()   
        {
          //<ArticleInfo ArticleType="OriginalPaper" ContainsESM="No" Language="En" NumberingStyle="Unnumbered" TocLevels="0">
            textWriter.WriteStartElement("ArticleInfo");
                    textWriter.WriteAttributeString("ArticleType", "OriginalPaper");
                    textWriter.WriteAttributeString("ContainsESM", "No");
                    textWriter.WriteAttributeString("Language", "En");
                    textWriter.WriteAttributeString("NumberingStyle","Unnumbered");
                    textWriter.WriteAttributeString("TocLevels",  "0");



            textWriter.WriteElementString("ArticleID", ArticleDetailsOBJ.AID);
            textWriter.WriteElementString("ArticleDOI", ArticleDetailsOBJ.DOI);
            textWriter.WriteElementString("ArticleSequenceNumber", ArticleDetails.SequenceNO);
            textWriter.WriteStartElement("ArticleTitle");
                textWriter.WriteAttributeString("Language","En");
                if (ArticleDetailsOBJ.ENArticleTitle.Equals(""))
                    textWriter.WriteString(ArticleDetailsOBJ.FRArticleTitle );
                else
                    textWriter.WriteString(ArticleDetailsOBJ.ENArticleTitle );
            textWriter.WriteEndElement();


            textWriter.WriteElementString("ArticleCategory", ArticleDetailsOBJ.ArticleCategory);
            textWriter.WriteElementString("ArticleFirstPage",ArticleDetailsOBJ.SPage);
            textWriter.WriteElementString("ArticleLastPage", ArticleDetailsOBJ.EPage);

            textWriter.WriteStartElement("ArticleCopyright");
            
            textWriter.WriteElementString("CopyrightHolderName", "JLE/Springer");
            textWriter.WriteElementString("CopyrightYear", IssueDetails.Year);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("ArticleContext");
            textWriter.WriteElementString("JournalID",_JournalID );
            textWriter.WriteElementString("VolumeIDStart", IssueDetails.Volume );
            textWriter.WriteElementString("VolumeIDEnd",   IssueDetails.Volume);
            textWriter.WriteElementString("IssueIDStart",  IssueDetails.ISSUE);
            textWriter.WriteElementString("IssueIDEnd",    IssueDetails.ISSUE);
            textWriter.WriteEndElement();
            
            textWriter.WriteEndElement();
                
        }

        private void WriteWorkflowInfo()  
        {
            textWriter.WriteStartElement("WorkflowInfo");
            textWriter.WriteAttributeString("TaskType", "DeliverCompoundObject");
            textWriter.WriteStartElement("Priority");
            textWriter.WriteAttributeString("Level", "Standard");
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("Supplier");
            textWriter.WriteStartElement("ExternalPublisher");
            WriteSupplierInfo();
            textWriter.WriteStartElement("FilesToPublisher");
            textWriter.WriteStartElement("ContentFiles");
            ///////////////////////////////////////////
            WriteCoverInfo();

            int Counter = 0;
            foreach (string PdfFile in PdfFileInfo)
            {
                Counter++;
                WriteFileInfo(PdfFile,Counter.ToString());
            }
            ///////////////////////////////////////////
            textWriter.WriteEndElement();//////////ContentFiles Close
            textWriter.WriteEndElement();//////////FilesToPublisher Close
            textWriter.WriteEndElement();//////////ExternalPublisher close
            textWriter.WriteEndElement();//////////Supplier close
            textWriter.WriteEndElement();//////////WorkflowInfo close
        }

        private void WriteAuthorGroup()   
        {
            textWriter.WriteStartElement("AuthorGroup");
            ArticleDetailsOBJ.WriteAuthor(textWriter);
            ArticleDetailsOBJ.WriteAffilation(textWriter);
            textWriter.WriteEndElement();
        }

        
        private void WriteCoverInfo()              
        {
            textWriter.WriteStartElement("File");
            textWriter.WriteAttributeString("DiscreteObjectID",  "Cover");
            textWriter.WriteStartElement("Cover");
            textWriter.WriteStartElement("CoverInfo");

            textWriter.WriteElementString("CoverFirstPage", "");
            textWriter.WriteElementString("CoverLastPage", "");
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("CoverFigure");
            textWriter.WriteStartElement("MediaObject");

            textWriter.WriteStartElement("ImageObject");
                    textWriter.WriteAttributeString("Color","Color" );
                    textWriter.WriteAttributeString("FileRef",_JournalID+ "_12_4_Cover/" + _JournalID + "_12_4_CoverFigure_Print.tif" );
                    textWriter.WriteAttributeString("Format", "TIFF"); 
                    textWriter.WriteAttributeString("Rendition", "Print" );
                    textWriter.WriteAttributeString("Type", "Halftone");
                        textWriter.WriteString(" ");
                    textWriter.WriteEndElement();
                    textWriter.WriteEndElement();
            textWriter.WriteEndElement();
            textWriter.WriteEndElement();
            textWriter.WriteEndElement();
        }

        private void WriteFileInfo(string PdfName, string pdfNo) 
        {
            textWriter.WriteStartElement("File");
                textWriter.WriteAttributeString("DiscreteObjectID" ,"Art_" + pdfNo);
                textWriter.WriteStartElement("RenditionItem");
                    textWriter.WriteAttributeString("FileRef", PdfName); 
		            textWriter.WriteAttributeString("TargetType","OnlinePDF");
                textWriter.WriteEndElement();
           textWriter.WriteEndElement();
        }

        private void ProcessJournalInfo(string JID)
        {
            //textWriter.WriteRaw(JournalInfo.Replace("#$#", "&"));
            //textWriter.Formatting = Formatting.Indented;
        }
        private void InitialProcess(string JID)
        {
            string ExeLoc       = ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string MetaData     = ExeLoc + "\\SpringerJournalInfo.xml";
            string TempMetaData = ExeLoc + "\\TempSpringerJournalInfo.xml";

            StringBuilder xmlStr = new StringBuilder(File.ReadAllText(MetaData));

            xmlStr.Replace("&", "#$#");
            xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
            xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");

            File.WriteAllText(TempMetaData, xmlStr.ToString());

            XmlDocument       MyXmlDocument  = new XmlDocument();
            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.ProhibitDtd = false;
            XmlReader Reader = XmlReader.Create(TempMetaData, ReaderSettings);
            try
            {
                MyXmlDocument.PreserveWhitespace = true;
                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");

                XmlNode Node = MyXmlDocument.GetElementsByTagName(JID.ToUpper())[0];

                if (Node != null)
                {
                    //textWriter.WriteRaw(Node.InnerXml.Replace("#$#", "&"));
                    JournalInfo = Node.InnerXml.Replace("#$#", "&");

                    XmlNode JournalIDNode = Node.SelectSingleNode(".//JournalID");
                    if (JournalIDNode != null)
                    {
                        _JournalID = JournalIDNode.InnerText;
                    }
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Error message :" + ex.Message);
            }
            finally
            {
                Reader.Close();
                File.Delete(TempMetaData);
                MyXmlDocument = null;
            }
        }

        private void WriteSupplierInfo()           
        { 
            textWriter.WriteElementString("CompanyName","Jouve");
            textWriter .WriteStartElement("Contact");
	            textWriter.WriteElementString("Street","561, rue du Saint Leonard, BP 3");
	            textWriter.WriteElementString("City",    "Mayenne");
	            textWriter.WriteElementString("Postcode","53101");
	            textWriter.WriteElementString("Country","France");
	            textWriter.WriteElementString("Phone","+33243082571");
	            textWriter.WriteElementString("Fax","+33243082639");
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("ContactPerson");
	            textWriter.WriteStartElement("ContactPersonName");
		            textWriter.WriteElementString("GivenName","J&#xe9;r&#xf4;me");
		            textWriter.WriteElementString("FamilyName", "Cailliaux");
	            textWriter.WriteEndElement();
	        textWriter .WriteStartElement("Contact");
		            textWriter.WriteElementString("Phone","+33243083909");
		            textWriter.WriteElementString("Fax","+33243082630");
		            textWriter.WriteElementString("Email","jcailliaux@jouve.fr");
	            textWriter.WriteEndElement();
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("Deliverables");
	            textWriter.WriteStartElement("DeliverablesForCompoundObjects");
                textWriter.WriteAttributeString("AdvertisementPrintPDF","No"); 
                textWriter.WriteAttributeString("BackmatterPrintPDF","No" );
                textWriter.WriteAttributeString("CoverFigure","Yes" );
                textWriter.WriteAttributeString("CoverPrintPDF","No" );
                textWriter.WriteAttributeString("DiscreteContentObjectOnlineMediaObjects","No" );
                textWriter.WriteAttributeString("DiscreteContentObjectOnlinePDF","Yes" );
                textWriter.WriteAttributeString("DiscreteContentObjectPrintPDF","No" );
                textWriter.WriteAttributeString("DiscreteContentObjectXMLWithBody","No" );
                textWriter.WriteAttributeString("DiscreteContentObjectXMLWithBodyRefsOnly","No" );
                textWriter.WriteAttributeString("FrontmatterPrintPDF","No" );
                textWriter.WriteAttributeString("Pit-Stop-Reports", "No");
                textWriter.WriteEndElement();
            textWriter.WriteEndElement();
        }

        private void DiscreteIssueObjectInfo()     
        {
            textWriter.WriteStartElement("DiscreteIssueObjectInfo");
            textWriter.WriteAttributeString( "ID","Cover");
            textWriter.WriteStartElement("CoverInfo");
                textWriter.WriteElementString("CoverFirstPage","A1");
                textWriter.WriteElementString("CoverLastPage", "A4");
                textWriter.WriteEndElement();
            textWriter.WriteEndElement();


            string[] XmlFiles = Directory.GetFiles(_InPutPath, "*.xml", SearchOption.AllDirectories);
            Array.Sort(XmlFiles);
            
                string FileRef="";
            int Counter=0;
                foreach (string XmlFile in XmlFiles)
                {
                    Console.WriteLine("Process file :" + XmlFile);
                    Counter++;
                    textWriter.WriteStartElement("DiscreteIssueObjectInfo");
                    textWriter.WriteAttributeString("ID", "Art_" + Counter.ToString());

                    ArticleDetailsOBJ = new ArticleDetails(XmlFile);
                    FileRef = _JournalID + IssueDetails.Volume + "0" + IssueDetails.ISSUE + ".1v1/" + _JournalID  + ArticleDetailsOBJ.AID + ".pdf";
                    PdfFileInfo.Add(FileRef);
                    WriteArticleInfo();
                    WriteAuthorGroup();
                    textWriter.WriteEndElement();
                }
                
            
        }


        private StringBuilder ReplacePubmedEntity(string xmlStr)
        {
            StringBuilder XmlStr = new StringBuilder(xmlStr);
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\PubmedEntities.txt";

            if (!File.Exists(FileName))
            {
                Console.WriteLine(FileName + " does not exist");
                Console.WriteLine("Xml entity could not be converted into html entity.");
                Console.WriteLine("Press any key to continue..");
                Console.ReadLine();
                return XmlStr;
            }

            using (StreamReader sr = new StreamReader(FileName))
            {
                int indexNo;
                string line;
                string[] splitStr;
                StringCollection FIndAL = new StringCollection();
                StringCollection ReplaceAL = new StringCollection();
                while ((line = sr.ReadLine()) != null)
                {
                    splitStr = line.Split(' ');
                    FIndAL.Add("&" + splitStr[0] + ";");
                    ReplaceAL.Add(splitStr[1]);
                }
                Regex reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                string str = "";
                while (true)
                {
                    str = Regex.Match(XmlStr.ToString(), @"&[a-zA-Z\.0-9]{1,}\;").Value;

                    if (str.Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        indexNo = FIndAL.IndexOf(str);
                        if (indexNo != -1)
                        {
                            XmlStr.Replace(str, ReplaceAL[indexNo]);
                            //Console.WriteLine(str + " entity replace with html entity " + ReplaceAL[indexNo]);
                        }
                        else
                        {
                            Console.WriteLine(str + " entity is not define");
                            Console.WriteLine("Please define in file.");
                            Console.WriteLine(FileName);
                            break;
                        }
                    }
                }
                MatchCollection mch = reg.Matches(XmlStr.ToString());
                //Console.WriteLine(mch.Count + " entity found to be replaces.");
                reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                mch = reg.Matches(XmlStr.ToString());
                for (int i = 0; i < mch.Count; i++)
                {
                    indexNo = FIndAL.IndexOf(mch[i].Value);
                    if (indexNo != -1)
                    {
                        XmlStr.Replace(mch[i].Value, ReplaceAL[indexNo]);
                        Console.WriteLine(mch[i].Value + " entity replace with html entity " + ReplaceAL[indexNo]);
                    }
                    else
                    {
                        Console.WriteLine(" Html entity could not found for this xml entity " + mch[i].Value);
                    }
                }
            }
            return XmlStr;
        }
        private StringBuilder ReplaceEntity(string xmlStr)
        {
            StringBuilder XmlStr = new StringBuilder(xmlStr);
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\HexEntities.txt";

            if (!File.Exists(FileName))
            {
                Console.WriteLine(FileName + " does not exist");
                Console.WriteLine("Xml entity could not be converted into html entity.");
                Console.WriteLine("Press any key to continue..");
                Console.ReadLine();
                return XmlStr;
            }

            using (StreamReader sr = new StreamReader(FileName))
            {
                int indexNo;
                string line;
                string[] splitStr;
                StringCollection FIndAL = new StringCollection();
                StringCollection ReplaceAL = new StringCollection();
                while ((line = sr.ReadLine()) != null)
                {
                    splitStr = line.Split(' ');
                    FIndAL.Add("&" + splitStr[0] + ";");
                    ReplaceAL.Add(splitStr[1]);
                }
                Regex reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                string str = "";
                while (true)
                {
                    str = Regex.Match(XmlStr.ToString(), @"&[a-zA-Z\.0-9]{1,}\;").Value;

                    if (str.Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        indexNo = FIndAL.IndexOf(str);
                        if (indexNo != -1)
                        {
                            XmlStr.Replace(str, ReplaceAL[indexNo]);
                            //Console.WriteLine(str + " entity replace with html entity " + ReplaceAL[indexNo]);
                        }
                        else
                        {
                            Console.WriteLine(str + " entity is not define");
                            Console.WriteLine("Please define in file.");
                            Console.WriteLine(FileName);
                            break;
                        }
                    }
                }
                MatchCollection mch = reg.Matches(XmlStr.ToString());
                //Console.WriteLine(mch.Count + " entity found to be replaces.");
                reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                mch = reg.Matches(XmlStr.ToString());
                for (int i = 0; i < mch.Count; i++)
                {
                    indexNo = FIndAL.IndexOf(mch[i].Value);
                    if (indexNo != -1)
                    {
                        XmlStr.Replace(mch[i].Value, ReplaceAL[indexNo]);
                        Console.WriteLine(mch[i].Value + " entity replace with html entity " + ReplaceAL[indexNo]);
                    }
                    else
                    {
                        Console.WriteLine(" Html entity could not found for this xml entity " + mch[i].Value);
                    }

                }
            }
            return XmlStr;
        }
    }
}
