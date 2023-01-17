using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.XmlDiffPatch;

using System.Collections;
 
/// <summary>
/// Summary description for Class1
/// </summary>
public class FileIO
{
    public FileIO()
    {
        //
        // TODO: Add constructor logic here
        // 
    }
    public ArrayList AddOldOrders(string strServer,string strJID,string strAID,string strStage,string strServerPath,string strUserIp)
    {
        //string[] strArr;      
        ArrayList arrRet=new ArrayList(1);
        
     
        try
        {          

                //strArr = System.Configuration.ConfigurationManager.AppSettings["Stages"].Split(Convert.ToChar(";"));
                DirectoryInfo dirInfo = new DirectoryInfo(strServerPath);
                if (Directory.Exists(strServer + "//" + strUserIp ) != true)
                {
                    Directory.CreateDirectory((strServer + "//" + strUserIp));
                }
                if (Directory.Exists(strServer + "//" + strUserIp + "//" + strJID) != true)
                {
                    Directory.CreateDirectory((strServer + "//" + strUserIp + "//" + strJID));
                }
                if (Directory.Exists(strServer + "//" + strUserIp + "//" + strJID + "//" + strAID) != true)
                {
                    Directory.CreateDirectory((strServer  + "//" + strUserIp + "//" + strJID + "//" + strAID));
                }
                if (Directory.Exists(strServer + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage) != true)
                {
                    Directory.CreateDirectory((strServer + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage));
                }

                foreach (FileInfo filInfo in dirInfo.GetFiles())
                {
                    File.Copy(filInfo.FullName, (strServer + "\\" + strUserIp + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + filInfo.Name), true);
                    arrRet.Add( filInfo.Name.ToString());                    

                }

                return arrRet;
        }
        catch (Exception ex)
        {
            return arrRet;
        }


    }
    public ArrayList AddCurrentOrder(string strServer, string strJID, string strAID, string strStage, string strServerPath,string strUserIp)
    {
        //string[] strArr;        
        ArrayList arrRet=new ArrayList();
        
        try
        {          


                //strArr = System.Configuration.ConfigurationManager.AppSettings["Stages"].Split(Convert.ToChar(";"));

                DirectoryInfo dirInfo = new DirectoryInfo(strServerPath);
                if (Directory.Exists(strServer + "//" + strUserIp) != true)
                {
                    Directory.CreateDirectory((strServer + "//" + strUserIp));
                }
                if (Directory.Exists(strServer + "//" + strJID + "//" + strAID + "//" + strStage + "//CURRENT ORDER//") != true)
                {
                    Directory.CreateDirectory((strServer + "//" + strJID + "//" + strAID + "//" + strStage + "//CURRENT ORDER//"));
                }

                foreach (FileInfo filInfo in dirInfo.GetFiles())
                {
                    filInfo.CopyTo((strServer + "\\" + strUserIp + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\CURRENT ORDER\\" + filInfo.Name), true);
                    arrRet.Add(filInfo.Name);
                }
                return arrRet;
            
        }
        catch (Exception ex)
        {
            return arrRet;
        }
    }

    

}
