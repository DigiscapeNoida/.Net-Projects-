using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ProcessNotification;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace LWWAutoIntegrate
{
    class GoXMLList : MessageEventArgs
    {
        List<String> _GoFileList = new List<string>();
        string _WIPConnection = string.Empty;
        public List<String> GoFileList
        {
            get { return _GoFileList; }
        }
        public GoXMLList()
        {
            _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
        }
        public void AssignDownloadGoXMLList()
        {
            GetGoXMLList("[usp_GetLWWDownloadMaterial]");
        }
        public void AssignIntegrateArticleGoXMLList()
        {
            GetGoXMLList("[usp_GetLWWIntegrateArticle]");
        }
        public void GetGoXMLList(string spName)
        {
            ProcessEventHandler("Getting article list for process..");

            string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            try
            {
                _GoFileList.Clear();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_WIPConnection, System.Data.CommandType.StoredProcedure,spName);

                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        if (Dr["goxmlpath"] != null && Dr["goxmlpath"] != DBNull.Value)
                        {
                            _GoFileList.Add(Dr["goxmlpath"].ToString());

                            ProcessEventHandler( "goxmlpath :" + _GoFileList[_GoFileList.Count-1]);
                        }
                    }
                }
                Dr.Close();
                Dr.Dispose();
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }

            if (_GoFileList.Count == 0)
            {
                ProcessEventHandler("No artilce to be process ");
            }
        }

        public bool UpdateArticleDetails(GoXmlInfo GoXml)
        {
            if (UpdateDatabase(GoXml))
            {
                if (UpdateStatus(GoXml))
                {
                    UpdateGoMetaXML(GoXml);
                    return true;
                }
            }
            return false;
        }

        private bool UpdateStatus(GoXmlInfo GoXml)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@serverip", GoXml.ServerIP);
            param[1] = new SqlParameter("@guid", GoXml.Guid);

            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWIntegrationStatus]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }

        private bool UpdateGoMetaXML(GoXmlInfo GoXml)
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@Goxml", GoXml.GoXML);
            param[1] = new SqlParameter("@MetaDataXml", GoXml.MetaDataXML);
            param[2] = new SqlParameter("@guid", GoXml.Guid);
            param[3] = new SqlParameter("@SubmissionXML", string.IsNullOrEmpty( GoXml.SubmissionXML)?"":GoXml.SubmissionXML);
            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWDownloadGoXMl]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }
        private bool UpdateDatabase(GoXmlInfo GoXml)
        {
            try
            {
             Type OBJ = GoXml.GetType();
            PropertyInfo[] Properties = OBJ.GetProperties();
            foreach (PropertyInfo P in Properties)
            {
                if (P.GetValue(GoXml, null) == null)
                {
                    if (P.CanWrite)
                        P.SetValue(GoXml, "", null);
                }
                
            }

            SqlParameter[] param = new SqlParameter[13];

            param[0] = new SqlParameter("@Guid", GoXml.Guid);
            param[1] = new SqlParameter("@Aid", GoXml.AID);
            param[2] = new SqlParameter("@TaskName", GoXml.TaskName);
            param[3] = new SqlParameter("@Mss", GoXml.MSS);
            param[4] = new SqlParameter("@FigCount", GoXml.FigCount);
            param[5] = new SqlParameter("@Doi", GoXml.DOI);
            param[6] = new SqlParameter("@Jid", GoXml.JID);
            param[7] = new SqlParameter("@Stage", GoXml.FMSStage);
            param[8] = new SqlParameter("@Vol", GoXml.VOL);
            param[9] = new SqlParameter("@Iss", GoXml.Issue);

            if (string.IsNullOrEmpty(GoXml.Remarks))
            {
                GoXml.Remarks = "";
            }

            param[10] = new SqlParameter("@remarks", GoXml.Remarks);
            param[11] = new SqlParameter("@GoXmlPath", GoXml.GoXMLPath);
            param[12] = new SqlParameter("@DUEDATE", GoXml.DueDate);
          
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWDownload]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }

            // Check for duplicate

            DataSet ds = new DataSet();
            using (SqlConnection sqlcon = new SqlConnection(_WIPConnection))
            {

                SqlCommand sqlcmd = new SqlCommand("[CheckDuplicateTask]", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@JID", SqlDbType.VarChar).Value = GoXml.JID;
                sqlcmd.Parameters.Add("@AID", SqlDbType.VarChar).Value = GoXml.AID;
                sqlcmd.Parameters.Add("@TaskName", SqlDbType.VarChar).Value = GoXml.TaskName;
                SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);
                sqlad.Fill(ds);
            }

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 1)
                { 
                    // Send Mail To Internal team 
                        string mailto = "";
                    string region = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["Region"].ToString()) ? "pm_lww@thomsondigital.com,pm_lwwus@thomsondigital.com" : ds.Tables[0].Rows[0]["Region"].ToString();
                    if (region == "UK")
                        mailto = "pm_lww@thomsondigital.com";
                    else if (region == "US")
                        mailto = "pm_lwwus@thomsondigital.com";
                    else {
                        mailto = "pm_lww@thomsondigital.com,pm_lwwus@thomsondigital.com";
                    }
                    string mailcc = ConfigurationSettings.AppSettings["MailCC"];
                    string subaject = "Task Repeted for "+GoXml.JID+" "+GoXml.AID + "";
                    string mailBody = "Dear PM,</br></br> This task is received again. </br></br>Regards, </br>Thomson Team";
                    SendMail(mailto, mailcc, subaject, mailBody);
                }
            }

            return true;
           
        }

        public void SendMail(String MailTo, String MailCC, String subject, String mailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("eproof@thomsondigital.com");
                mail.To.Add(MailTo);
                if (MailCC != String.Empty)
                    mail.CC.Add(MailCC);
                mail.Bcc.Add("Rohit.singh@digiscapetech.com");
                mail.Subject = subject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
                eMailClient.UseDefaultCredentials = false;
                eMailClient.Credentials = new System.Net.NetworkCredential("eproof@thomsondigital.com", "Welcome@#$4321");
                eMailClient.Port = 587;
                eMailClient.EnableSsl = true;
                eMailClient.Timeout = 600000;
                //smtp.Host = "192.168.0.4";
                eMailClient.Send(mail);

            }
            catch (Exception e)
            {

            }
        }


     
    }
}
