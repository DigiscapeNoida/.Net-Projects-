#pragma checksum "D:\WinCVS\ThimeXMLORDER\UpdateOrder.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A9F60C7440872171D1FE6CD6E870CD40418EBA1E"

#line 1 "D:\WinCVS\ThimeXMLORDER\UpdateOrder.aspx.cs"
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

public partial class UpdateOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillAccount();
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

    protected void FillAccount()
    {
        cmbAccount.Items.Clear();
        cmbAccount.Items.Add("JWUSA");
        cmbAccount.Items.Add("JWUK");
        cmbAccount.Items.Add("JWVCH");
    }
    protected void FillJID()
    {
        string JIDFile;
        if (cmbAccount.Text != "")
        {
            if (Session["LoginID"] != null)
            {
                if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58848")
                {
                    cmbJID.Items.Add("PPUL");
                    cmbJID.Items.Add("TEA");
                    cmbJID.Items.Add("JPS");
                    cmbJID.Items.Add("AJIM");
                    cmbJID.Items.Add("DEV");
                    cmbJID.Items.Add("NUR");
                }
                else if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58902")
                {
                    cmbJID.Items.Add("JCB");
                    cmbJID.Items.Add("MC");
                    cmbJID.Items.Add("PROS");
                    cmbJID.Items.Add("AJMB");
                }
                else if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58889")
                {
                    cmbJID.Items.Add("BEM");
                    cmbJID.Items.Add("JSO");
                    cmbJID.Items.Add("PPUL");
                    cmbJID.Items.Add("MRD");
                }
            }
        }
    }



}


#line default
#line hidden
