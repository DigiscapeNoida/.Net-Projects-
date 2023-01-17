using System;
using System.Drawing;
using System.Web.Configuration;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Default2 : System.Web.UI.Page
{
    string SaveZipFilePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Server.Transfer("~/Default.aspx");
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(SaveZipFilePath))
            {
                File.Delete(SaveZipFilePath);
            }
        }
        catch(Exception ex  )
        {
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string DesPath = "c:\\temp\\";

        string TargetFolderPath  = WebConfigurationManager.AppSettings["TargetFolderPath"];
        string OldFolderPath     = WebConfigurationManager.AppSettings["OldFolderPath"];
        SaveZipFilePath = "c:\\temp\\" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond + ".zip"; ;

        if (!FileUpload1.FileName.Equals(""))
        {
            if (!Directory.Exists(DesPath))
            {
                Directory.CreateDirectory(DesPath);
            }
            //SaveZipFilePath = DesPath + "\\" + FileUpload1.FileName;
            FileUpload1.SaveAs(SaveZipFilePath);
            FileInfo finfo = new FileInfo(SaveZipFilePath);
            if (FileUpload1.PostedFile.ContentLength == finfo.Length)
            {
                LogDetail.SeqCount = 0;
                XmlFileInfo obj = new XmlFileInfo(SaveZipFilePath, TargetFolderPath, OldFolderPath);

                try
                {
                    if (obj.SaveFileInZip()) 
                    {
                        //Label1.Text = "Uploaded file status";
                        //Label2.Text = "File Name:";
                        //Label3.Text = "Total files in zip file:";
                        //Label4.Text = "No. of xml files:";
                        //Label5.Text = "No. of text files:";

                        //Label7.Text = FileUpload1.PostedFile.FileName;
                        //Label8.Text = obj.TotalFilesCount.ToString();
                        //Label9.Text = obj.XmlFilesCount.ToString();

                        LblUploadedFileStatus.Text = "Uploaded file status";
                        LblFileName.Text = "File Name:";
                        LblTotalFiles.Text = "Total files in zip file:";
                        LblXMLFILES.Text = "No. of XML files:";
                        LblTXTFILES.Text = "No. of HTML files:";

                        LblFileNameValue.Text = FileUpload1.PostedFile.FileName;
                        LblTotalFilesValue.Text = obj.TotalFilesCount.ToString();
                        LblXMLFILESValue.Text = obj.XmlFilesCount.ToString();

                        if (obj.TotalFilesCount != obj.XmlFilesCount)
                        {
                            LblXMLFILESValue.ForeColor = Color.Red;
                            LblXMLFILESValue.Font.Bold = true;
                        }
                        LblTXTFILESValue.Text = obj.TextFilesCount.ToString();

                        Repeater1.Visible = true;
                        Repeater1.DataSource = obj.Log;
                        Repeater1.DataBind();

                    }
                    else
                    {
                        Response.Write("Failed :: " );
                    }
                    FileUpload1.Dispose();
                    File.Delete(SaveZipFilePath);
                }
                catch(Exception ex)
                {
                   // Response.Write(ex.Message);
                }
                
            }
        }
    }

    protected void LogStatusRepeater_ItemCreated(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem!= null)
        {
            LogDetail LogDetailObj=   (LogDetail)e.Item.DataItem;
            if (LogDetailObj.Result.Equals("Identical"))
            {
                Label lbl = (Label)e.Item.FindControl("statusLabel");
                lbl.ForeColor = Color.Red;
                lbl.Font.Bold = true;
            }
        }
    }

    protected void HomeLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JSSView.aspx");

    }
    protected void LogoutLinkButton_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.Redirect("~/Default.aspx",true);
    }
}
