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

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cookies.Clear();
        HttpContext.Current.Request.Cookies.Clear();
        
    }
protected void Button1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Write("<script language='javascript'>");
        Response.Write("var backlen=history.length;");
        Response.Write("history.go(-backlen);");
        Response.Write("window.location.href='Logout1.aspx';");
        Response.Write("</script>");
        Response.Redirect("Logout1.aspx");
    }
}