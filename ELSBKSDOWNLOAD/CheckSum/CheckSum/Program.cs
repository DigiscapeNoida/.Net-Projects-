using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CheckSum
{
    class Program
    {
        public static string host = "";
        public static string rootDirectory = "";
        public static string version = "";
        public static string userid = "";
        public static string password = "";
        public static string port = "";

        public static string processedpath = "";
        public static string unprocesspath = "";
        public static string downloadedpath = "";
        public static string processedfailpath = "";

        public static string key = "";
        public static string value = "";

        public static DataTable dt;

        List<string[]> sFTPList = new List<string[]>();
        static void Main(string[] args)
        {
            //downloadedpath = @"D:\Test\Downloaded";
            //processedpath = @"D:\Test\Processed";
            //unprocesspath = @"D:\Test\Unprocessed";
            //processedfailpath = @"D:\Test\ProcessedFail";
            logwriter("start");



            GetConfigDeatilsFromDB();
            logwriter("Config complete");


            GetSFTPdetailsFromDB();
            logwriter("download start");

            DownloadFromSFTP();
            ValidateCheckSum();
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
        public static void GetConfigDeatilsFromDB()
        {
            try
            {
                string constring = @"Data Source=172.16.23.8;Initial Catalog=Els_Books_PC_Flow;User id = sa;password=p@ssw0rd;";


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

        public static void GetSFTPdetailsFromDB()
        {
            try
            {
                // isko app config me dalo Rohit 1
                string constring = @"Data Source=172.16.23.8;Initial Catalog=Els_Books_PC_Flow;User id = sa;password=p@ssw0rd;";

                dt = new DataTable("SFTPDetails");

                using (var conn = new SqlConnection(constring))
                {
                    
                    string command = "SELECT * FROM SFTPDetails";

                    using (var cmd = new SqlCommand(command, conn))
                    {
                       
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                        conn.Open();
                        
                        adapt.Fill(dt);
                        
                        conn.Close();
                        
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void DownloadFromSFTP()
        {
            Program objProgram = new Program();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    host = row["Host"].ToString();
                    userid = row["UserID"].ToString();
                    password = row["Password"].ToString();
                    port = row["Port"].ToString();
                    rootDirectory = row["RootDirectory"].ToString();
                    version = row["Version"].ToString();
                    ArrayList FILELIST = objProgram.DownloadFile(host, userid, password, rootDirectory, unprocesspath);
                    //Code to download files from FTP to local system
                    foreach(var file in FILELIST)
                    {

                        if (file.ToString() != "test") {
                            if (!objProgram.IsAlreadyProcessed(file.ToString()))
                            {
                                logwriter("FileName = " + file.ToString());
                                string filesize = objProgram.DownloadFile_SFTP(host, userid, password, file.ToString(), unprocesspath, rootDirectory);
                                FileInfo objFileInfo = new FileInfo(unprocesspath + "\\" + file.ToString());
                                string size = objFileInfo.Length.ToString();
                                logwriter("FileSize = " + size.ToString());
                                if (filesize == size)
                                {
                                    // ye sahi download h isko process karna h

                                    // insert into db
                                    logwriter("Insert into Database");
                                    InsertIntoDatabase(file.ToString(), "Downloaded", version);
                                    // delete from ftp
                                    //DeleteFile(file.ToString(), host, userid, password);

                                    logwriter("Insert Success, Start to move file at = " + downloadedpath + "\\" + file.ToString());
                                    //file move krni hai i hai local vali unprocessed se download mai??
                                    if (!File.Exists(downloadedpath + "\\" + file.ToString()))
                                    File.Move(unprocesspath + "\\" + file.ToString(), downloadedpath + "\\" + file.ToString()); //Moved file from unprocessed to downloaded
                                }
                                else
                                {
                                    // ye fail ho gya isko delete karna h local se aur 5 baar retry karna h
                                    logwriter("Download failed FileName = " + file.ToString());

                                    bool check = false;
                                    File.Delete(unprocesspath + "\\" + file.ToString());
                                    for (int i = 0; i < 5; i++)
                                    {
                                        //Retry to download 5 times maximum
                                        string filesize1 = objProgram.DownloadFile_SFTP(host, userid, password, file.ToString(), unprocesspath, rootDirectory);
                                        FileInfo objFileInfo1 = new FileInfo(unprocesspath + "\\" + file.ToString());
                                        string size1 = objFileInfo1.Length.ToString();
                                        if (filesize1 == size1)
                                        {
                                            //INsert into db
                                            InsertIntoDatabase(file.ToString(), "Downloaded", version);

                                            // delete from ftp
                                            //DeleteFile(file.ToString(), host, userid, password);
                                            if(!File.Exists(downloadedpath + "\\" + file.ToString()))
                                            File.Move(unprocesspath + "\\" + file.ToString(), downloadedpath + "\\" + file.ToString()); //Moved file from unprocessed to downloaded
                                            check = true;
                                            break;
                                        }
                                        else
                                        {
                                            File.Delete(unprocesspath + "\\" + file.ToString());  //Delete file from local
                                        }
                                    }
                                    if (check == false)
                                    {
                                        InsertIntoDatabase(file.ToString(), "Failed", version);
                                    }
                                    //insert into db with status fail
                                }
                            }
                            
                        }
                    }


                }
            }
            catch (Exception Ex)
            {
                logwriter("Exception = " + Ex.ToString());
            }
        }

        public bool IsAlreadyProcessed(string fileName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=172.16.23.8;Initial Catalog=Els_Books_PC_Flow;User id = sa;password=p@ssw0rd;"))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_IsAlreadyProcessedDownload]", conn);
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
                logwriter("Exception = " + ex.ToString());
                return false;
            }
        }

        public ArrayList DownloadFile(string host, string userName, string password, string rootDirectory, string DownloadPath)
        {
            ArrayList fName = new ArrayList();
            try
            {
                String localFileName = "";
                String remoteFileName = "";
                using (var sftp = new SftpClient(host, userName, password))
                {
                    sftp.Connect();
                    //sftp.Dow
                    var files = sftp.ListDirectory(rootDirectory);

                    foreach (var file in files)
                    {
                        if (file.Name == "." || file.Name == "..")
                            continue;
                        fName.Add(file.Name);
                    }



                    sftp.Disconnect();
                }
                return fName;
            }
            catch (Exception ex)
            {
                logwriter("Exception = " + ex.ToString());
                return fName;
            }

        }
        public string DownloadFile_SFTP(string host, string userName, string password, string FileNameToDownload, string DownloadPath, string FTPDirectory)
        {
            string  fileSize = "";
            try
            {
                ArrayList fName = new ArrayList();
                String localFileName = "";
                String remoteFileName = @"Signals/" + FileNameToDownload;
                using (var sftp = new SftpClient(host, userName, password))
                {
                    sftp.Connect();

                    SftpFile filesftp = sftp.Get(remoteFileName);
                    var size = filesftp.Attributes.Size;
                    fileSize = size.ToString();
                    using (var file1 = File.OpenWrite(DownloadPath + "\\" + FileNameToDownload)) //Exception aara hai yahn kya exception h dikao
                    {
                        sftp.DownloadFile(remoteFileName, file1);
                    }
                    sftp.Disconnect();
                }
                return fileSize;
            }
            catch (Exception e)
            {
                logwriter("Exception = " + e.ToString());
                return "";
            }

        }


        public static void ValidateCheckSum()
        {
            try
            {
                string[] files = Directory.GetFiles(unprocesspath);
                if (files.Count() > 1)
                {
                    foreach (string file in files)
                    {
                        string MD5checksum_local = GetFileChecksum(file, new MD5CryptoServiceProvider());
                        string MD5checksum_ftp = GetFileChecksum(file, new MD5CryptoServiceProvider());

                        if (MD5checksum_local == MD5checksum_ftp)
                        {

                            //delete file from ftp
                        }
                        else if (MD5checksum_local != MD5checksum_ftp)
                        {
                            bool check = false;
                            //retry 5 times
                            for (int i = 0; i < 5; i++)
                            {
                                //baar baar download krna hai ya bss checksum check krna hai?

                                MD5checksum_local = GetFileChecksum(file, new MD5CryptoServiceProvider());
                                MD5checksum_ftp = GetFileChecksum(file, new MD5CryptoServiceProvider());

                                if (MD5checksum_local == MD5checksum_ftp)
                                {
                                    //delete file from ftp
                                    check = true;
                                    break;
                                }
                                else
                                {
                                    File.Delete(file); //delete file from local
                                    //download again same file from ftp?
                                }
                            }
                            if (check == false)
                            {
                                File.Delete(file);
                            }
                        }
                    }
                }
                
            }
            catch (Exception Ex)
            {
            }
        }

        public static string GetFileChecksum(string file, HashAlgorithm algorithm)
        {
            
                string result = string.Empty;

                using (FileStream fs = File.OpenRead(file))
                {
                    result = BitConverter.ToString(algorithm.ComputeHash(fs)).ToLower().Replace("-", "");
                }

                return result;
            
        }

        public static string DeleteFile(string fileName, string host, string userName, string password)
        {
            using (var sftp = new SftpClient(host, userName, password))
            {
                sftp.Connect();
                //sftp.Dow
                String remoteFileName = @"Signals/" + fileName;
                sftp.Delete(remoteFileName);

                sftp.Disconnect();
                return "OK";
            }
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + @"/Signals/" + fileName);
            //request.Method = WebRequestMethods.Ftp.DeleteFile;
            //request.Credentials = new NetworkCredential(userid, password);

            //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //{
            //    return response.StatusDescription;
            //}
        }

        public static void InsertIntoDatabase(string filename, string status, string version)
        {
            
            string constring = @"Data Source=172.16.23.8;Initial Catalog=Els_Books_PC_Flow;User id = sa;password=p@ssw0rd;";
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {

                    SqlCommand sqlcmd = new SqlCommand("[usp_InsertIntoSignalDownload]", conn);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@Filename", SqlDbType.VarChar).Value = filename;
                    sqlcmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
                    sqlcmd.Parameters.Add("@version", SqlDbType.VarChar).Value = version;

                    conn.Open();
                    sqlcmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                logwriter("Exception : " + ex.ToString());
            }
        }
    }
}
