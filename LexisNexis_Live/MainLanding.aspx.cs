using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainLanding : System.Web.UI.Page
{
    private ResourceManager rm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[SESSION.LOGGED_USER] == null)
        {
            Response.Redirect("Default.aspx");
        }
        lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
        /*
        CultureInfo ci;
        string lang = "";
        if (Session["lang"] != null)
        {
            lang = Session["lang"].ToString();
        }
        else
        {
            //lang = "en-US";
            Session["lang"] = "fr-FR";
            lang = "fr-FR";
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        LoadData(ci);
        */
    }
    public void LoadData(CultureInfo ci)
    {

       
        lnkdosier.Text = rm.GetString("Dossiers", ci);
        lnkencyclo.Text = rm.GetString("Encyclopedia", ci);
        lnkfiche.Text = rm.GetString("Fiches", ci);
        lnkjournal.Text = rm.GetString("Journals", ci);
    }
    protected void lnkdosier_Click(object sender, EventArgs e)
    {
        Session[SESSION.LOGGED_PRODSITE]= "DS";
        Response.Redirect("DossierLanding1.aspx");
       
    }
    protected void lnkencyclo_Click(object sender, EventArgs e)
    {
        Session[SESSION.LOGGED_PRODSITE] = "EC";
        Response.Redirect("EncyclopediasLanding.aspx");
    }
    protected void lnkfiche_Click(object sender, EventArgs e)
    {
      //  Session[SESSION.LOGGED_PRODSITE] = "FS";
      //  Response.Redirect("FicheLanding.aspx");
    }
    protected void lnkjournal_Click(object sender, EventArgs e)
    {
      //  Session[SESSION.LOGGED_PRODSITE] = "RV";
     //   Response.Redirect("JournalLanding.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session[SESSION.LOGGED_USER] = null;
        Session[SESSION.LOGGED_PRODSITE] = null;
        Response.Redirect("Default.aspx");
    }
}