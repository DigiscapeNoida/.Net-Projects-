using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using ProcessNotification;
using Renci.SshNet;

namespace LWWAutoIntegrate
{
    // delegate void NotifyMsg(string NotificationMsg);
    /// <summary>
    /// //////Dir==Directory
    /// </summary>
    class FtpProcess : MessageEventArgs
    {

        string _FTPPath = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;

        StringCollection _AlreadyFileDownLoad = new StringCollection();
        StringCollection _ALLPRFFile = new StringCollection();

        StringCollection _PRFFileDownLoad = new StringCollection();
        StringCollection _PRFFileDownLoadSuccessfully = new StringCollection();

        StringCollection _PRFFilesPathList = new StringCollection();

        public StringCollection PRFFilesPathList
        {
            get { return _PRFFilesPathList; }
        }
        public string[] AlreadyFileDownLoad
        {
            set
            {
                _AlreadyFileDownLoad.AddRange(value);
            }
            get
            {
                string[] ARR = new string[_AlreadyFileDownLoad.Count];
                _AlreadyFileDownLoad.CopyTo(ARR, 0);
                return ARR;
            }

        }
        public StringCollection ALLPRFFile
        {
            get { return _ALLPRFFile; }

        }
        public StringCollection PRFFileDownLoad
        {
            get { return _PRFFileDownLoadSuccessfully; }
        }
        public StringCollection ProcessDirs
        {
            get { return _ProcessDirs; }
        }
        StringCollection _ProcessDirs = new StringCollection();
        StringCollection _NtfcnFiles = new StringCollection();

        //public FtpProcess()
        //{
        //    _FTPPath  = StaticInfo.FTPPath;
        //    _UserName = StaticInfo.FTPUName;
        //    _Password = StaticInfo.FTPPWD;
        //}
        public FtpProcess(string FTPPath, string UserName, string Password)
        {
            _FTPPath = FTPPath;
            _UserName = UserName;
            _Password = Password;
        }

        public bool DisConnect()
        {
            return true;
        }
        public bool Connect()
        {
            return true;
        }

        public StringCollection NtfcnFiles
        {
            get { return _NtfcnFiles; }
        }

        public void DownLoadPRFFiles(string ExeLocationPath)
        {

            ProcessMessage("Download process start");
            ProcessMessage("Files on  ftp :: " + _PRFFileDownLoad.Count);

            string DownloadPath = ExeLocationPath + "\\PrfDownload\\" + DateTime.Now.ToShortDateString().Replace("/", "-");

            if (!Directory.Exists(DownloadPath))
            {
                Directory.CreateDirectory(DownloadPath);
            }
            foreach (string PRFFile in _PRFFileDownLoad)
            {
                if (PRFFile.EndsWith("prf.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    string DownloadFilePath = DownloadPath + "\\" + Path.GetFileName(PRFFile);
                    ProcessMessage("Start file download from ftp :: " + PRFFile);
                    Download(DownloadFilePath, PRFFile);
                }
            }
            ProcessMessage("Download process end..");
        }

        public List<string> GetFileList()
        {



            List<string> FilesWthExt = new List<string>();
            string FTPPath = _FTPPath;

            try
            {
                string[] ft = FTPPath.Split('/');
                using (var sftp = new SftpClient(ft[2].ToString(), _UserName, _Password))
                {
                    ProcessMessage("Connecting to " + ft[2].ToString() + " as " + _UserName);
                    sftp.Connect();
                    ProcessMessage("Connected!");
                    ProcessMessage("Getting file list from " + FTPPath);
                    var files = sftp.ListDirectory(ft[3].ToString());
                    foreach (var fl in files)
                    {
                        if (fl.Name.ToString().ToLower().EndsWith(".xml") || fl.Name.ToString().ToLower().EndsWith(".zip"))
                        {
                            FilesWthExt.Add(fl.FullName.Replace("/home/lww1/", ""));
                        }
                    }
                    ProcessMessage(FilesWthExt.Count.ToString() + " Files found");
                }
            }
            catch (Exception ex)
            {
                ProcessMessage("Error to connect FTP...");
                ProcessMessage("Error Message :: " + ex.ToString());
            }
            finally
            {
            }
            return FilesWthExt;
        }
        private List<string> GetFilesList(string FilesList, string ext)
        {
            List<string> FilesWthExt = new List<string>();
            string[] AllFiles = FilesList.Replace("\r", "").Split('\n');
            Array.Sort(AllFiles);
            foreach (string FL in AllFiles)
            {
                ProcessMessage(FL);
                if (FL.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    FilesWthExt.Add(Path.GetFileName(FL));
                    ProcessMessage(ext + " File Path :: " + FL);
                }
            }
            return FilesWthExt;
        }
        public bool Download(string DownloadFilePath, string FTPFileURL)
        {
            try
            {
                using (var sftp = new SftpClient(System.Configuration.ConfigurationSettings.AppSettings["FTPIP"], 22, System.Configuration.ConfigurationSettings.AppSettings["FtpUsr"], System.Configuration.ConfigurationSettings.AppSettings["FtpPwd"]))
                {
                    sftp.Connect();

                    using (var file = File.OpenWrite(DownloadFilePath))
                    {
                        sftp.DownloadFile(FTPFileURL, file);
                    }

                    sftp.Disconnect();
                }
                ProcessEventHandler("File downloaded.");


                FileInfo Fi = new FileInfo(DownloadFilePath);
                if (Fi.Length == 0)
                {
                    ProcessEventHandler("Fi.Length ==0");
                    return false;
                }
                ProcessEventHandler("Finish Download");
                return true;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                //System. MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void ProcessMessage(string Msg)
        {
            ProcessEventHandler(Msg);
        }

        public void GetProcessFolder(string ProcessPath)
        {
            RecurseFolder(ProcessPath);
        }

        private void RecurseFolder(string SrchPath)
        {
            StringCollection Dirs = GetDirList(SrchPath);

            foreach (string Fldr in Dirs)
            {
                RecurseFolder(Fldr);
            }
            _ProcessDirs.Add(SrchPath);
        }
        private StringCollection GetDirList(string FtpDirPath)
        {

            FtpDirPath = FtpDirPath.Replace("\\", "/");
            FtpDirPath = FtpDirPath.Replace("ftp:/ftp", "ftp://ftp");

            FtpDirPath = FtpDirPath.Replace("#", "%23"); //HttpUtility.UrlEncode(FtpDirPath);
            StringCollection FtpDirs = new StringCollection();
            ProcessMessage("Connecting FTP...");
            ProcessMessage("Connecting Path :: " + FtpDirPath);


            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpDirPath));

                reqFTP.Credentials = new NetworkCredential(_UserName, _Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                reqFTP.KeepAlive = true;

                ProcessMessage("Getting response from FTP...");
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                ProcessMessage("Response received...");

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                if (result.ToString().LastIndexOf('\n') != -1)
                {
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                }
                reader.Close();
                response.Close();

                ProcessMessage("Disconnect ftp...");
            }
            catch (Exception ex)
            {
                ProcessMessage("Error to connect FTP...");
                ProcessMessage("Error Message :: " + ex.ToString());
                return FtpDirs;
            }
            string[] AllFiles = result.ToString().Split('\n');
            Array.Sort(AllFiles);
            foreach (string FL in AllFiles)
            {
                ProcessMessage(FL);
                if (!string.IsNullOrEmpty(FL) & "#/n#".IndexOf(FL) == -1)
                {
                    string Extn = Path.GetExtension(FL);
                    if (string.IsNullOrEmpty(Extn) && Extn.Length != 4)
                    {
                        string LastPart = Path.GetFileNameWithoutExtension(FtpDirPath);
                        string FtpDir = Path.GetDirectoryName(FtpDirPath).Replace("\\", "/").Replace("ftp:/ftp", "ftp://ftp") + "/" + FL;
                        ProcessMessage("AA::" + FtpDir);

                        FtpDirs.Add(FtpDir);

                        //if (FL.StartsWith(LastPart))
                        //    FtpDirs.Add(Path.GetDirectoryName( FtpDirPath) + "/" + FL);
                        //else
                        //   FtpDirs.Add(FtpDirPath + "/" + FL);
                    }
                    else if (FL.EndsWith("prf.pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        //string PRFFilePath = FtpDirPath + "/" + FL;
                        string FtpDir = Path.GetDirectoryName(FtpDirPath).Replace("\\", "/").Replace("ftp:/ftp", "ftp://ftp");
                        string PRFFilePath = FtpDir + "/" + FL;

                        if (_AlreadyFileDownLoad.IndexOf(PRFFilePath) == -1)
                        {
                            _PRFFileDownLoad.Add(PRFFilePath);
                            ProcessMessage(PRFFilePath);
                        }
                        _ALLPRFFile.Add(PRFFilePath);
                        ProcessMessage("PRFFilePath :: " + PRFFilePath);
                    }
                }
            }
            return FtpDirs;
        }
    }
}

//Current Opinion Journals
//European Journal of Anaesthesiology
//Adverse Drug Reaction Bulletin
//Journal of Hypertension
//Journal of Cardiovascular Medicine
//Blood Coagulation and Fibrinolysis
//Reviews in Medical Microbiology
//Pathology
//AIDS
//Intervention