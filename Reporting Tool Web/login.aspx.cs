using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LN_Report
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            if (!Page.IsPostBack)
            {
                Session.Abandon();
                Session["uid"] = null;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblerror.Text = null;
            if (txtid.Text == "ln_report" && txtpwd.Text == "ln_report")
            {
                Session["uid"] = "leonard";
                Response.Redirect("report.aspx");
            }
            else
            {
                lblerror.Text = "User ID / Password not valid.";
            }
        }
    }
}