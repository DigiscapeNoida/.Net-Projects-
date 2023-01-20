using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Net.Mail;
namespace PCFlowReminderMail
{
    class Worker
    {
        public Worker()
        {

        }
        public void SendNotification()
        {
            try
            {
                //DataSet DTEditor = GetReminders(true);
                DataSet DTAuthor = GetReminders(false);
                Console.WriteLine("Reminder mail sending process is started.  " + DateTime.Now.ToString("hh-mm-ss tt"));
                GlobalConfig.oGlobalConfig.WriteLog("Reminder mail sending process is started.");
                if (DTAuthor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in DTAuthor.Tables[0].Rows)
                    {
                        try
                        {
                            MailInfo OMailInfo = new MailInfo();
                            string isbn = (item["ISBN"] != DBNull.Value) ? item["ISBN"].ToString() : "";
                            string strPII = (item["PII"] != DBNull.Value) ? item["PII"].ToString() : "";
                            OMailInfo.PII = strPII;
                            DateTime dateT = item["DownDate"] != DBNull.Value ? (DateTime)item["DownDate"] : DateTime.MinValue;
                            OMailInfo.WasSendOn = dateT.ToString("dd-MM-yyyy");
                            OMailInfo.ToMail = (item["MailTo"] != DBNull.Value) ? item["MailTo"].ToString() : "";
                            OMailInfo.CCMail = (item["MailCC"] != DBNull.Value) ? item["MailCC"].ToString() : "";
                            OMailInfo.BCCMail = (item["BCCMail"] != DBNull.Value) ? item["BCCMail"].ToString() : "";
                            DateTime dateTduedate = item["DueDate"] != DBNull.Value ? (DateTime)item["DueDate"] : DateTime.MinValue;
                            OMailInfo.DueDate = dateTduedate.ToString("dd-MM-yyyy");
                            OMailInfo.Link = (item["URL"] != DBNull.Value) ? item["URL"].ToString() : "";
                            OMailInfo.PMName = (item["PM"] != DBNull.Value) ? item["PM"].ToString() : "";
                            OMailInfo.BookTitle = Get_Book_Title(isbn);
                            //OMailInfo.FromSender = "eproof@elsevier.thomsondigital.com";
                            OMailInfo.FromSender = GlobalConfig.oGlobalConfig.ERRFRM;
                            string chepter = strPII.Substring(strPII.Length - 7, 5);
                            OMailInfo.ChapterNo = (item["ChapTitle"] != DBNull.Value) ? item["ChapTitle"].ToString() : ""; //chepter;
                            if ((Validate(OMailInfo)))
                            {
                                //// editd by kshitij to include author name in the subject line 19 Jan 2017
                                string authorname = string.Empty;
                                if (!(string.IsNullOrWhiteSpace(isbn)))
                                    authorname = GetAuthorName(isbn);
                                string body = string.Empty;
                                string strSubject = "Reminder: Proof of " + OMailInfo.BookTitle + ": " + authorname + " : Chapter " + Convert.ToInt32(chepter);
                                if (item["BranchType"] is DBNull)
                                {
                                    Console.WriteLine("Error  : Branch Type not found for pii " + OMailInfo.PII.ToString());
                                    GlobalConfig.oGlobalConfig.WriteLog(Environment.NewLine+ "Error  : Branch Type not found for pii " + OMailInfo.PII.ToString());
                                }
                                else
                                {
                                    if (item["BranchType"].ToString().Trim().ToLower() == "mrw")
                                    {
                                        body = GlobalConfig.oGlobalConfig.GetTempBodyAuthor;
                                    }
                                    if (item["BranchType"].ToString().Trim().ToLower() == "snt")
                                    {
                                        body = GlobalConfig.oGlobalConfig.GetTempBodyAuthorSnt;
                                    }
									if (item["BranchType"].ToString().Trim().ToLower() == "ehs")
									{
										if (item["Laung"] is DBNull)
										{
											Console.WriteLine("Error  : Language not found for pii " + OMailInfo.PII.ToString());
											GlobalConfig.oGlobalConfig.WriteLog(Environment.NewLine + "Error  : Language not found for pii " + OMailInfo.PII.ToString());
										}
										else if (item["AuthorName"] is DBNull)
										{
											Console.WriteLine("Error  : Author name not found for pii " + OMailInfo.PII.ToString());
											GlobalConfig.oGlobalConfig.WriteLog(Environment.NewLine + "Error  : Author name not found for pii " + OMailInfo.PII.ToString());
										}
										else
										{
											if (item["Laung"].ToString().Trim().ToLower() == "english")
											{
												body = GlobalConfig.oGlobalConfig.GetTempBodyAuthorSnt;
											}
											if (item["Laung"].ToString().Trim().ToLower() == "french")
											{
												body = GlobalConfig.oGlobalConfig.GetTempBodyAuthorFrench;
												strSubject = "Rappel pour la relecture de chapitre";
											}
											if (item["Laung"].ToString().Trim().ToLower() == "german")
											{
												body = GlobalConfig.oGlobalConfig.GetTempBodyAuthorSnt;
											}
										}
									}

								}
                                if (body != string.Empty)
                                {
                                    body = body.Replace("[BookTitle]", OMailInfo.BookTitle);
                                    body = body.Replace("[DownDate]", OMailInfo.WasSendOn);
                                    body = body.Replace("[PM]", OMailInfo.PMName);
                                    body = body.Replace("[Url]", OMailInfo.Link);
                                    body = body.Replace("[DUEDATE]", OMailInfo.DueDate);
									body = body.Replace("[autt_name]", item["AuthorName"].ToString().Trim());
									Mailing oMailing = new Mailing();
                                    if (oMailing.SendMail(OMailInfo.ToMail, strSubject, body, OMailInfo.CCMail, OMailInfo.BCCMail, OMailInfo.FromSender, ""))
                                    {
                                        UpdateStatus(OMailInfo.PII, "AU");
                                        GlobalConfig.oGlobalConfig.WriteLog("Successfully : Mail is sent to " + OMailInfo.ToMail + " for subject " + strSubject + Environment.NewLine + body + Environment.NewLine);
                                        Console.WriteLine("Successfully : Mail sent to " + OMailInfo.ToMail + " for subject " + strSubject);
                                    }
                                    else
                                    {
                                        GlobalConfig.oGlobalConfig.WriteLog("Error : Unable to send mail to " + OMailInfo.ToMail + " for subject " + strSubject + Environment.NewLine + body + Environment.NewLine);
                                        Console.WriteLine("Error : Unable to send mail to " + OMailInfo.ToMail + " for subject " + strSubject);
                                        GlobalConfig.oGlobalConfig.Send_Error("Error: Unable to send mail to " + OMailInfo.ToMail + " for subject " + strSubject + " (PII : " + OMailInfo.PII + ")", OMailInfo.BCCMail);
                                    }
                                }
                                else
                                {
                                    GlobalConfig.oGlobalConfig.WriteLog("Error : Unable to find valid BranchType for " + OMailInfo.PII + " for subject " + strSubject + Environment.NewLine);
                                    Console.WriteLine("Error : Unable to find valid BranchType for " + OMailInfo.PII + " for subject " + strSubject);
                                    GlobalConfig.oGlobalConfig.Send_Error("Error: Unable to find valid BranchType for " + OMailInfo.PII + " for subject " + strSubject, OMailInfo.BCCMail);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error  : Validation fail for pii " + OMailInfo.PII.ToString());
                                GlobalConfig.oGlobalConfig.WriteLog("Error  : Validation fail for pii " + OMailInfo.PII.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error  : " + ex.Message.ToString());
                            GlobalConfig.oGlobalConfig.WriteLog("Error|Author" + ex.Message.ToString());
                            continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Data not found for reminder mail  " + DateTime.Now.ToString("hh-mm-ss tt"));
                    GlobalConfig.oGlobalConfig.WriteLog("Data not found for reminder mail");
                }
                Console.WriteLine("Author data is reviewed at  " + DateTime.Now.ToString("hh-mm-ss tt"));              
                Console.WriteLine("Editors data is reviewed at  " + DateTime.Now.ToString("hh-mm-ss tt"));
                GlobalConfig.oGlobalConfig.WriteLog("Reminder mail sending process end");
            }
            catch (Exception exe)
            {
                Console.WriteLine("Error  : " + exe.Message.ToString());
                GlobalConfig.oGlobalConfig.WriteLog("Error|" + exe.Message.ToString());
            }
        }

        private bool Validate(MailInfo OMailInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(OMailInfo.ToMail.Trim()))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("To mail not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(OMailInfo.PII))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("PII not found");
                    return false;
                }
                if (string.IsNullOrEmpty(OMailInfo.PMName))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("PM name not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(OMailInfo.WasSendOn))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("Original mail send date not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(OMailInfo.Link))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("Download link not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }

                if (string.IsNullOrEmpty(OMailInfo.DueDate))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("Due date not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }

                if (string.IsNullOrEmpty(OMailInfo.ChapterNo))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("Chapter No not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }
                if (string.IsNullOrEmpty(OMailInfo.BookTitle))
                {
                    GlobalConfig.oGlobalConfig.WriteLog("Book Title not found for pii " + OMailInfo.PII.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public string Get_Book_Title(string strISBN)
        {
            try
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
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error in book title | " + exe.Message.ToString());
                return "";              
            }
        }

        private string GetInformation(string content, string tag)
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
        private DataSet GetReminders(bool isEditor)
        {
            try
            {
                string SpName = "";
                if (isEditor)
                {
                    SpName = SPNames.EdInfo;
                }
                else
                {
                    SpName = SPNames.AuInfo;
                }

                DataSet DS = DataAccess.ExecuteDataSetSP(SpName);
                return DS;
            }
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error|" + exe.Message.ToString());
                return null;
            }
        }

        private bool UpdateStatus(string pii, string type)
        {
            try
            {
                string Query = "";
                if (type == "AU")
                {
                    Query = "update [PCDATASETINFO] set isReminder='YES' where pii='" + pii + "'";
                }
                else
                {
                    Query = "update [PCDATASETINFO] set isEDRemider='YES' where pii='" + pii + "'";
                }

                DataAccess.ExecuteNonQuery(Query);
                GlobalConfig.oGlobalConfig.WriteLog("Status updated in database.");
                return true;
            }
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("ERROR in updating status | " + exe.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Edited by Kshitij, to get the atuhorname to include in the Subject line of reminder name
        /// date: 19/Jan/2017
        /// </summary>
        /// <param name="isbn">isbn Number</param>
        /// <param name="address">PPM Address</param>
        /// <returns>AuthorName or String</returns>
        private string GetAuthorName(string isbn)
        {
            //string isbn = "9780080999531";
            if (isbn.IndexOf("-") > -1)
            {
                isbn =  isbn.Replace("-", string.Empty);
            }

            string address = @"\\td-nas\Elsinpt\ElsBook\Orders\PPM\";
            try
            {
                string directoryPath = Path.Combine(address, isbn);
                if ((string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(address)))
                    throw new Exception();
                if (!Directory.Exists(directoryPath))
                    
                    throw new Exception();

                string[] dir = Directory.GetFiles(directoryPath, @"kup*.xml", SearchOption.AllDirectories);

                if (!(dir.Count() > 0))
                    throw new Exception();

                XmlDocument doc = new XmlDocument();
                XmlTextReader myTextReader = new XmlTextReader(Path.Combine(dir[0].Trim()));
                myTextReader.XmlResolver = null;
                myTextReader.DtdProcessing = DtdProcessing.Ignore;
                myTextReader.WhitespaceHandling = WhitespaceHandling.None;

                string authorName = string.Empty;
                string chpId = string.Empty;
                while (myTextReader.Read())
                {
                    if (myTextReader.LocalName == "originator"
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("sort-order").ToString() == "1")
                    {
                        /// read next node 
                        myTextReader.Read(); // for originator-type
                        if (myTextReader.LocalName.Equals("originator-type", StringComparison.InvariantCultureIgnoreCase))
                        {
                            myTextReader.Read(); // for originator-type value
                            if (myTextReader.Value.Equals("author", StringComparison.InvariantCultureIgnoreCase) || myTextReader.Value.Equals("editor", StringComparison.InvariantCultureIgnoreCase))
                            {
                                myTextReader.Read(); // for originator-type open
                                myTextReader.Read(); // for First name

                                //// To pick firstname or last name
                                if (myTextReader.LocalName.Equals("first-name", StringComparison.InvariantCultureIgnoreCase) || myTextReader.LocalName.Equals("last-name", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    myTextReader.Read(); // for firstname value
                                    authorName += myTextReader.Value;
                                    myTextReader.Read(); // for firstname close
                                }

                                else
                                {
                                    myTextReader.Read(); // for firstname value
                                    myTextReader.Read(); // for firstname close
                                }
                                //// twice
                                myTextReader.Read(); // forlast name start tag
                                if (myTextReader.LocalName.Equals("first-name", StringComparison.InvariantCultureIgnoreCase) || myTextReader.LocalName.Equals("last-name", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    myTextReader.Read(); //for lastname value
                                    authorName += " " + myTextReader.Value;
                                    myTextReader.Read(); //for lastname close

                                }
                                else
                                {
                                    myTextReader.Read(); //for lastname value
                                    myTextReader.Read(); //for lastname close
                                }

                                if (!string.IsNullOrWhiteSpace(authorName))
                                    //// get out of loop
                                    break;
                            }
                        }
                    }
                }

                return authorName;
            }
            catch
            {
                return string.Empty;
                //throw;
            }


        }


        public void SendInternalNotification()
        {
            DataSet DSReminder = GetInternalReminders();
            GlobalConfig.oGlobalConfig.WriteLog("Reminder mail sending process is started.");
            if (DSReminder != null)
            {
                if (DSReminder.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in DSReminder.Tables[0].Rows)
                    {
                        try
                        {
                            string isbn = (item["ISBN"] != DBNull.Value) ? item["ISBN"].ToString() : "";
                            string strPII = (item["PII"] != DBNull.Value) ? item["PII"].ToString() : "";
                            string BookTitle = Get_Book_Title(isbn);
                            string chaplbl = strPII.Substring(strPII.Length - 7, 5);
                            string mailto = (item["BCCMAIL"] != DBNull.Value) ? item["BCCMAIL"].ToString() : "";
                            //string url = (item["URL"] != DBNull.Value) ? item["URL"].ToString() : "";
                            string mailcc = ConfigurationSettings.AppSettings["INTERNALCC"];
                            string mailfrom = ConfigurationSettings.AppSettings["ERRFRM"];
                            string mailsubject = BookTitle + ": " + chaplbl + " Author correction due";
                            //DateTime newtime = 
                            string duedate = (item["duedate"] != DBNull.Value) ? item["duedate"].ToString() : "";
                            //string ndate = duedate.Split(' ');
                            //if (duedate == "Saturday")
                            //{
                            //    duedate = DateTime.Now.AddDays(3).ToString();
                            //}
                            //if (duedate == "Sunday")
                            //{
                            //    duedate = DateTime.Now.AddDays(2).ToString();
                            //}
                            //string duedate =   DateTime.Now.AddDays(1).DayOfWeek.ToString();
                            string mailbody = "Dear PM,</br> The author corrections for " + BookTitle + " " + chaplbl + " is due " + duedate.Substring(0, 10) + ". If we don't get corrections from author proofing application shall send a reminder to the author. This is for your information and further action. </br>This is auto-reminder email internal to Thomson Digital.";
                            GlobalConfig.oGlobalConfig.WriteLog("Sending internal mail for " + strPII);
                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress(mailfrom);
                            mail.To.Add(mailto);
                            if (mailcc != String.Empty)
                                mail.CC.Add(mailcc);
                            //if (MailBCC != String.Empty)
                            //    mail.Bcc.Add(MailBCC);
                            mail.Subject = mailsubject;
                            mail.Body = mailbody;
                            mail.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient("103.35.121.108");
                            //smtp.Host = ConfigurationSettings.AppSettings["SMTP"];
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential("thomson", "Express@2008##");
                            smtp.Port = 25;
                            //smtp.EnableSsl = true;
                            smtp.Timeout = 600000;
                            mail.Bcc.Add(new MailAddress("deepak.verma@digiscapetech.com"));
                            smtp.Send(mail);
                            GlobalConfig.oGlobalConfig.WriteLog("Success mail internal mail for " + strPII);
                            //ConfigurationSettings.AppSettings["SMTP"];
                            //OMailInfo.PII = strPII;
                            //DateTime dateT = item["DownDate"] != DBNull.Value ? (DateTime)item["DownDate"] : DateTime.MinValue;
                            //OMailInfo.WasSendOn = dateT.ToString("dd-MM-yyyy");
                            //OMailInfo.ToMail = (item["MailTo"] != DBNull.Value) ? item["MailTo"].ToString() : "";
                            //OMailInfo.CCMail = (item["MailCC"] != DBNull.Value) ? item["MailCC"].ToString() : "";
                            //OMailInfo.BCCMail = (item["BCCMail"] != DBNull.Value) ? item["BCCMail"].ToString() : "";
                            //DateTime dateTduedate = item["DueDate"] != DBNull.Value ? (DateTime)item["DueDate"] : DateTime.MinValue;
                            //OMailInfo.DueDate = dateTduedate.ToString("dd-MM-yyyy");
                            //OMailInfo.Link = (item["URL"] != DBNull.Value) ? item["URL"].ToString() : "";
                            //OMailInfo.PMName = (item["PM"] != DBNull.Value) ? item["PM"].ToString() : "";

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }


        private DataSet GetInternalReminders()
        {
            try
            {
                String SpName = "GetInternalReminder";

                DataSet DS = DataAccess.ExecuteDataSetSP(SpName);
                return DS;
            }
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error|" + exe.Message.ToString());
                return null;
            }
        }


    }
}
