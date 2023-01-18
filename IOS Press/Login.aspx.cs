using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

public partial class Login : System.Web.UI.Page
{
           
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
        if (!Page.IsPostBack)
        {
            Session.Abandon();
        }
       // HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
       // Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
       // HttpContext.Current.Response.Cache.SetNoServerCaching();
       // HttpContext.Current.Response.Cache.SetNoStore();
       // HttpContext.Current.Response.Cookies.Clear();
       // HttpContext.Current.Request.Cookies.Clear();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {  
        lblError.Visible = false;
        //string strConnectionString = "";        
        //strConnectionString = "Provider=SQLOLEDB;Server=application1;Database=IOS_Press;User ID=sa;password=tpms_tpms;Connection Timeout = 300;";
        //strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ToString());
        string pwd=Crypto.Encrypt(txtPassword.Text);
        SqlCommand cmd= new SqlCommand("get_user", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@uname", txtUName.Text);
        cmd.Parameters.AddWithValue("@pwd", Crypto.Encrypt(txtPassword.Text));
        SqlDataAdapter da=new SqlDataAdapter(cmd);
        DataTable dt=new DataTable();
        string red = "";
        try
        {
            conn.Open();
            da.Fill(dt);
            conn.Close();
        }
        catch(Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
        }
        if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][2].ToString() == "user")
                {
                    red = "Default.aspx";
                }
                if (dt.Rows[0][2].ToString() == "admin")
                {
                    red = "EntryForm.aspx";
                }
                if (red.Length > 0)
                {
                    Session["uid"] = dt.Rows[0][0].ToString();
                    Session["role"] = dt.Rows[0][2].ToString();
                    Response.Redirect(red);
                }
            }
            //else
            //{
            //    lblError.Text = "Data not found.";
            //    lblError.Visible = true;
            //}      
        //SqlSt = "Select * from Login_IOS where Username= BINARY '" + txtUName.Text + "' and password = BINARY '" + txtPassword.Text + "'";
        //objCon = new MySqlConnection(strConnectionString);
        //objCon.Open();
        //MySqlDataAdapter da = new MySqlDataAdapter(SqlSt, objCon);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //objCon.Close();       
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
