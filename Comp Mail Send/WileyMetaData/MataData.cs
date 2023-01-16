using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WileyMetaData
{
    public class MetaData
    {

        string _XmlPath        = string.Empty;
        ArticleInfo _ArtclInfo = new ArticleInfo();
        XmlDocument _xDoc      = new XmlDocument();
        public MetaData()
        {

        }
        public MetaData(string XmlPath)
        {
            _XmlPath = XmlPath;

            XmlInfo XmlInfoObj = new XmlInfo(XmlPath);
            XmlInfoObj.LoadXml();

            _xDoc = XmlInfoObj.xmlDocument;

            XmlNodeList ms_idList = _xDoc.DocumentElement.GetElementsByTagName("ms_id");
             
            while (ms_idList.Count>1)
            {
                ms_idList[1].ParentNode.RemoveChild(ms_idList[1]);
            }

            //////do'nt change the sequence
            XmlNodeList RvsDate = _xDoc.DocumentElement.GetElementsByTagName("revised_date");

            while (RvsDate.Count > 1)
            {
                RvsDate[0].ParentNode.RemoveChild(RvsDate[0]);
            }

            RemoveNode("editor_list");
            RemoveNode("office_contact");
            RemoveNode("reviewer_list");
            RemoveNode("email_list");
            RemoveNode("user_list");

            XmlNodeList FileList = _xDoc.DocumentElement.GetElementsByTagName("file");
            foreach (XmlNode FileNode in FileList)
                   _ArtclInfo.FileList.Add(ProcessFileNode(FileNode));

            ////////Do'nt Change Sequence
            RemoveNode("file_list");


            XmlNodeList AuthorList = _xDoc.DocumentElement.GetElementsByTagName("author_list");
            if (AuthorList.Count == 1)
            {
                XmlNode Authors = AuthorList[0];
               foreach (XmlNode Author in Authors)
                   _ArtclInfo.Authors.Add(ProcessAuNode(Author));

               Authors.ParentNode.RemoveChild(Authors);
            }

            XmlNodeList NL = _xDoc.DocumentElement.SelectNodes(".//*");
            //StringBuilder Str = new StringBuilder();
            foreach (XmlNode Node in NL)
            {
                ProcessNode(Node);
                //Str.AppendLine(Node.Name + "\t" + Node.InnerText.Replace("\n", " "));
            }

            //NL = _xDoc.DocumentElement.SelectNodes(".//text()");
            //foreach (XmlNode Node in NL)
            //{
            //    ProcessNode(Node.ParentNode);
            //}



            //XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfArticleInfo));
            //using (FileStream fileStream = new FileStream("c:\\112.xml", FileMode.Open))
            //{
            //    ArrayOfArticleInfo ArtilcleDetailsList = (ArrayOfArticleInfo)serializer.Deserialize(fileStream);
            //    VCHRmdrOBJ.ArtilcleDetails = ArtilcleDetailsList.ArtclDtls;
            //    ArticleInfo _a = new ArticleInfo();
            //    _a = ArtilcleDetailsList.ArtclInfo[0];
            //}
        }
        public ArticleInfo ArticleDetail 
        {
            get { return _ArtclInfo; }
        }
        private void RemoveNode(string NodeName)
        {
            XmlNodeList NodeList = _xDoc.DocumentElement.GetElementsByTagName(NodeName);
            if (NodeList.Count == 1)
            {
                NodeList[0].ParentNode.RemoveChild(NodeList[0]);
            }
        }
        private void ProcessNode(XmlNode Node)
        {
            switch (Node.Name)
            {
                case "abstract":
                    {
                        _ArtclInfo.Abstract = Node.InnerXml;
                        break;
                    }
                case "addr1":
                    {
                        break;
                    }
                case "addr2":
                    {
                        break;
                    }
                case "addr3":
                    {
                        break;
                    }
                case "affiliation":
                    {
                        break;
                    }
                case "approval_date":
                    {
                        break;
                    }
                case "article":
                    {
                        if (Node.Attributes.GetNamedItem("ms_no") != null)
                            _ArtclInfo.RefCode = Node.Attributes.GetNamedItem("ms_no").Value;
                        break;
                    }
                case "article_id":
                    {
                        break;
                    }
                case "article_id_list":
                    {
                        break;
                    }
                case "article_status":
                    {
                        _ArtclInfo.ArticleStatus = Node.InnerText;
                        break;
                    }
                case "article_sub_title":
                    {
                        _ArtclInfo.ArticleSubTitle = Node.InnerText;
                        
                        break;
                    }
                case "article_title":
                    {
                        _ArtclInfo.Articletitle = Node.InnerText;
                        break;
                    }
                case "assigned":
                    {
                        break;
                    }
                case "attr_type":
                    {
                        break;
                    }
                case "attribute":
                    {
                        break;
                    }
                case "author":
                    {
                        break;
                    }
                case "author_comments":
                    {
                        break;
                    }
                case "author_list":
                    {
                        break;
                    }
                case "author_returned_date":
                    {
                        break;
                    }
                case "body":
                    {
                        break;
                    }
                case "cc":
                    {
                        break;
                    }
                case "city":
                    {
                        break;
                    }
                case "comment":
                    {
                        break;
                    }
                case "comments":
                    {
                        break;
                    }
                case "configurable_data_fields":
                    {
                        break;
                    }
                case "content":
                    {
                        break;
                    }
                case "country":
                    {
                        break;
                    }
                case "current_profile_affiliation":
                    {
                        break;
                    }
                case "custom_fields":
                    {
                        ProcessCustomFields(Node);
                        break;
                    }
                case "day":
                    {
                        if (Node.ParentNode.Name.Equals("decision_date"))
                            _ArtclInfo.HistoryDate.AcceptedDate.Day = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("received_date"))
                            _ArtclInfo.HistoryDate.ReceivedDate.Day = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("revised_date"))
                            _ArtclInfo.HistoryDate.RevisedDate.Day = Node.InnerText;
                        break;
                    }
                case "decision":
                    {
                        break;
                    }
                case "decision_date":
                    {
                        break;
                    }
                case "degree":
                    {
                        break;
                    }
                case "dept":
                    {
                        break;
                    }
                case "editor":
                    {
                        break;
                    }
                case "editor_list":
                    {
                        break;
                    }
                case "email":
                    {
                        break;
                    }
                case "email_list":
                    {
                        break;
                    }
                case "email_sent":
                    {
                        break;
                    }
                case "fax":
                    {
                        break;
                    }
                case "file":
                    {
                        break;
                    }
                case "file_caption":
                    {
                        break;
                    }
                case "file_designation":
                    {
                        break;
                    }
                case "file_extension":
                    {
                        break;
                    }
                case "file_format":
                    {
                        break;
                    }
                case "file_list":
                    {
                        break;
                    }
                case "file_originalname":
                    {
                        break;
                    }
                case "file_tag":
                    {
                        break;
                    }
                case "first_name":
                    {
                        break;
                    }
                case "first_page":
                    {
                        break;
                    }
                case "full_journal_title":
                    {
                        _ArtclInfo.JournalTitle = Node.InnerText;
                        break;
                    }
                case "fulltext_url":
                    {
                        break;
                    }
                case "galleyDeliveryType":
                    {
                        break;
                    }
                case "history":
                    {
                        break;
                    }
                case "hour":
                    {
                        break;
                    }
                case "inst":
                    {
                        break;
                    }
                case "invited":
                    {
                        break;
                    }
                case "issn":
                    {
                        if (Node.Attributes.GetNamedItem("issn_type") != null)
                        {
                            string  ISSNType= Node.Attributes.GetNamedItem("issn_type").InnerText;

                            if (ISSNType.Equals("digital"))
                                _ArtclInfo.EISSN = Node.InnerText;
                            else if (ISSNType.Equals("print"))
                                _ArtclInfo.PISSN = Node.InnerText;
                        }
                        break;
                    }
                case "journal":
                    {
                        break;
                    }
                case "journal_abbreviation":
                    {
                        _ArtclInfo.JID = Node.InnerText;
                        break;
                    }
                case "last_name":
                    {
                        break;
                    }
                case "last_page":
                    {
                        break;
                    }
                case "lte_text_area":
                    {
                        break;
                    }
                case "middle_name":
                    {
                        break;
                    }
                case "minute":
                    {
                        break;
                    }
                case "month":
                    {
                        if (Node.ParentNode.Name.Equals("decision_date"))
                            _ArtclInfo.HistoryDate.AcceptedDate.Month = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("received_date"))
                            _ArtclInfo.HistoryDate.ReceivedDate.Month = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("revised_date"))
                            _ArtclInfo.HistoryDate.RevisedDate.Month = Node.InnerText;
                        break;
                    }
                case "most_recent_decision_date":
                    {
                        break;
                    }
                case "ms_id":
                    {
                        break;
                    }
                case "notes":
                    {
                        break;
                    }
                case "num_bw_figures":
                    {
                        break;
                    }
                case "num_color_figures":
                    {
                        break;
                    }
                case "num_pages_actual":
                    {
                        break;
                    }
                case "num_pages_calc":
                    {
                        break;
                    }
                case "office_contact":
                    {
                        break;
                    }
                case "person_title":
                    {
                        break;
                    }
                case "phone":
                    {
                        break;
                    }
                case "post_code":
                    {
                        break;
                    }
                case "profile_affiliation":
                    {
                        break;
                    }
                case "province":
                    {
                        break;
                    }
                case "publication_type":
                    {
                        
                        break;
                    }
                case "publisher_name":
                    {
                        
                        break;
                    }
                case "pubmed_abbreviation":
                    {
                        break;
                    }
                case "rating":
                    {
                        break;
                    }
                case "received_date":
                    {
                        //_ArtclInfo.

                        break;
                    }
                case "recommendation":
                    {
                        break;
                    }
                case "replaces":
                    {
                        break;
                    }
                case "response":
                    {
                        break;
                    }
                case "rev_id":
                    {
                        break;
                    }
                case "review":
                    {
                        break;
                    }
                case "review_comments":
                    {
                        break;
                    }
                case "reviewer":
                    {
                        break;
                    }
                case "reviewer_list":
                    {
                        break;
                    }
                case "revised_date":
                    {
                        break;
                    }
                case "room":
                    {
                        break;
                    }
                case "salutation":
                    {
                        break;
                    }
                case "score":
                    {
                        break;
                    }
                case "second":
                    {
                        break;
                    }
                case "send_date":
                    {
                        break;
                    }
                case "state":
                    {
                        break;
                    }
                case "status":
                    {
                        break;
                    }
                case "subject":
                    {
                        break;
                    }
                case "submitted_date":
                    {


                        break;
                    }
                case "suffix":
                    {
                        break;
                    }
                case "task":
                    {
                        break;
                    }
                case "time_zone":
                    {
                        break;
                    }
                case "to":
                    {
                        break;
                    }
                case "total_figures":
                    {
                        break;
                    }
                case "total_pages_actual":
                    {
                        break;
                    }
                case "total_pages_calc":
                    {
                        break;
                    }
                case "total_pdf_pages":
                    {
                        break;
                    }
                case "total_tables":
                    {
                        break;
                    }
                case "transmission_date":
                    {
                        break;
                    }
                case "user":
                    {
                        break;
                    }
                case "user_list":
                    {
                        break;
                    }
                case "vernacular_title":
                    {
                        break;
                    }
                case "web_publish_date":
                    {
                        break;
                    }
                case "year":
                    {
                        if (Node.ParentNode.Name.Equals("decision_date"))
                            _ArtclInfo.HistoryDate.AcceptedDate.Year = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("received_date"))
                            _ArtclInfo.HistoryDate.ReceivedDate.Year = Node.InnerText;
                        else if (Node.ParentNode.Name.Equals("revised_date"))
                            _ArtclInfo.HistoryDate.RevisedDate.Year = Node.InnerText;
                        break;
                    }
            }

        }
        private Affilation ProcessAffilation(XmlNode Node)
        {
            XmlNodeList AllChild = Node.ChildNodes;
            Affilation Aff = new Affilation ();

            foreach (XmlNode chNode in AllChild)
            {
                ProcessAffilation(chNode,Aff) ;
            }
            return Aff;
        }
        private void ProcessAffilation(XmlNode Node, Affilation Aff)
        {
            
            switch (Node.Name)
            {
                case "addr1":
                    {
                        Aff.Addr1 = Node.InnerText;
                        break;
                    }
                case "addr2":
                    {
                        Aff.Addr2 = Node.InnerText;
                        break;
                    }
                case "addr3":
                    {
                        Aff.Addr3 = Node.InnerText;
                        break;
                    }
                case "building":
                    {
                        Aff.Building = Node.InnerText;
                        break;
                    }
                case "city":
                    {
                        Aff.City = Node.InnerText;
                        break;
                    }
                case "country":
                    {
                        Aff.Country = Node.InnerText;

                        if (Node.Attributes.GetNamedItem("country_code")!= null)
                        {
                            Aff.CountryCode = Node.Attributes.GetNamedItem("country_code").Value;
                        }
                        break;
                    }
                case "dept":
                    {
                        Aff.Dept = Node.InnerText;
                        break;
                    }
                case "fax":
                    {
                        Aff.Fax = Node.InnerText;
                        break;
                    }
                case "inst":
                    {
                        Aff.Inst = Node.InnerText;
                        break;
                    }
                case "person_title":
                    {
                        Aff.PersonTitle = Node.InnerText;
                        break;
                    }
                case "phone":
                    {
                        Aff.Phone = Node.InnerText;
                        break;
                    }
                case "post_code":
                    {
                        Aff.PostCode = Node.InnerText;
                        break;
                    }
                case "post_office_box":
                    {
                        Aff.PostOfficeBox = Node.InnerText;
                        break;
                    }
                case "province":
                    {
                        Aff.Province = Node.InnerText;
                        break;
                    }
                case "room":
                    {
                        Aff.Room = Node.InnerText;
                        break;
                    }
                case "state":
                    {
                        Aff.State = Node.InnerText;
                        break;
                    }

            }
        }
        private FileDetail  ProcessFileNode(XmlNode FileNode)
        {
            FileDetail _FileDetail   = new FileDetail ();
            XmlNode  file_originalname = FileNode.SelectSingleNode(".//file_originalname");
            XmlNode  attribute = FileNode.SelectSingleNode(".//attribute[@attr_name='File Designation']");

            if( file_originalname != null)
            {
             _FileDetail.FileName = file_originalname.InnerText;   
            }

            if (attribute != null)
            {
                _FileDetail.FileDescription =attribute.InnerText;
            }

            return _FileDetail;
            
        }
        private AuthorInfo  ProcessAuNode(XmlNode AuNode)
        {
            AuthorInfo Author = new AuthorInfo();

            if (AuNode.Attributes.GetNamedItem("corr") != null)
            {
                string Corr= AuNode.Attributes.GetNamedItem("corr").Value;
                if (Corr.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    Author.isCorr = true;
                }

            }

            XmlNodeList NL = AuNode.SelectNodes(".//*");
            foreach(XmlNode Node     in NL)
            switch (Node.Name)
            {
                case "affiliation":
                    {
                        Author.Aff = ProcessAffilation(Node);
                        break;
                    }
                case "attr_type":
                    {
                        break;
                    }
                case "attribute":
                    {
                        break;
                    }
                case "comments":
                    {
                        break;
                    }
                case "degree":
                    {
                        Author.Degree   = Node.InnerText;
                        break;
                    }
                case "email":
                    {
                        if (Node.Attributes.GetNamedItem("addr_type")!= null)
                        {
                            if (Node.Attributes.GetNamedItem("addr_type").Value.Equals("primary"))
                                Author.eMail = Node.InnerText;
                        }
                        break;
                    }
                case "first_name":
                    {
                        Author.FirstName = Node.InnerText;
                        break;
                    }
                case "flags":
                    {
                        break;
                    }
                case "last_name":
                    {
                        Author.LastName = Node.InnerText;
                        break;
                    }
                case "middle_name":
                    {
                        Author.MiddleName = Node.InnerText;
                        break;
                    }
                case "orcid":
                    {
                        break;
                    }
                case "researcher_id":
                    {
                        break;
                    }
                case "salutation":
                    {
                        Author.Salutation = Node.InnerText;
                        break;
                    }
                case "suffix":
                    {
                        Author.Suffix = Node.InnerText;
                        break;
                    }
            }
            return Author;
        }
        private void ProcessCustomFields(XmlNode Node)
        {
            if (Node.Attributes.GetNamedItem("cd_code") != null)
            {
                string cd_code=Node.Attributes.GetNamedItem("cd_code").Value;
                string cd_value = Node.Attributes.GetNamedItem("cd_value").Value;
                AssignValue(cd_code, cd_value);
            }
            
        }
        private void AssignValue(string cd_code ,string cd_value)
        {
            if (cd_code.Equals("Wiley - Total number of figures"))
            {
                _ArtclInfo.Figures = System.Text.RegularExpressions.Regex.Match(cd_value,"[0-9]+").Value;
            }
            else if (cd_code.Equals("Wiley - Number of tables"))
            {
                _ArtclInfo.Tables = cd_value;
            }
            else if (cd_code.Equals("Wiley - Number of manuscript pages"))
            {
                _ArtclInfo.MSS = System.Text.RegularExpressions.Regex.Match(cd_value, "[0-9]+").Value; ;
            }
            else if (cd_code.Equals("Wiley - Manuscript type"))
            {
                if (cd_value.Contains("-"))
                {
                    string[] ArrStr = cd_value.Split('-');
                    if (ArrStr[0].Trim().Length == 1)
                    {
                        cd_value = ArrStr[1].Trim();
                    }
                }
                _ArtclInfo.MSSType = cd_value;
            }

        }
        private string  SerializeToXML<T>(List<T> source)
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

            return SerializeXML.ToString().Replace("#$#","&");

        }
        public void InsertMetaData(string Clnt, string JID, string AID)
        { 
            List<ArticleInfo> AI = new List<ArticleInfo>();
            AI.Add(_ArtclInfo);
            string MetaDataXML=  SerializeToXML(AI);

            InsertInDataBase(Clnt, JID, AID, MetaDataXML);
        }
        public ArticleInfo GetArticleMetaData(string Clnt, string JID, string AID)
        {
            ArticleInfo AI = new ArticleInfo();
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfArticleInfo));
            string MetaXML = GetMetaDataFromDataBase(Clnt, JID, AID);

            if (string.IsNullOrEmpty(MetaXML))
            {
                return null;
            }

            string TempFolder = "c:\\Temp";

            if (!Directory.Exists(TempFolder))
                 Directory.CreateDirectory(TempFolder);

            string MetaFile = TempFolder + "\\" + DateTime.Today.Ticks.ToString() + ".xml";

            File.WriteAllText(MetaFile, MetaXML);

            using (FileStream fileStream = new FileStream(MetaFile, FileMode.Open))
            {
                ArrayOfArticleInfo ArtilcleDetailsList = (ArrayOfArticleInfo)serializer.Deserialize(fileStream);
                ArtilcleDetailsList.ArtclInfo= ArtilcleDetailsList.ArtclInfo;
                
                AI = ArtilcleDetailsList.ArtclInfo[0];
            }
            return AI;
        }
        private string GetMetaDataFromDataBase(string Client, string JID, string AID)
        {
            string MetaXML = string.Empty;
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConString);
            SqlCommand Com = new SqlCommand();
            try
            {
                Con.Open();
                if (Con.State == System.Data.ConnectionState.Open)
                {
                    Com.CommandText = "usp_GetMetaData";
                    Com.Connection = Con;
                    Com.CommandType = System.Data.CommandType.StoredProcedure;

                    Com.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 100));
                    Com.Parameters["@Client"].Value = Client;


                    Com.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 100));
                    Com.Parameters["@JID"].Value = JID;

                    Com.Parameters.Add(new SqlParameter("@AID", SqlDbType.VarChar, 100));
                    Com.Parameters["@AID"].Value = AID;

                    //Com.Parameters.Add(new SqlParameter("MetaXML", SqlDbType.Xml);
                    //Com.Parameters["@AID"].Value = AID;

                    SqlParameter OutPut = new SqlParameter("@ArticleInfo", SqlDbType.Xml,Int32.MaxValue);
                    OutPut.Direction = ParameterDirection.Output;
                    //OutPut.Value = new SqlXml(new XmlTextReader(MetaXML, XmlNodeType.Document, null));
                    Com.Parameters.Add(OutPut);

                    Com.ExecuteNonQuery();
                    Con.Close();

                    MetaXML = OutPut.Value.ToString();
                }

            }
            catch (SqlException Ex)
            {

            }
            finally
            {
                Con.Dispose();
                Com.Dispose();
            }
            return MetaXML;
        }
        private void InsertInDataBase(string Client, string JID,string AID , string MetaXML)
        {
            string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConString);
            SqlCommand Com = new SqlCommand();
            try
            {
                Con.Open();
                if (Con.State == System.Data.ConnectionState.Open)
                {
                    Com.CommandText = "usp_InsertMetaData";
                    Com.Connection = Con;
                    Com.CommandType = System.Data.CommandType.StoredProcedure;

                    Com.Parameters.Add(new SqlParameter("@Client", SqlDbType.VarChar, 100));
                    Com.Parameters["@Client"].Value = Client;


                    Com.Parameters.Add(new SqlParameter("@JID", SqlDbType.VarChar, 100));
                    Com.Parameters["@JID"].Value = JID;

                    Com.Parameters.Add(new SqlParameter("@AID", SqlDbType.VarChar, 100));
                    Com.Parameters["@AID"].Value = AID;

                    //Com.Parameters.Add(new SqlParameter("MetaXML", SqlDbType.Xml);
                    //Com.Parameters["@AID"].Value = AID;

                    SqlParameter param = new SqlParameter("@ArticleInfo", SqlDbType.Xml);
                    param.Value = new SqlXml( new XmlTextReader(MetaXML,XmlNodeType.Document,null));
                    Com.Parameters.Add(param);

                    Com.ExecuteNonQuery();
                    Con.Close();
                   
                }
                
            }
            catch (SqlException Ex)
            {
                
            }
            finally
            {
                Con.Dispose();
                Com.Dispose();
            }
        }
    }

    public class ArrayOfArticleInfo
    {
        [XmlElement("ArticleInfo")]
        public List<ArticleInfo> ArtclInfo { get; set; }
    }

    [Serializable]
    public class ArticleInfo
    {
        List<FileDetail> _FileList       = new List<FileDetail> ();
        List<AuthorInfo> _Authors = new List<AuthorInfo>();

        string _RefCode         = string.Empty;
        string _JournalTitle    = string.Empty;
        string _JournalABBR     = string.Empty;
        string _PISSN           = string.Empty;
        string _EISSN           = string.Empty;
        string _ArticleStatus   = string.Empty;
        string _Articletitle    = string.Empty;
        string _ArticleSubTitle = string.Empty;

        History _History = new History();
        string _Abstract = string.Empty;
        string _Figures  = string.Empty;
        string _ColorFigures      = string.Empty;
        string _Tables            = string.Empty;
        string _MSS               = string.Empty;
        string _MSSType           = string.Empty;
        string _PublicationType   = string.Empty;

        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        public string JournalTitle   
        {
            get { return _JournalTitle; }
            set { _JournalTitle = value; }
        }
        public string PublicationType
        {
            get { return _PublicationType; }
            set { _PublicationType = value; }
        }
        public string JID    
        {
            get { return _JournalABBR; }
            set { _JournalABBR = value; }
        }
        public string PISSN          
        {
            get { return _PISSN; }
            set { _PISSN = value; }
        }
        public string EISSN          
        {
            get { return _EISSN; }
            set { _EISSN = value; }
        }
        public string ArticleStatus
        {
            get { return _ArticleStatus; }
            set { _ArticleStatus = value; }
        }
        public string Articletitle
        {
            get { return _Articletitle; }
            set { _Articletitle = value; }
        }
        public string ArticleSubTitle
        {
            get { return _ArticleSubTitle; }
            set { _ArticleSubTitle = value; }
        }
        public List<AuthorInfo> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }
        public List<FileDetail> FileList
        {
            get { return _FileList; }
            set { _FileList = value; }
        }
        public History HistoryDate
        {
            get { return _History; }
            set { _History = value; }
        }
        public string Abstract
        {
            get { return _Abstract; }
            set { _Abstract = value; }
        }
        public string Figures
        {
            get { return _Figures; }
            set { _Figures = value; }
        }
        public string ColorFigures
        {
            get { return _ColorFigures; }
            set { _ColorFigures = value; }
        }
        public string Tables
        {
            get { return _Tables; }
            set { _Tables = value; }
        }
        public string MSS
        {
            get { return _MSS; }
            set { _MSS = value; }
        }
        public string MSSType
        {
            get { return _MSSType; }
            set { _MSSType = value; }
        }
    }
    public class AuthorInfo 
    {
        bool _isCorr = false;
        string _Salutation = string.Empty;
        string _FirstName = string.Empty;
        string _MiddleName = string.Empty;
        string _LastName = string.Empty;
        string _Suffix = string.Empty;
        string _Degree = string.Empty;
        //string _Inst = string.Empty;
        //string _Dept = string.Empty;
        //string _City = string.Empty;
        //string _CountryCode = string.Empty;
        //string _Country = string.Empty;
        //string _PostCode = string.Empty;
        //string _Phone = string.Empty;
        //string _Fax = string.Empty;
        string _eMail = string.Empty;


        Affilation _Aff = new Affilation();

        public Affilation Aff
        {
            set { _Aff = value; }
            get { return _Aff; }
        }

        public bool isCorr
        {
            get { return _isCorr; }
            set { _isCorr = value; }
        }

        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        public string Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        public string Degree
        {
            get { return _Degree; }
            set { _Degree = value; }
        }
        //public string Inst
        //{
        //    get { return _Inst; }
        //    set { _Inst = value; }
        //}
        //public string Dept
        //{
        //    get { return _Dept; }
        //    set { _Dept = value; }
        //}
        //public string City
        //{
        //    get { return _City; }
        //    set { _City = value; }
        //}
        //public string CountryCode
        //{
        //    get { return _CountryCode; }
        //    set { _CountryCode = value; }
        //}
        //public string Country
        //{
        //    get { return _Country; }
        //    set { _Country = value; }
        //}
        //public string PostCode
        //{
        //    get { return _PostCode; }
        //    set { _PostCode = value; }
        //}
        //public string Phone
        //{
        //    get { return _Phone; }
        //    set { _Phone = value; }
        //}
        //public string Fax
        //{
        //    get { return _Fax; }
        //    set { _Fax = value; }
        //}


    }
    public class DatePart   
    {
        string _Year   = string.Empty;
        string _Month  = string.Empty;
        string _Day    = string.Empty;
        string _Hour   = string.Empty;
        string _Minute = string.Empty;
        string _Second = string.Empty;


        public DatePart()
        {}
        public DatePart(string Year,String Month , string Day)
        {
            _Year  = Year;
            _Month = Month ;
            _Day   = Day;
        }

        public string Year 
        {
            get{return _Year;}
            set{_Year = value;}
        }
        
        public string Month 
        {
            get{return _Month;}
            set{_Month = value;}
        }
        
        public string Day 
        {
            get{return _Day;}
            set{_Day = value;}
        }
        
        public string Hour
        {
            get{return _Hour;}
            set{_Hour = value;}
        }
        
        public string Minute
        {
            get{return _Minute;}
            set{_Minute = value;}
        }
        
        public string Second 
        {
            get{return _Second;}
            set{_Second = value;}
        }
    }
    public class History    
    {
        DatePart _ReceivedDate = new DatePart();
        DatePart _RevisedDate  = new DatePart();
        DatePart _AcceptedDate = new DatePart();

        public DatePart ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate= value ; }
        }

        public DatePart RevisedDate
        {
            get { return _RevisedDate; }
            set { _RevisedDate = value; }
        }

        public DatePart AcceptedDate
        {
            get { return _AcceptedDate; }
            set { _AcceptedDate = value; }
        }
    }
    public class Affilation 
    {
        string _CountryCode = string.Empty;
        string _Inst = string.Empty;
        string _Dept = string.Empty;
        string _PersonTitle = string.Empty;
        string _Room = string.Empty;
        string _Building = string.Empty;
        string _Addr1 = string.Empty;
        string _Addr2 = string.Empty;
        string _Addr3 = string.Empty;
        string _PostOfficeBox = string.Empty;
        string _City = string.Empty;
        string _State = string.Empty;
        string _Province = string.Empty;
        string _Country = string.Empty;
        string _PostCode = string.Empty;
        string _Phone = string.Empty;
        string _Fax = string.Empty;


        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string Inst
        {
            get { return _Inst; }
            set { _Inst = value; }
        }
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }
        public string PersonTitle
        {
            get { return _PersonTitle; }
            set { _PersonTitle = value; }
        }
        public string Room
        {
            get { return _Room; }
            set { _Room = value; }
        }
        public string Building
        {
            get { return _Building; }
            set { _Building = value; }
        }
        public string Addr1
        {
            get { return _Addr1; }
            set { _Addr1 = value; }
        }
        public string Addr2
        {
            get { return _Addr2; }
            set { _Addr2 = value; }
        }
        public string Addr3
        {
            get { return _Addr3; }
            set { _Addr3 = value; }
        }
        public string PostOfficeBox
        {
            get { return _PostOfficeBox; }
            set { _PostOfficeBox = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
    }

    public class FileDetail
    {
        string _FileDescription = string.Empty;
        string _FileName = string.Empty;

        public FileDetail()
        {

        }
        public FileDetail(string FileName,string FileDescription)
        {
            _FileName        = FileName;
            _FileDescription = FileDescription;
        }
        public string FileName
        {
            get {return _FileName;}
            set {_FileName = value;}
        }
        public string FileDescription
        {
            get {return _FileDescription;}
            set {_FileDescription = value;}
        }
    }
}
