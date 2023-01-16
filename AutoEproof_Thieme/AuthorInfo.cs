using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Configuration;
using System.Xml;
using ProcessNotification;
using MsgRcvr;

/// <summary>
/// Summary description for AuthorInfo
/// </summary>
/// 
namespace AutoEproof
{
    [Serializable]
    public class AuthorInfo
    {

        public AuthorInfo()
        {
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

        

    }

    public class ArticleXMLProcess:MessageEventArgs
    {
        string _FMSPATH = string.Empty;
        string _XMLPATH = string.Empty;
        string _Client = string.Empty;
        string _JID = string.Empty;
        string _AID = string.Empty;
        string _ArticleTitle = string.Empty;
        //string _AuthorName      = string.Empty;
        XmlDocument _XmlDoc = new XmlDocument();

        ArticleInfo _ArticleInfoOBJ = new ArticleInfo();

        public ArticleInfo ArticleInfoOBJ
        {
            get { return _ArticleInfoOBJ; }
        }

        public ArticleXMLProcess(string XMLPATH)
        {
            _XMLPATH = XMLPATH;
            if (!string.IsNullOrEmpty(_XMLPATH))
            {
                AssignXmlDoc();
                ProcessEventHandler("XML load Successfully");
            }
            else
            {
                ProcessEventHandler("Error :: " + "XML Path is empty");
            }

        }

        private void AssignXmlDoc()
        {

            if (File.Exists(_XMLPATH))
            {
                string XML = File.ReadAllText(_XMLPATH);
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
                            _XmlDoc.LoadXml(_XMLPATH);

                        RemoveComment();
                    }
                    catch (XmlException ex)
                    {
                        ProcessEventHandler("Error :: XML could not be load.");
                        ProcessEventHandler("Error Message:: " + ex.Message);
                        return;
                    }

                    GetAuthorname();

                    if (string.IsNullOrEmpty(ArticleInfoOBJ.AuthorEmail))
                        GetAuthorEmailID();

                    GetArticleTitle();
                    GetAJIMEditor();
                    string strquery = File.ReadAllText(_XMLPATH);
                    int strq = strquery.IndexOf("<query id=", 0);
                    if (strq != -1 )
                        ArticleInfoOBJ.IsQuerypage = true;
                    else
                        ArticleInfoOBJ.IsQuerypage = false;

                    int strgraphics = strquery.IndexOf("<abstract type=\"graphical\"", 0);
                    if (strgraphics != -1)
                        ArticleInfoOBJ.IsgraphicalAbs = true;
                    else
                        ArticleInfoOBJ.IsgraphicalAbs = false;
                }
                else
                {
                }
            }
            else
            {
                ProcessEventHandler("Error :: " + _XMLPATH + " does not exist.");
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

                 if (!string.IsNullOrEmpty(AuthorName) )
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
        private void GetAuthorEmailID()
        {
            XmlNode eMail = _XmlDoc.SelectSingleNode(".//email");
            if (eMail != null)
            {
                string AuthorEmail = Filter(eMail.InnerText);
                if (AuthorEmail.IndexOf(" ") != -1)
                {
                    AuthorEmail = AuthorEmail.Substring(AuthorEmail.IndexOf(" "));
                }
                ArticleInfoOBJ.AuthorEmail = AuthorEmail.Trim(new char[] { ' ', ':', '(', ')', '"', '[', ']' });
            }

            ////As requirement by Sonu Sir:-For Match all email ID with Database coressponding email ID
            //XmlNodeList eMail = _XmlDoc.GetElementsByTagName("email");
            //if (eMail != null && eMail.Count > 0)
            //{

            //    for (int j = 0; j < eMail.Count; j++)
            //    {
            //        if (eMail[j].Name.Equals("email"))
            //            ArticleInfoOBJ.AuthorEmail += eMail[j].InnerText + ", ";
            //    }

            //    ArticleInfoOBJ.AuthorEmail = ArticleInfoOBJ.AuthorEmail.TrimEnd(new char[] { ',', ' ' });
            //}
            //else
            //    ArticleInfoOBJ.AuthorEmail = string.Empty;

        }
        private void GetAJIMEditor()
        {
            try
            {
                string XML = File.ReadAllText(_XMLPATH);
                int sPos = XML.IndexOf("<body");
                int sPos2 = XML.IndexOf(">", sPos);
                int ePos = sPos != -1 ? XML.IndexOf("<bibliography", sPos) + 1 : 0;
                if (sPos > 0 && ePos > 0)
                {
                    string XmlStr = ePos > 0 ? XML.Substring(sPos2 + 1, ePos - sPos2 - 1) : "";
                    int str = XmlStr.IndexOf("DISCLOSURE BY AJIM EDITOR OF RECORD</title>");
                    int str2 = XmlStr.IndexOf("</p>", str);
                    string XmlStr2 = str > 0 ? XmlStr.Substring(str + 1, str2 - str - 1) : "";
                    if (XmlStr2.ToLower().Contains("rodney ehrlich") || XmlStr2.ToLower().Contains("paul landsbergis"))
                        _ArticleInfoOBJ.AJIMEditor = "rodney ehrlich";
                    else if (XmlStr2.ToLower().Contains("john meyer md") || XmlStr2.ToLower().Contains("john meyer") || XmlStr2.ToLower().Contains("steven b. markowitz") || XmlStr2.ToLower().Contains("steven markowitz"))  //as per request 26/11/2018 by Rinchen
                        _ArticleInfoOBJ.AJIMEditor = "john meyer";
                    else
                        _ArticleInfoOBJ.AJIMEditor = "";
                }
                else
                {
                    _ArticleInfoOBJ.AJIMEditor = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

}