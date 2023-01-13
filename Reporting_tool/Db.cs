using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace report
{
    internal class Db
    {
        static Log log = Log.GetInstance();
        SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnString"]);
        SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["ConnString1"]);
        private Db()
        {
        }
        private static Db obj;
        private static readonly object mylockobject = new object();
        public static Db GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new Db();
                    }
                }
            }
            return obj;
        }
        public DataTable GetData(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(qry, conn);
                if (conn.State.ToString() == "Close")
                {
                    conn.Open();
                }
                dataAdapter.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                log.Generatelog("Error : " + ex.Message);
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }
            return dt;
        }
        public DataTable GetData1(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(qry, conn1);
                if (conn1.State.ToString() == "Close")
                {
                    conn1.Open();
                }
                dataAdapter.Fill(dt);
                conn1.Close();
            }
            catch (Exception ex)
            {
                log.Generatelog("Error : " + ex.Message);
                if (conn1.State.ToString() == "Open")
                {
                    conn1.Close();
                }
            }
            return dt;
        }
        public string AddUpdateData(string qry)
        {
            string ret = "0";
            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                ret = "1";
            }
            catch (Exception ex)
            {
                log.Generatelog("Error : " + ex.Message);
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }
            return ret;
        }
        public string AddUpdateData1(string qry)
        {
            string ret = "0";
            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn1);
                if (conn1.State.ToString() == "Closed")
                {
                    conn1.Open();
                }
                cmd.ExecuteNonQuery();
                conn1.Close();
                ret = "1";
            }
            catch (Exception ex)
            {
                log.Generatelog("Error : " + ex.Message);
                if (conn1.State.ToString() == "Open")
                {
                    conn1.Close();
                }
            }
            return ret;
        }
        public DataTable GetData(string sp, SqlParameter[] sqlParams)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = sp;
                myCommand.Parameters.Clear();
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
                log.Generatelog("Error found in fetching data : " + ex.Message);
            }
            return dt;
        }
        public DataTable GetData1(string sp, SqlParameter[] sqlParams)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn1;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = sp;
                myCommand.Parameters.Clear();
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                if (conn1.State.ToString() == "Closed")
                {
                    conn1.Open();
                }
                da.Fill(dt);
                conn1.Close();
            }
            catch (Exception ex)
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
                log.Generatelog("Error found in fetching data : " + ex.Message);
            }
            return dt;
        }
        public string InsertUpdateData(string sp, SqlParameter[] sqlParams)
        {
            string ret = "0";
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = conn;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = sp;
            myCommand.Parameters.Clear();
            for (int i = 0; i < sqlParams.Length; i++)
            {
                myCommand.Parameters.Add(sqlParams[i]);
            }
            try
            {
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                myCommand.ExecuteNonQuery();
                conn.Close();
                ret = "1";
            }
            catch (Exception ex)
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                    log.Generatelog("Error while inserting data : " + ex.Message);
                }
            }
            return ret;
        }
    }
}
