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

public partial class EncyclopediaReviseEntry : System.Web.UI.Page
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
            string id = Request.QueryString["EID"].ToString();
            hiddenval.Text = id;
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("eid", id);
            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getencycloReviseentry, paramlist);
            txtleonardid.Text = id;
            txtleonardid.Enabled = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                txttitlefesc.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1]["DTITLE"].ToString();
            }

        }
    }
    public void LoadData(CultureInfo ci)
    {
        lblHeading.Text = rm.GetString("Log_correction", ci);
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
                    // DateTime dt=DateTime.Now;
                  
                    string ERID = "";
                    DataSet eridtable = new DataSet();
                    eridtable = DataAccess.ExecuteDataSetSP(SPNames.getEncycloERID);
                    if (eridtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string temperid = eridtable.Tables[0].Rows[0]["ERID"].ToString();
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
                            ERID = "ERNC" + tidval;//dt.Year + dt.Month +
                        }
                        else
                        {
                            ERID = "ERNC" + "00001";//+ dt.Year + dt.Month
                        }
                    }
                    else
                    {
                        ERID = "ERNC" + "00001";// + dt.Year + dt.Month
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

                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID);
                    }

                    if (Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID))
                    {
                        if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\Backup"))
                        {
                            Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\Backup");
                        }
                        string[] oldfile1 = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\Backup");

                        string[] oldfile = Directory.GetFiles(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID);
                        int versionnum = oldfile1.Length;
                        foreach (string file in oldfile)
                        {
                            versionnum++;
                            string tempfilename = Path.GetFileName(file);
                            string newbkpfilename = "Version_" + versionnum + "_" + tempfilename;
                            File.Copy(file, Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\Backup\\" + newbkpfilename, true);
                            File.Delete(file);
                        }
                    }

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);

                    int iteration = 0;
                    string folio = "";
                    string demadtype = "";
                    string collection = "";
                    string itemtype = "";
                    string dtd = "";
                    string applicationname = "";
                    string notifi = "";
                    string tat = "";
                    string reference = "";
                    string category = "";
                    SqlParameter[] paramlist1 = new SqlParameter[1];
                    paramlist1[0] = new SqlParameter("eid", hiddenval.Text.Trim());
                    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getencycloReviseentry, paramlist1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                         iteration = ds.Tables[0].Rows.Count;
                         folio = ds.Tables[0].Rows[iteration - 1]["FOLIO"].ToString();
                         demadtype = ds.Tables[0].Rows[iteration - 1]["DEMANDTYPE"].ToString();
                         collection = ds.Tables[0].Rows[iteration - 1]["COLLECTION"].ToString();
                         itemtype = ds.Tables[0].Rows[iteration - 1]["ITEMTYPE"].ToString();
                         dtd = ds.Tables[0].Rows[iteration - 1]["DTD"].ToString();
                         applicationname = ds.Tables[0].Rows[iteration - 1]["APPLICANTNAME"].ToString();
                         notifi = ds.Tables[0].Rows[iteration - 1]["NOTIFICATION"].ToString();
                         tat = ds.Tables[0].Rows[iteration - 1]["tat"].ToString();
                         reference = ds.Tables[0].Rows[iteration - 1]["reference"].ToString();
                         category = ds.Tables[0].Rows[iteration - 1]["category"].ToString();
                    }
                    // for calculate TAT
                    ////////  DateTime dt = System.DateTime.Now;
                    double numofdays = 0;
                    if (tat == "Urgent")
                    {
                        numofdays = 1;
                    }
                    else if (tat == "Courant")
                    {
                        numofdays = 2;
                    }
                    else if (tat == "Étendu")
                    {
                        numofdays = 6;
                    }
                    else
                    {
                        numofdays = 1;
                    }

                    System.DateTime dt = System.DateTime.Now;

                    System.DateTime dt1;
                    //dt = dt.AddDays(1); 

                    if (dt.DayOfWeek == DayOfWeek.Friday)
                    {
                        numofdays = numofdays + 2;
                        dt1 = dt.AddDays(numofdays);
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        numofdays = numofdays + 2;
                        dt1 = dt.AddDays(numofdays);
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        numofdays = numofdays + 1;
                        dt1 = dt.AddDays(numofdays);
                    }
                    else
                        dt1 = dt.AddDays(numofdays);
                    /////////////////////////////////

                    SqlParameter[] paramList = new SqlParameter[19];
                    paramList[0] = new SqlParameter("EID", EID);
                    paramList[1] = new SqlParameter("DTITLE", txttitlefesc.Text.Trim());
                    paramList[2] = new SqlParameter("FOLIO", folio);
                    paramList[3] = new SqlParameter("DEMANDTYPE", demadtype);
                    paramList[4] = new SqlParameter("COLLECTION", collection);
                    paramList[5] = new SqlParameter("ITEMTYPE", itemtype);
                    paramList[6] = new SqlParameter("DTD", dtd);
                    paramList[7] = new SqlParameter("APPLICANTNAME", applicationname);
                    paramList[8] = new SqlParameter("NOTIFICATION", notifi);
                    paramList[9] = new SqlParameter("COMMENTS", txtcomment.Text.Trim());
                    //  paramList[10] = new SqlParameter("INDATE", DateTime.Now);
                    //  paramList[1] = new SqlParameter("ALLOCATION_DATE", "Success");
                    paramList[10] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));
                    //  paramList[3] = new SqlParameter("REVISED_DUEDATE", "");
                    //  paramList[1] = new SqlParameter("DELIVERED_DATE", "Success");
                    paramList[11] = new SqlParameter("ITERATION", Convert.ToInt16(iteration));
                    // paramList[3] = new SqlParameter("RETURNDATE", "");
                    //  paramList[2] = new SqlParameter("PAGECOUNT", DateTime.Now);
                    paramList[12] = new SqlParameter("STAGE", "En correction");
                    paramList[13] = new SqlParameter("tat", tat);
                    paramList[14] = new SqlParameter("reference", reference);
                    paramList[15] = new SqlParameter("filesname", filename);
                    paramList[16] = new SqlParameter("category", category);
                    paramList[17] = new SqlParameter("erid", ERID);
                    paramList[18] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertencyclo, paramList);
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
                    txtcomment.Text = "";
                    txttitlefesc.Text = "";
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
        txtcomment.Text = "";
        txttitlefesc.Text = "";
        lblmessage.Text = "";
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
      "window.close();" + System.Environment.NewLine +
      "</script>");
    }
}