    using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using ProcessNotification;
using System.Data;
using System.Threading;
namespace LWWAutoIntegrate
{
    class ProcessTwo:MessageEventArgs
    {
        GoXMLList d = new GoXMLList();
        Dictionary<string, string> _MAPJIDList = new Dictionary<string, string>();
        string ExeLocationPath = string.Empty;
        string ProcessedPath = string.Empty;
        public ProcessTwo()
        {
            ExeLocationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //ProcessedPath = @"\\172.16.2.209\e$\Application\LWWAutoIntegrate" + "\\Processed";
            ProcessedPath = @"D:\E_Drive\LWWAutoIntegrate\Processed";    //live
            //ProcessedPath = @"D:\LWWAutoIntegrate" + "\\Processed";
        }
        public void StartProcess()
        {
            ProcessEventHandler("Start process to filll article's details and create order");
            string JIDMap = ConfigurationManager.AppSettings["JIDMap"];



            if (!string.IsNullOrEmpty(JIDMap))
            {
                string[] JIDS = JIDMap.Split(',');
                foreach (string JID in JIDS)
                {
                    string[] SplitJID = JID.Split('#');
                    if (!_MAPJIDList.ContainsKey(SplitJID[0]))
                        _MAPJIDList.Add(SplitJID[0], SplitJID[1]);
                }
            }

            
                
           ProcessEventHandler("Fetch areticle list from database");
            // get record list of item to integrate from database
           d.AssignIntegrateArticleGoXMLList();

           ProcessEventHandler("Count GoXML ::" + d.GoFileList.Count);
           foreach (string s in   d.GoFileList)
           {
               try
               {
                   ProcessGoXML(s);

               }
               catch(Exception ex)
               {
                   ProcessErrorHandler(ex);
               }
           }

           ProcessEventHandler("ProcessGoXML Finish");
           MoveInputFilesInJIDAIDFolder();

           ProcessEventHandler("MoveInputFilesInJIDAIDFolder Finish");
            
        }

        public void ProcessGoXML(string GoXMLPath)
        {

            bool FMSProcess = false;
            ProcessEventHandler("ProcessGoXML : " +GoXMLPath);
            string ExeLocationPath = ConfigDetails.TempPath ;// Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string XMLPath      = ExeLocationPath + "\\Download\\" + Path.GetFileNameWithoutExtension(GoXMLPath) + ".go.xml";
            string ZipPath      = ExeLocationPath + "\\Download\\" + Path.GetFileNameWithoutExtension(GoXMLPath) + ".zip";


            if (!File.Exists(XMLPath) || !File.Exists(ZipPath))
            { 
                
                ProcessEventHandler(XMLPath + " : not found");
                ProcessEventHandler(ZipPath + " : not found");
                return;
            }

            ProcessEventHandler("XMLPath : " + XMLPath);
            ProcessEventHandler("ZipPath : " + ZipPath);

            GoXmlPrcs GoXmlPrcsObj = new GoXmlPrcs(XMLPath, ZipPath);
            GoXmlPrcsObj.ProcessNotification += GoXmlPrcsObj_ProcessNotification;
            GoXmlPrcsObj.ErrorNotification += GoXmlPrcsObj_ErrorNotification;
            GoXmlPrcsObj.Client    = "LWW";
            GoXmlPrcsObj.ServerIP  = "FMS";
            //GoXmlPrcsObj.FigCount =   
            if (GoXmlPrcsObj.StartToGetArticleInfo())
            {
                GoXmlPrcsObj.GoXMLPath = GoXMLPath;

                ProcessEventHandler("GoXMLPath : " + GoXMLPath);

                //if (GoXmlPrcsObj.JID == "OTAI")
                //{
                //    GoXmlPrcsObj.JID = "OTA";
                //}
                //       check database for fresh prrof
                DataSet ds1 = GetDataForANNSURG(GoXmlPrcsObj.JID, GoXmlPrcsObj.AID);
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (_MAPJIDList.ContainsKey(GoXmlPrcsObj.JID))
                        {
                            GoXmlPrcsObj.JID = _MAPJIDList[GoXmlPrcsObj.JID];
                        }
                    }
                }
                if (GoXmlPrcsObj.JID == "ANNSURG")
                {
                    GoXmlPrcsObj.JID = GoXmlPrcsObj.JID;
                }
                else if (_MAPJIDList.ContainsKey(GoXmlPrcsObj.JID))
                {
                    GoXmlPrcsObj.JID = _MAPJIDList[GoXmlPrcsObj.JID];
                }
                
                

                if (GoXMLPath.IndexOf("Send to Compositor", StringComparison.OrdinalIgnoreCase) != -1 || GoXMLPath.IndexOf("Collate Correction", StringComparison.OrdinalIgnoreCase) != -1 || GoXMLPath.IndexOf("Revised Proof", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    GoXmlPrcsObj.FMSStage = "S200";
                    GoXmlPrcsObj.Stage = "REVISED";
                    FMSProcess = true;
                }
                else if (GoXMLPath.IndexOf("Prepare First Proof", StringComparison.OrdinalIgnoreCase) != -1 || GoXMLPath.IndexOf("Set Up Peer Review", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    GoXmlPrcsObj.FMSStage = "S100";
                    GoXmlPrcsObj.Stage = "Fresh";
                    FMSProcess = true;
                }
                else
                {
                    GoXmlPrcsObj.FMSStage = "S100";
                    GoXmlPrcsObj.Stage    = "Fresh";
                    FMSProcess = false;
                }

                if (string.IsNullOrEmpty(GoXmlPrcsObj.TaskName) && GoXmlPrcsObj.FMSStage.Equals("S200"))
                {
                    GoXmlPrcsObj.TaskName = "Revise Article Proofs";
                }
                if (string.IsNullOrEmpty(GoXmlPrcsObj.DOI))
                {
                    GoXmlPrcsObj.DOI = "";
                }


                //// code for some specific jid Rohit 14-6-21
                String WrongJidNames = ConfigurationManager.AppSettings["WrongJidNames"].ToString();
                if (WrongJidNames.Contains(GoXmlPrcsObj.JID + ",") && GoXmlPrcsObj.Stage == "S200")
                {
                    string FmsPath_half = ConfigurationSettings.AppSettings["FmsServerPath"].ToString();
                    string FmsPath = FmsPath_half + GoXmlPrcsObj.JID + "\\" + GoXmlPrcsObj.AID + "\\Attributes\\aid.ini";
                    if (!File.Exists(FmsPath))
                    {
                        // now integrate with previous id
                        if (GoXmlPrcsObj.JID == "CARDIODISCOVERY")
                        {
                            GoXmlPrcsObj.JID = "CD9";
                        }
                        else if (GoXmlPrcsObj.JID == "EJCCM")
                        {
                            GoXmlPrcsObj.JID = "EJ9";
                        }
                        else if (GoXmlPrcsObj.JID == "JBIOXRESEARCH")
                        {
                            GoXmlPrcsObj.JID = "JR9";
                        }
                        else if (GoXmlPrcsObj.JID == "MD-CASES")
                        {
                            GoXmlPrcsObj.JID = "MD9";
                        }

                    }
                }



                // yha pe inout file ko ek location pe bhej dena h     
                //string rawInput = ConfigurationSettings.AppSettings["UnitouchRawInputPath"] + "\\" + Path.GetFileName(XMLPath);
                //if (File.Exists(XMLPath))
                //{
                //    File.Copy(XMLPath, rawInput);
                //}


                if (FMSProcess)
                {
                    XmlOrder OrdrObj = new XmlOrder(GoXmlPrcsObj);

                    ProcessEventHandler("Start to create CreateXMLOrder");

                    ProcessEventHandler("CreateXMLOrder Started");
                    
                    OrdrObj.CreateXMLOrder();

                    ProcessEventHandler("End to create CreateXMLOrder");

                    string FMSOrder = ConfigDetails.FMSPath + "\\" + Path.GetFileName(OrdrObj.OrderFileName);
                    string FMSZip = ConfigDetails.FMSPath + "\\" + Path.GetFileNameWithoutExtension(OrdrObj.OrderFileName) + ".zip";

                    string UniTouchFMSOrder_Bak = ConfigurationSettings.AppSettings["UnitouchFmsPath_Bak"] + "\\" + Path.GetFileName(OrdrObj.OrderFileName);
                    string UniTouchFMSZip_Bak = ConfigurationSettings.AppSettings["UnitouchFmsPath_Bak"] + "\\" + Path.GetFileNameWithoutExtension(OrdrObj.OrderFileName) + ".zip";

                    string UniTouchFMSOrder = ConfigurationSettings.AppSettings["UnitouchFmsPath"] + "\\" + Path.GetFileName(OrdrObj.OrderFileName);
                    string UniTouchFMSZip = ConfigurationSettings.AppSettings["UnitouchFmsPath"] + "\\" + Path.GetFileNameWithoutExtension(OrdrObj.OrderFileName) + ".zip";



                    string UnitouchJid = ConfigurationSettings.AppSettings["UnitouchJID"].ToString();
                    string[] unijid = UnitouchJid.Split('#');

                    //if (unijid.Length > 0)
                    //{
                    //    for (int i = 0; i < unijid.Length; i++)
                    //    {
                    //        if (GoXmlPrcsObj.JID == unijid[i])
                    //        {
                    //            if (GoXmlPrcsObj.FMSStage == "S100")
                    //            {
                    //                ProcessEventHandler("copy start UnitouchFmsPath_Bak");
                    //                File.Copy(ZipPath, UniTouchFMSZip_Bak, true);
                    //                File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder_Bak);

                    //                ProcessEventHandler("copy end UnitouchFmsPath_Bak");

                    //                ProcessEventHandler("copy start UnitouchFmsPath");
                    //                File.Copy(ZipPath, UniTouchFMSZip, true);
                    //                File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder);

                    //                ProcessEventHandler("copy end UnitouchFmsPath");
                    //            }
                    //            if (GoXmlPrcsObj.FMSStage == "S200")
                    //            {
                    //                // chake db exists if yes then 
                    //                DataSet ds = GetDataForUnitouch(GoXmlPrcsObj.JID, GoXmlPrcsObj.AID);
                    //                if (ds.Tables.Count > 0)
                    //                {
                    //                    if (ds.Tables[0].Rows.Count > 0)
                    //                    {
                    //                        ProcessEventHandler("copy start UnitouchFmsPath_Bak");
                    //                        File.Copy(ZipPath, UniTouchFMSZip_Bak, true);
                    //                        File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder_Bak);

                    //                        ProcessEventHandler("copy end UnitouchFmsPath_Bak");

                    //                        ProcessEventHandler("copy start UnitouchFmsPath");
                    //                        File.Copy(ZipPath, UniTouchFMSZip, true);
                    //                        File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder);

                    //                        ProcessEventHandler("copy end UnitouchFmsPath");
                    //                    }
                    //                }
                    //            }

                                
                    //        }
                    //    }
                    //}
                    
                    
                    ProcessEventHandler("FMSOrder : " + FMSOrder);
                    ProcessEventHandler("FMSZip : " + FMSZip);
                    //string magzineoutput = "\\\\172.16.1.107\\LWW-Magzine\\Download\\";
                    string magzineoutput = "D:\\E_Drive\\LWW-Magzine\\Download\\";
                    string MagazineOrder = Path.GetFileName(OrdrObj.OrderFileName);
                    string MagazineZip = Path.GetFileNameWithoutExtension(OrdrObj.OrderFileName) + ".zip";
                    if (GoXmlPrcsObj.JID == "NHH")
                    {
                        magzineoutput = magzineoutput + "NHH\\" + GoXmlPrcsObj.AID;
                        if (!Directory.Exists(magzineoutput))
                        {
                            Directory.CreateDirectory(magzineoutput);
                            if (File.Exists(ZipPath))
                            {
                                MagazineOrder = magzineoutput + "\\" + MagazineOrder;
                                MagazineZip = magzineoutput + "\\" + MagazineZip;
                                File.Copy(ZipPath, MagazineZip, true);
                                Thread.Sleep(20000);
                                File.Copy(OrdrObj.OrderFileName, MagazineOrder);
                            }
                        }
                    }
                    if (GoXmlPrcsObj.JID == "NCF-JCN")
                    {
                        magzineoutput = magzineoutput + "NCF-JCN\\" + GoXmlPrcsObj.AID;
                        if (!Directory.Exists(magzineoutput))
                        {
                            Directory.CreateDirectory(magzineoutput);
                            if (File.Exists(ZipPath))
                            {
                                MagazineOrder = magzineoutput + "\\" + MagazineOrder;
                                MagazineZip = magzineoutput + "\\" + MagazineZip;
                                File.Copy(ZipPath, MagazineZip, true);
                                Thread.Sleep(20000);
                                File.Copy(OrdrObj.OrderFileName, MagazineOrder);
                            }
                        }
                    }
                    if (GoXmlPrcsObj.JID == "MCN")
                    {
                        magzineoutput = magzineoutput + "MCN\\" + GoXmlPrcsObj.AID;
                        if (!Directory.Exists(magzineoutput))
                        {
                            Directory.CreateDirectory(magzineoutput);
                            if (File.Exists(ZipPath))
                            {
                                MagazineOrder = magzineoutput + "\\" + MagazineOrder;
                                MagazineZip = magzineoutput + "\\" + MagazineZip;
                                File.Copy(ZipPath, MagazineZip, true);
                                Thread.Sleep(20000);
                                File.Copy(OrdrObj.OrderFileName, MagazineOrder);
                            }
                        }
                    }


                    if (File.Exists(ZipPath))
                    {
                        ProcessEventHandler("copy start");
                        File.Copy(ZipPath, FMSZip, true);
                        ProcessEventHandler("File Copied" + FMSZip);
                        Thread.Sleep(20000);
                        File.Copy(OrdrObj.OrderFileName, FMSOrder);
                        ProcessEventHandler("File Copied" + FMSOrder);
                        string bakpath = "D:\\Application\\Backup\\" + Path.GetFileName(OrdrObj.OrderFileName);
                        File.Copy(OrdrObj.OrderFileName, bakpath, true);
                        bakpath = "D:\\Application\\Backup\\" + Path.GetFileNameWithoutExtension(OrdrObj.OrderFileName) + ".zip";
                        File.Copy(ZipPath, bakpath, true);
                        ProcessEventHandler("copy end");
                        d.UpdateArticleDetails(GoXmlPrcsObj);
                        
                        // for unitouch
                        try
                        {
                            if (GoXmlPrcsObj.FMSStage == "S100")
                            {
                                ProcessEventHandler("copy start UnitouchFmsPath_Bak");
                                File.Copy(ZipPath, UniTouchFMSZip_Bak, true);
                                File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder_Bak);
                                ProcessEventHandler("copy end UnitouchFmsPath_Bak");
                                ProcessEventHandler("copy start UnitouchFmsPath");
                                File.Copy(ZipPath, UniTouchFMSZip, true);
                                File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder);
                                ProcessEventHandler("copy end UnitouchFmsPath");
                            }
                            if (GoXmlPrcsObj.FMSStage == "S200")
                            {
                                if (GoXmlPrcsObj.JID == "MD")
                                {
                                    ProcessEventHandler("copy start UnitouchFmsPath");
                                    File.Copy(ZipPath, UniTouchFMSZip, true);
                                    File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder);
                                    ProcessEventHandler("copy end UnitouchFmsPath");
                                }
                                //// chake db exists if yes then 
                                DataSet ds = GetDataForUnitouch(GoXmlPrcsObj.JID, GoXmlPrcsObj.AID);
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        ProcessEventHandler("copy start UnitouchFmsPath_Bak");
                                        File.Copy(ZipPath, UniTouchFMSZip_Bak, true);
                                        File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder_Bak);
                                        ProcessEventHandler("copy end UnitouchFmsPath_Bak");
                                        ProcessEventHandler("copy start UnitouchFmsPath");
                                        File.Copy(ZipPath, UniTouchFMSZip, true);
                                        File.Copy(OrdrObj.OrderFileName, UniTouchFMSOrder);
                                        ProcessEventHandler("copy end UnitouchFmsPath");
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        { }


                        string MoveAftrPrcs = ProcessedPath + "\\" + GoXmlPrcsObj.Stage;

                        if (!Directory.Exists(MoveAftrPrcs))
                            Directory.CreateDirectory(MoveAftrPrcs);

                        string MoveAftrPrcsXMLPath = MoveAftrPrcs + "\\" + OrdrObj.JID + "#" + OrdrObj.AID + ".go.xml";
                        string MoveAftrPrcsZipPath = MoveAftrPrcs + "\\" + OrdrObj.JID + "#" + OrdrObj.AID + ".zip";

                        if (File.Exists(MoveAftrPrcsXMLPath))
                            File.Delete( MoveAftrPrcsXMLPath);

                        if (File.Exists(MoveAftrPrcsZipPath))
                            File.Delete(MoveAftrPrcsZipPath);
                        
                        // commented on 6-sept-2019 to change the sequence
                        //File.Move(XMLPath, MoveAftrPrcsXMLPath);
                        //File.Move(ZipPath, MoveAftrPrcsZipPath);

                        File.Move(ZipPath, MoveAftrPrcsZipPath);
                        File.Move(XMLPath, MoveAftrPrcsXMLPath);
                    }
                }
                else
                {
                    d.UpdateArticleDetails(GoXmlPrcsObj);
                    File.Delete(XMLPath);
                    File.Delete(ZipPath);
                }
            }
           
        }

        public string GetStageFromFmsFile(string FmsPath)
        {
            string stage = "";
            StreamReader sr = new StreamReader(FmsPath);
            string FileCon = sr.ReadToEnd();
            sr.Close();

            string[] SplitA = new string[4];
            SplitA[0] = "\r\n";
            SplitA[1] = "\n\r";
            SplitA[2] = "\n";
            SplitA[3] = "\r";

            string[] FileArr = FileCon.Split(SplitA, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ar in FileArr)
            {
                if (ar.StartsWith("stage="))
                {
                    stage = ar.Substring(ar.IndexOf("=") + 1);
                }
            }

            return stage;
        }

        public DataSet GetDataForUnitouch(string JID, string AID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UnitouchConnection"].ConnectionString))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_GetUnitouchDataForIntegration]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@JID", SqlDbType.VarChar).Value = JID;
                    sqlcmd.Parameters.Add("@AID", SqlDbType.VarChar).Value = AID;
                    //string strSelectCmd = "select PII,Filename, TypeSignal, URL, Remarks, DownloadDate from SignalInfo where isbn = '" + isbn + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                    return ds;

                }
            }
            catch (Exception ex)
            {
                //log write
                return ds;
            }
        }
        public DataSet GetDataForANNSURG(string JID, string AID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_GetDataForANNSURG]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@JID", SqlDbType.VarChar).Value = JID;
                    sqlcmd.Parameters.Add("@AID", SqlDbType.VarChar).Value = AID;
                    //string strSelectCmd = "select PII,Filename, TypeSignal, URL, Remarks, DownloadDate from SignalInfo where isbn = '" + isbn + "'";
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                    return ds;

                }
            }
            catch (Exception ex)
            {
                //log write
                return ds;
            }
        }
        void MoveInputFilesInJIDAIDFolder()
        {
            DoProcess(ProcessedPath, ConfigDetails.MoveInputFiles);
        }
        void DoProcess(string CopyFrom,string CopyTo)
        {
            string[] ZipFiles = Directory.GetFiles(CopyFrom, "*.zip",SearchOption.AllDirectories);

            foreach (string ZipFile in ZipFiles)
            {
                string FileName = Path.GetFileNameWithoutExtension(ZipFile);
                string XMLFile = ZipFile.Replace(".zip",".go.xml");

                string[] JIDAID = FileName.Split('#');
                string JID = JIDAID[0];
                string AID = JIDAID[1];

                string JIDAIDFolder    = CopyTo + "\\"+ JID  + "\\" + AID;
               
                
                if (!Directory.Exists(JIDAIDFolder))
                     Directory.CreateDirectory(JIDAIDFolder);

                if (ZipFile.Contains("REVISED"))
                {
                    string JIDAIDRevFolder = CopyTo + "\\" + JID + "\\" + AID + "\\Rev";
                    string[] Revs = Directory.GetDirectories(JIDAIDFolder, "Rev*");

                    if (Revs.Length > 0)
                        JIDAIDRevFolder = CopyTo + "\\" + JID + "\\" + AID + "\\Rev" + Revs.Length.ToString();

                    if (!Directory.Exists(JIDAIDRevFolder))
                    {
                        Directory.CreateDirectory(JIDAIDRevFolder);
                    }
                    JIDAIDFolder = JIDAIDRevFolder;
                }
                

                if (File.Exists(XMLFile))
                {
                    string GoXML = File.ReadAllText(XMLFile);
                     
                    //string MatFile = JIDAIDFolder + "\\" + JID + "_" + AID + ".zip";

                    string MatFile = JIDAIDFolder + "\\" + AID + ".zip";
                    File.Copy(ZipFile, MatFile,true);
                    File.Delete(ZipFile);
                    File.Delete(XMLFile);
                }
            }
        }
        void GoXmlPrcsObj_ErrorNotification(Exception Ex)
        {
            ProcessErrorHandler(Ex);
        }

        void GoXmlPrcsObj_ProcessNotification(string NotificationMsg)
        {
            ProcessEventHandler(NotificationMsg);
        }


        void ProcessOldGoXML()
        {
            //string[] OldXmls = Directory.GetFiles(@"\\fmsbooks\k$\DOWNLOAD", "*.go.xml",SearchOption.AllDirectories);
            string[] OldXmls = Directory.GetFiles(@"\\172.16.23.5\k$\DOWNLOAD", "*.go.xml", SearchOption.AllDirectories);
            foreach (string OldXML in OldXmls)
            {
                GoXmlPrcs GoXmlPrcsObj = new GoXmlPrcs(OldXML);
                GoXmlPrcsObj.ProcessNotification += GoXmlPrcsObj_ProcessNotification;
                GoXmlPrcsObj.ErrorNotification += GoXmlPrcsObj_ErrorNotification;
                GoXmlPrcsObj.Client = "LWW";
                GoXmlPrcsObj.ServerIP = "FMS";

                if (GoXmlPrcsObj.TaskName.IndexOf("Composition", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    GoXmlPrcsObj.FMSStage = "S100";
                    GoXmlPrcsObj.Stage = "Fresh";
                   
                }
                else
                {
                    GoXmlPrcsObj.FMSStage = "S200";
                    GoXmlPrcsObj.Stage = "REVISED";
                }
                InsertLwwWip(GoXmlPrcsObj);
            }
        }

        private bool InsertLwwWip(GoXmlPrcs GoXml)
        {
           string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@Guid", GoXml.Guid);
            param[1] = new SqlParameter("@TskName", GoXml.TaskName);
            param[2] = new SqlParameter("@duedate", GoXml.DueDate);
            param[3] = new SqlParameter("@Doi", GoXml.DOI);
            param[4] = new SqlParameter("@Aid", GoXml.AID);
            param[5] = new SqlParameter("@goxml", GoXml.GoXML);
            param[6] = new SqlParameter("@Jid", GoXml.JID);
            param[7] = new SqlParameter("@clnt", GoXml.Client);
            param[8] = new SqlParameter("@stg", GoXml.FMSStage);

            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[Usp_Lww_LwwWip]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }

            return true;

        }
    }
}

