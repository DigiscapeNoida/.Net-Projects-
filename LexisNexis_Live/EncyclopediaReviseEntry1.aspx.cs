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

            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
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
                txttitlefesc.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["DTITLE"].ToString();
            }

        }
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

                    filename = EID + "_" + iteration.ToString() + ext;//+ "."
                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);
                    // for calculate TAT
                    ////////  DateTime dt = System.DateTime.Now;
                    /*
                    double numofdays = 0;
                    if (tat == "Urgent")
                    {
                        numofdays = 1;
                    }
                    else if (tat == "Courant")
                    {
                        numofdays = 2;
                    }
                    else if (tat == "Express")
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
                    */

                    ////////  DateTime dt = System.DateTime.Now;
                    int numofdays = 0;
                    double numofhour = 0;
                    if (tat == "Urgent")
                    {
                        numofdays = 2;
                    }
                    else if (tat == "Courant")
                    {
                        numofdays = 6;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else if (tat == "Express")//Étendu
                    {
                        numofdays = 1;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else if (tat == "retraitement")//Étendu
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
                    /////////////////////////////////

                    SqlParameter[] paramList = new SqlParameter[20];
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
                    paramList[18] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                    paramList[19] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertencyclo, paramList);
                    if (rowAffected > 0)
                    {
                        lblmessage.Text = "Item ajouté";
                        //Success mail
                        SqlParameter[] paramlist11 = new SqlParameter[1];
                        paramlist11[0] = new SqlParameter("eid", EID);
                        DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildataencyclo, paramlist11);
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            string strTitle = set.Tables[0].Rows[0]["DTITLE"].ToString();
                            string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                            string strComments = set.Tables[0].Rows[0]["COMMENTS"].ToString();

                            string strTo = Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                            string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = set.Tables[0].Rows[0]["NOTIFICATION"].ToString();
                            // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                            // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                            string strFile = Server.MapPath("App_Data\\MAILS\\THOMSON_RECEIVE_CORRECTION\\") + "E.html";

                            if (File.Exists(strFile))
                            {
                                StreamReader sr = new StreamReader(strFile);
                                string FileC = sr.ReadToEnd();
                                sr.Close();
                                string strBody = FileC;
                                strBody = strBody.Replace("[ILT]", strTitle);
                                strBody = strBody.Replace("[DAT]", strDuedate);
                                if (txtcomment.Text.Trim() == "")
                                {
                                    strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                }
                                else
                                {
                                    strBody = strBody.Replace("[IACE]", txtcomment.Text.Trim());
                                }
                                strBody = strBody.Replace("[IHT]", strLink);

                                string strSubject = "Léonard – Demande de correction sur le document :  « " + strTitle + " »";
                                Common cmn = new Common();
                                Common.SendEmail(strTo, strCC, strSubject, strBody);
                                // Utility.NumberToEnglish.email();
                            }

                        }
                        //  btnFinish.Visible = true;
                    }
                    else
                    {
                        lblmessage.Text = "error";
                        return;
                    }
                    txtcomment.Text = "";
                    txttitlefesc.Text = "";
                    if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                     "window.opener.location.href='LNEncyclopedia.aspx';window.close();" + System.Environment.NewLine +
                     "</script>");
                    }
                    else
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                       "window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
                       "</script>");
                    }
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
        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
         "window.opener.location.href='LNEncyclopedia.aspx';window.close();" + System.Environment.NewLine +
         "</script>");
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
           "</script>");
        }
      //  Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
      //"window.close();" + System.Environment.NewLine +
      //"</script>");
    }
}