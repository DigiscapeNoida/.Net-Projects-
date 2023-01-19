using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LwwUploadingReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string isbn = DateTime.Now.Date.ToString();
            string stage = "";
            string[] asd = isbn.Split(' ');
            if (rtbnS200.Checked == true)
                stage = "S200";
            else
                stage = "S100";

            ShowReport(asd[0], stage, 0);
        }
        
        
        //gridviewDetails

    }
    public void lbtnSubmit_Click(object sender, EventArgs e)
    {
        String isbn = tbisbn.Text.ToString();
        String stage = "";
        if (rtbnS200.Checked == true)
            stage = "S200";
        else
            stage = "S100";
        ShowReport(isbn, stage, 1);
    }

    public void ShowReport(string isbn, string stage, int number)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlserverLWW"].ToString()))
        {
            string strSelectCmd = "";
            DataSet dsPerson = new DataSet();
            if (number == 1)
            {
                strSelectCmd = "select Jid,Aid,Stage,Status,MsgDate from MessageDetail  where Client = 'Lww' AND CONVERT(varchar, MsgDate, 23) = '" + isbn + "' AND Stage = '" + stage + "'";
            }
            else
            {
                strSelectCmd = "select Jid,Aid,Stage,Status,MsgDate from MessageDetail  where Client = 'Lww' AND CONVERT(varchar, MsgDate, 105) = '" + isbn + "' AND Stage = '" + stage + "'";
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