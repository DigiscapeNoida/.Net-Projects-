using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Domino;
namespace FMSIntegrateUsingEOOLink
{
    class JQAnotification
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        private string _MailSubject = string.Empty;
        private string _MailFrom = string.Empty;
        private string _MailBody = string.Empty;
        private string _JidAid = string.Empty;
        private string _stage=string.Empty;

        private string _Jid;
        private string _Iss;
        private string _Vol;
        NotesDocument Document;
       // private string _stage;
        private string _JQAStatus;
        public JQAnotification(string subject, string from, string body, NotesDocument Document)
        {
            MailSubject = subject;
            this.Document = Document;
            MailFrom = from;
            MailBody = body;
        }
        public void StartProcess()
        {
            FillData();
        }

        private void DownloadAttachment()
        {
            try
            {
                DownloadAttachements oDownload = new DownloadAttachements(Document);
                oDownload.Download(JidAid);
            }
            catch (Exception)
            {                                
            }                
        }       

        private string MailSubject
        {
            get 
            {
                return _MailSubject;
            }
            set
            {
                 _MailSubject = value;
            }
        }
        private string MailFrom
        {
            get
            {
                return _MailFrom;
            }
            set
            {
                _MailFrom = value;
            }
        }
        private string MailBody
        {
            get
            {
                return _MailBody;
            }
            set
            {
                _MailBody = value;
            }
        }        
        private string JidAid
        {
            get
            {
                return _JidAid;
            }
            set
            {
                _JidAid = value;
            }
        }
        private string Stage
        {
            get
            {
                return _stage;
            }
            set
            {
                _stage = value;
            }

        }
        private string Jid
        {
            get
            {
                return _Jid;
            }
            set
            {
                _Jid = value;
            }
        }
        private string Vol
        {
            get
            {
                return _Vol;
            }
            set
            {
                _Vol = value;
            }
        }
        private string Issue
        {
            get
            {
                return _Iss;
            }
            set
            {
                _Iss = value;
            }
        }
        private string JQAStatus
        {
            get
            {
                return _JQAStatus;
            }
            set
            {
                _JQAStatus = value;
            }
        }
        private void FilterIssue(string val)
        {
            try
            {
                Stage = "FI";
                string _subject = val;

                int sIndex = _subject.IndexOf("[");
                if (sIndex != -1)
                {
                    int eIndex = _subject.IndexOf("]", sIndex);
                    if (eIndex != -1)
                    {
                        _subject = _subject.Substring(sIndex + 1, eIndex - sIndex - 1);
                        if (_subject != "")
                        {
                            if (_subject.Contains("/"))
                            {
                                string tmp = _subject;
                                string[] sub = _subject.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                if (sub.Length > 3)
                                {
                                    Jid = sub[0].Trim();
                                    Vol = sub[2].Trim();
                                    Issue = sub[3].Trim();
                                }
                                else
                                {
                                    Jid = sub[0].Trim();
                                    Vol = sub[1].Trim();
                                    Issue = sub[2].Trim();
                                }
                            }
                            else
                            {
                                string[] sub = _subject.Split(new string[] { " ", "|" }, StringSplitOptions.RemoveEmptyEntries);
                                Jid = sub[0].Trim();
                                Vol = sub[1].Trim();
                                Issue = sub[2].Trim();
                            }
                        }
                    }
                }
                else
                {
                    JidAid = "";
                    Stage = "";
                    Console.WriteLine("Wrong format for JQA mail subject :: " + _subject);
                }

            }
            catch (Exception Ex)
            {
                
            }
        }
        private void FillData()
        {
            string _subject;
            try
            {
                if (!(string.IsNullOrEmpty(MailSubject)))
                {
                    _subject = MailSubject;
                    if (_subject.ToLower().Contains("published"))
                    {
                        JQAStatus = "Published";
                        if (_subject.ToLower().Contains("ev") || _subject.ToLower().Contains("aa"))
                        {
                            FilterJidAid(_subject);
                        }
                        else
                        {
                            FilterIssue(_subject);
                        }
                    }
                    else if (_subject.ToLower().Contains("resupply"))
                    {
                        JQAStatus = "Resupply";
                        if (_subject.ToLower().Contains("ev") || _subject.ToLower().Contains("aa"))
                        {
                            FilterJidAid(_subject);
                            if (_subject.ToLower().Contains("ev"))
                            {
                                UpdateWipData();      // insert S280 wipdata.
                                DownloadAttachment();// downlaod attach files
                            }
                        }
                        else
                        {
                            FilterIssue(_subject);
                        }

                    }
                    else if (_subject.ToLower().Contains("uploaded"))
                    {
                        JQAStatus = "Uploaded";
                        if (_subject.ToLower().Contains("ev") || _subject.ToLower().Contains("aa"))
                        {
                            FilterJidAid(_subject);
                        }
                        else
                        {
                            FilterIssue(_subject);
                        }
                    }
                    else if (_subject.ToLower().Contains("failed"))
                    {
                        if (_subject.ToLower().Contains(".zip"))
                        {
                            if (_subject.ToLower().Contains("r.zip"))
                            {
                                JQAStatus = "ResupplyFailed";
                                FilterZipSubject(_subject.Replace("r.zip",".zip"));
                            }
                            else
                            {
                                JQAStatus = "Failed";
                                FilterZipSubject(_subject);
                            }                            
                        }
                    }                    
                }
                if (!(String.IsNullOrEmpty(JidAid)))
                {                    
                    UpdateStatusDB(false);
                }
                else
                {
                    if (String.IsNullOrEmpty(Jid))
                    {
                        updateError();
                    }
                    else
                    {
                        UpdateStatusDB(true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        private void updateError()
        {
            try
            {

                string applpath = AppDomain.CurrentDomain.BaseDirectory + "JQA\\" + "JQALog_" + DateTime.Now.ToString("dd_MM_yyyy") + ".log";
                if (!(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "JQA")))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "JQA");
                }
                File.AppendAllText(applpath , "Error:" + MailSubject+ Environment.NewLine);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        private void FilterZipSubject(string val) //AJMGB_EV_AJMGB32283r.zip failed Package Validation
        {
            string sub = val;
            try
            {
                string filename = "";
                string[] zip = sub.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string  item in zip)
                {
                    if (item.ToLower().Contains(".zip"))
                    {
                        filename = item;
                        filename = filename.Replace(".zip","");
                        break;
                    }
                }
                if (filename != "")
                {
                    string[] para = filename.Split('_');
                    JidAid = para[2];
                    Stage =para[1]  ;
                }
                else
                {
                    JidAid = "";
                    Stage = "";
                    Console.WriteLine("Unknown message type ::" + val);
                }

            }
            catch (Exception)
            {
                                
            }
        }
        private void FilterJidAid(string val)
        {
             try 
	        {
                      string _subject = val;
		      
                       int sIndex =  _subject.IndexOf("[");
                       if (sIndex != -1)
                       {
                           int eIndex = _subject.IndexOf("]", sIndex);
                           if (eIndex != -1)
                           {
                               _subject = _subject.Substring(sIndex + 1, eIndex - sIndex - 1);
                               if (_subject != "")
                               {
                                   string[] sub = _subject.Split(new string[] { "/", " " },StringSplitOptions.RemoveEmptyEntries);
                                   JidAid = sub[0].Trim();
                                   Stage = sub[1].Trim();
                               }
                           }
                       }
                       else
                       {
                           JidAid ="";
                           Stage  = "";
                           ProcessMessage("Wrong format for JQA mail subject :: " + _subject);
                       }
                    
	            }
	        catch (Exception)
	        {		
		        throw;
	        }
        }
        private bool UpdateStatusDB(bool isIssue)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                if (isIssue == true)
                {
                    param[0] = new SqlParameter("@JIDAid", Jid.Trim());
                }
                else 
                {
                    param[0] = new SqlParameter("@JIDAid", JidAid.Trim());
                }
                param[1] = new SqlParameter("@JQAStatus", JQAStatus.Trim());
                param[2] = new SqlParameter("@stage", Stage.Trim());
                param[3] = new SqlParameter("@vol",( Vol == null ?"":Vol.Trim()));
                param[4] = new SqlParameter("@Iss", (Issue == null ?"":Issue.Trim()));                 
                SqlHelper.ExecuteNonQuery(StaticInfo.OPSConfig, System.Data.CommandType.StoredProcedure, "[USP_JQAStatusUpdate]", param);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
            return false;
        }
        private bool UpdateWipData()
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@JIDAid", JidAid.Trim());
                SqlHelper.ExecuteNonQuery(StaticInfo.OPSConfig, System.Data.CommandType.StoredProcedure, "[usp_UpdateJQAWipS280ToResuply]", param);
                return true;
            }
            catch (Exception ex)
            {
                   ErrorMessage(ex);
            }

            return false;
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
    }
}
