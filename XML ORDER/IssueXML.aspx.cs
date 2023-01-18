using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
public partial class IssueXML : System.Web.UI.Page
{
    #region Declare Variable of Issue XML
    public StreamWriter LogWriter;
    public static string JournalNo;
    public static string issNo;
    public ArrayList JidNo = new ArrayList();
    public ArrayList IssnNo = new ArrayList();
    public ArrayList Stage = new ArrayList();
    public static string PhotoCover = "no";
    public static string isWoc = "no";
    DataTable DT = new DataTable();
    ArrayList pages = new ArrayList();
    string tt;
    DateTime DueDate;
    string[] ddPart = { "" };
    public ArrayList ArticleNo = new ArrayList();
    public ArrayList Article_id = new ArrayList();
    ArrayList pdf_page = new ArrayList();
    ArrayList s_page = new ArrayList();
    ArrayList e_page = new ArrayList();
    ArrayList strArray = new ArrayList();
    ArrayList strArray_EndPages = new ArrayList();
    public ArrayList Pg_from = new ArrayList();
    public ArrayList Pg_to = new ArrayList();
    string DTYear = DateTime.Now.Year.ToString();
    StringBuilder sb = new StringBuilder();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            isWoc = "no";
            Session["gridData"] = "";
            if (Session["UserName"] != null)
            {
                lblUser.Text = Session["UserName"].ToString();
                txtAID.Text = txtAID.Text.ToUpper();
                FillAccount();
            }
            else
            {
                lblUser.Text = "Session not initialized for UserName";
                Response.Redirect("~/Login.aspx");
            }
          
        }
        else
        {
            Grid.Controls.Add(new LiteralControl( Session["gridData"].ToString()));
        }
    }
  
    //protected void ReadDetailsFromMYSQL(ArrayList artno)
    //{
    //    try
    //    {
    //        int Counter = 0;
    //        MySqlConnection MySqlConn = null;
    //        MySqlCommand MySqlCmd;
    //        MySqlDataAdapter MySqlDA;
    //        DataSet DS = new DataSet();
    //        string sqlstr = "";
    //        bool CheckStatus = true;
    //        try
    //        {
    //            sb = new StringBuilder();
    //            MySqlConn = new MySqlConnection("");
    //            MySqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["FMS2ConnectionString"].ToString();
    //            MySqlConn.Open();
         
    //            for (int i = 0; i < artno.Count; i++)
    //            {
    //                sqlstr = @"SELECT  DEPARTMENT,PAGES FROM dept_info2 WHERE dept_info2.JID_AID='" + artno[i] + "' AND dept_info2.custOMER='" + ddlAccount.Text.ToUpper() + @"'";
    //                MySqlCmd = new MySqlCommand(sqlstr, MySqlConn);
    //                MySqlDA = new MySqlDataAdapter(MySqlCmd);
    //                MySqlDA.Fill(DS);
    //                if (DS.Tables[0].Rows.Count > Counter)
    //                {
    //                    sb.Append("<span style='font-size:8pt;'>In FMS </span><br/>");
    //                    Counter++;
    //                }
    //                else
    //                {
    //                    sb.Append("<span style='font-size:8pt; color:red;'>Not in FMS </span><br/>");
    //                    CheckStatus = false;
    //                }
    //            }
    //            Session["gridData"] = sb.ToString();
    //            Grid.InnerHtml = "";
    //            Grid.Controls.Add(new LiteralControl(sb.ToString()));
    //            DT = DS.Tables[0];
    //            if (artno.Count != DT.Rows.Count)
    //            {
    //                lblError.Text = "Articles does not exists in FMS.Please check the FMS status of list of articles. !!!";
    //            }
    //            else
    //            {
    //                lblError.Text = "";
    //            }
    //            if (DT.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < DT.Rows.Count; i++)
    //                {
    //                    Convert.ToInt32(pages.Add((DT.Rows[i]["PAGES"])));
    //                }
    //            }
    //            else
    //            {
    //                string alertScript = "<script language=JavaScript>";
    //                alertScript += "alert('" + "Article not found in database. Please check the Token status in FMS" + "');";
    //                alertScript += "</script" + "> ";
    //                this.RegisterClientScriptBlock("alert", alertScript);
    //            }
    //            #region Check Start Page and End Pages
    //            StartPage_EndPage();
               
    //            DT.Columns.Add("StartPage", Type.GetType("System.String"));
    //            DT.Columns.Add("EndPage", Type.GetType("System.String"));
    //            for (int i = 0; i < strArray.Count - 1; i++)
    //            {
    //                DT.Rows[i]["StartPage"] = strArray[i];

    //            }
    //            for (int i = 0; i < strArray_EndPages.Count; i++)
    //            {
    //                DT.Rows[i]["EndPage"] = strArray_EndPages[i];

    //            }
    //            #endregion
    //            if (CheckStatus == true)
    //            {
    //                grvFMSDetails.DataSource = DT;
    //                grvFMSDetails.DataBind();
    //                ViewState["Table"] = DT;
    //                fileUpload.Visible = true;
    //                Button2.Enabled = true;
    //                lblError.Text = string.Empty;
    //            }
    //            else if(CheckStatus == false)
    //            {
    //                grvFMSDetails.DataSource = null;
    //                grvFMSDetails.DataBind();
    //                ViewState["Table"] = DT;
    //                fileUpload.Visible = false;
    //                Button2.Enabled = false;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string alertScript = "<script language=JavaScript>";
    //            alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')";
    //            alertScript += "</script" + "> ";
    //            this.RegisterClientScriptBlock("alert", alertScript);
    //        }
    //        finally
    //        {
    //            if (!(MySqlConn == null))
    //            {
    //                MySqlConn.Close();
    //                MySqlConn = null;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblError.Text = ex.ToString();
    //        string alertScript = "<script language=JavaScript>";
    //        alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')";
    //        alertScript += "</script" + "> ";
    //        this.RegisterClientScriptBlock("alert", alertScript);
    //    }
    //}

    protected void FillAccount()
    {
        ddlAccount.Items.Clear();
        ddlAccount.Items.Insert(0, "--Select Account--");
        ddlAccount.Items.Add("EDP");
        ddlAccount.Items.Add("JWUSA");
        ddlAccount.Items.Add("JWUK");
        ddlAccount.Items.Add("JWVCH");
    }

    protected void WorkFlow()
    {
        ddlWorkFlow.Items.Clear();
        ddlWorkFlow.Items.Insert(0, "--Select Work Flow--");
        ddlWorkFlow.Items.Add(@"3B2");
        ddlWorkFlow.Items.Add(@"Latex");
        ddlWorkFlow.Items.Add(@"ISSUE_WITH_ME_AND_GRAPHIC");
        ddlWorkFlow.Items.Add(@"ISSUE_WITH_ME_WITHOUT_GRAPHIC");
        ddlWorkFlow.Items.Add(@"ISSUE_WITHOUT_ME_WITH_GRAPHIC");
        ddlWorkFlow.Items.Add(@"ISSUE_WITHOUT_ME_WITHOUT_GRAPHIC");
    }
  
    protected void Due_Date()
    {
        DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        string format = "yyyy MM dd";
        int i = 0;

        if (ddlWorkFlow.Text == @"3B2" || ddlWorkFlow.Text == @"Latex" || ddlWorkFlow.Text == @"ISSUE_WITH_ME_AND_GRAPHIC" || ddlWorkFlow.Text == @"ISSUE_WITH_ME_WITHOUT_GRAPHIC" || ddlWorkFlow.Text == @"ISSUE_WITHOUT_ME_WITH_GRAPHIC" || ddlWorkFlow.Text == @"ISSUE_WITHOUT_ME_WITHOUT_GRAPHIC")
        {
            int firstReminder = 3;
            while (i < firstReminder)
            {
                DueDate = DueDate.AddDays(1);
                i++;
            }

        }
        if (ddlWorkFlow.Text == @"JWUSA\uk")
        {
            int SeMySqlConndReminder = 5;
            while (i <= SeMySqlConndReminder)
            {
                tt = DueDate.AddDays(1).ToString();
                i++;
            }
        }
        if (ddlWorkFlow.Text == @"JWUSA\vch")
        {
            int ThirdReminder = 5;
            while (i <= ThirdReminder)
            {
                tt = DueDate.AddDays(1).ToString();
                i++;
            }
        }
        if (ddlWorkFlow.Text == @"JWUSA\uk")
        {
            int SeMySqlConndReminder = 5;
            while (i <= SeMySqlConndReminder)
            {
                tt = DueDate.AddDays(1).ToString();
                i++;
            }
        }
        string dd = DueDate.ToString(format);
        ddPart = dd.Split(' ');
    }

    protected void JIDNO()
    {
        string JIDNumber;
        JIDNumber = ddlAccount.Text + "JournalNo.ini";
        StreamReader sr = new StreamReader(Server.MapPath(JIDNumber));
        if (ddlAccount.Text == "JWUSA")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            if (JidNo.Count != 0)
            {
                for (int i = 0; i < JidNo.Count; i++)
                {
                    string rd = JidNo[i].ToString();
                    if (rd.IndexOf(ddlJID.Text) != -1)
                    {
                        rd = rd.Replace(ddlJID.Text + "#", "");
                        JournalNo = rd.Trim();

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                //lblError.Text = "Please enter the JournalNo in Journal No File";
            }
        }
        else if (ddlAccount.Text == "JWUK")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            if (JidNo.Count != 0)
            {
                for (int i = 0; i < JidNo.Count; i++)
                {
                    string rd = JidNo[i].ToString();
                    if (rd.IndexOf(ddlJID.Text) != -1)
                    {
                        rd = rd.Replace(ddlJID.Text + "#", "");
                        JournalNo = rd.Trim();
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                // lblError.Text = "Please enter the JournalNo in Journal No File";
            }
        }
        else if (ddlAccount.Text == "JWVCH")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            if (JidNo.Count != 0)
            {
                for (int i = 0; i < JidNo.Count; i++)
                {
                    string rd = JidNo[i].ToString();
                    if (rd.IndexOf(ddlJID.Text) != -1)
                    {
                        rd = rd.Replace(ddlJID.Text + "#", "");
                        JournalNo = rd.Trim();
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                //lblError.Text = "Please enter the JournalNo in Journal No File";
            }
        }

        if (ddlAccount.Text == "EDP")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            if (JidNo.Count != 0)
            {
                for (int i = 0; i < JidNo.Count; i++)
                {
                    string rd = JidNo[i].ToString();
                    if (rd.IndexOf(ddlJID.Text) != -1)
                    {
                        rd = rd.Replace(ddlJID.Text + "#", "");
                        JournalNo = rd.Trim();

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                //lblError.Text = "Please enter the JournalNo in Journal No File";
            }
        }

    }

    protected void ISSN_NO()
    {
        string JIDNumber;
        JIDNumber = ddlAccount.Text + "IssnNO.ini";
        StreamReader sr = new StreamReader(Server.MapPath(JIDNumber));
        if (ddlAccount.Text == "JWUSA")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            for (int i = 0; i < JidNo.Count; i++)
            {
                string rd = JidNo[i].ToString();
                if (rd.IndexOf(ddlJID.Text) != -1)
                {
                    rd = rd.Replace(ddlJID.Text + "#", "");
                    issNo = rd.Trim();

                }
                else
                {

                }

            }
        }
        else if (ddlAccount.Text == "JWUK")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            for (int i = 0; i < JidNo.Count; i++)
            {
                string rd = JidNo[i].ToString();
                if (rd.IndexOf(ddlJID.Text) != -1)
                {
                    rd = rd.Replace(ddlJID.Text + "#", "");
                    issNo = rd.Trim();
                }
                else
                {

                }

            }
        }
        else if (ddlAccount.Text == "JWVCH")
        {
            while (sr.Peek() > -1)
            {
                JidNo.Add(sr.ReadLine().Trim());
            }
            for (int i = 0; i < JidNo.Count; i++)
            {
                string rd = JidNo[i].ToString();
                if (rd.IndexOf(ddlJID.Text) != -1)
                {
                    rd = rd.Replace(ddlJID.Text + "#", "");
                    issNo = rd.Trim();
                }
                else
                {

                }

            }
        }
    }

    protected void FromPage_ToPage()
    {
        DataTable DT = new DataTable();
        DT = (DataTable)ViewState["Table"];
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            Convert.ToInt32(Pg_from.Add((DT.Rows[i]["StartPage"])));
            Convert.ToInt32(Pg_to.Add((DT.Rows[i]["EndPage"])));

        }
        //////string frm = txtsPages.Text;
        //////string to = ""; /////txtepages.Text;
        //////string Replace_With = ",";
        //////string strFrm = frm.Replace("\r\n", Replace_With);
        //////string strTo = to.Replace("\r\n", Replace_With);
        //////strFrm = strFrm.Trim();
        //////strTo = strTo.Trim();
        //////string[] StrFrmPart = strFrm.Split(',');
        //////string[] StrToPart = strTo.Split(',');
        //////for (int i = 0; i < StrFrmPart.Length; i++)
        //////{          
        //////    Pg_from.Add(StrFrmPart[i]);
        //////}
        //////for (int i = 0; i < StrToPart.Length; i++)
        //////{         
        //////    Pg_to.Add(StrToPart[i]);
        //////}

    }
  
    protected void _AID()
    {
        string TextBoxAID = txtAID.Text;
        string replacewith = ",";
        string Str = TextBoxAID.Replace("\r\n", replacewith).Replace(ddlJID.Text, "");
        Str = Str.Trim();
        if (Str.IndexOf(',') != -1)
        {
            string[] StrPart = Str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < StrPart.Length; i++)
            {
                ArticleNo.Add(ddlJID.Text + "_" + StrPart[i]);
                Article_id.Add(StrPart[i]);
            }
        }
        else
        {
            if(Str!="")
            {
                ArticleNo.Add(ddlJID.Text + "_" + Str);
                Article_id.Add(Str);
            }
        }
        for (int i = 0; i < pages.Count; i++)
        {
            pdf_page.Add(pages[i]);
        }
    }

    protected void _Stage()
    {
        try
        {

            DataTable DT = new DataTable();
            DT = (DataTable)ViewState["Table"];
            //////string textboxstage = "";/////txtStage.Text;
            //////string replacewith = ",";
            //////string Str = textboxstage.Replace("\r\n", replacewith);
            //////Str = Str.Trim();
            //////if (Str.IndexOf(',') != -1)
            //////{
            //////    string[] StrPart = textboxstage.Split(',');
            //////    for (int i = 0; i < StrPart.Length; i++)
            //////    {
            //////        Stage.Add(StrPart[i]);
            //////    }
            //////}
            //////else
            //////{

            //////}
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Convert.ToInt32(Stage.Add((DT.Rows[i]["FMSSTAGE"])));

            }
        }
        catch
        { 
        }

    }

    protected void FillJID()
    {
        if (ddlAccount.Text == "--Select Account--")
        {
            ddlJID.Items.Clear();
            ddlJID.Items.Insert(0, new ListItem("--Select JID--", "0"));
            return;
        }
        string JIDFile;
        if (Session["Account"] != null)
        {
            switch (Session["Account"].ToString())
            {
                case "JW-JOURNALS":
                    {
                        if (ddlAccount.Text != "")
                        {
                            //if (Session["LoginID"] != null)
                            //{
                            JIDFile = ddlAccount.Text + "Journals.ini";
                            if (File.Exists(Server.MapPath(JIDFile)))
                            {
                                StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                                ddlJID.Items.Clear();
                                ddlJID.Items.Insert(0, "--Select JID--");
                                while (sr.Peek() > -1)
                                {

                                    ddlJID.Items.Add(sr.ReadLine().Trim());
                                }
                                sr.Close();
                            }
                            //}
                        }
                        break;
                    }
                case "EDP":
                    {
                        if (ddlAccount.Text != "")
                        {
                            //if (Session["LoginID"] != null)
                            //{
                            JIDFile = ddlAccount.Text + "Journals.ini";
                            if (File.Exists(Server.MapPath(JIDFile)))
                            {
                                StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                                ddlJID.Items.Clear();
                                ddlJID.Items.Insert(0, "--Select JID--");
                                while (sr.Peek() > -1)
                                {

                                    ddlJID.Items.Add(sr.ReadLine().Trim());
                                }
                                sr.Close();
                            }
                            //}
                        }
                        break;
                    }
                default:
                    {
                        JIDFile = Session["Account"] + "Journals.ini";
                        StreamReader sr = new StreamReader(Server.MapPath(JIDFile));
                        ddlJID.Items.Clear();
                        while (sr.Peek() > -1)
                        {
                            ddlJID.Items.Add(sr.ReadLine().Trim());
                        }
                        sr.Close();
                        break;
                    }
            }


        }

    }
  
    protected void StartPage_EndPage()
    {
        try
        {
            ViewState["pdf"] = pages;
            int sPage = 0, ePage = 0;
            sPage = Convert.ToInt32(txtsPages.Text);
            strArray.Add(Convert.ToString(txtsPages.Text));
            for (int i = 0; i < pages.Count; i++)
            {
                e_page.Add(sPage + Convert.ToInt32(pages[i]));
                ePage = sPage + Convert.ToInt32(pages[i]);
                s_page.Add(ePage + 1);
                sPage = Convert.ToInt32(e_page[i]) + 1;
                strArray.Add(Convert.ToString(sPage));
                strArray_EndPages.Add(Convert.ToString(ePage));
            }
        }
        catch
        { 
        
        }
    }

    protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillJID();
    }

    protected void ddlJID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEditorDetails(ddlJID.Text, ddlAccount.Text);
        WorkFlow();
    }

    protected void btnFMS_Click(object sender, EventArgs e)
    {
        if (ddlAccount.SelectedValue == "--Select Account--")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please select the Account.');</script>");
            ddlAccount.Focus();
        }
        else if (ddlJID.SelectedValue == "--Select JID--")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please select the JID.');</script>");
            ddlJID.Focus();
        }
        else if (ddlWorkFlow.SelectedValue == "--Select Work Flow--")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please select the Work Flow.');</script>");
            ddlWorkFlow.Focus();
        }
        else if (txtRemarks.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Remarks.');</script>");
            txtRemarks.Focus();
        }
        else if (txtAID.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the AID.');</script>");
            txtAID.Focus();
        }
        else if (txtVolume.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Volume No.');</script>");
            txtVolume.Focus();
        }
        else if (txtIssueNo.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Issue No.');</script>");
            txtIssueNo.Focus();
        }
        else if (txtsPages.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Pages Start.');</script>");
            txtsPages.Focus();
        }

        else
        {
            _AID();
            //waseem 21 Nov
            //ReadDetailsFromMYSQL(ArticleNo);
           
            // comment for IssueXML order on 26 Nov 2018 by waseem.
            //ReadDetailsFromSqlServer(ArticleNo);

            //Button2.Enabled = true;
            lblDisplay.Text = "";
            //fileUpload.Visible = true;
        }
    }

    private void ReadDetailsFromSqlServer(ArrayList artno)   
    {
        try
        {
            int Counter = 0;
            SqlConnection MySqlConn = null;
            SqlCommand MySqlCmd;
            SqlDataAdapter MySqlDA;
            DataSet DS = new DataSet();
            string sqlstr = "";
            bool CheckStatus = true;
            try
            {
                sb = new StringBuilder();
                MySqlConn = new SqlConnection("");
                MySqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["MDKConnectionString"].ToString();
                MySqlConn.Open();

                for (int i = 0; i < artno.Count; i++)
                {
                 // sqlstr = @"SELECT  DEPARTMENT,PAGES FROM dept_info2 WHERE dept_info2.JID_AID='" + artno[i] + "' AND dept_info2.custOMER='" + ddlAccount.Text.ToUpper() + @"'";
                    sqlstr = @"SELECT JID, AID,MSS,FMSSTAGE from EDP.dbo.EDPWIP where JIDAID='" + artno[i].ToString().Replace("_","") + "' AND FMSSTAGE in('S250','S275')";
                    MySqlCmd = new SqlCommand(sqlstr, MySqlConn);
                    MySqlDA = new SqlDataAdapter(MySqlCmd);
                    MySqlDA.Fill(DS);
                    if (DS.Tables[0].Rows.Count > Counter)
                    {
                        sb.Append("<span style='font-size:8pt;'>In FMS </span><br/>");
                        Counter++;
                    }
                    else
                    {
                        sb.Append("<span style='font-size:8pt; color:red;'>Not in FMS </span><br/>");
                        CheckStatus = false;
                    }
                }
                Session["gridData"] = sb.ToString();
                Grid.InnerHtml = "";
                Grid.Controls.Add(new LiteralControl(sb.ToString()));
                DT = DS.Tables[0];
                if (artno.Count != DT.Rows.Count)
                {
                    lblError.Text = "Articles does not exists in FMS.Please check the FMS status of list of articles. !!!";
                }
                else
                {
                    lblError.Text = "";
                }
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        Convert.ToInt32(pages.Add((DT.Rows[i]["MSS"])));
                    }
                }
                else
                {
                    string alertScript = "<script language=JavaScript>";
                    alertScript += "alert('" + "Article not found in database. Please check the Token status in FMS" + "');";
                    alertScript += "</script" + "> ";
                    this.RegisterClientScriptBlock("alert", alertScript);
                }
                #region Check Start Page and End Pages
                StartPage_EndPage();

                DT.Columns.Add("StartPage", Type.GetType("System.String"));
                DT.Columns.Add("EndPage", Type.GetType("System.String"));
                for (int i = 0; i < strArray.Count - 1; i++)
                {
                    DT.Rows[i]["StartPage"] = strArray[i];

                }
                for (int i = 0; i < strArray_EndPages.Count; i++)
                {
                    DT.Rows[i]["EndPage"] = strArray_EndPages[i];

                }
                #endregion
                if (CheckStatus == true)
                {
                    grvFMSDetails.DataSource = DT;
                    grvFMSDetails.DataBind();
                    ViewState["Table"] = DT;
                    //fileUpload.Visible = true;
                    Button2.Enabled = true;
                    lblError.Text = string.Empty;
                }
                else if (CheckStatus == false)
                {
                    grvFMSDetails.DataSource = null;
                    grvFMSDetails.DataBind();
                    ViewState["Table"] = DT;
                   // fileUpload.Visible = false;
                    Button2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string alertScript = "<script language=JavaScript>";
                alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')";
                alertScript += "</script" + "> ";
                this.RegisterClientScriptBlock("alert", alertScript);
            }
            finally
            {
                if (!(MySqlConn == null))
                {
                    MySqlConn.Close();
                    MySqlConn = null;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
        }
    }

    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        JIDNO();
        ISSN_NO();

    }

    protected void cmdGenerate_Click(object sender, EventArgs e)
    {
        string IssuZipfilename = "";
        if (ddlAccount.SelectedValue == "--Select Account--")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please select the Account.');</script>");
            ddlAccount.Focus();
        }
        else if (ddlJID.SelectedValue == "--Select JID--")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please select the JID.');</script>");
            ddlJID.Focus();
        }

        else if (txtAID.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the AID.');</script>");
            txtAID.Focus();
        }
        else if (txtVolume.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Volume No.');</script>");
            txtVolume.Focus();
        }
        else if (txtIssueNo.Text == "")
        {
            this.RegisterClientScriptBlock("alert", "<script>alert('! Please enter the Issue No.');</script>");
            txtIssueNo.Focus();
        }

        if (IssueflUpload.HasFile)
        {
             IssuZipfilename = Path.GetFileName(IssueflUpload.FileName);
            string ext = Path.GetExtension(IssueflUpload.FileName);
            if (ext == ".zip")
            {

                string FMSOrderPath = ConfigurationManager.AppSettings["FMSORDERPATH"];
                try
                {
                    if (!Directory.Exists(FMSOrderPath))
                    {
                        Directory.CreateDirectory(FMSOrderPath);
                    }
                    // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                    if (File.Exists(FMSOrderPath + "\\" + IssuZipfilename))
                    {
                        File.Delete(FMSOrderPath + "\\" + IssuZipfilename);
                    }

                  //  IssueflUpload.SaveAs(FMSOrderPath + "\\" + IssuZipfilename);

                }
                catch (Exception ex)
                {
                    lblDisplay.Text = ".zip file could not be uploaded on the required location, please check with technical team.";
                    return;
                }
            }
            else
            {
                lblDisplay.Text = "Please select a .zip file for the uploading";
                return;
            }
        }

            GenerateXmlOrder(ddlAccount.Text, ddlJID.Text, txtRemarks.Text, ArticleNo, Stage, IssuZipfilename);
            Button2.Enabled = true;
            ddlAccount.SelectedIndex = 0;
            ddlJID.Items.Clear();
            ddlWorkFlow.Items.Clear();
            txtAID.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtsPages.Text = string.Empty;
            txtVolume.Text = string.Empty;
            txtIssueNo.Text = string.Empty;
            Session["gridData"] = "&nbsp;";
            //Grid.Controls.Add(new LiteralControl(Session["gridData"].ToString()));
            Grid.InnerHtml = "";

        
    }

    protected void chkWOC_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWOC.Checked == true)
        {
            isWoc = "yes";
        }
        else
        {
            isWoc = "no";
        }
    }

    protected bool WriteLog(string txtLog)
    {
        if (ddlJID.Text != "")
        {

            if (Session["Account"].ToString().Equals("JW-JOURNALS", StringComparison.OrdinalIgnoreCase))
            {
                LogWriter = new StreamWriter(ViewState["OrderPath"] + "\\" + ddlJID.Text + ".log", true);
            }
            else
            {
                LogWriter = new StreamWriter("c:\\xmlorder\\" + ddlJID.Text + ".log", true);
            }
            LogWriter.WriteLine("-->    " + txtLog + " |||");
            LogWriter.Flush();
            LogWriter.Close();
            return true;
        }
        //else
        //{
        //    this.RegisterClientScriptBlock("alert", "<script>alert('Please Fill JID, Stage, DOI Properly.');</script>");
        //    return false;
        //}
        return false;
    }

    protected void GetEditorDetails(string sJID, string sCust)
    {

        System.Configuration.Configuration rootWebConfig =
        System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/XMLORDER");
        string StrSQL = "";
        System.Configuration.ConnectionStringSettings MySqlConnnString = null;
        if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
        {
            if (Session["Account"] != null)
            {
                switch (Session["Account"].ToString())
                {
                    case "JW-JOURNALS":
                        {
                            MySqlConnnString = rootWebConfig.ConnectionStrings.ConnectionStrings["OPSConnectionString"];
                            StrSQL = "Select Jname, Peditor, Designation, Pe_Email, Phone, Fax, Address from  OPSDetails where Client='" + sCust + "' and  Jid='" + sJID + "'";
                            break;
                        }
                    case "THIEME":
                        {
                            MySqlConnnString = rootWebConfig.ConnectionStrings.ConnectionStrings["AEPS-THIEME"];
                            StrSQL = "Select Journal_Name, [Production Editor], Designation, Pe_Email, Phone, Fax, Address from Thieme_Fresh where Journal_ID='" + sJID + "'";
                            break;
                        }
                    case "IOS":
                        {
                            break;
                        }
                    case "LWW":
                        {
                            break;
                        }
                }
            }
        }
        if (MySqlConnnString != null)
        {
            SqlConnection AEPSCon = new SqlConnection(MySqlConnnString.ConnectionString);

            AEPSCon.Open();
            SqlCommand AEPSCom = new SqlCommand(StrSQL, AEPSCon);
            try
            {
                SqlDataReader AEPSDr = AEPSCom.ExecuteReader();

                if (AEPSDr.HasRows == true)
                {
                    while (AEPSDr.Read() == true)
                    {
                        //txtEDName.Text = AEPSDr[1].ToString();
                        //txtEDDesign.Text = AEPSDr["Designation"].ToString();
                        //txtEDMail.Text = AEPSDr["Pe_Email"].ToString();
                        //txtEDTel.Text = AEPSDr["Phone"].ToString();
                        //txtEDFax.Text = AEPSDr["Fax"].ToString();
                        //txtEDAddress.Text = AEPSDr["Address"].ToString();
                        LblJIDTitle.Text = AEPSDr[0].ToString();
                    }
                }
                AEPSDr.Close();
            }
            catch (Exception ex)
            {
                string alertScript = "<script language=JavaScript>";
                alertScript += "alert(\"---error in GetDetails" + ex.Message.Replace("'", ":") + "---\")";
                alertScript += "</script" + "> ";
                this.RegisterClientScriptBlock("alert", alertScript);
            }
            finally
            {
                if (AEPSCon != null)
                {
                    AEPSCon.Close();
                }
            }
        }

    }

    protected bool GenerateXmlOrder(string cust, string jid, string Remarks, ArrayList aid, ArrayList stage, string IssuZipfilename)
    {
        try
        {
            _AID();
           // FromPage_ToPage();
            _Stage();

            string OrderPath = "";
            string OrderDirectory = "";

            string FMSOrderPath = ConfigurationManager.AppSettings["FMSORDERPATH"];

            if (Session["Account"].ToString().Equals("JW-JOURNALS", StringComparison.OrdinalIgnoreCase))
            {
                OrderPath = "C:\\XmlOrder\\" + Session["Account"].ToString() + "\\" + ddlJID.Text.Trim();
                OrderDirectory = "C:\\XmlOrder";
            }
            if (Session["Account"].ToString().Equals("EDP", StringComparison.OrdinalIgnoreCase))
            {
                OrderPath = "C:\\XmlOrder\\" + Session["Account"].ToString() + "\\" + ddlJID.Text.Trim();
                OrderDirectory = "C:\\XmlOrder";
            }

            string OrderName = "";
            string TempOrder = "";
            string fmspath = "";
            string fmszippath = "";
            if (Directory.Exists(OrderDirectory + "\\" + cust) == false)
            {
                Directory.CreateDirectory(OrderDirectory + "\\" + cust);
            }

            OrderDirectory = OrderDirectory + "\\" + cust;
      
            if (Session["Account"].ToString().Equals("JW-JOURNALS", StringComparison.OrdinalIgnoreCase))
            {
                OrderName = OrderDirectory + "\\CurrentOrder\\" + cust + "_" + jid + "_Order_0.xml";
                TempOrder = Server.MapPath(@"Validation\" + cust + "_" + jid + "_" + "issue_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml");
                fmspath = FMSOrderPath + cust + "_" + jid + "_" + "issue_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml";
            }
            else
            {
                OrderName = OrderDirectory + "\\CurrentOrder\\" + cust + "_" + jid + "_Order_0.xml";

                if (isWoc == "yes")
                {
                    TempOrder = Server.MapPath(@"Validation\" + cust + "_" + jid + "_" + "WOC_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml");
                    fmspath = FMSOrderPath + cust + "_" + jid + "_" + "WOC_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml";
                    fmszippath = FMSOrderPath + cust + "_" + jid + "_" + "WOC_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.zip";
                }
                else
                {
                    TempOrder = Server.MapPath(@"Validation\" + cust + "_" + jid + "_" + "issue_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml");
                    fmspath = FMSOrderPath + cust + "_" + jid + "_" + "issue_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.xml";
                    fmszippath = FMSOrderPath + cust + "_" + jid + "_" + "issue_" + txtVolume.Text + '(' + txtIssueNo.Text + ')' + "_Order_0.zip";
                }
            }
            if (ViewState["FMS_PATH"] == null)
            {
                if (FMSOrderPath != "")
                {
                    ViewState.Add("FMS_PATH", FMSOrderPath);
                }
                else
                {
                    this.RegisterClientScriptBlock("alert", "<script>alert('Could not get FMS path from AFSRoot.xml.');</script>");
                    return false;
                }
            }

            XmlTextWriter OrderWriter = new XmlTextWriter(TempOrder, System.Text.Encoding.ASCII);
            OrderWriter.Formatting = Formatting.Indented;
            OrderWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
            OrderWriter.WriteRaw("<!DOCTYPE orders SYSTEM \"FMS-J-Order.dtd\">\n<?xml-stylesheet type=\"text/xsl\" href=\"WileyJ-Order.xsl\"?>\n");

            OrderWriter.WriteStartElement("orders");

            OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteStartElement("order");

            if (Session["Account"] != null)
            {
                if (Session["Account"].ToString() == "JW-JOURNALS")
                {
                    OrderWriter.WriteAttributeString("customer", ddlAccount.Text.Trim());

                }
                else
                {
                    OrderWriter.WriteAttributeString("customer", Session["Account"].ToString());

                }
            }
            OrderWriter.WriteAttributeString("category", "JOURNAL");

            OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteStartElement("time");

            OrderWriter.WriteAttributeString("day", DateTime.Now.ToString("dd"));

            OrderWriter.WriteAttributeString("month", DateTime.Now.ToString("MM"));

            OrderWriter.WriteAttributeString("yr", DateTime.Now.Year.ToString());

            OrderWriter.WriteAttributeString("hr", DateTime.Now.ToString("hh"));

            OrderWriter.WriteAttributeString("min", DateTime.Now.ToString("mm"));

            OrderWriter.WriteAttributeString("sec", DateTime.Now.ToString("ss"));

            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");//time
            OrderWriter.WriteRaw("\n\t\t");
            if (ddlWorkFlow.Text != "")
            {
                Due_Date();

                OrderWriter.WriteStartElement("due-date");
                //WriteLog("WriteStartElement(\"due-date\")");
                OrderWriter.WriteRaw("\n\t\t\t");
                OrderWriter.WriteStartElement("date");
                //WriteLog("WriteStartElement(\"date\")");
                OrderWriter.WriteAttributeString("day", ddPart[2]);
                //WriteLog("WriteAttributeString(\"day\"" + txtDueDate.Text.Substring(0, (txtDueDate.Text.IndexOf("/"))));
                OrderWriter.WriteAttributeString("month", ddPart[1]);
                //WriteLog("WriteAttributeString(\"month\"" + txtDueDate.Text.Substring(txtDueDate.Text.IndexOf("/") + 1, 2));
                OrderWriter.WriteAttributeString("yr", ddPart[0]);
                //WriteLog("WriteAttributeString(\"yr\"" + txtDueDate.Text.Substring(txtDueDate.Text.LastIndexOf("/") + 1));
                OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n\t\t");//date
                OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n\t\t");//due-date
            }
            if (Session["Account"] != null)
            {
                switch (Session["Account"].ToString())
                {
                    case "JW-JOURNALS":
                        {
                            OrderWriter.WriteElementString("prod-site", "Wiley-Blackwell Journals"); OrderWriter.WriteRaw("\n\t\t");

                            break;
                        }
                    case "THIEME":
                        {
                            OrderWriter.WriteElementString("prod-site", "US"); OrderWriter.WriteRaw("\n\t\t");

                            break;
                        }
                    case "IOS":
                        {
                            OrderWriter.WriteElementString("prod-site", "IOS Press - UK"); OrderWriter.WriteRaw("\n\t\t");

                            break;
                        }
                    case "EDP":
                        {
                            OrderWriter.WriteElementString("prod-site", "EDPJournal"); OrderWriter.WriteRaw("\n\t\t");

                            break;
                        }
                    default:
                        {
                            OrderWriter.WriteElementString("prod-site", Session["Account"].ToString()); OrderWriter.WriteRaw("\n\t\t");

                            break;
                        }
                }

            }
            OrderWriter.WriteStartElement("stage");

            OrderWriter.WriteAttributeString("step", "S300");

            OrderWriter.WriteEndElement();//stage

            OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteStartElement("executor");

            OrderWriter.WriteAttributeString("type", "TYPESETTER");

            OrderWriter.WriteAttributeString("addressee", "yes"); OrderWriter.WriteRaw("\n\t\t\t");

            OrderWriter.WriteElementString("exec-code", "THOM"); OrderWriter.WriteRaw("\n\t\t\t");

            OrderWriter.WriteElementString("exec-name", "Thomson Digital"); OrderWriter.WriteRaw("\n\t\t\t");
            OrderWriter.WriteStartElement("aff");

            OrderWriter.WriteRaw("\n\t\t\t\t");
            OrderWriter.WriteElementString("address", "B/10-12"); OrderWriter.WriteRaw("\n\t\t\t\t");

            OrderWriter.WriteElementString("address-contd", "Noida Special Economic Zone"); OrderWriter.WriteRaw("\n\t\t\t\t");

            OrderWriter.WriteStartElement("zipcode");

            OrderWriter.WriteAttributeString("zipcode-pos", "AFTERCTY");

            OrderWriter.WriteString("201 305");

            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n\t\t\t\t");//zipcode
            OrderWriter.WriteElementString("cty", "Noida"); OrderWriter.WriteRaw("\n\t\t\t\t");

            OrderWriter.WriteElementString("cny", "India"); OrderWriter.WriteRaw("\n\t\t\t\t");

            OrderWriter.WriteElementString("tel", "+91-120-256 2499"); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteElementString("fax", "+91-120-256 2299"); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");  //aff
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");  //executor


            OrderWriter.WriteStartElement("issue-info"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteStartElement("general-info"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("version-no", "H300.1"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("pii", "ZZZZ"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("doi",""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("embargo", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("issue-production-type", "TYP"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteStartElement("buffer-status");
            OrderWriter.WriteAttributeString("status", "no");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

            //WriteLog("WriteStartElement(\"item-info\")");
            OrderWriter.WriteElementString("jid", ddlJID.Text); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteElementString("journal-no", "00000"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("issn", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("journal-title",""); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteElementString("vol-from", txtVolume.Text); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("vol-to", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("iss-from", txtIssueNo.Text); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("iss-to",""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("supp",""); OrderWriter.WriteRaw("\n");            
            OrderWriter.WriteElementString("paper-type-interior", "zzzz"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("paper-type-cover", PhotoCover); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("cover-finishing", "zzzz"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("binding-type", "zzzz"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("trimmed-size", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("head-margin", "zzzz"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("back-margin", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("typeset-model", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("righthand-start", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("issue-weight", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("spine-width", ""); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteElementString("effect-cover-date", "zzzz"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteStartElement("cover-date"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteStartElement("date-range"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("start-date", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("cover-date-printed", DTYear); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteStartElement("special-issue"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteElementString("special-issue-id", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("full-name", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteStartElement("conference"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteElementString("abbr-name", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("venue", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("effect-date", ""); OrderWriter.WriteRaw("\n");            
            OrderWriter.WriteStartElement("conference-date"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteStartElement("date-range"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("start-date", "2018"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("sponsor", ""); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("editors", ""); OrderWriter.WriteRaw("\n");                       
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");


            OrderWriter.WriteElementString("no-pages-prelims", "1"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-interior", "211"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-extra", "21"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-insert", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-bm", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-print", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-web", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-total", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-blank", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("no-pages-adverts", DTYear); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteStartElement("page-ranges"); OrderWriter.WriteRaw("\n\t");
            OrderWriter.WriteStartElement("page-range");
            OrderWriter.WriteAttributeString("type", "PRELIM"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("first-page", "21"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("last-page", "1"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteStartElement("page-range");
            OrderWriter.WriteAttributeString("type", "INTERIOR"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("first-page", "21"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("last-page", "1"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteStartElement("page-range");
            OrderWriter.WriteAttributeString("type", "BACKMATTER"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteElementString("first-page", "21"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("last-page", "1"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteStartElement("corrections");
            OrderWriter.WriteAttributeString("type", "REMARKS");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");


            OrderWriter.WriteStartElement("issue-content"); OrderWriter.WriteRaw("\n\t");
            if (Article_id.Count > 0)
            {
                ArrayList pdfpage = new ArrayList();

                pdfpage = (ArrayList)ViewState["pdf"];


                for (int i = 0; i < Article_id.Count; i++)
                {
                    int VersionNo = i + 1;
                    string _aid = Article_id[i].ToString();
                    OrderWriter.WriteStartElement("row");
                    OrderWriter.WriteAttributeString("type", "ce"); OrderWriter.WriteRaw("\n\t\t");
                    OrderWriter.WriteElementString("version-no", "S300." + VersionNo.ToString()); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("aid", _aid); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("item-title", "S30"); OrderWriter.WriteRaw("\n");
                    //OrderWriter.WriteElementString("page-from", Pg_from[i].ToString()); OrderWriter.WriteRaw("\n");
                    //OrderWriter.WriteElementString("page-to", Pg_to[i].ToString()); OrderWriter.WriteRaw("\n");
                    //OrderWriter.WriteElementString("pdf-pages", pdfpage[i].ToString()); OrderWriter.WriteRaw("\n");

                    OrderWriter.WriteStartElement("online-version");
                    OrderWriter.WriteAttributeString("type", "print");
                    OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

                    OrderWriter.WriteElementString("no-offprints-tot", "0"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("no-offprints-paid", "0"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("no-offprints-free", "0"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("covers", "N"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("e-suite", "no"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteElementString("no-colour-figs", "0"); OrderWriter.WriteRaw("\n");
                    OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
                    OrderWriter.Flush();
                }
            }
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");

            OrderWriter.WriteStartElement("issue-remarks"); OrderWriter.WriteRaw("\n\t\t");
            OrderWriter.WriteStartElement("issue-remark"); OrderWriter.WriteRaw("\n\t\t\t");
            OrderWriter.WriteElementString("remark-type", "Problem"); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("remark", Remarks); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteElementString("response", ""); OrderWriter.WriteRaw("\n"); // "ASEEDHOU 19-MAY-2011 10:38 Please ignore this remark. Thank you"
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");//order
            OrderWriter.WriteEndElement(); OrderWriter.WriteRaw("\n");//orders

            OrderWriter.Flush();
            OrderWriter.Close();

            Process ValidateProcess = new Process();
            ValidateProcess.StartInfo.FileName = Server.MapPath(@"validation\JWParse.bat");
            WriteLog("Parsing Xml Order File from " + Server.MapPath(@"validation\JWParse.bat"));
            ValidateProcess.StartInfo.Arguments = TempOrder;
            ValidateProcess.StartInfo.UseShellExecute = false;
            ValidateProcess.StartInfo.RedirectStandardOutput = false;
            ValidateProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            ValidateProcess.Start();
            WriteLog("Parsing started...");
            ValidateProcess.Refresh();
            ValidateProcess.WaitForExit();
            ValidateProcess.Dispose();
            WriteLog("Parsing Ended...");

            //waseem
            //if (fileUpload.FileName == "")
            //{
            //    this.RegisterClientScriptBlock("alert", "<script>alert('Please specify input zip file to upload. It should not be left blank!');</script>");
            //    Button2.Enabled = true;
            //    return false;
            //}



            File.Copy(TempOrder, fmspath, true);
            try
            {
                IssueflUpload.SaveAs(fmszippath);
                Thread.Sleep(2000);
            }
            catch
            { 
               
            }
          
            //if (Session["Account"] != null)
            //{
            //    string fmsZip = FMSOrderPath + "\\" + Path.GetFileNameWithoutExtension(TempOrder) + ".zip";
            //    fileUpload.SaveAs(fmsZip);
            //}

            lblDisplay.Text = "Issue XML order generated Sucessfully.";
            return true;
            WriteLog("returning true.");
        }
        catch (Exception ex)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
            return false;
            WriteLog("returning false.");
        }
    }
}
