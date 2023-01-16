using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    public class AuthorInfo
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;
        #region Declare Properties
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
        public string DOI
        {
            get;
            set;
        }
        public string AuthorName
        {
            get;
            set;
        }
        public string AuthorEmail
        {
            get;
            set;
        }
        public string VOL
        {
            get;
            set;
        }
        public string ISS
        {
            get;
            set;
        }
        #endregion

        #region Declare Constructor

        public AuthorInfo()
        { 

        }
        public AuthorInfo(string _Jid, string _Aid, string _DOI)
        {
            JID = _Jid.Trim();
            AID = _Aid;
            DOI = _DOI;
            GetAuthorDetails();
        }
        #endregion

        #region Get Author Details from Database
        public void GetAuthorDetails()
        {
            CorAuthorDetaill CorADobj = ThiemeDataProcess.GetCorAuthorDetails(JID, AID, DOI);
            if (CorADobj != null)
            {
                AuthorName = CorADobj.CorName;
                AuthorEmail = CorADobj.CorMail;
            }
        }

        public void GetAuthorDetails(string XMLFile)
        {
           XmlDocument xDoc = new XmlDocument();

           try
           {
               xDoc.Load(XMLFile);

               ProcessMessage("XML has been load successfully");
           }
           catch (Exception ex)
           {
               ErrorMessage(ex);
               return;
           }

           ProcessMessage("Getting email Node.");
           XmlNode email = xDoc.SelectSingleNode(".//corresp/email");

           ProcessMessage("Getting fullname Node.");
           XmlNode fullname = xDoc.SelectSingleNode(".//corresp/fullname");

           ProcessMessage("Getting volume Node.");
           XmlNode volume = xDoc.SelectSingleNode(".//article-meta/volume");

           ProcessMessage("Getting issue Node.");
           XmlNode issue = xDoc.SelectSingleNode(".//article-meta/issue");

           if (fullname != null)
           {
               AuthorName = fullname.InnerText;

               if (AuthorName.IndexOf(",") != -1)
               {
                   AuthorName = AuthorName.Substring(0, AuthorName.IndexOf(","));
                   ProcessMessage("AuthorName ::" + AuthorName);
               }
               else
               {
                   ProcessMessage("AuthorName :: Not Found"  );
               }
           }

           if (volume != null)
           {
               VOL = volume.InnerText;
               ProcessMessage("VOL :: " + VOL);
           }
           else
           {
               ProcessMessage("VOL :: Not Found");
           }
           if (issue != null)
           {
               ISS = issue.InnerText;
               ProcessMessage("ISS :: " + ISS);
           }
           else
           {
               ProcessMessage("ISS :: Not Found");
           }

           if (email != null)
           {
               AuthorEmail = email.InnerText.Trim();
               ProcessMessage("AuthorEmail :: " + AuthorEmail);
           }
           else
           {
               ProcessMessage("AuthorEmail :: Not Found");
           }


            
            string DOI_AID = Path.GetFileNameWithoutExtension(XMLFile);
            if (DOI_AID.IndexOf("_") != -1)
            {
                string[] Arr = DOI_AID.Split('_');
                DOI = Arr[0];
                AID = Arr[1];
            }
            else
            { 
                XmlNode ArticleID = xDoc.SelectSingleNode(".//article-id[@pub-id-type='manuscript']");
                XmlNode ArticleDOI = xDoc.SelectSingleNode(".//article-id[@pub-id-type='doi']");
                if (ArticleID != null)
                {
                    AID = ArticleID.InnerText;
                }
            }

            ProcessMessage("DOI :: " + DOI);
            ProcessMessage("AID :: " + AID);
        }

        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }
        #endregion
    }
}
