using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ProcessNotification;
namespace LWWAutoIntegrate
{
    class GoXMLList : MessageEventArgs
    {
        List<String> _GoFileList = new List<string>();
        string _WIPConnection = string.Empty;
        public List<String> GoFileList
        {
            get { return _GoFileList; }
        }
        public GoXMLList()
        {
            _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
        }
        public void AssignDownloadGoXMLList()
        {
            GetGoXMLList("[usp_GetLWWDownloadMaterial]");
        }
        public void AssignIntegrateArticleGoXMLList()
        {
            GetGoXMLList("[usp_GetLWWIntegrateArticle]");
        }
        public void GetGoXMLList(string spName)
        {
            ProcessEventHandler("Getting article list for process..");

            string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            try
            {
                _GoFileList.Clear();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_WIPConnection, System.Data.CommandType.StoredProcedure,spName);

                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        if (Dr["goxmlpath"] != null && Dr["goxmlpath"] != DBNull.Value)
                        {
                            _GoFileList.Add(Dr["goxmlpath"].ToString());

                            ProcessEventHandler( "goxmlpath :" + _GoFileList[_GoFileList.Count-1]);
                        }
                    }
                }
                Dr.Close();
                Dr.Dispose();
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
            }

            if (_GoFileList.Count == 0)
            {
                ProcessEventHandler("No artilce to be process ");
            }
        }

        public bool UpdateArticleDetails(GoXmlInfo GoXml)
        {
            if (UpdateDatabase(GoXml))
            {
                if (UpdateStatus(GoXml))
                {
                    UpdateGoMetaXML(GoXml);
                    return true;
                }
            }
            return false;
        }

        private bool UpdateStatus(GoXmlInfo GoXml)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@serverip", GoXml.ServerIP);
            param[1] = new SqlParameter("@guid", GoXml.Guid);

            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWIntegrationStatus]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }

        private bool UpdateGoMetaXML(GoXmlInfo GoXml)
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@Goxml", GoXml.GoXML);
            param[1] = new SqlParameter("@MetaDataXml", GoXml.MetaDataXML);
            param[2] = new SqlParameter("@guid", GoXml.Guid);
            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWDownloadGoXMl]", param);
            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }
        private bool UpdateDatabase(GoXmlInfo GoXml)
        {
            SqlParameter[] param = new SqlParameter[13];

            param[0] = new SqlParameter("@Guid", GoXml.Guid);
            param[1] = new SqlParameter("@Aid", GoXml.AID);
            param[2] = new SqlParameter("@TaskName", GoXml.TaskName);
            param[3] = new SqlParameter("@Mss", GoXml.MSS);
            param[4] = new SqlParameter("@FigCount", GoXml.FigCount);
            param[5] = new SqlParameter("@Doi", GoXml.DOI);
            param[6] = new SqlParameter("@Jid", GoXml.JID);
            param[7] = new SqlParameter("@Stage", GoXml.FMSStage);
            param[8] = new SqlParameter("@Vol", GoXml.VOL);
            param[9] = new SqlParameter("@Iss", GoXml.Issue);
            param[10] = new SqlParameter("@remarks", "");
            param[11] = new SqlParameter("@GoXmlPath", GoXml.GoXMLPath);
            param[12] = new SqlParameter("@DUEDATE", GoXml.DueDate);
            try
            {
                SqlHelper.ExecuteNonQuery(_WIPConnection, System.Data.CommandType.StoredProcedure, "[usp_UpdateLWWDownload]", param);
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
