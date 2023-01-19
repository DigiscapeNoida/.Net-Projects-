using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Collections.Specialized;
using System.IO;
using System.Net;
using WinSCP;
using System.Configuration;
using Renci.SshNet;

namespace Contrast4ElsBooks
{
    class FtpProcess
    {
        string _FTPPath = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;
        string _FTPFolder = string.Empty;

        StringCollection _NtfcnFiles = new StringCollection(); 
        public FtpProcess()
        {

        }
        //public FtpProcess(string FTPPath, string UserName, string Password)
        public FtpProcess(string FTPPath, string strFolder, string UserName, string Password)
        {
            _FTPPath = FTPPath;
            _UserName = UserName;
            _Password = Password;
            _FTPFolder = strFolder;
        }
        public StringCollection NtfcnFiles
        {
            get { return _NtfcnFiles; }
        }
        
        public bool UploadFileToSFTP(string sourcefile)
        {

            try
            {
                bool flag = false;
                //=============================================================
                string host = "elsbkftp.proofcentral.com";
                string username = "tombk3";
                string password = "mfZrHFjFpNxrLpNp";
                int port = 22;
                //string host = "ftpsrv.thomsondigital.com";
                //string username = "lww1";
                //string password = "td$lww";
                //int port = 22;
                //=============================================================

                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    //=========================================
                    client.ChangeDirectory("/IN");
                    //client.ChangeDirectory("Test");
                    //=========================================
                    using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024;
                        client.UploadFile(fs, Path.GetFileName(sourcefile));
                        flag = true;
                    }
                }
                if (flag == false)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                //  File.WriteAllText("c:\\winscplog.txt", "Error " + e.Message.ToString());
                Console.WriteLine("Error: {0}", e);
                return false;
            }
        }
        public bool UploadFileToFTP(string sourcefile)
        {

            try
            {
                bool flag = false;
                //==================================================================
                String host = "ftp.elsevierproofcentral.com";
                String username = "tombk2";
                String password = "sKatYyTPxfUgrzNm";
                int port = 22;
                //string host = "ftpsrv.thomsondigital.com";
                //string username = "lww1";
                //string password = "td$lww";
                //int port = 22;
                //==================================================================

                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    //=====================================
                    client.ChangeDirectory("/IN");
                    //client.ChangeDirectory("Test");
                    //=====================================
                    using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024;
                        client.UploadFile(fs, Path.GetFileName(sourcefile));
                        flag = true;
                    }
                }
                if (flag == false)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
              //  File.WriteAllText("c:\\winscplog.txt", "Error " + e.Message.ToString());
                Console.WriteLine("Error: {0}", e);
                return false;
            }
        }
        public bool UploadFileToFTP_test(string paths)
        {
            bool flag = false;
            string uploadPath = "/IN/";
                //ConfigurationManager.AppSettings["UploadRemotePath"];

            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    //  FtpSecure= FtpSecure.ExplicitSsl,
                    //HostName = ConfigurationManager.AppSettings["HostName"],
                    //UserName = ConfigurationManager.AppSettings["UserName"],
                    //Password = ConfigurationManager.AppSettings["Password"]
                    HostName = _FTPPath,
                    UserName = _UserName,
                    Password = _Password
                };


                //using (Session session = new Session())
                //{
                //    // Connect
                //    session.DisableVersionCheck = true;
                //    //UAT
                //    //sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 a1:c6:94:0d:a8:1e:4f:67:7a:0d:dc:d7:85:16:gc:b7";
                //    //Live
                //    sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 da:3d:0d:b7:e9:e2:e9:af:8c:2d:01:91:f5:6e:1b:04";
                //    sessionOptions.Timeout = new TimeSpan(1, 0, 0);
                //    session.Open(sessionOptions);

                //    // download files
                //    TransferOptions transferOptions = new TransferOptions();
                //    transferOptions.TransferMode = TransferMode.Binary;

                //    TransferOperationResult transferResult;
                //    //transferResult = session.PutFiles(paths, uploadPath, false, transferOptions);
                //    transferResult = session.PutFiles(paths, uploadPath, false, transferOptions);

                //    // Throw on any error
                //    transferResult.Check();

                //    // Print results

                //    foreach (TransferEventArgs transfer in transferResult.Transfers)
                //    {
                //        flag = true;
                //        Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                //        //   File.WriteAllText("c:\\winscplog.txt", "transfered " + transfer.FileName);
                //    }
                //}
                if (flag == false)
                {
                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
              //  File.WriteAllText("c:\\winscplog.txt", "Error " + e.Message.ToString());
                Console.WriteLine("Error: {0}", e);
                return false;
            }
        }



        public bool UploadFileToFTP_FTWWE(string source)
        {
            string errrr = "";
            try
            {

                string filename = Path.GetFileName(source);
                string ftpfullpath = _FTPPath + "//" + filename;
                errrr = ftpfullpath;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Proxy = new WebProxy();
                ftp.Credentials = new NetworkCredential(_UserName, _Password);
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                
              //  ftp.Timeout = 500000000;
               
                ftp.Method = WebRequestMethods.Ftp.UploadFile;                
                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();                 
                
                return true;
            }
            catch (Exception ex)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error:File " + errrr + ex.Message.ToString());
                return false;
            }
        }
        public bool CreateFtpFolder(string FtpFolderPath)
        {



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

                return true;
            }
            catch (WebException ex)
            {
                throw ex;
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

            }
            return IsExists;
        }
    }
}
