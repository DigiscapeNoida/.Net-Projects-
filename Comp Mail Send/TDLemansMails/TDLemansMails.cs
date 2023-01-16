using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ProcessNotification;
namespace TDLemansMails
{
    public class TDLemansMail:MessageEventArgs
    {
        string _Rcvtime  = string.Empty;
        string _MailSub  = string.Empty;
        string _MailBody = string.Empty;

        public TDLemansMail(string Rcvtime, string MailSub, string MailBody)
        {
            _Rcvtime  = Rcvtime;
            _MailSub  = MailSub;
            _MailBody = MailBody;
        }

        public bool InsertTDLemansMail()
        {
            bool Rslt = false;
            string GangDBConnectionString    = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            string AltGangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
            if (!InsertTDLemansMail(GangDBConnectionString))
            {
                if (InsertTDLemansMail(AltGangDBConnectionString))
                {
                    Rslt = true;
                }
            }
            else
            {
                    Rslt = true;
            }

            return Rslt;
        }
        private bool InsertTDLemansMail(string Constr)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@RcvTime", _Rcvtime.Trim());
                param[1] = new SqlParameter("@MailSubject", _MailSub.Trim());
                param[2] = new SqlParameter("@MailBody", _MailBody.Trim());

                SqlHelper.ExecuteNonQuery(Constr, System.Data.CommandType.StoredProcedure, "[usp_InsertTDLemansMailsInWorkAroungTime]", param);
                return true;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
        }
    }
}
