using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProcessNotification;

namespace AutoEproof
{
    class FreshResupply :ProcessMsg
    {
        public FreshResupply()
        { 
        }
        public void StartProcess()
        { 
            string SrchPath = ConfigDetails.S100RESUPPLY;
            string[] pdfFls = Directory.GetFiles(SrchPath, "*.pdf");
            foreach (string pdfFl in pdfFls)
            {
                string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                string Fname = Path.GetFileNameWithoutExtension(pdfFl);
                string[] JIDAIDArr = Fname.Split(new char[]{ '_','-'});
                string JIDAID = string.Empty;
                if (JIDAIDArr.Length == 2)
                {
                    string JID = JIDAIDArr[0].ToUpper();
                    string AID = JIDAIDArr[1];
                    JIDAID = JID + AID.ToUpper().Replace(JID, "").Trim(new char[] { ' ', '_', '-', '\t', '.' });
                }
                else
                {
                    JIDAID = Fname;
                }


                if (!string.IsNullOrEmpty(JIDAID))
                {
                    string SqlStr = "SELECT [CLIENT],[JID],[AID] from [WileyWIP] where [JIDAID] ='" + JIDAID + "'";
                    JIDAID = JIDAID.ToUpper();
                    SqlDataReader DR = SqlHelper.ExecuteReader(OPSConStr, System.Data.CommandType.Text, SqlStr);

                    if (DR.HasRows)
                    {
                        DR.Read();
                        string C = DR[0].ToString();
                        string J = DR[1].ToString();
                        string A = DR[2].ToString();

                        if (InsertInWileyFreshResuplly(C, J, A))
                        {
                            File.Delete(pdfFl);

                            ////if ("#JABA#JEAB#".IndexOf("#" + J + "#") != -1)
                            ////{
                            ////    DatabaseLayer.OPSDB     OPSDBObj  = new DatabaseLayer.OPSDB(OPSConStr);
                            ////    DatabaseLayer.OPSDetail OPSDtl    = OPSDBObj.GetOPSDetails(J, C);

                            ////    SqlStr = "Update eProofHistory Set AID=AID+ '_EDITOR' where [OPSID] =" + OPSDtl.OPSID + " AND AID='" + A + "'";

                            ////    ProcessEventHandler("Update eProofHistory Sql Str  :: " + SqlStr);
                            ////    SqlHelper.ExecuteNonQuery(OPSConStr, System.Data.CommandType.Text, SqlStr);
                            ////}
                        }
                    }
                    else
                    {
                        ProcessEventHandler(JIDAID + " does not exist in WileyWIP.");
                    }
                    DR.Close();
                    DR.Dispose();
                }
                else
                { 
                    ProcessEventHandler("JIDAID is empty");
                }
            }
        }

        private bool InsertInWileyFreshResuplly(string Client, string JID, string AID)
        {
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@Client", Client);
            para[1] = new SqlParameter("@JID", JID);
            para[2] = new SqlParameter("@AID", AID);

           
            try
            {
                int Rslt = SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.StoredProcedure, "usp_WileyFreshResuplly", para);

                string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                SqlHelper.ExecuteNonQuery(OPSConStr, System.Data.CommandType.StoredProcedure, "usp_UpdateFreshResupplyDueDate", para);

            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }

            
            return true;
        }
    }
}
