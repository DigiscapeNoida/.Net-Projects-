using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;

namespace Orders
{
    public static class ConfigDetails
    {
        
        static string _FMSPath = "";
        static string _EMCFMSPath = "";
        static string _IPIPFMSPath = "";
        
        static string _RootPath           = "";
        static string _TemplatePath       = "";
        static string _EMCTemplatePath = "";
        static string _Application3ConStr = "";

       
        static  ConfigDetails()
        {
            string[] KEYS = ConfigurationManager.AppSettings.AllKeys;
            foreach (string key in KEYS)
            {
                switch (key)
                {
                    case "TemplatePath":
                    {
                        _TemplatePath = ConfigurationManager.AppSettings[key];
                        break;
                    }
                    case "IPIPFMSPATH":
                    {
                        _IPIPFMSPath = ConfigurationManager.AppSettings[key];
                        break;
                    }
                }
            }
        }
        public static string IPIPFMSPath
        {
            set { _IPIPFMSPath = value; }
            get { return _IPIPFMSPath; }
        }

        public static string TemplatePath
        {
            set { _TemplatePath = value; } 
            get { return _TemplatePath; } 
        }
        public static string EMCTemplatePath
        {
            set { _EMCTemplatePath = value; }
            get { return _EMCTemplatePath; }
        }

        public static string FMSPath
        {
            set { _FMSPath = value; }
            get { return _FMSPath; }
        }
        public static string EMCFMSPath
        {
            set { _EMCFMSPath = value; }
            get { return _EMCFMSPath; }
        }
        public static string RootPath 
        { 
            set { _RootPath = value; } 
            get { return _RootPath; }
        }
        public static string Application3ConStr 
        {
            set { _Application3ConStr = value; } 
            get { return _Application3ConStr; } 
        }
    }
    public class XmlOrder   
    {
        static XmlTextWriter textWriter;
        #region Private Variabele
        private string _DirectoryPath = "";

        private string _WorkFlow = "";
        private string _JTitle = "";
        private string _Client        = "";
        private string _JID           = "";
        private string _AID           = "";
        private string _Stage         = "";
        private string _FMSStage = "";
        private string _DOI           = "";
        private string _Volume        = "";
        private string _Issue         = "";
        private string _Figs            = "";
        private string _ArticleCategory = "";
        private string _ArticleType     = "";
        private string _MSS             = "";


        private string _Editor = "";
        private string _Designation ="";
        private string _Address="";
        private string _Tel ="";
        private string _Fax = "";

        private string _FrstAuthFName = "";
        private string _FrstAuthSName = "";
        private string _FrstAuthDgree = "";
        private string _ArtTitle      = "";

        private string _CorAuthName = "";
        private string _CorMailCC   = "";
        private string _Remarks     = "";
        
        private string _CorAuthEmail = "";
        private string _PDFName = "";
        private string _DocName = "";
        private string _ProdSite = "";
        private DateTime _ReceivedDate ;
        private DateTime _RevisedDate ;
        private DateTime _AcceptedDate;
        private DateTime _ActutalDueDate;
        private DateTime _InternalDuedate;
        private string _SupplementaryMaterial = "";
        #endregion
        #region Public Property
        public XmlOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string DocName
        {
            set { _DocName = value; }
            get { return _DocName; }
        }
        public string Remarks
        {
            set { _Remarks = value; }
            get { return _Remarks; }
        }
        public string ProdSite
        {
            set { _ProdSite = value; }
            get { return _ProdSite; }
        }
        public string FrstAuthFName
        {
            get { return _FrstAuthFName; }
            set { _FrstAuthFName = value; }
        }
        public string FrstAuthSName
        {
            get { return _FrstAuthSName; }
            set { _FrstAuthSName = value; }
        }

        public string FrstAuthDgree
        {
            get { return _FrstAuthDgree; }
            set { _FrstAuthDgree = value; }
        }

        public string ArtTitle
        {
            get { return _ArtTitle; }
            set { _ArtTitle = value; }
        }
      
        public string CorAuthName
        {
            get { return _CorAuthName; }
            set { _CorAuthName = value; }
        }
        public string CorMailCC
        {
            get { return _CorMailCC; }
            set { _CorMailCC = value; }
        }
        public string CorAuthEmail
        {
            get { return _CorAuthEmail; }
            set { _CorAuthEmail = value; }
        }

        public string PDFName
        {
            get { return _PDFName; }
            set { _PDFName = value; }
        }
        public string Editor
        {
            set { _Editor = value; }
            get { return _Editor; }
        }

        public string Designation
        {
            set { _Designation = value; }
            get { return _Designation; }
        }

        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
        }

        public string Tel
        {
            set { _Tel = value; }
            get { return _Tel; }
        }

        public string Fax
        {
            set { _Fax = value; }
            get { return _Fax; }
        }

        public string WorkFlow
        {
            set { _WorkFlow = value; }
            get { return _WorkFlow; }
        }


        public string JTitle
        {
            set { _JTitle = value; }
            get { return _JTitle; }
        }
        public string Client
        {
            set { _Client = value; }
            get { return _Client; }
        }

        public string JID
        {
            set { _JID = value; }
            get { return _JID; }
        }
        public string AID
        {
            set { _AID = value.Replace("_","-"); }
            get { return _AID; }
        }
        public string Stage
        {
            set { _Stage = value; }
            get { return _Stage; }
        }
        public string FMSStage
        {
            set { _FMSStage = value; }
            get { return _FMSStage; }
        }
        public string DOI
        {
            set { _DOI = value; }
            get { return _DOI; }
        }
        public string Volume
        {
            set 
            {
                if (string.IsNullOrEmpty(value))
                    _Volume = "0";
                else
                    _Volume = value; 
            }
            get { return _Volume; }
        }
        public string Issue
        {
            set 
            {
               if (string.IsNullOrEmpty(value))
                    _Issue = "0";
                else
                    _Issue = value;
            }
            get { return _Issue; }
        }
        public string Figs
        {
            set { _Figs = value; }
            get { return _Figs; }
        }
        public string ArticleCategory
        {
            set { _ArticleCategory = value; }
            get { return _ArticleCategory; }
        }
        public string ArticleType
        {
            set { _ArticleType = value; }
            get { return _ArticleType; }
        }
        public string MSS
        {
            set { _MSS = value; }
            get { return _MSS; }
        }
        public DateTime ReceivedDate
        {
            set { _ReceivedDate = value; }
            get { return _ReceivedDate; }
        }
        public DateTime RevisedDate
        {
            set { _RevisedDate = value; }
            get { return _RevisedDate; }
        }
        public DateTime AcceptedDate
        {
            set { _AcceptedDate = value; }
            get { return _AcceptedDate; }
        }
        public DateTime ActutalDueDate
        {
            set { _ActutalDueDate = value; }
            get { return _ActutalDueDate; }
        }
        public DateTime InternalDuedate
        {
            set { _InternalDuedate = value; }
            get { return _InternalDuedate; }
        }
        public string SupplementaryMaterial
        {
            set { _SupplementaryMaterial = value; }
            get { return _SupplementaryMaterial; }
        }
        #endregion
        #region  Fresh Process
        public  string CreateXMLOrder()
        {
            
            XmlReader reader;
            string OrderPath = GetOrderPath();
            _DirectoryPath = OrderPath;

            if (!Directory.Exists(_DirectoryPath))
            {
                Directory.CreateDirectory(_DirectoryPath);
            }

            string OrderFileName = OrderPath + "\\" + _Client + "_" + JID + "_" + _Stage + "_" + AID + "_Order_" + GetNextFileNo() + ".xml";
            string XMLFIlePath = ConfigDetails.TemplatePath;

            if (!File.Exists(XMLFIlePath))
            {
                return "";
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ProhibitDtd = false;
            settings.IgnoreWhitespace = false;
            reader = XmlReader.Create(XMLFIlePath, settings);
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(reader);
            reader.Close();

            if (RevisedDate.Year==0001 )
            {
                XmlNodeList RevisedDateList = myXmlDocument.DocumentElement.GetElementsByTagName("revised-date");
                if (RevisedDateList.Count > 0)
                {
                    RevisedDateList[0].ParentNode.RemoveChild(RevisedDateList[0]);
                }
            }

            if (AcceptedDate.Year == 0001)
            {
                XmlNodeList AcceptedDateList = myXmlDocument.DocumentElement.GetElementsByTagName("accept-date");
                if (AcceptedDateList.Count > 0)
                {
                    AcceptedDateList[0].ParentNode.RemoveChild(AcceptedDateList[0]);
                }
            }
           
            textWriter = new XmlTextWriter(OrderFileName, null);

            textWriter.Indentation = 4;
            textWriter.IndentChar = '\t';

            textWriter.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n");
            textWriter.WriteRaw("<!DOCTYPE orders SYSTEM \"FMS-J-Order.dtd\">" + "\n");

            textWriter.WriteRaw("<?xml-stylesheet type=\"text/xsl\" href=\"WileyJ-Order.xsl\"?>" + Environment.NewLine);

            SearchNode(myXmlDocument.DocumentElement);

            textWriter.Flush();
            textWriter.Close();
            textWriter = null;
            string[] txt = File.ReadAllLines(OrderFileName);
            string txt1="";
            foreach (string txt2 in txt)
            {
                if (txt1 == "")
                {
                    txt1 = txt2.Trim();
                }
                else
                {
                    txt1=txt1 + txt2.Trim();
                }
            }
            if (ActutalDueDate.Year != 0001)
            {
                string dday = ActutalDueDate.Day.ToString().Trim();
                string mon = ActutalDueDate.Month.ToString().Trim();
                if (dday.Length==1)
                {
                    dday = "0" + dday;
                }
                if (mon.Length==1)
                {
                    mon = "0" + mon;
                }
                txt1 = txt1.Replace("<due-date><date /></due-date>", "<due-date><date day=\"" + dday + "\" month=\"" + mon + "\" yr=\"" + ActutalDueDate.Year.ToString() + "\" /></due-date>");
                txt1 = txt1.Replace("><",">\n<");
                File.Delete(OrderFileName);
                Thread.Sleep(1000);
                File.WriteAllText(OrderFileName, txt1);
            }          
            return OrderFileName;
        }

        private void SearchNode (XmlNode node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    bool endElement;
                    XmlNodeList nodeList;
                    ProcessNode(node, out endElement);
                    nodeList = node.ChildNodes;
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        SearchNode(nodeList[i]);
                    }
                    if (endElement)
                    {
                        textWriter.WriteEndElement();
                    }
                    break;
                case XmlNodeType.Text:
                    textWriter.WriteString(node.InnerText);
                    break;
                case XmlNodeType.CDATA:
                    break;
                case XmlNodeType.ProcessingInstruction:
                    break;
                case XmlNodeType.Comment:
                    textWriter.WriteRaw(node.Value);
                    break;
                case XmlNodeType.XmlDeclaration:
                    break;
                case XmlNodeType.Document:
                    break;
                case XmlNodeType.DocumentType:
                    break;
                case XmlNodeType.EntityReference:
                    break;
                case XmlNodeType.EndElement:
                    break;
                case XmlNodeType.Entity:

                    break;
                case XmlNodeType.Whitespace:
                    textWriter.WriteRaw(node.Value);
                    break;
            }
            textWriter.Flush();
        }

        private void ProcessNode(XmlNode node, out bool endElement)
        {
            endElement = true;
            switch (node.Name)
            {
                case "prod-site":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ProdSite;
                    break;
                case "stage":
                     textWriter.WriteStartElement(node.Name);
                     textWriter.WriteAttributeString("step",_FMSStage);
                    break;
                case "order":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteAttributeString("customer",_Client);
                    textWriter.WriteAttributeString("category", "JOURNAL");
                    break;
                case "time":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteAttributeString("day", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("month", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("yr", DateTime.Now.Year.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("hr", DateTime.Now.Hour.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("min", DateTime.Now.Minute.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("sec", DateTime.Now.Second.ToString().PadLeft(2,'0'));
                    break;
                case "date":
                    textWriter.WriteStartElement(node.Name);
                    if ( ReceivedDate.Year==1 && node.ParentNode.Name.Equals("received-date"))
                    {
                        textWriter.WriteAttributeString("day", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr", DateTime.Now.Year.ToString().PadLeft(2, '0'));
                    }
                    else if (node.ParentNode.Name.Equals("due-date"))
                    {
                        string[] arr = _InternalDuedate.ToShortDateString().Split('/');
                        if (arr.Length > 2)
                        {

                            textWriter.WriteAttributeString("day",   _InternalDuedate.Day.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("month", _InternalDuedate.Month.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("yr",    _InternalDuedate.Year.ToString());
                        }
                    }
                    else if (node.ParentNode.Name.Equals("revised-date"))
                    {
                        textWriter.WriteAttributeString("day",   RevisedDate.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", RevisedDate.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr",    RevisedDate.Year.ToString().PadLeft(2, '0'));
                    }
                    else if (node.ParentNode.Name.Equals("accept-date"))
                    {
                        textWriter.WriteAttributeString("day",   AcceptedDate.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", AcceptedDate.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr",    AcceptedDate.Year.ToString().PadLeft(2, '0'));
                    }
                    else if (node.ParentNode.Name.Equals("received-date"))
                    {
                        textWriter.WriteAttributeString("day",   ReceivedDate.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", ReceivedDate.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr",    ReceivedDate.Year.ToString().PadLeft(2, '0'));
                    }
                    break;
                case "jid":
                    {
                        if (!string.IsNullOrEmpty(_DocName))
                        {
                            textWriter.WriteComment("<mssname>" + _DocName.Replace("<mssname>","").Replace("</mssname>","")  + "</mssname>");
                        }
                        textWriter.WriteString("\n");
                        textWriter.WriteStartElement(node.Name);
                        node.InnerXml = _JID;
                    }
                    break;
                case "jname":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _JTitle;
                    break;
                case "aid":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _AID;
                    break;
                case "doi":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _DOI;
                    break;
                case "vol":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Volume;
                    break;
                case "issue":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Issue;
                    break;
                case "to-mail":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _CorAuthEmail;
                    break;
                case "typesetting-required":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteAttributeString("required", "yes");
                    //if (GloVal.MailSubject.IndexOf("Direct", StringComparison.OrdinalIgnoreCase) != -1)
                    //    textWriter.WriteAttributeString("required", "yes");
                    //else if (GloVal.MailSubject.IndexOf("typesetting", StringComparison.OrdinalIgnoreCase) != -1)
                    //    textWriter.WriteAttributeString("required", "yes");
                    //else if (GloVal.MailSubject.IndexOf("type-setting", StringComparison.OrdinalIgnoreCase) != -1)
                    //    textWriter.WriteAttributeString("required", "yes");
                    //else
                    //    textWriter.WriteAttributeString("required", "no");
                    break;
                case "copy-editing-required":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteAttributeString("required", "no");
                    //if (GloVal.MailSubject.IndexOf("Copy", StringComparison.OrdinalIgnoreCase) != -1 || GloVal.MailBody.IndexOf("Copy", StringComparison.OrdinalIgnoreCase) != -1)
                    //    textWriter.WriteAttributeString("required", "yes");
                    //else 
                    //    textWriter.WriteAttributeString("required", "no");
                    break;
                case "no-mns-pages":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _MSS;
                    break;
                case "no-phys-figs":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Figs;
                    break;
                case "artcat":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArticleCategory;
                    break;
                case "arttype":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArticleType;
                    break;
                case "workflowdesc":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _WorkFlow;
                    break;
                case "editor":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Editor;
                    break;
                case "designation":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Designation;
                    break;
                case "tel":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Tel;
                    break;
                case "fax":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Fax;
                    break;
                case "degree":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("first-author"))
                        node.InnerXml = _FrstAuthDgree;
                    break;
                case "fnm":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("first-author"))
                        node.InnerXml = _FrstAuthFName;
                    else if (node.ParentNode.Name.Equals("corr-author"))
                        node.InnerXml = _CorAuthName;
                    break;
                case "snm":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("first-author"))
                        node.InnerXml = _FrstAuthSName;
                    break;
                case "item-title":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArtTitle;
                    break;
                case "remark":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Remarks;
                    break;
                default:
                    Default(node);
                    if ("".IndexOf(node.Name) != -1)
                    {
                        node.InnerXml = "";
                    }
                    break;
            }
            return;
        }

        private void Default    (XmlNode node)
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

        private int GetNextFileNo()
        {
            int MaxNo = 1;
            if (Directory.Exists(_DirectoryPath))
                MaxNo = Directory.GetFiles(_DirectoryPath, "*.xml").Length;
            


            return MaxNo;
        }

        private string GetOrderPath()
        {
            if (ConfigDetails.RootPath != null)
            {
                if (!ConfigDetails.RootPath.Equals(""))
                {
                    if (!Directory.Exists(ConfigDetails.RootPath))
                        Directory.CreateDirectory(ConfigDetails.RootPath);
                }
            }

            string OrderPath = ConfigDetails.RootPath.Trim(new char[]{'\\'}) + "\\" + _Client + "\\" + _JID + "\\" + _AID + "\\"+ _Stage;

            

            return OrderPath;
        }
        #endregion
        #region Revised Process

        public bool RevisedXMLOrder()
        {
            XmlTextReader Reader;
            string OrderPath = GetOrderPath();
            _DirectoryPath = OrderPath;
            string OrderFileName = OrderPath + "\\" + _Client + "_" + _JID + "_" + _Stage + "_" + _AID + "_Order_" + (GetNextFileNo()-1) + ".xml";

            if (!File.Exists(OrderFileName))
            {
                OrderFileName = OrderPath + "\\" + _Client + "_" + _JID + "_" + _FMSStage + "_" + _AID + "_Order_" + (GetNextFileNo() - 1) + ".xml";
                if (!File.Exists(OrderFileName))
                    return false;
            }


            Reader = new XmlTextReader(OrderFileName);
            Reader.XmlResolver = null;

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(Reader);
            Reader.Close();
            _SearchNode(myXmlDocument.DocumentElement);

            string DOI = GetPtrnText(myXmlDocument, ".//doi");
            if (!string.IsNullOrEmpty(DOI))
                _DOI = DOI;

            string AuthrName = GetPtrnText(myXmlDocument, ".//corr-author/fnm");
            if (!string.IsNullOrEmpty(AuthrName))
                _CorAuthName = AuthrName;

            string FAFnm     = GetPtrnText(myXmlDocument, ".//first-author/fnm");
            if (!string.IsNullOrEmpty(FAFnm))
                _FrstAuthFName = FAFnm;

            string FASnm     = GetPtrnText(myXmlDocument, ".//first-author/snm");
            if (!string.IsNullOrEmpty(FASnm))
                _FrstAuthSName = FASnm;

            string FADg      = GetPtrnText(myXmlDocument, ".//first-author/degree");
            if (!string.IsNullOrEmpty(FADg))
                _FrstAuthDgree = FADg;

            string AutEmail  = GetPtrnText(myXmlDocument, ".//eproofing-info/to-mail");
            if (!string.IsNullOrEmpty(AutEmail))
                _CorAuthEmail = AutEmail;


            string artcat    = GetText(myXmlDocument,"artcat");
            if (!string.IsNullOrEmpty(artcat))
                _ArticleCategory = artcat;

            string arttype   = GetText(myXmlDocument,"arttype");
            if (!string.IsNullOrEmpty(arttype))
                _ArticleType = arttype;

            //string AuthrName = "received-date";
            //string AuthrName = "accept-date";
            //string AuthrName = "revised-date";
            
            return true;
        }

        public bool GetDOIFromXML()
        {
            XmlTextReader Reader;
            string OrderPath = GetOrderPath();
            _DirectoryPath = OrderPath;
            string OrderFileName = OrderPath + "\\" + _Client + "_" + _JID + "_" + _Stage + "_" + _AID + "_Order_" + (GetNextFileNo() - 1) + ".xml";

            if (!File.Exists(OrderFileName))
            {
                    return false;
            }

            Reader = new XmlTextReader(OrderFileName);
            Reader.XmlResolver = null;

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(Reader);
            Reader.Close();
            _SearchNode(myXmlDocument.DocumentElement);

            string DOI = GetPtrnText(myXmlDocument, ".//doi");
            if (!string.IsNullOrEmpty(DOI))
                _DOI = DOI;

            return true;

        }
        private string  GetText(XmlDocument myXmlDocument, string NodeName)
        {
            string Result = string.Empty;
            XmlNodeList NL = myXmlDocument.GetElementsByTagName(NodeName);
            if (NL.Count > 0)
                Result = NL[0].InnerText;

            return Result;
        }

        private string GetPtrnText(XmlDocument myXmlDocument, string Ptrn)
        {
            string Result = string.Empty;
            XmlNode Node = myXmlDocument.DocumentElement.SelectSingleNode(Ptrn);
            if (Node!= null)
                Result = Node.InnerText;

            return Result;
        }

        private void _SearchNode(XmlNode node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    XmlNodeList nodeList;
                    ProcessNode(node);
                    nodeList = node.ChildNodes;
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        _SearchNode(nodeList[i]);
                    }

                    break;
                case XmlNodeType.Text:
                    break;
                case XmlNodeType.CDATA:
                    break;
                case XmlNodeType.ProcessingInstruction:
                    break;
                case XmlNodeType.Comment:
                    {
                        _DocName = node.Value;
                        break;
                    }
                case XmlNodeType.XmlDeclaration:
                    break;
                case XmlNodeType.Document:
                    break;
                case XmlNodeType.DocumentType:
                    break;
                case XmlNodeType.EntityReference:
                    break;
                case XmlNodeType.EndElement:
                    break;
                case XmlNodeType.Entity:
                    break;
                case XmlNodeType.Whitespace:

                    break;
            }

        }
        private void ProcessNode(XmlNode node)
        {
            switch (node.Name)
            {
                case "no-mns-pages":
                   _MSS = node.InnerXml;
                    break;
                case "jid":
                    JID = node.InnerXml;
                    break;
                case "doi":
                    DOI = node.InnerXml;
                    break;
                case "vol":
                     Volume = node.InnerXml;
                    break;
                case "issue":
                    Issue = node.InnerXml;
                    break;
                case "no-phys-figs":
                    Figs = node.InnerXml;
                    if (string.IsNullOrEmpty(Figs))
                    {
                        Figs = "0";
                    }
                    break;
                case"item-title":
                    _ArtTitle= node.InnerXml;
                    break;
                case "remark":
                    _Remarks = node.InnerXml;
                    break;
                case"date":
                        {
                            string dy   = node.Attributes.GetNamedItem("day")  != null ? node.Attributes.GetNamedItem("day").Value:"";
                            string mnth = node.Attributes.GetNamedItem("month")!= null ? node.Attributes.GetNamedItem("month").Value:"";
                            string yr    = node.Attributes.GetNamedItem("yr")   != null ? node.Attributes.GetNamedItem("yr").Value : "";

                          
                            if (!string.IsNullOrEmpty(dy))
                            {
                                int Day = GetNumeric(dy);
                                int Mnth = GetNumeric(mnth);
                                int Year = GetNumeric(yr);


                                if (node.ParentNode.Name.Equals("received-date"))
                                {
                                    _ReceivedDate = GetDate(Year, Mnth, Day);
                                }
                                else if (node.ParentNode.Name.Equals("revised-date"))
                                {
                                    _RevisedDate = GetDate(Year, Mnth, Day);
                                }
                                else if (node.ParentNode.Name.Equals("accept-date"))
                                {
                                    _AcceptedDate = GetDate(Year, Mnth, Day);
                                }
                            }
                            break;
                        }
                    break;
            }
            return;
        }
        private DateTime GetDate(int Year, int Mnth, int Day)
        {
            DateTime DT = new DateTime();
            if (Year > 0 && Mnth > 0 && Day > 0)
                DT = new DateTime(Year, Mnth, Day);

            return DT;
        }
        private int GetNumeric(string str)
        {
            int Val;
            Int32.TryParse(str, out Val);
            return Val;
        }
        #endregion
        #region Set Cor Author Details in Database
        public void SetCorAuthorDetails()
        {
            string _ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("usp_InsertCorAuthorDetaill", con);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
                cmd.Parameters["@Client"].Value = Client;

                cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
                cmd.Parameters["@JID"].Value = JID;

                cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.VarChar, 50));
                cmd.Parameters["@AID"].Value = AID;

                cmd.Parameters.Add(new SqlParameter("@CorName", SqlDbType.VarChar, 50));
                cmd.Parameters["@CorName"].Value = CorAuthName;

                cmd.Parameters.Add(new SqlParameter("@CorMail", SqlDbType.VarChar, 150));
                cmd.Parameters["@CorMail"].Value = CorAuthEmail;


                cmd.Parameters.Add(new SqlParameter("@CorMailCC", SqlDbType.VarChar, 150));
                cmd.Parameters["@CorMailCC"].Value = CorMailCC;


                cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, -1));
                cmd.Parameters["@Title"].Value = "";

                cmd.Parameters.Add(new SqlParameter("@PdfName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PdfName"].Value = PDFName;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        public void SetCorAuthorDetailsforThieme()
        {
            string _ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("usp_InsertCorAuthorDetaill_New", con);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
                cmd.Parameters["@Client"].Value = Client;

                cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
                cmd.Parameters["@JID"].Value = JID;

                cmd.Parameters.Add(new SqlParameter("@DOI", SqlDbType.VarChar, 50));
                cmd.Parameters["@DOI"].Value = DOI;

                cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.VarChar, 50));
                cmd.Parameters["@AID"].Value = AID;

                cmd.Parameters.Add(new SqlParameter("@CorName", SqlDbType.VarChar, 50));
                cmd.Parameters["@CorName"].Value = CorAuthName;

                cmd.Parameters.Add(new SqlParameter("@CorMail", SqlDbType.VarChar, 150));
                cmd.Parameters["@CorMail"].Value = CorAuthEmail;


                cmd.Parameters.Add(new SqlParameter("@CorMailCC", SqlDbType.VarChar, 150));
                cmd.Parameters["@CorMailCC"].Value = CorMailCC;


                cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, -1));
                cmd.Parameters["@Title"].Value = "";

                cmd.Parameters.Add(new SqlParameter("@PdfName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PdfName"].Value = PDFName;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
    public class JIDInfo    
    {
        StringCollection _Client = new StringCollection();
        static StringCollection _JID = new StringCollection();
        static StringCollection _Stage = new StringCollection();
        static StringCollection _WorkFlow = new StringCollection();



        public StringCollection Client
        {
            get { return _Client; }
        }

        public StringCollection JID   
        {
            get { return _JID; }
        }

        public StringCollection Stage 
        {
            get { return _Stage; }
        }

        public StringCollection WorkFlow
        {
            get { return _WorkFlow; }
        }

        public JIDInfo()
        {
        }

        public JIDInfo(bool client)
        {
            GetClient();
        }

        public  StringCollection GetClient()
        {
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd    = new SqlCommand("GetClient", con);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Client.Add(reader["ClientName"].ToString());
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return _Client;
        }
        public  StringCollection GetJID(string Client)
        {
            _JID.Clear();
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand    cmd = new SqlCommand("GetJID", con);

            cmd.CommandType   = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                 while (reader.Read())
                 {
                     _JID.Add(reader["JID"].ToString());
                 }
                reader.Close();
                
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return _JID;
        }
        public  StringCollection GetStage(string Client)
        {
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd = new SqlCommand("GetStage", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;
            try
            {
                con.Open();
                _Stage.Clear();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Stage.Add(reader["Stage"].ToString());
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return _Stage;
        }

        public string GetFMSStage(string Client, string Stage)
        {
            string FMSStage = "";
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd = new SqlCommand("GetFMSStage", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;

            cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
            cmd.Parameters["@Stage"].Value = Stage;

            try
            {
                con.Open();
                _Stage.Clear();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FMSStage=reader["FMSStage"].ToString();
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return FMSStage;
        }

        public  StringCollection GetWorkFlow(string Client, string JID, string Stage)
        {
            WorkFlow.Clear();
            if (Stage.ToLower() == "revised" && Client.ToLower()=="thieme")
            {
                _WorkFlow.Add("WITH GRAPHICS");
                _WorkFlow.Add("WITHOUT GRAPHICS");
                return _WorkFlow;
            }
            else 
            {
                SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
                SqlCommand cmd = new SqlCommand("GetWorkFLow", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
                cmd.Parameters["@Client"].Value = Client;

                cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
                cmd.Parameters["@JID"].Value = JID;

                cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
                cmd.Parameters["@Stage"].Value = Stage;

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        _WorkFlow.Add(reader["WorkflowName"].ToString());
                    }
                    reader.Close();

                }
                catch (SqlException err)
                {
                    // Replace the error with something less specific.
                    // You could also log the error now.
                    throw new ApplicationException("Data error.");
                }
                finally
                {
                    con.Close();
                }

                return _WorkFlow;
            }
           
        }

        public int GetTAT(string Client, string JID, string Stage)
        {
            int TAT = 0;
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd = new SqlCommand("GetTAT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;

            cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
            cmd.Parameters["@JID"].Value = JID;

            cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
            cmd.Parameters["@Stage"].Value = Stage;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TAT = (int)(reader["TAT"]);
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return TAT;
        }

       

        public StringCollection GetTRACode()
        {
            _JID.Clear();
            _JID.Add("-Select-");

            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd    = new SqlCommand("GetEmcJID", con);
            cmd.CommandType   = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _JID.Add(reader["TRACODE"].ToString());
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return _JID;
        }

        public TRACodeInfo GetTRACodeInfo(string TRACode)
        {

            TRACodeInfo TRACodeInfoOBJ = new TRACodeInfo();
            SqlConnection con = new SqlConnection(ConfigDetails.Application3ConStr);
            SqlCommand cmd = new SqlCommand("GetEMCJIDDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@TRACode", SqlDbType.VarChar, 50));
            cmd.Parameters["@TRACode"].Value = TRACode;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["ColorModel"]!= null) TRACodeInfoOBJ.ColorModel = reader["ColorModel"].ToString();
                    if (reader["ISBN"]!= null) TRACodeInfoOBJ.ISBN             = reader["ISBN"].ToString();
                    if (reader["ISSN"]!= null) TRACodeInfoOBJ.ISSN             = reader["ISSN"].ToString();
                    if (reader["TRACode"]!= null) TRACodeInfoOBJ.TRACode       = reader["TRACode"].ToString();
                    if (reader["TRATitle"]!= null) TRACodeInfoOBJ.TRATitle     = reader["TRATitle"].ToString();
                    if (reader["TypeSettingModelFormat"] != null) TRACodeInfoOBJ.TypeSettingModelFormat = reader["TypeSettingModelFormat"].ToString();
                    if (reader["TRAJID"] != null) TRACodeInfoOBJ.TRAJID         = reader["TRAJID"].ToString(); ;
                }
                reader.Close();

            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
            return TRACodeInfoOBJ;
        }
       
    }
    public class EMCXmlOrder
    {
        static XmlTextWriter textWriter;

        #region Private Variabele
        private string _DirectoryPath = "";
        private string _Client = "";
        private string _JID = "";

        private DateTime _InternalDuedate = DateTime.Now ;
        private string _ProdSite = "";
        private string _Stage = "";
        private string _TraRoot = "";
        private string _Tracode = "";
        private string _Fasnumero = "";
        private string _Trajid = "";
        private string _Aid = "";
        private string _Pii = "";
        private string _Doi = "";
        private string _ItemTitle = "";
        private string _ItemSubtitle = "";
        private string _TraiteTitle = "";
        private string _ColorModel = "";
        private string _Issn = "";
        private string _Isbn = "";
        private string _TypeSettingModelFormat = "";
        private string _ArticleType = "";
        private string _MajNo = "";
        private string _MajCote = "";
        private string _MajAnne = "";
        private string _Vol = "";
        private string _Chaptre = "";
        private string _Subchaptre = "";
        private string _NbPagesCommande = "";
        private string _NbPagesEstimate = "";
        private string _NbTableau = "";
        private string _NbFig = "";
        private string _NbPhoto = "";
        private string _NbDessin = "";
        private string _NbArbrePapier = "";
        private string _NbEncadreT1 = "";
        private string _NbEncadreT2 = "";
        private string _NbBiblio = "";
        private string _NbBiblioSs = "";
        private string _NbSavoirPlus = "";
        private string _ResumeEn = "";
        private string _ResumeFr = "";
        private string _McEn = "";
        private string _McFr = "";
        private string _TitreEn = "";
        private string _NbArbreIntractif = "";
        private string _NbIconoSup = "";
        private string _NbVideo = "";
        private string _NbDocLegaux = "";
        private string _NbFichePatient = "";
        private string _NbFicheTech = "";
        private string _NbAutoeval = "";
        private string _NbClinique = "";
        private string _NbQuotidien = "";
        private string _Lblvide = "";
        private string _Nbvide = "";
        private string _AppelIcono = "";
        private string _AppelBiblio = "";
        private string _IconoOk = "";
        private string _ArbreDeci = "";

        private string _PrincAuthorNom ="";
        private string _PrincAuthorPnom ="";
        private string _PrincAuthorAff ="";
        private string _SecondAuthorNom ="";
        private string _SecondAuthorPnom ="";
        private string _SecondAuthorAff ="";
        private string _CorrAuthorPhone ="";
        private string _CorrAuthorFAX ="";
        private string _CorrAuthorEmail = "";
        #endregion 

        #region Public Property
        public string Client
        {
            set { _Client = value; }
            get { return _Client; }
        }

        public string JID
        {
            set { _JID = value; }
            get { return _JID; }
        }

        public DateTime InternalDuedate
        {
            set { _InternalDuedate = value; }
            get { return _InternalDuedate; }
        }
        public string ProdSite
        {
            set { _ProdSite = value; }
            get { return _ProdSite; }
        }
        public string Stage
        {
            set { _Stage = value; }
            get { return _Stage; }
        }
      
   
       
       
        public string TraRoot
        {
            set { _TraRoot = value; }
            get { return _TraRoot; }
        }
        public string Tracode
        {
            set { _Tracode = value; }
            get { return _Tracode; }
        }
        public string Fasnumero
        {
            set { _Fasnumero = value; }
            get { return _Fasnumero; }
        }
        public string Trajid
        {
            set { _Trajid = value; }
            get { return _Trajid; }
        }
        public string Aid
        {
            set { _Aid = value; }
            get { return _Aid; }
        }
        public string Pii
        {
            set { _Pii = value; }
            get { return _Pii; }
        }
        public string Doi
        {
            set { _Doi = value; }
            get { return _Doi; }
        }
        public string ItemTitle
        {
            set { _ItemTitle = value; }
            get { return _ItemTitle; }
        }
        public string ItemSubtitle
        {
            set { _ItemSubtitle = value; }
            get { return _ItemSubtitle; }
        }
        public string TraiteTitle
        {
            set { _TraiteTitle = value; }
            get { return _TraiteTitle; }
        }
        public string ColorModel
        {
            set { _ColorModel = value; }
            get { return _ColorModel; }
        }
        public string Issn
        {
            set { _Issn = value; }
            get { return _Issn; }
        }
        public string Isbn
        {
            set { _Isbn = value; }
            get { return _Isbn; }
        }
        public string TypesettingModelFormat
        {
            set { _TypeSettingModelFormat = value; }
            get { return _TypeSettingModelFormat; }
        }
        public string ArticleType
        {
            set { _ArticleType = value; }
            get { return _ArticleType; }
        }
        public string MajNo
        {
            set { _MajNo = value; }
            get { return _MajNo; }
        }
        public string MajCote
        {
            set { _MajCote = value; }
            get { return _MajCote; }
        }
        public string MajAnne
        {
            set { _MajAnne = value; }
            get { return _MajAnne; }
        }
        public string Vol
        {
            set { _Vol = value; }
            get { return _Vol; }
        }
        public string Chaptre
        {
            set { _Chaptre = value; }
            get { return _Chaptre; }
        }
        public string Subchaptre
        {
            set { _Subchaptre = value; }
            get { return _Subchaptre; }
        }
        public string NbPagesCommande
        {
            set { _NbPagesCommande = value; }
            get { return _NbPagesCommande; }
        }
        public string NbPagesEstimate
        {
            set { _NbPagesEstimate = value; }
            get { return _NbPagesEstimate; }
        }
        public string NbTableau
        {
            set { _NbTableau = value; }
            get { return _NbTableau; }
        }
        public string NbFig
        {
            set { _NbFig = value; }
            get { return _NbFig; }
        }
        public string NbPhoto
        {
            set { _NbPhoto = value; }
            get { return _NbPhoto; }
        }
        public string NbDessin
        {
            set { _NbDessin = value; }
            get { return _NbDessin; }
        }
        public string NbArbrePapier
        {
            set { _NbArbrePapier = value; }
            get { return _NbArbrePapier; }
        }
        public string NbEncadreT1
        {
            set { _NbEncadreT1 = value; }
            get { return _NbEncadreT1; }
        }
        public string NbEncadreT2
        {
            set { _NbEncadreT2 = value; }
            get { return _NbEncadreT2; }
        }
        public string NbBiblio
        {
            set { _NbBiblio = value; }
            get { return _NbBiblio; }
        }
        public string NbBiblioSs
        {
            set { _NbBiblioSs = value; }
            get { return _NbBiblioSs; }
        }
        public string NbSavoirPlus
        {
            set { _NbSavoirPlus = value; }
            get { return _NbSavoirPlus; }
        }
        public string ResumeEn
        {
            set { _ResumeEn = value; }
            get { return _ResumeEn; }
        }
        public string ResumeFr
        {
            set { _ResumeFr = value; }
            get { return _ResumeFr; }
        }
        public string McEn
        {
            set { _McEn = value; }
            get { return _McEn; }
        }
        public string McFr
        {
            set { _McFr = value; }
            get { return _McFr; }
        }
        public string TitreEn
        {
            set { _TitreEn = value; }
            get { return _TitreEn; }
        }
        public string NbArbreIntractif
        {
            set { _NbArbreIntractif = value; }
            get { return _NbArbreIntractif; }
        }
        public string NbIconoSup
        {
            set { _NbIconoSup = value; }
            get { return _NbIconoSup; }
        }
        public string NbVideo
        {
            set { _NbVideo = value; }
            get { return _NbVideo; }
        }
        public string NbDocLegaux
        {
            set { _NbDocLegaux = value; }
            get { return _NbDocLegaux; }
        }
        public string NbFichePatient
        {
            set { _NbFichePatient = value; }
            get { return _NbFichePatient; }
        }
        public string NbFicheTech
        {
            set { _NbFicheTech = value; }
            get { return _NbFicheTech; }
        }
        public string NbAutoeval
        {
            set { _NbAutoeval = value; }
            get { return _NbAutoeval; }
        }
        public string NbClinique
        {
            set { _NbClinique = value; }
            get { return _NbClinique; }
        }
        public string NbQuotidien
        {
            set { _NbQuotidien = value; }
            get { return _NbQuotidien; }
        }
        public string Lblvide
        {
            set { _Lblvide = value; }
            get { return _Lblvide; }
        }
        public string Nbvide
        {
            set { _Nbvide = value; }
            get { return _Nbvide; }
        }
        public string AppelIcono
        {
            set { _AppelIcono = value; }
            get { return _AppelIcono; }
        }
        public string AppelBiblio
        {
            set { _AppelBiblio = value; }
            get { return _AppelBiblio; }
        }
        public string IconoOk
        {
            set { _IconoOk = value; }
            get { return _IconoOk; }
        }
        public string ArbreDeci
        {
            set { _ArbreDeci = value; }
            get { return _ArbreDeci; }
        }
        public string PrincAuthorNom
        {
            set { _PrincAuthorNom = value; }
            get { return _PrincAuthorNom; }
        }
        public string PrincAuthorPnom
        {
            set { _PrincAuthorPnom = value; }
            get { return _PrincAuthorPnom; }
        }
        public string PrincAuthorAff
        {
            set { _PrincAuthorAff = value; }
            get { return _PrincAuthorAff; }
        }
        public string SecondAuthorNom
        {
            set { _SecondAuthorNom = value; }
            get { return _SecondAuthorNom; }
        }
        public string SecondAuthorPnom
        {
            set { _SecondAuthorPnom = value; }
            get { return _SecondAuthorPnom; }
        }
        public string SecondAuthorAff
        {
            set { _SecondAuthorAff = value; }
            get { return _SecondAuthorAff; }
        }
        public string CorrAuthorPhone
        {
            set { _CorrAuthorPhone = value; }
            get { return _CorrAuthorPhone; }
        }
        public string CorrAuthorFAX
        {
            set { _CorrAuthorFAX = value; }
            get { return _CorrAuthorFAX; }
        }
        public string CorrAuthorEmail
        {
            set { _CorrAuthorEmail = value; }
            get { return _CorrAuthorEmail; }
        }
        #endregion

        #region FreshProcess
        public string CreateXMLOrder()
        {
            XmlReader reader;
            string OrderPath = GetOrderPath();
            _DirectoryPath = OrderPath;
            string OrderFileName = OrderPath + "\\" + _Client + "_" + _JID + "_" + _Stage + "_" + Aid + "_Order_" + GetNextFileNo() + ".xml";
            string XMLFIlePath = ConfigDetails.EMCTemplatePath;

            if (!File.Exists(XMLFIlePath))
            {
                return "";
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ProhibitDtd = false;
            settings.IgnoreWhitespace = false;
            reader = XmlReader.Create(XMLFIlePath, settings);
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(reader);
            reader.Close();

            textWriter = new XmlTextWriter(OrderFileName, null);

            textWriter.Indentation = 4;
            textWriter.IndentChar = '\t';

            textWriter.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n");
            textWriter.WriteRaw("<!DOCTYPE orders SYSTEM \"emc_order.dtd\">" + "\n");

            SearchNode(myXmlDocument.DocumentElement);

            textWriter.Flush();
            textWriter.Close();
            textWriter = null;

            return OrderFileName;
        }

        private void SearchNode(XmlNode node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    bool endElement;
                    XmlNodeList nodeList;
                    ProcessNode(node, out endElement);
                    nodeList = node.ChildNodes;
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        SearchNode(nodeList[i]);
                    }
                    if (endElement)
                    {
                        textWriter.WriteEndElement();
                    }
                    break;
                case XmlNodeType.Text:
                    textWriter.WriteString(node.InnerText);
                    break;
                case XmlNodeType.CDATA:
                    break;
                case XmlNodeType.ProcessingInstruction:
                    break;
                case XmlNodeType.Comment:
                    textWriter.WriteRaw(node.Value);
                    break;
                case XmlNodeType.XmlDeclaration:
                    break;
                case XmlNodeType.Document:
                    break;
                case XmlNodeType.DocumentType:
                    break;
                case XmlNodeType.EntityReference:
                    break;
                case XmlNodeType.EndElement:
                    break;
                case XmlNodeType.Entity:

                    break;
                case XmlNodeType.Whitespace:
                    textWriter.WriteRaw(node.Value);
                    break;
            }
            textWriter.Flush();
        }

        private void ProcessNode(XmlNode node, out bool endElement)
        {
            endElement = true;
            switch (node.Name)
            {
                case "orders":
                    Default(node);
                    break;
                case "order":
                    textWriter.WriteStartElement("order");
                    textWriter.WriteAttributeString("","EMC");
                    textWriter.WriteAttributeString("category","JOURNAL");
                    break;
                case "time":
                    textWriter.WriteStartElement(node.Name);

                    textWriter.WriteAttributeString("day",   DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("month", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("yr",    DateTime.Now.Year.ToString().PadLeft(2, '0'));

                    textWriter.WriteAttributeString("hr",  DateTime.Now.Hour.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("min", DateTime.Now.Minute.ToString().PadLeft(2, '0'));
                    textWriter.WriteAttributeString("sec", DateTime.Now.Second.ToString().PadLeft(2, '0'));
                    break;
                case "date":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("received-date"))
                    {
                        textWriter.WriteAttributeString("day",   DateTime.Now.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr",    DateTime.Now.Year.ToString().PadLeft(2, '0'));
                    }
                    else if (node.ParentNode.Name.Equals("due-date"))
                    {
                        string[] arr = _InternalDuedate.ToShortDateString().Split('/');
                        if (arr.Length > 2)
                        {

                            textWriter.WriteAttributeString("day",   _InternalDuedate.Day.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("month", _InternalDuedate.Month.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("yr",    _InternalDuedate.Year.ToString());
                        }
                    }
                    break;
                case "prod-site":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ProdSite;
                    break;
                case "stage":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteAttributeString("step", _Stage);
                    break;
                case "executor":
                    Default(node);
                    break;
                case "tra-root":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _TraRoot;
                    break;
                case "tracode":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Tracode;
                    break;
                case "fasnumero":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Fasnumero;
                    break;
                case "trajid":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Trajid;
                    break;
                case "aid":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Aid;
                    break;
                case "pii":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Pii;
                    break;
                case "doi":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Doi;
                    break;
                case "item-title":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ItemTitle;
                    break;
                case "item-subtitle":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ItemSubtitle;
                    break;
                case "traite-title":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _TraiteTitle;
                    break;
                case "color-model":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ColorModel;
                    break;
                case "issn":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Issn;
                    break;
                case "isbn":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Isbn;
                    break;
                case "typesetting-model-format":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _TypeSettingModelFormat;
                    break;
                case "article-type":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArticleType;
                    break;
                case "maj-no":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _MajNo;
                    break;
                case "maj-cote":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _MajCote;
                    break;
                case "maj-anne":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _MajAnne;
                    break;
                case "vol":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Vol;
                    break;
                case "chaptre":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Chaptre;
                    break;
                case "subchaptre":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Subchaptre;
                    break;
                case "nb-pages-commande":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbPagesCommande;
                    break;
                case "nb-pages-estimate":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbPagesEstimate;
                    break;
                case "nb-tableau":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbTableau;
                    break;
                case "nb-fig":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbFig;
                    break;
                case "nb-photo":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbPhoto;
                    break;
                case "nb-dessin":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbDessin;
                    break;
                case "nb-arbre-papier":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbArbrePapier;
                    break;
                case "nb-encadre-t1":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbEncadreT1;
                    break;
                case "nb-encadre-t2":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbEncadreT2;
                    break;
                case "nb-biblio":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbBiblio;
                    break;
                case "nb-biblio-ss":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbBiblioSs;
                    break;
                case "nb-savoir-plus":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbSavoirPlus;
                    break;
                case "resume-en":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ResumeEn;
                    break;
                case "resume-fr":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ResumeFr;
                    break;
                case "mc-en":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _McEn;
                    break;
                case "mc-fr":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _McFr;
                    break;
                case "titre-en":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _TitreEn;
                    break;
                case "nb-arbre-intractif":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbArbreIntractif;
                    break;
                case "nb-icono-sup":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbIconoSup;
                    break;
                case "nb-video":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbVideo;
                    break;
                case "nb-doc-legaux":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbDocLegaux;
                    break;
                case "nb-fiche-patient":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbFichePatient;
                    break;
                case "nb-fiche-tech":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbFicheTech;
                    break;
                case "nb-autoeval":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbAutoeval;
                    break;
                case "nb-clinique":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbClinique;
                    break;
                case "nb-quotidien":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _NbQuotidien;
                    break;
                case "autre-nb":
                    Default(node);
                    break;
                case "lblvide":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Lblvide;
                    break;
                case "nbvide":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _Nbvide;
                    break;
                case "appel-icono":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _AppelIcono;
                    break;
                case "appel-biblio":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _AppelBiblio;
                    break;
                case "icono-ok":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _IconoOk;
                    break;
                case "arbre-deci":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArbreDeci;
                    break;
                case "princ-author":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteElementString("nom",  _PrincAuthorNom);
                    textWriter.WriteElementString("pnom", _PrincAuthorPnom);
                    textWriter.WriteElementString("aff",  _PrincAuthorAff);
                    break;
                case "second-author":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteElementString("nom",  _SecondAuthorNom);
                    textWriter.WriteElementString("pnom", _SecondAuthorPnom);
                    textWriter.WriteElementString("aff",  _SecondAuthorAff);
                    break;
                case "corr-author":
                    textWriter.WriteStartElement(node.Name);
                    textWriter.WriteElementString("phone", _CorrAuthorPhone);
                    textWriter.WriteElementString("fax",   _CorrAuthorFAX);
                    textWriter.WriteElementString("email", _CorrAuthorEmail);
                    break;
                case "nom":
                    endElement = false;
                    break;
                case "pnom":
                    endElement = false;
                    break;
                case "aff":
                    if ("princ-author#second-author".IndexOf(node.ParentNode.Name) != -1)
                        endElement = false;
                    else
                        Default(node);
                    break;
                case "phone":
                    endElement=false;
                    break;
                case "fax":
                    if (node.ParentNode.Name.Equals("aff"))
                    {textWriter.WriteStartElement(node.Name);}
                    else
                    { endElement = false; }
                    break;
                case "email":
                    endElement = false;
                    break;
                default:
                    Default(node);
                    if ("".IndexOf(node.Name) != -1)
                    {
                        node.InnerXml = "";
                    }
                    break;
            }
            return;
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

        private int GetNextFileNo()
        {
            int MaxNo = 0;
            if (Directory.Exists(_DirectoryPath))
                MaxNo = Directory.GetFiles(_DirectoryPath, "*.xml").Length;

            return MaxNo;
        }

        private string GetOrderPath()
        {
            if (ConfigDetails.RootPath != null)
            {
                if (!ConfigDetails.RootPath.Equals(""))
                {
                    if (!Directory.Exists(ConfigDetails.RootPath))
                        Directory.CreateDirectory(ConfigDetails.RootPath);
                }
            }

            string OrderPath = ConfigDetails.RootPath.Trim(new char[] { '\\' }) + "\\" + _Client + "\\" + _JID + "\\" + _Aid + "\\" + _Stage;
            if (!Directory.Exists(OrderPath))
                Directory.CreateDirectory(OrderPath);

            return OrderPath;
        }
        #endregion

        #region ReviseProcess
        public bool RevisedXMLOrder()
        {
            XmlTextReader Reader;
            string OrderPath = GetOrderPath();
            _DirectoryPath = OrderPath;
            string OrderFileName = OrderPath + "\\" + _Client + "_" + _JID + "_" + _Stage + "_" + _Aid + "_Order_" + (GetNextFileNo() - 1) + ".xml";
            if (!File.Exists(OrderFileName))
                return false;


            Reader = new XmlTextReader(OrderFileName);
            Reader.XmlResolver = null;
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(Reader);
            Reader.Close();

            _SearchNode(myXmlDocument.DocumentElement);
            return true;
        }
        private void _SearchNode(XmlNode node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    XmlNodeList nodeList;
                    ProcessNode(node);
                    nodeList = node.ChildNodes;
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        _SearchNode(nodeList[i]);
                    }

                    break;
                case XmlNodeType.Text:
                    break;
                case XmlNodeType.CDATA:
                    break;
                case XmlNodeType.ProcessingInstruction:
                    break;
                case XmlNodeType.Comment:
                    break;
                case XmlNodeType.XmlDeclaration:
                    break;
                case XmlNodeType.Document:
                    break;
                case XmlNodeType.DocumentType:
                    break;
                case XmlNodeType.EntityReference:
                    break;
                case XmlNodeType.EndElement:
                    break;
                case XmlNodeType.Entity:
                    break;
                case XmlNodeType.Whitespace:

                    break;
            }

        }
        private void ProcessNode(XmlNode node)
        {
            switch (node.Name)
            {
                case "tra-root":
                    {
                        _TraRoot = node.InnerXml;
                        break;
                    }
                case "tracode":
                    {
                        _Tracode = node.InnerXml;
                        break;
                    }
                case "fasnumero":
                    {
                        _Fasnumero = node.InnerXml;
                        break;
                    }
                case "trajid":
                    {
                        _Trajid = node.InnerXml;
                        break;
                    }
                case "aid":
                    {
                        _Aid=  node.InnerXml;
                        break;
                    }
                case "pii":
                    {
                        _Pii = node.InnerXml;
                        break;
                    }
                case "doi":
                    {
                        _Doi = node.InnerXml;
                        break;
                    }
                case "item-title":
                    {
                        _ItemTitle = node.InnerXml;
                        break;
                    }
                case "item-subtitle":
                    {
                        _ItemSubtitle = node.InnerXml;
                        break;
                    }
                case "traite-title":
                    {
                        _TraiteTitle = node.InnerXml;
                        break;
                    }
                case "color-model":
                    {
                        _ColorModel = node.InnerXml;
                        break;
                    }
                case "issn":
                    {
                        _Issn = node.InnerXml;
                        break;
                    }
                case "isbn":
                    {
                        _Isbn = node.InnerXml;
                        break;
                    }
                case "typesetting-model-format":
                    {
                        _TypeSettingModelFormat = node.InnerXml;
                        break;
                    }
                case "article-type":
                    {
                        _ArticleType = node.InnerXml;
                        break;
                    }
                case "maj-no":
                    {
                        _MajNo= node.InnerXml;
                        break;
                    }
                case "maj-cote":
                    {
                        _MajCote = node.InnerXml;
                        break;
                    }
                case "maj-anne":
                    {
                        _MajAnne = node.InnerXml;
                        break;
                    }
                case "vol":
                    {
                        _Vol = node.InnerXml;
                        break;
                    }
                case "chaptre":
                    {
                        _Chaptre = node.InnerXml;
                        break;
                    }
                case "subchaptre":
                    {
                        _Subchaptre = node.InnerXml;
                        break;
                    }
                case "nb-pages-commande":
                    {
                        _NbPagesCommande = node.InnerXml;
                        break;
                    }
                case "nb-pages-estimate":
                    {
                        _NbPagesEstimate = node.InnerXml;
                        break;
                    }
                case "nb-tableau":
                    {
                        _NbTableau = node.InnerXml;
                        break;
                    }
                case "nb-fig":
                    {
                        _NbFig = node.InnerXml;
                        break;
                    }
                case "nb-photo":
                    {
                        _NbPhoto = node.InnerXml;
                        break;
                    }
                case "nb-dessin":
                    {
                        _NbDessin = node.InnerXml;
                        break;
                    }
                case "nb-arbre-papier":
                    {
                        _NbArbrePapier = node.InnerXml;
                        break;
                    }
                case "nb-encadre-t":
                    {
                        _NbEncadreT1 = node.InnerXml;
                        break;
                    }
                case "nb-biblio":
                    {
                        _NbBiblio = node.InnerXml;
                        break;
                    }
                case "nb-biblio-ss":
                    {
                        _NbBiblioSs = node.InnerXml;
                        break;
                    }
                case "nb-savoir-plus":
                    {
                        _NbSavoirPlus = node.InnerXml;
                        break;
                    }
                case "resume-en":
                    {
                        _ResumeEn = node.InnerXml;
                        break;
                    }
                case "resume-fr":
                    {
                        _ResumeFr = node.InnerXml;
                        break;
                    }
                case "mc-en":
                    {
                        _McEn  = node.InnerXml;
                        break;
                    }
                case "mc-fr":
                    {
                        _McFr = node.InnerXml;
                        break;
                    }
                case "titre-en":
                    {
                        _TitreEn = node.InnerXml;
                        break;
                    }
                case "nb-arbre-intractif":
                    {
                        _NbArbreIntractif = node.InnerXml;
                        break;
                    }
                case "nb-icono-sup":
                    {
                        _NbIconoSup = node.InnerXml;
                        break;
                    }
                case "nb-video":
                    {
                        _NbVideo = node.InnerXml;
                        break;
                    }
                case "nb-doc-legaux":
                    {
                        _NbDocLegaux = node.InnerXml;
                        break;
                    }
                case "nb-fiche-patient":
                    {
                        _NbFichePatient = node.InnerXml;
                        break;
                    }
                case "nb-fiche-tech":
                    {
                        _NbFicheTech = node.InnerXml;
                        break;
                    }
                case "nb-autoeval":
                    {
                        _NbAutoeval = node.InnerXml;
                        break;
                    }
                case "nb-clinique":
                    {
                        _NbClinique = node.InnerXml;
                        break;
                    }
                case "nb-quotidien":
                    {
                        _NbQuotidien = node.InnerXml;
                        break;
                    }
                case "lblvide":
                    {
                        _Lblvide = node.InnerXml;
                        break;
                    }
                case "nbvide":
                    {
                        _Nbvide = node.InnerXml;
                        break;
                    }
                case "appel-icono":
                    {
                        _AppelIcono = node.InnerXml;
                        break;
                    }
                case "appel-biblio":
                    {
                        _AppelBiblio = node.InnerXml;
                        break;
                    }
                case "icono-ok":
                    {
                        _IconoOk = node.InnerXml;
                        break;
                    }
                case "arbre-deci":
                    {
                        _ArbreDeci = node.InnerXml;
                        break;
                    }
                case "nom":
                    {
                        if (node.ParentNode.Name.Equals("princ-author"))
                            _PrincAuthorNom  = node.InnerXml;
                        else if (node.ParentNode.Name.Equals("second-author"))
                            _SecondAuthorNom = node.InnerXml;

                        break;
                    }
                case "pnom":
                    {
                        if (node.ParentNode.Name.Equals("princ-author"))
                            _PrincAuthorPnom  = node.InnerXml;
                        else if (node.ParentNode.Name.Equals("second-author"))
                            _SecondAuthorPnom = node.InnerXml;
                        break;
                    }
                case "phone":
                    {
                        if (node.ParentNode.Name.Equals("corr-author"))
                            _CorrAuthorPhone = node.InnerXml;
                        
                        break;
                    }
                case "fax":
                    {
                        if (node.ParentNode.Name.Equals("corr-author"))
                            _CorrAuthorFAX = node.InnerXml;

                        break;
                    }
                case "email":
                    {
                        if (node.ParentNode.Name.Equals("corr-author"))
                            _CorrAuthorEmail = node.InnerXml;

                        break;
                    }
            }
            return;
        }
        #endregion
    }
    public class TRACodeInfo
    {
        string _TRACode, _TRAJID, _TRATitle, _ISSN, _ISBN, _TYpeSettingMOdelFormat, _ColorModel="";

        public string TRACode
        {
            get { return _TRACode; }
            set {  _TRACode=value; }
        }

        public string TRAJID
        {
            get { return _TRAJID; }
            set { _TRAJID = value; }
        }

        public string TRATitle
        {
            get { return _TRATitle; }
            set { _TRATitle = value; }
        }

        public string ISSN
        {
            get { return _ISSN; }
            set { _ISSN = value; }
        }

        public string ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        public string TypeSettingModelFormat
        {
            get { return _TYpeSettingMOdelFormat; }
            set { _TYpeSettingMOdelFormat = value; }
        }

        public string ColorModel
        {
            get { return _ColorModel; }
            set { _ColorModel = value; }
        }
    }
}





