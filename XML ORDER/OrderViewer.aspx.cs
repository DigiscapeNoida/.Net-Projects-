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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
public partial class OrderViewer : System.Web.UI.Page
{
    public string OrderRootPath = "";
    public string AFSRootPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            
            Label lblUser = new Label();
                lblUser.Text = Session["UserName"].ToString();
                //Response.Write(Session["UserName"].ToString());
            
        }
        else
        {
            Label lblUser = new Label();
            lblUser.Text = "Session not initialized for UserName";
        }
        if (Page.IsPostBack == false)
        {
            FillJID();
        }
        if (Session["Account"] != null)
        {

            if (Session["Account"].ToString() == "JW-JOURNALS")
            {
                
                Label4.Visible = true;
                cmbAccount.Visible = true;
            }
            else
            {
                Label4.Visible = false;
                cmbAccount.Visible = false;
            }
        }
        else
        {
            this.RegisterClientScriptBlock("alert", "<script>alert(' Session variable found null for Account Name. \n:Page Load Function:');</script>");
        }

        //Transform(Server.MapPath(@"validation\tmp.xml"), Server.MapPath(@"validation\WileyJ-Order.xsl"));
        txtOrderNotFound.Text = "";
//        ExpirePageCache();
        
    }

    public void Transform(string sXmlPath, string sXslPath)
    {

        try
        {

            //load the Xml doc
            XPathDocument myXPathDoc = new XPathDocument(sXmlPath);

            XslTransform myXslTrans = new XslTransform();

            //load the Xsl 
            myXslTrans.Load(sXslPath);

            //create the output stream
            XmlTextWriter myWriter = new XmlTextWriter(Server.MapPath("Order.html"), null);
                
            //do the actual transform of Xml
            myXslTrans.Transform(myXPathDoc, null, myWriter);

            myWriter.Close();


        }
        catch (Exception e)
        {
            string alertScript = "<script language=JavaScript>alert('" +e.ToString()+ "')</script> ";
            this.RegisterClientScriptBlock("alert", alertScript);

        }

    }


    private void ExpirePageCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }


    protected void btnViewOrder_Click(object sender, EventArgs e)
    {
        string strJID, strAID, strStage;
        strJID = cmbJID.Text.Trim().ToLower();
        strAID = txtAID.Text.Trim().ToLower();
        strStage = cmbStage.Text.Trim().ToLower(); 
        string strDir;

        try
        {
            if (Page.IsValid == true)
            {
                if (cmbStage.Text.Trim() != "")
                {
                    strDir = System.Configuration.ConfigurationManager.AppSettings["OrderPath"] + "\\" + cmbAccount.Text.Trim() + "\\" + cmbJID.Text.Trim() + "\\" + cmbStage.Text.Trim() + "\\" + txtAID.Text.Trim()+"\\CurrentOrder\\";
                    string[] xmlOrder = Directory.GetFiles(strDir);
                    if (xmlOrder.Length == 1)
                    {
                        Xml1.DocumentSource = xmlOrder[0];
                        Xml1.TransformSource = Server.MapPath("validation") + "//WileyJ-Order.xsl";
                    }
                    else
                    {
                        string alertScript = "<script language=JavaScript>";
                        alertScript += "alert('--- Order not in proper format ---')";
                        alertScript += "</script" + "> ";
                        this.RegisterClientScriptBlock("alert", alertScript);

                    }
                }

            }
        }
        catch (Exception ex)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('--- "+ex.Message+" ---')";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);

        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
                RegisterClientScriptBlock("print", "<script language='javascript'> window.print(); </script>");
        }
        catch (Exception ex)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('--- " + ex.Message + " ---')";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
        }
    }

    private void GetOrderRootPath()
    {
        XmlTextReader AFSreader = new XmlTextReader(Server.MapPath("AFSRoot.xml"));
        string OrderTAGName = "";
        OrderTAGName = Session["Account"].ToString() + "-ORDER";

        while (AFSreader.Read() == true)
        {
            if (AFSreader.Name.Replace(" ", "") == cmbAccount.Text)
            {
                AFSRootPath = AFSreader.ReadElementString(cmbAccount.Text);
                if (AFSreader.ReadToFollowing(OrderTAGName) == true)
                {
                    OrderRootPath = AFSreader.ReadElementString(OrderTAGName);
                }
                break;
            }

        }
        AFSreader.Close();
    }

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        string strJID, strAID, strStage;
        strJID = cmbJID.Text.Trim().ToLower();
        strAID = txtAID.Text.Trim().ToLower();
        strStage = cmbStage.Text.Trim().ToLower();
        string strDir;

        try
        {
            if (Page.IsValid == true)
            {
                if (cmbStage.Text.Trim() != "")
                {
                    XmlTextReader AFSreader = new XmlTextReader(Server.MapPath("AFSRoot.xml"));
                    string OrderTAGName = "";
                    string OrderRootPath = "";
                    if (Session["Account"] != null)
                    {
                        OrderTAGName = Session["Account"].ToString() + "-ORDER";

                        while (AFSreader.Read() == true)
                        {
                            if (AFSreader.Name.ToString() == OrderTAGName)
                            {
                                OrderRootPath = AFSreader.ReadElementString(OrderTAGName);
                                break;
                            }

                        }
                        if (OrderRootPath == "")
                        {
                            Response.Write("Order Root path not found");
                        }
                    }
                    AFSreader.Close();

                    if (Session["Account"].ToString() == "JW-JOURNALS")
                    {
                        strDir = System.Configuration.ConfigurationManager.AppSettings["OrderPath"] + "\\" + cmbAccount.Text.Trim() + "\\" + cmbJID.Text.Trim() + "\\" + cmbStage.Text.Trim() + "\\" + txtAID.Text.Trim() + "\\CurrentOrder\\";
                    }
                    else
                    {
                        strDir = OrderRootPath  + "\\" + Session["Account"].ToString() + "\\" + cmbJID.Text.Trim() + "\\" + cmbStage.Text.Trim() + "\\" + txtAID.Text.Trim() + "\\CurrentOrder\\";
                    }
                    if (Directory.Exists(strDir) == true)
                    {

                        string[] xmlOrder = Directory.GetFiles(strDir);
                        if (xmlOrder.Length == 1)
                        {
                            Xml1.DocumentSource = xmlOrder[0];
                            if (Session["Account"] != null)
                            {
                                Xml1.TransformSource = Server.MapPath("validation") +"\\"+ Session["Account"].ToString() + "-Order.xsl";
                            }
                            else
                            {
                                string alertScript = "<script language=JavaScript>";
                                alertScript += "alert('--- Session Account not defined ---')";
                                alertScript += "</script" + "> ";
                                this.RegisterClientScriptBlock("alert", alertScript);
                            }
                        }
                        else
                        {
                            string alertScript = "<script language=JavaScript>";
                            alertScript += "alert('--- Order not in proper format ---')";
                            alertScript += "</script" + "> ";
                            this.RegisterClientScriptBlock("alert", alertScript);

                        }
                    }
                    else
                    {
                        txtOrderNotFound.Text = "Order Path Not Found";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('--- " + ex.Message + " ---')";
            alertScript += "</script" + "> ";
            txtOrderNotFound.Text = ex.Message;
            this.RegisterClientScriptBlock("alert", alertScript);

        }
    }
    protected void cmbJID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillJID();
    }

    protected void FillJID()
    {
        string JIDFile;
        if (Session["Account"] != null)
        {
            switch (Session["Account"].ToString())
            {
                case "JW-JOURNALS":
                    {
                        if (cmbAccount.Text != "")
                        {
                            if (Session["LoginID"] != null)
                            {
                                if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58848")
                                {
                                    cmbJID.Items.Add("PPUL");
                                    cmbJID.Items.Add("TEA");
                                    cmbJID.Items.Add("JPS");
                                    cmbJID.Items.Add("AJIM");
                                    cmbJID.Items.Add("DEV");
                                    cmbJID.Items.Add("NUR");
                                }
                                else if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58902")
                                {
                                    cmbJID.Items.Add("JCB");
                                    cmbJID.Items.Add("MC");
                                    cmbJID.Items.Add("PROS");
                                    cmbJID.Items.Add("AJMB");
                                }
                                else if (cmbAccount.Text.Trim() == "JWUSA" && Session["LoginID"].ToString() == "58889")
                                {
                                    cmbJID.Items.Add("BEM");
                                    cmbJID.Items.Add("JSO");
                                    cmbJID.Items.Add("PPUL");
                                    cmbJID.Items.Add("MRD");
                                }
                                else
                                {
                                    JIDFile = cmbAccount.Text + "Journals.ini";
                                    StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                                    cmbJID.Items.Clear();
                                    while (sr.Peek() > -1)
                                    {
                                        cmbJID.Items.Add(sr.ReadLine().Trim());
                                    }
                                    sr.Close();
                                }

                            }
                        }
                        break;
                    }
                default:
                    {
                        JIDFile = Session["Account"] + "Journals.ini";

                        if (File.Exists(JIDFile))
                        {
                            StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                            cmbJID.Items.Clear();
                            while (sr.Peek() > -1)
                            {
                                cmbJID.Items.Add(sr.ReadLine().Trim());
                            }
                            sr.Close();
                        }
                        break;
                    }
            }


        }

    }
    private void FillJID1()
    {
        string JIDFile = "";
        if (cmbAccount.Text != "")
        {
            if (cmbAccount.Text.Trim() == "JWUSA")
            {
                cmbJID.Items.Add("PPUL");
                cmbJID.Items.Add("TEA");
                cmbJID.Items.Add("JPS");
                cmbJID.Items.Add("AJIM");
                cmbJID.Items.Add("DEV");
                cmbJID.Items.Add("NUR");
                cmbJID.Items.Add("JCB");
                cmbJID.Items.Add("MC");
                cmbJID.Items.Add("PROS");
                cmbJID.Items.Add("AJMB");
                cmbJID.Items.Add("BEM");
                cmbJID.Items.Add("JSO");
                cmbJID.Items.Add("PPUL");
                cmbJID.Items.Add("MRD");
                JIDFile = cmbAccount.Text + "Journals.ini";
                StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                cmbJID.Items.Clear();
                while (sr.Peek() > -1)
                {
                    cmbJID.Items.Add(sr.ReadLine().Trim());
                }
                sr.Close();
            }
            else
            {
                JIDFile = cmbAccount.Text + "Journals.ini";
                StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                cmbJID.Items.Clear();
                while (sr.Peek() > -1)
                {
                    cmbJID.Items.Add(sr.ReadLine().Trim());
                }
                sr.Close();
            }
        }
    }
    public void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        GetOrderRootPath();
        string strDir = "", strLog="";
        if (Session["Account"].ToString() == "JW-JOURNALS")
        {
            strDir = System.Configuration.ConfigurationManager.AppSettings["FailPath"];
            strLog=cmbAccount.Text.Trim() + "_" + cmbJID.Text.Trim() + "_" + cmbStage.Text.Trim() + "_" + txtAID.Text.Trim();
        }
        else
        {
            strDir = System.Configuration.ConfigurationManager.AppSettings["FailPath"];
            strLog= Session["Account"].ToString() + "_" + cmbJID.Text.Trim() + "_" + cmbStage.Text.Trim() + "_" + txtAID.Text.Trim();
        }
        string[] logFiles=Directory.GetFiles(strDir,"*.log");

        foreach(string logFile in logFiles)
        {
            if(logFile.IndexOf(strLog.ToUpper())>=0)
            {
                Label lbl1=new Label();
                lbl1.Text = "Failed";
//                this.Controls.Add(lbl1);
                lbl1.Style.Add(HtmlTextWriterStyle.Top, "200px");
                lbl1.Style.Add(HtmlTextWriterStyle.Left, "800");
                lbl1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
                this.Controls[0].Controls[5].Controls.Add(lbl1);
                //                lnkLog.PostBackUrl = logFile;
                
            }
            else
            {
//                lnkLog.Text = "Successfully Integrated in FMS.";
            }
        }


    }
    protected void lnkLog_Click(object sender, EventArgs e)
    {
        
    }
}

    

