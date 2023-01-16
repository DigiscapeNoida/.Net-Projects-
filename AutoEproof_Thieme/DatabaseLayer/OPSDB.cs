using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq;
using System.Linq;
using System.Data;
using ProcessNotification;

namespace DatabaseLayer
{
    public class OPSDB:MessageEventArgs
    {
        string connString="";

        //PdfProcess PdfProcessOBJ = null;
        AEPSJWDataContext DataContextOBJ;
        OPSDetail _OPSDetail = new OPSDetail();
        public OPSDB(string ConnectionString)
        {
            connString = ConnectionString;
            DataContextOBJ = new AEPSJWDataContext(connString);
        }

        public OPSDB()
        {
            //connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            DataContextOBJ = new AEPSJWDataContext(connString);
        }

        //public OPSDB(PdfProcess PdfProcessOBJ_):this()
        //{
        //    PdfProcessOBJ = PdfProcessOBJ_;
        //    PdfProcessOBJ.JID = PdfProcessOBJ.JID.Trim();
        //    _OPSDetail = GetOPSDetails(PdfProcessOBJ.JID, PdfProcessOBJ.Client);
            

        //}
        public OPSDetail GetOPSDetails(string JID, string Client)
        {
            var matches    = from   OPSDtl in DataContextOBJ.GetTable<OPSDetail>()
                             where  OPSDtl.Jid==JID && OPSDtl.Client == Client
                             select OPSDtl;
            return matches.FirstOrDefault();
        }

        public OPSDetail GetOPSDetails(string JID)
        {
            var matches = from OPSDtl in DataContextOBJ.GetTable<OPSDetail>()
                          where OPSDtl.Jid == JID && OPSDtl.Client.ToUpper( )!= "THIEME"
                          select OPSDtl;
            return matches.FirstOrDefault();
        }
        public ArticleContentReport GetACRDetails(string JID, string AID)
        {
            var matches = from ACRDtl in DataContextOBJ.GetTable<ArticleContentReport>()
                          where ACRDtl.AID ==JID+AID
                          select ACRDtl;
            return matches.FirstOrDefault();
        }
        public OPSDetail GetOPSDetails(int OPSID)
         {
            var matches = from OPSDtl in DataContextOBJ.GetTable<OPSDetail>()
                          where OPSDtl.OPSID == OPSID
                          select OPSDtl;

            return matches.FirstOrDefault();
        }

        public void CheckeProofExistence(int OPSID, string AID, out int eProofCount)
        {
            eProofCount = 0;
            var Rslt = DataContextOBJ.usp_GeteProofHistory(OPSID, AID);
            foreach (usp_GeteProofHistoryResult Msg in Rslt)
            {
                eProofCount++;
            }
        }

        public bool CheckeProofExistence(int OPSID, string AID)
        {
            bool isEproofed = false;

            var Rslt = DataContextOBJ.usp_GeteProofHistory(OPSID, AID);
            foreach (usp_GeteProofHistoryResult Msg in Rslt)
            {
                isEproofed=true;
            }
            if (isEproofed == false)
            {
                OPSDetail OD = GetOPSDetails(OPSID);
                int? RVWID = DataContextOBJ.CheckReViewExistence(OD.Jid, AID);
                if (RVWID == null)
                    isEproofed = false;
                else
                    isEproofed = true;
            }

            return isEproofed;
        }
        public int CheckReViewExistence(string JID , string AID)
        {
            int RID = 0;
            int? RVWID;
            RVWID=DataContextOBJ.CheckReViewExistence(JID, AID);

            if (RVWID == null)
                RID = 0;
            else
                RID = RVWID.Value;

            if ("ETC#IEAM".IndexOf(JID)!=-1)
            {
            }
            else if (RID == 0)
            {
                OPSDetail OD = GetOPSDetails(JID);
                var Rslt = DataContextOBJ.usp_GeteProofHistory(OD.OPSID, AID);
                foreach (usp_GeteProofHistoryResult Msg in Rslt)
                {
                    RID = 1111;
                }
            }

            return RID;
        }
        public int CheckACRJIDExistence(string JID)
        {
            int? RVWID;
            RVWID = DataContextOBJ.ToCheckArticleContentReport (JID);

            if (RVWID == null)
                return 0;
            else
                return RVWID.Value;

        }
        public string GetAuthorEmailFromArticleContentReport(string JID, string AID)
        {
            string ArticleContentReport;
            ArticleContentReport = DataContextOBJ.GetAuthorEmailFromArticleContentReport(JID, AID);
            return ArticleContentReport;

        }
        public void InsertReviseHistory(usp_GetReviseHistoryResult RvsHstryObj)
        {
            DataContextOBJ.usp_InsertReviseHistory(RvsHstryObj.OPSID, RvsHstryObj.AID, RvsHstryObj.MailFrom, RvsHstryObj.MailTo, RvsHstryObj.MailCC, RvsHstryObj.MailBCC, RvsHstryObj.CorrName, RvsHstryObj.RevisionType);
        }
        public ISingleResult<usp_GetReviseHistoryResult> GetReviseHistory(int OPSID, string AID)
        {
            return DataContextOBJ.usp_GetReviseHistory(OPSID, AID);
        }

        public ISingleResult<usp_GetReviseHistoryStageWiseResult> GetReviseHistory(int OPSID, string AID, string Stage)
        {
            return DataContextOBJ.usp_GetReviseHistoryStageWise(OPSID, AID,Stage);
        }

        public usp_GetJQAHistoryResult GetJQAHistory(string JID, string AID, string Vol, string Iss, string Stage)
        {
           usp_GetJQAHistoryResult JQAHistoryResult = new usp_GetJQAHistoryResult();
           var  JQAHistoryObj = DataContextOBJ.usp_GetJQAHistory(JID, AID,Stage, Vol, Iss);
           foreach (usp_GetJQAHistoryResult x in JQAHistoryObj)
           {
               JQAHistoryResult.AID = x.AID;
               JQAHistoryResult.Client = x.Client;
               JQAHistoryResult.IP = x.IP;
               JQAHistoryResult.ISSUE = x.ISSUE;
               JQAHistoryResult.JID = x.JID;
               JQAHistoryResult.JIDAID = x.JIDAID;
               JQAHistoryResult.JQADate = x.JQADate;
               JQAHistoryResult.JQAStatus = x.JQAStatus;
               JQAHistoryResult.Sno = x.Sno;
               JQAHistoryResult.STAGE = x.STAGE;
               JQAHistoryResult.TDProcessDate = x.TDProcessDate;
               JQAHistoryResult.TDStatus = x.TDStatus;
               JQAHistoryResult.VOL = x.VOL;
               break;
           }
           return JQAHistoryResult;
        }
        
        public int GetReviseHistoryCount(int OPSID, string AID, string Stage)
        {
            int? Count;
            Count = DataContextOBJ.GetReviseCountStageWise(OPSID, AID, Stage);

            
            if (Count == null)
                return 0;
            else
                return Count.Value;
        }
        public int GetReviseHistoryCount(int OPSID, string AID)
        {
            int? Count;
            Count= DataContextOBJ.GetReviseCount(OPSID, AID);

            if (Count == null)
                return 0;
            else
                return Count.Value;
        }
        public OPSRevise  GetReviseDetails(int OPSID)
        {
            var matches = from RvsDtl in DataContextOBJ.GetTable<OPSRevise>()
                          where RvsDtl.OPSID==OPSID
                          select RvsDtl;
            return matches.FirstOrDefault();
        }
        public OPSFAX GetFAXDetails(int OPSID)
        {
            var matches = from FAXDtl in DataContextOBJ.GetTable<OPSFAX>()
                          where FAXDtl.OPSID == OPSID
                          select FAXDtl;
            return matches.FirstOrDefault();
        }
        public void InsertMessageDetal(MessageDetail MsgDtl)
        {
            DataContextOBJ.usp_InsertMessageDetail(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage,MsgDtl.IP);
        }

        public void UpdateMessageDetal(int MsgID, int Pages)
        {
            DataContextOBJ.usp_UpdateMessageStatus(MsgID,  Pages);
        }

        public void UpdateMessageDetalJQA(int MsgID, string status)
        {
            DataContextOBJ.usp_UpdateMessageStatusJQA(MsgID, status);
        }

        public void UpdateRvsIssMessageDetal(int MsgID)
        {
            DataContextOBJ.usp_UpdateRvsIssMessageStatus(MsgID);
        }
        public void UpdateStripnsFilesMessageDetail(int MsgID)
        {
            DataContextOBJ.usp_UpdateStripnsFilesMessageDetail(MsgID);
        }

        public List<MessageDetail> GetInprocessMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList=  DataContextOBJ.usp_GetInprocessMessageDetail();

            FillMessageDetail(MsgList, MsgDtlList);
            return MsgList;
        }
        public List<MessageDetail> GetReviseFilesMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList = DataContextOBJ.usp_GetReviseFilesMessageDetail();

            FillMessageDetail(MsgList, MsgDtlList);
            return MsgList;
        }
        public List<MessageDetail> GetIssueFilesMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList = DataContextOBJ.usp_GetIssueFilesMessageDetail();

            FillMessageDetail(MsgList, MsgDtlList);
            return MsgList;
        }
        public List<MessageDetail> GetGetStripnsFilesMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList = DataContextOBJ.usp_GetStripnsFilesMessageDetail();

            FillMessageDetail(MsgList, MsgDtlList);
            return MsgList;
        }
        public List<MessageDetail> GetJQAMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList = DataContextOBJ.usp_GetJQAMessageDetail();

            FillMessageDetail(MsgList, MsgDtlList);
            return MsgList;
        }

     

        private void FillMessageDetail(List<MessageDetail> MsgList, ISingleResult<usp_GetInprocessMessageDetailResult> MsgDtlList)
        {
            
             
            foreach (usp_GetInprocessMessageDetailResult Msg in MsgDtlList)
            {

                MessageDetail MD = new MessageDetail();

                MD.MsgID = Msg.MsgID;
                MD.Client = Msg.Client;
                MD.JID = Msg.JID;
                MD.AID = Msg.AID;
                MD.Stage = Msg.Stage;
                MD.IP = Msg.IP;
                MD.Status = Msg.Status;
                MsgList.Add(MD);
               
            }
        }
        public bool isOPSJID(string JID_)
        {
            var matches = from OPSJID in DataContextOBJ.GetTable<OPSJIDLIST>()
                          where OPSJID.JID==JID_
                          select OPSJID;

            OPSJIDLIST ops= matches.FirstOrDefault();
            if (matches.FirstOrDefault()!=null)
                return true;
            else
                return false;

        }
        public OPSFtpDtl GetFtpDetails(int OPSID)
        {
            var matches = from FtpDtl in DataContextOBJ.GetTable<OPSFtpDtl>()
                          where FtpDtl.OPSID == OPSID
                          select FtpDtl;
            return matches.FirstOrDefault();
        }
        public void InsertEproofHistory(eProofHistory HstryObj )
        {
            DataContextOBJ.usp_InserteProofHistory(HstryObj.OPSID, HstryObj.AID, HstryObj.ArticleTitle, HstryObj.MailFrom, HstryObj.MailTo, HstryObj.MailCC, HstryObj.MailBCC, HstryObj.CorrName,HstryObj.DOI);
        }
        public void InsertIssFilesDetail(string JIDAID,string Stage)
        {
            DataContextOBJ.usp_InsertIssueFilesMessageDetail(JIDAID, Stage);
        }
        public void InsertToAuthorMessage(string JIDAID, string Stage)
        {
            DataContextOBJ.usp_InsertToAuthorMessageDetail(JIDAID, Stage);
        }
        public void DeleteEproofHistory(eProofHistory HstryObj)
        {
            DataContextOBJ.usp_DeleteProofHistory(HstryObj.OPSID, HstryObj.AID);
        }
        public CorAuthorDetaill GetCorAuthorDetaill(string JID, string AID)
        {
            var matches = from CorDtl in DataContextOBJ.GetTable<CorAuthorDetaill>()
                          where CorDtl.JID== JID    && CorDtl.AID==AID
                          select CorDtl;
            return matches.FirstOrDefault();
        }
        public void DeleteReviewDetails(string JID,string AID)
        {
           
            DataContextOBJ.usp_DeleteArticleReviewDetail(JID, AID);
        }
        public string GetArticleCategory(string Client, string JID, string AID)
        {

            var matches = from ArtclCatgry in DataContextOBJ.GetTable<ArticleCategory>()
                          where ArtclCatgry.Client == Client && ArtclCatgry.JID == JID && ArtclCatgry.AID == AID
                          select ArtclCatgry;

            ArticleCategory ArtlCtry = matches.FirstOrDefault();

            if (ArtlCtry != null && !string.IsNullOrEmpty(ArtlCtry.Category))
                return ArtlCtry.Category;
            else
                return string.Empty;
        }

        public void InsertJQAHistory(MessageDetail Msg, string Vol, string Iss )
        { 
            DataContextOBJ.usp_JQAStatus(Msg.JID,Msg.AID,Msg.Stage, Vol,Iss,Msg.IP);
        }

        public void UplateS280WIPWithJQAStatus(MessageDetail Msg, string Vol, string Iss)
        {
            DataContextOBJ.usp_UpdateS280WIPWithJQAStatus(Msg.JID+Msg.AID, Msg.Stage);
        }
    }
}

