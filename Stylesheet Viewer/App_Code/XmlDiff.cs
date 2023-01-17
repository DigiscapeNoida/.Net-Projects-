using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.XmlDiffPatch;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public class XmlDiff
{
    public ArrayList BaseDataList = new ArrayList();
    public ArrayList SOList        = new ArrayList();
    public ArrayList PITNodeList   = new ArrayList();
    public ArrayList SECHeadList   = new ArrayList();
    public ArrayList CUList        = new ArrayList();//added by Rahul
    public ArrayList S100List      = new ArrayList();
    public ArrayList S200List      = new ArrayList();
    public ArrayList P100List      = new ArrayList();
    public ArrayList S300List      = new ArrayList();
    public ArrayList PrintList     = new ArrayList();
    public ArrayList DisPatchList  = new ArrayList();
    public ArrayList StandardTextList  = new ArrayList();//added by munesh
    public ArrayList OtherInstList = new ArrayList();
    public ArrayList EditiorList = new ArrayList();

    string _dtdPath = "";

    XmlReader Reader;

    XmlReaderSettings ReaderSettings = new XmlReaderSettings();
    int _XmlCount = 0;

    public int XmlCount
    {
        get
        {
            return _XmlCount;
        }
        set
        {
            _XmlCount = value;
        }
    }
    XmlDocument XmlDocument1 = new XmlDocument();
    XmlDocument XmlDocument2 = new XmlDocument();
    XmlDocument XmlDocument3 = new XmlDocument();
    XmlDocument XmlDocument4 = new XmlDocument();
    XmlDocument XmlDocument5 = new XmlDocument();

    private bool LoadXmlFile1(string XmlPath)
    {
        try
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string tempXMlPath = "C:\\temp\\NewTemp1.xml";


            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(XmlPath).Replace("&amp;#x", "&#x").Replace("&", "#$#"));

            string str = Regex.Match(XmlStr.ToString(), "<journalStylesheet[^>]{1,}").Value;
            XmlStr.Replace(str, "<journalStylesheet");
            XmlStr.Replace("&amp;#x", "&#x");
            XmlStr.Replace("#$#amp;#x", "#$##x");
            

            File.WriteAllText(tempXMlPath, XmlStr.ToString());

            Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            XmlDocument1 = new XmlDocument();
            XmlDocument1.PreserveWhitespace = false;
            XmlDocument1.Load(Reader);
            XmlDocument1.InnerXml = XmlDocument1.InnerXml.Replace("\r", "").Replace("\n","");
            Reader.Close();
            XmlDocument1.InnerXml = ReplaceDoubleSpaceWithSingleSpace(XmlDocument1.InnerXml);
            MergeSectionTitleWithPara(XmlDocument1);
            //XmlDocument1.InnerXml = XmlDocument1.InnerXml.Replace("#$#", "&");

            InterChangeNode(XmlDocument1.DocumentElement);
        }
        catch (XmlException ex)
        {
            try
            {
                Reader.Close();
            }
            catch { }
            return false;
        }
        return true;
    }
    
    private bool LoadXmlFile2(string XmlPath)
    {
        try
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string tempXMlPath = "C:\\temp\\NewTemp2.xml";

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(XmlPath).Replace("&amp;#x", "&#x").Replace("&", "#$#"));

            string str = Regex.Match(XmlStr.ToString(), "<journalStylesheet[^>]{1,}").Value;
            XmlStr.Replace(str, "<journalStylesheet");
            XmlStr.Replace("&amp;#x", "&#x");


            File.WriteAllText(tempXMlPath, XmlStr.ToString());

            Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            XmlDocument2 = new XmlDocument();
            XmlDocument2.PreserveWhitespace = false;
            XmlDocument2.Load(Reader);
            XmlDocument2.InnerXml = XmlDocument2.InnerXml.Replace("\r", "").Replace("\n","");
            Reader.Close();
            XmlDocument2.InnerXml = ReplaceDoubleSpaceWithSingleSpace(XmlDocument2.InnerXml);

            MergeSectionTitleWithPara(XmlDocument2);
            //XmlDocument2.InnerXml = XmlDocument2.InnerXml.Replace("#$#", "&");
            InterChangeNode(XmlDocument2.DocumentElement);
        }
        catch (XmlException ex)
        {
            try
            {
                Reader.Close();
            }
            catch { }
            return false;
        }
        return true;
    }
    
    private bool LoadXmlFile3(string XmlPath)
    {
        try
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string tempXMlPath = "C:\\temp\\NewTemp3.xml";

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(XmlPath).Replace("&amp;#x", "&#x").Replace("&", "#$#"));

            string str = Regex.Match(XmlStr.ToString(), "<journalStylesheet[^>]{1,}").Value;

            if (!str.Equals(""))
                XmlStr.Replace(str, "<journalStylesheet");

            XmlStr.Replace("&amp;#x", "&#x");
            File.WriteAllText(tempXMlPath, XmlStr.ToString());

            Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            XmlDocument3 = new XmlDocument();
            XmlDocument3.PreserveWhitespace = false;
            XmlDocument3.Load(Reader);
            XmlDocument3.InnerXml = XmlDocument3.InnerXml.Replace("\r", "").Replace("\n","");
            Reader.Close();

            XmlDocument3.InnerXml = ReplaceDoubleSpaceWithSingleSpace(XmlDocument3.InnerXml);
            MergeSectionTitleWithPara(XmlDocument3);
            InterChangeNode(XmlDocument3.DocumentElement);
        }
        catch (XmlException ex)
        {
            try
            {
                Reader.Close();
            }
            catch { }
            return false;
        }
        return true;
    }
    
    private bool LoadXmlFile4(string XmlPath)
    {
        try
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string tempXMlPath = "C:\\temp\\NewTemp4.xml";

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(XmlPath).Replace("&amp;#x", "&#x").Replace("&", "#$#"));

            string str = Regex.Match(XmlStr.ToString(), "<journalStylesheet[^>]{1,}").Value;
            if (!str.Equals(""))
                 XmlStr.Replace(str, "<journalStylesheet");

            XmlStr.Replace("&amp;#x", "&#x");

            File.WriteAllText(tempXMlPath, XmlStr.ToString());

            Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            XmlDocument4 = new XmlDocument();
            XmlDocument4.PreserveWhitespace = false;
            XmlDocument4.Load(Reader);
            XmlDocument4.InnerXml = XmlDocument4.InnerXml.Replace("\r", "").Replace("\n","");
            Reader.Close();
            XmlDocument4.InnerXml = ReplaceDoubleSpaceWithSingleSpace(XmlDocument4.InnerXml);
            MergeSectionTitleWithPara(XmlDocument4);
            InterChangeNode(XmlDocument4.DocumentElement);
        }
        catch (XmlException ex)
        {
            try
            {
                Reader.Close();
            }
            catch { }
            return false;
        }
        return true;
    }



    
    private bool LoadXmlFile5(string XmlPath)
    {
        try
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string tempXMlPath = "C:\\temp\\NewTemp5.xml";

            StringBuilder XmlStr = new StringBuilder(File.ReadAllText(XmlPath).Replace("&amp;#x", "&#x").Replace("&", "#$#"));

            string str = Regex.Match(XmlStr.ToString(), "<journalStylesheet[^>]{1,}").Value;

            if (!str.Equals(""))
                 XmlStr.Replace(str, "<journalStylesheet");

            File.WriteAllText(tempXMlPath, XmlStr.ToString());

            Reader = XmlReader.Create(tempXMlPath, ReaderSettings);
            XmlDocument5 = new XmlDocument();
            XmlDocument5.PreserveWhitespace = false;
            XmlDocument5.Load(Reader);
            XmlDocument5.InnerXml = XmlDocument5.InnerXml.Replace("\r", "").Replace("\n","");
            Reader.Close();
            XmlDocument5.InnerXml = ReplaceDoubleSpaceWithSingleSpace(XmlDocument5.InnerXml);
            InterChangeNode(XmlDocument2.DocumentElement);
            MergeSectionTitleWithPara(XmlDocument5);
        }
        catch (XmlException ex)
        {
            try
            {
                Reader.Close();
            }
            catch { }
            return false;
        }
        return true;
    }
    private void MergeSectionTitleWithPara(XmlDocument _XDoc)
    {

        XmlNodeList NL = _XDoc.GetElementsByTagName("sectiontitle");
        while(NL.Count>0)
        {
            XmlNode Node = NL[0];
            if (Node.NextSibling != null && Node.NextSibling.Name.Equals("p"))
            {
                Node.NextSibling.InnerXml = Node.InnerXml + ": " + Node.NextSibling.InnerXml;
                Node.ParentNode.RemoveChild(Node);
            }
            else
                break;
        }
    }

    public void  GetDiff1()
    {
        foreach (XmlNode node in XmlDocument1.DocumentElement)
        {
            switch (node.Name)
            {
                case "baseData":
                    {
                        ProcessBaseDataNode(node);
                        break;
                    }
//"CU" is added by Rahul
                case "cu":
                    {
                        ProcessCUNode(node);
                        break;
                    }
//standardText add by munesh
                case "standardText":
                    {
                        ProcessStandardTextNode(node);
                        break;
                    }
                case "s0":
                    {
                        ProcessSONode(node);
                        break;
                    }
                case "pit":
                    {
                        foreach (XmlNode chNode in node)
                        {
                            if (chNode.Name.Equals("docHeads"))
                            {
                                ProcessPITNode(chNode);
                            }
                            else if (chNode.Name.Equals("sectionHeads"))
                            {
                                ProcessSectionHeadNode(chNode);
                            }
                        }
                        break;
                    }
                case "s100":
                    {
                        ProcessS100Node(node);
                        break;
                    }
                case "s200":
                    {
                        ProcessS200Node(node);
                        break;
                    }
                case "p100":
                    {
                        ProcessP100Node(node);
                        break;
                    }
                case "s300":
                    {
                        ProcessS300Node(node);
                        break;
                    }
               
                case "print":
                    {
                        ProcessPrintNode(node);
                        break;
                    }
                case "despatch":
                    {
                        ProcessDispatchNode(node);
                        break;
                    }
                case "otherInstructions":
                    {
                        ProcessOtherInstructNode(node);
                        break;
                    }
                case "editors":
                     {
                         ProcessEditiorNode(node);
                        break;
                    }
                
            }
        }

    }

    private void SetReaderSettings()
    {
        ReaderSettings.IgnoreComments = false;
        ReaderSettings.IgnoreWhitespace = false;

        if (_dtdPath.Equals(""))
        {
            ReaderSettings.ValidationType = ValidationType.None;
            ReaderSettings.ProhibitDtd = false;
        }
        else
        {
            ReaderSettings.ProhibitDtd = true;
            ReaderSettings.ValidationType = ValidationType.DTD;
        }
    }

    public void  XmlCompare(string FolderPath)
    {

        ///*****************
        if (!Directory.Exists(FolderPath)) return;
        ///*****************
        ///

        string[] FL = Directory.GetFiles(FolderPath,"*.xml");
        if (FL.Length == 0) return;
         _XmlCount = FL.Length;
        //Array.Reverse(FL);

        FL=MyReverse(FL,".xml");

        LoadXmlFile1(FL[0]);

        GetDiff1();

        if (FL.Length >= 2)
        {
            LoadXmlFile2(FL[1]);
            FIleXdata(2);
        }
        if (FL.Length >= 3)
        {
            LoadXmlFile3(FL[2]);
            FIleXdata(3);
        }
        if (FL.Length >= 4)
        {
            LoadXmlFile4(FL[3]);
            FIleXdata(4);
        }
        if (FL.Length >= 5)
        {
            LoadXmlFile5(FL[4]);
            FIleXdata(5);
        }
    }

    private void FIleXdata( int FileNo)
    {
        XmlData XmlDataObj;
        XmlNode node;
        XmlNode RootNode=null;
        switch(FileNo)
        {
            case 2:
                {
                    RootNode= XmlDocument2.DocumentElement;
                    break;
                }
            case 3:
                {
                    RootNode = XmlDocument3.DocumentElement;
                    break;
                }
            case 4:
                {
                    RootNode = XmlDocument4.DocumentElement;
                    break;
                }
            case 5:
                {
                    RootNode = XmlDocument5.DocumentElement;
                    break;
                }
        }
        if (RootNode == null) return;

        XmlNode BaseNode = null;
        node = null;
        for (int i = 0; i < BaseDataList.Count; i++)
        {
            XmlDataObj = ((XmlData)BaseDataList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                BaseNode = RootNode.SelectSingleNode("//baseData");
                if (BaseNode!=null)
                    node     = BaseNode.SelectSingleNode("//" + XmlDataObj.getColumnHead());

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode S0Node = null;
        node = null;
        for (int i = 0; i < SOList.Count; i++)
        {
            XmlDataObj = ((XmlData)SOList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                S0Node = RootNode.SelectSingleNode("//s0");
                if (S0Node!=null)
                    node = S0Node.SelectSingleNode("//" + XmlDataObj.getColumnHead());

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        //******Added By Rahul for C&U
        XmlNode CUNode = null;
        node = null;
        for (int i = 0; i < CUList.Count; i++)
        {
            XmlDataObj = ((XmlData)CUList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                CUNode = RootNode.SelectSingleNode("//cu");
                if (CUNode != null)
                    node = CUNode.SelectSingleNode("//" + XmlDataObj.getColumnHead());
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo == 2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        //*******
        //******Added By munesh for standerd text
        XmlNode STNode = null;
        node = null;
        for (int i = 0; i < StandardTextList.Count; i++)
        {
            XmlDataObj = ((XmlData)StandardTextList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                STNode = RootNode.SelectSingleNode("//standardText");
                if (STNode != null)
                    node = STNode.SelectSingleNode("//" + XmlDataObj.getColumnHead());
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo == 2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }
        XmlNode S100Node = null;
        node = null;
        for (int i = 0; i < S100List.Count; i++)
        {
            XmlDataObj = ((XmlData)S100List[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                S100Node = RootNode.SelectSingleNode("//s100");
                if (S100Node != null)
                    node = S100Node.SelectSingleNode("//" + XmlDataObj.getColumnHead());
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode S200Node = null;
        node = null;
        for (int i = 0; i < S200List.Count; i++)
        {
            XmlDataObj = ((XmlData)S200List[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                S200Node = RootNode.SelectSingleNode("//s200");
                if (S200Node != null)
                    node = S200Node.SelectSingleNode("//" + XmlDataObj.getColumnHead());

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode P100Node = null;
        node = null;
        for (int i = 0; i < P100List.Count; i++)
        {
            XmlDataObj = ((XmlData)P100List[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                P100Node = RootNode.SelectSingleNode("//p100" );
                if (P100Node !=null)
                    node = P100Node.SelectSingleNode("//" + XmlDataObj.getColumnHead());
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode S300Node = null;
        node = null;

        for (int i = 0; i < S300List.Count; i++)
        {
            XmlDataObj = ((XmlData)S300List[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                S300Node=RootNode.SelectSingleNode("//s300");
                if (S300Node!=null)
                    node = S300Node.SelectSingleNode("//" + XmlDataObj.getColumnHead());
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode PrintNode = null;
                PrintNode = RootNode.SelectSingleNode("//print");
                node      = null;

        string NodeName = "";

        for (int i = 0; i < PrintList.Count; i++)
        {
            XmlDataObj = ((XmlData)PrintList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                if (PrintNode != null)
                {
                    node = PrintNode.SelectSingleNode("//" + XmlDataObj.getColumnHead()) ;
                }

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode EditorNode = null;
        node = null;
        EditorNode = RootNode.SelectSingleNode("//editors");
        for (int i = 0; i < EditiorList.Count; i++)
        {
            XmlDataObj = ((XmlData)EditiorList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                if (EditorNode != null)
                {
                    try
                    {
                        int nodecount = Regex.Matches(NodeName, XmlDataObj.getColumnHead()).Count;
                        nodecount++;

                        node = EditorNode.SelectSingleNode("//editor[" +  nodecount.ToString() + "]/" + XmlDataObj.getColumnHead());

                        NodeName = NodeName + "#" + node.Name;
                    }
                    catch { }
                }
                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo == 2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }
        XmlNode DespatchNode = null;
        node = null;

        for (int i = 0; i < DisPatchList.Count; i++)
        {
            XmlDataObj = ((XmlData)DisPatchList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {

                DespatchNode = RootNode.SelectSingleNode("//despatch");
                if (DespatchNode!=null)
                    node =DespatchNode.SelectSingleNode("//" + XmlDataObj.getColumnHead());

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo==2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }

        XmlNode OtherInstNode = null;
        node = null;
        for (int i = 0; i < OtherInstList.Count; i++)
        {
            XmlDataObj = ((XmlData)OtherInstList[i]);
            if (!XmlDataObj.ColumnHead.Equals(""))
            {
                OtherInstNode = RootNode.SelectSingleNode("//otherInstructions");

                if (OtherInstNode!= null)
                    node = OtherInstNode.SelectSingleNode("//otherInstructions/" + XmlDataObj.getColumnHead());

                if (node != null && !XmlDataObj.OnlyHead)
                {
                    if (FileNo == 2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
                else if  (node==null)
                {
                    node = RootNode.SelectSingleNode("//otherInstructions");
                    if (FileNo == 2)
                        XmlDataObj.Column2 = node.InnerText;
                    else if (FileNo == 3)
                        XmlDataObj.Column3 = node.InnerText;
                    else if (FileNo == 4)
                        XmlDataObj.Column4 = node.InnerText;
                    else if (FileNo == 5)
                        XmlDataObj.Column5 = node.InnerText;
                }
            }
        }
    }
    private void ProcessBaseDataNode(XmlNode node)
    {
        ProcessChild(node, BaseDataList);
    }

    private void ProcessSONode(XmlNode node)
    {
        ProcessChild(node,SOList);
    }

    
    private void ProcessChild(XmlNode node, ArrayList ArrayListOBJ)
    {

       

        foreach (XmlNode xnd in node)
        {
            Debug.WriteLine("xnd ::" + xnd.Name);

            if (xnd.PreviousSibling != null)
            {
                if ("#offprints#style-e-issue#".IndexOf("#" + xnd.PreviousSibling.Name + "#") != -1)
                {
                    ArrayListOBJ.Add(new XmlData("AdditionalInfo", true));
                }
            }

            if (xnd.FirstChild == null)
            {
                ArrayListOBJ.Add(new XmlData(xnd, ""));
            }
            else if (xnd.Name.Equals("p"))
            {
                string RefinePara = RefineParaXml(xnd);
                ArrayListOBJ.Add(new XmlData(xnd, RefinePara));
            }
            else if (xnd.FirstChild.NodeType == XmlNodeType.Text)
            {
                ArrayListOBJ.Add(new XmlData(xnd.Name, xnd.InnerText));
            }
            else if (xnd.FirstChild.Name.Equals("p"))
            {
                string RefinePara = RefineParaXml(xnd);
                ArrayListOBJ.Add(new XmlData(xnd.Name, RefinePara));
            }
            else
            {
                if ("ptsData#JM-EditData".IndexOf(xnd.Name) != -1)
                {
                }
                else
                {
                    if (xnd.Name.Equals("editor"))
                    {
                        if (xnd.FirstChild.Name.Equals("editorName"))
                        {
                            ArrayListOBJ.Add(new XmlData("EDITOR" + ": " + xnd.FirstChild.InnerText, true));
                        }
                        else
                        {
                            ArrayListOBJ.Add(new XmlData(xnd, true));
                        }
                    }
                    else
                       ArrayListOBJ.Add(new XmlData(xnd, true));

                }
             
                foreach (XmlNode ynd in xnd)
                {

                    if ("fixedPlanInd#s5Required#colourComment".IndexOf(ynd.Name) != -1)
                    {
                    }
                    if (ynd.Name.Equals("latexFrequency"))
                    {
                        Debug.WriteLine("ynd ::" + ynd.Name);
                    }
                    if (ynd.NodeType == XmlNodeType.Element)
                    {
                        if (ynd.Name.Equals("p"))
                        {
                            ArrayListOBJ.Add(new XmlData(ynd, ynd.InnerXml));
                        }
                        else if (ynd.FirstChild == null && ynd.NodeType == XmlNodeType.Element)
                        {
                            ArrayListOBJ.Add(new XmlData(ynd, ""));
                        }
                        else if (ynd.FirstChild.NodeType != XmlNodeType.Text && !ynd.FirstChild.Name.Equals("p"))
                            ArrayListOBJ.Add(new XmlData(ynd, ""));
                        else if (ynd.FirstChild.NodeType == XmlNodeType.Text)
                            ArrayListOBJ.Add(new XmlData(ynd, ynd.InnerText));
                        else if (ynd.FirstChild.Name.Equals("p"))
                        {
                            string RefinePara = RefineParaXml(ynd);
                            ArrayListOBJ.Add(new XmlData(ynd, RefinePara));
                        }
                    }
                }
            }
        }
    }

    private string RefineParaXml(XmlNode ynd)
    {
        string RefinePara="";

        if (ynd.InnerXml == null)
        {
            RefinePara = ynd.InnerText;
        }
        else if (ynd.InnerXml.Equals(""))
        {
            RefinePara = ynd.InnerText;
        }
        else
        {
            RefinePara = ynd.InnerXml;
        }
        RefinePara = RefinePara.Replace("</p> ", "</p>");
        RefinePara = RefinePara.Replace("</p> ", "</p>");
        RefinePara = RefinePara.Replace("</p><p>", "<br/>");

        RefinePara = RefinePara.Replace("<p>", "");
        RefinePara = RefinePara.Replace("</p>", "");
        return RefinePara;
    }

    
    private void ProcessSectionHeadNode(XmlNode node)
    {
        string[] Col = new string[3];
        XmlNodeList docHead = node.SelectNodes("sectionHead");
        XmlData XmlDataObj = null;
        for (int i = 0; i < docHead.Count; i++)
        {
            for (int j = 0; j < docHead[i].ChildNodes.Count; j++)
            {
                Col[j] = docHead[i].ChildNodes[j].InnerXml;
            }
            if (docHead[i].LastChild != null)
            {
                XmlDataObj = new XmlData(docHead[i].LastChild, Col[0], Col[1], Col[2]);
                XmlDataObj.PIT = true;
            }
            SECHeadList.Add(XmlDataObj);
        }

        foreach (XmlNode xnd in node)
        {
            Debug.WriteLine("xnd ::" + xnd);
            if (xnd.Equals("sectionHead"))
            {
            }
            else
            {
                if (xnd.FirstChild == null)
                {
                    XmlDataObj = new XmlData(xnd, "");
                    XmlDataObj.PIT = true;
                    SECHeadList.Add(XmlDataObj);
                }
                else if (xnd.FirstChild.NodeType == XmlNodeType.Text)
                {
                    XmlDataObj = new XmlData("",xnd.Name, xnd.InnerText);
                    XmlDataObj.PIT = true;
                    SECHeadList.Add(XmlDataObj);
                }
                else if (xnd.FirstChild.Name.Equals("p"))
                {
                    string RefinePara = RefineParaXml(xnd);

                    XmlDataObj =new XmlData("",xnd.Name, RefinePara);
                    XmlDataObj.PIT = true;
                    SECHeadList.Add(XmlDataObj);
                }
                else
                {
                    SECHeadList.Add(new XmlData(xnd, true));
                    foreach (XmlNode ynd in xnd)
                    {
                        Debug.WriteLine("ynd ::" + ynd);
                        if (ynd.Equals("p"))
                        {
                            string RefinePara = RefineParaXml(ynd);
                            SECHeadList.Add(new XmlData(ynd, RefinePara));
                        }
                        else if (ynd.FirstChild.NodeType != XmlNodeType.Text && !ynd.FirstChild.Name.Equals("p"))
                            SECHeadList.Add(new XmlData(ynd, ""));
                        else if (ynd.FirstChild.NodeType == XmlNodeType.Text)
                            SECHeadList.Add(new XmlData(ynd, ynd.InnerText));
                        else if (node.FirstChild.Name.Equals("p"))
                        {
                            string RefinePara = ynd.InnerText;
                            RefinePara = RefinePara.Replace("</p> ", "</p>");
                            RefinePara = RefinePara.Replace("</p> ", "</p>");
                            RefinePara = RefinePara.Replace("</p><p>", "<br/>");

                            RefinePara = RefinePara.Replace("<p>", "");
                            RefinePara = RefinePara.Replace("</p>", "");
                            SECHeadList.Add(new XmlData(ynd, RefinePara));
                        }
                    }
                }
            }
        }
    }
    private void ProcessPITNode(XmlNode node)
    {
        string[] Col = new string[3];
        XmlNodeList docHead = node.SelectNodes("docHead");
        XmlData XmlDataObj = null;
        for (int i = 0; i < docHead.Count; i++)
        {
            for (int j = 0; j < docHead[i].ChildNodes.Count; j++)
            {
                Col[j] = docHead[i].ChildNodes[j].InnerXml;
            }
            if (docHead[i].LastChild != null)
            {
                XmlDataObj = new XmlData(docHead[i].LastChild, Col[0], Col[1], Col[2]);
                XmlDataObj.PIT = true;
            }
            PITNodeList.Add(XmlDataObj);
        }

        foreach (XmlNode xnd in node)
        {
            if (!xnd.Equals("docHead"))
                if (xnd.FirstChild == null)
                {
                    PITNodeList.Add(new XmlData(xnd, ""));
                }
                else if (xnd.FirstChild.NodeType == XmlNodeType.Text)
                {
                    PITNodeList.Add(new XmlData(xnd, xnd.InnerText));
                }
                else if (xnd.FirstChild.Name.Equals("p"))
                {
                    string RefinePara = RefineParaXml(xnd);
                    XmlDataObj = new XmlData(xnd, RefinePara);
                    XmlDataObj.PIT = true;
                    PITNodeList.Add(XmlDataObj);
                }
                else
                {
                    PITNodeList.Add(new XmlData(xnd, true));
                    foreach (XmlNode ynd in xnd)
                    {
                        if (ynd.Equals("p"))
                        {
                        }
                        else if (ynd.FirstChild.NodeType != XmlNodeType.Text && !ynd.FirstChild.Name.Equals("p"))
                            PITNodeList.Add(new XmlData(ynd, ""));
                        else if (ynd.FirstChild.NodeType == XmlNodeType.Text)
                            PITNodeList.Add(new XmlData(ynd, ynd.InnerText));
                        else if (node.FirstChild.Name.Equals("p"))
                        {
                            string RefinePara = ynd.InnerText;
                            RefinePara = RefinePara.Replace("</p> ", "</p>");
                            RefinePara = RefinePara.Replace("</p> ", "</p>");
                            RefinePara = RefinePara.Replace("</p><p>", "<br/>");

                            RefinePara = RefinePara.Replace("<p>", "");
                            RefinePara = RefinePara.Replace("</p>", "");
                            PITNodeList.Add(new XmlData(ynd, RefinePara));
                        }
                    }
                }
        }
    }
    private void ProcessCUNode(XmlNode node)
    {
        ProcessChild(node, CUList);
        return;
    }
    private void ProcessStandardTextNode(XmlNode node)
    {
        ProcessChild(node, StandardTextList);
        return;
    }
    private void ProcessS100Node(XmlNode node)
    {
        ProcessChild(node, S100List);
        return;
    }
    private void ProcessS200Node(XmlNode node)
    {
        ProcessChild(node, S200List);
        return;
    }
    private void ProcessP100Node(XmlNode node)
    {
        ProcessChild(node, P100List);
        return;
    }
    private void ProcessS300Node(XmlNode node)
    {
        ProcessChild(node, S300List);
        return;
    }
    private void ProcessPrintNode(XmlNode node)
    {
        ProcessChild(node, PrintList);
        return;
    }
    private void ProcessDispatchNode(XmlNode node)
    {
        ProcessChild(node, DisPatchList);
        return;
    }
    private void ProcessOtherInstructNode(XmlNode node)
    {
        //ProcessChild(node, OtherInstList);
        string RefinePara = RefineParaXml(node);
        OtherInstList.Add(new XmlData(node, RefinePara));
        return;
    }
    private void ProcessEditiorNode(XmlNode node)
    {
        ProcessChild(node, EditiorList);
        return;
    }

    private string  ReplaceDoubleSpaceWithSingleSpace(string InputStr)
    {
        StringBuilder Str = new StringBuilder(InputStr);
        while (Str.ToString().IndexOf("  ") != -1)
        {
            Str.Replace("  ", " ");
        }
        return Str.ToString();
    }
    private void InterChangeNode( XmlNode RootNode)
    {

          XmlNode ptsReportDate  = RootNode.SelectSingleNode("//ptsReportDate");
          XmlNode dateModified   = RootNode.SelectSingleNode("//dateModified");
          XmlNode modifiedBy     = RootNode.SelectSingleNode("//modifiedBy");


          ptsReportDate.ParentNode.InsertAfter(modifiedBy, ptsReportDate);
          ptsReportDate.ParentNode.InsertAfter(dateModified, ptsReportDate);
          //RootNode.InsertAfter(dateModified, ptsReportDate);
        //Date of last PTS report       "select="jss:ptsData/jss:ptsReportDate
        //Non-PTS data last modified on "select="jss:JM-EditData/jss:dateModified
    }
    private string[] MyReverse(string[] FileLIst, string ext)
    {
        string[] RevFileList = new string[FileLIst.Length];
        List<int> File_No = new List<int>();
        if (FileLIst.Length > 0)
        {
            int SeqNo = 0;
            MatchCollection MatchCol;
            foreach (string fName in FileLIst)
            {
                MatchCol = Regex.Matches(fName, "[0-9]{1,}");
                if (MatchCol.Count > 0)
                {
                    SeqNo = Int32.Parse(MatchCol[MatchCol.Count - 1].Value);
                    File_No.Add(SeqNo);
                }
            }

            File_No.Sort();
            File_No.Reverse();
            int SNo = 0;
            foreach (int SN in File_No)
            {
                foreach (string fName in FileLIst)
                {
                    if (fName.EndsWith("_" + SN + ext))
                    {
                        RevFileList.SetValue(fName, SNo);
                        SNo++;
                        break;
                    }
                }
            }
        }
        return RevFileList;
    }

}
