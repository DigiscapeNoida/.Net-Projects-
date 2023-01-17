using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Read_mail
{
    class Program
    {
        static Log log = new Log();
        static void Main(string[] args)
        {
            while (true)
            {
                log.Generatelog("==========================Start reading mails==========================");
                Microsoft.Office.Interop.Outlook.Application Application = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.Accounts accounts = Application.Session.Accounts;
                Microsoft.Office.Interop.Outlook.Folder selectedFolder = Application.Session.DefaultStore.GetRootFolder() as Microsoft.Office.Interop.Outlook.Folder;
                selectedFolder = GetFolder(@"\\" + "afsana.khan@thomsondigital.com");
                if (selectedFolder == null)
                {
                    log.Generatelog("Mail ID not found.");
                    continue;
                }
                Microsoft.Office.Interop.Outlook.Folders childFolders = selectedFolder.Folders;
                string download_path = "C:\\temp2\\mail";
                string zippath = "C:\\temp2\\file_for_zip";
                string backup_path = "E:\\journal\\";
                string target_path = "\\\\172.16.30.3\\stylesheet\\Elsevier\\Journal\\";
                try
                {
                    log.Generatelog("Deleting folder " + download_path);
                    Directory.Delete(download_path, true);
                    Thread.Sleep(1000);
                    Directory.CreateDirectory(download_path);
                    log.Generatelog("Folder re-created " + download_path);
                }
                catch (System.Exception ex)
                {
                    log.Generatelog("Error found " + ex.Message);
                    continue;
                }
                if (childFolders.Count > 0)
                {
                    try
                    {
                        foreach (Microsoft.Office.Interop.Outlook.Folder childFolder in childFolders)
                        {
                            if (childFolder.FolderPath.Contains("Inbox"))
                            {
                                log.Generatelog("Inbox folder found.");
                                foreach (MAPIFolder folder in childFolder.Folders)
                                {
                                    if (folder.Name.Contains("Stylesheet"))
                                    {
                                        log.Generatelog("Stylesheet folder found.");
                                        var fi = folder.Items;
                                        if (fi != null)
                                        {
                                            foreach (Object item in fi)
                                            {
                                                try
                                                {
                                                    Microsoft.Office.Interop.Outlook.MailItem mi = (Microsoft.Office.Interop.Outlook.MailItem)item;
                                                    if (mi.UnRead == true)
                                                    {
                                                        DateTime dtt = mi.SentOn;
                                                        string frm = mi.SenderEmailAddress;
                                                        string sub = mi.Subject;
                                                        string mail_body = mi.Body.ToString();
                                                        if (download(sub.Split(' ')[1], dtt))
                                                        {
                                                            log.Generatelog("Downloading " + sub.Split(' ')[1] + " " + dtt.ToString());
                                                            foreach (Attachment at in mi.Attachments)
                                                            {
                                                                if (at.FileName.Trim().ToLower().EndsWith("-jss.xml"))
                                                                {
                                                                    string fname = at.FileName.Substring(4);
                                                                    string jid = at.FileName.Replace("-jss.xml", "").Substring(4);
                                                                    if (jid.Length < 8)
                                                                    {
                                                                        log.Generatelog("Downloading file " + fname);
                                                                        try
                                                                        {
                                                                            if (File.Exists(download_path + "\\" + fname))
                                                                            {
                                                                                File.Delete(download_path + "\\" + fname);
                                                                            }
                                                                            at.SaveAsFile(download_path + "\\" + fname);
                                                                        }
                                                                        catch (System.Exception ex)
                                                                        {
                                                                            log.Generatelog("Error found " + ex.Message);
                                                                        }
                                                                        try
                                                                        {
                                                                            log.Generatelog("Creating backup " + fname);
                                                                            if (!Directory.Exists(backup_path + jid))
                                                                            {
                                                                                Directory.CreateDirectory(backup_path + jid);
                                                                            }
                                                                            if (File.Exists(backup_path + jid + "\\" + fname))
                                                                            {
                                                                                string c_path = backup_path + jid + "\\" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString();
                                                                                Directory.CreateDirectory(c_path);
                                                                                string[] fls = Directory.GetFiles(backup_path + jid, "*.*", SearchOption.TopDirectoryOnly);
                                                                                foreach (string f in fls)
                                                                                {
                                                                                    File.Copy(f, c_path + "\\" + Path.GetFileName(f), true);
                                                                                    File.Delete(f);
                                                                                }
                                                                            }
                                                                            at.SaveAsFile(backup_path + jid + "\\" + fname);
                                                                        }
                                                                        catch (System.Exception ex)
                                                                        {
                                                                            log.Errorlog("Error found " + ex.Message);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            foreach (Attachment at in mi.Attachments)
                                                            {
                                                                if (at.FileName.Trim().ToLower().EndsWith("-diff.html"))
                                                                {
                                                                    string fname1 = at.FileName.Substring(4);

                                                                    string jid = at.FileName.Replace("-diff.html", "").Substring(4);
                                                                    if (jid.Length < 8)
                                                                    {
                                                                        log.Generatelog("Downloading file " + fname1);
                                                                        try
                                                                        {
                                                                            if (!Directory.Exists(backup_path + jid))
                                                                            {
                                                                                Directory.CreateDirectory(backup_path + jid);
                                                                            }
                                                                            if (File.Exists(backup_path + jid + "\\" + fname1))
                                                                            {
                                                                                File.Delete(backup_path + jid + "\\" + fname1);
                                                                            }
                                                                            at.SaveAsFile(backup_path + jid + "\\" + fname1);
                                                                        }
                                                                        catch (System.Exception ex)
                                                                        {
                                                                            log.Generatelog("Error found " + ex.Message);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        mi.UnRead = false;
                                                    }
                                                }
                                                catch
                                                {
                                                    continue;
                                                }
                                            }
                                            try
                                            {
                                                log.Generatelog("Moving files to ZIP location");
                                                string[] fls1 = Directory.GetFiles(download_path);
                                                foreach (string f in fls1)
                                                {
                                                    File.Copy(f, zippath + "\\" + Path.GetFileName(f), true);
                                                }
                                            }
                                            catch (System.Exception ex)
                                            {
                                                log.Generatelog("Error found in moving file " + ex.Message);
                                            }
                                            String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
                                            SqlConnection con = new SqlConnection(str);
                                            try
                                            {
                                                log.Generatelog("Updating Database.");
                                                string[] fls2 = Directory.GetFiles(zippath);                                              
                                                con.Open();
                                                foreach (string fl in fls2)
                                                {
                                                    try
                                                    {
                                                        string file_name = Path.GetFileName(fl);
                                                        if (file_name.Trim().ToLower().EndsWith("-jss.xml"))
                                                        {
                                                            string jid = file_name.Split('-')[0];
                                                            XmlNodeList nodeList = null;
                                                            XmlDocument myXmlDocument = new XmlDocument();
                                                            myXmlDocument.Load(fl);
                                                            string JTitle = "";
                                                            string Prdctsite = "";
                                                            nodeList = myXmlDocument.GetElementsByTagName("journalTitle");
                                                            if (nodeList.Count > 0)
                                                            {
                                                                JTitle = Regex.Match(nodeList[0].InnerXml, @"(<!\[CDATA\[)(.*?)(]]>)").Groups[2].Value;
                                                            }

                                                            nodeList = myXmlDocument.GetElementsByTagName("productionSite");
                                                            if (nodeList.Count > 0)
                                                            {
                                                                Prdctsite = nodeList[0].InnerXml;
                                                            }
                                                            //if (!Directory.Exists(target_path + jid))
                                                            //{
                                                            //    Directory.CreateDirectory(target_path + jid.ToUpper());
                                                            //}
                                                            SqlDataAdapter da = new SqlDataAdapter("select * from elsstylesheet where jid='" + jid + "'", con);
                                                            DataTable dt = new DataTable();
                                                            da.Fill(dt);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                SqlCommand cmd = new SqlCommand("UPDATEJID", con);
                                                                cmd.CommandType = CommandType.StoredProcedure;
                                                                cmd.Parameters.Clear();
                                                                cmd.Parameters.AddWithValue("@JID", jid);
                                                                cmd.Parameters.AddWithValue("@JOURNALTITLE", JTitle);
                                                                cmd.Parameters.AddWithValue("@PRODUCTIONSITE", Prdctsite);
                                                                cmd.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                SqlDataAdapter da1 = new SqlDataAdapter("select max(SNO) from elsstylesheet", con);
                                                                DataTable dt1 = new DataTable();
                                                                da1.Fill(dt1);
                                                                int sno = Convert.ToInt32(dt1.Rows[0][0]);
                                                                sno = sno + 1;
                                                                log.Generatelog("Inserting jid : " + jid);
                                                                SqlCommand cmd1 = new SqlCommand("ADDJID", con);
                                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                                cmd1.Parameters.Clear();
                                                                cmd1.Parameters.AddWithValue("@SNO", sno);
                                                                cmd1.Parameters.AddWithValue("@JID", jid);
                                                                cmd1.Parameters.AddWithValue("@JOURNALTITLE", JTitle);
                                                                cmd1.Parameters.AddWithValue("@PRODUCTIONSITE", Prdctsite);
                                                                cmd1.Parameters.AddWithValue("@CUSTOMER", "Elsevier");
                                                                cmd1.ExecuteNonQuery();
                                                                log.Generatelog("New JID added : " + jid);
                                                            }
                                                            //File.Copy(fl, target_path + jid + "\\" + file_name, true);
                                                        }
                                                    }
                                                    catch (System.Exception ex)
                                                    {
                                                        log.Generatelog(ex.Message);
                                                        continue;
                                                    }
                                                    //File.Copy(fl, zippath + "\\" + Path.GetFileName(f), true);
                                                }
                                                con.Close();
                                                log.Generatelog("Database updated.");
                                                //if (File.Exists("C:\\temp2\\jss\\jss.zip"))
                                                //{
                                                //    File.Delete("C:\\temp2\\jss\\jss.zip");
                                                //    Thread.Sleep(1000);
                                                //}
                                                //ZipFile.CreateFromDirectory(zippath, "C:\\temp2\\jss\\jss.zip");
                                            }
                                            catch (System.Exception ex)
                                            {
                                                log.Generatelog("Error found " + ex.Message);
                                                if (con.State.ToString() == "Open")
                                                {
                                                    con.Close();
                                                }
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //    log.Generatelog("Stylesheet folder not found.");
                                    //}
                                }
                            }
                            //else
                            //{
                            //    log.Generatelog("Inbox not found.");
                            //}
                        }
                    }
                    catch (System.Exception ex)
                    {
                        log.Generatelog("Error found " + ex.Message);
                    }
                }
                log.Generatelog("Waiting for 20 min.");
                Thread.Sleep(1200000);
            }
        }
        static bool download(string jid, DateTime dt)
        {
            string found = "0";
            bool ret = false;
            try
            {
                string[] lines = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\jid.txt");
                foreach (string ln in lines)
                {
                    if (ln.Contains(jid))
                    {
                        found = "1";
                        DateTime dt1 = DateTime.Parse(ln.Split('|')[1].ToString());
                        if (dt > dt1)
                        {
                            string txt = File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\jid.txt");
                            txt = txt.Replace(ln, jid + "|" + dt.ToString());
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\jid.txt");
                            File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\jid.txt", txt);
                            ret = true;
                        }
                        break;
                    }
                }
                if (found == "0")
                {
                    File.AppendAllText(System.Windows.Forms.Application.StartupPath + "\\jid.txt", jid + "|" + dt.ToString() + Environment.NewLine);
                    ret = true;
                }
            }
            catch (System.Exception ex)
            {
                log.Generatelog("Error found " + ex.Message);
            }
            return ret;
        }
        static Outlook.Folder GetFolder(string folderPath)
        {
            Console.WriteLine("Looking for: " + folderPath);
            Outlook.Folder folder;
            string backslash = @"\";
            try
            {
                if (folderPath.StartsWith(@"\\"))
                {
                    folderPath = folderPath.Remove(0, 2);
                }
                String[] folders = folderPath.Split(backslash.ToCharArray());
                Outlook.Application Application = new Outlook.Application();
                folder = Application.Session.Folders[folders[0]] as Outlook.Folder;
                if (folder != null)
                {
                    for (int i = 1; i <= folders.GetUpperBound(0); i++)
                    {
                        Outlook.Folders subFolders = folder.Folders;
                        folder = subFolders[folders[i]] as Outlook.Folder;
                        if (folder == null)
                        {
                            return null;
                        }
                    }
                }
                return folder;
            }
            catch (System.Exception ex)
            {
                log.Generatelog("Error found " + ex.Message);
                return null;
            }
        }
    }

}
