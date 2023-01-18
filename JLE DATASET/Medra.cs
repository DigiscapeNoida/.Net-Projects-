using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace JLEDATASET
{
    public class Medra
    {
        string _sPage = "";
        string _ePage = "";
        string _Language = "";
        string _DocHead = "";
        string ENArticleTitle = "";
        string FRArticleTitle = "";
        string _JID = "";
        string _AID = "";
        string _DOI = "";
        string _InPutPath = "";
        string _TargetPath = "";

        static StringBuilder ErrorLog = new StringBuilder();
        static bool isValid = false;
        private static int ErrCount = 0;

        XmlNamespaceManager nsmgr = null;
        XmlTextWriter       textWriter;
        XmlDocument         MyXmlDocument = new XmlDocument();

        private bool XmlLoad(string XmlFilePath)
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

        public Medra(string ProcessDirectoryPath, string InputDirectoryPath)
        {
            _InPutPath = ProcessDirectoryPath;
            _TargetPath = InputDirectoryPath;
        }

        public void GetDetails(string XmlFilePath)
        {
            nsmgr = new XmlNamespaceManager(MyXmlDocument.NameTable);
            nsmgr.AddNamespace("td", "http://www.thomsondigital.com/xml/common/dtd");
            nsmgr.AddNamespace("sb", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
            nsmgr.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            nsmgr.AddNamespace("tb", "http://www.thomsondigital.com/xml/common/table/dtd");
            nsmgr.AddNamespace("tp", "http://www.thomsondigital.com/xml/common/struct-bib/dtd");



            string ArticleFolderPath = Path.GetFileName(Path.GetDirectoryName(XmlFilePath));
            string[] PageRange = ArticleFolderPath.Split('-');

            _sPage = PageRange[0].TrimStart('0');
            _ePage = PageRange[1].TrimStart('0');

            ////////////Do'nt change sequence
            XmlLoad(XmlFilePath);

            XmlNode Node = MyXmlDocument.GetElementsByTagName("aid")[0];
            if (Node != null)
                _AID = Node.InnerText;

            Node = MyXmlDocument.GetElementsByTagName("jid")[0];
            if (Node != null)
                _JID = Node.InnerText;

            Node = MyXmlDocument.GetElementsByTagName("td:doi")[0];
            _DOI = Node.InnerText;


            GetArticleTitle();
            if (ENArticleTitle.Equals(""))
            {
                GetDochead(nsmgr);
                ENArticleTitle = _DocHead;
            }
            GetArticleLanguage();
            
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
                        ENArticleTitle = NL[0].InnerText;
                }
            }
            else
            {
                string ENGJID = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ENGJID.txt";
                if (File.Exists(ENGJID))
                {
                    ENGJID = File.ReadAllText(ENGJID);
                    if (ENGJID.IndexOf(_JID) != -1)
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

        private void GetDochead(XmlNamespaceManager nsmgr)
        {
            XmlNodeList nodeList = MyXmlDocument.GetElementsByTagName("td:dochead");
            if (nodeList.Count > 0)
            {
                XmlNodeList textfn = nodeList[0].SelectNodes(".//td:textfn", nsmgr);
                if (textfn != null)
                {
                    //ProcessTitle(textfn[0]);
                    _DocHead = textfn[0].InnerXml;
                }
                else
                { 
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

        public void WriteCrossrefXML()
        {
            //MEDRA_JID_VOLUME_ISSUE
            //string FileName = "BAUDOUINREGID_" + IssueDetails.Year + "_en.xml";

            string FileName = "Crossref_" + IssueDetails.JID + "_" + IssueDetails.Volume + "_" + IssueDetails.ISSUE + ".xml";
            //JID_VOLUME_ISSUE
            string CrossrefXml = Path.GetDirectoryName(_TargetPath) + "\\" + FileName;
            textWriter = new XmlTextWriter(CrossrefXml, Encoding.UTF8);

            textWriter.Indentation = 1;
            textWriter.IndentChar = '\t';
            textWriter.Formatting = Formatting.Indented;


            textWriter.WriteStartDocument();  

            textWriter.WriteStartElement("doi_batch");
            textWriter.WriteAttributeString("xmlns", "http://www.crossref.org/schema/4.4.1");
            textWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            textWriter.WriteAttributeString("xsi:schemaLocation", "http://www.crossref.org/schema/4.4.1 http://www.crossref.org/schema/deposit/crossref4.4.1.xsd");
            textWriter.WriteAttributeString("version", "4.4.1");
            //textWriter.WriteAttributeString("xsi:noNamespaceSchemaLocation", "ONIX_DOIMetadata_1.1.xsd");
            string CurrentDate = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
            textWriter.WriteStartElement("head");
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            textWriter.WriteElementString("doi_batch_id", IssueDetails.JID + "-" + IssueDetails.Volume + "-" + IssueDetails.ISSUE + "-" + g.ToString());
            //textWriter.WriteElementString("timestamp", DateTime.Now.ToString().Replace("-","").Replace(":","").Replace(" ","").Replace("AM","").Replace("PM","").Replace(".", ""));
            textWriter.WriteElementString("timestamp", Regex.Replace(DateTime.Now.ToString(), @"[^0-9]+", ""));
            textWriter.WriteStartElement("depositor");
            textWriter.WriteElementString("depositor_name", "John Libbey Eurotext");
            textWriter.WriteElementString("email_address", "veema.mohun@thomsondigital.com");
            textWriter.WriteEndElement();
            textWriter.WriteElementString("registrant", "John Libbey Eurotext");
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("body");

            string[] XmlFiles = Directory.GetFiles(_InPutPath, "*.xml", SearchOption.AllDirectories);
            Array.Sort(XmlFiles);

            foreach (string XMLFile in XmlFiles)
            {
                Console.WriteLine("Process xml file :" + XMLFile);
                WriteDOISerialArticleWorkCrossref(XMLFile, IssueDetails.JID);
                //WriteDOISerialArticleWork(XMLFile);
            }

            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();

            textWriter.Flush();
            textWriter.Close();

            FinalFilteration(CrossrefXml);

            //Console.WriteLine("Validation start of mEDRA xml.");
            //WriteErrorLog(MedraXml);
            //StringBuilder XmlStr = new StringBuilder(File.ReadAllText(MedraXml));
            //XmlStr.Replace("\r", "");
            //File.WriteAllText(MedraXml, XmlStr.ToString());
        }


        public  void WriteMedraXML()
        {
           //MEDRA_JID_VOLUME_ISSUE
           //string FileName = "BAUDOUINREGID_" + IssueDetails.Year + "_en.xml";

            string FileName = "MEDRA_" +  IssueDetails.JID +  "_" + IssueDetails.Volume+ "_"+ IssueDetails.ISSUE+".xml";
            //JID_VOLUME_ISSUE
            string MedraXml = Path.GetDirectoryName (_TargetPath) + "\\" + FileName;
            textWriter = new XmlTextWriter(MedraXml, Encoding.UTF8);

            textWriter.Indentation = 1;
            textWriter.IndentChar = '\t';
            textWriter.Formatting = Formatting.Indented;


            textWriter.WriteStartDocument();

            textWriter.WriteStartElement("ONIXDOISerialArticleWorkRegistrationMessage");
            textWriter.WriteAttributeString("xmlns","http://www.editeur.org/onix/DOIMetadata/1.1");
            textWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            textWriter.WriteAttributeString("xsi:schemaLocation", "http://www.editeur.org/onix/DOIMetadata/1.1 http://www.medra.org/schema/onix/DOIMetadata/1.1/ONIX_DOIMetadata_1.1.xsd");
            //textWriter.WriteAttributeString("xsi:noNamespaceSchemaLocation", "ONIX_DOIMetadata_1.1.xsd");
            WriteHeaderElement();

            string[] XmlFiles = Directory.GetFiles(_InPutPath, "*.xml", SearchOption.AllDirectories);
            Array.Sort(XmlFiles);

            foreach (string XMLFile in XmlFiles)
            {
                Console.WriteLine("Process xml file :" + XMLFile);
                WriteDOISerialArticleWorkCrossref(XMLFile, IssueDetails.JID);
                //WriteDOISerialArticleWork(XMLFile);
            }

            textWriter.WriteEndDocument();

            textWriter.Flush();
            textWriter.Close();

            FinalFilteration(MedraXml);

            Console.WriteLine("Validation start of mEDRA xml.");
            WriteErrorLog(MedraXml);
            //StringBuilder XmlStr = new StringBuilder(File.ReadAllText(MedraXml));
            //XmlStr.Replace("\r", "");
            //File.WriteAllText(MedraXml, XmlStr.ToString());
        }

        private void WriteHeaderElement()
        {
                string CurrentDate = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
                textWriter.WriteStartElement ("Header");
                textWriter.WriteElementString("FromCompany", "Thomson Digital (Mauritius) ltd");
                textWriter.WriteElementString("FromEmail", "veema.mohun@thomsondigital.com");
		        textWriter.WriteElementString("ToCompany","mEDRA");
                textWriter.WriteElementString("SentDate", CurrentDate);
	            textWriter.WriteEndElement();
       }

        public DataTable ReadExcel(string fileName, string fileExt, string jid)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [ListMediaType$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }

        public DataTable GetISSNANDMediaType(string jid)
        {
            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string MetaData = ExeLoc + "\\MetaData.xml";
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MediaType"));
            dt.Columns.Add(new DataColumn("ISSN"));
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(MetaData, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            xmlnode = xmldoc.GetElementsByTagName(jid);
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                foreach(XmlNode child in xmlnode[i].SelectNodes("issn"))
                {
                    DataRow dr = dt.NewRow();
                    dr["MediaType"] = child.Attributes["pub-type"].Value.ToString(); //xmlnode[i].ChildNodes.Item(z).Attributes["pub-type"].Value.ToString();
                    dr["ISSN"] = child.InnerText.Trim().ToString(); //xmlnode[i].ChildNodes.Item(z).InnerText.Trim().ToString();
                    dt.Rows.Add(dr);
                }
                
                //for (int z = 0; z <= xmlnode[i].ChildNodes.Count; z++)
                //{
                //    if (String.IsNullOrEmpty(xmlnode[i].ChildNodes.Item(z).Name.ToString()))
                //    {
                //        if (xmlnode[i].ChildNodes.Item(z).Name.ToString() == "issn")
                //        {
                //            DataRow dr = dt.NewRow();
                //            dr["MediaType"] = xmlnode[i].ChildNodes.Item(z).Attributes["pub-type"].Value.ToString();
                //            dr["ISSN"] = xmlnode[i].ChildNodes.Item(z).InnerText.Trim().ToString();
                //            dt.Rows.Add(dr);
                //        }
                //    }
                //    //xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                //    //str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                //}
            }
            return dt;

        }

        public void WriteDOISerialArticleWorkCrossref(string XMLFile, string JID)
        {
            string excelName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\MediaType.xls";
            DataTable dt = GetISSNANDMediaType(JID);
            DataTable dt1 = ReadExcel(excelName, ".xls", JID);
            bool electronic = false;
            bool print = false;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["Revues"].ToString() == JID)
                {
                    if (dt1.Rows[i]["Eonly"].ToString() == "YES")
                    {
                        electronic = true;
                    }
                    if (dt1.Rows[i]["Print"].ToString() == "YES")
                    {
                        print = true;
                    }
                }
            }

            //string _FirstName = "";
            //string _LastName = "";
            //string Suffix = "";
            //string Prefix = "";
            //string node = "";
            //string eMail = "";
            //string Degrees = "";
            //DataTable dt = new DataTable();
            //dt = ReadExcel("","");

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
            GetDetails(XMLFile);
                textWriter.WriteStartElement("journal");
                    textWriter.WriteStartElement("journal_metadata");
                    string JT = GetJournalTitle(_JID);
                    textWriter.WriteElementString("full_title", JT);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        textWriter.WriteStartElement("issn");
                        textWriter.WriteAttributeString("media_type", dt.Rows[i]["MediaType"].ToString() == "ppub" ? "print" : "electronic");
                        textWriter.WriteString(dt.Rows[i]["ISSN"].ToString());
                        textWriter.WriteEndElement();
                    }
                        textWriter.WriteEndElement();
                    textWriter.WriteStartElement("journal_issue");
                        textWriter.WriteStartElement("publication_date");
                        if (electronic == true)
                        {
                            textWriter.WriteAttributeString("media_type", "electronic");
                        }
                        if (print == true)
                        {
                            textWriter.WriteAttributeString("media_type", "print");
                        }
                        String monthwithzero = "";
            String[] month = IssueDetails.Month.Split('-');
            if(month.Length == 2) {
            monthwithzero = month[1].ToString().Length == 1 ? "0" + month[1].ToString() : month[1].ToString();}
            if (month.Length == 1)
            {
                monthwithzero = month[0].ToString().Length == 1 ? "0" + month[0].ToString() : month[0].ToString();
            }
            textWriter.WriteElementString("month", monthwithzero);
            
            textWriter.WriteElementString("year", IssueDetails.Year);
                        textWriter.WriteEndElement();
                        textWriter.WriteStartElement("journal_volume");
                            textWriter.WriteElementString("volume", IssueDetails.Volume);
                        textWriter.WriteEndElement();
                        textWriter.WriteElementString("issue", IssueDetails.ISSUE);
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("journal_article");
                    if (_Language.Equals("FR", StringComparison.OrdinalIgnoreCase))
                        textWriter.WriteAttributeString("language", "fr");
                    else
                        textWriter.WriteAttributeString("language", "en");
                        textWriter.WriteStartElement("titles");
                        textWriter.WriteElementString("title", ENArticleTitle);
                        textWriter.WriteEndElement();
                            
                                string _FirstName = "";
                                string _LastName = "";
                                string Prefix = "", Suffix = "", Degrees = "", eMail = "";
                                XmlNodeList AuthorNL = MyXmlDocument.GetElementsByTagName("td:author");
                                if (AuthorNL.Count > 0)
                                {

                                    textWriter.WriteStartElement("contributors");
                                    for (int X = 0; X < AuthorNL.Count; X++)
                                    {
                                        textWriter.WriteStartElement("person_name");
                                        textWriter.WriteAttributeString("contributor_role", "author");
                                        textWriter.WriteAttributeString("sequence", "additional");
                                        XmlNode node = AuthorNL[X];
                                        _FirstName = "";
                                        _LastName = "";
                                        Prefix = "";
                                        Suffix = "";
                                        Degrees = "";
                                        for (int i = 0; i < node.ChildNodes.Count; i++)
                                        {
                                            XmlNode chnode = node.ChildNodes[i];
                                            if (!chnode.Equals("") && chnode.NodeType == XmlNodeType.Element)
                                                chnode.InnerXml = chnode.InnerXml.Replace("#$#lt;sup#$#gt;a#$#lt;/sup#$#gt;", "#$##x00AA;");
                                            if (chnode.Name.Equals("td:e-address"))
                                                eMail = chnode.InnerText;
                                            else if (chnode.Name.Equals("td:suffix"))
                                                Suffix = chnode.InnerXml;
                                            else if (chnode.Name.Equals("td:initials"))
                                                Prefix = chnode.InnerXml;
                                            else if (chnode.Name.Equals("td:degrees"))
                                                Degrees = chnode.InnerXml;
                                            else if (chnode.Name.Equals("td:given-name"))
                                            {
                                                if (chnode.ChildNodes.Count > 1)
                                                {
                                                    foreach (XmlNode temp in chnode)
                                                    {
                                                        if (temp.Name.Equals("td:small-caps"))
                                                        {
                                                            _FirstName += temp.InnerText.ToUpper();
                                                        }
                                                        else if (temp.Name.Equals("td:sup"))
                                                        {
                                                            _FirstName += temp.InnerText; ;
                                                        }
                                                        else
                                                        {
                                                            _FirstName += temp.InnerText;
                                                        }
                                                    }
                                                }
                                                else
                                                    _FirstName = chnode.InnerXml;
                                            }
                                            else if (chnode.Name.Equals("td:surname"))
                                            {
                                                if (chnode.ChildNodes.Count > 1)
                                                {
                                                    foreach (XmlNode temp in chnode)
                                                    {
                                                        if (temp.Name.Equals("td:small-caps"))
                                                        {
                                                            _LastName += temp.InnerText.ToUpper();
                                                        }
                                                        else if (temp.Name.Equals("td:sup"))
                                                        {
                                                            _LastName += temp.InnerText;
                                                        }
                                                        else
                                                        {
                                                            _LastName += temp.InnerText;
                                                        }
                                                    }
                                                }
                                                else
                                                    _LastName = chnode.InnerXml;
                                            }
                                            else if (chnode.Name.Equals("td:cross-ref"))
                                            {
                                            }
                                        }
                                        if (_FirstName.Equals(""))
                                        {
                                            Console.WriteLine("Author information missing.........");
                                            Console.WriteLine("First Name  not define in td:author tag");
                                            //Console.WriteLine("Please First Name must be define in td:author tag");
                                            //Console.WriteLine("Please define in xml for further process : ");
                                            //Console.WriteLine("Please press any key to exit.");
                                            //Console.ReadLine();
                                            // Environment.Exit(0);
                                        }
                                        if (_LastName.Equals(""))
                                        {
                                            Console.WriteLine("Author information missing.........");
                                            Console.WriteLine("Last Name  not define in td:author tag");
                                            //Console.WriteLine("Please Last Name must be define in td:author tag");
                                            //Console.WriteLine("Please define in xml for further process : ");
                                            //Console.WriteLine("Please press any key to exit.");
                                            //Console.ReadLine();
                                            //Environment.Exit(0);
                                        }

                                        if (!_FirstName.Equals("") && !_LastName.Equals(""))
                                        {


                                            textWriter.WriteElementString("given_name", _FirstName);
                                            textWriter.WriteElementString("surname", _LastName);

                                        }
                                        textWriter.WriteEndElement();

                                    }

                                    textWriter.WriteEndElement();
                                }

                                
                        textWriter.WriteStartElement("publication_date");
                        textWriter.WriteElementString("month", monthwithzero);
                            textWriter.WriteElementString("year", IssueDetails.Year);
                        textWriter.WriteEndElement();
                        textWriter.WriteStartElement("pages");
                        textWriter.WriteElementString("first_page", _sPage);
                        textWriter.WriteElementString("last_page", _ePage);
                        textWriter.WriteEndElement();
                        textWriter.WriteStartElement("doi_data");
                            textWriter.WriteElementString("doi", _DOI);
                            textWriter.WriteElementString("resource", "http://www.john-libbey-eurotext.fr/medline.md?doi=" + _DOI);
                        textWriter.WriteEndElement();
                    textWriter.WriteEndElement();
                textWriter.WriteEndElement();
            //            textWriter.WriteElementString("DOI", _DOI);
            //textWriter.WriteElementString("DOI", _DOI);
            //textWriter.WriteElementString("DOI", _DOI);
            //textWriter.WriteElementString("DOI", _DOI);
            //textWriter.WriteElementString("DOIWebsiteLink", "http://www.john-libbey-eurotext.fr/medline.md?doi=" + _DOI);
            //textWriter.WriteStartElement("Website");
            //textWriter.WriteElementString("WebsiteRole", "01");
            //textWriter.WriteElementString("WebsiteLink", "http://www.john-libbey-eurotext.fr");
            //textWriter.WriteEndElement();
            //textWriter.WriteElementString("RegistrantName", "John Libbey Eurotext");
            //textWriter.WriteElementString("RegistrationAuthority", "mEDRA");

            //WriteSerialPublication();
            //WriteJournalIssue();
            //WriteContentItem(XMLFile);
            ////textWriter.WriteEndElement();
        }
        
        private void WriteDOISerialArticleWork(string XMLFile )
        {
            GetDetails(XMLFile);
            textWriter.WriteStartElement("DOISerialArticleWork");

            if (CheckReplace(_JID + _AID))
            {
                textWriter.WriteElementString("NotificationType", "07");
            }
            else
            {
                textWriter.WriteElementString("NotificationType", "06");
            }

				textWriter.WriteElementString("DOI",_DOI);
                textWriter.WriteElementString("DOIWebsiteLink","http://www.john-libbey-eurotext.fr/medline.md?doi=" + _DOI);
                textWriter.WriteStartElement("Website");
                textWriter.WriteElementString("WebsiteRole","01");
                textWriter.WriteElementString("WebsiteLink", "http://www.john-libbey-eurotext.fr");
                textWriter.WriteEndElement();
                textWriter.WriteElementString("RegistrantName","John Libbey Eurotext");
                textWriter.WriteElementString("RegistrationAuthority","mEDRA");

                    WriteSerialPublication();
                    WriteJournalIssue();
                    WriteContentItem(XMLFile);
            textWriter.WriteEndElement();
        }

        private void WriteSerialPublication()
        { 
             textWriter.WriteStartElement ("SerialPublication");
						textWriter.WriteStartElement ("SerialWork");
							textWriter.WriteStartElement ("Title");
								textWriter.WriteElementString("TitleType","01");

                                string JT = GetJournalTitle(_JID);
								textWriter.WriteElementString("TitleText",JT);
							textWriter.WriteEndElement();
							textWriter.WriteStartElement ("Publisher");
								textWriter.WriteElementString("PublishingRole","01");
                                textWriter.WriteElementString("PublisherName", "John Libbey Eurotext");
							textWriter.WriteEndElement();
                            textWriter.WriteElementString("CountryOfPublication", "FR");
					textWriter.WriteEndElement();
		   textWriter.WriteEndElement();
        }

        private void WriteJournalIssue()
        { 
              textWriter.WriteStartElement ("JournalIssue");
                    textWriter.WriteElementString("JournalVolumeNumber",IssueDetails.Volume);
				    textWriter.WriteElementString("JournalIssueNumber",IssueDetails.ISSUE);
                    textWriter.WriteStartElement("JournalIssueDate");
                            textWriter.WriteElementString("DateFormat","01");
                            textWriter.WriteElementString("Date", IssueDetails.Year + IssueDetails.Month.PadLeft(2, '0'));
                    textWriter.WriteEndElement();
		       textWriter.WriteEndElement();
       }

        private void WriteContentItem(string XMLFile)
        {
            textWriter.WriteStartElement("ContentItem");
                WriteTextItem();
                WriteTitle();
                WriteContributor();
                WriteLanguage();
                WriteCopyrightStatement();
            textWriter.WriteEndElement();
        }

        private void WriteTextItem()
        {
            textWriter.WriteStartElement("TextItem");
				textWriter.WriteStartElement ("PageRun");
                    textWriter.WriteElementString("FirstPageNumber",_sPage);
					textWriter.WriteElementString("LastPageNumber",_ePage);
				textWriter.WriteEndElement();
			textWriter.WriteEndElement();
        }

        private void WriteTitle()
        {
            textWriter.WriteStartElement("Title");
                textWriter.WriteElementString("TitleType","01");
                textWriter.WriteElementString("TitleText", ENArticleTitle);
			textWriter.WriteEndElement();
        }

        private void WriteLanguage()
        {
            textWriter.WriteStartElement("Language");
			        textWriter.WriteElementString("LanguageRole","01");

            if (_Language.Equals("FR", StringComparison.OrdinalIgnoreCase))
			        textWriter.WriteElementString("LanguageCode","fre");
            else
                textWriter.WriteElementString("LanguageCode","eng");

			textWriter.WriteEndElement();
        }

        private void WriteCopyrightStatement()
        {
            string CopyRightYear = "";
            if (!_DOI.Equals(""))
            {
               if (_DOI.IndexOf('/') != -1)
                   CopyRightYear = _DOI.Split('.')[2];
                else
                   CopyRightYear = _DOI.Split('.')[1];
            }
            textWriter.WriteStartElement("CopyrightStatement");
                    textWriter.WriteElementString("CopyrightYear", CopyRightYear);
				    textWriter.WriteStartElement("CopyrightOwner");
                        textWriter.WriteElementString("CorporateName", "John Libbey Eurotext");
				    textWriter.WriteEndElement();
			textWriter.WriteEndElement();
        }

        private void WriteContributor()
        {
            string _FirstName = "";
            string _LastName = "";
            string Prefix = "", Suffix = "", Degrees = "", eMail = "";
            XmlNodeList AuthorNL = MyXmlDocument.GetElementsByTagName("td:author");
            for (int X = 0; X < AuthorNL.Count; X++)
            {
                XmlNode node = AuthorNL[X];
                _FirstName = "";
                _LastName = "";
                Prefix = "";
                Suffix = "";
                Degrees = "";
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode chnode = node.ChildNodes[i];
                    if (!chnode.Equals("") && chnode.NodeType == XmlNodeType.Element)
                        chnode.InnerXml = chnode.InnerXml.Replace("#$#lt;sup#$#gt;a#$#lt;/sup#$#gt;", "#$##x00AA;");
                    if (chnode.Name.Equals("td:e-address"))
                        eMail = chnode.InnerText;
                    else if (chnode.Name.Equals("td:suffix"))
                        Suffix = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:initials"))
                        Prefix = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:degrees"))
                        Degrees = chnode.InnerXml;
                    else if (chnode.Name.Equals("td:given-name"))
                    {
                        if (chnode.ChildNodes.Count > 1)
                        {
                            foreach (XmlNode temp in chnode)
                            {
                                if (temp.Name.Equals("td:small-caps"))
                                {
                                    _FirstName += temp.InnerText.ToUpper();
                                }
                                else if (temp.Name.Equals("td:sup"))
                                {
                                    _FirstName +=  temp.InnerText ; ;
                                }
                                else
                                {
                                    _FirstName += temp.InnerText;
                                }
                            }
                        }
                        else
                            _FirstName = chnode.InnerXml;
                    }
                    else if (chnode.Name.Equals("td:surname"))
                    {
                        if (chnode.ChildNodes.Count>1)
                        {
                                foreach (XmlNode temp in chnode)
                                {
                                    if (temp.Name.Equals("td:small-caps"))
                                    {
                                        _LastName += temp.InnerText.ToUpper();
                                    }
                                    else if (temp.Name.Equals("td:sup"))
                                    {
                                        _LastName +=  temp.InnerText;
                                    }
                                    else
                                    {
                                        _LastName += temp.InnerText;
                                    }
                                }
                        }
                        else
                             _LastName = chnode.InnerXml;
                    }
                    else if (chnode.Name.Equals("td:cross-ref"))
                    {
                    }
                }
                if (_FirstName.Equals(""))
                {
                    Console.WriteLine("Author information missing.........");
                    Console.WriteLine("First Name  not define in td:author tag");
                    //Console.WriteLine("Please First Name must be define in td:author tag");
                    //Console.WriteLine("Please define in xml for further process : ");
                    //Console.WriteLine("Please press any key to exit.");
                    //Console.ReadLine();
                    // Environment.Exit(0);
                }
                if (_LastName.Equals(""))
                {
                    Console.WriteLine("Author information missing.........");
                    Console.WriteLine("Last Name  not define in td:author tag");
                    //Console.WriteLine("Please Last Name must be define in td:author tag");
                    //Console.WriteLine("Please define in xml for further process : ");
                    //Console.WriteLine("Please press any key to exit.");
                    //Console.ReadLine();
                    //Environment.Exit(0);
                }

                if (! _FirstName.Equals("") && ! _LastName.Equals(""))
                {

                    textWriter.WriteStartElement("Contributor");
                    textWriter.WriteElementString("ContributorRole", "A01");
                    textWriter.WriteElementString("PersonName", _FirstName + " " + _LastName);
                    textWriter.WriteElementString("NamesBeforeKey", _FirstName);
                    textWriter.WriteElementString("KeyNames", _LastName);
                    textWriter.WriteEndElement();
                }
            }
        }

        private string GetJournalTitle(string _JID)
        {
            _JID = _JID.ToLower();
            string JT = "";
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\MedraJT.txt";
            string[] JIDTitle = File.ReadAllLines(FileName);
            foreach (string str in JIDTitle)
            {
                if (str.StartsWith(_JID, StringComparison.OrdinalIgnoreCase))
                {
                    JT = str.Split('\t')[1];
                    break;
                }
            }
            return JT;
        }

        private void FinalFilteration(string MedraXml)
        {
            StringBuilder HtmlStr = new StringBuilder(File.ReadAllText(MedraXml));

            HtmlStr.Replace("#$#", "&");
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
          
            
            //HtmlStr.Replace("</head>", "<link href=\"xEdt.css\" rel=\"stylesheet\" type=\"text/css\" />\n</head>");

            HtmlStr = ReplaceMedraEntity(HtmlStr.ToString());
            HtmlStr = ReplaceEntity(HtmlStr.ToString());
            File.WriteAllText(MedraXml, HtmlStr.ToString());
        }

        private StringBuilder ReplaceMedraEntity(string xmlStr)
        {
                StringBuilder XmlStr = new StringBuilder(xmlStr);
                string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\MedraEntity.txt";

                if (!File.Exists(FileName))
                {
                    Console.WriteLine(FileName + " does not exist");
                    Console.WriteLine("Xml entity could not be converted into html entity.");
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadLine();
                    return XmlStr;
                }

                Regex reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                string [] ALLLines = File.ReadAllLines(FileName); 
                MatchCollection mch = reg.Matches(XmlStr.ToString());
                reg = new Regex(@"&[a-zA-Z\.0-9]{1,}\;");
                mch = reg.Matches(XmlStr.ToString());
                for (int i = 0; i < mch.Count; i++)
                {
                    bool Result=false;
                    string Str = "";
                    foreach ( string Line in ALLLines)
                    {
                        if (Line.EndsWith(mch[i].Value,StringComparison.Ordinal))
                        {
                            Str = Line;
                            Result= true;
                            break;
                        }
                    }
                    if (Result && Str.IndexOf('_')!=-1)
                    {
                        try
                        {
                            int NO = Int32.Parse(Str.Split('_')[0]);
                            XmlStr.Replace(mch[i].Value, Convert.ToChar(NO).ToString());
                            //Console.WriteLine(mch[i].Value + ":::" + Convert.ToChar(NO).ToString());
                        }
                        catch
                        {
                            Result = false;
                        }
                    }

                    if (Result==false)
                    {
                        Console.WriteLine("Error Report ::");
                        Console.WriteLine("Please check MedraEntity.txt file for mention entity :: " + mch[i].Value);
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
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

        private bool CheckReplace(string JIDAID)
        {
            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (File.Exists(ExeLoc + "\\ReplaceMedra.txt"))
            {
                string[] FreeArticle = File.ReadAllLines(ExeLoc + "\\ReplaceMedra.txt");

                for (int i = 0; i < FreeArticle.Length; i++)
                {
                    if (FreeArticle[i].StartsWith(JIDAID,StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs args) 
        {
            ErrCount++;
            isValid = false;

            if (args.Severity == XmlSeverityType.Warning)
                ErrorLog.AppendLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message); 
            else
                ErrorLog.AppendLine("E:(" + ErrCount + ")At " + args.Exception.LineNumber + ":" + args.Exception.LinePosition + "     " + args.Message.Trim());
                //Console.WriteLine("\tValidation error: " + args.Message);
        } 

        private static void  MyValidationEventHandler(object sender, ValidationEventArgs args)
        {
              ErrCount++;
              isValid = false;
            //Console.WriteLine("Validation event\n" + args.Message + "--" + args.Exception.LineNumber);
            //ErrorLog.AppendLine(args.Exception.LineNumber + ":"+args.Message);
              ErrorLog.AppendLine("E:(" + ErrCount + ")At " + args.Exception.LineNumber + ":" + args.Exception.LinePosition + "     " + args.Message.Trim());
        }

        public static void WriteErrorLog(string  XmlPath)
        {

                    //XmlPath = @"F:\Input\JLE\Medra\SampleXML\BAUDOUINREGID_20100907092117_en.xml";

                    string ErrLogFile = XmlPath.Replace(".xml", ".err");
                    if (File.Exists(ErrLogFile))
                    {
                        File.Delete(ErrLogFile);
                    }

                        string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        string XmlForParsing = ExeLoc + "\\" + Path.GetFileName(XmlPath);
                        File.Copy(XmlPath, XmlForParsing, true);
                        isValid = true;
                        ErrorLog = new StringBuilder("");


                       XmlReaderSettings settings = new XmlReaderSettings(); 
                       settings.ValidationType = ValidationType.Schema; 
                       settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema; 
                       settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation; 
                       settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings; 
                       settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);          

                    // Create the XmlReader object.         
                       XmlReader reader = XmlReader.Create(XmlForParsing, settings);         
                    // Parse the file.          
                        try
                        {
                            while (reader.Read())
                            {
                                // Can add code here to process the content.
                            }
                        }
                        catch (XmlException EX)
                        {
                              ErrorLog.AppendLine("E:(" + ErrCount + ")At " + EX.LineNumber + ":" + EX.LinePosition + "     " + EX.Message.Trim());
                            //ErrorLog.AppendLine(EX.Message + "--" + EX.LineNumber + ":" + EX.LinePosition);
                        }
                        reader.Close();
                        File.Delete(XmlForParsing);

                        // Check whether the document is valid or invalid.
                        if (isValid)
                            Console.WriteLine("mEDRA xml is valid");
                        else
                            Console.WriteLine("mEDRA xml is invalid");

                        if (ErrorLog.Length == 0)
                        {
                            return;
                        }
                    

                    StringBuilder  LogStr = new StringBuilder("");
                    LogStr.AppendLine("Medra XML Validation 1.0 run at " + DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToShortTimeString() + " " + DateTime.Today.Year);
                    LogStr.AppendLine("-----------------------------------------------------------------------------------");
                    LogStr.AppendLine("File: " + XmlPath);
                    LogStr.AppendLine("This is the start of the log file generated by TEES validation process");
                    LogStr.AppendLine("-----------------------------------------------------------------------------------");


                    LogStr.AppendLine(ErrorLog.ToString());

                    LogStr.AppendLine();
                    LogStr.AppendLine("'# of Error: " + ErrCount);
                    LogStr.AppendLine("This is the end of the log file");
                    LogStr.AppendLine("Copyright (C) 2012-2013 Thomson Digital");
                    File.WriteAllText(ErrLogFile , LogStr.ToString());
        }

    }
}


