using System.Data.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using DatabaseLayer;
namespace LWWeProof
{
    class DBProcess
    {
        static DBProcess _DBProcessObj;
        static string    _ConStr = string.Empty;
        LWWDataClassesDataContext DataContext = null;
        static OPSDB _OPSDBObj = null;
        DBProcess(string ConStr)
        {
            _ConStr = ConStr;
            DataContext = new LWWDataClassesDataContext(_ConStr);
        }
        public static string  DBConStr
        {
            get { return _ConStr; }
            set {  _ConStr= value; }
        }
        public static DBProcess DBProcessObj
        {
            get
            {
                if (_DBProcessObj == null)
                {
                    _DBProcessObj = new DBProcess(_ConStr);

                    string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                    _OPSDBObj = new OPSDB(_OPSConStr);
                }
                return _DBProcessObj;
            }
        }


        public ISingleResult<usp_GetAIDDetailsResult> usp_GetAIDDetailsResult(string AID, string Stage)
        {
            return DataContext.usp_GetAIDDetails(AID, Stage);
        }

        public bool isDatasetUpload(string JID, string AID, string Stage, string taskName)
        {
            bool isEproofed = false;
             int? RNO = DataContext.usp_GetDatasetHistoryRNO(JID, AID, Stage);
             
             if (RNO == null || RNO.Value==0)
                 isEproofed = false;
             else
                 isEproofed = true;

             return isEproofed;
        }

        public void insertDatasetHistory(string JID, string AID,string Stage)
        {
            DataContext.usp_InsertDatasetHistory(JID, AID, Stage);
        }

        public void UpdateMessageStatus(int MsgID, int WIPRno, int Pages, string status)
        {
            DataContext.usp_UpdateMessageStatus(MsgID, WIPRno,Pages, status);
            
        }

        public void InsertMessageDetail(string JID, string AID, string Stage)
        {
            string IP= GetIP();
            DataContext.usp_InsertMessageDetail("LWW", JID, AID, Stage, IP);

        }
        private string GetIP()
        {
            string strHostName = string.Empty;

            strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            return addr[0].ToString();
        }

        public List<MessageDetail> GetInprocessMessageDetail()
        {
            List<MessageDetail> MsgList = new List<MessageDetail>();
            var MsgDtlList = DataContext.usp_GetInprocessMessageDetail();

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

        public OPSDetail GetOPSDtl(string Client, string JID)
        {
            OPSDetail _OPSDetailObj = _OPSDBObj.GetOPSDetails(JID,Client);
            return _OPSDetailObj;
        }
    }
}
