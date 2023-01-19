using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string emailid = Session["emailid"] as string;

        if (!string.IsNullOrEmpty(emailid))
        {
            lblshowemailid.Text = emailid;
        }
        else
        {
            lblshowemailid.Text = "Guest";
        }
     
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BTE_Templete.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Order_Viewer.aspx");
    }
}
