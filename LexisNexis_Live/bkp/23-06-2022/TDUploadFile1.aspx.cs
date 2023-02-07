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

public partial class TDUploadFile1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
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
                hiddenval1.Text = ds.Tables[0].Rows[0]["ITERATION"].ToString();
            }

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    txttitlefesc.Text = ds.Tables[0].Rows[0]["DTITLE"].ToString();
            //}

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
                            File.Copy(file, Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD" + "\\" + EID + "\\Backup\\" + newbkpfilename, true);
                            File.Delete(file);
                        }
                    }

                    filename = EID + "_" + hiddenval1.Text + ext;// + "."
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

                        //Success mail
                        SqlParameter[] paramlist1 = new SqlParameter[1];
                        paramlist1[0] = new SqlParameter("eid", EID);
                        DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildataencyclo, paramlist1);
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            string strTitle = set.Tables[0].Rows[0]["DTITLE"].ToString();
                            string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                            string strComments = txtcomment.Text.Trim();// set.Tables[0].Rows[0]["COMMENTS"].ToString();

                            string strTo = set.Tables[0].Rows[0]["userid"].ToString(); //Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                           // string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = set.Tables[0].Rows[0]["NOTIFICATION"].ToString() + "," + System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];
                            // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                            // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                            string strFile = Server.MapPath("App_Data\\MAILS\\THOMSON_SEND_CORRECTION\\") + "E.html";

                            if (File.Exists(strFile))
                            {
                                StreamReader sr = new StreamReader(strFile);
                                string FileC = sr.ReadToEnd();
                                sr.Close();
                                string strBody = FileC;
                                strBody = strBody.Replace("[ILT]", strTitle);
                                strBody = strBody.Replace("[DTAD]", strDuedate);
                                if (strComments.Trim() == "")
                                {
                                    strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                }
                                else
                                {
                                    strBody = strBody.Replace("[IACE]", strComments);
                                }
                                strBody = strBody.Replace("[IHT]", strLink);

                                string strSubject = "Léonard – Extranet Éditorial – Correction faite pour le document : « " + strTitle + " »";
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
        txttitlefesc.Text = "";
        txtcomment.Text = "";
        lblmessage.Text = "";
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
     "window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
     "</script>");

    }
}