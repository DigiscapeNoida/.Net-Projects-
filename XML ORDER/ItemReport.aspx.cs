using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ItemReport : System.Web.UI.Page
{
    public string DateClicked = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetStageStatus();
        }
    }

    protected void GetStageStatus()
    {
        OleDbConnection tiscon = new OleDbConnection("Provider=MSDAORA;Data Source=tis;Password=tis2web;User ID=oldpts1");
        OleDbCommand tiscom = new OleDbCommand("SELECT DISTINCT STAGE from OLDPTS1.JW_PTS_TABLE", tiscon);
        OleDbCommand STATUSCommand = new OleDbCommand("SELECT DISTINCT STATUS from OLDPTS1.JW_PTS_TABLE", tiscon);
        //; SELECT DISTINCT STATUS from JW_PTS_TABLE
        OleDbDataReader STAGESTATUSreader = null;
        try
        {
            tiscon.Open();
            STAGESTATUSreader = tiscom.ExecuteReader();
            while (STAGESTATUSreader.Read())
            {
                cmbSTAGE.Items.Add(STAGESTATUSreader[0].ToString().ToUpper());
            }
            STAGESTATUSreader = STATUSCommand.ExecuteReader();
            while (STAGESTATUSreader.Read())
            {
                cmbSTATUS.Items.Add(STAGESTATUSreader[0].ToString().ToUpper());
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            STAGESTATUSreader.Close();
            tiscom.Dispose();
            tiscon.Dispose();
        }

    }
    protected void cmdFrmSend_Click(object sender, EventArgs e)
    {
      
        DateClicked = "FROMSEND";
        ViewState.Add("DateInfo", DateClicked);
        cal.Visible = true;

    }
    protected void cmdToSend_Click(object sender, EventArgs e)
    {
        DateClicked = "TOSEND";
        ViewState.Add("DateInfo", DateClicked);
        cal.Visible = true;
    }
    protected void cmdFrmDue_Click(object sender, EventArgs e)
    {
        DateClicked = "FROMDUE";
        ViewState.Add("DateInfo", DateClicked);
        cal.Visible = true;

    }
    protected void cmdToDue_Click(object sender, EventArgs e)
    {
        DateClicked = "TODUE";
        ViewState.Add("DateInfo", DateClicked);
        cal.Visible = true;

    }
    protected void cmdShow_Click(object sender, EventArgs e)
    {
      

        //GlobalFunctions.SQLSTR = "SELECT JID, AID, STAGE, CUSTOMER, TO_CHAR(CUSTSENDDATE,'DD-MON-YYYY') as SENT_DATE, TO_CHAR(CUSTDUEDATE,'DD-MON-YYYY') AS DUE_DATE, STATUS, TO_CHAR(CLOSEDATE,'DD-MON-YYYY') AS CLOSED_DATE FROM OLDPTS1.JW_PTS_TABLE WHERE STAGE = '" + cmbSTAGE.Text.ToUpper() + "' AND STATUS = '" + cmbSTATUS.Text.ToUpper() + "' ";
        //if (txtJID.Text != "")
        //{
        //    GlobalFunctions.SQLSTR+=" AND JID='"+txtJID.Text.ToUpper().Trim()+"'";
        //}
        //if (txtAID.Text != "")
        //{
        //    GlobalFunctions.SQLSTR+=" AND AID='"+txtAID.Text.ToUpper().Trim()+"'";
        //}
        //if (txtFrmSend.Text != "" || txtToSend.Text != "")
        //{
        //    if (txtFrmSend.Text != "" && txtToSend.Text == "")
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTSENDDATE,'DD/MM/YYYY') >'" + txtFrmSend.Text + "'";
        //    }
        //    else if (txtFrmSend.Text == "" && txtToSend.Text != "")
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTSENDDATE,'DD/MM/YYYY') >'" + txtFrmSend.Text + "'";
        //    }
        //    else
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTSENDDATE,'DD/MM/YYYY') BETWEEN '" + txtFrmSend.Text + "' AND '" + txtToSend.Text + "'";
        //    }
        //}
        //if (txtFromDue.Text != "" || txtToDue.Text != "")
        //{
        //    if (txtFromDue.Text != "" && txtToDue.Text == "")
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTDUEDATE,'DD/MM/YYYY') >'" + txtFromDue.Text + "'";
        //    }
        //    else if (txtFromDue.Text == "" && txtToSend.Text != "")
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTDUEDATE,'DD/MM/YYYY') < '" + txtToDue.Text + "'";
        //    }
        //    else
        //    {
        //        GlobalFunctions.SQLSTR += " AND TO_CHAR(CUSTDUEDATE,'DD/MM/YYYY') BETWEEN '" + txtFromDue.Text + "' AND '" + txtToDue.Text + "'";
        //    }
        //}
        //Response.Redirect("LoginReport.aspx");

    }

    protected void cal_SelectionChanged(object sender, EventArgs e)
    {
        string strDD, strMM, strYY;
        try
        {
            if (cal.SelectedDate.ToShortDateString().Length != 0)
            {
                string[] rec = cal.SelectedDate.ToShortDateString().Split('/');
                if (rec[0].ToString().Length == 1)
                {
                    strMM = "0" + rec[0].ToString();
                }
                else
                {
                    strMM = rec[0].ToString();
                }
                if (rec[1].ToString().Length == 1)
                {
                    strDD = "0" + rec[1].ToString();
                }
                else
                {
                    strDD = rec[1].ToString();
                }
                strYY = rec[2].ToString();
                if (ViewState["DateInfo"].ToString() == "FROMDUE")
                {
                    txtFromDue.Text = strDD + "/" + strMM + "/" + strYY;
                }
                else if (ViewState["DateInfo"].ToString() == "TODUE")
                {
                    txtToDue.Text = strDD + "/" + strMM + "/" + strYY;
                }
                else if (ViewState["DateInfo"].ToString() == "FROMSEND")
                {
                    txtFrmSend.Text = strDD + "/" + strMM + "/" + strYY;
                }
                else if (ViewState["DateInfo"].ToString() == "TOSEND")
                {
                    txtToSend.Text = strDD + "/" + strMM + "/" + strYY;
                }
            }
            cal.SelectedDates.Clear();
            cal.Visible = false;
            cal.VisibleDate = DateTime.Today;
        }
        catch (Exception ex)
        {
            this.RegisterClientScriptBlock("alert", "<script language=JavaScript>alert('"+ex.Message+"');</script>");
        }
    }

   
}
