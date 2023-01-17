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
    string filename = "";
    public static DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminid"] == null)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            if (Session["adminid"].ToString() != "tdadminuser")
            {
                Response.Redirect("login.aspx");
            }
        }
        if (IsPostBack == false)
        {
            dt = new DataTable();
            dt.Columns.Add("JIDAID");
            dt.Columns.Add("Stage");
            dt.Columns.Add("REV");
            dt.Columns.Add("Response");
            dt.Columns.Add("OrderPath");

            TreeNode ParentNode = new TreeNode();
            ParentNode.Text = "Inbox";
            TreeView1.Nodes.Add(ParentNode);
            PopulateSubL1Nodes("E:\\Elsinpt\\VIEWER\\ORDERS", ParentNode);
        }
    }
    
    private void PopulateSubL1Nodes(string L1Path ,TreeNode ParentNodeL1)
    {
        
        string[] dir = Directory.GetDirectories(L1Path);
        for (int i = 0; i <= dir.Length - 1; i++)
        {
            TreeNode ChildNodeL1=new TreeNode();
            ChildNodeL1.Text = dir[i].ToString().Replace("E:\\Elsinpt\\VIEWER\\ORDERS" + "\\Year ", "");
            ParentNodeL1.ChildNodes.Add(ChildNodeL1);
            PopulateSubL2Nodes(dir[i].ToString(), ChildNodeL1);
        }
    }
    private void PopulateSubL2Nodes(string L2Path, TreeNode ParentNodeL2)
    {

        string[] dir = Directory.GetDirectories(L2Path);
        for (int i = 0; i <= dir.Length - 1; i++)
        {
            TreeNode ChildNodeL2 = new TreeNode();
            ChildNodeL2.Text = dir[i].ToString().Replace(L2Path + "\\", "");
            ParentNodeL2.ChildNodes.Add(ChildNodeL2);
            PopulateSubL3Nodes(dir[i].ToString(), ChildNodeL2);
        }
    }
    private void PopulateSubL3Nodes(string L3Path, TreeNode ParentNodeL3)
    {

        string[] dir = Directory.GetDirectories(L3Path);
        for (int i = 0; i <= dir.Length - 1; i++)
        {
            TreeNode ChildNodeL3 = new TreeNode();
            ChildNodeL3.Text = dir[i].ToString().Replace(L3Path + "\\", "");
            ParentNodeL3.ChildNodes.Add(ChildNodeL3);
        }
    }

    private void Populate_GridView(string Jid,string Stage, string Rev,string Response,string Orderpath)
    {       
        DataRow dr = dt.NewRow();
        dr[0] = Jid;
        dr[1] = Stage;
        dr[2] = Rev;
        dr[3] = Response;
        dr[4] = Orderpath;
        dt.Rows.Add(dr);
        
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string str = TreeView1.SelectedNode.Text;
        string[] spl=null;
        if (str.IndexOf(' ') > -1)
        {
            spl = str.Split(' ');
            if (spl[1].ToString().Trim() == TreeView1.SelectedNode.Parent.Text)
            {
                dt.Rows.Clear();
                string ItemPath = "E:\\Elsinpt\\VIEWER\\ORDERS\\Year " + TreeView1.SelectedNode.Parent.Parent.Text + "\\" + spl[1].ToString() + "\\" + str;
                string[] ItemFiles = Directory.GetFiles(ItemPath);
                string[] Item;
                for (int j = 0; j <= ItemFiles.Length - 1; j++)
                {
                    StreamReader sr = new StreamReader(ItemFiles[j].ToString());
                    string Contents = sr.ReadToEnd();
                    sr.Close();
                    int pos1 = 0;
                    int pos2 = 0;
                    pos1 = Contents.IndexOf("<PATH>");
                    pos2 = Contents.IndexOf("</PATH>");
                    string opth = Contents.Substring(pos1, pos2 - pos1);
                    opth = opth.Replace("<PATH>", "").Trim();
                    int pos3 = 0;
                    int pos4 = 0;
                    pos3 = Contents.IndexOf("<RESPONSE>");
                    pos4 = Contents.IndexOf("</RESPONSE>");
                    string resp = Contents.Substring(pos3, pos4 - pos3);
                    resp = resp.Replace("<RESPONSE>", "").Trim();
                    Item = Path.GetFileNameWithoutExtension(ItemFiles[j].ToString()).Split('_');
                    Populate_GridView(Item[0].ToString() + "_" + Item[1].ToString(), Item[2].ToString(), Item[3].ToString().Trim(), resp,opth);
                }                              
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    public void View_Order(object sender, EventArgs e)
    {
        int i;        
        try
        {

            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                RadioButton rbtn = (RadioButton)GridView1.Rows[i].FindControl("rborder");
                if (rbtn.Checked == true)
                {
                    rbtn.Focus();
                    rbtn.Checked = false;
                    GridView1.Rows[i].Font.Bold = false;                    
                    Session["path"] = GridView1.Rows[i].Cells[5].Text;
                    //FindSrc("OrderView.aspx"); 
                    filename = Path.GetFileNameWithoutExtension(GridView1.Rows[i].Cells[5].Text);
                    string[] arrvar = filename.Split('_');
                    strJID = arrvar[0].ToString();
                    strAID = arrvar[1].ToString();
                    strStage = arrvar[2].ToString();
                    objFileIO = new FileIO();
                    System.Configuration.ConfigurationManager.AppSettings["LocalServer"] = Request.UserHostAddress;
                    string strDir, strServer;
                    string strUserIp;
                    blStageSet = true;
                   
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
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        finally
        {
           
        }
    }
    public void FindSrc(string srcname)
    {
        HtmlControl ifmid = (HtmlControl)Page.FindControl("ifm");
        ifmid.Attributes["src"] = srcname;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[5].Visible = false;
        string jidaid = e.Row.Cells[1].Text;
        string stage = e.Row.Cells[2].Text;
        string localorderpath = Server.MapPath("App_Data") + "//" + "Orders";
        string UserIp = Request.UserHostAddress;
        if (Directory.Exists(localorderpath + "//" + UserIp + "//" + jidaid.Replace("_","//") + "//" + stage))
        {
            e.Row.Font.Bold = false;
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
                string utfvar1 = File.ReadAllText(strOrderPath, System.Text.Encoding.UTF8);
                StreamReader sr = null;
                if (utfvar1.IndexOf("iso-8859-1") > -1)
                {
                    //For iso-8859-1
                    sr = new StreamReader(strOrderPath, System.Text.Encoding.UTF8);
                }
                else
                {
                    //For UTF8
                    sr = new StreamReader(strOrderPath, System.Text.Encoding.UTF8);
                }


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
                    StreamWriter sw = null;
                    if (utfvar1.IndexOf("iso-8859-1") > -1)
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

                    File.Copy(Server.MapPath("xslt") + "\\diff" + strDTD, Server.MapPath("App_data") + "\\" + "Orders" + "//" + strUserIp + "//" + strJID + "//" + strAID + "//" + strStage + "//" + strDTD, true);

                    ViewState["StrOrderPath"] = strOrderPath;
                    ViewState["transformsource"] = Server.MapPath("xslt") + "//" + strXSLName;

                    RefineDiff(strOrderPath);

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


                    //==============================
                    //For UTF8
                    if (utfvar1.IndexOf("iso-8859-1") > -1)
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

    public void ReadWrite_Order(string orderpath)
    {
        string strXMLOrdertmp = "";
        StreamReader srtmp = new StreamReader(orderpath, System.Text.Encoding.UTF8);
        strXMLOrdertmp = srtmp.ReadToEnd();
        srtmp.Close();
        File.Delete(orderpath);
        StreamWriter swtmp = new StreamWriter(orderpath,false,System.Text.Encoding.UTF8);
        swtmp.Write(strXMLOrdertmp);
        swtmp.Close();
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

                strOrderPath1 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strFileName;
                strOrderPath2 = Server.MapPath("App_data") + "\\" + "Orders" + "\\" + Request.UserHostAddress + "\\" + strJID + "\\" + strAID + "\\" + strStage + "\\" + strOldOrderName;

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
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainForm.aspx");
    }
    
    protected void lnknormal_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void RefineDiff(string XMLFilePath)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.XmlResolver = null;
        xDoc.Load(XMLFilePath);
        XmlNodeList DiffNodes = xDoc.GetElementsByTagName("diff");

        foreach(XmlNode diff in DiffNodes)
        {
            if (diff.ParentNode != null && diff.ParentNode.Name.IndexOf("remark")==-1)
            { 
                XmlElement dummy = xDoc.CreateElement("dummy");
                dummy.InnerXml= diff.InnerXml;
                diff.ParentNode.ReplaceChild(dummy, diff);
            }
        }

        StringBuilder Str = new StringBuilder(xDoc.OuterXml);
        Str.Replace("<dummy>", "");
        Str.Replace("</dummy>", "");
        File.WriteAllText(XMLFilePath, Str.ToString()); 

    }
}



