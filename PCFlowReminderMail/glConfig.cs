using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace PCFlowReminderMail
{
    public sealed class GlobalConfig
    {
        public static GlobalConfig _GlobalConfig = null;
        string _LOGPATH = "";  
        string _smtp = "";
        string _errTO = "";
        string _errfrm = "";
        string _errCC = "";
        string _mailTemplateAu = "";
        string _mailTemplateAuSnt = "";
		string _mailTemplateAuFrench = "";
		string _mailTemplateEd = "";
        private GlobalConfig()
        {          
          
            _LOGPATH = AppDomain.CurrentDomain.BaseDirectory + "LOG"; 
            if (!(Directory.Exists(_LOGPATH)))
            {
                Directory.CreateDirectory(_LOGPATH);
            }

            _mailTemplateAu = AppDomain.CurrentDomain.BaseDirectory + "\\RemindAuthor.htm";
            _mailTemplateAuSnt = AppDomain.CurrentDomain.BaseDirectory + "\\RemindAuthor_Snt.htm";
			_mailTemplateAuFrench = AppDomain.CurrentDomain.BaseDirectory + "\\RemindAuthor_French.htm";
			_mailTemplateEd = AppDomain.CurrentDomain.BaseDirectory + "\\RemindEditors.htm";

            _smtp = ConfigurationSettings.AppSettings["SMTP"];
            _errTO = ConfigurationSettings.AppSettings["ERRTo"];
            _errCC = ConfigurationSettings.AppSettings["ERRCC"];
            _errfrm = ConfigurationSettings.AppSettings["ERRFRM"];
        }
        public static GlobalConfig oGlobalConfig
        {
            get
            {
                if (_GlobalConfig == null)
                {
                    _GlobalConfig = new GlobalConfig();
                }
                return _GlobalConfig;
            }
        }

        public string GetTempBodyAuthor
        {
            get
            {
                string filebody = File.ReadAllText(_mailTemplateAu);
                return filebody;
            }
        }
        public string GetTempBodyAuthorSnt
        {
            get
            {
                string filebody = File.ReadAllText(_mailTemplateAuSnt);
                return filebody;
            }
        }
		public string GetTempBodyAuthorFrench
		{
			get
			{
				string filebody = File.ReadAllText(_mailTemplateAuFrench);
				return filebody;
			}
		}
		public string GetTempBodyEditors
        {
            get
            {
                string filebody = File.ReadAllText(_mailTemplateEd);
                return filebody;
            }
        } 

        public string LOGPATH
        {
            get
            {
                return _LOGPATH;
            }
        }
        public string SMTP
        {
            get
            {
                return _smtp;
            }
        }
        public string ERRTo
        {
            get
            {
                return _errTO;
            }
        }
        public string ERRCC
        {
            get
            {
                return _errCC;
            }
        }        
        public string ERRFRM
        {
            get
            {
                return _errfrm;
            }
        } 
        public void Send_Error(string error,string bcc)
        {
            try
            {
                GlobalConfig.oGlobalConfig.WriteLog(error + Environment.NewLine);
                string format = "<p><font face=\"Arial\" size=\"2\">Dear Team ,</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">Please check following  :</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">" + error + "</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">This is auto genrated mail from PC Work flow</font></p>";
                string mailbody = "<html><head><title>ERROR</title></head><body>" + format + "</body></html>";
                Mailing mail = new Mailing();
                mail.SendMail(bcc, "Auto reminder pc work flow mail sending error", mailbody, "", "", GlobalConfig.oGlobalConfig.ERRFRM, "");
                GlobalConfig.oGlobalConfig.WriteLog(Environment.NewLine + mailbody + Environment.NewLine);
            }
            catch (Exception exx)
            {
                GlobalConfig.oGlobalConfig.WriteLog(error + Environment.NewLine);
            }
        }        

        public void WriteLog(string data)
        {
            try
            {
                //Console.WriteLine(data);
                string FileName = LOGPATH;
                if (!(Directory.Exists(LOGPATH)))
                {
                    Directory.CreateDirectory(LOGPATH);
                }
                FileName = FileName + "\\ErrorLog" + DateTime.Now.ToString("dd_MM_yyyy") + ".log";
                File.AppendAllText(FileName, DateTime.Now.ToString("hh:mm:ss tt") + " - " + data + Environment.NewLine);
            }
            catch
            { }
        }
    }
}
