using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
namespace OrderViewer
{
    public class OrderInfo
    {
        StringCollection Temp = new StringCollection();
        List<StageInfo> _Stages = new List<StageInfo>();

        public static string ServerPath
        { get;set; }
        
        public List<StageInfo> Stages
        {get { return _Stages; }}

        public string InputPath
        { get;set; }
        public OrderInfo() { }
        public OrderInfo(string InPutFolder) 
        {
            InputPath = InPutFolder;
            ProcessAllXml();
        }

        public void  ProcessAllXml()
        {
                if (!Directory.Exists(InputPath)) return;
                string [] FL= Directory.GetFiles(InputPath,"*.xml", SearchOption.AllDirectories);
                Array.Sort(FL);
                Array.Reverse(FL);
                string Stg="";
                foreach(string XmlFile in FL)
                {
                    if (XmlFile.IndexOf("old") == -1)
                    {
                        Stg = GetStage(XmlFile);
                        if (!Stg.Equals("") && Stg.IndexOf("old") == -1)
                        {
                            if (Temp.IndexOf(Stg) == -1)
                            {
                                Temp.Add(Stg);
                                _Stages.Add(new StageInfo(Stg, XmlFile));
                            }
                        }
                    }
                    
                }
        }
        private string GetStage(string XmlFile)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.XmlResolver = null;
            xDoc.LoadXml(File.ReadAllText(XmlFile ).Replace("EMCOrder10.dtd", ServerPath + "\\xslt\\EMCOrder10.dtd").Replace("&","#$#"));

            XmlNode Node = xDoc.GetElementsByTagName("stage")[0];

            if (Node.Attributes.GetNamedItem("step") != null)
            {
                return Node.Attributes.GetNamedItem("step").Value;
            }
            return "";
        }

    }

    public class StageInfo
    {
        public  StageInfo()
        {}

        public  StageInfo(string Stage, string XMLFilePath)
        {
            StageName = Stage;
            XMLPath   = XMLFilePath;
        }

        public string StageName
        { get; set; }
        public string XMLPath
        { get; set; }

    }
   
}

