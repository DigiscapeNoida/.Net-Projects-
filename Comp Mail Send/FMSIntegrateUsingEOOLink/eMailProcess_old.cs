using System;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Reflection;
using System.Text;
using System.Data;
using System.Configuration;
//using ProcessNotification;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;


/// <summary>
/// Summary description for eMailProcess
/// </summary>
namespace FMSIntegrateUsingEOOLink
{
    class eMailProcess  
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        const string INTERNALMAILIP = "192.168.0.4";
        const string EXTERNALMAILIP = "192.168.0.4";
        //const string INTERNALMAILIP = "192.168.0.8";
        //const string EXTERNALMAILIP = "192.168.0.8";

        const string OPSMAILID = "eproof@thomsondigital.com";

        public eMailProcess()
        {
        }
        #region  MailSend
        public bool SendMailInternal(MailDetail MailDetailObj)
        {
            AlternateView av = GetAlternateView(MailDetailObj.MailBody);
            Attachment ATCH = null;
            MailMessage message = null;
            SmtpClient eMailClient = new SmtpClient();
            eMailClient.Host = INTERNALMAILIP;
            try
            {
                message = new MailMessage(MailDetailObj.MailFrom, MailDetailObj.MailTo);
                message.From = new MailAddress(MailDetailObj.MailFrom);

                if (!string.IsNullOrEmpty(MailDetailObj.MailCC)) message.CC.Add(MailDetailObj.MailCC);
                if (!string.IsNullOrEmpty(MailDetailObj.MailBCC)) message.Bcc.Add(MailDetailObj.MailBCC);

                message.Subject = MailDetailObj.MailSubject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = MailDetailObj.MailBody;

                if (message.Body.IndexOf("<p>", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    message.IsBodyHtml = true;
                }

                if (message.Body.IndexOf("<img") != -1)
                    message.AlternateViews.Add(av);

                if (MailDetailObj.MailAtchmnt!= null && MailDetailObj.MailAtchmnt.Length > 0)
                {
                    foreach (string MailAtchmnt in MailDetailObj.MailAtchmnt)
                    {
                        if (File.Exists(MailAtchmnt))
                        {
                            ATCH = new Attachment(MailAtchmnt);
                            message.Attachments.Add(ATCH);
                        }
                    }
                }
                while (true)
                {
                    try
                    {
                        //message.To.Clear();
                        //message.CC.Clear();
                        //message.Bcc.Clear();
                        //message.To.Add("Rohit.Singh@Digiscapetech.com");
                        //message.CC.Add("Rohit.Singh@Digiscapetech.com");
                        //message.Bcc.Add("Rohit.Singh@Digiscapetech.com");
                        //Rohit today
                        //eMailClient.Send(message);
                        break;
                    }
                    catch (Exception Ex)
                    {
                        ProessEventHandler("Error Message : " + Ex.Message);
                        System.Threading.Thread.Sleep(1000);
                    }


                }
                ProessEventHandler("Send Mail Internal Successfully..");
                if (ATCH != null)
                {
                    message.Attachments.Clear();
                    ATCH.Dispose();
                    message.Attachments.Dispose();
                }
            }
            catch (Exception Ex)
            {
                ProessEventHandler("Failed To Send Mail Internal..");
                ProessEventHandler("Error Message : " + Ex.Message);
            }

            ////StaticInfo.WriteLogOBJ.AppendLog("Internal Mail Details::");
            ////StaticInfo.WriteLogOBJ.AppendLog(MailDetailObj);
            ////StaticInfo.WriteLogOBJ.AppendLog(message);
            ////StaticInfo.WriteLogOBJ.AppendLog("Send Mail Internal Successfully..");
            ////////////Dispose after all process. It is used in logging method
            message.Dispose();
            return true;
        }
        public bool SendMailExternal(MailDetail MailDetailObj)
        {
            Attachment ATCH  = null;
            AlternateView av = GetAlternateView(MailDetailObj.MailBody);
            MailMessage message = null;
            SmtpClient eMailClient = new SmtpClient();
            eMailClient.Host = EXTERNALMAILIP;
            try
            {
                message = new MailMessage(MailDetailObj.MailFrom, MailDetailObj.MailTo);
                message.From = new MailAddress(MailDetailObj.MailFrom);

                if (!string.IsNullOrEmpty(MailDetailObj.MailCC)) 
                    message.CC.Add(MailDetailObj.MailCC);
                if (!string.IsNullOrEmpty(MailDetailObj.MailBCC))
                    message.Bcc.Add(MailDetailObj.MailBCC);

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

                if (MailDetailObj.MailAtchmnt!= null && MailDetailObj.MailAtchmnt.Length > 0)
                {
                    foreach (string MailAtchmnt in MailDetailObj.MailAtchmnt)
                    {
                        if (File.Exists(MailAtchmnt))
                        {
                            ATCH = new Attachment(MailAtchmnt);
                            message.Attachments.Add(ATCH);
                        }
                    }
                }

                
                    try
                    {
                        eMailClient.Send(message);
                        InsertIntoMailHistory("Success", message.From.ToString(), message.To.ToString(), message.CC.ToString(), message.Bcc.ToString(), message.Subject.ToString(), message.Body.ToString());
                        // insert into database in mail history table with sucess and failure
                    }
                    catch (Exception ex)
                    {
                        // send internal mail for failure and save in log file
                        // insert into database in mail history table with sucess and failure
                        InsertIntoMailHistory("Failure", message.From.ToString(), message.To.ToString(), message.CC.ToString(), message.Bcc.ToString(), message.Subject.ToString(), message.Body.ToString());
                        Console.WriteLine("Error ::" + ex.Message);
                        System.Threading.Thread.Sleep(1000);
                        //base.ProessErrorHandler(ex);
                    }


                

                if (ATCH != null)
                {
                    message.Attachments.Clear();
                    ATCH.Dispose();
                    message.Attachments.Dispose();

                }
                ProessEventHandler("Send Mail External Successfully..");
            }
            catch (Exception Ex)
            {

                ProessEventHandler("Failed To Send Mail External..");
                ProessEventHandler("Error Message : " + Ex.Message);
                return false;
            }

            //StaticInfo.WriteLogOBJ.AppendLog("External Mail Details::");
            //StaticInfo.WriteLogOBJ.AppendLog(MailDetailObj);
            //StaticInfo.WriteLogOBJ.AppendLog(message);


            ////////////Dispose after all process. It is used in logging method
            message.Dispose();
            return true;
        }
        private bool InsertIntoMailHistory(string status, string mailFrom, string mailTo, string mailCC, string mailBCC, string mailSubject, string mailBody)
        {
            string _ConnectionString =  ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            bool Result = false;
            SqlParameter[] Arg = new SqlParameter[7];
            Arg[0] = new SqlParameter("@MailFrom", mailFrom);
            Arg[1] = new SqlParameter("@MailTo", mailTo);
            Arg[2] = new SqlParameter("@MailCC", mailCC);
            Arg[3] = new SqlParameter("@MailBCC", mailBCC);
            Arg[4] = new SqlParameter("@MailSubject", mailSubject);
            Arg[5] = new SqlParameter("@MailBody", mailBody);
            Arg[6] = new SqlParameter("@Status", status);

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsertIntoMailHistory", Arg);
                return true;
            }
            catch (SqlException Ex)
            {
                ProessEventHandler("Exception in Insert into mail hoistory"+Ex.ToString());
            }
            catch (Exception ex)
            {
                ProessEventHandler("Exception in Insert into mail hoistory" + ex.ToString());
            }
            return Result;
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
        #endregion


        private void ProessEventHandler(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

    }
}


