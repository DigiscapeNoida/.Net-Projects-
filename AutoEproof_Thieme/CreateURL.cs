using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ProcessNotification;
namespace AutoEproof
{
    class CreateURL : MessageEventArgs
    {
        public  CreateURL()
        { 

        }
        public void ToCreateURL(MsgRcvr.MNTInfo MNT, string PDFPath)
        {

            URLService.CreateEproofURL EproofURLOBJ = new URLService.CreateEproofURL();
            string pdf = UploadFile(EproofURLOBJ, PDFPath);
            EproofURLOBJ.CreateURL(MNT.Client, MNT.JID, MNT.AID, pdf);
        }
        private string UploadFile(URLService.CreateEproofURL EproofURL, string filename)
        {
            try
            {
                // get the exact file name from the path
                String strFile = System.IO.Path.GetFileName(filename);

                // create an instance fo the web service


                // get the file information form the selected file
                FileInfo fInfo = new FileInfo(filename);

                // get the length of the file to see if it is possible
                // to upload it (with the standard 4 MB limit)
                long numBytes = fInfo.Length;
                double dLen = Convert.ToDouble(fInfo.Length / 1000000);

                // Default limit of 4 MB on web server
                // you want to allow larger uploads
                if (dLen < 4)
                {
                    // set up a file stream and binary reader for the 
                    // selected file
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);

                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();

                    // pass the byte array (file) and file name to the web service
                    string sTmp = EproofURL.UploadFile(data, strFile);
                    fStream.Close();
                    fStream.Dispose();

                    return sTmp;
                    // this will always say OK unless an error occurs,
                    // if an error occurs, the service returns the error message
                    //MessageBox.Show("File Upload Status: " + sTmp, "File Upload");
                }
                else
                {
                    // Display message if the file was too large to upload
                    //MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
                }
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                // display an error message to the user
                //MessageBox.Show(ex.Message.ToString(), "Upload Error");
            }
            return string.Empty;
        }
    }
}
