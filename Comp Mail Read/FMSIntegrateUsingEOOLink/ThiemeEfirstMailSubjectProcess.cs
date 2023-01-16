using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class ThiemeEfirstMailSubjectProcess
    {
        public static int NotCount = 1;
        public static int InsCount = 1;
        string _JID   = string.Empty;
        string _AID   = string.Empty;
        string _DOI   = string.Empty;
        string _VOL   = string.Empty;
        string _Year  = string.Empty;

        string _Stage       = string.Empty;
        string _MailSubject = string.Empty;
        string _MailBody    = string.Empty;
        static StringDictionary _ThiemeJournalTitles = new StringDictionary();
        static string           _ConnectionString    = string.Empty;

        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;
        
        static ThiemeEfirstMailSubjectProcess()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            FillThiemeJournalTitles();
        }

        public ThiemeEfirstMailSubjectProcess(string MailSubject, string MailBody)
        {
            _MailSubject = MailSubject;
            _MailBody    = MailBody;
        }

        private static void FillThiemeJournalTitles()
        {
           string Query ="select JID,Jname  from opsdetails (nolock) where client='Thieme' order by Jname";
           SqlDataReader DR=  SqlHelper.ExecuteReader(_ConnectionString,System.Data.CommandType.Text,Query);
           if (DR != null && DR.HasRows)
           {
                while (DR.Read())
                    _ThiemeJournalTitles.Add(DR[0].ToString(), DR[1].ToString());
           }
        }

        public void StartProcess()
        {
            StaticInfo.WriteLogMsg.AppendLog("Start Process to extract information from  email subject");
            ProcessMessage("Start Process to extract information from  email subject");

            if (_MailSubject.IndexOf("Efirst", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ProcessMessage("_Stage = Efirst");
                _Stage = "Efirst";

                StaticInfo.WriteLogMsg.AppendLog("_Stage = Efirst");
                GetEfirstDetail();

            }
            else if (_MailSubject.Replace(" ", "").EndsWith(":online", StringComparison.OrdinalIgnoreCase))
            {
                StaticInfo.WriteLogMsg.AppendLog("_Stage = Fiz");
                ProcessMessage("_Stage = Fiz");
                _Stage = "Fiz";
                 GetFIZDetail();
            }
            else
            {
                return;
            }
            
            if (true)
            {
                StaticInfo.WriteLogMsg.AppendLog("Insert db calls");
                InsertEfirstDetail();
                InsCount++;
            }
            else
            {
                NotCount++;
            }
        }
        private void GetEfirstDetail()
        { 
            //Efirst article online, Global Spine Journal, File name:10-1055-s-0034-1375560_1300028.xml

              string          JT = string.Empty;
              string     DOI_AID = string.Empty;
              string[] SubParts  = _MailSubject.Split(',');

              ProcessMessage("Split ,Mail subject with ,");

              if (SubParts.Length == 3)
              {
                  JT = SubParts[1];
                  ProcessMessage("JT = " + JT);
              }
              else
              { 
                  ProcessMessage("Error :: Check mail subject. It should be ',' seprated and length must be 3");
              }
               
            if (!string.IsNullOrEmpty(JT))
            {

                 GetJID(JT);
                 if (!string.IsNullOrEmpty(_JID))
                 {
                     DOI_AID = SubParts[2].Replace("File name:", "").Replace("Filename:", "").Replace(".xml", "");
                     DOI_AID = Path.GetFileNameWithoutExtension(DOI_AID);

                     ProcessMessage("DOI_AID ::" + DOI_AID);

                     string[] ARR = DOI_AID.Split('_');
                     if (ARR.Length == 2)
                     {
                         _DOI = ARR[0];
                         _AID = ARR[1];
                         ProcessMessage("Efirst DOI : " + _DOI);
                         ProcessMessage("_AID : " +_AID);
                     }
                     else
                     {
                         ProcessMessage("Error :: DOI and AID couold not be foud it should be DOI_AID");
                     }
                 }
                 else
                 {
                     ProcessMessage("Error :: JID is not found. Please check the mail subject and database JT.");
                 }
            }
        }

        private void GetJID(string Jtitle)
        {
            ProcessMessage ("Start to get JID from Journal title");
            string MailJT = Jtitle.ToLower().Replace("the ", "").Replace("&", "").Replace("and", "").Replace(" ", "").Replace("-", "").Replace("—", "").Replace("fw:", ""); /////////////Mail Subject Journal Title

            ProcessMessage("MailJT ::" + MailJT);

            if (MailJT.Contains(":"))
            {
                ProcessMessage("MailJT Contains : So substring from:" );
                MailJT = MailJT.Substring(0, MailJT.IndexOf(":"));
            }

            foreach (string JID in _ThiemeJournalTitles.Keys)
            {
                string DBJT = string.Empty;
                if (JID == "cmtro" || JID == "cmtr")
                    DBJT = _ThiemeJournalTitles[JID].ToLower().Replace("the ", "").Replace("and", "").Replace(" ", "").Replace("-", "").Replace("—", "").Replace("&", "amp;");
                else
                 DBJT = _ThiemeJournalTitles[JID].ToLower().Replace("the ", "").Replace("&", "").Replace("and", "").Replace(" ", "").Replace("-", "").Replace("—", ""); /////////////Database Journal Title
                ProcessMessage("DBJT " + DBJT);
   
                if (MailJT.Equals(DBJT,StringComparison.OrdinalIgnoreCase))
                {
                   _JID = JID.ToUpper();
                    ProcessMessage("_JID : " + _JID);
                    break;
                }
            }
        }
        private void InsertEfirstDetail()
        {

            ProcessMessage("Start InsertEfirstDetail");
            SqlParameter [] para = new SqlParameter [7];

            para[0] = new SqlParameter("@JID", _JID.Trim());
            para[1] = new SqlParameter("@AID", _AID.Trim());
            para[2] = new SqlParameter("@DOI", _DOI.Trim().Replace(".","-").Replace("/","-"));
            para[3] = new SqlParameter("@Volume", _VOL.Trim());
            para[4] = new SqlParameter("@Year", _Year.Trim());
            para[5] = new SqlParameter("@Stage", _Stage.Trim());
            para[6] = new SqlParameter("@MailSubjectLine", _MailSubject.Trim());

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsetThiemeOffPrintDetail", para);

                ProcessMessage("Insert Efirst/Fiz Detail Successfully");
            }
            catch (SqlException Ex)
            {
                ErrorMessage(Ex);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        public void     GetFIZDetail()
        {
            ProcessMessage("Start to GetFIZDetail");

            ProcessMessage("Split ,Mail subject with ,");
            string[] MailSub = _MailSubject.Split(',');

            if (MailSub.Length>1)
            {
                    string JT = MailSub[0];
                    ProcessMessage("JT = " + JT);
                    GetJID(JT);
            }
            else
            { 
                  ProcessMessage("Error :: Check mail subject. It should be ',' seprated and ");
            }

            if (!string.IsNullOrEmpty(_JID))
            {
                ProcessMessage("Traverse each line of mail body.");

                string[] Lines = _MailBody.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string Line in Lines)
                {
                    if (Line.IndexOf("/pdf/", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        int Pos = Line.IndexOf("/pdf/");
                        if (Pos > 0)
                        {
                            string Doi = Line.Substring(Pos + 5).Trim();

                            

                            FizProcessing(Doi);

                            break;
                        }
                    }
                }
            }
            else
            {
                 ProcessMessage("Error :: JID is not found. Please check the mail subject and database JT.");
            }
            
        }

        public void FizProcessing(string _PDFName)
        {
            try
            {
                string DOI = string.Empty;
                string AID = string.Empty;

                DOI = _PDFName.Replace(".pdf", "");
                DOI = DOI.Replace("/", "-");
                DOI = DOI.Replace(".", "-");

                _AID = "";
                _DOI = DOI;

                string[] MailSub = _MailSubject.Replace(": online","").Trim(new  char[]{' ',' ',':'}).Split(',');

                if (MailSub.Length > 1)
                {
                    string[] VolYear = MailSub[1].Split('/');

                    if (VolYear.Length >= 2)
                    {
                        _VOL = VolYear[0];
                        _Year = VolYear[1];
                    }
                }
                ProcessMessage("FIZ DOI :: " + _DOI); ;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
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

    }
}
