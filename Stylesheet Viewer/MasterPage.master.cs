using System;
using System.Collections;
using System.Configuration;
using System.Data;

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
        if (Request.Url.AbsolutePath.Equals("/JSSViewer/JSSView.aspx"))
        {
            //JournalCodeDropDownList.Visible = true;
            //JournalCodeLabel.Visible = true;
            //JIDList XmlDiffObj = new JIDList();
            //JournalCodeDropDownList.DataSource = XmlDiffObj.JID;
            //JournalCodeDropDownList.DataBind();
        }
        else
        {
            //JournalCodeDropDownList.Visible = false;
            //JournalCodeLabel.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        
  }
}

