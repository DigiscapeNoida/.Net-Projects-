using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Contrast4ElsBooks
{
    public sealed class GlobalConfig
    {
        public static GlobalConfig _GlobalConfig = null;
        public static string _strPIT = "";
        
        string _MARKERLOCATION = "";
        string _OutLoaction = "";
        string _LOGPATH = "";
        string _SourcePath = "";
        string _ProcessedPath = "";     
        string _smtp = "";
        string _errTO = "";
        string _errfrm = "";
        string _errCC = "";
        string _MrkrPath = "";
        string _failedvtool = "";
        private GlobalConfig()
        {
           
            
            _LOGPATH = AppDomain.CurrentDomain.BaseDirectory + "\\LOG";

            if (!(Directory.Exists(_LOGPATH)))
            {
                Directory.CreateDirectory(_LOGPATH);
            }
            _failedvtool = ConfigurationSettings.AppSettings["Failedvtool"];
            _smtp = ConfigurationSettings.AppSettings["SMTP"];
            _errTO = ConfigurationSettings.AppSettings["ERRTo"];
            _errCC = ConfigurationSettings.AppSettings["ERRCC"];
            _errfrm = ConfigurationSettings.AppSettings["ERRFRM"];
            _OutLoaction = ConfigurationSettings.AppSettings["TargetPath"];  
            _ProcessedPath = ConfigurationSettings.AppSettings["Processed"];
            _SourcePath = ConfigurationSettings.AppSettings["Source"];
            _MrkrPath = ConfigurationSettings.AppSettings["Marker"];
          

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
        public string MARKERLOCATION
        {
            get
            {
                return _MARKERLOCATION;
            }
        }
        public string TARGET
        {
            get
            {
                return _OutLoaction;
            }
        }
        public string Source
        {
            get
            {
                return _SourcePath;
            }
        }
        public string ProcessPath
        {
            get
            {
                return _ProcessedPath;
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
        public string Failedvtool
        {
            get
            {
                return _failedvtool;
            }
        }

        public string Marker
        {
            get
            {
                return _MrkrPath;
            }
        }

       
        public string ERRFRM
        {
            get
            {
                return _errfrm;
            }
        } 
       /* public void Send_Error(string error,string attach)
        {
            try
            {
                GlobalConfig.oGlobalConfig.WriteLog(error);
                string format = "<p><font face=\"Arial\" size=\"2\">Dear Team ,</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">Please check following  :</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">" + error + "</font></p>";
                format = format + "<p><font face=\"Arial\" size=\"2\">This is auto genrated mail from PC Work flow</font></p>";
                string mailbody = "<html><head><title>ERROR</title></head><body>" + format + "</body></html>";
                Mailing mail = new Mailing();
                mail.SendMail(GlobalConfig.oGlobalConfig.ERRTo, "Auto PC Work flow dataset Uploaler", mailbody, GlobalConfig.oGlobalConfig.ERRCC, "", GlobalConfig.oGlobalConfig.ERRFRM, attach);

            }
            catch (Exception exx)
            {
                GlobalConfig.oGlobalConfig.WriteLog(error);
            }
        }*/
        public void Send_Error(string error, string isbn,string pii,string Status, string attach)
        {
            try
            {
                GlobalConfig.oGlobalConfig.WriteLog(error);
                string format = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" +
                        "<HTML><HEAD><TITLE></TITLE></HEAD><BODY>" +
                        "Dear Team,<br><br>" +
                        "Information : Please find the details as below.<br><br><br>" +
                        "Date :" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss tt") + "<br>" +
                        "ISBN/TOMBK : " + isbn + "<br> " +
                        "PII : " + pii  + "<br> " +
                        "Status : " + Status + "<br> " +
                        "Error :<div id=\"err_query_no\">" + error + ".</div> <br>" +
                        "Thanking You,<br>Software Team<br></BODY></HTML>";    

                Mailing mail = new Mailing();
                mail.SendMail(GlobalConfig.oGlobalConfig.ERRTo, " PC Dataset Status for [ "+ isbn +" _ " + pii +" ]", format, GlobalConfig.oGlobalConfig.ERRCC, "", GlobalConfig.oGlobalConfig.ERRFRM, attach);

            }
            catch (Exception exx)
            {
                GlobalConfig.oGlobalConfig.WriteLog(error);
            }
        }

        public bool ZipDir(string ZipPath,string Zipfolderpath )
        {
            try
            {
                if (File.Exists(ZipPath))
                {
                    File.Delete(ZipPath);
                }                                                                                      
                ZipFile.CreateFromDirectory(Zipfolderpath, ZipPath,CompressionLevel.NoCompression,true);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void WriteLog(string data)
        {
            Console.WriteLine(data);
            string FileName = LOGPATH;
            if (!(Directory.Exists(LOGPATH)))
            {
                Directory.CreateDirectory(LOGPATH);
            }
            FileName = FileName + "\\ErrorLog" + DateTime.Now.ToString("dd_MM_yyyy") + ".log";
            File.AppendAllText(FileName, data + Environment.NewLine);
        }
        public string ConvertToHex(string asciiString)
        {
            string hex = "";
            string mystring;
            foreach (char c in asciiString)
            {
                int tmp = c;
                string hexval = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                mystring = "&#x";
                int zeros = 5 - hexval.Length;
                for (int j = 0; j < zeros; j++)
                {
                    mystring = mystring + "0";
                }
                mystring = mystring + hexval;
                mystring = mystring + ";";
                hex += mystring;
            }
            return hex;
        }
        public bool Xcopy(string source, string dest, bool eOption, string EditName)
        {
            bool tflg = false;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "copy";// "xcopy";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (eOption == true)
                {

                    startInfo.Arguments = "\"" + source + "\"" + " " + "\"" + dest + "\"" + @" /e /y /I";
                }
                else
                {
                    //  startInfo.Arguments = "\"" + source + "\"" + " " + "\"" + dest + "\"" + @" /y /I";
                    startInfo.Arguments = "\"" + source + "\"" + " " + "\"" + dest + "\\" + EditName + "\"" + @" /y";
                }
                try
                {

                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                    tflg = true;
                }
                catch (Exception exp)
                {
                    tflg = false;
                }
                return tflg;
            }
            catch (Exception ex)
            {
                return false;
            }

        }  
        public string ReadySignal(string Tombkno,string zipname,string mdfive)
        {
            try
            {
                string Content = string.Empty;
                string signaltemp = AppDomain.CurrentDomain.BaseDirectory + "\\TombkSignal.xml" ;
                if(File.Exists(signaltemp))
                {
                  Content  =  File.ReadAllText(signaltemp);
                  Content = Content.Replace("[TOMBK]", Tombkno) ;
                  Content = Content.Replace("[ZIPNAME]", zipname);
                  Content = Content.Replace("[MD]", mdfive);
                  return Content;
                }
                else
                {
                Console.WriteLine("Error: Signal Template does not exist!!!");                
                } 
                return "ERROR";
              
            }
            catch (Exception exe)
            {
              Console.WriteLine(exe.Message.ToString());
                return "ERROR";
            }
        }
        public static string MDFive
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "\\AppData";
            }
        }
        public string GenerateMD5(string strVal)
        {
            try
            {

                string FilePath = MDFive.ToString();
                if (!Directory.Exists(FilePath))
                {
                    //MessageBox.Show("Not able to create directory \"" + FilePath + "\", Please check with technical team", "WebHosting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
                FilePath = FilePath + "\\MD5";
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                if (!Directory.Exists(FilePath))
                {
                    //MessageBox.Show("Not able to create directory \"" + FilePath + "\", Please check with technical team", "WebHosting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                DirectoryInfo DI = new DirectoryInfo(strVal);
                string FName = FilePath + "\\" + DI.Name + ".txt";
                StreamWriter sw = new StreamWriter(FName);
                sw.WriteLine(strVal);
                sw.Close();


                //MD5 Creation

                string strMD5 = MDFive.ToString();
                string strMDInput = FName;
                string strMDOutput = FName.Replace(".txt", ".checksum");
                string strProcess = strMD5;
                System.Diagnostics.Process MD5Process = new System.Diagnostics.Process();
                if (File.Exists(strMD5 + "\\MDFive.cmd"))
                {
                    MD5Process.StartInfo.FileName = strMD5 + "\\MDFive.cmd";
                }
                else
                {
                    MD5Process.StartInfo.FileName = strMD5 + "\\MDFive.bat";
                }

                MD5Process.StartInfo.Arguments = strMD5 + " " + strVal + " " + strMDOutput;
                MD5Process.EnableRaisingEvents = true;
                MD5Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                MD5Process.Start();
                MD5Process.WaitForExit();


                if (!File.Exists(strMDOutput))
                {
                    // MessageBox.Show("Not able to run MD5 checksum, please check", "WebHosting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                StreamReader sr = new StreamReader(strMDOutput);
                string FileC = sr.ReadToEnd();
                sr.Close();
                FileC = FileC.Trim();
                if (FileC.Length <= 32)
                {
                    // MessageBox.Show("Invalid checksum generated for MD5, please check", "WebHosting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
                string strCheckSum = FileC.Substring(0, FileC.IndexOf(" "));
                strCheckSum = strCheckSum.Trim();
                if (strCheckSum.Length != 32)
                {
                    // MessageBox.Show("Invalid checksum generated for MD5, please check", "WebHosting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
                return strCheckSum.ToLower();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public  void Convert_UTF8toENT(string filepath)
        {
            try
            {               
                string batpath = AppDomain.CurrentDomain.BaseDirectory + "AppData\\" + "ENTtoUTF8.bat";
                Process Pr = new Process();
                Pr.StartInfo.FileName = batpath;
                Pr.StartInfo.Arguments = "\"" + filepath + "\"" + " " + "\"" + filepath + "\"";
                Pr.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
              
                Pr.Start();
                Pr.WaitForExit();

                /*
                if (File.Exists(batpath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = batpath;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.Arguments = "\"" + filepath + "\"" + " " + "\"" + filepath + "\"";
                    
                    try
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    catch (Exception exp)
                    {  
                    }
                }
                else
                {
                   
                }*/
            }
            catch (Exception err)
            {

            }
        }
    }
}
