using System;
using System.Diagnostics;
using System.Text.RegularExpressions ;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Text;

namespace NLMXML
{
    class Program
    {
        public static string NLMDTDPath;
        public static string PUBMEDPath;

        XmlDocument MyXmlDocument = new XmlDocument();
        XmlReaderSettings ReaderSettings = new XmlReaderSettings();

       

        private static void BrowseNXML(string args)
        {
            string tempNXMlPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TempN.xml";
            File.Copy(args, tempNXMlPath, true);
            Xml2NLM Xml2NLMObj = new Xml2NLM();
            Xml2NLMObj._NLMFileName = tempNXMlPath;
            Xml2NLMObj.XmlParsing(tempNXMlPath);
        }
        
        private void MsgDisplay()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadLine();
        }
        private static void StartupMsg()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine();
            Console.WriteLine("                 Thomson Electronic Editing System(TEES) Ver. 1.0              ");
            Console.WriteLine();
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                                     Developed by:             ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                                     Software Development Team ");
            Console.WriteLine("                                                     Thomson Digital           ");
            Console.WriteLine("                                                     NSEZ Noida                ");
            Console.WriteLine("_______________________________________________________________________________");
            Console.WriteLine();
        }

        public static  bool ProcessStart(string XMLFilePath, JLEDATASET.Xml2HTML  Xml2HTMLObj )
        {
            
            XmlInfo  XmlInfoObj = new XmlInfo(XMLFilePath);

            XmlInfoObj.IsParsingRequired = true;
            XmlInfoObj.PreserveSpace = true;

            if (XmlInfoObj.LoadXml())
            {
                ////////////////Conevrt xml to NLM
                if (XmlInfoObj.MakeNLM(Xml2HTMLObj ) == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public static bool CheckFolderExistence()
        {

            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            PUBMEDPath  = ExeLoc + "\\DTD\\PUBMED";

            if (!Directory.Exists(PUBMEDPath))
            {
                Console.WriteLine(PUBMEDPath + " does not exist");
                return false;
            }

            NLMDTDPath  = ExeLoc + "\\DTD\\archive-2.3";
            if (!Directory.Exists(NLMDTDPath))
            {
                Console.WriteLine(NLMDTDPath + " does not exist");
                return false;
            }

            string TDMLDTDPath = ExeLoc + "\\DTD\\TDML";
            if (!Directory.Exists(TDMLDTDPath))
            {
                Console.WriteLine(TDMLDTDPath + " does not exist");
                return false;
            }

            string BrowserPath = ExeLoc + "\\browser";
            if (!Directory.Exists(BrowserPath))
            {
                Console.WriteLine(BrowserPath + " does not exist");
                return false;
            }

            string ValidationPath = ExeLoc + "\\validation";
            if (!Directory.Exists(ValidationPath))
            {
                Console.WriteLine(ValidationPath + " does not exist");
                return false;
            }
            else 
            {
                WriteParsingBatchFile(ValidationPath);
            }

            string EntityFilePath = ExeLoc + "\\HexEntities.txt";
            if (!File.Exists(EntityFilePath))
            {
                Console.WriteLine(EntityFilePath + " does not exist");
                return false;
            }
            string eps2jpg = ExeLoc + "\\eps2jpg.bat";
            if (!File.Exists(EntityFilePath))
            {
                Console.WriteLine(eps2jpg + " does not exist");
                return false;
            }
            
            string MapFilePath = ExeLoc + "\\Map.txt";
            if (!File.Exists(MapFilePath))
            {
                Console.WriteLine(MapFilePath + " does not exist");
                return false;
            }

            string SecTitle = ExeLoc + "\\SecTitle.txt";
            if (!File.Exists(SecTitle))
            {
                Console.WriteLine(SecTitle + " does not exist");
                return false;
            }

            string ArticleType = ExeLoc + "\\ArticleType.txt";
            if (!File.Exists(ArticleType))
            {
                Console.WriteLine(ArticleType + " does not exist");
                return false;
            }

            string MetaData = ExeLoc + "\\MetaData.xml";
            if (!File.Exists(MetaData))
            {
                Console.WriteLine(MetaData + " does not exist");
                return false;
            }
            string Pubmed = ExeLoc + "\\Pubmed.txt";
            if (!File.Exists(Pubmed))
            {
                Console.WriteLine(Pubmed + " does not exist");
                return false;
            }


            string mEDRAJID = ExeLoc + "\\mEDRAJID.txt";
            if (!File.Exists(mEDRAJID))
            {
                Console.WriteLine(mEDRAJID + " does not exist");
                return false;
            }
            

            return true;
        }
        private static void  WriteParsingBatchFile(string ValidationPath)
        {
            StringBuilder BathStr = new StringBuilder("");
            BathStr.AppendLine("@SET SGML_CATALOG_FILES=" + ValidationPath + "\\xml.soc");
            BathStr.AppendLine("@SET SP_CHARSET_FIXED=YES");
            BathStr.AppendLine("@SET SP_ENCODING=XML");
            BathStr.AppendLine("@" + ValidationPath + "\\nsgmls -sgf %1.err -wxml %1");

            File.WriteAllText(ValidationPath + "\\Parse.bat", BathStr.ToString());

            string socStr = "SGMLDECL \"" + ValidationPath + "\\xml.dcl\"";

            File.WriteAllText(ValidationPath + "\\xml.soc", socStr);
        }
    }
    
    class Xml2NLM
    {
        int InlineFigure = 0;
        bool ce_author_group = false;


        JLEDATASET.Xml2HTML Xml2HTMLObj = new JLEDATASET.Xml2HTML();
        public string sPage
        {
            get;
            set;
        }

        public string lPage
        {
            get;
            set;
        }

        public string PubDate
        {
            get;
            set;
        }

        public bool isFree
        {
            get;
            set;
        }
        public string JLELibParu
        {
            get;
            set;
        }

        public string JLELibSomm
        {
            get;
            set;
        }

        string Doi = "";
        string NLMFileName = "";
        string JID, AID = "";
        
        XmlNodeList ARTFootNode;

        string ValidationPath = "";

        XmlNamespaceManager nsmgr;
        XmlDocument         MyXmlDocument;
        XmlTextWriter       textWriter;

        XmlNode CopyrightNode;
        XmlNode FloatNode;
        XmlNode ItemInfoNode;

        int MathID          = 0;

        /// All static variable------Information store in these variables does not change
        static StringDictionary Mapping = new StringDictionary();
        //static StringDictionary Entity  = new StringDictionary();
        static Dictionary<string, string> Entity = new Dictionary<string, string>();
        static string[] TitleArr = { "" };
        static string ExeLoc = "";
        
        /// </summary>
        

        public Xml2NLM()
        {
            ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

          //NLMDTDPath     = ExeLoc + "\\DTD\\TDML";
          //TDMLDTDPath    = ExeLoc + "\\DTD\\archive-2.3";
          //BrowserPath    = ExeLoc + "\\browser";

            ValidationPath = ExeLoc + "\\validation";
        }


        public Xml2NLM(XmlDocument xmlDocument)
            : this()
        {
            MyXmlDocument = xmlDocument;
            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
        }
        public Xml2NLM(XmlDocument xmlDocument, string NLMFileName)
            : this()
        {
            MyXmlDocument = xmlDocument;
            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            this.NLMFileName = NLMFileName;
        }

        public string _NLMFileName
        {
            get
            {
                return NLMFileName;
            }
            set
            {
                NLMFileName = value;
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
        protected internal void MakeXNLM()
        {
            if (NLMFileName.Equals(""))
            {
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(NLMFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(NLMFileName));
            }
            try
            {
                textWriter = new XmlTextWriter(NLMFileName, null);
            }
            catch
            {
            }
            textWriter.Indentation = 4;
            textWriter.IndentChar = '\t';

            textWriter.WriteStartDocument();
          //textWriter.WriteProcessingInstruction("xml-stylesheet", "open_access");
            textWriter.WriteDocType("article", "-//NLM//DTD Journal Archiving and Interchange DTD v2.3 20080202//EN", ExeLoc + @"\DTD\archive-2.3\archivearticle.dtd", "");

            textWriter.WriteStartElement("article");

            textWriter.WriteAttributeString("xmlns:xlink", "http://www.w3.org/1999/xlink");
            textWriter.WriteAttributeString("xmlns:mml", "http://www.w3.org/1998/Math/MathML");


            //******************Need to be changed***************************\\
            //docsubtype="fla"
            //textWriter.WriteAttributeString("article-type", "research-article");
            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("docsubtype") != null)
            {
                string AtrVal = MyXmlDocument.DocumentElement.Attributes.GetNamedItem("docsubtype").Value;
                if (Mapping[AtrVal] != null)
                    textWriter.WriteAttributeString("article-type", Mapping[AtrVal]);
            }
            //*********************************************\\
            //textWriter.WriteProcessingInstruction("properties", "open_access");
            //textWriter.WriteProcessingInstruction("DTDIdentifier.IdentifierValue", "-//NLM//DTD Journal Publishing DTD v2.3 20070202//EN");
            //textWriter.WriteProcessingInstruction("DTDIdentifier.IdentifierType", "public");
            //textWriter.WriteProcessingInstruction("SourceDTD.DTDName", "journalpublishing.dtd");
            textWriter.WriteProcessingInstruction("SourceDTD.Version", "2.3");
            //textWriter.WriteProcessingInstruction("ConverterInfo.XSLTName", "jp2nlmx2.xsl");
            //textWriter.WriteProcessingInstruction("ConverterInfo.Version", "2");

            FilterToNLM();

            SearchNode(MyXmlDocument.DocumentElement);


            textWriter.WriteEndDocument();
            textWriter.Close();

            StringBuilder NLMStr = new StringBuilder(File.ReadAllText(NLMFileName));

            /////////////////////////////////////////////////**Delete file**\\\\\\\\\\\\\\\\\\\\\\\\
            File.Delete(NLMFileName);
            /////////////////////////////////////////////////****\\\\\\\\\\\\\\\\\\\\\\\\

            ///////////To need check.
            NLMStr.Replace("#$#", "&");
            NLMStr.Replace("#$#", "&");
            NLMStr.Replace("  ", " ");
            NLMStr.Replace("  ", " ");
            NLMStr.Replace("  ", " ");
            NLMStr.Replace("</history><history>", "");
            NLMStr.Replace("</caption><caption>", "");
            NLMStr.Replace("</author-notes><author-notes>", "");
            NLMStr.Replace("</table-wrap-foot><table-wrap-foot>", "");
            NLMStr.Replace("</fn-group><fn-group>", "");

            NLMStr.Replace("<app>", "<app-group><app>");
            NLMStr.Replace("</app>", "</app></app-group>");
            NLMStr.Replace("</app-group><app-group>", "");
            NLMStr.Replace("</back><back>", "");
            NLMStr.Replace("</def-list><def-list>", "");
            NLMStr.Replace("<text>", "");
            NLMStr.Replace("</text>", "");

            //string strPattern = "(<td:acknowledg)(.)(</body>)";
            //Regex Reg = new Regex(strPattern);

            string strTemp = NLMStr.ToString();

            //Jitender 1-10-2012

            int backPos = strTemp.IndexOf("<back>");
            int ackPos  = strTemp.IndexOf("<ack>");

            if (backPos == -1 && ackPos != -1)
            {
                NLMStr.Replace("</body>", "</body><back></back>");
                strTemp = NLMStr.ToString();
            }


            int ePos = strTemp.IndexOf("</ack></body><back>");
            if (ePos != -1)
            {
                int sPos = strTemp.LastIndexOf("<ack>");
                if (sPos != -1)
                {
                    if (sPos < ePos)
                    {
                        string TempS = strTemp.Substring(sPos, ePos - sPos + "</ack></body><back>".Length);
                        NLMStr.Replace(TempS, "</body><back>" + TempS.Replace("</body>", "").Replace("<back>", ""));
                    }
                }
            }
            

            string JLELibParuStr = "<custom-meta><meta-name>Date</meta-name><meta-value>" + JLELibParu + "</meta-value></custom-meta>";
            string CustomMetaStr = @"<custom-meta-wrap><custom-meta><meta-name>Order of the article</meta-name><meta-value>1</meta-value></custom-meta>" + JLELibParuStr + "</custom-meta-wrap>";
            NLMStr.Replace("</article-meta>",CustomMetaStr + "</article-meta>");


            NLMFileName = Path.GetDirectoryName(NLMFileName) + "\\" + JID + AID + ".nxml";

            NLMStr = new StringBuilder(ReplaceEntity(NLMStr).ToString());
            File.WriteAllText(NLMFileName, NLMStr.ToString());

            if (XmlParsing(NLMFileName))
            {
                if (isFree == false)
                    return;
                
                ///////////To need check.
                NLMStr.Replace( "&", "#$#");

                MyXmlDocument.XmlResolver = null;
                MyXmlDocument.LoadXml(NLMStr.ToString());

                XmlNode SrchNode= null;

                XmlNodeList HistoryNL = MyXmlDocument.GetElementsByTagName("history");
                XmlNodeList LpageNL = MyXmlDocument.GetElementsByTagName("lpage");


                if (HistoryNL.Count > 0)
                {
                    SrchNode= HistoryNL[0];
                }
                else if (LpageNL.Count > 0)
                {
                    SrchNode = LpageNL[0];
                }

                //<copyright-statement>Take from XML</copyright-statement>
                //<copyright-year>Take from XML</copyright-year>
                //<license license-type="open-access" xlink:href="http://www.jle.com/en/index.phtml"><p>Article gratuit.</p></license>        
                XmlElement PermissionsNode = MyXmlDocument.CreateElement("permissions");

                if (CopyrightNode!= null)
                {
                    if (CopyrightNode.HasChildNodes)
                    {
                        XmlElement copyrightstatement = MyXmlDocument.CreateElement("copyright-statement");
                        copyrightstatement.InnerText = CopyrightNode.InnerText;
                        PermissionsNode.AppendChild(copyrightstatement);
                    }
                    if (CopyrightNode.Attributes.GetNamedItem("year") != null)
                   {
                       if (!string.IsNullOrEmpty(CopyrightNode.Attributes.GetNamedItem("year").Value))
                       {
                           XmlElement copyrightyear = MyXmlDocument.CreateElement("copyright-year");
                           copyrightyear.InnerText = CopyrightNode.Attributes.GetNamedItem("year").Value;
                          PermissionsNode.AppendChild(copyrightyear);
                       }
                   }
                }
                XmlElement license = MyXmlDocument.CreateElement("license");
                license.SetAttribute("license-type","open-access");
                license.SetAttribute("xlink:href",  "http://www.jle.com/en/index.phtml");
                license.InnerXml="<p>Article gratuit.</p>";
                PermissionsNode.AppendChild(license);
                if (SrchNode!= null)
                {
                    SrchNode.ParentNode.InsertAfter(PermissionsNode, SrchNode);

                    NLMStr = new StringBuilder(MyXmlDocument.OuterXml);
                    NLMStr.Replace("#$#", "&");

                    NLMStr.Replace("license-type=\"open-access\" href", "license-type=\"open-access\" xlink:href");

                    NLMStr.Replace("<fpage></fpage><lpage></lpage>", "");
                    File.WriteAllText(NLMFileName, NLMStr.ToString());

                    XmlParsing(NLMFileName);
                }
                
            }


        }

        private StringBuilder ReplaceEntity(StringBuilder XmlStr)
        {
                Regex reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                string XmlEntity = "";
                while (true)
                {
                    XmlEntity = Regex.Match(XmlStr.ToString(), @"&[a-zA-Z\.0-9]{1,}\;").Value;
                    if (XmlEntity.Equals(""))
                        break;
                    else
                    {
                        if (Entity.ContainsKey(XmlEntity))
                            XmlStr.Replace(XmlEntity, Entity[XmlEntity]);
                        else
                        {
                            Console.WriteLine("********************************************");
                            Console.WriteLine(XmlEntity + " entity is not define");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadLine();
                            break;
                        }
                    }
                }

                MatchCollection mch = reg.Matches(XmlStr.ToString());
                reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                mch = reg.Matches(XmlStr.ToString());
                for (int i = 0; i < mch.Count; i++)
                {
                    if (Entity.ContainsKey(mch[i].Value))
                    {
                        XmlStr.Replace(mch[i].Value, Entity[mch[i].Value]);
                    }
                    else
                    {
                        Console.WriteLine("HTML entity could not found for this xml entity " + mch[i].Value);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                    }
                }
            return XmlStr;
        }
        private void SearchNode(XmlNode node)
        {
            if (node.HasChildNodes)
            {
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
                    else if (nodeList[i].NodeType == XmlNodeType.Comment) { }
                    else if (nodeList[i].NodeType == XmlNodeType.Text)
                    {
                        if (nodeList[i].InnerText.StartsWith("The first national"))
                        {
                        }

                        textWriter.WriteString(nodeList[i].InnerText);
                    }
                    else
                        textWriter.WriteString(nodeList[i].InnerText);

                  
                }
               
                if (endElement)
                {
                    textWriter.WriteEndElement();
                    /*if (node.Name == "ce:source")
                    {
                        textWriter.WriteEndElement();
                    }*/
                }
                textWriter.Flush();
            }
            else if (node.NodeType == XmlNodeType.Element && node.HasChildNodes == false)
            {

                bool endElement = false;
                if (node.Name.Equals("ce:br"))
                {
                    endElement = false;
                }

                ProcessNode(node, out endElement);
                if (endElement) textWriter.WriteEndElement();
                //Console.WriteLine(node.Name);
            }
            else if (node.NodeType == XmlNodeType.Text)
            {

                if (node.InnerText.StartsWith("The first national"))
                {
                }
                textWriter.WriteString(node.InnerText);
            }
            else
            {
                if (node.Value.IndexOf("<query") != -1)
                {
                    node.ParentNode.RemoveChild(node);
                }
                else if (node.NodeType == XmlNodeType.Comment)
                {
                    //add by 27 feb
                    if (node.Value.IndexOf("<RunningTitle>") != -1)
                    {
                    }
                    else
                    {
                        textWriter.WriteRaw(node.Value);
                    }
                }
                else
                {
                    textWriter.WriteRaw(node.OuterXml);
                    Console.WriteLine("Check Node:::" + node.OuterXml);
                    Console.ReadLine();
                }
            }



        }
        private void ProcessNode(XmlNode node, out bool endElement)
        {

            endElement = true;

            if (node.NodeType == XmlNodeType.Element)
            {
                if (node.Name.Equals("mml:math"))
                {
                    if (node.ParentNode.Name.Equals("ce:formula"))
                    {
                        DefaultMath(node);
                    }
                    else
                    {
                        textWriter.WriteStartElement("inline-formula");
                        DefaultMath(node);
                        textWriter.WriteEndElement();
                    }
                }
                else if (node.Name.Equals("mml:"))
                {
                    DefaultMath(node);
                }
                else if (Mapping.ContainsKey(node.Name))
                {

                    textWriter.WriteStartElement(Mapping[node.Name]);
                }
                else
                {
                    switch (node.Name)
                    {
                        case "aid":
                            {
                                AID = node.InnerText;
                                endElement = false;
                                break;
                            }
                        case "RunningTitle":
                            {
                                endElement = false;
                                break;
                            }
                        case "article":
                            {
                                endElement = false;
                                break;
                            }
                            
                        case "body":
                            {
                                textWriter.WriteStartElement("body");
                                break;
                            }
                            //Munesh
                        case "book-review":
                            {
                                //textWriter.WriteStartElement("book-review");
                                endElement = false;
                                break;
                            }
                        case "book-review-head":
                            {
                                MakeFront(node);
                                endElement = false;
                                break;
                            }
                        case "ce:city":
                            {
                                textWriter.WriteStartElement("addr-line");
                                break;
                            }
                        case "ce:state":
                            {
                                textWriter.WriteStartElement("addr-line");
                                break;
                            }
                        case "ce:e-component ":
                            {
                                textWriter.WriteStartElement("media");
                                break;
                            }
                        case "ce:organization":
                            {
                                textWriter.WriteStartElement("institution");
                                break;
                            }  
                            
                        case "ce:abstract":
                            {
                                textWriter.WriteStartElement("abstract");
                                if (node.Attributes.GetNamedItem("xml:lang") != null)
                                {
                                    string Lang = node.Attributes.GetNamedItem("xml:lang").Value;
                                    textWriter.WriteAttributeString("xml:lang", Lang);
                                }
                                break;
                            }
                        case "ce:abstract-sec":
                            {
                                if (node.ParentNode.Name.Equals("ce:abstract"))
                                {
                                    textWriter.WriteStartElement("sec");
                                }
                                else
                                {
                                    Default(node);

                                }
                                break;
                            }
                        case "ce:acknowledgment":
                            {
                                textWriter.WriteStartElement("ack");
                                break;
                            }
                        case "ce:affiliation":
                            {
                                //if (node.Attributes.Count == 0)
                                //{
                                //    node.RemoveAll();
                                //    endElement = false;
                                //    break;
                                //}
                                textWriter.WriteStartElement("aff");
                                DefaultAtrbut(node);

                                break;
                            }
                        //case "ce:alt-e-component":
                        //    {
                        //        break;
                        //    }
                        case "ce:alt-subtitle":
                            {
                                textWriter.WriteStartElement("alt-title");
                                break;
                            }
                        case "ce:alt-title":
                            {
                                textWriter.WriteStartElement("alt-title");

                                if (node.Attributes.GetNamedItem("xml:lang")!=null)
                                {
                                    string Lang = node.Attributes.GetNamedItem("xml:lang").Value;
                                   //textWriter.WriteAttributeString("xml:lang", Lang);
                                }
                                 break;
                            }
                        //case "ce:float":
                        //    {
                        //        break;
                        //    }
                        case "ce:appendices":
                            {
                                textWriter.WriteStartElement("app");
                                break;
                            }
                        case "ce:article-footnote":
                            {
                                textWriter.WriteStartElement("fn");
                                break;
                                if (ce_author_group)
                                {
                                    textWriter.WriteStartElement("fn-group");
                                }
                                else
                                {
                                    endElement = false;
                                    //Default(node);
                                }
                                break;
                            }
                        case "ce:author":
                            {
                                if (node.ParentNode.Name.Equals("ce:author-group"))
                                {
                                    textWriter.WriteStartElement("contrib");
                                    textWriter.WriteAttributeString("contrib-type", "author");
                                    if (node.NextSibling != null)
                                    {
                                        if (node.NextSibling.Name.Equals("ce:collaboration"))
                                        {
                                            if (node.SelectSingleNode(".//name") != null)
                                            {
                                                XmlNodeList tmpNodeList = node.SelectNodes(".//name");
                                                node.InsertAfter(node.NextSibling, tmpNodeList[tmpNodeList.Count - 1]);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Default(node);
                                }
                                if (node.LastChild.Name.Equals("ce:link"))
                                {
                                    node.ParentNode.InsertAfter(node.LastChild, node);
                                }
                                break;
                            }
                        case "ce:author-group":
                            {
                                if (ce_author_group)
                                {
                                    textWriter.WriteEndElement();
                                    ce_author_group = false;
                                }
                                textWriter.WriteStartElement("contrib-group");


                                if (node.FirstChild != null && node.FirstChild.Name.Equals("ce:collaboration"))
                                {
                                    XmlElement ele = xmlDocument.CreateElement("contrib");
                                    ele.InnerXml = node.FirstChild.OuterXml;
                                    node.ReplaceChild(ele, node.FirstChild);
                                }
                                //foreach (XmlNode chNode in ARTFootNode)
                                //{
                                //    ce_author_group = true;
                                //    SearchNode(chNode);
                                //}

                                //if (ce_author_group == true)
                                //{
                                //    ce_author_group = false;
                                //    textWriter.WriteEndElement();
                                //    //////////////Close title group
                                //}
                                //endElement = false;
                                //ProcessAuthorGroup(node);
                                //node.RemoveAll();
                                break;
                            }
                        case "ce:bib-reference":
                            {
                                textWriter.WriteStartElement("ref");
                                DefaultAtrbut(node);
                                break;
                            }
                        case "ce:bibliography":
                            {
                                if (node.SelectSingleNode(".//ce:bibliography-sec/ce:section-title", nsmgr) != null)
                                {
                                    textWriter.WriteStartElement("sec");

                                }
                                else
                                    textWriter.WriteStartElement("ref-list");
                                break;
                            }
                        case "ce:bibliography-sec":
                            {
                                if (node.PreviousSibling.Name.Equals("ce:bibliography-sec"))
                                {
                                    textWriter.WriteStartElement("ref-list");
                                }
                                else if (node.FirstChild.Name.Equals("ce:section-title"))
                                {
                                    textWriter.WriteStartElement("sec");
                                    SearchNode(node.FirstChild);
                                    node.RemoveChild(node.FirstChild);
                                    textWriter.WriteStartElement("ref-list");
                                    foreach (XmlNode chnode in node)
                                    {
                                        SearchNode(chnode);
                                    }
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();
                                    endElement = false;
                                }
                                else
                                {
                                    textWriter.WriteStartElement("ref-list");
                                }
                                break;
                            }
                        case "ce:biography":
                            {
                                textWriter.WriteStartElement("bio");
                                break;
                            }
                        //case "ce:bold":
                        //    {
                        //        break;
                        //    }
                        case "ce:br":
                            {
                                textWriter.WriteStartElement("break");
                                break;
                            }
                        case "ce:caption":
                            {
                                if (node.ParentNode.Name.Equals("ce:textbox"))
                                {
                                    textWriter.WriteStartElement("title");
                                }
                                else
                                {
                                    textWriter.WriteStartElement("caption");
                                }
                                break;
                            }
                        case "ce:chem":
                            {
                                textWriter.WriteStartElement("chem-struct");
                                break;
                            }
                        //case "ce:collab-aff":
                        //    {
                        //        break;
                        //    }
                        case "ce:collaboration":
                            {
                                textWriter.WriteStartElement("collab");
                                break;
                            }
                        //case "ce:compound-formula":
                        //    {
                        //        break;
                        //    }
                        //case "ce:compound-info":
                        //    {
                        //        break;
                        //    }
                        //case "ce:compound-name":
                        //    {
                        //        break;
                        //    }
                        //case "ce:compound-struct":
                        //    {
                        //        break;
                        //    }
                        //case "ce:copyright":
                        //    {
                        //        break;
                        //    }
                        //case "ce:copyright-line":
                        //    {
                        //        break;
                        //    }
                        case "ce:correspondence":
                            {
                                //textWriter.WriteStartElement("author-notes");
                                textWriter.WriteStartElement("corresp");

                                if (node.Attributes.GetNamedItem("id") != null)
                                {
                                    textWriter.WriteAttributeString("id", node.Attributes.GetNamedItem("id").Value);
                                }
                                if (node.NextSibling != null)
                                {
                                    if (node.ParentNode.SelectSingleNode(".//ce:footnote", nsmgr) != null)
                                    {
                                        XmlNode FootNote = node.ParentNode.SelectSingleNode(".//ce:footnote", nsmgr);
                                        node.AppendChild(FootNote);
                                    }
                                }
                                break;
                            }
                        case "ce:cross-out":
                            {
                                textWriter.WriteStartElement("strike");
                                break;
                            }
                        case "ce:cross-ref":
                            {
                                textWriter.WriteStartElement("xref");
                                WriteTargetAttribute(node);
                                //xref ref-type="aff" rid="au1"
                                break;
                            }
                        case "ce:cross-refs":
                            {
                                textWriter.WriteStartElement("xref");
                                WriteTargetAttribute(node);
                                break;
                            }
                        case "ce:date-accepted":
                            {
                                //<history>
                                //<date date-type="accepted">
                                //<day>29</day>
                                //<month>01</month>
                                //<year>1999</year></date>
                                //</history>
                                //<ce:date-accepted day="11" month="11" year="2010"/>

                                textWriter.WriteStartElement("history");

                                textWriter.WriteStartElement("date");
                                textWriter.WriteAttributeString("date-type", "accepted");

                                if (node.Attributes.GetNamedItem("day") != null)
                                {
                                    textWriter.WriteStartElement("day");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("day").Value);
                                    textWriter.WriteEndElement();

                                }
                                if (node.Attributes.GetNamedItem("month") != null)
                                {
                                    textWriter.WriteStartElement("month");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("month").Value);
                                    textWriter.WriteEndElement();
                                }
                                if (node.Attributes.GetNamedItem("year") != null)
                                {
                                    textWriter.WriteStartElement("year");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("year").Value);
                                    textWriter.WriteEndElement();
                                }

                                textWriter.WriteEndElement();//////////Date Close
                                textWriter.WriteEndElement();//////////History Close

                                endElement = false;
                                break;
                            }
                        case "ce:date-received":
                            {
                                textWriter.WriteStartElement("history");

                                textWriter.WriteStartElement("date");
                                textWriter.WriteAttributeString("date-type", "received");

                                if (node.Attributes.GetNamedItem("day") != null)
                                {
                                    textWriter.WriteStartElement("day");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("day").Value);
                                    textWriter.WriteEndElement();

                                }
                                if (node.Attributes.GetNamedItem("month") != null)
                                {
                                    textWriter.WriteStartElement("month");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("month").Value);
                                    textWriter.WriteEndElement();
                                }
                                if (node.Attributes.GetNamedItem("year") != null)
                                {
                                    textWriter.WriteStartElement("year");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("year").Value);
                                    textWriter.WriteEndElement();
                                }

                                textWriter.WriteEndElement();//////////Date Close
                                textWriter.WriteEndElement();//////////History Close

                                endElement = false;
                                break;
                            }
                        case "ce:date-revised":
                            {
                                textWriter.WriteStartElement("history");

                                textWriter.WriteStartElement("date");
                                textWriter.WriteAttributeString("date-type", "rev-recd");

                                if (node.Attributes.GetNamedItem("day") != null)
                                {
                                    textWriter.WriteStartElement("day");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("day").Value);
                                    textWriter.WriteEndElement();

                                }
                                if (node.Attributes.GetNamedItem("month") != null)
                                {
                                    textWriter.WriteStartElement("month");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("month").Value);
                                    textWriter.WriteEndElement();
                                }
                                if (node.Attributes.GetNamedItem("year") != null)
                                {
                                    textWriter.WriteStartElement("year");
                                    textWriter.WriteString(node.Attributes.GetNamedItem("year").Value);
                                    textWriter.WriteEndElement();
                                }

                                textWriter.WriteEndElement();//////////Date Close
                                textWriter.WriteEndElement();//////////History Close

                                endElement = false;
                                break;
                            }
                        //case "ce:dedication":
                        //    {
                        //        break;
                        //    }
                        case "ce:def-description":
                            {
                                textWriter.WriteStartElement("def");
                                break;
                            }
                        case "ce:def-list":
                            {
                                textWriter.WriteStartElement("def-list");
                                break;
                            }
                        case "ce:def-term":
                            {
                                textWriter.WriteStartElement("term");
                                break;
                            }
                        case "ce:degrees":
                            {
                                textWriter.WriteStartElement("degrees");
                                break;
                            }
                        case "ce:display":
                            {
                                endElement = false;
                                break;
                            }
                        case "ce:displayed-quote":
                            {
                                textWriter.WriteStartElement("disp-quote");
                                break;
                            }
                        case "ce:dochead":
                            {

                                textWriter.WriteStartElement("article-categories");
                                textWriter.WriteStartElement("subj-group");
                                textWriter.WriteStartElement("subject");
                                textWriter.WriteString(JLELibSomm);
                                textWriter.WriteEndElement();
                                textWriter.WriteEndElement();
                                textWriter.WriteEndElement();


                                //XmlNodeList textFn = node.SelectNodes("ce:textfn", nsmgr);

                                //if (textFn.Count == 0)
                                //{
                                //    textFn = node.SelectNodes("ce:text", nsmgr);
                                //}

                                //if (textFn.Count > 0)
                                //{
                                //    textWriter.WriteStartElement("article-categories");
                                //    foreach (XmlNode chNode in textFn)
                                //    {
                                //        textWriter.WriteStartElement("subj-group");
                                //        textWriter.WriteStartElement("subject");
                                //        //textWriter.WriteString(chNode.InnerText);
                                //        textWriter.WriteString(JLELibSomm);
                                //        textWriter.WriteEndElement();
                                //        textWriter.WriteEndElement();
                                //    }
                                //    textWriter.WriteEndElement();
                                //}
                                //else
                                //{
                                //    textWriter.WriteStartElement("article-categories");
                                //    textWriter.WriteStartElement("subj-group");
                                //    textWriter.WriteStartElement("subject");
                                //    textWriter.WriteString(JLELibSomm);
                                //    textWriter.WriteEndElement();
                                //    textWriter.WriteEndElement();
                                //    textWriter.WriteEndElement();
                                //}
                                node.RemoveAll();
                                endElement = false;
                                break;
                            }
                        //case "ce:doctopic":
                        //    {
                        //        break;
                        //    }
                        //case "ce:doctopics":
                        //    {
                        //        break;
                        //    }
                        //case "ce:document-thread":
                        //    {
                        //        break;
                        //    }
                        case "ce:doi":
                            {

                                //  <object-id pub-id-type="doi">MyPub.20070215.03154.s433</object-id>

                                if (node.ParentNode.Name.Equals("citation"))
                                {
                                    textWriter.WriteStartElement("object-id");
                                    textWriter.WriteAttributeString("pub-id-type", "doi");
                                }
                                else
                                {
                                    textWriter.WriteStartElement("article-id");
                                    textWriter.WriteAttributeString("pub-id-type", "doi");
                                    Doi = node.InnerText;
                                }
                                //endElement = false;
                                break;
                            }
                        case "ce:e-address":
                            {
                                textWriter.WriteStartElement("email");

                                if (node.Attributes.GetNamedItem("id") != null)
                                {
                                    node.Attributes.Remove((XmlAttribute) node.Attributes.GetNamedItem("id"));
                                    node.Attributes.RemoveAll();
                                }

                                //DefaultAtrbut(node);

                                break;
                            }
                        case "ce:e-component":
                            {
                                textWriter.WriteStartElement("media");

                                if (node.Attributes.GetNamedItem("id")!= null)
                                {
                                    string href= node.Attributes.GetNamedItem("id").Value;
                                    textWriter.WriteAttributeString("xlink:href", href);
                                }
                                break;
                            }
                        //case "ce:edition":
                        //    {
                        //        //textWriter.WriteStartElement("email");
                        //        break;
                        //    }
                        //case "ce:editors":
                        //    {
                        //        break;
                        //    }
                        //case "ce:enunciation":
                        //    {
                        //        break;
                        //    }
                        case "ce:exam-answers":
                            {
                                if (node.ParentNode.Name.Equals("exam"))
                                    textWriter.WriteStartElement("body");
                                else
                                    endElement = false;
                                break;
                           }
                        case "ce:exam-questions":
                            {
                                if (node.ParentNode.Name.Equals("exam"))
                                  textWriter.WriteStartElement("body");
                                else
                                 endElement = false;
                                break;
                            }
                        //case "ce:exam-reference":
                        //    {
                        //        break;
                        //    }
                        case "ce:figure":
                            {
                                
                                //<fig position="float" id="F1">
                                textWriter.WriteStartElement("fig");
                                DefaultAtrbut(node);
                                textWriter.WriteAttributeString("position", "float");

                                //foreach (XmlNode chnode in node)
                                //{ 
                                //    SearchNode(chnode);
                                //}

                                //textWriter.WriteEndElement();
                                //if (!node.ParentNode.Name.Equals("p"))
                                //{
                                //    textWriter.WriteStartElement("p");
                                //}
                                //node.RemoveAll();
                                //endElement = false;
                                break;
                            }
                        //case "ce:first-page":
                        //    {
                        //        break;
                        //    }
                        case "ce:float-anchor":
                            {
                                if (node.Attributes.GetNamedItem("refid") != null)
                                {
                                    string refId = node.Attributes.GetNamedItem("refid").Value;

                                    if ("ce:section#body".IndexOf(node.ParentNode.Name) != -1)
                                    {
                                        ProcessFloatNode(refId);
                                    }
                                    else if (!node.ParentNode.Name.Equals("ce:para"))
                                    {
                                        textWriter.WriteStartElement("p");

                                        ProcessFloatNode(refId);

                                        if (!node.ParentNode.Name.Equals("ce:para"))
                                        {
                                            textWriter.WriteEndElement();
                                        }
                                    }
                                }
                                else
                                {
                                    Default(node);
                                }

                                endElement = false;
                                break;
                            }
                        //case "ce:floats":
                        //    {
                        //        break;
                        //    }
                        case "ce:footnote":
                            {
                                //<author-notes><corresp id="cor1">Address for correspondence: Franklin D. Lowy, Department of Medicine, Columbia University, College of Physicians and Surgeons, 630 W 168th St, New York, NY 10032, USA; email: <email xlink:href="fl189@columbia.edu" xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple">fl189@columbia.edu</email></corresp></author-notes>
                                if (node.ParentNode.Name.Equals("head") || node.ParentNode.Name.Equals("simple-head"))
                                {
                                    textWriter.WriteStartElement("author-notes");
                                    textWriter.WriteStartElement("fn");
                                    DefaultAtrbut(node);
                                    foreach (XmlNode chnode in node)
                                    {
                                        SearchNode(chnode);
                                    }
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();

                                }
                                else if (node.ParentNode.Name.Equals("ce:correspondence"))
                                {
                                    textWriter.WriteStartElement("fn");
                                    DefaultIDAtrbut(node);
                                }
                                else if (node.ParentNode.Name.Equals("td:author-group"))
                                {

                                }
                                else
                                {
                                    /////////**********************************\\\\\\\\\\\\\\\\
                                    textWriter.WriteStartElement("fn");
                                    DefaultIDAtrbut(node);
                                    /////////**********************************\\\\\\\\\\\\\\\\
                                    //Default(node);
                                }
                                break;
                            }
                        case "ce:formula":
                            {
                                textWriter.WriteStartElement("disp-formula");
                                DefaultIDAtrbut(node);
                                break;
                            }
                        case "ce:further-reading":
                            {
                                textWriter.WriteStartElement("ref-list");
                                break;
                            }
                        case "ce:further-reading-sec":
                            {
                                endElement = false;
                                break;
                            }
                        case "ce:given-name":
                            {
                                textWriter.WriteStartElement("given-names");
                                break;
                            }
                        case "ce:glossary":
                            {
                                textWriter.WriteStartElement("glossary");
                                break;
                            }
                        case "ce:glossary-def":
                            {
                                textWriter.WriteStartElement("def");
                                textWriter.WriteStartElement("p");
                                    TraverseChild(node);
                                textWriter.WriteEndElement();
                                node.RemoveAll();
                                break;
                            }
                        case "ce:glossary-entry":
                            {
                                textWriter.WriteStartElement("def-list");
                                textWriter.WriteStartElement("def-item");
                                TraverseChild(node);
                                textWriter.WriteEndElement();
                                node.RemoveAll();
                                break;
                           }
                         case "ce:glossary-heading":
                            {
                                textWriter.WriteStartElement("term");
                                break;
                            }
                        case "ce:glossary-sec":
                           {
                               textWriter.WriteStartElement("gloss-group");
                                break;
                            }
                        case "ce:glyph":
                            {
                                textWriter.WriteString("#$#bond;");
                                endElement = false;
                                break;
                            }
                        case "ce:hsp":
                            {
                                if (node.Attributes.GetNamedItem("sp") != null)
                                {
                                    string AtrStr = node.Attributes.GetNamedItem("sp").Value;
                                    if (AtrStr.Equals("0.16") || AtrStr.Equals("0.25"))
                                    {
                                        textWriter.WriteRaw("&#x02009;");
                                        //<!ENTITY ThinSpace        "&#x02009;" ><!--space of width 3/18 em alias ISOPUB thinsp -->

                                    }
                                    else if (AtrStr.Equals("0.5"))
                                    {
                                        textWriter.WriteRaw("&#x0205F;");
                                        //<!ENTITY MediumSpace      "&#x0205F;" ><!--space of width 4/18 em -->
                                    }
                                    else if (AtrStr.Equals("1.0"))
                                    {

                                        //<!ENTITY VeryThinSpace    "&#x0200A;" ><!--space of width 1/18 em alias ISOPUB hairsp -->
                                        textWriter.WriteRaw("&#x000A0;");
                                    }
                                    else
                                        textWriter.WriteRaw(" ");

                                }
                                endElement = false;
                                break;
                            }
                        //case "ce:imprint":
                        //    {
                        //        break;
                        //    }
                        //case "ce:include-item":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-entry":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-flag":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-flag-see":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-flag-see-also":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-flag-term":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-heading":
                        //    {
                        //        break;
                        //    }
                        //case "ce:index-sec":
                        //    {
                        //        break;
                        //    }
                        //case "ce:indexed-name":
                        //    {
                        //        break;
                        //    }
                        //case "ce:inf":
                        //    {
                        //        break;
                        //    }
                        //case "ce:initials":
                        //    {
                        //        break;
                        //    }
                        case "mml:math":
                            {
                                InlineFigure++;
                                textWriter.WriteStartElement("inline-graphic");
                                if (node.Attributes.GetNamedItem("altimg") != null)
                                    {
                                        string GifName = node.Attributes.GetNamedItem("altimg").Value;
                                        string ID  = JID + AID + "fx" + InlineFigure.ToString();

                                        string ImgName = "jle" + JID.ToLower() + AID.ToLower() + GifName + ".gif";

                                        textWriter.WriteAttributeString("id", ID);
                                        textWriter.WriteAttributeString("xlink:href", ImgName);
                                        textWriter.WriteAttributeString("xlink:type", "simple");
                                        node.RemoveAll();
                                    }
                                break;
                            }
                        case "ce:inline-figure":
                            {
                                if (node.InnerXml.IndexOf("Picto_DVD", StringComparison.OrdinalIgnoreCase) != -1)
                                {
                                    endElement = false;
                                    break;
                                }

                                InlineFigure++;
                                textWriter.WriteStartElement("inline-graphic");
                                if (node.FirstChild != null)
                                {
                                    if (node.FirstChild.Name.Equals("ce:link"))
                                    {
                                        if (node.FirstChild.Attributes.GetNamedItem("locator") != null)
                                        {
                                            string FxName = node.FirstChild.Attributes.GetNamedItem("locator").Value.ToLower();

                                            InlineFigure++;
                                            string ID = JID + AID + "fx" + InlineFigure.ToString();
                                            string ImgName = "jle" + JID.ToLower() + AID.ToLower() + FxName + ".jpg";

                                            textWriter.WriteAttributeString("id", ID);
                                            textWriter.WriteAttributeString("xlink:href", ImgName);
                                            textWriter.WriteAttributeString("xlink:type", "simple");

                                            node.RemoveAll();
                                        }
                                    }
                                }
                                break;
                            }
                        case "ce:inter-ref":
                            {
                                // ec
                                // Enzyme nomenclature. See http://www.chem.qmw.ac.uk/iubmb/enzyme/ 
                                //gen
                                // GenBank identifier 
                                //genpept
                                // Translated protein-encoding sequence database 
                                //highwire
                                // HighWire press intrajournal 
                                //pdb
                                // Protein data bank. See http://www.rcsb.org/pdb/ 
                                //pgr
                                // Plant gene register. See http://www.tarweed.com/pgr/ 
                                //pir
                                // Protein Information Resource. See http://pir.georgetown.edu 
                                //pirdb
                                // Protein Information Resource. See http://pir.georgetown.edu 
                                //pmc
                                // Used to link between articles in PubMed Central (access is PMID) 
                                //sprot
                                // Swiss-Prot. See http://www.ebi.ac.uk/swissprot/ 
                                //aoi
                                // Astronomical Object Identifier 
                                //doi
                                // Digital Object Identifier 
                                //ftp
                                // File transfer protocol 
                                //uri
                                // Website or web service
                                if (node.ParentNode.Name.Equals("email"))
                                {
                                    endElement = false;
                                }
                                else
                                {
                                    //<ext-link xlink:href="http://www.sciencedirect.com" ext-link-type="uri">www.sciencedirect.com</ext-link>
                                    textWriter.WriteStartElement("ext-link");

                                    if (node.Attributes.GetNamedItem("xlink:href") != null)
                                    {
                                        string AtrVal = node.Attributes.GetNamedItem("xlink:href").Value;

                                        textWriter.WriteAttributeString("xlink:href", AtrVal);
                                        textWriter.WriteAttributeString("xmlns:xlink", "http://www.w3.org/1999/xlink");

                                        if (AtrVal.StartsWith("omim:"))
                                        { }
                                        else if (AtrVal.IndexOf("/pdb/") != -1)
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "pdb");
                                        }
                                        else if (AtrVal.IndexOf("/swissprot/") != -1)
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "sprot");
                                        }
                                        else if (AtrVal.IndexOf("/pgr/") != -1)
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "pgr");
                                        }
                                        else if (AtrVal.IndexOf("/pir.") != -1)
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "pir");
                                        }
                                        else if (AtrVal.IndexOf("/enzyme") != -1)
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "ec");
                                        }
                                        else if (AtrVal.StartsWith("ftp"))
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "ftp");
                                        }
                                        else if (AtrVal.StartsWith("http:"))
                                        {
                                            textWriter.WriteAttributeString("ext-link-type", "uri");
                                        }
                                        else
                                        {
                                            ///need to be changed...
                                            textWriter.WriteAttributeString("ext-link-type", "uri");
                                        }


                                    }
                                    //Default(node);
                                }
                                break;
                            }
                        //case "ce:inter-ref-end":
                        //    {
                        //        break;
                        //    }
                        //case "ce:inter-ref-title":
                        //    {
                        //        break;
                        //    }
                        //case "ce:inter-refs":
                        //    {
                        //        break;
                        //    }
                        //case "ce:inter-refs-link":
                        //    {
                        //        break;
                        //    }
                        //case "ce:inter-refs-text":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-ref":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-ref-end":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-ref-title":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-refs":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-refs-link":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intra-refs-text":
                        //    {
                        //        break;
                        //    }
                        //case "ce:intro":
                        //    {
                        //        break;
                        //    }
                        case "ce:isbn":
                            {
                                textWriter.WriteStartElement("isbn");
                                break;
                            }
                        //case "ce:issn":
                        //    {
                        //        break;
                        //    }
                        //case "ce:italic":
                        //    {
                        //        break;
                        //    }
                        case "ce:keyword":
                            {
                                //textWriter.WriteStartElement("kwd");
                                endElement = false;
                                break;
                            }
                        case "ce:keywords":
                            {
                                textWriter.WriteStartElement("kwd-group");
                                if (node.Attributes.GetNamedItem("xml:lang") != null)
                                {
                                    string Lang = node.Attributes.GetNamedItem("xml:lang").Value;
                                    textWriter.WriteAttributeString("xml:lang", Lang);
                                }
                                break;
                            }
                        case "ce:label":
                            {
                                //if (node.NextSibling.Name.Equals("ce:note-para"))
                                //{
                                //    node.NextSibling.InnerXml = node.OuterXml + node.NextSibling.InnerXml;
                                //    node.RemoveAll();
                                //    endElement = false;
                                //}
                                //else
                                //{
                                    textWriter.WriteStartElement("label");
                                //}
                                break;
                            }
                        //case "ce:last-page":
                        //    {
                        //        break;
                        //    }
                        case "ce:legend":
                            {
                                if (node.ParentNode.Name.Equals("ce:figure"))
                                    endElement = false;
                                else if (node.ParentNode.Name.Equals("ce:table"))
                                {
                                    textWriter.WriteStartElement("table-wrap-foot");
                                    textWriter.WriteStartElement("fn");
                                    foreach (XmlNode chNode in node)
                                    {
                                        SearchNode(chNode);
                                    }
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();
                                }
                                else
                                    textWriter.WriteStartElement("fn");
                                break;
                            }
                        case "ce:link":
                            {
                                //<ce:link locator="gr2"/>
                                //<graphic xlink:href="1471-2105-6-S4-S22-1" position="float" 
                                //orientation="portrait" 
                                //xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple" />

                                if (node.ParentNode.Name.Equals("ce:author-group"))
                                {
                                    textWriter.WriteStartElement("bio");
                                    textWriter.WriteStartElement("p");
                                }
                                string HrefStr = "";
                                if (node.Attributes.GetNamedItem("locator") != null)
                                {

                                    HrefStr = node.Attributes.GetNamedItem("locator").Value.ToLower();
                                    //HrefStr = JID + AID + "gr" + Regex.Match(HrefStr, "[0-9]{1,}").Value.PadLeft(3, '0'); ;
                                    //HrefStr = JID + AID + HrefStr;
                                    //HrefStr = HrefStr.ToLower() + ".jpg";
                                    HrefStr = "jle" + JID.ToLower() + AID.ToLower() + HrefStr + ".jpg";
                                    //HrefStr = "gr" + Regex.Match(HrefStr, "[0-9]{1,}").Value.PadLeft(3, '0');
                                }
                                else
                                {
                                }
                                textWriter.WriteStartElement("graphic");
                                textWriter.WriteAttributeString("xlink:href", HrefStr);
                                textWriter.WriteAttributeString("position", "float");
                                //textWriter.WriteAttributeString("orientation","portrait");
                                textWriter.WriteAttributeString("xmlns:xlink", "http://www.w3.org/1999/xlink");
                                textWriter.WriteAttributeString("xlink:type", "simple");

                                if (node.ParentNode.Name.Equals("ce:author-group"))
                                {
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    endElement = false;
                                }
                                break;
                            }
                        case "ce:list":
                            {
                                textWriter.WriteStartElement("list");
                                string lbl = "";

                                if (node.FirstChild.FirstChild != null)
                                {
                                    if (node.FirstChild.FirstChild.Name.Equals("ce:label"))
                                    {
                                        lbl = node.FirstChild.FirstChild.InnerText.Trim(new char[] { '.', '(', ')' });
                                        //Console.WriteLine(lbl);
                                    }
                                }

                                if (lbl.Equals("1"))
                                {
                                    textWriter.WriteAttributeString("list-type", "order");
                                }
                                else if (lbl.Equals("a"))
                                {
                                    textWriter.WriteAttributeString("list-type", "alpha-lower");
                                }
                                else if (lbl.Equals("A"))
                                {
                                    textWriter.WriteAttributeString("list-type", "alpha-upper");
                                }
                                else if (lbl.Equals("i"))
                                {
                                    textWriter.WriteAttributeString("list-type", "roman-lower");
                                }
                                else if (lbl.Equals("I"))
                                {
                                    textWriter.WriteAttributeString("list-type", "roman-upper");
                                }
                                else if (lbl.Equals("#$#bull;"))
                                {
                                    textWriter.WriteAttributeString("list-type", "bullet");
                                }
                                else if (!lbl.Equals(""))
                                {
                                    textWriter.WriteAttributeString("list-type", "simple");
                                }
                                else
                                { }
                                break;
                            }
                        case "ce:list-item":
                            {
                                textWriter.WriteStartElement("list-item");
                                break;
                            }
                        case "ce:miscellaneous":
                            {
                               ////td:author-group><td:miscellaneous>11&nbsp;f&eacute;vrier&nbsp;2011</td:miscellaneous></head><bo
                                //if (node.PreviousSibling.Name.Equals("ce:author-group"))
                                //{
                                //    if (node.NextSibling == null)
                                //    {
                                //        textWriter.WriteStartElement("pub-date");
                                //        break;
                                //    }
                                //}
                                //***********To check this condition****************\\\
                                node.RemoveAll();
                                node.ParentNode.RemoveChild(node);
                                endElement = false;
                                break;
                            }
                        //case "ce:monospace":
                        //    {
                        //        break;
                        //    }
                        //case "ce:nomenclature":
                        //    {
                        //        break;
                        //    }
                        case "ce:note":
                            {
                                textWriter.WriteStartElement("note");
                                //endElement = false;
                                break;
                            }
                        case "ce:note-para":
                            {
                                if (node.ParentNode.Name.Equals("ce:article-footnote"))
                                {
                                    //textWriter.WriteStartElement("fn");
                                    if (node.FirstChild.Name.Equals("ce:label"))
                                    {
                                        SearchNode(node.FirstChild);
                                        node.RemoveChild(node.FirstChild);
                                    }
                                    textWriter.WriteStartElement("p");
                                    foreach (XmlNode chNode in node)
                                    {
                                        SearchNode(chNode);
                                    }
                                    textWriter.WriteEndElement();
                                    //textWriter.WriteEndElement();
                                    endElement = false;
                                    node.RemoveAll();

                                    //if (ce_author_group)
                                    //{
                                    //    textWriter.WriteStartElement("fn");
                                    //}
                                    //else
                                    //{
                                    //    endElement = false;
                                    //}
                                }
                                else if (node.ParentNode.ParentNode.Name.Equals("head"))
                                {
                                    //textWriter.WriteStartElement("corresp");
                                    //<corresp id="cor1">Address for correspondence: Franklin D. Lowy, Department of Medicine, Columbia University, College of Physicians and Surgeons, 630 W 168th St, New York, NY 10032, USA; email: <email xlink:href="fl189@columbia.edu" xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple">fl189@columbia.edu</email></corresp>

                                    textWriter.WriteStartElement("p");
                                }
                                else
                                {
                                    textWriter.WriteStartElement("p");

                                }
                                break;
                            }
                        case "ce:other-ref":
                            {
                                if (node.ParentNode.Name.Equals("book-review-head"))
                                {
                                    textWriter.WriteStartElement("product");
                                }
                                else
                                {
                                    textWriter.WriteStartElement("citation");
                                    textWriter.WriteAttributeString("citation-type", "other");
                                }
                                break;
                            }
                        //case "ce:pages":
                        //    {
                        //        break;
                        //    }
                        //case "ce:para":
                        //    {
                        //        break;
                        //    }
                        //case "ce:pii":
                        //    {
                        //        break;
                        //    }
                        //case "ce:preprint":
                        //    {
                        //        break;
                        //    }
                        //case "ce:presented":
                        //    {
                        //        break;
                        //    }
                        //case "ce:ranking":
                        //    {
                        //        break;
                        //    }
                        //case "ce:reader-see":
                        //    {
                        //        break;
                        //    }
                        //case "ce:refers-to-document":
                        //    {
                        //        break;
                        //    }
                        case "ce:roles":
                            {
                                textWriter.WriteStartElement("role");
                                break;
                            }
                        case "ce:salutation":
                            {
                                if (node.ParentNode.Name.Equals("body"))
                                    textWriter.WriteStartElement("p");
                                else
                                    Default(node);
                                break;
                            }
                        //case "ce:sans-serif":
                        //    {
                        //        break;
                        //    }
                        case "section":
                            {
                                textWriter.WriteStartElement("sec");
                                SecAttribute(node);
                                break;
                            }
                        case "ce:section":
                            {
                                textWriter.WriteStartElement("sec");
                                SecAttribute(node);
                                break;
                            }
                        case "ce:section-title":
                            {
                                if (node.ParentNode.Name.Equals("ce:abstract-sec"))
                                {
                                    textWriter.WriteStartElement("title");
                                }
                                else if (node.ParentNode.Name.Equals("ce:section"))
                                {
                                    textWriter.WriteStartElement("title");
                                }
                                else
                                {
                                    textWriter.WriteStartElement("title");
                                    //Default(node);
                                }
                                break;
                            }
                        case "ce:sections":
                            {
                                if (node.ParentNode.Name.Equals("body"))
                                    endElement = false;
                                else
                                {
                                    endElement = false;
                                    //Default(node);
                                }
                                break;
                            }
                        //case "ce:see":
                        //    {
                        //        break;
                        //    }
                        //case "ce:see-also":
                        //    {
                        //        break;
                        //    }
                        case "ce:simple-para":
                            {
                                if (node.ParentNode.Name.Equals("ce:abstract-sec"))
                                {
                                    textWriter.WriteStartElement("p");
                                }
                                else if (node.ParentNode.Name.Equals("ce:caption"))
                                {
                                    if (node.ParentNode.ParentNode.Name.Equals("ce:textbox"))
                                    {
                                        endElement = false;
                                    }
                                    else
                                    {
                                        textWriter.WriteStartElement("p");
                                    }
                                }
                                else
                                {
                                    textWriter.WriteStartElement("p");
                                    //Default(node);
                                }
                                break;
                            }
                        case "ce:small-caps":
                           {
                               node.InnerXml = node.InnerXml.ToUpper();
                               endElement = false;
                               break;
                           }
                        case "ce:source":
                            {

                                if (node.ParentNode.SelectSingleNode(".//ce:table", nsmgr) != null)
                                {
                                    //<table-wrap-foot><fn><p>
                                    textWriter.WriteStartElement("table-wrap-foot");
                                    textWriter.WriteStartElement("fn");
                                    textWriter.WriteStartElement("p");
                                    foreach (XmlNode chnode in node)
                                    {
                                        SearchNode(chnode);
                                    }
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();
                                    endElement = false;
                                }
                                else if (node.ParentNode.Name.Equals("ce:figure"))
                                {
                                    textWriter.WriteStartElement("p");
                                }
                                else if (node.ParentNode.Name.Equals("ce:table"))
                                {
                                    textWriter.WriteStartElement("table-wrap-foot");
                                    textWriter.WriteStartElement("fn");
                                    textWriter.WriteStartElement("p");
                                    foreach (XmlNode chnode in node)
                                    {
                                        SearchNode(chnode);
                                    }
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();
                                    endElement = false;
                                }
                                else
                                {
                                    //Jitender 1-10-2012
                                    textWriter.WriteStartElement("related-article");
                                    textWriter.WriteAttributeString("related-article-type", "source");
                                            textWriter.WriteStartElement("source");
                                                    foreach (XmlNode chnode in node)
                                                    {
                                                        SearchNode(chnode);
                                                    }
                                            textWriter.WriteEndElement();
                                    textWriter.WriteEndElement();
                                    node.RemoveAll();
                                    endElement = false;
                                }
                                break;
                            }
                        case "ce:sponsorship":
                            {
                                textWriter.WriteStartElement("contract-sponsor");
                                break;
                            }
                        //case "ce:stereochem":
                        //    {
                        //        break;
                        //    }
                        case "ce:subtitle":
                            {
                                textWriter.WriteStartElement("subtitle");
                                break;
                            }
                        case "ce:suffix":
                            {
                                textWriter.WriteStartElement("suffix");
                                break;
                            }
                        //case "ce:sup":
                        //    {
                        //        break;
                        //    }
                        case "ce:surname":
                            {
                                textWriter.WriteStartElement("surname");
                                break;
                            }
                        case "ce:table":
                            {
                                if (node.SelectNodes(".//ce:source", nsmgr).Count > 0)
                                {
                                    XmlNodeList NL = node.SelectNodes(".//ce:source", nsmgr);
                                    for (int i = NL.Count - 1; i >= 0; i--)
                                    {
                                        node.InsertAfter(NL[i], node.LastChild);
                                    }
                                }
                                textWriter.WriteStartElement("table-wrap");
                                DefaultIDAtrbut(node);
                                textWriter.WriteAttributeString("position", "float");
                                //table-wrap id="tbl1" position="float"
                                break;
                            }
                        case "ce:table-footnote":
                            {
                                textWriter.WriteStartElement("fn");
                                DefaultIDAtrbut(node);
                                break;
                            }
                        case "ce:text":
                            {
                                if (node.ParentNode.Name.Equals("ce:correspondence"))
                                {
                                    endElement = false;
                                    //textWriter.WriteStartElement("corresp");
                                }
                                else if (node.ParentNode.Name.Equals("ce:keyword"))
                                {
                                    textWriter.WriteStartElement("kwd");

                                }
                                else if (node.ParentNode.Name.Equals("ce:collaboration"))
                                {
                                    endElement = false;
                                }
                                else
                                {
                                    Default(node);
                                }
                                break;
                            }
                        case "ce:textbox":
                            {
                                textWriter.WriteStartElement("boxed-text");

                                if (node.InnerXml.IndexOf ("Legends for video sequences",StringComparison.OrdinalIgnoreCase)!=-1)
                                {
                                    textWriter.WriteAttributeString("content-type", "hidden");
                                }
                                else if( node.InnerXml.IndexOf ("Legend for video sequence",StringComparison.OrdinalIgnoreCase)!=-1)
                                {
                                    textWriter.WriteAttributeString("content-type", "hidden");
                                }
                                else if (node.InnerXml.IndexOf("TEST YOURSELF", StringComparison.OrdinalIgnoreCase) != -1)
                                {
                                    textWriter.WriteAttributeString("content-type", "hidden");
                                }

                                DefaultAtrbut(node);
                                break;
                            }
                        case "ce:textbox-body":
                            {
                                endElement = false;
                                break;
                            }
                        //case "ce:textbox-head":
                        //    {
                        //        break;
                        //    }
                        //case "ce:textbox-tail":
                        //    {
                        //        break;
                        //    }
                        case "ce:textfn":
                            {
                                if (node.ParentNode.Name.Equals("ce:dochead"))
                                {
                                    textWriter.WriteStartElement("fn");
                                }
                                else
                                {
                                    endElement = false;
                                }
                                break;
                            }
                        case "ce:textref":
                            {
                                endElement = false;
                                break;
                            }
                        case "ce:title":
                            {
                                if ("book-review-head#head#simple-head".IndexOf(node.ParentNode.Name) != -1)
                                {
                                    ProcessArticleTitle(node);
                                    node.RemoveAll();
                                    endElement = false;
                                }
                                else
                                {
                                    Default(node);
                                }
                                break;
                            }
                        //case "ce:underline":
                        //    {
                        //        break;
                        //    }
                        case "ce:vsp":
                            {
                                endElement = false;
                                break;
                            }
                        case "cnt":
                            {
                                textWriter.WriteStartElement("country");
                                //endElement = false;
                                break;
                            }
                        case "colspec":
                            {
                                endElement = false;
                                break;
                            }
                        case "email":
                            {
                                textWriter.WriteStartElement("email");
                                break;
                            }
                        case "entry":
                            {
                                textWriter.WriteStartElement("td");
                                TDAtrbut(node);
                                break;
                            }
                        case "exam":
                            {
                                endElement = false;
                                break;
                            }
                        case "fax":
                            {
                                endElement = false;
                                break;
                            }
                        
                            
                        case "head":
                            {
                                MakeFront(node);
                                endElement = false;
                                break;
                            }
                        //case "item-info":
                        //    {
                        //        break;
                        //    }
                        case "jid":
                            {
                                JID = node.InnerText;
                                endElement = false;
                                break;
                            }
                        //case "lrh":
                        //    {
                        //        break;
                        //    }
                        case "row":
                            {
                                textWriter.WriteStartElement("tr");
                                break;
                            }
                        //case "rrh":
                        //    {
                        //        break;
                        //    }
                        
                        case "sb:author":
                            {
                                textWriter.WriteStartElement("name");
                                break;
                            }
                        case "sb:authors":
                            {
                                break;
                            }
                        //case "sb:book":
                        //    {
                        //        break;
                        //    }
                        //case "sb:book-series":
                        //    {
                        //        break;
                        //    }
                        case "sb:collaboration":
                            {
                                textWriter.WriteStartElement("collab");
                                break;
                            }
                        case "sb:comment":
                            {
                                textWriter.WriteStartElement("comment");
                                break;
                            }
                            //munesh
                        case "sb:conference":
                            {
                                endElement = false;
                                break;
                            }
                           //-------------------//
                        //case "sb:contribution":
                        //    {
                        //        break;
                        //    }
                        case "sb:date":
                            {
                                textWriter.WriteStartElement("year");
                                break;
                            }
                        case "sb:e-host":
                            {
                                endElement = false;
                                break;
                            }
                        //case "sb:edited-book":
                        //    {
                        //        break;
                        //    }
                        case "sb:edition":
                            {
                                textWriter.WriteStartElement("edition");
                                break;
                            }
                        case "sb:editor":
                            {
                                textWriter.WriteStartElement("name");
                                break;
                            }
                        case "sb:editors":
                            {
                                //  person-group-type Type of People in the Person Group

                                textWriter.WriteStartElement("person-group");
                                textWriter.WriteAttributeString("person-group-type", "editor");

                                break;
                            }
                        case "sb:et-al":
                            {
                                textWriter.WriteStartElement("etal");
                                break;
                            }
                        case "sb:first-page":
                            {
                                textWriter.WriteStartElement("fpage");
                                break;
                            }
                        //case "sb:host":
                        //    {
                        //        break;
                        //    }
                        case "sb:isbn":
                            {
                                textWriter.WriteStartElement("isbn");
                                break;
                            }
                        //case "sb:issn":
                        //    {
                        //        break;
                        //    }
                        //case "sb:issue":
                        //    {
                        //        break;
                        //    }
                        case "sb:issue-nr":
                            {
                                textWriter.WriteStartElement("issue");
                                break;
                            }
                        case "sb:last-page":
                            {
                                textWriter.WriteStartElement("lpage");
                                break;
                            }
                        case "sb:location":
                            {
                                if (node.ParentNode.Name.Equals("sb:publisher"))
                                    textWriter.WriteStartElement("publisher-loc");
                                else
                                    Default(node);
                                break;
                            }
                        case "sb:maintitle":
                            {

                                if (node.NextSibling.Name.Equals("sb:maintitle"))
                                {
                                    textWriter.WriteStartElement("source");
                                }
                                else if (node.PreviousSibling.Name.Equals("sb:maintitle"))
                                {
                                    textWriter.WriteRaw(" ");
                                    endElement = false;
                                }
                                else
                                    textWriter.WriteStartElement("series");

                                break;
                            }
                        case "sb:name":
                            {
                                if (node.ParentNode.Name.Equals("sb:publisher"))
                                    textWriter.WriteStartElement("publisher-name");
                                else
                                    Default(node);
                                break;
                            }
                        //case "sb:pages":
                        //    {
                        //        break;
                        //    }
                        case "sb:publisher":
                            {
                                endElement = false;
                                break;
                            }
                        //case "sb:reference":
                        //    {
                        //        break;
                        //    }
                        //case "sb:series":
                        //    {
                        //        break;
                        //    }
                        //case "sb:subtitle":
                        //    {
                        //        break;
                        //    }
                        //case "sb:title":
                        //    {
                        //        break;
                        //    }
                        //case "sb:translated-title":
                        //    {
                        //        break;
                        //    }
                        case "sb:volume-nr":
                            {
                                textWriter.WriteStartElement("volume");
                                break;
                            }
                        case "simple-article":
                            {
                                endElement = false;
                                //textWriter.WriteStartElement("article");
                                break;
                            }
                        case "simple-head":
                            {
                                MakeFront(node);
                                endElement = false;
                                break;
                            }
                        case "simple-tail":
                            {
                                textWriter.WriteStartElement("back");
                                //endElement = false;
                                break;
                            }
                        case "tail":
                            {
                                textWriter.WriteStartElement("back");
                                break;
                            }
                        //case "tb:alignmark":
                        //    {
                        //        break;
                        //    }
                        //case "tb:bottom-border":
                        //    {
                        //        break;
                        //    }
                        //case "tb:colspec":
                        //    {
                        //        break;
                        //    }
                        //case "tb:left-border":
                        //    {
                        //        break;
                        //    }
                        //case "tb:right-border":
                        //    {
                        //        break;
                        //    }
                        //case "tb:top-border":
                        //    {
                        //        break;
                        //    }
                        //case "tbody":
                        //    {
                        //        break;
                        //    }
                        case "tel":
                            {
                                endElement = false;
                                break;
                            }
                        case "tgroup":
                            {
                                textWriter.WriteStartElement("table");
                                node.ParentNode.Attributes.RemoveNamedItem("id");
                                TableAttriibute(node.ParentNode);
                                break;
                            }
                        case "thead":
                            {
                                textWriter.WriteStartElement("thead");
                                break;
                            }
                        case "pub-date":
                            {


                                textWriter.WriteRaw(node.OuterXml);
                                node.RemoveAll();


                                if (!string.IsNullOrEmpty(JLEDATASET.Program.VOL))
                                {
                                    textWriter.WriteStartElement("volume");
                                    textWriter.WriteString(JLEDATASET.Program.VOL);
                                    textWriter.WriteEndElement();
                                }

                                if (!string.IsNullOrEmpty(JLEDATASET.Program.ISSUE))
                                {
                                    textWriter.WriteStartElement("issue");
                                    textWriter.WriteString(JLEDATASET.Program.ISSUE);
                                    textWriter.WriteEndElement();
                                }

                               
                                if (!string.IsNullOrEmpty(sPage))
                                {
                                    textWriter.WriteStartElement("fpage");
                                    textWriter.WriteString(sPage);
                                    textWriter.WriteEndElement();
                                }

                                ////////////put dummy space here and remove in filtaration
                                SearchNode(TextElement(" "));
                                ////
                                if (!string.IsNullOrEmpty(lPage))
                                {
                                    textWriter.WriteStartElement("lpage");
                                    textWriter.WriteString(lPage);
                                    textWriter.WriteEndElement();
                                }


                                ////<volume>24</volume>
                                ////<issue>18</issue>
                                ////<fpage>2777</fpage>
                                ////<lpage>2787</lpage>

                                endElement = false;
                                break;
                            }
                        default:
                            {
                                if (node.Name.IndexOf(":") != -1)
                                {
                                    Console.WriteLine(node.Name);
                                }
                                Default(node);
                                break;
                            }
                    }
                }
            }
        }
        private XmlNode TextElement(string InnerText)
        {
            XmlElement Ele = xmlDocument.CreateElement("text");
            Ele.InnerText = InnerText;
            return (XmlNode)(Ele);
        }
        private void Default(XmlNode node)
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
        private void DefaultMath(XmlNode node)
        {
            textWriter.WriteStartElement(node.Name);
            MathID++;
            textWriter.WriteAttributeString("id", "M" + MathID.ToString());
            if (node.Attributes.Count > 0)
            {
                for (int X = 0; X < node.Attributes.Count; X++)
                {
                    if (!node.Attributes[X].Name.StartsWith("xmlns") && !node.Attributes[X].Name.Equals("overflow"))
                        textWriter.WriteAttributeString(node.Attributes[X].Name, node.Attributes[X].Value);
                }
            }
            foreach (XmlNode chNode in node)
            {
                SearchNode(chNode);
            }
            node.RemoveAll();
        }
        private void DefaultAtrbut(XmlNode node)
        {
            if (node.Attributes.Count > 0)
            {
                for (int X = 0; X < node.Attributes.Count; X++)
                {
                    if (node.Attributes[X].Name.StartsWith("role")) { }
                    else if (!node.Attributes[X].Name.StartsWith("xmlns"))
                        textWriter.WriteAttributeString(node.Attributes[X].Name, node.Attributes[X].Value);
                }
            }
        }
        private void TDAtrbut(XmlNode node)
        {
            int ColSpanStart = 0, ColSpanEnd = 0;
            if (node.Attributes.Count > 0)
            {
                for (int X = 0; X < node.Attributes.Count; X++)
                {
                    if (node.Attributes[X].Name.StartsWith("morerows"))
                    {
                        textWriter.WriteAttributeString("rowspan", node.Attributes[X].Value);
                    }
                    else if (node.Attributes[X].Name.StartsWith("rowsep"))
                    {
                        //textWriter.WriteAttributeString("rowsep", node.Attributes[X].Value);
                    }
                    else if (node.Attributes[X].Name.StartsWith("colsep"))
                    {
                        if (ColSpanStart == 0 && ColSpanEnd == 0)
                        {
                            textWriter.WriteAttributeString("colspan", node.Attributes[X].Value);
                        }
                    }
                    else if (node.Attributes[X].Name.Equals("namest"))
                        ColSpanStart = Int32.Parse(node.Attributes[X].Value.Replace("col", ""));
                    else if (node.Attributes[X].Name.Equals("nameend"))
                        ColSpanEnd = Int32.Parse(node.Attributes[X].Value.Replace("col", ""));
                    else if (!node.Attributes[X].Name.StartsWith("xmlns"))
                        textWriter.WriteAttributeString(node.Attributes[X].Name, node.Attributes[X].Value);
                }
            }
            if (ColSpanStart > 0 && ColSpanEnd > 0)
            {
                int ColSpan = 0;
                ColSpan = ColSpanEnd - ColSpanStart;
                ColSpan++;
                textWriter.WriteAttributeString("colspan", ColSpan.ToString());
            }
        }
        private void DefaultIDAtrbut(XmlNode node)
        {
            if (node.Attributes.GetNamedItem("id") != null)
            {
                string ID = node.Attributes.GetNamedItem("id").Value;
                textWriter.WriteAttributeString("id", ID);
            }
        }
        private void ProcessOutsideParaNode(XmlNode node)
        {

            /////////////Do'nt change sequence
            XmlNodeList NL;
            NL = node.ChildNodes;
            for (int i = NL.Count - 1; i >= 0; i--)
            {
                if (NL[i].Name.Equals("ce:displayed-quote"))
                {
                    node.ParentNode.InsertAfter(NL[i], node);
                }
                else if (NL[i].Name.Equals("ce:display"))
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
        private void MakeFront(XmlNode node)
        {
            textWriter.WriteStartElement("front"); //////////////Start Front

            textWriter.WriteStartElement("journal-meta"); //////////////Start Journal-Meta
            ProcessJournalMeta();
            textWriter.WriteEndElement();     //////////////Close Journal-Meta

            textWriter.WriteStartElement("article-meta"); //////////////Start Article-Meta

            if (!Doi.Equals(""))
            {
                textWriter.WriteStartElement("article-id");
                textWriter.WriteAttributeString("pub-id-type", "doi");
                textWriter.WriteString(Doi);
                textWriter.WriteEndElement();
            }

            if (node.InnerXml.IndexOf("ce:dochead") == -1)
            {
                textWriter.WriteStartElement("subj-group");
                textWriter.WriteStartElement("subject");
                textWriter.WriteString(JLELibSomm);
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();
            }
            TraverseChild(node);

            node.RemoveAll();           
            //ProcessArticleMeta();

            //XmlNode AT = node.SelectSingleNode("./ce:title", nsmgr);

            //if (AT != null)
            //{
            //    SearchNode(AT);
            //    node.RemoveChild(AT);
            //}
            textWriter.WriteEndElement();     //////////////Close Article-Meta
            textWriter.WriteEndElement(); //////////////Close Front

        }

        private void ProcessJournalMeta()
        {
            string MetaData     = ExeLoc+ "\\MetaData.xml";
            string TempMetaData = ExeLoc + "\\TempMetaData.xml";

            StringBuilder xmlStr = new StringBuilder(File.ReadAllText(MetaData));
            xmlStr.Replace("&", "#$#");

            xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
            xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");
            
            File.WriteAllText(TempMetaData, xmlStr.ToString());

            XmlDocument MyXmlDocument = new XmlDocument();
            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.ProhibitDtd = false;
            XmlReader Reader = XmlReader.Create(TempMetaData, ReaderSettings);
            try
            {
                MyXmlDocument.PreserveWhitespace = true;
                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");

                XmlNode Node = MyXmlDocument.GetElementsByTagName(JID)[0];

                if (Node != null)
                {
                    textWriter.WriteRaw(Node.InnerXml.Replace("#$#", "&"));
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


            ////"John Libbey Eurotext Limited"
            //textWriter.WriteStartElement("journal-meta");
            //    textWriter.WriteElementString("journal-id journal-id-type", "");
            //    textWriter.WriteStartElement("journal-title-group");
            //            textWriter.WriteStartElement("journal-title","");
            //    textWriter.WriteEndElement();

            //    textWriter.WriteStartElement("issn");
            //    textWriter.WriteAttributeString("", "");
            //    textWriter.WriteString(" ");
            //    textWriter.WriteEndElement();

            //    textWriter.WriteStartElement("issn");
            //    textWriter.WriteAttributeString("","");
            //    textWriter.WriteString(" ");
            //    textWriter.WriteEndElement();

            //    textWriter.WriteStartElement("publisher");
            //        textWriter.WriteElementString("publisher-name", "John Libbey Eurotext Limited");
            //    textWriter.WriteEndElement();//////Close publisher
            //textWriter.WriteEndElement();//////Close journal-meta
        }
        private void ProcessArticleMeta()
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            nsmgr.AddNamespace("xmlns:ce", "http://www.elsevier.com/xml/common/dtd");
            nsmgr.AddNamespace("xmlns:td", "http://www.elsevier.com/xml/common/dtd");
            //<article-id pub-id-type="doi">10.3390/ijerph7020509</article-id>
            if (!Doi.Equals(""))
            {
                textWriter.WriteStartElement("article-id");
                textWriter.WriteAttributeString("pub-id-type", "doi");
                textWriter.WriteString(Doi);
                textWriter.WriteEndElement();
            }
            textWriter.WriteStartElement("article-categories");////////////////Article-categories Start
            textWriter.WriteStartElement("subj-group");////////////////SubjectGroup Start
            textWriter.WriteAttributeString("subj-group-type", "heading");
            textWriter.WriteStartElement("subject"); ;////////////////Subject Start
            textWriter.WriteString("Article");
            textWriter.WriteEndElement();////////////////Subject Close
            textWriter.WriteEndElement();////////////////SubjectGroup End
            textWriter.WriteEndElement();////////////////Article-categories Close
        }
        private void ProcessArticleTitle(XmlNode node)
        {
            textWriter.WriteStartElement("title-group");
            textWriter.WriteStartElement("article-title");

            if (MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang") != null)
            {
                string Lang = MyXmlDocument.DocumentElement.Attributes.GetNamedItem("xml:lang").Value;
                textWriter.WriteAttributeString("xml:lang", Lang);
            }

            TraverseChild(node);
            textWriter.WriteEndElement();

            if (node.NextSibling != null)
            {
                while (node.NextSibling.Name.IndexOf("title") != -1)// || node.NextSibling.InnerXml.IndexOf("article")!=-1)
                {
                    //if (node.NextSibling.Name.Equals("ce:alt-title"))
                    //{
                    SearchNode(node.NextSibling);
                    node.ParentNode.RemoveChild(node.NextSibling);
                    //}
                    if (node.NextSibling == null)
                    {
                        break;
                    }
                }
            }
            if (node.NextSibling != null)
            {
                while (node.NextSibling.Name.IndexOf("fn-group") != -1)// || node.NextSibling.InnerXml.IndexOf("article")!=-1)
                {
                    //if (node.NextSibling.Name.Equals("ce:alt-title"))
                    //{
                    SearchNode(node.NextSibling);
                    node.ParentNode.RemoveChild(node.NextSibling);
                    //}
                    if (node.NextSibling == null)
                    {
                        break;
                    }
                }
            }
            //textWriter.WriteEndElement();
            if (ce_author_group == false)
                textWriter.WriteEndElement();
        }
        private void ProcessAuthorGroup(XmlNode node)
        {
            BreakNodeFromLastTillChNodeName(node, "ce:author");
            //<contrib contrib-type="author">
            textWriter.WriteStartElement("contrib-group");
            TraverseChild(node);

        }
        private void BreakNodeFromLastTillChNodeName(XmlNode node, string ChNodeName)
        {
            ///////////////////////////////Move child node to outside
            XmlNodeList NodeList = node.ChildNodes;
            for (int ChNodeCount = NodeList.Count - 1; ChNodeCount > 0; ChNodeCount--)
            {
                if (ChNodeName.IndexOf(NodeList[ChNodeCount].Name) != -1) /////////////break if node name match 
                {
                    break;
                }
                NodeList[ChNodeCount].ParentNode.ParentNode.InsertAfter(NodeList[ChNodeCount], NodeList[ChNodeCount].ParentNode);
            }
        }
        private void TraverseChild(XmlNode node)
        {
            foreach (XmlNode chNode in node.ChildNodes)
            {
                SearchNode(chNode);
            }
        }
        private void FilterToNLM()
        {
            StringBuilder XmlStr = new StringBuilder (MyXmlDocument.InnerXml);
            XmlStr.Replace("<ce:def-term>", "<def-item><ce:def-term>");
            XmlStr.Replace("</ce:def-description>", "</ce:def-description></def-item>");

            MyXmlDocument.InnerXml = XmlStr.ToString();
          //XmlNodeList MathNL = MyXmlDocument.GetElementsByTagName("mml:math");
          //for (int i = MathNL.Count - 1; i >= 0; i--)
          //{
          //    if (MathNL[i].ParentNode.Name.Equals("");
          //}
            nsmgr.AddNamespace("ce", "http://www.elsevier.com/xml/common/dtd");


            XmlNodeList NL;

            NL = MyXmlDocument.SelectNodes(".//ce:caption/ce:simple-para/ce:inline-figure",nsmgr);
            if (NL.Count > 0)
            {
                //while (NL.Count>0)
                for (int i = 0; i < NL.Count; i++) 
                {
                    
                    if (NL[i].ParentNode != null && NL[i].ParentNode.Name.Equals("ce:simple-para"))
                    {
                        if (NL[i].ParentNode.ParentNode != null)
                        {
                            if (NL[i].ParentNode.LastChild.Equals(NL[i]) && NL[i].ParentNode.ParentNode.Name.Equals("ce:caption"))
                            {
                                NL[i].ParentNode.ParentNode.ParentNode.InsertAfter(NL[i].FirstChild, NL[i].ParentNode.ParentNode);

                                NL[i].ParentNode.RemoveChild(NL[i]);
                            }
                        }
                    }
                }
            }
            



            XmlNode SrchNode = null;
            NL = MyXmlDocument.GetElementsByTagName("ce:correspondence");

            if (NL.Count > 0)
            {
                SrchNode = NL[NL.Count - 1];
                if (SrchNode.NextSibling != null )
                {
                    SrchNode = SrchNode.ParentNode.LastChild;
                }
            }
            else
            {
                NL = MyXmlDocument.GetElementsByTagName("ce:author-group");

                if (NL.Count > 0)
                {
                    SrchNode = NL[NL.Count - 1];

                    if (SrchNode.PreviousSibling != null && SrchNode.PreviousSibling.Name.Equals("ce:other-ref"))
                    {
                         SrchNode.ParentNode.InsertAfter(SrchNode.PreviousSibling, SrchNode);
                    }
                }
                else
                {
                    NL = MyXmlDocument.GetElementsByTagName("ce:affiliation");
                    if (NL.Count > 0)
                    {
                        SrchNode = NL[NL.Count - 1];
                    }
                    else
                    {
                        NL = MyXmlDocument.GetElementsByTagName("head");

                        if (NL.Count == 0)
                        {
                            NL = MyXmlDocument.GetElementsByTagName("simple-head");
                        }


                        if (NL.Count > 0)
                        {
                            SrchNode = NL[NL.Count - 1];
                        }
                        else
                        {
                            NL = MyXmlDocument.GetElementsByTagName("simple-head");
                            if (NL.Count > 0)
                            {
                                SrchNode = NL[NL.Count - 1];
                            }
                            else
                            {
                                Console.WriteLine("Need to check this condition");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                    }
                }
            }

            if (SrchNode != null && !string.IsNullOrEmpty(PubDate))
            {
                string[] PubDatePart = PubDate.Split('-');
                if (PubDatePart.Length == 3)
                {
                    XmlElement PubDateNode;
                    //<pub-date pub-type="pub"><day>27</day><month>03</month><year>1999</year></pub-date>
                    PubDateNode = MyXmlDocument.CreateElement("pub-date");
                    PubDateNode.SetAttribute("pub-type", "ppub");


                    PubDateNode.InnerXml = "<day>" + PubDatePart[2] + "</day><month>" + PubDatePart[1] + "</month><year>" + PubDatePart[0] + "</year>";

                    if (SrchNode.Name.Contains("head"))
                    {
                        SrchNode.InsertAfter(PubDateNode, SrchNode.LastChild);
                    }
                    else
                    {
                        SrchNode.ParentNode.InsertAfter(PubDateNode, SrchNode);
                    }
                }
            }



            XmlNodeList nodeList = MyXmlDocument.SelectNodes("//ce:footnote", nsmgr);
            XmlElement FnGroup = MyXmlDocument.CreateElement("fn-group");
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].PreviousSibling != null)
                {
                    if (nodeList[i].PreviousSibling.Name.Equals("ce:cross-ref"))
                    {
                        if (nodeList[i].FirstChild.Name.Equals("ce:label"))
                        {
                            nodeList[i].InnerXml = nodeList[i].InnerXml.Replace("</ce:label>", "</ce:label>");
                        }
                        FnGroup.AppendChild(nodeList[i]);
                    }
                }
            }

            if (FnGroup.ChildNodes.Count > 0)
            {
                if (MyXmlDocument.GetElementsByTagName("tail").Count > 0)
                {
                    MyXmlDocument.GetElementsByTagName("tail")[0].AppendChild(FnGroup);
                }
                else
                {
                    XmlElement BACK = MyXmlDocument.CreateElement("back");
                    BACK.AppendChild(FnGroup);
                    MyXmlDocument.DocumentElement.AppendChild(BACK);
                }
            }

             NL = MyXmlDocument.GetElementsByTagName("ce:float-anchor");
            for (int i = NL.Count - 1; i >= 0; i--)
            {
                ProcessOutsideFloatNode(NL[i]);
            }

            ARTFootNode = MyXmlDocument.GetElementsByTagName("ce:article-footnote");
            if (ARTFootNode.Count > 0)
            {
                FnGroup = MyXmlDocument.CreateElement("fn-group");

                while (ARTFootNode.Count != 0)
                {
                    FnGroup.AppendChild(ARTFootNode[0]);
                }
                //for (int x = 0; x < ARTFootNode.Count; x++)
                //{
                //    FnGroup.AppendChild(ARTFootNode[x]);
                //}

             XmlNode HeadNode = MyXmlDocument.GetElementsByTagName("head")[0];

             if(HeadNode!= null)
                for (int i = HeadNode.ChildNodes.Count - 1; i >= 0; i--)
                { 
                    if (HeadNode.ChildNodes[i].Name.EndsWith("author-group"))
                    {
                        HeadNode.InsertBefore(FnGroup, HeadNode.ChildNodes[i]);
                        //HeadNode.InsertBefore(ARTFootNode[x], HeadNode.ChildNodes[i]);
                        //ARTFootNode[0].ParentNode.RemoveChild(ARTFootNode[0]);                       
                        break;
                    }
                    else if ((HeadNode.ChildNodes[i].Name.EndsWith("title")))
                    {
                        while(ARTFootNode.Count!=0)
                        {
                            HeadNode.InsertBefore(ARTFootNode[0], HeadNode.ChildNodes[i]);
                            ARTFootNode[0].ParentNode.RemoveChild(ARTFootNode[0]);
                        }
                        break;
                    }
                }
            }

            //if (MyXmlDocument.GetElementsByTagName("ce:author-group").Count > 0)
            //{
            //    XmlNode AuthorGroupNode = MyXmlDocument.GetElementsByTagName("ce:author-group")[0];

            //    for (int i =0;i<ARTFootNode.Count; i++)
            //    {
            //        AuthorGroupNode.ParentNode.InsertBefore(ARTFootNode[i], AuthorGroupNode);
            //        //AuthorGroupNode.ParentNode.RemoveChild(ARTFootNode[i]);
            //    }
            //    if (AuthorGroupNode != null)
            //    {
            //        MyXmlDocument.InnerXml = AuthorGroupNode.OwnerDocument.InnerXml;
            //    }
            //}

            //for (int i = ARTFootNode.Count - 1; i >= 0; i--)
            //{
            //    //ARTFootNode[i].ParentNode.RemoveChild(ARTFootNode[i]);
            //    //ProcessOutsideFloatNode(NL[i]);
            //}

            ////////////*******************Referenece conversion***************************************\\\\\\\\\\\\\\\\\\\\\\
            XmlElement ele;

            XmlNodeList Au_G_NodeList = MyXmlDocument.GetElementsByTagName("ce:author-group");
            if (Au_G_NodeList.Count > 0)
            {
                XmlNodeList NodeList = Au_G_NodeList[0].ChildNodes;
                for (int ChNodeCount = NodeList.Count - 1; ChNodeCount > 0; ChNodeCount--)
                {
                    if ("ce:author#ce:collaboration".IndexOf(NodeList[ChNodeCount].Name) != -1) /////////////break if node name match 
                    {
                        break;
                    }
                    NodeList[ChNodeCount].ParentNode.ParentNode.InsertAfter(NodeList[ChNodeCount], NodeList[ChNodeCount].ParentNode);
                }
            }

            //surname, given-names?, prefix?, suffix?
            XmlNodeList Au_NodeList = MyXmlDocument.GetElementsByTagName("ce:author");
            XmlNode SurName, GivenNames, Prefix, Suffix;
            if (Au_NodeList.Count > 0)
            {
                for (int i = 0; i < Au_NodeList.Count; i++)
                {
                    XmlNodeList NodeList = Au_NodeList[i].ChildNodes;
                    ele = MyXmlDocument.CreateElement("name");

                    SurName = Au_NodeList[i].SelectSingleNode(".//ce:surname", nsmgr);
                    GivenNames = Au_NodeList[i].SelectSingleNode(".//ce:given-name", nsmgr);
                    Prefix = Au_NodeList[i].SelectSingleNode(".//ce:initials", nsmgr);
                    Suffix = Au_NodeList[i].SelectSingleNode(".//ce:suffix", nsmgr);
                    if (SurName != null) ele.AppendChild(SurName);
                    if (GivenNames != null) ele.AppendChild(GivenNames);
                    if (Prefix != null) ele.AppendChild(Prefix);
                    if (Suffix != null) ele.AppendChild(Suffix);

                    if (ele.HasChildNodes)
                        Au_NodeList[i].PrependChild(ele);
                }
            }


            //<table-wrap-foot>
            //////////////////*********Add tableNotes  *******\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            NL = MyXmlDocument.GetElementsByTagName("tgroup");
            for (int i = 0; i < NL.Count; i++)
            {
                if (NL[i].NextSibling != null && !NL[i].NextSibling.Name.Equals("tgroup"))
                {
                    string TempStr = NL[i].ParentNode.InnerXml;
                    TempStr = TempStr.Replace("</tgroup>", "</tgroup><table-wrap-foot>");
                    TempStr = TempStr + "</table-wrap-foot>";
                    TempStr = TempStr.Replace("</tgroup><table-wrap-foot><tgroup", "</tgroup><tgroup");
                    NL[i].ParentNode.InnerXml = TempStr;
                }

                }

            FloatNode = MyXmlDocument.DocumentElement.SelectSingleNode("ce:floats", nsmgr);
            if (FloatNode != null)
            {
                FloatNode.ParentNode.RemoveChild(FloatNode);
                //MyXmlDocument.RemoveChild(FloatNode);
            }

            ItemInfoNode = MyXmlDocument.DocumentElement.GetElementsByTagName("item-info")[0];
            if (ItemInfoNode != null)
            {
                XmlNode DoiNode = ItemInfoNode.SelectSingleNode(".//ce:doi", nsmgr);
                if (DoiNode != null)
                {
                    Doi = DoiNode.InnerText;
                }

                XmlNode JIDNode = MyXmlDocument.DocumentElement.GetElementsByTagName("jid")[0];
                if (JIDNode != null)
                {
                    JID = JIDNode.InnerText;
                }
                XmlNode AIDNode = MyXmlDocument.DocumentElement.GetElementsByTagName("aid")[0];
                if (AIDNode != null)
                {
                    AID = AIDNode.InnerText;
                }

                CopyrightNode = MyXmlDocument.DocumentElement.GetElementsByTagName("ce:copyright")[0];
                
                ItemInfoNode.ParentNode.RemoveChild(ItemInfoNode);
            }

            ////////////*******************Start Referenece conversion***************************************\\\\\\\\\\\\\\\\\\\\\\
            XmlNodeList Ref_NodeList = MyXmlDocument.GetElementsByTagName("sb:reference");
            nsmgr.AddNamespace("sb", "http://www.elsevier.com/xml/common/struct-bib/dtd");
            int NodeListCount = Ref_NodeList.Count;
            int counter = 0; /////////Counter
            string[] JourREF = "sb:issue#sb:volume-nr#sb:first-page".Split('#');
            string[] BookREF = "sb:publisher#sb:location".Split('#');

            while (Ref_NodeList.Count > 0)
            {
                ele = MyXmlDocument.CreateElement("citation");

                foreach (string TestStr in JourREF)
                {
                    if (Ref_NodeList[counter].InnerXml.IndexOf(TestStr) != -1)
                    {
                        ele.SetAttribute("citation-type", "journal");
                    }
                    break;
                }
                if (ele.GetAttribute("citation-type").Equals(""))
                {
                    foreach (string TestStr in BookREF)
                    {
                        if (Ref_NodeList[counter].InnerXml.IndexOf(TestStr) != -1)
                        {
                            ele.SetAttribute("citation-type", "book");
                        }
                        break;
                    }
                }



                //XmlNode LblNode = Ref_NodeList[counter].SelectSingleNode(".//ce:label", nsmgr);
                //if (LblNode != null) ele.AppendChild(LblNode);

                XmlNodeList AuNodeList = Ref_NodeList[counter].SelectNodes(".//sb:author", nsmgr);
                XmlElement NameNode;
                XmlElement PersonGroupNode = MyXmlDocument.CreateElement("person-group");
                PersonGroupNode.SetAttribute("person-group-type", "author");
                for (int j = 0; j < AuNodeList.Count; j++)
                {
                    NameNode = MyXmlDocument.CreateElement("name");

                    SurName = AuNodeList[j].SelectSingleNode(".//ce:surname", nsmgr);
                    GivenNames = AuNodeList[j].SelectSingleNode(".//ce:given-name", nsmgr);
                    Prefix = AuNodeList[j].SelectSingleNode(".//ce:initials", nsmgr);
                    Suffix = AuNodeList[j].SelectSingleNode(".//ce:suffix", nsmgr);

                    if (SurName != null) NameNode.AppendChild(SurName);
                    if (GivenNames != null) NameNode.AppendChild(GivenNames);
                    if (Prefix != null) NameNode.AppendChild(Prefix);
                    if (Suffix != null) NameNode.AppendChild(Suffix);

                    if (NameNode.HasChildNodes)
                        PersonGroupNode.AppendChild(NameNode);
                }

                if (PersonGroupNode.HasChildNodes)
                {

                    if (AuNodeList[AuNodeList.Count-1].NextSibling!= null)
                    {
                        if (AuNodeList[AuNodeList.Count - 1].NextSibling.Name.Equals("sb:et-al"))
                        {
                            PersonGroupNode.AppendChild(AuNodeList[AuNodeList.Count - 1].NextSibling);
                        }
                    }
                    ele.AppendChild(PersonGroupNode);
                }
                else
                {
                }

                AuNodeList = Ref_NodeList[counter].SelectNodes(".//sb:editor", nsmgr);
                PersonGroupNode = MyXmlDocument.CreateElement("person-group");
                PersonGroupNode.SetAttribute("person-group-type", "editor");
                for (int j = 0; j < AuNodeList.Count; j++)
                {
                    NameNode = MyXmlDocument.CreateElement("name");

                    SurName = AuNodeList[j].SelectSingleNode(".//ce:surname", nsmgr);
                    GivenNames = AuNodeList[j].SelectSingleNode(".//ce:given-name", nsmgr);
                    Prefix = AuNodeList[j].SelectSingleNode(".//ce:initials", nsmgr);
                    Suffix = AuNodeList[j].SelectSingleNode(".//ce:suffix", nsmgr);

                    if (SurName != null) NameNode.AppendChild(SurName);
                    if (GivenNames != null) NameNode.AppendChild(GivenNames);
                    if (Prefix != null) NameNode.AppendChild(Prefix);
                    if (Suffix != null) NameNode.AppendChild(Suffix);

                    if (NameNode.HasChildNodes)
                        PersonGroupNode.AppendChild(NameNode);
                }

                if (PersonGroupNode.HasChildNodes)
                {
                    if (AuNodeList[AuNodeList.Count - 1].NextSibling != null)
                    {
                        if (AuNodeList[AuNodeList.Count - 1].NextSibling.Name.Equals("sb:et-al"))
                        {
                            PersonGroupNode.AppendChild(AuNodeList[AuNodeList.Count - 1].NextSibling);
                        }
                    }
                    ele.AppendChild(PersonGroupNode);
                }
                else
                { }

                XmlNodeList NodeList = Ref_NodeList[counter].SelectNodes(".//sb:maintitle", nsmgr);
                XmlElement tempEle;

                if (NodeList.Count == 2)
                {
                    tempEle = MyXmlDocument.CreateElement("article-title");
                    tempEle.InnerXml = NodeList[0].InnerXml;
                    NodeList[0].ParentNode.ReplaceChild(tempEle, NodeList[0]);

                    tempEle = MyXmlDocument.CreateElement("source");
                    tempEle.InnerXml = NodeList[1].InnerXml;
                    NodeList[1].ParentNode.ReplaceChild(tempEle, NodeList[1]);
                }
                else if (NodeList.Count == 1)
                {
                    //if (NodeList[0].ParentNode.ParentNode.NextSibling.FirstChild != null)
                    //{ 
                    //    if (NodeList[0].ParentNode.ParentNode.NextSibling.FirstChild.Name.Equals("sb:book"))

                    //}

                    tempEle = MyXmlDocument.CreateElement("article-title");
                    tempEle.InnerXml = NodeList[0].InnerXml;
                    NodeList[0].ParentNode.ReplaceChild(tempEle, NodeList[0]);
                }
                else
                {
                }

                //publisher-loc | publisher-name
                NodeList = Ref_NodeList[counter].SelectNodes(".//*");

                for (int j = 0; j < NodeList.Count; j++)
                {
                    //Console.WriteLine(NodeList[j].Name);
                    if (NodeList[j].FirstChild == null)
                    { }
                    else if (NodeList[j].Name.Equals("sb:publisher"))
                    {
                        ele.AppendChild(NodeList[j]);
                    }
                    else if ("sb:location#sb:name".IndexOf(NodeList[j].Name) != -1)
                    {
                        //
                    }
                    else if (NodeList[j].FirstChild.NodeType == XmlNodeType.Text)
                    {
                        ele.AppendChild(NodeList[j]);
                    }
                    else if (NodeList[j].FirstChild.Name.StartsWith("ce:"))
                    {
                        ele.AppendChild(NodeList[j]);
                    }
                }

                //XmlElement RefNode = MyXmlDocument.CreateElement("ref");
                //if (Ref_NodeList[counter].Attributes.GetNamedItem("id") != null)
                //{
                //    RefNode.SetAttribute("id", Ref_NodeList[counter].Attributes.GetNamedItem("id").Value);
                //}
                //if (ele.FirstChild.Name.Equals("ce:label"))
                //    RefNode.AppendChild(ele.FirstChild);

                //RefNode.AppendChild(ele);
                Ref_NodeList[counter].ParentNode.ReplaceChild(ele, Ref_NodeList[counter]);
            }
            ////////////*******************End Referenece conversion***************************************\\\\\\\\\\\\\\\\\\\\\\

            ///////////////////Start move ce:acknowledgment to tail/////////////////////////////////////
            XmlNode AckNode = MyXmlDocument.DocumentElement.SelectSingleNode(".//ce:acknowledgment", nsmgr);
            ///////////////////End move ce:acknowledgment to tailt/////////////////////////////////////

            if (AckNode != null)
            {
                if (AckNode.NextSibling != null && AckNode.NextSibling.Name.Equals("ce:section"))
                {
                    XmlNode Sec = xmlDocument.CreateElement("section");
                    while (AckNode.HasChildNodes)
                    {
                        Sec.AppendChild(AckNode.FirstChild);
                    }

                    AckNode.ParentNode.ReplaceChild(Sec, AckNode);
                }
                else
                {
                    XmlNodeList TailNode = MyXmlDocument.GetElementsByTagName("tail");

                    if (TailNode.Count > 0)
                    {
                        TailNode[0].PrependChild(AckNode);
                    }
                }
            }

            ///////////////////Start move ce:appendices to tail/////////////////////////////////////
            XmlNode APPNode = MyXmlDocument.DocumentElement.SelectSingleNode(".//ce:appendices", nsmgr);
            ///////////////////End move ce:acknowledgment to tailt/////////////////////////////////////

            if (APPNode != null)
            {
                XmlNodeList TailNode = MyXmlDocument.GetElementsByTagName("tail");

                if (TailNode.Count > 0)
                {
                    TailNode[0].PrependChild(APPNode);
                }
                else
                {
                    XmlElement TailEle = MyXmlDocument.CreateElement("tail");
                    TailEle.PrependChild(APPNode);
                    MyXmlDocument.DocumentElement.AppendChild(TailEle);
                }
            }


            XmlNodeList Correspondence = MyXmlDocument.GetElementsByTagName("ce:correspondence");
            XmlElement AuthorNotes = MyXmlDocument.CreateElement("author-notes");
            XmlNode CorrespondencePrvusNode = null;
            if (Correspondence.Count > 0)
                CorrespondencePrvusNode = Correspondence[0].PreviousSibling;
            
            while (Correspondence.Count > 0)
                 AuthorNotes.AppendChild(Correspondence[0]);

            if (AuthorNotes!=null && CorrespondencePrvusNode!= null)
            {
                CorrespondencePrvusNode.ParentNode.InsertAfter(AuthorNotes,CorrespondencePrvusNode);
            }
        }
      
        private void ProcessOutsideFloatNode(XmlNode node)
        {
            if (node.ParentNode.Name.Equals("ce:para"))
            {
                node.ParentNode.ParentNode.InsertAfter(node, node.ParentNode);
            }
            return;
        }
        private void ProcessFloatNode(string refid)
        {
            if (FloatNode.HasChildNodes)
            {
                XmlNodeList FloatNL;
                FloatNL = FloatNode.ChildNodes;
                int NodeCount = FloatNL.Count;
                for (int i = 0; i < NodeCount; i++)
                {
                    if (FloatNL[i].NodeType == XmlNodeType.Element)
                    {
                        if (FloatNL[i].Attributes.Count > 0)
                        {
                            if (FloatNL[i].Attributes[0].Value.Equals(refid))
                            {
                                SearchNode(FloatNL[i]);
                                break;
                            }
                        }
                        else if (FloatNL[i].Name.Equals("tabular"))
                        {
                            if (FloatNL[i].FirstChild.Attributes.Count > 0)
                                if (FloatNL[i].FirstChild.Attributes[0].Value.Equals(refid))
                                {
                                    SearchNode(FloatNL[i]);
                                    break;
                                }
                        }
                    }
                }
            }
        }
        private void WriteTargetAttribute(XmlNode node)
        {
            //ref-types aff                   | app   | author-notes | bibr | boxed-text | chem  | contrib | corresp | 
            //          disp-formula          | fig   | fn           | kwd  | list       | plate | scheme  | sec | statement | 
            //         supplementary-material | table | table-fn     |  other                                      


            

            if (node.Attributes[0] == null) return;

            if (node.Attributes.GetNamedItem("id") != null)
            {
                node.Attributes.Remove((XmlAttribute)node.Attributes.GetNamedItem("id"));
            }

            string Id = node.Attributes[0].Value;

            //ref-type="aff" rid="au1"
            textWriter.WriteAttributeString("rid", Id);

            if (Id.StartsWith("eq"))
                textWriter.WriteAttributeString("ref-type", "other");
            else if (Id.StartsWith("fig") || Id.StartsWith("sch"))
                textWriter.WriteAttributeString("ref-type", "fig");
            else if (Id.StartsWith("tbl"))
                textWriter.WriteAttributeString("ref-type", "table");
            else if (Id.StartsWith("bib"))
                textWriter.WriteAttributeString("ref-type", "bibr");
            else if (Id.StartsWith("tb"))
                textWriter.WriteAttributeString("ref-type", "boxed-text");
            else if (Id.StartsWith("aff"))
                textWriter.WriteAttributeString("ref-type", "aff");

        }

        public static void GetAllMappingTextFile()
        {
            ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string FileName = ExeLoc + "\\Map.txt";

            if (!File.Exists(FileName))
            {
                Console.WriteLine(FileName + " does not exist");
                Console.WriteLine("Press any key to continue..");
                Console.ReadLine();
            }

            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                string[] splitStr;
                while ((line = sr.ReadLine()) != null)
                {
                    splitStr = line.Split('\t');
                    if (splitStr.Length > 1)
                    {
                        if (!Mapping.ContainsKey(splitStr[0]))
                        {
                            Mapping.Add(splitStr[0], splitStr[1]);
                        }
                    }
                }
            }

            FileName = ExeLoc + "\\ArticleType.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                string[] splitStr;
                while ((line = sr.ReadLine()) != null)
                {
                    splitStr = line.Split('\t');
                    if (splitStr.Length > 1)
                    {
                        if (!Mapping.ContainsKey(splitStr[0]))
                        {
                            Mapping.Add(splitStr[0], splitStr[1]);
                        }
                    }
                }
            }
            string SectitleFile = ExeLoc + "\\Sectitle.txt";
            if (!File.Exists(SectitleFile))
            {
                Console.WriteLine("Required file missing.");
                Console.WriteLine("Please check path: ");
                Console.Write(SectitleFile);

                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                Environment.Exit(0);
                return;
            }

            TitleArr = File.ReadAllLines(SectitleFile);

            

            /////////////////Read Entity File////////////////////////////////
            FileName = ExeLoc + "\\HexEntities.txt";
            string[] FileLines = File.ReadAllLines(FileName);
            string[] SplitText={""};
            string Key="";
            foreach (string line in FileLines)
            {
                    if (line.IndexOf("\t")!=-1)
                        SplitText = line.Split('\t');
                    else if (line.IndexOf(" ") != -1)
                        SplitText = line.Split(' ');

                    if (SplitText.Length == 2)
                    {
                        if (SplitText[0].StartsWith("&") && SplitText[0].EndsWith(";"))
                            Key = SplitText[0];
                        else
                            Key = "&" + SplitText[0] + ";";

                        if (!Entity.ContainsKey(Key))
                            Entity.Add(Key, SplitText[1]);
                    }
             }
        }
        
        public bool XmlParsing(string str)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName  = ValidationPath + "\\Parse.BAT";
                myProcess.StartInfo.Arguments = "\"" + str + "\"";
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                Console.WriteLine("Process strat to Parse Xml file................");
                myProcess.Start();
                myProcess.WaitForExit();

                string LogFIle = str + ".err";

                FileInfo FInfo = new FileInfo(LogFIle);
                if (FInfo.Length == 0)
                {
                    File.Delete(LogFIle);
                    File.WriteAllText(str, File.ReadAllText(str).Replace(Program.NLMDTDPath + "\\", "").Replace("dtd\"[]", "dtd\"").Replace("\t",""));
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

        private void BrowseXml()
        {

            if (!Directory.Exists(ExeLoc + "\\browser"))
            {
                Console.WriteLine("Browser folder does not exist.");
                Console.WriteLine("Check this location:" + ExeLoc + "\\browser");
                return;
            }
            string xmlStr = "<?xml version=\"1.0\"?>"
            + "\n" + "<?xml-stylesheet type=\"text/xsl\" href=\"" + ExeLoc + "\\browser\\ViewNLM-v2.3.xsl\"?>"
            + "\n" + "<article xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:mml=\"http://www.w3.org/1998/Math/MathML\">";


            StringBuilder nXmlStr = new StringBuilder(File.ReadAllText(NLMFileName));
            int sPos = nXmlStr.ToString().IndexOf("<front>");

            if (sPos != -1)
            {
                xmlStr = xmlStr + nXmlStr.ToString().Substring(sPos);
            }
            string BrowseFileName = ExeLoc + @"\browser\" + Path.GetFileNameWithoutExtension(NLMFileName) + ".xml";
            File.WriteAllText(BrowseFileName, xmlStr);

            Process.Start("iexplore", BrowseFileName);

            //  MyXmlDocument.DocumentElement.FirstChild
        }

        private void TableAttriibute(XmlNode node)
        {
            for (int X = 0; X < node.Attributes.Count; X++)
            {
                if (node.Attributes[X].Name.Equals("border"))
                {
                    textWriter.WriteAttributeString("border", node.Attributes[X].Value);
                }
                else if (node.Attributes[X].Name.Equals("rules"))
                {
                    textWriter.WriteAttributeString("rules", node.Attributes[X].Value);
                }
                else if (node.Attributes[X].Name.Equals("frame"))
                {
                    if (node.Attributes[X].Value.Equals("topbot"))
                    {
                        //frame="above" rules="groups" border="0"
                        textWriter.WriteAttributeString("frame", "above");
                        textWriter.WriteAttributeString("rules", "groups");
                        textWriter.WriteAttributeString("border", "0");
                    }
                    else if (node.Attributes[X].Value.Equals("all"))
                    {
                        textWriter.WriteAttributeString("frame", "border");
                    }
                    else if (node.Attributes[X].Value.Equals("top"))
                    {
                        textWriter.WriteAttributeString("frame", "above");
                        textWriter.WriteAttributeString("rules", "none");
                        textWriter.WriteAttributeString("border", "0");
                    }
                    else if (node.Attributes[X].Value.Equals("bottom"))
                    {
                        textWriter.WriteAttributeString("frame", "below");
                        textWriter.WriteAttributeString("rules", "groups");
                        textWriter.WriteAttributeString("border", "0");
                    }
                    //if (node.Attributes[X].Value.Equals("none"))
                    //{
                    //    textWriter.WriteAttributeString("frame", "void");
                    //    //textWriter.WriteAttributeString("rules", "none");
                    //    //textWriter.WriteAttributeString("border", "0");
                    //}
                    //else
                    //{
                    //    textWriter.WriteAttributeString("frame", node.Attributes[X].Value);
                    //}
                }
            }
        }

        private void SecAttribute(XmlNode Node)
        {
            if (Node.Attributes.GetNamedItem("id")!=null)
            {
                string AtrVal= Node.Attributes.GetNamedItem("id").Value;
                textWriter.WriteAttributeString("id", AtrVal);
            }

            if (Node.FirstChild.Name.Equals("ce:section-title"))
            {
                foreach (string TItle in TitleArr)
                {
                    string[] ARR = TItle.Split('\t');
                    if (Node.FirstChild.InnerText.StartsWith(ARR[0], StringComparison.OrdinalIgnoreCase))
                    {
                        textWriter.WriteAttributeString("sec-type", ARR[1]);
                        break;
                    }
                }
            }
        }
    }
    
    class XmlInfo
    {
        XmlDocument MyXmlDocument = new XmlDocument();
        XmlReader Reader;
        XmlReaderSettings ReaderSettings = new XmlReaderSettings();
        string ExeLoc = ""; /////////Exe Location
        string processFilePath = "";
        string XmlFilePath = "";
        string dtdPath = "";
        string DocumentElement = "";

        bool isParsingRequired = false;
        bool preserveSpace = false;

        public XmlInfo()
        {
        }
       


        public void SaveXml()
        {
            if (!XmlFilePath.Equals(""))
            {
                MyXmlDocument.Save(XmlFilePath.Replace(".xml", "_edt.xml"));
            }
        }
        public XmlInfo(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
        }
        public XmlDocument xmlDocument
        {
            get
            {
                return MyXmlDocument;
            }
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
            get { return XmlFilePath; }
            set
            {
                if (value.Equals(""))
                    throw new Exception("Empty string");
                else if (!File.Exists(value))
                    throw new FileNotFoundException();
                else
                    XmlFilePath = value;
            }
        }
        public string ProcessFilePath
        {
            get { return processFilePath; }

        }
        public string DtdPath
        {
            get { return dtdPath; }
            set
            {
                if (value.Equals(""))
                    throw new Exception("Empty string");
                else if (!File.Exists(dtdPath))
                    throw new FileNotFoundException();
                else
                    dtdPath = value;
            }
        }
        public string DocumentElementName
        {
            get { return DocumentElement; }
        }
        public bool IsParsingRequired
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
        public bool PreserveSpace
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
        private void SetReaderSettings()
        {
            ReaderSettings.IgnoreComments = false;
            ReaderSettings.IgnoreWhitespace = false;

            if (dtdPath.Equals(""))
            {
                ReaderSettings.ValidationType = ValidationType.None;
                ReaderSettings.ProhibitDtd = false;
            }
            else
            {
                ReaderSettings.ProhibitDtd = true;
                ReaderSettings.ValidationType = ValidationType.DTD;
            }
        }
        private bool LoadXmlFile()
        {
            try
            {
                            ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string tempXMlPath = ExeLoc + "\\temp.xml";


                /////Read Xml file
                StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));

                /////Start Make required changes in temp Xml file

                xmlStr.Replace("&", "#$#");
                xmlStr.Replace("<td:", "<ce:");

                xmlStr.Replace("td:style=", "ce:style=");
                xmlStr.Replace("td:refstyle=", "ce:refstyle=");
                xmlStr.Replace("td:aid=", "ce:aid=");
                xmlStr.Replace("td:mode", "ce:mode");
                xmlStr.Replace("\" notreq=\"", " ");

                xmlStr.Replace("<tp:", "<sb:");

                xmlStr.Replace("</td:", "</ce:");
                xmlStr.Replace("</tp:", "</sb:");

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
                            string dtdLoc = ExeLoc + @"\DTD\TDML\art501.dtd";
                            if (!File.Exists(dtdLoc))
                            {
                                Console.WriteLine("DTD could not found");
                                Console.WriteLine("Please check this path: " + dtdLoc);
                                Console.WriteLine("Press any key to exit");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                            xmlStr.Replace(str, dtdLoc);
                        }
                    }
                }

                /////Write changes in temp Xml file
                xmlStr = xmlStr.Replace("<article version=\"1.0\"", "<article version=\"5.0\"");
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

                Console.WriteLine("Error :: " + ex.Message);
                Console.WriteLine("Prees any key to exit..");
                Console.ReadLine();
                return false;
            }
            finally
            {
                Reader.Close();
            }

            return true;
        }
        public bool MakeNLM( JLEDATASET.Xml2HTML  Xml2HTMLObj)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.InnerXml    = MyXmlDocument.InnerXml;


                //Xml2HTMLObj.IndexInfo.PageDebut, Xml2HTMLObj.IndexInfo.PageFin, Xml2HTMLObj.IndexInfo.DateParu, Xml2HTMLObj.IndexInfo.Gratuit

                Xml2NLM Xml2NLMObj    = new Xml2NLM(xmlDoc);
                Xml2NLMObj.sPage      = Xml2HTMLObj.IndexInfo.PageDebut;
                Xml2NLMObj.lPage      = Xml2HTMLObj.IndexInfo.PageFin;
                Xml2NLMObj.PubDate    = Xml2HTMLObj.IndexInfo.DateParu;
                Xml2NLMObj.JLELibParu = Xml2HTMLObj.IndexInfo.LibParu;
                Xml2NLMObj.JLELibSomm = Xml2HTMLObj.IndexInfo.LibSomm;

                if (Xml2HTMLObj.IndexInfo.Gratuit.Equals("1"))
                    Xml2NLMObj.isFree = true;

                Xml2NLMObj._NLMFileName = XmlFilePath.Replace(".xml", ".nxml");
                Xml2NLMObj.MakeXNLM();
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Line No :"  + ex.LineNumber);
                Console.WriteLine("Position :" + ex.LinePosition);
                Console.WriteLine(ex.Message);

                return false;
            }
            return true;
        }
        private bool LoadXmlFile1()
        {
            try
            {
                Reader = XmlReader.Create(XmlFilePath, ReaderSettings);
                MyXmlDocument = new XmlDocument();
                if (preserveSpace)
                    MyXmlDocument.PreserveWhitespace = true;

                MyXmlDocument.Load(Reader);
                Reader.Close();
            }
            catch (XmlException ex)
            {
                return false;
            }
            return true;
        }
        public bool LoadXml()
        {
            try
            {
                if (!XmlFilePath.Equals(""))
                {
                    SetReaderSettings();
                    if (LoadXmlFile())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (XmlException ex)
            {
                return false;
            }
        }
    }
}
