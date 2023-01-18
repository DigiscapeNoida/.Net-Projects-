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
//using CrystalDecisions.CrystalReports;
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
public partial class _Default : System.Web.UI.Page
{
    OleDbConnection objCon;
    OleDbCommand objCmnd;
    OleDbDataReader Dr;
    string strConnectionString = "";
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];
        //"Provider=SQLOLEDB;Server=application1;Database=IOS_Press;User ID=sa;password=tpms_tpms;Connection Timeout = 300;";
        if (IsPostBack == false)
        {
            GetJIDList();
        }
		
    }


    protected void GetJIDList()
    {
        
        objCon = new OleDbConnection(strConnectionString);

        dt = new DataTable();        
        string SqlSt = "";
        SqlSt = "Select distinct JID from IOSJournal order by JID";        
        objCon.Open();
        objCmnd = new OleDbCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();
        DDL.Items.Clear();
        DDL.Items.Add("ALL");
        while (Dr.Read())
        {
            
            string strJID = Dr[0].ToString();
            if (strJID.Length > 0)
                DDL.Items.Add(strJID);            
        }              
        objCon.Close();
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
            {
                //txtJID.Text = "";
                txtVIS.Text = "";

            }
        }
        objCon = new OleDbConnection(strConnectionString);
        
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
            dt.Columns.Add(new DataColumn("TsRemarks", typeof(string)));

            string SqlSt = "";
            SqlSt = "Select JID, AID, VolIss, title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, TsRemarks from IOSJournal a, IOSJournalDetails b where a.JournalID = b.JournalID ";
            if (paramVal.Trim().Length > 0)
            {
                SqlSt = SqlSt + paramVal;
            }
            SqlSt = SqlSt + " order by JID, AID";
            objCon.Open();
            objCmnd = new OleDbCommand(SqlSt, objCon);
            Dr = objCmnd.ExecuteReader();
            while (Dr.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = Dr[0].ToString();
                dr[1] = Dr[1].ToString();
                dr[2] = Dr[2].ToString();
                dr[3] = Dr[3].ToString();
                dr[4] = Dr[4].ToString().Replace("12:00:00 AM", "").Trim();
                dr[5] = Dr[5].ToString();
                dr[6] = Dr[6].ToString();
                dr[7] = Dr[7].ToString().Replace("12:00:00 AM", "").Trim();
                dr[8] = Dr[8].ToString().Replace("12:00:00 AM", "").Trim();
                dr[9] = Dr[9].ToString().Replace("12:00:00 AM", "").Trim();
                dr[10] = Dr[10].ToString().Replace("12:00:00 AM", "").Trim();
                dr[11] = Dr[11].ToString().Replace("12:00:00 AM", "").Trim();
                dr[12] = Dr[12].ToString().Replace("12:00:00 AM", "").Trim();
                dr[13] = Dr[13].ToString().Replace("12:00:00 AM", "").Trim();
                dr[14] = Dr[14].ToString().Replace("12:00:00 AM", "").Trim();
                dr[15] = Dr[15].ToString().Replace("12:00:00 AM", "").Trim();
                dr[16] = Dr[16].ToString().Replace("12:00:00 AM", "").Trim();
                dr[17] = Dr[17].ToString().Replace("12:00:00 AM", "").Trim();
                dr[18] = Dr[18].ToString();
                           
               
                dt.Rows.Add(dr);
            }
            MyDataGrid.DataSource =dt;
            if (Dr.HasRows)
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
            objCon.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        MyDataGrid.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
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
        Session["Type"] = "";
        Response.Redirect("Login.aspx");
    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void DDL_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
