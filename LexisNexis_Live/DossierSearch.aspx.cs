using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TD.Data;

public partial class DossierSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binduser();
            binddec();
            bindcategory();
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
        }
    }
    private void binduser()
    {
        ddlusersearch.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("roleid", Session[SESSION.LOGGED_ROLE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getuserbyrole, paramlist);
        ddlusersearch.DataSource = ds;
        ddlusersearch.DataTextField = "fullname";
        ddlusersearch.DataValueField = "userid";
        ddlusersearch.DataBind();
        ddlusersearch.Items.Insert(0, new ListItem("----------", "-1"));
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
        string user = ddlusersearch.SelectedValue.ToString();
        if (user == "-1")
        {
            user = "";
        }
       
        string reduction=ddlcategory.SelectedItem.Text;
        if (reduction == "----------")
        {
            reduction = "";
        }
        string declination=ddlDeclination.SelectedItem.Text;
        if (declination == "----------")
        {
            declination = "";
        }
        string stage = ddlstage.SelectedItem.Text;
        if (stage == "----------")
        {
            stage = "";
        }
        string SQLQuery = "select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and n.Active <> 'No'";// "select * from news where Active <> 'No' ";           
                        if(reduction !="")
                        {
                        SQLQuery = SQLQuery + " and category = '"+reduction+"'";
                        }
                        if(declination !="")
                        {
                        SQLQuery = SQLQuery +" and DECLINATION = '"+declination+"'";
                        }
                        if (stage != "")
                        {
                            SQLQuery = SQLQuery + " and STAGE = '" + stage + "'";
                        }
                        if (user != "")
                        {
                            SQLQuery = SQLQuery + " and n.userid = '" + user + "'";
                        }

                        if (txtid.Text.Trim() != "")
                        {
                            SQLQuery = SQLQuery + " and DID = '" + txtid.Text.Trim() + "'";
                        }
                        if(txtauthor.Text.Trim() !="")
                        {
                            SQLQuery = SQLQuery + " and Author = '" + txtauthor.Text.Trim() + "'";
                        }
                        if(txtfoldertitle.Text.Trim() !="")
                        {
                            SQLQuery = SQLQuery + " and CTITLE = '" + txtfoldertitle.Text.Trim() + "'";
                        }
                        if (txtfromduedate.Text.Trim() != "" && txttoduedate.Text.Trim() != "")
                        {
                            DateTime date = DateTime.ParseExact(txtfromduedate.Text.Trim(), "dd/MM/yyyy", null);
                            string Fromdate = date.ToString("MM/dd/yyyy");

                            date = DateTime.ParseExact(txttoduedate.Text.Trim(), "dd/MM/yyyy", null);
                            string Todate = date.ToString("MM/dd/yyyy");

                            SQLQuery = SQLQuery + " And DUEDATE between  '" + Fromdate + "'" + " and '" + Todate + "'";
                        }
                        if (txtlogindatefrom.Text.Trim() != "" && txtlogindateto.Text.Trim() != "")
                        {
                            DateTime date = DateTime.ParseExact(txtlogindatefrom.Text.Trim(), "dd/MM/yyyy", null);
                            string Loginfromdate = date.ToString("MM/dd/yyyy");

                            date = DateTime.ParseExact(txtlogindateto.Text.Trim(), "dd/MM/yyyy", null);
                            string Logintodate = date.ToString("MM/dd/yyyy");

                            SQLQuery = SQLQuery + " And INDATE between  '" + Loginfromdate + "'" + " and '" + Logintodate + "'";
                        }
                       
                        Session["searchqry"] = SQLQuery;
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
    public string FormatPostingDate(string str)
    {
        if (str != null && str != string.Empty)
        {
            DateTime postingDate = Convert.ToDateTime(str);
            return string.Format("{0:MM/dd/yyyy}", postingDate);
        }
        return string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
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
