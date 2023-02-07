using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TD.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class TDUploadFile : System.Web.UI.Page
{
    private ResourceManager rm;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            CultureInfo ci;
            string lang = "";
            if (Session["lang"] != null)
            {
                lang = Session["lang"].ToString();
            }
            else
            {
                //lang = "en-US";
                Session["lang"] = "fr-FR";
                lang = "fr-FR";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
            ci = Thread.CurrentThread.CurrentCulture;
            LoadData(ci);

            // get and loda data 
            string id = Request.QueryString["ERID"].ToString();
            hiddenval.Text = id;
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("erid", id);
            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.gettduploadEncyclo, paramlist);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtleonardid.Text = ds.Tables[0].Rows[0]["EID"].ToString();
                txtleonardid.Enabled = false;
                txttitlefesc.Text = ds.Tables[0].Rows[0]["DTITLE"].ToString();
            }
           
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    txttitlefesc.Text = ds.Tables[0].Rows[0]["DTITLE"].ToString();
            //}

        }
    }
    public void LoadData(CultureInfo ci)
    {
        lblHeading.Text = rm.GetString("Upload_Article", ci);
        lblheading2.Text = rm.GetString("Encyclopedia", ci);
        lblId.Text = rm.GetString("Leonardo_id", ci);
        lblTitlefesc.Text = rm.GetString("Title_Fesc", ci);
        lblComment.Text = rm.GetString("Comment", ci);
        lblLoadafile.Text = rm.GetString("Load_a_file", ci);

        btnSend.Text = rm.GetString("To_send", ci);
        btnCancel.Text = rm.GetString("Cancel", ci);


        //if (ddlCollection != null)
        //{
        //    ddlCollection.Items[0].Text = rm.GetString("Select", ci);
        //}
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);

            string ext = Path.GetExtension(FileUpload1.FileName);
            if (ext == ".zip" || ext == ".rar" || ext == ".7zip")
            {
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];



                try
                {
                    if (!Directory.Exists(Expath))
                    {
                        Directory.CreateDirectory(Expath);
                    }

                    // get max value
                    string EID = txtleonardid.Text.Trim();


                    // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString());
                    }
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD"))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD");
                    }

                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID);
                    }

                    if (Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID))
                    {
                        if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\Backup"))
                        {
                            Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\Backup");
                        }
                        string[] oldfile1 = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\Backup");

                        string[] oldfile = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID);
                        int versionnum = oldfile1.Length;
                        foreach (string file in oldfile)
                        {
                            versionnum++;
                            string tempfilename = Path.GetFileName(file);
                            string newbkpfilename = "Version_" + versionnum + "_" + tempfilename;
                            File.Copy(file, Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\Backup\\" + newbkpfilename,true);
                            File.Delete(file);
                        }
                    }

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\" + filename);

                    SqlParameter[] paramList = new SqlParameter[6];
                    paramList[0] = new SqlParameter("erid", hiddenval.Text.Trim());
                    paramList[1] = new SqlParameter("title", txttitlefesc.Text.Trim());
                    paramList[2] = new SqlParameter("tdcomment", txtcomment.Text.Trim());
                    paramList[3] = new SqlParameter("tdfile", filename);
                    paramList[4] = new SqlParameter("stage", "Contenu préparé");
                    paramList[5] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.tduploadInsertencyclo, paramList);
                    if (rowAffected > 0)
                    {
                        lblmessage.Text = "Item ajouté";

                        //  btnFinish.Visible = true;
                    }
                    else
                    {
                        lblmessage.Text = "error";
                        return;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                string message = "sélectionner un fichier valide";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                   "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                   "</script>");
            }
        }
        else
        {
            string message = "choisir le dossier";
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
               "</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txttitlefesc.Text = "";
        txtcomment.Text = "";
        lblmessage.Text = "";


    }
}