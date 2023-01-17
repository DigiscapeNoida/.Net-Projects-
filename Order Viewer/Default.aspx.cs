using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.XmlDiffPatch;


public partial class _Default : System.Web.UI.Page 
{
    string strJID;
    string strAID;
    string strStage;     
    FileIO objFileIO;    
    ArrayList arrTemp;
    public Boolean blStageSet;
    string UserHostAddress = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
            UserHostAddress = Request.UserHostAddress;
            if (UserHostAddress.Equals("::1"))
            {
                UserHostAddress = "127.0.0.1";
            }
        if (Session["userid"] == null)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            if (Session["userid"].ToString() != "tduser")
            {
                Response.Redirect("login.aspx");
            }
        }
            //if (!this.IsCallback)
            //{
            //    if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            //    {
            //        Response.Redirect("login.aspx");
            //    }
            //}
            
            objFileIO = new FileIO();
            System.Configuration.ConfigurationManager.AppSettings["LocalServer"] = UserHostAddress;
            txtJID.Focus();
            txtOrderNotFound.Text = "";
            if (!IsPostBack)
            {
                  txtJID.Attributes.Add("onChange", "javascript:this.value=this.value.toUpperCase();");
                  txtAid.Attributes.Add("onChange", "javascript:this.value=this.value.toUpperCase();");
                txtStage.Attributes.Add("onChange", "javascript:this.value=this.value.toUpperCase();");
            }

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
    }

    protected void btnViewOrder_Click(object sender, EventArgs e)
    {
        strJID   = txtJID.Text.Trim().ToLower();
        strAID   = txtAid.Text.Trim().ToLower();
        strStage = txtStage.Text.Trim().ToLower();

        string strDir,strServer;
        string[] strArrStages;
        string strUserIp;
        //string strFilePathNoStage;
        int i;
        blStageSet = true;
        
        try
        {
            if (Page.IsValid == true)
            {
                if (txtStage.Text.Trim() != "")
                {
                    
                    strUserIp = UserHostAddress;
                    strServer = Server.MapPath("App_Data") + "//" + "Orders";                    
                    strDir = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage;
                    if (Directory.Exists(strDir))
                    {
                        arrTemp = objFileIO.AddOldOrders(strServer, strJID, strAID, strStage, strDir, strUserIp);
                        lbOrderList.Items.Clear();
                        for (i = 0; i < arrTemp.Count; i++)
                        {
                            lbOrderList.Items.Add(arrTemp[i].ToString());
                        }
                    }
                    else
                    {
                        txtOrderNotFound.Text = "Order could not be found for these JID/AID/ISSUE. Please try again with valid JID/AID/ISSUE.";
                        lbOrderList.Items.Clear();
                        txtJID.Focus();
                    }
                    //arrTemp = objFileIO.AddCurrentOrder(strServer, strJID, strAID, strStage, System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage + "\\CURRENT ORDER\\");
                    //for (i = 0; i < arrTemp.Count; i++)
                    //{
                    //    lbOrderList.Items.Add(arrTemp[i].ToString());
                    //}

                }
                else
                {
                   strUserIp = UserHostAddress;
                   if (strUserIp.Equals("::1"))
                   {
                       strUserIp = "127.0.0.1";
                   }
                   blStageSet = false;
                   strArrStages=System.Configuration.ConfigurationManager.AppSettings["Stages"].Split(Convert.ToChar(';'));
                   lbOrderList.Items.Clear();

                   string ServerPath = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] ;
                   for (i = 0; i < strArrStages.Length; i++)
                   {
                       string StageDir = ServerPath + strJID + "\\" + strAID + "\\" + strArrStages[i];
                       if (Directory.Exists(StageDir))
                       {
                           if(strArrStages[i].Trim() != "")
                           {
                                //lbOrderList.Items.Add(strArrStages[i]);                                  
                               strServer = Server.MapPath("App_data") + "\\" + "Orders";
                               strDir = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strArrStages[i].Trim();
                               arrTemp = objFileIO.AddOldOrders(strServer, strJID, strAID, strArrStages[i].Trim(), strDir,strUserIp);
                               //lbOrderList.Items.Clear();
                               for (int j = 0; j < arrTemp.Count; j++)
                               {
                                   lbOrderList.Items.Add(arrTemp[j].ToString());
                               }
                           }
                       }
                   }
                   if (lbOrderList.Items.Count == 0)
                   {
                       txtOrderNotFound.Text = "Order could not be found for these JID/AID/ISSUE. Please try again with valid JID/AID/ISSUE.";
                       lbOrderList.Items.Clear();
                       txtJID.Focus();
                   }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtJID_TextChanged(object sender, EventArgs e)
    {
        txtAid.Enabled = true;
        txtStage.Enabled = true;        
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void lbOrderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] strArrStage;
        string strTempSelected;
        string strXMLOrder, strXSLName="",strOrderPath;
        string strUserIp = UserHostAddress;

        if (strUserIp.Equals("::1"))
            strUserIp = "127.0.0.1";

        string strDTD;

        strArrStage = lbOrderList.SelectedValue.ToString().Split(Convert.ToChar('_'));
        strStage = strArrStage[2];
        ViewState["Stage"] = strStage;
        //strStage = txtStage.Text.Trim();
        try
        {
            strJID=txtJID.Text.Trim().ToLower();
            strAID = txtAid.Text.Trim().ToLower();

            //if (strStage == "" )//&& (string)ViewState["StageSelected"]== null )
            //{
            //    blStageSet = false;
            //    strStage = lbOrderList.SelectedValue.ToString();
            //    txtStage.Text = lbOrderList.SelectedValue.ToString();
            //}
            //else
            //{
            //    blStageSet = true;
            //    strStage = txtStage.Text.Trim();
            //}
            blStageSet = true;
            strTempSelected = lbOrderList.SelectedValue.ToString();
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

                string utfvar = File.ReadAllText(strOrderPath, System.Text.Encoding.UTF8);

                //if (utfvar.IndexOf("<orders>") != -1)
                //{
                //    int sPos = utfvar.IndexOf("<orders>");
                //    string TempXML = utfvar.Substring(sPos);
                //    XmlDocument xDoc = new XmlDocument();
                //    xDoc.LoadXml(TempXML);
                //    XmlNodeList NL = xDoc.SelectNodes(".//text()");

                //    foreach (XmlNode Node in NL)
                //    {
                //        if (Node.InnerText.IndexOf("-") != -1)
                //        {
                //            Node.InnerText = Node.InnerText.Replace("-", "&#45;");
                //        }
                //    }
                   
                //    File.WriteAllText(strOrderPath, utfvar.Substring(0, sPos) +xDoc.OuterXml);
                //}


                StreamReader sr = null;
                if (utfvar.IndexOf("iso-8859-1") > -1)
                {
                    //For iso-8859-1
                    sr = new  StreamReader(strOrderPath,System.Text.Encoding.UTF8);
                }
                else
                {
                    ////For UTF8
                    sr = new StreamReader(strOrderPath, System.Text.Encoding.UTF8);
                }
                strXMLOrder = sr.ReadToEnd();
                
                int i, j;

                i = strXMLOrder.IndexOf("<!DOCTYPE orders ");
                j= strXMLOrder.IndexOf(".dtd\">");
                strDTD= strXMLOrder.Substring(i+25,(j+4)-(i+25));
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
                    if(strTempSelected.Contains("("))
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
                    StreamWriter sw = null;
                    if (utfvar.IndexOf("iso-8859-1") > -1)
                    {
                        //strXMLOrder = strXMLOrder.Replace("iso-8859-1", "UTF-8");
                        sw = new StreamWriter(strOrderPath);
                    }
                    else
                    {
                        //strXMLOrder = strXMLOrder.Replace("iso-8859-1", "UTF-8");
                        sw = new StreamWriter(strOrderPath, false, System.Text.Encoding.UTF8);
                    }
                    sw.Write(strXMLOrder);
                    sw.Close();
                    

                    XmlCompare(strTempSelected, strJID, strAID);

                    File.Copy(Server.MapPath("xslt") + "\\diff" + strDTD, Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strDTD,true);

                    ViewState["StrOrderPath"] = strOrderPath;
                    ViewState["transformsource"] = Server.MapPath("xslt") + "//" + strXSLName;

                    RefineDiff(strOrderPath);
                    Xml1.DocumentSource = strOrderPath;
                   
                    Xml1.TransformSource = Server.MapPath("xslt") + "//" + strXSLName;
                    
                    

                }

                        //XPathDocument doc = new XPathDocument(Server.MapPath("Orders") + "//" + Flname);
                //XslTransform transform = new XslTransform();
                //transform.Load(this.MapPath("orders") + "//" + xslname);
                // Transform XML data.
                //transform.Transform(doc,null, Response.Output);

                        //if(File.Exists(Server.MapPath("Orders") + "//" + Flname))
                //{
                //    File.Delete(Server.MapPath("Orders") + "//" + Flname);
                //}
                //}
                else
                {
                    if (strTempSelected.ToString().ToLower().StartsWith(strJID))
                    {
                        strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strTempSelected;
                    }
                    else
                    {
                        //strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + "CURRENT ORDER" + "//" + strTempSelected;
                        strOrderPath = Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage  + "//" + strTempSelected;
                    }

                    //==============================
                    //For UTF-8
                    if (utfvar.IndexOf("iso-8859-1") > -1)
                    {                        
                    }
                    else
                    {
                        ReadWrite_Order(strOrderPath);
                    }

                    //==============================                   
                    
                    File.Copy((Server.MapPath("xslt") + "\\diff" + strDTD), (Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strDTD), true);
                    XmlCompare(strTempSelected, strJID, strAID);

                    ViewState["StrOrderPath"] = strOrderPath;
                    ViewState["transformsource"] = Server.MapPath("xslt") + "//" + strXSLName;


                    RefineDiff(strOrderPath);
                    Xml1.DocumentSource = strOrderPath;
                    Xml1.TransformSource = Server.MapPath("xslt") + "//" + strXSLName;




                    //XPathDocument doc = new XPathDocument(Server.MapPath("Orders") + "//" + Flname);
                    //XslTransform transform = new XslTransform();
                    //transform.Load(this.MapPath("orders") + "//" + xslname);
                    //// Transform XML data.
                    //transform.Transform(doc,null, Response.Output);

                    //if(File.Exists(Server.MapPath("Orders") + "//" + Flname))
                    //{
                    //    File.Delete(Server.MapPath("Orders") + "//" + Flname);
                    //}
                }

            }
            else if (blStageSet == false)
            {
                string strDir, strServer;
                int i;
                
                ViewState["StageSelected"]="Deselected" ;                      
                blStageSet = true;
                strServer = Server.MapPath("Orders");
                strDir = System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage;
                arrTemp = objFileIO.AddOldOrders(strServer, strJID, strAID, strStage, strDir,strUserIp);
                lbOrderList.Items.Clear();
                for (i = 0; i < arrTemp.Count; i++)
                {
                    lbOrderList.Items.Add(arrTemp[i].ToString());
                }
                //arrTemp = objFileIO.AddCurrentOrder(strServer, strJID, strAID, strStage, System.Configuration.ConfigurationManager.AppSettings["ServerPath"] + strJID + "\\" + strAID + "\\" + strStage + "\\CURRENT ORDER\\");
                //for (i = 0; i < arrTemp.Count; i++)
                //{
                //    lbOrderList.Items.Add(arrTemp[i].ToString());
                //}
            }
			
        }
        catch(Exception Ex)
        {
            Response.Write(Ex.Message.ToString());
        }
        
        
    }
    public void ReadWrite_Order(string orderpath)
    {
        string strXMLOrdertmp = "";
        StreamReader srtmp = new StreamReader(orderpath,System.Text.Encoding.UTF8);
        strXMLOrdertmp = srtmp.ReadToEnd();
        srtmp.Close();
        File.Delete(orderpath);


        //string utf8String = strXMLOrdertmp;
        //byte[] utf8Bytes = new byte[utf8String.Length];
        //for (int i = 0; i < utf8String.Length; ++i)
        //{
        //    //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
        //    utf8Bytes[i] = (byte)utf8String[i];
        //}
        
        //string temp= System.Text.Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);

        //File.WriteAllText(orderpath, temp);
        StreamWriter swtmp = new StreamWriter(orderpath, false, System.Text.Encoding.UTF8);
        swtmp.Write(strXMLOrdertmp);
        swtmp.Close();
    }

    
    public void XmlCompare(string strFileName, string strJID, string strAID)
    {
        string[] strTempArr,strTempArr1;
        string strTemp, strStage, strFileNo;
        string strOldOrderName;        
        string strOrderPath1="", strOrderPath2="", strXMl1;// strXML2;
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
                    strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strOldOrderName;

                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                

                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml"))
                {
                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                }


               
                XmlTextWriter diffGramWriter = new XmlTextWriter(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", null);
                GenerateDiffGram(strOrderPath1, strOrderPath2, diffGramWriter);

                ////StreamReader sr = new StreamReader(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                ////strDiff = sr.ReadToEnd();
                ////sr.Close;

                ////if (strDiff.Contains("<xd:add>")>0)
                ////{
                ////    int j;
                ////    int k;
                ////    j = strDiff.IndexOf("<xd:add>");
                ////    k = strDiff.indes("</xd:add>");
                ////    strTemp = strDiff.Substring(j + 8, k - j);
                ////    if (strTemp.Contains("<remark>")>0)
                ////    {

                ////    }

                ////}


                XmlDocument xDoc = new XmlDocument();
                XmlNodeList xlist;
                ArrayList arrNodeTextList = new ArrayList();
                int intCount = 0;
                FileStream fs;

                fs = new FileStream(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", FileMode.Open);
                xDoc.Load(fs);
                fs.Close();
                xlist = xDoc.GetElementsByTagName("remark");

                //item-remark
                //issue-remark
                foreach (XmlNode node in xlist)
                {
                    if ((node.ParentNode.ParentNode.ParentNode.Name== "xd:add") || (node.ParentNode.ParentNode.Name== "xd:add"))
                    {
                        intCount = intCount + 1;
                        arrNodeTextList.Add(node.InnerText);                       
                        
                    }
                }

                //strXMl1 = File.ReadAllText(strOrderPath1, System.Text.Encoding.UTF8);
                StreamReader sr = new StreamReader(strOrderPath1, System.Text.Encoding.UTF8);
                strXMl1 = sr.ReadToEnd();
                sr.Close();

                


                for (i = 0; i < arrNodeTextList.Count; i++)
                {
                    if (strXMl1.Contains(arrNodeTextList[i].ToString()))
                    {
                        strXMl1 = strXMl1.Replace("<remark>" + arrNodeTextList[i].ToString() + "</remark>", "<diff><remark>" + arrNodeTextList[i].ToString().Trim() + "</remark></diff>");
                    }                    
                }
                while(strXMl1.Contains("<diff><diff>"))
                {
                    strXMl1 = strXMl1.Replace("<diff><diff>","<diff>");
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
                StreamWriter sw =new StreamWriter(strOrderPath1);
                sw.Write(strXMl1);
                sw.Close();

                //StreamReader sr = new StreamReader(strOrderPath1);
                //strXMl1 = sr.ReadToEnd();
                //sr.Close();

                //sr = new StreamReader(strOrderPath2);
                //strXML2 = sr.ReadToEnd();
                //sr.Close();

            }
            else if(Convert.ToInt16(strFileNo)==0)
            {
                string strStageBefore="";
                DirectoryInfo di;
                FileInfo[] fi;
                if (strStage != "S5")
                {
                    strTempArr1 = System.Configuration.ConfigurationManager.AppSettings["Stages"].Split(Convert.ToChar(";"));
                    for (i = 1; i < strTempArr1.Length; i++)
                    {
                        if(strStage==strTempArr1[i])
                        {
                            for (int j = i-1; j >1; j--)
                            {
                                if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strTempArr1[j]) && strTempArr1[j]!= "")
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
                            if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                            {
                                di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);
                                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                {
                                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                }
                                fi = di.GetFiles("*.xml");
                                strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32 (fi.Length) - 1) + ".xml";
                                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                            }
                            else
                            {
                                strStageBefore = strStageBefore.Replace("RESUPPLY", "");
                                if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                                {
                                    di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);
                                    if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                    {
                                        File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                    }
                                    fi = di.GetFiles("*.xml");
                                    strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32(fi.Length) - 1) + ".xml";
                                    strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                    strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                                }
                            }
                        }
                        else
                        {   if (Directory.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore))
                            {
                                di = new DirectoryInfo(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore);                            
                                if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml"))
                                {
                                    File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + "diff.xml");
                                }
                                fi = di.GetFiles("*.xml");
                                strOldOrderName = strJID + "_" + strAID + "_" + strStageBefore + "_" + (Convert.ToInt32(fi.Length) - 1) + ".xml";
                                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                                strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStageBefore + "\\" + strOldOrderName;
                            }
                        }
                        if (File.Exists(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml"))
                        {
                            File.Delete(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                        }
                        if (strOrderPath1 != "" && strOrderPath2 != "")
                        {
                            XmlTextWriter diffGramWriter = new XmlTextWriter(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", null);
                            GenerateDiffGram(strOrderPath1, strOrderPath2, diffGramWriter);

                            ////StreamReader sr = new StreamReader(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml");
                            ////strDiff = sr.ReadToEnd();
                            ////sr.Close;

                            ////if (strDiff.Contains("<xd:add>")>0)
                            ////{
                            ////    int j;
                            ////    int k;
                            ////    j = strDiff.IndexOf("<xd:add>");
                            ////    k = strDiff.indes("</xd:add>");
                            ////    strTemp = strDiff.Substring(j + 8, k - j);
                            ////    if (strTemp.Contains("<remark>")>0)
                            ////    {

                            ////    }

                            ////}

                            XmlDocument xDoc = new XmlDocument();
                            XmlNodeList xlist;
                            ArrayList arrNodeTextList = new ArrayList();
                            int intCount = 0;
                            FileStream fs;

                            fs = new FileStream(Server.MapPath("App_data") + "\\" + "Orders" + "\\" + UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + "diff.xml", FileMode.Open);
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

                          


                            StreamReader sr = new StreamReader(strOrderPath1,System.Text.Encoding.UTF8);
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
    public void GenerateDiffGram(string originalFile, string finalFile,XmlTextWriter diffGramWriter)
    {

      



        XmlDiff xmldiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder |
                                         XmlDiffOptions.IgnoreNamespaces |
                                         XmlDiffOptions.IgnorePrefixes);
        bool bIdentical = xmldiff.Compare(finalFile, originalFile, false, diffGramWriter);
        diffGramWriter.Close();
    }
    protected void lnkprint_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbOrderList.SelectedIndex != -1)
            {
                RegisterClientScriptBlock("print", "<script language='javascript'> window.print(); </script>");
            }
            else
            {
                txtOrderNotFound.Text = "No orders selected. Please select an order first";
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void lnksave_Click(object sender, EventArgs e)
    {
        string strTempPath;
        string strOrderPath;
        string strJID, strAID, strStage, strFileName;
        string strCurrentOrderFileName;
        string[] strArr;
        int intPos;
        FileInfo fi;
        try
        {
            if (lbOrderList.SelectedIndex != -1)
            {
                strJID = txtJID.Text.Trim().ToLower();
                strAID = txtAid.Text.Trim().ToLower();
                strFileName = lbOrderList.SelectedValue.ToString().Trim();
                strStage = ViewState["Stage"].ToString().Trim();
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
            else
            {
                txtOrderNotFound.Text = "No orders selected. Please select an order first";
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkadvanced_Click(object sender, EventArgs e)
    {
        Response.Redirect("Security.aspx");        
    }
    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        //FormsAuthentication.SignOut();
        //Response.Buffer = true;
        //Response.Cache.SetCacheability(HttpCacheability.NoCache)
        //Response.ExpiresAbsolute = DateTime.Now().AddDays(-1)
        //Response.Expires = -1500;
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now); 
        //Response.Cookies.Clear();
        //Response.Cache.SetNoStore();
        //Response.CacheControl = "no-cache";
        //Response.AddHeader("Pragma", "no-cache");
        //Response.Expires = -1;
        System.Web.Security.FormsAuthentication.SignOut();
        Session.Clear();
        Session.Abandon();     
        Response.Redirect("login.aspx");

    }
    private void RefineDiff(string XMLFilePath)
    {
        StringBuilder Str = new StringBuilder(File.ReadAllText(XMLFilePath));

        Str.Replace("&", "$#$");

        XmlDocument xDoc = new XmlDocument();
        xDoc.XmlResolver = null;
        xDoc.LoadXml(Str.ToString());
        XmlNodeList DiffNodes = xDoc.GetElementsByTagName("diff");

        for (int i = 0; i < DiffNodes.Count; i++)
        {
            XmlNode diff = DiffNodes[i];
            if (diff.ParentNode != null && diff.ParentNode.Name.IndexOf("remark") == -1)
            {
                XmlElement dummy = xDoc.CreateElement("dummy");
                dummy.InnerXml = diff.InnerXml;
                diff.ParentNode.ReplaceChild(dummy, diff);
                i--;
            }
        }

        Str = new StringBuilder(xDoc.OuterXml);
        Str.Replace("<dummy>", "");
        Str.Replace("</dummy>", "");
        Str.Replace( "$#$","&");
        Str.Replace( "$#$","&");
        Str.Replace("[]", "");
        

        File.WriteAllText(XMLFilePath, Str.ToString());

    }
}
