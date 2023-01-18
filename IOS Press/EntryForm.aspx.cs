using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

public partial class EntryForm : System.Web.UI.Page
{
    MySqlConnection objCon;
    MySqlCommand objCmnd;
    MySqlDataReader Dr;
    string strConnectionString = "";
    DataTable dt;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] == null)
        {
            Response.Redirect("Logout.aspx");
        }
        else
        {
            if (Session["role"].ToString() != "admin")
            {
                Response.Redirect("Logout.aspx");
            }
        }
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cookies.Clear();
        HttpContext.Current.Request.Cookies.Clear();
        //strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];
        strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];

        if (IsPostBack == false)
        {

            StreamReader sr = new StreamReader(Server.MapPath("~") + "\\" + "JID.ini");
            string strCont = sr.ReadToEnd();
            sr.Close();

            string[] sp = new string[4];

            sp[0] = "\n\r";
            sp[1] = "\r\n";
            sp[2] = "\r";
            sp[3] = "\n";

            string[] Arr = strCont.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            //Array.Sort(Arr);
            cmbJID.Items.Clear();
            for (int i = 0; i < Arr.Length; i++)
            {
                cmbJID.Items.Add(Arr[i]);
            }

            //// Fill article type dropdown
            //// Edited by kshitij

            StreamReader sr_Article = new StreamReader(Server.MapPath("~") + "\\" + "ArticleType.ini");
            string articleTypes = sr_Article.ReadToEnd();
            sr_Article.Close();

            String[] typeArr = articleTypes.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            dd_ArticleType.Items.Clear();
            foreach (string arr in typeArr)
                dd_ArticleType.Items.Add(arr.ToUpper());
            //dd_ArticleType.DataBind();

            //// Fill WaterMark dropdown
            dd_watermark.Items.Add(new ListItem("--Select--"));
            dd_watermark.Items.Add(new ListItem("Yes"));
            dd_watermark.Items.Add(new ListItem("No"));

        }

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ToString());
        string strJID = cmbJID.Text.Trim();
        string strAID = txtAID.Text.Trim();
        string strVolIss = txtVolIss.Text.Trim();
        string strReceipt = txtReceiptDt.Text.Trim();
        string strJIDDesc = txtJIDDescription.Text.Trim();
        string strTitle = txtTitle.Text.Trim();
        string strAuthors = txtAuthors.Text.Trim();
        string strTypesetPages = txtTypesetPages.Text.Trim();
        string strSR = txtSR.Text.Trim();
        string strtoEditor = txttoEditor.Text.Trim();
        string strtoAuthors = txttoAuthors.Text.Trim();
        string strtoEditorial = txttoEditorial.Text.Trim();
        string strfromAuthors = txtfromAuthors.Text.Trim();
        string strfromEditor = txtfromEditor.Text.Trim();
        string strfromEditorial = txtfromEditorial.Text.Trim();
        string strCompilationDt = txtCompilationDt.Text.Trim();
        string strCRCDt = txtCRCDt.Text.Trim();
        string strPrintConfirmationDT = txtPrintConfirmationDt.Text.Trim();
        string strPrintDtIOS = txtPrintDtIOS.Text.Trim();
        string strThomsonRemarks = txtThomsonRemarks.Text.Trim();
        string strTo = txtTo.Text.Trim();
        string strCC = txtCC.Text.Trim();
        string strBCC = txtBCC.Text.Trim();
        string strRDate = txtReminderDate.Text.Trim();
        string strOAO = txtOAO.Text.Trim();

        //////////////////



        string SqlSt = "";
        string SqlSt1 = "";
        string strJournalID = CheckForExistence(strJID, strAID);
        if (strJournalID.Trim().Length > 0)
        {
            SqlSt = "Update IOSJournal set VolIss = '" + strVolIss + "', title = '" + strTitle +
                "', JIDDesc = '" + strJIDDesc + "'";

            SqlSt1 = "Update IOSJournalDetails set Authors = '" + strAuthors + "', " +
                "TSPages = '" + strTypesetPages + "'," +
                "AuthorReq = '" + strSR + "'," +
                "TsRemarks = '" + strThomsonRemarks + "' ";

            

            string TempS;
            TempS = "";
            if (strReceipt.Length > 0)
            {
                TempS = TempS + "DtReceipt = '" + strReceipt + "'";
            }

            if (strtoAuthors.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtProofsSent_Author = '" + strtoAuthors + "'";
                else
                    TempS = "DtProofsSent_Author = '" + strtoAuthors + "'";
            }

            if (strfromAuthors.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtCorrectionsRec_Author = '" + strfromAuthors + "'";
                else
                    TempS = "DtCorrectionsRec_Author = '" + strfromAuthors + "'";
            }

            if (strtoEditor.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtProofsSent_Editor = '" + strtoEditor + "'";
                else
                    TempS = "DtProofsSent_Editor = '" + strtoEditor + "'";
            }

            if (strfromEditor.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtCorrectionsRec_Editor = '" + strfromEditor + "'";
                else
                    TempS = "DtCorrectionsRec_Editor = '" + strfromEditor + "'";
            }

            if (strtoEditorial.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtProofsSent_Editorial = '" + strtoEditorial + "'";
                else
                    TempS = "DtProofsSent_Editorial = '" + strtoEditorial + "'";
            }

            if (strfromEditorial.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",DtCorrectionsRec_Editorial = '" + strfromEditorial + "'";
                else
                    TempS = "DtCorrectionsRec_Editorial = '" + strfromEditorial + "'";
            }

            if (strCompilationDt.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",IssComDt = '" + strCompilationDt + "'";
                else
                    TempS = "IssComDt = '" + strCompilationDt + "'";
            }

            if (strCRCDt.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",CRCDeliveryDt = '" + strCRCDt + "'";
                else
                    TempS = "CRCDeliveryDt = '" + strCRCDt + "'";
            }

            if (strPrintConfirmationDT.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",PrintPDFDT = '" + strPrintConfirmationDT + "'";
                else
                    TempS = "PrintPDFDT = '" + strPrintConfirmationDT + "'";
            }

            if (strPrintDtIOS.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",PrintPDF2IOSDt = '" + strPrintDtIOS + "'";
                else
                    TempS = "PrintPDF2IOSDt = '" + strPrintDtIOS + "'";
            }

            string TempS1 = "";
            if (strTo.Length > 0)
            {
                if (TempS1.Length > 0)
                    TempS1 = TempS1 + ",EmailTo = '" + strTo + "'";
                else
                    TempS1 = "EmailTo = '" + strTo + "'";
            }

            if (strCC.Length > 0)
            {
                if (TempS1.Length > 0)
                    TempS1 = TempS1 + ",EmailCC = '" + strCC + "'";
                else
                    TempS1 = "EmailCC = '" + strCC + "'";
            }
           

            if (strBCC.Length > 0)
            {
                if (TempS1.Length > 0)
                    TempS1 = TempS1 + ",EmailBCC = '" + strBCC + "'";
                else
                    TempS1 = "EmailBCC = '" + strBCC + "'";
            }

            if (strRDate.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",ReminderDate = '" + strRDate + "'";
                else
                    TempS = "ReminderDate = '" + strRDate + "'";
            }

            if (strOAO.Length > 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",OAO = '" + strOAO + "'";
                else
                    TempS = "OAO = '" + strOAO + "'";
            }





            //// Edited By Kshitij

            //// TempS1 is for iosjounrnal
            //// Temps is for iosjournaldetails

            //// Adding newly added coloumns

            if (dd_ArticleType.SelectedIndex != 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",ArticleType = '" + dd_ArticleType.SelectedValue + "'";
                else
                    TempS = "ArticleType  = '" + dd_ArticleType.SelectedValue + "'";
            }
            if ( ! string.IsNullOrEmpty(txt_Reprints.Text))
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",Reprints = '" +  txt_Reprints.Text.Trim() + "'";
                else
                    TempS = "Reprints = '" + txt_Reprints.Text.Trim() + "'";
            }
            if (dd_watermark.SelectedIndex != 0)
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",WaterMark = '" + dd_watermark.SelectedValue + "'";
                else
                    TempS = "WaterMark = '" + dd_watermark.SelectedValue + "'";
            }
            if (!string.IsNullOrEmpty(txt_AuthorCR.Text))
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",AutoCorrectionReminder = '" + txt_AuthorCR.Text.Trim() + "'";
                else
                    TempS = "AutoCorrectionReminder = '" + txt_AuthorCR.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txt_ColorFigure.Text))
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",ColorFigure= '" + txt_ColorFigure.Text.Trim() + "'";
                else
                    TempS = "ColorFigure= '" + txt_ColorFigure.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txt_SpecialIssueName.Text.Trim()))
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",SpecialIssueName= '" + txt_SpecialIssueName.Text.Trim() + "'";
                else
                    TempS = "SpecialIssueName= '" + txt_SpecialIssueName.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txr_HardCopy.Text.Trim()))
            {
                if (TempS.Length > 0)
                    TempS = TempS + ",HardCopy= '" + txr_HardCopy.Text.Trim() + "'";
                else
                    TempS = "HardCopy= '" + txr_HardCopy.Text.Trim() + "'";
            }

            //// done

            
            if (TempS.Length > 0)
            {
                SqlSt1 = SqlSt1 + "," + TempS;
            }

            if (TempS1.Length > 0)
            {
                SqlSt = SqlSt + "," + TempS1;
            }

            SqlCommand cmd2 = new SqlCommand("update_isodetail", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@auth", strAuthors);
            cmd2.Parameters.AddWithValue("@tsp", strTypesetPages);
            cmd2.Parameters.AddWithValue("@authreq", strSR);
            cmd2.Parameters.AddWithValue("@tsremark", strThomsonRemarks);
            cmd2.Parameters.AddWithValue("@jid", strJournalID);
            cmd2.Parameters.AddWithValue("@dtrec", strReceipt);
            cmd2.Parameters.AddWithValue("@dtsentau", strtoAuthors);
            cmd2.Parameters.AddWithValue("@dtrecau", strfromAuthors);
            cmd2.Parameters.AddWithValue("@dtsented", strtoEditor);
            cmd2.Parameters.AddWithValue("@dtreced", strfromEditor);
            cmd2.Parameters.AddWithValue("@dtsenteditorial", strtoEditorial);
            cmd2.Parameters.AddWithValue("@dtreceditorial", strfromEditorial);
            cmd2.Parameters.AddWithValue("@isscomdt", strCompilationDt);
            cmd2.Parameters.AddWithValue("@crcdldt", strCRCDt);
            cmd2.Parameters.AddWithValue("@printpdfdt", strPrintConfirmationDT);
            cmd2.Parameters.AddWithValue("@printpdfiosdt", strPrintDtIOS);
            cmd2.Parameters.AddWithValue("@remdate", strRDate);
            cmd2.Parameters.AddWithValue("@oao", strOAO);
            if (dd_ArticleType.SelectedIndex == 0)
            {
                cmd2.Parameters.AddWithValue("@artype", "");
            }
            else
            {
                cmd2.Parameters.AddWithValue("@artype", dd_ArticleType.SelectedValue);
            }
            cmd2.Parameters.AddWithValue("@reprint", txt_Reprints.Text.Trim());
            if (dd_watermark.SelectedIndex == 0)
            {
                cmd2.Parameters.AddWithValue("@watermark", "");
            }
            else
            {
                cmd2.Parameters.AddWithValue("@watermark", dd_watermark.SelectedValue);
            }
            cmd2.Parameters.AddWithValue("@autocorrem", txt_AuthorCR.Text.Trim());
            cmd2.Parameters.AddWithValue("@colfig", txt_ColorFigure.Text.Trim());
            cmd2.Parameters.AddWithValue("@splname", txt_SpecialIssueName.Text.Trim());
            cmd2.Parameters.AddWithValue("@hcopy", txr_HardCopy.Text.Trim());
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }

            SqlCommand cmd = new SqlCommand("update_isojournal", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@volis", strVolIss);
            cmd.Parameters.AddWithValue("@ttl", strTitle);
            cmd.Parameters.AddWithValue("@jidds", strJIDDesc);
            cmd.Parameters.AddWithValue("@mailto", strTo);
            cmd.Parameters.AddWithValue("@mailcc", strCC);
            cmd.Parameters.AddWithValue("@mailbcc", strBCC);
            cmd.Parameters.AddWithValue("@jid", strJournalID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }

            SqlSt = SqlSt + " where JournalID = '" + strJournalID + "'";

            SqlSt1 = SqlSt1 + " where JournalID = '" + strJournalID + "'";
        }
        else
        {
            strJournalID = GetUniqueValue();
            string strSeqID = "1";
            SqlSt = "Insert into IOSJournal values ('" + strJournalID +
                        "','" + strJID +
                        "','" + strJIDDesc +
                        "','" + strAID +
                        "','" + strTitle +
                        "','" + strVolIss +
                        "','" + strTo +
                        "','" + strCC +
                        "','" + strBCC +
                        "','No','')";
            SqlCommand cmd1 = new SqlCommand("insert_isojournal", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Clear();
            cmd1.Parameters.AddWithValue("@volis1", strVolIss);
            cmd1.Parameters.AddWithValue("@ttl1", strTitle);
            cmd1.Parameters.AddWithValue("@jidds1", strJIDDesc);
            cmd1.Parameters.AddWithValue("@mailto1", strTo);
            cmd1.Parameters.AddWithValue("@mailcc1", strCC);
            cmd1.Parameters.AddWithValue("@mailbcc1", strBCC);
            cmd1.Parameters.AddWithValue("@jourid", strJournalID);
            cmd1.Parameters.AddWithValue("@jid1", strJID);
            cmd1.Parameters.AddWithValue("@aid", strAID);
            try
            {
                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }

            SqlCommand cmd2 = new SqlCommand("insert_isodetail", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@jid", strJournalID);
            cmd2.Parameters.AddWithValue("@dtrec", strReceipt);
            cmd2.Parameters.AddWithValue("@auth", strAuthors);
            cmd2.Parameters.AddWithValue("@dtsentau", strtoAuthors);
            cmd2.Parameters.AddWithValue("@dtrecau", strfromAuthors);
            cmd2.Parameters.AddWithValue("@dtsented", strtoEditor);
            cmd2.Parameters.AddWithValue("@dtreced", strfromEditor);
            cmd2.Parameters.AddWithValue("@dtsenteditorial", strtoEditorial);
            cmd2.Parameters.AddWithValue("@dtreceditorial", strfromEditorial);
            cmd2.Parameters.AddWithValue("@tsp", strTypesetPages);
            cmd2.Parameters.AddWithValue("@authreq", strSR);
            cmd2.Parameters.AddWithValue("@isscomdt", strCompilationDt);
            cmd2.Parameters.AddWithValue("@crcdldt", strCRCDt);
            cmd2.Parameters.AddWithValue("@printpdfdt", strPrintConfirmationDT);
            cmd2.Parameters.AddWithValue("@printpdfiosdt", strPrintDtIOS);
            cmd2.Parameters.AddWithValue("@tsremark", strThomsonRemarks);                       
            cmd2.Parameters.AddWithValue("@remdate", strRDate);
            cmd2.Parameters.AddWithValue("@oao", strOAO);
            if (dd_ArticleType.SelectedIndex == 0)
            {
                cmd2.Parameters.AddWithValue("@artype", "");
            }
            else
            {
                cmd2.Parameters.AddWithValue("@artype", dd_ArticleType.SelectedValue);
            }
            cmd2.Parameters.AddWithValue("@reprint", txt_Reprints.Text.Trim());
            if (dd_watermark.SelectedIndex == 0)
            {
                cmd2.Parameters.AddWithValue("@watermark", "");
            }
            else
            {
                cmd2.Parameters.AddWithValue("@watermark", dd_watermark.SelectedValue);
            }
            cmd2.Parameters.AddWithValue("@autocorrem", txt_AuthorCR.Text.Trim());
            cmd2.Parameters.AddWithValue("@colfig", txt_ColorFigure.Text.Trim());
            cmd2.Parameters.AddWithValue("@splname", txt_SpecialIssueName.Text.Trim());
            cmd2.Parameters.AddWithValue("@hcopy", txr_HardCopy.Text.Trim());
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }
            SqlSt1 = "Insert into IOSJournalDetails values (";

            string strF = "";
            string strV = "";

            if (strJournalID.Length > 0)
            {
                strF = "SeqID, JournalID";
                strV = "'" + strJournalID + "','" + strJournalID + "'";
            }

            if (strReceipt.Length > 0)
            {
                strF = strF + ",DtReceipt";
                strV = strV + ",'" + strReceipt + "'";
            }


            if (strAuthors.Length > 0)
            {
                strF = strF + ",Authors";
                strV = strV + ",'" + strAuthors + "'";
            }

            if (strtoAuthors.Length > 0)
            {
                strF = strF + ",DtProofsSent_Author";
                strV = strV + ",'" + strtoAuthors + "'";
            }

            if (strfromAuthors.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Author";
                strV = strV + ",'" + strfromAuthors + "'";
            }

            if (strtoEditor.Length > 0)
            {
                strF = strF + ",DtProofsSent_Editor";
                strV = strV + ",'" + strtoEditor + "'";
            }
            if (strfromEditor.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Editor";
                strV = strV + ",'" + strfromEditor + "'";
            }
            if (strtoEditorial.Length > 0)
            {
                strF = strF + ",DtProofsSent_Editorial";
                strV = strV + ",'" + strtoEditorial + "'";
            }
            if (strfromEditorial.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Editorial";
                strV = strV + ",'" + strfromEditorial + "'";
            }
            if (strTypesetPages.Length > 0)
            {
                strF = strF + ",TSPages";
                strV = strV + ",'" + strTypesetPages + "'";
            }
            if (strSR.Length > 0)
            {
                strF = strF + ",AuthorReq";
                strV = strV + ",'" + strSR + "'";
            }
            if (strCompilationDt.Length > 0)
            {
                strF = strF + ",IssComDt";
                strV = strV + ",'" + strCompilationDt + "'";
            }
            if (strCRCDt.Length > 0)
            {
                strF = strF + ",CRCDeliveryDt";
                strV = strV + ",'" + strCRCDt + "'";
            }
            if (strPrintConfirmationDT.Length > 0)
            {
                strF = strF + ",PrintPDFDT";
                strV = strV + ",'" + strPrintConfirmationDT + "'";
            }
            if (strPrintDtIOS.Length > 0)
            {
                strF = strF + ",PrintPDF2IOSDt";
                strV = strV + ",'" + strPrintDtIOS + "'";
            }
            if (strThomsonRemarks.Length > 0)
            {
                strF = strF + ",TsRemarks";
                strV = strV + ",'" + strThomsonRemarks + "'";
            }

            if (strRDate.Length > 0)
            {
                strF = strF + ",ReminderDate";
                strV = strV + ",'" + strRDate + "'";
            }

            if (strOAO.Length > 0)
            {
                strF = strF + ",OAO";
                strV = strV + ",'" + strOAO + "'";
            }


            //// Edited By Kshitij
            //// Inserting newly added coloumns

            if (dd_ArticleType.SelectedIndex != 0)
            {
                strF = strF + ",ArticleType";
                strV += ",'" + dd_ArticleType.SelectedValue + "'";

            }
            if (!string.IsNullOrEmpty(txt_Reprints.Text))
            {
                strF+= ",Reprints";
                strV += ",'" + txt_Reprints.Text + "'";
            }
            if (dd_watermark.SelectedIndex != 0)
            {
                strF += ",WaterMark";
                strV += ",'" + dd_watermark.SelectedValue + "'";
            }
            if (!string.IsNullOrEmpty(txt_AuthorCR.Text))
            {

                strF += ",AutoCorrectionReminder";
                strV += ",'" + txt_AuthorCR.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txt_ColorFigure.Text))
            {
                strF = strF + ",ColorFigure";
                strV += ",'" + txt_ColorFigure.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txt_SpecialIssueName.Text.Trim()))
            {
                strF += ",SpecialIssueName";
                strV += ",'" + txt_SpecialIssueName.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txr_HardCopy.Text.Trim()))
            {
              strF += ",HardCopy";
              strV +=  ",'" + txr_HardCopy.Text.Trim() + "'";
            }

            //// done


            /*            if (strTo.Length > 0)
                        {
                            strF = strF + ",EmailTo";
                            strV = strV + ",'" + strTo + "'";
                        }

           
                        if (strTo.Length > 0)
                        {
                            strF = strF + ",EmailCC";
                            strV = strV + ",'" + strCC + "'";
                        }

                        if (strBCC.Length > 0)
                        {
                            strF = strF + ",EmailBCC";
                            strV = strV + ",'" + strBCC + "'";
                        }

                        */



            SqlSt1 = "Insert into IOSJournalDetails (" + strF + ") values (" + strV + ")";


            /*
       Select JID, AID, VolIss, title, DtReceipt, Authors, 
       TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  
       DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, 
       DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, 
       PrintPDF2IOSDt, TsRemarks from IOSJournal a, IOSJournalDetails b 
        where a.JournalID = b.JournalID";
        */

        }



        //objCon = new OleDbConnection(strConnectionString);
        //===================================================================
       // objCon = new MySqlConnection(strConnectionString);
        //objCon.Open();

        //objCmnd = new OleDbCommand(SqlSt, objCon);

        //objCmnd = new MySqlCommand(SqlSt, objCon);
        //int cnt = objCmnd.ExecuteNonQuery();
        //if (cnt == 0)
        //{
        //    // No rows update
        //}
        //else
        //{
        //    // Rows updated
        //}

        //objCmnd = new MySqlCommand(SqlSt1, objCon);
        //int cnt1 = objCmnd.ExecuteNonQuery();
        // int cnt1 = objCmnd.ExecuteNonQuery();
        //objCon.Close();
        //===================================================================
        AlertMessage("Updation task completed successfully");


    }

    public string CheckForExistence(string strJID, string strAID)
    {

        //objCon = new OleDbConnection(strConnectionString);
        //objCon = new MySqlConnection(strConnectionString);
        
        conn.Open();

        string SqlSt;

        SqlSt = "Select JournalID from IOSJournal where JID ='" + strJID + "' and AID='" + strAID + "'";

        //objCmnd = new OleDbCommand(SqlSt, objCon);
        SqlCommand cmd = new SqlCommand("chk_exist", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@jid", strJID);
        cmd.Parameters.AddWithValue("@aid", strAID);
        //objCmnd = new MySqlCommand(SqlSt, objCon);
        SqlDataAdapter da=new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //SqlDataReader Dr1=cmd.ExecuteReader();
        //Dr = objCmnd.ExecuteReader();

        string strJournalID1 = "";
        if(dt.Rows.Count>0)
        {
            strJournalID1 = dt.Rows[0][0].ToString();
        }

        conn.Close();
        return strJournalID1;
    }

    public string CheckForExistence_(string strJID, string strAID)
    {

        //objCon = new OleDbConnection(strConnectionString);

        objCon = new MySqlConnection(strConnectionString);
        objCon.Open();

        string SqlSt;

        SqlSt = "Select JournalID from  IOSJournal where JID ='" + strJID + "' and AID='" + strAID + "'";

        //objCmnd = new OleDbCommand(SqlSt, objCon);

        objCmnd = new MySqlCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();

        string strJournalID = "";
        while (Dr.Read())
        {
            strJournalID = Dr[0].ToString().Trim();
        }
        objCon.Close();
        return strJournalID;
    }



    public string GetUniqueValue()
    {

        //objCon = new OleDbConnection(strConnectionString);
        //objCon = new MySqlConnection(strConnectionString);
        conn.Open();

        string strDT = "";
        int y = System.DateTime.Today.Year;
        int m = System.DateTime.Today.Month;

        string strJID = "";
        string strAID = "";

        strDT = "J" + y.ToString().Trim();
        if (m < 10)
        {
            strDT = strDT + "0" + m.ToString().Trim();
        }
        else
        {
            strDT = strDT + m.ToString().Trim();
        }

        string SqlSt;
        SqlSt = "Select max(JournalID) from  IOSJournal where JournalID like'" + strDT + "%'";
        SqlCommand cmd = new SqlCommand("get_journalid", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@strdt", strDT);
        //objCmnd = new OleDbCommand(SqlSt, objCon);
        //objCmnd = new MySqlCommand(SqlSt, objCon);
        //Dr = objCmnd.ExecuteReader();
        //SqlDataReader Dr1 = cmd.ExecuteReader();
        SqlDataAdapter da=new SqlDataAdapter(cmd);
        DataTable dt=new DataTable();
        da.Fill(dt);
        string strJournalID = "";
        if(dt.Rows.Count>0)
        {
            strJournalID = dt.Rows[0][0].ToString();
        }
        conn.Close();

        if (strJournalID.Length == 12)
        {
            string s = strJournalID.Replace(strDT, "");
            //Check whether number or not

            double num = Convert.ToDouble(s);
            num++;

            if (num < 10)
            {
                strJournalID = strDT + "0000" + num.ToString().Trim();
            }
            else if (num < 100)
            {
                strJournalID = strDT + "000" + num.ToString().Trim();
            }
            else if (num < 1000)
            {
                strJournalID = strDT + "00" + num.ToString().Trim();
            }
            else if (num < 10000)
            {
                strJournalID = strDT + "0" + num.ToString().Trim();
            }
            else if (num < 100000)
            {
                strJournalID = strDT + num.ToString().Trim();
            }
            else
            {
                strJournalID = strDT + num.ToString().Trim();
            }
        }
        else
        {
            strJournalID = strDT + "00001";
        }
        return strJournalID;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ClearFields("All");
    }
    protected void txtfromEditorial_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtThomsonRemarks_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtVolIss_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmdView_Click(object sender, EventArgs e)
    {
        string strJID = cmbJID.Text.Trim();
        if (strJID.Equals("Select JID"))
        {
            strJID = "";
        }

        string strAID = txtAID.Text.Trim();

        if ((strJID.Length == 0))
        {
            if (strAID.Length == 0)
            {
                ClearFields("All");
                AlertMessage("Please select JID and AID!");
                return;
            }
            else
            {
                ClearFields("AID");
                AlertMessage("AID \"" + strAID + "\" is specified, but JID is missing, Please check!");
                return;
            }
        }
        else
        {
            if (strAID.Length == 0)
            {
                ClearFields("JID");
                AlertMessage("JID \"" + strJID + "\" is specified, but AID is missing, Please check!");
                return;
            }
            else
            {
                ClearFields("JID,AID");
            }

        }

        //objCon = new OleDbConnection(strConnectionString);       
        objCon = new MySqlConnection(strConnectionString);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ToString());
        string SqlSt = "";
        //SqlSt = "Select JID, AID, VolIss, title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, TsRemarks, JIDDesc, EmailTo, EmailCC, EmailBCC, ReminderDate, OAO from IOSJournal a, IOSJournalDetails b where a.JournalId = b.JournalID and JID = '" + strJID + "' and AID = '" + strAID + "'";
        //// edited by kshitij for mysql and new added fields
        SqlSt = "Select JID, AID, VolIss, title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, TsRemarks, JIDDesc, EmailTo, EmailCC, EmailBCC, ReminderDate, OAO, ArticleType, Reprints, WaterMark, AutoCorrectionReminder, ColorFigure, SpecialIssueName, HardCopy from IOSJournal a, IOSJournalDetails b where a.JournalId = b.JournalID and JID = '" + strJID + "' and AID = '" + strAID + "'";

        conn.Open();
        //objCmnd = new OleDbCommand(SqlSt, objCon);
        objCmnd = new MySqlCommand(SqlSt, objCon);
        SqlCommand cmd=new SqlCommand("get_details", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@jrid", strJID);
        cmd.Parameters.AddWithValue("@arid", strAID);
        //Dr = objCmnd.ExecuteReader();
        SqlDataReader Dr1=cmd.ExecuteReader();

        int RecCnt = 0;
        while (Dr1.Read())
        {
            RecCnt++;
            txtVolIss.Text = Dr1[2].ToString();
            txtTitle.Text = Dr1[3].ToString();
            txtReceiptDt.Text = Dr1[4].ToString();
            txtAuthors.Text = Dr1[5].ToString();
            txtTypesetPages.Text = Dr1[6].ToString();
            txttoAuthors.Text = Dr1[7].ToString();
            txtfromAuthors.Text = Dr1[8].ToString();
            txtSR.Text = Dr1[9].ToString();
            txttoEditor.Text = Dr1[10].ToString();
            txtfromEditor.Text = Dr1[11].ToString();
            txttoEditorial.Text = Dr1[12].ToString();
            txtfromEditorial.Text = Dr1[13].ToString();
            txtCompilationDt.Text = Dr1[14].ToString();
            txtCRCDt.Text = Dr1[15].ToString();
            txtPrintConfirmationDt.Text = Dr1[16].ToString();
            txtPrintDtIOS.Text = Dr1[17].ToString();
            txtThomsonRemarks.Text = Dr1[18].ToString();
            txtJIDDescription.Text = Dr1[19].ToString();
            txtTo.Text = Dr1[20].ToString();
            txtCC.Text = Dr1[21].ToString();
            txtBCC.Text = Dr1[22].ToString();
            txtReminderDate.Text = Dr1[23].ToString();
            txtOAO.Text = Dr1[24].ToString();

            //// edited by kshitij to add new fields
            if(!string.IsNullOrEmpty(Dr1[25].ToString().Trim()))
                dd_ArticleType.SelectedValue = Dr1[25].ToString();;
            if (!string.IsNullOrEmpty(Dr1[27].ToString().Trim()))
                dd_watermark.SelectedValue = Dr1[27].ToString().Trim();
            txt_Reprints.Text = Convert.ToString(Dr1[26]);
            txt_AuthorCR.Text = Convert.ToString(Dr1[28]);
            txt_ColorFigure.Text = Convert.ToString(Dr1[29]);
            txt_SpecialIssueName.Text = Convert.ToString(Dr1[30]);
            txr_HardCopy.Text = Convert.ToString(Dr1[31]);

        }

        conn.Close();
        if (RecCnt == 0)
        {

            ///////////


            SqlSt = "Select JID, AID, VolIss, title, JIDDesc, EmailTo, EmailCC, EmailBCC from IOSJournal a where JID = '" + strJID + "' and AID = '" + strAID + "'";
            conn.Open();
            //objCmnd = new OleDbCommand(SqlSt, objCon);
            cmd=new SqlCommand("get_journal", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@jid", strJID);
            cmd.Parameters.AddWithValue("@aid", strAID);
            //objCmnd = new MySqlCommand(SqlSt, objCon);
            Dr1 = cmd.ExecuteReader();
            //Dr = objCmnd.ExecuteReader();
            RecCnt = 0;
            while (Dr1.Read())
            {
                RecCnt++;
                txtVolIss.Text = Dr1[2].ToString();
                txtTitle.Text = Dr1[3].ToString();
                txtJIDDescription.Text = Dr1[4].ToString();
                txtTo.Text = Dr1[5].ToString();
                txtCC.Text = Dr1[6].ToString();
                txtBCC.Text = Dr1[7].ToString();
                //txtReceiptDt.Text = Dr[4].ToString();
                //txtAuthors.Text = Dr[5].ToString();
                //txtTypesetPages.Text = Dr[6].ToString();
                //txttoAuthors.Text = Dr[7].ToString();
                //txtfromAuthors.Text = Dr[8].ToString();
                //txtSR.Text = Dr[9].ToString();
                //txttoEditor.Text = Dr[10].ToString();
                //txtfromEditor.Text = Dr[11].ToString();
                //txttoEditorial.Text = Dr[12].ToString();
                //txtfromEditorial.Text = Dr[13].ToString();
                //txtCompilationDt.Text = Dr[14].ToString();
                //txtCRCDt.Text = Dr[15].ToString();
                //txtPrintConfirmationDt.Text = Dr[16].ToString();
                //lbl1.Text = Dr[17].ToString();
                //txtThomsonRemarks.Text = Dr[18].ToString();

            }

            conn.Close();

            if (RecCnt == 0)
            {
                ///////////
                AlertMessage("Information not found for JID: \"" + strJID + "\" AID: \"" + strAID + "\", Please check!");
                ClearFields("JID,AID");
            }

        }

    }

    public void AlertMessage(string strMsg)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
"alert('" + strMsg + "');" + System.Environment.NewLine +
"</script>");

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmdReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        ClearFields("All");
    }

    public void ClearFields(string ClearType)
    {
        if (ClearType.Equals("All"))
        {
            cmbJID.Text = "Select JID";
            txtAID.Text = "";
        }
        else
        {
            if (ClearType.IndexOf("JID") == -1)
            {
                cmbJID.Text = "Select JID";
            }
            if (ClearType.IndexOf("AID") == -1)
            {
                txtAID.Text = "";
            }
        }


        txtAuthors.Text = "";

        txtVolIss.Text = "";
        txtTitle.Text = "";
        txtReceiptDt.Text = "";
        txtAuthors.Text = "";
        txtTypesetPages.Text = "";
        txttoAuthors.Text = "";
        txtfromAuthors.Text = "";
        txtSR.Text = "";
        txttoEditor.Text = "";
        txtfromEditor.Text = "";
        txttoEditorial.Text = "";
        txtfromEditorial.Text = "";
        txtCompilationDt.Text = "";
        txtCRCDt.Text = "";
        txtPrintConfirmationDt.Text = "";
        txtPrintDtIOS.Text = "";
        txtThomsonRemarks.Text = "";
        txtJIDDescription.Text = "";
        txtTo.Text = "";
        txtCC.Text = "";
        txtBCC.Text = "";
        txtOAO.Text = "";

        //// Edited by kshitij for new fields
        dd_ArticleType.SelectedIndex = 0;
        dd_watermark.SelectedIndex = 0;
        txt_AuthorCR.Text = string.Empty;
        txt_ColorFigure.Text = string.Empty;
        txt_OtherArticleType.Text = string.Empty;
        txt_Reprints.Text = string.Empty;
        txt_SpecialIssueName.Text = string.Empty;
    }
    protected void cmdLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
    }
    protected void txtCRCDt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void dd_ArticleType_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (dd_ArticleType.SelectedIndex == 0)
        //{
        //    AlertMessage("Please select the article type");
        //}
        if (dd_ArticleType.SelectedItem.Text.Equals("others", StringComparison.InvariantCultureIgnoreCase))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "$('#myModal').modal('show');", true);
        }
    }
    protected void save_ArticleType_Click(object sender, EventArgs e)
    {
        //// show selected item in the dropdown temporarily
        dd_ArticleType.Items.Add(txt_OtherArticleType.Text.Trim().ToUpper());
        dd_ArticleType.SelectedIndex = dd_ArticleType.Items.Count - 1;

        txt_OtherArticleType.Text = string.Empty;
    }
}
