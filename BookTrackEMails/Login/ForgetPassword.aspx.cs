using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Xml;
using System.Net.Mail;
using System.Net;

public partial class Login_ForgetPassword : System.Web.UI.Page
{
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    string sql = null;
    string username = null;
    string usermail = null;
    string pawwd;
    string mail;
    string Host_mail_ip;


    GlbClasses objGlbCls = new GlbClasses();
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string usermail = txtmailid.Text;
       
        //string username = txtuserid.Text;

        if (usermail != null)
        {
            ckeckuserbymail();
        }
        //if (username != null && (usermail == null || usermail.Trim().Length == 0))
        //{
        //    ckeckuserbyusername();
        //}
        //if ((usermail == null || usermail.Trim().Length == 0) && (username == null || username.Trim().Length == 0))
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "All fields are Requried";
        //}
        //if (usermail != "" && username!="" )
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Please enter only one entry";
        //}
    }
    public void ckeckuserbymail()
    {
        string usermail = txtmailid.Text;
        sql = "select userid, password, email_id from login where email_id='" + usermail + "'";
        SendMail();
    }
    //public void ckeckuserbyusername()
    //{
    //    string userid= txtuserid.Text;
    //    sql = "select password, email_id from login where email_id='" + userid + "'";
    //    SendMail();

    //}
    public string ReadData()
    {
        string uid="";
        string pawwd = "";
        string email = "";
        string concat = "";
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            cmd = new SqlCommand(sql, con);
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                uid=Dr.GetString(0);
                pawwd = Dr.GetString(1);
                email = Dr.GetString(2);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Your are not Registered User.";
        }
        finally
        {
            if (Dr == null)
            {
                lblmsg.Text = "Please Enter Correct Email ID.";
               
            }
            if (con != null)
            {
                con.Close();
            }
        }
        concat = uid+";"+pawwd +";"+ email;
        return concat;
    }
    public void SendMail()
    {
        try
        {
            string pwd_email = ReadData();
            string[] arr = pwd_email.Split(';');
            string userid=arr[0];
            string pawwd = arr[1];
            string email = arr[2];
            string from = "balram.a@thomsondigital.com";
            string to = email;
            // string smtpip = "192.168.0.4";
            //Host_mail_ip = System.Configuration.ConfigurationSettings.AppSettings["mailip"]; 
            //string smtpip = Host_mail_ip;
            //SmtpClient client = new SmtpClient(smtpip, 25);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient client = new SmtpClient("103.35.121.108");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("thomson", "Express@2008##");
            client.Port = 25;
            //mailClient.EnableSsl = true;
            client.Timeout = 600000;
            MailMessage message = new MailMessage(from, to);
            string Mailpath = Server.MapPath("~/App_Data/ChangePassword.htm");
            StreamReader sr = new StreamReader(Mailpath);
            string FileCon = sr.ReadToEnd();
            sr.Close();
            FileCon = FileCon.Replace("[Userid]",userid);
            FileCon = FileCon.Replace("[Password]",pawwd);
            message.Body=FileCon;
            message.IsBodyHtml=true;
            //message.Body = "Your Password is'" + pawwd + "'.This is auto generated mail.NO Reply";
            message.Subject = "PASSWORD";
            client.Send(message);
            lblmsg.Visible = true;
            lblmsg.Text = "Your password has been successfully sent your mail-id.";
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Mail id not found";
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Page.Dispose();
    }
}
