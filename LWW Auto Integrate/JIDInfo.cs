using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
namespace LWWAutoIntegrate
{
    public class JIDInfo
    {
        StringCollection _Client = new StringCollection();
        static StringCollection _JID = new StringCollection();
        static StringCollection _Stage = new StringCollection();
        static StringCollection _WorkFlow = new StringCollection();



        public StringCollection Client
        {
            get { return _Client; }
        }

        public StringCollection JID
        {
            get { return _JID; }
        }

        public StringCollection Stage
        {
            get { return _Stage; }
        }

        public StringCollection WorkFlow
        {
            get { return _WorkFlow; }
        }

        public JIDInfo()
        {
        }

        public JIDInfo(bool client)
        {
            GetClient();
        }

        public StringCollection GetClient()
        {
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetClient", con);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Client.Add(reader["ClientName"].ToString());
                }
                reader.Close();

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
            return _Client;
        }
        public StringCollection GetJID(string Client)
        {
            _JID.Clear();
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetJID", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _JID.Add(reader["JID"].ToString());
                }
                reader.Close();

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
            return _JID;
        }
        public StringCollection GetStage(string Client)
        {
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetStage", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;
            try
            {
                con.Open();
                _Stage.Clear();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Stage.Add(reader["Stage"].ToString());
                }
                reader.Close();

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
            return _Stage;
        }

        public string GetFMSStage(string Client, string Stage)
        {
            string FMSStage = "";
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetFMSStage", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;

            cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
            cmd.Parameters["@Stage"].Value = Stage;

            try
            {
                con.Open();
                _Stage.Clear();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FMSStage = reader["FMSStage"].ToString();
                }
                reader.Close();

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
            return FMSStage;
        }

        public StringCollection GetWorkFlow(string Client, string JID, string Stage)
        {
            WorkFlow.Clear();
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetWorkFLow", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;

            cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
            cmd.Parameters["@JID"].Value = JID;

            cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
            cmd.Parameters["@Stage"].Value = Stage;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _WorkFlow.Add(reader["WorkflowName"].ToString());
                }
                reader.Close();

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
            return _WorkFlow;
        }

        public int GetTAT(string Client, string JID, string Stage)
        {
            int TAT = 0;
            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetTAT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 50));
            cmd.Parameters["@Client"].Value = Client;

            cmd.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 50));
            cmd.Parameters["@JID"].Value = JID;

            cmd.Parameters.Add(new SqlParameter("@Stage", SqlDbType.VarChar, 50));
            cmd.Parameters["@Stage"].Value = Stage;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TAT = (int)(reader["TAT"]);
                }
                reader.Close();

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
            return TAT;
        }



        public StringCollection GetTRACode()
        {
            _JID.Clear();
            _JID.Add("-Select-");

            SqlConnection con = new SqlConnection(ConfigDetails.XMLOrderConnectionString);
            SqlCommand cmd = new SqlCommand("GetEmcJID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _JID.Add(reader["TRACODE"].ToString());
                }
                reader.Close();

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
            return _JID;
        }

       

    }
}
