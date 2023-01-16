using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using MsgRcvr;
using ProcessNotification;
using System.Data.SqlClient;
using System.Data;
namespace AutoEproof
{

    //\\172.16.3.39\tdxps\NIH\Abbreviation
    //\\172.16.3.39\tdxps\NIH\Complete
    //\\td-nas\W-Input\Downloads\Material Input\US Materials
    //\\td-nas\W-Input\Downloads\Material Input\UK Materials
    //\\td-nas\W-Input\Downloads\Material Input\VCH Materials
    //\\td-nas\W-Input\Downloads\Material Input\Singapore Materials

    class NIHProcess:MessageEventArgs
    {
        MNTInfo _MNT       = null;
        string _XMLPath    = string.Empty;
        string _InputZip   = string.Empty;
        string _OutZip = string.Empty;
        string _NIHFullOut = string.Empty;
        string _NIHAbrOut  = string.Empty;

        ArticleInfo _ArticleInfoOBJ = new ArticleInfo();
        XmlDocument _XmlDoc = new XmlDocument();
        public ArticleInfo ArticleInfoOBJ
        {
            get { return _ArticleInfoOBJ; }
        }
        public NIHProcess(string XMLPath, MNTInfo MNT)
        {
            _MNT     = MNT;
            _XMLPath = XMLPath;
           
        }
        public  string InputPath
        {
            get;
            set;
        }
        private string[] NIHFullValues
        {
            get;
            set;
        }
        private string[] NIHAbrValues
        {
            get;
            set;
        }
        private void Intialize()
        {
            string ListDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string NIHFull = Path.Combine(ListDirectory, "NIHFull.txt");
            string NIHAbr = Path.Combine(ListDirectory, "NIHAbr.txt");

            ProcessEventHandler("NIHFull Values listing");

            if (File.Exists(NIHFull))
                NIHFullValues = File.ReadAllLines(NIHFull);


            ProcessEventHandler("NIHAbr Values listing");

            if (File.Exists(NIHAbr))
                NIHAbrValues = File.ReadAllLines(NIHAbr);

            ProcessEventHandler("SetInputOutZipPath");
            SetInputOutZipPath();
        }

        public void StartProess()
        {
            ProcessEventHandler("Intialize Values");
            Intialize();
            SearchInFile();
            CopyZIp();
            GetInfoForMail();
        }
        private void SearchInFile()
        {
           bool isAbbrFound = false;
           bool isFullFound = false;
           try
           {                  
                 XmlDocument Xdoc = new XmlDocument();
                 Xdoc.XmlResolver = null;
                 Xdoc.Load(_XMLPath) ;                 

                 XmlNodeList xNodelst =   Xdoc.GetElementsByTagName("body");
                 XmlNode BodyNode = xNodelst[0];
                 //Code for remove bibliography tag
                 StringBuilder BodyText2 = new StringBuilder();
                 XmlNodeList nodelist = BodyNode.ChildNodes;
                 foreach (XmlNode nod in nodelist)
                 {
                     if (nod.Name != "bibliography")
                     {
                         BodyText2.Append(nod.InnerText);
                     }
                 }

                 string BodyText = BodyText2.ToString();
                 // string BodyText = BodyNode.InnerText;

                foreach (string NIHFullValue in NIHFullValues)
                {
                    if (BodyText.IndexOf( NIHFullValue,StringComparison.OrdinalIgnoreCase)!= -1)
                    {
                       isFullFound = true ;
                       break;
                    }
                }
                foreach (string NIHAbrValue in NIHAbrValues)
                {
                    if (BodyText.IndexOf( " " + NIHAbrValue + " ",StringComparison.OrdinalIgnoreCase)!= -1)
                    {
                       isAbbrFound = true ;
                       break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);   
            }

           if (isAbbrFound)
               _OutZip = _NIHAbrOut;
           if (isFullFound)
               _OutZip = _NIHFullOut;
        }
        private void CopyZIp()
        {
            if (!string.IsNullOrEmpty(_InputZip) && ! string.IsNullOrEmpty(_OutZip))
            {

                string Dest = Path.GetDirectoryName(_OutZip);
                if (!(Directory.Exists(Dest)))
                {
                    Directory.CreateDirectory(Dest);
                }

                _OutZip = _OutZip + "\\" + _MNT.JID+ "_" + _MNT.AID + ".zip";

                ProcessEventHandler("InputZip : " + _InputZip);
                ProcessEventHandler("OutZip : " + _OutZip);
                File.Copy(_InputZip, _OutZip,true);
            }
        }
        private void SetInputOutZipPath()
        {
            _NIHFullOut = System.Configuration.ConfigurationManager.AppSettings["NIHComplete"]; 
            _NIHAbrOut = System.Configuration.ConfigurationManager.AppSettings["NIHAbbreviation"];

            ProcessEventHandler("NIHFullOut" + _NIHFullOut);
            ProcessEventHandler("NIHAbrOut" + _NIHAbrOut);

            string ClntKey       = _MNT.Client + "Materials";
            string MaterialPath = System.Configuration.ConfigurationManager.AppSettings[ClntKey];

            ProcessEventHandler("ClntKey : " + ClntKey);

            if (!string.IsNullOrEmpty(MaterialPath))
            {
                string JIDAIDFolder = MaterialPath + "\\" + _MNT.JID + "\\" + _MNT.AID + "\\Fresh";
                if (Directory.Exists(JIDAIDFolder))
                {
                    ProcessEventHandler("JIDAIDFolder : " + JIDAIDFolder);
                    string[] Zips = Directory.GetFiles(JIDAIDFolder, "*.zip");
                    if (Zips.Length > 0)
                    {
                        _InputZip = Zips[0];
                    }

                    ProcessEventHandler("InputZip : " + _InputZip);
                }
            }
            else
            {
                ProcessEventHandler("MaterialPath does not exist : " + MaterialPath);
            }
        }

        private void GetInfoForMail()
        {
            if (!string.IsNullOrEmpty(_OutZip))
            {
                if (File.Exists(_XMLPath))
                {
                    string XML = File.ReadAllText(_XMLPath);
                    int sPos = XML.IndexOf("<header");
                    int ePos = sPos != -1 ? XML.IndexOf("<body", sPos) + 1 : 0;


                    if (sPos > 0 && ePos > 0)
                    {
                        string XmlStr = ePos > 0 ? XML.Substring(sPos, ePos - sPos - 1) : "";
                        try
                        {
                            if (ePos > 0)
                                _XmlDoc.LoadXml(XmlStr.Replace("wiley:", "").Replace("&", "#$#"));
                            else
                                _XmlDoc.LoadXml(_XMLPath);

                            RemoveComment();
                        }
                        catch (XmlException ex)
                        {
                            ProcessEventHandler("Error :: XML could not be load.");
                            ProcessEventHandler("Error Message:: " + ex.Message);
                            return;
                        }
                        try
                        {
                            GetAuthorname();
                            GetDOI();
                            GetArticleTitle();
                            String connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(connString))
                            {
                                SqlCommand command = new SqlCommand("InsertNIHDetails", conn);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Client", _MNT.Client));
                                command.Parameters.Add(new SqlParameter("@JID", _MNT.JID));
                                command.Parameters.Add(new SqlParameter("@AID", _MNT.AID));
                                command.Parameters.Add(new SqlParameter("@ZipName", _OutZip));
                                command.Parameters.Add(new SqlParameter("@DOI", ArticleInfoOBJ.DOI));
                                if (_OutZip.Contains("Complete"))
                                    command.Parameters.Add(new SqlParameter("@IsComplete", true));
                                else
                                    command.Parameters.Add(new SqlParameter("@IsComplete", false));
                                command.Parameters.Add(new SqlParameter("@CORRAuthor", ArticleInfoOBJ.Authors));
                                command.Parameters.Add(new SqlParameter("@ArticleTitle", ArticleInfoOBJ.ArticleTitle));

                                command.Parameters.Add(new SqlParameter("@FTPStatus", false));
                                command.Parameters.Add(new SqlParameter("@EmailStatus", false));
                                if (conn.State == ConnectionState.Closed)
                                    conn.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception e)
                        {
                            ProcessEventHandler("Error Message:: " + e.Message);
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    ProcessEventHandler("Error :: " + _XMLPath + " does not exist.");
                }
            }
        }

        private void GetAuthorname()
        {
            XmlNodeList CreatorNodes = _XmlDoc.GetElementsByTagName("creator");

            if (CreatorNodes.Count == 0)
            {
                return;
            }

            foreach (XmlNode Creator in CreatorNodes)
            {
                if (Creator.Attributes.GetNamedItem("correspondenceRef") != null && Creator.Attributes.GetNamedItem("creatorRole") != null)
                {
                    ProcessCreator(Creator);
                    break;
                }
            }

            foreach (XmlNode Creator in CreatorNodes)
            {
                XmlNode PersonGroup = Creator.SelectSingleNode(".//personName");

                if (PersonGroup == null)
                {
                    continue;
                }
                string gN = "";
                string fN = "";
                foreach (XmlNode chNode in PersonGroup)
                {

                    if (chNode.Name.Equals("givenNames"))
                        gN = chNode.InnerText;
                    else if (chNode.Name.Equals("familyName"))
                        fN = chNode.InnerText;
                }
                string AuthorName = gN + " " + fN;

                _ArticleInfoOBJ.Authors = " " + _ArticleInfoOBJ.Authors + AuthorName + ", ";
            }

            if (!string.IsNullOrEmpty(ArticleInfoOBJ.AuthorName))
                _ArticleInfoOBJ.Authors = _ArticleInfoOBJ.Authors.Trim(new char[] { ',', ' ' });

            if (string.IsNullOrEmpty(ArticleInfoOBJ.AuthorName) && CreatorNodes.Count == 1)
            {
                ProcessCreator(CreatorNodes[0]);
            }

            if (string.IsNullOrEmpty(ArticleInfoOBJ.AuthorName))
            {
                ProcessEventHandler("Error :: Author name could not be get.");
                ProcessEventHandler("Error :: Please check correspondenceRef and creatorRole attribute in creator element.");
            }
        }

        private void ProcessCreator(XmlNode Creator)
        {
            try
            {
                string gN = "";
                string fN = "";

                XmlNode PersonGroup = Creator.SelectSingleNode(".//personName");

                if (PersonGroup == null)
                {
                    return;
                }

                foreach (XmlNode chNode in PersonGroup)
                {
                    if (chNode.Name.Equals("givenNames"))
                        gN = chNode.InnerText;
                    else if (chNode.Name.Equals("familyName"))
                        fN = chNode.InnerText;
                }
                string AuthorName = gN + " " + fN;

                if (!string.IsNullOrEmpty(AuthorName))
                {
                    ArticleInfoOBJ.AuthorName = Filter(AuthorName);

                    if (Creator.SelectNodes(".//email") != null && Creator.SelectNodes(".//email").Count > 0)
                    {
                        string AuthorEmail = Creator.SelectNodes(".//email")[0].InnerText.Trim();
                        ArticleInfoOBJ.AuthorEmail = AuthorEmail.Trim(new char[] { ' ', ':', '(', ')', '"', '[', ']' });
                    }
                }
            }
            catch (XmlException ex)
            {
                ProcessErrorHandler(ex);
            }

        }
        private void GetArticleTitle()
        {
            XmlNodeList ContentMetaList = _XmlDoc.GetElementsByTagName("contentMeta");
            XmlNode ContentMeta = ContentMetaList[0];
            XmlNode Title = ContentMeta.SelectSingleNode(".//title[@type='main']");
            if (Title != null)
            {
                string ArtTitle = Title.InnerXml;
                ArtTitle = Filter(ArtTitle);
                ArticleInfoOBJ.ArticleTitle = ArtTitle;
            }
            else
            {
                ProcessEventHandler("Error ::  Article title does not exist.");
            }

        }
        private static string Filter(string PrcsString)
        {
            PrcsString = PrcsString.Replace("#$#", "&");
            PrcsString = PrcsString.Replace("<fc>", "");
            PrcsString = PrcsString.Replace("</fc>", "");

            PrcsString = PrcsString.Replace("&#x201D;", "\"");
            PrcsString = PrcsString.Replace("&#x201C;", "\"");
            PrcsString = PrcsString.Replace("&#x2010;", "-");
            PrcsString = PrcsString.Replace("&#x2013;", "-");
            PrcsString = PrcsString.Replace("&#x0026;", "&amp;"); ;
            PrcsString = PrcsString.Replace("&#x0027;", "&#39;");
            return PrcsString;
        }

        private void GetDOI()
        {
            XmlNodeList doi = _XmlDoc.GetElementsByTagName("doi");
            if (doi != null)
            {
                foreach (XmlNode node in doi)
                {
                    if (node.InnerText.Contains(_MNT.JID) || node.InnerText.Contains(_MNT.JID.ToLower()))
                        ArticleInfoOBJ.DOI = node.InnerText;
                }
            }
            else
            {
                ProcessEventHandler("Error ::  Article title does not exist.");
            }
        }
        private void RemoveComment()
        {
            XmlNodeList list = _XmlDoc.SelectNodes("//comment()");
            foreach (XmlNode node in list)
            {
                node.ParentNode.RemoveChild(node);
            }

            list = _XmlDoc.GetElementsByTagName("//link");
            foreach (XmlNode node in list)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

    }
}

