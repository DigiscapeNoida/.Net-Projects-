using System;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;
using System.Net.NetworkInformation;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgRcvr;
using ProcessNotification;

namespace AutoEproof
{
    class StripnsMsg : MessageEventArgs
    {
        string _OPSConStr = string.Empty;
        List<MNTInfo> MNTList = new List<MNTInfo>();
        List<MessageDetail> _StrpnsFlsMsgList = null;
        OPSDB _OPSDBObj = null;
        
        private void GetMsgList()
        {
            ProcessEventHandler("GetMsgList called to stripns Files");

            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(_OPSConStr))
            {
                 ProcessEventHandler("OPS database Connection String is not set");
            }
            ProcessEventHandler("OPS Database initializion process start");
            try
            {
                _OPSDBObj = new OPSDB(_OPSConStr);
                ProcessEventHandler("OPS Database initialize successfully for stripns..");

                _StrpnsFlsMsgList = _OPSDBObj.GetGetStripnsFilesMessageDetail();
                 ProcessEventHandler("Getting stripns InProcess Message Detail");
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
        }
        private void GetWileyStripinsStatusMessage()
        { 
                string GangIP = ConfigDetails.GangIP;

                MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgPublisherDtl();
                MsgPubObj.IPAddress               = GangIP;
                MsgPubObj.UName                   = ConfigurationSettings.AppSettings["GangUID"];
                MsgPubObj.Password                = ConfigurationSettings.AppSettings["GangPWD"];
                MsgPubObj.QName                   = "WileyStripinsStatus";

                try
                {
                    ProcessEventHandler("Connecting message from queue for stripins.");
                    MsgRcvr.MessageRcvr Msg = new MessageRcvr();

                    Msg.ProcessNotification += ProcessEventHandler;
                    Msg.ErrorNotification += ProcessErrorHandler;

                    Msg.StartProcessForPSMsg(MsgPubObj);
                    MNTList = Msg.MsgList;

                    ProcessEventHandler("Finished getting msgList stripins.");
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
              
        }
        public void StartProces()
        {
            ProcessEventHandler("GetWileyStripinsStatusMessage");

            GetWileyStripinsStatusMessage();

            ProcessEventHandler("GetMsgList");
            GetMsgList();

            if (_StrpnsFlsMsgList == null)
            {
                return;
            }
            foreach (MessageDetail MsgDtl in _StrpnsFlsMsgList)
            {
                try
                {

                    if ((MNTList.Find(x => x.JID.Equals(MsgDtl.JID) && x.AID.Equals(MsgDtl.AID)) != null) || MsgDtl.IP.Equals("FMS"))
                    {
                        _OPSDBObj.UpdateStripnsFilesMessageDetail(MsgDtl.MsgID);
                        ProcessEventHandler("UpdateStripnsFilesMessageDetail " + MsgDtl.MsgID + "\t" + MsgDtl.JID + "\t" + MsgDtl.AID);
                    }
                    else
                    {
                        string[] IP1 = ConfigDetails.TDXPSGangtokIP.Split('.');
                        string[] IP2 = MsgDtl.IP.Replace(":61616", "").Split('.');
                        if (IP1.Length > 1 && IP1.Length > 1)
                        {
                            if (IP1[IP1.Length - 1].Equals(IP2[IP2.Length - 1]))
                            {
                                ProcessEventHandler("ToProcess " + MsgDtl.Client + "\t" + MsgDtl.JID + "\t" + MsgDtl.AID);
                                if (ToProcess(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID))
                                {
                                    _OPSDBObj.UpdateStripnsFilesMessageDetail(MsgDtl.MsgID);
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
        }

        private bool ToProcess(string Client, string JID, string AID)
        {
            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110";

            string MNTFolder = ConfigDetails.TDXPSGangtokTempFolder + "\\" + MNT;
            string EqnFolder = ConfigDetails.TDXPSGangtokTempFolder + "\\" + MNT + "\\equation";
            string PagXML = MNTFolder + "\\pagination.xml";
            string ArtclXML = MNTFolder + "\\tx1.xml";
            string tx1_ne   = MNTFolder   + "\\tx1_ne.xml";
            
            bool Rslt = false;
            
            string LclXMLFile ;
            if (isMNTExist(MNTFolder, ArtclXML, out LclXMLFile))
            {
                if (!File.Exists(tx1_ne))
                {
                    File.Copy(ArtclXML, tx1_ne);
                }
                if (isMathExist(LclXMLFile))
                {
                    if (WritePagXML(PagXML, EqnFolder))
                    {
                        if (SendMsgToGang(Client, JID, AID))
                        {
                            Rslt = true;
                        }
                    }
                }
                else
                {
                    Rslt = true;
                }
            }
            else
            {
                ProcessEventHandler("isMNTExist retrurn false..");
                //InsertInWileyS280Resuplly(Client, JID, AID);
            }
            return Rslt;
        }
        private bool SendMsgToGang(string Client, string JID, string AID)
        {
            Console.WriteLine("Connecting to Gangtok Server");

            string MNT = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110";
            string GangIP = ConfigDetails.GangIP;

            MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgRcvr.MsgPublisherDtl();
            MsgPubObj.IPAddress               = GangIP;
            MsgPubObj.UName                   = ConfigurationSettings.AppSettings["GangUID"];
            MsgPubObj.Password                = ConfigurationSettings.AppSettings["GangPWD"];
            MsgPubObj.QName                   = "INPAGINATIONMGMT";

            int RetryAttempt = 50;
            while (!isPing(ConfigDetails.TDXPSGangtokIP))
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
                ProcessEventHandler("Connecting message from queue to send message stripns .");
                MsgRcvr.MessageRcvr Msg = new MsgRcvr.MessageRcvr();

                Msg.ProcessNotification += this.ProcessEventHandler;
                Msg.ErrorNotification   += this.ProcessErrorHandler;

                Msg.WriteStrpnsMsg(MsgPubObj, MNT, "S280");

                ProcessEventHandler("Mesage Sent for  stripns :: " + MNT);

                return true;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return false;
        }
        private bool InsertInWileyS280Resuplly(string Client, string JID, string AID)
        {
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;

            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@Client", Client);
            para[1] = new SqlParameter("@JID", JID);
            para[2] = new SqlParameter("@AID", AID);
            para[3] = new SqlParameter("@STAGE", "S280");

            try
            {
                int Rslt = SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_WileyReviseToME", para);

            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }


            return true;
        }


        private bool isMNTExist(string MNTFolder, string ArtclXML, out string LclXMLFile)
        { 
            bool Rslt = false;

            LclXMLFile = "C:\\" + System.Guid.NewGuid().ToString() + ".txt";
            if (Directory.Exists(MNTFolder))
            {
                if (File.Exists(ArtclXML))
                {
                    File.Copy(ArtclXML, LclXMLFile);
                    ProcessEventHandler(ArtclXML + "  has been copied to " + LclXMLFile) ;
                    Rslt = true;
                }
                else
                {
                    ProcessEventHandler(ArtclXML + "  does not exist for stripins.");
                    
                }
            }
            else
            {
                ProcessEventHandler(MNTFolder + "  does not exist for stripins.");
            }

            return Rslt;
        }
        private bool isMathExist(string LclXMLFile)
        {
            bool Rslt = false;
            if (File.Exists(LclXMLFile))
            {
                if (File.ReadAllText(LclXMLFile).IndexOf("<math") != -1)
                    Rslt = true;
                else
                    Rslt =false;
            }
            return Rslt;
        }
        private bool WritePagXML(string PagXML, string EqnFolder)
        {
            bool Rslt = false;
            string str = "<pagination><stage>S280</stage><vol>9999</vol><issue>9999</issue><startP>xx</startP><endP>xx</endP></pagination>";

            try
            {
                File.WriteAllText(PagXML, str);
                Rslt = true;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            
            return Rslt;
        }
        private bool isPNGexist(string EqnFolder)
        {
            bool Rslt = false;
            if (Directory.Exists(EqnFolder))
            {
                string[] PNGFiles = Directory.GetFiles(EqnFolder, "*.png");
                if (PNGFiles.Length > 0)
                {
                    Rslt = false;
                }
            }
            return Rslt;
        }

        private bool isPing(string ServerIP)
        {


            ServerIP = ServerIP.Replace("activemq", "").Replace("tcp", "").Replace("61616", "").Replace(":", "").Replace("/", "");

            bool Result = false;

            if (ServerIP == "59.160.102.180")
            {

                return true;
            }


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
    }
}
