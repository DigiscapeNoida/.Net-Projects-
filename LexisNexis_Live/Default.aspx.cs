using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TD.Data;
using System.IO;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Web.Services;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/Default.aspx?p=0");
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["P"] == null)
            {
                lbl_error.Visible = false;
            }
            else
            {
                lbl_error.Visible = true;
            }
            
        }
//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5
//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // .NET 4.0
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lbl_error.Visible = false;
        //Errlbl.Visible = false;

        //else
        //{
        //    Session[SESSION.LOGGED_USER] = "LAD";
        //    Session[SESSION.LOGGED_ROLE] = "";
        //    Session[SESSION.LOGGED_PRODSITE] = "";
        //    Response.Redirect("AdminUserCreate.aspx");
        //}
       
            if (txtCaptcha.Text.Trim().Length > 0 && txtlogin.Text.Trim().Length > 0 && txtpassword.Text.Trim().Length > 0)
            {
                if (captchaBox.Validate(txtCaptcha.Text) == true)
                {
                    string redirect = "";
                    Security sec = new Security();
                    if ((redirect = sec.AuthenticateUser(this.Session, this.Response, txtlogin.Text.Trim(), txtpassword.Text.Trim(), false)) != string.Empty)
                    {
                        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
                        {
                            Response.Redirect("AdminUserCreate.aspx");
                            // Response.Redirect("LNAdmin.aspx");
                            // Response.Redirect("LNDossierLanding.aspx");

                        }
                        else if (Session[SESSION.LOGGED_ROLE].ToString() == "TAD")
                        {
                            Response.Redirect("AdminTDUserCreate.aspx");
                        }
                        else
                        {
                            if (Session[SESSION.LOGGED_PREVIOUS_DASHBOARD] == "" || Session[SESSION.LOGGED_PREVIOUS_DASHBOARD] == null)
                            {
                                Session[SESSION.LOGGED_PRODSITE] = "EC";
                                Response.Redirect("EncyclopediasLanding.aspx");
                            }
                            else if (Session[SESSION.LOGGED_PREVIOUS_DASHBOARD].ToString() == "DS")
                            {
                                Session[SESSION.LOGGED_PRODSITE] = "DS";
                                Response.Redirect("DossierLanding1.aspx");
                            }
                            else if (Session[SESSION.LOGGED_PREVIOUS_DASHBOARD].ToString() == "EC")
                            {
                                Session[SESSION.LOGGED_PRODSITE] = "EC";
                                Response.Redirect("EncyclopediasLanding.aspx");
                            }
                            else if (Session[SESSION.LOGGED_PREVIOUS_DASHBOARD].ToString() == "FS")
                            {
                                Session[SESSION.LOGGED_PRODSITE] = "FS";
                                Response.Redirect("FicheLanding.aspx");
                            }
                            else if (Session[SESSION.LOGGED_PREVIOUS_DASHBOARD].ToString() == "RV")
                            {
                                Session[SESSION.LOGGED_PRODSITE] = "RV";
                                Response.Redirect("JournalLanding.aspx");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx?p=0");
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?p=0");
                }
            }
            else
            {
                Response.Redirect("Default.aspx?p=0");
            }       
    }


    [WebMethod(EnableSession = true)]
    public static string RegisterUser(string userid)
    {

        string allowedChars = "";

        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

        allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 8; i++)
        {

            temp = arr[rand.Next(0, arr.Length)];

            passwordString += temp;

        }

       string newpass = passwordString;




       //int uid = Convert.ToInt16(userid);
       SqlParameter[] paramList = new SqlParameter[2];
       paramList[0] = new SqlParameter("uiid", userid);// SqlDbType.VarChar, 50);//, userid
       paramList[1] = new SqlParameter("newpass", newpass);// SqlDbType.VarChar, 50);//, newpassword
       //paramList[2].Value = newpassword;
       int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.resetpassword, paramList);
       if (rowAffected > 0)
       {
           string strFile = "<html><body>Cher Utilisateur,<br><br>Votre mot de passe a &eacute;t&eacute; chang&eacute;. Votre nouveau mot de page est : [PASSWORD]  <br><br>"+
               "Veuillez loguer avec ce nouveau mot de passe.<br><br>Merci de ne pas r&eacute;pondre &agrave; ce mail envoy&eacute; automatiquement.<br><br>L&eacute;onard</body></html>";



           string strBody = strFile;
               strBody = strBody.Replace("[PASSWORD]", newpass);

               string strSubject = "Léonard – Mot de passe oublié";

               Common cmn = new Common();
               Common.SendEmail(userid, "", strSubject, strBody);
               // Utility.NumberToEnglish.email();


               return "Mot de passe envoyé à l'adresse mail enregistrée";//        Password Change Successfully!
       }
       else
       {
           return "Nom d'utilisateur n'existe pas";// email id not found
       }
        // for send email

       


    }
}