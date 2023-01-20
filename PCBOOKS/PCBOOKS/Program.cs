using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace PCBOOKS
{
    class Program
    {
        public DataSet ds = null;
        static string key = "";
        static string value = "";
        static string processedpath = "";
        static string unprocesspath = "";
        static string downloadedpath = "";
        static string processedfailpath = "";
        static void Main(string[] args)
        {
            try
            {
                DateTime t = DateTime.MinValue;
                Program objProgram = new Program();
                objProgram.StartProcess();
            }
            catch (Exception e)
            {
                logwriter("Exception in Mail Function" + e.ToString());
            }
        }

        public void StartProcess()
        {
            //
            // 1 = check all folder setup
            // 2 = Read property.ini file and validate all path exists
            // 3 = Get all files from the download folder to process one by one
            // 4 = chek database that file already processed or not
            // 5 = send mail if already found the file already processed
            // 6 = Check the filename contain PC AU ED or MC
            // 7 = IF PC PC AU ED or MC then
            // 7.a = read the signal xml get all relevant values from xml
            // 7.b = get author name and editor name from ppm(\\td - nas\Elsinpt\ElsBook\Orders\PPM\)
            // 7.c = get book title from ppm path (\\td - nas\Elsinpt\ElsBook\Orders\PPM\)
            // 7.d = exec procedure to get all detail from database which is saved at the time of dataset upload(usp_getautorinfo_duedate)
            // 7.e = check all values has valid data or some fileds are empty, if empty then send internal mail
            // 7.f = Check Ftp details and choose mail template accordingly
            // 7.g = replace values from mail template and send mail
            // 7.h = if getting error in sending mail then send mail internally
            // 7.i = move files in sucess folder and update in database
            // get data from database
            // check space
            // check mail

            logwriter("Process Started");
            if (CheckSpace())
            {
                if (CheckMail())
                {
                    GetConfigDeatilsFromDB();
                    DataTable dtSignal = GetAllDownloadedSignalList();
                    if (dtSignal.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtSignal.Rows.Count; i++)
                        {

                            string fileName = dtSignal.Rows[i]["FileName"].ToString();
                            if (fileName.EndsWith(".xml")) { 
                                if (IsAlreadyProcessed(fileName))
                                {
                                    // this is already uploaded please send mail to internal team
                                    // wip 1
                                }
                                else
                                {
                                    ProcessItem(fileName);
                                }
                            }
                        }

                    }


                }
            }



        }

        public void ProcessItem(string fileName)
        {
            fileName = downloadedpath + "\\" + fileName;
            //string downloadPath = "";
            //string processedPath = "";
            if (fileName.ToLower().Contains("-pc-") && (!(fileName.ToLower().Contains("-pc-2"))))
            {
                if (PCProcess(fileName))
                {
                    logwriter("PCProcess successfull for " + fileName.ToString());
                }
                else
                {
                    logwriter("PCProcess is failed for " + fileName.ToString());
                }
            }
            else if (fileName.ToLower().Contains("-mc-") && (!(fileName.ToLower().Contains("-mc-q"))))
            {
                string error = "";
                MCSignal oMCSignal = ReadMCSignal(fileName);
                if (oMCSignal != null)
                {
                    if (ElsMCSubmition(fileName, out error, oMCSignal))
                    {
                        //logwriter("MCSubmition successfull for " + fileName.ToString());
                    }
                    else
                    {
                        //logwriter("MCSubmition failed for " + fileName.ToString());
                    }
                }
                else
                {
                    //logwriter("E5:MCSubmition failed unable to read MCSignal signal is null for " + fileName.ToString());
                }
            }
            else if ((fileName.ToLower().Contains("-mc-q")))
            {
                string error = "";
                MCSignal oMCSignal = ReadMCSignal(fileName);
                if (oMCSignal != null)
                {
                    if (ElsMCQSubmition(fileName, out error, oMCSignal))
                    {
                        //logwriter(fileName, "MCSubmition successfull for " + fileName.ToString(), "INFO");
                    }
                    else
                    {
                        //logwriter(fileName, "MCSubmition failed for " + fileName.ToString(), "ERROR");
                    }
                }
                else
                {
                    //logwriter(fileName, "E5:MCSubmition failed unable to read MCSignal signal is null for " + fileName.ToString(), "ERROR");
                }
            }
            else if (fileName.ToLower().Contains("-au-"))
            {
                string error = "";
                AuthorSignal ASignal = ReadAuthorSignal(fileName);
                if (ASignal == null)
                {
                    logwriter("AuthorSignal failed unable to read Author Signalis for " + fileName.ToString());
                }
                else
                {
                    if (ASignal.next_proof_corrector.ToLower() == "editor")
                    {
                        if (EditorMailOnAuthorSubmition(fileName, out error, ASignal))//if editrs flow mail to editor..
                        {
                            logwriter("Editor Mail On Author Submition successfull for " + fileName.ToString());
                        }
                        else
                        {
                            logwriter("Editor Mail On Author Submition failed for " + fileName.ToString());
                        }
                    }
                    else
                    {
                        if (AuthorProcess(fileName, out error, ASignal))
                        {
                            logwriter("Author Process Succesfully done for " + fileName.ToString());
                        }
                        else
                        {
                            logwriter("E5:Author Process failed for " + fileName.ToString());
                        }
                    }
                }
            }
            else if (fileName.ToLower().Contains("-ed-"))
            {
                string error = string.Empty;
                EditorSignal EDSignal = ReadEditorSignal(fileName);
                if (EditorProcessPM(fileName, out error, EDSignal))
                {
                    logwriter("Editor Process PM Succesfully done for " + fileName.ToString());
                }
                else
                {
                    logwriter("E5:Editor Process PM failed for " + fileName.ToString() + " Details :: " + error);
                }
            }
            else if (fileName.ToLower().Contains("-pc-2"))
            {
                // Els pm
                if (PCProcessRoundTwo(fileName))
                {
                    //logwriter(fileName, "PC Process Round Two successfully for " + fileName.ToString(), "INFO");
                }
                else
                {
                    //logwriter(fileName, "E5:PC Process Round Two failed for " + fileName.ToString(), "ERROR");
                }
            }
            else if (fileName.ToLower().Contains("-pm-"))
            {
                //Aditya 
                string error = "";
                PMSignal oPMSignal = ReadPMSignal(fileName);
                if (ElsPMSubmition(fileName, out error, oPMSignal))
                {
                    //logwriter(fileName, "PM Submition  successfully for " + fileName.ToString(), "INFO");
                }
                else
                {
                    //logwriter(fileName, "PC Process Round Two failed for " + fileName.ToString() + " Details :: " + error, "ERROR");
                }
            }
            else
            {
                //File.Move(fileName, strdfpath + "\\Ignore\\" + fileName.Name);
            }

        }

        private bool PCProcess(string strFile)
        {
            string strSupplier, strISBN, strPII, strStatus, strLink, strRemarks;
            strSupplier = strISBN = strPII = strStatus = strLink = strRemarks = "";
            string strduedate = "";
            string DbError = "";
            string TypeSignal = "PC";
            FileInfo FI = new FileInfo(strFile);
            bool boolVal = Read_Information_From_XML(strFile, out strSupplier, out strISBN, out strPII, out strStatus, out strLink, out strRemarks);
            if (boolVal == true)
            {
                string author_editorname = "";
                string strauthorname = Get_PPMOrder_Authorname(strISBN);
                string streditorname = Get_PPMOrder_editorname(strISBN);

                if (strauthorname.Trim() == "")
                {
                    author_editorname = streditorname;
                }
                else
                {
                    author_editorname = strauthorname;
                }

                string strSubject = "";
                string strTitle = "";
                string DBBCC = "";
                string chplbl = "";
                string docsubtype = "";
                strTitle = Get_Book_Title(strISBN);
                strTitle = strTitle.Replace("\r\n", "");
                strTitle = strTitle.Replace("\n", "");
                string chepter = strPII.Substring(strPII.Length - 7, 5);

                ///////////////

                SqlParameter[] sqlinputparm = new SqlParameter[2];
                sqlinputparm[0] = new SqlParameter("PII", strPII);
                sqlinputparm[1] = new SqlParameter("Type", "AU");

                DataSet DS = ExecuteDataSetSP("usp_GetAuthorInfo_DUEDate", sqlinputparm);
                string strPM = "";
                string strTo = "";
                string strCC = "";
                string chapterTitle = "";
                string BranchType = "";
                string FtpDetails = "";
                string Laung = "";
                string AuthorName = "";
                if (DS.Tables[0].Rows.Count > 0 && DS != null)
                {
                    DBBCC = DS.Tables[0].Rows[0]["ThomsonPMMail"] != DBNull.Value ? DS.Tables[0].Rows[0]["ThomsonPMMail"].ToString() : "";
                    chplbl = DS.Tables[0].Rows[0]["ChapLbl"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapLbl"].ToString() : "";
                    strPM = DS.Tables[0].Rows[0]["PM"] != DBNull.Value ? DS.Tables[0].Rows[0]["PM"].ToString() : "";
                    DateTime dateT = DS.Tables[0].Rows[0]["DueDate"] != DBNull.Value ? (DateTime)DS.Tables[0].Rows[0]["DueDate"] : DateTime.MinValue;
                    strTo = DS.Tables[0].Rows[0]["MailTo"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailTo"].ToString() : "";
                    strCC = DS.Tables[0].Rows[0]["MailCC"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailCC"].ToString() : "";
                    chapterTitle = DS.Tables[0].Rows[0]["ChapTitle"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapTitle"].ToString() : "";
                    strduedate = dateT.ToString("dd-MM-yyyy");
                    docsubtype = DS.Tables[0].Rows[0]["Docsubtype"] != DBNull.Value ? DS.Tables[0].Rows[0]["Docsubtype"].ToString() : "";
                    BranchType = DS.Tables[0].Rows[0]["BranchType"] != DBNull.Value ? DS.Tables[0].Rows[0]["BranchType"].ToString() : "";
                    FtpDetails = DS.Tables[0].Rows[0]["FtpDetails"] != DBNull.Value ? DS.Tables[0].Rows[0]["FtpDetails"].ToString() : "";
                    AuthorName = DS.Tables[0].Rows[0]["AuthorName"] != DBNull.Value ? DS.Tables[0].Rows[0]["AuthorName"].ToString() : "";
                    Laung = DS.Tables[0].Rows[0]["Laung"] != DBNull.Value ? DS.Tables[0].Rows[0]["Laung"].ToString() : "";
                }

                int sop = chplbl.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chplbl.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chplbl = chplbl.Remove(sop, esop - sop + 1);
                    }
                    sop = chplbl.IndexOf("<", 0);
                }
                sop = chapterTitle.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chapterTitle.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chapterTitle = chapterTitle.Remove(sop, esop - sop + 1);
                    }
                    sop = chapterTitle.IndexOf("<", 0);
                }


                //Mail_Class obj = new Mail_Class();
                if (strStatus.Trim().ToLower() == "success")
                {
                    bool DbData = true;

                    string strFilePath = "";
                    string strFilePathSnT = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\S&T\" + strISBN.Replace("-", "") + @"\Q300\Current_Order\S&T_" + strISBN.Replace("-", "") + "_Q300.xml";

                    string strFilePathEHS = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\EHS\" + strISBN.Replace("-", "") + @"\Q300\Current_Order\EHS_" + strISBN.Replace("-", "") + "_Q300.xml";

                    if (File.Exists(strFilePathSnT))
                        strFilePath = strFilePathSnT;
                    else if (File.Exists(strFilePathEHS))
                        strFilePath = strFilePathEHS;

                    if (File.Exists(strFilePath))
                    {

                        StreamReader sro = new StreamReader(strFilePath);
                        string fco = sro.ReadToEnd();
                        sro.Close();
                        strTitle = GetInformation(fco, "book-title");
                        author_editorname = GetInformation(fco, "volume-editor");
                    }
                    string mailbody = "";
                    string[] attachment = new string[] { "" };
                    if (BranchType.ToLower() == "mrw")
                    {
                        if (FtpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("MRWPC2.0");
                            mailbody = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        }
                        if (FtpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("MRWPC3.0");
                            mailbody = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        }
                    }
                    if (BranchType.ToLower() == "snt")
                    {
                        if (FtpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTPC2.0");
                            mailbody = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (FtpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTPC3.0");
                            mailbody = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (BranchType.ToLower() == "ehs")
                    {
                        if (Laung.ToLower() == "english")
                        {
                            if (FtpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("SNTPC2.0");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (FtpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("SNTPC3.0");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }
                        if (Laung.ToLower() == "french")
                        {
                            if (FtpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("EHSFrench3.1");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (FtpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("EHSFrench3.1");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }
                        if (Laung.ToLower() == "german")
                        {
                            if (FtpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("SNTPC2.0");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (FtpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("SNTPC3.0");
                                mailbody = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }

                    }
                    if (strPM.Trim() == "" || strTo.Trim() == "" || strCC.Trim() == "" || chapterTitle.Trim() == "" || strTitle.Trim() == "" || author_editorname.Trim() == "")
                    {
                        logwriter("Some blank Information found in database to send author mail Mailnot sent for this case");
                        logwriter("strPm = " + strPM + ", strTo = " + strTo + " , strCC = " + strCC + ", chaptertitile = " + chapterTitle + " , strTitle = " + strTitle + "author_editorname = " + author_editorname);
                        DbData = false;
                    }
                    string strMsg = mailbody;

                    if (DbData == true)
                    {
                        strMsg = strMsg.Replace("[AuthorName]", AuthorName);
                        strMsg = strMsg.Replace("[ChapterNo]", strPII.Substring(strPII.Length - 7, 5));
                        strMsg = strMsg.Replace("[PCURL]", strLink);
                        strMsg = strMsg.Replace("[PM]", strPM);
                        strMsg = strMsg.Replace("[DUEDATE]", strduedate);
                        strMsg = strMsg.Replace("[BT]", strTitle);
                        strMsg = strMsg.Replace("[CT]", chapterTitle);

                        string signalStatus = "Success";
                        logwriter("Information found in database to send author mail ");
                        SendMail(strTo, strCC, DBBCC, strSubject, strMsg, attachment);
                        DbError = "PCProcess:Mail send succesfully for " + FI.Name.ToString() + " to " + strTo;
                        Insert_Signal_Details(strPII, FI.Name, strISBN, strLink, DbError, signalStatus, TypeSignal);
                        if (File.Exists(processedpath + "\\" + FI.Name))
                        {
                            File.Delete(processedpath + "\\" + FI.Name);
                        }
                        File.Copy(strFile, processedpath + "\\" + FI.Name);
                        File.Delete(strFile);
                    }
                    else
                    {
                        logwriter("Information missing to send author mail ");
                        if (strLink.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[PCURL]", strLink);
                        }
                        if (strPM.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[PM]", strPM);
                        }
                        if (strduedate.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[DUEDATE]", strduedate);
                        }
                        if (strTitle.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[BT]", strTitle);
                        }
                        if (chapterTitle.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[CT]", chapterTitle);
                        }
                        logwriter("Information found in database to send author mail ");
                        string signalStatus = "Failed";
                        SendMail(DBBCC, DBBCC, DBBCC, strSubject, strMsg, attachment);
                        DbError = "Mail send succesfully to internal team  " + FI.Name.ToString() + " to " + strTo;
                        Insert_Signal_Details(strPII, FI.Name, strISBN, strLink, DbError, signalStatus, TypeSignal);
                        if (File.Exists(processedpath + "\\" + FI.Name))
                        {
                            File.Delete(processedpath + "\\" + FI.Name);
                        }
                        File.Copy(strFile, processedpath + "\\" + FI.Name);
                        File.Delete(strFile);
                    }
                }
                else
                {
                    //Failed signal    
                    string signalStatus = "Failed";
                    string errormail = ConfigurationSettings.AppSettings["ErrorMail"].ToString();
                    DataTable dt = GetMailTemplate("SignalReadFailure");
                    string[] attachment = new string[] { strFile };
                    string mailbody = dt.Rows[0]["MailBody"].ToString().Replace("[FileName]", FI.Name);
                    strSubject = dt.Rows[0]["MailSubject"].ToString();
                    strSubject = strSubject.Replace("[FileName]", FI.Name);
                    SendMail(errormail, "", "", strSubject, mailbody, attachment);
                    // send fail mail, insert into signal info, update download signal status, move file in fail folder
                    DbError = "Tool not able to read the signal file";
                    Insert_Signal_Details(strPII, FI.Name, strISBN, strLink, DbError, signalStatus, TypeSignal);
                    if (File.Exists(processedfailpath + "\\" + FI.Name))
                    {
                        File.Delete(processedfailpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedfailpath + "\\" + FI.Name);
                    File.Delete(strFile);
                }
            }
            else
            {
                // fail mail when xml read error
                string mailbody = "";
                string strSubject = "";
                string signalStatus = "Failed";
                string errormail = ConfigurationSettings.AppSettings["ErrorMail"].ToString();
                string[] attachment = new string[] { strFile };
                //string body = FailBody("Unable to extract information for " + FI.Name);
                DataTable dt = GetMailTemplate("SignalReadFailure");
                mailbody = dt.Rows[0]["MailBody"].ToString().Replace("[FileName]", FI.Name);
                strSubject = dt.Rows[0]["MailSubject"].ToString();
                strSubject = strSubject.Replace("[FileName]", FI.Name);
                // do in every case
                SendMail(errormail, "", "", strSubject, mailbody, attachment);
                // send fail mail, insert into signal info, update download signal status, move file in fail folder
                DbError = "Tool not able to read the signal file";
                Insert_Signal_Details(strPII, FI.Name, strISBN, strLink, DbError, signalStatus, TypeSignal);
                if (File.Exists(processedfailpath + "\\" + FI.Name))
                {
                    File.Delete(processedfailpath + "\\" + FI.Name);
                }
                File.Copy(strFile, processedfailpath + "\\" + FI.Name);
                File.Delete(strFile);
            }
            return true;
        }

        public void Insert_Signal_Details(string strPII, string FileName, string strISBN, string strLink, string DbError, string signalStatus, string TypeSignal)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString()))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_Insert_Signal_Details]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@PII", SqlDbType.VarChar).Value = strPII;
                    sqlcmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName;
                    sqlcmd.Parameters.Add("@ISBN", SqlDbType.VarChar).Value = strISBN;
                    sqlcmd.Parameters.Add("@LINK", SqlDbType.VarChar).Value = strLink;
                    sqlcmd.Parameters.Add("@DbError", SqlDbType.VarChar).Value = DbError;
                    sqlcmd.Parameters.Add("@SignalStatus", SqlDbType.VarChar).Value = signalStatus;
                    sqlcmd.Parameters.Add("@TypeSignal", SqlDbType.VarChar).Value = TypeSignal;

                    conn.Open();
                    sqlcmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //logwriter("Exception : " + ex.Message);
            }
        }
        private bool ElsMCSubmition(string strFile, out string Error, MCSignal mcSignal)
        {
            string MCSUBMITION = "";
            string DBError = "";
            try
            {
                FileInfo FI = new FileInfo(strFile);
                if (!(File.Exists(MCSUBMITION)))
                {
                    Error = "Unable to find MC template " + strFile;
                    return false;
                }
                StreamReader srat = new StreamReader(MCSUBMITION);
                string strfcat = srat.ReadToEnd();
                srat.Close();

                string strTitle = "";
                strTitle = Get_Book_Title(mcSignal.isbn);
                strTitle = strTitle.Replace("\r\n", "");
                strTitle = strTitle.Replace("\n", "");
                string chepter = mcSignal.pii.Substring(mcSignal.pii.Length - 7, 5);

                string strMsg = strfcat;
                //string signalLink = "ftp://ftp.elsevierproofcentral.com/Signals/" + FI.Name.ToString();

                string PIII = mcSignal.pii;

                strMsg = strMsg.Replace("[BT]", strTitle);
                strMsg = strMsg.Replace("[ChapterNo]", chepter);
                strMsg = strMsg.Replace("[LINK]", mcSignal.url);
                string Subject = "";

                Subject = "MC corrections received for " + strTitle + ": Chapter " + chepter;
                //Mail_Class obj = new Mail_Class();

                //string zippath = "";
                //FileInfo FIe = new FileInfo(strFile);
                //if (File.Exists(strfpath + "\\" + mcSignal.zip_name))
                //{
                //    zippath = strfpath + "\\" + mcSignal.zip_name;
                //}
                //else if (File.Exists(strspath + "\\" + mcSignal.zip_name))
                //{
                //    zippath = strspath + "\\" + mcSignal.zip_name;
                //}
                //else if (File.Exists(FIe.DirectoryName + "\\" + mcSignal.zip_name))
                //{
                //    zippath = FIe.DirectoryName + "\\" + mcSignal.zip_name;
                //}
                string[] attachment = new string[] { };
                string mailto = "deepak.verma@digiscapetech.com";
                string mailcc = "deepak.verma@digiscapetech.com";
                string mailbcc = "deepak.verma@digiscapetech.com";

                // Raushan commented on 7 oct 2016
                //was
                //if (obj.SendMailMy(mailto, Subject, strMsg, mailcc, mailbcc, "eproof@elsevier.thomsondigital.com", attachment))
                //{
                //    DBError = "Mail Send for MC signal to " + FI.Name.ToString() + " to " + GlobalVar.GetTdPMTO;
                //    //GlobalVar.logwriter("Signal", "Mail Send for MC signal to " + FI.Name.ToString() + " to " + GlobalVar.GetTdPMTO, "INFO");
                //    File.Move(strFile, strspath + "\\" + FI.Name);
                //}
                //else
                //{
                //    DBError = "E2:Unable to send mail for MC signal to " + FI.Name.ToString();
                //    //GlobalVar.logwriter("Signal", "Unable to send mail for MC signal to " + FI.Name.ToString(), "ERROR");
                //    File.Move(strFile, strfpath + "\\" + FI.Name);
                //}



                // please comment below line 
                //File.Move(strFile, strspath + "\\" + FI.Name);

                //////////////////////////////////////////////////////////////////

                ////GlobalVar.Update_Signal(mcSignal.pii, FI.Name, mcSignal.isbn, mcSignal.url, DBError);
                Error = "";
                return true;
            }
            catch (Exception exc)
            {
                Error = exc.Message.ToString();
                ////GlobalVar.logwriter("Signal", "E5: " + Error, "ERROR");
                return false;
            }
        }
        public bool CheckSpace()
        {
            //try
            //{
            //    logwriter("Checking Space");
            //    foreach (DriveInfo drive in DriveInfo.GetDrives())
            //    {
            //        if (drive.IsReady)
            //        {
            //            Int64 space = drive.TotalFreeSpace; // /1024*1024 in gb
            //            space = space / (1024 * 1024 * 1024);
            //            if (space < 1)
            //            {
            //                string[] attachment = new string[] { "" };
            //                string mailTo = "Rohit.Singh@digiscapetech.com";
            //                string mailCC = "it.noida@thomsondigital.com,fms_support@thomsondigital.com";
            //                string mailSubject = "No Space on server 172.16.1.238";
            //                string mailBody = "Hi Team </br></br> Please check there is no space on Server. </br></br> Regards,</br>Technology";
            //                SendMail(mailTo, mailCC, "", mailSubject, mailBody, attachment);
            //                if (space < 1)
            //                {
            //                    logwriter("Space is not avilable on Server.");
            //                    return false;

            //                }
            //                else
            //                {
            //                    return true;
            //                }

            //            }
            //        }
            //    }
            return true;
            //}
            //catch (Exception e)
            //{
            //    logwriter("Exception in Checking Space :: " + e.ToString());
            //    return false;
            //}
        }

        public bool CheckMail()
        {
            //try
            //{
            //    string[] attachment = new string[] { "" };
            //    logwriter("Checking Mail");
            //    string mailTo = "Rohit.singh@digiscapetech.com";
            //    string mailCC = "Rohit.singh@digiscapetech.com";
            //    string mailBCC = "Rohit.singh@digiscapetech.com";
            //    string mailSubject = "Mail is working Fine";
            //    string mailBody = "Hi Team </br></br> Test Mail to check Mail Server. </br></br> Regards,</br>Technology";
            //    SendMail(mailTo, mailCC, mailBCC, mailSubject, mailBody, attachment);
            return true;
            //}
            //catch (Exception e)
            //{
            //    logwriter("Exception in Sending Mail From CheckMail()" + e.ToString());
            //    return false;
            //}
        }

        //public DataTable GetAllConfigData()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString()))
        //        {
        //            SqlCommand sqlcmd = new SqlCommand("[usp_GetAllConfigDetails]", conn);
        //            sqlcmd.CommandType = CommandType.StoredProcedure;
        //            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
        //            conn.Open();
        //            da.Fill(dt);
        //            conn.Close();
        //            return dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //log write
        //        return dt;
        //    }
        //}

        public static DataSet ExecuteDataSetSP(string spName, SqlParameter[] sqlParams)
        {
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString());
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;

                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Length; i++)
                    {
                        myCommand.Parameters.Add(sqlParams[i]);
                    }
                }

                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DataTable GetAllDownloadedSignalList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString()))
                {
                    SqlCommand sqlcmd = new SqlCommand("[usp_GetAllDownloadedSignalList]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                //log write
                return dt;
            }
        }

        public DataTable GetMailTemplate(string mailtemplate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString()))
                {
                    SqlCommand sqlcmd = new SqlCommand("[usp_GetMailTemplate]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@mailtemplate", SqlDbType.VarChar).Value = mailtemplate;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                //log write
                return dt;
            }
        }

        public bool IsAlreadyProcessed(string fileName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["sqlConnection"].ToString()))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_IsAlreadyProcessed]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@fileName", SqlDbType.VarChar).Value = fileName;
                    conn.Open();
                    //string strSelectCmd = "select PII,Filename, TypeSignal, URL, Remarks, DownloadDate from SignalInfo where isbn = '" + isbn + "'";
                    SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);
                    DataSet ds = new DataSet();
                    sqlad.Fill(ds);
                    conn.Close();
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                //log write
                return false;
            }
        }

        public static void logwriter(string strMessage)
        {
            try
            {
                Console.WriteLine(strMessage);
                string strFilePath = System.Configuration.ConfigurationSettings.AppSettings["LogPath"];
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }
                if (!Directory.Exists(strFilePath))
                {
                    Console.WriteLine("Location is invalid... please check");
                    Console.Read();
                    //return false;
                }


                string error = System.DateTime.Now.ToString("hh:mm:ss tt") + ":  " + strMessage + Environment.NewLine;
                string file = strFilePath + "\\LOG_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
                File.AppendAllText(file, error);

                //return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Excetion raised while writing in the log file: " + Ex.Message);
                Console.Read();
                //return false;
            }

        }

        public void SendMail(string mailTo, string mailCC, string mailBCC, string mailSubject, string mailBody , string[] Attach)
        {
            try
            {
                MailMessage mail = new MailMessage();
                if (mailCC.EndsWith(","))
                {
                    mailCC = mailCC.Substring(0, mailCC.Count() - 1);
                }
                if (mailBCC.EndsWith(","))
                {
                    mailBCC = mailCC.Substring(0, mailBCC.Count() - 1);
                }
                if (Attach != null)
                {
                    foreach (string atch in Attach)
                    {
                        if (atch.Trim() != "")
                        {
                            FileInfo info = new FileInfo(atch);
                            Double sizes = ConvertBytesToMegabytes(info.Length);
                            if (sizes < 11)
                            {
                                Attachment ATCH = new Attachment(atch);
                                mail.Attachments.Add(ATCH);
                            }
                        }
                    }
                }

                logwriter("Start to send Mail");
                mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["MailFrom"]);
                mail.To.Add(mailTo);
                if (mailCC != String.Empty)
                    mail.CC.Add(mailCC);
                if (mailBCC != String.Empty)
                    mail.Bcc.Add(mailBCC);
                mail.Bcc.Add("books.thomson@gmail.com");
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                logwriter("Mail send: Mailsub " + mail.Subject);
                logwriter("Mail send: MailTo " + mail.To);
                logwriter("Mail send: MailCC " + mail.CC);
                logwriter("Mail send: MailBCC " + mail.Bcc);
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = System.Configuration.ConfigurationSettings.AppSettings["MailServerIP"];
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
                eMailClient.UseDefaultCredentials = false;
                eMailClient.Credentials = new System.Net.NetworkCredential("eproof.elsevier@thomsondigital.com", "Yaz42733");
                eMailClient.Port = 587;
                eMailClient.EnableSsl = true;
                eMailClient.Timeout = 600000;
                //==================================================
                //mail.CC.Clear();
                //mail.Bcc.Clear();
                //mail.To.Clear();
                mail.Bcc.Add(new MailAddress("deepak.verma@digiscapetech.com"));
                mail.CC.Add(new MailAddress("vrishni.d@thomsondigital.com"));
                mail.CC.Add(new MailAddress("Balam.singh@thomsondigital.com"));
                //==================================================
                eMailClient.Send(mail);

            }
            catch (Exception e)
            {
                logwriter("Exception in Mail send: " + e.ToString());

            }
        }
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public MCSignal ReadMCSignal(string auxml)
        {
            try
            {
                MCSignal ASignal = new MCSignal();
                StreamReader sr = new StreamReader(auxml);
                string strFC = sr.ReadToEnd();
                sr.Close();
                string pii = GetInformation(strFC, "aid");
                ASignal.pii = GetInformation(strFC, "pii");
                ASignal.url = GetInformation(strFC, "url");
                ASignal.zip_name = GetInformation(strFC, "zip-name");
                ASignal.zip_url = GetInformation(strFC, "zip-url");
                ASignal.rejected_ce_changes = GetInformation(strFC, "rejected-ce-changes");
                ASignal.queries = GetInformation(strFC, "queries");
                ASignal.next_proof_corrector = GetInformation(strFC, "next-proof-corrector");
                string isbn = GetInformation(strFC, "jid");
                ASignal.isbn = GetInformation(strFC, "isbn");
                ASignal.instructions_on_graphics = GetInformation(strFC, "instructions-on-graphics");
                ASignal.instructions = GetInformation(strFC, "instructions");
                ASignal.edits = GetInformation(strFC, "edits");
                ASignal.attachments_to_queries = GetInformation(strFC, "attachments-to-queries");
                ASignal.attachments = GetInformation(strFC, "attachments");
                if (ASignal.isbn == "")
                    ASignal.isbn = isbn;
                if (ASignal.pii == "")
                    ASignal.pii = pii;
                ////GlobalVar.logwriter("Signal", "Signal decoded for " + auxml, "INFO");
                return ASignal;
            }
            catch (Exception EXE)
            {
                ////GlobalVar.logwriter("Signal", EXE.Message.ToString(), "ERROR");
                return null;
            }
        }
        public AuthorSignal ReadAuthorSignal(string auxml)
        {
            try
            {
                AuthorSignal ASignal = new AuthorSignal();
                StreamReader sr = new StreamReader(auxml);
                string strFC = sr.ReadToEnd();
                sr.Close();
                ASignal.pii = GetInformation(strFC, "pii");
                ASignal.url = GetInformation(strFC, "url");
                ASignal.zip_name = GetInformation(strFC, "zip-name");
                ASignal.zip_url = GetInformation(strFC, "zip-url");
                ASignal.rejected_ce_changes = GetInformation(strFC, "rejected-ce-changes");
                ASignal.queries = GetInformation(strFC, "queries");
                ASignal.next_proof_corrector = GetInformation(strFC, "next-proof-corrector");
                ASignal.isbn = GetInformation(strFC, "isbn");
                ASignal.instructions_on_graphics = GetInformation(strFC, "instructions-on-graphics");
                ASignal.instructions = GetInformation(strFC, "instructions");
                ASignal.edits = GetInformation(strFC, "edits");
                ASignal.attachments_to_queries = GetInformation(strFC, "attachments-to-queries");
                ASignal.attachments = GetInformation(strFC, "attachments");

                // Created on 19-09-2016

                #region

                ASignal.article_title_edits = GetInformation(strFC, "article-title-edits");

                ASignal.article_title_comments = GetInformation(strFC, "article-title-comments");

                ASignal.author_group_edits = GetInformation(strFC, "author-group-edits");

                ASignal.author_group_comments = GetInformation(strFC, "author-group-comments");

                ASignal.first_author_edits = GetInformation(strFC, "first-author-edits");

                ASignal.first_author_comments = GetInformation(strFC, "first-author-comments");

                ASignal.corresponding_author_edits = GetInformation(strFC, "corresponding-author-edits");

                ASignal.corresponding_author_comments = GetInformation(strFC, "corresponding-author-comments");

                ASignal.correspondence_field_edits = GetInformation(strFC, "correspondence-field-edits");

                ASignal.correspondence_field_comments = GetInformation(strFC, "correspondence-field-comments");

                int cnt = 0;
                string question = "";
                int spos = strFC.IndexOf("<query>");
                while (spos != -1)
                {
                    int epos = strFC.IndexOf("</query>", spos);
                    if (epos != -1)
                    {

                        cnt++;
                        string tempques = strFC.Substring(spos + "<query>".Length, epos - spos - "<query>".Length);
                        tempques = tempques.Replace("<![CDATA[AU: ", "");
                        tempques = tempques.Replace("<![CDATA[AU:", "");
                        tempques = tempques.Replace("<![CDATA[", "");
                        tempques = tempques.Replace("]]>", "");
                        tempques = tempques.Replace("]]", "");
                        tempques = tempques.Replace("<", "");
                        tempques = tempques.Replace(">", "");
                        // question = question + "Q" + cnt + " " + tempques + "\r\n";
                        if (question == "")
                        {
                            question = "<br><b>Queries and Responses</b><br><br><table style =\"width:100%;border:1px solid black\">";
                        }
                        question = question + "<tr><td style =\"border:1px solid black\">Q" + cnt + "</td><td  style =\"border:1px solid black\">" + tempques + "</td></tr>";
                        int andspos = strFC.IndexOf("<answer>", epos);
                        if (andspos != -1)
                        {
                            string chkanswer = strFC.Substring(epos + "</query>".Length, andspos - epos - "</query>".Length);
                            if (!chkanswer.Contains("<query>"))
                            {
                                int eandspos = strFC.IndexOf("</answer>", andspos);
                                if (eandspos != -1)
                                {
                                    string tempanswer = strFC.Substring(andspos + "<answer>".Length, eandspos - andspos - "<answer>".Length);
                                    tempanswer = tempanswer.Replace("<![CDATA[AU: ", "");
                                    tempanswer = tempanswer.Replace("<![CDATA[AU:", "");
                                    tempanswer = tempanswer.Replace("<![CDATA[", "");
                                    tempanswer = tempanswer.Replace("]]>", "");
                                    tempanswer = tempanswer.Replace("]]", "");
                                    tempanswer = tempanswer.Replace("<", "");
                                    tempanswer = tempanswer.Replace(">", "");

                                    question = question + "<tr><td style =\"border:1px solid black\">R" + cnt + "</td><td  style =\"border:1px solid black\">" + tempanswer + "</td></tr>";
                                }
                            }
                        }
                    }
                    spos = strFC.IndexOf("<query>", spos + "<query>".Length);
                }
                if (question != "")
                {
                    question = question + "</table>";
                }
                ASignal.AuthorQuery = question;

                #endregion

                //


                ////GlobalVar.logwriter("Signal", "Signal decoded for " + auxml, "INFO");
                return ASignal;
            }
            catch (Exception EXE)
            {
                ////GlobalVar.logwriter("Signal", EXE.Message.ToString(), "ERROR");
                return null;
            }
        }
        public EditorSignal ReadEditorSignal(string auxml)
        {
            try
            {
                EditorSignal ASignal = new EditorSignal();
                StreamReader sr = new StreamReader(auxml);
                string strFC = sr.ReadToEnd();
                sr.Close();
                ASignal.pii = GetInformation(strFC, "pii");
                ASignal.url = GetInformation(strFC, "url");
                ASignal.zip_name = GetInformation(strFC, "zip-name");
                ASignal.zip_url = GetInformation(strFC, "zip-url");
                ASignal.rejected_ce_changes = GetInformation(strFC, "rejected-ce-changes");
                ASignal.queries = GetInformation(strFC, "queries");
                ASignal.next_proof_corrector = GetInformation(strFC, "next-proof-corrector");
                ASignal.isbn = GetInformation(strFC, "isbn");
                ASignal.instructions_on_graphics = GetInformation(strFC, "instructions-on-graphics");
                ASignal.instructions = GetInformation(strFC, "instructions");
                ASignal.edits = GetInformation(strFC, "edits");
                ASignal.attachments_to_queries = GetInformation(strFC, "attachments-to-queries");
                ASignal.attachments = GetInformation(strFC, "attachments");

                // Created on 19-09-2016

                #region

                ASignal.article_title_edits = GetInformation(strFC, "article-title-edits");

                ASignal.article_title_comments = GetInformation(strFC, "article-title-comments");

                ASignal.author_group_edits = GetInformation(strFC, "author-group-edits");

                ASignal.author_group_comments = GetInformation(strFC, "author-group-comments");

                ASignal.first_author_edits = GetInformation(strFC, "first-author-edits");

                ASignal.first_author_comments = GetInformation(strFC, "first-author-comments");

                ASignal.corresponding_author_edits = GetInformation(strFC, "corresponding-author-edits");

                ASignal.corresponding_author_comments = GetInformation(strFC, "corresponding-author-comments");

                ASignal.correspondence_field_edits = GetInformation(strFC, "correspondence-field-edits");

                ASignal.correspondence_field_comments = GetInformation(strFC, "correspondence-field-comments");




                int cnt = 0;
                string question = "";
                int spos = strFC.IndexOf("<query>");
                while (spos != -1)
                {
                    int epos = strFC.IndexOf("</query>", spos);
                    if (epos != -1)
                    {

                        cnt++;
                        string tempques = strFC.Substring(spos + "<query>".Length, epos - spos - "<query>".Length);
                        tempques = tempques.Replace("<![CDATA[AU: ", "");
                        tempques = tempques.Replace("<![CDATA[AU:", "");
                        tempques = tempques.Replace("<![CDATA[", "");
                        tempques = tempques.Replace("]]>", "");
                        tempques = tempques.Replace("]]", "");
                        tempques = tempques.Replace("<", "");
                        tempques = tempques.Replace(">", "");
                        // question = question + "Q" + cnt + " " + tempques + "\r\n";
                        if (question == "")
                        {
                            question = "<br><b>Queries and Responses</b><br><br><table style =\"width:100%;border:1px solid black\">";
                        }
                        question = question + "<tr><td style =\"border:1px solid black\">Q" + cnt + "</td><td  style =\"border:1px solid black\">" + tempques + "</td></tr>";
                        int andspos = strFC.IndexOf("<answer>", epos);
                        if (andspos != -1)
                        {
                            string chkanswer = strFC.Substring(epos + "</query>".Length, andspos - epos - "</query>".Length);
                            if (!chkanswer.Contains("<query>"))
                            {
                                int eandspos = strFC.IndexOf("</answer>", andspos);
                                if (eandspos != -1)
                                {
                                    string tempanswer = strFC.Substring(andspos + "<answer>".Length, eandspos - andspos - "<answer>".Length);
                                    tempanswer = tempanswer.Replace("<![CDATA[AU: ", "");
                                    tempanswer = tempanswer.Replace("<![CDATA[AU:", "");
                                    tempanswer = tempanswer.Replace("<![CDATA[", "");
                                    tempanswer = tempanswer.Replace("]]>", "");
                                    tempanswer = tempanswer.Replace("]]", "");
                                    tempanswer = tempanswer.Replace("<", "");
                                    tempanswer = tempanswer.Replace(">", "");

                                    question = question + "<tr><td style =\"border:1px solid black\">R" + cnt + "</td><td  style =\"border:1px solid black\">" + tempanswer + "</td></tr>";
                                }
                            }
                        }
                    }
                    spos = strFC.IndexOf("<query>", spos + "<query>".Length);
                }
                if (question != "")
                {
                    question = question + "</table>";
                }
                ASignal.EditorQuery = question;

                #endregion

                //

                ////GlobalVar.logwriter("Signal", "Signal decoded for " + auxml, "INFO");
                return ASignal;
            }
            catch (Exception EXE)
            {
                ////GlobalVar.logwriter("Signal", EXE.Message.ToString(), "ERROR");
                return null;
            }
        }
        public PMSignal ReadPMSignal(string auxml)
        {
            try
            {
                PMSignal ASignal = new PMSignal();
                StreamReader sr = new StreamReader(auxml);
                string strFC = sr.ReadToEnd();
                sr.Close();
                ASignal.pii = GetInformation(strFC, "pii");
                ASignal.url = GetInformation(strFC, "url");
                ASignal.zip_name = GetInformation(strFC, "zip-name");
                ASignal.zip_url = GetInformation(strFC, "zip-url");
                ASignal.rejected_ce_changes = GetInformation(strFC, "rejected-ce-changes");
                ASignal.queries = GetInformation(strFC, "queries");
                ASignal.next_proof_corrector = GetInformation(strFC, "next-proof-corrector");
                ASignal.isbn = GetInformation(strFC, "isbn");
                ASignal.instructions_on_graphics = GetInformation(strFC, "instructions-on-graphics");
                ASignal.instructions = GetInformation(strFC, "instructions");
                ASignal.edits = GetInformation(strFC, "edits");
                ASignal.attachments_to_queries = GetInformation(strFC, "attachments-to-queries");
                ASignal.attachments = GetInformation(strFC, "attachments");
                ////GlobalVar.logwriter("Signal", "Signal decoded for " + auxml, "INFO");
                return ASignal;
            }
            catch (Exception EXE)
            {
                //////GlobalVar.logwriter("Signal", EXE.Message.ToString(), "ERROR");
                return null;
            }
        }
        public string GetInformation(string content, string tag)
        {
            string RetVal = "";

            int spos = content.IndexOf("<" + tag + ">");
            int epos = content.IndexOf("</" + tag + ">");

            if (spos != -1 && epos != -1 && spos < epos)
            {
                RetVal = content.Substring(spos + tag.Length + 2, epos - spos - tag.Length - 2);
            }
            return RetVal;
        }

        public bool Read_Information_From_XML(string strFile, out string strSupplier, out string strISBN, out string strPII, out string strStatus, out string strLink, out string strRemarks)
        {
            try
            {
                strStatus = "";
                strPII = "";
                strStatus = "";
                strLink = "";
                strRemarks = "";
                strISBN = "";
                strSupplier = "";

                StreamReader sr = new StreamReader(strFile);
                string strFC = sr.ReadToEnd();
                sr.Close();

                strSupplier = strISBN = strPII = strStatus = strLink = strRemarks = "";
                strSupplier = GetInformation(strFC, "name");
                strISBN = GetInformation(strFC, "isbn");
                strPII = GetInformation(strFC, "pii");
                strStatus = GetInformation(strFC, "status");
                strLink = GetInformation(strFC, "url");
                strRemarks = GetInformation(strFC, "error-description");

                if (strSupplier.Trim() == "" || strISBN.Trim() == "" || strPII.Trim() == "" || strStatus.Trim() == "")
                {

                    if (strSupplier.Length == 0)
                    {
                        logwriter("Supplier Information not found");
                    }
                    else if (strISBN.Length == 0)
                    {
                        logwriter("ISBN Information not found");
                    }
                    else if (strPII.Length == 0)
                    {
                        logwriter("PII Information not found");
                    }
                    else if (strStatus.Length == 0)
                    {
                        logwriter("Status Information not found");
                    }


                    return false;
                }
                return true;
            }
            catch (Exception exec)
            {
                strStatus = "";
                strPII = "";
                strStatus = "";
                strLink = "";
                strRemarks = "";
                strISBN = "";
                strSupplier = "";
                logwriter("Exception in reading xml file for PC process :: " + exec.Message.ToString());
                return false;
            }

        }

        public string Get_Book_Title(string strISBN)
        {
            string strTitle = "";
            strISBN = strISBN.Replace("-", "");
            string strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            if (!(Directory.Exists(strPPMPath)))
            {
                strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            }
            if (Directory.Exists(strPPMPath))
            {
                DirectoryInfo[] DInfo = new DirectoryInfo(strPPMPath).GetDirectories();
                for (int i = 0; i < DInfo.Length; i++)
                {
                    if (Directory.Exists(DInfo[i].FullName + "\\Current_order"))
                    {
                        FileInfo[] FInfo = new DirectoryInfo(DInfo[i].FullName + "\\Current_order").GetFiles();
                        for (int j = 0; j < FInfo.Length; j++)
                        {
                            if (FInfo[j].Name.ToLower().StartsWith("kup") && FInfo[j].Name.ToLower().EndsWith("xml"))
                            {
                                StreamReader sr = new StreamReader(FInfo[j].FullName);
                                string filecon = sr.ReadToEnd();
                                sr.Close();
                                strTitle = GetInformation(filecon, "book-title");
                                break;
                            }
                        }
                        if (strTitle.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }
            return strTitle;
        }
        public string Get_PPMOrder_Authorname(string strISBN)
        {
            string Authorname = "";
            // string strTitle = "";
            strISBN = strISBN.Replace("-", "");
            string strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            if (!(Directory.Exists(strPPMPath)))
            {
                strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            }
            if (Directory.Exists(strPPMPath))
            {
                DirectoryInfo[] DInfo = new DirectoryInfo(strPPMPath).GetDirectories();
                for (int i = 0; i < DInfo.Length; i++)
                {
                    if (Directory.Exists(DInfo[i].FullName + "\\Current_order"))
                    {
                        FileInfo[] FInfo = new DirectoryInfo(DInfo[i].FullName + "\\Current_order").GetFiles();
                        for (int j = 0; j < FInfo.Length; j++)
                        {
                            if (FInfo[j].Name.ToLower().StartsWith("kup") && FInfo[j].Name.ToLower().EndsWith("xml"))
                            {
                                List<KeyValuePair<int, string>> dicmain = new List<KeyValuePair<int, string>>();
                                StreamReader sr = new StreamReader(FInfo[j].FullName);
                                string filecon = sr.ReadToEnd();
                                sr.Close();
                                // strTitle = GetInformation(filecon, "book-title");
                                string tempallauthor = "";

                                int checkBInfoPos = filecon.IndexOf("<book-info");
                                if (checkBInfoPos == -1)
                                    checkBInfoPos = 0;



                                int sop = filecon.IndexOf("<originators>", checkBInfoPos);
                                if (sop != -1)
                                {
                                    int esop = filecon.IndexOf("</originators>", sop);
                                    tempallauthor = filecon.Substring(sop, esop - sop + "</originators>".Length);


                                    int ssop = tempallauthor.IndexOf("<originator ", 0);
                                    while (ssop != -1)
                                    {
                                        int essop = tempallauthor.IndexOf(">", ssop);

                                        int eessop = tempallauthor.IndexOf("</originator>", essop);
                                        string temporiginator = tempallauthor.Substring(ssop, eessop - ssop + "</originator>".Length);
                                        // for sort order
                                        string shortorder = "";
                                        int sortpos = temporiginator.IndexOf("sort-order=\"", 0);
                                        if (sortpos != -1)
                                        {
                                            int esortpos = temporiginator.IndexOf("\"", sortpos + "sort-order=\"".Length);
                                            shortorder = temporiginator.Substring(sortpos + "sort-order=\"".Length, esortpos - sortpos - "sort-order=\"".Length);
                                        }
                                        // for type
                                        string origtype = "";
                                        int typesop = temporiginator.IndexOf("<originator-type>", 0);
                                        if (typesop != -1)
                                        {
                                            int etypesop = temporiginator.IndexOf("</originator-type>", typesop);
                                            origtype = temporiginator.Substring(typesop + "<originator-type>".Length, etypesop - typesop - "<originator-type>".Length);
                                        }
                                        if (origtype.ToLower() == "author")
                                        //if (origtype.ToLower() == "series volume editor")
                                        {
                                            // for first name
                                            string firstname = "";
                                            int fnamesop = temporiginator.IndexOf("<first-name>", 0);
                                            if (fnamesop != -1)
                                            {
                                                int efnamesop = temporiginator.IndexOf("</first-name>", fnamesop);
                                                firstname = temporiginator.Substring(fnamesop + "<first-name>".Length, efnamesop - fnamesop - "<first-name>".Length);
                                            }
                                            // for last name
                                            string lastname = "";
                                            int lnamesop = temporiginator.IndexOf("<last-name>", 0);
                                            if (lnamesop != -1)
                                            {
                                                int elnamesop = temporiginator.IndexOf("</last-name>", lnamesop);
                                                lastname = temporiginator.Substring(lnamesop + "<last-name>".Length, elnamesop - lnamesop - "<last-name>".Length);
                                            }
                                            dicmain.Add(new KeyValuePair<int, string>(Convert.ToInt32(shortorder), firstname + " " + lastname));
                                        }
                                        ssop = tempallauthor.IndexOf("<originator ", ssop + "<originator ".Length);
                                    }
                                }

                                var items1 = from pair in dicmain
                                             orderby pair.Key ascending
                                             select pair;

                                foreach (KeyValuePair<int, string> pair in items1)
                                {

                                    Authorname = pair.Value;
                                    break;
                                }


                                break;
                            }
                        }
                        if (Authorname.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }
            return Authorname;
        }
        public string Get_PPMOrder_editorname(string strISBN)
        {
            string Editorname = "";
            // string strTitle = "";
            strISBN = strISBN.Replace("-", "");
            string strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            if (!(Directory.Exists(strPPMPath)))
            {
                strPPMPath = @"\\172.16.28.2\Elsinpt\ElsBook\Orders\PPM\" + strISBN;
            }
            if (Directory.Exists(strPPMPath))
            {
                DirectoryInfo[] DInfo = new DirectoryInfo(strPPMPath).GetDirectories();
                for (int i = 0; i < DInfo.Length; i++)
                {
                    if (Directory.Exists(DInfo[i].FullName + "\\Current_order"))
                    {
                        FileInfo[] FInfo = new DirectoryInfo(DInfo[i].FullName + "\\Current_order").GetFiles();
                        for (int j = 0; j < FInfo.Length; j++)
                        {
                            if (FInfo[j].Name.ToLower().StartsWith("kup") && FInfo[j].Name.ToLower().EndsWith("xml"))
                            {
                                List<KeyValuePair<int, string>> dicmain = new List<KeyValuePair<int, string>>();
                                StreamReader sr = new StreamReader(FInfo[j].FullName);
                                string filecon = sr.ReadToEnd();
                                sr.Close();
                                // strTitle = GetInformation(filecon, "book-title");
                                string tempallauthor = "";

                                int checkBInfoPos = filecon.IndexOf("<book-info");
                                if (checkBInfoPos == -1)
                                    checkBInfoPos = 0;

                                int sop = filecon.IndexOf("<originators>", checkBInfoPos);
                                if (sop != -1)
                                {
                                    int esop = filecon.IndexOf("</originators>", sop);
                                    tempallauthor = filecon.Substring(sop, esop - sop + "</originators>".Length);


                                    int ssop = tempallauthor.IndexOf("<originator ", 0);
                                    while (ssop != -1)
                                    {
                                        int essop = tempallauthor.IndexOf(">", ssop);

                                        int eessop = tempallauthor.IndexOf("</originator>", essop);
                                        string temporiginator = tempallauthor.Substring(ssop, eessop - ssop + "</originator>".Length);
                                        // for sort order
                                        string shortorder = "";
                                        int sortpos = temporiginator.IndexOf("sort-order=\"", 0);
                                        if (sortpos != -1)
                                        {
                                            int esortpos = temporiginator.IndexOf("\"", sortpos + "sort-order=\"".Length);
                                            shortorder = temporiginator.Substring(sortpos + "sort-order=\"".Length, esortpos - sortpos - "sort-order=\"".Length);
                                        }
                                        // for type
                                        string origtype = "";
                                        int typesop = temporiginator.IndexOf("<originator-type>", 0);
                                        if (typesop != -1)
                                        {
                                            int etypesop = temporiginator.IndexOf("</originator-type>", typesop);
                                            origtype = temporiginator.Substring(typesop + "<originator-type>".Length, etypesop - typesop - "<originator-type>".Length);
                                        }
                                        if (origtype.ToLower().IndexOf("editor") != -1)
                                        {
                                            // for first name
                                            string firstname = "";
                                            int fnamesop = temporiginator.IndexOf("<first-name>", 0);
                                            if (fnamesop != -1)
                                            {
                                                int efnamesop = temporiginator.IndexOf("</first-name>", fnamesop);
                                                firstname = temporiginator.Substring(fnamesop + "<first-name>".Length, efnamesop - fnamesop - "<first-name>".Length);
                                            }
                                            // for last name
                                            string lastname = "";
                                            int lnamesop = temporiginator.IndexOf("<last-name>", 0);
                                            if (lnamesop != -1)
                                            {
                                                int elnamesop = temporiginator.IndexOf("</last-name>", lnamesop);
                                                lastname = temporiginator.Substring(lnamesop + "<last-name>".Length, elnamesop - lnamesop - "<last-name>".Length);
                                            }
                                            dicmain.Add(new KeyValuePair<int, string>(Convert.ToInt32(shortorder), firstname + " " + lastname));
                                        }
                                        ssop = tempallauthor.IndexOf("<originator ", ssop + "<originator ".Length);
                                    }
                                }

                                var items1 = from pair in dicmain
                                             orderby pair.Key ascending
                                             select pair;

                                foreach (KeyValuePair<int, string> pair in items1)
                                {

                                    Editorname = pair.Value;
                                    break;
                                }


                                break;
                            }
                        }
                        if (Editorname.Length > 0)
                        {
                            break;
                        }
                    }
                }
            }
            return Editorname;
        }

        private bool ElsMCQSubmition(string strFile, out string Error, MCSignal mcSignal)
        {

            string DBError = "";
            try
            {
                string MCQTemplate = @"D:\FloorProblem\PC_ElsBooks_Setup2.0\MCQTemplate.htm";
                FileInfo FI = new FileInfo(strFile);
                if (!(File.Exists(MCQTemplate)))
                {
                    Error = "Unable to find MC template " + strFile;
                    return false;
                }
                StreamReader srat = new StreamReader(MCQTemplate);
                string strfcat = srat.ReadToEnd();
                srat.Close();

                string strTitle = "";
                strTitle = Get_Book_Title(mcSignal.isbn);
                strTitle = strTitle.Replace("\r\n", "");
                strTitle = strTitle.Replace("\n", "");
                string chepter = mcSignal.pii.Substring(mcSignal.pii.Length - 7, 5);

                string strMsg = strfcat;
                //string signalLink = "ftp://ftp.elsevierproofcentral.com/Signals/" + FI.Name.ToString();

                string PIII = mcSignal.pii;

                strMsg = strMsg.Replace("[BT]", strTitle);
                strMsg = strMsg.Replace("[ChapterNo]", chepter);
                strMsg = strMsg.Replace("[PCURL]", mcSignal.url);
                string Subject = "";

                Subject = "PM Link received for " + strTitle + ": Chapter " + chepter;


                string[] attachment = new string[] { };
                string mailto = "deepak.verma@digiscapetech.com";
                string mailcc = "deepak.verma@digiscapetech.com";
                string mailbcc = "deepak.verma@digiscapetech.com";

                // Raushan commented on 7 oct 2016
                //was
                //if (obj.SendMailMy(mailto, Subject, strMsg, mailcc, mailbcc, "eproof@elsevier.thomsondigital.com", attachment))
                //{
                //    DBError = "Mail Send for MC signal to " + FI.Name.ToString() + " to " + GlobalVar.GetTdPMTO;
                //    //GlobalVar.logwriter("Signal", "Mail Send for MC signal to " + FI.Name.ToString() + " to " + GlobalVar.GetTdPMTO, "INFO");
                //    if (!File.Exists((strspath + "\\" + FI.Name)))
                //    { 
                //    File.Move(strFile, strspath + "\\" + FI.Name);
                //    }
                //}
                //else
                //{
                //    DBError = "E2:Unable to send mail for MC signal to " + FI.Name.ToString();
                //    //GlobalVar.logwriter("Signal", "Unable to send mail for MC signal to " + FI.Name.ToString(), "ERROR");
                //    File.Move(strFile, strfpath + "\\" + FI.Name);
                //}



                // please comment below line 
                if (File.Exists(strFile))
                {
                    //File.Move(strFile, strspath + "\\" + FI.Name);
                }
                //////////////////////////////////////////////////////////////////

                ////GlobalVar.Update_Signal(mcSignal.pii, FI.Name, mcSignal.isbn, mcSignal.url, DBError);
                Error = "";
                return true;
            }
            catch (Exception exc)
            {
                Error = exc.Message.ToString();
                ////GlobalVar.logwriter("Signal", "E5: " + Error, "ERROR");
                return false;
            }
        }

        private bool PCProcessRoundTwo(string strFile)
        {
            string strSupplier, strISBN, strPII, strStatus, strLink, strRemarks;
            strSupplier = strISBN = strPII = strStatus = strLink = strRemarks = "";
            string strduedate = "";
            string errStatus = "";
            FileInfo FI = new FileInfo(strFile);
            bool boolVal = Read_Information_From_XML(strFile, out strSupplier, out strISBN, out strPII, out strStatus, out strLink, out strRemarks);
            if (boolVal == true)
            {
                string author_editorname = "";
                string strauthorname = Get_PPMOrder_Authorname(strISBN);
                string streditorname = Get_PPMOrder_editorname(strISBN);

                if (strauthorname.Trim() == "")
                {
                    author_editorname = streditorname;
                }
                else
                {
                    author_editorname = strauthorname;
                }

                string strSubject = "";
                string strTitle = "";
                strTitle = Get_Book_Title(strISBN);
                string chepter = strPII.Substring(strPII.Length - 7, 5);
                string PMTempR2 = "";
                StreamReader srat = new StreamReader(PMTempR2);
                string strfcat = srat.ReadToEnd();
                srat.Close();

                SqlParameter[] sqlinputparm = new SqlParameter[1];
                sqlinputparm[0] = new SqlParameter("PII", strPII);
                DataSet DS = null; // DataAccess.ExecuteDataSetSP(SPNames.GetpcRoundTwo, sqlinputparm);

                string MCLink = "";
                string strTo = "";
                string strCC = "";
                string chapterTitle = "";
                string chplbl = "";
                string DBBCC = "";
                if (DS.Tables[0].Rows.Count > 0 && DS != null)
                {
                    DBBCC = DS.Tables[0].Rows[0]["BCCMail"] != DBNull.Value ? DS.Tables[0].Rows[0]["BCCMail"].ToString() : "";
                    chplbl = DS.Tables[0].Rows[0]["ChapLbl"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapLbl"].ToString() : "";
                    chapterTitle = DS.Tables[0].Rows[0]["ChapTitle"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapTitle"].ToString() : "";
                    strTo = DS.Tables[0].Rows[0]["MailTo"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailTo"].ToString() : "";
                    strCC = DS.Tables[0].Rows[0]["MailCC"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailCC"].ToString() : "";
                    MCLink = DS.Tables[0].Rows[0]["URL"] != DBNull.Value ? DS.Tables[0].Rows[0]["URL"].ToString() : "";
                    //GlobalVar.logwriter("Signal Info[PCProcessRoundTwo]", chapterTitle.Trim() + "||" + strTo.Trim() + "||" + strCC.Trim() + "||" + MCLink.Trim(), "INFO");
                }
                // tag replace in chapter label or title


                int sop = chplbl.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chplbl.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chplbl = chplbl.Remove(sop, esop - sop + 1);
                    }
                    sop = chplbl.IndexOf("<", 0);
                }
                /////////////////
                // tag replace in chapter title
                sop = chapterTitle.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chapterTitle.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chapterTitle = chapterTitle.Remove(sop, esop - sop + 1);
                    }
                    sop = chapterTitle.IndexOf("<", 0);
                }

                //Mail_Class obj = new Mail_Class();
                if (strStatus.Trim().ToLower() == "success")
                {
                    bool DbData = true;
                    if (chplbl.Trim() == "")
                    {
                        if (strTitle.Length > 0)
                        {
                            strSubject = "Revised proof of " + strTitle + ": " + author_editorname + ": Chapter " + Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5));
                        }
                        else
                        {
                            strSubject = "Revised proof of [" + strISBN + "]: Chapter " + Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5));
                        }
                    }
                    else
                    {
                        if (strTitle.Length > 0)
                        {
                            strSubject = "Revised proof of " + strTitle + ": " + author_editorname + ": " + chplbl;
                        }
                        else
                        {
                            strSubject = "Revised proof of [" + strISBN + "]: " + chplbl;
                        }
                    }

                    if (strTitle.Trim() == "" || chapterTitle.Trim() == "" || strTo.Trim() == "" || strCC.Trim() == "" || MCLink.Trim() == "" || author_editorname.Trim() == "")
                    {
                        DbData = false;
                    }
                    string strMsg = strfcat;

                    if (DbData == true)
                    {
                        strMsg = strMsg.Replace("[PCURL]", strLink);
                        strMsg = strMsg.Replace("[BT]", strTitle);
                        strMsg = strMsg.Replace("[CT]", chapterTitle);
                        strMsg = strMsg.Replace("[MCLINK]", MCLink);
                        //GlobalVar.logwriter("Signal", "Information found in database to send PC2-Mail to ELS PM ", "INFO");

                        string[] attachment = null;
                        //if (File.Exists(strspath + "\\" + FI.Name))
                        //{
                        //    File.Delete(strFile);
                        //}
                        //if (!File.Exists(strspath + "\\" + FI.Name))
                        //{
                        //    if (obj.SendMailMy(strTo, strSubject, strMsg, strCC, GlobalVar.GetTdBCC + ";" + DBBCC, "eproof@elsevier.thomsondigital.com", attachment))
                        //    {
                        //        File.Move(strFile, strspath + "\\" + FI.Name);
                        //        //GlobalVar.logwriter("Signal", "PC2-Mail is send to ELS PM for " + FI.Name, "INFO");
                        //        errStatus = "Data is found and mail is send to " + strTo;
                        //    }
                        //    else
                        //    {

                        //        //obj.SendMailMy(GlobalVar.GetTdPMTO, strSubject, strMsg, GlobalVar.GetTdPMCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment);
                        //        //GlobalVar.logwriter("Signal", "PC2-Mail is not send to ELS PM for " + FI.Name, "ERROR");
                        //        errStatus = "E2:Data is found but unable to send mail " + strTo;
                        //        File.Move(strFile, strfpath + "\\" + FI.Name);
                        //    }
                        //}
                    }
                    else
                    {
                        //GlobalVar.logwriter("Signal", "information missing to send Main PM mail for " + FI.Name, "ERROR");
                        if (strLink.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[PCURL]", strLink);
                        }
                        if (MCLink.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[MCLINK]", MCLink);
                        }
                        if (strTitle.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[BT]", strTitle);
                        }
                        if (chapterTitle.Trim() != "")
                        {
                            strMsg = strMsg.Replace("[CT]", chapterTitle);
                        }
                        string[] attachment = null;

                        // 14 Apr 17
                        //if (strTo != "")
                        //{
                        //    if (strCC != "")
                        //    {
                        //        obj.SendMailMy(strTo, strSubject, strMsg, strCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment);
                        //    }
                        //    else
                        //    {
                        //        obj.SendMailMy(strTo, strSubject, strMsg, GlobalVar.GetTdPMCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment);
                        //    }
                        //}
                        //if (File.Exists(strspath + "\\" + FI.Name))
                        //{
                        //    File.Delete(strFile);
                        //}
                        //if (!File.Exists(strspath + "\\" + FI.Name))
                        //{
                        //    if (obj.SendMailMy(GlobalVar.GetTdPMTO, strSubject, strMsg, GlobalVar.GetTdPMCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment))
                        //    {
                        //        //GlobalVar.logwriter("Signal", "PC2-Mail is send to local PM for " + strFile, "INFO");
                        //        errStatus = "E1:Data is  not found and mail is send to " + GlobalVar.GetTdPMTO;
                        //        File.Move(strFile, strspath + "\\" + FI.Name);
                        //    }
                        //    else
                        //    {
                        //        //GlobalVar.logwriter("Signal", "Unable to send PC2-Mail is send to local PM for " + strFile, "INFO");
                        //        errStatus = "E2:Data is found not found and unble to send mail to " + GlobalVar.GetTdPMTO;
                        //        File.Move(strFile, strfpath + "\\" + FI.Name);
                        //    }
                        //}
                    }
                }
                else
                {
                    //Failed signal  

                    //string Subject = "Revised proof central Fail description for [ " + strISBN + "_" + strPII + " ]";
                    //StreamReader ErrCont = new StreamReader(FAILTemp);
                    //string htmlbody = ErrCont.ReadToEnd();
                    //srat.Close();
                    //htmlbody = htmlbody.Replace("[DATE]", DateTime.Now.ToString("dd-MM-yyyy"));
                    //htmlbody = htmlbody.Replace("[ISBN]", strISBN);
                    //htmlbody = htmlbody.Replace("[PII]", strPII);
                    //htmlbody = htmlbody.Replace("[ERROR]", strRemarks);
                    //string[] attachment = new string[] { strFile };
                    //if (obj.SendMailMy(ErrorMailTo, Subject, htmlbody, ErrorMailCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment))
                    //{
                    //    //GlobalVar.logwriter("Signal_Process", "Mail send.Failed information found in PC signal received." + strFile, "Success");
                    //    errStatus = "E1:Failed information found in xml and mail is send to " + GlobalVar.GetTdPMTO;
                    //}
                    //else
                    //{
                    //    //GlobalVar.logwriter("Signal_Process", "Mail sening failed.Failed information found in PC signal received." + strFile, "Success");
                    //    errStatus = "E2:Failed information found in xml but unable to send to " + GlobalVar.GetTdPMTO;
                    //}
                    //File.Move(strFile, strfpath + "\\" + FI.Name);
                }
                //GlobalVar.Update_Signal(strPII, FI.Name, strISBN, strLink, errStatus);
            }
            else
            {
                //string[] attachment = new string[] { strFile };
                //string body = FailBody("Round 2 unable to extract information for " + FI.Name);
                //Mail_Class obj = new Mail_Class();

                //if (obj.SendMailMy(ErrorMailTo, "Revised Proof central fail description for [ " + strISBN + "_" + strPII + " ]", body, ErrorMailCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment))
                //{
                //    errStatus = "E1:Round 2  Mail send when information is not found in database for " + strFile;
                //    //GlobalVar.logwriter("Signal_Process", "Round 2  Mail send when information is not found in xml for " + strFile, "Success");
                //}
                //else
                //{
                //    errStatus = "E2:Round 2  Mail send when information is not found in database for " + strFile;
                //    //GlobalVar.logwriter("Signal_Process", "Error :Round 2 Unable to send mail when information is not found in xml for " + strFile, "Error");
                //}
                //File.Move(strFile, strfpath + "\\" + FI.Name);
                //GlobalVar.Update_Signal(strPII, FI.Name, strISBN, strLink, errStatus);
            }
            return true;
        }//                                 >>>--------------->     Tested   >>>----------------->


        private bool AuthorProcess(string strFile, out string Error, AuthorSignal ASignal)
        {
            try
            {
                bool hasValue = true;
                string DbError = "";
                FileInfo FI = new FileInfo(strFile);
                if (!(File.Exists(strFile)))
                {
                    Error = "Unable to find author submission template for PM " + strFile;
                    return false;
                }

                string strPII = "";
                strPII = ASignal.pii.Trim();
                SqlParameter[] sqlinputparm = new SqlParameter[1];
                sqlinputparm[0] = new SqlParameter("PII", strPII);

                DataSet DS = ExecuteDataSetSP("usp_GetInternalInfo", sqlinputparm);  // DataAccess.ExecuteDataSetSP(SPNames.GetInternalInfo, sqlinputparm);
                string strTo = "";
                string strCC = "";
                string DBBCC = "";
                string chplbl = "";
                string chapterTitle = "";
                string branchType = "";
                string strfcat = "";
                string ftpdetails = ""; 
                string Laung = ""; 
                string pmName = "";
                string strTitle = Get_Book_Title(ASignal.isbn.Trim());
                strTitle = strTitle.Replace("\r\n", "");
                strTitle = strTitle.Replace("\n", "");
                string author_editorname = "";
                string strauthorname = Get_PPMOrder_Authorname(ASignal.isbn.Trim());
                string streditorname = Get_PPMOrder_editorname(ASignal.isbn.Trim());

                if (strauthorname.Trim() == "")
                {
                    author_editorname = streditorname;
                }
                else
                {
                    author_editorname = strauthorname;
                }
                if (DS.Tables[0].Rows.Count > 0 && DS != null)
                {

                    chplbl = DS.Tables[0].Rows[0]["ChapLbl"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapLbl"].ToString() : "";
                    strTo = DS.Tables[0].Rows[0]["MailTO"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailTO"].ToString() : "";
                    strCC = DS.Tables[0].Rows[0]["MailCC"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailCC"].ToString() : "";
                    chapterTitle = DS.Tables[0].Rows[0]["ChapTitle"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapTitle"].ToString() : "";
                    branchType = DS.Tables[0].Rows[0]["BranchType"] != DBNull.Value ? DS.Tables[0].Rows[0]["BranchType"].ToString() : "";
                    ftpdetails = DS.Tables[0].Rows[0]["FtpDetails"] != DBNull.Value ? DS.Tables[0].Rows[0]["FtpDetails"].ToString() : "";
                    pmName = DS.Tables[0].Rows[0]["ElsPMName"] != DBNull.Value ? DS.Tables[0].Rows[0]["ElsPMName"].ToString() : "";
                    DBBCC = DS.Tables[0].Rows[0]["BCCMail"] != DBNull.Value ? DS.Tables[0].Rows[0]["BCCMail"].ToString() : "";
                    Laung = DS.Tables[0].Rows[0]["Laung"] != DBNull.Value ? DS.Tables[0].Rows[0]["Laung"].ToString() : ""; 
                    //BranchType
                }
                //string Subject = "Author Submission of " + strTitle + ": " + author_editorname + ": " + chplbl + "";
                string Subject = "";
                if (branchType.ToLower() == "mrw")
                {
                    if (ftpdetails == "2")
                    {
                        DataTable dt = GetMailTemplate("MRWAU2.2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                    }
                    if (ftpdetails == "3")
                    {
                        DataTable dt = GetMailTemplate("MRWAU3.2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                    }
                }
                if (branchType.ToLower() == "snt")
                {
                    if (ftpdetails == "2")
                    {
                        DataTable dt = GetMailTemplate("SNTAU2.2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                    }
                    if (ftpdetails == "3")
                    {
                        DataTable dt = GetMailTemplate("SNTAU3.2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                    }
                }
                if (branchType.ToLower() == "ehs")
                {
                    if (Laung.ToLower() == "english")
                    {
                        if (ftpdetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTAU3.2.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpdetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTAU3.2.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (Laung.ToLower() == "french")
                    {
                        if (ftpdetails == "2")
                        {
                            DataTable dt = GetMailTemplate("EHSFrenchED3.1");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpdetails == "3")
                        {
                            DataTable dt = GetMailTemplate("EHSFrenchED3.1");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (Laung.ToLower() == "german")
                    {
                        if (ftpdetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTAU3.2.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpdetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTAU3.2.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }

                }

                string strMsg = strfcat;
                string signalLink = "ftp://ftp.elsevierproofcentral.com/Signals/" + FI.Name.ToString();
                string PIII = ASignal.pii;
                strMsg = strMsg.Replace("[PM]", pmName.Trim());
                strMsg = strMsg.Replace("[ISBN]", ASignal.isbn.Trim());
                strMsg = strMsg.Replace("[PII]", ASignal.pii);
                strMsg = strMsg.Replace("[LINK]", ASignal.url);
                strMsg = strMsg.Replace("[FILENAME]", ASignal.zip_name);
                strMsg = strMsg.Replace("[SIGNALLINK]", signalLink);
                strMsg = strMsg.Replace("[Inst]", ASignal.instructions);

                strMsg = strMsg.Replace("[Totalattachmnt]", ASignal.attachments);
                strMsg = strMsg.Replace("[AttachmenttoQuery]", ASignal.attachments_to_queries);
                strMsg = strMsg.Replace("[QueryResponse]", ASignal.queries);
                strMsg = strMsg.Replace("[Rject]", ASignal.rejected_ce_changes);
                strMsg = strMsg.Replace("[Edit]", ASignal.edits);
                strMsg = strMsg.Replace("[Graphics]", ASignal.instructions_on_graphics);

                // Created on 19-09-2016

                strMsg = strMsg.Replace("[FMEditArticleTitle]", ASignal.article_title_edits);
                strMsg = strMsg.Replace("[FMCommentArticleTitle]", ASignal.article_title_comments);
                strMsg = strMsg.Replace("[FMEditAutGrp]", ASignal.author_group_edits);
                strMsg = strMsg.Replace("[FMCommentAutGrp]", ASignal.author_group_comments);
                strMsg = strMsg.Replace("[FMEditFAut]", ASignal.first_author_edits);
                strMsg = strMsg.Replace("[FMCommentAut]", ASignal.first_author_comments);
                strMsg = strMsg.Replace("[FMEditCAut]", ASignal.corresponding_author_edits);
                strMsg = strMsg.Replace("[FMCommentCAut]", ASignal.corresponding_author_comments);
                strMsg = strMsg.Replace("[FMEditCField]", ASignal.correspondence_field_edits);
                strMsg = strMsg.Replace("[FMCommentCField]", ASignal.correspondence_field_comments);
                strMsg = strMsg.Replace("[ZipName]", Path.GetFileName(ASignal.zip_name).Replace(".xml", ".zip"));
                if (ASignal.AuthorQuery != "")
                {
                    strMsg = strMsg.Replace("[Queryresponse]", ASignal.AuthorQuery);
                }
                string strfpath = "";
                string strspath = "";
                string zippath = "";
                FileInfo FIe = new FileInfo(strFile);
                if (File.Exists(downloadedpath + "\\" + ASignal.zip_name))
                {
                    zippath = downloadedpath + "\\" + ASignal.zip_name;
                }
                else if (File.Exists(FIe.DirectoryName + "\\" + ASignal.zip_name))
                {
                    zippath = FIe.DirectoryName + "\\" + ASignal.zip_name;
                }

                string[] attachment = new string[] { strFile, zippath };
                // tag replace in chapter label or title
                int sop = chplbl.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chplbl.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chplbl = chplbl.Remove(sop, esop - sop + 1);
                    }
                    sop = chplbl.IndexOf("<", 0);
                }
                /////////////////
                // tag replace in chapter title
                sop = chapterTitle.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chapterTitle.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chapterTitle = chapterTitle.Remove(sop, esop - sop + 1);
                    }
                    sop = chapterTitle.IndexOf("<", 0);
                }
                
                if (strTo.Trim() == "" || strCC.Trim() == "" || chplbl.Trim() == "" || chapterTitle.Trim() == "" || strPII.Trim() == "" || strTitle == "" || author_editorname.Trim() == "")
                {
                    hasValue = false;
                }
                strMsg = strMsg.Replace("[BT]", strTitle);
                string chepterNO = strPII.Substring(strPII.Length - 7, 5);
                strMsg = strMsg.Replace("[CHAPTERTITLE]", chapterTitle.Trim());
                strMsg = strMsg.Replace("[CHAPTERNUM]", chplbl);
                if (hasValue == true)
                {

                    string signalStatus = "Success";
                    logwriter("Information found in database to send author mail ");
                    SendMail(strTo, strCC, DBBCC, Subject, strMsg, attachment);
                     DbError = "PCProcess:Mail send succesfully for " + FI.Name.ToString() + " to " + strTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DbError, signalStatus, "AU");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }
                else
                {
                    logwriter("Information found in database to send author mail ");
                    string signalStatus = "Failed";
                    SendMail(DBBCC, DBBCC, DBBCC, Subject, strMsg, attachment);
                     DbError = "Mail send succesfully to internal team  " + FI.Name.ToString() + " to " + strTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DbError, signalStatus, "AU");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }

                //GlobalVar.Update_Signal(PIII, FI.Name, ASignal.isbn, ASignal.url, DbError);
                Error = "";
                return true;
            }
            catch (Exception exc)
            {
                Error = exc.Message.ToString();
                return false;
            }
        }   //tested

        private bool EditorProcessPM(string strFile, out string Error, EditorSignal ASignal)
        {
            try
            {
                string strElsETPM = "";
                bool hasValue = true;
                string DBError = "";
                FileInfo FI = new FileInfo(strFile);
                if (!(File.Exists(strFile)))
                {
                    Error = "Unable to find Editor PM template " + strFile;
                    return false;
                }

                string strPII = "";
                strPII = ASignal.pii.Trim();
                SqlParameter[] sqlinputparm = new SqlParameter[1];
                sqlinputparm[0] = new SqlParameter("PII", strPII);

                DataSet DS = ExecuteDataSetSP("usp_GetInternalInfo", sqlinputparm);  // DataAccess.ExecuteDataSetSP(SPNames.GetInternalInfo, sqlinputparm);
                string strTo = "";
                string strCC = "";
                string chplbl = "";
                string DBBCC = "";
                string chapterTitle = "";
                string branchType = "";
                string strfcat = "";
                string ftpDetails = ""; 
                string Laung = ""; 
                string pmName = "";
                string author_editorname = "";
                string strauthorname = Get_PPMOrder_Authorname(ASignal.isbn.Trim());
                string streditorname = Get_PPMOrder_editorname(ASignal.isbn.Trim());

                if (strauthorname.Trim() == "")
                {
                    author_editorname = streditorname;
                }
                else
                {
                    author_editorname = strauthorname;
                }

                string strTitle = Get_Book_Title(ASignal.isbn.Trim());
                if (DS.Tables[0].Rows.Count > 0 && DS != null)
                {
                    chplbl = DS.Tables[0].Rows[0]["ChapLbl"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapLbl"].ToString() : "";
                    strTo = DS.Tables[0].Rows[0]["MailTO"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailTO"].ToString() : "";
                    strCC = DS.Tables[0].Rows[0]["MailCC"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailCC"].ToString() : "";
                    chapterTitle = DS.Tables[0].Rows[0]["ChapTitle"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapTitle"].ToString() : "";
                    branchType = DS.Tables[0].Rows[0]["BranchType"] != DBNull.Value ? DS.Tables[0].Rows[0]["BranchType"].ToString() : "";
                    ftpDetails = DS.Tables[0].Rows[0]["FtpDetails"] != DBNull.Value ? DS.Tables[0].Rows[0]["FtpDetails"].ToString() : "";
                    pmName = DS.Tables[0].Rows[0]["ElsPMName"] != DBNull.Value ? DS.Tables[0].Rows[0]["ElsPMName"].ToString() : "";
                    DBBCC = DS.Tables[0].Rows[0]["BCCMail"] != DBNull.Value ? DS.Tables[0].Rows[0]["BCCMail"].ToString() : "";
                    Laung = DS.Tables[0].Rows[0]["Laung"] != DBNull.Value ? DS.Tables[0].Rows[0]["Laung"].ToString() : ""; 
                }
                string Subject = "";
                if (branchType.ToLower() == "mrw")
                {
                    if (ftpDetails == "2")
                    {
                        DataTable dt = GetMailTemplate("MRWED2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                    }
                    if (ftpDetails == "3")
                    {
                        DataTable dt = GetMailTemplate("MRWED2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                    }
                }
                if (branchType.ToLower() == "snt")
                {
                    if (ftpDetails == "2")
                    {
                        DataTable dt = GetMailTemplate("SNTED2.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                    }
                    if (ftpDetails == "3")
                    {
                        DataTable dt = GetMailTemplate("SNTED3.0");
                        strfcat = dt.Rows[0]["MailBody"].ToString();
                        Subject = dt.Rows[0]["MailSubject"].ToString();
                        Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                    }
                }
                if (branchType.ToLower() == "ehs")
                {
                    if (Laung.ToLower() == "english")
                    {
                        if (ftpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTED3.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTED3.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (Laung.ToLower() == "french")
                    {
                        if (ftpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("EHSFrenchED3.1");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("EHSFrenchED3.1");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (Laung.ToLower() == "german")
                    {
                        if (ftpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTED3.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTED3.0");
                            strfcat = dt.Rows[0]["MailBody"].ToString();
                            Subject = dt.Rows[0]["MailSubject"].ToString();
                            Subject = Subject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }

                }


                string strMsg = strfcat;
                string signalLink = "ftp://ftp.elsevierproofcentral.com/Signals/" + FI.Name.ToString();

                string PIII = ASignal.pii;
                //string chepter = PIII.Substring(PIII.Length - 7, 5);               

                /* int no;
                 if (Int32.TryParse(chepter, out no))
                 {
                     chepter = no.ToString();
                 }*/

                strMsg = strMsg.Replace("[PM]", pmName);
                strMsg = strMsg.Replace("[ISBN]", ASignal.isbn);
                strMsg = strMsg.Replace("[PII]", ASignal.pii);
                strMsg = strMsg.Replace("[LINK]", ASignal.url);
                strMsg = strMsg.Replace("[FILENAME]", ASignal.zip_name);
                strMsg = strMsg.Replace("[SIGNALLINK]", signalLink);
                strMsg = strMsg.Replace("[Inst]", ASignal.instructions);
                strMsg = strMsg.Replace("[Totalattachmnt]", ASignal.attachments);
                strMsg = strMsg.Replace("[AttachmenttoQuery]", ASignal.attachments_to_queries);
                strMsg = strMsg.Replace("[QueryResponse]", ASignal.queries);
                strMsg = strMsg.Replace("[Rject]", ASignal.rejected_ce_changes);
                strMsg = strMsg.Replace("[Edit]", ASignal.edits);
                strMsg = strMsg.Replace("[Graphics]", ASignal.instructions_on_graphics);
                // Created on 19-09-2016

                strMsg = strMsg.Replace("[FMEditArticleTitle]", ASignal.article_title_edits);
                strMsg = strMsg.Replace("[FMCommentArticleTitle]", ASignal.article_title_comments);
                strMsg = strMsg.Replace("[FMEditAutGrp]", ASignal.author_group_edits);
                strMsg = strMsg.Replace("[FMCommentAutGrp]", ASignal.author_group_comments);
                strMsg = strMsg.Replace("[FMEditFAut]", ASignal.first_author_edits);
                strMsg = strMsg.Replace("[FMCommentAut]", ASignal.first_author_comments);
                strMsg = strMsg.Replace("[FMEditCAut]", ASignal.corresponding_author_edits);
                strMsg = strMsg.Replace("[FMCommentCAut]", ASignal.corresponding_author_comments);
                strMsg = strMsg.Replace("[FMEditCField]", ASignal.correspondence_field_edits);
                strMsg = strMsg.Replace("[FMCommentCField]", ASignal.correspondence_field_comments);
                strMsg = strMsg.Replace("[ZipName]", Path.GetFileName(ASignal.zip_name).Replace(".xml", ".zip"));
                if (ASignal.EditorQuery != "")
                {
                    strMsg = strMsg.Replace("[Queryresponse]", ASignal.EditorQuery);
                }


                ///////////////////////

                //Mail_Class obj = new Mail_Class();
                string zippath = "";
                FileInfo FIe = new FileInfo(strFile);
                if (File.Exists(downloadedpath + "\\" + ASignal.zip_name))
                {
                    zippath = downloadedpath + "\\" + ASignal.zip_name;
                }
                else if (File.Exists(FIe.DirectoryName + "\\" + ASignal.zip_name))
                {
                    zippath = FIe.DirectoryName + "\\" + ASignal.zip_name;
                }
                string[] attachment = new string[] { strFile, zippath };


                // tag replace in chapter label or title
                int sop = chplbl.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chplbl.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chplbl = chplbl.Remove(sop, esop - sop + 1);
                    }
                    sop = chplbl.IndexOf("<", 0);
                }
                /////////////////
                // tag replace in chapter title
                sop = chapterTitle.IndexOf("<", 0);
                while (sop != -1)
                {
                    int esop = chapterTitle.IndexOf(">", sop);
                    if (esop != -1)
                    {
                        chapterTitle = chapterTitle.Remove(sop, esop - sop + 1);
                    }
                    sop = chapterTitle.IndexOf("<", 0);
                }

                

                if (strTo.Trim() == "" || strCC.Trim() == "" || chplbl.Trim() == "" || chapterTitle.Trim() == "" || strPII.Trim() == "" || strTitle == "" || author_editorname == "")
                {
                    hasValue = false;
                }

                // strTo = "sharma.a@thomsondigital.com";        //temp
                // strCC = "sachin.k@thomsondigital.com";

                // Created on 19-09-2016
                strMsg = strMsg.Replace("[BT]", strTitle);
                /////////
                strMsg = strMsg.Replace("[CHAPTERTITLE]", chapterTitle.Trim());
                strMsg = strMsg.Replace("[CHAPTERNUM]", chplbl.Trim());
                if (hasValue == true)
                {

                    string signalStatus = "Success";
                    logwriter("Information found in database to send author mail ");
                    SendMail(strTo, strCC, DBBCC, Subject, strMsg, attachment);
                    DBError = "PCProcess:Mail send succesfully for " + FI.Name.ToString() + " to " + strTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DBError, signalStatus, "ED");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }
                else
                {
                    logwriter("Information found in database to send author mail ");
                    string signalStatus = "Failed";
                    SendMail(DBBCC, DBBCC, DBBCC, Subject, strMsg, attachment);
                    DBError = "Mail send succesfully to internal team  " + FI.Name.ToString() + " to " + strTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DBError, signalStatus, "ED");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }
                //GlobalVar.Update_Signal(ASignal.pii, FI.Name, ASignal.isbn, ASignal.url, DBError);
                Error = "";
                return true;
            }
            catch (Exception exc)
            {
                Error = exc.Message.ToString();
                //GlobalVar.logwriter("Signal", "E3:" + Error, "Error");
                return false;
            }
        }    //tested 

        private bool ElsPMSubmition(string strFile, out string Error, PMSignal PMSignal)
        {
            string PMSubmissionR2 = "";
            bool R2 = false;
            string DBError = "";
            try
            {

                FileInfo FI = new FileInfo(strFile);
                if (FI.Name.Contains("PM-2"))
                {
                    R2 = true;
                }
                if (!(File.Exists(PMSubmissionR2)))
                {
                    Error = "Unable to find Editor PM template " + strFile;
                    return false;
                }
                StreamReader srat = new StreamReader(PMSubmissionR2);
                string strfcat = srat.ReadToEnd();
                srat.Close();

                string strMsg = strfcat;
                string signalLink = "ftp://ftp.elsevierproofcentral.com/Signals/" + FI.Name.ToString();

                string PIII = PMSignal.pii;
                strMsg = strMsg.Replace("[ISBN]", PMSignal.isbn);
                strMsg = strMsg.Replace("[PII]", PMSignal.pii);
                strMsg = strMsg.Replace("[LINK]", PMSignal.url);
                strMsg = strMsg.Replace("[FILENAME]", PMSignal.zip_name);
                strMsg = strMsg.Replace("[SIGNALLINK]", signalLink);
                strMsg = strMsg.Replace("[Inst]", PMSignal.instructions);
                strMsg = strMsg.Replace("[Totalattachmnt]", PMSignal.attachments);
                strMsg = strMsg.Replace("[AttachmenttoQuery]", PMSignal.attachments_to_queries);
                strMsg = strMsg.Replace("[QueryResponse]", PMSignal.queries);
                strMsg = strMsg.Replace("[Rject]", PMSignal.rejected_ce_changes);
                strMsg = strMsg.Replace("[Edit]", PMSignal.edits);
                strMsg = strMsg.Replace("[Graphics]", PMSignal.instructions_on_graphics);
                string Subject = "";
                if (R2 == true)
                {
                    Subject = "PM Submission of [" + PMSignal.isbn + "_" + PMSignal.pii + "] (Round 2)";
                }
                else
                {
                    Subject = "PM Submission of [" + PMSignal.isbn + "_" + PMSignal.pii + "]";
                }


                //Mail_Class obj = new Mail_Class();

                //string zippath = "";
                //FileInfo FIe = new FileInfo(strFile);

                //if (File.Exists(strfpath + "\\" + PMSignal.zip_name))
                //{
                //    zippath = strfpath + "\\" + PMSignal.zip_name;
                //}
                //else if (File.Exists(strspath + "\\" + PMSignal.zip_name))
                //{
                //    zippath = strspath + "\\" + PMSignal.zip_name;
                //}
                //else if (File.Exists(FIe.DirectoryName + "\\" + PMSignal.zip_name))
                //{
                //    zippath = FIe.DirectoryName + "\\" + PMSignal.zip_name;
                //}
                //string[] attachment = new string[] { strFile, zippath };
                //if (File.Exists(strspath + "\\" + FI.Name))
                //{
                //    File.Delete(strFile);
                //}
                //if (!File.Exists(strspath + "\\" + FI.Name))
                //{
                //    if (obj.SendMailMy(GlobalVar.GetTdPMTO, Subject, strMsg, GlobalVar.GetTdPMCC, GlobalVar.GetTdBCC, "eproof@elsevier.thomsondigital.com", attachment))
                //    {
                //        //GlobalVar.logwriter("Signal", "Mail Send for PM signal to " + FI.Name.ToString(), "INFO");
                //        DBError = "Mail Send for PM signal to " + FI.Name.ToString();
                //        File.Move(strFile, strspath + "\\" + FI.Name);
                //    }
                //    else
                //    {
                //        DBError = "E2:Unable to send mail for PM signal to " + FI.Name.ToString();
                //        //GlobalVar.logwriter("Signal", "Unable to send mail for PM signal to " + FI.Name.ToString(), "ERROR");
                //        File.Move(strFile, strfpath + "\\" + FI.Name);
                //    }
                //}
                //GlobalVar.Update_Signal(PMSignal.pii, FI.Name, PMSignal.isbn, PMSignal.url, DBError);
                Error = "";
                //  Errr = "";
                return true;
            }
            catch (Exception exc)
            {
                Error = exc.Message.ToString();
                return false;
            }
        }

        public bool EditorMailOnAuthorSubmition(string strFile, out string error, AuthorSignal ASignal)
        {
            try
            {
                string strETP = "";
                string DBError = "";
                FileInfo FI = new FileInfo(strFile);
                if (!(File.Exists(strFile)))
                {
                    error = "Unable to find Editor template while on author submission :: " + strFile;
                    return false;
                }


                string author_editorname = "";
                string strauthorname = Get_PPMOrder_Authorname(ASignal.isbn);
                string streditorname = Get_PPMOrder_editorname(ASignal.isbn);

                if (strauthorname.Trim() == "")
                {
                    author_editorname = streditorname;
                }
                else
                {
                    author_editorname = strauthorname;
                }

                string strTitle = Get_Book_Title(ASignal.isbn);
                strTitle = strTitle.Replace("\r\n", "");
                strTitle = strTitle.Replace("\n", "");
                if (strTitle == "")
                {
                    error = "Unable to find book title from 172.16.28.2 for file " + strFile;
                    return false;
                }
                string strPII = ASignal.pii;
                //string chepter = PIII.Substring(PIII.Length - 7, 5);

                if (strTitle == "")
                {
                    error = "Unable to find book titlt for file " + strFile;
                    return true;
                }
                bool dbStatus = true;
                SqlParameter[] sqlinputparm = new SqlParameter[2];
                sqlinputparm[0] = new SqlParameter("PII", ASignal.pii);
                sqlinputparm[1] = new SqlParameter("Type", "ED");
                string EdTo = "";
                string EdCC = "";
                string DBBCC = "";
                string chplbl = "";
                string docsubtype = "";
                string MSG = "";
                string EdName = "";
                string branchType = "";
                string Laung = "";
                string ftpDetails = "";
                    string strSubject = "";
                 
                DataSet DS = ExecuteDataSetSP("usp_GetAuthorInfo_DUEDate", sqlinputparm); 
                if (DS.Tables[0].Rows.Count > 0 && DS != null)
                {

                    DBBCC = DS.Tables[0].Rows[0]["ThomsonPMMail"] != DBNull.Value ? DS.Tables[0].Rows[0]["ThomsonPMMail"].ToString() : "";
                    chplbl = DS.Tables[0].Rows[0]["ChapLbl"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapLbl"].ToString() : "";
                    string strPM = DS.Tables[0].Rows[0]["PM"] != DBNull.Value ? DS.Tables[0].Rows[0]["PM"].ToString() : "";
                    DateTime EDdate = DS.Tables[0].Rows[0]["EditorDueDate"] != DBNull.Value ? (DateTime)DS.Tables[0].Rows[0]["EditorDueDate"] : DateTime.MinValue;
                    EdTo = DS.Tables[0].Rows[0]["EditorsEmail"] != DBNull.Value ? DS.Tables[0].Rows[0]["EditorsEmail"].ToString() : "";
                    EdCC = DS.Tables[0].Rows[0]["MailCC"] != DBNull.Value ? DS.Tables[0].Rows[0]["MailCC"].ToString() : "";
                    string chapterTitle = DS.Tables[0].Rows[0]["ChapTitle"] != DBNull.Value ? DS.Tables[0].Rows[0]["ChapTitle"].ToString() : "";
                    string Edduedate = EDdate.ToString("dd-MM-yyyy");
                    docsubtype = DS.Tables[0].Rows[0]["Docsubtype"] != DBNull.Value ? DS.Tables[0].Rows[0]["Docsubtype"].ToString() : "";
                    EdName = DS.Tables[0].Rows[0]["EditorsName"] != DBNull.Value ? DS.Tables[0].Rows[0]["EditorsName"].ToString() : "";
                    branchType = DS.Tables[0].Rows[0]["BranchType"] != DBNull.Value ? DS.Tables[0].Rows[0]["BranchType"].ToString() : "";
                    ftpDetails = DS.Tables[0].Rows[0]["FtpDetails"] != DBNull.Value ? DS.Tables[0].Rows[0]["FtpDetails"].ToString() : "";
                    Laung = DS.Tables[0].Rows[0]["Laung"] != DBNull.Value ? DS.Tables[0].Rows[0]["Laung"].ToString() : "";
                    //BranchType
                    // doc sub type 
                    //if (docsubtype == "" || docsubtype.ToLower() == "chp")
                    if (branchType.ToLower() == "mrw")
                    {
                        if (ftpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("MRWAU2.0");
                            MSG = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        }
                        if (ftpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("MRWAU3.0");
                            MSG = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                        }
                    }
                    if (branchType.ToLower() == "snt")
                    {
                        if (ftpDetails == "2")
                        {
                            DataTable dt = GetMailTemplate("SNTAU2.0");
                            MSG = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            //strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", EdName).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                        if (ftpDetails == "3")
                        {
                            DataTable dt = GetMailTemplate("SNTAU3.0");
                            MSG = dt.Rows[0]["MailBody"].ToString();
                            strSubject = dt.Rows[0]["MailSubject"].ToString();
                            strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", EdName).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                        }
                    }
                    if (branchType.ToLower() == "ehs")
                    {
                        if (Laung.ToLower() == "english")
                        {
                            if (ftpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("SNTAU3.0");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (ftpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("SNTAU3.0");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }
                        if (Laung.ToLower() == "french")
                        {
                            if (ftpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("EHSFrenchAU3.1");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (ftpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("EHSFrenchAU3.1");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }
                        if (Laung.ToLower() == "german")
                        {
                            if (ftpDetails == "2")
                            {
                                DataTable dt = GetMailTemplate("SNTAU3.0");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                            if (ftpDetails == "3")
                            {
                                DataTable dt = GetMailTemplate("SNTAU3.0");
                                MSG = dt.Rows[0]["MailBody"].ToString();
                                strSubject = dt.Rows[0]["MailSubject"].ToString();
                                strSubject = strSubject.Replace("[Title]", strTitle).Replace("[AuthorName]", author_editorname).Replace("[ChapterNo]", Convert.ToInt32(strPII.Substring(strPII.Length - 7, 5)).ToString());

                            }
                        }

                    }


                    // tag replace in chapter label or title
                    int sop = chplbl.IndexOf("<", 0);
                    while (sop != -1)
                    {
                        int esop = chplbl.IndexOf(">", sop);
                        if (esop != -1)
                        {
                            chplbl = chplbl.Remove(sop, esop - sop + 1);
                        }
                        sop = chplbl.IndexOf("<", 0);
                    }
                    /////////////////
                    // tag replace in chapter title
                    sop = chapterTitle.IndexOf("<", 0);
                    while (sop != -1)
                    {
                        int esop = chapterTitle.IndexOf(">", sop);
                        if (esop != -1)
                        {
                            chapterTitle = chapterTitle.Remove(sop, esop - sop + 1);
                        }
                        sop = chapterTitle.IndexOf("<", 0);
                    }


                    if (strTitle.Trim() == "" || chapterTitle.Trim() == "" || strTitle.Trim() == "" || ASignal.url.Trim() == "" || strPM == "" || EdTo == "" || author_editorname == "" || EdName.Trim() == "")
                    {
                        //-- information not found --//
                        dbStatus = false;
                        MSG = MSG.Replace("[EditorName]", EdName.Trim());
                        MSG = MSG.Replace("[CT]", chapterTitle.Trim());
                        MSG = MSG.Replace("[BT]", strTitle);
                        MSG = MSG.Replace("[PCURL]", ASignal.url.Trim());
                        MSG = MSG.Replace("[DUEDATE]", Edduedate);
                        MSG = MSG.Replace("[PM]", strPM);
                    }
                    else
                    {
                        MSG = MSG.Replace("[EditorName]", EdName.Trim());
                        MSG = MSG.Replace("[CT]", chapterTitle.Trim());
                        MSG = MSG.Replace("[BT]", strTitle);
                        MSG = MSG.Replace("[PCURL]", ASignal.url.Trim());
                        MSG = MSG.Replace("[DUEDATE]", Edduedate);
                        MSG = MSG.Replace("[PM]", strPM);
                    }

                }
                else
                {
                    // Record not found in database//                     
                    dbStatus = false;

                    StreamReader srat = new StreamReader(strETP);
                    MSG = srat.ReadToEnd();
                    srat.Close();

                    MSG = MSG.Replace("[BT]", strTitle);
                    MSG = MSG.Replace("[PCURL]", ASignal.url.Trim());
                }



                /// for docsubtype





                string chNo = strPII.Substring(strPII.Length - 7, 5);
                string[] attachment = new string[] { "" };
                //Mail_Class obj = new Mail_Class();
                if (dbStatus == true)
                {
                    string signalStatus = "Success";
                    logwriter("Information found in database to send author mail ");
                    SendMail(EdTo, EdCC, DBBCC, strSubject, MSG, attachment);
                    string DbError = "PCProcess:Mail send succesfully for " + FI.Name.ToString() + " to " + EdTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DbError, signalStatus, "AU");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }
                else
                {
                    logwriter("Information found in database to send author mail ");
                    string signalStatus = "Failed";
                    SendMail(DBBCC, DBBCC, DBBCC, strSubject, MSG, attachment);
                    string DbError = "Mail send succesfully to internal team  " + FI.Name.ToString() + " to " + EdTo;
                    Insert_Signal_Details(strPII, FI.Name, ASignal.isbn, ASignal.url.Trim(), DbError, signalStatus, "AU");
                    if (File.Exists(processedpath + "\\" + FI.Name))
                    {
                        File.Delete(processedpath + "\\" + FI.Name);
                    }
                    File.Copy(strFile, processedpath + "\\" + FI.Name);
                    File.Copy(strFile.Replace(".xml", ".zip"), processedpath + "\\" + FI.Name.Replace(".xml", ".zip"));
                    File.Delete(strFile);
                    File.Delete(strFile.Replace(".xml", ".zip"));
                }
                //GlobalVar.Update_Signal(ASignal.pii, FI.Name, ASignal.isbn, ASignal.url, DBError);
                error = "";
                return true;
            }
            catch (Exception exe)
            {
                error = exe.Message.ToString();
                //GlobalVar.logwriter("Signal", error, "ERROR");
                return false;
            }
        }     //tested   
              //tested   

        public static void GetConfigDeatilsFromDB()
        {

            try
            {
                string constring = ConfigurationSettings.AppSettings["sqlConnection"].ToString();

                DataTable dt1 = new DataTable("ConfigDetails");

                using (var conn = new SqlConnection(constring))
                {

                    string command = "SELECT * FROM ConfigDetails";

                    using (var cmd = new SqlCommand(command, conn))
                    {
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                        conn.Open();
                        adapt.Fill(dt1);
                        conn.Close();
                    }
                    foreach (DataRow dr in dt1.Rows)
                    {
                        key = dr["Key"].ToString();
                        value = dr["Value"].ToString();

                        if (key == "Processed")
                        {
                            processedpath = value;
                        }
                        else if (key == "UnProcessed")
                        {
                            unprocesspath = value;
                        }
                        else if (key == "Downloaded")
                        {
                            downloadedpath = value;
                        }
                        else if (key == "ProcessedFail")
                        {
                            processedfailpath = value;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
    }
}
