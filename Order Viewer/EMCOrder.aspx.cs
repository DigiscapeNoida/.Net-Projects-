using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using OrderViewer;
public partial class AutoCompleteTextBox : System.Web.UI.Page
{
    OrderInfo OrderInfoOBJ = new OrderInfo();
    static string FMSPath="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            if (Session["userid"].ToString() != "tduser")
            {
                Response.Redirect("login.aspx");
            }
        }
        //FMSPath = @"\\fms\d$\FMS\centralized_server\EMC\JOURNAL\";
        FMSPath = ConfigurationManager.AppSettings["FMSPath"];
          OrderInfo.ServerPath = Server.MapPath("");
        //AutoCompleteAID.ClientID = "AutoCompleteAID";
        
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static List<string> GetJID(string prefixText, int count)
    {
        List<string> JIDS = new List<string>();
        string StartChar = prefixText.ToUpper();
        try
        {
            string[] FL = Directory.GetDirectories(FMSPath, StartChar + "*", SearchOption.TopDirectoryOnly);
           
            foreach (string JID in FL)
            {
                JIDS.Add(Path.GetFileName(JID));
            }
        }
        catch(Exception ex)
        { 
        }
        
        return JIDS;
    }

    [System.Web.Services.WebMethod]
    public static List<string> GetAID(string prefixText, int count, string contextKey)
    {
        string StartChar = prefixText.ToUpper();
        string JIDPath = FMSPath + contextKey;
        List<string> AIDS = new List<string>();
        if (Directory.Exists(JIDPath))
        {
            string[] FL = Directory.GetDirectories(JIDPath, StartChar + "*", SearchOption.TopDirectoryOnly);
            foreach (string AID in FL)
                AIDS.Add(Path.GetFileName(AID));
        }
        return AIDS;
    }
    protected void txtAID_TextChanged(object sender, EventArgs e)
    {
        if (!txtAID.Text.Equals("") && !txtJID.Text.Equals(""))
        {
            OrderInfoOBJ = new OrderInfo(FMSPath+ txtJID.Text +"\\"+ txtAID.Text + "\\orders");
            this.ddlStage.DataSource = OrderInfoOBJ.Stages;
            this.ddlStage.DataTextField = "StageName";
            this.ddlStage.DataValueField = "XMLPath";
            this.ddlStage.DataBind();
            this.ddlStage.Items.Insert(0, "-Select-");
        }

    }
    protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ddlStage.SelectedValue.Equals("-Select-"))
        {

            Xml1.DocumentContent = File.ReadAllText(ddlStage.SelectedValue).Replace("EMCOrder10.dtd", Server.MapPath("")+ "\\xslt\\EMCOrder10.dtd");
            //Xml1.DocumentSource = this.ddlStage.SelectedValue;
            Xml1.TransformSource = "~/xslt/EMCOrder10.xsl";
        }
    }

}
