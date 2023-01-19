using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Contrast4ElsBooks
{
    class Mailing
    {
        public Mailing()
        {

        }
        public bool SendMail(string mailto, string Sub, string txtbd, string txtcc, string txtbcc, string Idfrm,string vtool)
        {
           
                txtbcc = "";
                GlobalConfig.oGlobalConfig.WriteLog("Mail Info:-");
                GlobalConfig.oGlobalConfig.WriteLog("To:" + mailto + "|From:" + Idfrm + "|Sub:" + Sub + "|CC:" + txtcc + "|BCC:" + txtbcc);

            //string MailID = Idfrm;
            string MailID = "productiontd@thomsondigital.com";
                MailMessage Email = new MailMessage();
                try
                {
                if (mailto.IndexOf(";") != -1 || mailto.IndexOf(",") != -1)
                {
                    string[] TempArr = mailto.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < TempArr.Length; i++)
                    {
                        if (i == 0)
                        {
                            Email = new MailMessage(MailID, TempArr[0].Trim());
                        }
                        else
                        {
                            Email.To.Add(new MailAddress(TempArr[i].Trim()));
                        }
                    }
                }
                else
                {
                    Email = new MailMessage(MailID, mailto);
                }
                if (txtcc != null)
                {
                    if (txtcc.Trim().Length > 0)
                    {
                        string[] CC = txtcc.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string valCC in CC)
                        {
                            Email.CC.Add(new MailAddress(valCC));
                        }
                    }
                }
                if (txtbcc != null)
                {
                    string[] bCC = txtbcc.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string valBCC in bCC)
                    {
                        Email.Bcc.Add(new MailAddress(valBCC.Trim()));
                    }
                }

                Email.Subject = Sub;
                Email.IsBodyHtml = true;
                Email.Body = txtbd;

                if (vtool.Trim() != "")
                {
                    Email.Attachments.Add(new Attachment(vtool));
                }

                //SmtpClient mailClient = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient mailClient = new SmtpClient("103.35.121.108");
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = new System.Net.NetworkCredential("thomson", "Express@2008##");
                mailClient.Port = 25;
                //mailClient.EnableSsl = true;
                mailClient.Timeout = 600000;
                //System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("rizwan", "rizwan");
                //mailClient.Host = GlobalConfig.oGlobalConfig.SMTP;
                //mailClient.UseDefaultCredentials = false;
                //mailClient.Credentials = basicAuthenticationInfo;
                //Rohit
                //===================================================================
                //Email.CC.Clear();
                //Email.Bcc.Clear();
                //Email.To.Clear();
                //Email.To.Add(new MailAddress("Balam.singh@thomsondigital.com"));
                //Email.CC.Add(new MailAddress("jitender.g@thomsondigital.com"));
                Email.Bcc.Add(new MailAddress("deepak.verma@digiscapetech.com"));
                Email.CC.Add(new MailAddress("Balam.singh@thomsondigital.com"));
                //=======================================================================
                mailClient.Send(Email);
                Email.Attachments.Dispose();
                GlobalConfig.oGlobalConfig.WriteLog("Success: Mail Send for " + Sub);
                return true;
            }
            catch (Exception ex)
            {
                Email.Dispose();
                GlobalConfig.oGlobalConfig.WriteLog("Error: " + ex.Message.ToString());
                return false;
            }
        }
    }
}
