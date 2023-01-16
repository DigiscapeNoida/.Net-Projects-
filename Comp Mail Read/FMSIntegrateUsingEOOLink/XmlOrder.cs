using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace FMSIntegrateUsingEOOLink
{
    public static class ConfigDetails
    {
        static string _FMSPath = "";
        static string _RootPath           = "";
        static string _TemplatePath       = "";
        static string _Application3ConStr = "";
        static string _ACRDownloadPath    = "";

       
        static  ConfigDetails()
        {

            string[] KEYS = ConfigurationSettings.AppSettings.AllKeys;
            foreach (string key in KEYS)
            {
                switch (key)
                {
                    case "TemplatePath":
                        _TemplatePath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "FmsPath":
                        _FMSPath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "ROOTPATH":
                        _RootPath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "ACRDownloadPath":
                        _ACRDownloadPath = ConfigurationSettings.AppSettings[key];
                        break;
                }
            }
        }

      

        public static string TemplatePath
        {
            set { _TemplatePath = value; } 
            get { return _TemplatePath; } 
        }
        
        public static string FMSPath
        {
            set { _FMSPath = value; }
            get { return _FMSPath; }
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
        public static string ACRDownloadPath 
        {
            set { _ACRDownloadPath = value; }
            get { return _ACRDownloadPath; } 
        }
        
    }
    class XmlOrder   
    {
        static XmlTextWriter textWriter;
        #region Private Variabele
        private string      _DirectoryPath = "";
        private ArticleInfo _ArticlInfo    = null;

        private string _WorkFlow      = "";
        private string _JTitle        = "";
        private string _Client        = "";
        private string _JID           = "";
        private string _AID           = "";
        private string _Stage         = "";
        private string _FMSStage      = "";
        private string _DOI           = "";
        private string _Volume        = "0";
        private string _Issue         = "0";
        private string _Figs            = "0";
        private string _ArticleCategory = "";
        private string _ArticleType     = "";
        private string _MSS             = "";


        private string _Editor      = "";
        private string _EditorMail = "";
        private string _Designation = "";
        private string _Address     = "";
        private string _Tel         = "";
        private string _Fax         = "";
        private string _OrderFileName = string.Empty;
        private DateTime _ReceivedDate;
        private DateTime _RevisedDate ;
        private DateTime _AcceptedDate ;
        private DateTime _ActutalDueDate ;
        private DateTime _InternalDuedate ;
        private string _SupplementaryMaterial = "";
        #endregion
        #region Public Property
        public XmlOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public XmlOrder(ArticleInfo ArticlInfo)
        {
            try
            {

                _ArticlInfo = ArticlInfo;

                StaticInfo.WriteLogMsg.AppendLog("_JID ="  + _ArticlInfo.JID);
                StaticInfo.WriteLogMsg.AppendLog("_AID = " + _ArticlInfo.AID);

                
                _Client = "JWUSA";
                _JID = _ArticlInfo.JID;
                _AID = _ArticlInfo.AID;
                _FMSStage = "S100";
                _Stage = "Fresh";
                _DOI = _ArticlInfo.AID;
                if (_ArticlInfo.Figs != null) _Figs = _ArticlInfo.Figs;


                


                if (!string.IsNullOrEmpty(_ArticlInfo.ManuscriptType))
                     _ArticleCategory = _ArticlInfo.ManuscriptType.Replace("&", "&amp;"); ;

                StaticInfo.WriteLogMsg.AppendLog("_ArticleCategory = " + _ArticleCategory);

                StaticInfo.WriteLogMsg.AppendLog("_ArticleType = " + _ArticleType);

                _ArticleType = _ArticleCategory;


                if (string.IsNullOrEmpty(_ArticlInfo.ManuscriptPages))
                {
                    _ArticlInfo.ManuscriptPages = "0";
                }

                StaticInfo.WriteLogMsg.AppendLog("ManuscriptPages = " + _ArticlInfo.ManuscriptPages);
                _MSS = _ArticlInfo.ManuscriptPages;


                if (!string.IsNullOrEmpty(_ArticlInfo.Accepted))
                    _AcceptedDate = GetDateTime(_ArticlInfo.Accepted);

                if (!string.IsNullOrEmpty(_ArticlInfo.Revised))
                    _RevisedDate = GetDateTime(_ArticlInfo.Revised);

                if (!string.IsNullOrEmpty(_ArticlInfo.Received))
                    _ReceivedDate = GetDateTime(_ArticlInfo.Received);
                else
                    _ReceivedDate = DateTime.Today;


                int DaystoAdd = Convert.ToInt16(StaticInfo.USTATDays[_JID].ToString());

                StaticInfo.WriteLogMsg.AppendLog("DaystoAdd = " + DaystoAdd.ToString());
                
                DateTime actdt = DateTime.Today;
                actdt = AddDaysNoWeekends(actdt, DaystoAdd);
                _InternalDuedate = actdt;


                StaticInfo.WriteLogMsg.AppendLog("AssignWorkflow = ");
                AssignWorkflow();

                _WorkFlow = _WorkFlow.Replace("/", "_");

                StaticInfo.WriteLogMsg.AppendLog("GetEditorDetails");
                GetEditorDetails(_JID, _Client);

                StaticInfo.WriteLogMsg.AppendLog("GetOrderPath");
                string OrderPath = GetOrderPath();
                _DirectoryPath = OrderPath;
                _OrderFileName = OrderPath + "\\" + _Client + "_" + JID + "_" + _Stage + "_" + AID + "_Order_" + GetNextFileNo() + ".xml";
            }
            catch (Exception ex)
            {
                StaticInfo.WriteLogMsg.AppendLog(ex);
            }
        }
        public DateTime AddDaysNoWeekends(DateTime dt, int Days)
        {
            int DayIcr = 0;
            while (true)
            {
                dt = dt.AddDays(1);
                //  if (   dt.DayOfWeek           == DayOfWeek.Saturday 
                // || dt.DayOfWeek           == DayOfWeek.Sunday
                // || dt.ToShortDateString() == "1/26/2012"
                //   || dt.ToShortDateString() == "2/20/2012"
                // || dt.ToShortDateString() == "2/28/2012"
                //  || dt.ToShortDateString() == "3/9/2012"
                // || dt.ToShortDateString() == "8/2/2012"
                // || dt.ToShortDateString() == "8/15/2012"
                // || dt.ToShortDateString() == "8/20/2012"
                //|| dt.ToShortDateString() == "10/24/2012"
                // || dt.ToShortDateString() == "10/2/2012"
                // || dt.ToShortDateString() == "11/13/2012"
                //|| dt.ToShortDateString() == "11/28/2012"
                // || dt.ToShortDateString() == "12/25/2012"
                //|| dt.ToShortDateString() == "12/31/2011
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday
                    || dt.ToShortDateString() == "1/1/2013"
                    || dt.ToShortDateString() == "3/27/2013"
                    || dt.ToShortDateString() == "4/24/2013"
                    || dt.ToShortDateString() == "8/9/2013"
                    || dt.ToShortDateString() == "8/15/2013"
                    || dt.ToShortDateString() == "8/20/2013"
                    || dt.ToShortDateString() == "10/02/2013"
                    || dt.ToShortDateString() == "10/14/2013"
                    || dt.ToShortDateString() == "11/02/2013"
                    || dt.ToShortDateString() == "12/25/2013"
                    || dt.ToShortDateString() == "12/31/2013")
                {
                }
                else
                    DayIcr++;

                if (DayIcr == Days) break;
            }
            return dt;
        }

        private void AssignWorkflow()
        {
            if ((_JID.ToUpper().Trim() == "CJCE")
                || (_JID.ToUpper().Trim() == "BIOM")
                || (_JID.ToUpper().Trim() == "CJS"))
            {
                if (_ArticlInfo.isTexFileExist)
                    _WorkFlow = @"JW_NEW_Workflow_With_Graphics_WITHOUT/LOGIN";
                else 
                    _WorkFlow = @"JW(UK) JOURNAL/FRESH/COMPLETE_WITHOUT/LOGIN";
            }
             else if ((_JID.ToUpper().Trim() == "IEAM")
                    || (_JID.ToUpper().Trim() == "ETC")
                    || (_JID.ToUpper().Trim() == "AFDR")//Changes Done By Ajay Add new journals[22/7/2012)
                    || (_JID.ToUpper().Trim() == "ECNO")
                    || (_JID.ToUpper().Trim() == "EUFM")
                    || (_JID.ToUpper().Trim() == "FLAN")
                    || (_JID.ToUpper().Trim() == "JSCH")
                    || (_JID.ToUpper().Trim() == "MODL")
                    || (_JID.ToUpper().Trim() == "JABA")
                    || (_JID.ToUpper().Trim() == "JEAB")
            )
            {
                _WorkFlow = @"WORKFLOW OF JW(VCH) JOURNAL/ FRESH/COMPLETE/WITHOUT_LOGIN";
            }
            else if ((_JID.ToUpper().Trim() == "JBMR")
                  || (_JID.ToUpper().Trim() == "FLAN")
                  || (_JID.ToUpper().Trim() == "POLQ")
                  || (_JID.ToUpper().Trim() == "MODL"))
            {
                _WorkFlow = @"JW(UK) JOURNAL/FRESH/W/O-COPYING_WITHOUT/LOGIN";
            }
            else if ((_JID.ToUpper().Trim() == "AJIM")
                || (_JID.ToUpper().Trim() == "AJMGA")
                || (_JID.ToUpper().Trim() == "AJMGB")
                || (_JID.ToUpper().Trim() == "AJMGC")
                || (_JID.ToUpper().Trim() == "AJT")
                || (_JID.ToUpper().Trim() == "BIT")
                || (_JID.ToUpper().Trim() == "JEZ")
                || (_JID.ToUpper().Trim() == "JEZB")
                || (_JID.ToUpper().Trim() == "DEV")
                || (_JID.ToUpper().Trim() == "JCB")
                || (_JID.ToUpper().Trim() == "JCP")
                || (_JID.ToUpper().Trim() == "JMV")
                || (_JID.ToUpper().Trim() == "JPS")
                || (_JID.ToUpper().Trim() == "PPUL")
                || (_JID.ToUpper().Trim() == "BEM")
                || (_JID.ToUpper().Trim() == "JSO")
                || (_JID.ToUpper().Trim() == "LSM")
                || (_JID.ToUpper().Trim() == "MAS")
                || (_JID.ToUpper().Trim() == "MC")
                || (_JID.ToUpper().Trim() == "MRD")
                || (_JID.ToUpper().Trim() == "NUR")
                || (_JID.ToUpper().Trim() == "PROS")
                || (_JID.ToUpper().Trim() == "CAE")
                || (_JID.ToUpper().Trim() == "JOR")
                || (_JID.ToUpper().Trim() == "PBC")
                || (_JID.ToUpper().Trim() == "NAU")
                || (_JID.ToUpper().Trim() == "TEA")
                || (_JID.ToUpper().Trim() == "AJAD")
                || (_JID.ToUpper().Trim() == "BIOM")
                || (_JID.ToUpper().Trim() == "MONO")
                || (_JID.ToUpper().Trim() == "VSU")
                || (_JID.ToUpper().Trim() == "AB")
                || (_JID.ToUpper().Trim() == "AJP")
                || (_JID.ToUpper().Trim() == "FUT")
                || (_JID.ToUpper().Trim() == "NAU")
                || (_JID.ToUpper().Trim() == "JCPH")
                || (_JID.ToUpper().Trim() == "CPDD")
                || (_JID.ToUpper().Trim() == "TEA")
                || (_JID.ToUpper().Trim() == "JABA")
                || (_JID.ToUpper().Trim() == "JEAB")
                || (_JID.ToUpper().Trim() == "SCA")
                || (_JID.ToUpper().Trim() == "ZOO")
                || (_JID.ToUpper().Trim() == "POI3")
                || (_JID.ToUpper().Trim() == "POP4")
                || (_JID.ToUpper().Trim() == "RHC3")
                || (_JID.ToUpper().Trim() == "WMH3"))
            {
                _WorkFlow = @"JW(UK) JOURNAL/FRESH/COMPLETE_WITHOUT/LOGIN";
            }
            else
            {
                _WorkFlow = @"JW(US)JOURNAL/FRESH/COMPLETE_WITHOUT/LOGIN";
                //_WorkFlow = @"W/F FOR JW(FRESH)_FOR DAT JOURNAL_WITH GRAPHICS/WITHOUT_LOGIN";
                //_WorkFlow = @"JW(UK) JOURNAL/FRESH/W/O-COPYING_WITHOUT/LOGIN";
                //_WorkFlow = @"JW fresh New workflow for abstract with graphices";
            }

        }
        private DateTime GetDateTime(string DateStr)
        {
            string[] Month = "#JAN#FEB#MAR#APR#MAY#JUN#JUL#AUG#SEP#OCT#NOV#DEC".Split('#');
            string[] DatePart = DateStr.Split('-');
            int day = Int32.Parse(DatePart[0]);
            int mnth = Array.IndexOf(Month, DatePart[1].ToUpper());
            int year= Int32.Parse(DatePart[2]);
            DateTime Date = new DateTime(year, mnth, day);

            return Date;
            
        }


        public  string OrderFileName
        {
            get { return _OrderFileName; }
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
            set { _AID = value; }
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
            set { _Volume = value; }
            get { return _Volume; }
        }
        public string Issue
        {
            set { _Issue = value; }
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
        public  void CreateXMLOrder()
        {
            XmlReader reader;
            string XMLFIlePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Template\\Template.xml";
            if (!File.Exists(XMLFIlePath))
            {
                return;
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ProhibitDtd = false;
            settings.IgnoreWhitespace = false;
            reader = XmlReader.Create(XMLFIlePath, settings);
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = true;
            myXmlDocument.Load(reader);
            reader.Close();

            textWriter = new XmlTextWriter(_OrderFileName, null);

            textWriter.Indentation = 4;
            textWriter.IndentChar = '\t';

            textWriter.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n");
            textWriter.WriteRaw("<!DOCTYPE orders SYSTEM \"FMS-J-Order.dtd\">" + "\n");

            textWriter.WriteRaw("<?xml-stylesheet type=\"text/xsl\" href=\"WileyJ-Order.xsl\"?>" + Environment.NewLine);
            try
            {
                SearchNode(myXmlDocument.DocumentElement);
            }
            catch (Exception ex)
            { 
            }

            textWriter.Flush();
            textWriter.Close();
            textWriter = null;

            
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

                case "copyright-recd-date":
                    {
                        if (_AcceptedDate== DateTime.MinValue)
                        {
                            node.RemoveAll();
                            endElement = false;
                        }
                        else
                        {
                            textWriter.WriteStartElement(node.Name);
                        }
                        break;
                    }
                case "accept-date":
                    {
                        if (_AcceptedDate== DateTime.MinValue)
                        {
                            node.RemoveAll();
                            endElement = false;
                        }
                        else
                        {
                            textWriter.WriteStartElement(node.Name);
                        }
                        break;
                    }
                case "revised-date":
                        {
                            if (_RevisedDate== DateTime.MinValue)
                            {
                                node.RemoveAll();
                                endElement = false;
                            }
                            else
                            {
                                textWriter.WriteStartElement(node.Name);
                            }
                            break;
                        }
                case "received-date":
                       {
                           if (_ReceivedDate== DateTime.MinValue)
                           {
                               node.RemoveAll();
                               endElement = false;
                           }
                           else
                           {
                               textWriter.WriteStartElement(node.Name);
                           }
                           break;
                       }
                case "date":
                    
                    if (node.ParentNode.Name.Equals("revised-date"))
                            ProcessDate(node, _RevisedDate);
                    else if (node.ParentNode.Name.Equals("accept-date"))
                            ProcessDate(node, _AcceptedDate);
                    else if (node.ParentNode.Name.Equals("received-date"))
                            ProcessDate(node, _ReceivedDate);
                    else if (node.ParentNode.Name.Equals("copyright-recd-date"))
                            ProcessDate(node, _AcceptedDate);
                    else if (node.ParentNode.Name.Equals("due-date"))
                    {
                        string[] arr = _InternalDuedate.ToShortDateString().Split('/');
                        if (arr.Length > 2)
                        {
                            textWriter.WriteStartElement(node.Name);
                            textWriter.WriteAttributeString("day",   _InternalDuedate.Day.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("month", _InternalDuedate.Month.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("yr",    _InternalDuedate.Year.ToString());
                        }
                    }
                    break;
                case "jid":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _JID;
                    break;
                case "jname":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _JTitle.Replace("&", "&amp;");
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

        private void ProcessDate(XmlNode node, DateTime Date)
        {
            textWriter.WriteStartElement(node.Name);
            textWriter.WriteAttributeString("day",    Date.Day.ToString().PadLeft(2, '0'));
            textWriter.WriteAttributeString("month",  Date.Month.ToString().PadLeft(2, '0'));
            textWriter.WriteAttributeString("yr",     Date.Year.ToString().PadLeft(2, '0'));
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
            int MaxNo = 0;
            if (Directory.Exists(_DirectoryPath))
                MaxNo = Directory.GetFiles(_DirectoryPath, "*.xml").Length;

            //for testing if (MaxNo > 0) MaxNo--;
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
            if (!Directory.Exists(OrderPath))
                Directory.CreateDirectory(OrderPath);

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
                    break;
            }
            return;
        }



        protected void GetEditorDetails(string sJID, string sCust)
        {
           
                string StrSQL     = "Select Jname, Peditor, Designation, Pe_Email, Phone, Fax, Address from " + sCust + "_Journal1 where Jid='" + sJID + "'";
                SqlConnection AEPSCon = new SqlConnection(StaticInfo.AepsJwConfig);
                //AEPSCon.ConnectionTimeout = 1;

                AEPSCon.Open();
                SqlCommand AEPSCom = new SqlCommand(StrSQL, AEPSCon);
                try
                {
                    SqlDataReader AEPSDr = AEPSCom.ExecuteReader();

                    if (AEPSDr.HasRows == true)
                    {
                        while (AEPSDr.Read() == true)
                        {
                            _Editor      = AEPSDr[1].ToString();
                            _Designation = AEPSDr["Designation"].ToString();
                            _EditorMail  = AEPSDr["Pe_Email"].ToString();
                            _Tel         = AEPSDr["Phone"].ToString();
                            _Fax         = AEPSDr["Fax"].ToString();
                            _Address     = AEPSDr["Address"].ToString();
                            _JTitle      = AEPSDr[0].ToString();
                        }
                    }
                    AEPSDr.Close();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            

        }
#endregion

    }
    
    
}




