using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using ADODB;
public class DataUtility : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    bool logn = false;
    public DataUtility()
    {

    }
    public string GetConnectionString()
    {
        //return System.Configuration.ConfigurationManager.AppSettings["MyConnString"].ToString(); 
        return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }
    public void write(String msg)
    {
        StreamWriter sw = new StreamWriter("c:/testing.txt", true);
        sw.WriteLine(msg);
        sw.Close();
    }
    public bool ValidateUser(string uname, string pwd)
    {
        try
        {
            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select * from Login";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (uname == Dr["userid"].ToString() && pwd == Dr["Password"].ToString() && Dr["Role"].ToString() == "ADMIN")
                {
                    Session["role"] = Dr["Role"].ToString();
                    Session["location"] = Dr["Location"].ToString();
                    Session["emailid"] = Dr["Email_id"].ToString();
                    return true;
                }
                else if (uname == Dr["userid"].ToString() && pwd == Dr["Password"].ToString() && Dr["Role"].ToString() == "LOCAL")
                {
                    Session["role"] = Dr["Role"].ToString();
                    Session["location"] = Dr["Location"].ToString();
                    Session["emailid"] = Dr["Email_id"].ToString();
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.ToString());
            return false;
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }

        return false;
    }
    public bool CheckUserId(string TokenID)
    {
        try
        {

            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select * from Login";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (TokenID == Dr["userid"].ToString())
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        return false;
    }
    public bool InsertInfo(string userId, string userPwd, string userRole, string userLocation, string userEmailId)
    {
        try
        {
            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "insert into Login(userid,password,role,location,email_id) values('" + userId + "','" + userPwd + "','" + userRole + "','" + userLocation + "','" + userEmailId + "')";
            cmd = new SqlCommand(sqlstr, con);
            int res = cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        return false;
    }
    public bool getbookid(string BookId, string Stage)
    {
        try
        {
            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select bid from book_Info where bid='" + BookId + "' and Stage='" + Stage + "'";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            if (Dr.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            return false;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
    }
    public string CheckSearchIsbn(string SearchIsbn)
    {
        try
        {

            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select BID from book_info where isbn='" + SearchIsbn + "'";
            cmd = new SqlCommand(sqlstr, con);
            SqlDataAdapter olda = new SqlDataAdapter(sqlstr, con);
            DataSet ds = new DataSet();
            olda.Fill(ds, "table");

            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                //if (SearchIsbn.Equals(Dr[0].ToString()))
                //{
                return Dr[0].ToString();
                //}
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close();
            cmd.Dispose();
        }
        return "";
    }
    public bool Validate_Isbn(string InsertIsbn)
    {
        try
        {
            con = new SqlConnection(GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select * from book_info where isbn='" + InsertIsbn + "'";
            //cmd = new SqlCommand(sqlstr, con);
            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            //Dr = cmd.ExecuteReader();
            //while (Dr.Read())
            //{
            //    if (InsertIsbn == Dr["isbn"].ToString())
            //    {
            //        return true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            con.Close();
            //cmd.Dispose();
        }

        return false;
    }
}
