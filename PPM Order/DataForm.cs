using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Diagnostics;
using PPM_TRACKING_SYSTEM.Classes.ClsObjects;
//using WinSCP;
namespace PPM_TRACKING_SYSTEM
{
    public partial class DataForm : Form
    {
        clsObjects objClsObj = new clsObjects();
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds = null;
        SqlDataAdapter adap;
        XmlDocument xdoc;

        public DataForm()
        {
             InitializeComponent();
            
        }

        private void GetData(string query)
        {
            string url = System.Configuration.ConfigurationSettings.AppSettings["databaseurl"];
            if (PingHost(url) == false)
            {                
                MessageBox.Show("Error 6: Connectivity with database server failed.");
                return;
            }
            try
            {
                string strcon = System.Configuration.ConfigurationSettings.AppSettings["ConnString"];
                con = new SqlConnection(strcon);
                adap = new SqlDataAdapter(query, con);
                ds = new DataSet();                
                adap.Fill(ds);
                grdvw.DataSource = ds.Tables[0];
                
               for (int i = 0; i < grdvw.Rows.Count; i++)
                {
                    if (grdvw.Rows[i].Cells[6].Value.ToString() == "Pending")
                    {
                        grdvw.Rows[i].Cells[6].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        grdvw.Rows[i].Cells[6].Style.ForeColor = Color.Green;
                    }
                }  
            }
            catch(Exception ex)
            {                
                MessageBox.Show("Error 7: Connectivity with database failed.");
            }
            finally
            {
                adap.Dispose();
                ds.Dispose();
                con.Close();
            }
        }


        private void DataForm_Load(object sender, EventArgs e)
        {
            load_ampping();
            string url = System.Configuration.ConfigurationSettings.AppSettings["databaseurl"];
            if (PingHost(url) == false)
            {
                MessageBox.Show("Error 6: Connectivity with database server failed.");
                Application.Exit();
            }

            string OPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];

            if (!Directory.Exists(OPath))
            {
                MessageBox.Show("Error 8: Connectivity with the td-nas is not mapped.");
                Application.Exit();
            }

            Filters();

            //this.GetData("SELECT Distinct Isbn,PPMShorttitle,PPMCreationdate,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDuedate from PPM_Information WHERE UploadStatus='Pending' order by PPMCreationdate desc");

          

        }
        private void Filters()
        {
            try
            {
                string grpby = " group by  Isbn,PPMShorttitle,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn ,vol_no,jid ,parenttitle ";
                if (rdpSerAll.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDuedate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE UploadStatus='Pending' " + grpby + " order by max(PPMCreationdate) desc");
                }
                else if (rdpSerBookseries.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDuedate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE (Jid IS NOT NULL) and ( Jid !='') and UploadStatus='Pending' " + grpby + " order by max(PPMCreationdate) desc");
                }
                else if (rdbSerBook.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDuedate from vw_ppminfo WHERE ((Jid IS NULL) or ( Jid =''))  and UploadStatus='Pending' " + grpby + " order by max(PPMCreationdate) desc");
                }                
                
                grdvw.Refresh();

                for (int i = 0; i < grdvw.Rows.Count; i++)
                {
                    if (grdvw.Rows[i].Cells[8].Value.ToString().Contains("1900"))
                    {

                        grdvw.Rows[i].Cells[8].Value = DBNull.Value;
                    }
                }
            }
            catch(Exception erw)
            {}        
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Display_Grid();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Display_Grid();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Display_Grid();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Display_Grid();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Display_Grid();
        }
        private void load_ampping()
        {
            try 
            {
                String tpaths = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Map.bat";
                Process ps = new Process();
                ps.StartInfo.FileName = tpaths;
                ps.Start();
                System.Threading.Thread.Sleep(3000);

            }
            catch(Exception err)
            {}
        
        }
        public void Display_Grid()
        {
            string grpby = " group by  Isbn,PPMShorttitle,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn ,vol_no,jid ,parenttitle ";
            if (rdbSerBook.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate),PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Pending'  and ((Jid IS  NULL)  or Jid='' ) " + grpby + " order by max(PPMCreationdate) desc");
                }
                else if (radioButton2.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate),PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate  from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Uploaded' and ((Jid IS  NULL) or Jid='' ) " + grpby + " order by max(PPMCreationdate) desc");
                }
                else
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate),PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and ((Jid IS  NULL) or Jid='')  " + grpby + " order by max(PPMCreationdate) desc ");
                }           
            }
            else if (rdpSerBookseries.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Pending' and (Jid IS NOT NULL) and Jid !=''  " + grpby + " order by max(PPMCreationdate) desc");
                }
                else if (radioButton2.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Uploaded' and (Jid IS NOT NULL) and Jid !=''  " + grpby + " order by max(PPMCreationdate) desc");
                }
                else
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate)as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and (Jid IS NOT NULL) and Jid !=''  " + grpby + " order by max(PPMCreationdate) desc ");
                }
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Pending' " + grpby + " order by max(PPMCreationdate) desc ");
                }
                else if (radioButton2.Checked == true)
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' and UploadStatus='Uploaded' " + grpby + "  order by max(PPMCreationdate) desc ");
                }
                else
                {
                    this.GetData("SELECT Distinct Isbn,PPMShorttitle,max(PPMCreationdate) as PPMCreationdate ,PPMOrdertype,SignalCreation,SignalId,UploadStatus,ProdSite,PlanDueDate,Issn  as 'ISSN' ,vol_no as 'Volume No',jid as 'Jid',parenttitle as 'Series Title' from vw_ppminfo WHERE Isbn LIKE '" + textBox1.Text + "%' and PPMShorttitle LIKE '" + textBox2.Text + "%' " + grpby + " order by max(PPMCreationdate) desc ");
                }

            }
                                             
            grdvw.Refresh();
            for (int i = 0; i < grdvw.Rows.Count; i++)
            {
                if (grdvw.Rows[i].Cells[8].Value.ToString().Contains("1900"))
                {

                    grdvw.Rows[i].Cells[8].Value = DBNull.Value;
                }

            }
        }



        private void celldblclick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow dgvr = grdvw.CurrentRow;

            string strISBN = dgvr.Cells[0].Value.ToString();    
            string strStage = dgvr.Cells[3].Value.ToString();
            string strSC = dgvr.Cells[4].Value.ToString();

            if (strStage == "FILES-TO-ARCHIVE")
            {
                if (optViewPPM.Checked == true)
                {
                    MessageBox.Show("Please check, this signal is for Archive.\nWe don't receive PPM Order for Archive.");
                    return;
                }
            }

            if (strSC.Length == 0)
            {
                if (optViewSignal.Checked == true)
                {
                    MessageBox.Show("Signal is not created for this PPM Order, please check.");
                    return;
                }
            }           


            //string XMLFilePath = @"D:\ElsBook\Orders\PPM\" + strISBN.Replace("-","") + "\\" + strStage + "\\Current_Order";
            string OtempPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
            //string XMLFilePath = @"Z:\ElsBook\Orders\PPM\" + strISBN.Replace("-", "") + "\\" + strStage + "\\Current_Order";
            string XMLFilePath = OtempPath+ "\\" + strISBN.Replace("-", "") + "\\" + strStage + "\\Current_Order";
            
            if (!Directory.Exists(XMLFilePath))
            { 
                MessageBox.Show("PPM Order for \"" + strStage + "\" not found at \"" + XMLFilePath + "\\");
                return;
            }

            FileInfo[] FInfo = new DirectoryInfo(XMLFilePath).GetFiles();

            string strVFileName = "";
            for (int i = 0; i < FInfo.Length; i++)
            {                 
                if (optViewSignal.Checked == true)
                {
                    if (FInfo[i].Name.ToLower().StartsWith("thosg") && FInfo[i].Name.ToLower().EndsWith(".xml"))
                    {
                        
                        strVFileName = @"C:\PPMTS\ProcessPPM\" + FInfo[i].Name;
                        //File.Delete(strVFileName);
                        if (!File.Exists(strVFileName))
                        {
                            File.Copy(FInfo[i].FullName, strVFileName);
                        }
                    }
                }
                else
                {
                    if (FInfo[i].Name.ToLower().StartsWith("kup") && FInfo[i].Name.ToLower().EndsWith(".xml"))
                    {
                        strVFileName = @"C:\PPMTS\ProcessPPM\" + FInfo[i].Name;
                        //File.Delete(strVFileName);
                        if (!File.Exists(strVFileName))
                        {
                            File.Copy(FInfo[i].FullName, strVFileName);
                        }

                    }
                }
                

                if (strVFileName.Length>0)
                {
                    string temppath = "C:\\PPMTS\\BrowseXml";
                    File.WriteAllText("C:\\PPMTS\\BrowseXml\\dd.html", ApplyXSLTransformation(strVFileName),Encoding.Default);
                    ProcessStartInfo psi = new ProcessStartInfo(@"C:\Program Files\Internet Explorer\iexplore.exe", "file://C:\\PPMTS\\BrowseXml\\dd.html");
                    Process.Start(psi);              
                    break;             
                }
                

            }       
        }
        private void Overrite(string order_Path)
        {
            try
            {

                StreamReader sr = new StreamReader(order_Path,Encoding.Default);
                string order_text = sr.ReadToEnd();
                order_text = order_text.Replace("'UTF-8'?", "'windows-1252'?");
                sr.Close();
                StreamWriter sw = new StreamWriter(order_Path,false,Encoding.Default);
                sw.WriteLine(order_text);
                sw.Close();
               
          }
            catch
            { 
            
            
            }
        
        }
        private string ApplyXSLTransformation(string PPMFName)
        {
            string strHtml;

            string strXstFile = "";
            if (PPMFName.ToLower().IndexOf("kup") != -1)
            {
                strXstFile = @"C:\PPMTS\ProcessPPM\ppmorder.xsl";
            }
            else if (PPMFName.ToLower().IndexOf("thosg") != -1)
            {
                strXstFile = @"C:\PPMTS\ProcessPPM\ppmsignal.xsl";
            }
            else
            {
                return "";
            }

           // Overrite(PPMFName);
            
            //XslCompiledTransform xslt = new XslCompiledTransform();

            // Load the XML 
            //XPathDocument doc = new XPathDocument("D:\\PPMXML\\KUP1282289380549-20100820_082946.xml");
            XmlReaderSettings dd = new XmlReaderSettings();
            dd.ProhibitDtd = false;
                        

            XmlReader doc = XmlReader.Create(PPMFName, dd);
            // Load the style sheet.

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(strXstFile);
            
            MemoryStream ms = new MemoryStream();
             XmlTextWriter writer = new XmlTextWriter(ms, Encoding.Default);
            StreamReader rd = new StreamReader(ms,Encoding.Default);
            xslt.Transform(doc, writer);
            ms.Position = 0;
            strHtml = rd.ReadToEnd();
            rd.Close();
            ms.Close();
            return strHtml;
        }

        private void groupBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Test event");
        } 
        
        //private void grdvw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    string temppath = "C:\\BrowseXml";
        //    //string s = grdvw.Rows[e.RowIndex].Cells[0].Value.ToString();
        //    //File.WriteAllText("c:\\dd.html",ApplyXSLTransformation());
        //    File.WriteAllText("C:\\BrowseXml\\dd.html", ApplyXSLTransformation());
        //   //string ppmxml = "D:\\PPMXML\\KUP1282289380549-20100820_082946.xml";
        //   //ProcessStartInfo psi = new ProcessStartInfo(@"C:\Program Files\Internet Explorer\iexplore.exe","file://c:/dd.html");
        //    ProcessStartInfo psi = new ProcessStartInfo(@"C:\Program Files\Internet Explorer\iexplore.exe", "file://C:\\BrowseXml\\dd.html");
        //   Process.Start(psi);
            
        //  // XmlDocument xdoc = new XmlDocument();
        //  //xdoc.Load(ppmxml);
            
        //  //XslTransform xtrans = new XslTransform();
        //  // string ppmxsl = "D:\\PPMXML\\ppmorder.xsl";
        //  // xtrans.Load(ppmxsl);
        //  // xtrans.Transform(ppmxml, ppmxsl);
        //  // Xml1.Document = xdoc;
        //  // Xml1.Transform = xtrans;
        //}
        

        private void button3_Click(object sender, EventArgs e)
        {
            clsObjects obj = new clsObjects();
            //obj.objXMLOper.FetchData("D:\\PPMXML\\KUP1288347263446-20101029_111453.xml");
            obj.objXMLOper.FetchData(@"C:\Documents and Settings\58813\Desktop\PPM\KUP1282289380549-20100820_082946.xml");
        }

        private void FtpUpload()
        {

            try
            {
                string url = System.Configuration.ConfigurationSettings.AppSettings["uploadurl"];
                if (PingHost(url) == false)
                {
                    MessageBox.Show("Failed to connect to FTP Site \"" + url + "\"");
                    return;
                }

                string OPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];

                if (!Directory.Exists(OPath))
                {
                    MessageBox.Show("Connectivity with the td-nas is not mapped.");
                    return;
                }

                DataGridViewRow dgvr = grdvw.CurrentRow;
                string strISBN = dgvr.Cells[0].Value.ToString();
                string strStage = dgvr.Cells[3].Value.ToString();
                //string PPMOrderPath = @"D:\ElsBook\Orders\PPM\" + strISBN.Replace("-","") + "\\" + strStage + "\\Current_order\\";
                string OrderSPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
                string PPMOrderPath = OrderSPath + strISBN.Replace("-", "") + "\\" + strStage + "\\Current_order\\";
                if (!Directory.Exists(PPMOrderPath))
                {
                    MessageBox.Show("Invalid Path, ISBN Information not available on server");
                    return;
                }

                FileInfo[] FInfo = new DirectoryInfo(PPMOrderPath).GetFiles();
                if (FInfo.Length > 0)
                {
                    for (int i = 0; i < FInfo.Length; i++)
                    {
                        if (FInfo[i].Name.StartsWith("THOSG"))
                        {
                            //Upload(PPMOrderPath, FInfo[i].Name, "ftp.thomsondigital.com", "PPMSignal", "swtest", "td@swtest");
                            string strIP = System.Configuration.ConfigurationSettings.AppSettings["uploadhost"];
                            string strFolder = System.Configuration.ConfigurationSettings.AppSettings["UploadF"];
                            string strUID = System.Configuration.ConfigurationSettings.AppSettings["uploaduid"];
                            string strPWD = System.Configuration.ConfigurationSettings.AppSettings["uploadpwd"];

                            //if (Upload(PPMOrderPath, FInfo[i].Name, "202.54.251.84", "PPMSignal", "swtest", "td$swtest"))

                            if (Upload(PPMOrderPath, FInfo[i].Name, strIP, strFolder, strUID, strPWD))
                            {

                                objClsObj.objDataOper.UpdateStatus("Uploaded", strISBN, strStage);
                                MessageBox.Show("Successfully uploaded");

                            }
                            else
                            {
                                objClsObj.objDataOper.UpdateStatus("Pending", strISBN, strStage);

                            }
                            Display_Grid();
                        }
                    }
                }

            }
            catch (Exception eee)
            {
                                
            }
        
        }
        private void SFTPUpload()
        {

            try
            {
                string url = System.Configuration.ConfigurationSettings.AppSettings["uploadurl"]; 
                string OPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];

                if (!Directory.Exists(OPath))
                {
                    MessageBox.Show("Connectivity with the td-nas is not mapped.");
                    return;
                }

                DataGridViewRow dgvr = grdvw.CurrentRow;
                string strISBN = dgvr.Cells[0].Value.ToString();
                string strStage = dgvr.Cells[3].Value.ToString();
                
                string OrderSPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
                string PPMOrderPath = OrderSPath + strISBN.Replace("-", "") + "\\" + strStage + "\\Current_order\\";

                if (!Directory.Exists(PPMOrderPath))
                {
                    MessageBox.Show("Invalid Path, ISBN Information not available on server");
                    return;
                }

                FileInfo[] FInfo = new DirectoryInfo(PPMOrderPath).GetFiles();
                if (FInfo.Length > 0)
                {
                    for (int i = 0; i < FInfo.Length; i++)
                    {
                        if (FInfo[i].Name.StartsWith("THOSG"))
                        {
                            //Upload(PPMOrderPath, FInfo[i].Name, "ftp.thomsondigital.com", "PPMSignal", "swtest", "td@swtest");
                            string strIP = System.Configuration.ConfigurationSettings.AppSettings["uploadhost"];
                            string strFolder = System.Configuration.ConfigurationSettings.AppSettings["UploadF"];
                            string strUID = System.Configuration.ConfigurationSettings.AppSettings["uploaduid"];
                            string strPWD = System.Configuration.ConfigurationSettings.AppSettings["uploadpwd"];

                            

                            //if (UploadWINSCP(PPMOrderPath, FInfo[i].Name, strIP, strFolder, strUID, strPWD))
                            //{

                            //    objClsObj.objDataOper.UpdateStatus("Uploaded", strISBN, strStage);
                               
                            //    MessageBox.Show("Successfully uploaded");
                            //}
                            clsWinscp wsc = new  clsWinscp();
                            if (wsc.DoUpload(PPMOrderPath  +"\\"+ FInfo[i].Name) == 0)
                            {
                                objClsObj.objDataOper.UpdateStatus("Uploaded", strISBN, strStage);
                                MessageBox.Show("Successfully uploaded");
                            }
                            else
                            {
                                MessageBox.Show("Error while uploading");
                                objClsObj.objDataOper.UpdateStatus("Pending", strISBN, strStage);
                            }
                            Display_Grid();
                        }
                    }
                }

            }
            catch (Exception eee)
            {

            }

        }

       
        public static bool UploadWINSCP(string strSourcePath, string strFName, string FTPSite, string strFolder, string strUname, string strPwd)
        {
            try
            {
                FileInfo toUpload = new FileInfo(strSourcePath + "\\" + strFName);

                string programpath = System.Configuration.ConfigurationSettings.AppSettings["winscp"];
                string batpath = programpath + "\\winscp.com";

                if (!(File.Exists(batpath)))
                {
                    MessageBox.Show("WinScp.Com is not installed ...!!!!!");
                    return false;
                }
                

                string content = AppDomain.CurrentDomain.BaseDirectory + "example.txt";

                if (!(File.Exists(content)))
                {
                    MessageBox.Show("example.txt is not installed ...!!!!!");
                    return false;
                }

                string input =  AppDomain.CurrentDomain.BaseDirectory + "Source.txt";

                string filename= strSourcePath + "\\" + strFName;

                content = File.ReadAllText(content);
                content = content.Replace("FilePathSignal",filename.Trim());

                if (File.Exists(input))
                {
                    File.Delete(input);
                }
                File.WriteAllText(input, content);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                string batFile;
                if(File.Exists( AppDomain.CurrentDomain.BaseDirectory + "runscp.bat"))
                {
                 batFile = AppDomain.CurrentDomain.BaseDirectory + "runscp.bat";
                }
                else if(File.Exists( AppDomain.CurrentDomain.BaseDirectory + "runscp.cmd"))
                {
                 batFile = AppDomain.CurrentDomain.BaseDirectory + "runscp.cmd";
                }
                else
                {
                MessageBox.Show("File rumscp.cmd doesnot exist");
                return false;
                }


                startInfo.FileName = batFile; //batpath;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "winlog.log"))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "winlog.log");
                }
                startInfo.Arguments = "\"" +input +"\" \"" + AppDomain.CurrentDomain.BaseDirectory + "winlog.log" + "\"";//"/script=" + input + " /log=" + AppDomain.CurrentDomain.BaseDirectory + "winlog.log";
                //startInfo.Arguments = "/script=" + input + " /log=" + AppDomain.CurrentDomain.BaseDirectory + "winlog.log";
                try
                {
                    if (File.Exists(input))
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not Uploaded Error while creating the source.txt...!!!!!");
                        return false;
                    }
                    
                }
                catch (Exception exp)
                {
                    return false;
                }

                string ErrorChk = "";

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "winlog.log"))
                {
                    ErrorChk = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "winlog.log");
                    Process.Start("notepad.exe", AppDomain.CurrentDomain.BaseDirectory + "winlog.log");
                }
                else
                {
                    MessageBox.Show("WinLog not created please check winscp application.....!");
                    return false;
                }

               if (ErrorChk.Contains("Script: Failed"))
               {
                   MessageBox.Show("Script Failed please check log...!" );
                   return false;
               }
               
                return true;
            }
            catch (Exception ex)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "winlog.log"))
                {
                    Process.Start("notepad.exe", AppDomain.CurrentDomain.BaseDirectory + "winlog.log");
                }
                MessageBox.Show("Please check for FTP Connectivity, it is not able to connect to FTP site" + ex.ToString());
                Console.WriteLine("Upload: " + ex.Message);
                return false;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SFTPUpload();
            
            ///FtpUpload();
          // NewUpload();            
        }
        public static bool Upload(string strSourcePath, string strFName, string FTPSite, string strFolder, string strUname, string strPwd)
        {
            try
            {
                FileInfo toUpload = new FileInfo(strSourcePath + "\\" + strFName);

                FtpWebRequest request;
                if (strFolder.Length > 0)
                    request = (FtpWebRequest)WebRequest.Create("ftp://" + FTPSite + "/" + strFolder + "/" + strFName);
                else
                    request = (FtpWebRequest)WebRequest.Create("ftp://" + FTPSite + "/" + strFName);
                          

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(strUname, strPwd);
                
                request.Proxy = null;
                request.UsePassive = false;
              
                
                Stream ftpStream = request.GetRequestStream();
                FileStream file = File.OpenRead(strSourcePath + "\\" + strFName);

                int length = 1024;
                byte[] buffer = new byte[length];
                int bytesRead = 0;
                double d = file.Length;
                int x = 0;
                do
                {
                    x++;
                    bytesRead = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesRead);
                    Console.WriteLine(x + " -- " + (x * 1024) + "==" + d);
                }
                while (bytesRead != 0);

                file.Close();
                ftpStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check for FTP Connectivity, it is not able to connect to FTP site"+ex.ToString());
                Console.WriteLine("Upload: " + ex.Message);
                return false;
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {        
            DataGridViewRow dgvr = grdvw.CurrentRow;
            string strISBN = dgvr.Cells[0].Value.ToString(); 
            string strStage = dgvr.Cells[3].Value.ToString();
            string strSC = dgvr.Cells[4].Value.ToString();
            if (strSC.Length > 0)
            {
                MessageBox.Show("Signal exists, please check.");
                return;
            }
            SignalGeneration objSG = new SignalGeneration();
            string strMsgID = "";
            int intPS = objSG.Process_Signal(false, strISBN, strStage, out strMsgID);
            if (intPS == 0)
                {
                    string OrderSPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
                    File.Move(@"C:/PPMTS/ProcessPPM/" + strMsgID + ".xml", OrderSPath + strISBN.Replace("-", "") + "/" + strStage + "/Current_order/" + strMsgID + ".xml");
                    objClsObj.objDataOper.UpdateData(strISBN, strStage, strMsgID);//Update Data for Signal ID...
                    Display_Grid();
                }

            else if (intPS==1)
            {
                MessageBox.Show("Error 1: No PPM Orders received for this ISBN or does not exist on server");                
            }
            else if (intPS == 2)
            {
                MessageBox.Show("Error 2: PPM Order file not found of server, even though structure exists on server");
            }
            else if (intPS == 3)
            {
                MessageBox.Show("Error 3: Error rasied while generating the Message ID");
            }
            else if (intPS == 4)
            {
                MessageBox.Show("Error 4: Counter file for generating Message ID does not exist, or you don't have the permission to use it.");
            }
            else if(intPS == 5)
            {
                MessageBox.Show("Error 5: Exception raised while generating the Signal, please contact Technical Team");
            }            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = grdvw.CurrentRow;
            string strISBN = dgvr.Cells[0].Value.ToString();
            string strSTitle = dgvr.Cells[1].Value.ToString();
            string strCD = dgvr.Cells[2].Value.ToString();
            string strStage = dgvr.Cells[3].Value.ToString();
            string strSite = dgvr.Cells[7].Value.ToString();
            //if (strStage != "TYPESET-ORDER" && strStage != "FULL-SERVICE-ORDER")
            if (strStage.Trim().EndsWith("ORDER"))// != "TYPESET-ORDER" && strStage != "FULL-SERVICE-ORDER")
            {
                
            }
            else
            {
                MessageBox.Show("Please select the \"TYPESET-ORDER\" or  FULL-SERVICE-ORDER for this ISBN");
                return;
            }

            SignalGeneration objSG = new SignalGeneration();
            string strMsgID = "";
            //string strArchivePath = @"D:/ElsBook/Orders/PPM/" + strISBN.Replace("-", "") + "/FILES-TO-ARCHIVE/Current_order/";
            string OrderSPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
            string strArchivePath = OrderSPath + "/" +  strISBN.Replace("-", "") + "/FILES-TO-ARCHIVE/Current_order/";
            if (!Directory.Exists(strArchivePath))
            {
                Directory.CreateDirectory(strArchivePath);
            }
            else
            {
                MessageBox.Show("Archive for this PPM already exists, please check.");
                return;
            }
            int intPS = objSG.Process_Signal(true, strISBN, strStage, out strMsgID);
            if (intPS==0)
            {
                if (strMsgID.Length > 0)
                {
                    File.Move(@"C:/PPMTS/ProcessPPM/" + strMsgID + ".xml", strArchivePath + strMsgID + ".xml");
                    objClsObj.objDataOper.InsertData("1", " ", " ", strCD, "FILES-TO-ARCHIVE", DateTime.Now.ToString("yyyy-MM-dd"), strSTitle, strISBN, " ", "Pending", strSite, DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"));
                    objClsObj.objDataOper.UpdateData(strISBN, "FILES-TO-ARCHIVE", strMsgID);//Update Data for Signal ID...
                    Display_Grid();
                }
               
            }
            else if (intPS == 1)
            {
                MessageBox.Show("Error 1: No PPM Orders received for this ISBN or does not exist on server");
            }
            else if (intPS == 2)
            {
                MessageBox.Show("Error 2: PPM Order file not found of server, even though structure exists on server");
            }
            else if (intPS == 3)
            {
                MessageBox.Show("Error 3: Error rasied while generating the Message ID");
            }
            else if (intPS == 4)
            {
                MessageBox.Show("Error 4: Counter file for generating Message ID does not exist, or you don't have the permission to use it.");
            }
            else if (intPS == 5)
            {
                MessageBox.Show("Error 5: Exception raised while generating the Signal, please contact Technical Team");
            }
        }

        private void grdvw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dgvr = grdvw.CurrentRow;

                string strStage = dgvr.Cells[3].Value.ToString();
                string strSC = dgvr.Cells[4].Value.ToString();
                string strSID = dgvr.Cells[5].Value.ToString();
                string strUS = dgvr.Cells[6].Value.ToString();
                //  if (strStage == "TYPESET-ORDER" || strStage  == "FULL-SERVICE-ORDER")
                if (strStage.Trim().EndsWith("ORDER"))// || strStage  == "FULL-SERVICE-ORDER")
                {
                    button4.Enabled = true;
                }
                else
                {
                    button4.Enabled = false;
                }

                if (strSC == null || strSC == "")
                {
                    //button5.Enabled = false;
                    button5.Enabled = true;
                }
                else
                {
                    //button5.Enabled = true;
                    button5.Enabled = false;
                }

                /*if (strStage == "TYPE-FILES-TO-ARCHIVE")
                {
                    button4.Enabled = false;
                }
                else
                {
                    button4.Enabled = true;
                }*/

                if (strUS == "Uploaded")
                {
                    button2.Enabled = false;
                }
                else
                {
                    if (strSC == null || strSC == "")
                    {
                        button2.Enabled = false;
                    }
                    else
                    {
                        button2.Enabled = true;
                    }
                }
            }
            catch(Exception ex)
            {}
        }

        private void grpView_Enter(object sender, EventArgs e)
        {

        }

        private void grdvw_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grdvw_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Rows : " + grdvw.RowCount ;
        }
        
        private void btplan_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = grdvw.CurrentRow;
            string strISBN = dgvr.Cells[0].Value.ToString();
            string strPlanDueDate = dgvr.Cells[8].Value.ToString();
            string strPPMOrdertype = dgvr.Cells[3].Value.ToString();
            objClsObj.objDataOper.UpdatePlanDate(strISBN, strPlanDueDate, strPPMOrdertype);
            Display_Grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = grdvw.CurrentRow;
            string strISBN = dgvr.Cells[0].Value.ToString();
            string strPlanDueDate = dgvr.Cells[8].Value.ToString();
            string strPPMOrdertype = dgvr.Cells[3].Value.ToString();
            objClsObj.objDataOper.UpdatePlanDate(strISBN, strPlanDueDate,strPPMOrdertype);
            Display_Grid();            

        }
        public bool PingHost(string ip)
        {
            return true;
            try
            {
                IPAddress ipaddr=Dns.GetHostEntry(ip).AddressList[0];
                for (int i = 0; i < 3; i++)
                {
                    System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                    System.Net.NetworkInformation.PingReply pingReply = ping.Send(ipaddr);
                    Console.WriteLine(pingReply.Buffer.Count().ToString());
                    Console.WriteLine(pingReply.RoundtripTime.ToString());
                    Console.WriteLine(pingReply.Options.Ttl.ToString());
                    Console.WriteLine(pingReply.Status.ToString());
                    System.Threading.Thread.Sleep(100);
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private void rdpSerBookseries_CheckedChanged(object sender, EventArgs e)
        {
            Filters();
        }

        private void rdbSerBook_CheckedChanged(object sender, EventArgs e)
        {
            Filters();
        }

        private void rdpSerAll_CheckedChanged(object sender, EventArgs e)
        {
            Filters();
        }

        private void grdvw_RowsAdded(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Rows : " + grdvw.RowCount; 
        }



        
    }
}
