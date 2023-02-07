using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TD.Data;

public partial class DossierEntry1 : System.Web.UI.Page
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
            binddec();
            bindcategory();
        }
    }
    public void LoadData(CultureInfo ci)
    {
        lblCategory.Text = rm.GetString("Category", ci);
        lblHeading.Text = rm.GetString("Log_folder", ci);
        lblDeclination.Text = rm.GetString("Declination", ci);
        lblFolderTitle.Text = rm.GetString("Folder_Title", ci);
        lblAuthor.Text = rm.GetString("Author", ci);
        lblmailnotification.Text = rm.GetString("Additional_mail_notification", ci);
        lblComment.Text = rm.GetString("Comment", ci);
        lblLoadafile.Text = rm.GetString("Load_a_fileDosier", ci);

        btnSend.Text = rm.GetString("To_send", ci);
        btnCancel.Text = rm.GetString("Cancel", ci);
    }
    private void bindcategory()
    {
        ddlcategory.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getinputcategory, paramlist);
        ddlcategory.DataSource = ds;
        ddlcategory.DataTextField = "catdesc";
        ddlcategory.DataValueField = "catid";
        ddlcategory.DataBind();
        ddlcategory.Items.Insert(0, new ListItem("----------", "-1"));
    }
    private void binddec()
    {
        ddlDeclination.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.Declination, paramlist);
        ddlDeclination.DataSource = ds;
        ddlDeclination.DataTextField = "decdesc";
        ddlDeclination.DataValueField = "decid";
        ddlDeclination.DataBind();
        ddlDeclination.Items.Insert(0, new ListItem("----------", "-1"));
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
                    string DID = "";
                   // DateTime dt = DateTime.Now;
                    DataSet idtable = new DataSet();
                    idtable = DataAccess.ExecuteDataSetSP(SPNames.getdossierid);
                    if (idtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string tempeid = idtable.Tables[0].Rows[0]["DID"].ToString();
                        if (tempeid != "")
                        {
                            string tt = tempeid.Substring(tempeid.Length - 5, 5);
                            int val = Convert.ToInt32(tt);
                            val = val + 1;
                            if (val.ToString().Length == 1)
                            {
                                tidval = "0000" + val.ToString();
                            }
                            else if (val.ToString().Length == 2)
                            {
                                tidval = "000" + val.ToString();
                            }
                            else if (val.ToString().Length == 3)
                            {
                                tidval = "00" + val.ToString();
                            }
                            else if (val.ToString().Length == 4)
                            {
                                tidval = "0" + val.ToString();
                            }
                            else if (val.ToString().Length == 5)
                            {
                                tidval = val.ToString();
                            }
                            DID = "DOS" + tidval;//+ dt.Year + dt.Month
                        }
                        else
                        {
                            DID = "DOS" + "00001";
                        }
                    }
                    else
                    {
                        DID = "DOS" + "00001";//+ dt.Year + dt.Month
                    }

                    // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString());
                    }
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()+"\\"+DID))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()+"\\"+DID);
                    }

                    if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()+"\\"+DID + "\\" + filename))
                    {
                        File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()+"\\"+DID + "\\" + filename);
                    }

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename);

                    // for calculate TAT
                  ////////  DateTime dt = System.DateTime.Now;
                    System.DateTime dt = System.DateTime.Now;

                    System.DateTime dt1;
                    //dt = dt.AddDays(1); 

                    if (dt.DayOfWeek == DayOfWeek.Friday)
                    {
                        dt1 = dt.AddDays(3);
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        dt1 = dt.AddDays(3);
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dt1 = dt.AddDays(2);
                    }
                    else
                        dt1 = dt.AddDays(1); 
                    /////////////////////////////////



                    SqlParameter[] paramList = new SqlParameter[14];
                    paramList[0] = new SqlParameter("@DID", DID);
                    paramList[1] = new SqlParameter("DECLINATION", ddlDeclination.SelectedItem.Text);
                    paramList[2] = new SqlParameter("@CTITLE", txtfoldertitle.Text.Trim());
                    paramList[3] = new SqlParameter("@DEMANDTYPE","");
                    paramList[4] = new SqlParameter("@DURATION", "Courant");
                    paramList[5] = new SqlParameter("@ITERATION", "");
                    paramList[6] = new SqlParameter("@STAGE", "En attente");
                    paramList[7] = new SqlParameter("@REMARKS", txtcomment.Text.Trim());
                    paramList[8] = new SqlParameter("@Author", txtauthor.Text.Trim());
                    paramList[9] = new SqlParameter("@authormail", txtmailnotification.Text);
                    paramList[10] = new SqlParameter("@filename", filename);
                    paramList[11] = new SqlParameter("@category", ddlcategory.SelectedItem.Text);
                    paramList[12] = new SqlParameter("@duedate", Convert.ToDateTime(dt1));
                    paramList[13] = new SqlParameter("InDate", Common.GetDayLightTime());
                    // txtreference ddldelai, due date

                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertdossier_withduedate, paramList);
                    if (rowAffected > 0)
                    {
                        lblmessage.Text = "Item ajouté";
                        //  btnFinish.Visible = true;
                        //Jitender Shootmail

                    }
                    else
                    {
                        lblmessage.Text = "error";
                        return;
                    }
                    ddlcategory.SelectedIndex = -1;
                    ddlDeclination.SelectedIndex = -1;
                    txtfoldertitle.Text = "";
                    txtcomment.Text = "";
                    txtauthor.Text = "";
                    txtmailnotification.Text = "";
                   // Response.Redirect("DossierLanding1.aspx");
                   // lblmessage.Text = "";
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlcategory.SelectedIndex = -1;
        ddlDeclination.SelectedIndex = -1;
                    txtfoldertitle.Text ="";
                    txtcomment.Text = "";
                    txtauthor.Text = "";
                    txtmailnotification.Text = "";
        lblmessage.Text ="";
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
        "window.close();" + System.Environment.NewLine +
        "</script>");
    }
}