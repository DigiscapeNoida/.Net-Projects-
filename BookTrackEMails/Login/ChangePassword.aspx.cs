using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Xml;
using System.Net.Mail;

public partial class Login_ChangePassword : System.Web.UI.Page
{
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    GlbClasses objGlbCls = new GlbClasses();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtconnewpassword.Text != txtnewpassword.Text)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Password does not match.Please Enter again.";
        }
        else
        {
            try
            {
                string userid = txtuserid.Text;
                string newpass = txtnewpassword.Text;
                sql = "UPDATE login SET  password = '" + newpass + "' where password='" + txtoldpassword.Text + "' and userid='" + userid + "'";
                con = new SqlConnection(objGlbCls.objData.GetConnectionString());
                con.Open();
                cmd = new SqlCommand(sql, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Password has changed successfully.";
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Enter your correct old password";
                }

            }
            catch (Exception ex)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Enter Your correct password.";
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
}
