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
using ProcessNotification;
using System.Diagnostics;

namespace LWWAutoIntegrate
{
    public static class ConfigDetails
    {
        public static string OPSConnectionString
        { get; set; }
        public static string XMLOrderConnectionString
        { get; set; }

        static string _FMSPath = "";
        static string _TempPath = "";
        static string _RootPath           = "";
        static string _MoveInputFiles = "";
        
        static string _SrchPath       = "";
        static string _INTEGRATESERVERIP = "";
        
        static string _EXELoc = string.Empty;
       
        static  ConfigDetails()
        {
            OPSConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
           
            _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] KEYS = ConfigurationSettings.AppSettings.AllKeys;
            foreach (string key in KEYS)
            {
                switch (key)
                {
                     case "FmsPath":
                        _FMSPath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "ROOTPATH":
                        _RootPath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "SRCHPATH":
                        _SrchPath = ConfigurationSettings.AppSettings[key];
                        break;
                    case "INTEGRATESERVERIP":
                        _INTEGRATESERVERIP = ConfigurationSettings.AppSettings[key];
                        break;
                    case "MoveInputFiles":
                        _MoveInputFiles = ConfigurationSettings.AppSettings[key];
                        break;
                    case "TempPath":
                        _TempPath = ConfigurationSettings.AppSettings[key];
                        break;                     
                }
            }
        }


        public static string EXELoc { get { return _EXELoc; } }
        public static string SrchPath
        {
            set { _SrchPath = value; }
            get { return _SrchPath; } 
        }
      
        public static string FMSPath
        {
            set { _FMSPath = value; }
            get { return _FMSPath; }
        }
        public static string TempPath
        {
            set { _TempPath = value; }
            get { return _TempPath; }
        }
        public static string RootPath 
        { 
            set { _RootPath = value; } 
            get { return _RootPath; }
        }
         public static string MoveInputFiles 
        { 
            set { _MoveInputFiles = value; } 
            get { return _MoveInputFiles; }
        }
        
        public static string INTEGRATESERVERIP  
        {
            set { _INTEGRATESERVERIP = value; }
            get { return _INTEGRATESERVERIP; }
        }
       
        
    }
    class XmlOrder  :MessageEventArgs
    {
        #region Private Variabele

        XmlTextWriter textWriter;
        
        private string      _DirectoryPath = "";
        private GoXmlPrcs _ArticlInfo = null;

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
        //private string _RevisePdf = "";
        #endregion
        #region Public Property

        public XmlOrder(GoXmlPrcs ArticlInfo)
        {
             _ArticlInfo = ArticlInfo;
            //_RevisePdf = ArticlInfo.
        }

        public void PreProcess()
        {
            string _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\IntegrationLog";

            WriteLog objWriteLog = new WriteLog(_EXELoc);
            objWriteLog.AppendLog("Pre Process started");
            
            
            try
            {

                objWriteLog.AppendLog("_JID =" + _ArticlInfo.JID);
                objWriteLog.AppendLog("_AID = " + _ArticlInfo.AID);

                
                _Client   = _ArticlInfo.Client;
                _JID      = _ArticlInfo.JID;
                _AID      = _ArticlInfo.AID;
                _FMSStage = _ArticlInfo.FMSStage; 
                _Stage    = _ArticlInfo.Stage;
                _DOI      = _ArticlInfo.DOI;

                if (_ArticlInfo.FigCount != null) 
                    _Figs = _ArticlInfo.FigCount;

                if (!string.IsNullOrEmpty(_ArticlInfo.ArticleCategory))
                     _ArticleCategory = _ArticlInfo.ArticleCategory.Replace("&", "&amp;"); ;

                objWriteLog.AppendLog("_ArticleCategory = " + _ArticleCategory);

                _ArticleType = _ArticleCategory;

                objWriteLog.AppendLog("_ArticleType = " + _ArticleType);

                


                if (string.IsNullOrEmpty(_ArticlInfo.MSS))
                {
                    _ArticlInfo.MSS = "0";
                }

                objWriteLog.AppendLog("ManuscriptPages = " + _ArticlInfo.MSS);
                _MSS = _ArticlInfo.MSS;


                if (!string.IsNullOrEmpty(_ArticlInfo.HistoryDate.AcceptedDate.Date))
                    _AcceptedDate = GetDateTime(_ArticlInfo.HistoryDate.AcceptedDate.Date);

                if (!string.IsNullOrEmpty(_ArticlInfo.HistoryDate.RevisedDate.Date))
                    _RevisedDate = GetDateTime(_ArticlInfo.HistoryDate.RevisedDate.Date);

                if (!string.IsNullOrEmpty(_ArticlInfo.HistoryDate.ReceivedDate.Date))
                    _ReceivedDate = GetDateTime(_ArticlInfo.HistoryDate.ReceivedDate.Date);
                else
                    _ReceivedDate = DateTime.Today;

                try
                {
                    if (!DateTime.TryParse(_ArticlInfo.DueDate, out _InternalDuedate))
                    {
                        _InternalDuedate = DateTime.Today.AddDays(4);
                    }
                }
                catch (Exception ex)
                {
                    objWriteLog.AppendLog("Pre Process internal exception ");
                    objWriteLog.AppendLog(ex);
                }


                objWriteLog.AppendLog("AssignWorkflow = ");

                _WorkFlow = "LWW";

                objWriteLog.AppendLog("GetOrderPath");
                string OrderPath = GetOrderPath();
                _DirectoryPath = OrderPath;

                _OrderFileName = OrderPath + "\\" + _Client + "_" + JID + "_" + _Stage + "_" + AID + "_Order_" + GetNextFileNo() + ".xml";
                
                objWriteLog.AppendLog("_OrderFileName PreProcess : " + _OrderFileName);
            }
            catch (Exception ex)
            {
                objWriteLog.AppendLog("Pre Process exception");
                objWriteLog.AppendLog(ex);
            }
        }
        public DateTime AddDaysNoWeekends(DateTime dt, int Days)
        {
            int DayIcr = 0;
            while (true)
            {
                dt = dt.AddDays(1);
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

   
        private DateTime GetDateTime(string DateStr)
        {
            string[] Month    = "#JAN#FEB#MAR#APR#MAY#JUN#JUL#AUG#SEP#OCT#NOV#DEC".Split('#');
            string[] FulMonth = "#JANUARY#FEBRUARY#MARCH#APRIL#MAY#JUNE#JULY#AUGUST#SEPTEMBER#OCTOBER#NOVEMBER#DECEMBER".Split('#');

            string[] DatePart = DateStr.Split(new char[]{'/','-'});

            int day = Int32.Parse(DatePart[0]);
            int mnth = Array.IndexOf(Month, DatePart[1].ToUpper());

            if (mnth == -1)
                mnth = Array.IndexOf(FulMonth, DatePart[1].ToUpper());

            if (mnth == -1)
                mnth = Int32.Parse(DatePart[1]);

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

        //public string RevisedPdf
        //{
        //    set { _Re = value; }
        //    get { return _SupplementaryMaterial; }
        //}
        #endregion
        #region  Fresh Process
        public  void CreateXMLOrder()
        {
            string _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\IntegrationLog";

            WriteLog objWriteLog = new WriteLog(_EXELoc);
            objWriteLog.AppendLog("Pre Process started");
            
            ProcessEventHandler("CreateXMLOrder Started");
             
            objWriteLog.AppendLog("asdasd");
            string t = ConfigDetails.OPSConnectionString;
            objWriteLog.AppendLog("config details t : " + t);
            PreProcess();
            ProcessEventHandler("PreProcess End");

            XmlReader reader;
            string XMLFIlePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Template\\Template.xml";
            objWriteLog.AppendLog("XMLFIlePath : " + XMLFIlePath);
            objWriteLog.AppendLog("_OrderFileName : " + _OrderFileName);
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

            ProcessEventHandler("XML Loaded");
            
                objWriteLog.AppendLog("Target order path : " + _OrderFileName);
            
            textWriter = new XmlTextWriter(_OrderFileName, null);

            textWriter.Indentation = 4;
            textWriter.IndentChar = '\t';

            textWriter.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\n");
            textWriter.WriteRaw("<!DOCTYPE orders SYSTEM \"FMS-J-Order.dtd\">" + "\n");

            textWriter.WriteRaw("<?xml-stylesheet type=\"text/xsl\" href=\"WileyJ-Order.xsl\"?>" + Environment.NewLine);
            ProcessEventHandler("XML Writed");
            try
            {
                SearchNode(myXmlDocument.DocumentElement);
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }

            textWriter.Flush();
            textWriter.Close();
            textWriter = null;

            StringBuilder Str = new StringBuilder(File.ReadAllText(_OrderFileName));
            Str.Replace("#$#", "&");
            //if (_ArticlInfo.RevisePdf != "")
            //{
            //    Str.Replace("<item-info>", "<item-info><correctionnumber>"+ _ArticlInfo.RevisePdf + "</correctionnumber>");
            //}
            File.WriteAllText(_OrderFileName, Str.ToString());
            ProcessEventHandler("XMLOrder Created succesfully");
            try
            {

                if (JID == "MD" && _ArticlInfo.TaskName == "Revise Article Proofs" && _ArticlInfo.RevisePdf != "")
                {
                    ProcessEventHandler("MD special process called");
                    string exePath = @"E:\JavaProcess\revert_MD_Item_FMS.bat";
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = exePath,
                            Arguments = _ArticlInfo.AID + " " + _ArticlInfo.RevisePdf,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    ProcessEventHandler("MD special process end");
                }
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }

            //MoveToCurrent();

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
                           /*
                        string[] arr = _InternalDuedate.ToShortDateString().Split(new char[]{'/','-'});
                        if (arr.Length > 2)
                        {
                            textWriter.WriteStartElement(node.Name);
                            textWriter.WriteAttributeString("day",   _InternalDuedate.Day.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("month", _InternalDuedate.Month.ToString().PadLeft(2, '0'));
                            textWriter.WriteAttributeString("yr",    _InternalDuedate.Year.ToString());
                        }*/

                        DateTime dt = new DateTime();
                        dt = System.DateTime.Now.AddDays(2);
                        if (dt.DayOfWeek.ToString()  == "Sunday")
                            dt = dt.AddDays(1);
                        else if (dt.DayOfWeek.ToString() == "Saturday")
                            dt = dt.AddDays(2);

                        textWriter.WriteStartElement(node.Name);
                        textWriter.WriteAttributeString("day", dt.Day.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("month", dt.Month.ToString().PadLeft(2, '0'));
                        textWriter.WriteAttributeString("yr", dt.Year.ToString());


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
                case "item-title":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArticlInfo.ArticleTitle.Trim();
                    break;
                case "to-mail":
                    textWriter.WriteStartElement(node.Name);
                    node.InnerXml = _ArticlInfo.CorEmail;
                    break;
                case "fnm":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("corr-author"))
                    {
                         node.InnerXml = _ArticlInfo.CorName;
                    }
                    break;
                case "degree":
                    textWriter.WriteStartElement(node.Name);
                    if (node.ParentNode.Name.Equals("degree"))
                    {
                        node.InnerXml = _ArticlInfo.CorDegree;
                    }
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
            string _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\IntegrationLog";

            WriteLog objWriteLog = new WriteLog(_EXELoc);
            objWriteLog.AppendLog("_DirectoryPath : " + _DirectoryPath);
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
                    string _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\IntegrationLog";

                    WriteLog objWriteLog = new WriteLog(_EXELoc);
                    if (!Directory.Exists(ConfigDetails.RootPath))
                        objWriteLog.AppendLog("Root path not found : " + ConfigDetails.RootPath.ToString());
                        //Directory.CreateDirectory(ConfigDetails.RootPath);
                }
            }

            string OrderPath = ConfigDetails.RootPath.Trim(new char[] { '\\' }) + "\\" + _Client + "\\" + _JID + "\\" + _AID + "\\" + _Stage;
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



       
#endregion



        private void MoveToCurrent()
        {
            string OrderDirectory = Path.GetDirectoryName( _OrderFileName);
            string CurrentOrder = OrderDirectory + "\\CurrentOrder";

            if (Directory.Exists(CurrentOrder))
            { 
                Directory.Delete(CurrentOrder,true);
            }
            Directory.CreateDirectory(CurrentOrder);

            
            System.Threading.Thread.Sleep(1000);
            if (Directory.Exists(CurrentOrder))
            {
                CurrentOrder = CurrentOrder + "\\" + Path.GetFileName(_OrderFileName);
                File.Copy(_OrderFileName, CurrentOrder);
            }

        }
  

   
    }
    
    
}




