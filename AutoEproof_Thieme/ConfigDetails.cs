using System;
using System.Net.NetworkInformation;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;

namespace AutoEproof
{
    class ConfigDetails
    {

        static string _S200InPut = string.Empty;
        static string _S275InPut = string.Empty;
        static string _S280InPut = string.Empty;

        static string _PaginationOUT = string.Empty;
        static string _GrOut     = string.Empty;
        static string _EPOut     = string.Empty;
        static string _EVOut     = string.Empty;
        static string _IssCrx    = string.Empty;
        static string _Issue     = string.Empty;
        static string _GangIP = string.Empty;
        
        static string _ToAuthor = string.Empty;
        static string _S100RESUPPLY = string.Empty;
        static string _HeavyPDF     = string.Empty;
        static int   _HeavyPdfSize;
        static string _ServerPath        = string.Empty;
        static string _GangServerPath     = string.Empty;
        static string _TDXPSNoidaFMSFolder = string.Empty;
        static string _TDXPSNoidaTempFolder = string.Empty;
        static string _TDXPSGangtokFMSFolder = string.Empty;
        static string _TDXPSGangtokTempFolder = string.Empty;
        static string _TDXPSGangtokIP = string.Empty;
        static string _TDXPSGangtokAlrntvIP = string.Empty;
        static string _TDXPSNoidaIP = string.Empty;
        static string _EXELoc = string.Empty;
        static string _TemplatePath = string.Empty;

        static string _OPSServerPath    = string.Empty;
        static string _OPSServerIP      = string.Empty;
        static string _OPSPDFLoc        = string.Empty;
        static string _OPSXMLLoc        = string.Empty;
        static string _AltrntvOPSXMLLoc = string.Empty;

        static string _SearchFolder = string.Empty;
        static string _InputFilesForEditor = string.Empty;
        static string _FinalQC = string.Empty;
        
        static bool _OPSTest = false;
        
        static string _TemplatePathThieme = string.Empty;
        static string _HowToAnnotateYourProof = string.Empty;

        static Dictionary<string, TempClass> JIDDetail = new Dictionary<string, TempClass>();

        private ConfigDetails()
        {

        }
        static ConfigDetails()
        {
            bool SwitchIP = false;
            string G1 = ConfigurationManager.AppSettings["TDXPSGangtokIP"];
            string G2 = ConfigurationManager.AppSettings["TDXPSGangtokAlrntvIP"];
            if (isPing(G1) == false)
            {
                if (isPing(G2))
                {
                    SwitchIP = true;
                }
            }

            string[] KEYS = ConfigurationManager.AppSettings.AllKeys;
            foreach (string key in KEYS)
            {
                string KeyVal = ConfigurationManager.AppSettings[key];
                if (SwitchIP)
                {
                    KeyVal = KeyVal.Replace(G1, G2);
                }
                switch (key)
                {

                   case "HeavyPdfSize":
                        {
                            string TmpStr = KeyVal;
                            if (string.IsNullOrEmpty(TmpStr))
                            {
                                int size;
                                if(Int32.TryParse(TmpStr, out size))
                                {
                                    _HeavyPdfSize= size;
                                }
                            }
                            break;
                        }
                   case "InputFilesForEditor":
                        {
                            _InputFilesForEditor = KeyVal;
                            break;
                        }
                    case "HeavyPdf":
                        {
                            _HeavyPDF = KeyVal;
                            break;
                        }
                    case "S100RESUPPLY":
                        {
                            _S100RESUPPLY = KeyVal;
                            break;
                        }
                    case "ToAuthor":
                        {
                           _ToAuthor=KeyVal;
                            break;
                        }
                        
                    case "Issue":
                        {
                            _Issue = KeyVal;
                            break;
                        }
                    case "S200InPut":
                        {
                            _S200InPut = KeyVal;
                            break;
                        }
                    case "S275InPut":
                        {
                            _S275InPut = KeyVal;
                            break;
                        }
                    case "S280InPut":
                        {
                            _S280InPut = KeyVal;
                            break;
                        }
                    case "IssCrx":
                        {
                            _IssCrx = KeyVal;
                            break;
                        }
                    case "EPOut":
                        {
                            _EPOut = KeyVal;
                            break;
                        }
                    case "EVOut":
                        {
                            _EVOut = KeyVal;
                            break;
                        }
                    case "GrOut":
                        {
                            _GrOut = KeyVal;
                            break;
                        }
                    case "TDXPSNoidaFMSFolder":
                        {
                            _TDXPSNoidaFMSFolder = KeyVal;
                            break;
                        }
                    case "TDXPSNoidaTempFolder":
                        {
                            _TDXPSNoidaTempFolder = KeyVal;
                            break;
                        }
                    case "TDXPSGangtokFMSFolder":
                        {
                            _TDXPSGangtokFMSFolder = KeyVal;
                            break;
                        }
                    case "TDXPSGangtokTempFolder":
                        {
                            _TDXPSGangtokTempFolder = KeyVal;
                            break;
                        }
                    case "OPSTEST":
                        {
                            if (KeyVal.Equals("True", StringComparison.OrdinalIgnoreCase))
                                _OPSTest = true;
                            break;
                        }
                    case "TemplatePath":
                        {
                            _TemplatePath = KeyVal;
                            break;
                        }
                    case "OPSServerPath":
                        {
                            _OPSServerPath = KeyVal;
                            break;
                        }
                    case "OPSServerIP":
                        {
                            _OPSServerIP = KeyVal;
                            break;
                        }
                    case "OPSPDFLoc":
                        {
                            _OPSPDFLoc = KeyVal;
                            break;
                        }
                    case "OPSXMLLoc":
                        {
                            _OPSXMLLoc = KeyVal;
                            break;
                        }
                    case "AltrntvOPSXMLLoc":
                        {
                            _AltrntvOPSXMLLoc = KeyVal;
                            break;
                        }
                    case "SearchFolder":
                        {
                            _SearchFolder = KeyVal;
                            break;
                        }
                    case "RevisedSearchFolder":
                        {
                            _SearchFolder = _SearchFolder + "#" + KeyVal;
                            break;
                        }
                    case "FAXSearchFolder":
                        {
                            _SearchFolder = _SearchFolder + "#" + KeyVal;
                            break;
                        }
                    case "HowToAnnotateYourProofTemplatePath":
                        {
                            _HowToAnnotateYourProof = KeyVal;
                            break;
                        }
                    case "TemplatePathThieme":
                        {
                            _TemplatePathThieme = KeyVal;
                            break;
                        }
                    case "TDXPSNoidaIP":
                        {
                            _TDXPSNoidaIP = KeyVal;
                            break;
                        }
                    case "TDXPSGangtokIP":
                        {
                            _TDXPSGangtokIP = KeyVal;
                            break;
                        }
                    case "TDXPSGangtokAlrntvIP":
                        {
                            _TDXPSGangtokAlrntvIP = KeyVal;
                            break;
                        }
                        
                    case "ServerPath":
                        {
                            _ServerPath = KeyVal;
                            break;
                        }
                        case "GangServerPath":
                        {
                            _GangServerPath = KeyVal;
                            break;
                        }
                        case "GangIP":
                        {
                            _GangIP = KeyVal;
                            break;
                        }
                        case "PaginationOUT":
                        {
                            _PaginationOUT = KeyVal;
                            break;
                        }
                        case "FinalQC":
                        {
                            _FinalQC = KeyVal;
                            break;
                        }
                        
                }
            }


            _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string Fpath = _EXELoc + "\\DailyWork.txt";

            if (File.Exists(Fpath))
            {
                string[] Lines = File.ReadAllLines(Fpath);
                foreach (string Line in Lines)
                {
                    if (!string.IsNullOrEmpty(Line))
                    {
                        TempClass OBJ = new TempClass();
                        string[] Arr = Line.Split('\t');
                        OBJ.JID = Arr[0];
                        OBJ.AuthorName = Arr[1];
                        OBJ.AuthorMail = Arr[2];
                        OBJ.Jtitle = Arr[3];

                        if (!JIDDetail.ContainsKey(Arr[0]))
                            JIDDetail.Add(Arr[0], OBJ);
                    }
                }
            }
            //GetMailFromExcel();
        }


        public static bool isPing(string ServerIP)
        {

            ServerIP = ServerIP.Replace("activemq", "").Replace("tcp", "").Replace("61616", "").Replace(":", "").Replace("/", "");

            bool Result = false;

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            Console.WriteLine("Send ping command to :: " + ServerIP);
            PingReply reply = pingSender.Send(ServerIP, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Result = true;
                Console.WriteLine(ServerIP + " is pinging....");
            }

            return Result;
        }
        public static string AltrntvOPSXMLLoc { get { return _AltrntvOPSXMLLoc; } }
        public static string OPSXMLLoc { get { return _OPSXMLLoc; } }
        public static string OPSPDFLoc { get { return _OPSPDFLoc; } }

        public static string ServerPath { get { return _ServerPath; } }
        public static string GangServerPath { get { return _GangServerPath; } }

        public static string TDXPSNoidaFMSFolder    { get { return _TDXPSNoidaFMSFolder;    } }
        public static string TDXPSNoidaTempFolder   { get { return _TDXPSNoidaTempFolder;   } }
        public static string TDXPSGangtokFMSFolder  { get { return _TDXPSGangtokFMSFolder;  } }
        public static string TDXPSGangtokTempFolder { get { return _TDXPSGangtokTempFolder; } }

        public static string TDXPSNoidaIP { get { return _TDXPSNoidaIP; } }
        public static string TDXPSGangtokIP { get { return _TDXPSGangtokIP; } }

        public static string FinalQC { get { return _FinalQC; } }

        public static string GangIP { get { return _GangIP; } }

        public static string S200InPut { get { return _S200InPut; } }
        public static string S275InPut { get { return _S275InPut; } }
        public static string S280InPut { get { return _S280InPut; } }

        public static string Issue     { get { return _Issue; } }
        public static string ToAuthor  { get { return _ToAuthor; } }
        public static string S100RESUPPLY { get { return _S100RESUPPLY; } }
        public static string HeavyPDF { get { return _HeavyPDF; } }
        
        public static string IssCrx    { get { return _IssCrx; } }
        public static string EPOut     { get { return _EPOut; } }
        public static string EVOut { get { return _EVOut; } }
        public static string GrOut { get { return _GrOut; } }
        public static string PaginationOUT { get { return _PaginationOUT; } }
        public static bool isAuthorDetailsFromExcel(string JIDAID)
        {

            if (JIDDetail.ContainsKey(JIDAID))
                return true;
            else
                return false;
        }
        public static bool OPSTest { get { return _OPSTest; } }

        public static string HowToAnnotateYourProof { get { return _HowToAnnotateYourProof; } }
        public static string TemplatePath { get { return _TemplatePath; } }
        public static string OPSServerPath { get { return _OPSServerPath; } }
        public static string OPSServerIP { get { return _OPSServerIP; } }
        public static string SearchFolder { get { return _SearchFolder; } }
        public static string TemplatePathThieme { get { return _TemplatePathThieme; } }
        public static string EXELoc { get { return _EXELoc; } }
        public static string GetAuthor(string JID)
        {
            if (JIDDetail.ContainsKey(JID))
                return JIDDetail[JID].AuthorName;
            else
                return "";
        }
        public static string GetAuthorMail(string JID)
        {
            if (JIDDetail.ContainsKey(JID))
                return JIDDetail[JID.ToUpper()].AuthorMail;
            else
                return "";
        }
        public static string GetTitle(string JID)
        {
            if (JIDDetail.ContainsKey(JID))
                return JIDDetail[JID].Jtitle;
            else
                return "";
        }
        public static void GetMailFromExcel()
        {
            try
            {
                string ExclPath = @"C:\AEPS\OPSMailTO.xlsx";
                if (!File.Exists(ExclPath))
                {
                    return;
                }

                ExcelDetails ExlDtl = new ExcelDetails(ExclPath);
                string eMailDtl = ExlDtl.GetDataAsXML();


                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(eMailDtl);

                XmlNodeList NL = xDoc.GetElementsByTagName("EmailDetail");

                foreach (XmlNode chNode in NL)
                {
                    TempClass OBJ = new TempClass();
                    foreach (XmlNode Node in chNode)
                    {
                        if (Node.Name.Equals("JIDAID"))
                        {
                            OBJ.JID = Node.InnerText;
                        }
                        else if (Node.Name.Equals("MailID"))
                        {
                            OBJ.AuthorMail = Node.InnerText; ;
                        }
                        else if (Node.Name.Equals("Name"))
                        {
                            OBJ.AuthorName = Node.InnerText; ;
                        }
                        else if (Node.Name.Equals("Title"))
                        {
                            OBJ.Jtitle = Node.InnerText; ;
                        }

                    }
                    //OBJ.Jtitle = "";
                    if (OBJ != null && OBJ.JID != null)
                    {
                        JIDDetail.Add(OBJ.JID, OBJ);
                    }
                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.Source);
                //MessageBox.Show(ex.InnerException.ToString());
                //MessageBox.Show(ex.StackTrace);
            }


        }

        public static string InputFilesForEditor { get { return _InputFilesForEditor; } }
        
    }

    class TempClass
    {
        public string JID
        {
            get;
            set;
        }
        public string AuthorName
        {
            get;
            set;
        }
        public string AuthorMail
        {
            get;
            set;
        }
        public string Jtitle
        {
            get;
            set;
        }
    }
}
