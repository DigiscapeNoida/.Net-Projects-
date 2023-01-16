using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoEproof
{
    class ArticleUserUpdate
    {
        string OPSConnectionString     = string.Empty;
        string NoidaDBConnectionString = string.Empty;
        string GangDBConnectionString  = string.Empty;

        DataTable WIPTable            =new DataTable();
        List<WIPArticleInfo> ArticleList = new List<WIPArticleInfo>();

        
        public ArticleUserUpdate()
        { 
            OPSConnectionString     = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            GangDBConnectionString  = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            NoidaDBConnectionString = ConfigurationManager.ConnectionStrings["NoidaDBConnectionString"].ConnectionString;
        }


        public void StartToUpdateArticleUser()
        {
            DoProcess();
        }
        public bool isDBConnectionFail
        {
            get;
            set;
        }
        public void DoProcess()
        {
            DataSet DS = new DataSet();
            try
            {
                DS = SqlHelper.ExecuteDataset(OPSConnectionString, System.Data.CommandType.StoredProcedure, "usp_GetNullUID");
            }
            catch (SqlException ex)
            {
                isDBConnectionFail = true;
                return ;
            }

            WIPTable = DS.Tables[0];
            WIPTable.TableName = "JIDLIST";
            List<JIDAID> JIDAIDList = new List<JIDAID>();
            foreach (DataRow DR in WIPTable.Rows)
            {
                JIDAIDList.Add(new JIDAID(DR["Client"].ToString().Trim().Trim(new char[] { '\t' }), DR["JID"].ToString(), DR["AID"].ToString(), DR["STAGE"].ToString()));
            }

            if (JIDAIDList.Count > 0)
            {
                string SrlzXML = SerializeToXML(JIDAIDList);
                SrlzXML = SrlzXML.Replace("RESUPPLY</STAGE>", "</STAGE>");

                FillArticleList(SrlzXML, GangDBConnectionString, ConfigDetails.TDXPSGangtokIP);

                SrlzXML = SerializeToXML(ArticleList);

                UpdateUser(SrlzXML);
            }
            return ;
        }

        private void UpdateUser(string SrlzXML)
        {
            using (SqlConnection conn = new SqlConnection(OPSConnectionString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        SqlCommand command      = new SqlCommand("usp_UpdateArticlesUserName", conn);
                        command.CommandTimeout  = 0;
                        command.CommandType     = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@strXML", SrlzXML));
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        isDBConnectionFail = true;
                    }
                }
                catch (SqlException ex)
                {
                    isDBConnectionFail = true;
                    return;
                }
            }
        }
        private void FillArticleList( string SrlzXML,string ConStr,string IP)
        {
                DataSet  DS = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConStr))
                {
                    try
                    {
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            SqlCommand command = new SqlCommand("usp_GetArticlesUserName", conn);
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@strXML", SrlzXML));
                            var adapter = new SqlDataAdapter(command);
                            adapter.Fill(DS);
                        }
                        else
                        {
                            isDBConnectionFail = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        isDBConnectionFail = true;
                        return;
                    }

                }
                DataTable DT = DS.Tables[0];
                DT = DS.Tables[0];
                DT.TableName = "ArticleStatus";

                int Count = 0;

                foreach (DataRow DR in DT.Rows)
                {
                    Count++;
                    WIPArticleInfo AI = new WIPArticleInfo();
                    AI.SNO = Count.ToString();
                    AI.Client = DR[0].ToString();
                    AI.JID = DR[1].ToString();
                    AI.AID = DR[2].ToString();

                    AI.ClientStage = DR[4].ToString();
                    AI.PSELoginID = DR[5].ToString();
                    ArticleList.Add(AI);
                }
            }
           
        


        public  void GettingArticleList()
        {
            bool UsePrvusRslt = false;
            string TDXPSPlanner = "C:\\TEMP\\TDXPSPlanner.xml";

            if (File.Exists(TDXPSPlanner))
            {
                FileInfo FI = new FileInfo(TDXPSPlanner);
                TimeSpan TimeDiff = DateTime.Now.TimeOfDay;
                TimeDiff = DateTime.Now - FI.LastWriteTime;
                if (TimeDiff.Hours == 0 && TimeDiff.Minutes < 5)
                    UsePrvusRslt = true;
            }

            if (UsePrvusRslt)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfArticleInfo));
                using (FileStream fileStream = new FileStream(TDXPSPlanner, FileMode.Open))
                {
                    ArrayOfArticleInfo ArtilcleDetailsList = (ArrayOfArticleInfo)serializer.Deserialize(fileStream);
                    ArticleList = ArtilcleDetailsList.WIPArticleInfo;
                }
            }
            else
            {
                if (ArticleList.Count > 0)
                {
                    string ArticleInfoXML = SerializeToXML(ArticleList);
                    File.WriteAllText(TDXPSPlanner, ArticleInfoXML);
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfArticleInfo));
                    using (FileStream fileStream = new FileStream(TDXPSPlanner, FileMode.Open))
                    {
                        ArrayOfArticleInfo ArtilcleDetailsList = (ArrayOfArticleInfo)serializer.Deserialize(fileStream);
                        ArticleList = ArtilcleDetailsList.WIPArticleInfo;
                    }
                }
            }
        }
        private void SerializeToXML<T>(List<T> source, string FilePath)
        {

            XmlSerializer serializer = new XmlSerializer(source.GetType());
            var settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter _XmlWriter = XmlWriter.Create(FilePath, settings);
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(_XmlWriter, source, emptyNs);
            _XmlWriter.Flush();
            _XmlWriter.Close();

        }
        private string SerializeToXML<T>(List<T> source)
        {

            StringBuilder SerializeXML = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(source.GetType());
            var settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter _XmlWriter = XmlWriter.Create(SerializeXML, settings);
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(_XmlWriter, source, emptyNs);
            _XmlWriter.Flush();
            _XmlWriter.Close();

            string s = SerializeXML.ToString().Replace("#$#", "&");
            return s;

        }
    }
}
