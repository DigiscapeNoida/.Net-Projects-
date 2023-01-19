using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = @"D:\Rohit\Workingcopy\AutoEproof_Live\bin\Debug\Process\ACI200172R\ACI-200172R.pdf";
                FileInfo fInfo = new FileInfo(fileName);
                long numBytes = fInfo.Length;
        FileStream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fStream);

        // convert the file to a byte array
        byte[] data = br.ReadBytes((int)numBytes);
        br.Close();
        string TempDir = "C:\\Temp\\";
        if (!Directory.Exists(TempDir))
        {
            Directory.CreateDirectory(TempDir);
        }
        string ProcessFile = TempDir + DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(fileName);

        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {
            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(data);
            // instance a filestream pointing to the 
            // storage folder, use the original file name
            // to name the resulting file
            FileStream fs = new FileStream(ProcessFile, FileMode.Create);
            // write the memory stream containing the original
            // file as a byte array to the filestream
            ms.WriteTo(fs);
            // clean up
            ms.Close();
            fs.Close();
            fs.Dispose();
            // return OK if we made it this far
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
        }

    }
}