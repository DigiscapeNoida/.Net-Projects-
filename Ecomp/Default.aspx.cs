using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        
        
        //gridviewDetails

    }
    public void lbtnSubmit_Click(object sender, EventArgs e)
    {
        String isbn = tbisbn.Text.ToString();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLServer2005DBConnectionString"].ToString()))
        {
            DataSet dsPerson = new DataSet();

            string strSelectCmd = "select PII,Filename, TypeSignal, URL, Remarks, DownloadDate from SignalInfo where isbn = '" + isbn + "'";

            SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, conn);

            conn.Open();

            da.Fill(dsPerson, "Person");

            //DataView dvPerson = dsPerson.Tables["Person"].DefaultView;

            gridviewDetails.DataSource = dsPerson.Tables["Person"];
            gridviewDetails.DataBind();
        }
    }
}