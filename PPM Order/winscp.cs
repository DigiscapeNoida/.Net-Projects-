using System;
using WinSCP;
using System.IO;
using PPM_TRACKING_SYSTEM;
class clsWinscp
{
    //Below commented and updated by kumar vivek on 13/05/2020
    //string glDirectory = "/ftpdata/outputbuckets/TOMBK/";
    string glDirectory = "/bts-ewii-prod-sftp/output/TOMBK/";
    public int DoUpload(string paths)
    {
        bool flag = false;
        try
        {
            string host = System.Configuration.ConfigurationSettings.AppSettings["UploadSFTPhost"];
            string uid = System.Configuration.ConfigurationSettings.AppSettings["UploadSFTPuid"]; ;
            string pwd = System.Configuration.ConfigurationSettings.AppSettings["UploadSFTPpwd"]; ;
            



            // Setup session options
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                //  FtpSecure= FtpSecure.ExplicitSsl,
                //HostName = "oxpzaps20l-sftp.elsevier.co.uk",
                //HostName = "ewiisftp.elsevier.com",
                HostName = host,
                UserName = uid,
                Password = pwd

                //UserName = "tombkpew",
                //Password = "tombkpu25189"
                // SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx",
                // FtpMode = FtpMode.Active           
            };


            using (Session session = new Session())
            {
                // Connect

                session.DisableVersionCheck = true;
                //Below commented and updated by kumar vivek on 06/05/2020
            //sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 a9:c8:c5:4a:dc:a7:22:0d:80:b8:f0:77:0a:73:24:bf";
            sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 0b:7e:f0:d1:f6:54:04:9b:aa:23:8c:14:b8:0b:10:a0";

                TimeSpan TS = new TimeSpan(120000000000);
                session.Timeout = TS;
                session.Open(sessionOptions);


                /*
                session.DisableVersionCheck = true;                
                sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 42:53:29:0b:7f:4f:06:ff:a4:eb:4a:1f:d3:1b:da:12";                
                sessionOptions.Timeout = new TimeSpan(1, 0, 0);
                session.Open(sessionOptions);
                  */                                      
                // download files
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                transferOptions.PreserveTimestamp = false;
                TransferOperationResult transferResult;
                //Below commented and updated by kumar vivek on 06/05/2020
                //transferResult = session.PutFiles(paths, "/ftpdata/dropzones/tombkpew/", false, transferOptions);
                transferResult = session.PutFiles(paths, "/bts-ewii-prod-sftp/input/tombkpew/", false, transferOptions);
                

                // Throw on any error
                transferResult.Check();

                // Print results

                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    flag = true;
                    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                    File.WriteAllText("c:\\sachin.txt", "transfered " + transfer.FileName);
                }
            }
            if (flag == false)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {
            File.WriteAllText("c:\\sachin.txt", "Error " + e.Message.ToString());
            Console.WriteLine("Error: {0}", e);
            return 1;
        }
    }
    public int downlaod(string Filename) 
    {
        try
        {

            string host = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPhost"];
            string uid = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPuid"]; ;
            //string pwd = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPpwd"]; ;
            string pwd = "8hH8tM&2Q8sJ=8c^8_e+$22Mv7+UjL";


            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                //   HostName = "oxpzaps20l-sftp.elsevier.co.uk",

                //   HostName = "ewiisftp.elsevier.com",
                //   UserName = "tombk",
                //   Password = "tombkgt15844"

                HostName = host,
                UserName = uid,
                Password = pwd




            };
            /*
             SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,                
                //HostName = "oxpzaps20l-sftp.elsevier.co.uk",
                HostName = "ewiisftp.elsevier.com",
                UserName = "tombk",
                Password = "tombkgt15844"
            };

            */
             using (Session session = new Session())
             {
                 session.DisableVersionCheck = true;
                 //sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 42:53:29:0b:7f:4f:06:ff:a4:eb:4a:1f:d3:1b:da:12";
                 sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 0b:7e:f0:d1:f6:54:04:9b:aa:23:8c:14:b8:0b:10:a0";
                 TimeSpan TS = new TimeSpan(120000000000);
                 session.Timeout = TS;
                 session.Open(sessionOptions);

                 /*session.DisableVersionCheck = true;
                 sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 42:53:29:0b:7f:4f:06:ff:a4:eb:4a:1f:d3:1b:da:12";
                 session.Open(sessionOptions);
                 */
                 string TLoc = "C:\\PPMTS\\PPMOrder" ;
                 if (!Directory.Exists(TLoc))
                 {
                     Directory.CreateDirectory(TLoc);
                 }
                 
                 string remotePath = glDirectory + Filename;
                 string localPath = TLoc + "\\" + Filename;

             
                 if (session.FileExists(remotePath))
                 {
                     bool download;
                     if (!File.Exists(localPath))
                     {
                         GlobalFunc.LogFunc("Download to local ::" + localPath);                       
                         download = true;
                     }
                     else
                     {
                         DateTime remoteWriteTime = session.GetFileInfo(remotePath).LastWriteTime;
                         DateTime localWriteTime = File.GetLastWriteTime(localPath);

                         if (remoteWriteTime > localWriteTime)
                         {
                             //Console.WriteLine(
                             //    "File {0} as well as local backup {1} exist, " +
                             //    "but remote file is newer ({2}) than local backup ({3})",
                             //    remotePath, localPath, remoteWriteTime, localWriteTime);
                             download = true;
                         }
                         else
                         {
                             //Console.WriteLine(
                             //    "File {0} as well as local backup {1} exist, " +
                             //    "but remote file is not newer ({2}) than local backup ({3})",
                             //    remotePath, localPath, remoteWriteTime, localWriteTime);
                             download = false;
                         }
                     }

                     if (download)
                     {
                         // Download the file and throw on any error
                         session.GetFiles(remotePath, localPath,false,null).Check();
                         GlobalFunc.LogFunc("Download to backup done :: " + Filename);
                     }
                 }
                 else
                 {
                     GlobalFunc.LogFunc("File does not exist at remote path :: " + Filename);
                 }
             }
             return 0;
        }
        catch (Exception)
        {
            return 1;             
        }    
    }
    public string[] ListDirectory()
    {
        try
        {
            // Setup session options


            string host = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPhost"];
            string uid = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPuid"];;
            //string pwd = System.Configuration.ConfigurationSettings.AppSettings["DownloadSFTPpwd"];;
            string pwd = "8hH8tM&2Q8sJ=8c^8_e+$22Mv7+UjL";


            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
             //   HostName = "oxpzaps20l-sftp.elsevier.co.uk",

             //   HostName = "ewiisftp.elsevier.com",
             //   UserName = "tombk",
             //   Password = "tombkgt15844"

                HostName = host,
                UserName = uid,
                Password = pwd




            };
             string[] files;
            using (Session session = new Session())
            {
                string fname = "";
                session.DisableVersionCheck = true;
                //sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 42:53:29:0b:7f:4f:06:ff:a4:eb:4a:1f:d3:1b:da:12";
                //Below commented and updated by kumar vivek on 13/05/2020 
                //sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 a9:c8:c5:4a:dc:a7:22:0d:80:b8:f0:77:0a:73:24:bf";
                sessionOptions.SshHostKeyFingerprint = "ssh-rsa 2048 0b:7e:f0:d1:f6:54:04:9b:aa:23:8c:14:b8:0b:10:a0";
                
                TimeSpan TS = new TimeSpan(120000000000);
                session.Timeout = TS;

                session.Open(sessionOptions);
                RemoteDirectoryInfo directory = session.ListDirectory(glDirectory);

                files = new string[directory.Files.Count];
                int i = 0;
                foreach (RemoteFileInfo fileInfo in directory.Files)
                {
                    if (fileInfo.Name.ToString().ToUpper().Trim().StartsWith("KU"))
                    {
                        files[i] = fileInfo.Name;
                        i++;
                    }                    
                }
            }
            return files;
        }
        catch (Exception e)
        {          
            return null;
        }    
    }

    //StreamWriter sw = new StreamWriter(TLoc + "\\" + FTPFile, false, Encoding.Default);
    //txtReplace = txtReplace.Replace("'UTF-8'?", "'windows-1252'?");
    //txtReplace = txtReplace.Replace("", "");
    //txtReplace = txtReplace.Replace("", "");    
}
