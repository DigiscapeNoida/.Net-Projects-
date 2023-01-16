using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAontentReport
{
    class ProcessACR
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        string _ExlFile = string.Empty;
        string _XMLFile = string.Empty;

        List<ACRDetail> _ArticleACRDetailList = new List<ACRDetail>();
        public ProcessACR()
        {
        }

        public ProcessACR(string InputExl)
        {
            ProcessMessage("Input Excel File :: " + InputExl);
            if (File.Exists(InputExl))
            {
                _ExlFile = InputExl;
                _XMLFile = Path.ChangeExtension(InputExl, ".xml");

                ProcessMessage("Input XML File :: " + _XMLFile);

                ExcelSheet.ProcessExcel PrcsExl = new ExcelSheet.ProcessExcel();

                ProcessMessage("Start Convert to XML File");

                PrcsExl.ConvertToXML(_ExlFile);

                ProcessMessage("XML file converted");
            }
        }
        public List<ACRDetail> ArticleACRDetailList
        {
            get { return _ArticleACRDetailList; }
        }
        public void StartProcss()
        {
            if (File.Exists(_XMLFile))
            {
                XmlDocument xDoc = new XmlDocument();

                ProcessMessage("Load XML File");
                xDoc.Load(_XMLFile);

                ProcessMessage("Getting ROWS from XML File");
                XmlNodeList NL = xDoc.GetElementsByTagName("Row");
                List<XmlNode> Rows = new List<XmlNode>();
                Rows = NL.OfType<XmlNode>().ToList();

                int MaxCol = Rows.Max(x => Math.Max(x.ChildNodes.Count, 1));
                List<XmlNode> PrcsRows = Rows.FindAll(x => x.ChildNodes.Count == MaxCol);


                ProcessMessage("Start to process rows");

                ProcessMessage("No. of rows to process ::" + PrcsRows.Count);

                foreach (XmlNode Node in PrcsRows)
                {
                    ACRDetail Obj = new ACRDetail();
                    Obj.AID = Node.ChildNodes[0].InnerText;
                    Obj.CorAuthor = Node.ChildNodes[5].InnerText;
                    Obj.CorEmail = Node.ChildNodes[15].InnerText.TrimEnd(new char[] { ' ', ',', '.' });
                    Obj.SNO = (_ArticleACRDetailList.Count + 1).ToString();
                    _ArticleACRDetailList.Add(Obj);
                }
                ProcessMessage("End to process rows");

                File.Delete(_XMLFile);

                ProcessMessage("Delete XML file..");
            }
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
    }
}
