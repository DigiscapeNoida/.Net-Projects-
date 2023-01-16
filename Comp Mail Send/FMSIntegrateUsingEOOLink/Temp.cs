using System;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class Temp
    {
        string _XmlPath = string.Empty;
        string _JID   = string.Empty;
        string _AID   = string.Empty;
        string _DOI   = string.Empty;
        string _VOL   = string.Empty;
        string _Year  = string.Empty;

        string _Stage       = string.Empty;
        string _MailSubject = "";
        string _MailBody = string.Empty;

        public Temp(string JID, string XmlPath, string Stage,string Year)
        {
            _JID = JID;
            _XmlPath = XmlPath;
            _Stage = Stage;
            _Year = Year;
        }

        public void InsertEfirstDetail()
        {
            string           _ConnectionString    = string.Empty;
           _ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlParameter[] para = new SqlParameter[7];

            para[0] = new SqlParameter("@JID",    _JID.Trim());
            para[1] = new SqlParameter("@AID",    _AID.Trim());
            para[2] = new SqlParameter("@DOI",    _DOI.Trim());
            para[3] = new SqlParameter("@Volume", _VOL.Trim());
            para[4] = new SqlParameter("@Year",   _Year.Trim());
            para[5] = new SqlParameter("@Stage",  _Stage.Trim());
            para[6] = new SqlParameter("@MailSubjectLine", _MailSubject.Trim());

            try
            {
                SqlHelper.ExecuteNonQuery(_ConnectionString, System.Data.CommandType.StoredProcedure, "usp_InsetThiemeOffPrintDetail", para);
            }
            catch (SqlException Ex)
            {
            }
            catch (Exception ex)
            {
            }
        }


        public void GetArticleDteail()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_XmlPath);

            //XmlNode email = xDoc.SelectSingleNode(".//corresp/email");
            //XmlNode fullname = xDoc.SelectSingleNode(".//corresp/fullname");

            XmlNode volume = xDoc.SelectSingleNode(".//article-meta/volume");
            //XmlNode issue = xDoc.SelectSingleNode(".//article-meta/issue");

            

            if (volume != null)
            {
                _VOL = volume.InnerText;
            }
            string DOI_AID = Path.GetFileNameWithoutExtension(_XmlPath);
            if (DOI_AID.IndexOf("_") != -1)
            {
                string[] Arr = DOI_AID.Split('_');
                _DOI = Arr[0];
                _AID = Arr[1];
            }
            else
            {
                XmlNode ArticleID = xDoc.SelectSingleNode(".//article-id[@pub-id-type='manuscript']");
                XmlNode ArticleDOI = xDoc.SelectSingleNode(".//article-id[@pub-id-type='doi']");
                if (ArticleID != null)
                {
                    _AID = ArticleID.InnerText;
                }
            }
        }
    }
}

