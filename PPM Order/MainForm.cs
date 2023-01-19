using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using PPM_TRACKING_SYSTEM.Classes.ClsObjects;
using PPM_TRACKING_SYSTEM.Classes.Connection;



namespace PPM_TRACKING_SYSTEM
{
    public partial class PPM : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds = null;
        SqlDataAdapter adap;
        bool header = true;

       public static string mailcontent = "";
        string Mfrom ="";
        string MTo = "";
        string MCC = "";
        string Host = "";        
        
        public PPM()
        {            
            InitializeComponent();
        }
        private void InitializeListView()
        {
            
        }
        public void DownloadInformation_Old()
        {
            try
            {
                GlobalFunc.LogFunc("Download Information");
                string[,] FTPGrid;
                FtpWebRequest request;
                string strDIP =  System.Configuration.ConfigurationSettings.AppSettings["Downloadhost"];
                string strDFiles = System.Configuration.ConfigurationSettings.AppSettings["DownloadF"];
                string strDUid = System.Configuration.ConfigurationSettings.AppSettings["Downloaduid"];
                string strDPwd = System.Configuration.ConfigurationSettings.AppSettings["Downloadpwd"];

                GlobalFunc.LogFunc("Information: " + strDIP + ":" + strDFiles + ":" + strDUid + ":" + strDPwd);

                string host = "";
                if (strDFiles.Length > 0)
                    host = "ftp://" + strDIP + "/" + strDFiles + "/";
                else
                    host = "ftp://" + strDIP + "/";

                GlobalFunc.LogFunc("Host: " + host);

                request = (FtpWebRequest)WebRequest.Create(host);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(strDUid, strDPwd);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream,Encoding.Default);
                string FTPCont = reader.ReadToEnd();

                string FTPFiles = FilesInformation(FTPCont);

                string[] FTPArr = FTPFiles.Split(';');
                
                FTPGrid = new string[FTPArr.Length, 3];

                GlobalFunc.LogFunc("FTP Items count: " + FTPArr.Length);
                for (int i = 0; i < FTPArr.Length; i++)
                {
              
                    if (FTPArr[i].Length == 0)
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":Empty");
                        FTPGrid[i, 0] = "";
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "";
                        continue;
                    }

                     if (isDownload(FTPArr[i]) == false)
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":" + FTPArr[i]);
                        Download_FTPFile(FTPArr[i]);
                        FTPGrid[i, 0] = FTPArr[i];
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "Downloaded";
                    }
                    else
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":" + FTPArr[i]);
                        FTPGrid[i, 0] = FTPArr[i];
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "Downloaded";
                    }

                }

                string[] FMInfo;
                string[] SplitOpt1 = new string[1];
                SplitOpt1[0] = "<S>";
                if (header == true)
                {
                    grdserver.Columns.Add("Filename", "Filename");
                    grdserver.Columns.Add("colDate", "Date");
                    grdserver.Columns.Add("Status", "Status");
                    header = false;
                }
                else
                {
 
                }

                if (FTPArr.Length > 0)
                {
                    grdserver.Rows.Add(FTPArr.Length);
                }
                string[] SplitOpt2 = new string[1];

                for (int i = 0; i < FTPArr.Length; i++)
                {
                    grdserver.Rows[i].Cells[0].Value = FTPGrid[i, 0];
                    grdserver.Rows[i].Cells[1].Value = FTPGrid[i, 1];
                    grdserver.Rows[i].Cells[2].Value = FTPGrid[i, 2];                    
                }
                grdserver.Refresh();
                for (int i = 0; i < FTPArr.Length; i++)
                {
                    if (FTPArr[i].Length == 0)
                    {
                        continue;
                    }
                  
                    string PPMFilename = FTPGrid[i, 0];
                    if (File.Exists(@"C:\PPMTS\PPMOrder\" + PPMFilename))
                    {
                        clsObjects obj = new clsObjects();
                        if (isDownload(PPMFilename.Trim()) == false)
                        {
                            obj.objXMLOper.FetchData(@"C:\PPMTS\PPMOrder\" + PPMFilename);
                        }                     
                        string PPath = @"C:\PPMTS\PPMOrder\Processed\";

                        if (!Directory.Exists(PPath))
                        {
                            Directory.CreateDirectory(PPath);
                        }
                        File.Delete(@"C:\PPMTS\PPMOrder\" + PPMFilename);
                    }                 
                    
                }
                if (mailcontent.Contains("---"))
                {
                    try
                    {
                        mailcontent = mailcontent + "</table><br>";
                        mailcontent = mailcontent +  "This is the auto genrated mail by PPM downloader";
                        clsEmail obj = new clsEmail();

                        obj.subject = "New PPM orders received";
                        obj.fromEmail = Mfrom;
                        obj.smtpServer =Host;
                        obj.messageBody = mailcontent;
                        obj.SendEmail(MTo, MCC);
                    }
                    catch(Exception ex)
                    {                      
                        GlobalFunc.LogFunc("Error:Download_FTPFile:1" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Excption: DownloadInformation:2" + ex.Message); 
            }
        }
        public void DownloadInformation()
        {
            try
            {
                GlobalFunc.LogFunc("Download Information");
                string[,] FTPGrid;
                FtpWebRequest request;
                string strDIP = System.Configuration.ConfigurationSettings.AppSettings["Downloadhost"];
                string strDFiles = System.Configuration.ConfigurationSettings.AppSettings["DownloadF"];
                string strDUid = System.Configuration.ConfigurationSettings.AppSettings["Downloaduid"];
                string strDPwd = System.Configuration.ConfigurationSettings.AppSettings["Downloadpwd"];

                GlobalFunc.LogFunc("Information: " + strDIP + ":" + strDFiles + ":" + strDUid + ":" + strDPwd);

              /*  string host = "";
                if (strDFiles.Length > 0)
                    host = "ftp://" + strDIP + "/" + strDFiles + "/";
                else
                    host = "ftp://" + strDIP + "/";

                GlobalFunc.LogFunc("Host: " + host);

                request = (FtpWebRequest)WebRequest.Create(host);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(strDUid, strDPwd);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                string FTPCont = reader.ReadToEnd();

                string FTPFiles = FilesInformation(FTPCont);

                string[] FTPArr = FTPFiles.Split(';');
                */
                clsWinscp oWsc = new clsWinscp();
                string[] FTPArr = oWsc.ListDirectory();

                FTPGrid = new string[FTPArr.Length, 3];

                GlobalFunc.LogFunc("FTP Items count: " + FTPArr.Length);
                for (int i = 0; i < FTPArr.Length; i++)
                {
                    if (FTPArr[i] == null)
                    {
                        continue;
                    }

                    if (FTPArr[i].Length == 0)
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":Empty");
                        FTPGrid[i, 0] = "";
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "";
                        continue;
                    }

                    if (isDownload(FTPArr[i]) == false)
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":" + FTPArr[i]);
                        //Download_FTPFile(FTPArr[i]);
                        oWsc.downlaod(FTPArr[i].ToString());
                        FTPGrid[i, 0] = FTPArr[i];
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "Downloaded";
                    }
                    else
                    {
                        GlobalFunc.LogFunc("Item: " + i + ":" + FTPArr[i]);
                        FTPGrid[i, 0] = FTPArr[i];
                        FTPGrid[i, 1] = "";
                        FTPGrid[i, 2] = "Downloaded";
                    }

                }

                string[] FMInfo;
                string[] SplitOpt1 = new string[1];
                SplitOpt1[0] = "<S>";
                if (header == true)
                {
                    grdserver.Columns.Add("Filename", "Filename");
                    grdserver.Columns.Add("colDate", "Date");
                    grdserver.Columns.Add("Status", "Status");
                    header = false;
                }
                else
                {

                }

                if (FTPArr.Length > 0)
                {
                    grdserver.Rows.Add(FTPArr.Length);
                }
                string[] SplitOpt2 = new string[1];

                for (int i = 0; i < FTPArr.Length; i++)
                {
                    grdserver.Rows[i].Cells[0].Value = FTPGrid[i, 0];
                    grdserver.Rows[i].Cells[1].Value = FTPGrid[i, 1];
                    grdserver.Rows[i].Cells[2].Value = FTPGrid[i, 2];
                }
                grdserver.Refresh();
                for (int i = 0; i < FTPArr.Length; i++)
                {
                    if ((FTPArr[i]==null) || FTPArr[i].Length == 0)
                    {
                        continue;
                    }

                    string PPMFilename = FTPGrid[i, 0];
                    if (File.Exists(@"C:\PPMTS\PPMOrder\" + PPMFilename))
                    {
                        clsObjects obj = new clsObjects();
                        if (isDownload(PPMFilename.Trim()) == false)
                        {
                            obj.objXMLOper.FetchData(@"C:\PPMTS\PPMOrder\" + PPMFilename);
                        }
                        string PPath = @"C:\PPMTS\PPMOrder\Processed\";

                        if (!Directory.Exists(PPath))
                        {
                            Directory.CreateDirectory(PPath);
                        }
                        File.Delete(@"C:\PPMTS\PPMOrder\" + PPMFilename);
                    }

                }
                if (mailcontent.Contains("---"))
                {
                    try
                    {
                        mailcontent = mailcontent + "</table><br>";
                        mailcontent = mailcontent + "This is the auto genrated mail by PPM downloader";
                        clsEmail obj = new clsEmail();

                        obj.subject = "New PPM orders received";
                        obj.fromEmail = Mfrom;
                        obj.smtpServer = Host;
                        obj.messageBody = mailcontent;
                        obj.SendEmail(MTo, MCC);
                    }
                    catch (Exception ex)
                    {
                        GlobalFunc.LogFunc("Error:Download_FTPFile:1" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Excption: DownloadInformation:2" + ex.Message);
            }
        }
        public bool isDownload(string FName)
        {

            clsObjects obj = new clsObjects();
            if (obj.objXMLOper.CheckData(FName) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Download_FTPFile(string FTPFile)
        {

            try
            {
                GlobalFunc.LogFunc("File: " + FTPFile);
                FtpWebRequest request;
                string strDIP = System.Configuration.ConfigurationSettings.AppSettings["Downloadhost"];
                string strDFiles = System.Configuration.ConfigurationSettings.AppSettings["DownloadF"];
                string strDUid = System.Configuration.ConfigurationSettings.AppSettings["Downloaduid"];
                string strDPwd = System.Configuration.ConfigurationSettings.AppSettings["Downloadpwd"];

                string host = "";
                if (strDFiles.Length > 0)
                    host = "ftp://" + strDIP + "/" + strDFiles + "/";
                else
                    host = "ftp://" + strDIP + "/";


                request = (FtpWebRequest)WebRequest.Create(host + FTPFile);
                //request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(strDUid, strDPwd);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse( );
                

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream,Encoding.Default );
                string TLoc = "C:\\PPMTS\\PPMOrder";
                if (!Directory.Exists(TLoc))
                {
                    Directory.CreateDirectory(TLoc);
                }
                StreamWriter sw = new StreamWriter(TLoc + "\\" + FTPFile, false, Encoding.Default);
                String txtReplace = reader.ReadToEnd();
                txtReplace = txtReplace.Replace("'UTF-8'?", "'windows-1252'?");
                txtReplace = txtReplace.Replace("", "");
                txtReplace = txtReplace.Replace("", "");                
                sw.WriteLine(txtReplace);
                sw.Close();
            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Error: Download_FTPFile:3" + ex.Message);
            }

        }      
        public string FilesInformation(string FTPCont)
        {
            string RetStr = "";

            string[] SplitOpt1 = new string[4];
            SplitOpt1[0] = "\n\r";
            SplitOpt1[1] = "\r\n";
            SplitOpt1[2] = "\r";
            SplitOpt1[3] = "\n";

            try
            {
                Regex reg = new Regex(@"kup[a-z0-9\-_]+\.xml");
                MatchCollection match = reg.Matches(FTPCont.ToLower());
                if (match.Count > 0)
                {
                    for (int i = 0; i < match.Count; i++)
                    {
                        if (RetStr.IndexOf(match[i].Value) == -1)
                        {
                            RetStr = RetStr + ";" + match[i].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Error:" + ex.ToString());
            }
 
           /* string[] Arr = FTPCont.Split(SplitOpt1,StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < Arr.Length; i++)
            {
               /* if (Arr[i].StartsWith("d"))
                {
                    continue;
                }*/
             /*   if (!Arr[i].ToLower().EndsWith(".xml"))
                {
                    continue;
                }*/
                /*string TempS = Arr[i].Substring(30);
                TempS = TempS.Trim();
                while (TempS.IndexOf("  ") != -1)
                {
                    TempS = TempS.Replace("  ", " ");
                }
                string[] TempArr = TempS.Split(' ');
                if (TempArr.Length == 5)
                { 

                }*/
            /*    string TempS = Arr[i].Substring(Arr[i].IndexOf("KUP"));

                RetStr = RetStr + ";" + TempS;
            }*/
            return RetStr;

        }

        private void PPM_Load(object sender, EventArgs e)
        {

            clsObjects obj3 = new clsObjects();
            //obj3.objXMLOper.FetchData(@"C:\Users\59928\Desktop\KUP1464958871965_122060-20160603_140114.xml");
            
            //time Being this.GetData("SELECT PPMFilename,PPMDate,UploadStatus from PPM_Information");
            GlobalFunc.LogFunc("*******************************************");
            GlobalFunc.LogFunc("Date: " + DateTime.Now.ToLongTimeString());

            mailcontent = "Dear All " ;
            mailcontent = mailcontent + "<br><br><br>" + "Please note that new PPM(s) has been downloaded. The details for the same are listed below.";

           Mfrom = System.Configuration.ConfigurationSettings.AppSettings["Efrom"];
           MTo = System.Configuration.ConfigurationSettings.AppSettings["EmailTo"];
           MCC = System.Configuration.ConfigurationSettings.AppSettings["EmailCC"];
           Host = System.Configuration.ConfigurationSettings.AppSettings["EHost"];
            
          //DriveInfo dinf = new DriveInfo("Q");
          if (Directory.Exists(@"\\172.16.28.2\Elsinpt"))
          {
                mailcontent = mailcontent + "<table>"; 
                DownloadInformation();
          }
          else
          {
           try
                {
                    clsEmail obj = new clsEmail();
                    obj.subject = "PPM Order Downloader - Error";
                    obj.fromEmail = Mfrom;
                    obj.smtpServer = Host;
                    obj.messageBody = "Dear All " + "<br><br><br>" + "Connectivity issue found between server 172.16.27.8 and 172.16.28.2. Please contact to IT." + "<br><br><br>" + "Regards " + "<br><br>" + "Auto mail";
                    obj.SendEmail(MTo, MCC);
                }
                catch (Exception es)
                {
                    GlobalFunc.LogFunc("Error:Download_FTPFile:4" + es.Message);
                }
            
            }                       
            GlobalFunc.LogFunc(" ");
            GlobalFunc.LogFunc(" ");           
            this.GetData("SELECT PPMFilename,PPMDate from PPM_Information");
        }

        private void GetData(string query)
        {
            try
            {                               
               
                //con = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PPMTrackingSystem;Data Source=TD-KC5");
                string strcon = System.Configuration.ConfigurationSettings.AppSettings["ConnString"];
                con = new SqlConnection(strcon);
                adap = new SqlDataAdapter(query, con);
                ds = new DataSet();
                adap.Fill(ds);
                grdserver2.DataSource = ds.Tables[0];

            }
            catch
            {
                
                //throw;
            }
            finally
            {
                adap.Dispose();
                ds.Dispose();
                con.Close();
            }
        }

        private void grdserver_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
