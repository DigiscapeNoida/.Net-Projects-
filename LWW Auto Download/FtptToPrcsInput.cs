using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProcessNotification;
using System.Data;
using System.IO;

namespace LWWAutoIntegrate
{


    class FtptToPrcsInput:MessageEventArgs
    {

        public FtptToPrcsInput()
        { 
        }
        public void StartProcess()
        {
            //2017-04-05 Pradeep Kushawah
            //Information of Host, User and Password is picked up from the Config file.
            string strFTP = System.Configuration.ConfigurationSettings.AppSettings["FTPIP"];
            string FtpUrl = "ftp://" + strFTP  + "/Set Up Peer Review";
            string FtpUsr = System.Configuration.ConfigurationSettings.AppSettings["FtpUsr"];
            string FtpPwd = System.Configuration.ConfigurationSettings.AppSettings["FtpPwd"];

            StartProcess(FtpUrl, FtpUsr, FtpPwd);

            FtpUrl = "ftp://" + strFTP  + "/Prepare First Proof";
            StartProcess(FtpUrl, FtpUsr, FtpPwd);


            FtpUrl = "ftp://" + strFTP + "/Revised Proof";
            StartProcess(FtpUrl, FtpUsr, FtpPwd);

            //FtpUrl = "ftp://" + strFTP + "/Prep For Proofreading";
            //FtpUrl = "ftp://" + strFTP + "/Correct Issue Proof";
            //StartProcess(FtpUrl, FtpUsr, FtpPwd);

            FtpUrl = "ftp://" + strFTP + "/Collate Correction";
            StartProcess(FtpUrl, FtpUsr, FtpPwd);

            //FtpUrl = "ftp://" + strFTP + "/Send to Compositor";
            //FtpUrl = "ftp://" + strFTP + "/Compose Issue Proof";
            //StartProcess(FtpUrl, FtpUsr, FtpPwd);


            FtpUrl = "ftp://" + strFTP + "/";


            DownlaodXMLZIP DwnldObj = new DownlaodXMLZIP(FtpUrl, FtpUsr, FtpPwd);
            DwnldObj.ProcessNotification += ProcessNotification;
            DwnldObj.ErrorNotification += ErrorNotification;
            DwnldObj.StartProcess();
        }
        void ErrorNotification(Exception Ex)
        {
            ProcessErrorHandler(Ex);
        }

        void ProcessNotification(string NotificationMsg)
        {
            ProcessEventHandler(NotificationMsg);
        }
        private void StartProcess(string FtpUrl, string FtpUsr, string FtpPwd)
        {
            FtpProcess FtpObj = new FtpProcess(FtpUrl,FtpUsr, FtpPwd);
            FtpObj.ProcessNotification += ProcessNotification;
            

            List<String> FtpFileList = FtpObj.GetFileList();

            if (FtpFileList.Count > 0)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString);
                try
                {
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("GOXMLPATH");
                    //foreach (var fn in FtpFileList)
                    //{
                    //    if (fn.ToString().Trim().ToLower().EndsWith(".zip"))
                    //    {
                    //        dt.Rows.Add(fn);
                    //    }
                    //}                 
                    //SqlBulkCopy objbulk = new SqlBulkCopy(conn);
                    //objbulk.DestinationTableName = "LWWWIP";
                    //objbulk.ColumnMappings.Add("GOXMLPATH", "GOXMLPATH");
                    conn.Open();
                    bool ins=false;
                    foreach (var fn in FtpFileList)
                    {
                        ins = false;
                        if (fn.ToString().Trim().ToLower().EndsWith(".zip"))
                        {
                            //string filename = System.Configuration.ConfigurationSettings.AppSettings["download_path"] + "\\Download\\" + fn.Split('/')[1];
                            //filename = filename.Replace(".zip", ".go.xml");
                            //if (File.Exists(filename))
                            //{
                            //    //FileInfo fi = new FileInfo(filename);
                            //    //if (fi.LastWriteTime < DateTime.Now.AddDays(-5))
                            //    //{
                            //        ins = false;
                            //    //}
                            //}
                            //else
                            //{
                                SqlDataAdapter da=new SqlDataAdapter("select SNO from LWWWIP where GOXMLPATH='"+fn.ToString()+"'", conn);
                                DataTable dt=new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    ins = false;
                                }
                                else
                                {
                                    ins = true;
                                }
                            //}                          
                        }
                        if (ins == true)
                        {
                            SqlCommand cmd = new SqlCommand("insert into LWWWIP (GOXMLPATH) values ('" + fn.ToString() + "')", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //insert bulk Records into DataBase.  
                    //objbulk.WriteToServer(dt);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    ProcessNotification(ex.Message);
                }
                finally
                {
                    if (conn.State.ToString() == "Open")
                    {
                        conn.Close();
                    }
                }
                //StringBuilder SrlzStr = new StringBuilder(SerializeClass.SerializeToXML(FtpFileList));

                //if (SrlzStr.Length > 0)
                //{
                //    SrlzStr.Replace("<string>", "<string><GoXMLPath>");
                //    SrlzStr.Replace("</string>", "</GoXMLPath></string>");

                    //InsertInDB(SrlzStr.ToString());
                //}
            }
        }
        private void InsertInDB(string Str)
        {
            string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@strXML", Str.Replace("<string />",""));

            try
            {

                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_lwwwip_insert_NEW]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }
        }
    }
}
