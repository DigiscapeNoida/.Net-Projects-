using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;


namespace TD.Data
{
    public sealed class DataAccess
    {
        private DataAccess()
        { }
        public static bool isDBConnectionFail
        {
            get;
            set;
        }
        #region SQLCONNECTION

        /// <summary>
        /// This method gets the connection string.
        /// 
        /// changed private to public by kshitij
        /// </summary>
        /// <returns>Connection String</returns>
        public static string GetConnectionString()
        {
            try
            {
                string strReturnConnectionString;

                /* This code takes connection string from the web.config file.*/
                strReturnConnectionString = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString.ToString();
                return strReturnConnectionString;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// This method returns MySqlConnection object.
        /// </summary>
        /// <returns>MySqlConnection</returns>
        public static SqlConnection GetsqlConnection()
        {
            try
            {
                string strConnection;
                strConnection = GetConnectionString();
                SqlConnection SqlConnection = new SqlConnection(strConnection);
                return SqlConnection;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        static private int KillSleepingConnections()
        {
            
            return 0;
        }



        #endregion


        #region EXECUTE DATASET

        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="spName">Store Procedure Name</param>
        /// <returns>Data in the form of Dataset.</returns>
        /// <summary>
        public static DataSet ExecuteDataSetSP(string spName)
        {
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = GetsqlConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                DataAdapter.SelectCommand = myCommand;
                DataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="spName">Store Procedure Name</param>
        /// <param name="sqlParams">SqlParameters</param>
        /// <returns>Data in the form of Dataset.</returns>
        public static DataSet ExecuteDataSetSP(string spName, SqlParameter[] sqlParams)
        {
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = GetsqlConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;

                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Length; i++)
                    {
                        myCommand.Parameters.Add(sqlParams[i]);
                    }
                }

                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="cmdText">Command text</param>
        /// <returns>Data in the form of Dataset.</returns>
        /// <summary>
        public static DataSet ExecuteDataSet(string CommandText)
        {
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = GetsqlConnection();
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="cmdText">Command text</param>
        /// <param name="sqlParams">SqlParameters</param>
        /// <returns>Data in the form of Dataset.</returns>
        public static DataSet ExecuteDataSet(string CommandText, SqlParameter[] sqlParams)
        {
            try
            {
                DataSet dsData = new DataSet();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = GetsqlConnection();
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;

                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }

                myDataAdapter.SelectCommand = myCommand;
                myDataAdapter.Fill(dsData);
                return dsData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion


        #region EXECUTE SCALAR


        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <returns>Object</returns>
        public static object ExecuteScalarSP(string spName)
        {
            object objValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                SqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }


        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="sqlParams">SqlParameter</param>
        /// <returns>Object</returns>
        public static object ExecuteScalarSP(string spName, SqlParameter[] sqlParams)
        {
            object objValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }
                SqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }

        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="CommandText">CommandText</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string CommandText)
        {
            object objValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                SqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }


        /// <summary>
        /// This method returns object typecast the object to int or string depending upon return type.
        /// </summary>
        /// <param name="CommandText">CommandText</param>
        /// <param name="sqlParams">SqlParameter</param>
        /// <returns>Object</returns>
        public static object ExecuteScalar(string CommandText, SqlParameter[] sqlParams)
        {
            object objValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }
                SqlConnection.Open();
                objValue = myCommand.ExecuteScalar();
                return objValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }

        #endregion


        #region EXECUTE NONQUERY

        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="CommandText">CommandText</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string CommandText)
        {
            int intRValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                SqlConnection.Open();
              //  SqlTransaction SqlTransaction = SqlConnection.BeginTransaction();
                intRValue = myCommand.ExecuteNonQuery();
               // SqlTransaction.Commit();
                return intRValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }


        /// <summary>
        /// /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="CommandText">CommandText</param>
        /// <param name="sqlParams">SqlParameters</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string CommandText, SqlParameter[] sqlParams)
        {
            int intRValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }

                SqlConnection.Open();

                SqlTransaction SqlTransaction = SqlConnection.BeginTransaction();
                intRValue = myCommand.ExecuteNonQuery();
                SqlTransaction.Commit();

                return intRValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }

        /// <summary>
        /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="spName">Store Procedure Name</param>
        /// <returns></returns>
        public static int ExecuteNonQuerySP(string spName)
        {
            int intRValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                SqlConnection.Open();
                SqlTransaction SqlTransaction = SqlConnection.BeginTransaction();
                intRValue = myCommand.ExecuteNonQuery();
                SqlTransaction.Commit();
                return intRValue;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }


        /// <summary>
        /// /// This methods returns no of rows affected.
        /// </summary>
        /// <param name="spName">Store Procedure Name</param>
        /// <param name="sqlParams">SqlParameters</param>
        /// <returns></returns>
        public static int ExecuteNonQuerySP(string spName, SqlParameter[] sqlParams)
        {
            KillSleepingConnections();
            int intRValue;
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }

                SqlConnection.Open();

                SqlTransaction SqlTransaction = SqlConnection.BeginTransaction();

                myCommand.Transaction = SqlTransaction;
                intRValue = myCommand.ExecuteNonQuery();
                SqlTransaction.Commit();

                return intRValue;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                SqlConnection.Close();
            }

        }

        #endregion


        #region EXECUTE READER

        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="CommandText">Command Text</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string CommandText)
        {
            KillSleepingConnections();
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlDataReader SqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                SqlConnection.Open();
                SqlDataReader = myCommand.ExecuteReader();
                return SqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="spName">SP Name</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReaderSP(string spName)
        {
            KillSleepingConnections();
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlDataReader SqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                SqlConnection.Open();
                SqlDataReader = myCommand.ExecuteReader();
                return SqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }


        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="CommandText">Command Text</param>
        /// <param name="sqlParams">SqlParameterS</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string CommandText, SqlParameter[] sqlParams)
        {
            KillSleepingConnections();
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlDataReader SqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = CommandText;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }

                SqlConnection.Open();
                SqlDataReader = myCommand.ExecuteReader();
                return SqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// This method returns data in form of reader form.
        /// </summary>
        /// <param name="CommandText">SP Name</param>
        /// <param name="sqlParams">SqlParameterS</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReaderSP(string spName, SqlParameter[] sqlParams)
        {
            KillSleepingConnections();
            SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlDataReader SqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    myCommand.Parameters.Add(sqlParams[i]);
                }

                SqlConnection.Open();
                SqlDataReader = myCommand.ExecuteReader();
                return SqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public static SqlDataReader ExecuteReaderSP(string spName, SqlParameter[] sqlParams, SqlConnection SqlConnection)
        {
            KillSleepingConnections();
            //SqlConnection SqlConnection = GetsqlConnection();
            try
            {
                SqlDataReader SqlDataReader;
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = SqlConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = spName;
                if (sqlParams != null)
                {
                    for (int i = 0; i < sqlParams.Length; i++)
                    {
                        myCommand.Parameters.Add(sqlParams[i]);
                    }
                }

                SqlConnection.Open();
                SqlDataReader = myCommand.ExecuteReader();
                return SqlDataReader;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
