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
            binddec();
            bindcategory();
            System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));
           // System.DateTime dt1111 = System.DateTime.Now;
            System.DateTime dt1;
            //dt = dt.AddDays(1); 

           // dt1 = dt.AddHours(4);

            System.DateTime CurTime1 = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 09, 0, 0);
            if (dt < CurTime1)
            {
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 0, 0);
            }
            dt1 = dt.AddHours(4);

            System.DateTime CurrDate = System.DateTime.Now;
            System.DateTime CurTime = new DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, 18, 0, 0);

            if (CurTime < dt1)
            {
                System.DateTime nxtday, finalday;
                TimeSpan ts = dt1.Subtract(CurTime);
                nxtday = CurTime.AddDays(1);

                finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9, 0, 0);
                //ts = new TimeSpan(dtdiff.Hour,dtdiff.Minute,dtdiff.Second);
                finalday = finalday.Add(ts);
                dt1 = finalday;
            }

            /*
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
            */
            lbldosierheureval.Text = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
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
        if (FileUpload1.HasFile)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);

            string ext = Path.GetExtension(FileUpload1.FileName);
            if (ext == ".doc" || ext == ".docx")//ext == ".zip" || ext == ".rar" || ext == ".7zip" || 
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
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID);
                    }

                    if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename))
                    {
                        File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename);
                    }

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + DID + "\\" + filename);

                    // for calculate TAT
                    ////////  DateTime dt = System.DateTime.Now;
                    System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds)); 

                    System.DateTime dt1;
                    //dt = dt.AddDays(1); 

                    //Modify by Pradeep
                    System.DateTime CurTime1 = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 09, 0, 0);
                    if (dt < CurTime1)
                    {
                        dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 0, 0);
                    }
                    dt1 = dt.AddHours(4);

                    System.DateTime CurrDate = System.DateTime.Now;
                    System.DateTime CurTime = new DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, 18, 0, 0);

                    if (CurTime < dt1)
                    {
                        System.DateTime nxtday, finalday;
                        TimeSpan ts = dt1.Subtract(CurTime);
                        nxtday = CurTime.AddDays(1);

                        finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9, 0, 0);
                        //ts = new TimeSpan(dtdiff.Hour,dtdiff.Minute,dtdiff.Second);
                        finalday = finalday.Add(ts);
                        dt1 = finalday;
                    }
	            //Changes by Jitender 2018-03-14


        //    System.DateTime CurrDate = System.DateTime.Now;
        //        System.DateTime CurTime = new DateTime(CurrDate.Year,CurrDate.Month,CurrDate.Day,18,0,0);            

        //    if (CurTime < dt1)
        //    {
        //        System.DateTime dtdiff, nxtday,finalday;
        //        TimeSpan ts = dt1.Subtract(CurTime);
        //        nxtday = CurTime.AddDays(1);

        //        finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9,0,0);
        //        //ts = new TimeSpan(dtdiff.Hour,dtdiff.Minute,dtdiff.Second);
        //        finalday = finalday.Add(ts);

        //        if (finalday.DayOfWeek.ToString() == "Saturday")
        //        {
        //             finalday = finalday.AddDays(2);
        //        }
        //        else if (finalday.DayOfWeek.ToString() == "Sunday")
        //        {
        //             finalday = finalday.AddDays(1);                 
        //        }               
        //dt1 = finalday;
        //    }






                   
                    /*
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
                    */

                    /////////////////////////////////
                    string declination = ddlDeclination.SelectedItem.Text;
                    if (declination == "----------")
                    {
                        declination = "";
                    }
                  


                    SqlParameter[] paramList = new SqlParameter[16];
                    paramList[0] = new SqlParameter("@DID", DID);
                    paramList[1] = new SqlParameter("DECLINATION", "");//declination// ddlDeclination.SelectedItem.Text
                    paramList[2] = new SqlParameter("@CTITLE", txtfoldertitle.Text.Trim());
                    paramList[3] = new SqlParameter("@DEMANDTYPE", "");
                    paramList[4] = new SqlParameter("@DURATION", txtdosierdelai.Text.Trim());//"Courant"
                    paramList[5] = new SqlParameter("@ITERATION", "");
                    paramList[6] = new SqlParameter("@STAGE", "Dossier envoyé à Thomson");//"En attente prod"
                    paramList[7] = new SqlParameter("@REMARKS", txtcomment.Text.Trim());
                    paramList[8] = new SqlParameter("@Author", txtauthor.Text.Trim());
                    paramList[9] = new SqlParameter("@authormail", txtmailnotification.Text);
                    paramList[10] = new SqlParameter("@filename", filename);
                    paramList[11] = new SqlParameter("@category", ddlcategory.SelectedItem.Text.Replace("----------",""));
                    paramList[12] = new SqlParameter("@duedate", Convert.ToDateTime(dt1));
                    paramList[13] = new SqlParameter("@userid", Session[SESSION.LOGGED_USER].ToString());
                    if (txtdosierpublicationdate.Text != "")
                    {
                        paramList[14] = new SqlParameter("@pubdate", Convert.ToDateTime(txtdosierpublicationdate.Text));
                    }
                    else
                    {
                        paramList[14] = new SqlParameter("@pubdate", DBNull.Value);
                    }
                    paramList[15] = new SqlParameter("InDate", Common.GetDayLightTime());
                    // txtreference ddldelai, due date

                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertdossier_withduedate, paramList);
                    if (rowAffected > 0)
                    {
                        lblmessage.Text = "Item ajouté";
                        //  btnFinish.Visible = true;
                        //Jitender Shootmail
                        string strFile = Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\") + "D.html";

                        if (File.Exists(strFile))
                        {
                            string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"];
                            // string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];
                            StreamReader sr = new StreamReader(strFile);
                            string FileC = sr.ReadToEnd();
                            sr.Close();
                            string strBody = FileC;
                            strBody = strBody.Replace("[ILT]", txtfoldertitle.Text.Trim());
                            strBody = strBody.Replace("[DTAD]", dt1.ToString("dd-MMM-yyyy HH:mm:ss"));
                            if (txtcomment.Text.Trim() == "")
                            {
                                strBody = strBody.Replace("[IAC]", "aucun commentaire");
                            }
                            else
                            {
                                strBody = strBody.Replace("[IAC]", txtcomment.Text.Trim());
                            }
                            strBody = strBody.Replace("[IHA]", strLink);

                            string strSubject = "Léonard – Demande d’intervention sur le document : « " + txtfoldertitle.Text.Trim() + " »";
                            Common cmn = new Common();
                            Common.SendEmail(strTo, strCC, strSubject, strBody);
                            // Utility.NumberToEnglish.email();
                        }

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

                    Session["searchqry"] = null;
                    if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                     "window.opener.location.href='LNDossierLanding.aspx';window.close();" + System.Environment.NewLine +
                     "</script>");
                    }
                    else
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
             "window.opener.location.href='DossierLanding1.aspx';window.close();" + System.Environment.NewLine +
             "</script>");
                    }

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
        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
         "window.opener.location.href='LNDossierLanding.aspx';window.close();" + System.Environment.NewLine +
         "</script>");
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.opener.location.href='DossierLanding1.aspx';window.close();" + System.Environment.NewLine +
               "</script>");
        }
    }
}