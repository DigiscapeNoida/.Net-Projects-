using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Configuration;
using System.Threading;

namespace report
{
    internal class CsvDownload
    {
        static Log log = Log.GetInstance();
        private CsvDownload()
        {
        }
        private static CsvDownload obj;
        private static readonly object mylockobject = new object();
        public static CsvDownload GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new CsvDownload();
                    }
                }
            }
            return obj;
        }
        public void GetCsv()
        {
            Db db = Db.GetInstance();
            string del = db.AddUpdateData("delete from download_files");
            if (del == "1")
            {
                Outlook.Application Application1 = new Outlook.Application();
                Outlook.Accounts accounts = Application1.Session.Accounts;
                Outlook.Folder selectedFolder = Application1.Session.DefaultStore.GetRootFolder() as Outlook.Folder;
                selectedFolder = GetFolder(@"\\" + "lexisnexis@thomsondigital.com");
                string download_path = ConfigurationSettings.AppSettings["download"];
                string movepath = ConfigurationSettings.AppSettings["input"];
                if (selectedFolder != null)
                {
                    Outlook.Folders childFolders = selectedFolder.Folders;
                    if (childFolders.Count > 0)
                    {
                        try
                        {
                            foreach (Outlook.Folder childFolder in childFolders)
                            {
                                if (childFolder.FolderPath.Contains("LexisNexis"))
                                {
                                    Console.WriteLine("LexisNexis folder found.");
                                    {
                                        var fi = childFolder.Items;
                                        if (fi != null)
                                        {
                                            foreach (Object item in fi)
                                            {
                                                try
                                                {
                                                    Outlook.MailItem mi = (Outlook.MailItem)item;
                                                    if (mi.UnRead == true && mi.SenderEmailAddress == "extranet.editorial@lexisnexis.fr")
                                                    {
                                                        DateTime dtt = mi.SentOn;
                                                        string frm = mi.SenderEmailAddress;
                                                        string sub = mi.Subject;
                                                        string mail_body = mi.Body.ToString();
                                                        log.Generatelog("Downloading : " + sub + " " + dtt.ToString());
                                                        foreach (Attachment at in mi.Attachments)
                                                        {
                                                            if (at.FileName.Trim().ToLower() == "document_properties.csv")
                                                            {
                                                                string fname = "";
                                                                if (sub.Trim().ToLower().Contains("demande d’intervention"))
                                                                {
                                                                    fname = "_received.csv";
                                                                }
                                                                if (sub.Trim().ToLower().Contains("intervention terminée"))
                                                                {
                                                                    fname = "_delivered.csv";
                                                                }
                                                                if (sub.Trim().ToLower().Contains("intervention annulée"))
                                                                {
                                                                    fname = "_cancelled.csv";
                                                                }
                                                                if (sub.Trim().ToLower().Contains("intervention refusée"))
                                                                {
                                                                    fname = "_refused.csv";
                                                                }
                                                                string tm = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss.fff");
                                                                tm = tm.Replace(" ", "_").Replace(":", "_").Replace(".", "_");
                                                                tm = tm + RandomString(36);
                                                                try
                                                                {
                                                                    at.SaveAsFile(download_path + "\\" + tm + ".csv");
                                                                    Thread.Sleep(500);
                                                                    FileInfo f1 = new FileInfo(download_path + "\\" + tm + ".csv");
                                                                    string creation_time = f1.CreationTime.ToString("yyyy-MM-dd hh:mm:ss.fff");
                                                                    log.Generatelog("File downloaded : " + download_path + "\\" + tm + ".csv");
                                                                    string[] lns = File.ReadAllLines(download_path + "\\" + tm + ".csv", Encoding.UTF8);
                                                                    if (lns.Length > 2)
                                                                    {
                                                                        File.Delete(download_path + "\\" + tm + ".csv");
                                                                        Thread.Sleep(500);
                                                                        File.AppendAllText(download_path + "\\" + tm + ".csv", lns[0] + Environment.NewLine, Encoding.UTF8);
                                                                        Thread.Sleep(500);
                                                                        string ln2 = "";
                                                                        for (int i = 0; i < lns.Length; i++)
                                                                        {
                                                                            if (i == 1)
                                                                            {
                                                                                ln2 = lns[i];
                                                                            }
                                                                            if (i > 1)
                                                                            {
                                                                                ln2 = ln2 + " " + lns[i];
                                                                            }
                                                                        }
                                                                        File.AppendAllText(download_path + "\\" + tm + ".csv", ln2, Encoding.UTF8);
                                                                        Thread.Sleep(500);
                                                                        lns = null;
                                                                        lns = File.ReadAllLines(download_path + "\\" + tm + ".csv", Encoding.UTF8);
                                                                    }
                                                                    if (lns[0].Split(';')[0].Trim().ToLower() == "titre de la fiche pratique")
                                                                    {
                                                                        fname = tm + "_FP" + fname;
                                                                    }
                                                                    else if (lns[0].Split(';')[0].Trim().ToLower() == "titre  du fasc de synthèse")
                                                                    {
                                                                        fname = tm + "_syntheses" + fname;
                                                                    }
                                                                    else if (lns[0].Split(';')[0].Trim().ToLower() == "titre de la collection")
                                                                    {
                                                                        if (lns[1].Split(';')[2].Trim().ToLower() == "fiche de mise à jour")
                                                                        {
                                                                            fname = tm + "_jour" + fname;
                                                                        }
                                                                        else if (lns[1].Split(';')[2].Trim().ToLower() == "fascicule")
                                                                        {
                                                                            fname = tm + "_encyclo" + fname;
                                                                        }
                                                                    }
                                                                    File.Move(download_path + "\\" + tm + ".csv", movepath + "\\" + fname);
                                                                    Thread.Sleep(500);
                                                                    string upd = db.AddUpdateData("insert into download_files (file_name, creation_date) values ('" + movepath + "\\" + fname + "', '" + creation_time + "')");
                                                                    log.Generatelog("File moved to input location : " + movepath + "\\" + fname);
                                                                }
                                                                catch (System.Exception ex)
                                                                {
                                                                    log.Generatelog(ex.Message);
                                                                    continue;
                                                                }
                                                            }
                                                        }
                                                        mi.UnRead = false;
                                                    }
                                                }
                                                catch (System.Exception ex)
                                                {
                                                    log.Generatelog(ex.Message);
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            log.Generatelog("Error found " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                log.Generatelog("Unable to download CSV files. Database connectivity issue.");
            }
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
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
