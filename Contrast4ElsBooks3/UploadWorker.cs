using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Data;


namespace Contrast4ElsBooks
{
    class UploadWorker:Work
    {
        public UploadWorker()
        { 
        
        }
        private bool UpdateStatus(string tomno,string status)
        {
            try
            {
                string Query = "Update [PCDATASETINFO] set IsUploaded  = '" + status + "',UploadDate=Getdate()  where TombkNo='" + tomno + "'";
                DataAccess.ExecuteNonQuery(Query);
                return true;
            }
            catch (Exception)
            {
                return false;                  
            }
        }
        public bool UploadFinalData()
        {
            try
            {
               DataSet DS =  DataAccess.ExecuteDataSetSP(SPNames.GetUpload);
               if (DS != null)
               {
                   foreach (DataRow DRow in DS.Tables[0].Rows)
                   {
                      string Tomno =  (DRow["TombkNo"] != DBNull.Value) ? DRow["TombkNo"].ToString() : "";
                      string ISBN = (DRow["ISBN"] != DBNull.Value) ? DRow["ISBN"].ToString() : "";
                      string PII = (DRow["PII"] != DBNull.Value) ? DRow["PII"].ToString() : "";
                      string ftpid = (DRow["FtpDetails"] != DBNull.Value) ? DRow["FtpDetails"].ToString() : "";

                        if (Tomno.Trim() != "")
                      {
                         string tompath =  GlobalConfig.oGlobalConfig.TARGET + "\\" + Tomno;
                         if (Directory.Exists(tompath))
                         {
                             DirectoryInfo DI = new DirectoryInfo(tompath);

                             string targetfile = GlobalConfig.oGlobalConfig.TARGET + "\\" + DI.Name.Replace("TOMB","") + ".zip";
                             
                             var t = Task.Run(() =>
                             {
                                 GlobalConfig.oGlobalConfig.ZipDir(targetfile, tompath);
                             }); 
                             t.Wait();
                             if (File.Exists(targetfile))
                             {
                                 string signal = "";
                                 if (CreteSignalFile(Tomno, targetfile, out signal))
                                 {
                                     if (signal.Trim() != "")
                                     {
                                         FtpDetails oFtpDetails = new FtpDetails();
                                         //FtpProcess oFtpProcess = new FtpProcess("ftp://" + oFtpDetails.Host + "//" + oFtpDetails.Location, oFtpDetails.User, oFtpDetails.password);

                                         FtpProcess oFtpProcess = new FtpProcess(oFtpDetails.Host, oFtpDetails.Location, oFtpDetails.User, oFtpDetails.password);

                                         GlobalConfig.oGlobalConfig.WriteLog("Uploading Start Time for :: \"" + targetfile + "\": " + System.DateTime.Now);
                                            //Skip
                                            //if (true)
                                            if (ftpid == "2")
                                            {
                                                if (oFtpProcess.UploadFileToFTP(targetfile))
                                                {
                                                    GlobalConfig.oGlobalConfig.WriteLog("Uploading End Time for :: \"" + targetfile + "\": " + System.DateTime.Now);
                                                    // System.Threading.Thread.Sleep(90000);

                                                    GlobalConfig.oGlobalConfig.WriteLog("Uploading Start Time for :: \"" + signal + "\": " + System.DateTime.Now);
                                                    //Skip
                                                    // if (true)
                                                    if (oFtpProcess.UploadFileToFTP(signal))
                                                    {
                                                        GlobalConfig.oGlobalConfig.WriteLog("Uploading End Time for :: \"" + signal + "\": " + System.DateTime.Now);
                                                        UpdateStatus(Tomno, "DONE");
                                                        GlobalConfig.oGlobalConfig.Send_Error(" Data Set is created and uploaded successfully :: " + Tomno, ISBN, PII, "DONE", "");
                                                        //File.Delete(signal);
                                                        // File.Delete(targetfile);
                                                    }
                                                    else
                                                    {
                                                        UpdateStatus(Tomno, "FAIL");
                                                        GlobalConfig.oGlobalConfig.Send_Error("Unable to upload at ready signal :: " + Tomno, ISBN, PII, "Failed", "");
                                                    }
                                                }
                                                else
                                                {
                                                    GlobalConfig.oGlobalConfig.Send_Error("Error in uploading :: \"" + targetfile + "\": " + System.DateTime.Now, ISBN, PII, "Failed", "");

                                                    UpdateStatus(Tomno, "FAIL");
                                                    GlobalConfig.oGlobalConfig.Send_Error("Unable to upload at ftp :: " + Tomno, ISBN, PII, "Failed", "");
                                                }
                                            }
                                            if (ftpid == "3")
                                            {
                                                if (oFtpProcess.UploadFileToSFTP(targetfile))
                                                {
                                                    GlobalConfig.oGlobalConfig.WriteLog("Uploading End Time for :: \"" + targetfile + "\": " + System.DateTime.Now);
                                                    // System.Threading.Thread.Sleep(90000);

                                                    GlobalConfig.oGlobalConfig.WriteLog("Uploading Start Time for :: \"" + signal + "\": " + System.DateTime.Now);
                                                    //Skip
                                                    // if (true)
                                                    if (oFtpProcess.UploadFileToSFTP(signal))
                                                    {
                                                        GlobalConfig.oGlobalConfig.WriteLog("Uploading End Time for :: \"" + signal + "\": " + System.DateTime.Now);
                                                        UpdateStatus(Tomno, "DONE");
                                                        GlobalConfig.oGlobalConfig.Send_Error(" Data Set is created and uploaded successfully :: " + Tomno, ISBN, PII, "DONE", "");
                                                        //File.Delete(signal);
                                                        // File.Delete(targetfile);
                                                    }
                                                    else
                                                    {
                                                        UpdateStatus(Tomno, "FAIL");
                                                        GlobalConfig.oGlobalConfig.Send_Error("Unable to upload at ready signal :: " + Tomno, ISBN, PII, "Failed", "");
                                                    }
                                                }
                                                else
                                                {
                                                    GlobalConfig.oGlobalConfig.Send_Error("Error in uploading :: \"" + targetfile + "\": " + System.DateTime.Now, ISBN, PII, "Failed", "");

                                                    UpdateStatus(Tomno, "FAIL");
                                                    GlobalConfig.oGlobalConfig.Send_Error("Unable to upload at ftp :: " + Tomno, ISBN, PII, "Failed", "");
                                                }
                                            }
                                        }
                                     else
                                     {
                                         UpdateStatus(Tomno, "FAIL");
                                         GlobalConfig.oGlobalConfig.Send_Error("Unable to create ready signal at ftp :: " + Tomno,  ISBN, PII, "Failed", "");                                              
                                     }
                                 }
                                 else
                                 {
                                     UpdateStatus(Tomno, "FAIL");
                                     GlobalConfig.oGlobalConfig.Send_Error("Unable to create ready signal at ftp :: " + Tomno, ISBN, PII, "Failed", "");                                             
                                 }
                             }   
                             else
                             {
                                 GlobalConfig.oGlobalConfig.Send_Error("Zip file does not exist :: " + Tomno, ISBN, PII, "Failed", "");                                                                           
                             }                          

                         }
                         else
                         {
                             GlobalConfig.oGlobalConfig.Send_Error("TomBk does not exist for :: " + Tomno, ISBN, PII, "Failed", "");                                                                           
                            // tombk is now not exist for 
                         }
                      }
                   }
               }
               Console.WriteLine("************* Waiting for files for upload.");
               return true;
            }
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error :" + exe.Message.ToString());                                                                         
                return false;                
                //Tombk ---->                 
            }           
        }
        private bool  CreteSignalFile(string tombk,string zip,out string signalPath)
        {
            try
            {
                DirectoryInfo Di = new DirectoryInfo(zip);
                string filename = Di.Name.Replace(Di.Extension,"");
                string MDfive = GlobalConfig.oGlobalConfig.GenerateMD5(zip);
                MDfive = MDfive.ToLower();
                string SignalContent = GlobalConfig.oGlobalConfig.ReadySignal("TOMB" + filename, Di.Name, MDfive);
                if (SignalContent.Trim() == "")
                {
                    signalPath = "";
                    return false;
                }
                signalPath = Di.FullName.Replace(Di.Name,"") + "\\" + tombk + ".ready.xml";
                if (File.Exists(signalPath))
                {
                    File.Delete(signalPath);
                }
                File.AppendAllText(signalPath, SignalContent);
                return true;
            }
            catch (Exception exe)
            {
                signalPath = "";
                return false;
            }
        }
        public bool mTask()
        {
            try
            {
                if (UploadFinalData())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;                 
            }   
        }

        public bool OnError()
        {
            return true;
        }

        public bool OnSuccess()
        {
            return true;
        }
    }
}
