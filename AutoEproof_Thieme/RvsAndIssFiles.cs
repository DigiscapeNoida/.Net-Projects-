using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ProcessNotification;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using MsgRcvr;
using DatabaseLayer;

namespace AutoEproof
{
    class RvsAndIssFiles : MessageEventArgs
    {
        bool _IsSuccess = false;
        public bool IsSuccess
        {
            get { return _IsSuccess; }
        }

        List<MNTInfo> MNTList = new List<MNTInfo>();
        List<MessageDetail> _RvsFlsMsgList = null;
        List<MessageDetail> _IssFlsMsgList = null;

        string _OPSConStr = string.Empty;
        OPSDB _OPSDBObj = null;

        private bool GetMsgList()
        {
            ProcessEventHandler("GetMsgList called to get Rvs And Iss Files");

            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(_OPSConStr))
            {
                ProcessEventHandler("OPS database Connection String is not set");
                _IsSuccess = false;
            }


            ProcessEventHandler("OPS Database initializion process start");
            try
            {
                _OPSDBObj = new OPSDB(_OPSConStr);
                ProcessEventHandler("OPS Database initialize successfully..");
                ProcessEventHandler("Getting InProcess Message Detail");

                _RvsFlsMsgList = _OPSDBObj.GetReviseFilesMessageDetail();
                _IssFlsMsgList = _OPSDBObj.GetIssueFilesMessageDetail();

            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return _IsSuccess;
        }

        public void StartProces()
        {
            GetMsgList();

            List<MessageDetail> S200List = _RvsFlsMsgList.FindAll(x => x.Stage.Contains("200"));
            foreach (MessageDetail MsgDtl in S200List)
            {
                try
                {
                    int CommentsCount = ReviewerCommentsCount(MsgDtl.JID, MsgDtl.AID);

                    if (CommentsCount > 0)
                    {
                        SqlParameter[] param = new SqlParameter[5];

                        param[0] = new SqlParameter("@Clnt", MsgDtl.Client);
                        param[1] = new SqlParameter("@JID", MsgDtl.JID);
                        param[2] = new SqlParameter("@AID", MsgDtl.AID);
                        param[3] = new SqlParameter("@Stage", MsgDtl.Stage);
                        param[4] = new SqlParameter("@ACount", CommentsCount);

                        ProcessEventHandler("JID   +  AID : " + MsgDtl.JID + "\t" + MsgDtl.AID);
                        ProcessEventHandler("CommentsCount: " + CommentsCount);
                        SqlHelper.ExecuteNonQuery(_OPSConStr, System.Data.CommandType.StoredProcedure, "[usp_UpdateAnnotationCount]", param);
                    }

                }
                catch (SqlException ex)
                {
                    ProcessErrorHandler(ex);
                }
            }


            foreach (MessageDetail MsgDtl in _IssFlsMsgList)
            {
                string CopyTo = ConfigDetails.Issue + "\\" + MsgDtl.Stage;
                if (!Directory.Exists(CopyTo))
                {
                    Directory.CreateDirectory(CopyTo);
                    ProcessEventHandler("CreateDirectory : " + CopyTo);
                }
                IssueMsgPrcs(MsgDtl, CopyTo);
            }



            foreach (MessageDetail MsgDtl in _RvsFlsMsgList)
            {
                InsertMsgDtl(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID);
            }

            foreach (MessageDetail MsgDtl in _RvsFlsMsgList)
            {
                string CopyTo = string.Empty;

                if (MsgDtl.Client.Equals("JWUSA") && MsgDtl.Stage.Contains("200"))
                {
                    _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                    ProcessEventHandler("UpdateRvsIssMessageDetal : " + MsgDtl.MsgID);
                    continue;
                }
                //else if (!MsgDtl.Client.Equals("JWUSA") && !MsgDtl.Stage.Contains("200"))
                //{
                //    ProcessEventHandler("UpdateRvsIssMessageDetal : " + MsgDtl.MsgID);
                //    continue;
                //}


                if (MsgDtl.Stage.Contains("200"))
                    CopyTo = ConfigDetails.S200InPut;
                else if (MsgDtl.Stage.Contains("275"))
                    CopyTo = ConfigDetails.S275InPut;
                else if (MsgDtl.Stage.Contains("280"))
                    CopyTo = ConfigDetails.S280InPut;

                MsgPrcs(MsgDtl, CopyTo);

            }


        }
        private bool InsertMsgDtl(string Client, string JID, string AID)
        {

            ProcessEventHandler("RemoveJunkGr JID AID" + JID + AID);

            bool Rslt = false;
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@Client", Client);
            para[1] = new SqlParameter("@JID", JID);
            para[2] = new SqlParameter("@AID", AID);
            para[3] = new SqlParameter("@Stage", "RemoveJunkGr");


            try
            {
                SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsertMsgDtl", para);
                ProcessEventHandler("Successfully inserted RemoveJunkGr message's detail in database");
                Rslt = true;
            }

            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                Rslt = false;
            }
            if (Rslt == false)
            {
                try
                {
                    GangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
                    SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsertMsgDtl", para);
                    ProcessEventHandler("Successfully inserted RemoveJunkGr message's detail in database");
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            return true;
        }
        public int ReviewerCommentsCount(string JID, string AID)
        {
            try
            {
                int CommentsCount = 0;
                string SrchPath = ConfigDetails.OPSXMLLoc;
                string[] ReviewDirs = Directory.GetDirectories(SrchPath, JID + "*" + AID + "*");

                if (ReviewDirs.Length == 0)
                {
                    SrchPath = ConfigDetails.AltrntvOPSXMLLoc;
                    ReviewDirs = Directory.GetDirectories(SrchPath, JID + "*" + AID + "*");
                }

                if (string.IsNullOrEmpty(SrchPath) || !Directory.Exists(SrchPath))
                {
                    return CommentsCount;
                }
                string ReviewDir = string.Empty;
                int Max = 0;
                foreach (string RvwDir in ReviewDirs)
                {
                    if (Directory.GetFiles(RvwDir, "*.xml").Length > Max)
                    {
                        ReviewDir = RvwDir;
                        Max = Directory.GetFiles(RvwDir, "*.xml").Length;
                    }
                }

                if (string.IsNullOrEmpty(ReviewDir))
                {
                    return CommentsCount;
                }

                //D:20150112231615

                string deadline = "\n<m:deadline>" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + (DateTime.Now.Day - 1).ToString().PadLeft(2, '0') + "000000</m:deadline>";

                string TDXMLFile = string.Empty;
                string[] XMLFiles = Directory.GetFiles(ReviewDir, "*.xml");

                foreach (string XMLFile in XMLFiles)
                {
                    StringBuilder XMLStr = new StringBuilder(File.ReadAllText(XMLFile));
                    XMLStr.Replace("&", "#$#");
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.XmlResolver = null;
                    xDoc.PreserveWhitespace = false;
                    try
                    {
                        xDoc.LoadXml(XMLStr.ToString().Replace("<m:", "<").Replace("</m:", "</"));

                    }
                    catch (XmlException ex)
                    {
                        ProcessErrorHandler(ex);
                        continue;
                    }
                    XmlNodeList NL = xDoc.GetElementsByTagName("item");
                    XmlNodeList AdminNL = xDoc.GetElementsByTagName("isAdmin");
                    if (AdminNL != null && AdminNL.Count > 0)
                        if (AdminNL[0].InnerText.Equals("true"))
                        {
                            TDXMLFile = XMLFile;
                            CommentsCount = NL.Count;

                            CommentsCount = CommentsCount - Regex.Matches(xDoc.InnerXml, "StrikeOutTextEdit").Count;


                            //XmlNodeList TitleNL = xDoc.GetElementsByTagName("title");

                            ////foreach (XmlNode Title in TitleNL)
                            //for(int i=0; i<TitleNL.Count;i++)
                            //{
                            //    XmlNode Title = TitleNL[i];
                            //    XmlNode NextTitle = null;
                            //    if (i<TitleNL.Count)
                            //        NextTitle = TitleNL[i + 1];

                            //    if (Title != null && NextTitle != null)
                            //    {
                            //        if (Title.InnerText.StartsWith("StrikeOut") && NextTitle.InnerText.StartsWith("Caret"))
                            //        {
                            //            CommentsCount--;
                            //        }
                            //    }
                            //}
                            xDoc = null;
                            break;
                        }
                }

                if (!string.IsNullOrEmpty(TDXMLFile))
                {
                    StringBuilder TDXMLFileStr = new StringBuilder(File.ReadAllText(TDXMLFile));

                    if (TDXMLFileStr.ToString().IndexOf("m:deadline") == -1)
                    {
                        TDXMLFileStr.Replace("</m:corporation>", "</m:corporation>" + deadline);
                        File.WriteAllText(TDXMLFile, TDXMLFileStr.ToString());
                    }
                }

                return CommentsCount;
            }
            catch (Exception)
            {
                return 0;
            }
            //<item><title>StrikeOut Annotation
            //<item><title>Caret Annotation
        }
        private bool MsgPrcs(MessageDetail MsgDtl, string CopyTo)
        {
            bool Result = false;
            string FMSFolder = string.Empty;
            string TempFolder = string.Empty;
            MNTInfo MNT = new MNTInfo(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage);
            //if (MsgDtl.IP.Contains(ConfigDetails.TDXPSGangtokIP))

            if (MsgDtl.IP.Contains(ConfigDetails.TDXPSNoidaIP))
            {
                FMSFolder = ConfigDetails.TDXPSNoidaFMSFolder;
                TempFolder = ConfigDetails.TDXPSNoidaTempFolder;
            }
            else
            {
                string[] IP1 = ConfigDetails.TDXPSGangtokIP.Split('.');
                string[] IP2 = MsgDtl.IP.Split(new char[] { '.', ':' });
                if (IP1.Length > 1 && IP2.Length > 1)
                {
                    if (IP1[3].Equals(IP2[3]))
                    {
                        FMSFolder = ConfigDetails.TDXPSGangtokFMSFolder;
                        TempFolder = ConfigDetails.TDXPSGangtokTempFolder;
                    }
                }

            }

            if (String.IsNullOrEmpty(FMSFolder))
            {
                _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                return true;
            }


            try
            {
                GetPDFXML GetPDFXMLoBJ = new GetPDFXML(MNT.Client, MNT.JID, MNT.AID, FMSFolder, TempFolder);
                GetPDFXMLoBJ.ProcessNotification += ProcessEventHandler;
                GetPDFXMLoBJ.ErrorNotification += ProcessErrorHandler;

                if (CopyTo.Contains("280"))
                {
                    if (GetPDFXMLoBJ.StartProcessToGetS280Files(CopyTo))
                        _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                }
                else
                {
                    if (GetPDFXMLoBJ.StartProcessToGetRvsFiles(CopyTo))
                        _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                }

                ProcessEventHandler("UpdateRvs_S280_IssMessageDetal : " + MsgDtl.MsgID);
            }
            catch (Exception ex)
            {
            }
            return Result;
        }


        private void IssueMsgPrcs(MessageDetail MsgDtl, string CopyTo)
        {
            if (MsgDtl.IP == null)
            {
                ProcessEventHandler("MsgDtl.IP  is NULL so No process further.");
                return;
            }

            string FMSFolder = string.Empty;
            string TempFolder = string.Empty;
            MNTInfo MNT = new MNTInfo(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage);
            if (MsgDtl.IP.Contains(ConfigDetails.TDXPSNoidaIP))
            {
                FMSFolder = ConfigDetails.TDXPSNoidaFMSFolder;
                TempFolder = ConfigDetails.TDXPSNoidaTempFolder;
            }
            else
            {
                string[] IP1 = ConfigDetails.TDXPSGangtokIP.Split('.');
                string[] IP2 = MsgDtl.IP.Split(new char[] { '.', ':' });
                if (IP1.Length > 1 && IP2.Length > 1)
                {
                    if (IP1[3].Equals(IP2[3]))
                    {
                        FMSFolder = ConfigDetails.TDXPSGangtokFMSFolder;
                        TempFolder = ConfigDetails.TDXPSGangtokTempFolder;
                    }
                }
            }

            if (String.IsNullOrEmpty(FMSFolder))
            {
                _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                return;
            }

            ProcessEventHandler("FMSFolder : " + FMSFolder);
            ProcessEventHandler("TempFolder : " + TempFolder);

            GetPDFXML GetPDFXMLoBJ = new GetPDFXML(MNT.Client, MNT.JID, MNT.AID, FMSFolder, TempFolder);
            GetPDFXMLoBJ.ProcessNotification += ProcessEventHandler;
            GetPDFXMLoBJ.ErrorNotification += ProcessErrorHandler;

            if (GetPDFXMLoBJ.StartProcessToGetIssFiles(CopyTo))
            {
                _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                ProcessEventHandler("UpdateRvsIssMessageDetal : " + MsgDtl.MsgID);
            }
        }
    }
}
