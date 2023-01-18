using System;
using System.IO;
using ProcessNotification;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWeProof
{
    interface IGoXml
    {

        string archivefile { get; set; }
        string Guid      { get; set; }
        string AID       { get; set; }
        string DOI       { get; set; }
        string TaskName  { get; set; }
        string JID       { get; set; }
        string GoXMLString     { get; set; }
        string DueDate   { get; set; }

    }
    class GoXML:MessageEventArgs, IGoXml
    {
        XmlDocument _xDoc = new XmlDocument();
        public GoXML(string GoXMLStr)
        {

            GoXMLString = GoXMLStr;
           

        }

        public bool ProcessGoXml()
        {
            if (string.IsNullOrEmpty(GoXMLString))
            {
                return false;
            }

            try
            {
                try
                {
                _xDoc.XmlResolver = null;
                _xDoc.PreserveWhitespace = true;
                _xDoc.LoadXml(GoXMLString);
                }
                catch(XmlException ex)
                {
                    ProcessErrorHandler(ex);
                }

                ProcessEventHandler("Getting ParamList");
                XmlNodeList ParamList = _xDoc.GetElementsByTagName("parameter");
                foreach (XmlNode param in ParamList)
                {
                    if (param.Attributes.GetNamedItem("name") != null && param.Attributes.GetNamedItem("value") != null)
                    {
                        string ParamName = param.Attributes.GetNamedItem("name").Value;
                        string ParamValue = param.Attributes.GetNamedItem("value").Value;

                        ProcessEventHandler("SetParamValue ParamName == ParamValue");
                        ProcessEventHandler(ParamName  + "=="+ ParamValue);
                        SetParamValue(ParamName, ParamValue);
                    }
                }

                ProcessEventHandler("Getting archive-file");
                XmlNodeList archivefileList = _xDoc.GetElementsByTagName("archive-file");
                if (archivefileList.Count > 0)
                {
                    if (archivefileList[0].Attributes.GetNamedItem("name") != null)
                        archivefile = archivefileList[0].Attributes.GetNamedItem("name").Value.ToUpper().Trim();

                    ProcessEventHandler ("archivefile" + archivefile);
                }


                ProcessEventHandler("Getting JID");
                XmlNodeList JournalList = _xDoc.GetElementsByTagName("journal");
                if (JournalList.Count > 0)
                {
                    if (JournalList[0].Attributes.GetNamedItem("code") != null)
                        JID = JournalList[0].Attributes.GetNamedItem("code").Value.ToUpper().Trim();

                    ProcessEventHandler ("JID" + JID);
                }

                XmlNodeList FileList = _xDoc.GetElementsByTagName("file");
                foreach (XmlNode FileNode in FileList)
                {

                    if (FileNode.Attributes.GetNamedItem("name") != null)
                    {
                        string FileName = FileNode.Attributes.GetNamedItem("name").Value;
                        string ext = Path.GetExtension(FileName);
                        
                        if (string.IsNullOrEmpty(AID) && ext.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            string AIDPdf = Path.GetFileNameWithoutExtension(FileName);
                            if (string.IsNullOrEmpty(AID))
                                AID = AIDPdf;
                            else if (AIDPdf.Length < AID.Length)
                            {
                                AID = AIDPdf;
                            }
                        }
                    }
                }
              

            }
            catch (XmlException ex)
            {
                
                return false;
            }
          
            return true;
        }
        private void SetParamValue(string ParamName, string ParamValue)
        {
            switch (ParamName)
            {
                case "production-task-id":
                    {
                        Guid = ParamValue;
                        break;
                    }
                case "production-task-name":
                    {
                        TaskName = ParamValue;
                        break;
                    }
                case "production-task-due-date":
                    {
                        DueDate = ParamValue;
                        break;
                    }
                case "DOI":
                    {
                        DOI = ParamValue;
                        break;
                    }
                case "manuscript-number":
                    {
                        AID = ParamValue.Replace(" ", "");
                        break;
                    }

            }
        }


        public string Guid
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

        public string TaskName
        {
            get;
            set;
        }

        public string JID
        {
            get;
            set;
        }

        public string GoXMLString
        {
            get;
            set;
        }

        public string DueDate
        {
            get;
            set;
        }

        public string archivefile
        {
            get;
            set;
        }
    }
}
