using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LWWUploadTrigger
{
    class Process
    {
        static Log log = new Log();
        public void CheckFile()
        {
            try
            {
                string S100Path = System.Configuration.ConfigurationSettings.AppSettings["S100Path"];
                string S200Path = System.Configuration.ConfigurationSettings.AppSettings["S200Path"];
                string pdfpath = System.Configuration.ConfigurationSettings.AppSettings["pdf_path"];
                string S100Success = S100Path + "/Success/";
                string S100Fail = S100Path + "/Fail/";
                string S200Success = S200Path + "/Success/";
                string S200Fail = S200Path + "/Fail/";
                if (Directory.Exists(S100Path))
                {
                    String[] files = Directory.GetFiles(S100Path, "*.*", SearchOption.TopDirectoryOnly);
                    if (files.Count() > 0)
                    {
                        log.Generatelog("============================================================");
                        log.Generatelog("Total S100 files : " + files.Length.ToString());
                        for (int i = 0; i < files.Count(); i++)
                        {
                            log.Generatelog("File found " + files[i]);
                            string aid = Path.GetFileName(files[i]);
                            if (aid.Trim().ToLower().EndsWith(".pdf"))
                            {
                                string aidname = Path.GetFileNameWithoutExtension(files[i]);
                                string _jid = CheckDatabase(aidname, "S100");
                                if (_jid != "")
                                {
                                    if (ValidJid(_jid)==true && IsFms(_jid, aidname, "S100")==true)
                                    {
                                        try
                                        {
                                            //Move file in succees folder files may be already exists
                                            if (File.Exists(S100Success + aid))
                                            {
                                                File.Delete(S100Success + aid);
                                            }
                                            if (!Directory.Exists(pdfpath + _jid))
                                            {
                                                Directory.CreateDirectory(pdfpath + _jid);
                                            }
                                            if (!Directory.Exists(pdfpath + _jid + "\\" + aidname))
                                            {
                                                Directory.CreateDirectory(pdfpath + _jid + "\\" + aidname);
                                            }
                                            File.Copy(files[i], pdfpath + _jid + "\\" + aidname + "\\" + aid, true);
                                            log.Generatelog("File copied to fms location " + pdfpath + _jid + "\\" + aidname + "\\" + aid);
                                            File.Move(files[i], S100Success + aid);
                                            InsertDatabase(aidname, "S100");
                                            log.Generatelog("Database updated for S100 : " + aid);
                                            SuccessMail("File sent successfully in upload process : " + aid, aid, "S100");
                                        }
                                        catch (Exception ex)
                                        {
                                            log.Generatelog("Error found " + ex.Message);
                                            if (File.Exists(S100Fail + aid))
                                            {
                                                File.Delete(S100Fail + aid);
                                            }
                                            File.Move(files[i], S100Fail + aid);
                                            SendMail("Error found : " + ex.Message, aid, "", "S100");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        log.Generatelog("Either JID not included or the file is already in process.");
                                        if (File.Exists(S100Fail + aid))
                                        {
                                            File.Delete(S100Fail + aid);
                                        }
                                        File.Move(files[i], S100Fail + aid);
                                        SendMail("Either JID not included or the file is already in process : " + aidname, aid, "", "S100");
                                    }
                                }
                                else
                                {
                                    //Move files in fail folder  files may be already exists
                                    if (File.Exists(S100Fail + aid))
                                    {
                                        File.Delete(S100Fail + aid);
                                    }
                                    File.Move(files[i], S100Fail + aid);
                                    log.Generatelog("File not integrated by application " + files[i]);
                                }
                            }
                            else
                            {
                                log.Generatelog("Only pdf files allowed.");
                            }
                        }
                    }
                }
                else
                {
                    log.Generatelog("Directory not found : " + S100Path);
                }
                if (Directory.Exists(S200Path))
                {
                    String[] files = Directory.GetFiles(S200Path, "*.*", SearchOption.TopDirectoryOnly);
                    log.Generatelog("Total S200 files : " + files.Length.ToString());
                    if (files.Count() > 0)
                    {
                        log.Generatelog("============================================================");
                        log.Generatelog("Total S200 files : " + files.Length.ToString());
                        for (int i = 0; i < files.Count(); i++)
                        {
                            log.Generatelog("File found " + files[i]);
                            string aid = Path.GetFileName(files[i]);
                            if (aid.Trim().ToLower().EndsWith(".pdf"))
                            {
                                string aidname = Path.GetFileNameWithoutExtension(files[i]);
                                string _jid = CheckDatabase(aidname, "S200");
                                if (_jid != "")
                                {
                                    if (ValidJid(_jid)==true && IsFms(_jid, aidname, "S200")==true)
                                    {
                                        try
                                        {
                                            //Move file in succees folder files may be already exists
                                            if (File.Exists(S200Success + aid))
                                            {
                                                File.Delete(S200Success + aid);
                                            }
                                            if (!Directory.Exists(pdfpath + _jid))
                                            {
                                                Directory.CreateDirectory(pdfpath + _jid);
                                            }
                                            if (!Directory.Exists(pdfpath + _jid + "\\" + aidname))
                                            {
                                                Directory.CreateDirectory(pdfpath + _jid + "\\" + aidname);
                                            }
                                            File.Copy(files[i], pdfpath + _jid + "\\" + aidname + "\\" + aid, true);
                                            log.Generatelog("File copied to fms location " + pdfpath + _jid + "\\" + aidname + "\\" + aid);
                                            File.Move(files[i], S200Success + aid);
                                            //insert in database
                                            InsertDatabase(aidname, "S200");
                                            log.Generatelog("Database updated for S200 : " + aid);
                                            SuccessMail("File sent successfully in upload process : " + aid, aid, "S200");
                                        }
                                        catch (Exception ex)
                                        {
                                            log.Generatelog("Error found " + ex.Message);
                                            SendMail("Error found : " + ex.Message, aid, "", "S200");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        log.Generatelog("Either JID not included or the file is already in process.");
                                        SendMail("Either JID not included or the file is already in process : " + aidname, aid, "", "S200");
                                    }
                                }
                                else
                                {
                                    //Move files in fail folder  files may be already exists
                                    if (File.Exists(S200Fail + aid))
                                    {
                                        File.Delete(S200Fail + aid);
                                    }
                                    File.Move(files[i], S200Fail + aid);
                                    log.Generatelog("File not integrated by application " + files[i]);
                                }
                            }
                            else
                            {
                                log.Generatelog("Only pdf files allowed.");
                            }
                        }
                    }
                }
                else
                {
                    log.Generatelog("Directory not found : " + S200Path);
                }
            }
            catch (Exception ex)
            {
                SendExceptionMail(ex.ToString());
            }
            //return true;
        }
        public void InsertDatabase(string aid, string stage)
        {
            string connString = System.Configuration.ConfigurationSettings.AppSettings["sqlconn"];
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlcmd = new SqlCommand("[usp_InsertMessageDetailForUpload]", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Aid", aid);
            sqlcmd.Parameters.AddWithValue("@Stage", stage);
            conn.Open();
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }
        public string CheckDatabase(string aid, string stage)
        {
            string message = "";
            string email = "";
            string jid = "";
            string connString = System.Configuration.ConfigurationSettings.AppSettings["sqlconn"];
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlcmd = new SqlCommand("[usp_CheckValidAid_new]", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Aid", aid);
            sqlcmd.Parameters.AddWithValue("@Stage", stage);
            sqlcmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
            sqlcmd.Parameters.Add("@Email", SqlDbType.Char, 500);
            sqlcmd.Parameters.Add("@jid", SqlDbType.Char, 500);
            sqlcmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            sqlcmd.Parameters["@Email"].Direction = ParameterDirection.Output;
            sqlcmd.Parameters["@jid"].Direction = ParameterDirection.Output;
            conn.Open();
            sqlcmd.ExecuteNonQuery();
            message = (string)sqlcmd.Parameters["@ERROR"].Value;
            email = (string)sqlcmd.Parameters["@Email"].Value;
            jid = (string)sqlcmd.Parameters["@jid"].Value;
            message = message.Trim();
            email = email.Trim();
            conn.Close();
            if (message.Contains("Success") && jid != "0")
            {
                return jid.Trim();
            }
            else
            {
                SendMail(message, aid, email, stage);
                return "";
            }
        }
        public void SendMail(string message, string aid, string emailid, string stage)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"]);
            mail.To.Add(System.Configuration.ConfigurationSettings.AppSettings["MailTo"]);
            mail.CC.Add(System.Configuration.ConfigurationSettings.AppSettings["MailCC"]);
            mail.CC.Add("pm_lww@thomsondigital.com");
            mail.Bcc.Add("deepak.verma@digiscapetech.com");
            mail.Subject = "Unable to upload " + aid + " (" + stage + ")";
            mail.Body = message;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
            eMailClient.UseDefaultCredentials = false;
            eMailClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"], System.Configuration.ConfigurationSettings.AppSettings["pwd"]);
            eMailClient.Port = 587;
            eMailClient.EnableSsl = true;
            eMailClient.Timeout = 600000;
            eMailClient.Send(mail);
        }
        public void SuccessMail(string message, string aid, string stage)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"]);
            mail.To.Add(System.Configuration.ConfigurationSettings.AppSettings["MailTo"]);
            mail.CC.Add(System.Configuration.ConfigurationSettings.AppSettings["MailCC"]);
            mail.CC.Add("pm_lww@thomsondigital.com");
            mail.Bcc.Add("deepak.verma@digiscapetech.com");
            mail.Subject = "File moved in uploading process " + aid + " (" + stage + ")";
            mail.Body = message;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
            eMailClient.UseDefaultCredentials = false;
            eMailClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"], System.Configuration.ConfigurationSettings.AppSettings["pwd"]);
            eMailClient.Port = 587;
            eMailClient.EnableSsl = true;
            eMailClient.Timeout = 600000;
            eMailClient.Send(mail);
        }
        public void SendExceptionMail(string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"]);
                mail.To.Add(System.Configuration.ConfigurationSettings.AppSettings["MailTo"]);
                mail.CC.Add(System.Configuration.ConfigurationSettings.AppSettings["MailCC"]);
                mail.CC.Add("pm_lww@thomsondigital.com");
                mail.Bcc.Add("deepak.verma@digiscapetech.com");
                mail.Subject = "Exception at upload";
                mail.Body = message;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
                eMailClient.UseDefaultCredentials = false;
                eMailClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"], System.Configuration.ConfigurationSettings.AppSettings["pwd"]);
                eMailClient.Port = 587;
                eMailClient.EnableSsl = true;
                eMailClient.Timeout = 600000;
                eMailClient.Send(mail);
            }
            catch (Exception ex)
            { }
        }
        public bool ValidJid(string jid1)
        {
            bool ret = false;
            try
            {
                string[] jids = System.Configuration.ConfigurationSettings.AppSettings["valid_jid"].Split('|');
                foreach (string pid in jids)
                {
                    if (pid.Trim().ToLower() == jid1.Trim().ToLower())
                    {
                        ret = true;
                        break;
                    }
                }
            }
            catch(Exception ex) { log.Generatelog("Error : " + ex.Message); }
            return ret;
        }
        public bool IsFms(string jd, string ad, string stg)
        {
            bool ret = false;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["sqlconn"]);
            try
            {                
                SqlCommand cmdd = new SqlCommand("usp_isfms", conn);
                cmdd.CommandType = CommandType.StoredProcedure;
                cmdd.Parameters.Clear();
                cmdd.Parameters.AddWithValue("@jid", jd);
                cmdd.Parameters.AddWithValue("@aid", ad);
                cmdd.Parameters.AddWithValue("@stage", stg);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmdd);
                if (conn.State.ToString() == "Close")
                {
                    conn.Open();
                }
                da.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                log.Generatelog("Error : " + ex.Message);
                if (conn.State.ToString()=="Open")
                {
                    conn.Close();
                }
            }
            return ret;
        }
    }
}
