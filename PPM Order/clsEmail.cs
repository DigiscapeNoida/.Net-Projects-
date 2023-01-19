using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace PPM_TRACKING_SYSTEM
{
    class clsEmail
    {

        public string subject { get; set; }
        public string fromEmail { get; set; }
        public string fromName { get; set; }
        public string messageBody { get; set; }
        public string smtpServer { get; set; }
        public NetworkCredential smtpCredentials { get; set; }

        public bool SendEmail(string toEmail, string toCC)
        {
            try
            {

                MailMessage Message = new MailMessage();
                Message.IsBodyHtml = true;

                string[] tocccc = toCC.Split(',');
                for (int i = 0; i < tocccc.Length;i++ )
                {
                    Message.CC.Add(new MailAddress(tocccc[i]));
                }
                
                Message.To.Add(new MailAddress(toEmail));
                Message.From = (new MailAddress("productiontd@thomsondigital.com"));
                Message.Subject = this.subject;
                Message.IsBodyHtml = true;
                Message.Body = this.messageBody;

                //SmtpClient sc = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sc = new SmtpClient("smtp.office365.com");
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential("productiontd@thomsondigital.com", "Wof21377");
                sc.Port = 587;
                sc.EnableSsl = true;
                sc.Timeout = 600000;
                //sc.Host = this.smtpServer;
                //Message.To.Clear();
                //Message.CC.Clear();
                //Message.Bcc.Clear();
                Message.Bcc.Add("deepak.verma@digiscapetech.com");
                sc.Send(Message);
                Console.WriteLine("Success");

            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc(this.messageBody);
                GlobalFunc.LogFunc("Error:Mail error" + ex.Message);
                Console.WriteLine("Fail");
                return false;
            }
            return true;
        }

        public delegate bool SendEmailDelegate(string toEmail, string toName);

        public void GetResultsOnCallback(IAsyncResult ar)
        {
            SendEmailDelegate del = (SendEmailDelegate)((AsyncResult)ar).AsyncDelegate;
            try
            {
                bool result = del.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                bool result = false;
            }
        }

        public bool SendEmailAsync(string toEmail, string toName)
        {
            try
            {
                SendEmailDelegate dc = new SendEmailDelegate(this.SendEmail);
                AsyncCallback cb = new AsyncCallback(this.GetResultsOnCallback);
                IAsyncResult ar = dc.BeginInvoke(toEmail, toName, cb, null);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}











    

