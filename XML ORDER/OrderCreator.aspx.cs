using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;


public partial class OrderCreator : System.Web.UI.Page
{
    Orders.JIDInfo JIDInfoOBJ = new Orders.JIDInfo();
    Orders.XmlOrder XmlOrderOBJ = new Orders.XmlOrder();
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        if (Request.QueryString["Client"] != null)
            XmlOrderOBJ.Client = Request.QueryString["Client"];
        else
            Response.Redirect("Login.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Inialize();
        }
    }
    protected void Inialize()
    {        

        cmbAccount.Items.Add(XmlOrderOBJ.Client);

        cmbJID.Items.Clear();
        //*****************\\
        JIDInfoOBJ.GetJID(XmlOrderOBJ.Client);
        cmbJID.DataSource = JIDInfoOBJ.JID;
        cmbJID.DataBind();
        //*****************\\

        //*****************\\
        JIDInfoOBJ.GetStage(XmlOrderOBJ.Client);
        cmbStage.DataSource = JIDInfoOBJ.Stage;
        cmbStage.DataBind();
        //*****************\\

        //*****************\\
        for (int i = 0; i < 100; i++)
        {
            cmbFig.Items.Add(i.ToString());
        }
        //*****************\\

        FillDate();


        FillCategory();
        //*****************\\

        CalculateActualDueDate();
        //*****************\\

        FillWorkFlow();

        if (cmbAccount.Text.Trim().Equals("THIEME"))
            GetEditorDetails(cmbJID.Text.Trim(), cmbAccount.Text.Trim());
    }

    private void FillDate()
    {

        FillDay(ReciveDay);
        FillMonth(ReciveMonth);
        FillYear(ReciveYear);

        FillDay(ReviseDay);
        FillMonth(ReviseMonth);
        FillYear(ReviseYear);

        FillDay(AcceptedDay);
        FillMonth(AcceptedMonth);
        FillYear(AcceptedYear);
    }
    protected void DisableControls(Control c)
    {
        if (c is WebControl && !(c is Label))
            ((WebControl)c).Enabled = false;
        foreach (Control child in c.Controls)
            DisableControls(child);


    }
    protected void ExpirePageCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void FillWorkFlow()
    {
        cmbMCE.Items.Clear();
        JIDInfoOBJ.GetWorkFlow(XmlOrderOBJ.Client, cmbJID.Text, cmbStage.Text);
        cmbMCE.DataSource = JIDInfoOBJ.WorkFlow;
        cmbMCE.DataBind();
    }
    protected void cmdGenerate_Click(object sender, EventArgs e)
    {


        if (checkDOI(txtDOI.Text))
        {
            //lblDOIError.Visible = true;
            //lblDOIError.Text = "Exist";
            txtDOI.Text = "";
            string alertScript = "<script language=JavaScript>";
            alertScript += "error(\"---DOI already Exist" + "---\")";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("error", alertScript);
            return;
        }
        
        bool Result = false;

        XmlOrderOBJ.Client = cmbAccount.Text;
        XmlOrderOBJ.JID = cmbJID.Text;
        XmlOrderOBJ.AID = txtAID.Text;

        if (cmbStage.Text.Equals("CE", StringComparison.OrdinalIgnoreCase))
        {
        }
        else if (XmlOrderOBJ.Client.Equals("MEDKNOW", StringComparison.OrdinalIgnoreCase))
        {
            if (isArticleProcessedInFresh() == 0 && !cmbStage.Text.Equals("Fresh", StringComparison.OrdinalIgnoreCase))
            {
                string alertScript = "<script language=JavaScript>";
                alertScript += "alert('---This article is not processes in fresh stage---')</script" + "> ";
                this.RegisterClientScriptBlock("alert", alertScript);
                return;
            }
        }
        if (XmlOrderOBJ.Client.ToUpper().Equals("THIEME"))
        {
            if (!string.IsNullOrEmpty(txtCorAuthEmail.Text))
            {
                string[] CorCCmailid = txtCorCCEmail.Text.Trim().Replace(";", ",").Split(',');
                foreach (string singleemail in CorCCmailid)
                {
                    if (!string.IsNullOrEmpty(singleemail))
                    {
                        if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(singleemail))
                        {
                            string alertScript = "<script language=JavaScript>";
                            alertScript += "alert('Invalid CC email id.')</script" + "> ";
                            this.RegisterClientScriptBlock("alert", alertScript);
                            return;
                        }
                        if (txtCorAuthEmail.Text == singleemail)
                        {
                            string alertScript = "<script language=JavaScript>";
                            alertScript += "alert('Cor author email id should not be include in CC Mail id list.')</script" + "> ";
                            this.RegisterClientScriptBlock("alert", alertScript);
                            return;
                        }
                    }
                }
            }
            else
            {
                string alertScript = "<script language=JavaScript>";
                alertScript += "alert('---Cor Author email id should not be blank---')</script" + "> ";
                this.RegisterClientScriptBlock("alert", alertScript);
                return;
            }
        }
        DateTime DT = DateTime.Today;




        XmlOrderOBJ.ActutalDueDate = AddDaysNoWeekends(JIDInfoOBJ.GetTAT(XmlOrderOBJ.Client, cmbJID.Text, cmbStage.Text));
        XmlOrderOBJ.InternalDuedate = XmlOrderOBJ.ActutalDueDate;




        if (cmbCategory.Text.StartsWith("-S") && cmbAccount.Text.Equals("IS"))
            XmlOrderOBJ.ArticleCategory = "Article";
        else
            XmlOrderOBJ.ArticleCategory = cmbCategory.Text;

        if (cmbArtType.Text.StartsWith("-S") && cmbAccount.Text.Equals("IS"))
            XmlOrderOBJ.ArticleType = "FLA";
        else
            XmlOrderOBJ.ArticleType = cmbArtType.Text;




        XmlOrderOBJ.DOI = txtDOI.Text;
        XmlOrderOBJ.Figs = cmbFig.Text;



        XmlOrderOBJ.Issue = txtIssue.Text;


        XmlOrderOBJ.MSS = txtPages.Text;

        XmlOrderOBJ.Editor = txtEDName.Text;
        XmlOrderOBJ.Designation = txtEDDesign.Text;
        XmlOrderOBJ.Address = txtEDAddress.Text;
        XmlOrderOBJ.Tel = txtEDTel.Text;
        XmlOrderOBJ.Fax = txtEDFax.Text;


        XmlOrderOBJ.CorAuthName = txtCorAuthName.Text;
        XmlOrderOBJ.CorAuthEmail = txtCorAuthEmail.Text;
        XmlOrderOBJ.CorMailCC = txtCorCCEmail.Text;
        XmlOrderOBJ.PDFName = txtPDFName.Text;

        XmlOrderOBJ.FrstAuthDgree = txtAuDeg.Text;
        XmlOrderOBJ.FrstAuthFName = txtAUFN.Text;
        XmlOrderOBJ.FrstAuthSName = txtAUSN.Text;



        int Year = GetNumeric(ReviseYear.Text);
        int Mnth = GetNumeric(ReviseMonth.Text);
        int Day = GetNumeric(ReviseDay.Text);
        XmlOrderOBJ.RevisedDate = GetDate(Year, Mnth, Day);

        Year = GetNumeric(ReciveYear.Text);
        Mnth = GetNumeric(ReciveMonth.Text);
        Day = GetNumeric(ReciveDay.Text);
        XmlOrderOBJ.ReceivedDate = GetDate(Year, Mnth, Day);

        Year = GetNumeric(AcceptedYear.Text);
        Mnth = GetNumeric(AcceptedMonth.Text);
        Day = GetNumeric(AcceptedDay.Text);
        XmlOrderOBJ.AcceptedDate = GetDate(Year, Mnth, Day);

        //Year = GetNumeric()

        XmlOrderOBJ.Stage = cmbStage.Text;
        XmlOrderOBJ.FMSStage = JIDInfoOBJ.GetFMSStage(cmbAccount.Text, cmbStage.Text);

        XmlOrderOBJ.Volume = txtVol.Text;
        XmlOrderOBJ.WorkFlow = cmbMCE.Text;

        //if (XmlOrderOBJ.Client.Equals("MEDKNOW", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(txtPlnrRemark.Text))
        //{
        //    XmlOrderOBJ.Remarks = DateTime.Now.ToString() + " : <htc>" + txtPlnrRemark.Text+"</htc>";
        //}
        //else
            XmlOrderOBJ.Remarks = DateTime.Now.ToString() + " : " + txtPlnrRemark.Text;

        if (XmlOrderOBJ.Client.Equals("EDP"))
            XmlOrderOBJ.ProdSite = XmlOrderOBJ.Client + "EDP-Science";

        else
            XmlOrderOBJ.ProdSite = XmlOrderOBJ.Client + " Journals";



        string TempPath = @"C:\Temp\";


        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }
        string SaveTo = TempPath + "\\" + flUpload.FileName;
        flUpload.SaveAs(SaveTo);

        if (XmlOrderOBJ.Client.Equals("MEDKNOW", StringComparison.OrdinalIgnoreCase))
        {
            XmlOrderOBJ.Client = "LWW";


            string DocName = Orders.Global.GetDocFileName(SaveTo);
            if (!string.IsNullOrEmpty(DocName))
            {
                XmlOrderOBJ.DocName = DocName;
            }

            XmlOrderOBJ.PDFName = XmlOrderOBJ.DocName;

            if (cmbStage.Text.Equals("Fresh", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(XmlOrderOBJ.DocName))
                {
                    string[] Arr = XmlOrderOBJ.ArticleCategory.ToUpper().Split(' ');
                    string CatgryStr = string.Empty;
                    foreach (string a in Arr)
                    {
                        CatgryStr += a[0];
                    }
                    XmlOrderOBJ.DocName = XmlOrderOBJ.DocName.ToUpper() + "_" + CatgryStr;
                }
            }
            InsertArticleDetailForWIP();
        }

        string XMLFilePath = XmlOrderOBJ.CreateXMLOrder();
        if (File.Exists(XMLFilePath))
        {
            string ZipFile = XMLFilePath.Replace(".xml", ".zip");
            if (File.Exists(ZipFile))
            {
                File.Delete(ZipFile);
            }

            //flUpload.SaveAs(ZipFile);
            if (File.Exists(SaveTo))
            {
                File.Move(SaveTo, ZipFile);
            }


            FileInfo zipFl = new FileInfo(ZipFile);


            string FMSPath = string.Empty;
            if (XmlOrderOBJ.Client.Equals("IS"))
                FMSPath = Orders.ConfigDetails.IPIPFMSPath;
            else
                FMSPath = Orders.ConfigDetails.FMSPath;

            if (flUpload.PostedFile.ContentLength == zipFl.Length)
            {
                //=========================================================================
                if (cmbAccount.Text.Trim().ToLower()=="medknow")
                {
                    FMSPath = ConfigurationManager.AppSettings["MEDKNOW"];
                }
                if (cmbAccount.Text.Trim().ToLower() == "thieme")
                {
                    FMSPath = ConfigurationManager.AppSettings["THIEME"];
                }
                //=========================================================================
                string FMSXMLFile = FMSPath.TrimEnd(new char[] { '\\' }) + "\\" + Path.GetFileName(XMLFilePath).Replace(XmlOrderOBJ.Client, cmbAccount.Text);
                File.Copy(XMLFilePath, FMSXMLFile);

                string FMSZipFile = FMSPath.TrimEnd(new char[] { '\\' }) + "\\" + Path.GetFileName(ZipFile).Replace(XmlOrderOBJ.Client, cmbAccount.Text);
                File.Copy(ZipFile, FMSZipFile);

                if (XmlOrderOBJ.Client.Equals("THIEME", StringComparison.OrdinalIgnoreCase))
                    XmlOrderOBJ.SetCorAuthorDetailsforThieme();
                else
                    XmlOrderOBJ.SetCorAuthorDetails();    // Insert Cor Author Details in Database OPSTest

                ResetInfo();
                Result = true;
            }
        }
        if (Result)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('--- Order Created successfully---')</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
        }
    }
    private void ResetInfo()
    {
        txtDOI.Text = "";
        txtVol.Text = "";
        txtIssue.Text = "";
        cmbFig.Text = "0";
        txtAID.Text = "";
        txtPages.Text = "";

        ReciveDay.Text = "-Day-";
        ReciveMonth.Text = "-Month-";
        ReciveYear.Text = "-Year-";

        AcceptedDay.Text = "-Day-";
        AcceptedMonth.Text = "-Month-";
        AcceptedYear.Text = "-Year-";

        ReviseDay.Text = "-Day-";
        ReviseMonth.Text = "-Month-";
        ReviseYear.Text = "-Year-";

        cmbCategory.Text = "-Select-";
        cmbArtType.Text = "-Select-";

        txtArticleTitle.Text = "";
        txtCorAuDeg.Text = "";
        txtCorAuthName.Text = "";
        txtCorAuthEmail.Text = "";
        txtPDFName.Text = "";
        txtAUFN.Text = "";
        txtAUSN.Text = "";
        txtAuDeg.Text = "";
        txtCorCCEmail.Text = "";



    }
    private DateTime GetDate(int Year, int Mnth, int Day)
    {
        DateTime DT = new DateTime();
        if (Year > 0 && Mnth > 0 && Day > 0)
            DT = new DateTime(Year, Mnth, Day);

        return DT;
    }
    private int GetNumeric(string str)
    {
        int Val;
        Int32.TryParse(str, out Val);
        return Val;
    }
    protected void GetEditorDetails(string sJID, string sCust)
    {
        string StrSQL = "";
        string connString = null;
        if (Session["Account"] != null)
        {
            switch (XmlOrderOBJ.Client)
            {
                case "JW-JOURNALS":
                    {
                        connString = ConfigurationManager.ConnectionStrings["AEPSConnectionString"].ConnectionString;
                        StrSQL = "Select Jname, Peditor, Designation, Pe_Email, Phone, Fax, Address from " + sCust + "_Journal1 where Jid='" + sJID + "'";
                        break;
                    }
                case "THIEME":
                    {
                        connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                        StrSQL = "select jname, peditor, designation, pe_email, phone, fax, address from opsdetails where jid='" + sJID + "'";
                        break;
                    }
            }
        }
        if (!string.IsNullOrEmpty(connString))
        {
            SqlConnection AEPSCon = new SqlConnection(connString);

            AEPSCon.Open();
            SqlCommand AEPSCom = new SqlCommand(StrSQL, AEPSCon);
            try
            {
                SqlDataReader AEPSDr = AEPSCom.ExecuteReader();

                if (AEPSDr.HasRows == true)
                {
                    while (AEPSDr.Read() == true)
                    {
                        txtEDName.Text = AEPSDr[1].ToString();
                        txtEDDesign.Text = AEPSDr["Designation"].ToString();
                        txtEDMail.Text = AEPSDr["Pe_Email"].ToString();
                        txtEDTel.Text = AEPSDr["Phone"].ToString();
                        txtEDFax.Text = AEPSDr["Fax"].ToString();
                        txtEDAddress.Text = AEPSDr["Address"].ToString();
                        LblJTitle.Text = AEPSDr[0].ToString();

                        XmlOrderOBJ.JTitle = LblJTitle.Text;
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
    protected void cmbJID_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillWorkFlow();
        ClearEproofingDetails();
        GetEditorDetails(cmbJID.Text.Trim(), cmbAccount.Text.Trim());
        CalculateActualDueDate();
        FillCategory();
    }
    protected void FillCategory()
    {
        try
        {
            cmbCategory.Items.Clear();
            cmbArtType.Items.Clear();

            cmbCategory.Items.Add("-Select-");
            cmbArtType.Items.Add("-Select-");

            if (XmlOrderOBJ.Client.Equals("THIEME"))
            {
                cmbCategory.Items.Add("CASE REPORT");
                cmbCategory.Items.Add("CASE STUDY");
                cmbCategory.Items.Add("CLINICAL LETTER");
                cmbCategory.Items.Add("DIAGNOSTIC PROBLEMS IN HEPATOLOGY");
                cmbCategory.Items.Add("EDITORIAL");
                cmbCategory.Items.Add("ERRATA");
                cmbCategory.Items.Add("ERRATUM");
                cmbCategory.Items.Add("FOREWORD");
                cmbCategory.Items.Add("HOW I DO IT");
                cmbCategory.Items.Add("IN MEMORIAM");
                cmbCategory.Items.Add("INTRODUCTION");
                cmbCategory.Items.Add("LETTER TO THE EDITOR");
                cmbCategory.Items.Add("MINI-REVIEW");
                cmbCategory.Items.Add("MORBIDITY AND MORTALITY CASE");
                cmbCategory.Items.Add("ORIGINAL ARTICLE");
                cmbCategory.Items.Add("PREFACE");
                cmbCategory.Items.Add("REVIEW ARTICLE");
                cmbCategory.Items.Add("SHORT COMMUNICATION");
                cmbCategory.Items.Add("SPECIAL FOCUS SECTION");


                cmbArtType.Items.Add("scientific");
                cmbArtType.Items.Add("magazine");
                cmbArtType.Items.Add("erratum");
                cmbArtType.Items.Add("evaluation");
                cmbArtType.Items.Add("congress-abstract");


                cmbArtType.Items.Add("LIT");
                cmbArtType.Items.Add("BRV");
                cmbArtType.Items.Add("DIS");
                cmbArtType.Items.Add("EDI");
                cmbArtType.Items.Add("EXM");
                cmbArtType.Items.Add("FLA");
                cmbArtType.Items.Add("NWS");
                cmbArtType.Items.Add("PRP");
                cmbArtType.Items.Add("SCO");

                return;
            }
            else
            {
                string ArtCatgryFile = Server.MapPath("") + "\\" + XmlOrderOBJ.Client + "_ArtCat.ini";

                if (File.Exists(ArtCatgryFile))
                {
                    string[] Lines = File.ReadAllLines(ArtCatgryFile, Encoding.Default);
                    cmbCategory.DataSource = Lines;
                    cmbCategory.DataBind();

                }
                string artTypeFile = Server.MapPath("") + "\\" + XmlOrderOBJ.Client + "_Arttype.ini";
                if (File.Exists(artTypeFile))
                {
                    string[] Lines = File.ReadAllLines(artTypeFile, Encoding.Default);
                    cmbArtType.DataSource = Lines;
                    cmbArtType.DataBind();
                }
                return;
            }

        }
        catch (Exception ex)
        {
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert('---" + ex.Message.Replace("'", ":") + "---')";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
        }
    }
    protected void ClearEproofingDetails()
    {
        txtTo.Text = "";
        txtCC.Text = "";
        if (cmbAccount.Text == "JWUSA")
        {
            cmbCTA.Enabled = false;
            cmbCID.Enabled = false;
            cmbOFFPrint.Enabled = false;
        }
        cmbCTA.Text = "";
        cmbCID.Text = "";
        cmbOFFPrint.Text = "";
        txtAdditionalText.Text = "";
        txtRemarks.Text = "";
        lblError.Text = "";
        txtAuAdd.Text = "";
        txtAuCity.Text = "";
        txtAuCny.Text = "";
        txtAuDeg.Text = "";
        txtAuDept.Text = "";
        txtAuEAD.Text = "";
        txtAuFax.Text = "";
        txtAUFN.Text = "";
        txtAuInstitute.Text = "";
        txtAUSN.Text = "";
        txtAuTel.Text = "";
        txtAuzip.Text = "";
        txtCC.Text = "";
        txtCorAuDeg.Text = "";
        txtCorAuFN.Text = "";
        txtCorAuSN.Text = "";
        if ((Session["Account"].Equals("JW-JOURNALS")))
            txtAID.Text = "";
        txtVol.Text = "";
        txtIssue.Text = "";
        txtDueDate.Text = "";
        txtPages.Text = "";
        cmbFig.Text = "0";

        txtDueDate.Text = "";
        txtActualDate.Text = "";
        txtRemarks.Text = "";

    }
    public DateTime AddDaysNoWeekends(int Days)
    {
        int DayIcr = 0;
        DateTime dt = DateTime.Today;
        if (Days == 0)
        {
            return dt;
        }
        try
        {
            //dt=dt.AddDays(-1); //////////////////// To exclude current date
            while (true)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday
                    || dt.ToShortDateString() == "1/26/2012"   //Republic Day
                    || dt.ToShortDateString() == "3/9/2012"    //Annual Day
                    || dt.ToShortDateString() == "3/11/2009"   //Holi
                    || dt.ToShortDateString() == "8/5/2009"    //Rakshabandhan
                    || dt.ToShortDateString() == "8/15/2012"   //Independence Day
                    || dt.ToShortDateString() == "10/6/2011"   //Dussehra
                    || dt.ToShortDateString() == "10/2/2011"   //Mahatma Gandhi's Birthday
                    || dt.ToShortDateString() == "10/26/2011"  //Diwali
                    || dt.ToShortDateString() == "11/10/2011"  //Guru Nanak’s Birthday
                    || dt.ToShortDateString() == "12/25/2011"  //Christmas Day
                    || dt.ToShortDateString() == "12/31/2011") //Christmas Day
                {
                }
                else
                    DayIcr++;

                if (DayIcr == Days) break;
            }
        }
        catch (Exception Ex)
        {
            StringBuilder Str = new StringBuilder();
            Str.AppendLine("Days :" + Days);
            Str.AppendLine("Message :" + Ex.Message);
            Str.AppendLine("DateTime.Today.ToLongDateString() :" + DateTime.Today.ToLongDateString());
            File.WriteAllText(GetLogPath(), Str.ToString());
        }
        return dt;
    }
    private string GetLogPath()
    {
        string AppPath = Server.MapPath("");
        //////////////************Start Create log file path 
        StringBuilder LogFilePath = new StringBuilder("");
        string LogDirPath = "";
        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.AddDays(-7).ToShortDateString().Replace("/", "-");

        if (Directory.Exists(LogDirPath))
        {
            try
            {
                Directory.Delete(LogDirPath, true);
            }
            catch { }
        }

        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.ToShortDateString().Replace("/", "-");
        if (!Directory.Exists(LogDirPath))
        {
            Directory.CreateDirectory(LogDirPath);
        }

        LogFilePath = new StringBuilder(String.Format(DateTime.Now.ToString(), "{0:dd/MM/yyyy}") + ".log");
        LogFilePath.Replace('/', '-');
        LogFilePath.Replace(':', '_').Replace(" ", "_");
        LogFilePath = new StringBuilder(LogDirPath + @"\" + LogFilePath);

        //////////////************End Create log file path 
        return LogFilePath.ToString();
    }
    protected void CalculateActualDueDate()
    {
        //1. First get the TAT
        //2. Pass the TAT to method 'AddDaysNoWeekends' to calculate due date; 

        txtDueDate.Text = AddDaysNoWeekends(JIDInfoOBJ.GetTAT(XmlOrderOBJ.Client, cmbJID.Text, cmbStage.Text)).ToString("dd/MM/yyyy");
        txtActualDate.Text = txtDueDate.Text;
    }
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        // ClientScript.RegisterHiddenField(DateControl, Request.Form[DateControl]);

    }
    protected void cmbMCE_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbMCE.ToolTip = cmbMCE.SelectedIndex.ToString();
        cmdGenerate.Focus();
    }
    protected void RecvDateChanged(object sender, EventArgs e)
    {

    }
    protected void txtActualDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmbStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillWorkFlow();
        CalculateActualDueDate();
        if (cmbStage.Text.Equals("Fresh", StringComparison.OrdinalIgnoreCase) == false)
            txtAID.AutoPostBack = true;
        else
            txtAID.AutoPostBack = false;
    }
    protected void cmbSupMat_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnAddFigure_Click(object sender, ImageClickEventArgs e)
    {


    }
    internal static bool IsNumeric(object ObjectToTest)
    {
        if (ObjectToTest == null)
        {
            return false;

        }
        else
        {
            double OutValue;
            return double.TryParse(ObjectToTest.ToString().Trim(),
                System.Globalization.NumberStyles.Any,

                System.Globalization.CultureInfo.CurrentCulture,

                out OutValue);
        }
    }
    protected string getPIT(string artType)
    {
        switch (artType)
        {
            case "Abstract":
                {
                    return "ABS";
                    break;
                }
            case "Article":
                {
                    return "FLA";
                    break;
                }
            case "Addendum":
                {
                    return "ADD";
                    break;
                }
            case "Advertisement":
                {
                    return "ADV";
                    break;
                }
            case "Announcements":
                {
                    return "ANN";
                    break;
                }
            case "Book Review":
                {
                    return "BRV";
                    break;
                }
            case "Calendar":
                {
                    return "CAL";
                    break;
                }
            case "About a conference":
                {
                    return "CNF";
                    break;
                }
            case "Contents list":
                {
                    return "CON";
                    break;
                }
            case "Correspondence":
                {
                    return "COR";
                    break;
                }
            case "Letter to the editor":
                {
                    return "COR";
                    break;
                }
            case "Reply to the letter":
                {
                    return "COR";
                    break;
                }
            case "Discussion":
                {
                    return "DIS";
                    break;
                }
            case "Editorial":
                {
                    return "EDI";
                    break;
                }
            case "Erratum":
                {
                    return "ERR";
                    break;
                }
            case "Examination":
                {
                    return "EXM";
                    break;
                }
            case "Full-length article":
                {
                    return "FLA";
                    break;
                }
            case "Index":
                {
                    return "IND";
                    break;
                }
            case "Literature":
                {
                    return "LIT";
                    break;
                }
            case "Miscellaneous":
                {
                    return "MIS";
                    break;
                }
            case "News":
                {
                    return "NWS";
                    break;
                }
            case "Personal report":
                {
                    return "PRP";
                    break;
                }
            case "Product review":
                {
                    return "PRV";
                    break;
                }
            case "Publisher’s note":
                {
                    return "PUB";
                    break;
                }
            case "Request for assistance":
                {
                    return "REQ";
                    break;
                }
            case "Review Article":
                {
                    return "REV";
                    break;
                }
            case "Short Communication":
                {
                    return "SCO";
                    break;
                }
            case "Short Review":
                {
                    return "SSU";
                    break;
                }
            case "Minireview":
                {
                    return "SSU";
                    break;
                }
            default:
                {
                    return "NA";
                    break;
                }
        }
    }
    protected void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void AcceptedDay_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CmdXmlGenerate_Click(object sender, EventArgs e)
    {

    }

    protected void txtAID_TextChanged(object sender, EventArgs e)
    {

        string AID = txtAID.Text;
        string RN = System.Text.RegularExpressions.Regex.Match(txtAID.Text, "[rR][0-9]+").Value;
        if (RN.Length < 4 && AID.EndsWith(RN))
        {
            AID = AID.Substring(0, AID.Length - RN.Length);
            txtAID.Text = AID;
        }

        if (!cmbStage.SelectedItem.Text.Equals("FRESH") || true)
        {
            if (XmlOrderOBJ.Client.Equals("MEDKNOW", StringComparison.OrdinalIgnoreCase))
            {
                XmlOrderOBJ.Client = "LWW";
            }
            XmlOrderOBJ.JID = cmbJID.Text;
            XmlOrderOBJ.AID = txtAID.Text;
            XmlOrderOBJ.Stage = cmbStage.SelectedItem.Text;

            XmlOrderOBJ.Stage = "Fresh";
            XmlOrderOBJ.FMSStage = "S100";

            //XmlOrderOBJ.Stage = cmbStage.Items[cmbStage.SelectedIndex - 1].Text;

            if (XmlOrderOBJ.RevisedXMLOrder())
            {
                txtDOI.Text = XmlOrderOBJ.DOI;
                txtVol.Text = XmlOrderOBJ.Volume;
                txtIssue.Text = XmlOrderOBJ.Issue;


                if (cmbFig.Items.Contains(new ListItem(XmlOrderOBJ.Figs)))
                {
                    cmbFig.Text = XmlOrderOBJ.Figs;
                }
                else
                {
                    cmbFig.Items.Add(new ListItem(XmlOrderOBJ.Figs));
                    cmbFig.Text = XmlOrderOBJ.Figs;
                }
                txtPages.Text = XmlOrderOBJ.MSS;
                txtCorAuthName.Text = XmlOrderOBJ.CorAuthName;
                txtCorAuthEmail.Text = XmlOrderOBJ.CorAuthEmail;

                txtAUFN.Text = XmlOrderOBJ.FrstAuthFName;
                txtAUSN.Text = XmlOrderOBJ.FrstAuthSName;
                txtAuDeg.Text = XmlOrderOBJ.FrstAuthDgree;

                txtPlnrRemark.Text = XmlOrderOBJ.Remarks;

                txtArticleTitle.Text = XmlOrderOBJ.ArtTitle;

                if (cmbCategory.Items.Contains(new ListItem(XmlOrderOBJ.ArticleCategory)))
                    cmbCategory.Text = XmlOrderOBJ.ArticleCategory;

                if (cmbArtType.Items.Contains(new ListItem(XmlOrderOBJ.ArticleType)))
                    cmbArtType.Text = XmlOrderOBJ.ArticleType;

                if (XmlOrderOBJ.ReceivedDate.Year != 1)
                {
                    ReciveDay.Text = XmlOrderOBJ.ReceivedDate.Day.ToString().PadLeft(2, '0');
                    ReciveMonth.Text = XmlOrderOBJ.ReceivedDate.Month.ToString().PadLeft(2, '0');
                    ReciveYear.Text = XmlOrderOBJ.ReceivedDate.Year.ToString().PadLeft(2, '0');
                }

                if (XmlOrderOBJ.RevisedDate.Year != 1)
                {
                    ReviseDay.Text = XmlOrderOBJ.RevisedDate.Day.ToString().PadLeft(2, '0');
                    ReviseMonth.Text = XmlOrderOBJ.RevisedDate.Month.ToString().PadLeft(2, '0');
                    ReviseYear.Text = XmlOrderOBJ.RevisedDate.Year.ToString().PadLeft(2, '0');
                }

                if (XmlOrderOBJ.AcceptedDate.Year != 1)
                {
                    AcceptedDay.Text = XmlOrderOBJ.AcceptedDate.Day.ToString().PadLeft(2, '0');
                    AcceptedMonth.Text = XmlOrderOBJ.AcceptedDate.Month.ToString().PadLeft(2, '0');
                    AcceptedYear.Text = XmlOrderOBJ.AcceptedDate.Year.ToString().PadLeft(2, '0');
                }

                if (!string.IsNullOrEmpty(XmlOrderOBJ.ArticleCategory))
                {
                    if (!cmbCategory.Items.Contains(new ListItem(XmlOrderOBJ.ArticleCategory)))
                    {
                        cmbCategory.Items.Add(XmlOrderOBJ.ArticleCategory);
                    }
                    cmbCategory.Text = XmlOrderOBJ.ArticleCategory;
                }
            }
        }
    }

    private void FillYear(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Add("-Year-");
        DDL.Items.Add(DateTime.Now.Year.ToString());
        DDL.Items.Add((DateTime.Now.Year - 1).ToString());
        DDL.Items.Add((DateTime.Now.Year - 2).ToString());
        DDL.Items.Add((DateTime.Now.Year - 3).ToString());
        DDL.Items.Add((DateTime.Now.Year - 4).ToString());
        DDL.Items.Add("");

    }

    private void FillDay(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Add("-Day-");
        for (int i = 1; i < 32; i++)
        {
            DDL.Items.Add(i.ToString().PadLeft(2, '0'));
        }
        DDL.Items.Add("");
    }

    private void FillMonth(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Add("-Month-");
        for (int i = 1; i < 13; i++)
        {
            DDL.Items.Add(i.ToString().PadLeft(2, '0'));
        }
        DDL.Items.Add("");
    }


    private void InsertArticleDetailForWIP()
    {
        DateTime _DueDate = AddDaysNoWeekends(DateTime.Today, 2);

        string Stage = cmbStage.Text;
        String connString = ConfigurationManager.ConnectionStrings["MDKConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("[usp_InsertMDKWIPDetail]", conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Client", cmbAccount.Text));
                command.Parameters.Add(new SqlParameter("@JID", cmbJID.Text));
                command.Parameters.Add(new SqlParameter("@AID", txtAID.Text.Replace("_","-").Trim()));
                command.Parameters.Add(new SqlParameter("@STAGE", Stage));
                command.Parameters.Add(new SqlParameter("@MSS", txtPages.Text));
                command.Parameters.Add(new SqlParameter("@FIGS", cmbFig.Text));
                command.Parameters.Add(new SqlParameter("@DueDate", _DueDate));
                command.Parameters.Add(new SqlParameter("@ISSUE", txtIssue.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@ServerIP", "172.16.0.128"));
                command.Parameters.Add(new SqlParameter("@Remark", txtPlnrRemark.Text));
                command.Parameters.Add(new SqlParameter("@Annotation", "0"));
                command.Parameters.Add(new SqlParameter("@FNAME", XmlOrderOBJ.PDFName));
                command.ExecuteNonQuery();
            }
        }
    }
    private int isArticleProcessedInFresh()
    {
        String connString = ConfigurationManager.ConnectionStrings["MDKConnectionString"].ConnectionString;

        Int32 RNO = 0;
        string sql = "select dbo.usp_isArticleprocessedInFresh(@CLIENT,@JID,@AID)";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CLIENT", XmlOrderOBJ.Client);
            cmd.Parameters.AddWithValue("@JID", XmlOrderOBJ.JID);
            cmd.Parameters.AddWithValue("@AID", XmlOrderOBJ.AID);
            //cmd.Parameters.AddWithValue("@AID","ejo_10_16");
            try
            {

                RNO = (Int32)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return (int)RNO;
    }
    public DateTime AddDaysNoWeekends(DateTime dt, int Days)
    {
        int DayIcr = 0;
        while (true)
        {
            dt = dt.AddDays(1);
            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
            }
            else
                DayIcr++;

            if (DayIcr == Days) break;
        }
        return dt;
    }
    protected void txtDOI_TextChanged(object sender, EventArgs e)
    {
        if (checkDOI(txtDOI.Text))
        {
            //lblDOIError.Visible = true;
            //lblDOIError.Text = "Exist";
            string alertScript = "<script language=JavaScript>";
            alertScript += "alert(\"---DOI already Exist" + "---\")";
            alertScript += "</script" + "> ";
            this.RegisterClientScriptBlock("alert", alertScript);
            txtDOI.Text = "";
        }
    }
    private bool checkDOI(string doival)
    {
        bool isDOIExist = false;
        // isDOIExist = CheckDOIFromXMLFile(doival);
        //if (!isDOIExist)
        //{
        if (!string.IsNullOrEmpty(doival))
        {
            String connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    //SqlCommand command = new SqlCommand("select DOI from CorAuthorDetaill where DOI='" + doival + "' and ISNULL(duplicate_doi_allowed,false) = 'true' ", conn);
                    SqlCommand command = new SqlCommand("select DOI from CorAuthorDetaill CD Left join [XMLOrder].[dbo].[JIDINFO] JD ON JD.JId = Cd.Jid where DOI='" + doival + "' and ISNULL(duplicate_doi_allowed,1) = 1", conn);
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.Read())
                    {
                        if (rdr.HasRows)
                        {
                            isDOIExist = true;
                        }
                        else
                            isDOIExist = false;
                    }

                }
            }
        }
        // }
        return isDOIExist;

    }

    private bool CheckDOIFromXMLFile(string doival)
    {
        bool doiExist = false;
        string AID = txtAID.Text;
        string RN = System.Text.RegularExpressions.Regex.Match(txtAID.Text, "[rR][0-9]+").Value;
        if (RN.Length < 4 && AID.EndsWith(RN))
        {
            AID = AID.Substring(0, AID.Length - RN.Length);
            txtAID.Text = AID;
        }

        if (!cmbStage.SelectedItem.Text.Equals("FRESH") || true)
        {
            if (XmlOrderOBJ.Client.Equals("MEDKNOW", StringComparison.OrdinalIgnoreCase))
            {
                XmlOrderOBJ.Client = "LWW";
            }
            XmlOrderOBJ.JID = cmbJID.Text;
            XmlOrderOBJ.AID = txtAID.Text;
            XmlOrderOBJ.Stage = cmbStage.SelectedItem.Text;
            XmlOrderOBJ.CreateXMLOrder();

            XmlOrderOBJ.Stage = "Fresh";
            XmlOrderOBJ.FMSStage = "S100";

            if (XmlOrderOBJ.GetDOIFromXML())
            {
                if (XmlOrderOBJ.DOI == doival)
                    doiExist = true;
                else
                    doiExist = false;
            }
        }
        return doiExist;
    }
}



