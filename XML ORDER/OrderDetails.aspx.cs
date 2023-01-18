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

public partial class OrderDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//            ExpirePageCache();

    }
    

    protected void lblLogout_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
        Response.Write("<script language=javascript>wnd.close();</script>");
        Response.Write("<script language=javascript>window.open('Login.aspx','_parent',replace=true);</script>");
        Session.Abandon();
        FormsAuthentication.SignOut();
    }
    private void ExpirePageCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }

}
