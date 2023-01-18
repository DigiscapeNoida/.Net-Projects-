#pragma checksum "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B5547C74B199C048FD4B9B8E8F58C501E78A8C9D"

#line 1 "D:\WinCVS\ThimeXMLORDER\ViewLog.aspx.cs"
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
public partial class ViewLog : System.Web.UI.Page
{
    StreamWriter LogWriter;
    protected void Page_Load(object sender, EventArgs e)
    {
      //  ExpirePageCache();
      //  showGridView();
//        GetRecordsFromDataBase();

    }

    

/*    private void GetRecordsFromDataBase()
    {
        SqlConnection LogConnection = null;
        string strConnection;
        string strSQL;
        SqlCommand cmdFetch;
        SqlDataReader drLog;

        try
        {
            strSQL = "Select ItemTransaction.Tran_No, ItemTransaction.Item_id, ItemTransaction.Account, ItemTransaction.JID, ItemTransaction.Stage, ItemTransaction.DOI, ItemTransaction.LoginDate, Login.FirstName + ' ' + Login.LastName from ItemTransaction , Login where ItemTransaction.Username=Login.LoginID";
            strConnection = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            GlobalFunctions.WriteAppLog("Connection Defined...");
            LogConnection = new SqlConnection(strConnection);
            LogConnection.Open();
            GlobalFunctions.WriteAppLog("Connection Opened Sucessfully...");
            cmdFetch = new SqlCommand(strSQL, LogConnection);

            

            drLog = cmdFetch.ExecuteReader();
            GlobalFunctions.WriteAppLog("Creating Streamwriter for Report.html file...");
            if (Session["LoginID"] != null)
            {
                LogWriter = new StreamWriter(Server.MapPath(Session["LoginID"].ToString() + "Report.html"), false);
                LogWriter.WriteLine("<html><body bgcolor='silver'>");
                LogWriter.WriteLine("<table width='100%' bgcolor='#669999' cellpadding='0' border='1'>");
                LogWriter.WriteLine("<thead style='font-family:Verdana; font-weight:bold;color:Aqua;font-size:x-small'>");
                LogWriter.WriteLine("<tr><td>Transaction ID</td><td>Item ID</td><td>Account</td><td>JID</td><td>STAGE</td><td>DOI</td><td>LOGIN DATE</td><td>User Name</td></tr><tr></tr></thead>");
                LogWriter.WriteLine("<tbody style='font-family:Verdana; font-weight:bold;font-size:xx-small'>");

                GlobalFunctions.WriteAppLog("Reading DataReader for Report.html file...");




                while (drLog.Read())
                {
                    LogWriter.WriteLine("<tr>");
                    for (int i = 0; i <= 7; i++)
                    {
                        GlobalFunctions.WriteAppLog("Looping into datareader (" + i.ToString() + ")...");
                        LogWriter.WriteLine("<td>" + drLog[i].ToString() + "</td>");
                    }
                    LogWriter.WriteLine("</tr>");

                }
                //Writing SQL to insert record in Table
                LogWriter.WriteLine("</tbody></table></body></html>");
                LogWriter.Flush();
                LogWriter.Close();
                GlobalFunctions.WriteAppLog("Closing Report.html file...");
            }
            else
            {
                GlobalFunctions.WriteAppLog("Report.html could not be create as Session['LoginID'] was null.");
            }
        }
        catch (Exception ex)
        {
            GlobalFunctions.WriteAppLog("GetRecordsFromDatabase Function Exception: " + ex.Message);
        }
        finally
        {
            if (LogConnection != null)
            {
                LogConnection.Close();
                LogConnection.Dispose();
            }
        }

    }*/
/*
    protected bool WriteHTML(string txtHtml)
    {
        try
        {
            LogWriter = new StreamWriter(Server.MapPath("Report.html"), true);
            LogWriter.WriteLine("");
            LogWriter.Flush();
            LogWriter.Close();
            return true;
        }
        catch (Exception ex)
        {
            GlobalFunctions.WriteAppLog("WriteHTML Function Exception: " + ex.Message);
            return false;
        }
    }
*/
    protected void lblLogout_Click(object sender, EventArgs e)
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
}


#line default
#line hidden
