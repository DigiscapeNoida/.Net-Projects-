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

public partial class Security : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            if (Session["userid"].ToString() != "tduser")
            {
                Response.Redirect("login.aspx");
            }
        }
    }
    protected void cmdlogin_Click(object sender, EventArgs e)
    {
        string adminuservalues = System.Configuration.ConfigurationManager.AppSettings["AdminUsers"].ToString();
        string EntrdUID = "," + txtuserid.Text.Trim(',') + ",";
        //if (adminuservalues.IndexOf(EntrdUID) > -1)
        if(adminuservalues.Contains(txtuserid.Text.Trim()))
        {
            Session["adminid"] = "tdadminuser";
            Response.Redirect("AdvancedMode.aspx");
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Invalid login information.....');" + System.Environment.NewLine +
            "</script>");

        }
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

  
}
