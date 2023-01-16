using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using Domino;
using lotus;
using System.Text;
using System.Configuration;
//using ThiemeProcess;

namespace FMSIntegrateUsingEOOLink
{

    /// <summary>
    /// ACR-----------Article Content Report
    /// </summary>
    class LotusProcess
    {

        int countThieme = 1;
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        List<MailInfo> _EOOMails = new List<MailInfo>();
        //NotesSessionClass Session = null;
        NotesDatabase DataBase = null;
        NotesRichTextItem RichTextBody = null;
        NotesEmbeddedObject Attach = null;
        NotesDocumentCollection DocumentCollec = null;
        readonly string ACRDOWNLOADPATH;

        public LotusProcess()
        {
            ACRDOWNLOADPATH = ConfigDetails.ACRDownloadPath;
        }

        public List<MailInfo> EOOMails
        {
            get { return _EOOMails; }
        }
        //private bool ConnectMailServer()
        //{
        //    try
        //    {
        //        ProcessMessage("Trying to connect main server");

        //        string MailServerIP = StaticInfo.MAILSERVERDOMAIN;
        //        string NsfFileName = StaticInfo.NSFFILE;
        //        string PWD = StaticInfo.PASSWORD;

        //        Session = new NotesSessionClass();
        //        Session.Initialize(PWD);

        //        DataBase = Session.GetDatabase(MailServerIP, NsfFileName, false);
        //        //DataBase = Session.GetDatabase("", NsfFileName, false);
        //        if (!DataBase.IsOpen)
        //        {
        //            ProcessMessage("Could not connect to mail server..");
        //            return false;
        //        }
        //    }
            
        //    catch (Exception Ex)
        //    {
        //        ErrorNotification(Ex);
        //        Console.WriteLine("Database Message ==>" + Ex.Message);
        //        Console.ReadLine();
        //        return false;

        //    }
        //    return true;
        //}
        public void GettingEOOMails()
        {
            ProcessMessage("Process all mails...");
            ProcessMails();
        }
        public void GETMail()
        {
            Microsoft.Office.Interop.Outlook.Application Application = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.Accounts accounts = Application.Session.Accounts;
            Microsoft.Office.Interop.Outlook.Folder selectedFolder = Application.Session.DefaultStore.GetRootFolder() as Microsoft.Office.Interop.Outlook.Folder;
            selectedFolder = GetFolder(@"\\" + ConfigurationSettings.AppSettings["mail_id"]);
            Microsoft.Office.Interop.Outlook.Folders childFolders = selectedFolder.Folders;
            if (childFolders.Count > 0)
            {
                foreach (Microsoft.Office.Interop.Outlook.Folder childFolder in childFolders)
                {
                    if (childFolder.FolderPath.Contains("Inbox"))
                    {
                        var fi = childFolder.Items;
                        if (fi != null)
                        {
                            foreach (Object item in fi)
                            {
                                try
                                {
                                    Microsoft.Office.Interop.Outlook.MailItem mi = (Microsoft.Office.Interop.Outlook.MailItem)item;
                                    if (mi.UnRead == true)
                                    {
                                        string frm = mi.SenderEmailAddress;
                                        string sub = mi.Subject;
                                        string mail_body = mi.Body.ToString();

                                        ProcessMail(frm, sub, mail_body);

                                        mi.UnRead = false;
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }
        static Microsoft.Office.Interop.Outlook.Folder GetFolder(string folderPath)
        {
            Microsoft.Office.Interop.Outlook.Folder folder;
            string backslash = @"\";
            try
            {
                if (folderPath.StartsWith(@"\\"))
                {
                    folderPath = folderPath.Remove(0, 2);
                }
                String[] folders = folderPath.Split(backslash.ToCharArray());
                Microsoft.Office.Interop.Outlook.Application Application = new Microsoft.Office.Interop.Outlook.Application();
                folder = Application.Session.Folders[folders[0]] as Microsoft.Office.Interop.Outlook.Folder;
                if (folder != null)
                {
                    for (int i = 1; i <= folders.GetUpperBound(0); i++)
                    {
                        Microsoft.Office.Interop.Outlook.Folders subFolders = folder.Folders;
                        folder = subFolders[folders[i]] as Microsoft.Office.Interop.Outlook.Folder;
                        if (folder == null)
                        {
                            return null;
                        }
                    }
                }
                return folder;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private void ProcessMails()
        {

            // Write all mail code here

            GETMail();
            
            
            //ProcessMessage("ProcessMails");

            //int Counter = 0;
            //NotesDocument Doc = null;
            //NotesView View = null;
            //try
            //{
            //    int TotalMails = DataBase.AllDocuments.Count;

            //    ProcessMessage("TotalMails ::" + TotalMails.ToString());

            //    if (DataBase != null)
            //    {
            //        ProcessMessage("DataBase");
            //        if (DataBase.AllDocuments.Count > 0)
            //        {
            //            ProcessMessage("DataBase.GetView($All);");

            //            View = DataBase.GetView("$All");

            //            if (View != null)
            //            {
            //                ProcessMessage("View != null");
            //                Doc = View.GetFirstDocument();
            //            }
            //            else 
            //            {
            //                ProcessMessage("View == null");
            //                return;
            //            }
                       
            //            while (Doc != null )
            //            {
            //                ProcessMessage("Doc.NoteID ::  " + Doc.NoteID);

            //                Counter++;
            //                Console.WriteLine(Counter.ToString() + " out of " + TotalMails);
            //                ProcessMessage(Counter.ToString()    + " out of " + TotalMails);
            //                ProcessMessage("Process mail function has been called...");

            //                ///////////////Do'nt change sequence. Mail of article content report may be deleted. So preserve the next mail.........
            //                NotesDocument NextDocument = View.GetNextDocument(Doc);
            //                ProcessMail(Doc);

            //                Doc = NextDocument;
            //                if (Doc == null) break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Doc = null;
            //        View = null;
            //    }
            //}
            //catch (Exception NID)
            //{
            //    ErrorNotification(NID);
            //}
            //finally
            //{
            //}
            //ProcessMessage("ProcessMails :: Finished");

        }
        private bool ProcessMailSubject(string MailSubject)
        {
            String[] CheckMailSubject = null;
            if (!string.IsNullOrEmpty(StaticInfo.CheckSubject))
                CheckMailSubject = StaticInfo.CheckSubject.Split('#');
            for (int i = 0; i < CheckMailSubject.Length; i++)
            {
                if (MailSubject.IndexOf(CheckMailSubject[i], StringComparison.OrdinalIgnoreCase) != -1)
                    return true;
            }
            return false;
        }
        private void ProcessMail(string MailFrom ,  string MailSubject , string MailBody)
        {
            //NotesDocument Doc = new NotesDocument() ;
            //string MailFrom = "";
            //string MailSubject = "";
            //string MailBody = "";
            //if (Doc.IsDeleted || Doc==null)
            //{
            //    return;
            //}
            try
            {

                if (MailFrom.Contains("eproof@thomsondigital.com"))
                {
                    ProcessMessage("Mail is getting deleted if mail from eproof@thomsondigital.com");
                    
                }
                else if (MailFrom.Contains("eeolink@wiley.com"))
                {
                    ProcessMessage("**********************Start Wiley************************************************* ");

                    ProcessMessage("wiley mail found and is getting process..  ");
                    ProcessMessage("Number of ::" + countThieme);

                    ProcessMessage("wiley MailFrom ::" + MailFrom);
                    ProcessMessage("wiley MailSubject ::" + MailSubject);

                    ProcessMessage("JQA MailBody ::" + MailBody);

                    if (MailSubject.StartsWith("EEO:Link Manuscript", StringComparison.OrdinalIgnoreCase))
                    {
                        //wileyProcess wiley = new wileyProcess(MailSubject, MailFrom, MailBody, Doc);
                        //wiley.ErrorNotification += ErrorMessage;
                        //wiley.ProcessNotification += ProcessMessage;

                        //wiley.StartProcess();
                        //Doc.Remove(true);
                        //ProcessMessage("**********************End Wiley************************************************* ");
                    }

                }
                else if (MailFrom.Contains("jqa-notification-service@wiley.com"))
                {
                    //ProcessMessage("**********************Start Wiley JQA************************************************* ");

                    //ProcessMessage("JQA mail found and is getting process..  ");
                    //ProcessMessage("Number of ::" + countThieme);

                    //ProcessMessage("JQA MailFrom ::" + MailFrom);
                    //ProcessMessage("JQA MailSubject ::" + MailSubject);

                    //item = Doc.GetFirstItem("Body");
                    //MailBody = item != null ? item.Text : "";

                    //item = Doc.GetFirstItem("Body");
                    //ProcessMessage("JQA MailBody ::" + MailBody);
                   
                    ////_EOOMails.Add(new MailInfo(Doc.NoteID, MailFrom, "", MailSubject, MailBody, Doc.Created.ToString()));

                    //JQAnotification jqa = new JQAnotification(MailSubject, MailFrom, MailBody, Doc);
                    //jqa.ErrorNotification += ErrorMessage;
                    //jqa.ProcessNotification += ProcessMessage;

                    //jqa.StartProcess();
                    //Doc.Remove(true);
                    //ProcessMessage("**********************End JQA************************************************* ");

                }
                //else if (MailFrom.Contains("info@thieme-ejournals.de"))   Email id was changed as checked at sunil system
                else if (MailFrom.Contains("THIEME.J"))
                {

                    ProcessMessage("**********************Start Thieme************************************************* ");

                    ProcessMessage("Thieme offprint mail found and is getting process..  ");
                    ProcessMessage("Number of thieme offprint mail ::" + countThieme);

                    ProcessMessage("Thieme MailFrom ::" + MailFrom);
                    ProcessMessage("Thieme MailSubject ::" + MailSubject);

                    //item = Doc.GetFirstItem("Body");
                    //MailBody = item != null ? item.Text : "";

                    ProcessMessage("Thieme MailBody ::" + MailBody);

                    if (MailSubject.IndexOf("freigeben") != -1) ///////////No need to process ready for publishing mail
                    {
                        ProcessMessage("freigeben" + MailSubject.IndexOf("freigeben").ToString());

                        ProcessMessage("MailSubject.IndexOf(freigeben) != -1)");
                        ProcessMessage("No need to process this mail");
                    }
                    else if (MailSubject.IndexOf("Fehler/error") != -1) ///////////No need to process error mail
                    {
                        ProcessMessage("Fehler/error " + MailSubject.IndexOf("Fehler/error").ToString());
                        ProcessMessage("No need to process this mail");
                    }
                    else if (MailSubject.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || MailSubject.Trim().EndsWith("online", StringComparison.OrdinalIgnoreCase))
                    {
                        if (MailBody.Contains("info@thieme-ejournals.de") || MailBody.Contains("info@E-Publishing.Thieme.de") || MailBody.Contains("info@e-publishing.thieme.de"))
                        InsertThiemeEmail(MailSubject, MailBody);
                        ProcessMessage("Insert thieme mail details in database and deleted from lotus inbox.");
                    }

                    countThieme++;
                   // Doc.Remove(true);

                    ProcessMessage("**********************End Thieme************************************************* ");

                }
                //else if (MailFrom.Contains("@eesmail.elsevier.com"))
                //{
                //    ProcessMessage("**********************Start TDLemansMail************************************************* ");

                //    ProcessMessage("Doc.NoteID  :: " + Doc.NoteID);
                //    ProcessMessage("MailFrom    :: " + MailFrom);
                //    ProcessMessage("MailSubject :: " + MailSubject);
                //    //ProcessMessage("Doc.Created :: " + Doc.Created.ToString());
                //    if (MailSubject.IndexOf("Accepted",StringComparison.OrdinalIgnoreCase)!=-1)
                //    {
                //        ProcessMessage("MailBody    :: " + MailBody);
                //        item = Doc.GetFirstItem("Body");
                //        MailBody = item != null ? item.Text : "";
                //        TDLemansMails.TDLemansMail LemansMailObj = new TDLemansMails.TDLemansMail(Doc.Created.ToString(), MailSubject, MailBody);
                //        LemansMailObj.ProcessNotification += ProcessMessage;
                //        LemansMailObj.ErrorNotification   += ErrorMessage;

                //        if (LemansMailObj.InsertTDLemansMail())
                //        {
                //            ProcessMessage("Inserted mail subject :: " + MailSubject);
                //            Doc.Remove(true);
                //        }
                //    }
                //    else
                //    {
                //        ProcessMessage("Deleted mail subject :: " + MailSubject);
                //        Doc.Remove(true);
                //    }
                //    ProcessMessage("**********************End TDLemansMail************************************************* ");
                //}
                else if (MailFrom.Contains("BIAdmin@wiley.com"))
                {
                    //ProcessACRMail(Doc);
                    //Doc.Remove(true);
                }
                //else if (!string.IsNullOrEmpty(MailSubject) && ProcessMailSubject(MailSubject))
                //{
                //    item = Doc.GetFirstItem("Body");
                //    MailBody = item != null ? item.Text : "";
                //    _EOOMails.Add(new MailInfo(Doc.NoteID, MailFrom, "", MailSubject, MailBody, Doc.Created.ToString()));

                //    ProcessMessage("Doc.NoteID  :: " + Doc.NoteID);
                //    ProcessMessage("MailFrom    :: " + MailFrom);
                //    ProcessMessage("MailSubject :: " + MailSubject);
                //    ProcessMessage("MailBody    :: " + MailBody);
                //    ProcessMessage("Doc.Created :: " + Doc.Created.ToString());
                //}
                else
                {
                    //ProcessMessage("Doc.NoteID  :: " + Doc.NoteID);
                    //ProcessMessage("MailFrom    :: " + MailFrom);
                    //ProcessMessage("MailSubject :: " + MailSubject);
                    //ProcessMessage("MailBody    :: " + MailBody);
                    //ProcessMessage("Doc.Created :: " + Doc.Created.ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
            }
        }
        private void InsertThiemeEmail(string MailSub, string MailBody)
        {

           ThiemeEfirstMailSubjectProcess ThiemeEfirst = new ThiemeEfirstMailSubjectProcess(MailSub, MailBody);
           ThiemeEfirst.ErrorNotification   += new NotifyErrMsg(ThmErrorMessage);
           ThiemeEfirst.ProcessNotification += new NotifyMsg(ThmProcessMessage);
           ThiemeEfirst.StartProcess();

        }
        private void ProcessACRMail(NotesDocument Doc )
        {
            NotesItem item = null;
            if (Doc.HasEmbedded)
            {
                item = Doc.GetFirstItem("Body");
                object EmbeddedObjects = null;
                RichTextBody = (Domino.NotesRichTextItem)item;
                EmbeddedObjects = RichTextBody.EmbeddedObjects;
                Array embedArray = (System.Array)EmbeddedObjects;
                if (!Directory.Exists(ACRDOWNLOADPATH))
                {
                    Directory.CreateDirectory(ACRDOWNLOADPATH);
                }
                for (int Z = 0; Z < embedArray.Length; Z++)
                {
                    Attach = (Domino.NotesEmbeddedObject)embedArray.GetValue(Z);
                    //string AttachFilePath = ACRDOWNLOADPATH + "\\" + Doc.NoteID+ Path.GetExtension( Attach.Name);
                    string AttachFilePath = ACRDOWNLOADPATH + "\\" + Attach.Name;

                    if (File.Exists(AttachFilePath))
                    {
                        File.Delete(AttachFilePath);
                    }
                    Attach.ExtractFile(AttachFilePath);
                }
            }
            
        }


        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }

        private void ThmProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ThmErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }

    }
}

