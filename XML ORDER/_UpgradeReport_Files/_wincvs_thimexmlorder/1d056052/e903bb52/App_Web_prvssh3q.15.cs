#pragma checksum "D:\WinCVS\ThimeXMLORDER\GenerateOrder.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6C3C756D70D75FF21D5AEB12BA6F1EF29FC3744B"

#line 1 "D:\WinCVS\ThimeXMLORDER\GenerateOrder.aspx.cs"
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

public partial class GenerateOrder : System.Web.UI.Page
{

    //
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        HyperLink OrderCreatorAnchor = (HyperLink)(this.Master.Master.FindControl("OrderCreatorAnchor"));
        

        if (OrderCreatorAnchor != null)
        {
            if (Request.QueryString["Client"] != null)
            {
                if (Request.QueryString["Client"].Equals("EMC"))
                    OrderCreatorAnchor.NavigateUrl = "EMCOrder.aspx";
                else
                    OrderCreatorAnchor.NavigateUrl = OrderCreatorAnchor.NavigateUrl + "?Client=" + Request.QueryString["Client"];
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblUser.Text = Session["UserName"].ToString();
        }
        else
        {
            lblUser.Text = "Session not initialized for UserName";
        }
    }
  
}


#line default
#line hidden
