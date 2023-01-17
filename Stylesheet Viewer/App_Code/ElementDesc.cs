using System;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Xml;

/// <summary>
/// Summary description for ElementDesc
/// </summary>
public class ElementDesc
{
    XmlDocument myXmlDocument = new XmlDocument();
    XmlReader Reader;
    XmlReaderSettings ReaderSettings = new XmlReaderSettings();
    public static string EleNameFilePath = "";

    public ElementDesc(string xmlPath )
    {
        LoadXmlFile(xmlPath);
    }
    private bool LoadXmlFile(string XmlPath)
    {
        try
        {
            Reader = XmlReader.Create(XmlPath);
            myXmlDocument = new XmlDocument();
            myXmlDocument.PreserveWhitespace = false;
            myXmlDocument.Load(Reader);
            Reader.Close();
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
   
    public  string GetElementDespCription(string EleName )
    {

        try
        {
            //XmlNode node = null;
            //node = myXmlDocument.SelectSingleNode("//" + EleName);
            //if (node != null)
            //{
            //    return node.InnerText;
            //}
            if (EleName.Equals("copyPermDiscl"))
            {
                return "";
            }
            else if (GloVar.EleDic.ContainsKey(EleName))
            {
                return GloVar.EleDic[EleName];
            }
            else
            {
                Debug.WriteLine("NotDefine ::" + EleName);
                return "NotDefine";

            }
        }
        catch
        {
            return EleName;
        }
    }
    public  string GetElementDespCription(string EleName , string AtrValue)
    {

        if (EleName.Equals(""))
            return "";

            XmlNode node = null;
            node= myXmlDocument.SelectSingleNode("//" + EleName + "[@atr='" + AtrValue + "']");

            if (node!=null)
            {
               if (node.Attributes.GetNamedItem("value")!=null)
                {
                    return node.Attributes.GetNamedItem("value").Value;
                }

                else
                {
                    return "NotDefine";
                }
            }
            else if(EleName.Equals("copyPermDiscl"))
            {
                return "";
            }
            else
            {
                return AtrValue;
            }
    }
}
