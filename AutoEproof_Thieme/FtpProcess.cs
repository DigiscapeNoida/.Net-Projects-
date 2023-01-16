using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using ProcessNotification;

namespace AutoEproof
{
   

    class FtpProcess : MessageEventArgs
    {
        string _FTPPath  = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;

        StringCollection _NtfcnFiles = new StringCollection();

        public FtpProcess()
        {
            //_FTPPath  = StaticInfo.FTPPath;
            //_UserName = StaticInfo.FTPUName;
            //_Password = StaticInfo.FTPPWD;
        }
        public FtpProcess(string FTPPath, string UserName, string Password)
        {
            _FTPPath = FTPPath;
            _UserName = UserName;
            _Password = Password;
        }

        //public bool DisConnect()
        //{
        //    return true;
        //}
        //public bool Connect()
        //{
        //    return true;
        //}

        public StringCollection NtfcnFiles
        {
            get { return _NtfcnFiles; }
        }

      
        public  bool UploadFileToFTP(string source)
        {
            string filename = Path.GetFileName(source);
            string ftpfullpath = _FTPPath.TrimEnd('/') + "/" + filename;
            
            try
            {
                ProcessEventHandler("Start UploadFileToFTP");
             


                ProcessEventHandler("ftpfullpath :: " + ftpfullpath);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                
                ftp.Proxy = new WebProxy();

                ftp.Credentials = new NetworkCredential(_UserName, _Password);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                ProcessEventHandler("source :: " + source);
                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();

                ProcessEventHandler("End UploadFileToFTP");
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    WebClient upload = new WebClient();
                    upload.Credentials = new NetworkCredential(_UserName, _Password);
                    upload.UploadFile(ftpfullpath,"STOR", source);
                }
                catch (Exception ex1)
                {
                    ProcessErrorHandler(ex1);
                }
                ProcessErrorHandler(ex);
                return false;
            }
        }

        public bool CreateFtpFolder(string FtpFolderPath)
        {

            ProcessEventHandler("Start CreateFtpFolder");

            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FtpFolderPath);
                request.Credentials = new NetworkCredential(_UserName, _Password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                Console.WriteLine("Getting the response");
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
                ProcessEventHandler("End CreateFtpFolder");
                return true;
            }
            catch (WebException ex)
            {
                ProcessErrorHandler(ex);
            }
            return false;
            //request.
        }

        public bool FtpDirectoryExists(string directoryPath)
        {
            bool IsExists = true;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryPath);
                request.Credentials = new NetworkCredential(_UserName, _Password);
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                request.Proxy = new WebProxy();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                
            }
            catch (WebException ex)
            {
                IsExists = false;
                ProcessErrorHandler(ex);
            }
            return IsExists;
        }
    }
}

