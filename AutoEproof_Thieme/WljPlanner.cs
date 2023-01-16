using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

namespace AutoEproof
{
    public class ArrayOfJIDAID
    {
        [XmlElement("JIDAID")]
        public List<JIDAID> JIDAIDInfo { get; set; }
    }
    public class JIDAID
    {
        public JIDAID()
        { }

        public JIDAID(string Client, string JID, string AID, string Stage)
        {
            this.Client = Client;
            this.JID = JID;
            this.AID = AID;
            this.STAGE = Stage;
            this.ClientStage = Stage;
        }

        public string ClientStage
        {
            get;
            set;
        }
        public string Client
        {
            get;
            set;
        }

        public string JID
        {
            get;
            set;
        }

        public string AID
        {
            get;
            set;
        }

        public string STAGE
        {
            get;
            set;
        }

    }


    public class ArrayOfArticleInfo
    {
        [XmlElement("ArticleInfo")]
        public List<WIPArticleInfo> WIPArticleInfo { get; set; }
    }

    [Serializable]
    public class WIPArticleInfo : IComparable<WIPArticleInfo>
    {
        public string SNO
        {
            get;
            set;
        }
        public WIPArticleInfo()
        {
        }

        public string Client
        {
            get;
            set;
        }
        public string JIDAID
        {
            get;
            set;
        }
        public string JID
        {
            get;
            set;
        }
        public string AID
        {
            get;
            set;
        }
        public string ClientStage
        {
            get;
            set;
        }
        public string PSEStage
        {
            get;
            set;
        }
        public string PSELoginID
        {
            get;
            set;
        }
        public string PSEName
        {
            get;
            set;
        }
        public string GrLoginID
        {
            get;
            set;
        }
        public string GrUserName
        {
            get;
            set;
        }
        public string GrStatus
        {
            get;
            set;
        }
        public string RcvdDate
        {
            get;
            set;
        }
        public string DueDate
        {
            get;
            set;
        }
        public string PdfPages
        {
            get;
            set;
        }
        public string MSS
        {
            get;
            set;
        }
        public string FIGS
        {
            get;
            set;
        }
        public string IP
        {
            get;
            set;
        }
        #region IComparable<AuthorInfo> Members

        public int CompareTo(WIPArticleInfo other)
        {
            return (JIDAID).CompareTo((other.JIDAID));
        }

        #endregion
    }

    public class WljPlanner
    {
        string OPSConnectionString     = string.Empty;
        string NoidaDBConnectionString = string.Empty;
        string GangDBConnectionString  = string.Empty;

        DataTable WIPTable            =new DataTable();
        List<WIPArticleInfo> ArticleList = new List<WIPArticleInfo>();


        public WljPlanner()
        {
            OPSConnectionString     = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            NoidaDBConnectionString = ConfigurationManager.ConnectionStrings["NoidaDBConnectionString"].ConnectionString;
        }
        public bool isDBConnectionFail
        {
            get;
            set;
        }
        public List<WIPArticleInfo> GetArticleList()
        {

            DataSet DS = new DataSet();

            try
            {
                DS = SqlHelper.ExecuteDataset(OPSConnectionString, System.Data.CommandType.StoredProcedure, "FMSReport");
            }
            catch (SqlException ex)
            {
                isDBConnectionFail = true;
                return ArticleList;
            }


            WIPTable = DS.Tables[0];
            WIPTable.TableName = "JIDLIST";
            List<JIDAID> JIDAIDList = new List<JIDAID>();
            foreach (DataRow DR in WIPTable.Rows)
            {
                JIDAIDList.Add(new JIDAID(DR["Client"].ToString().Trim().Trim(new char[] { '\t' }), DR["JID"].ToString(), DR["AID"].ToString(), DR["STAGE"].ToString()));
            }

            string SrlzXML = SerializeToXML(JIDAIDList);

            FillArticleList(SrlzXML, NoidaDBConnectionString, ConfigDetails.TDXPSNoidaIP);
            //FillArticleList(SrlzXML, GangDBConnectionString,  ConfigDetails.TDXPSGangtokIP);



            try
            {
                DS = SqlHelper.ExecuteDataset(OPSConnectionString, System.Data.CommandType.StoredProcedure, "FMSReportBatch1");
            }
            catch (SqlException ex)
            {
                isDBConnectionFail = true;
                return ArticleList;
            }

            WIPTable = DS.Tables[0];
            WIPTable.TableName = "JIDLIST";
            JIDAIDList = new List<JIDAID>();
            foreach (DataRow DR in WIPTable.Rows)
            {
                JIDAIDList.Add(new JIDAID(DR["Client"].ToString().Trim().Trim(new char[] { '\t' }), DR["JID"].ToString(), DR["AID"].ToString(), DR["STAGE"].ToString()));
            }
            SrlzXML = SerializeToXML(JIDAIDList);
            FillArticleList(SrlzXML, NoidaDBConnectionString, ConfigDetails.TDXPSNoidaIP);
            //FillArticleList(SrlzXML, GangDBConnectionString, ConfigDetails.TDXPSGangtokIP);
            return ArticleList;
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
                            SqlCommand command = new SqlCommand("usp_GetArticleDetail", conn);
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
                        Console.WriteLine(ex.Message);
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
                    AI.ClientStage = DR[3].ToString();

                    AI.PSELoginID = DR[4].ToString();
                    AI.PSEName = DR[5].ToString();
                    AI.PSEStage = DR[6].ToString();

                    AI.GrLoginID = DR[7].ToString();
                    AI.GrUserName = DR[8].ToString();
                    AI.GrStatus = DR[9].ToString();

                    AI.JIDAID = AI.JID + AI.AID;

                    DataRow[] DRs = WIPTable.Select("JIDAID ='" + AI.JIDAID + "'");
                    if (DRs.Length > 0)
                    {
                        DataRow Row = DRs[0];
                        DateTime RcvDate;
                        DateTime DueDate;

                        if (DateTime.TryParse(Row["RCDDATE"].ToString(), out RcvDate))
                            AI.RcvdDate = string.Format("{0:dd-MMM-yyyy}", RcvDate);
                        else
                            AI.RcvdDate = "";


                        if (DateTime.TryParse(Row["DUEDATE"].ToString(), out DueDate))
                            AI.DueDate = string.Format("{0:dd-MMM-yyyy}", DueDate);
                        else
                            AI.DueDate = "";

                        AI.MSS = Row["MSS"].ToString();
                        AI.PdfPages = Row["PAGES"].ToString();
                        AI.FIGS = Row["FIGS"].ToString();
                        AI.IP = IP;
                    }

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