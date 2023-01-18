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
using MySql.Data.MySqlClient;
//using CrystalDecisions.CrystalReports;
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    /*
    OleDbConnection objCon;
    OleDbCommand objCmnd;
    OleDbDataReader Dr;
     * */
    private static string journalID = string.Empty;
    //MySqlConnection objCon;
    //MySqlCommand objCmnd;
    //MySqlDataReader Dr;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ToString());
    string strConnectionString = "";
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string sp = Session["uid"].ToString();
        if (Session["uid"] == null)
        {
            Response.Redirect("Logout.aspx");
        }
HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cookies.Clear();
        HttpContext.Current.Request.Cookies.Clear();
        strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];
        //"Provider=SQLOLEDB;Server=application1;Database=IOS_Press;User ID=sa;password=tpms_tpms;Connection Timeout = 300;";
        if (IsPostBack == false)
        {
            GetJIDList();
        }




    }


    protected void GetJIDList()
    {

        //objCon = new OleDbConnection(strConnectionString);       
        //objCon = new MySqlConnection(strConnectionString);
        string SqlSt = "";
        SqlSt = "Select distinct JID from IOSJournal order by JID";
        conn.Open();
        SqlCommand cmd = new SqlCommand("get_jid", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        SqlDataReader Dr1=cmd.ExecuteReader();
        //objCmnd = new OleDbCommand(SqlSt, objCon);
        //objCmnd = new MySqlCommand(SqlSt, objCon);
        //Dr = objCmnd.ExecuteReader();
        DDL.Items.Clear();
        DDL.Items.Add("ALL");
        while (Dr1.Read())
        {

            string strJID = Dr1[0].ToString();
            if (strJID.Length > 0)
                DDL.Items.Add(strJID);
        }
        conn.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["Type"] = "ArticleReport";
        Response.Redirect("Default2.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Type"] = "OverallReport";
        Response.Redirect("Default2.aspx");
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

        string paramVal = "";

        if (DDL.Text.Trim().Length > 0)
        {
            if (DDL.Text.Trim() != "ALL")
            {
                paramVal = " and " + " jid='" + DDL.Text.ToUpper().Trim() + "' ";

                if (ddl_manu.Text.Trim().Length > 0)
                {
                    if (ddl_manu.Text.Trim() != "ALL")
                    {
                        paramVal = paramVal + " and (LEN(VolIss) = 0)";
                        //// for mysql
                        //paramVal = paramVal + " and (LENGTH(VolIss) = 0)";
                        txtVIS.Text = "";
                    }
                }

                if (txtAID.Text.Trim().Length > 0)
                {
                    paramVal = paramVal + " and " + " aid='" + txtAID.Text.ToUpper().Trim() + "' ";
                }
                else if (txtVIS.Text.Trim().Length > 0)
                {
                    paramVal = paramVal + " and " + " voliss='" + txtVIS.Text.ToUpper().Trim() + "' ";
                }
            }
            else
            {                //txtJID.Text = "";
                txtVIS.Text = "";
                txtAID.Text = "";


                if (ddl_manu.Text.Trim().Length > 0)
                {
                    if (ddl_manu.Text.Trim() != "ALL")
                    {
                        paramVal = paramVal + " and (LEN(VolIss) = 0)";                     
                        //paramVal = paramVal + " and (LENGTH(VolIss) = 0)";
                    }
                }
            }
        }


        //objCon = new OleDbConnection(strConnectionString);
        //objCon = new MySqlConnection(strConnectionString);

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("JID", typeof(string)));
        dt.Columns.Add(new DataColumn("AID", typeof(string)));
        dt.Columns.Add(new DataColumn("VolIss", typeof(string)));
        dt.Columns.Add(new DataColumn("Title", typeof(string)));
        dt.Columns.Add(new DataColumn("DtReceipt", typeof(string)));
        dt.Columns.Add(new DataColumn("Authors", typeof(string)));
        dt.Columns.Add(new DataColumn("TSPages", typeof(string)));
        dt.Columns.Add(new DataColumn("DtProofsSent_Author", typeof(string)));
        dt.Columns.Add(new DataColumn("DtCorrectionsRec_Author", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorReq", typeof(string)));
        dt.Columns.Add(new DataColumn("DtProofsSent_Editor", typeof(string)));
        dt.Columns.Add(new DataColumn("DtCorrectionsRec_Editor", typeof(string)));
        dt.Columns.Add(new DataColumn("DtProofsSent_Editorial", typeof(string)));
        dt.Columns.Add(new DataColumn("DtCorrectionsRec_Editorial", typeof(string)));
        dt.Columns.Add(new DataColumn("IssComDt", typeof(string)));
        dt.Columns.Add(new DataColumn("CRCDeliveryDt", typeof(string)));
        dt.Columns.Add(new DataColumn("PrintPDFDT", typeof(string)));
        dt.Columns.Add(new DataColumn("PrintPDF2IOSDt", typeof(string)));
        dt.Columns.Add(new DataColumn("ReminderDate", typeof(string)));
        dt.Columns.Add(new DataColumn("TsRemarks", typeof(string)));
        dt.Columns.Add(new DataColumn("OAO", typeof(string)));
        //// Edited by kshitij for new added coloumns
        dt.Columns.Add(new DataColumn("ArticleType", typeof(string)));
        dt.Columns.Add(new DataColumn("Reprints", typeof(string)));
        dt.Columns.Add(new DataColumn("WaterMark", typeof(string)));
        dt.Columns.Add(new DataColumn("AutoCorrectionReminder", typeof(string)));
        dt.Columns.Add(new DataColumn("ColorFigure", typeof(string)));
        dt.Columns.Add(new DataColumn("SpecialIssueName", typeof(string)));
        dt.Columns.Add(new DataColumn("HardCopy", typeof(string)));

        //// JournalId
        dt.Columns.Add(new DataColumn("JournalID", typeof(string)));



        string SqlSt = "";
        //SqlSt = "Select JID, AID, VolIss, Title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, ReminderDate, TsRemarks, OAO from IOSJournal a, IOSJournalDetails b where a.JournalID = b.JournalID ";
        SqlSt = "Select JID, AID, VolIss, Title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, ReminderDate, TsRemarks, OAO, ArticleType, Reprints, WaterMark, AutoCorrectionReminder, ColorFigure, SpecialIssueName, HardCopy, a.JournalID from IOSJournal a, IOSJournalDetails b where a.JournalID = b.JournalID ";
        if (paramVal.Trim().Length > 0)
        {
            SqlSt = SqlSt + paramVal;
        }
        SqlSt = SqlSt + " and IsVisible is null order by JID, AID ";
        conn.Open();
        //objCmnd = new OleDbCommand(SqlSt, objCon);
        SqlCommand cmd;

        if (paramVal.Trim().Length > 0)
        {
            cmd = new SqlCommand(SqlSt, conn);
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@qry", paramVal + " and IsVisible is null order by JID, AID");
        }
        else
        {
            cmd = new SqlCommand("get_report1", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
        }
        
        //objCmnd = new MySqlCommand(SqlSt, objCon);
        SqlDataReader Dr1=cmd.ExecuteReader();
        //Dr = objCmnd.ExecuteReader();
        while (Dr1.Read())
        {
            DataRow dr = dt.NewRow();
            dr[0] = Dr1[0].ToString();
            dr[1] = Dr1[1].ToString();
            dr[2] = Dr1[2].ToString();
            dr[3] = Dr1[3].ToString();
            dr[4] = Dr1[4].ToString().Replace("12:00:00 AM", "").Trim();
            dr[5] = Dr1[5].ToString();
            dr[6] = Dr1[6].ToString();
            dr[7] = Dr1[7].ToString().Replace("12:00:00 AM", "").Trim();
            dr[8] = Dr1[8].ToString().Replace("12:00:00 AM", "").Trim();
            dr[9] = Dr1[9].ToString().Replace("12:00:00 AM", "").Trim();
            dr[10] = Dr1[10].ToString().Replace("12:00:00 AM", "").Trim();
            dr[11] = Dr1[11].ToString().Replace("12:00:00 AM", "").Trim();
            dr[12] = Dr1[12].ToString().Replace("12:00:00 AM", "").Trim();
            dr[13] = Dr1[13].ToString().Replace("12:00:00 AM", "").Trim();
            dr[14] = Dr1[14].ToString().Replace("12:00:00 AM", "").Trim();
            dr[15] = Dr1[15].ToString().Replace("12:00:00 AM", "").Trim();
            dr[16] = Dr1[16].ToString().Replace("12:00:00 AM", "").Trim();
            dr[17] = Dr1[17].ToString().Replace("12:00:00 AM", "").Trim();
            dr[18] = Dr1[18].ToString();
            dr[19] = Dr1[19].ToString();
            dr[20] = Dr1[20].ToString();

            //// edited by kshitij
            dr[21] = Dr1[21].ToString();
            dr[22] = Dr1[22].ToString();
            dr[23] = Dr1[23].ToString();
            dr[24] = Dr1[24].ToString();
            dr[25] = Dr1[25].ToString();
            dr[26] = Dr1[26].ToString();
            dr[27] = Dr1[27].ToString();

            ////JournalID
            dr[28] = Dr1[28].ToString();


            dt.Rows.Add(dr);
        }
        MyDataGrid.DataSource = dt;
        if (Dr1.HasRows)
        {
            //lblmsg.Visible = false;

            lblErrorMsg.Visible = false;
            MyDataGrid.Visible = true;
            MyDataGrid.DataBind();
        }
        else
        {
            lblErrorMsg.Text = "No matching information  found";
            lblErrorMsg.Visible = true;
            MyDataGrid.Visible = false;
        }
        conn.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        if (DDL.SelectedItem.Text.Equals("all", StringComparison.InvariantCultureIgnoreCase) && ddl_manu.SelectedItem.Text.Equals("all", StringComparison.InvariantCultureIgnoreCase))
        {
            //objCon = new MySqlConnection(strConnectionString);
            string SqlSt = "";
            //SqlSt = "Select JID, AID, VolIss, Title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, ReminderDate, TsRemarks, OAO from IOSJournal a, IOSJournalDetails b where a.JournalID = b.JournalID ";
            SqlSt = "Select JID, AID, VolIss, Title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, ReminderDate, TsRemarks, OAO, ArticleType, Reprints, WaterMark, AutoCorrectionReminder, ColorFigure, SpecialIssueName, HardCopy, a.JournalID from IOSJournal a, IOSJournalDetails b where a.JournalID = b.JournalID  and IsVisible is null order by JID, AID ";
            //objCon.Open();
            //objCmnd = new OleDbCommand(SqlSt, objCon);
            //objCmnd = new MySqlCommand(SqlSt, objCon);
            DataSet ds = new DataSet();
            //objCmnd.ExecuteNonQuery();
            //MySqlDataAdapter da = new MySqlDataAdapter(objCmnd);
            //da.Fill(ds);
            //// export dataset to excel
            ExportDataSetToExcel(ds, "Report.xls");

        }

        else
        {
            //// disableing databgrid for export

            foreach (DataGridItem row in MyDataGrid.Items)
            {

                row.Cells[29].Visible = false;
                // Set Enabled property of the fourth column in the DGV.

            }



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            MyDataGrid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Buffer = false;
            Response.End();
        }
    }

    protected void Grid_DeleteCommand(Object sender, DataGridCommandEventArgs e)
    {
        if (((Button)e.CommandSource).CommandName == "Delete")
        {

            journalID = Convert.ToString(e.Item.Cells[28].Text);
            //// Detele button
            Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "$('#myModal').modal('show');", true);
            //TableCell JIDCell = e.Item.Cells[0];
            //TableCell AIDCell = e.Item.Cells[1];
            //string JID = JIDCell.Text;
            //string AID = AIDCell.Text;

            //// Ask for usernam and password before deleting
            //// Delete from both the columns
            //// select from id iosjournal  and delete from iosjournaldetails
            //// update grid

            //// JournalID = Item.Cells[28].Text
        }
    }

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void MyDataGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtJID_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
           //Session["Type"] = "";
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void DDL_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btn_AdminLogin_Click(object sender, EventArgs e)
    {

        MySqlConnection objCon;
        MySqlCommand objCmnd;
        MySqlDataReader Dr;
        string strConnectionString = "";
        //strConnectionString = "Provider=SQLOLEDB;Server=application1;Database=IOS_Press;User ID=sa;password=tpms_tpms;Connection Timeout = 300;";
        strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];

        //// For mysql database
        //MySqlConnection connection = new MySqlConnection(strConnectionString);

        string SqlSt = "";
        string uname = "";
        string pwd = "";
        uname = txt_UserName.Text;
        pwd = txt_Password.Text;

        SqlSt = "Select * from Login_IOS where Username='" + uname + "' and password ='" + pwd + "'";
        //objCon = new OleDbConnection(strConnectionString);
        objCon = new MySqlConnection(strConnectionString);
        conn.Open();
        //objCmnd = new OleDbCommand(SqlSt, objCon);
        //objCmnd = new MySqlCommand(SqlSt, objCon);
        SqlCommand cmd2=new SqlCommand("get_user", conn);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Clear();
        cmd2.Parameters.AddWithValue("@uname", uname);
        cmd2.Parameters.AddWithValue("@pwd", Crypto.Encrypt(pwd));
        //SqlDataReader Dr1=cmd2.ExecuteReader();
        //Dr = objCmnd.ExecuteReader();
        DataTable dt = new DataTable();
        SqlDataAdapter da=new SqlDataAdapter(cmd2);
        da.Fill(dt);
        bool fnd = false;
        string strType = "";
        if (dt.Rows.Count > 0)
        {
            strType = dt.Rows[0][2].ToString();
            fnd = true;
        }
        //while (Dr1.Read())
        //{
        //    strType = Dr1[2].ToString().Trim();
        //    fnd = true;
        //}

        conn.Close();

        if (fnd == true)
        {
            if (strType.ToLower().Contains("admin") && !string.IsNullOrEmpty(journalID.Trim()))
            {
                string updateQuery = "Update IOSJournaldetails set IsVisible = '0' where JournalID = '" + journalID + "'";

                objCon = new MySqlConnection(strConnectionString);
                conn.Open();

                //objCmnd = new OleDbCommand(SqlSt, objCon);

                //objCmnd = new MySqlCommand(updateQuery, objCon);
                SqlCommand cmd1 = new SqlCommand("delete_record", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@jrid", journalID);
                int cnt = cmd1.ExecuteNonQuery();
                conn.Close();
                if (cnt == -1)
                {
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                    "alert(' Row Delete Successfully ');" + System.Environment.NewLine +
                    "</script>");
                }
                else
                {

                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                        "alert(' Couldn't delete row');" + System.Environment.NewLine +
                        "</script>");
                }

                Button1_Click1(sender, e);
                //// go delete the row
            }
            else
            {
                journalID = string.Empty;
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                    "alert(' You need to sign in as admin to delete a row  ');" + System.Environment.NewLine +
                    "</script>");
            }
        }
        else
        {

        }



    }

    public void ExportDataSetToExcel(DataSet ds, string filename)
    {
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");

        // create a string writer
        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.RenderControl(htw);
                response.Write(sw.ToString());
                response.End();
            }
        }
    }

}
