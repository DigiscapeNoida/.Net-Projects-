using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        
        //if (Membership.ValidateUser(Login1.UserName, Login1.Password))
        if (Login1.UserName.Equals("all")&& Login1.Password.Equals("p@ssw0rd"))
        {
            //Session["userid"] = Login1.UserName;
            Session["userid"] = "tduser";
            //FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet);
            Response.Redirect("default.aspx");
            e.Authenticated = true;
            
        }
        else
        {
            e.Authenticated = false;
        }
    }
}
