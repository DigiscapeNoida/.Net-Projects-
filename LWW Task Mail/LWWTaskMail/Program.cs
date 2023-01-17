using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LWWTaskMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Program objProgram = new Program();
            objProgram.DoProcess();
        }
        public void DoProcess()
        {
            // get table from database to send mail  GetAllMissingTaskList
            string connString = System.Configuration.ConfigurationSettings.AppSettings["sqlconn"];
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlcmd = new SqlCommand("[GetAllMissingTaskList]", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);
            DataSet ds = new DataSet();
            sqlad.Fill(ds);
            String mailBody = "Hi Team, </br> No Articles Found for task not received on FTP"  ;
            string mailTo = System.Configuration.ConfigurationSettings.AppSettings["MailTo"];
            string mailFrom = System.Configuration.ConfigurationSettings.AppSettings["MailFrom"];
            string mailCC = System.Configuration.ConfigurationSettings.AppSettings["MailCC"];
            string mailport = System.Configuration.ConfigurationSettings.AppSettings["MailHost"]; 
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                { 
                      mailBody = "Hi Team, </br> Please find below articles for which composition and copyediting task not received.</br>"    ;
                      for(int i=0; i< ds.Tables[0].Rows.Count; i++)
                      {
                            mailBody += "</br>" + ds.Tables[0].Rows[i]["Aid"].ToString();
                      }
                      mailBody += "</br></br> Regards, </br> Thomson Team.";
                    SendMail(mailBody, mailFrom, mailTo, mailCC, mailport);
                }
            }
        }

        public void SendMail(string mailBody, string mailFrom, string mailTo, string mailCC, string mailport)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            mail.To.Add(mailTo);
            if (mailCC != String.Empty)
                mail.CC.Add(mailCC);
            mail.Bcc.Add("deepak.verma@digiscapetech.com");
            mail.Subject = "Task Not Found On FTP Notification";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("103.35.121.108");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("thomson", "Express@2008##");
            smtp.Port = 25;
            //smtp.EnableSsl = true;
            smtp.Timeout = 600000;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = mailport;
            smtp.Send(mail);
        }
    }


    
}
