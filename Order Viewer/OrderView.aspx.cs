using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
public partial class OrderView : System.Web.UI.Page
{
    string strJID;
    string strAID;
    string strStage;
    FileIO objFileIO;
    ArrayList arrTemp;
    public Boolean blStageSet;
    string filename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        filename = Path.GetFileNameWithoutExtension(Session["Path"].ToString());
        string[] arrvar = filename.Split('_');
        strJID = arrvar[0].ToString();
        strAID = arrvar[1].ToString();
        strStage = arrvar[2].ToString();
        objFileIO = new FileIO();
        System.Configuration.ConfigurationManager.AppSettings["LocalServer"] = Request.UserHostAddress;
        string strDir, strServer;       
        string strUserIp;      
        blStageSet = true;
        try
        {
                strUserIp = Request.UserHostAddress;
                strServer = Server.MapPath("App_Data") + "//" + "Orders";
                strDir = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage;
                if (Directory.Exists(strDir))
                {
                    arrTemp = objFileIO.AddOldOrders(strServer, strJID, strAID, strStage, strDir, strUserIp);
                    ////lbOrderList.Items.Clear();
                    ////for (i = 0; i < arrTemp.Count; i++)
                    ////{
                    ////    lbOrderList.Items.Add(arrTemp[i].ToString());
                    ////}
                }
                else
                {
                    Response.Write("Order you have specified not found. Please enter the values again.");
                    ////lbOrderList.Items.Clear();
                    ////txtJID.Focus();
                }

                Select_Order();
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        finally
        {
        }              
		
    }
    private void Select_Order()
    {
        string strTempSelected;
        string strXMLOrder, strXSLName = "", strOrderPath;
        string strUserIp = Request.UserHostAddress;
        string strDTD;

        ViewState["Stage"] = strStage;
        try
        {
            blStageSet = true;
            strTempSelected = filename + ".xml";
            if (blStageSet == true)
            {

                if (strTempSelected.ToString().ToLower().StartsWith(strJID))
                {
                    strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strTempSelected;
                }
                else
                {
                    strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strTempSelected;
                }

                StreamReader sr = new StreamReader(strOrderPath, System.Text.Encoding.Default);
                strXMLOrder = sr.ReadToEnd();

             


                int i, j;

                i = strXMLOrder.IndexOf("<!DOCTYPE orders ");
                j = strXMLOrder.IndexOf(".dtd\">");
                strDTD = strXMLOrder.Substring(i + 25, (j + 4) - (i + 25));
                i = strXMLOrder.IndexOf("href=");
                if (i > 0)
                {
                    i = i + 6;
                    j = strXMLOrder.IndexOf("?>", i);
                    if (j > i)
                        strXSLName = strXMLOrder.Substring(i, j - i - 2);
                    strXSLName = strXSLName.Trim();
                }
                sr.Close();
                if (strXMLOrder.IndexOf("<batch>") > 0)
                {
                    if (strTempSelected.Contains("("))
                    {
                        strXMLOrder = strXMLOrder.Replace(strXSLName, "issue-duckling.xsl");
                        strXSLName = "issue-duckling.xsl";
                    }
                    else
                    {
                        strXMLOrder = strXMLOrder.Replace(strXSLName, "duck-internal.xsl");
                        strXSLName = "duck-internal.xsl";
                    }
                    File.Delete(strOrderPath);
                    strXMLOrder = strXMLOrder.Replace("iso-8859-1", "UTF-8");
                    StreamWriter sw = new StreamWriter(strOrderPath);
                    sw.Write(strXMLOrder);
                    sw.Close();


                    XmlCompare(strTempSelected, strJID, strAID);

                    File.Copy(Server.MapPath("xslt") + "\\diff" + strDTD, Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strDTD, true);

                    ViewState["StrOrderPath"] = strOrderPath;
                    ViewState["transformsource"] = Server.MapPath("xslt") + "//" + strXSLName;
                    Xml1.DocumentSource = strOrderPath;
                    Xml1.TransformSource = Server.MapPath("xslt") + "//" + strXSLName;

                }
                else
                {
                    if (strTempSelected.ToString().ToLower().StartsWith(strJID))
                    {
                        strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strTempSelected;
                    }
                    else
                    {
                        strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strTempSelected;
                    }

                    File.Copy((Server.MapPath("xslt") + "\\diff" + strDTD), (Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strDTD), true);
                    XmlCompare(strTempSelected, strJID, strAID);

                    ViewState["StrOrderPath"] = strOrderPath;
                    ViewState["transformsource"] = Server.MapPath("xslt") + "//" + strXSLName;

                    Xml1.DocumentSource = strOrderPath;
                    Xml1.TransformSource = Server.MapPath("xslt") + "//" + strXSLName;


                }

            }
            else if (blStageSet == false)
            {
                string strDir, strServer;
                ViewState["StageSelected"] = "Deselected";
                blStageSet = true;
                strServer = Server.MapPath("Orders");
                strDir = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage;
                arrTemp = objFileIO.AddOldOrders(strServer, strJID, strAID, strStage, strDir, strUserIp);                

            }

        }
        catch (Exception Ex)
        {
            Response.Write(Ex.Message.ToString());
        }
    }
    public void XmlCompare(string strFileName, string strJID, string strAID)
    {
        string[] strTempArr, strTempArr1;
        string strTemp, strStage, strFileNo;
        string strOldOrderName;
        string strOrderPath1 = "", strOrderPath2 = "", strXMl1;// strXML2;
        //string strDiff;
        int i;
        int intTemp;

        try
        {
            strTempArr = strFileName.Split(Convert.ToChar('_'));
            strStage = strTempArr[strTempArr.Length - 2];
            strTemp = strTempArr[strTempArr.Length - 1];
            strTempArr = strTemp.Split(Convert.ToChar('.'));
            strFileNo = strTempArr[0];
            if (Convert.ToInt16(strFileNo) > 0)
            {
                intTemp = Convert.ToInt16(strFileNo) - 1;
                strOldOrderName = strJID + "_" + strAID + "_" + strStage + "_" + intTemp + ".xml";



                string ZeroVrsnOrderName = strJID + "_" + strAID + "_" + strStage.Replace("RESUPPLY", "") + "_0.xml";
                string ZeroVrsnOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage.Replace("RESUPPLY", "") + "\\" + ZeroVrsnOrderName;
                if (File.Exists(ZeroVrsnOrderPath))
                    strOrderPath2 = ZeroVrsnOrderPath;
                else
                    strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strOldOrderName;

                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;


                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml"))
                {
                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                }

                XmlTextWriter diffGramWriter = new XmlTextWriter(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", null);
                GenerateDiffGram(strOrderPath1, strOrderPath2, diffGramWriter);

                XmlDocument xDoc = new XmlDocument();
                XmlNodeList xlist;
                ArrayList arrNodeTextList = new ArrayList();
                int intCount = 0;
                FileStream fs;

                fs = new FileStream(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", FileMode.Open);
                xDoc.Load(fs);
                fs.Close();
                xlist = xDoc.GetElementsByTagName("remark");
                foreach (XmlNode node in xlist)
                {
                    if ((node.ParentNode.ParentNode.ParentNode.Name == "xd:add") || (node.ParentNode.ParentNode.Name == "xd:add"))
                    {
                        intCount = intCount + 1;
                        arrNodeTextList.Add(node.InnerText);

                    }
                }

                //strXMl1 = File.ReadAllText(strOrderPath1, System.Text.Encoding.UTF8);
                StreamReader sr = new StreamReader(strOrderPath1, System.Text.Encoding.Default);
                strXMl1 = sr.ReadToEnd();
                sr.Close();

                for (i = 0; i < arrNodeTextList.Count; i++)
                {
                    if (strXMl1.Contains(arrNodeTextList[i].ToString()))
                    {
                        strXMl1 = strXMl1.Replace("<remark>" + arrNodeTextList[i].ToString() + "</remark>", "<diff><remark>" + arrNodeTextList[i].ToString().Trim() + "</remark></diff>");
                    }
                }
                while (strXMl1.Contains("<diff><diff>"))
                {
                    strXMl1 = strXMl1.Replace("<diff><diff>", "<diff>");
                }
                while (strXMl1.Contains("</diff></diff>"))
                {
                    strXMl1 = strXMl1.Replace("</diff></diff>", "</diff>");
                }
                if (File.Exists(strOrderPath1))
                {
                    File.Delete(strOrderPath1);
                }
                strXMl1 = strXMl1.Replace("iso-8859-1", "UTF-8");
                strXMl1 = strXMl1.Replace(" UTF-8", "UTF-8");
                strXMl1 = strXMl1.Replace("UTF-8 ", "UTF-8");
                StreamWriter sw = new StreamWriter(strOrderPath1);
                sw.Write(strXMl1);
                sw.Close();

            }
            else if (Convert.ToInt16(strFileNo) == 0)
            {
                string strStageBefore = "";
                DirectoryInfo di;
                FileInfo[] fi;
                if (strStage != "S5")
                {
                    strTempArr1 = System.Configuration.ConfigurationManager.AppSettings["Stages"].Split(Convert.ToChar(";"));
                    for (i = 1; i < strTempArr1.Length; i++)
                    {
                        if (strStage == strTempArr1[i])
                        {
                            for (int j = i - 1; j > 1; j--)
                            {
                                if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strTempArr1[j]) && strTempArr1[j] != "")
                                {
                                    strStageBefore = strTempArr1[j];
                                    break;
                                }
                            }
                        }
                    }
                    if (strStageBefore != "")
                    {
                        if (strStageBefore.ToLower().Contains("resupply"))
                        {
                            if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                            {
                                di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);
                                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                {
                                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                }
                                fi = di.GetFiles("*.xml");
                                strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32(fi.Length) - 1) + ".xml";
                                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                            }
                            else
                            {
                                strStageBefore = strStageBefore.Replace("RESUPPLY", "");
                                if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                                {
                                    di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);
                                    if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                    {
                                        File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                    }
                                    fi = di.GetFiles("*.xml");
                                    strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32(fi.Length) - 1) + ".xml";
                                    strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                    strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                            {
                                di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);
                                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                {
                                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                }
                                fi = di.GetFiles("*.xml");
                                strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32(fi.Length) - 1) + ".xml";
                                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                            }
                        }
                        if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml"))
                        {
                            File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                        }
                        if (strOrderPath1 != "" && strOrderPath2 != "")
                        {
                            XmlTextWriter diffGramWriter = new XmlTextWriter(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", null);
                            GenerateDiffGram(strOrderPath1, strOrderPath2, diffGramWriter);

                            XmlDocument xDoc = new XmlDocument();
                            XmlNodeList xlist;
                            ArrayList arrNodeTextList = new ArrayList();
                            int intCount = 0;
                            FileStream fs;

                            fs = new FileStream(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", FileMode.Open);
                            xDoc.Load(fs);
                            fs.Close();
                            xlist = xDoc.GetElementsByTagName("remark");
                            foreach (XmlNode node in xlist)
                            {
                                if ((node.ParentNode.ParentNode.ParentNode.Name == "xd:add") || (node.ParentNode.ParentNode.Name == "xd:add"))
                                {
                                    intCount = intCount + 1;
                                    arrNodeTextList.Add(node.InnerText);

                                }
                            }

                            StreamReader sr = new StreamReader(strOrderPath1, System.Text.Encoding.Default);
                            strXMl1 = sr.ReadToEnd();
                            sr.Close();

                            for (i = 0; i < arrNodeTextList.Count; i++)
                            {
                                if (strXMl1.Contains(arrNodeTextList[i].ToString()))
                                {
                                    strXMl1 = strXMl1.Replace("<remark>" + arrNodeTextList[i].ToString() + "</remark>", "<diff><remark>" + arrNodeTextList[i].ToString().Trim() + "</remark></diff>");
                                }
                            }
                            while (strXMl1.Contains("<diff><diff>"))
                            {
                                strXMl1 = strXMl1.Replace("<diff><diff>", "<diff>");
                            }
                            while (strXMl1.Contains("</diff></diff>"))
                            {
                                strXMl1 = strXMl1.Replace("</diff></diff>", "</diff>");
                            }
                            if (File.Exists(strOrderPath1))
                            {
                                File.Delete(strOrderPath1);
                            }
                            //File.WriteAllText(strOrderPath1,strXMl1,System.Text.Encoding.UTF8);
                            strXMl1 = strXMl1.Replace("iso-8859-1", "UTF-8");
                            StreamWriter sw = new StreamWriter(strOrderPath1);
                            sw.Write(strXMl1);
                            sw.Close();
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void GenerateDiffGram(string originalFile, string finalFile, XmlTextWriter diffGramWriter)
    {
        XmlDiff xmldiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreNamespaces | XmlDiffOptions.IgnorePrefixes);
        bool bIdentical = xmldiff.Compare(finalFile, originalFile, false, diffGramWriter);
        diffGramWriter.Close();
    }
    protected void lnksave_Click(object sender, EventArgs e)
    {
        string strTempPath;
        string strOrderPath;
        string strFileName;
        string strCurrentOrderFileName;
        string[] strArr;
        int intPos;
        FileInfo fi;
        try
        {
            
                //strJID = txtJID.Text.Trim().ToLower();
                //strAID = txtAid.Text.Trim().ToLower();
                //strFileName = lbOrderList.SelectedValue.ToString().Trim();
                //strStage = ViewState["Stage"].ToString().Trim();
                strFileName = Path.GetFileName(Session["Path"].ToString());
                strOrderPath = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                strTempPath = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage + "\\" + "CURRENT ORDER";
                strArr = Directory.GetFiles(strTempPath + "\\");
                intPos = strArr[0].LastIndexOf('\\');
                strCurrentOrderFileName = strArr[0].Substring(intPos + 1);
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strCurrentOrderFileName);
                fi = new FileInfo(strOrderPath);
                Response.AddHeader("Content-Length", fi.Length.ToString());
                //Response.ClearHeaders();
                Response.WriteFile(strOrderPath);
                Response.End();

            
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
    }
    protected void lnkprint_Click(object sender, EventArgs e)
    {
        try
        {
            RegisterClientScriptBlock("print", "<script language='javascript'> window.print(); </script>");
            
        }
        catch (Exception ex)
        {
        }
    }
}
