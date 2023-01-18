using ProcessNotification;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWeProof
{
    class CreateGo:MessageEventArgs
    {
        XmlDocument _xDoc = new XmlDocument();


        public string GoXMLString
        {
            get
            {
                if (_xDoc!=null)
                    return _xDoc.OuterXml; 
                else
                    return string.Empty; 
            }
        }
      

        public CreateGo(string GoXMLString)
        {
            
            if (!string.IsNullOrEmpty(GoXMLString))
            {
                try
                {
                    _xDoc.XmlResolver = null;
                    _xDoc.PreserveWhitespace = true;
                    _xDoc.LoadXml(GoXMLString);
                }
                catch (XmlException ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
        }
        public void SetFileGroup(string[] AppendFiles)
        {
            ProcessEventHandler("Getting filegroup node..");
            XmlNodeList filegroupList = _xDoc.GetElementsByTagName("filegroup");
            if (filegroupList.Count > 0)
            {
                XmlNode filegroup = filegroupList[0];
                XmlNode importFile = null;
                if (filegroup != null)
                {
                    importFile = filegroup.SelectSingleNode("import-file");
                }
                
                filegroup.RemoveAll();
                ProcessEventHandler("Remove all child node filegroup node..");
                foreach (string fl in AppendFiles)
                {
                    XmlElement ele= _xDoc.CreateElement("file");
                    ele.SetAttribute("name",Path.GetFileName(fl));
                    filegroup.AppendChild(ele);
                }
                if (importFile != null)
                {
                    filegroup.AppendChild(importFile);
                }

                
            }
        }
    }
}
