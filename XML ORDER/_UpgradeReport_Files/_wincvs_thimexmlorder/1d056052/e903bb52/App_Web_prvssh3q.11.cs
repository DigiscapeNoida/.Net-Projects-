#pragma checksum "D:\WinCVS\ThimeXMLORDER\LogData.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C7151F51992AF92CCB19CC3A07562B7F4889A56E"

#line 1 "D:\WinCVS\ThimeXMLORDER\LogData.aspx.cs"
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
public partial class LogData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showGridView();
    }
    
    private void showGridView()
    {
               try
                {
                    SqlDataSource dSource = null;

                    // configure the data source to get the data from the database
                    dSource = new SqlDataSource();
                    dSource.ConnectionString = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
                    dSource.DataSourceMode = SqlDataSourceMode.DataReader;
                    if (Session["LoginID"].ToString().Equals("12345"))
                    {
                        dSource.SelectCommand = "Select ItemTransaction.Tran_No, ItemTransaction.Item_id, ItemTransaction.Account, ItemTransaction.JID, ItemTransaction.Stage, ItemTransaction.DOI, ItemTransaction.LoginDate, Login.FirstName + ' ' + Login.LastName as UserName from ItemTransaction , "
                        + " Login where Login.LoginID = ItemTransaction.Username and LoginDate>(GETDATE()-1)";
                    }
                    else
                    {
                        dSource.SelectCommand = "Select ItemTransaction.Tran_No, ItemTransaction.Item_id, ItemTransaction.Account, ItemTransaction.JID, ItemTransaction.Stage, ItemTransaction.DOI, ItemTransaction.LoginDate, Login.FirstName + ' ' + Login.LastName as UserName from ItemTransaction , "
                        + " Login where Login.LoginID = ItemTransaction.Username AND ItemTransaction.Username='" + Session["LoginID"].ToString()+"'";
                    }

                    // set the source of the data for the gridview control and bind it
                    gvLog.DataSource = dSource;
                    gvLog.DataBind();
                    
                    if (gvLog.Rows.Count == 0)
                    {
                        lblmsg.Text = "Sorry! No record found for " + Session["LoginID"].ToString() + " user";
                        return;
                    }
                    

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    } // CH02QuickAndDirtyGridViewCS

}


#line default
#line hidden
