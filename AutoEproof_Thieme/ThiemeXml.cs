using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using ProcessNotification;
namespace AutoEproof
{
    public class ThiemeXml : MessageEventArgs
    {
                   
        private string _JID             = string.Empty;
        private string _XMLPath         = string.Empty;

        public ThiemeXml(string XmlPath)
        {
            
            _XMLPath        = XmlPath;
        }

        public bool FillArticleInfo(ArticleInfo _ArtObj)
        {
            bool Result = false;
            try
            {
                if (File.Exists(_XMLPath))
                {
                   
                     XmlDocument _Xdoc = new XmlDocument();

                     try
                     {
                        _Xdoc.Load(_XMLPath);
                         Result = true;
                     }
                     catch (XmlException ex)
                     {
                         ProcessErrorHandler(ex);
                     }

                     if (_Xdoc == null)
                     {
                         return Result;
                     }
                     _ArtObj.Authors = "";
                     XmlNode JornalTL     = _Xdoc.SelectSingleNode(".//journal-title");
                    _ArtObj.JournalTitle = JornalTL.InnerText;

                     XmlNode NLArtTL      = _Xdoc.SelectSingleNode(".//article-title");
                    _ArtObj.ArticleTitle = NLArtTL.InnerText;

                   // XmlNodeList NLContGrp = _Xdoc.GetElementsByTagName("contrib");  only front contrib-group should be taken
                    XmlNode NLContCont = _Xdoc.SelectSingleNode(".//contrib-group");
                    XmlNodeList NLContGrp = NLContCont.ChildNodes;
                    for (int i = 0; i < NLContGrp.Count; i++)
                    {
                        XmlNodeList CHContrib = NLContGrp[i].ChildNodes;
                        for (int j = 0; j < CHContrib.Count; j++)
                        {
                            if (CHContrib[j].Name.Equals("name"))
                            {
                                XmlNode AuthorName =CHContrib[j];

                                XmlNode FN = AuthorName.SelectSingleNode(".//given-names");
                                XmlNode SN = AuthorName.SelectSingleNode(".//surname");
                                XmlNode MN = AuthorName.SelectSingleNode(".//middle-name");

                                string FullName = string.Empty;

                                if (FN != null)
                                    FullName = FN.InnerText; 
                                if (MN != null)
                                    FullName = FullName + " "  +MN.InnerText; 
                                if (SN != null)
                                    FullName = FullName + " "  +SN.InnerText; 

                                _ArtObj.Authors = " " + _ArtObj.Authors + FullName + ", ";
                                
                            }
                            else if (CHContrib[j].Name.Equals("degrees"))
                            {
                                _ArtObj.Authors = _ArtObj.Authors.Trim(new char[] { ',' ,' '});
                                _ArtObj.Authors =_ArtObj.Authors + ", "  + CHContrib[j].InnerText+ " ";
                            }
                        }
                    }
                    _ArtObj.Authors = _ArtObj.Authors.Trim(new char[] { ',',' ' });

                    XmlNodeList NLCorr = _Xdoc.GetElementsByTagName("corresp");
                    for (int i = 0; i < NLCorr.Count; i++)
                    {
                        XmlNodeList CHCorres = NLCorr[i].ChildNodes;
                        for (int j = 0; j < CHCorres.Count; j++)
                        {
                            if (CHCorres[j].Name.Equals("fullname"))
                                _ArtObj.ContactData += CHCorres[j].InnerText + ", ";
                            else if (CHCorres[j].Name.Equals("institution"))
                                _ArtObj.ContactData += CHCorres[j].InnerText + ", ";
                            else if (CHCorres[j].Name.Equals("addr-line"))
                                _ArtObj.ContactData += CHCorres[j].InnerText + ", ";
                            else if (CHCorres[j].Name.Equals("country"))
                                _ArtObj.ContactData += CHCorres[j].InnerText + ", ";
                            else if (CHCorres[j].Name.Equals("email"))
                                _ArtObj.CorEmail += CHCorres[j].InnerText + ", ";
                        }
                    }

                    XmlNode DOINode = _Xdoc.SelectSingleNode(".//article-id[@pub-id-type='doi']");

                    if (DOINode != null)
                        _ArtObj.DOI = DOINode.InnerText;
                    else
                        _ArtObj.DOI = "";

                    if (_ArtObj.JournalTitle != "Organic Materials")
                        _ArtObj.ContactData = _ArtObj.ContactData.TrimEnd(new char[] { ',', ' ' });
                    _ArtObj.CorEmail = _ArtObj.CorEmail.TrimEnd(new char[] { ',', ' ' });   //remove , at end
                }
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return Result;
        }
    }
}
