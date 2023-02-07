using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TD.Data;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
    public Security()
    {
    }
    public string AuthenticateUser(System.Web.SessionState.HttpSessionState objSession, System.Web.HttpResponse objResponse, string email, string password, bool bPersist)
    {
        string nLoginID = "";
        string nLoginType = "";
        string nLoginusername = "";
        string nprodsite = "";
        // previous dashboard
        string npredashboard = "";
        // collection , redaction,email
        string nprecollection = "";
        string npreredaction = "";
        string npreemail = "";



        Login(email, password, ref nLoginID, ref nLoginType, ref nLoginusername, ref nprodsite, ref npredashboard, ref nprecollection, ref npreredaction, ref npreemail);

        if (nLoginID != "")
        {

            System.Web.Security.FormsAuthentication.SetAuthCookie(email, bPersist);
            objSession[SESSION.LOGGED_USER] = nLoginID.ToString();
            objSession[SESSION.LOGGED_ROLE] = nLoginType.ToString();
            objSession[SESSION.LOGGED_USER_NAME] = nLoginusername;
            objSession[SESSION.LOGGED_PRODSITE] = "";
           // objSession[SESSION.LOGGED_PRODSITE] = nprodsite;
            objSession[SESSION.LOGGED_Lang] = "fr-FR";
            objSession[SESSION.LOGGED_PREVIOUS_DASHBOARD] = npredashboard;
            objSession[SESSION.LOGGED_PREVIOUS_COLLECTION] = nprecollection;
            objSession[SESSION.LOGGED_PREVIOUS_REDACTION] = npreredaction;
            objSession[SESSION.LOGGED_PREVIOUS_RVEMAIL] = npreemail;
            return "yes";
        }
        else
        {
            return string.Empty;
        }

    }

    /// <summary>
    /// Verifies the login and password that were given
    /// </summary>
    /// <param name="email">the login</param>
    /// <param name="password">the password</param>
    /// <param name="nLoginID">returns the login id</param>
    /// <param name="nLoginType">returns the login type</param>
    public void Login(string email, string password, ref string nLoginID, ref string nLoginType, ref string nLoginusername, ref string nprodsite, ref string npredashboard, ref string nprecollection, ref string npreredaction, ref string npreemail)
    {
        SqlParameter[] paramList = new SqlParameter[2];
        SqlParameter paramLogin = new SqlParameter("uName", SqlDbType.VarChar, 50);
        paramLogin.Value = email;
        SqlParameter paramPassword = new SqlParameter("passw", SqlDbType.VarChar, 50);
        paramPassword.Value = password;
        paramList[0] = paramLogin;
        paramList[1] = paramPassword;



        SqlDataReader row = DataAccess.ExecuteReaderSP(SPNames.Login, paramList);
        if (row.Read())
        {
            // Get the login id and the login type
            nLoginID = row["userid"].ToString();//Convert.ToInt32(
            nLoginType = row["roleId"].ToString();
            nLoginusername = (row["FirstName"]) + " " + (row["LastName"]);
            nprodsite = (row["PRODID"]).ToString();
            npredashboard=(row["dashboard"]).ToString();
            nprecollection = (row["collections"]).ToString();
            npreredaction = (row["redaction"]).ToString();
            npreemail = (row["RVmailnoti"]).ToString();
        }
        else
        {
            nLoginID = "";
            nLoginType = "";
        }

        // this code for validation with the type of error

    }
}

public static class SESSION
{
    
    public const string LOGGED_USER = "LoginId";
    public const string DOWNLOAD_FILE = "FileToDownload";
    public const string TEMP = "tmp";
    public const string LOGGED_USER_NAME = "Name";
    public const string LOGGED_ROLE = "Role";
    public const string LOGGED_PRODSITE = "Prodsite";
    public const string LOGGED_Lang = "fr-FR";
    public const string LOGGED_PREVIOUS_DASHBOARD = "Dashboard";
    public const string LOGGED_PREVIOUS_COLLECTION = "Collection";
    public const string LOGGED_PREVIOUS_REDACTION = "Redaction";
    public const string LOGGED_PREVIOUS_RVEMAIL = "rvemail";
}
public static class USER_ROOT
{
    public static bool IsTaskAllowed(MenuTask menuTask, int roleId)
    {
        if (roleId == 1 && menuTask != MenuTask.Integration)
        {
            return true;
        }

        if (roleId == 4 && menuTask == MenuTask.Planner)
        {
            return true;

        }

        else
            return false;
    }

    public static bool IsGridTaskAllowed(GridTask grdTask, int roleId)
    {
        if (roleId == 1 || roleId == 0)
        {
            return true;
        }
        if (roleId == 2 && grdTask == GridTask.Delete)
        {
            return false;
        }
        else
            return false;

    }

}
public enum MenuTask
{
    Integration = 1,
    Planner = 2


}

public enum GridTask
{
    Delete = 1,
    Edit = 2
}