using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            bindjid();
            bindtat();

            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;

            System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(-3, -30, 0));
            // System.DateTime dt1111 = System.DateTime.Now;
            System.DateTime dt1;
            //dt = dt.AddDays(1); 

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

            lblJournalheureval.Text = dt1.ToString("dd-MMM-yyyy HH:mm:ss");
        }
    }
    private void bindjid()
    {
        ddlreview.Items.Clear();
        //SqlParameter[] paramlist = new SqlParameter[1];
        //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getinputcategory);
        ddlreview.DataSource = ds;
        ddlreview.DataTextField = "JID";
        ddlreview.DataValueField = "JID";
        ddlreview.DataBind();
        ddlreview.Items.Insert(0, new ListItem("----------", "-1"));
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
    protected void btnSend_Click(object sender, EventArgs e)
    {

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
        ddlreview.SelectedIndex = -1;
        txtarticletitle.Text = "";
        txtjournalauthor.Text = "";
        txtarticletype.Text = "";
        txtpubnum.Text = "";
        ddldelai.SelectedIndex = -1;
        txtsupnotification.Text = "";
        txtcomment.Text = "";
        lblmessage.Text = "";
    }
}