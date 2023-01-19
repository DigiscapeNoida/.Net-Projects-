using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LNDeliveryUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        string articleid = tbArticleID.Text;
        string dttime = tbdate.Text;

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlserverPTS4LN"].ToString()))
        {
            DataSet dsPerson = new DataSet();

            //string strSelectCmd = "select Jid,Aid,TaskName,DownloadedOn,IntegratedOn from Lwwwip  where CONVERT(varchar, IntegratedON, 23) = '" + isbn + "'";
            string strSelectCmd = "update Article_Details set duedate = '" + dttime + "' where Articleid = '" + articleid +"'";

            SqlCommand da = new SqlCommand(strSelectCmd, conn);

            conn.Open();

            da.ExecuteNonQuery();

            //DataView dvPerson = dsPerson.Tables["Person"].DefaultView;

            
        }
    }
}