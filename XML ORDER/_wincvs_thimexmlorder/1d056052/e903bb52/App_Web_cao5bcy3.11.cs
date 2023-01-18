#pragma checksum "D:\WinCVS\ThimeXMLORDER\Login.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FB49F6D95ED6C2B5541EAC327EB5A207BC8D242A"

#line 1 "D:\WinCVS\ThimeXMLORDER\Login.aspx.cs"
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
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
public partial class Login : System.Web.UI.Page
{
    Orders.JIDInfo JIDInfoOBj = new Orders.JIDInfo(true);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Session["XmlOrderPath"] = Request.QueryString.Get("orderpath");
            cmbCustomer.DataSource  = JIDInfoOBj.Client;
            cmbCustomer.DataBind();
            cmbCustomer.Items.Add("EMC");
        }
    }

    private void LogoutXML()
    {
        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
        Response.Write("<script language=javascript>wnd.close();</script>");
        Response.Write("<script language=javascript>window.open('Login.aspx','_parent',replace=true);</script>");
        Session.Abandon();
        FormsAuthentication.SignOut();

    }
    private void ExpirePageCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }
    public bool IsAlphaNumericSpecialChar(String strToCheck)
    {
        Regex objAlphaNumericPattern = new Regex("@[^a-zA-Z0-9~`!@#$%^&*?_]");
        return objAlphaNumericPattern.IsMatch(strToCheck);
    }

    protected void GetAccountName()
    {
            try
            {
                string JIDFile = "";

                JIDFile = "Accounts.ini";
                StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                cmbCustomer.Items.Clear();
                while (sr.Peek() > -1)
                {
                    cmbCustomer.Items.Add(sr.ReadLine().Trim());
                }
                sr.Close();


            }
            catch (Exception ex)
            {
                //GlobalFunctions.WriteAppLog("Error  in Login Page: " + ex.Message);
            }
    
    }


	protected void Login1_Authenticate(Object sender, AuthenticateEventArgs e)
		{
			// name of querystring parameter containing return URL
			const String QS_RETURN_URL = "ReturnURL";

			SqlConnection  sqlConn = null;
			SqlCommand     sqlCmd = null;
			SqlDataReader  dr = null;

			String strConnection = null;
			String strSQL = null;
			string nextPage = "";

            FormsAuthenticationTicket ticket = null;

            HttpCookie cookie = null;

            string encryptedStr = null;

			try
			{
                if (cmbCustomer.SelectedItem.ToString() == "")
                {
                    Login1.FailureText = "Please provide Account Name";
                    return;
                }
                //Check for alphanumeric password
                if (Regex.Match(Login1.Password, @"(?=.{5,})(?=(.*\d){1,})(?=(.*\W){1,})").Success)
                {
                }
                else
                {
                    Login1.FailureText = "Password should be combination of alphanumeric with special character (Minimum 5 and Maximum 14 characters). \nPlease change your password.\n\n(As per password policy).";
                    return;
                }

				// get the connection string from web.config and open a connection
				// to the database
				strConnection = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
                sqlConn = new SqlConnection(strConnection);
                sqlConn.Open();
	
				// check to see if the user exists in the database
                strSQL = "SELECT (FirstName + ' ' + LastName) AS UserName, Role " +
                "FROM Login Where LoginID='" + Login1.UserName + "' and Password='" + Login1.Password+"'";

				sqlCmd = new SqlCommand(strSQL, sqlConn);

				dr  = sqlCmd.ExecuteReader();

			    if (dr.Read( ))
			    {
                    // user credentials were found in the database so notify the system
                    // that the user is authenticated
                    // create an authentication ticket for the user with an expiration
                    // time of 30 minutes and placing the user's role in the userData
                    // property
                    ticket = new FormsAuthenticationTicket(1,
                                    (String)(dr["UserName"]),
                                    DateTime.Now,
                                    DateTime.Now.AddMinutes(30),
                                    Login1.RememberMeSet,
                                    (String)(dr["Role"]));

                    encryptedStr = FormsAuthentication.Encrypt(ticket);
                    Session["UserName"] = (String)(dr["UserName"]);
                    Session["Account"] = cmbCustomer.Text;
                    // add the encrypted authentication ticket in the cookies collection
                    // and if the cookie is to be persisted, set the expiration for
                    // 10 years from now. Otherwise do not set the expiration or the
                    // cookie will be created as a persistent cookie.

                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedStr);
                    if (Login1.RememberMeSet)
                    {
                        cookie.Expires = ticket.IssueDate.AddYears(10);
                    }

                    Response.Cookies.Add(cookie);


			    // get the next page for the user
			        if (Request.QueryString[QS_RETURN_URL] != null)
			        {
				        // user attempted to access a page without logging in so redirect
				        // them to their originally requested page
				        nextPage = Request.QueryString[QS_RETURN_URL];
			        }
			        else
			        {
				        // user came straight to the login page so just send them to the
				        // home page
//				        nextPage = "OrderCreator.aspx";
                        Session["LoginID"] = Login1.UserName;

                        if (Session["LoginID"].ToString() == "all")
                        {
                            nextPage = "OrderViewer.aspx";
                        }
                        else if (Session["LoginID"].ToString().ToLower() == "cbp")
                        {
                            nextPage = "CBP_OrderCreator.aspx";
                        }
                        else
                        {
                            //rogin code added on 29/03/10
                            if (Session["XmlOrderPath"] == null)
                            {
                                nextPage = "GenerateOrder.aspx";
                            }
                            else
                            {
                                nextPage = "OrderCreator.aspx";
                            }
                            //rogin code added on 29/03/10
                        }

                    }
			        //		which does not cause around trip to the client browser
			        //		and thus will not write the authentication cookie to the
			        //		client browser.
			        // Redirect user to the next page
			        // NOTE: This must be a Response.Redirect to write the cookie to
			        //		the user's browser. Do NOT change to Server.Transfer
			        //		which does not cause around trip to the client browser
			        //		and thus will not write the authentication cookie to the
			        //		client browser.
                    if (Request.QueryString[QS_RETURN_URL] == null)
                         nextPage=nextPage +   "?Client=" + cmbCustomer.Text;

    			    Response.Redirect(nextPage, true);
		        }
		        else
		        {
			        //user credentials do not exist in the database so output error
			        //message indicating the problem
			        Login1.FailureText = "Login ID or password is incorrect. " +
					        "Please check your credentials and try again.";
		        }
	        } // try

	        finally
	        {
		        // cleanup
		        if (dr != null)
		        {
			        dr.Close( );
		        }

		        if (sqlConn!= null)
		        {
			        sqlConn.Close();
		        }
	        } // finally

        } // Login1_Authenticate



}


#line default
#line hidden
