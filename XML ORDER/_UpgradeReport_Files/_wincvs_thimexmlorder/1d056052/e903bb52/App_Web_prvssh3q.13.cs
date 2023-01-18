#pragma checksum "D:\WinCVS\ThimeXMLORDER\LoginReport.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0BD62C3AF6D7B1E64F4F6B87A79D457C6C006E01"

#line 1 "D:\WinCVS\ThimeXMLORDER\LoginReport.aspx.cs"
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class LoginReport : System.Web.UI.Page
{
    //OleDbConnection TISCon = null;
    //OleDbCommand TISCom = null;
    DataSet tisds;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ShowData();
    }
    protected void Excel_Click(object sender, EventArgs e)
    {
   

        //Export the GridView to Excel

        PrepareGridViewForExport(GridView1);

        ExportGridView();

    }

 

    private void ExportGridView()

    {

        string attachment = "attachment; filename=Report.xls";
        string style = @"<style> .text { mso-number-format:\@; } </style> "; 


        Response.ClearContent();

        Response.AddHeader("content-disposition", attachment);

        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.RenderControl(htw);
        Response.Write(style); 
        Response.Write(sw.ToString());

        Response.End();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[1].Attributes.Add("class", "text");

        }

    }


    public override void VerifyRenderingInServerForm(Control control)

    {

    }

 

    private void PrepareGridViewForExport(Control gv)

    {

        LinkButton lb = new LinkButton();

        Literal l = new Literal();

        string name = String.Empty;

        for (int i = 0; i < gv.Controls.Count; i++)

        {

            if (gv.Controls[i].GetType() == typeof(LinkButton))

            {

                l.Text = (gv.Controls[i] as LinkButton).Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            else if (gv.Controls[i].GetType() == typeof(DropDownList))

            {

                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            else if (gv.Controls[i].GetType() == typeof(CheckBox))

            {

                l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";

                gv.Controls.Remove(gv.Controls[i]);

                gv.Controls.AddAt(i, l);

            }

            if (gv.Controls[i].HasControls())

            {

                PrepareGridViewForExport(gv.Controls[i]);

            }

        }

    }


////    protected void ShowData()
////    {
//////        OleDbConnection tiscon = new OleDbConnection("Provider=MSDAORA;Data Source=tis;Password=tis2web;User ID=oldpts1");
////        OleDbConnection tiscon = new OleDbConnection("Provider=MSDAORA;Data Source=tis;Password=ptsuser;User ID=ptsuser");
////        OleDbCommand com = new OleDbCommand();
////        OleDbDataAdapter tisda = new OleDbDataAdapter(GlobalFunctions.SQLSTR, tiscon);
////        tisds = new DataSet();
////        tisda.Fill(tisds);
////        GridView1.DataSource = tisds;
////        GridView1.DataBind();
////        tiscon.Close();
////        tiscon = null;
////    }

//    protected void Show_Click(object sender, EventArgs e)
//    {
//        OleDbConnection tiscon = new OleDbConnection("Provider=MSDAORA;Data Source=tis;Password=tis2web;User ID=oldpts1");
////        OleDbDataAdapter tisda = new OleDbDataAdapter("SELECT JID, AID, STAGE, CUSTOMER, TO_CHAR(CUSTSENDDATE,'DD-MON-YYYY') as SENT_DATE, TO_CHAR(CUSTDUEDATE,'DD-MON-YYYY') AS DUE_DATE, STATUS, TO_CHAR(CLOSEDATE,'DD-MON-YYYY') AS CLOSED_DATE FROM JW_PTS_TABLE WHERE STAGE = '" + cmbStage.Text.ToUpper() + "' AND STATUS = '" + cmbStatus.Text.ToUpper() + "' ORDER BY JW_PTS_SERIAL", tiscon);
//        OleDbDataAdapter tisda = new OleDbDataAdapter(GlobalFunctions.SQLSTR, tiscon);
//        tisds = new DataSet();
//        tisda.Fill(tisds);
//        GridView1.DataSource = tisds;
//        GridView1.DataBind();
//        tiscon.Close();
//        tiscon = null;
        
//    }

  
    
    
    



    protected void GridView1_Sorted(object sender, EventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
//        OleDbConnection tiscon = new OleDbConnection("Provider=MSDAORA;Data Source=tis;Password=tis2web;User ID=oldpts1");
////        OleDbDataAdapter tisda = new OleDbDataAdapter("SELECT JID, AID, STAGE, CUSTOMER, TO_CHAR(CUSTSENDDATE,'DD-MON-YYYY') as SENT_DATE, TO_CHAR(CUSTDUEDATE,'DD-MON-YYYY') AS DUE_DATE, STATUS, TO_CHAR(CLOSEDATE,'DD-MON-YYYY') AS CLOSED_DATE FROM JW_PTS_TABLE WHERE STAGE = '" + cmbStage.Text.ToUpper() + "' AND STATUS = '" + cmbStatus.Text.ToUpper() + "' ORDER BY JW_PTS_SERIAL", tiscon);
//        OleDbDataAdapter tisda = new OleDbDataAdapter(GlobalFunctions.SQLSTR, tiscon);
//        tisds = new DataSet();
//        tisda.Fill(tisds);
//        DataView dv = new DataView(tisds.Tables[0]);

//        dv.Sort =e.SortExpression;
        

//        GridView1.DataSource = dv;
//        GridView1.DataBind();
    }

   

}


#line default
#line hidden
