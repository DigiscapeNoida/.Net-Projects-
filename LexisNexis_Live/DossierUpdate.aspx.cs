using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TD.Data;

public partial class DossierEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
            string id = Request.QueryString["DID"].ToString();
            hiddenfeild.Text = id;
            binddec();
            bindcategory();

            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("DID", id);

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getdossierforedit, paramlist);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByText(ds.Tables[0].Rows[0]["category"].ToString()));
                ddlDeclination.SelectedIndex = ddlDeclination.Items.IndexOf(ddlDeclination.Items.FindByText(ds.Tables[0].Rows[0]["DECLINATION"].ToString()));
                txtfoldertitle.Text = ds.Tables[0].Rows[0]["CTITLE"].ToString();
                txtauthor.Text = ds.Tables[0].Rows[0]["Author"].ToString();
                txtmailnotification.Text = ds.Tables[0].Rows[0]["authormail"].ToString();
                    txtcomment.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
            }
            
        }
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
        //if (FileUpload1.HasFile)
        //{
        //    string filename = Path.GetFileName(FileUpload1.FileName);

        //    string ext = Path.GetExtension(FileUpload1.FileName);
        //    if (ext == ".zip" || ext == ".rar" || ext == ".7zip" || ext == ".doc" || ext == ".docx")
        //    {
        //        string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];



                try
                {
                    //if (!Directory.Exists(Expath))
                    //{
                    //    Directory.CreateDirectory(Expath);
                    //}

                    // get max value
                    /*
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
                   if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID))
                   {
                       Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID);
                   }

                   if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename))
                   {
                       File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename);
                   }

                   FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename);
                   */
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



                    SqlParameter[] paramList = new SqlParameter[12];
                    paramList[0] = new SqlParameter("@DID", hiddenfeild.Text);
                    paramList[1] = new SqlParameter("DECLINATION", ddlDeclination.SelectedItem.Text.Replace("----------", ""));
                    paramList[2] = new SqlParameter("@CTITLE", txtfoldertitle.Text.Trim());
                    paramList[3] = new SqlParameter("@DEMANDTYPE", "");
                    paramList[4] = new SqlParameter("@DURATION", "Courant");
                    paramList[5] = new SqlParameter("@ITERATION", "");
                    paramList[6] = new SqlParameter("@STAGE", "En attente prod");
                    paramList[7] = new SqlParameter("@REMARKS", txtcomment.Text.Trim());
                    paramList[8] = new SqlParameter("@Author", txtauthor.Text.Trim());
                    paramList[9] = new SqlParameter("@authormail", txtmailnotification.Text);
                  //  paramList[10] = new SqlParameter("@filename", filename);
                    paramList[10] = new SqlParameter("@category", ddlcategory.SelectedItem.Text.Replace("----------", ""));
                    paramList[11] = new SqlParameter("@duedate", Convert.ToDateTime(dt1));
                    // txtreference ddldelai, due date

                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.updatedossier, paramList);
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
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
         "window.opener.location.href='DossierLanding1.aspx';window.close();" + System.Environment.NewLine +
         "</script>");

                }
                catch (Exception ex)
                {
                }
        //    }
        //    else
        //    {
        //        string message = "Merci de charger une pièce jointe valide (zip/docx/doc)";
        //        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
        //           "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
        //           "</script>");
        //    }
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlcategory.SelectedIndex = -1;
        ddlDeclination.SelectedIndex = -1;
        txtfoldertitle.Text = "";
        txtcomment.Text = "";
        txtauthor.Text = "";
        txtmailnotification.Text = "";
        lblmessage.Text = "";
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "window.opener.location.href='DossierLanding1.aspx';window.close();" + System.Environment.NewLine +
           "</script>");
        //Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
        //"window.close();" + System.Environment.NewLine +
        //"</script>");
    }
}