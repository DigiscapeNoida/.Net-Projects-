using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblinfomesg.Text = Session["info"].ToString();
        lblinfobookid.Text = Session["bkid"].ToString();
        //lblinfostage.Text = Session["stg"].ToString();
        if (Session["info"].ToString().IndexOf("allready exists") > -1)
        {
            lblinfomesg.ForeColor = System.Drawing.Color.GreenYellow;
        }
        else
        {
            lblinfomesg.ForeColor = System.Drawing.Color.GreenYellow;
        }
        if (Session["ln"] != null && Session["lp"] != null)
        {
            lblinformationln.Visible = true;
            lblinformationlp.Visible = true;
            lblinfoln.Visible = true;
            lblinfolp.Visible = true;
            lblinfoln.Text = Session["ln"].ToString();
            lblinfolp.Text = Session["lp"].ToString();
        }
        else
        {
            lblinfoln.Visible = false;
            lblinfolp.Visible = false;
            lblinformationln.Visible = false;
            lblinformationlp.Visible = false;
        }
    }
    protected void lnkeditorder_Click(object sender, EventArgs e)
    {
        Session.Remove("ln");
        Session.Remove("lp");
        Session.Remove("msg");
        Response.Redirect("Order_Viewer.aspx");
    }
    protected void lnkvieworder_Click(object sender, EventArgs e)
    {
        Session.Remove("ln");
        Session.Remove("lp");
        Session.Remove("msg");
        Response.Redirect("XmlOrderForm.aspx");
    }
    protected void lnkcacel_Click(object sender, EventArgs e)
    {
        Session.Remove("info");
        Session.Remove("bkid");
        Session.Remove("stg");
        Session.Remove("JT");
        Session.Remove("JT1");
        Session.Remove("ln");
        Session.Remove("lp");
        Session.Remove("msg");
        Response.Redirect("Action.aspx");
    }
}
