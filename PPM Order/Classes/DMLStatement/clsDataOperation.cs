using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PPM_TRACKING_SYSTEM.Classes.ClsObjects;
using PPM_TRACKING_SYSTEM.Classes.Connection;


namespace PPM_TRACKING_SYSTEM.Classes.DMLStatement
{
    //fetch the connection string from the Connection String
    class clsDataOperation
    {
        private SqlConnection sqlCon = null;
        private clsObjects objClsObj = null;
        
        private SqlDataAdapter objSDA = null;
        //Write your DML Operations here...
        //Insert/update/delete
        public int InsertData(String PPMno, String PPMFilename, String PPMDDate, String PPMDate, String PPMOrdertype,
                                String PPMCreationdate, String PPMShorttitle, String Isbn, String JobTitle, String UploadStatus, String ProdSite, String PlanDueDate)
        {

            SqlConnection oSql = clsConnection.getSQLConnection();
            try
            {
                objClsObj = new clsObjects();
                objSDA = new SqlDataAdapter("sp_insert_data", oSql);
                objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMno", PPMno);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMFilename", PPMFilename);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMDDate", PPMDDate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMDate", PPMDate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMOrdertype", PPMOrdertype);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMCreationdate", PPMCreationdate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMShorttitle", PPMShorttitle);
                objSDA.SelectCommand.Parameters.AddWithValue("@Isbn", Isbn);
                objSDA.SelectCommand.Parameters.AddWithValue("@JobTitle", JobTitle);
                objSDA.SelectCommand.Parameters.AddWithValue("@UploadStatus", UploadStatus);
                objSDA.SelectCommand.Parameters.AddWithValue("@ProdSite", ProdSite);
                objSDA.SelectCommand.Parameters.AddWithValue("@PlanDueDate", PlanDueDate);

                int status = objSDA.SelectCommand.ExecuteNonQuery();
                return status;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                oSql.Close();
            }
        }

        public int InsertData(String PPMno, String PPMFilename, String PPMDDate, String PPMDate, String PPMOrdertype,
                                 String PPMCreationdate, String PPMShorttitle, String Isbn, String JobTitle,String UploadStatus, String ProdSite,String PlanDueDate,String ISSN,String JID,String serialTitle,String vol)

        {
            
            SqlConnection Connect = clsConnection.getSQLConnection();
            try
            {
                objClsObj = new clsObjects();
                objSDA = new SqlDataAdapter("sp_insert_data_BS", Connect);
                objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMno", PPMno);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMFilename", PPMFilename);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMDDate", PPMDDate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMDate", PPMDate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMOrdertype", PPMOrdertype);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMCreationdate", PPMCreationdate);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMShorttitle", PPMShorttitle);
                objSDA.SelectCommand.Parameters.AddWithValue("@Isbn", Isbn);
                objSDA.SelectCommand.Parameters.AddWithValue("@JobTitle", JobTitle);
                objSDA.SelectCommand.Parameters.AddWithValue("@UploadStatus", UploadStatus);
                objSDA.SelectCommand.Parameters.AddWithValue("@ProdSite", ProdSite);
                objSDA.SelectCommand.Parameters.AddWithValue("@PlanDueDate", PlanDueDate);
                objSDA.SelectCommand.Parameters.AddWithValue("@Issn", ISSN);
                objSDA.SelectCommand.Parameters.AddWithValue("@Jid", JID);
                objSDA.SelectCommand.Parameters.AddWithValue("@vol_no", vol);
                objSDA.SelectCommand.Parameters.AddWithValue("@ParentTitle", serialTitle);

                int status = objSDA.SelectCommand.ExecuteNonQuery();
                return status;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                Connect.Close();
            }
        }

        public int UpdateData(String strISBN, String strStage, String strSignalID)
        {
            SqlConnection connn = clsConnection.getSQLConnection();
            try
            {
                objClsObj = new clsObjects();

                string strCreationDate = DateTime.Now.ToString();
                objSDA = new SqlDataAdapter("sp_UpdateSignal_data", connn);
                objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                objSDA.SelectCommand.Parameters.AddWithValue("@Isbn", strISBN);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMOrdertype", strStage);
                objSDA.SelectCommand.Parameters.AddWithValue("@SignalId", strSignalID);
                objSDA.SelectCommand.Parameters.AddWithValue("@SignalCreation", strCreationDate);
                int status = objSDA.SelectCommand.ExecuteNonQuery();
                return status;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                connn.Close();
            }
        }

        public int UpdateStatus(String UploadStatus, String strISBN,String strStage)
        {

            SqlConnection connn = clsConnection.getSQLConnection();
            try
            {
                objClsObj = new clsObjects();

                string strCreationDate = DateTime.Now.ToString();
                objSDA = new SqlDataAdapter("sp_UpdateStatus_data", connn);
                objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                objSDA.SelectCommand.Parameters.AddWithValue("@Isbn", strISBN);
                objSDA.SelectCommand.Parameters.AddWithValue("@UploadStatus", UploadStatus);
                objSDA.SelectCommand.Parameters.AddWithValue("@PPMOrdertype", strStage);
                int status = objSDA.SelectCommand.ExecuteNonQuery();
                return status;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                connn.Close();
            }
        }


        public int CheckData(String PPMFilename)
        {
            SqlConnection connn = clsConnection.getSQLConnection();
            try
            {
                objClsObj = new clsObjects();
                objSDA = new SqlDataAdapter("Select Count(*) from PPM_Information where PPMFilename='" + PPMFilename + "'", connn);
                objSDA.SelectCommand.CommandType = CommandType.Text;
                Object objstatus = objSDA.SelectCommand.ExecuteScalar();
                int status = Convert.ToInt32(objstatus);
                return status;
            }
            catch (Exception ex)
            {
                return 2;
            }
            finally
            {
                connn.Close();
            }
        }
        public int UpdatePlanDate(String strISBN, String strPlanDueDate, String strPPMOrdertype)
        {
             SqlConnection con = clsConnection.getSQLConnection();
             try
             {
                 objClsObj = new clsObjects();

                 //string strCreationDate = DateTime.Now.ToString();


                 objSDA = new SqlDataAdapter("sp_UpdatePlanDate_data", con);
                 objSDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                 objSDA.SelectCommand.Parameters.AddWithValue("@Isbn", strISBN);
                 objSDA.SelectCommand.Parameters.AddWithValue("@PlanDueDate", strPlanDueDate);
                 objSDA.SelectCommand.Parameters.AddWithValue("@PPMOrdertype", strPPMOrdertype);

                 int status = objSDA.SelectCommand.ExecuteNonQuery();
                 return status;
             }
             catch (Exception ex)
             {
                 return 0;
             }
             finally
             {
                 con.Close();
             }
        }

        //public Boolean DeleteData() { return false; }
    }
}
