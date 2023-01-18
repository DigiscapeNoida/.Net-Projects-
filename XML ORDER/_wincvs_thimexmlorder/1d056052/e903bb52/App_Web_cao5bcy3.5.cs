#pragma checksum "D:\WinCVS\ThimeXMLORDER\Default.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7510B7BEF11D907F871DF187306020C4F8EC76D0"

#line 1 "D:\WinCVS\ThimeXMLORDER\Default.aspx.cs"
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }
    protected void lblLogout_Click(object sender, EventArgs e)
    {

    }
}


#line default
#line hidden
