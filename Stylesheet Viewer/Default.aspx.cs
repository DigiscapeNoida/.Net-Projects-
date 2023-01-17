using System;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FormsAuthentication.GetAuthCookie(Login1.UserName, true);
        }
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

        //string userDataString = string.Concat("Puneet", "|", "DD");
        //HttpCookie authCookie = FormsAuthentication.GetAuthCookie(Login1.UserName, Login1.RememberMeSet);
        //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        ////
        //FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userDataString);
        //string p = Crypto.Encrypt(Login1.Password);
        if (Membership.ValidateUser(Login1.UserName, Crypto.Encrypt(Login1.Password)))
        {
            Session["userid"] = Login1.UserName;
            FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet);
            //HttpCookie usrCookie = new HttpCookie(Login1.UserName);
            //usrCookie.Domain = "thomson";
            //usrCookie.Secure = true;
            //usrCookie.Value  = Login1.Password;
            //Response.Cookies.Add(usrCookie);
            Response.Redirect("JSSView.aspx");
            //Login1.RememberMeSet=true;
            //login1.RememberMeSet = rememberMe.Checked   
            e.Authenticated = true;
        }
        else
        {
            e.Authenticated = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Membership.CreateUser("58161", "p@sssw0rd");
    }
}
