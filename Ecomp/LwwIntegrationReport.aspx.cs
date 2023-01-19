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
        if (!IsPostBack)
        {
            string isbn = DateTime.Now.Date.ToString();
            string[] asd = isbn.Split(' ');
            ShowReport(asd[0],0);
        }
        
        
        //gridviewDetails

    }
    public void lbtnSubmit_Click(object sender, EventArgs e)
    {
        String isbn = tbisbn.Text.ToString();
        ShowReport(isbn, 1);
    }

    public void ShowReport(string isbn, int number)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlserverLWW"].ToString()))
        {
            string strSelectCmd = "";
            DataSet dsPerson = new DataSet();
            if (number == 1)
            {
                strSelectCmd = "select Jid,Aid,TaskName,DownloadedOn,IntegratedOn from Lwwwip  where CONVERT(varchar, IntegratedON, 23) = '" + isbn + "'";
            }
            else {
                strSelectCmd = "select Jid,Aid,TaskName,DownloadedOn,IntegratedOn from Lwwwip  where CONVERT(varchar, IntegratedON, 105) = '" + isbn + "'";
            }
            SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, conn);

            conn.Open();

            da.Fill(dsPerson, "Person");
            conn.Close();

            //DataView dvPerson = dsPerson.Tables["Person"].DefaultView;

            gridviewDetails.DataSource = dsPerson.Tables["Person"];
            gridviewDetails.DataBind();
        }
    }
}