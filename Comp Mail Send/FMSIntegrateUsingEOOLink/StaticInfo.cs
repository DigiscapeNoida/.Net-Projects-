using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    static class StaticInfo
    {
        static Hashtable _USTATDays = new Hashtable();
        static WriteLog _WriteLog = null;
        static StaticInfo()
        {
            AppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _WriteLog = new WriteLog(StaticInfo.AppPath);

            PASSWORD         = ConfigurationSettings.AppSettings["PASSWORD"];
            MAILSERVERDOMAIN = ConfigurationSettings.AppSettings["MAILSERVERDOMAIN"];
            NSFFILE          = ConfigurationSettings.AppSettings["NSFFILE"];
            CHECKMAILID      = ConfigurationSettings.AppSettings["CHECKMAILID"];
            Config           = ConfigurationSettings.AppSettings["Config"];
            AepsJwConfig     = ConfigurationSettings.AppSettings["AepsJwConfig"];
            OrderPath        = ConfigurationSettings.AppSettings["OrderPath"];
            FmsPath          = ConfigurationSettings.AppSettings["FmsPath"];
            ZipFilePath      = ConfigurationSettings.AppSettings["ZipFilePath"];
            MaterialPath     = ConfigurationSettings.AppSettings["MaterialPath"];
            ErrorLog         = ConfigurationSettings.AppSettings["ErrorLog"];
            CheckSubject     = ConfigurationSettings.AppSettings["CheckSubject"];
            OPSConfig =  ConfigurationSettings.AppSettings["OPSConfig"];
            
            _USTATDays.Add("NAME", "USA");
            _USTATDays = FillTATDays(USTATDays);

            Hashtable UKTATDays = new Hashtable();
            UKTATDays.Add("NAME", "UK");
            UKTATDays = FillTATDays(UKTATDays);

            Hashtable VCHTATDays = new Hashtable();
            VCHTATDays.Add("NAME", "VCH");
            VCHTATDays = FillTATDays(VCHTATDays);
         

            Hashtable SingaTATDays = new Hashtable();
            SingaTATDays.Add("NAME", "SINGAPORE");
            SingaTATDays = FillTATDays(SingaTATDays);


            

        }
        public static Hashtable USTATDays
        {
            get { return _USTATDays; }
        }
        public static WriteLog WriteLogMsg
        {
         get{return _WriteLog;}   
        }

        public static string AppPath
        { get; set; }

        public static string PASSWORD
        { get; set; }
        public static string MAILSERVERDOMAIN
        { get; set; }
        public static string NSFFILE
        { get; set; }
        public static string CHECKMAILID
        { get; set; }
        public static string Config
        { get; set; }
        public static string OPSConfig
        { get; set; }
        public static string AepsJwConfig
        { get; set; }
        public static string OrderPath
        { get; set; }
        public static string FmsPath
        { get; set; }
        public static string ZipFilePath
        { get; set; }
        public static string MaterialPath
        { get; set; }
        public static string ErrorLog
        { get; set; }
        public static string CheckSubject
        { get; set; }
        public static Hashtable FillTATDays(Hashtable TAT)
        {
            if (TAT.ContainsValue("UK"))
            {
                TAT.Add("ACP", "8");
                TAT.Add("ARP", "4");
                TAT.Add("ATR", "8");
                TAT.Add("BDM", "8");
                TAT.Add("BIN", "8");
                TAT.Add("BREF", "4");
                TAT.Add("BSL", "4");
                TAT.Add("CASP", "8");
                TAT.Add("CAV", "8");
                TAT.Add("CB", "8");
                TAT.Add("CB13", "8");
                TAT.Add("CBF", "8");
                TAT.Add("CBIN", "8");//added by ajay bhardwaj 26 sept 2012
                TAT.Add("IID3", "8");//added by ajay bhardwaj 26 sept 2012
                TAT.Add("CEM", "8");
                TAT.Add("CMMI", "4");
                TAT.Add("EJSP", "8");
                TAT.Add("ENV", "8");
                TAT.Add("ERV", "8");
                TAT.Add("ETEP", "8");
                TAT.Add("ETT", "8");
                TAT.Add("GJ", "8");
                TAT.Add("GPS", "8");
                TAT.Add("HON", "8");
                TAT.Add("HPM", "8");
                TAT.Add("HUP", "8");
                TAT.Add("IIR", "8");
                TAT.Add("IRD", "4");
                TAT.Add("JID", "8");
                TAT.Add("JMR", "8");
                TAT.Add("JOB", "8");
                TAT.Add("JQS", "4");
                TAT.Add("KPM", "8");
                TAT.Add("LDR", "8");
                TAT.Add("NBM", "4");
                TAT.Add("NVSM", "8");
                TAT.Add("OA", "8");
                TAT.Add("PA", "8");
                TAT.Add("PAD", "8");
                TAT.Add("PAT", "8");
                TAT.Add("PDS", "8");
                TAT.Add("PER", "8");
                TAT.Add("PIP", "8");
                TAT.Add("POC", "8");
                TAT.Add("PPP", "4");
                TAT.Add("RBF", "8");
                TAT.Add("RCM", "4");
                TAT.Add("RMV", "8");
                TAT.Add("RRA", "8");
                TAT.Add("SEC", "8");
                TAT.Add("SRES", "8");
                TAT.Add("WCM", "8");
                // TAT.Add("JCB", "8");

            }
            else if (TAT.ContainsValue("USA"))
            {

                TAT.Add("AB", "8");
                TAT.Add("AFDR", "8");
                TAT.Add("AJAD", "8");
                TAT.Add("AJIM", "8");
                TAT.Add("AJMGA", "8");
                TAT.Add("AJMGB", "8");
                TAT.Add("AJMGC", "8");
                TAT.Add("AJP", "8");
                TAT.Add("AJT", "8");
                TAT.Add("BEM", "8");
                TAT.Add("BIOM", "8");
                TAT.Add("BIT", "8");
                TAT.Add("CAE", "8");
                TAT.Add("CJCE", "8");
                TAT.Add("CJS", "8");
                TAT.Add("CPDD", "8");
                TAT.Add("DAT", "4");
                TAT.Add("DEV", "8");
                TAT.Add("ECNO", "8");
                TAT.Add("ETC", "4");
                TAT.Add("EUFM", "8");
                TAT.Add("FLAN", "4");
                //TAT.Add("FLAN", "8");
                TAT.Add("FUT", "8");
                TAT.Add("IEAM", "4");
                TAT.Add("JABA", "8");
                TAT.Add("JBMR", "4");
                TAT.Add("JCB", "8");
                TAT.Add("JCP", "8");
                TAT.Add("JCPH", "8");
                TAT.Add("JEAB", "8");
                TAT.Add("JEZ", "8");
                TAT.Add("JEZB", "8");
                TAT.Add("JMV", "8");
                TAT.Add("JOR", "8");
                TAT.Add("JPS", "8");
                TAT.Add("JSCH", "8");
                TAT.Add("JSO", "8");
                TAT.Add("JWMG", "8");
                TAT.Add("LSM", "8");
                TAT.Add("MAS", "8");
                TAT.Add("MC", "8");
                TAT.Add("MODL", "4");
                //TAT.Add("MODL", "8");
                TAT.Add("MONO", "8");
                TAT.Add("MRD", "8");
                TAT.Add("NAU", "8");
                TAT.Add("NUR", "8");
                TAT.Add("PBC", "8");
                TAT.Add("POI3", "8");
                TAT.Add("POLQ", "4");
                TAT.Add("POP4", "8");
                TAT.Add("PPUL", "8");
                TAT.Add("PROS", "8");
                TAT.Add("RHC3", "8");
                TAT.Add("SCA", "8");
                TAT.Add("TEA", "8");
                TAT.Add("VSU", "8");
                TAT.Add("WMH3", "8");
                TAT.Add("WMON", "8");
                TAT.Add("WSB", "8");
                TAT.Add("ZOO", "8");
            }
            else if (TAT.ContainsValue("VCH"))
            {
                //TAT.Add("PROTEOMICS", "4");
                //TAT.Add("ELPHO", "4");
                TAT.Add("ADBI", "4");
                TAT.Add("ADFM", "4");
                TAT.Add("AEM", "8");
                TAT.Add("ADEM", "8");
                TAT.Add("AFM", "4");
                TAT.Add("AM", "4");
                TAT.Add("ARDP", "8");
                TAT.Add("BIES", "8");
                TAT.Add("BTJ", "4");
                TAT.Add("CLEAN", "8");
                TAT.Add("CLEN", "8");
                TAT.Add("CVDE", "4");
                TAT.Add("EJLT", "8");
                TAT.Add("EMMM", "8");
                TAT.Add("EMBO", "8");
                TAT.Add("FUCE", "4");
                TAT.Add("IROH", "8");
                TAT.Add("JOBM", "8");
                TAT.Add("JSS", "4");
                TAT.Add("MACO", "8");
                TAT.Add("MASY", "4");
                TAT.Add("MBS", "4");
                TAT.Add("MABI", "4");
                TAT.Add("MCP", "4");
                TAT.Add("MME", "4");
                TAT.Add("MAME", "4");
                TAT.Add("MNF", "4");
                TAT.Add("MRC", "4");
                TAT.Add("MRE", "4");
                TAT.Add("MREN", "4");
                TAT.Add("MTS", "4");
                TAT.Add("MATS", "4");
                TAT.Add("MVN", "4");
                TAT.Add("PEP", "4");
                TAT.Add("PPAP", "8");
                TAT.Add("PSSA", "4");
                TAT.Add("PSSB", "4");
                TAT.Add("QCS", "4");
                TAT.Add("SMALL", "4");
                TAT.Add("SRIN", "8");
                TAT.Add("STAR", "8");
                TAT.Add("Steel", "4");
            }
            else if (TAT.ContainsValue("SINGAPORE"))
            {
                TAT.Add("AERE", "8");//Changes Done By Ajay Add new journals[26/9/2012)
                TAT.Add("AFDR", "4");
                TAT.Add("CAG", "4");
                TAT.Add("ECNO", "4");
                TAT.Add("EDE", "8");
                TAT.Add("EUFM", "4");
                TAT.Add("INFI", "8");
                TAT.Add("JFIR", "4");
                TAT.Add("JIPB", "4");
                TAT.Add("JOCS", "8");
                TAT.Add("JOIC", "8");
                TAT.Add("JORI", "8");
                TAT.Add("JWIP", "8");
                TAT.Add("MIM", "4");
                TAT.Add("PBAF", "8");
                TAT.Add("RURD", "4");
                TAT.Add("JORC", "8");
                TAT.Add("JSE", "4");
                TAT.Add("IJET", "4");
            }
            return TAT;
        }


    }
    public static class HumanFriendlyInteger
    {
        static string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }
            string friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negative " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }
    }
}