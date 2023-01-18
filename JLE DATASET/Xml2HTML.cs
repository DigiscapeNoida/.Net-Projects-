using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Specialized ;
using System.Reflection ;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Text;

namespace JLEDATASET
{

   public  class IndexXmlInfo
    {
        string _ENKeyWords  = "";
        string _Motscles    = "";
        string _FrAbstract  = "";
        string _EnAbstract  = "";

        string _PageDebut   = " ";
        string _PageFin     = " ";
        string _PageOrdre   = "";
        string _Title       = "";
        string _ENTitle     = "";
        string _doi         = "";
        string _Author      = "";
        string _Affilation  = "";
        string _DatePubli   = "";
        string _DateParu    = "";
        string _LibParu     = "";
        string _Gratuit     = "";
        string _LibSomm     = "";
        string _ArticleLanguage = "";
        public string ArticleLanguage
        {
            get { return _ArticleLanguage; }
            set { _ArticleLanguage = value; }
        }

        public IndexXmlInfo()
        { 
        }
        public string Motscles
        {
            get { return _Motscles; }
            set { _Motscles = value; }
        }
        public string ENKeyWords
        {
            get { return _ENKeyWords; }
            set { _ENKeyWords = value; }
        }
        public string ENTitle
        {
            get { return _ENTitle; }
            set { _ENTitle = value; }
        }
        public string EnAbstract
        {
            get { return _EnAbstract; }
            set { _EnAbstract = value; }
        }
        public string FrAbstract
        {
            get { return _FrAbstract; }
            set { _FrAbstract = value; }
        }
        public string PageDebut
        {
            get { return _PageDebut; }
            set { _PageDebut = value; }
        }
        public string PageFin
        {
            get { return _PageFin; }
            set { _PageFin=value; }
        }
        public string PageOrdre
        {
            get { return _PageOrdre; }
            set { _PageOrdre = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string doi
        {
            get { return _doi; }
            set 
            {
                if (_doi.Equals(""))
                {
                    _doi = value;
                }
            }
        }
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public string Affilation
        {
            get { return _Affilation; }
            set { _Affilation = value; }
        }
        public string DatePubli
        {
            get { return _DatePubli; }
            set { _DatePubli = value; }
        }
        public string DateParu
        {
            get { return _DateParu; }
            set { _DateParu = value; }
        }
        public string LibParu
        {
            get { return _LibParu; }
            set { _LibParu = value; }
        }
        public string Gratuit
        {
            get { return _Gratuit; }
            set { _Gratuit = value; }
        }
        public string LibSomm
        {
            get { return _LibSomm; }
            set { _LibSomm = value; }
        }
    }
       
    class Xml2HTML
    {

        BibList BibListOBJ = null;
        string Para = "#td:caption#td:display#td:affiliation#td:author-group#td:correspondence#td:figure#td:note-para#td:footnote#td:para#td:section-title#td:simple-para#td:table#td:title#";
        string _Title           = "";
        string              JID = "";
        string              AID = "";
        string              HTMLFileName = "";
        string             _IndexXMLName = "";
        XmlDocument         MyXmlDocument;
        XmlTextWriter       textWriter;
        IndexXmlInfo        IndexXmlInfoObj = new IndexXmlInfo();
        XmlNamespaceManager nsmgr = null;

        bool BibStart     =false;


        public string jid
        {
            get
            {
                return JID;
            }
            
        }
        public string aid
        {
            get
            {
                return AID;
            }

        }

        public string Title
        {
            get
            {
                return _Title;
            }

        }


        static bool _AOPArticle = false; /////////////////////////Single article (Ahead of print)

        public static bool AOPArticle
        {
            set { _AOPArticle=value; }
            get { return _AOPArticle; }
        }

        public Xml2HTML()
        {
        }
        public Xml2HTML(XmlDocument xmlDocument):this()
        {
            MyXmlDocument = xmlDocument;
        }
        public Xml2HTML(XmlDocument xmlDocument, string HtmlFileName):this()
        {
            MyXmlDocument = xmlDocument;
            HTMLFileName = HtmlFileName;
        }
        public string HtmlFileName     
        {
            get
            {
                return HTMLFileName;
            }
            set 
            {
                HTMLFileName = value;
            }
        }

        public IndexXmlInfo IndexInfo  
        {
            get { return IndexXmlInfoObj; }
        }

        public string IndexXMLName     
        {
            get
            {
                return _IndexXMLName;
            }
            set
            {
                _IndexXMLName = value;
            }
        }
        public XmlDocument xmlDocument 
        {
            get
            {
                return MyXmlDocument;
            }
            set 
            {
                MyXmlDocument = value;
            }
        }
        protected internal void MakeXhtml()
        {

            string[] PageRange = Path.GetFileName(Path.GetDirectoryName(HTMLFileName)).Split('-');

            if (PageRange.Length > 1 && _AOPArticle== false)
            {
                IndexXmlInfoObj.PageDebut = PageRange[0];
                IndexXmlInfoObj.PageFin   = PageRange[1];
            }

            if (HTMLFileName.Equals(""))
            {
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(HTMLFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(HTMLFileName));
            }

            FilterNode();
            try
            {
                
                /////////////////////****************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\    
                textWriter = new XmlTextWriter(HTMLFileName, null);
                /////////////////////****************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error :" + ex.Message);
                Console.WriteLine("Press any key to ext...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            textWriter.Indentation = 1;
            textWriter.IndentChar    = '\t';
            textWriter.WriteStartDocument();

          //textWriter.WriteDocType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", "http://www.w3.org/1998/Math/MathML");

            textWriter.WriteStartElement("html");

          //textWriter.WriteAttributeString("XMLNS:mml","http://www.w3.org/1998/Math/MathML");
            textWriter.WriteStartElement("head");    //////////headStart

          //textWriter.WriteStartElement("SCRIPT");
          //textWriter.WriteAttributeString("LANGUAGE","javascript");
          //textWriter.WriteAttributeString("SRC", "xEdt.js");
          //textWriter.WriteString(" ");
          //textWriter.WriteEndElement();

          //<SCRIPT LANGUAGE="javascript" SRC="MergeAll.js"></SCRIPT>

            textWriter.WriteElementString("title", "index");

            //textWriter.WriteStartElement("OBJECT");
            //textWriter.WriteAttributeString("ID","MathPlayer");
            //textWriter.WriteAttributeString("CLASSID","clsid:32F66A20-7614-11D4-BD11-00104BD3F987");
            //textWriter.WriteString(" ");
            //textWriter.WriteEndElement();
            //textWriter.WriteRaw("<?IMPORT NAMESPACE=\"mml\" IMPLEMENTATION=\"#MathPlayer\" ?>");
            //textWriter.WriteString("XmlEditior");

            textWriter.WriteEndElement();   //////////headClose

            textWriter.WriteStartElement("body");

            

            SearchNode(MyXmlDocument.DocumentElement);

            textWriter.WriteEndElement();//////////bodyClose
            textWriter.WriteEndDocument();
            textWriter.Close();

            StringBuilder HtmlStr = new StringBuilder(File.ReadAllText(HTMLFileName));

            HtmlStr.Replace("#$#lt;i#$#gt;et al.#$#lt;/i#$#gt; ,", "#$#lt;i#$#gt;et al.#$#lt;/i#$#gt;, ");
            HtmlStr.Replace("#$#", "&");
            HtmlStr.Replace("\t", "");
            HtmlStr.Replace("\r", "");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");

            HtmlStr.Replace("  ", " ");
            HtmlStr.Replace("  ", " ");
            HtmlStr.Replace("  ", " ");

            HtmlStr.Replace("&lt;b&gt;", "<b>");
            HtmlStr.Replace("&lt;/b&gt;", "</b>");

            HtmlStr.Replace("&lt;i&gt;", "<i>");
            HtmlStr.Replace("&lt;sub&gt;", "<sub>");
            HtmlStr.Replace("&lt;sup&gt;", "<sup>");

            
            HtmlStr.Replace("&lt;/i&gt;", "</i>");
            HtmlStr.Replace("&lt;/sub&gt;", "</sub>");
            HtmlStr.Replace("&lt;/sup&gt;","</sup>");
            HtmlStr.Replace("<p />", "");
            HtmlStr.Replace("<sup>,</sup> <a", " <a");
            
            HtmlStr.Replace("<?xml version=\"1.0\"?>","");
            
            //HtmlStr.Replace("</head>", "<link href=\"xEdt.css\" rel=\"stylesheet\" type=\"text/css\" />\n</head>");

            File.WriteAllText(HTMLFileName, ReplaceEntity(HtmlStr.ToString()).ToString());
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
        public void FilterNode( )                         
        {
            //IndexXmlInfoObj.doi = Program.
           

            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            nsmgr.AddNamespace("td", "http://www.thomsondigital.com/xml/common/dtd");
            nsmgr.AddNamespace("sb", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsmgr.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            nsmgr.AddNamespace("tb", "http://www.thomsondigital.com/xml/common/table/dtd");
            nsmgr.AddNamespace("tp", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");

            XmlNode Node = MyXmlDocument.GetElementsByTagName("aid")[0];

            if (Node != null)
            {
                AID = Node.InnerText;
            }

            Node = MyXmlDocument.GetElementsByTagName("jid")[0];

            if (Node != null)
            {
                JID = Node.InnerText;
            }


            if (jid.Equals("AGR",StringComparison.OrdinalIgnoreCase))
                SufflingBib();

            
            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string HTMLFilterPath = ExeLoc + "\\HTMLFilter.txt";

            if (File.Exists(HTMLFilterPath))
            {

                File.WriteAllText(HTMLFilterPath, File.ReadAllText(HTMLFilterPath).Replace("&","#$#"));
                string[] FilterLines = File.ReadAllLines(HTMLFilterPath);
                foreach (string FilterLine in FilterLines)
                {
                    string[] SplitStr = FilterLine.Split(new string []{ "|||"},StringSplitOptions.RemoveEmptyEntries);
                    try
                    {

                        if (SplitStr.Length>1)
                          MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace(SplitStr[0], SplitStr[1]);
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine("Error generating during find replace using HTMLFilter.txt");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Press any key to continue..");
                        Console.ReadLine();
                    }
                }
            }

            StringBuilder TempString = new StringBuilder(MyXmlDocument.InnerXml);
            TempString.Replace("<td:para><!--<Ack>--></td:para>", "");
            TempString.Replace("<td:para><!--</Ack>--></td:para>", "");
            TempString.Replace("<!--<SHADE>", "");
            TempString.Replace("</SHADE>-->", "");

            TempString.Replace("<SHADE>", "");
            TempString.Replace("</SHADE>", "");
            
            
            

            TempString.Replace("<!--<Heading>-->", "&lt;b&gt;").Replace("<!--</Heading>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<Ack>-->", "&lt;b&gt;").Replace("<!--</Ack>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-No>-->", "&lt;b&gt;").Replace("<!--</ABS-No>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-Title>-->", "&lt;b&gt;").Replace("<!--</ABS-Title>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-Reference>-->", "&lt;b&gt;").Replace("<!--</ABS-Reference>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<RunningTitle>", "<RunningTitle>");
            TempString.Replace("<!--<RunningTitle&gt;", "<RunningTitle>");
            TempString.Replace("</RunningTitle>-->", "</RunningTitle>");
            TempString.Replace("&lt;/RunningTitle>-->", "</RunningTitle>");

            MyXmlDocument.InnerXml = TempString.ToString();

            //<td:given-name>Fr&eacute;d&eacute;ric</td:given-name><td:surname>Bretagnol</td:surname>
            //F. Bretagnol

            //////////////////////////////Put tp:date after titile
            //<td:given-name>Fr&eacute;d&eacute;ric</td:given-name><td:surname>Bretagnol</td:surname>
            //F. Bretagnol

            //////////////////////////////Put tp:date after titile
            //add by puneet 4/3/2013
            int XCount1 = 0;
            XmlNodeList RunntingTitle = MyXmlDocument.GetElementsByTagName("RunningTitle");
            while (XCount1 < RunntingTitle.Count)
            {
                RunntingTitle[0].ParentNode.RemoveChild(RunntingTitle[0]);
            }

            XmlNodeList TPDate = MyXmlDocument.GetElementsByTagName("tp:date");
            XmlNode TempNode;
            XmlNode SeriesNode;
            XmlNode xNode ;
            int XCount = 0;
             try
                {

            while (XCount<TPDate.Count)
            {
               
                    xNode = TPDate[XCount];
                    TempNode = xNode;

                    //if (XCount == 14)
                    //{ 
                    //}

                    while (TempNode != null)
                    {
                        if (TempNode.Name.Equals("tp:series"))
                        {
                            SeriesNode = TempNode;
                            XmlNode SeriesChild = SeriesNode.LastChild;
                            while (SeriesChild != null)
                            {
                                if (SeriesChild.Name.IndexOf("title") != -1)
                                {
                                    SeriesChild.ParentNode.InsertAfter(xNode, SeriesChild);
                                }
                                SeriesChild = SeriesChild.PreviousSibling;
                                break;
                            }
                        }
                        else if (TempNode.Name.IndexOf("title") != -1)
                        {
                            xNode.ParentNode.InsertAfter(xNode, TempNode);
                            break;
                        }
                        TempNode = TempNode.PreviousSibling;
                    }
                    XCount++;
               
            }
                }
             catch (XmlException ex)
             {
                 Console.WriteLine("Error :" + ex.Message);
             }
//////////////////////////////**********************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            //<td:given-name>C&eacute;line</td:given-name><td:surname>Duval</td:surname>

            XmlNodeList CorrNode = MyXmlDocument.GetElementsByTagName("td:correspondence");

            XmlNodeList AuthorGroup = MyXmlDocument.GetElementsByTagName("td:author-group");

            StringDictionary CorAuth = new StringDictionary();
            if (AuthorGroup.Count > 0)
            {
                XmlNodeList AuthorList = AuthorGroup[0].SelectNodes(".//td:author", nsmgr);

                string GName = "";
                string SName = "";
                string CorrID = "";
                foreach (XmlNode AuNode in AuthorList)
                {
                    GName = "";
                    SName = "";
                    CorrID = "";
                    foreach (XmlNode CNode in AuNode)
                    {
                        if (CNode.Name.Equals("td:given-name"))
                            GName = CNode.InnerText;
                        else if (CNode.Name.Equals("td:surname"))
                            SName = CNode.InnerXml;
                        else if (CNode.Name.Equals("td:cross-ref"))
                        {
                            if (CNode.Attributes.GetNamedItem("refid") != null)
                            {
                                CorrID = CNode.Attributes.GetNamedItem("refid").Value;
                                if (GName != "" && SName != "" && CorrID != "")
                                {
                                    if (GName.StartsWith("#"))
                                    {
                                        int ColonPos = GName.IndexOf(';') +1;
                                        string str = GName.Substring(0, ColonPos) + ". "   + SName;
                                        if (!CorAuth.ContainsKey(CorrID))
                                            CorAuth.Add(CorrID, str);
                                    }
                                    else if (GName.IndexOf(' ') != -1)
                                    {
                                        
                                        string[] arr = GName.Split(' ');
                                        string str = GName.Substring(0, 1).ToUpper() + ". " + arr[1] + " " + SName;
                                        if (!CorAuth.ContainsKey(CorrID))
                                            CorAuth.Add(CorrID, str);
                                    }
                                    else if (GName.EndsWith("."))
                                    {
                                        string str = GName + " " + SName;
                                        if (!CorAuth.ContainsKey(CorrID))
                                            CorAuth.Add(CorrID, str);
                                    }
                                    else
                                    {
                                        string str = GName.Substring(0, 1).ToUpper() + "." + " " + SName;
                                        if (!CorAuth.ContainsKey(CorrID))
                                             CorAuth.Add(CorrID, str);
                                    }
                                }
                            }
                        }
                    }

                   
                }
            }

            if (CorrNode.Count > 0)
            {
                if (CorrNode[0].Attributes.GetNamedItem("id") != null)
                { 
                    string AtrVal= CorrNode[0].Attributes.GetNamedItem("id").Value;
                    if (CorAuth.ContainsKey(AtrVal))
                    {
                        if (CorrNode[0].FirstChild.InnerXml.Trim(' ').IndexOf(" ") == -1)
                            CorrNode[0].FirstChild.InnerXml = "&lt;b&gt;" + CorrNode[0].FirstChild.InnerXml.Trim(':') + ":&lt;/b&gt; " + CorAuth[AtrVal];
                        else if (CorrNode[0].FirstChild.InnerXml.StartsWith("Tir",StringComparison.OrdinalIgnoreCase ) && CorrNode[0].FirstChild.InnerXml.Trim(new char[]{ '.',':','#','$','n','b','s','p',';'}).EndsWith("part",StringComparison.OrdinalIgnoreCase))
                        {
                            CorrNode[0].FirstChild.InnerXml = "&lt;b&gt;" + CorrNode[0].FirstChild.InnerXml.Trim(':') + " :&lt;/b&gt; " + CorAuth[AtrVal];
                        }
                        else
                        {
                            string Str = CorrNode[0].FirstChild.InnerXml.Substring(0, CorrNode[0].FirstChild.InnerXml.IndexOf(' '));
                            if (Str.StartsWith("Correspondence", StringComparison.OrdinalIgnoreCase))
                            {
                                CorrNode[0].FirstChild.InnerXml = CorrNode[0].FirstChild.InnerXml.Replace(Str, "&lt;b&gt;" + Str + "&lt;/b&gt;");
                            }
                        }
                    }

                }
                //XmlNodeList GivenName = MyXmlDocument.GetElementsByTagName("td:given-name");
                //XmlNodeList SurName = MyXmlDocument.GetElementsByTagName("td:surname");
                //CorrNode[0].FirstChild.InnerXml = CorrNode[0].FirstChild.InnerXml + " " + GivenName[0].InnerXml.Substring(0, 1).ToUpper() + "." + " " + SurName[0].InnerXml;
            }

            XmlNodeList ARTFootNode = MyXmlDocument.GetElementsByTagName("td:article-footnote");
            if (ARTFootNode.Count > 0)
            {
                for (int x = ARTFootNode.Count - 1; x >= 0; x--)
                {
                    //HeadNode.InsertAfter(HeadNode.ChildNodes[i], ARTFootNode[x]);
                    ARTFootNode[0].ParentNode.RemoveChild(ARTFootNode[0]);
                }
                //XmlNode HeadNode = MyXmlDocument.GetElementsByTagName("head")[0];
                //for (int i = HeadNode.ChildNodes.Count - 1; i >= 0; i--)
                //{
                //    if (HeadNode.ChildNodes[i].Name.EndsWith("author-group"))
                //    {
                //        for (int x = ARTFootNode.Count - 1; x > 0; x--)
                //        {
                //            HeadNode.InsertAfter(HeadNode.ChildNodes[i], ARTFootNode[x]);
                //            //ARTFootNode[0].ParentNode.RemoveChild(ARTFootNode[0]);
                //        }
                //        break;
                //    }
                //    else if ((HeadNode.ChildNodes[i].Name.EndsWith("title")))
                //    {
                //        while (ARTFootNode.Count != 0)
                //        {
                //            HeadNode.InsertBefore(ARTFootNode[0], HeadNode.ChildNodes[i]);
                //            ARTFootNode[0].ParentNode.RemoveChild(ARTFootNode[0]);
                //        }
                //        break;
                //    }
                //}
            }

             XmlNodeList nodeList = MyXmlDocument.SelectNodes("//td:footnote",nsmgr);
             for(int i=0; i<nodeList.Count;i++ ) 
             {
                 if (nodeList[i].PreviousSibling != null)
                 {
                     if (nodeList[i].PreviousSibling.Name.Equals("td:cross-ref"))
                     {
                         MyXmlDocument.DocumentElement.AppendChild(nodeList[i]);
                     }
                 }
             }



            ProcessHsp();

           
            XmlNode node = MyXmlDocument.SelectSingleNode("//td:floats", nsmgr);

            if (node != null)
            {
                XmlNode tmpNode;
                string ID = "";
                for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
                {
                    if (node.ChildNodes[i].NodeType == XmlNodeType.Element)
                    {
                        if (node.ChildNodes[i].Attributes.GetNamedItem("id") != null)
                        {
                            ID = node.ChildNodes[i].Attributes.GetNamedItem("id").Value;
                            tmpNode = MyXmlDocument.SelectSingleNode("//td:para/td:float-anchor[@refid='" + ID + "']", nsmgr);
                            if (tmpNode != null)
                            {
                                tmpNode.ParentNode.ParentNode.InsertAfter(node.ChildNodes[i], tmpNode.ParentNode);
                                tmpNode.ParentNode.RemoveChild(tmpNode);
                            }
                        }
                    }
                }
            }

            

            nodeList = MyXmlDocument.SelectNodes("//td:author", nsmgr);
            string AuthorName = "";

            for (int i = 0; i < nodeList.Count; i++)
            {
                for (int j = 0; j < nodeList[i].ChildNodes.Count; j++)
                {
                    if ("td:given-name#td:surname".IndexOf(nodeList[i].ChildNodes[j].Name) != -1)
                        AuthorName += nodeList[i].ChildNodes[j].InnerText + " ";
                }
                AuthorName = AuthorName.Trim();
                AuthorName += ", ";
            }
            IndexXmlInfoObj.Author = AuthorName.Trim(new char[]{' ',','});

            nodeList = MyXmlDocument.GetElementsByTagName("td:affiliation");

            string AffilName = "";

            for (int i = 0; i < nodeList.Count; i++)
            {
                for (int j = 0; j < nodeList[i].ChildNodes.Count; j++)
                {
                    if (!nodeList[i].ChildNodes[j].Name.Equals("td:label"))
                    AffilName += nodeList[i].ChildNodes[j].InnerText + " ";
                }
                AffilName = AffilName.Trim();
                AffilName += ", ";
            }
            IndexXmlInfoObj.Affilation = AffilName.Trim(new char[]{' ', ','});

            nodeList = MyXmlDocument.GetElementsByTagName("td:title");
            if (nodeList.Count > 0)
            {
                ProcessTitle(nodeList[0]);
                IndexXmlInfoObj.Title = nodeList[0].InnerXml;

                _Title = nodeList[0].InnerXml;

                ///////////////Remove title no need to convert
                nodeList[0].ParentNode.RemoveChild(nodeList[0]);
            }

            nodeList = MyXmlDocument.GetElementsByTagName("td:alt-title");
            if (nodeList.Count > 0)
            {
                ///IndexXmlInfoObj.Title = nodeList[0].InnerXml;
                ///////////////Remove alt title no need to convert
                nodeList[0].ParentNode.RemoveChild(nodeList[0]);
            }
            nodeList = MyXmlDocument.GetElementsByTagName("td:roles");
            if (nodeList.Count > 0)
            {
                ///IndexXmlInfoObj.Title = nodeList[0].InnerXml;
                ///////////////Remove alt title no need to convert
                nodeList[0].InnerXml = "(" + nodeList[0].InnerXml + ")";
            }
            //<td:roles>
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            nodeList = MyXmlDocument.GetElementsByTagName("td:alt-title");
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            GetDochead(nsmgr);
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            GetKeyWords(nsmgr);
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            GetAbstract(nsmgr);
            /////////////////////////////////********************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            //string KeyWordString = "";
            //nodeList = MyXmlDocument.GetElementsByTagName("td:keyword");
            //for (int j = 0; j < nodeList.Count; j++)
            //{
            //    KeyWordString += nodeList[j].FirstChild.InnerXml + ", ";
            //}
            //KeyWordString = KeyWordString.Trim(new char[] { ' ', ',' });
            //IndexXmlInfoObj.Motscles = KeyWordString;
            

            nodeList = MyXmlDocument.SelectNodes("//td:label", nsmgr);
            for (int i = 0; i < nodeList.Count; i++)
            {
                ///////////////To make footnote label in super script
                if (nodeList[i].ParentNode.Name.IndexOf("note") != -1 || nodeList[i].ParentNode.Name.Equals("td:affiliation"))
                {
                    if (nodeList[i].FirstChild.InnerXml.IndexOf("sup") == -1)
                    {
                        string XmlStr = "&lt;sup&gt;" + nodeList[i].FirstChild.InnerText + "&lt;/sup&gt; ";
                        nodeList[i].RemoveChild(nodeList[i].FirstChild);
                        nodeList[i].InnerXml = XmlStr + nodeList[i].InnerXml;
                    }
                }
                else if (nodeList[i].ParentNode.Name.Equals("td:bib-reference"))
                {
                    if (nodeList[i].InnerXml.Trim().IndexOf(" ") != -1)
                    {
                        nodeList[i].InnerXml = "";
                    }
                }

                if (nodeList[i].NextSibling != null)
                {
                    if (nodeList[i].NextSibling.Name.Equals("td:section-title") || nodeList[i].NextSibling.Name.Equals("td:caption") || nodeList[i].NextSibling.Name.Equals("td:note-para"))
                    {
                        nodeList[i].InnerXml = nodeList[i].InnerXml + " ";
                        nodeList[i].NextSibling.PrependChild(nodeList[i]);
                    }
                }
            }

            ////////////////Don't change sequence
            nodeList = MyXmlDocument.SelectNodes("//td:bib-reference", nsmgr);
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (!nodeList[i].InnerText.EndsWith("."))
                {
                    XmlNodeList NL = nodeList[i].SelectNodes(".//text()", nsmgr);
                    NL[NL.Count - 1].ParentNode.InnerXml = NL[NL.Count - 1].ParentNode.InnerXml + ".";
                }
                nodeList[i].InnerXml = nodeList[i].InnerXml.Replace("><", "> <");
            }

            nodeList = MyXmlDocument.GetElementsByTagName("tp:reference");
            for (int i = 0; i < nodeList.Count; i++)
            {
               ProcessBibl(nodeList[i]);
            }

            nodeList = MyXmlDocument.GetElementsByTagName("td:given-name");
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].ParentNode.NextSibling != null)
                {
                    if (!nodeList[i].ParentNode.NextSibling.Name.Equals("td:author"))
                        nodeList[i].InnerXml = nodeList[i].InnerXml.Replace(".", "");
                }
            }
            ////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            nodeList = MyXmlDocument.GetElementsByTagName("td:para");
            for (int i = 0; i < nodeList.Count; i++)
            {
                ProcessOutsideParaNode(nodeList[i]);
            }



            nodeList = MyXmlDocument.GetElementsByTagName("td:table");
            XmlNode ChNode;
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].InnerXml.IndexOf("<td:label><td:bold>") == -1)
                {
                    string str = nodeList[i].InnerXml;
                    str = str.Replace("<td:label>", "<td:label>&lt;b&gt;");
                    str = str.Replace("</td:label>", "&lt;/b&gt;</td:label>");
                    nodeList[i].InnerXml = str;
                }

                ChNode = nodeList[i];
                for (int j = 0; j < ChNode.ChildNodes.Count; j++)
                {
                    if (ChNode.ChildNodes[j].Name.Equals("tgroup"))
                        break;
                    else
                    {
                        ChNode.ParentNode.InsertBefore(ChNode.ChildNodes[j], ChNode);
                        j--;
                    }
                }
                while (true)
                {
                    if (ChNode.LastChild == null)
                    {
                        break;
                    }
                    if (ChNode.LastChild.Name.Equals("tgroup"))
                        break;
                    else
                        ChNode.ParentNode.InsertAfter(ChNode.LastChild, ChNode);
                }

                //for (int j = ChNode.ChildNodes.Count - 1; j >= 0; j--)
                //{
                //    if (ChNode.ChildNodes[j].Name.Equals("tgroup"))
                //        break;
                //    else
                //    {
                //        ChNode.ParentNode.InsertAfter(ChNode.ChildNodes[j], ChNode);
                //        j--;
                //    }
                //}
            }

            nodeList = MyXmlDocument.GetElementsByTagName("td:author-group");
            if (nodeList.Count > 0)
            {
                nodeList = nodeList[0].ChildNodes;
            }

            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                if (nodeList[i].Name.Equals("td:author")) break;
                nodeList[i].ParentNode.ParentNode.InsertAfter(nodeList[i], nodeList[i].ParentNode);

            }

            /////////*****************Move <tp:et-al> after to <tp:authors>***************************************\\\\\\\\\\\\\\\\\\\\
            nodeList = MyXmlDocument.GetElementsByTagName("tp:et-al");
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                if (nodeList[i].Name.Equals("tp:authors")) break;
                nodeList[i].ParentNode.ParentNode.InsertAfter(nodeList[i], nodeList[i].ParentNode);

            }

             
            nodeList = MyXmlDocument.SelectNodes(".//td:caption/td:simple-para/td:inline-figure", nsmgr);
            if (nodeList.Count > 0)
            {

                //while (nodeList.Count > 0)
                for (int i = 0; i < nodeList.Count; i++) 
                {
                    if (nodeList[i].ParentNode != null && nodeList[i].ParentNode.Name.Equals("td:simple-para"))
                    {
                        if (nodeList[i].ParentNode.ParentNode != null)
                        {
                            if (nodeList[i].ParentNode.LastChild.Equals(nodeList[i]) && nodeList[i].ParentNode.ParentNode.Name.Equals("td:caption"))
                            {
                                nodeList[i].ParentNode.ParentNode.ParentNode.InsertAfter(nodeList[i].FirstChild, nodeList[i].ParentNode.ParentNode);

                                nodeList[i].ParentNode.RemoveChild(nodeList[i]);
                            }
                        }
                    }
                }
            }
         
            /////////********************************************************\\\\\\\\\\\\\\\\\\\\

            nodeList = MyXmlDocument.GetElementsByTagName("td:figure");


            if (nodeList.Count >= 0)
            {
                string ImageFileName = Path.GetDirectoryName(HTMLFileName) + "\\images.htm";
                /////////////////////****************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\    
                textWriter = new XmlTextWriter(ImageFileName, null);
                /////////////////////**************************************
                //textWriter.Indentation = 4;
                //textWriter.Formatting = Formatting.Indented;

                textWriter.WriteStartDocument();
                textWriter.WriteStartElement("html");
                textWriter.WriteStartElement("head");    //////////headStart



                textWriter.WriteElementString("title", "images");

                textWriter.WriteEndElement();   //////////headClose
                textWriter.WriteStartElement("body");

                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (!nodeList[i].ParentNode.Name.Equals("td:display"))
                    {
                        SearchNode(nodeList[i]);
                    }
                }

                textWriter.WriteEndElement();//////////bodyClose
                textWriter.WriteEndDocument();
                textWriter.Close();

                StringBuilder HtmlStr = new StringBuilder(File.ReadAllText(ImageFileName));

                HtmlStr.Replace("#$#", "&");
                HtmlStr.Replace("\t", "");
                HtmlStr.Replace("\r", "");
                HtmlStr.Replace("\n\n", "\n");
                HtmlStr.Replace("\n\n", "\n");

                HtmlStr.Replace("  ", " ");
                HtmlStr.Replace("  ", " ");
                HtmlStr.Replace("  ", " ");
                HtmlStr.Replace("<p>",         "\n<p>");
                HtmlStr.Replace("&lt;b&gt;",   "<b>");
                HtmlStr.Replace("&lt;i&gt;",   "<i>");
                HtmlStr.Replace("&lt;sub&gt;", "<sub>");
                HtmlStr.Replace("&lt;sup&gt;", "<sup>");

                HtmlStr.Replace("&lt;/b&gt;", "</b>");
                HtmlStr.Replace("&lt;/i&gt;", "</i>");
                HtmlStr.Replace("&lt;/sub&gt;", "</sub>");
                HtmlStr.Replace("&lt;/sup&gt;", "</sup>");


                
                HtmlStr.Replace("</body>", "");
                HtmlStr.Replace("</html>", "</body></html>");
              //HtmlStr.Replace("</head>", "<link href=\"xEdt.css\" rel=\"stylesheet\" type=\"text/css\" />\n</head>");

                File.WriteAllText(ImageFileName, ReplaceEntity(HtmlStr.ToString()).ToString());
            }

        }
        public void GetKeyWords(XmlNamespaceManager nsmgr)
        {
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:keywords");
            string FrKeyWords = "";
            string EnKeyWords = "";
            string LNG = "";
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Attributes.GetNamedItem("xml:lang") != null)
                {
                    LNG = nodeList[i].Attributes.GetNamedItem("xml:lang").Value;
                }

                if (LNG.Equals("fr"))
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        FrKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexXmlInfoObj.Motscles = FrKeyWords.Trim(new char[] {',',' '});
                }
                else if (LNG.Equals("en"))
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        EnKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexXmlInfoObj.ENKeyWords  = EnKeyWords.Trim(new char[] { ',', ' ' });
                }
                else if (nodeList[i].FirstChild.InnerXml.IndexOf("Mots cl" , StringComparison.OrdinalIgnoreCase) != -1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        FrKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexXmlInfoObj.Motscles = FrKeyWords.Trim(new char[] { ',', ' ' });
                }
                else if (nodeList[i].FirstChild.InnerXml.IndexOf("Key word" , StringComparison.OrdinalIgnoreCase) != -1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        EnKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexXmlInfoObj.ENKeyWords = EnKeyWords.Trim(new char[] { ',', ' ' });
                }
                else if (nodeList.Count == 1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        FrKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexXmlInfoObj.Motscles = FrKeyWords.Trim(new char[] { ',', ' ' });
                }
            }
            
        }
        private void ProcessBibl(XmlNode node)             
        {
             //</tp:author> 
            node.InnerXml = node.InnerXml.Replace(@" </tp:author> ", "</tp:author>");
            node.InnerXml = node.InnerXml.Replace(@"</tp:author><tp:author>", "</tp:author>,<tp:author>");

            if (node.SelectNodes(".//tp:maintitle", nsmgr).Count > 0)
            {
                XmlNodeList NodeList = node.SelectNodes(".//tp:maintitle", nsmgr);
                foreach (XmlNode chNode in NodeList)
                {
                    if (chNode.ParentNode.Name.Equals("tp:title"))
                    {
                        if (chNode.ParentNode.ParentNode.ParentNode.Name.Equals("tp:issue"))
                            chNode.InnerXml = "&lt;i&gt;" + chNode.InnerXml + "&lt;/i&gt;";
                    }
                }
            }

            if (jid.Equals("AGR"))
            {
            }
            else
            {
                if (node.SelectSingleNode(".//tp:volume-nr", nsmgr) != null)
                {
                    XmlNode VolNode = node.SelectSingleNode(".//tp:volume-nr", nsmgr);
                    VolNode.InnerXml = VolNode.InnerXml + " : ";
                }
            }
            if (node.SelectSingleNode(".//tp:last-page",nsmgr)!= null)
            {
                XmlNode LPNode = node.SelectSingleNode(".//tp:last-page", nsmgr);
                LPNode.InnerXml= "-" +LPNode.InnerXml ;
            }
            if (node.SelectSingleNode(".//tp:date",nsmgr)!= null)
            {
                XmlNode DateNode = node.SelectSingleNode(".//tp:date", nsmgr);

                /////////////////'No need this process due to Sufflingbib method'
                if (JID.Equals("AGR"))
                { 
                     DateNode.InnerXml = " " + DateNode.InnerXml + ". ";
                }
                else if  (DateNode.PreviousSibling.NodeType == XmlNodeType.Text)
                {
                   
                    if (!DateNode.PreviousSibling.PreviousSibling.Name.Equals("tp:authors"))
                        DateNode.InnerXml = DateNode.InnerXml + ";";
                }
                else if (!DateNode.PreviousSibling.Name.Equals("tp:authors"))
                      DateNode.InnerXml = DateNode.InnerXml + ";";
            }
            node.InnerXml = node.InnerXml.Replace("</tp:first-page> ", "</tp:first-page>");

           
        }
        private void GetDochead (XmlNamespaceManager nsmgr)
        {
            string JIDAID = jid + aid;
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:dochead");
            if (  nodeList.Count > 0 )
            {
                XmlNodeList textfn = nodeList[0].SelectNodes(".//td:textfn", nsmgr);

                if (textfn != null && textfn.Count > 0)
                {
                    ProcessTitle(textfn[0]);
                    IndexXmlInfoObj.LibSomm = textfn[0].InnerXml;
                }
                else if (nodeList[0].FirstChild != null && nodeList[0].FirstChild.Equals("td:text"))
                {
                    ProcessTitle(nodeList[0].FirstChild);
                    IndexXmlInfoObj.LibSomm = nodeList[0].FirstChild.InnerXml;
                }
                else
                {
                    IndexXmlInfoObj.LibSomm = nodeList[0].InnerXml;
                }

                ///////////////Remove title no need to convert
                nodeList[0].ParentNode.RemoveChild(nodeList[0]);
            }
            
               
            if (File.Exists("C:\\Doctype.txt"))
            {
                string []DocHead= File.ReadAllLines("C:\\Doctype.txt");
                foreach (string DocType in DocHead)
                { 
                    if (DocType.StartsWith(JIDAID,StringComparison.OrdinalIgnoreCase))
                    {
                        string[] Arr = DocType.Split('\t');
                        IndexXmlInfoObj.LibSomm = Arr[1];
                    }
                }
            }
            if (string.IsNullOrEmpty(IndexXmlInfoObj.LibSomm))
            {
                Console.WriteLine("TOC doctype is not assigned for " + JIDAID);
            }
        }
        private void GetAbstract(XmlNamespaceManager nsmgr)
        {
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:abstract");

            string FrAbstractPara = "";
            string EnAbstractPara = "";
            string LNG = "";
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Attributes.GetNamedItem("xml:lang") != null)
                {
                    LNG = nodeList[i].Attributes.GetNamedItem("xml:lang").Value;
                }

                if (LNG.Equals("fr"))
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);
                    if (ParaList.Count == 1)
                    {
                        ProcessAbstarct(ParaList[0]);
                        FrAbstractPara = ParaList[0].InnerXml;
                    }
                    else
                    {
                        for (int j = 0; j < ParaList.Count; j++)
                        {
                            ProcessAbstarct(ParaList[j]);
                            FrAbstractPara += "<p>" + ParaList[j].InnerXml + "</p>";
                        }
                    }
                    IndexXmlInfoObj.FrAbstract = FrAbstractPara.Trim();
                }
                else if (LNG.Equals("en"))
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);
                    if (ParaList.Count == 1)
                    {
                        ProcessAbstarct(ParaList[0]);
                        EnAbstractPara = ParaList[0].InnerXml;
                    }
                    else
                    {
                        for (int j = 0; j < ParaList.Count; j++)
                        {
                            ProcessAbstarct(ParaList[j]);
                            EnAbstractPara += "<p>" + ParaList[j].InnerXml + "</p>";
                        }
                    }
                    IndexXmlInfoObj.EnAbstract = EnAbstractPara.Trim();
                }
                else if (nodeList[i].FirstChild.InnerXml.IndexOf("R#$#eacute;sum#$#eacute;",StringComparison.OrdinalIgnoreCase) != -1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);

                    if (ParaList.Count == 1)
                    {
                        ProcessAbstarct(ParaList[0]);
                        FrAbstractPara = ParaList[0].InnerXml ;
                    }
                    else
                    {
                        for (int j = 0; j < ParaList.Count; j++)
                        {
                            ProcessAbstarct(ParaList[j]);
                            FrAbstractPara += "<p>" + ParaList[j].InnerXml + "</p>";
                        }
                    }
                    IndexXmlInfoObj.FrAbstract = FrAbstractPara.Trim();
                }
                else if (nodeList[i].FirstChild.InnerXml.IndexOf("Abstract", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);

                    if (ParaList.Count == 1)
                    {
                        ProcessAbstarct(ParaList[0]);
                        EnAbstractPara = ParaList[0].InnerXml;
                    }
                    else
                    {
                        for (int j = 0; j < ParaList.Count; j++)
                        {
                            ProcessAbstarct(ParaList[j]);
                            EnAbstractPara += "<p>" + ParaList[j].InnerXml + "</p>";
                        }
                    }
                    IndexXmlInfoObj.EnAbstract = EnAbstractPara.Trim();
                }
                else if (nodeList.Count == 1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:simple-para", nsmgr);
                    if (ParaList.Count == 1)
                    {
                        ProcessAbstarct(ParaList[0]);
                        FrAbstractPara = ParaList[0].InnerXml;
                    }
                    else
                    {
                        for (int j = 0; j < ParaList.Count; j++)
                        {
                            ProcessAbstarct(ParaList[j]);
                            FrAbstractPara += "<p>" + ParaList[j].InnerXml + "</p>";
                        }
                    }
                    IndexXmlInfoObj.FrAbstract = FrAbstractPara.Trim();
                }
            }

            IndexXmlInfoObj.FrAbstract = IndexXmlInfoObj.FrAbstract.Replace("<p></p>", "");
            IndexXmlInfoObj.EnAbstract = IndexXmlInfoObj.EnAbstract.Replace("<p></p>", "");
        }
        private void SearchNode (XmlNode node)             
        {
            //if (node.HasChildNodes)
            //{
                bool endElement = false;
                ////////////////***********Start Process Node Name and attribute**************////////////////////////////////////////////////
                ProcessNode(node, out endElement);
                ////////////////***********End Process Node Name and attribute**************////////////////////////////////////////////////
                XmlNodeList nodeList;
                nodeList = node.ChildNodes;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (nodeList[i].NodeType == XmlNodeType.Element)
                        ////////////////***********Start Process Node Name and attribute**************////////////////////////////////////////////////
                        SearchNode(nodeList[i]);
                    ////////////////***********End Process Node Name and attribute**************////////////////////////////////////////////////
                    else if (nodeList[i].NodeType == XmlNodeType.Whitespace)
                        textWriter.WriteString(nodeList[i].InnerText);
                    else if (nodeList[i].NodeType == XmlNodeType.Comment) 
                    { 
                    }
                    else if (nodeList[i].NodeType == XmlNodeType.Text)

                        textWriter.WriteString(nodeList[i].InnerText);
                    else
                        textWriter.WriteString(nodeList[i].InnerText);
                }
                if (endElement) textWriter.WriteEndElement();
                textWriter.Flush();
           // }
        }
        private void ProcessNode(XmlNode node, out bool endElement)
        {
            if (node.Name.Equals("person-group"))
            { 
            }
            endElement = true;
            if (node.Name.Equals("td:author"))
            { 

            }
            if (node.Name.Equals("td:author"))
                endElement = true;

            if (node.NodeType == XmlNodeType.Element)
            {
                if (node.Name.StartsWith("mml"))
                {
                    Default(node);
                }
                else
                {
                    switch (node.Name)
                    {
                        case "td:br":
                            {
                                textWriter.WriteStartElement("br");
                                break;
                            }
                        case "td:small-caps":
                            {
                                node.InnerText = node.InnerText.ToUpper();
                                endElement = false;
                                break;
                            }
                        case "td:textref":
                            {
                              endElement = false;
                              break;
                            }
                        case "source":
                            {
                               endElement = false;
                                break;
                            }
                        case "article-title":
                            {
                                if (node.InnerXml.EndsWith("?") )
                                    node.InnerXml = node.InnerXml + "  ";
                               else
                                    node.InnerXml = node.InnerXml.TrimEnd(new char[] { '.' }) + ". ";


                                endElement = false;
                                break;
                            }
                        case "td:given-name":
                            {
                                endElement = false;
                                if (node.ParentNode.NextSibling == null)
                                    if (JID.Equals("AGR"))
                                        node.InnerXml = node.InnerXml + ", ";
                                    else 
                                        node.InnerXml = node.InnerXml + " ";
                                else
                                   node.InnerXml = node.InnerXml + ", ";
                                break;
                            }
                        case "td:surname":
                            {
                                endElement = false;
                                node.InnerXml = node.InnerXml + " ";
                                break;
                            }
                        case "td:other-ref":
                            {
                                endElement = false;
                                break;
                            }
                        case "name":
                            {
                                endElement = false;
                                break;
                            }
                        case "person-group":
                            {
                                if (node.Attributes.GetNamedItem("person-group-type")!= null && node.Attributes.GetNamedItem("person-group-type").Value.Equals("editor"))
                                {
                                }
                                endElement = false;
                                break;
                            }
                        case "tp:collaboration":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:comment":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:date":
                            {
                                //"nedapp"
                                endElement = false;
                                break;
                            }
                        case "tp:e-host":
                            {
                                //if node.FirstChild.NodeType == XmlNodeType.Text;
                                endElement = false;
                                break;
                            }
                        case "tp:edition":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:editor":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:editors":
                            {
                                endElement = false;
                                break;
                            }
                        case "td:inter-ref":
                            {
                                endElement = false;
                                break;
                            }
                        
                        case "tp:et-al":
                            {

                                if (node.ParentNode.Name.Equals("person-group") && jid.Equals("AGR"))
                                    textWriter.WriteString(" #$#lt;i#$#gt;et al.#$#lt;/i#$#gt;, ");
                                else
                                    textWriter.WriteString(" #$#lt;i#$#gt;et al.#$#lt;/i#$#gt; ");

                                endElement = false;
                                break;
                            }
                        case "tp:first-page":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:issue-nr":
                            {
                                if (JID.Equals("AGR"))
                                {
                                    node.InnerText = "(" + node.InnerText + "):";
                                }
                                else
                                {
                                    node.InnerText = node.InnerText + " ";
                                }

                                endElement = false;
                                break;
                            }
                        case "tp:last-page":
                            {
                                endElement = false;
                                break;
                            }
                        case "tp:location":
                            {
                                node.InnerXml = node.InnerXml.TrimEnd(new char[] { '.', ':',' ' }) + " : ";
                                endElement = false;
                                break;
                            }
                        case "tp:name":
                            {
                                node.InnerXml = node.InnerXml.TrimEnd(new char[] { ',',  ' ' }) + ", ";
                                endElement = false;
                                break;
                            }
                        case "tp:publisher":
                            {
                                endElement = false;
                                node.InnerXml = node.InnerXml + " ";
                                break;
                            }
                        case "tp:volume-nr":
                            {
                                endElement = false;
                                node.InnerXml = node.InnerXml + " ";
                                break;
                            }
                        case "td:bib-reference":
                            {
                                BibStart = true;
                                textWriter.WriteStartElement("p");
                              
                                BibListOBJ = new BibList(node);
                                BibListOBJ.JID = JID;
                                if (BibListOBJ.JID.Equals("AGR"))
                                {
                                    BibListOBJ.DateAfterAuthor = true;
                                    BibListOBJ.DateAfterSource = false;
                                }
                                BibListOBJ.StartProcess();
                                if (BibListOBJ.BibChilds[0].Name.Equals("td:label"))
                                {
                                    ///////////////////Only for numbered references
                                    if (Program.IsNumeric(BibListOBJ.BibChilds[0].InnerText.Trim(new char[] { '.', ',', '\r' })))
                                    {
                                        SearchNode(BibListOBJ.BibChilds[0]);
                                    }
                                    else if (BibListOBJ.BibChilds[0].InnerText.EndsWith("#$#bull;"))
                                    {
                                        SearchNode(BibListOBJ.BibChilds[0]);
                                    }
                                    //Convert other ref default way.....
                                    for (int i = 1; i < BibListOBJ.BibChilds.Count; i++)
                                    {
                                        if (BibListOBJ.BibChilds[i].NodeType == XmlNodeType.Element)
                                        {
                                            if (BibListOBJ.BibChilds[i].Name.Equals("source"))
                                            {
                                                if ((i <BibListOBJ.BibChilds.Count-1)&& (BibListOBJ.BibChilds[i + 1].Name.Equals("tp:date")  || jid.Equals("AGR")))
                                                    BibListOBJ.BibChilds[i].InnerXml = BibListOBJ.BibChilds[i].InnerXml.TrimEnd(new char[] { '.' }) + " ";
                                                else
                                                    BibListOBJ.BibChilds[i].InnerXml = BibListOBJ.BibChilds[i].InnerXml.TrimEnd(new char[] { '.' }) + ". ";

                                                SearchNode(BibListOBJ.BibChilds[i]);
                                            }
                                            else if (BibListOBJ.BibChilds[i].Name.Equals("tp:date"))
                                            {
                                                if (jid.Equals("AGR"))
                                                {
                                                }
                                                else
                                                {
                                                    if (BibListOBJ.BibChilds.Count - 1 == i)
                                                        BibListOBJ.BibChilds[i].InnerXml = BibListOBJ.BibChilds[i].InnerXml.TrimEnd(new char[] { '.', ';' }) + ".";
                                                    else
                                                        BibListOBJ.BibChilds[i].InnerXml = BibListOBJ.BibChilds[i].InnerXml.TrimEnd(new char[] { '.', ';' }) + " ; ";
                                                }
                                                SearchNode(BibListOBJ.BibChilds[i]);
                                            }
                                            else
                                            {
                                                SearchNode(BibListOBJ.BibChilds[i]);
                                            }
                                        }
                                    }
                                }
                                node.RemoveAll();
                                BibStart = false;
                                break;
                            }
                        case "td:underline":
                            {
                                textWriter.WriteStartElement("u");
                                break;
                            }
                        case "td:subtitle":
                            {
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        case "td:alt-title":
                            {
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        case "td:e-address":
                            {
                                textWriter.WriteStartElement("a");
                                if (node.Attributes.GetNamedItem("type")!= null)
                                {
                                    string atrVal = node.Attributes.GetNamedItem("type").Value;
                                    if (atrVal.Equals("email"))
                                        textWriter.WriteAttributeString("href", "mailto:"+ node.InnerText  );
                                }
                                //<a href="mailto:jacques.balosso@centre-etoile.org" >
                                break;
                            }
                        case "jid":
                            {
                                JID = node.InnerText;
                                Program.JID = JID;
                                endElement = false;
                                node.RemoveAll();
                                break;
                            }
                        case "aid":
                            {
                                AID = node.InnerText;
                                Program.AID = AID;
                                endElement = false;
                                node.RemoveAll();
                                break;
                            }
                        case "td:doi":
                            {
                                ///////////Set Doi
                                IndexXmlInfoObj.doi = node.InnerText;

                                if (BibStart == true)
                                {
                                    textWriter.WriteString(node.InnerText);
                                }
                                endElement = false;
                                node.RemoveAll();
                                break;
                            }
                        case "td:inf":
                            textWriter.WriteStartElement("sub");
                            break;
                        case "td:sup":
                            textWriter.WriteStartElement("sup");
                            break;
                        case "td:bold":
                            textWriter.WriteStartElement("b");
                            break;
                        case "td:italic":
                            textWriter.WriteStartElement("i");
                            break;
                        case "td:table":
                            {

                              

                                bool astrat = false;
                                if (node.Attributes.GetNamedItem("id") != null)
                                {
                                    string AtrVal = node.Attributes.GetNamedItem("id").Value;
                                    textWriter.WriteStartElement("a");
                                    textWriter.WriteAttributeString("name", AtrVal);
                                    astrat = true;
                                    //<a name="tbl1">
                                }
                                textWriter.WriteStartElement("table");
                                textWriter.WriteAttributeString("frame", "border");

                                textWriter.WriteAttributeString("frame", "border");
                                textWriter.WriteAttributeString("style", "border:1px solid #c3c3c3;border-collapse:collapse;");
                                textWriter.WriteAttributeString("cellspacing","0");
                                textWriter.WriteAttributeString("cellpadding","0" );
                                textWriter.WriteAttributeString("border","1" );
                                textWriter.WriteAttributeString("width", "100%");

                                foreach (XmlNode chNode in node.ChildNodes)
                                {
                                    SearchNode(chNode);
                                }
                                //textWriter.WriteAttributeString("rules", "rows");
                                //textWriter.WriteAttributeString( "border","1");
                                textWriter.WriteEndElement();

                                if (astrat)
                                {
                                    textWriter.WriteEndElement();
                                }
                                node.RemoveAll();
                                endElement = false; 
                                break;
                            }

                        case "mml:math":
                            {
                                string atrVal = "";
                                if (node.Attributes.GetNamedItem("altimg") != null)
                                {
                                    atrVal = node.Attributes.GetNamedItem("altimg").Value;
                                }
                                textWriter.WriteStartElement("img");

                                string GifName = "jle" + JID.ToLower() + AID.ToLower() + atrVal + ".gif";

                                textWriter.WriteAttributeString("src", GifName);
                                node.RemoveAll();
                                break;
                            }
                        case "tp:contribution":
                            {
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        case "td:link":
                            {
                                string atrVal = "";
                                if (node.Attributes.GetNamedItem("locator") != null)
                                {
                                    atrVal = node.Attributes.GetNamedItem("locator").Value;
                                }

                                if (atrVal.Equals("Picto_DVD", StringComparison.OrdinalIgnoreCase))
                                {
                                    endElement = false;
                                    break;
                                }

                                if (!node.ParentNode.Name.Equals("td:inline-figure"))
                                {
                                    textWriter.WriteStartElement("p");
                                    textWriter.WriteStartElement("center");
                                }
                                textWriter.WriteStartElement("img");

                                if (node.ParentNode.Attributes.GetNamedItem("id") != null)
                                {
                                    textWriter.WriteAttributeString("id", node.ParentNode.Attributes.GetNamedItem("id").Value);
                                }
                                 textWriter.WriteAttributeString("src", "jle" + JID.ToLower() + AID.ToLower() + atrVal + ".jpg");
                               //textWriter.WriteAttributeString("src", "jle" + JID.ToLower() + AID.ToLower() + "-" + atrVal +".jpg");

                                 if (!node.ParentNode.Name.Equals("td:inline-figure"))
                                 {
                                     textWriter.WriteEndElement();
                                     textWriter.WriteEndElement();
                                 }
                                 textWriter.WriteEndElement();
                                 endElement = false;
                                //<P><CENTER><IMG src="jlebdc01468-gr2.jpg"></CENTER>
                                 break;
                            }
                        case "td:figure":
                            {
                                if (node.SelectSingleNode("td:link",nsmgr) != null)
                                {
                                   XmlNodeList tdlinkList= node.SelectNodes( ".//td:link", nsmgr);
                                   //while (tdlinkList.Count > 0)
                                   for (int i = 0; i < tdlinkList.Count;i++ )
                                   {
                                       XmlNode tmpNode = tdlinkList[i];
                                       SearchNode(tmpNode);
                                       tmpNode.ParentNode.RemoveChild(tmpNode);
                                   }
                               }
                                if (node.InnerXml.IndexOf("<td:label><td:bold>") == -1)
                                {
                                    string str = node.InnerXml;
                                    str = str.Replace("<td:label>", "<td:label>&lt;b&gt;");
                                    str = str.Replace("</td:label>", "&lt;/b&gt;</td:label>");
                                    node.InnerXml = str;
                                }
                                textWriter.WriteStartElement("p");
                                //textWriter.WriteAttributeString("style", "TEXT-ALIGN: justify; TEXT-INDENT: 0.5em;");
                                //textWriter.WriteAttributeString("class", node.Name.Replace(":","-"));
                                TraverseChild(node);
                                node.RemoveAll();

                                break;
                            }
                        case "thead":
                            {
                                textWriter.WriteStartElement("thead");
                                break;
                            }
                        case "row":
                            {

                                textWriter.WriteStartElement("tr");
                                break;
                            }
                        case "entry":
                            {
                                if (node.ParentNode.ParentNode.Name.Equals("thead"))
                                    textWriter.WriteStartElement("th");
                                else
                                    textWriter.WriteStartElement("td");

                                if (node.Attributes.GetNamedItem("align") != null)
                                {
                                    textWriter.WriteAttributeString("align", node.Attributes.GetNamedItem("align").Value);
                                    if (node.Attributes.GetNamedItem("char") != null)
                                        textWriter.WriteAttributeString("char", node.Attributes.GetNamedItem("char").Value);
                                
                                }
                                if (node.Attributes.GetNamedItem("morerows") != null)
                                {
                                    int  SpanNo =  Int32.Parse(node.Attributes.GetNamedItem("morerows").Value);
                                    SpanNo++;
                                    textWriter.WriteAttributeString("rowspan" , SpanNo.ToString());
                                }
                                if (node.Attributes.GetNamedItem("namest") != null)
                                {
                                    int sCol = Int32.Parse(node.Attributes.GetNamedItem("namest").Value.Replace("col",""));
                                    int eCol = Int32.Parse(node.Attributes.GetNamedItem("nameend").Value.Replace("col", ""));
                                    int span = eCol - sCol;
                                    span++;
                                    textWriter.WriteAttributeString("colspan", span.ToString());
                                }
                                break;
                            }
                        case "tgroup":
                            {
                                endElement = false;
                                break;
                            }
                        case "tbody":
                            {
                                textWriter.WriteStartElement("tbody");
                                break;
                            }
                        case "td:section-title":
                            {
                                if (node.InnerText.IndexOf("Produits phytopharmaceutiques") != -1)
                                { 
                                }
                                string SecNo="";
                                if (node.ParentNode.Name.Equals("td:section"))
                                {                                                                                                                                                       
                                    if ((node.InnerText.StartsWith("Confli", StringComparison.OrdinalIgnoreCase) && node.ParentNode.PreviousSibling.Name.Equals("td:acknowledgment")) || "Conflit d#$#rsquo;int#$#eacute;r#$#ecirc;ts#Conflits d#$#rsquo;int#$#eacute;r#$#ecirc;ts#Conflict of interest#Conflicts of interest".IndexOf(node.InnerText, StringComparison.OrdinalIgnoreCase) != -1)
                                    {
                                        if (node.NextSibling.InnerText.StartsWith("aucun", StringComparison.OrdinalIgnoreCase) || node.NextSibling.InnerText.StartsWith("none", StringComparison.OrdinalIgnoreCase))
                                        {
                                            string Str = "<p><b>" + node.InnerText + ":</b> " + node.NextSibling.InnerText + "</p>";
                                            textWriter.WriteRaw(Str);
                                            node.NextSibling.RemoveAll();
                                            node.RemoveAll();
                                            endElement = false;
                                            break;
                                        }
                                    }

                                    SecNo=GetSectionNo(node.ParentNode).ToString();
                                    textWriter.WriteStartElement("h" + SecNo);
                                    
                                }

                                if (node.ParentNode.Name.Equals("td:acknowledgment"))
                                        textWriter.WriteStartElement("h1");
                                else if (node.ParentNode.Name.Equals("td:bibliography"))
                                    textWriter.WriteStartElement("h1");
                                else if (node.ParentNode.Name.Equals("td:bibliography-sec"))
                                    textWriter.WriteStartElement("h2");
                                break;
                            }
                        case "td:author-group":
                            {
                                textWriter.WriteStartElement("P");
                                textWriter.WriteRaw("<b>Auteur(s) :</b> ");

                                //if (node.SelectNodes(".//td:author", nsmgr).Count > 1)
                                //{ 
                                //}

                                node.InnerXml = node.InnerXml.Replace("</td:author><td:author", "</td:author>, <td:author").Replace("</td:cross-ref><td:cross-ref", "</td:cross-ref>&lt;sup&gt;,&lt;/sup&gt;<td:cross-ref");
                                TraverseChild(node);
                                node.RemoveAll();
                                break;
                            }
                        case "td:keywords":
                            {
                                /////////////No need to convert td:abstract
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        case "td:abstract":
                            {
                                /////////////No need to convert td:abstract
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        case "td:cross-refs":
                            {
                                if (node.Attributes.GetNamedItem("refid") != null)
                                {
                                    string atrVal = node.Attributes.GetNamedItem("refid").Value;

                                    if (atrVal.IndexOf(" ")!=-1)
                                    {
                                        atrVal = atrVal.Substring(0, atrVal.IndexOf(" "));
                                    }
                                    if (atrVal.StartsWith("fig"))
                                    {
                                        textWriter.WriteStartElement("a");
                                        textWriter.WriteAttributeString("href", "images.htm#" + atrVal);
                                    }
                                    else if (atrVal.StartsWith("tbl") && atrVal.IndexOf("fn") == -1)
                                    {
                                        textWriter.WriteStartElement("a");
                                        textWriter.WriteAttributeString("href", "index.htm#" + atrVal);
                                    }
                                    else
                                    {
                                        if (node.InnerXml.IndexOf("[2, 3]") != -1)
                                        {
                                        }
                                        endElement = false;
                                    }
                                    //<a href="images.htm">"
                                    //<a href="index.htm#tbl1">
                                }
                                else
                                {
                                    endElement = false;
                                }
                                break;
                            }
                        case "td:cross-ref":
                            {
                                if (node.Attributes.GetNamedItem("refid") != null)
                                {
                                    string atrVal = node.Attributes.GetNamedItem("refid").Value;
                                    if (atrVal.StartsWith("fig"))
                                    {
                                        textWriter.WriteStartElement("a");
                                        textWriter.WriteAttributeString("href", "images.htm#" + atrVal);
                                    }
                                    else if (atrVal.StartsWith("tbl") && atrVal.IndexOf("fn") == -1)
                                    {
                                        textWriter.WriteStartElement("a");
                                        textWriter.WriteAttributeString("href", "index.htm#" + atrVal);
                                    }
                                    else if (atrVal.StartsWith("fn") || atrVal.StartsWith("aff"))
                                    {
                                        textWriter.WriteStartElement("a");
                                        textWriter.WriteAttributeString("href", "#" + atrVal);
                                    }
                                    else
                                    {
                                        endElement = false;
                                    }
                                    //<a href="images.htm">"
                                    //<a href="index.htm#tbl1">
                                }
                                else
                                {
                                    endElement= false;
                                }
                                break;
                            }
                        case "td:list":
                            {
                                textWriter.WriteStartElement("ul");
                                break;
                            }
                        case "td:list-item":
                            {
                                textWriter.WriteStartElement("li");
                                break;
                            }
                        case "td:label":
                            {
                               endElement = false;
                               node.InnerXml = "&lt;b&gt;" + node.InnerXml.Trim(".".ToCharArray()) + ".&lt;/b&gt; ";
                                //textWriter.WriteRaw(" ");
                                break;
                            }
                        //case "td:author":
                        //    {
                        //        endElement = false;
                        //        break;
                        //    }
                        default:
                            {
                                if (BibStart == true)
                                {
                                    endElement = false;
                                }
                                else if (node.Name.Equals("td:para") && node.ParentNode.Name.Equals("td:list-item"))
                                {
                                    if (node.PreviousSibling != null)
                                    {
                                        if (node.PreviousSibling.Name.Equals("td:para"))
                                        {
                                            textWriter.WriteStartElement("p");
                                            TraverseChild(node);
                                            node.RemoveAll();
                                        }
                                        else
                                        {
                                            endElement = false;
                                        }
                                    }
                                    else
                                        endElement = false;
                                }
                                else if (Para.IndexOf("#" + node.Name + "#") != -1)
                                {
                                    textWriter.WriteStartElement("p");

                                    if (node.Name.Equals("td:footnote"))
                                    {
                                        if (node.Attributes.GetNamedItem("id") != null)
                                        {
                                            string AtrVal = node.Attributes.GetNamedItem("id").Value;
                                            textWriter.WriteAttributeString("id", AtrVal);
                                        }
                                    }
                                    //textWriter.WriteAttributeString("style", "TEXT-ALIGN: justify; TEXT-INDENT: 0.5em;");
                                    //textWriter.WriteAttributeString("class", node.Name.Replace(":","-"));
                                    TraverseChild(node);
                                    node.RemoveAll();
                                }
                                else
                                {
                                    if (node.FirstChild != null)
                                    {
                                        if (node.FirstChild.NodeType == XmlNodeType.Text)
                                        {
                                            textWriter.WriteStartElement("p");
                                            //textWriter.WriteAttributeString("class", node.Name.Replace(":","-"));
                                        }
                                        else if (node.FirstChild.NodeType == XmlNodeType.Element)
                                        {

                                            try
                                            {
                                                if (node.ParentNode == null)
                                                {
                                                    endElement = false;
                                                }
                                                else if (node.ParentNode.Name.Equals("entry"))
                                                {
                                                    TraverseChild(node);
                                                    node.RemoveAll();
                                                    endElement = false;
                                                }
                                                else
                                                {
                                                    endElement = false;
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        else
                                        {
                                            endElement = false;
                                        }
                                    }
                                    else
                                    {
                                        endElement = false;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
        }
        private void TraverseChild(XmlNode node) 
        {
            foreach (XmlNode chNode in node.ChildNodes)
            {
                if (chNode.Name.StartsWith("mml"))
                {
                    SearchNode(chNode);
                }
                else if (chNode.Name.Equals("td:bold"))
                {
                    SearchNode(chNode);
                }
                else if (chNode.Name.StartsWith("td:cross-out"))
                {
                    textWriter.WriteRaw("strike");
                }
                else if (chNode.Name.StartsWith("td:link"))
                {
                    if (chNode.ParentNode.Name.Equals("td:inline-figure"))
                    {
                        SearchNode(chNode);
                    }
                }
                else if (chNode.Name.StartsWith("td:figure"))
                {
                    SearchNode(chNode);
                }
                else if (chNode.Name.StartsWith("td:table"))
                {
                    SearchNode(chNode);
                }
                else if (chNode.Name.StartsWith("td:e-address"))
                {
                    textWriter.WriteRaw(" ");
                    SearchNode(chNode);
                }
                else if (chNode.Name.StartsWith("td:list"))
                {
                    SearchNode(chNode);
                    //textWriter.WriteRaw(" ");
                }
                else if (chNode.Name.StartsWith("td:cross-ref"))
                {
                    SearchNode(chNode);
                }
                else if (chNode.Name.StartsWith("tp:et-al"))
                {
                    textWriter.WriteString(" #$#lt;i#$#gt;et al.#$#lt;/i#$#gt; ");
                }
                else if (chNode.NodeType == XmlNodeType.Comment)
                {
                    if (chNode.OuterXml.StartsWith("<!--<") && chNode.OuterXml.EndsWith(">-->"))
                    {
                    }
                    else
                    {
                        textWriter.WriteRaw(chNode.Value);
                    }

                }
                else if (chNode.NodeType == XmlNodeType.Whitespace)
                {
                    textWriter.WriteRaw(" ");
                }
                else
                {
                    if (chNode.NodeType == XmlNodeType.Element)
                    {
                        if (chNode.PreviousSibling != null)
                        {
                            if (chNode.PreviousSibling.NodeType != XmlNodeType.Whitespace)
                            {
                                if (!chNode.Name.StartsWith("tp"))
                                    textWriter.WriteRaw(" ");
                            }
                        }
                        if (chNode.FirstChild != null)
                        {
                            if (chNode.FirstChild.NodeType == XmlNodeType.Text)
                            {
                                //textWriter.WriteStartElement("span");
                                //WriteXedtAttribute(chNode);
                                //textWriter.WriteAttributeString("class", chNode.ParentNode.Name.Replace(":","-"));
                                //textWriter.WriteAttributeString("title", chNode.ParentNode.Name);

                                if (chNode.ParentNode.Name.Equals("td:label"))
                                {
                                    textWriter.WriteRaw(" ");
                                }
                                textWriter.WriteRaw(chNode.FirstChild.InnerText);
                                //textWriter.WriteEndElement();




                                chNode.RemoveChild(chNode.FirstChild);
                            }
                            TraverseChild(chNode);
                        }
                        else
                        {
                            SearchNode(chNode);
                            Console.WriteLine(chNode.OuterXml);

                            //Console.WriteLine(chNode.Name + " element not define");
                            //Console.ReadLine();
                            //Environment.Exit(0);
                        }
                    }
                    else
                    {
                        textWriter.WriteRaw(chNode.InnerText);
                    }
                }
            }
        }
        private int  GetSectionNo (XmlNode node) 
        {
            int SecLvl = 1;

            while (node.ParentNode != null)
            {
                if (node.ParentNode.Name.Equals("td:section"))
                {
                    SecLvl++;
                    node = node.ParentNode;
                }
                else
                    break;
            }
            return SecLvl;
        }
        void Default(XmlNode node)               
        {
            textWriter.WriteStartElement(node.Name);
            if (node.Attributes.Count > 0)
            {
                for (int X = 0; X < node.Attributes.Count; X++)
                {
                    if (!node.Attributes[X].Name.StartsWith("xmlns"))
                        textWriter.WriteAttributeString(node.Attributes[X].Name, node.Attributes[X].Value);
                }
            }
        }
        void ProcessOutsideParaNode(XmlNode node)
        {
            
            /////////////Do'nt change sequence
            XmlNodeList NL;
            NL = node.ChildNodes;
            for (int i = NL.Count - 1; i >= 0; i--)
            {
                if (NL[i].Name.Equals("td:displayed-quote"))
                {
                    node.ParentNode.InsertAfter(NL[i], node);
                }
                else if (NL[i].Name.Equals("td:display"))
                {
                        node.ParentNode.InsertAfter(NL[i], node);
                }
                else if (NL[i].NodeType == XmlNodeType.Text)
                {
                    break;
                }
            }
            return;
        }
        public void MakeIndexXml()
        {
            string[] MonthName    = " #Janvier#Février#Mars#Avril#Mai#Juin#Juillet#Août#Septembre#Octobre#Novembre#Décembre".Split('#');
            string[] EngMonthName = " #January#February#March#April#May#June#July#August#September#October#November#December".Split('#');
            //<?xml version="1.0" encoding="utf-8"?>
            //<!DOCTYPE Document SYSTEM "jle.dtd">
            //<Document Code="code" Nom="nom">
            // <References>
            //  <DC.Title>Enrichir la&#xa0;cellule canc&#xe9;reuse en&#xa0;platine, &#xe0;&#xa0;tout prix</DC.Title>
            //  <DC.Language>fr</DC.Language>
            //  <Pages>
            //       <Edition.Page.Debut>1029</Edition.Page.Debut>
            //       <Edition.Page.Fin>1029</Edition.Page.Fin>
            //       <Edition.Page.Ordre>1</Edition.Page.Ordre>
            //  </Pages>
            //  <Edition.Author>Jean B&#xe9;nard </Edition.Author>
            //  <JLE.DOI>10.1684/bdc.2010.1175</JLE.DOI>
            //  <JLE.Affiliation></JLE.Affiliation>
            //  <JLE.DatePubli>2010-09-01</JLE.DatePubli>
            //  <JLE.DateParu>2010-09-01</JLE.DateParu>
            //  <JLE.Lib.Paru>septembre 2010</JLE.Lib.Paru>
            //  <JLE.Gratuit>0</JLE.Gratuit>
            //  <JLE.Lib.Somm>Ce qu'il faut avoir lu</JLE.Lib.Somm>
            // </References>
            // <Corpus></Corpus>
            //</Document>

            //IndexXmlInfoObj.DatePubli = DateTime.Now.Year + "-"+(DateTime.Now.Month +1) +"-" + "01";

            string ENGJID = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ENGJID.txt";
            if (File.Exists(ENGJID))
               {
                    ENGJID = File.ReadAllText(ENGJID);
               }
            string JIDAID =jid+aid;
            if (_AOPArticle)
            {
                IndexXmlInfoObj.DateParu = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

                if (ENGJID.IndexOf(JID,StringComparison.OrdinalIgnoreCase) != -1)
                    IndexXmlInfoObj.LibParu = EngMonthName[DateTime.Now.Month] + " " + DateTime.Now.Year;
                else
                    IndexXmlInfoObj.LibParu = MonthName[DateTime.Now.Month] + " " + DateTime.Now.Year;

                //if (IndexXmlInfoObj.ArticleLanguage.Equals("en"))
                //    IndexXmlInfoObj.LibParu = EngMonthName[DateTime.Now.Month] + " " + DateTime.Now.Year;
                //else
                //    IndexXmlInfoObj.LibParu = MonthName[DateTime.Now.Month] + " " + DateTime.Now.Year;

                IndexXmlInfoObj.DatePubli = DateTime.Now.Year + "-" + (DateTime.Now.Month) + "-" + "01";
                IndexXmlInfoObj.DateParu = IndexXmlInfoObj.DatePubli;
            }
            else
            {
                string ISSUEMonth = "";
                string MM = "";
                if (Program.ISSUEMonth.IndexOf('-') != -1)
                {
                    string[] DualMM = Program.ISSUEMonth.Split('-');

                    ISSUEMonth = DualMM[0]; 
                    for (int i = 0; i < DualMM.Length; i++)
                    {
                        if (ENGJID.IndexOf(JID, StringComparison.OrdinalIgnoreCase) != -1)
                            MM += EngMonthName[Int16.Parse(DualMM[i])] + "-";
                        else
                            MM += MonthName[Int16.Parse(DualMM[i])] + "-";

                        //if (IndexXmlInfoObj.ArticleLanguage.Equals("en"))
                        //    MM+= EngMonthName[Int16.Parse(DualMM[i])] + "-";
                        //else
                        //    MM+= MonthName[Int16.Parse(DualMM[i])] +"-";
                    }
                    MM = MM.Trim(new char[]{'-'});
                }
                else
                {
                    ISSUEMonth = Program.ISSUEMonth.TrimStart(new char[]{'0'});
                    if (ENGJID.IndexOf(JID) != -1)
                        MM = EngMonthName[Int16.Parse(ISSUEMonth)];
                    else
                        MM = MonthName[Int16.Parse(ISSUEMonth)];
                    //if (IndexXmlInfoObj.ArticleLanguage.Equals("en"))
                    //    MM = EngMonthName[Int16.Parse(ISSUEMonth)];
                    //else
                    //    MM = MonthName[Int16.Parse(ISSUEMonth)];
                }

                IndexXmlInfoObj.DatePubli = DateTime.Now.Year + "-" + (DateTime.Now.Month).ToString().PadLeft(2, '0') + "-" + "01";
                IndexXmlInfoObj.DateParu  = Program.ISSUEYear + "-" + ISSUEMonth.PadLeft(2, '0') + "-" + "01";
                IndexXmlInfoObj.LibParu   = MM + " " + Program.ISSUEYear;

                //<JLE.DateParu> 2008-05-01 </ JLE.DateParu>
                //<JLE.Lib.Paru> May-June 2008 </ JLE.Lib.Paru>
            }

            
          //int  GetGratuit 
            if (Array.IndexOf(Program.FreeJID,JID)!=-1)
               IndexXmlInfoObj.Gratuit = "1";            
            else if (CheckFree(JIDAID))
               IndexXmlInfoObj.Gratuit = "1";            
            else
               IndexXmlInfoObj.Gratuit = "0";



            textWriter = new XmlTextWriter(_IndexXMLName, Encoding.UTF8);
            textWriter.Indentation = 1;
            textWriter.Formatting = Formatting.Indented;

            textWriter.WriteStartDocument();
            textWriter.WriteRaw(Environment.NewLine + "<!DOCTYPE Document SYSTEM \"jle.dtd\">");
            textWriter.WriteStartElement("Document");  /////////Document Start
            textWriter.WriteAttributeString("Code", "code");
            textWriter.WriteAttributeString("Nom", "nom");
            textWriter.WriteStartElement("References");/////////Reference Start

            textWriter.WriteStartElement("DC.Title");
            textWriter.WriteString(IndexXmlInfoObj.Title);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("DC.Language");

            if (IndexXmlInfoObj.ArticleLanguage.Equals(""))
                textWriter.WriteString("fr");
            else
                textWriter.WriteString(IndexXmlInfoObj.ArticleLanguage);

            textWriter.WriteEndElement();


            textWriter.WriteStartElement("Pages"); /////////Pages Start

            textWriter.WriteStartElement("Edition.Page.Debut");
            textWriter.WriteString(IndexXmlInfoObj.PageDebut.TrimStart('0'));
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Edition.Page.Fin");
            textWriter.WriteString(IndexXmlInfoObj.PageFin.TrimStart('0'));
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Edition.Page.Ordre");
            textWriter.WriteString("1");
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();         /////////Pages Close

            textWriter.WriteStartElement("Edition.Author");
            textWriter.WriteString(IndexXmlInfoObj.Author);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JLE.DOI");
            textWriter.WriteString(IndexXmlInfoObj.doi);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JLE.Affiliation");
            textWriter.WriteString(IndexXmlInfoObj.Affilation);
            textWriter.WriteEndElement();


            if (_AOPArticle == false)
            {
                textWriter.WriteStartElement("JLE.DatePubli");
                textWriter.WriteString(IndexXmlInfoObj.DatePubli);
                textWriter.WriteEndElement();
            }

            textWriter.WriteStartElement("JLE.DateParu");
            textWriter.WriteString(IndexXmlInfoObj.DateParu);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JLE.Lib.Paru");
            textWriter.WriteString(IndexXmlInfoObj.LibParu);
            //textWriter.WriteString("Hors série n°1, Décembre 2010");

            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JLE.Gratuit");
            textWriter.WriteString(IndexXmlInfoObj.Gratuit);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JLE.Lib.Somm");
            textWriter.WriteString(IndexXmlInfoObj.LibSomm.Trim());
            textWriter.WriteEndElement();

            textWriter.WriteEndElement();/////////Reference Close

            if (!IndexXmlInfoObj.FrAbstract.Equals(""))
            {
                textWriter.WriteStartElement("CaracteristiquesFR");//by sunil
                textWriter.WriteStartElement("Edition.Resume");
                textWriter.WriteRaw(IndexXmlInfoObj.FrAbstract);
                textWriter.WriteEndElement();/////////Edition.Resume Close

                if (!IndexXmlInfoObj.Motscles.Equals(""))
                {
                    textWriter.WriteStartElement("Edition.Motscles");
                    textWriter.WriteString(IndexXmlInfoObj.Motscles);
                    textWriter.WriteEndElement();/////////Edition.Resume Close
                }
                textWriter.WriteEndElement();/////////CaracteristiquesFR Close
            }
            if (!IndexXmlInfoObj.EnAbstract.Equals(""))
            {
                textWriter.WriteStartElement("CaracteristiquesEN");

                if (!IndexXmlInfoObj.ENTitle.Equals(""))
                {
                    textWriter.WriteStartElement("DC.Title");
                    textWriter.WriteRaw(IndexXmlInfoObj.ENTitle);
                    textWriter.WriteEndElement();/////////Edition.Resume Close
                }

                textWriter.WriteStartElement("Edition.Resume");
                textWriter.WriteString(IndexXmlInfoObj.EnAbstract);
                textWriter.WriteEndElement();/////////Edition.Resume Close

                if (!IndexXmlInfoObj.ENKeyWords.Equals(""))
                {
                    textWriter.WriteStartElement("Edition.Motscles");
                    textWriter.WriteString(IndexXmlInfoObj.ENKeyWords);
                    textWriter.WriteEndElement();/////////Edition.Resume Close
                }
                textWriter.WriteEndElement();/////////CaracteristiquesFR Close
            }
            else if (!IndexXmlInfoObj.ENTitle.Equals(""))
            {
                textWriter.WriteStartElement("CaracteristiquesEN");
                    textWriter.WriteStartElement("DC.Title");
                        textWriter.WriteRaw(IndexXmlInfoObj.ENTitle);
                    textWriter.WriteEndElement();/////////DC.Title Close
                    if (!IndexXmlInfoObj.ENKeyWords.Equals(""))
                    {
                        textWriter.WriteStartElement("Edition.Motscles");
                        textWriter.WriteString(IndexXmlInfoObj.ENKeyWords);
                        textWriter.WriteEndElement();/////////Edition.Resume Close
                    }
                textWriter.WriteEndElement();/////////CaracteristiquesEN
            }

            textWriter.WriteEndElement();/////////Document end
            textWriter.WriteEndDocument();
            textWriter.Flush();
            textWriter.Close();

            StringBuilder HtmlStr = new StringBuilder(File.ReadAllText(IndexXMLName));
            HtmlStr.Replace("#$#", "&");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");
            HtmlStr.Replace("\n\n", "\n");

            HtmlStr.Replace("  ", " ");
            HtmlStr.Replace("  ", " ");
            HtmlStr.Replace("> <", "><");


            HtmlStr.Replace("&lt;b&gt;", "<b>");
            HtmlStr.Replace("&lt;i&gt;", "<i>");
            HtmlStr.Replace("&lt;sub&gt;", "<sub>");
            HtmlStr.Replace("&lt;sup&gt;", "<sup>");

            HtmlStr.Replace("&lt;/b&gt;", "</b>");
            HtmlStr.Replace("&lt;/i&gt;", "</i>");

            HtmlStr.Replace("&lt;/sub&gt;", "</sub>");
            HtmlStr.Replace("&lt;/sup&gt;", "</sup>");

            HtmlStr.Replace("&lt;strike&gt;", "<strike>");
            HtmlStr.Replace("&lt;/strike&gt;", "</strike>");
            File.WriteAllText(IndexXMLName, ReplaceEntity(HtmlStr.ToString()).ToString());
        }
        private void ProcessHsp() 
        {
          //HtmlStr.Replace("#$#", "&");
            XmlNodeList NL=null ;
            try
            {
                NL = MyXmlDocument.GetElementsByTagName("td:hsp");
            }
            catch
            {
            }
            if(NL.Count>0)
            {
                XmlNode node = NL[0];
                if (node.Attributes.GetNamedItem("sp") != null)
                {
                    string AtrStr = node.Attributes.GetNamedItem("sp").Value;
                    if (AtrStr.Equals("0.16") ||AtrStr.Equals("0.25"))
                    {
                        //<!ENTITY VeryThinSpace    "&#x0200A;" ><!--space of width 1/18 em alias ISOPUB hairsp -->
                        //<!ENTITY ThinSpace        "&#x02009;" ><!--space of width 3/18 em alias ISOPUB thinsp -->
                        node.ParentNode.InnerXml = node.ParentNode.InnerXml.Replace(node.OuterXml, "#$##x02009;");
                    }
                    else if (AtrStr.Equals("0.5"))
                    {
                        node.ParentNode.InnerXml = node.ParentNode.InnerXml.Replace(node.OuterXml, "#$##x02009;");
                        //<!ENTITY MediumSpace      "&#x0205F;" ><!--space of width 4/18 em -->
                    }
                    else if (AtrStr.Equals("1.0"))
                    {
                        node.ParentNode.InnerXml = node.ParentNode.InnerXml.Replace(node.OuterXml, "#$##x02009;");
                        //<!ENTITY ThinSpace        "&#x02009;" ><!--space of width 3/18 em alias ISOPUB thinsp -->
                    }
                    else
                        node.ParentNode.InnerXml = node.ParentNode.InnerXml.Replace(node.OuterXml, " ");
                }
                ProcessHsp();
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
                XmlNodeList NL = node.SelectNodes(".//*",nsmgr );

                foreach (XmlNode chNode in NL)
                {
                    if (chNode.Name.Equals("td:list"))
                    {}
                    else if (chNode.Name.Equals("td:vsp"))
                    {}
                    else if (chNode.Name.Equals("td:underline"))
                    {}
                    else if (chNode.Name.Equals("td:label"))
                    {}
                    else if (chNode.Name.Equals("td:list-item"))
                    {}
                    else if (chNode.Name.Equals("td:para"))
                    {}
                    else if (chNode.Name.StartsWith("td:cross-ref"))
                    {
                        if (chNode.ParentNode!= null)
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
                    else if (chNode.Name.StartsWith("td:small-caps"))
                    {
                        if (chNode.ParentNode!= null)
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText.ToUpper());
                    }
                    else if (chNode.Name.StartsWith("td:inline"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:link"))
                    {
                        chNode.ParentNode.RemoveChild(chNode);
                    }
                    else if (chNode.Name.StartsWith("mml:"))
                    {
                        chNode.ParentNode.RemoveChild(chNode);
                    }
                    else
                    {
                        Console.WriteLine("************Warning.**************************");
                        Console.WriteLine("Please check this element :: " + chNode.Name);
                        Console.WriteLine("Application does not support this element in abstract node.");
                        Console.WriteLine("Please specify this element in application.");
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                        Console.WriteLine("**************************************");
                    }
                }

                ///////////Please dont change sequence
                StringBuilder  XmlStr = new StringBuilder ( node.InnerXml);
                XmlStr.Replace("<td:list-item><td:para>", "<li>");
                XmlStr.Replace("</td:para></td:list-item>", "</li>");

                XmlStr.Replace("</td:label><td:para>", " ");

                

                XmlStr.Replace("td:list-item", "li");
                XmlStr.Replace("td:list", "ul");
                XmlStr.Replace("td:para", "p");

                XmlStr.Replace("td:underline", "u");

                XmlStr.Replace("<td:label>", "");
                XmlStr.Replace("</td:label>", " ");
                node.InnerXml = XmlStr.ToString();
            }
            catch
            { 
            }
        }
        private bool CheckFree(string JIDAID)
        {

            for(int i=0; i<Program.FreeArticle.Length;i++)
            {
                if (Program.FreeArticle[i].StartsWith(JIDAID))
                {
                    return true;
                }
            }
              
            return false;
        }
        private void ProcessTitle(XmlNode node)
        {

            if (node == null)
            {
                return;
            }
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
                    if (chNode.Name.StartsWith("td:cross-ref"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:link"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:inline-figure"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:inter-ref"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                    }
                    else if (chNode.Name.StartsWith("td:small-caps"))
                    {
                        chNode.ParentNode.InnerXml = chNode.ParentNode.InnerXml.Replace(chNode.OuterXml, chNode.InnerText.ToUpper());
                    }
                    else if (chNode.NodeType == XmlNodeType.Comment)
                    {
                        node.ParentNode.RemoveChild(node);
                    }
                    else
                    {
                        Console.WriteLine("************Warning.**************************");
                        Console.WriteLine("Please check this element :: " + chNode.Name);
                        Console.WriteLine("Application does not support this element in title node.");
                        Console.WriteLine("Please specify this element in application.");
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                        Console.WriteLine("**************************************");
                    }
                    //node.InnerXml = node.InnerXml.Replace(chNode.OuterXml, chNode.InnerText);
                }
            }
            catch
            {
            }
        }
        private void SufflingBib()
        {
            XmlElement ele;
            XmlNodeList Au_NodeList = MyXmlDocument.GetElementsByTagName("tp:author");
            XmlNode SurName, GivenNames, Prefix, Suffix;
            if (Au_NodeList.Count > 0)
            {
                for (int i = 0; i < Au_NodeList.Count; i++)
                {
                    XmlNodeList NodeList = Au_NodeList[i].ChildNodes;
                    ele = MyXmlDocument.CreateElement("name");

                    SurName = Au_NodeList[i].SelectSingleNode(".//td:surname", nsmgr);
                    GivenNames = Au_NodeList[i].SelectSingleNode(".//td:given-name", nsmgr);
                    Prefix = Au_NodeList[i].SelectSingleNode(".//td:initials", nsmgr);
                    Suffix = Au_NodeList[i].SelectSingleNode(".//td:suffix", nsmgr);
                    if (SurName != null) ele.AppendChild(SurName);
                    if (GivenNames != null)
                    {
                        GivenNames.InnerXml=GivenNames.InnerXml.Replace(".","");
                        ele.AppendChild(GivenNames);
                    }
                    if (Prefix != null) ele.AppendChild(Prefix);
                    if (Suffix != null) ele.AppendChild(Suffix);
                    if (ele.HasChildNodes)
                    {
                        if (Au_NodeList[i].ParentNode.Name.Equals("tp:authors"))
                        {
                            Au_NodeList[i].RemoveAll();
                            Au_NodeList[i].PrependChild(ele);
                            Au_NodeList[i].InnerXml = Au_NodeList[i].InnerXml.Replace("<name>", "").Replace("</name>", "");
                        }
                    }
                }
            }


            //XmlNodeList BibNodeList = MyXmlDocument.GetElementsByTagName("td:bib-reference");
            //XmlNode TpDate;
            //string BibStr;
            //string DateXml; 
            //for (int i = 0; i < BibNodeList.Count; i++)
            //{
            //    TpDate = BibNodeList[i].SelectSingleNode(".//tp:date", nsmgr);
            //    if (TpDate!= null)
            //    {
            //        DateXml ="<tp:date>"+ TpDate.InnerXml + ". </tp:date>";
            //        BibStr = BibNodeList[i].InnerXml;
            //        BibStr = BibStr.Replace(DateXml,"");

            //        BibStr = BibStr.Replace("</tp:authors>", " </tp:authors>");
            //        BibStr = BibStr.Replace("</tp:authors>", "</tp:authors>, " + DateXml );

                    

            //        BibNodeList[i].InnerXml = BibStr;
            //        TpDate.ParentNode.RemoveChild(TpDate);
            //    }
            //}

        }
    }
    
    class PubmedInfo
    {
        string _Abstract      = "";
        string _Affiliation   = "";
        string _Article       = "";
        string _ArticleId     = "";
        string _ArticleIdList = "";
        string _ArticleSet    = "";
        string _ArticleTitle  = "";
        string _Author        = "";
        string _AuthorList    = "";
        string _Day           = "";
        string _FirstName     = "";
        string _FirstPage     = "";
        string _History       = "";
        string _Issn          = "";
        string _Issue         = "";
        string _Journal       = "";
        string _JournalTitle  = "";
        string _Language      = "";
        string _LastName      = "";
        string _LastPage      = "";
        string _MiddleName    = "";
        string _Month         = "";
        string _PubDate       = "";
        string _PublicationType = "";
        string _PublisherName   = "";
        string _VernacularTitle = "";
        string _Volume          = "";
        string _Year            = "";
        string _Objectlist = "";
        string _Object = "";
        string _param = "";
        string _ENKeyWords = "";

        string _PMID         = string.Empty;
      

        public static StringDictionary PubMedID = new StringDictionary();
        IndexXmlInfo IndexPubXmlInfoObj = new IndexXmlInfo();


        public string PMID
        {
            get { return _PMID; }
            set { _PMID = value; }
        }
        public string ENKeyWords
        {
            get { return _ENKeyWords; }
            set { _ENKeyWords = value; }
        }
        public string Objectlist
        {
            get { return _Objectlist; }
            set { _Objectlist = value; }
        }
        public string param
        {
            get { return _param; }
            set { _param = value; }
        }
        public string Object
        {
            get { return _Object; }
            set { _Object = value; }
        }
        public string Abstract
        {
            get { return _Abstract; }
            set { _Abstract = value; }
        }
        public string Affiliation
        {
            get { return _Affiliation; }
            set { _Affiliation = value; }
        }
        public string Article
        {
            get { return _Article; }
            set { _Article = value; }
        }
        public string ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        public string ArticleIdList
        {
            get { return _ArticleIdList; }
            set { _ArticleIdList = value; }
        }
        public string ArticleSet
        {
            get { return _ArticleSet; }
            set { _ArticleSet = value; }
        }
        public string ArticleTitle
        {
            get { return _ArticleTitle; }
            set { _ArticleTitle = value; }
        }
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        public string AuthorList
        {
            get { return _AuthorList; }
            set { _AuthorList = value; }
        }
        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string FirstPage
        {
            get { return _FirstPage; }
            set { _FirstPage = value; }
        }
        public string History
        {
            get { return _History; }
            set { _History = value; }
        }
        public string Issn
        {
            get { return _Issn; }
            set { _Issn = value; }
        }
        public string Issue
        {
            get { return _Issue; }
            set { _Issue = value; }
        }
        public string Journal
        {
            get { return _Journal; }
            set { _Journal = value; }
        }
        public string JournalTitle
        {
            get { return _JournalTitle; }
            set { _JournalTitle = value; }
        }
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
        public string LastPage
        {
            get { return _LastPage; }
            set { _LastPage = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        public string PubDate
        {
            get { return _PubDate; }
            set { _PubDate = value; }
        }
        public string PublicationType
        {
            get { return _PublicationType; }
            set { _PublicationType = value; }
        }
        public string PublisherName
        {
            get { return _PublisherName; }
            set { _PublisherName = value; }
        }
        public string VernacularTitle
        {
            get { return _VernacularTitle; }
            set { _VernacularTitle = value; }
        }
        public string Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string JID
        {
            get { return _JID; }
            set { _JID = value; }
        }
        string _PubmedXMLName = "";
        string _JID = "";
        XmlDocument MyXmlDocument;
        XmlTextWriter textWriter;
        XmlNamespaceManager nsmgr = null;

        public PubmedInfo()
        {
        }
        public PubmedInfo(XmlDocument xmlDocument, string PubmedXMLName)
            : this()
        {
            MyXmlDocument = xmlDocument;
            _PubmedXMLName = PubmedXMLName;


            StringBuilder TempString = new StringBuilder(MyXmlDocument.InnerXml);
            TempString.Replace("<td:para><!--<Ack>--></td:para>", "");
            TempString.Replace("<td:para><!--</Ack>--></td:para>", "");
            TempString.Replace("<!--<SHADE>", "");
            TempString.Replace("</SHADE>-->", "");
            TempString.Replace("<SHADE>", "");
            TempString.Replace("</SHADE>", "");
            TempString.Replace("<!--<Heading>-->", "&lt;b&gt;").Replace("<!--</Heading>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<Ack>-->", "&lt;b&gt;").Replace("<!--</Ack>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-No>-->", "&lt;b&gt;").Replace("<!--</ABS-No>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-Title>-->", "&lt;b&gt;").Replace("<!--</ABS-Title>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<ABS-Reference>-->", "&lt;b&gt;").Replace("<!--</ABS-Reference>-->", "&lt;/b&gt;");
            TempString.Replace("<!--<RunningTitle>", "<RunningTitle>");
            TempString.Replace("</RunningTitle>-->", "</RunningTitle>");

            TempString.Replace("<!--<RunningTitle&gt;", "<RunningTitle>");
            TempString.Replace("</RunningTitle>-->", "</RunningTitle>");
            TempString.Replace("&lt;/RunningTitle>-->", "</RunningTitle>");

            MyXmlDocument.InnerXml = TempString.ToString();

            //<td:given-name>Fr&eacute;d&eacute;ric</td:given-name><td:surname>Bretagnol</td:surname>
            //F. Bretagnol

            //////////////////////////////Put tp:date after titile
            //<td:given-name>Fr&eacute;d&eacute;ric</td:given-name><td:surname>Bretagnol</td:surname>
            //F. Bretagnol

            //////////////////////////////Put tp:date after titile
            //add by puneet 4/3/2013
            int XCount1 = 0;
            XmlNodeList RunntingTitle = MyXmlDocument.GetElementsByTagName("RunningTitle");
            while (XCount1 < RunntingTitle.Count)
            {
                RunntingTitle[0].ParentNode.RemoveChild(RunntingTitle[0]);
            }
            

        }

        public void CreatePubmedXML()
        {
            JID = MyXmlDocument.GetElementsByTagName("jid")[0].InnerText;

            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("docsubtype") != null)
            {
                string docsubtype = MyXmlDocument.DocumentElement.Attributes.GetNamedItem("docsubtype").Value;
                //if ("edi#lit".IndexOf(docsubtype) != -1 && _JID.Equals("BDC"))
                //{
                //    return;/////////////No need to generate Pubmed xml foe BDC's editorial article.
                //}
            }
            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            nsmgr.AddNamespace("td",    "http://www.thomsondigital.com/xml/common/dtd");
            nsmgr.AddNamespace("sb",    "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsmgr.AddNamespace("mml",   "http://www.w3.org/1998/Math/MathML");
            nsmgr.AddNamespace("tb",    "http://www.thomsondigital.com/xml/common/table/dtd");
            nsmgr.AddNamespace("tp",    "http://www.thomsondigital.com/xml/common/struct-bib/dtd");

            /////////////////////****************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\    
            textWriter = new XmlTextWriter(_PubmedXMLName, null);
            /////////////////////****************************************\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            string JIDAID = JID + ArticleId;

            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 1;
            textWriter.IndentChar = '\t';
            textWriter.WriteStartDocument();

            //<!DOCTYPE ArticleSet PUBLIC "-//NLM//DTD PubMed 2.0//EN" "http://www.ncbi.nlm.nih.gov:80/entrez/query/static/PubMed.dtd">
            //textWriter.WriteDocType("ArticleSet",  "-//NLM//DTD PubMed 2.0//EN", "http://www.ncbi.nlm.nih.gov:80/entrez/query/static/PubMed.dtd", "");

            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string PubmedDTDPath = ExeLoc + "\\DTD\\PUBMED\\PubMed.dtd";
             PubmedDTDPath =  "PubMed.dtd";

            textWriter.WriteDocType("ArticleSet", "-//NLM//DTD PubMed 2.0//EN", PubmedDTDPath, "");
            textWriter.WriteStartElement("ArticleSet");
            textWriter.WriteStartElement("Article");

            textWriter.WriteStartElement("Journal");

            textWriter.WriteStartElement("PublisherName");
            textWriter.WriteString("John Libbey Eurotext");
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("JournalTitle");


            string JT = GetJournalTitle(_JID);
            textWriter.WriteString(JT);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Issn");
            _Issn = GetISSN(_JID);
            textWriter.WriteString(_Issn);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Volume");

            if (_Volume.Equals(""))
                textWriter.WriteString(" ");
            else
                textWriter.WriteString(_Volume);

            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Issue");
            
            if (_Issue.Equals(""))
                textWriter.WriteString(" ");
            else
                textWriter.WriteString(_Issue);

            textWriter.WriteEndElement();

            //<Year>2009</Year><Month>09</Month><Day>14</Day>

            if (_Issue.Equals(""))
            {
                textWriter.WriteStartElement("PubDate");
                textWriter.WriteAttributeString("PubStatus", "aheadofprint");

                textWriter.WriteStartElement("Year");
                textWriter.WriteString(DateTime.Now.Year.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Month");
                textWriter.WriteString(DateTime.Now.Month.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Day");
                textWriter.WriteString(DateTime.Now.Day.ToString());

                textWriter.WriteEndElement();
                textWriter.WriteEndElement(); ///////////PubDate close
            }
            else
            {
                textWriter.WriteStartElement("PubDate");
                textWriter.WriteAttributeString("PubStatus", "ppublish");

                textWriter.WriteStartElement("Year");
                textWriter.WriteString(Program.ISSUEYear);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Month");
                
                if (Program.ISSUEMonth.IndexOf("-") == -1)
                    textWriter.WriteString(Program.ISSUEMonth.TrimStart ('0').PadLeft (2,'0'));
                else
                {
                    string [] MM= Program.ISSUEMonth.Split('-');
                    textWriter.WriteString(MM[1].TrimStart('0').PadLeft(2, '0'));
                }

                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Day");
                textWriter.WriteString("01");

                textWriter.WriteEndElement();
                textWriter.WriteEndElement(); ///////////PubDate close
            }



            textWriter.WriteEndElement();////////////////Journal Close

            
            //if (PubMedID.ContainsKey(JIDAID))
            //{
            //    //<Replaces IdType="pubmed">21393089</Replaces>
            //    if (!PubMedID[JIDAID].Equals(""))
            //    {
            //        textWriter.WriteStartElement("Replaces");
            //        textWriter.WriteAttributeString("IdType", "pubmed");
            //        textWriter.WriteString(PubMedID[JIDAID]);
            //        textWriter.WriteEndElement();
            //    }
            //}

            if (!string.IsNullOrEmpty(_PMID))
            {
                //<Replaces IdType="pubmed">21393089</Replaces>
                
                    textWriter.WriteStartElement("Replaces");
                    textWriter.WriteAttributeString("IdType", "pubmed");
                    textWriter.WriteString(_PMID);
                    textWriter.WriteEndElement();
                
            }

            string AT = GetArticleTitle();
            if (!AT.Equals(""))
            {
                textWriter.WriteStartElement("ArticleTitle");
                textWriter.WriteString(AT);
                textWriter.WriteEndElement();
            }
            else
            {
                Console.WriteLine("English article title does not exist in  xml..");
                Console.WriteLine("Please check, it's required for pubmed xml..");
                Console.WriteLine("Press any key to exit..");
                Console.ReadLine();
                Environment.Exit(0);
            }

            string VT = GetVernacularTitle();
            if (!VT.Equals(""))
            {
                textWriter.WriteStartElement("VernacularTitle");
                textWriter.WriteString(VT);
                textWriter.WriteEndElement();
            }

            textWriter.WriteStartElement("FirstPage");
            if (_FirstPage.Equals(""))
                textWriter.WriteString(" ");
            else
                textWriter.WriteString(FirstPage);

            textWriter.WriteEndElement();

            textWriter.WriteStartElement("LastPage");


          
            if (_LastPage.Equals(""))
                textWriter.WriteString(" ");
            else
                textWriter.WriteString(_LastPage);

            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Language");

            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value == "fr")
                    textWriter.WriteString("FR");
                else
                    textWriter.WriteString("EN");
            }

            
            textWriter.WriteEndElement();

            

            textWriter.WriteStartElement("AuthorList");
            CreateAuthorList();
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("PublicationType");
            textWriter.WriteString(" ");
            textWriter.WriteEndElement();

            if (ArticleIdList.Equals(""))
            {
                textWriter.WriteStartElement("ArticleIdList");
                string DOI = "";
                DOI = GetDOI();
                if (DOI.IndexOf('/') != -1)
                {
                    string PII = DOI.Split('/').GetValue(1).ToString();
                    textWriter.WriteStartElement("ArticleId");
                    textWriter.WriteAttributeString("IdType", "pii");
                    textWriter.WriteString(PII);
                    textWriter.WriteEndElement();
                }
                textWriter.WriteStartElement("ArticleId");
                textWriter.WriteAttributeString("IdType", "doi");
                textWriter.WriteString(DOI);
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();
            }
            else
            {
                textWriter.WriteStartElement("ArticleIdList");
                textWriter.WriteRaw(ArticleIdList);
                textWriter.WriteEndElement();

            }
            textWriter.WriteStartElement("History");
            textWriter.WriteString(" ");
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Abstract");
           



            
            string ABSTEXT ="";
            XmlElement Dummy = MyXmlDocument.CreateElement("Dummy");
            Dummy.InnerXml =_Abstract.Replace("&lt;sub&gt;","&lt;inf&gt;").Replace("&lt;/sub&gt;","&lt;/inf&gt;");
            //sup | inf
            foreach (XmlNode chNode in Dummy)
            {
                ABSTEXT += chNode.InnerText + " ";
            }
            ABSTEXT = ABSTEXT.Trim();
            textWriter.WriteRaw(ABSTEXT);
            textWriter.WriteEndElement();
            string KeyWordString = "";
            KeyWordString = GetKeyWords1(nsmgr);

            KeyWordString = KeyWordString.Replace("<sup>", "");
            KeyWordString = KeyWordString.Replace("</sup>", "");
            KeyWordString = KeyWordString.Replace("<inf>", "");
            KeyWordString = KeyWordString.Replace("</inf>", "");
            KeyWordString = KeyWordString.Replace("#$#lt;sub#$#gt;", "");
            KeyWordString = KeyWordString.Replace("#$#lt;sup#$#gt;", "");
            KeyWordString = KeyWordString.Replace("#$#lt;/sub#$#gt;", "");
            KeyWordString = KeyWordString.Replace("#$#lt;/sup#$#gt;", "");
            
            //XmlNodeList  nodelist = MyXmlDocument.GetElementsByTagName("td:keyword");
            //for (int j = 0; j < nodelist.Count; j++)
            //{
            // KeyWordString += nodelist[j].FirstChild.InnerXml + ", ";
              
            //}
            // KeyWordString =  KeyWordString.Trim(new char[] { ' ', ',' });
            KeyWordString = KeyWordString.ToString();
           // KeyWordString = KeyWordString.Replace("<sup>", "").Replace("</sup>", "");
            if (KeyWordString != "")
            {
                textWriter.WriteStartElement("ObjectList");
                string[] str = KeyWordString.Split(',');
                for (int i = 0; i < str.Length - 1; i++)
                {
                 
                    textWriter.WriteStartElement("Object");
                    textWriter.WriteAttributeString("Type", "keyword");
                    textWriter.WriteStartElement("Param");
                    textWriter.WriteAttributeString("Name", "value");
                    textWriter.WriteString(str[i].Trim());
                    textWriter.WriteEndElement();
                    textWriter.WriteEndElement();
                }
                textWriter.WriteEndElement();
            }
            //else
            //{
            //    textWriter.WriteString(" ");
            //    textWriter.WriteEndElement();
            //}
         
            textWriter.WriteEndElement();  ////////////////Articlet Close
            //textWriter.WriteEndElement();////////////////ArticleSet Close
            //textWriter.WriteEndElement();
            //textWriter.WriteEndElement();
            //textWriter.WriteEndElement();

            textWriter.Flush();
            textWriter.Close();

            StringBuilder HtmlStr = new StringBuilder(File.ReadAllText(_PubmedXMLName));

            HtmlStr.Replace("#$#", "&");

            //HtmlStr.Replace("\t", "");

            HtmlStr.Replace("\r", "");
            HtmlStr.Replace("> <", "><");

            HtmlStr.Replace("&lt;b&gt;", "");
            HtmlStr.Replace("&lt;i&gt;", "");

            HtmlStr.Replace("&lt;sub&gt;", "<inf>");
            HtmlStr.Replace("&lt;sup&gt;", "<sup>");

            HtmlStr.Replace("&lt;/b&gt;", "");
            HtmlStr.Replace("&lt;/i&gt;", "");

            HtmlStr.Replace("&lt;/sub&gt;", "</inf>");
            HtmlStr.Replace("&lt;/sup&gt;", "</sup>");
            HtmlStr.Replace("<sup> </sup>", " ");
            HtmlStr.Replace("<inf> </inf>", " ");
            HtmlStr.Replace("<inf>1</inf>", "1");
            HtmlStr.Replace("<inf>2-</inf>", "2-");
            //HtmlStr.Replace("<Param>&#x003B2;<inf>","<Param>&#x003B2;");
            //HtmlStr.Replace("</head>", "<link href=\"xEdt.css\" rel=\"stylesheet\" type=\"text/css\" />\n</head>");

            HtmlStr = ReplacePubmedEntity(HtmlStr.ToString());
            HtmlStr = ReplaceEntity(HtmlStr.ToString());
            File.WriteAllText(_PubmedXMLName, HtmlStr.ToString());
            if (!PubMedXmlParsing(_PubmedXMLName))
            { 
            }
        }
      
        public bool PubMedXmlParsing(string str)
        {
            //DTD\PUBMED\PubMed.dtd
            //D:\NewProject\JLEDataSet\JLEDATASET\bin\Debug\DTD\PUBMED\PubMed.dtd
            try
            {
                string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string ValidationPath = ExeLoc + "\\validation";
                string PUBMEDPath = ExeLoc + "\\DTD\\PUBMED\\PubMed.dtd.";


                File.WriteAllText(str, File.ReadAllText(str).Replace("PubMed.dtd",PUBMEDPath ));

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
                //$$$if (FInfo.Length == 0)
                {
                    File.Delete(LogFIle);

                    string RplcStr = @"http://www.ncbi.nlm.nih.gov:80/entrez/query/static/PubMed.dtd";

                    File.WriteAllText(str, File.ReadAllText(str).Replace(PUBMEDPath, RplcStr).Replace("dtd\"[]", "dtd\""));// .Replace("\t", ""));
                    Console.WriteLine("Result :: OK");
                    return true;
                }
                //else
                //{
                //    StringBuilder LogStr = new StringBuilder(File.ReadAllText(LogFIle));
                //    LogStr.Replace(ValidationPath + "\\nsgmls:", "");
                //    LogStr.Replace(str + ":", "");
                //    File.WriteAllText(LogFIle, LogStr.ToString());
                //    Process.Start("notepad", LogFIle);
                //}
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + Environment.NewLine);
            }
            return false;
        }
        public string GetJournalTitle(string _JID)
        {
            _JID = JID.ToLower();
            string JT = "";
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\JleJIDTitle.txt";
            string[] JIDTitle = File.ReadAllLines(FileName);
            foreach (string str in JIDTitle)
            {
                if (str.StartsWith(JID, StringComparison.OrdinalIgnoreCase))
                {
                    JT = str.Split('\t')[1];
                    JT = JT.Replace("&", "#$#");
                    break;
                }
            }
            return JT;
        }


        public string GetKeyWords1(XmlNamespaceManager nsmgr)
        {
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:keywords");
            string EnKeyWords = "";
            string LNG = "";
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Attributes.GetNamedItem("xml:lang") != null)
                {
                    LNG = nodeList[i].Attributes.GetNamedItem("xml:lang").Value;
                }

                if (LNG.Equals("en"))
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        EnKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexPubXmlInfoObj.ENKeyWords = EnKeyWords.Trim(new char[] { ',', ' ' });
                }
                
                else if (nodeList[i].FirstChild.InnerXml.IndexOf("Key word", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    XmlNodeList ParaList = nodeList[i].SelectNodes(".//td:text", nsmgr);
                    for (int j = 0; j < ParaList.Count; j++)
                    {
                        EnKeyWords += ParaList[j].InnerXml + ", ";
                    }
                    IndexPubXmlInfoObj.ENKeyWords = EnKeyWords.Trim(new char[] { ',', ' ' });
                }
                
            }
            return EnKeyWords;

        }
        public string GetISSN(string JID)
        {
            JID = JID.ToLower();
            string ISSN = " ";
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ISSN.txt";
            string[] JIDTitle = File.ReadAllLines(FileName);
            foreach (string str in JIDTitle)
            {
                if (str.StartsWith(JID, StringComparison.OrdinalIgnoreCase))
                {
                    ISSN = str.Split(' ')[1];
                    break;
                }
            }

            if (ISSN.Equals(""))
            {
                Console.WriteLine("ISSN not define for this JID");
                Console.WriteLine("Please define in file : ");
                Console.WriteLine(FileName);
                Console.WriteLine("Please press any key to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return ISSN;
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
        public string GetVernacularTitle()
        {
            string VT = "";
            XmlNodeList NL;
            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value.Equals("en"))
                {
                    return "";
                }
            }
            NL = MyXmlDocument.GetElementsByTagName("td:title");
            if (NL.Count > 0)
            {
                VT = NL[0].InnerXml;
            }
            return VT;
        }
        public string GetDOI()
        {
            string DOI = "";
            XmlNodeList DoiNode = MyXmlDocument.GetElementsByTagName("td:doi");
            if (DoiNode.Count > 0)
            {
                if (DoiNode[0].ParentNode.Name.Equals("item-info"))
                {
                    DOI = DoiNode[0].InnerText;
                }
            }
            return DOI;
        }

        public void CreateAuthorList()
        {
            XmlNodeList AuthorNL = MyXmlDocument.GetElementsByTagName("td:author");
            string sufix = "";
            string AffilName = "";
            foreach (XmlNode node in AuthorNL)
            {
                foreach (XmlNode chnode in node)
                {
                    if (!chnode.Equals("") && chnode.NodeType == XmlNodeType.Element)
                        chnode.InnerXml = chnode.InnerXml.Replace("#$#lt;sup#$#gt;a#$#lt;/sup#$#gt;", "#$##x00AA;");
                    if (chnode.Name.Equals("td:suffix"))
                    {
                        sufix = chnode.InnerXml;
                    }
                    else if (chnode.Name.Equals("td:given-name"))
                    {
                        _FirstName = chnode.InnerXml;
                    }
                    else if (chnode.Name.Equals("td:surname"))
                    {
                        _LastName = chnode.InnerXml;
                    }
                    else if (chnode.Name.Equals("td:cross-ref"))
                    {
                        //XmlNodeList NodeList = node.SelectNodes(".//td:cross-ref", nsmgr);
                        //if (NodeList.Count > 0)
                        //{
                            //string AfflID = NodeList[0].Attributes[0].InnerText;
                            string AfflID = chnode.Attributes[0].InnerText;
                            XmlNodeList aflNl = node.SelectNodes("//td:affiliation[@id='" + AfflID + "']", nsmgr);
                            if (aflNl.Count > 0)
                            {
                                for (int j = 0; j < aflNl[0].ChildNodes.Count; j++)
                                {
                                    if (!aflNl[0].ChildNodes[j].Name.Equals("td:label"))
                                        AffilName += ", " +aflNl[0].ChildNodes[j].InnerText;
                                }
                                AffilName = AffilName.Trim();
                            }
                        //}
                    }
                }
                if (AffilName.Equals(""))
                {
                    XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:affiliation");
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        for (int j = 0; j < nodeList[i].ChildNodes.Count; j++)
                        {
                            if (!nodeList[i].ChildNodes[j].Name.Equals("td:label"))
                                AffilName += nodeList[i].ChildNodes[j].InnerText + " ";
                        }
                        AffilName = AffilName.Trim();
                        AffilName += ", ";
                    }
                    
                }
                AffilName = AffilName.Trim(new char[] { ' ', ',' });

                AffilName = AffilName.Replace("&lt;b&gt;",   "");
                AffilName = AffilName.Replace("&lt;i&gt;",   "");
                AffilName = AffilName.Replace("&lt;sub&gt;", "");
                AffilName = AffilName.Replace("&lt;sup&gt;", "");

                AffilName = AffilName.Replace("&lt;/b&gt;",   "");
                AffilName = AffilName.Replace("&lt;/i&gt;",   "");
                AffilName = AffilName.Replace("&lt;/sub&gt;", "");
                AffilName = AffilName.Replace("&lt;/sup&gt;", "");

                AffilName = AffilName.Replace("#$#lt;b#$#gt;",    "");
                AffilName = AffilName.Replace("#$#lt;i#$#gt;",    "");
                AffilName = AffilName.Replace("#$#lt;sub#$#gt;",  "");
                AffilName = AffilName.Replace("#$#lt;sup#$#gt;",  "");

                AffilName = AffilName.Replace("#$#lt;/b#$#gt;",   "");
                AffilName = AffilName.Replace("#$#lt;/i#$#gt;",   "");
                AffilName = AffilName.Replace("#$#lt;/sub#$#gt;", "");
                AffilName = AffilName.Replace("#$#lt;/sup#$#gt;", "");

               _Affiliation = AffilName;

                AffilName = "";
                if (_FirstName.Equals(""))
                {
                    Console.WriteLine("First Name  not define in td:author tag");
                    Console.WriteLine("Please define in xml : ");
                    //Console.WriteLine(FileName);
                    Console.WriteLine("Please press any key to exit.");
                    Console.ReadLine();
                    //Environment.Exit(0);
                }
                if (_LastName.Equals(""))
                {
                    Console.WriteLine("Last Name  not define in td:author tag");
                    Console.WriteLine("Please define in xml : ");
                    //Console.WriteLine(FileName);
                    Console.WriteLine("Please press any key to exit.");
                    Console.ReadLine();
                    //Environment.Exit(0);
                }

                textWriter.WriteStartElement("Author");
                textWriter.WriteStartElement("FirstName");

                textWriter.WriteAttributeString("EmptyYN", "N");
                textWriter.WriteString(_FirstName);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("LastName");
                textWriter.WriteString(_LastName);
                textWriter.WriteEndElement();

                _FirstName = "";
                _LastName  = "";

                if (!sufix.Equals(""))
                {
                    textWriter.WriteStartElement("Suffix");
                    textWriter.WriteString(sufix);
                    textWriter.WriteEndElement();
                }

                textWriter.WriteStartElement("Affiliation");
                textWriter.WriteString(_Affiliation);
                textWriter.WriteEndElement();
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
    
    class Author
    {
        string _FirstName, _MiddleName, _LastName, _Suffix="";

        public string  FirstName  
        {
            set { _FirstName = value; }
            get { return _FirstName; }
        }
        public string  MiddleName 
        {
            set { _MiddleName = value; }
            get { return _MiddleName; }
        }
        public string  LastName   
        {
            set { _LastName = value; }
            get { return _LastName; }
        }
        public string  Suffix     
        {
            set { _Suffix = value; }
            get { return _Suffix; }
        }
    }

    class BibList
    {
        static int IdCountter = 0;
        string _JID = "";
        string _BibID = "";
        string _BibType = "";
        bool _DateAfterSource = true;
        bool _DateAfterAuthor = false;

        XmlNamespaceManager nsmgr;
        XmlNode _Node;
        List<XmlNode> _BibChilds = new List<XmlNode>();

        public bool DateAfterSource
        {
            get { return _DateAfterSource; }
            set { _DateAfterSource = value; }
        }

        public bool DateAfterAuthor
        {
            get { return _DateAfterAuthor; }
            set { _DateAfterAuthor = value; }
        }

        public BibList(XmlNode BibNode)
        {
            _Node = BibNode;
            nsmgr = new XmlNamespaceManager(_Node.OwnerDocument.NameTable);
            nsmgr.AddNamespace("td", "http://www.thomsondigital.com/xml/common/dtd");
            nsmgr.AddNamespace("sb", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsmgr.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            nsmgr.AddNamespace("tb", "http://www.thomsondigital.com/xml/common/table/dtd");
            nsmgr.AddNamespace("tp", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            IdCountter++;
        }
        public  void StartProcess()
        {
            /////////////////Do'nt change sequence of calling method
            GeBibType();

            ProcessBib();


            _BibID = "R" + IdCountter.ToString();

            if (_BibID.Equals("R20"))
            {
            }
            //_BibChilds.m
        }

        private void GeBibType()
        {

            if (_Node.SelectSingleNode(".//tp:book", nsmgr) != null)
            {
                _BibType = "BkRef";
            }
            else if (_Node.SelectSingleNode(".//td:other-ref", nsmgr) != null)
            {
                _BibType = "OrRef";
            }
            else
            {
                _BibType = "JrRef";
            }
        }

        private void ProcessBib()
        
        {

            if (_Node.InnerXml.IndexOf("The new WHO recommendations for HIV treatment") != -1)
            {
            }
            if (_BibType.Equals("OrRef"))
            {
                foreach (XmlNode chNode in _Node)
                {
                    _BibChilds.Add(chNode);
                }
                return;
            }

            XmlElement CommaEle = _Node.OwnerDocument.CreateElement("text");
            CommaEle.InnerText = ",";
            XmlElement SemiColonEle = _Node.OwnerDocument.CreateElement("text");
            SemiColonEle.InnerText = ";";
            XmlElement DotEle = _Node.OwnerDocument.CreateElement("text");
            DotEle.InnerText = ".";
            XmlElement SpaceEle = _Node.OwnerDocument.CreateElement("text");
            SpaceEle.InnerText = " ";

            List<XmlNode> TempBibChildsList = new List<XmlNode>();
            XmlNode SurName, GivenNames, Prefix, Suffix;
            XmlNodeList AuNodeList = _Node.SelectNodes(".//tp:author", nsmgr);
            XmlElement PersonGroupNode = _Node.OwnerDocument.CreateElement("person-group");
            XmlElement NameNode;
            PersonGroupNode.SetAttribute("person-group-type", "author");

            for (int j = 0; j < AuNodeList.Count; j++)
            {
                NameNode = _Node.OwnerDocument.CreateElement("name");
                SurName = AuNodeList[j].SelectSingleNode(".//td:surname", nsmgr);
                GivenNames = AuNodeList[j].SelectSingleNode(".//td:given-name", nsmgr);
                Prefix = AuNodeList[j].SelectSingleNode(".//td:initials", nsmgr);
                Suffix = AuNodeList[j].SelectSingleNode(".//td:suffix", nsmgr);

                if (SurName != null)
                {
                    NameNode.AppendChild(SurName.Clone());
                    SurName.ParentNode.RemoveChild(SurName);
                }
                if (GivenNames != null)
                {
                    NameNode.AppendChild(GivenNames.Clone());
                    GivenNames.ParentNode.RemoveChild(GivenNames);
                }
                if (Prefix != null)
                {
                    NameNode.AppendChild(Prefix.Clone());
                    Prefix.ParentNode.RemoveChild(Prefix);
                }
                if (Suffix != null)
                {
                    NameNode.AppendChild(Suffix.Clone());
                    Suffix.ParentNode.RemoveChild(Suffix);
                }

                AuNodeList[j].ParentNode.ReplaceChild(NameNode, AuNodeList[j]);

                if (NameNode.HasChildNodes)
                    PersonGroupNode.AppendChild(NameNode);
            }



            AuNodeList = _Node.SelectNodes(".//tp:editor", nsmgr);
            XmlElement EditorPersonGroupNode = _Node.OwnerDocument.CreateElement("person-group");
            EditorPersonGroupNode.SetAttribute("person-group-type", "editor");

            for (int j = 0; j < AuNodeList.Count; j++)
            {
                NameNode = _Node.OwnerDocument.CreateElement("name");

                SurName = AuNodeList[j].SelectSingleNode(".//td:surname", nsmgr);
                GivenNames = AuNodeList[j].SelectSingleNode(".//td:given-name", nsmgr);
                Prefix = AuNodeList[j].SelectSingleNode(".//td:initials", nsmgr);
                Suffix = AuNodeList[j].SelectSingleNode(".//td:suffix", nsmgr);

                if (SurName != null)
                {
                    NameNode.AppendChild(SurName.Clone());
                    SurName.ParentNode.RemoveChild(SurName);
                }
                if (GivenNames != null)
                {
                    NameNode.AppendChild(GivenNames.Clone());
                    GivenNames.ParentNode.RemoveChild(GivenNames);
                }
                if (Prefix != null)
                {
                    NameNode.AppendChild(Prefix.Clone());
                    Prefix.ParentNode.RemoveChild(Prefix);
                }
                if (Suffix != null)
                {
                    NameNode.AppendChild(Suffix.Clone());
                    Suffix.ParentNode.RemoveChild(Suffix);
                }

                AuNodeList[j].ParentNode.ReplaceChild(NameNode, AuNodeList[j]);
                if (NameNode.HasChildNodes)
                    EditorPersonGroupNode.AppendChild(NameNode);
            }

            XmlNode EtalNode = _Node.SelectSingleNode(".//tp:et-al", nsmgr);
            if (EtalNode != null)
            {
                if (EditorPersonGroupNode.HasChildNodes)
                    EditorPersonGroupNode.AppendChild(EtalNode);
                else
                    PersonGroupNode.AppendChild(EtalNode);
            }

            XmlNodeList NodeList = _Node.SelectNodes(".//tp:maintitle", nsmgr);
            XmlElement tempEle;
        
            if (NodeList.Count == 2)
            {
                tempEle = _Node.OwnerDocument.CreateElement("article-title");
                tempEle.InnerXml = NodeList[0].InnerXml;
                NodeList[0].ParentNode.ReplaceChild(tempEle, NodeList[0]);

                tempEle = _Node.OwnerDocument.CreateElement("source");
                tempEle.InnerXml = NodeList[1].InnerXml;
                NodeList[1].ParentNode.ReplaceChild(tempEle, NodeList[1]);
            }
            else if (NodeList.Count == 1)
            {
                tempEle = _Node.OwnerDocument.CreateElement("article-title");
                tempEle.InnerXml = NodeList[0].InnerXml;
                NodeList[0].ParentNode.ReplaceChild(tempEle, NodeList[0]);
            }
            XmlNodeList NL1 = _Node.SelectNodes(".//text()");
            foreach (XmlNode chNode in NL1)
            {
                if ("td:sub|td:sup|td:bold|td:italic|td:monospace|td:sans-serif|td:small-caps|td:inter-ref".IndexOf(chNode.ParentNode.Name) != -1)
                {
                    string tempSTR = "STAGLT" + chNode.ParentNode.Name + "STAGGT" + chNode.ParentNode.InnerXml + "ETAGLT" + chNode.ParentNode.Name + "ETAGGT";
                    _Node.InnerXml = _Node.InnerXml.Replace(chNode.ParentNode.OuterXml, tempSTR);
                }
            }

            NL1 = _Node.SelectNodes(".//text()");
            foreach (XmlNode chNode in NL1)
            {
                if (chNode.ParentNode.Name.Equals("td:label"))
                {
                    TempBibChildsList.Add(chNode.ParentNode);

                    if (PersonGroupNode.HasChildNodes)
                        TempBibChildsList.Add(PersonGroupNode);
                    if (EditorPersonGroupNode.HasChildNodes)
                    {
                         tempEle = _Node.OwnerDocument.CreateElement("text");
                         tempEle.InnerXml = " In : ";
                         EditorPersonGroupNode.PrependChild(tempEle);

                       

                         tempEle = _Node.OwnerDocument.CreateElement("text");
                         tempEle.InnerXml = " eds. ";
                         EditorPersonGroupNode.AppendChild(tempEle);


                         TempBibChildsList.Add(EditorPersonGroupNode);
                    }
                }
                else if (chNode.NodeType == XmlNodeType.Whitespace)
                { 
                }
                else
                    TempBibChildsList.Add(chNode.ParentNode);

            }
            int Publisher = 0;
            int PublisherLoc = 0;

            if (TempBibChildsList.Count > 0)
            {
                int AuthPos = 0;
                int DatePos = 0;
                int SourcePos = 0;
                int Counter = -1;

                if (TempBibChildsList[TempBibChildsList.Count - 1].Name.Equals("tp:location"))
                {
                    foreach (XmlNode node in TempBibChildsList)
                    {
                        Counter++;
                        if (node.Name.IndexOf("date") != -1)
                            DatePos = Counter;
                    }
                    if (DatePos > 0)
                    {
                        TempBibChildsList.Insert(TempBibChildsList.Count, TempBibChildsList[DatePos]);
                        TempBibChildsList.RemoveAt(DatePos);
                    }
                }

                DatePos = 0;
                Counter = -1;
                foreach (XmlNode node in TempBibChildsList)
                {
                    Counter++;
                    if (node.Name.Equals("source"))
                        SourcePos = Counter;

                    if (node.Name.IndexOf("date") != -1)
                        DatePos = Counter;

                    if (node.Name.Equals("person-group"))
                        AuthPos = Counter;

                    if (node.Name.Equals("tp:name"))
                        Publisher = Counter;

                    if (node.Name.Equals("tp:location"))
                        PublisherLoc = Counter;


                    if (node.Name.Equals("tp:edition"))
                    {
                        SourcePos = Counter;
                    }

                    if (_Node.InnerXml.IndexOf("tp:edited-book") != -1)
                    {
                        _BibType = "BkRef";
                    }

                }

                if (_DateAfterSource && !_BibType.Equals("BkRef"))
                {
                    if (DatePos > 0)
                    {
                        TempBibChildsList.Insert(SourcePos + 1, TempBibChildsList[DatePos]);
                        if (SourcePos > DatePos)
                            TempBibChildsList.RemoveAt(DatePos);
                        else
                            if (TempBibChildsList.Count > DatePos + 1)
                                TempBibChildsList.RemoveAt(DatePos + 1);
                    }
                }
                else if (_DateAfterAuthor && !_BibType.Equals("BkRef"))
                {
                    if (DatePos > 0)
                    {
                        if (TempBibChildsList[0].Name.Equals("td:label"))
                        {
                            //TempBibChildsList.Insert(1, PersonGroupNode);
                            TempBibChildsList.Insert(AuthPos + 1, TempBibChildsList[DatePos]);
                            TempBibChildsList.RemoveAt(DatePos + 1);
                        }
                    }
                }
                else
                {
                    if (JID.Equals("AGR") && _BibType.Equals("BkRef"))
                    { 
                        if (DatePos > 0)
                        {
                            if (TempBibChildsList.Count >= DatePos + 1)
                            {
                                TempBibChildsList.Insert(2, TempBibChildsList[DatePos]);
                                TempBibChildsList.RemoveAt(DatePos + 1);


                                XmlNode ATNode = TempBibChildsList.Find(x => x.Name.Equals("article-title"));
                                if (ATNode != null)
                                {
                                    int Pos = TempBibChildsList.FindIndex(x => x.Name.Equals("article-title"));
                                    TempBibChildsList.Insert(3,ATNode);
                                    TempBibChildsList.RemoveAt(Pos+1);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
            }

          
                if (Publisher > 0)
                {
                    Publisher = TempBibChildsList.FindIndex(x => x.Name.Equals("tp:name"));
                    PublisherLoc = TempBibChildsList.FindIndex(x => x.Name.Equals("tp:location"));
                   int SourcePos =  TempBibChildsList.FindIndex(x => x.Name.Equals("source"));

                    if (JID.Equals("AGR") && SourcePos>0 ) 
                    {
                       TempBibChildsList[SourcePos].InnerXml = "&lt;i&gt;" +  TempBibChildsList[SourcePos].InnerXml+ ".&lt;/i&gt;";;
                    }

                    TempBibChildsList.Insert(PublisherLoc + 1, TempBibChildsList[Publisher]);

                    if (PublisherLoc > Publisher)
                        TempBibChildsList.RemoveAt(Publisher);
                    else
                        TempBibChildsList.RemoveAt(Publisher + 1);
                }
                foreach (XmlNode node in TempBibChildsList)
                {
                    node.InnerXml = node.InnerXml.Replace("STAGLT", "<").Replace("STAGGT", ">").Replace("ETAGLT", "</").Replace("ETAGGT", ">").Replace("<td:small-caps>","<as>").Replace("</td:small-caps>","</as>");
                }
                _BibChilds = TempBibChildsList;
        }

        public List<XmlNode> BibChilds
        {
            get { return _BibChilds; }
        }

        public XmlNode BibNode
        {
            get { return _Node; }
        }

        public string BibType
        {
            get { return _BibType; }
        }

        public string BibID
        {
            get { return _BibID; }
        }

        public string JID
        {
            get { return _JID; }
            set { _JID = value; ; }
        }
    }
}

