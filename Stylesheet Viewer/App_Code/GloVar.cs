using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for GloVar
/// </summary>
public class GloVar
{

    public static string  AttrValuetxtPath=""; 
    public static ElementDesc ElementDescObj ; 
    public static StringDictionary  EleDic= new StringDictionary();
    public static string EleNameFilePath = "";
    public static JIDList JIDListObj = new JIDList();

     static XmlDocument _xDoc = new XmlDocument();
     static XmlNamespaceManager _nsmgr = null;
     static XmlNodeList whenList = null;
      static  GloVar()
    {
         
        
    }

    public GloVar()
    {
    }
    public GloVar(string EleNameFilePath)
	{
        
	}
    public static string GetElementDescription(string NodeName, string PrntNodeName, string AttVal)
    {
        string TextVal = string.Empty;
        XmlNode xslt = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + PrntNodeName + "/jss:" + NodeName +"=" + AttVal + "']", _nsmgr);
        TextVal = xslt == null ? "" : xslt.InnerText;

        if (xslt == null)
        {
           xslt = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + PrntNodeName + "/jss:" + NodeName+"']" , _nsmgr);
           if (xslt == null)
           {
               xslt = _xDoc.SelectSingleNode(@".//xsl:value-of[@select='//jss:" + PrntNodeName + "/jss:" + NodeName + "']", _nsmgr);
               TextVal = AttVal;
           }
        }
       
        return TextVal;
    }

    public static string GetElementDescriptionWithoutPrntNode(string NodeName,  string AttVal)
    {
        string TextVal = string.Empty;
        XmlNode xslt = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + NodeName + "=" + AttVal + "']", _nsmgr);
        TextVal = xslt == null ? "" : xslt.InnerText;

        if (xslt == null)
        {
            xslt = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + NodeName + "']", _nsmgr);
            if (xslt == null)
            {
                xslt = _xDoc.SelectSingleNode(@".//xsl:value-of[@select='//jss:" + NodeName + "']", _nsmgr);
                TextVal = AttVal;
            }
        }

        return TextVal;
    }

    public static string GetHeadDescription(string NodeName,  string AttVal)
    {
        string TextVal = string.Empty;
        int a;
        string XSLColHead = string.Empty;

        XmlNode whenNode = null;
        if (Int32.TryParse(AttVal, out a))
            _xDoc.SelectSingleNode(@".//xsl:when[@test='/jss:" + NodeName + "=" + AttVal + "']", _nsmgr);

        if (whenNode == null)
        {
            whenNode = _xDoc.SelectSingleNode(@".//xsl:when[@test='jss:" + NodeName + "']", _nsmgr);

            //if (whenNode == null)
                //whenNode = GetColHeadDescription(NodeName, PrntNodeName);
        }

        if (whenNode != null && whenNode.Name.Equals("xsl:if") && NodeName.Equals("sectiontitle"))
        {
            XmlNode PrvusNode = whenNode.PreviousSibling;

            while (true)
            {

                if (PrvusNode != null && PrvusNode.Name.StartsWith("H", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                PrvusNode = PrvusNode.PreviousSibling;
            }
            if (PrvusNode != null)
                return PrvusNode.InnerText;
        }

        if (whenNode == null)
        {
            foreach (XmlNode Node in whenList)
            {
                if (Node.OuterXml.Contains(":" + NodeName + "/"))
                {
                    whenNode = Node;
                    break;
                }
                else if (Node.OuterXml.Contains(":" + NodeName + "="))
                {
                    whenNode = Node;
                    break;
                }
            }
        }
        if (whenNode != null)
        {
            XmlNode chooseNode = whenNode.ParentNode;
            XmlNode tdNode = chooseNode != null ? whenNode.ParentNode.ParentNode : null;
            XmlNode PrvusTDNode = tdNode != null ? tdNode.PreviousSibling : null;
            if (PrvusTDNode != null)
            {
                TextVal = PrvusTDNode.InnerText;
            }

        }

        if (TextVal.Equals("No"))
        {
        }
        return TextVal;
    }

    public static string GetColHeadDescription(string NodeName, string PrntNodeName, string AttVal)
    {
        string TextVal = string.Empty;
        int a;
        string XSLColHead = string.Empty;

        XmlNode whenNode = null;
        if (Int32.TryParse(AttVal, out a))
            _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + PrntNodeName + "/jss:" + NodeName + "=" + AttVal + "']", _nsmgr);

        if (whenNode == null)
        {
            whenNode = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + PrntNodeName + "/jss:" + NodeName + "']", _nsmgr);

            if (whenNode == null)
                whenNode = GetColHeadDescription(NodeName, PrntNodeName);
        }

        if (whenNode != null && whenNode.Name.Equals("xsl:if") && NodeName.Equals("sectiontitle"))
        {
            XmlNode PrvusNode = whenNode.PreviousSibling;

            while(true)
            {

                if (PrvusNode != null && PrvusNode.Name.StartsWith("H",StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                PrvusNode = PrvusNode.PreviousSibling;
            }
            if (PrvusNode != null )
                return PrvusNode.InnerText;
        }

        if (whenNode == null)
        {
            foreach (XmlNode Node in whenList)
            {
                if (Node.OuterXml.Contains(":" + NodeName + "/"))
                {
                    whenNode = Node;
                    break;
                }
                else if (Node.OuterXml.Contains(":" + NodeName + "="))
                {
                    whenNode = Node;
                    break;
                }
            }
        }
        if (whenNode != null)
        {
            XmlNode chooseNode  = whenNode.ParentNode;
            XmlNode tdNode      = chooseNode !=null?whenNode.ParentNode.ParentNode: null;
            XmlNode PrvusTDNode  = tdNode!=null? tdNode.PreviousSibling :null;
            if (PrvusTDNode != null)
            {
                TextVal= PrvusTDNode.InnerText;
            }
            
        }

        if (TextVal.Equals("No"))
        { 
        }
        return TextVal;
    }

    public static XmlNode GetColHeadDescription(string NodeName, string PrntNodeName)
    {
        string TextVal = string.Empty;
        XmlNode ValueOfNode = _xDoc.SelectSingleNode(@".//xsl:value-of[@select='//jss:" + PrntNodeName + "/jss:" + NodeName + "']", _nsmgr);
        if (ValueOfNode != null)
        {
            if (ValueOfNode.ParentNode != null && ValueOfNode.ParentNode.Name.Equals("font"))
            {
                return ValueOfNode.ParentNode.ParentNode;
            }
        }
        return null;
    }

    public static string GetElementDescription(string NodeName, string PrntNodeName)
    {
        string TextVal = string.Empty;
        XmlNode xslt = _xDoc.SelectSingleNode(@".//xsl:when[@test='//jss:" + PrntNodeName + "/jss:" +  NodeName +"']", _nsmgr);
        TextVal = xslt == null ? "" : xslt.InnerText;

        return TextVal;
    }

    public static void GetElementDespCription()
    {

        string AppPath = Path.GetDirectoryName(AttrValuetxtPath) + "\\JSS.xsl";
        _xDoc.Load(AppPath);
        _nsmgr = new XmlNamespaceManager(_xDoc.NameTable);
        _nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        _nsmgr.AddNamespace("jss", "http://www.elsevier.com/xml/schema/journalStylesheets");
        _nsmgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

        whenList = _xDoc.SelectNodes(@".//xsl:when", _nsmgr);
        foreach (XmlNode when in whenList)
        {
            when.Attributes[0].InnerXml = when.Attributes[0].InnerXml.Replace(" ", "");
            when.Attributes[0].InnerXml = when.Attributes[0].InnerXml.Replace("''", "").Trim(new char[] { '=' });

        }


        
        ElementDescObj = new ElementDesc(AttrValuetxtPath);
        string[] EleArr = File.ReadAllLines( EleNameFilePath);
        string[] Arr = {" "," "};
        foreach(string elename in EleArr)
        {
            if (elename.IndexOf("\t")!=-1)
            {
                Arr =elename.Split('\t');
                if (!EleDic.ContainsKey(Arr[0]))
                {
                    EleDic.Add(Arr[0],Arr[1]);
                }
            }
        }
    }

    public static void UpdateJID(string InputPath)
    {

        string JID = "";
        string JTitle="";
        string Prdctsite = ""; 
        XmlNodeList nodeList = null;
        XmlDocument myXmlDocument = new XmlDocument();
        string[] DirList = Directory.GetDirectories(InputPath);
        string[] FileList=null;
        string JSSXml="";
        int count = 0;
        List<JIDDETAILS> AllJID = new List<JIDDETAILS>();

        foreach (string DirPath in DirList)
        { 
            FileList = Directory.GetFiles(DirPath,"*.xml");
            if (FileList.Length > 0)
            {
                //FileList = MyReverse(FileList, ".xml");
                //Array.Reverse(FileList);

                JSSXml = FileList[0];
                try
                {
                    myXmlDocument.Load(JSSXml);
                }
                catch
                { 

                }

                nodeList = myXmlDocument.GetElementsByTagName("journalCode");
                if (nodeList.Count>0)
                {
                    JID = nodeList[0].InnerXml;
                }

                nodeList = myXmlDocument.GetElementsByTagName("journalTitle");
                if (nodeList.Count > 0)
                {
                    JTitle =Regex.Match(nodeList[0].InnerXml, @"(<!\[CDATA\[)(.*?)(]]>)").Groups[2].Value;
                }

                nodeList = myXmlDocument.GetElementsByTagName("productionSite");
                if (nodeList.Count > 0)
                {
                    Prdctsite = nodeList[0].InnerXml;
                }
                count++;
                AllJID.Add(new JIDDETAILS(count, JID,JTitle,Prdctsite,"Elsevier"));
            }
        }
        UpdateDatabase UpdateDatabaseObj = new UpdateDatabase();
        if (AllJID.Count > 0)
        {
            UpdateDatabaseObj.DeleteAllRow();
        }
        foreach (JIDDETAILS JIDObj in AllJID)
        {
            UpdateDatabaseObj.InsertJID(JIDObj);
        }
    }

    private static string[] MyReverse(string[] FileLIst, string ext)
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
    public static string ConnectSystem(string NETWORKPATH, string UID, string PWD)
    {
        DisconnectSystem(NETWORKPATH);
        Process MAPProcess = new Process();
        MAPProcess.StartInfo.FileName = "cmd";

        if (UID.Equals(""))
            MAPProcess.StartInfo.Arguments = " " + @"/c net use " + NETWORKPATH;
        else
            MAPProcess.StartInfo.Arguments = " " + @"/c net use " + NETWORKPATH + " " + PWD + @" /user:" + UID;

        MAPProcess.StartInfo.RedirectStandardOutput = true;
        MAPProcess.StartInfo.UseShellExecute = false;
        MAPProcess.StartInfo.CreateNoWindow = true;
        MAPProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        MAPProcess.Start();
        MAPProcess.WaitForExit();
        string Result = MAPProcess.StandardOutput.ReadToEnd();
        return Result;
    }
     public static string DisconnectSystem(string SystemName)
    {
        string Result = "";
        using (Process MAPProcess = new Process())
        {
            MAPProcess.StartInfo.FileName = "cmd";
            MAPProcess.StartInfo.Arguments = " " + @"/c net use " + SystemName.TrimEnd('\\') + " \\d";
            MAPProcess.StartInfo.RedirectStandardOutput = true;
            MAPProcess.StartInfo.UseShellExecute = false;
            MAPProcess.StartInfo.CreateNoWindow = true;
            MAPProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            MAPProcess.Start();
            MAPProcess.WaitForExit();
            Result = MAPProcess.StandardOutput.ReadToEnd();
        }
        return Result;
    }
}
