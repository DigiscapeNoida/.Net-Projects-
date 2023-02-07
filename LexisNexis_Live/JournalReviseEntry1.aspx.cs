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

public partial class EncyclopediaReviseEntry1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindtat();
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
            // get and loda data 
            string id = Request.QueryString["ARTICLEID"].ToString();
            hiddenval.Text = id;
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("articleeid", id);
            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJOURNALReviseentry, paramlist);
            txtleonardid.Text = id;
            txtleonardid.Enabled = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
               // txttitlefesc.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ARTICLETITLE"].ToString();
                txtreview.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["JID"].ToString();
               // txtreview.ReadOnly = true;
                txtreview.Enabled = false;
                txtarticletitle.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ARTICLETITLE"].ToString();
               // txtarticletitle.ReadOnly = true;
                txtarticletitle.Enabled = false;
                txtjournalauthor.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["AUTHORNAME"].ToString();
               // txtjournalauthor.ReadOnly = true;
                txtjournalauthor.Enabled = false;
                txtarticletype.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ARTICLETYPE"].ToString();
               // txtarticletype.ReadOnly = true;
                txtarticletype.Enabled = false;
                txtpubnum.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Publishing_Number"].ToString();
               // txtpubnum.ReadOnly = true;
                txtpubnum.Enabled = false;
                txtsupnotification.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["email"].ToString();
               // txtsupnotification.ReadOnly = true;
                txtsupnotification.Enabled = false;
            }

        }
    }
    private void bindtat()
    {
        ddldelai.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.Tat, paramlist);
        ddldelai.DataSource = ds;
        ddldelai.DataTextField = "tattype";
        ddldelai.DataValueField = "tat";
        ddldelai.DataBind();
        ddldelai.Items.Insert(0, new ListItem("----------", "-1"));
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);

            string ext = Path.GetExtension(FileUpload1.FileName);
            if (ext == ".doc" || ext == ".docx")//ext == ".zip" || ext == ".rar" || ext == ".7zip"
            {
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];



                try
                {
                    if (!Directory.Exists(Expath))
                    {
                        Directory.CreateDirectory(Expath);
                    }

                    // get max value
                    string articleEID = txtleonardid.Text.Trim();
                    // DateTime dt=DateTime.Now;

                    string articlERID = "";
                    DataSet eridtable = new DataSet();
                    eridtable = DataAccess.ExecuteDataSetSP(SPNames.getJournalArticleRID);
                    if (eridtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string temperid = eridtable.Tables[0].Rows[0]["articleRID"].ToString();
                        if (temperid != "")
                        {
                            string tt = temperid.Substring(temperid.Length - 5, 5);
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
                            articlERID = "RV" + tidval;//dt.Year + dt.Month +
                        }
                        else
                        {
                            articlERID = "RV" + "00001";//+ dt.Year + dt.Month
                        }
                    }
                    else
                    {
                        articlERID = "RV" + "00001";// + dt.Year + dt.Month
                    }



                    // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString());
                    }
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN"))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN");
                    }

                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID);
                    }

                    if (Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID))
                    {
                        if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID + "\\Backup"))
                        {
                            Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID + "\\Backup");
                        }
                        string[] oldfile1 = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID + "\\Backup");

                        string[] oldfile = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID);
                        int versionnum = oldfile1.Length;
                        foreach (string file in oldfile)
                        {
                            versionnum++;
                            string tempfilename = Path.GetFileName(file);
                            string newbkpfilename = "Version_" + versionnum + "_" + tempfilename;
                            File.Copy(file, Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID + "\\Backup\\" + newbkpfilename, true);
                            File.Delete(file);
                        }
                    }

                   

                    int iteration = 0;
                    string jid = "";
                    string aid = "";
                    string articletitle = "";
                    string articletype = "";
                    string pubnum = "";
                    string tat = "";
                    string email = "";
                    string duedate = "";
                    string worktobedone = "";

                    SqlParameter[] paramlist1 = new SqlParameter[1];
                    paramlist1[0] = new SqlParameter("articleeid", hiddenval.Text.Trim());
                    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJOURNALReviseentry, paramlist1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        iteration = ds.Tables[0].Rows.Count;
                        jid = ds.Tables[0].Rows[iteration - 1]["jid"].ToString();
                        aid = ds.Tables[0].Rows[iteration - 1]["aid"].ToString();
                        articletitle = ds.Tables[0].Rows[iteration - 1]["articletitle"].ToString();
                        articletype = ds.Tables[0].Rows[iteration - 1]["articletype"].ToString();
                        pubnum = ds.Tables[0].Rows[iteration - 1]["Publishing_Number"].ToString();
                        tat = ds.Tables[0].Rows[iteration - 1]["tat"].ToString();
                        email = ds.Tables[0].Rows[iteration - 1]["email"].ToString();
                        tat = ds.Tables[0].Rows[iteration - 1]["tat"].ToString();
                        duedate = ds.Tables[0].Rows[iteration - 1]["duedate"].ToString();
                        //worktobedone = ds.Tables[0].Rows[iteration - 1]["WorkTobeDone"].ToString();
                    }

                    // commented on 14 dec 16
                    //filename = articleEID + "_" + iteration.ToString()  + ext;

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleEID + "\\" + filename);
                    // for calculate TAT
                    ////////  DateTime dt = System.DateTime.Now;
                    int numofdays = 0;
                    double numofhour = 0;
                    if (ddldelai.SelectedItem.Text == "Rapide")
                    {
                        //numofhour = 5;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
                        numofdays = 1;
                    }
                    else if (ddldelai.SelectedItem.Text == "Courant")
                    {
                        numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else if (ddldelai.SelectedItem.Text == "Express")
                    {
                        numofhour = 4;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else
                    {
                        numofdays = 1;
                    }

                    System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));
                    // System.DateTime dt = System.DateTime.Now;
                    System.DateTime dt1;

                    int nod = numofdays;
                    int counter = 0;
                    for (int i = 0; i <= nod; i++)
                    {
                        System.DateTime dttemp = dt.AddDays(i);
                        if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dttemp.Date.ToString("dd/MM/")) != -1)
                        {
                            counter++;
                        }
                    }



                    if (numofhour > 0)
                    {
                        dt1 = dt.AddHours(numofhour);
                    }
                    else if (numofdays > 0)
                    {
                        int TotalDay = nod + counter;

                        dt1 = dt.AddDays(TotalDay);
                    }
                    else
                    {
                        dt1 = dt.AddDays(0);
                    }

                    while (true)
                    {
                        if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dt1.Date.ToString("dd/MM/")) != -1)
                            dt1 = dt1.AddDays(1);
                        else
                        {
                            break;
                        }
                    }





                    SqlParameter[] paramList = new SqlParameter[18];
                    paramList[0] = new SqlParameter("articleID", txtleonardid.Text);
                    paramList[1] = new SqlParameter("jid",jid);
                    paramList[2] = new SqlParameter("aid", aid);
                    paramList[3] = new SqlParameter("articletitle", txtarticletitle.Text.Trim());
                    paramList[4] = new SqlParameter("articletype", txtarticletype.Text.Trim());
                    paramList[5] = new SqlParameter("pubnumber", txtpubnum.Text.Trim());// ddlitemtype.SelectedItem.Text.Replace("----------","")
                    paramList[6] = new SqlParameter("tat", ddldelai.SelectedItem.Text.Replace("----------", ""));
                    paramList[7] = new SqlParameter("email", txtsupnotification.Text.Trim());// txtapplicationname.Text.Trim()
                    paramList[8] = new SqlParameter("comments", txtcomment.Text.Trim());
                    paramList[9] = new SqlParameter("filesname", filename);
                    paramList[10] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));//txtduedate.Text
                    paramList[11] = new SqlParameter("ITERATION", iteration);
                    paramList[12] = new SqlParameter("STAGE", "En préparation");//En attente prod
                    paramList[13] = new SqlParameter("articlerid", articlERID);
                    paramList[14] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                    paramList[15] = new SqlParameter("authorname", txtjournalauthor.Text.Trim());
                    paramList[16] = new SqlParameter("worktobedone", ddlworktobedone.SelectedValue.ToString());
                    paramList[17] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertJournal, paramList);
                    if (rowAffected > 0)
                    {
                        string strID = txtleonardid.Text;
                        string strTitle = txtleonardid.Text + "- " + txttitlefesc.Text.Trim();// txttitle.Text.Trim();
                        string strDuedate = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
                        string strComments = txtcomment.Text.Trim();

                        string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"]; // System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                        string strLT = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];

                        //    string strStage = "Article in process";

                        string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                        string strCC ="";// txtsupnotification.Text.Trim();
                        // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                        // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                        string strFile = Server.MapPath("App_Data\\MAILS\\THOMSON_RECEIVE_CORRECTION\\") + "J.html";

                        if (File.Exists(strFile))
                        {
                            StreamReader sr = new StreamReader(strFile);
                            string FileC = sr.ReadToEnd();
                            sr.Close();
                            string strBody = FileC;
                            strBody = strBody.Replace("[ILT]", strTitle);
                            strBody = strBody.Replace("[DAT]", strDuedate);
                            if (strComments.Trim() == "")
                            {
                                strBody = strBody.Replace("[IACE]", "aucun commentaire");
                            }
                            else
                            {
                                strBody = strBody.Replace("[IACE]", strComments);
                            }
                            strBody = strBody.Replace("[IHT]", strLink);

                            string strSubject = "Léonard – Demande d’intervention sur le document : « " + strID + " »";
                            Common cmn = new Common();
                            Common.SendEmail(strTo, strCC, strSubject, strBody);
                            // Utility.NumberToEnglish.email();
                        }



                        lblmessage.Text = "Item ajouté";

                        //  btnFinish.Visible = true;
                    }
                    else
                    {
                        lblmessage.Text = "error";
                        return;
                    }
                  
                    txtcomment.Text = "";
                    lblmessage.Text = "";

                    /*
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
       "window.opener.location.href='JournalLanding.aspx';window.close();" + System.Environment.NewLine +
       "</script>");*/

                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
     "window.close();" + System.Environment.NewLine +
     "</script>");

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                string message = "Merci de charger une pièce jointe valide (format doc/docx)";
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
        txtcomment.Text = "";
        txttitlefesc.Text = "";
        lblmessage.Text = "";
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
      "window.close();" + System.Environment.NewLine +
      "</script>");
    }
}