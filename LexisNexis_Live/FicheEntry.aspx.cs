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

public partial class FicheEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            bindgrid();

        }
    }
    public void bindgrid()
    {

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.FichesFreshFile);
        grdViewEntry.DataSource = ds;
        grdViewEntry.DataBind();
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        foreach (GridViewRow row in grdViewEntry.Rows)
        {
            CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
            if (chkCalendarId.Checked)
            {
                cnt++;
            }
        }
        if(cnt==0)
        {
            }
        foreach (GridViewRow row in grdViewEntry.Rows)
        {
            CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
            if (chkCalendarId.Checked)
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
                            string EID = "";

                            /*
                            // DateTime dt=DateTime.Now;
                            DataSet idtable = new DataSet();
                            idtable = DataAccess.ExecuteDataSetSP(SPNames.getEncycloid);
                            if (idtable.Tables[0].Rows.Count > 0)
                            {
                                string tidval = "";
                                string tempeid = idtable.Tables[0].Rows[0]["FID"].ToString();
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
                                    EID = "MAJ" + tidval;//dt.Year + dt.Month +
                                }
                                else
                                {
                                    EID = "MAJ" + "00001";//+ dt.Year + dt.Month
                                }
                            }
                            else
                            {
                                EID = "MAJ" + "00001";// + dt.Year + dt.Month
                            }
                            string ERID = "";
                            DataSet eridtable = new DataSet();
                            eridtable = DataAccess.ExecuteDataSetSP(SPNames.getEncycloERID);
                            if (eridtable.Tables[0].Rows.Count > 0)
                            {
                                string tidval = "";
                                string temperid = eridtable.Tables[0].Rows[0]["FRID"].ToString();
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
                                    ERID = "MAJR" + tidval;//dt.Year + dt.Month +
                                }
                                else
                                {
                                    ERID = "MAJR" + "00001";//+ dt.Year + dt.Month
                                }
                            }
                            else
                            {
                                ERID = "MAJR" + "00001";// + dt.Year + dt.Month
                            }
                            */
                            EID = row.Cells[0].Text;
                            string FRID = (row.FindControl("litID") as Literal).Text;

                            filename = EID + "_" + "0" + "." + ext;



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

                            if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename))
                            {
                                File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);
                            }

                            FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);


                            // for calculate TAT
                            ////////  DateTime dt = System.DateTime.Now;
                            System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));

                            System.DateTime dt1;
                            //dt = dt.AddDays(1); 


                            dt1 = dt.AddDays(1);

                            /*
                            // for calculate TAT
                            ////////  DateTime dt = System.DateTime.Now;
                            int numofdays = 0;
                            double numofhour = 0;
                            if (ddldelai.SelectedItem.Text == "Courant")
                            {
                                numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                            }
                            else if (ddldelai.SelectedItem.Text == "Express")//Étendu
                            {
                                numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                            }
                            else if (ddldelai.SelectedItem.Text == "retraitement")//Étendu
                            {
                                // numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                                numofhour = 4;// Convert.ToDouble(ddldelai.SelectedValue.ToString());
                            }
                            else
                            {
                                numofdays = 1;
                            }

                            System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(-4, -30, 0));
                            // System.DateTime dt = System.DateTime.Now;
                            System.DateTime dt1;

                            int nod = numofdays;
                            int counter = 0;
                            for (int i = 0; i <= nod; i++)
                            {
                                System.DateTime dttemp = dt.AddDays(i);
                                if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday)
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
                            //int TotalDay = nod + counter;

                            //dt1 = dt.AddDays(TotalDay);

                            while (true)
                            {
                                if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday)
                                    dt1 = dt1.AddDays(1);
                                else
                                {
                                    break;
                                }
                            }
                            */
                        










                            SqlParameter[] paramList = new SqlParameter[9];
                            paramList[0] = new SqlParameter("FID", EID);
                            paramList[1] = new SqlParameter("NOTIFICATION", txtnotification.Text.Trim());
                            paramList[2] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));//txtduedate.Text
                            paramList[3] = new SqlParameter("ITERATION", Convert.ToInt16("0"));
                            paramList[4] = new SqlParameter("STAGE", "En préparation");//En attente prod
                            paramList[5] = new SqlParameter("tat", "Courant");
                            paramList[6] = new SqlParameter("filesname", filename);
                            paramList[7] = new SqlParameter("frid", FRID);
                            paramList[8] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                            int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.updatefiche, paramList);
                            if (rowAffected > 0)
                            {

                                string strTitle = "";
                                string strDuedate = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
                               

                                string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"]; // System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                                string strLT = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];

                                //    string strStage = "Article in process";

                                string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                                string strCC = txtnotification.Text.Trim();
                                // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                string strFile = Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\") + "E.html";

                                if (File.Exists(strFile))
                                {
                                    StreamReader sr = new StreamReader(strFile);
                                    string FileC = sr.ReadToEnd();
                                    sr.Close();
                                    string strBody = FileC;
                                    strBody = strBody.Replace("[ILTD]", strTitle);
                                    strBody = strBody.Replace("[DTAT]", strDuedate);
                                   
                                        strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                                                      
                                    strBody = strBody.Replace("[IHTA]", strLink);

                                    string strSubject = "Léonard – Demande d’intervention sur le document : « " + strTitle + " »";
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
                       

                            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
               "</script>");

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        string message = "Merci de charger une pièce jointe valide (docx/doc)";
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
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAllFields();
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
       "window.close();" + System.Environment.NewLine +
       "</script>");
    }
    protected void ClearAllFields()
    {
        txtnotification.Text = "";
        grdViewEntry.DataSource = null;
        grdViewEntry.DataBind();
    }
}