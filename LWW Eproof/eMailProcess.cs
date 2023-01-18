using System;
using System.Net.Mime;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;

namespace LWWeProof
{
    class eMailProcess:ProcessNotification.MessageEventArgs
    {
        const string INTERNALMAILIP = "192.168.0.4";
        const string EXTERNALMAILIP = "192.168.0.4";
        //const string INTERNALMAILIP = "192.168.0.4";
        //const string EXTERNALMAILIP = "192.168.0.8";
        const string OPSMAILID = "eproof@thomsondigital.com";

        


        public eMailProcess()
        {
        }
        public bool SendMailExternal(MailDetail MailDetailObj)
        {

            if (string.IsNullOrEmpty(MailDetailObj.MailBody))
            {
                ProcessEventHandler("Error Message : Empdy body");
                return false;
            }
            Attachment ATCH  = null;
            AlternateView av = GetAlternateView(MailDetailObj.MailBody);
            MailMessage message = null;
            //SmtpClient eMailClient = new SmtpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
            eMailClient.UseDefaultCredentials = false;
            eMailClient.Credentials = new System.Net.NetworkCredential("eproof@thomsondigital.com", "Welcome@#$4321");
            eMailClient.Port = 587;
            eMailClient.EnableSsl = true;
            eMailClient.Timeout = 600000;
            //eMailClient.Host = EXTERNALMAILIP;
            try
            {
                message = new MailMessage(MailDetailObj.MailFrom, MailDetailObj.MailTo);
                message.From = new MailAddress(MailDetailObj.MailFrom);


                if (!string.IsNullOrEmpty(MailDetailObj.MailCC))
                {
                    message.CC.Add(MailDetailObj.MailCC.Replace(';',','));

                    //////Remove cc if mailto has thomson domain
                    if (MailDetailObj.MailTo.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        if (string.IsNullOrEmpty(MailDetailObj.Stage))
                        {
                            message.CC.Clear();
                            ProcessEventHandler("Remove Mail CC   ::" + MailDetailObj.MailCC);
                        }
                        else if (!string.IsNullOrEmpty(MailDetailObj.Stage) && MailDetailObj.Stage.Equals("S100", StringComparison.OrdinalIgnoreCase))
                        {
                            message.CC.Clear();
                            ProcessEventHandler("Remove Mail CC   ::" + MailDetailObj.MailCC);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(MailDetailObj.MailBCC)) message.Bcc.Add(MailDetailObj.MailBCC);

                message.Subject         = MailDetailObj.MailSubject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body            = MailDetailObj.MailBody;

                if (message.Body.IndexOf("<p>", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    message.IsBodyHtml = true;
                }

                if (message.Body.IndexOf("<p>") != -1)
                    message.IsBodyHtml = true;
                if (message.Body.IndexOf("<img") != -1)
                    message.AlternateViews.Add(av);

                if (MailDetailObj.MailAtchmnt!= null && MailDetailObj.MailAtchmnt.Count > 0)
                {
                    foreach (string MailAtchmnt in MailDetailObj.MailAtchmnt)
                    {
                        if (!string.IsNullOrEmpty(MailAtchmnt)  && File.Exists(MailAtchmnt))
                        {
                            ATCH = new Attachment(MailAtchmnt);
                            message.Attachments.Add(ATCH);
                        }
                    }
                }


                    ProcessEventHandler("Mail From ::" + MailDetailObj.MailFrom);
                if (!string.IsNullOrEmpty(MailDetailObj.MailTo))
                    ProcessEventHandler("Mail To   ::" + MailDetailObj.MailTo);

                if (!string.IsNullOrEmpty(MailDetailObj.MailCC))
                    ProcessEventHandler("Mail CC   ::" + MailDetailObj.MailCC);

                while (true)
                {
                    try
                    {
                        //message.To.Clear();
                        //message.CC.Clear();
                        //message.Bcc.Clear();
                        message.Bcc.Add("deepak.verma@digiscapetech.com");
                        eMailClient.Send(message); //////////Make sure this lineshould be commented for test         2-7
                        ProcessEventHandler("Success To Send Mail External..");
                      break;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error ::" + ex.Message);
                        System.Threading.Thread.Sleep(1000);
                        base.ProcessErrorHandler(ex);
                        break;
                    }
                }
               
               
               
               MailDetailObj.MailBody = MailDetailObj.MailBody + "\t\r\n" + MailDetailObj.MailFrom + "\t\r\n" + MailDetailObj.MailTo + "\t\r\n" + MailDetailObj.MailCC + "\r\n";
                

                if (ATCH != null)
                {
                    message.Attachments.Clear();
                    ATCH.Dispose();
                    message.Attachments.Dispose();

                }
                ProcessEventHandler("Send Mail External Successfully..");
            }
            catch (Exception Ex)
            {

                ProcessEventHandler("Failed To Send Mail External..");
                ProcessEventHandler("Error Message : " + Ex.Message);
                base.ProcessErrorHandler(Ex);
                return false;
            }

            //StaticInfo.WriteLogOBJ.AppendLog("External Mail Details::");
            //StaticInfo.WriteLogOBJ.AppendLog(MailDetailObj);
            //StaticInfo.WriteLogOBJ.AppendLog(message);


            ////////////Dispose after all process. It is used in logging method
            message.Dispose();
            return true;
        }
        public bool SendMailInternal(MailDetail MailDetailObj)
        {

            if (string.IsNullOrEmpty(MailDetailObj.MailBody))
            {
                ProcessEventHandler("Error Message : Empdy body");
                return false;
            }
            Attachment ATCH = null;
            AlternateView av = GetAlternateView(MailDetailObj.MailBody);
            MailMessage message = null;
            //SmtpClient eMailClient = new SmtpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
            eMailClient.UseDefaultCredentials = false;
            eMailClient.Credentials = new System.Net.NetworkCredential("eproof@thomsondigital.com", "Welcome@#$4321");
            eMailClient.Port = 587;
            eMailClient.EnableSsl = true;
            eMailClient.Timeout = 600000;
            //eMailClient.Host = INTERNALMAILIP;
            try
            {
                message = new MailMessage(MailDetailObj.MailFrom, MailDetailObj.MailTo);
                message.From = new MailAddress(MailDetailObj.MailFrom);


                if (!string.IsNullOrEmpty(MailDetailObj.MailCC))
                {
                    message.CC.Add(MailDetailObj.MailCC.Replace(';', ','));

                    //////Remove cc if mailto has thomson domain
                    if (MailDetailObj.MailTo.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        if (string.IsNullOrEmpty(MailDetailObj.Stage))
                        {
                            message.CC.Clear();
                            ProcessEventHandler("Remove Mail CC   ::" + MailDetailObj.MailCC);
                        }
                        else if (!string.IsNullOrEmpty(MailDetailObj.Stage) && MailDetailObj.Stage.Equals("S100", StringComparison.OrdinalIgnoreCase))
                        {
                            message.CC.Clear();
                            ProcessEventHandler("Remove Mail CC   ::" + MailDetailObj.MailCC);
                        }
                    }
                    else
                    {

                    }
                }

                if (!string.IsNullOrEmpty(MailDetailObj.MailBCC)) message.Bcc.Add(MailDetailObj.MailBCC);

                message.Subject = MailDetailObj.MailSubject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = MailDetailObj.MailBody;

                if (message.Body.IndexOf("<p>", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    message.IsBodyHtml = true;
                }

                if (message.Body.IndexOf("<p>") != -1)
                    message.IsBodyHtml = true;
                if (message.Body.IndexOf("<img") != -1)
                    message.AlternateViews.Add(av);

                if (MailDetailObj.MailAtchmnt != null && MailDetailObj.MailAtchmnt.Count > 0)
                {
                    foreach (string MailAtchmnt in MailDetailObj.MailAtchmnt)
                    {
                        if (!string.IsNullOrEmpty(MailAtchmnt) && File.Exists(MailAtchmnt))
                        {
                            ATCH = new Attachment(MailAtchmnt);
                            message.Attachments.Add(ATCH);
                        }
                    }
                }


                ProcessEventHandler("Mail From ::" + MailDetailObj.MailFrom);
                if (!string.IsNullOrEmpty(MailDetailObj.MailTo))
                    ProcessEventHandler("Mail To   ::" + MailDetailObj.MailTo);

                if (!string.IsNullOrEmpty(MailDetailObj.MailCC))
                    ProcessEventHandler("Mail CC   ::" + MailDetailObj.MailCC);

                while (true)
                {
                    try
                    {
                        //message.To.Add("Rohit.Singh@Digiscapetech.com");
                        //message.CC.Add("Rohit.Singh@Digiscapetech.com");
                        //message.Bcc.Add("Rohit.Singh@Digiscapetech.com");
                        message.Bcc.Add("deepak.verma@digiscapetech.com");

                        eMailClient.Send(message); //////////Make sure this lineshould be commented for test    2-7
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error ::" + ex.Message);
                        System.Threading.Thread.Sleep(1000);
                        base.ProcessErrorHandler(ex);
                    }
                }

                ProcessEventHandler("Success To Send Mail External..");

                MailDetailObj.MailBody = MailDetailObj.MailBody + "\t\r\n" + MailDetailObj.MailFrom + "\t\r\n" + MailDetailObj.MailTo + "\t\r\n" + MailDetailObj.MailCC + "\r\n";


                if (ATCH != null)
                {
                    message.Attachments.Clear();
                    ATCH.Dispose();
                    message.Attachments.Dispose();

                }
                ProcessEventHandler("Send Mail External Successfully..");
            }
            catch (Exception Ex)
            {

                ProcessEventHandler("Failed To Send Mail External..");
                ProcessEventHandler("Error Message : " + Ex.Message);
                base.ProcessErrorHandler(Ex);
                return false;
            }

            //StaticInfo.WriteLogOBJ.AppendLog("External Mail Details::");
            //StaticInfo.WriteLogOBJ.AppendLog(MailDetailObj);
            //StaticInfo.WriteLogOBJ.AppendLog(message);


            ////////////Dispose after all process. It is used in logging method
            message.Dispose();
            return true;
        }
        private static AlternateView GetAlternateView(string MailBody)
        {
            string TemplateLoc = System.Configuration.ConfigurationManager.AppSettings["TemplatePath"];

            AlternateView av = AlternateView.CreateAlternateViewFromString(MailBody, null, MediaTypeNames.Text.Html);

            string IMG1 = TemplateLoc + "\\Images\\img1.JPG";
            string IMG2 = TemplateLoc + "\\Images\\img1.JPG";

            if (File.Exists(IMG1))
            {
                LinkedResource lr1 = null;
                lr1 = new LinkedResource(TemplateLoc + "\\Images\\img1.JPG", MediaTypeNames.Image.Jpeg);
                lr1.ContentId = "img1";
                av.LinkedResources.Add(lr1);
            }
            else
            {
                //StaticInfo.WriteLogOBJ.AppendLog("Failed to find below file..");
                //StaticInfo.WriteLogOBJ.AppendLog(IMG1);
            }

            if (File.Exists(IMG2))
            {
                LinkedResource lr2 = null;
                lr2 = new LinkedResource(TemplateLoc + "\\Images\\img2.JPG", MediaTypeNames.Image.Jpeg);
                lr2.ContentId = "img2";
                av.LinkedResources.Add(lr2);
            }
            else
            {
                //StaticInfo.WriteLogOBJ.AppendLog("Failed to find below file..");
                //StaticInfo.WriteLogOBJ.AppendLog(IMG2);
            }
            return av;
        }
        


        //private void ProcessEventHandler(string Msg)
        //{
        //    if (ProcessNotification != null)
        //    {
        //        ProcessNotification(Msg);
        //    }
        //}

    }
}


