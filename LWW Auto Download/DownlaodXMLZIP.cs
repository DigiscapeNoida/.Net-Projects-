using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using ProcessNotification;

namespace LWWAutoIntegrate
{
    class DownlaodXMLZIP : MessageEventArgs
    {
        string _FTPPath = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;

        public DownlaodXMLZIP(string FTPPath, string UserName, string Password)
        {
            _FTPPath = FTPPath;
            _UserName = UserName;
            _Password = Password;
        }
       

        void DownLoad(string GoXml)
        {
            string FtpUrl = _FTPPath;
            string FtpUsr = _UserName;
            string FtpPwd = _Password;
            string ExeLocationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //string DownloadPath = @"\\172.16.2.209\e$\Application\LWWAutoIntegrate" + "\\Download";
            //string DownloadPath = @"\\172.16.23.4\e$\Application\LWWAutoIntegrate" + "\\Download";
            //string DownloadPath = @"\\172.16.23.4\LWWAutoIntegrate" + "\\Download";
            string DownloadPath = System.Configuration.ConfigurationSettings.AppSettings["download_path"] + "\\Download";
            //string DownloadPath = ExeLocationPath + "\\Download";
            if (!Directory.Exists(DownloadPath))
            {
                Directory.CreateDirectory(DownloadPath);
            }


            //string ftpZip   = FtpUrl + GoXml;
            //string ftpGoXML = FtpUrl + GoXml.Replace(".zip", ".go.xml");
            string ftpZip = GoXml;
            string ftpGoXML = GoXml.Replace(".zip", ".go.xml");

            string DwnldZip   = DownloadPath + "\\" + Path.GetFileName(GoXml);
            string DwnldGoXML = DownloadPath + "\\" + Path.GetFileNameWithoutExtension(GoXml) + ".go.xml";

            FtpProcess FtpObj = new FtpProcess(FtpUrl, FtpUsr, FtpPwd);
            FtpObj.ProcessNotification += FtpObj_ProcessNotification;
            FtpObj.ErrorNotification += FtpObj_ErrorNotification;

            UpdateLWWDownloadStatus(GoXml, "Start");

            if (FtpObj.Download(DwnldGoXML, ftpGoXML) && FtpObj.Download(DwnldZip, ftpZip))
            {
                UpdateLWWDownloadStatus(GoXml, "Finish");
            }
            else
                UpdateLWWDownloadStatus(GoXml, "InProgress");

        }

        public void StartProcess()
        {

           // int ThreadCount = 1;

            GoXMLList GoXMLListObj = new GoXMLList();

            GoXMLListObj.AssignDownloadGoXMLList();
            int i = 0;
            foreach (string GoXml in GoXMLListObj.GoFileList)
            {
                i++;
                ProcessEventHandler("Processing file [" + i + "] Out of [" + GoXMLListObj.GoFileList.Count + "]");
                DownLoad(GoXml);
            }
        }

        /*
        List<Task> TaskList = new List<Task>();
        public async void StartProcess()
        {
            int ThreadCount = 1;
            GoXMLList GoXMLListObj = new GoXMLList();
            GoXMLListObj.AssignDownloadGoXMLList();
         
            foreach (string GoXml in GoXMLListObj.GoFileList)
            {
                DownLoad(GoXml);

                //await FtpDownLoadAsync(GoXml);
                //ThreadCount++;
                //if (ThreadCount > 10)
                //{
                //    Task.WaitAll(TaskList.ToArray());
                //    TaskList.Clear();
                //    ThreadCount = 1;
                //}
            }
        }
        async Task FtpDownLoadAsync(string GoXml)
        {
            TaskList.Add(Task.Run(() =>
            {

                string FtpUrl = _FTPPath;
                string FtpUsr = _UserName;
                string FtpPwd = _Password;
                string ExeLocationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string DownloadPath = ExeLocationPath + "\\Download";
                if (!Directory.Exists(DownloadPath))
                {
                    Directory.CreateDirectory(DownloadPath);
                }


                string ftpZip = FtpUrl + GoXml;
                string ftpGoXML = FtpUrl + GoXml.Replace(".zip", ".go.xml");

                string DwnldZip = DownloadPath + "\\" + Path.GetFileName(GoXml);
                string DwnldGoXML = DownloadPath + "\\" + Path.GetFileNameWithoutExtension(GoXml) + ".go.xml";

                FtpProcess FtpObj = new FtpProcess(FtpUrl, FtpUsr, FtpPwd);
                FtpObj.ProcessNotification += FtpObj_ProcessNotification;
                FtpObj.ErrorNotification += FtpObj_ErrorNotification;

                UpdateLWWDownloadStatus(GoXml, "Start");
                if (FtpObj.Download(DwnldZip, ftpZip))
                {
                    FtpObj.Download(DwnldGoXML, ftpGoXML);
                    UpdateLWWDownloadStatus(GoXml, "Finish");
                }
                else
                    UpdateLWWDownloadStatus(GoXml, "InProgress");

            }
            ));
        }
        */
        void FtpObj_ErrorNotification(System.Exception Ex)
        {
            ProcessErrorHandler(Ex);
        }

        void FtpObj_ProcessNotification(string NotificationMsg)
        {
            ProcessEventHandler(NotificationMsg);
        }
        void UpdateLWWDownloadStatus(string GoXmlPath, string Status)
        {
            string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@GoXmlPath", GoXmlPath);
            param[1] = new SqlParameter("@Status", Status);

            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWDownloadStatus]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }
        }

    }
}
