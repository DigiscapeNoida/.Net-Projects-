using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace PPM_TRACKING_SYSTEM
{
    public partial class UploadForm : Form
    {
        //ListView listview = new ListView();
        public UploadForm()
        {
         
        //Uploadlist.Dock = DockStyle.Fill;
        //PopulateListView();
        //this.Controls.Add(Uploadlist);
        //this.ClientSize = new Size(400, 200);
        }

        private void PopulateListView()
        {

            //Uploadlist.= Visible.

            //Uploadlist.Columns.Add("FilName", -2, HorizontalAlignment.Center);
            //Uploadlist.Columns.Add("Date", -2, HorizontalAlignment.Left);
            //Uploadlist.Columns.Add("Price", -2, HorizontalAlignment.Left);

            
            
        }
        private void Uploadlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UploadForm_Load(object sender, EventArgs e)
        {

        }

        public static void Upload(string strSourcePath, string strFName, string FTPSite, string strFolder, string strUname, string strPwd)
        {
/*            try
            {
              FileInfo toUpload = new FileInfo(strSourcePath + "\\" + strFName);

                FtpWebRequest request;
                if (strFolder.Length > 0)
                    request = (FtpWebRequest)WebRequest.Create("ftp://" + FTPSite + "/" + strFolder + "/" + strFName);
                else
                    request = (FtpWebRequest)WebRequest.Create("ftp://" + FTPSite + "/" + strFName);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(strUname, strPwd);
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
                Console.WriteLine("Upload: " + ex.Message);
                return false;
            }
            */
        }

   }
}
