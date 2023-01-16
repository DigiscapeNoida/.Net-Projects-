using MsgRcvr;
using System.Xml;
using System.Configuration;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Text;
using DatabaseLayer;
using PDFAnnotation;
using System.Net.NetworkInformation;
using ICSharpCode.SharpZipLib.Zip;
using System.Data;
using iTextSharp.text.pdf;
namespace AutoEproof
{
    public class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9;
        static WriteLog _WriteLogObj = null;
        public static void Main(string[] args)
        {
            while (true)
            {
                //TestForEproof();
                // CreateURL(@"c:\fms\MNT_JWUK_JOURNAL_JQS_2934_110\2934.pdf");
                // return;
                //string ArticleCategory = string.Empty;
                //  ArticleCategory= GetArticleCategory().Trim();

                //SendMsgToGang("SINGAPORE", "MIM", "12467", "QCSUBMIT");
                //SendMsgToGangP100AutoPdf("JWUK", "JQS", "I31.5", "0");
                // return;
                //SendMsgToGang("JWVCH", "EJLT", "I118.5", "0");
                ////////////////Copy Gangtok files and process .........

                IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                ShowWindow(winHandle, SW_MINIMIZE);
                string AutoEproofTxt = "C:\\AutoEproof.txt";
                string ExeName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (args.Length > 0 && args[0].Equals("RvsIssFileCopy"))
                {
                    ////////////////Copy Gangtok files and process .........
                    CopyRvsIssFiles();
                    return;
                }
                else if (args.Length > 0 && args[0].Equals("CleanProof", StringComparison.OrdinalIgnoreCase))
                {
                    ////////////////Copy clean proof from gangtok files and process .........
                    CopyCleanProofFiles();
                    return;
                }
                else if (args.Length > 0 && args[0].Equals("EPOut"))
                {
                    _WriteLogObj = new WriteLog(ConfigDetails.EXELoc + "\\EPOut");
                    if (File.Exists(AutoEproofTxt))
                    {
                        FileInfo FI = new FileInfo(AutoEproofTxt);
                        TimeSpan TimeDiff = DateTime.Now.TimeOfDay;
                        TimeDiff = DateTime.Now - FI.LastWriteTime;
                        if (TimeDiff.Hours == 0 && TimeDiff.Minutes > 30)
                        {
                            KillProcess("AdobeCollabSync");
                            KillProcess("Acrobat");
                            KillProcess("PDFAnnotationEnable");
                            KillProcess("AutoEproof");
                        }
                    }
                    EPOutProcess();
                    /////////////////Update  user of finished article
                    ArticleUserUpdate UserUpdateObj = new ArticleUserUpdate();
                    UserUpdateObj.StartToUpdateArticleUser();
                    return;
                }
                else
                {
                    Console.WriteLine("No argument available.");
                }

                Console.Title = "Auto Eproof";

                if (System.Diagnostics.Process.GetProcessesByName(ExeName).Length > 1)
                {
                    return;
                }

                string d = ConfigDetails.OPSServerIP;
                File.WriteAllText(AutoEproofTxt, DateTime.Now.ToShortTimeString());

                _WriteLogObj = new WriteLog(ConfigDetails.EXELoc);

                ProcessMsg ProcessMsgOBJ = new ProcessMsg();
                ProcessMsgOBJ.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                ProcessMsgOBJ.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                try
                {
                    /////////To make pdf annotable for the other application
                    MakingAnnotation();
                    MakingAnnotationforJLE();
                    //////////Insert Message to send proof if S100 Article is author revision and auto allocation for S200 
                    S100_S200_AUTHOR_REVISION(ProcessMsgOBJ);
                }
                catch (Exception ex)
                {
                    ProcessMsgOBJ_ErrorNotification(ex);
                }
                try
                {
                    string EproofValidationResult = EproofApplicationValidation();
                    if (EproofValidationResult.Length > 0)
                    {
                        _WriteLogObj.AppendLog(EproofValidationResult);
                        _WriteLogObj.WriteLogFileInDate();
                        eProoUnaccessible(EproofValidationResult);

                    }

                    string ValidationResult = ApplicationValidation();

                    if (ValidationResult.Length > 0)
                    {
                        _WriteLogObj.AppendLog(ValidationResult);
                        _WriteLogObj.WriteLogFileInDate();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ProcessMsgOBJ_ErrorNotification(ex);
                }

                try
                {
                    if (ProcessMsgOBJ.GetMsgList())
                    {
                        if (ProcessMsgOBJ.MsgList.Count > 0)
                        {
                            ProcessMsgOBJ.ProcessArticles();
                            _WriteLogObj.WriteLogFileInDate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessMsgOBJ_ErrorNotification(ex);
                }
                File.Delete(AutoEproofTxt);
                System.Threading.Thread.Sleep(10000);
            }
        }

         private static void MakingAnnotationforJLE()             
        {
            string SrchPath = @"C:\AnnPDFJLE\";
            string OutPath = @"C:\AnnPDFJLE\out\";

            if (!Directory.Exists(SrchPath))
                Directory.CreateDirectory(SrchPath);

            if (!Directory.Exists(OutPath))
                Directory.CreateDirectory(OutPath);

            try
            {
                /////////Do'nt change sequence of below three lines. It will effect the entire process.
                //string[] args = new string[1];
                //args[0] = SrchPath;
                string[] Pdfs = Directory.GetFiles(SrchPath, "*.pdf");


                if (Pdfs.Length > 0)
                    PDFAnnotation.Program.Main(Pdfs);


                foreach (string filePDF in Pdfs)
                {
                    if (File.Exists(filePDF))
                    {
                        string OutPutPdf = OutPath + "\\temp_" + Path.GetFileName(filePDF);

                        if (File.Exists(OutPutPdf))
                        {
                            File.Delete(OutPutPdf);
                        }

                        File.Copy(filePDF, OutPutPdf);

                        File.Delete(filePDF);
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                _WriteLogObj.AppendLog(ex);
            }
        }


        private static void RctRect(string XMLFile)
        {
            //@lang

            StringBuilder XMLStr = new StringBuilder(File.ReadAllText(XMLFile));
            XMLStr.Replace("&", "#$#");
            XmlDocument xDoc = new XmlDocument();
            xDoc.PreserveWhitespace = true;
            xDoc.LoadXml(XMLStr.ToString());



            XmlNodeList NL = xDoc.SelectNodes("//@rect");

            foreach (XmlNode RctNode in NL)
            {
                int Count = 0;
                char[] Chrs = RctNode.Value.ToCharArray();
                for (int i = 0; i < Chrs.Length; i++)
                {
                    if (Chrs[i].Equals(',') && Count == 0)
                    {
                        Chrs[i] = '.';
                        Count++;
                    }
                    else if (Chrs[i].Equals(',') && Count == 1)
                    {
                        Count--;
                    }
                    else if (Chrs[i].Equals('.') && Count == 0)
                    {
                        Count++;
                    }
                }
                RctNode.Value = new string(Chrs); ;

            }
            NL = xDoc.SelectNodes("//@coords");
            foreach (XmlNode RctNode in NL)
            {
                int Count = 0;
                char[] Chrs = RctNode.Value.ToCharArray();
                for (int i = 0; i < Chrs.Length; i++)
                {
                    if (Chrs[i].Equals(',') && Count == 0)
                    {
                        Chrs[i] = '.';
                        Count++;
                    }
                    else if (Chrs[i].Equals(',') && Count == 1)
                    {
                        Count--;
                    }
                    else if (Chrs[i].Equals('.') && Count == 0)
                    {
                        Count++;
                    }
                }
                RctNode.Value = new string(Chrs); ;

            }

            File.WriteAllText(XMLFile, xDoc.DocumentElement.OuterXml.Replace("#$#", "&"));

        }
        private static void StripnsProcess()
        {
            ProcessMsgOBJ_ProcessNotification("StripnsProcess Start");
            StripnsMsg StripnsObj = new StripnsMsg();
            StripnsObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
            StripnsObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
            StripnsObj.StartProces();
            ProcessMsgOBJ_ProcessNotification("StripnsProcess Finished");
        }



        private static void S100_S200_AUTHOR_REVISION(ProcessMsg ProcessMsgOBJ)
        {
            if (DateTime.Now.Hour > 19) //////////To Check AUTHOR_REVISION status article after 7 in Evening
            {
                WljPlanner WljPlannerObj = new WljPlanner();
                List<WIPArticleInfo> WIPArticleList = WljPlannerObj.GetArticleList();
                WIPArticleList.Sort();
                List<WIPArticleInfo> ArticleInEP = WIPArticleList.FindAll(x => x.PSEStage.Equals("AUTHOR_REVISION"));
                foreach (WIPArticleInfo WIPAI in ArticleInEP)
                {
                    if ("S100".IndexOf(WIPAI.ClientStage) != -1)
                    {
                        ProcessMsgOBJ.InsertMessageDetail(WIPAI);
                        if (WIPAI.IP.Equals(ConfigDetails.TDXPSGangtokIP))
                        {
                            CopyFromGangtokToNoida(WIPAI.JID, WIPAI.AID);
                        }
                    }
                }
            }
            else
            {
                WljPlanner WljPlannerObj = new WljPlanner();
                List<WIPArticleInfo> WIPArticleList = WljPlannerObj.GetArticleList();
                WIPArticleList.Sort();
                List<WIPArticleInfo> ArticleInEP = WIPArticleList.FindAll(x => x.ClientStage.Equals("S200") && x.PSELoginID.StartsWith("wljgenericel"));
                foreach (WIPArticleInfo WIPAI in ArticleInEP)
                {
                    WILEYS200AUTOALLOCATION(WIPAI);
                }
            }
        }
        static private void KillProcess(string ExeName)
        {

            Console.WriteLine("Going to kill instance of " + ExeName);

            Process[] PR = Process.GetProcessesByName(ExeName);
            if (PR.Length > 0)
            {
                while (PR.Length > 0)
                {
                    PR[0].Kill();
                    Console.WriteLine("Instance of " + ExeName + " closed successfully");
                }
            }
            else
            {
                Console.WriteLine("No open instance found for :: " + ExeName);
            }
        }
        private static void NewMethod()
        {
            string[] Zips = Directory.GetFiles("D:\\1p1p", "*.zip");

            foreach (string Zip in Zips)
            {
                string[] Arr = Path.GetFileNameWithoutExtension(Zip).Split('_');
                string PdfPath = UnzipFile(Zip);
                if (!string.IsNullOrEmpty(PdfPath))
                {
                    int ACount = PdfProcess.PDF.GetCount(PdfPath);
                    UpdateAnnotationCount(Arr[0], Arr[1], Arr[3], Arr[2], ACount);
                }
            }

            return;
        }


        static bool WILEYS200AUTOALLOCATION(WIPArticleInfo WIPAI)
        {
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@CLNT", WIPAI.Client);
            para[1] = new SqlParameter("@JID", WIPAI.JID);
            para[2] = new SqlParameter("@AID", WIPAI.AID);
            para[3] = new SqlParameter("@STAG", WIPAI.ClientStage);
            try
            {
                int Rslt = SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_WILEYS200AUTOALLOCATION", para);
            }
            catch (SqlException ex)
            {
                ProcessMsgOBJ_ErrorNotification(ex);
                return false;
            }


            return true;
        }
        static bool UpdateAnnotationCount(string Client, string JID, string AID, string ClientStage, int Acount)
        {
            if (ClientStage.Equals("revise"))
                ClientStage = "S200";
            else if (ClientStage.StartsWith("fax"))
                ClientStage = "S275";

            string OPSConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[5];

            para[0] = new SqlParameter("@CLNT", Client);
            para[1] = new SqlParameter("@JID", JID);
            para[2] = new SqlParameter("@AID", AID);
            para[3] = new SqlParameter("@Stage", ClientStage);
            para[4] = new SqlParameter("@ACount", Acount.ToString());
            try
            {
                int Rslt = SqlHelper.ExecuteNonQuery(OPSConnectionString, System.Data.CommandType.StoredProcedure, "[usp_UpdateAnnotationCount]", para);
            }
            catch (SqlException ex)
            {
                ProcessMsgOBJ_ErrorNotification(ex);
                return false;
            }


            return true;
        }

        static void CopyCleanProofFiles()
        {
            string ExeName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _WriteLogObj = new WriteLog(ConfigDetails.EXELoc + "\\CopyCleanProof");

            string GangIP = ConfigDetails.TDXPSGangtokIP;
            if (string.IsNullOrEmpty(GangIP) || ConfigDetails.isPing(GangIP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(GangIP + " IP is not accessible..");
                return;
            }

            ////////////Copy revise and issue files
            CleanProof CleanProofObj = new CleanProof();
            CleanProofObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
            CleanProofObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
            if (CleanProofObj.GetMsgList())
            {
                CleanProofObj.ProcessArticles();
            }

            string AutoIssuePDF = @"D:\tdxps\IssuePag\AUTO";
            string IssuePS2PDF = @"D:\tdxps\IssuePag\PS2PDF";
            if (Directory.Exists(AutoIssuePDF))
            {
                string[] txtFiles = Directory.GetFiles(AutoIssuePDF, "*.txt");
                foreach (string txt in txtFiles)
                {
                    ProcessMsgOBJ_ProcessNotification(txt);

                    MNTInfo MNT = new MNTInfo(Path.GetFileNameWithoutExtension(txt));
                    if (MNT.MNTFolder.EndsWith("P100"))
                    {
                        SendMsgToGangP100AutoPdf(MNT.Client, MNT.JID, MNT.AID, "0");
                        ProcessMsgOBJ_ProcessNotification("Auto pdf message has been sent for " + MNT.MNTFolder);
                    }


                    File.Delete(txt);
                }

            }
            if (Directory.Exists(IssuePS2PDF))
            {
                string[] txtFiles = Directory.GetFiles(IssuePS2PDF, "*.txt");
                foreach (string txt in txtFiles)
                {
                    ProcessMsgOBJ_ProcessNotification(txt);

                    MNTInfo MNT = new MNTInfo(Path.GetFileNameWithoutExtension(txt));
                    if (MNT.MNTFolder.EndsWith("P100"))
                    {
                        SendMsgToGangP100PS2PDF(MNT.Client, MNT.JID, MNT.AID, "0");
                        ProcessMsgOBJ_ProcessNotification("PS to pdf message has been sent for " + MNT.MNTFolder);
                    }
                    File.Delete(txt);
                }

            }
            try
            {
                IssueMail IssueMailObj = new IssueMail();
                IssueMailObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                IssueMailObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                IssueMailObj.StartProcess();
            }
            catch (Exception ex)
            {
                ProcessMsgOBJ_ErrorNotification(ex);
            }
            _WriteLogObj.WriteLogFileInDate();
        }
        static void CopyRvsIssFiles()
        {

            string ExeName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _WriteLogObj = new WriteLog(ConfigDetails.EXELoc + "\\RvsCopy");

            string GangIP = ConfigDetails.TDXPSGangtokIP;
            if (string.IsNullOrEmpty(GangIP) || ConfigDetails.isPing(GangIP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(GangIP + " IP is not accessible..");
                return;
            }

            ////////////Copy revise and issue files
            RvsAndIssFiles RvsAndIssFilesObj = new RvsAndIssFiles();
            RvsAndIssFilesObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
            RvsAndIssFilesObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
            RvsAndIssFilesObj.StartProces();

            _WriteLogObj.WriteLogFileInDate();
        }

        private static void EPOutProcess()
        {
            //////////Insert Message to get issue files
            InsertIssueFilesMessage();

            //////////Insert Message to send proof to author
            InsertToAuthorMessage();

            //////////Send  Message for Stripns Process
            StripnsProcess();

            string GangIP = ConfigDetails.TDXPSGangtokIP;
            if (string.IsNullOrEmpty(GangIP) || ConfigDetails.isPing(GangIP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(GangIP + " IP is not accessible..");
                return;
            }

            try
            {
                QCSubmit QCSubmitObj = new QCSubmit();
                QCSubmitObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                QCSubmitObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                QCSubmitObj.StartProcess(ConfigDetails.PaginationOUT);

                EPOut EPOutObj = new EPOut();
                EPOutObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                EPOutObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                EPOutObj.StartProcess(ConfigDetails.EPOut);

                CopyGraphicsToTDXPS TDXPSGraphicsObj = new CopyGraphicsToTDXPS();
                TDXPSGraphicsObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                TDXPSGraphicsObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                TDXPSGraphicsObj.StartProcess(ConfigDetails.GrOut);



                //EVDataset EVDatasetObj = new EVDataset(ConfigDetails.EVOut);
                //EVDatasetObj.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                //EVDatasetObj.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                //EVDatasetObj.StartProcess();
            }
            catch (Exception ex)
            {
                ProcessMsgOBJ_ErrorNotification(ex);
            }
            _WriteLogObj.WriteLogFileInDate();
        }
        static void setConfigFileAtRuntime(string runtimeconfigfile)
        {


            // Specify config settings at runtime.
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.File = runtimeconfigfile;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        static void ProcessMsgOBJ_ErrorNotification(Exception Ex)
        {
            _WriteLogObj.AppendLog("---------Start Error detail------ ");
            _WriteLogObj.AppendLog(Ex);
            _WriteLogObj.AppendLog("---------End Error detail------ ");
        }
        static void ProcessMsgOBJ_ProcessNotification(string NotificationMsg)
        {
            Console.WriteLine(NotificationMsg);
            _WriteLogObj.AppendLog(NotificationMsg);
        }

        static void InsertStrpnsMessage()
        {
            string StrpnsTXT = @"C:\Strpns.txt";
            if (File.Exists(StrpnsTXT))
            {
                string[] StrpnsJIDS = File.ReadAllLines(StrpnsTXT);
                foreach (string Line in StrpnsJIDS)
                {

                    string[] JIDVolIss = Line.Split('\t');
                    if (JIDVolIss.Length == 3)
                    {
                        string Clnt = JIDVolIss[0].ToUpper();
                        string JID = JIDVolIss[1];
                        string AID = JIDVolIss[2];
                        //StripnsMsg.SendMsgToGang(Clnt,JID,AID);
                    }
                }
            }
        }

        static void InsertIssueFilesMessage()
        {
            string SrchPath = ConfigDetails.Issue;
            string[] txtFls = Directory.GetFiles(SrchPath, "*.txt");
            foreach (string txtFl in txtFls)
            {
                string Fname = Path.GetFileNameWithoutExtension(txtFl);
                string[] JIDVolIss = Fname.Split('_');
                if (JIDVolIss.Length == 3)
                {
                    string JID = JIDVolIss[0].ToUpper();
                    string VOL = JIDVolIss[1];
                    string ISS = JIDVolIss[2];
                    string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                    OPSDB OPSDBobj = new OPSDB(_OPSConStr);

                    string[] AIDS = File.ReadAllLines(txtFl);
                    foreach (string AID in AIDS)
                    {
                        string JIDAID = JID + AID.ToUpper().Replace(JID, "").Trim(new char[] { ' ', '_', '-', '\t', '.' });
                        JIDAID = JIDAID.ToUpper();
                        OPSDBobj.InsertIssFilesDetail(JIDAID, Fname);
                        ProcessMsgOBJ_ProcessNotification("ISSUE File :: " + JIDAID + "\t" + Fname);
                    }
                    File.Delete(txtFl);
                }

            }
        }

        static void InsertToAuthorMessage()
        {
            FreshResupply FrshRsply = new FreshResupply();
            FrshRsply.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
            FrshRsply.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

            FrshRsply.StartProcess();


            //string SrchPath = ConfigDetails.ToAuthor;
            //string[] pdfFls = Directory.GetFiles(SrchPath, "*.pdf");
            //foreach (string pdfFl in pdfFls)
            //{

            //    string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            //    string Fname = Path.GetFileNameWithoutExtension(pdfFl);
            //    string[] JIDAIDArr = Fname.Split(new char[] { '_', '-' });
            //    string JIDAID = string.Empty;
            //    if (JIDAIDArr.Length == 2)
            //    {
            //        string JID = JIDAIDArr[0].ToUpper();
            //        string AID = JIDAIDArr[1];
            //        JIDAID = JID + AID.ToUpper().Replace(JID, "").Trim(new char[] { ' ', '_', '-', '\t', '.' });
            //    }
            //    else
            //    {
            //        JIDAID = Fname;
            //    }


            //    if (!string.IsNullOrEmpty(JIDAID))
            //    {
            //        string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            //        OPSDB OPSDBobj = new OPSDB(_OPSConStr);
            //        OPSDBobj.InsertToAuthorMessage(JIDAID, "S100");
            //        ProcessMsgOBJ_ProcessNotification("To Author File :: " + JIDAID + "\t" + Fname);
            //        File.Delete(pdfFl);
            //    }
            //}
        }

        static void MakingAnnotation()
        {
            string SrchPath = @"C:\AnnPDF\";
            string OutPath = @"C:\AnnPDF\out\";

            if (!Directory.Exists(SrchPath))
                Directory.CreateDirectory(SrchPath);

            if (!Directory.Exists(OutPath))
                Directory.CreateDirectory(OutPath);

            try
            {
                /////////Do'nt change sequence of below three lines. It will effect the entire process.
                //string[] args = new string[1];
                //args[0] = SrchPath;
                string[] Pdfs = Directory.GetFiles(SrchPath, "*.pdf");


                if (Pdfs.Length > 0)
                {
                    KillProcess("AdobeCollabSync");
                    KillProcess("Acrobat");
                    KillProcess("PDFAnnotationEnable");
//                    KillProcess("AutoEproof");
                    PDFAnnotation.Program.Main(Pdfs);
                }

                foreach (string filePDF in Pdfs)
                {
                    if (File.Exists(filePDF))
                    {
                        string OutPutPdf = OutPath + "\\temp_" + Path.GetFileName(filePDF);

                        if (File.Exists(OutPutPdf))
                        {
                            File.Delete(OutPutPdf);
                        }

                        File.Copy(filePDF, OutPutPdf);

                        File.Delete(filePDF);
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                _WriteLogObj.AppendLog(ex);
            }
        }
        static void CopyFromGang()
        {
            Console.WriteLine("Connecting to Gangtok Server");

            string GangIP = ConfigDetails.GangIP;
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = GangIP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName = ConfigurationSettings.AppSettings["QName"];

            if (string.IsNullOrEmpty(GangIP) || ConfigDetails.isPing(GangIP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(GangIP + " IP is not accessible..");
                return;
            }
            try
            {
                ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

                Msg.StartProcess(MsgPubObj);
                List<MsgRcvr.MNTInfo> MNTList = Msg.MsgList;


                if (MNTList != null && MNTList.Count > 0)
                {
                    ProcessMsgOBJ_ProcessNotification("Insert message's detail in database");
                    string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                    OPSDB _OPSDBObj = new OPSDB(_OPSConStr);
                    foreach (MsgRcvr.MNTInfo MNT in MNTList)
                    {
                        MessageDetail MsgDtl = new DatabaseLayer.MessageDetail();
                        MsgDtl.Client = MNT.Client;
                        MsgDtl.JID = MNT.JID;
                        MsgDtl.AID = MNT.AID;
                        MsgDtl.Stage = MNT.Stage;
                        MsgDtl.IP = MsgPubObj.IPAddress.Replace("activemq:tcp://", ""); ;
                        _OPSDBObj.InsertMessageDetal(MsgDtl);
                        ProcessMsgOBJ_ProcessNotification("Successfully inserted message's detail in database");
                    }
                }
                if (MNTList != null && MNTList.Count > 0)
                {
                    ProcessMsgOBJ_ProcessNotification("No. of message on Gangtok server :: " + MNTList.Count);

                    string _ServerPath = ConfigDetails.ServerPath;
                    string _GangServerPath = ConfigDetails.GangServerPath;
                    foreach (MsgRcvr.MNTInfo MNT in MNTList)
                    {
                        string CopyFrom = _GangServerPath + "\\" + MNT.JID + MNT.AID;
                        string CopyTo = _ServerPath + "\\" + MNT.JID + MNT.AID;

                        if (Directory.Exists(CopyTo))
                        {
                            Directory.Delete(CopyTo, true);
                        }
                        if (Directory.Exists(CopyFrom))
                        {
                            ProcessMsgOBJ_ProcessNotification("Copy From  " + CopyFrom);
                            ProcessMsgOBJ_ProcessNotification("Copy To  " + CopyTo);
                            Copy(CopyFrom, CopyTo);
                        }

                        if (Directory.Exists(CopyTo))
                        {
                            ProcessMsgOBJ_ProcessNotification("Copied Successfully");
                        }
                    }
                }
                else
                {
                    ProcessMsgOBJ_ProcessNotification("No Message on Gangtok server to process");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }

        }
        static bool CopyFromGangtokToNoida(string JID, string AID)
        {
            string _ServerPath = ConfigDetails.ServerPath;
            string _GangServerPath = ConfigDetails.GangServerPath;

            string CopyFrom = _GangServerPath + "\\" + JID + AID;
            string CopyTo = _ServerPath + "\\" + JID + AID;

            if (Directory.Exists(CopyTo))
            {
                Directory.Delete(CopyTo, true);
            }
            if (Directory.Exists(CopyFrom))
            {
                ProcessMsgOBJ_ProcessNotification("Copy From  " + CopyFrom);
                ProcessMsgOBJ_ProcessNotification("Copy To  " + CopyTo);
                Copy(CopyFrom, CopyTo);
                return true;
            }

            return false;
        }

        static bool UploadArticleOnFtp()
        {
            //ftp://sg-ftp.blackwellpublishing.com/From%20Thomson/MIM/Fresh%20Proofs/


            string DateFolder = DateTime.Today.ToString("dd-MM-yyyy");
            string FTPURL = "ftp://ftp.aptaracorp.com/INCOMING/CJCE/";// + DateFolder;
            string Uname = "wileyftp";
            string PWD = "APwil13";

            string _UploadFileName = @"C:\FMS\test.pdf";
            try
            {

                //LogStr.Add("Upload to FTP");
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);
                //FtpObj.ProcessNotification += ProcessEventHandler;
                //FtpObj.ErrorNotification += ProcessErrorHandler;


                if (FtpObj.FtpDirectoryExists(FTPURL) == false)
                {
                    FtpObj.CreateFtpFolder(FTPURL);
                }
                FtpObj.UploadFileToFTP(_UploadFileName);
                //LogStr.Add("File moved to backup");
            }
            catch (Exception ex)
            {
                //LogStr.Add("Error while uploaing to FTP..");
                //LogStr.Add("Error Message :: " + ex.Message);
            }

            return true;
        }
        static bool CreateURL(string _PDFPath)
        {
            //http://59.160.102.163/Thieme/JRM/13-0221/13-0221.pdf 
            //http://59.160.102.163/cgi-bin/JWVCH/PSSA/PSSA201300180/pssa_201300180.cgi

            URLService.CreateEproofURL URLObj = new URLService.CreateEproofURL();
            string pdf = UploadFile(URLObj, _PDFPath);
            if (!string.IsNullOrEmpty(pdf))
                URLObj.CreateURL("JWUK", "CBIN", "10689TEST", pdf, "10689", "10689");
            else
                return false;

            return true;
        }
        static bool CreateURL(string _PDFPath, string Client, string JID, string _AID, string _UID, string _PWd)
        {
            //http://59.160.102.163/Thieme/JRM/13-0221/13-0221.pdf 
            //http://59.160.102.163/cgi-bin/JWVCH/PSSA/PSSA201300180/pssa_201300180.cgi

            URLService.CreateEproofURL URLObj = new URLService.CreateEproofURL();
            string pdf = UploadFile(URLObj, _PDFPath);
            if (!string.IsNullOrEmpty(pdf))
                URLObj.CreateURL(Client, JID, _AID, pdf, _UID, _PWd);
            else
                return false;

            return true;
        }
        static string UploadFile(URLService.CreateEproofURL EproofURL, string filename)
        {
            try
            {
                // get the exact file name from the path
                String strFile = System.IO.Path.GetFileName(filename);

                // create an instance fo the web service


                // get the file information form the selected file
                FileInfo fInfo = new FileInfo(filename);

                // get the length of the file to see if it is possible
                // to upload it (with the standard 4 MB limit)
                long numBytes = fInfo.Length;
                double dLen = Convert.ToDouble(fInfo.Length / 1000000);

                // Default limit of 4 MB on web server
                // have to change the web.config to if
                // you want to allow larger uploads
                if (dLen < 4)
                {
                    // set up a file stream and binary reader for the 
                    // selected file
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);

                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();

                    // pass the byte array (file) and file name to the web service
                    string sTmp = EproofURL.UploadFile(data, strFile);
                    fStream.Close();
                    fStream.Dispose();

                    return sTmp;
                    // this will always say OK unless an error occurs,
                    // if an error occurs, the service returns the error message
                    //MessageBox.Show("File Upload Status: " + sTmp, "File Upload");
                }
                else
                {
                    // Display message if the file was too large to upload
                    // MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
                }
            }
            catch (Exception ex)
            {
                // display an error message to the user
                // MessageBox.Show(ex.Message.ToString(), "Upload Error");
            }
            return string.Empty;
        }
        public static void Copy(string Src, string Dst)
        {
            Console.WriteLine(Dst);
            ProcessStartInfo PI = new ProcessStartInfo();
            PI.FileName = "cmd";
            PI.UseShellExecute = false;
            PI.WindowStyle = ProcessWindowStyle.Hidden;
            PI.Arguments = " /c xcopy \"" + Src + "\\*.*\" \"" + Dst + "\\*.*\"" + " /I/C/Q/S/R";
            Process PR = new Process();
            PR.StartInfo = PI;
            PR.Start();
            PR.WaitForExit();
        }
        static bool CreatePdfTemp()
        {

            string _XMLPath = @"C:\3\IJA\130092.xml";
            StringBuilder Log = new StringBuilder();

            string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            OPSDB _OPSDBObj = new OPSDB(_OPSConStr);
            OPSDetail _OPSDetailObj = null;
            _OPSDetailObj = _OPSDBObj.GetOPSDetails("IJA", "THIEME");
            ArticleInfo _ArticleInfoOBJ = new ArticleInfo();


            ThiemeXml _ThiemeObj = null;
            PDFFormProcess _PDFProObj = null;

            try
            {
                string ProcessPath = @"C:\3\IJA\1.pdf";
                // ProcessPath = ProcessPath + "\\Form.pdf";


                _ThiemeObj = new ThiemeXml(_XMLPath);

                _ArticleInfoOBJ.JID = "IJA";

                _ArticleInfoOBJ.PEEmail = _OPSDetailObj.Pe_email;
                _ArticleInfoOBJ.PEName = _OPSDetailObj.Peditor;
                //_ArticleInfoOBJ.Publisher = _OPSDetailObj.Peditor + ", " + _OPSDetailObj.Pe_email;
                _ThiemeObj.FillArticleInfo(_ArticleInfoOBJ);  //sachin

                _PDFProObj = new PDFFormProcess(ProcessPath, _ArticleInfoOBJ);
                string result = _PDFProObj.ProcessOnPDF();
                if (result == "Yes")
                {
                    Console.WriteLine("Form pdf is filled properly");
                    return true;
                }
                else
                {
                    Console.WriteLine("Form pdf is not filled properly");
                    return false;
                }
            }
            catch (Exception Err)
            {
                return false;
            }

        }
        static void SendMail()
        {
            //MailDetail MailDetailOBJ = new MailDetail();
            //MailDetailOBJ.MailFrom = "thieme.j@thomsondigital.com";
            //MailDetailOBJ.MailTo = "";
            //MailDetailOBJ.MailCC = "";
            //MailDetailOBJ.MailBCC = "";

            //MailDetailOBJ.MailSubject = "Your eProof is now available for  IJA 130092 - Sample CTA";
            //MailDetailOBJ.MailBody = File.ReadAllText(@"C:\3\IJA\MailBody.txt");


            //eMailProcess eMailProcessOBJ = new eMailProcess();
            //eMailProcessOBJ.SendMailExternal(MailDetailOBJ);

        }
        static void TestForOPSSend(string _PDFPath)
        {

            ////////////To check if pdf page has line no then failed it
            PdfProcess.PDF PDFObj = new PdfProcess.PDF(_PDFPath);
            string ScndPageCntnt = PDFObj.GetScndPageCntnt();
            string[] AllLine = ScndPageCntnt.Split('\n');
            int x;
            for (int i = 0; i < AllLine.Length; i++)
            {
                if (AllLine[i].Contains(" "))
                {
                    AllLine[i] = AllLine[i].Substring(0, AllLine[i].IndexOf(' ')).Trim();

                    if (!Int32.TryParse(AllLine[i], out x))
                    {
                        AllLine[i] = string.Empty;
                    }
                }
            }
            string Mrg = string.Join(" ", AllLine).Replace(" ", "");
            string AnyChr = Regex.Match(Mrg, "[^0-9]", RegexOptions.IgnoreCase).Value;

            if (Mrg.Length > 10 && string.IsNullOrEmpty(AnyChr))
            {

            }




            //    foreach (string Line in AllLine)
            //    {
            //        string Ln = Line.Trim();
            //        if (Ln.Length > 2)
            //        {
            //            if (Line.StartsWith("6"))//&& Line.EndsWith("2"))
            //                LineStrtwth2 = true;

            //            if (Line.StartsWith("7"))//&& Line.EndsWith("3"))
            //            {
            //                LineStrtwth3 = true;
            //                break;
            //            }
            //        }
            //    }
            //if (LineStrtwth2 && LineStrtwth3)
            //{

            //   string Remark = "Revised's pdf is same as fresh pdf.";
            //}

            //string Client = string.Empty;
            //string JID = string.Empty;
            //string AID = string.Empty;
            //string Stage = string.Empty;


            //Client = "JWVCH";
            //JID = "CLEN";
            //AID = "201300929";
            //Stage = "S100";

            //MNTInfo MNT = new MNTInfo(Client, JID, AID, Stage);
            //GetInput GetInputOBj = new GetInput(MNT);
            //if (GetInputOBj.CopyXMLPDFFromServerOrFMS())
            //{
            //    InputFiles InputFilesObj = new InputFiles(GetInputOBj.PDFPath, GetInputOBj.XMLPath);
            //    if (MNT.Stage.EndsWith("100"))
            //    {
            //        OPSProcess OPSProcessOBJ = new OPSProcess(MNT, InputFilesObj);
            //        OPSProcessOBJ.Start();
            //    }
            //}
        }
        static void TestForEproof()
        {

            string Client = string.Empty;
            string JID = string.Empty;
            string AID = string.Empty;
            string Stage = string.Empty;


            Client = "THIEME";
            JID = "SRCCM";
            AID = "210204";
            Stage = "S100";

            MNTInfo MNT = new MNTInfo(Client, JID, AID, Stage);
            GetInput GetInputOBj = new GetInput(MNT);
            GetInputOBj.CopyXMLPDFFromServerOrFMS();

            //PdfProcess.PDF S = new PdfProcess.PDF(@"C:\FMS\02143.pdf");
            //S.AddThiemeWatermark(@"C:\FMS\02143.pdf", @"C:\FMS\02143_1.pdf");

            InputFiles InputFilesObj = new InputFiles(GetInputOBj.PDFPath, GetInputOBj.XMLPath);

            eProof eProofOBJ = new eProof(MNT, InputFilesObj);
            eProofOBJ.TempProcessMNT();
            //return;

            eProofOBJ.StartValidation();

            if (eProofOBJ.ValidationResult)
            {
                if (eProofOBJ.ProcessMNT() == false)
                {
                    eProofOBJ.ValidationResult = false;
                }

            }

            //eProofResultNotification.InternalAEPSMail(eProofOBJ);
        }
        static void SendMsgToGang()
        {
            Console.WriteLine("Connecting to Gangtok Server");

            string GangIP = ConfigDetails.GangIP;
            //string GangIP = ConfigurationSettings.AppSettings["IP"];
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = GangIP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName = "TDXPS_FMS1P1PQ";
            //MsgPubObj.QName = "TDXPS_FMS1P1PQ";
            //MsgPubObj.QName = "TDXPS_WILEYEP";


            if (string.IsNullOrEmpty(GangIP) || ConfigDetails.isPing(GangIP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(GangIP + " IP is not accessible..");
                return;
            }
            try
            {
                ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;



                //Msg.WriteMSGGangtok(MsgPubObj, "MNT_JWUSA_JOURNAL_JSO_23837_110", "PAGINATIONCOMPLETE");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }

        }
        static private bool InsertStripnsMessageDetail(string JIDAID)
        {
            bool Rslt = false;

            string OPSConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            try
            {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@JIDAID", JIDAID);
                param[1] = new SqlParameter("@Stage", "S280");

                SqlHelper.ExecuteNonQuery(OPSConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsertStripnsMessageDetail", param);
                Rslt = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }
            return Rslt;
        }
        public static bool SendMsgToGangP100PS2PDF(string Client, string JID, string AID, string Stage)
        {

            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_P100";

            Console.WriteLine("Connecting to Gangtok Server");


            string GangIP = ConfigDetails.GangIP;
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = GangIP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName = "TDXPS_FMS1P1PQ";

            int RetryAttempt = 50;
            while (!ConfigDetails.isPing(ConfigDetails.TDXPSGangtokIP))
            {
                Console.WriteLine("Attempt  to access " + RetryAttempt);

                System.Threading.Thread.Sleep(500);
                RetryAttempt--;
                if (RetryAttempt == 0)
                {

                    break;
                }
            }

            try
            {
                //ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

                if (Stage.Equals("0"))
                    Msg.WriteMSGGangtokPS2Pdf(MsgPubObj, MNT, "0");
                else
                    Msg.WriteMSGGangtok(MsgPubObj, MNT, Stage);

                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "FINALSUBMITQC");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "QCSUBMIT");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "IGQC");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }
            return false;
        }

        public static bool SendMsgToGangP100AutoPdf(string Client, string JID, string AID, string Stage)
        {

            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_P100";

            Console.WriteLine("Connecting to Gangtok Server");


            string GangIP = ConfigDetails.GangIP;
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = GangIP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName = "TDXPS_FMS1P1PQ";

            int RetryAttempt = 50;
            while (!ConfigDetails.isPing(ConfigDetails.TDXPSGangtokIP))
            {
                Console.WriteLine("Attempt  to access " + RetryAttempt);

                System.Threading.Thread.Sleep(500);
                RetryAttempt--;
                if (RetryAttempt == 0)
                {

                    break;
                }
            }

            try
            {
                //ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

                if (Stage.Equals("0"))
                    Msg.WriteMSGGangtokAutoPdf(MsgPubObj, MNT, "0");
                else
                    Msg.WriteMSGGangtok(MsgPubObj, MNT, Stage);

                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "FINALSUBMITQC");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "QCSUBMIT");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "IGQC");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }
            return false;
        }

        public static bool SendMsgToGang(string Client, string JID, string AID, string Stage)
        {

            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110";

            Console.WriteLine("Connecting to Gangtok Server");


            string GangIP = ConfigDetails.GangIP;
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = GangIP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName = "TDXPS_FMS1P1PQ";

            int RetryAttempt = 50;
            while (!ConfigDetails.isPing(ConfigDetails.TDXPSGangtokIP))
            {
                Console.WriteLine("Attempt  to access " + RetryAttempt);

                System.Threading.Thread.Sleep(500);
                RetryAttempt--;
                if (RetryAttempt == 0)
                {

                    break;
                }
            }

            try
            {
                //ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

                if (Stage.Equals("0"))
                    Msg.WriteMSGGangtokPS2Pdf(MsgPubObj, MNT, "0");
                else
                    Msg.WriteMSGGangtok(MsgPubObj, MNT, Stage);

                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "FINALSUBMITQC");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "QCSUBMIT");
                //Msg.WriteMSGGangtok(MsgPubObj, MNT, "IGQC");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }
            return false;
        }
        public static bool SendMsgToNoida(string Client, string JID, string AID)
        {

            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110_PS";

            Console.WriteLine("Connecting to Gangtok Server");
            string IP = ConfigurationSettings.AppSettings["IP"];
            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress = IP;
            MsgPubObj.UName = ConfigurationSettings.AppSettings["UID"];
            MsgPubObj.Password = ConfigurationSettings.AppSettings["PWd"];
            MsgPubObj.QName = "TDXPS_FMS1P1PQ";

            int RetryAttempt = 50;
            while (!ConfigDetails.isPing(IP))
            {
                RetryAttempt--;
                if (RetryAttempt == 0)
                {
                    ProcessMsgOBJ_ProcessNotification("Attempt  to access " + RetryAttempt);
                    break;
                }
            }

            if (string.IsNullOrEmpty(IP) || ConfigDetails.isPing(IP) == false)
            {
                ProcessMsgOBJ_ProcessNotification(IP + " IP is not accessible..");
                return false;
            }
            try
            {
                ProcessMsgOBJ_ProcessNotification("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                Msg.ErrorNotification += ProcessMsgOBJ_ErrorNotification;

                Msg.WriteMSGGangtok(MsgPubObj, MNT, "INPAGINATIONMGMT_LWW");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _WriteLogObj.AppendLog(ex);
            }
            return false;
        }


        static string EproofApplicationValidation()
        {
            string eProofServer = "172.16.2.251";

            StringBuilder LogStr = new StringBuilder();

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            int RetryAttempt = 0;
            while (true)
            {
                LogStr.Clear();
                PingReply reply = pingSender.Send(eProofServer, timeout, buffer, options);
                if (reply.Status == IPStatus.Success || RetryAttempt > 10)
                {
                    break;
                }
                else
                {
                    LogStr.AppendLine("eProof Server is not accessible");
                    LogStr.AppendLine("Check this IP.....");
                    LogStr.AppendLine(eProofServer);
                }
                RetryAttempt++;

                System.Threading.Thread.Sleep(1000);
            }

            string ChkPath = @"\\172.16.2.251\htdocs";
            //if (!Directory.Exists(ChkPath))
            //LogStr.AppendLine(ChkPath + " path is not accessible. Please check.");


            //if (!Directory.Exists(ConfigDetails.OPSPDFLoc))
            //LogStr.AppendLine(ConfigDetails.OPSPDFLoc + " path is not accessible. Please check.");


            Console.WriteLine(LogStr);
            return LogStr.ToString();
        }
        static string ApplicationValidation()
        {

            StringBuilder LogStr = new StringBuilder();
            /////////////////Validate config file and required application's key
            //AEPSJWConnectionString
            //TemplatePath
            //OPSIPAddress
            //CheckDirectory
            //ReviewFolderPath
            //BackupPath
            //EntityFilePath
            //WatermarkImage

            if (ConfigDetails.OPSServerIP.Equals(""))
                LogStr.AppendLine("Please provide 'OPSServerIP'   key value in config file");
            else if (ConfigDetails.OPSServerPath.Equals(""))
                LogStr.AppendLine("Please provide 'OPSServerPath' key value in config file");
            else if (ConfigDetails.OPSPDFLoc.Equals(""))
                LogStr.AppendLine("Please provide 'OPSPDFLoc'  key value in config file");
            else if (ConfigDetails.SearchFolder.Equals(""))
                LogStr.AppendLine("Please provide 'SearchFolder'  key value in config file");
            else if (ConfigDetails.TemplatePath.Equals(""))
                LogStr.AppendLine("Please provide 'TemplatePath'  key value in config file");


            if (LogStr.Length > 0)
            {
                string ConfigStr = "<appSettings>";
                ConfigStr += "<add key =\"TemplatePath\"    value=\"\\\\application1\\wwwroot\\AEPSManager\\OPSTEMPLATE\" />";
                ConfigStr += "<add key =\"OPSServerIP\"     value=\"203.196.133.9\" />";
                ConfigStr += "<add key =\"OPSPDFLoc\"       value=\"\\ServerIP\\OPSProofs\" />";
                ConfigStr += "<add key =\"SearchFolder\"    value=\"C:\\AEPS\\DISTILLER\\JWUSA\\FRESH#C:\\AEPS\\DISTILLER\\JWUK\\FRESH\" />";
                ConfigStr += "</appSettings>";
            }
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            /*
            LogStr.AppendLine("OPSServer is not checked and is ignored for 59.160.102.180");

            LogStr.AppendLine("OPSServer is going to check for " + ConfigDetails.OPSServerIP);
            
            PingReply reply = pingSender.Send(ConfigDetails.OPSServerIP, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {

            }
            else
            {
                if (ConfigDetails.OPSServerIP == "59.160.102.180")
                { }
                else
                {
                    LogStr.AppendLine("OPSServer not accessible");
                    LogStr.AppendLine("Check this IP.....");
                    LogStr.AppendLine(ConfigDetails.OPSServerIP);
                }
            }
             
*/


            if (!Directory.Exists(ConfigDetails.TemplatePath))
                LogStr.AppendLine(ConfigDetails.TemplatePath + " path is not accessible. Please check.");


            if (!Directory.Exists(ConfigDetails.OPSPDFLoc))
                LogStr.AppendLine(ConfigDetails.OPSPDFLoc + " path is not accessible. Please check.");

            //if (!Directory.Exists(ConfigDetails.OPSServerPath))
            //    LogStr.AppendLine(ConfigDetails.OPSServerPath + " path is not accessible. Please check.");


            Console.WriteLine(LogStr);
            return LogStr.ToString();
        }
        static void SetCultureInfo()
        {

            // Creating a Global culture specific to our application.
            System.Globalization.CultureInfo cultureInfo =
                new System.Globalization.CultureInfo("en-US");
            // Creating the DateTime Information specific to our application.
            System.Globalization.DateTimeFormatInfo dateTimeInfo =
                new System.Globalization.DateTimeFormatInfo();
            // Defining various date and time formats.
            dateTimeInfo.DateSeparator = "-";
            dateTimeInfo.LongDatePattern = "yyyy-MM-dd";
            dateTimeInfo.LongTimePattern = "hh:mm:ss tt";

            // Setting application wide date time format.
            cultureInfo.DateTimeFormat = dateTimeInfo;
            // Assigning our custom Culture to the application.

            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        static private string UnzipFile(string ZipPath)
        {
            string ExtractTo = "C:\\TempZip";
            try
            {
                string file = "";

                if (Directory.Exists(ExtractTo))
                {
                    Directory.Delete(ExtractTo, true);
                }
                Directory.CreateDirectory(ExtractTo);

                FastZip fz = new FastZip();
                fz.ExtractZip(ZipPath, ExtractTo, null);
                string[] ReviewFile = Directory.GetFiles(ExtractTo, "*.pdf");

                Array.Sort(ReviewFile);
                Array.Reverse(ReviewFile);
                if (ReviewFile.Length > 0)
                {
                    file = ReviewFile[0].ToString();
                }

                return file;
            }
            catch (Exception)
            {
                return "";
            }
        }

        static void s()
        {
            string ps = @"\\172.16.3.11\OPSProofs\CLEN-201400709_review.pdf__dfeb3303b0d6484d939d2d7567eaec36";
            string[] ss = Directory.GetFiles(ps, "*.xml");
            foreach (string f in ss)
            {
                RctRect(f);
            }
            return;
        }
        static bool eProoUnaccessible(string LogStr)
        {
            bool Result = false;

            ProcessMsgOBJ_ProcessNotification("GetMailBody Start..");

            StringBuilder MailB = new StringBuilder();
            MailB.AppendLine("Dell All,");
            MailB.AppendLine(" ");


            MailB.AppendLine("eProof Server is not accessible.");
            MailB.AppendLine(LogStr.ToString());

            MailB.AppendLine(" ");
            MailB.AppendLine("Thanks,");

            ProcessMsgOBJ_ProcessNotification("GetMailBody Finish..");

            try
            {
                ProcessMsgOBJ_ProcessNotification("Process start to send mamil");
                MailDetail MailDetailOBJ = new MailDetail();
                MailDetailOBJ.MailFrom = "eproof@thomsondigital.com";
                MailDetailOBJ.MailTo = ConfigurationManager.AppSettings["UnaccesseProofMails"];

                MailDetailOBJ.MailSubject = "eProof Server is not accessible..";
                MailDetailOBJ.MailBody = MailB.ToString();

                eMailProcess eMailProcessOBJ = new eMailProcess();
                eMailProcessOBJ.ProcessNotification += ProcessMsgOBJ_ProcessNotification;
                eMailProcessOBJ.ErrorNotification += ProcessMsgOBJ_ErrorNotification;
                if (eMailProcessOBJ.SendMailInternal(MailDetailOBJ))
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                ProcessMsgOBJ_ErrorNotification(ex);
            }
            return Result;
        }
        private static bool PDFSecurity(string source, string destination)
        {
            bool result = false;
            PdfReader reader;
            PdfStamper stamper;
            try
            {
                try
                {
                    reader = new PdfReader(source);
                    stamper = new PdfStamper(reader, new System.IO.FileStream(destination, System.IO.FileMode.CreateNew));
                    stamper.SetEncryption(null, System.Text.Encoding.UTF8.GetBytes("Th0MsOnD123"), PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_FILL_IN | PdfWriter.ALLOW_MODIFY_ANNOTATIONS | PdfWriter.ALLOW_COPY, PdfWriter.DO_NOT_ENCRYPT_METADATA);
                    stamper.Close();
                    reader.Close();
                    if (File.Exists(source))
                        File.Delete(source);
                    reader = new PdfReader(destination);
                    if (!reader.IsEncrypted())
                    {
                        ProcessMsgOBJ_ProcessNotification("PDF is not encrypted ");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ProcessMsgOBJ_ProcessNotification("Error in PDF Security: " + ex.Message);
                    return false;
                }

                result = true;
            }
            catch (Exception ex)
            {
                ProcessMsgOBJ_ProcessNotification(ex.Message);
                result = false;
            }
            System.Threading.Thread.Sleep(1000);
            return result;
        }
    }


    //public class TempClass:FMSStructure
    //{
    //    public private void DD()
    //    { 
    //        string  FMSPath = "D:\FMS\";
    //        string[] Lines = File.ReadAllLines("c:\\11.txt");
    //        foreach (string Line in Lines)
    //        {

    //            string[] Parts = Line.Split('\t');
    //            string Clnt = Parts[0];
    //            string JID = Parts[1];
    //            string AID = Parts[2];

    //            FMS ss = new FMS(Clnt,JID,AID,FMSPath);


    //        }

    //    }
    //}
    //public class FMS:FMSStructure
    //{

    //    public FMS(string Clnt,string  JID,string  AID,string  FMSPath):base(Clnt,JID,AID,FMSPath)
    //    {

    //    }

    //    public void CreateFolder ()
    //    {

    //        Directory.CreateDirectory(this.AID);
    //        //Directory.CreateDirectory(this.Attributes);
    //        //Directory.CreateDirectory(this.Orders);
    //        //Directory.CreateDirectory(this.Originals);
    //        Directory.CreateDirectory(this.Output);
    //        Directory.CreateDirectory(this.Text);
    //        Directory.CreateDirectory(this.Output);
    //        Directory.CreateDirectory(this.WorkArea);
    //        Directory.CreateDirectory(this.WorkAreaGraphics);
    //        Directory.CreateDirectory(this.MNTFolder);
    //        Directory.CreateDirectory(this.Client);
    //        Directory.CreateDirectory(this.JID);

    //    }
    //}
    //SendMsgToGang("JWVCH", "ADEM", "201500347", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "STAR", "201500118", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "AJMGA", "37395", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "VSU", "12403", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "ADEM", "201500405", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "ADEM", "201500361", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "PSSA", "201532398", "QCSUBMIT");
    //SendMsgToGang("SINGAPORE", "JIPB", "12424", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "PPAP", "201500080", "QCSUBMIT");
    //SendMsgToGang("SINGAPORE", "PBAF", "12084", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "JCB", "25370", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "JMV", "24377", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "JCPH", "599", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "AJP", "22479", "QCSUBMIT");
    //SendMsgToGang("JWVCH", "PSSA", "201532381", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "JABA", "251", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "AJP", "22483", "QCSUBMIT");
    //SendMsgToGang("JWUSA", "PBC", "25721", "QCSUBMIT");
}


