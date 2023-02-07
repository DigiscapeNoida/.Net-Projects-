using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;


public class Common
{

    public static int gmss = 0;
    public static int gwcount = 0;
    public static int gfig = 0;
    public static int geq = 0;
    public static int gtables = 0;
    public static int grecto = 0;
    public static int gtotalchap = 0;
    public static int gchap = 0;
    // for artlog
    public static int afig = 0;
    public static int abwfig = 0;
    public static int acolfig = 0;
    public static int abwreletter = 0;
    public static int acolreletter = 0;
    public static int aredrawing = 0;
    public static int acompset = 0;
    public static int amissfig = 0;
    public static int acomfig = 0;
    public static int asimredraw = 0;
    public static int amedredraw = 0;
    public static int acomredraw = 0;
    public static string artlogfilename = "";
    public static string castoffilename = "";

    public static string GeneratePassword()
    {
        Random rnd = new Random();
        const string Alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*=-~";
        string stest = Alphabets.Substring(rnd.Next(62, 73));
        string sTest = stest.Substring(0, 1);
        string stest1 = Alphabets.Substring(rnd.Next(44, 70));
        string sTest1 = stest1.Substring(0, 1);
        string password = rnd.Next(10, 99).ToString() + sTest1.ToString() + rnd.Next(2, 9) + sTest.ToString() +

        Alphabets.Substring(rnd.Next(26, 52)).Substring(0, 1);
        return password;
    }
    public static void SendEmail(string stTo, string stCc, string stSubject, string stBody)
    {

        SmtpClient smtpClient = new SmtpClient();
        MailMessage mMsg = new MailMessage();

        MailAddress fromAddress = new MailAddress("hassan.kaudeer@thomsondigital.com");
        smtpClient.Host = "192.168.0.4";// "192.168.0.8"; //"10.2.48.14";// ConfigurationSettings.AppSettings["MailServer"];
       // smtpClient.Port = 25;// Convert.ToInt32(ConfigurationSettings.AppSettings["smtpport"]);
        //smtpClient.Port = 26;
        mMsg.From = fromAddress;
        mMsg.To.Add(stTo);
        if (stCc != "")
        {
             if (stCc.Contains(";"))
            {
                string[] mailCC = stCc.Split(';');
                for (int i = 0; i < mailCC.Length; i++)
                {
                    if (mailCC[i].Trim() != "")
                    {
                        mMsg.CC.Add(mailCC[i]);
                    }
                }
            }
            else
            {
                mMsg.CC.Add(stCc);
            }
            
          //  mMsg.CC.Add(stCc);
        }
        mMsg.Subject = stSubject;
        mMsg.IsBodyHtml = true;
        string strBody = stBody;
        mMsg.Body = strBody;
		mMsg.Bcc.Add("hassan.kaudeer@thomsondigital.com");
        mMsg.Bcc.Add("beatrice.guildhary@thomsondigital.com");
        mMsg.CC.Add("georgette.b@thomsondigital.com");
        smtpClient.Send(mMsg);
    }
    public static void SendError(string stBody)
    {

        SmtpClient smtpClient = new SmtpClient();
        MailMessage mMsg = new MailMessage();

        MailAddress fromAddress = new MailAddress(ConfigurationSettings.AppSettings["MailFrom"]);
        smtpClient.Host = ConfigurationSettings.AppSettings["MailServer"];
        //smtpClient.Port = 26;
        mMsg.From = fromAddress;
        mMsg.To.Add("raushank@thomsondigital.com");
        mMsg.Subject = "Error in editorial template";
        mMsg.IsBodyHtml = true;
        string strBody = stBody;
        mMsg.Body = strBody;
        mMsg.CC.Add("georgette.b@thomsondigital.com");
        smtpClient.Send(mMsg);
    }
    public static void SendEmailToAdmin(string mailfrom, string cc, string stSubject, string stBody, List<string> attachments)
    {

        SmtpClient smtpClient = new SmtpClient();
        MailMessage mMsg = new MailMessage();
        MailAddress fromAddress = new MailAddress(ConfigurationSettings.AppSettings["MailFrom"]);
        string[] mailTo = ConfigurationSettings.AppSettings["MailToCastoff"].Split(';');
        mMsg.From = fromAddress;
        for (int i = 0; i < mailTo.Length; i++)
        {
            if (mailTo[i].Trim() != "")
            {
                mMsg.To.Add(mailTo[i]);
            }
        }
        mMsg.Subject = stSubject;
        Attachment objAttach;
        for (int i = 0; i < attachments.Count; i++)
        {
            objAttach = new Attachment(attachments[i]);
            mMsg.Attachments.Add(objAttach);
        }

        mMsg.IsBodyHtml = true;
        string strBody = stBody;
        mMsg.Body = strBody;
        smtpClient.Host = ConfigurationSettings.AppSettings["MailServer"];
        smtpClient.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["smtpport"]);
        mMsg.Bcc.Add("beatrice.guildhary@thomsondigital.com");
        mMsg.CC.Add("georgette.b@thomsondigital.com");
        smtpClient.Send(mMsg);
    }

    public static void FlashMessage(System.Web.UI.Page obj, string mesg, string sMessageId)
    {
        string sScript = "<script language=JavaScript>" + "alert('" + mesg + "')" + "</script>";
        if (!obj.IsStartupScriptRegistered(sMessageId))
        {
            obj.RegisterStartupScript(sMessageId, sScript);

        }
    }

    public static DateTime GetDayLightTime()
    {
        DateTime dt = DateTime.Now;

        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

        bool result = tzi.IsDaylightSavingTime(DateTime.Now);
        if (result)
        {
            dt = dt.AddMinutes(-210);
        }
        else
        {
            dt = dt.AddMinutes(-270);
        }
        return dt;
    }

    // send mail aeps
    /*
    public void SendMail(string strEmailFrom, string strEmailTo, string strMsg, string strSubject, string strCC, string AttMent, string UserType, string bcc)
    {
        try
        {
            MailMessage mailMsg = new MailMessage();
            MailAddress mailAddress = null;
            mailAddress = new MailAddress(strEmailFrom);
            mailMsg.To.Add(strEmailTo);
            mailMsg.CC.Add(strCC);
            mailMsg.Bcc.Add(bcc);
            mailMsg.From = mailAddress;
            mailMsg.Subject = strSubject;
            mailMsg.Body = strMsg;
            mailMsg.Body = mailMsg.Body;

            Attachment objattch1 = new Attachment(@"C:\AEPS_Service\eAnnotation of PDF Proofs.pdf");
            mailMsg.Attachments.Add(objattch1);
            if (UserType == "Author")
            {
                Attachment objattch2 = new Attachment(@"C:\AEPS_Service\MRW_proofing_instructions_for_contributors.doc");
                mailMsg.Attachments.Add(objattch2);
            }
            else
            {
                Attachment objattch3 = new Attachment(@"C:\AEPS_Service\MRW_proofing_instructions_for_editors.doc");
                mailMsg.Attachments.Add(objattch3);
            }
            Attachment objattch = new Attachment(AttMent);
            mailMsg.Attachments.Add(objattch);

            mailMsg.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("10.2.48.14", Convert.ToInt32(25));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            credentials.UserName = "";
            credentials.Password = "";
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMsg);
        }
        catch (Exception ex)
        {
            // throw new System.sm("Logfile cannot be read-only");
            MailMessage mailMsg = new MailMessage();
            MailAddress mailAddress = null;
            mailAddress = new MailAddress(strEmailFrom);
            mailMsg.To.Add(strEmailTo);
            //   mailMsg.CC.Add(strCC);
            //   mailMsg.Bcc.Add(bcc);
            mailMsg.From = mailAddress;
            mailMsg.Subject = "Failure Notice: " + strSubject;
            mailMsg.Body = strMsg;
            mailMsg.Body = mailMsg.Body;

            Attachment objattch1 = new Attachment(@"C:\AEPS_Service\eAnnotation of PDF Proofs.pdf");
            mailMsg.Attachments.Add(objattch1);
            if (UserType == "Author")
            {
                Attachment objattch2 = new Attachment(@"C:\AEPS_Service\MRW_proofing_instructions_for_contributors.doc");
                mailMsg.Attachments.Add(objattch2);
            }
            else
            {
                Attachment objattch3 = new Attachment(@"C:\AEPS_Service\MRW_proofing_instructions_for_editors.doc");
                mailMsg.Attachments.Add(objattch3);
            }
            Attachment objattch = new Attachment(AttMent);
            mailMsg.Attachments.Add(objattch);

            mailMsg.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("10.2.48.14", Convert.ToInt32(25));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            credentials.UserName = "";
            credentials.Password = "";
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMsg);
        }


    }
    */

}
