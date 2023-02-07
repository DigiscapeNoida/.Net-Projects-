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

public partial class JournalEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getemail();
            bindjid();
          // bindjidtest();
            bindtat();
           // bindcheckboxjid();
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;

           // System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));
            // System.DateTime dt1111 = System.DateTime.Now;
            System.DateTime dt1;
            dt1 = GetCutOffTime();
            //dt = dt.AddDays(1); 

            //if (dt.DayOfWeek == DayOfWeek.Friday)
            //{
            //    dt1 = dt.AddDays(3);
            //}
            //else if (dt.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    dt1 = dt.AddDays(3);
            //}
            //else if (dt.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    dt1 = dt.AddDays(2);
            //}
            //else
            //    dt1 = dt.AddDays(1);

            lblJournalheureval.Text = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
        }
    }

    private DateTime GetCutOffTime(string deliverttime = null)
    {
        TimeSpan ts = TimeSpan.Zero;
        int numofdays = 0;
        double numofhour = 0;
        DateTime DLTime = Common.GetDayLightTime();
        System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(DLTime.Subtract(DateTime.Now).Hours, DLTime.Subtract(DateTime.Now).Minutes, DLTime.Subtract(DateTime.Now).Seconds));
        // dt = new System.DateTime(2018, 5, 29, 18,05, 00);

        System.DateTime MorningTime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 09, 0, 0);
        System.DateTime eveningTime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 18, 0, 0);
        if (dt < MorningTime)
        {
            dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 0, 0);
        }
        else if (eveningTime < dt)
        {
            dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 0, 0).AddDays(1);
        }
        else
        {
            System.DateTime nxtday, finalday;
            ts = eveningTime.Subtract(dt);
            TimeSpan ts2 = dt.Subtract(eveningTime);
            if (ts.Hours < 4)
            {
                if (ddldelai.SelectedItem.Text == "Express" && deliverttime == "DeliveryTime")
                {
                    nxtday = dt.AddDays(1);
                    finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9, 0, 0);
                    dt = finalday;
                }
                else
                    nxtday = dt.AddDays(0);

            }
        }
        //======================================TAT Start==================================
        if (deliverttime == "DeliveryTime")
        {
            if (ddldelai.SelectedItem.Text == "Rapide")
            {
                numofdays = 1;
            }
            else if (ddldelai.SelectedItem.Text == "Courant")
            {
                numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
            }
            else if (ddldelai.SelectedItem.Text == "Express")
            {
                numofhour = 4;
            }
            else
            {
                numofdays = 1;
            }


        }
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
        //=========================================TAT=======================================


        if (deliverttime == "DeliveryTime")
        {
            // finalday = finalday.Add(ts);
            if (numofhour > 0)
            {
                if (ts.Hours < 4)
                    dt = dt.AddHours(numofhour).Add(-ts);
                else
                    dt = dt.AddHours(numofhour);
            }
            else if (numofdays > 0)
            {
                int TotalDay = nod + counter;

                dt = dt.AddDays(TotalDay);
            }
            else
            {
                dt = dt.AddDays(0).Add(-ts);
            }
        }

        //if (eveningTime < dt1)
        //{
        //    System.DateTime nxtday, finalday;
        //    TimeSpan ts = dt1.Subtract(eveningTime);
        //    nxtday = eveningTime.AddDays(1);

        //    finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9, 0, 0);
        //    dt1 = finalday;   
        //}
        while (true)
        {
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);
            else
            {
                break;
            }
        }


        return dt;
    }
    private void getemail()
    {
           SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getmailfromemories,paramlist);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtsupnotification.Text = ds.Tables[0].Rows[0]["RVmailnoti"].ToString();
        }
    }
    private void bindcheckboxjid()
    {
       // ddlreview.Items.Clear();
        //SqlParameter[] paramlist = new SqlParameter[1];
        //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
        CheckBoxList1.DataSource = ds;
        CheckBoxList1.DataTextField = "journal_name";
        CheckBoxList1.DataValueField = "JID";
        CheckBoxList1.DataBind();
        
    }
    private void bindjid()
    {
        ddlreview.Items.Clear();
        //SqlParameter[] paramlist = new SqlParameter[1];
        //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
        ddlreview.DataSource = ds;
        ddlreview.DataTextField = "journal_name";
        ddlreview.DataValueField = "JID";
        ddlreview.DataBind();
        ddlreview.Items.Insert(0, new ListItem("", "-1"));
       // ddlreview.Items.Insert(0, new ListItem("----------", "-1"));
    }
   
    private void bindjidtest()
    {
        ddlreview1.Items.Clear();
        //SqlParameter[] paramlist = new SqlParameter[1];
        //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
        ddlreview1.DataSource = ds;
        ddlreview1.DataTextField = "journal_name";
        ddlreview1.DataValueField = "JID";
        ddlreview1.DataBind();
        ddlreview1.Items.Insert(0, new ListItem("", "-1"));
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
    protected void btnSendJournal_Click(object sender, EventArgs e)
    {
string message = "";

	// string jidsssss = ddlTest.SelectedValue.ToString();
        /*
        int cnt = 0;
        foreach (ListItem chk in CheckBoxList1.Items)
        {

            if (chk.Selected == true)
            {
                cnt++;
            }
        }
        if (cnt == 0)
        {
            lblmessage.Text = "sélectionner un article";
            return;
        }
        */
        if (FileUpload1.HasFile)
        {

  	    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(FileUpload1.FileName);
            string filename = System.Text.Encoding.UTF8.GetString(byteArray);

           // string filename = Path.GetFileName(FileUpload1.FileName);

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

                    string articleid = "";
                    string jid = ddlreview.SelectedValue.ToString(); // ddlreview.SelectedItem.Text;
                    string aid = "";
                    DataSet dsaid = new DataSet();
                    SqlParameter[] paramjid = new SqlParameter[1];
                    paramjid[0] = new SqlParameter("jid", jid);
                    dsaid = DataAccess.ExecuteDataSetSP(SPNames.getmaxaid, paramjid);
                    if (dsaid.Tables[0].Rows.Count > 0)
                    {
                        if (dsaid.Tables[0].Rows[0]["aid"].ToString() == null || dsaid.Tables[0].Rows[0]["aid"].ToString() == "")
                        {
                            aid = "0001";
                        }
                        else
                        {
                            int tmpaid = Convert.ToInt16(dsaid.Tables[0].Rows[0]["aid"].ToString());
                            tmpaid++;
                            if (tmpaid.ToString().Length == 1)
                            {
                                aid = "000" + tmpaid.ToString();
                            }
                            else if (tmpaid.ToString().Length == 2)
                            {
                                aid = "00" + tmpaid.ToString();
                            }

                            else if (tmpaid.ToString().Length == 3)
                            {
                                aid = "0" + tmpaid.ToString();
                            }
                            else
                            {
                                aid = tmpaid.ToString();
                            }
                        }
                    }
                    else
                    {
                        aid = "0001";
                    }
                    string ArticleRID = "";
                    DataSet eridtable = new DataSet();
                    eridtable = DataAccess.ExecuteDataSetSP(SPNames.getJournalArticleRID);
                    if (eridtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string temperid = eridtable.Tables[0].Rows[0]["ARTICLERID"].ToString();
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
                            ArticleRID = "RV" + tidval;//dt.Year + dt.Month +
                        }
                        else
                        {
                            ArticleRID = "RV" + "00001";//+ dt.Year + dt.Month
                        }
                    }
                    else
                    {
                        ArticleRID = "RV" + "00001";// + dt.Year + dt.Month
                    }
                    articleid = jid + aid;

                    // commented on 14 dec 16
                  //  filename = articleid + "_" + "0" + ext;


                    // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString());
                    }
                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN"))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN");
                    }

                    if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleid))
                    {
                        Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleid);
                    }

                    if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleid + "\\" + filename))
                    {
                        File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleid + "\\" + filename);
                    }

                    FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + articleid + "\\" + filename);


            //        // for calculate TAT
            //        ////////  DateTime dt = System.DateTime.Now;
            //        int numofdays = 0;
            //        double numofhour = 0;
            //        if (ddldelai.SelectedItem.Text == "Rapide")
            //        {
            //            //numofhour = 5;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
            //            numofdays = 1;
            //        }
            //        else if (ddldelai.SelectedItem.Text == "Courant")
            //        {
            //            numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
            //        }
            //        else if (ddldelai.SelectedItem.Text == "Express")
            //        {
            //            numofhour = 4;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
            //        }
            //        else
            //        {
            //            numofdays = 1;
            //        }

            //        System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));
            //        // System.DateTime dt = System.DateTime.Now;
            //        System.DateTime dt1;

            //        int nod = numofdays;
            //        int counter = 0;
            //        for (int i = 0; i <= nod; i++)
            //        {
            //            System.DateTime dttemp = dt.AddDays(i);
            //            if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dttemp.Date.ToString("dd/MM/")) != -1)
            //            {
            //                counter++;
            //            }
            //        }

            //        /////////////////////////////Added by Pradeep on 01 May 2018//////////////////////////////////
            //        if (numofhour > 0)
            //        {
            //            System.DateTime CurTime1 = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 09, 0, 0);
            //            if (dt < CurTime1)
            //            {
            //                dt = new DateTime(dt.Year, dt.Month, dt.Day, 09, 0, 0);
            //            }
            //            dt1 = dt.AddHours(numofhour);

            //            System.DateTime CurrDate = System.DateTime.Now;
            //            System.DateTime CurTime = new DateTime(CurrDate.Year, CurrDate.Month, CurrDate.Day, 18, 0, 0);

            //            if (CurTime < dt1)
            //            {
            //                System.DateTime nxtday, finalday;
            //                TimeSpan ts = dt1.Subtract(CurTime);
            //                nxtday = CurTime.AddDays(1);

            //                finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9, 0, 0);
            //                finalday = finalday.Add(ts);
            //                dt1 = finalday;
            //            }
            //        }

            ////        if (numofhour > 0)
            ////        {
            ////            dt1 = dt.AddHours(numofhour);
			
            ////    //Changes by Jitender 2018-03-24

            ////System.DateTime CurrDate = System.DateTime.Now;
            ////    System.DateTime CurTime = new DateTime(CurrDate.Year,CurrDate.Month,CurrDate.Day,18,0,0);            

            ////    if (CurTime < dt1)
            ////    {
            ////        System.DateTime dtdiff, nxtday,finalday;
            ////        TimeSpan ts = dt1.Subtract(CurTime);
            ////        nxtday = CurTime.AddDays(1);

            ////        finalday = new DateTime(nxtday.Year, nxtday.Month, nxtday.Day, 9,0,0);
            ////        //ts = new TimeSpan(dtdiff.Hour,dtdiff.Minute,dtdiff.Second);
            ////        finalday = finalday.Add(ts);

            ////        if (finalday.DayOfWeek.ToString() == "Saturday")
            ////        {
            ////             finalday = finalday.AddDays(2);
            ////        }
            ////        else if (finalday.DayOfWeek.ToString() == "Sunday")
            ////        {
            ////             finalday = finalday.AddDays(1);                 
            ////        }               
            ////dt1 = finalday;
            ////    }



            ////        }
            //        else if (numofdays > 0)
            //        {
            //            int TotalDay = nod + counter;

            //            dt1 = dt.AddDays(TotalDay);
            //        }
            //        else
            //        {
            //            dt1 = dt.AddDays(0);
            //        }

            //        while (true)
            //        {
            //            if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dt1.Date.ToString("dd/MM/")) != -1)
            //                dt1 = dt1.AddDays(1);
            //            else
            //            {
            //                break;
            //            }
            //        }


                    System.DateTime dt1 = GetCutOffTime("DeliveryTime");


                    SqlParameter[] paramList = new SqlParameter[18];
                    paramList[0] = new SqlParameter("articleID", articleid);
                    paramList[1] = new SqlParameter("jid", ddlreview.SelectedValue.ToString().Replace("-1", ""));//ddlreview.SelectedItem.Text.Replace("----------", "")
                    paramList[2] = new SqlParameter("aid", aid);
                    paramList[3] = new SqlParameter("articletitle", txtarticletitle.Text.Trim());
                    paramList[4] = new SqlParameter("articletype", txtarticletype.Text.Trim());
                    paramList[5] = new SqlParameter("pubnumber", txtpubnum.Text.Trim());// ddlitemtype.SelectedItem.Text.Replace("----------","")
                    paramList[6] = new SqlParameter("tat", ddldelai.SelectedItem.Text.Replace("----------", ""));
                    paramList[7] = new SqlParameter("email", txtsupnotification.Text.Trim());// txtapplicationname.Text.Trim()
                    paramList[8] = new SqlParameter("comments", txtcomment.Text.Trim());
                    paramList[9] = new SqlParameter("filesname", filename);
                    paramList[10] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));//txtduedate.Text
                    paramList[11] = new SqlParameter("ITERATION", Convert.ToInt16("0"));
                    paramList[12] = new SqlParameter("STAGE", "En préparation");//En attente prod
                    paramList[13] = new SqlParameter("articlerid", ArticleRID);
                    paramList[14] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                    paramList[15] = new SqlParameter("authorname", txtjournalauthor.Text.Trim());
                    paramList[16] = new SqlParameter("worktobedone", ddlworktobedone.SelectedValue.ToString());
                    paramList[17] = new SqlParameter("InDate", GetCutOffTime());
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.insertJournal, paramList);
                    if (rowAffected > 0)
                    {
                        SqlParameter[] paramListemail = new SqlParameter[2];
                        paramListemail[0] = new SqlParameter("email", txtsupnotification.Text.Trim());
                        paramListemail[1] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                        int rowa = DataAccess.ExecuteNonQuerySP(SPNames.insertMailformemories, paramListemail);



                        string strTitle = articleid + "- " + txtarticletitle.Text.Trim();// txttitle.Text.Trim();
                        string strDuedate = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
                        string strComments = txtcomment.Text.Trim();

                        string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"]; // System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                        string strLT = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];

                        //    string strStage = "Article in process";

                        string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                        string strCC = txtsupnotification.Text.Trim();
                        // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                        // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                        string strFile = Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\") + "J.html";

                        if (File.Exists(strFile))
                        {
                            StreamReader sr = new StreamReader(strFile);
                            string FileC = sr.ReadToEnd();
                            sr.Close();
                            string strBody = FileC;
                            strBody = strBody.Replace("[ILTD]", strTitle);
                            strBody = strBody.Replace("[DTAT]", strDuedate);
                            if (strComments.Trim() == "")
                            {
                                strBody = strBody.Replace("[IACN]", "aucun commentaire");
                            }
                            else
                            {
                                strBody = strBody.Replace("[IACN]", strComments);
                            }
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
                    ddlreview.SelectedIndex = -1;
                    txtarticletitle.Text = "";
                    txtjournalauthor.Text = "";
                    txtarticletype.Text = "";
                    txtpubnum.Text = "";
                    ddldelai.SelectedIndex = -1;
                    txtsupnotification.Text = "";
                    txtcomment.Text = "";
                    lblmessage.Text = "";

                    Session["searchqryJournal"] = null;
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
      "window.close();" + System.Environment.NewLine +
      "</script>");
                   /* Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
       "window.opener.location.href='JournalLanding.aspx';window.close();" + System.Environment.NewLine +
       "</script>");*/

                }
                catch (Exception ex)
                {
					Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                       "alert(" + "\"" + ex.Message + "\"" + ");" + System.Environment.NewLine +
                       "</script>");
                }
            }
            else
            {
                message = "Merci de charger une pièce jointe valide (docx/doc)";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                   "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                   "</script>");
            }
        }
        else
        {
            message = "choisir le dossier";
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
               "</script>");
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
        foreach (ListItem item in CheckBoxList1.Items)
        {
            //check anything out here
            if (item.Selected)
                item.Selected = false;
        }
       // ddlreview.SelectedIndex = -1;
        txtarticletitle.Text = "";
        txtjournalauthor.Text = "";
        txtarticletype.Text = "";
        txtpubnum.Text = "";
        ddldelai.SelectedIndex = -1;
        txtsupnotification.Text = "";
        txtcomment.Text = "";
        lblmessage.Text = "";
    }
    protected void ddldelai_SelectedIndexChanged(object sender, EventArgs e)
    {
        //=====================Comment on 08/06/2018==================
        ////////  DateTime dt = System.DateTime.Now;
        //int numofdays = 0;
        //double numofhour=0;
        //if (ddldelai.SelectedItem.Text == "Rapide")
        //{
        //    //numofhour = 5;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
        //    numofdays = 1;
        //}
        //else if (ddldelai.SelectedItem.Text == "Courant")
        //{
        //    numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
        //}
        //else if (ddldelai.SelectedItem.Text == "Express")
        //{
        //    numofhour = 4;// Convert.ToInt16(ddldelai.SelectedValue.ToString());
        //}
        //else
        //{
        //    numofdays = 1;
        //}

        //System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));
        //// System.DateTime dt = System.DateTime.Now;
        //System.DateTime dt1;

        //int nod = numofdays;
        //int counter = 0;
        //for (int i = 0; i <= nod; i++)
        //{
        //    System.DateTime dttemp = dt.AddDays(i);
        //    if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dttemp.Date.ToString("dd/MM/")) != -1)
        //    {
        //        counter++;
        //    }
        //}

       
        //if (numofhour > 0)
        //{
        //  dt1 =   dt.AddHours(numofhour);
        //}
        //else if (numofdays > 0)
        //{
        //    int TotalDay = nod + counter;

        //    dt1 = dt.AddDays(TotalDay);
        //}
        //else
        //{
        //    dt1 = dt.AddDays(0);
        //}

        //while (true)
        //{
        //    if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dt1.Date.ToString("dd/MM/")) != -1)
        //        dt1 = dt1.AddDays(1);
        //    else
        //    {
        //        break;
        //    }
        //}
        System.DateTime dt1 = GetCutOffTime("DeliveryTime");
        lblJournalheureval.Text = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
    }
}