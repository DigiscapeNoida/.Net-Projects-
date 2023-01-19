using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PPM_TRACKING_SYSTEM.Classes.ClsObjects;
using System.IO;


namespace PPM_TRACKING_SYSTEM.Classes.XmlOperation
{
    
    class clsXMLOperation
    {
        string strordertype = "";
        string strcreationon = "";
        string strshorttitle = "";
        string strisbn = "";
        string strbooktitle = "";
        string strstatus = "";
        int ppmno = 1;
        string strppmno = "";
        //string strppmno = "1";
        string strppmddate = "";
        string strppmdate = DateTime.Now.ToString();
        string struploadstatus = "Pending";
        string strProdSite ="";
        string strplanduedate = "";

        string strissn = "";
        string strjid = "";
        string strseriestitle = "";
        string strvol_no = "";
        string strRemarks = "";
        string strlang = "";
        
        private XmlDocument objXDoc = null;
        //private XmlNode objXnd = null;
        //private XmlNodeList objXnl = null;
        private clsObjects objClsObj = null;

        private bool fileterXml(string FTPFile)
        {
            try
            {
                string TLoc;
            //if (!Directory.Exists(TLoc))
            //{
            //    Directory.CreateDirectory(TLoc);
            //}
                TLoc = FTPFile; //TLoc + "\\" + FTPFile;
            StreamReader reader = new StreamReader(TLoc, Encoding.Default);
            String txtReplace = reader.ReadToEnd();
            txtReplace = txtReplace.Replace("'UTF-8'?", "'windows-1252'?");
            txtReplace = txtReplace.Replace("", "");
            txtReplace = txtReplace.Replace("", "");
            txtReplace = txtReplace.Replace("", "");
            txtReplace = txtReplace.Replace( Convert.ToChar(29).ToString() , "");
            txtReplace = txtReplace.Replace(Convert.ToChar(28).ToString(), "");  
            reader.Close();
            
            StreamWriter sw = new StreamWriter(TLoc , false, Encoding.Default);                     
            sw.WriteLine(txtReplace);
            sw.Close();

            return true;
            }
            catch (Exception)
            {
                return false;                
            }
        
        }

        public void FetchData(String FilePath)
        {
            try 
            {


                String strfilename = new FileInfo(FilePath).Name;
                objClsObj = new clsObjects();
                objXDoc = new XmlDocument();
                //objXDoc.Load(FilePath);
                //objXDoc.Load("D:\\PPMXML\\KUP1288347263446-20101029_111453.xml");

                if (fileterXml(FilePath))
                {
                    GlobalFunc.LogFunc("Xml is filterd");
                }
                else
                {
                    GlobalFunc.LogFunc("Error: Unable to filter file!!!");
                }

                objXDoc.Load(FilePath);
                //String strfilename = new FileInfo("D:\\PPMXML\\KUP1288347263446-20101029_111453.xml").Name; 
                


                string dateNew = DateFormated(strfilename);

                if (dateNew == "")
                {
                    strcreationon = objXDoc.SelectSingleNode("//user-info/modified-on").InnerText;  //-19-05-14
                }
                else
                {
                    strcreationon = dateNew.Trim();
                }

                strordertype = objXDoc.SelectSingleNode("//orders/order/stage").Attributes["step"].Value;
                //strcreationon =objXDoc.SelectSingleNode("//user-info/created-on").InnerText;
                
                strshorttitle = objXDoc.SelectSingleNode("//short-title").InnerText;
                strisbn = objXDoc.SelectSingleNode("//isbn").InnerText;
                strbooktitle = objXDoc.SelectSingleNode("//book-title").InnerText;
                strProdSite = objXDoc.SelectSingleNode("//prod-site").InnerText;
               
                try
                {
                   
                    strRemarks="";
                    strlang="";
                    strissn="";
                    strjid ="";
                    strseriestitle="";

                   
                    try{
                        strlang = objXDoc.SelectSingleNode("//language").InnerText;
                        strRemarks = objXDoc.SelectSingleNode("//remarks").InnerText;
                    }
                    catch (Exception ERR) { }

                    strissn = objXDoc.SelectSingleNode("//issn").InnerText;
                    strjid = objXDoc.SelectSingleNode("//jid").InnerText;
                    strseriestitle = objXDoc.SelectSingleNode("//series-title").InnerText;
                    strvol_no = objXDoc.SelectSingleNode("//volume-number").InnerText;
                                     
                }
                catch (Exception ex)
                {
                   // GlobalFunc.LogFunc("Error:" + ex.ToString());
                }


                //strstatus = objXDoc.SelectSingleNode("//status").InnerText;
                //strstatus = "";
                //strppmno++;
                ppmno++;
                string strppmno = Convert.ToString(ppmno);

                //objClsObj.objDataOper.InsertData(strppmno, strfilename, strppmddate, strppmdate, strordertype,
                //                 strcreationon, strshorttitle, strisbn, strbooktitle, struploadstatus, strProdSite, strplanduedate);//Data inserted...


                Create_Marker(strisbn, strordertype);

                objClsObj.objDataOper.InsertData(strppmno, strfilename, strppmddate, strppmdate, strordertype,
                                 strcreationon, strshorttitle, strisbn, strbooktitle, struploadstatus, strProdSite, strplanduedate, strissn,strjid,strseriestitle,strvol_no);//Data inserted...
                try
                {
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Date</td><td>" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt") + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Title</td><td>" + strshorttitle + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Stage</td><td>" + strordertype + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>File Name</td><td>" + strfilename + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>ISBN</td><td>" + strisbn + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Language</td><td>" + strlang + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Remarks</td><td>" + strRemarks + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>Prod Site</td><td>" + strProdSite + "</td></tr>";
                    PPM.mailcontent = PPM.mailcontent + "<tr><td>--------------</td><td>--------------</td></tr>";
                }
                catch (Exception rt)
                {
                    GlobalFunc.LogFunc("Error:" + rt.ToString());
                }
                
                string PPMOrderPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
                PPMOrderPath = PPMOrderPath + "\\" + strisbn.Replace("-", "") + "\\" + strordertype + "\\Current_order";
                if (!Directory.Exists(PPMOrderPath))
                    Directory.CreateDirectory(PPMOrderPath);

                FileInfo[] FInfo = new DirectoryInfo(PPMOrderPath).GetFiles();
                if (FInfo.Length > 0)
                { //To be done

                    for (int i = 0; i < FInfo.Length; i++)
                    { // check sachin error --later
                        File.Move(FInfo[i].FullName, PPMOrderPath.Replace("Current_order", "") + FInfo[i].Name);
                        if (!(File.Exists(PPMOrderPath + "\\" + strfilename)))
                        {
                            File.Copy(FilePath, PPMOrderPath + "\\" + strfilename);
                        }
                    }
                }
                else
                {
                    //if (File.Exists(strfilename)) \\--changed by sachin 
                    if (!(File.Exists( PPMOrderPath + "\\" + strfilename)))
                    {
                        File.Copy(FilePath, PPMOrderPath + "\\" + strfilename);
                    }
                }       
            }
            catch (Exception ex)
            {
                //-|- Write error to Log -//
                GlobalFunc.LogFunc("Error:Download_FTPFile:5" + ex.Message);
            }
        }
        private void  Create_Marker(string isbn , string Stg)
        {
        try
        {

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\marker";
            if (!(Directory.Exists(filepath)))
            {
                Directory.CreateDirectory(filepath);
            }

            filepath = filepath + "\\" + isbn + "_" + Stg + ".txt";
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
               
            }
            
            File.Create(filepath);
            
            GlobalFunc.LogFunc("marker creaed" + filepath);

        }
         catch(Exception err)
        {
            GlobalFunc.LogFunc("Error:While creating markers" + err.Message);
        }
        
       }
        //private string DateFormated(String  filename)
        //{
        //    string temp;
        //    try
        //    {
        //        temp = filename;
        //        temp = temp.Substring(temp.IndexOf("-"), temp.Length - temp.IndexOf("-"));
        //        temp = temp.Substring(1, temp.IndexOf("_") - 1);
        //        char[] ch = temp.ToCharArray();
        //        string date = "";

        //        if (ch.Length > 7)
        //        {
        //            date = ch[0].ToString() + ch[1].ToString() + ch[2].ToString() + ch[3].ToString() + "-" + ch[4].ToString() + ch[5].ToString() + "-" + ch[6].ToString() + ch[7].ToString();
        //        }
        //        date = date + " 00:00:00";

        //        return date;
        //    }
        //    catch (Exception err)
        //    {
        //        GlobalFunc.LogFunc("Unable to format date" + err.Message);
        //        return "";
        //    }

        //}
        private static string DateFormated(string filename)
        {
            string temp;
            try
            {
                temp = filename;
                temp = temp.Substring(temp.IndexOf("-") + 1, temp.Length - temp.IndexOf("-") - 1);

                string[] td = temp.Split('_');
                char[] ch = td[0].ToCharArray();
                string date = "";

                if (ch.Length > 7)
                {
                    date = ch[0].ToString() + ch[1].ToString() + ch[2].ToString() + ch[3].ToString() + "-" + ch[4].ToString() + ch[5].ToString() + "-" + ch[6].ToString() + ch[7].ToString();
                }
                string time = td[1].Substring(0, td[1].IndexOf(".xml"));
                char[] chT = time.ToCharArray();
                time = chT[0].ToString() + chT[1].ToString() + ":" + chT[2].ToString() + chT[3].ToString() + ":" + chT[4].ToString() + chT[5].ToString();

                date = date + "T" + time;

                return date;
            }
            catch (Exception err)
            {
                GlobalFunc.LogFunc("Error:" + err.ToString());
                return "";
            }

        }
        public int  CheckData(String FName)
        {
            try
            {
                objClsObj = new clsObjects();                
                int strStatus  = objClsObj.objDataOper.CheckData(FName);//Data inserted...
                return strStatus;

            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Error:Download_FTPFile:6" + ex.Message);
                return 2;

                //Write error to Log
            }
        }

        public int Record_Count_Archive(String FName)
        {
            try
            {
                objClsObj = new clsObjects();                
                int strStatus = objClsObj.objDataOper.CheckData(FName);//Data inserted...
                return strStatus;
            }
            catch (Exception ex)
            {
                GlobalFunc.LogFunc("Error:" + ex.ToString());
                return 0;
                //Write error to Log
            }
        }

    }
}
