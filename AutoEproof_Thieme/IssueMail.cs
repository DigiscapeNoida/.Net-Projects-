using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;
using ProcessNotification;

namespace AutoEproof
{
    class IssueMail:MessageEventArgs
    {
        static List<MessageDetail> JIDAIDList = new List<MessageDetail>();
        
        string _OPSConStr = string.Empty;
        OPSDB _OPSDBObj = null;
        OPSDetail _OPSDetailObj = null;
        string GangDBConnectionString = string.Empty;
        public IssueMail()
        {
            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj = new OPSDB(_OPSConStr);
        }

        public void StartProcess()
        {
            GetInprocessMessage();
            foreach (MessageDetail Msg in JIDAIDList)
            {
                if (Msg.Stage.Equals("Issue", StringComparison.OrdinalIgnoreCase))
                {
                    SendMail(Msg);
                    UpdateMessageStatus(Msg.MsgID);
                }
            }
        }
        private void GetInprocessMessage()
        {
            bool Rslt = false;
             GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            DataSet DS = new DataSet();
            try
            {
                DS = SqlHelper.ExecuteDataset(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_GetInprocessWileyProcess");
                Rslt = true;
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }

            if (Rslt == false)
            {
                try
                {
                    GangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
                    DS = SqlHelper.ExecuteDataset(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_GetInprocessWileyProcess");
                    Rslt = true;
                }
                catch (SqlException ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            if (Rslt == false)
            {
                return;
            }

            DataTable DT = DS.Tables[0];
            DT.TableName = "WileyProcess";

            foreach (DataRow DR in DT.Rows)
            {
                
                MessageDetail WP = new MessageDetail();
                WP.MsgID = Int32.Parse(DR["MsgID"].ToString());
                WP.Client = DR["Client"].ToString().Trim().Trim(new char[] { '\t' });
                WP.JID = DR["JID"].ToString();
                WP.AID = DR["AID"].ToString();
                WP.Stage = DR["STAGE"].ToString();
                WP.Status = DR["Status"].ToString();
                JIDAIDList.Add(WP);
            }
        }
        private bool SendMail(MessageDetail Msg)
        {

           _OPSDetailObj = _OPSDBObj.GetOPSDetails(Msg.JID, Msg.Client);
            bool Result = false;
            ProcessEventHandler("GetMailBody Start..");

            StringBuilder MailB = new StringBuilder();

            MailB.AppendLine("Dear PM,");

            MailB.AppendLine(" ");
            MailB.AppendLine("Get the merged issue pdf from below location");

            string JID_AID = @"\\10.10.23.107\temp\issue\" + Msg.JID + "_" + Msg.AID;
            MailB.AppendLine(JID_AID );
            
            
            MailB.AppendLine(" ");
            MailB.AppendLine("Thanks,");
            MailB.AppendLine("eProof System");

           string  _MailBody = MailB.ToString();

            ProcessEventHandler("GetMailBody Finish..");

            try
            {
              
                        ProcessEventHandler("Process start to send mamil");
                        MailDetail MailDetailOBJ = new MailDetail();
                        MailDetailOBJ.MailFrom = "eproof@thomsondigital.com";
                        MailDetailOBJ.MailTo = _OPSDetailObj.FailEmail;
                        MailDetailOBJ.MailBCC = "wileyproof@wiley.thomsondigital.com";
                        MailDetailOBJ.MailSubject = "Issue pdf of " + Msg.JID + " (" + Msg.AID + ")";
                        MailDetailOBJ.MailBody = _MailBody;

                        eMailProcess eMailProcessOBJ = new eMailProcess();
                        eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                        eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;


                        if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                        {
                            Result = true;
                        }

            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }
            return Result;

        }

         private void UpdateMessageStatus(int MsgID)
        {
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@MsgID", MsgID);


            bool Rslt = false;
            GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            DataSet DS = new DataSet();
            try
            {
                SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_UpdateWileyProcessStatus", para);
                Rslt = true;
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }

            if (Rslt == false)
            {
                try
                {
                    GangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
                    SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_UpdateWileyProcessStatus", para);
                    Rslt = true;
                }
                catch (SqlException ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            if (Rslt == false)
            {
                return;
            }

           


        }
    }

    
}
