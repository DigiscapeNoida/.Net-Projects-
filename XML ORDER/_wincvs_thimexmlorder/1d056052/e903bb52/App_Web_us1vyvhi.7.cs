#pragma checksum "D:\WinCVS\ThimeXMLORDER\MasterPage.master.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "92B122C113D49ABBFDDC028F0D549142D84147B0"

#line 1 "D:\WinCVS\ThimeXMLORDER\MasterPage.master.cs"
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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
        Response.Write("<script language=javascript>wnd.close();</script>");
        Response.Write("<script language=javascript>window.open('GenerateOrder.aspx','_parent',replace=true);</script>");
        Session.Abandon();
        FormsAuthentication.SignOut();
    }
}


#line default
#line hidden
