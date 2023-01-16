using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Text;

namespace DatabaseLayer
{
    class DataBase
    {
        SqlConnection SqlCon = new SqlConnection();
        SqlCommand    SqlCom = new SqlCommand();
        SqlDataReader SqlRed = null;

        string _ConnectionString = "";

        public string ConnectionString
        {
            set
            {
                _ConnectionString = value;
                if (!value.Equals(""))
                    SqlCon.ConnectionString = _ConnectionString;
            }
            get
            {
                return _ConnectionString;
            }
        }

        public bool   CheckDatabaseConnectivity()
        {
            bool Result = false;
            SqlCon.ConnectionString = ConnectionString;
            try
            {
                SqlCon.Open();
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                    Result = true;
                }
            }
            catch { }
            finally
            {
                SqlCon.Close();
                SqlCon.Dispose();
            }
            return Result;
        }
        private       DataBase()
        {
            // TODO: Add constructor logic here
        }
        public DataBase(string ConStr)
        {
            _ConnectionString = ConStr;
        }
        public SqlDataReader ExecuteReader(string SqlString)
        {
            if (!SqlString.Equals(""))
            {
                try
                {
                    SqlCon.ConnectionString = ConnectionString;
                    SqlCon.Open();
                    if (SqlCon.State == ConnectionState.Open)
                    {
                        SqlCom.CommandText = SqlString;
                        SqlCom.Connection = SqlCon;
                        SqlRed = SqlCom.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    else
                        return null;
                }
                catch (SqlException SqlEx)
                {
                    throw SqlEx;
                }
                catch
                {
                }
                finally
                {
                    //SqlCon.Close();
                    //SqlCon.Dispose();
                }
            }
            return SqlRed;
        }
        public object ExecuteScalar(string SqlString)
        {
            object Result = null;
            if (!SqlString.Equals(""))
            {
                try
                {
                    SqlCon.ConnectionString = ConnectionString;
                    SqlCon.Open();
                    if (SqlCon.State == ConnectionState.Open)
                    {
                        SqlCom.CommandText = SqlString;
                        SqlCom.Connection = SqlCon;
                        Result = SqlCom.ExecuteScalar();
                    }
                    else
                        return null;
                }
                catch (SqlException SqlEx)
                {
                    throw SqlEx;
                }
                finally
                {
                    SqlCon.Close();
                    SqlCon.Dispose();
                }
            }
            return Result;
        }
        public int ExecuteNonQuery(string SqlString)
        {
            int Result = 0;
            if (!SqlString.Equals(""))
            {
                try
                {
                    SqlCon.ConnectionString = ConnectionString;
                    SqlCon.Open();
                    if (SqlCon.State == ConnectionState.Open)
                    {
                        SqlCom.CommandText = SqlString;
                        SqlCom.Connection = SqlCon;
                        Result = SqlCom.ExecuteNonQuery();
                    }
                    else
                        return Result;
                }
                catch (SqlException SqlEx)
                {
                    throw SqlEx;
                }
                finally
                {
                    SqlCon.Close();
                    SqlCon.Dispose();
                }
            }
            return Result;
        }

    }
}
