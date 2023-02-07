using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Resources;

public partial class MainMaster : System.Web.UI.MasterPage
{
    private ResourceManager rm;
    protected void Page_Load(object sender, EventArgs e)
    {
		//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        if (!IsPostBack)
        {
            CultureInfo ci;
            string lang = "fr-FR";
            Session["lang"] = "fr-FR";
            //if (Session[SESSION.LOGGED_Lang] != null)
            //{
            //    lang = Session[SESSION.LOGGED_Lang].ToString();
            //}
            //else
            //{
            //    //lang = "en-US";
            //    Session[SESSION.LOGGED_Lang] = "fr-FR";
            //    lang = "fr-FR";
            //}
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
            ci = Thread.CurrentThread.CurrentCulture;
            LoadData(ci);
        }
    }
    public void LoadData(CultureInfo ci)
    {
        Button lblUname = (Button)ContentPlaceHolder1.FindControl("btnLogin");
        LinkButton lblForgetPassword = (LinkButton)ContentPlaceHolder1.FindControl("lbkForgetPassword");
        Label lblloginheading = (Label)ContentPlaceHolder1.FindControl("lbllogin");
        TextBox txtLogin = (TextBox)ContentPlaceHolder1.FindControl("txtlogin");
        Label lblloginid = (Label)ContentPlaceHolder1.FindControl("lblloginid");
        Label lblpassword = (Label)ContentPlaceHolder1.FindControl("lblpassword");
       // Label lblcopyright1 = (Label)ContentPlaceHolder1.FindControl("lblcopyRight1");

        lblheading.Text = rm.GetString("Online_Production_Tracking", ci);
        lblheading2.Text = rm.GetString("System", ci);
        lblloginheading.Text = rm.GetString("Login", ci);
        lblUname.Text = rm.GetString("Login", ci);
        lblForgetPassword.Text = rm.GetString("Forget_Password", ci);
        txtCopyRight.Text = rm.GetString("Copyright", ci);
        lblcopyRight1.Text = rm.GetString("All_rights_reserved", ci);
        lblloginid.Text = rm.GetString("login_id", ci);
        lblpassword.Text = rm.GetString("Password", ci);
        Label mainresetpwd = (Label)ContentPlaceHolder1.FindControl("mainresetpwd");
        Label lblresetuid = (Label)ContentPlaceHolder1.FindControl("lblresetuid");
        Button btnresetpassword = (Button)ContentPlaceHolder1.FindControl("btnresetpassword");
       

        if (mainresetpwd != null)
        {
            mainresetpwd.Text = rm.GetString("PleaseInsertValidloginIDforResetpassword", ci);
        }
        if (lblresetuid != null)
        {
            lblresetuid.Text = rm.GetString("login_id", ci);
        }
        if (btnresetpassword != null)
        {
            btnresetpassword.Text = rm.GetString("submit", ci);
        }
       
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session["lang"] = "en-US";
        CultureInfo ci;
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        LoadData(Thread.CurrentThread.CurrentCulture);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Session["lang"] = "fr-FR";
        CultureInfo ci;
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        LoadData(Thread.CurrentThread.CurrentCulture);
    }
}
