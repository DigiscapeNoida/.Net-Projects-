using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Template : System.Web.UI.MasterPage
{
    static string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            prevPage = Request.UrlReferrer.ToString();
        }
 
    }

    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect(prevPage);
    //}
    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
  
    //    Session.Remove("bid");
    //    Session.Remove("bkid");
    //    Session.Remove("BID");
    //    Session.Remove("stage");
    //    Session.Remove("JT1");
    //    Session.Remove("JT");
    //    Session.Remove("stg");
    //    Session.Remove("location");
    //    Session.Remove("info");
    //    Session.Remove("global");
    //    Session.Remove("role");
    //    Session.Remove("gridcount");
    //    Session.Remove("table");
    //    Session.Remove("msg");
    //    Session.Remove("path");
    //    Response.Redirect("~/Login/Login.aspx");
    //}
    protected void linkback_Click(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }
    protected void lnksignout_Click(object sender, EventArgs e)
    {
        Session.Remove("bid");
        Session.Remove("bkid");
        Session.Remove("BID");
        Session.Remove("stage");
        Session.Remove("JT1");
        Session.Remove("JT");
        Session.Remove("stg");
        Session.Remove("location");
        Session.Remove("info");
        Session.Remove("global");
        Session.Remove("role");
        Session.Remove("gridcount");
        Session.Remove("table");
        Session.Remove("msg");
        Session.Remove("path");
        Response.Redirect("~/Login/Login.aspx");
    }
}
