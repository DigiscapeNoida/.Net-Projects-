using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for UpdateDatabasecs
/// </summary>
public class UpdateDatabase
{
    private string connectionString="";
	public UpdateDatabase()
	{
		//
		// TODO: Add constructor logic here
		//
        connectionString = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
	}

    public int InsertJID(JIDDETAILS JIDobj)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd    = new SqlCommand("InsertJID", con);

        int Rowid = 0;

        cmd.CommandType   = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SNO", SqlDbType.Int));
        cmd.Parameters["@SNO"].Value = JIDobj.SNO;

        cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
        cmd.Parameters["@JID"].Value = JIDobj.JID;

        cmd.Parameters.Add(new SqlParameter("@JOURNALTITLE", SqlDbType.VarChar, 200));
        cmd.Parameters["@JOURNALTITLE"].Value = JIDobj.JOURNALTITLE;

        cmd.Parameters.Add(new SqlParameter("@PRODUCTIONSITE", SqlDbType.VarChar, 200));
        cmd.Parameters["@PRODUCTIONSITE"].Value = JIDobj.PRODUCTIONSITE;

        cmd.Parameters.Add(new SqlParameter("@CUSTOMER", SqlDbType.VarChar, 100));
        cmd.Parameters["@CUSTOMER"].Value = JIDobj.CUSTOMER;

        cmd.Parameters.Add(new SqlParameter("@ROWID", SqlDbType.Int, 4));
        cmd.Parameters["@ROWID"].Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                cmd.ExecuteNonQuery();
                Rowid=(int) cmd.Parameters["@ROWID"].Value;
                
            }
            else
            { 
            }
            
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            //throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
        return (Rowid);
    }

    public void DeleteAllRow()
    {
        string DltCmd = "delete from elsstylesheet";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(DltCmd, con);

        cmd.CommandType = CommandType.Text;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }
    public void DeleteJID(string JID)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd    = new SqlCommand("DeleteJID", con);

        cmd.CommandType   = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DeleteID", SqlDbType.VarChar, 100));
        cmd.Parameters["@JID"].Value = JID;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    public JIDDETAILS GetJID(string JID)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd    = new SqlCommand("GetJID", con);
        cmd.CommandType   = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
        cmd.Parameters["@JID"].Value = JID;

        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            // Get the first row.
            reader.Read();
            JIDDETAILS  JIDObj = new JIDDETAILS((int)reader["SNO"],
                (string)reader["JID"], (string)reader["JOURNALTITLE"],
                (string)reader["PRODUCTIONSITE"], (string)reader["CUSTOMER"]);
            reader.Close();
            return JIDObj;
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    public List<JIDDETAILS> GetJID()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("GetAllJID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        // Create a collection for all the employee records.
        List<JIDDETAILS> AllJID = new List<JIDDETAILS>();

        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                JIDDETAILS JIDObj = new JIDDETAILS((int)reader["SNO"],
                     (string)reader["JID"], (string)reader["JOURNALTITLE"],
                     (string)reader["PRODUCTIONSITE"], (string)reader["CUSTOMER"]);
                AllJID.Add(JIDObj);
            }
            reader.Close();

            return AllJID;
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

  
    public int CountJID()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd    = new SqlCommand("CountJID", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            con.Open();
            return (int)cmd.ExecuteScalar();
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

}
