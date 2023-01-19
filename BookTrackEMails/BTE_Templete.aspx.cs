using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Xml;


public partial class BTE_Templete : System.Web.UI.Page
{
    int count = 0;
    DataTable dt;
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    string AID;
    string Volume_Issue_PII;
    string Book_Title;
    string Book_Subtitle;
    string Book_Editors;
    string ISBN;
    string Book_lang;
    string Stage;
    string cno;
    string PII;
    string DOI;
    string BookId;
    string Job_Type;
    static int cnt = 1;
    static int cnt1 = 1;
    string PIIAid;
    string PIIIssn;
    string PIIIsbn;
    string PIIYear;
    int ClcNum;
    int[] WF = new int[15];
    int[] WFEX = new int[18];
    int i;
    string Formatted_ISBN;
    string copyright_line;
    string copyright_type;
    string copyright_owner;
    string copyright_year;
    string imprint;

    string edition;
    string trim_size;
    string color;
    string OWNER;

    GlbClasses objGlbCls = new GlbClasses();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Stagebind();
            Jobbind();
            Languagebind();
        }
    }
    private void Stagebind()
    {
        ddlbtestage.Items.Insert(0, new ListItem("Select Stage", "0"));
        ddlbtestage.Items.Insert(1, "Q300");
        ddlbtestage.Items.Insert(2, "S300");
        ddlbtestage.Items.Insert(3, "S300-Resupply");
    }
    private void Jobbind()
    {
        ddlbtejob.Items.Insert(0, new ListItem("Job Type", "0"));
        ddlbtejob.Items.Insert(1, "EHS");
        ddlbtejob.Items.Insert(2, "S&T");
        ddlbtejob.Items.Insert(3, "BS");
    }
    private void Languagebind()
    {
        ddlbtelanguage.Items.Insert(0, new ListItem("Language", "0"));
        //ddlbtelanguage.Items.Insert(1, "English");
        ddlbtelanguage.Items.Insert(1, "Spanish");
        ddlbtelanguage.Items.Insert(2, "French");
        ddlbtelanguage.Items.Insert(3, "Italian");
        ddlbtelanguage.Items.Insert(4, "Portuguese");
        ddlbtelanguage.Items.Insert(5, "English");
        ddlbtelanguage.Items.Insert(6, "German");
    }
    protected int getCount()
    {
        count = count + 1;
        return count;
    }
    protected void btnbteextract_Click(object sender, EventArgs e)
 
   {
            Formatted_ISBN = txtbteisbn.Text.Trim();
            if (IsNumeric(txtbtechp.Text.Trim()) == false)
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('No. of chapters should be numeric...');" + System.Environment.NewLine +
                "</script>");
                return;
            }
            if (Formatted_ISBN.Contains("-"))
            {
                Fill_Grid();
            }
            else
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('Please enter formatted ISBN...');" + System.Environment.NewLine +
                "</script>");
                return;
            }
      
    }   
    private void Fill_Grid()
    {
        if (CheckExtendedIsbn(Formatted_ISBN) == true)
        {
            if (objGlbCls.objData.Validate_Isbn(Formatted_ISBN) == false)
            {
                string result = objGlbCls.objCpii.Checkpii_ppm(Formatted_ISBN);
                if (result != "")
                {
                    string[] arr = result.Split('_');

                    txtbtevolumewithissue.Text = arr[0];
                    txtbooktitle.Text = arr[1];
                    txtbooksubtitle.Text = arr[2];
                    txtbookeditors.Text = arr[3];
                    if (arr[4].Trim() == "")
                    {
                        txtimprint.Text = "";
                    }
                    else 
                    {
                        txtimprint.Text = arr[4];
                    }
                    /* New fields */

                    txtISSN.Text = arr[5];
                    txtJID.Text = arr[6];
                    txtSeriesTit.Text = arr[7];
                    txtVol.Text = arr[8];

                    if (arr[10].Trim() == "")
                    {
                        copyright_year = DateTime.Now.Year.ToString();
                    }
                    else
                    {
                        copyright_year = arr[10];
                    }

                    edition = arr[11].Trim();
                    trim_size = arr[12].Trim();
                    color = arr[13].Replace("- colors","").Trim();
                    OWNER = arr[14].Trim();
                    txttDesignName.Text = arr[15];
                    txtRefStyle.Text = arr[16];
                    txtyears.Value = copyright_year;
                    txtcolor.Value = color;
                    txtsize.Value = trim_size;
                    txtediton.Value = edition;
                    txtowner.Value = OWNER;

                }
                else
                {
                    txtbtevolumewithissue.Text = GeneratPII_Extended_Hub(txtbteisbn.Text, txtbtehubid.Text);
                }

                dt = new DataTable();
                dt.Columns.Add(new DataColumn("PII", typeof(string)));
                dt.Columns.Add(new DataColumn("DOI", typeof(string)));
                for (int i = 1; i <= Convert.ToInt32(txtbtechp.Text); i++)
                {
                    string taid = "";
                    if (i.ToString().Length == 1)
                    {
                        taid = "0000" + i.ToString();
                    }
                    else if (i.ToString().Length == 2)
                    {
                        taid = "000" + i.ToString();
                    }
                    else if (i.ToString().Length == 3)
                    {
                        taid = "00" + i.ToString();
                    }
                    else if (i.ToString().Length == 4)
                    {
                        taid = "0" + i.ToString();
                    }
                    else
                    {
                        taid = i.ToString();
                    }
                    try
                    {
                        DataRow drw = dt.NewRow();
                        string tpii = GeneratPII_Extended(txtbteisbn.Text, taid);
                        drw[0] = tpii;
                        drw[1] = 10.1016 + "/" + tpii;
                        dt.Rows.Add(drw);
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                        "alert('Problem in Fill_Grid function..');" + System.Environment.NewLine +
                        "</script>");
                        return;
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    linkbtnsave.Visible = true;
                }
                else
                {
                    linkbtnsave.Visible = false;
                    txtbtevolumewithissue.Text = "";
                    txtbooktitle.Text = "";
                    txtbooksubtitle.Text = "";
                    txtbookeditors.Text = "";
                    txtbteisbn.Text = "";
                    
                    txtISSN.Text = "";
                    txtJID.Text = "";
                    txtSeriesTit.Text = "";
                    txtVol.Text = "";
                }
            }
            else
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('This isbn will be integrated.Please confirm..');" + System.Environment.NewLine + "</script>");
                return;
            }
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Wrong ISBN Number....');" + System.Environment.NewLine +
            "</script>");
            return;
        }
    }
    private void fill_again()
    {
        try
        {

          copyright_year=  txtyears.Value ;
          color=txtcolor.Value ;
          trim_size =txtsize.Value ;
          edition= txtediton.Value;
          OWNER=txtowner.Value ;
        }
        catch(Exception err)
        {
        
        }
    
    }

    private void Fill_Grid_onchange()
    {
        if (CheckExtendedIsbn(Formatted_ISBN) == true)
        {
            //if (objGlbCls.objData.Validate_Isbn(Formatted_ISBN) == false)
            //{
                string result = objGlbCls.objCpii.Checkpii_ppm(Formatted_ISBN);
                if (result != "")
                {
                    string[] arr = result.Split('_');
                    txtbtevolumewithissue.Text = arr[0];
                    txtbooktitle.Text = arr[1];
                    txtbooksubtitle.Text = arr[2];
                    txtbookeditors.Text = arr[3];
                    txtimprint.Text = arr[4];

                    /* New fields */

                    txtISSN.Text = arr[5];
                    txtJID.Text = arr[6];
                    txtSeriesTit.Text = arr[7];
                    txtVol.Text = arr[8];

                    if (arr[10].Trim() == "")
                    {
                        copyright_year = DateTime.Now.Year.ToString();
                    }
                    else
                    {
                        copyright_year = arr[10];
                    }

                    edition = arr[11].Trim();
                    trim_size = arr[12].Trim();
                    color = arr[13].Replace("- colors", "").Trim();
                    OWNER = arr[14].Trim();

                    txttDesignName.Text = arr[15];
                    txtRefStyle.Text = arr[16];

                    txtyears.Value = copyright_year;
                    txtcolor.Value = color;
                    txtsize.Value = trim_size;
                    txtediton.Value = edition;
                    txtowner.Value = OWNER;

                    if (txtISSN.Text=="")
                    {

                        ddlbtestage.SelectedIndex = 1;
                        
                        if (arr[9] != "")
                        {
                            if (arr[9].ToUpper().Contains("HEALTH"))
                            {
                                ddlbtejob.SelectedIndex = 1;
                            }
                            else
                            {
                                ddlbtejob.SelectedIndex = 2;
                            }

                        }
                        else
                        {
                            ddlbtestage.SelectedIndex = 0;
                        }                     


                    }
                    else
                    {
                        ddlbtestage.SelectedIndex = 2;
                        ddlbtejob.SelectedIndex = 3;
                    }


                }
                else
                {
                    txtbtevolumewithissue.Text = GeneratPII_Extended_Hub(txtbteisbn.Text, txtbtehubid.Text);
                }

                //dt = new DataTable();
                //dt.Columns.Add(new DataColumn("PII", typeof(string)));
                //dt.Columns.Add(new DataColumn("DOI", typeof(string)));
                //for (int i = 1; i <= Convert.ToInt32(txtbtechp.Text); i++)
                //{
                //    string taid = "";
                //    if (i.ToString().Length == 1)
                //    {
                //        taid = "0000" + i.ToString();
                //    }
                //    else if (i.ToString().Length == 2)
                //    {
                //        taid = "000" + i.ToString();
                //    }
                //    else if (i.ToString().Length == 3)
                //    {
                //        taid = "00" + i.ToString();
                //    }
                //    else if (i.ToString().Length == 4)
                //    {
                //        taid = "0" + i.ToString();
                //    }
                //    else
                //    {
                //        taid = i.ToString();
                //    }
                //    try
                //    {
                //        DataRow drw = dt.NewRow();
                //        string tpii = GeneratPII_Extended(txtbteisbn.Text, taid);
                //        drw[0] = tpii;
                //        drw[1] = 10.1016 + "/" + tpii;
                //        dt.Rows.Add(drw);
                //    }
                //    catch (Exception ex)
                //    {
                //        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                //        "alert('Problem in Fill_Grid function..');" + System.Environment.NewLine +
                //        "</script>");
                //        return;
                //    }
                //}
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                //if (GridView1.Rows.Count > 0)
                //{
                //    linkbtnsave.Visible = true;
                //}
                //else
                //{
                //    linkbtnsave.Visible = false;
                //    txtbtevolumewithissue.Text = "";
                //    txtbooktitle.Text = "";
                //    txtbooksubtitle.Text = "";
                //    txtbookeditors.Text = "";
                //    txtbteisbn.Text = "";

                //    txtISSN.Text = "";
                //    txtJID.Text = "";
                //    txtSeriesTit.Text = "";
                //    txtVol.Text = "";
                //}
            //}
           
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Wrong ISBN Number....');" + System.Environment.NewLine +
            "</script>");
            return;
        }
    }
    public string GeneratPII_Extended_Hub(string extendedisbn, string extendedaid)
    {
        string pii;
        PIIIsbn = extendedisbn.Replace("-", "");
        PIIIsbn = PIIIsbn.Replace(" ", "");
        PIIAid = extendedaid;
        WFEX[0] = 67 * Convert.ToInt32(PIIIsbn.Substring(0, 1));
        WFEX[1] = 61 * Convert.ToInt32(PIIIsbn.Substring(1, 1));
        WFEX[2] = 59 * Convert.ToInt32(PIIIsbn.Substring(2, 1));
        WFEX[3] = 53 * Convert.ToInt32(PIIIsbn.Substring(3, 1));
        WFEX[4] = 47 * Convert.ToInt32(PIIIsbn.Substring(4, 1));
        WFEX[5] = 43 * Convert.ToInt32(PIIIsbn.Substring(5, 1));
        WFEX[6] = 41 * Convert.ToInt32(PIIIsbn.Substring(6, 1));
        WFEX[7] = 37 * Convert.ToInt32(PIIIsbn.Substring(7, 1));
        WFEX[8] = 31 * Convert.ToInt32(PIIIsbn.Substring(8, 1));
        WFEX[9] = 29 * Convert.ToInt32(PIIIsbn.Substring(9, 1));
        WFEX[10] = 23 * Convert.ToInt32(PIIIsbn.Substring(10, 1));
        WFEX[11] = 19 * Convert.ToInt32(PIIIsbn.Substring(11, 1));

        if (PIIIsbn.Substring(12, 1) == "X")
        {
            WFEX[12] = 17 * 10;
        }
        else
        {
            WFEX[12] = 17 * Convert.ToInt32(PIIIsbn.Substring(12, 1));
        }
        if (PIIAid.Substring(0, 1) == "X")
        {
            WFEX[13] = 13 * 10;
        }
        //WFEX[13] = 13 * Convert.ToInt32(PIIAid.Substring(0, 1));
        WFEX[14] = 7 * Convert.ToInt32(PIIAid.Substring(1, 1));
        WFEX[15] = 5 * Convert.ToInt32(PIIAid.Substring(2, 1));
        WFEX[16] = 3 * Convert.ToInt32(PIIAid.Substring(3, 1));
        WFEX[17] = 2 * Convert.ToInt32(PIIAid.Substring(4, 1));

        ClcNum = 0;
        for (i = 0; i <= 17; i++)
        {
            ClcNum = ClcNum + WFEX[i];
        }
        ClcNum = ClcNum % 11;
        string TempX;
        if (ClcNum == 10)
        {
            TempX = "X";
        }
        else
        {
            TempX = ClcNum.ToString().Trim();
        }
        if (extendedisbn.IndexOf("-") > 0)
        {
            pii = "B" + extendedisbn + "." + PIIAid + "-" + TempX;
        }
        else if (extendedisbn.IndexOf(" ") > 0)
        {
            pii = "B" + extendedisbn + "." + PIIAid + "-" + TempX;
        }
        else
        {
            pii = "B" + PIIIsbn + PIIAid + TempX;
        }
        return pii;
    }
    public string GeneratPII_Extended(string extendedisbn, string extendedaid)
    {
        string pii;
        PIIIsbn = extendedisbn.Replace("-", "");
        PIIIsbn = PIIIsbn.Replace(" ", "");
        PIIAid = extendedaid;
        WFEX[0] = 67 * Convert.ToInt32(PIIIsbn.Substring(0, 1));
        WFEX[1] = 61 * Convert.ToInt32(PIIIsbn.Substring(1, 1));
        WFEX[2] = 59 * Convert.ToInt32(PIIIsbn.Substring(2, 1));
        WFEX[3] = 53 * Convert.ToInt32(PIIIsbn.Substring(3, 1));
        WFEX[4] = 47 * Convert.ToInt32(PIIIsbn.Substring(4, 1));
        WFEX[5] = 43 * Convert.ToInt32(PIIIsbn.Substring(5, 1));
        WFEX[6] = 41 * Convert.ToInt32(PIIIsbn.Substring(6, 1));
        WFEX[7] = 37 * Convert.ToInt32(PIIIsbn.Substring(7, 1));
        WFEX[8] = 31 * Convert.ToInt32(PIIIsbn.Substring(8, 1));
        WFEX[9] = 29 * Convert.ToInt32(PIIIsbn.Substring(9, 1));
        WFEX[10] = 23 * Convert.ToInt32(PIIIsbn.Substring(10, 1));
        WFEX[11] = 19 * Convert.ToInt32(PIIIsbn.Substring(11, 1));

        if (PIIIsbn.Substring(12, 1) == "X")
        {
            WFEX[12] = 17 * 10;
        }
        else
        {
            WFEX[12] = 17 * Convert.ToInt32(PIIIsbn.Substring(12, 1));
        }
        WFEX[13] = 13 * Convert.ToInt32(PIIAid.Substring(0, 1));
        WFEX[14] = 7 * Convert.ToInt32(PIIAid.Substring(1, 1));
        WFEX[15] = 5 * Convert.ToInt32(PIIAid.Substring(2, 1));
        WFEX[16] = 3 * Convert.ToInt32(PIIAid.Substring(3, 1));
        WFEX[17] = 2 * Convert.ToInt32(PIIAid.Substring(4, 1));

        ClcNum = 0;
        for (i = 0; i <= 17; i++)
        {
            ClcNum = ClcNum + WFEX[i];
        }
        ClcNum = ClcNum % 11;
        string TempX;
        if (ClcNum == 10)
        {
            TempX = "X";
        }
        else
        {
            TempX = ClcNum.ToString().Trim();
        }
        if (extendedisbn.IndexOf("-") > 0)
        {
            pii = "B" + extendedisbn + "." + PIIAid + "-" + TempX;
        }
        else if (extendedisbn.IndexOf(" ") > 0)
        {
            pii = "B" + extendedisbn + "." + PIIAid + "-" + TempX;
        }
        else
        {
            pii = "B" + PIIIsbn + PIIAid + TempX;
        }
        return pii;
    }
    private bool CheckExtendedIsbn(string chkisbn)
    {
        try
        {
            int[] ChkEXISBN = new int[15];
            int j;
            int ChkNum;
            chkisbn = chkisbn.Replace("-", "");
            chkisbn = chkisbn.Replace(" ", "");

            ChkEXISBN[0] = 1 * Convert.ToInt32(chkisbn.Substring(0, 1));
            ChkEXISBN[1] = 3 * Convert.ToInt32(chkisbn.Substring(1, 1));
            ChkEXISBN[2] = 1 * Convert.ToInt32(chkisbn.Substring(2, 1));
            ChkEXISBN[3] = 3 * Convert.ToInt32(chkisbn.Substring(3, 1));
            ChkEXISBN[4] = 1 * Convert.ToInt32(chkisbn.Substring(4, 1));
            ChkEXISBN[5] = 3 * Convert.ToInt32(chkisbn.Substring(5, 1));
            ChkEXISBN[6] = 1 * Convert.ToInt32(chkisbn.Substring(6, 1));
            ChkEXISBN[7] = 3 * Convert.ToInt32(chkisbn.Substring(7, 1));
            ChkEXISBN[8] = 1 * Convert.ToInt32(chkisbn.Substring(8, 1));
            ChkEXISBN[9] = 3 * Convert.ToInt32(chkisbn.Substring(9, 1));
            ChkEXISBN[10] = 1 * Convert.ToInt32(chkisbn.Substring(10, 1));
            ChkEXISBN[11] = 3 * Convert.ToInt32(chkisbn.Substring(11, 1));

            ChkNum = 0;
            for (j = 0; j <= 11; j++)
            {
                ChkNum = ChkNum + ChkEXISBN[j];
            }
            ChkNum = ChkNum % 10;
            ChkNum = 10 - ChkNum;
            string c;
            if (ChkNum == 10)
            {
                c = "0";
            }
            else
            {
                c = ChkNum.ToString().Trim();
            }
            if (c == chkisbn.Substring(12, 1).ToUpper())
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('ISBN is not in correct format..');" + System.Environment.NewLine +
                "</script>");
            return false;
        }
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
    protected void linkbtnsave_Click(object sender, EventArgs e)
    {
        if (ddlbtestage.SelectedItem.Text != "<--Select Stage-->" && ddlbtejob.SelectedItem.Text != "<--JobType-->" && txtbooktitle.Text.Length != 0 && txtbteisbn.Text.Length != 0 && txtbookeditors.Text.Length != 0 && txtbtevolumewithissue.Text.Length != 0 && GridView1.Rows.Count != 0)
            {
                //book_info

                fill_again();
            
                Stage = ddlbtestage.SelectedItem.Text;
                Job_Type = ddlbtejob.SelectedItem.Text;
                Session["stg"] = Stage;
                Session["JT"] = Job_Type;
                string[] sep = { "\r\n" };
                Volume_Issue_PII = txtbtevolumewithissue.Text;
                Book_Title = txtbooktitle.Text;
                if (Book_Title.Contains("'"))
                {
                    Book_Title = Book_Title.Replace("'", "''");
                }
                Book_Subtitle = txtbooksubtitle.Text;
                if (Book_Subtitle.Contains("'"))
                {
                    Book_Subtitle = Book_Subtitle.Replace("'", "''");
                }
                Book_Editors = txtbookeditors.Text;
                imprint = txtimprint.Text;
                Formatted_ISBN = txtbteisbn.Text;
                BookId = Formatted_ISBN.Replace("-", "");
                BookId = ddlbtejob.SelectedItem.Text + "_" + BookId + "_" + ddlbtestage.SelectedItem.Text;
                Session["bkid"] = BookId;
                Book_lang = ddlbtelanguage.Text;
                Insert_Copyright_info(Book_lang);
            
                //chapter_info
                int i = 0;
                //int cnt = 1;
                for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                {
                    cno = Convert.ToString(i + 1);
                    PII = GridView1.Rows[i].Cells[1].Text;
                    DOI = GridView1.Rows[i].Cells[2].Text;
                    if (objGlbCls.objData.getbookid(BookId,Stage) == false)
                    {
                        cno = cno.Replace("-", "");
                        if (Session["location"].ToString() == "NSEZ" && Job_Type == "S&T")
                        {
                            //As per jitender
                            cno = cno + "0";
                            int a = Convert.ToInt32(cno);
                            a = a / 2;
                            cno = a.ToString();
                            if (cno.Length == 1)
                            {
                                cno = "c000" + cno;
                            }
                            else if (cno.Length == 2)
                            {
                                cno = "c00" + cno;
                            }
                            else if (cno.Length == 3)
                            {
                                cno = "c0" + cno;
                            }
                            else if (cno.Length == 4)
                            {
                                cno = "c" + cno;
                            }
                        }
                        else
                        {
                            cno = cno + "0";
                            int a = Convert.ToInt32(cno);
                            a = a / 2;
                            cno = a.ToString();
                            if (cno.Length == 1)
                            {
                                cno = "c000" + cno;
                            }
                            else if (cno.Length == 2)
                            {
                                cno = "c00" + cno;
                            }
                            else if (cno.Length == 3)
                            {
                                cno = "c0" + cno;
                            }
                            else if (cno.Length == 4)
                            {
                                cno = "c" + cno;
                            }
                        }

                        string[] aid1;
                        string[] aid2;
                        aid1 = PII.Split('.');
                        aid2 = aid1[1].Split('-');
                        AID = aid2[0];

                        string M_F = "";
                        if (File.Exists(System.Configuration.ConfigurationSettings.AppSettings["ExcelPath"] + ddlbtejob.SelectedItem.Text + "\\" + txtbteisbn.Text.Replace("-", "") + "\\" + ddlbtestage.SelectedItem.Text + "\\" + txtbteisbn.Text.Replace("-", "") + "_Analysis_Report.xls"))
                        {
                            //M_F = Read_Excel(System.Configuration.ConfigurationSettings.AppSettings["ExcelPath"] + ddlbtejob.SelectedItem.Text + "\\" + txtbteisbn.Text.Replace("-", "") + "\\" + ddlbtestage.SelectedItem.Text + "\\" + txtbteisbn.Text.Replace("-", "") + "_Analysis_Report.xls", txtbteisbn.Text.Replace("-", ""), Convert.ToInt32(AID).ToString());
                        }
                        string mss = "";
                        string fig = "";
                        string[] mssfig;
                        if (M_F.Length != 0)
                        {
                            mssfig = M_F.Split('_');
                            mss = mssfig[0].ToString();
                            fig = mssfig[1].ToString();
                            if (mss.Length == 0 || mss == "")
                            {
                                mss = "0";
                            }
                            if (fig.Length == 0 || fig == "")
                            {
                                fig = "0";
                            }
                        }
                        else
                        {
                            mss = "0";
                            fig = "0";
                        }
						if (!PII.Contains(txtbteisbn.Text))
                        {
                            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                             "alert('PII and ISBN mismatch. Please check.....');" + System.Environment.NewLine +
                             "</script>");
                            return;
                        }
                        Insert_ChapterInfo(BookId, cnt, cno, PII, DOI, AID, Stage, "chp", "full-transfer", "", mss, fig);
                        cnt = cnt + 1;
                    }
                }
                if (objGlbCls.objData.getbookid(BookId,Stage) == false)
                {
                    Insert_BookInfo();
                }
                else
                {
                    btnbtecancel.Enabled = true;
                    Session["info"] = "Information for given bookid and stage is allready exists in the database. Click on the corresponding link button to perform the right action.";
                    Response.Redirect("Information.aspx");
                }
                btnbtecancel.Enabled = true;
                Generate_XmlOrder();
                ddlbtestage.SelectedIndex = 0;
				Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('Xml saved successfully!');" + System.Environment.NewLine +
                "</script>");
            }
            else
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('Please check, Either you are missing Language or JobType or Stage or VolumeTitle or VolumePII or ISBN or VolumeEditor or Chepters informations.......');" + System.Environment.NewLine +
                "</script>");
            }
        }
    private void Insert_BookInfo()
    {
        //try
       // {
            string PM_Name= txtPMName.Text;
            string PM_Email=txtPmemail.Text;
            string Design_Name= txttDesignName.Text ;

            string Spelling = DDlistSpelling.SelectedItem.Text;
            if (Spelling.Contains("Select"))
            {
                Spelling = "";
            }
			
            string O_Author_Name= DDListAName.SelectedItem.Text;
            if (O_Author_Name.Contains("Select"))
            {
                O_Author_Name = "";
            }
            string O_Affiliation = DDListAffilation.SelectedItem.Text;
            if (O_Affiliation.Contains("Select"))
            {
                O_Affiliation = "";
            }
            string O_Mini_TOC=DDListToc.SelectedItem.Text ;
            if (O_Mini_TOC.Contains("Select"))
            {
                O_Mini_TOC = "";
            }
            string Reference_Style = txtRefStyle.Text;

            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
			
            sqlstr = "insert into book_info(bid,stage,booktitle,pii,subtitle,voleditor,isbn,job_type,Creation_Date,Location,lang,COPYRIGHT_LINE,COPYRIGHT,COPYRIGHTTEXT,YEAR,IMPRINT,ISSN,JID,VOl_No,PARENTTITLE,Color,Trim_Size,edition,Pagination_Platform,PM_Name,PM_Email,Design_Name,Spelling,O_Author_Name,O_Affiliation,O_Mini_TOC,Reference_Style ) " 
                     + " values('" + BookId + "','" + Stage + "','" + Book_Title + "','" + Volume_Issue_PII + "','" + Book_Subtitle + "','" + Book_Editors + "','" + Formatted_ISBN + "','" + Job_Type + "',GETDATE(),'" + Session["location"].ToString() + "','" + Book_lang + "','" + copyright_line + "','" + copyright_type + "','" + copyright_owner + "','"
                     + copyright_year + "','" + imprint + "','" + txtISSN.Text + "','" + txtJID.Text + "','" + txtVol.Text + "','" + txtSeriesTit.Text + "','" + color + "','" + trim_size + "','" + edition + "','Indesign','" +                     
                      PM_Name + "','" + PM_Email+ "','" + Design_Name+ "','" +Spelling+ "','" +O_Author_Name+ "','" +O_Affiliation+ "','" +O_Mini_TOC+ "','" +Reference_Style + "')";
            cmd = new SqlCommand(sqlstr, con);
			
            cmd.ExecuteNonQuery();
			
            Session["info"] = "Information for given bookid and stage has been saved successfully in database. Click on the corresponding link button to perform the right action.";
            con.Close();
            cmd.Dispose();
            btnbtecancel.Enabled = true;
			
            Generate_XmlOrder();
            ddlbtestage.SelectedIndex = 0;
			Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('Xml saved successfully!');" + System.Environment.NewLine +
                "</script>");
            Response.Redirect("Information.aspx");
			
      //  }
      //  catch (Exception ex)
      //  {
			

           
      //  }
      //  finally
      //  {
      //      con.Close();
      //      cmd.Dispose();
      //  }
    }
    private void Insert_Copyright_info(string lang)
    {
        try{
            //string year = System.Configuration.ConfigurationSettings.AppSettings["CopyrightYear"]; 
            if (Book_lang == "Spanish")
            {
                copyright_line = "Copyright &copy; " + copyright_year + " Elsevier Espa&ntilde;a, S.L.";
                copyright_type = "full-transfer";
                copyright_owner = "Elsevier Espa&#241;a, S.L.";
                copyright_year = copyright_year;
            }
            if (Book_lang == "French")
            {
                copyright_line = "Copyright &copy; " + copyright_year + " Elsevier Masson SAS. Tous droits r&eacute;serv&eacute;s";
                copyright_type = "full-transfer";
                copyright_owner = "Elsevier Masson SAS";
                copyright_year = copyright_year;

            }
            if (Book_lang == "Italian")
            {
                copyright_line = "Copyright &copy; " + copyright_year + " Elsevier Srl - Tutti i diritti riservati";
                copyright_type = "full-transfer";
                copyright_owner = "Elsevier Srl";
                copyright_year = copyright_year;

            }
            if (Book_lang == "Portuguese")
            {
                copyright_line = "Copyright &copy; " + copyright_year + " Elsevier Editora Ltda.";
                copyright_type = "full-transfer";
                copyright_owner = "Elsevier Editora Ltda.";
                copyright_year = copyright_year;
            }
            if (Book_lang.ToLower().Trim() =="english")
            {

                if (OWNER.Trim().ToUpper() == "INC")
                {
                    copyright_line = "Copyright &copy; " + copyright_year + " Elsevier Inc.";
                    copyright_type = "full-transfer";
                    copyright_owner = "Elsevier Inc.";
                }
                else if (OWNER.Trim().ToUpper() == "LTD")
                {
                    copyright_line = "Copyright &copy; " + copyright_year  + " Elsevier Ltd.";
                    copyright_type = "full-transfer";
                    copyright_owner = "Elsevier Ltd.";
                }
                else if (OWNER.Trim().ToUpper() == "BV")
                {
                    copyright_line = "Copyright &copy; " + copyright_year + " Elsevier B.V.";
                    copyright_type = "full-transfer";
                    copyright_owner = "Elsevier B.V.";

                }
				else if (OWNER.Trim().ToUpper() == "IND")
                {
                    copyright_line = "Copyright &copy; " + copyright_year + " RLEX India Pvt. Ltd.";
                    copyright_type = "full-transfer";
                    copyright_owner = "RLEX India Pvt. Ltd.";

                }
                               
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void Insert_ChapterInfo(string bookid1, int cid1, string cno1, string pii1, string doi1, string aid1, string stage1, string dc, string copyright1, string copyrighttext1, string mss1, string fig1)
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "insert into chapter_info(bid,cid,cno,pii,doi,aid,Stage,DocSubtype,Copyright,CopyrightText,mss_page,from_page,to_page,figures,CHP_NO) values('" + bookid1 + "','" + cid1 + "','" + cno1.Replace("-", "") + "','" + pii1 + "','" + doi1 + "','" + aid1 + "','" + stage1 + "','" + dc + "','" + copyright1 + "','" + copyrighttext1 + "','" + mss1 + "','0','0','" + fig1 + "','---')";

            //StreamWriter sw = new StreamWriter("Jeet.txt", true);
            //sw.WriteLine(sqlstr);
            //sw.Close();

            cmd = new SqlCommand(sqlstr, con);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
           
        }
        finally
        {
            con.Close();
            cmd.Dispose();

        }
    }
    private void Generate_XmlOrder()
    {
        try
        {
            XmlDocument XMLDoc = new XmlDocument();
            XmlElement XMLorders;
            XmlElement XMLorder;
            XmlElement bookmeta;
            XmlElement ver_update;
            XmlElement bookpii;
            XmlElement bookdoi;
            XmlElement bookisbn;
            XmlElement bookissn;
            XmlElement bookjid;
            XmlElement bookvolno;
            XmlElement bookptitle;
            XmlElement bookvoledtr;
            XmlElement booktitle;
            XmlElement booksubtitle;
            XmlElement bookedition;
            //XmlAttribute edn1;
            XmlElement bookimprint;
            XmlElement bookversion;
            XmlElement bookstage;
            XmlElement pubyear;
            XmlElement regyear;
            XmlAttribute pyear;
            XmlAttribute ryear;
            XmlElement jobtype;
            XmlElement chapters;
            XmlElement chapterinfo;
            XmlElement cid;
            XmlElement cno;
            XmlElement cpii;
            XmlElement doi;
            XmlElement aid;
            XmlElement docsubtype;
            XmlAttribute chp1;
            XmlElement ctitle;
            XmlElement frompage;
            XmlElement topage;
            XmlElement copyright;
            //XmlElement cpyyear;
            XmlAttribute yr;
            XmlAttribute cptype;
            XmlElement recdate;
            XmlElement duedate;
            XmlElement pmemail;
            XmlElement pmname;
            XmlElement authorto;
            XmlElement authorcc;
            XmlAttribute rd;
            XmlAttribute rm;
            XmlAttribute ry;
            XmlAttribute dd;
            XmlAttribute dm;
            XmlAttribute dy;
            XmlElement msspage;
            XmlElement jtnno;
            XmlElement paginationplatform;

            XmlElement PM_Name;
            XmlElement PM_Email;
            XmlElement Design_Name;
            XmlElement Spelling;
            XmlElement O_Author_Name;
            XmlElement O_Affiliation;
            XmlElement O_Mini_TOC;
            XmlElement Reference_Style;
  

            int j = 0;
            XMLorders = XMLDoc.CreateElement("orders");
            XMLDoc.AppendChild(XMLorders);

            XMLorder = XMLDoc.CreateElement("order");
            XMLorders.AppendChild(XMLorder);

            bookmeta = XMLDoc.CreateElement("book-metadata");

            ver_update = XMLDoc.CreateElement("update");
            ver_update.InnerText = "0";
            bookmeta.AppendChild(ver_update);

            jtnno = XMLDoc.CreateElement("jtn");
            jtnno.InnerText = "";
            if (jtnno.InnerText.Length != 0)
            {
                bookmeta.AppendChild(jtnno);
            }

            if (Volume_Issue_PII.Length != 0)
            {
                bookpii = XMLDoc.CreateElement("pii");
                bookpii.InnerText = Volume_Issue_PII;
                bookmeta.AppendChild(bookpii);
            }

            bookdoi = XMLDoc.CreateElement("doi");
            bookdoi.InnerText = "";
            if (bookdoi.InnerText.Length != 0)
            {
                bookmeta.AppendChild(bookdoi);
            }
            if (Formatted_ISBN.Length != 0)
            {
                bookisbn = XMLDoc.CreateElement("isbn");
                bookisbn.InnerText = Formatted_ISBN;
                bookmeta.AppendChild(bookisbn);
            }
            if (Book_Editors.Length != 0)
            {
                bookvoledtr = XMLDoc.CreateElement("volume-editor");
                bookvoledtr.InnerText = Book_Editors;
                bookmeta.AppendChild(bookvoledtr);
            }
            if (Book_Title.Length != 0)
            {
                booktitle = XMLDoc.CreateElement("book-title");
                booktitle.InnerText = Book_Title;
                bookmeta.AppendChild(booktitle);
            }
            if (Book_Subtitle.Length != 0)
            {
                booksubtitle = XMLDoc.CreateElement("subtitle");
                booksubtitle.InnerText = Book_Subtitle;
                bookmeta.AppendChild(booksubtitle);
            }
            bookedition = XMLDoc.CreateElement("edition");
            bookedition.InnerText = "";
            if (bookedition.InnerText.Length != 0)
            {
                bookmeta.AppendChild(bookedition);
            }
            bookimprint = XMLDoc.CreateElement("imprint");
            bookimprint.InnerText = "";
            if (bookimprint.InnerText.Length != 0)
            {
                bookmeta.AppendChild(bookimprint);
            }
            bookversion = XMLDoc.CreateElement("version");
            bookversion.InnerText = "";
            if (bookversion.InnerText.Length != 0)
            {
                bookmeta.AppendChild(bookversion);
            }
            if (Stage.Length != 0)
            {
                bookstage = XMLDoc.CreateElement("stage");
                bookstage.InnerText = Stage;
                bookmeta.AppendChild(bookstage);
            }
            if (Job_Type.Length != 0)
            {
                jobtype = XMLDoc.CreateElement("jobType");
                jobtype.InnerText = Job_Type;
                bookmeta.AppendChild(jobtype);
            }
            recdate = XMLDoc.CreateElement("received-date");
            rd = XMLDoc.CreateAttribute("day");
            rd.InnerText = "";
            recdate.SetAttributeNode(rd);
            rm = XMLDoc.CreateAttribute("month");
            rm.InnerText = "";
            recdate.SetAttributeNode(rm);
            ry = XMLDoc.CreateAttribute("year");
            ry.InnerText = "";
            recdate.SetAttributeNode(ry);
            if (rd.InnerText.Length != 0 && rm.InnerText.Length != 0 && ry.InnerText.Length != 0)
            {
                bookmeta.AppendChild(recdate);
            }

            duedate = XMLDoc.CreateElement("due-date");
            dd = XMLDoc.CreateAttribute("day");
            dd.InnerText = "";
            duedate.SetAttributeNode(dd);
            dm = XMLDoc.CreateAttribute("month");
            dm.InnerText = "";
            duedate.SetAttributeNode(dm);
            dy = XMLDoc.CreateAttribute("year");
            dy.InnerText = "";
            duedate.SetAttributeNode(dy);
            if (dd.InnerText.Length != 0 && dm.InnerText.Length != 0 && dy.InnerText.Length != 0)
            {
                bookmeta.AppendChild(duedate);
            }
            paginationplatform = XMLDoc.CreateElement("pagination-platform");
            paginationplatform.InnerText = "";
            if (paginationplatform.InnerText.Length != 0)
            {
                bookmeta.AppendChild(paginationplatform);
            }
            pmemail = XMLDoc.CreateElement("pm-email");
            pmemail.InnerText = "";
            if (pmemail.InnerText.Length != 0)
            {
                bookmeta.AppendChild(pmemail);
            }
            pmname = XMLDoc.CreateElement("pm-name");
            pmname.InnerText = "";
            if (pmname.InnerText.Length != 0)
            {
                bookmeta.AppendChild(pmname);
            }
            authorto = XMLDoc.CreateElement("Author-to");
            authorto.InnerText = "";
            if (authorto.InnerText.Length != 0)
            {
                bookmeta.AppendChild(authorto);
            }
            authorcc = XMLDoc.CreateElement("Author-cc");
            authorcc.InnerText = "";
            if (authorcc.InnerText.Length != 0)
            {
                bookmeta.AppendChild(authorcc);
            }
            // new file written 
            PM_Name =  XMLDoc.CreateElement("elsevier-pm-name");
            PM_Name.InnerText = txtPMName.Text;
            if (PM_Name.InnerText.Length != 0)
            {
                bookmeta.AppendChild(PM_Name);
            }
            pmemail = XMLDoc.CreateElement("elsevier-pm-email");
            pmemail.InnerText =txtPmemail.Text;
            if (pmemail.InnerText.Length != 0)
            {
                bookmeta.AppendChild(pmemail);
            }

            Design_Name = XMLDoc.CreateElement("design-name");
            Design_Name.InnerText = txttDesignName.Text;
            if (Design_Name.InnerText.Length != 0)
            {
                bookmeta.AppendChild(Design_Name);
            }

            Spelling = XMLDoc.CreateElement("spelling");
            string val = DDlistSpelling.SelectedItem.Text;
            if(!(val.Contains("Select")))
            {
                Spelling.InnerText = val;
                 if (val.Length != 0)
                 {
                     bookmeta.AppendChild(Spelling);
                 }
            }
            O_Author_Name = XMLDoc.CreateElement("opener-author-name");
            string valauthname = DDListAName.SelectedItem.Text;
            if (!(valauthname.Contains("Select")))
            {
                O_Author_Name.InnerText = valauthname;
                if (valauthname.Length != 0)
                {
                    bookmeta.AppendChild(O_Author_Name);
                }
            }

            O_Affiliation = XMLDoc.CreateElement("opener-affiliation");
            string valAffiliation = DDListAffilation.SelectedItem.Text;
            if (!(valAffiliation.Contains("Select")))
            {
                O_Affiliation.InnerText = valAffiliation;
                if (valAffiliation.Length != 0)
                {
                    bookmeta.AppendChild(O_Affiliation);
                }
            }
            
            O_Mini_TOC = XMLDoc.CreateElement("opener-mini-toc");
            string valMini= DDListToc.SelectedItem.Text;
            if (!(valMini.Contains("Select")))
            {
                O_Mini_TOC.InnerText = valMini;
                if (valMini.Length != 0)
                {
                    bookmeta.AppendChild(O_Mini_TOC);
                }
            }
            Reference_Style = XMLDoc.CreateElement("reference-style");
            string valRef = txtRefStyle.Text;
            if (!(valRef.Contains("Select")))
            {
                Reference_Style.InnerText = valRef;
                if (valRef.Length != 0)
                {
                    bookmeta.AppendChild(Reference_Style);
                }
            }

            XMLorder.AppendChild(bookmeta);
            chapters = XMLDoc.CreateElement("chapters");            
            for (j = 0; j <= GridView1.Rows.Count - 1; j++)
            {
                chapterinfo = XMLDoc.CreateElement("chapter-info");

                cid = XMLDoc.CreateElement("cid");
                cid.InnerText = cnt1.ToString();
                chapterinfo.AppendChild(cid);
                cnt1 = cnt1 + 1;

                cno = XMLDoc.CreateElement("cno");
                string cnno = Convert.ToString(j + 1);

                if (Session["location"].ToString() == "NSEZ" && Job_Type == "S&T")
                {
                    //as per jitender
                    cnno = cnno + "0";
                    int a = Convert.ToInt32(cnno);
                    a = a / 2;
                    cnno = a.ToString();
                    if (cnno.Length == 1)
                    {
                        cnno = "c000" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 2)
                    {
                        cnno = "c00" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 3)
                    {
                        cnno = "c0" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 4)
                    {
                        cnno = "c" + cnno;
                        cno.InnerText = cnno;
                    }
                }
                else
                {
                    cnno = cnno + "0";
                    int a = Convert.ToInt32(cnno);
                    a = a / 2;
                    cnno = a.ToString();
                    if (cnno.Length == 1)
                    {
                        cnno = "c000" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 2)
                    {
                        cnno = "c00" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 3)
                    {
                        cnno = "c0" + cnno;
                        cno.InnerText = cnno;
                    }
                    else if (cnno.Length == 4)
                    {
                        cnno = "c" + cnno;
                        cno.InnerText = cnno;
                    }
                }

                chapterinfo.AppendChild(cno);

                cpii = XMLDoc.CreateElement("pii");
                cpii.InnerText = GridView1.Rows[j].Cells[1].Text;
                chapterinfo.AppendChild(cpii);

                doi = XMLDoc.CreateElement("doi");
                doi.InnerText = GridView1.Rows[j].Cells[2].Text;
                chapterinfo.AppendChild(doi);

                string[] aid3;
                string[] aid4;
                aid3 = GridView1.Rows[j].Cells[1].Text.Split('.');
                aid4 = aid3[1].Split('-');
                aid = XMLDoc.CreateElement("aid");
                aid.InnerText = aid4[0];
                chapterinfo.AppendChild(aid);

                docsubtype = XMLDoc.CreateElement("docsubtype");
                chp1 = XMLDoc.CreateAttribute("type");
                chp1.InnerText = "";
                docsubtype.SetAttributeNode(chp1);
                if (chp1.InnerText.Length != 0)
                {
                    chapterinfo.AppendChild(docsubtype);
                }
                ctitle = XMLDoc.CreateElement("title");
                ctitle.InnerText = "";
                if (ctitle.InnerText.Length != 0)
                {
                    chapterinfo.AppendChild(ctitle);
                }

                msspage = XMLDoc.CreateElement("mss-page");
                msspage.InnerText = "0";
                if (msspage.InnerText != "0")
                {
                    chapterinfo.AppendChild(msspage);
                }
                frompage = XMLDoc.CreateElement("from-page");
                frompage.InnerText = "0";
                if (frompage.InnerText != "0")
                {
                    chapterinfo.AppendChild(frompage);
                }
                topage = XMLDoc.CreateElement("to-page");
                topage.InnerText = "0";
                if (topage.InnerText != "0")
                {
                    chapterinfo.AppendChild(topage);
                }
                copyright = XMLDoc.CreateElement("copyright");
                cptype = XMLDoc.CreateAttribute("type");
                cptype.InnerText = "full-transfer";
                copyright.SetAttributeNode(cptype);
                yr = XMLDoc.CreateAttribute("year");
                yr.InnerText = "";
                copyright.SetAttributeNode(yr);
                copyright.InnerText = "";
                if (yr.InnerText.Length != 0 && copyright.InnerText.Length != 0)
                {
                    chapterinfo.AppendChild(copyright);
                }

                chapters.AppendChild(chapterinfo);
                XMLorder.AppendChild(chapters);
            }
            XMLDoc.Save("D:\\temp_order.xml");
            XMLDoc = null;
            //string DTDpath = System.Configuration.ConfigurationManager.AppSettings["dtdpath"].ToString();
            string DTDpath = Server.MapPath("~/App_Data/order.dtd");
            string Xslpath = Server.MapPath("~/App_Data/order.xsl");
            string OrderPath = "";
            if (Session["location"].ToString() == "NSEZ")
            {
                OrderPath = System.Configuration.ConfigurationManager.AppSettings["orderpath"].ToString();
            }
            else if (Session["location"].ToString() == "CHN")
            {
                OrderPath = System.Configuration.ConfigurationManager.AppSettings["orderpathConversion"].ToString();
            }
            string str, mainstr;
            str = "<?xml version=" + (char)34 + "1.0" + (char)34 + " encoding=" + (char)34 + "utf-8" + (char)34 + "?>" + "\r\n";
            str = str + "<!DOCTYPE orders SYSTEM " + (char)34 + DTDpath + (char)34 + ">";
            str = str + "<?xml-stylesheet type=" + (char)34 + "text/xsl" + (char)34 + " href=" + (char)34 + Xslpath + (char)34 + "?>";
            StreamReader sr = new StreamReader("D:\\temp_order.xml");
            mainstr = sr.ReadToEnd();
            mainstr = mainstr.Replace(" />", "/>");
            sr.Close();
            mainstr = str + "\r\n" + mainstr;
            Directory.CreateDirectory(OrderPath + "\\" + ddlbtejob.SelectedItem.Text + "\\" + Formatted_ISBN.Replace("-", "") + "\\" + ddlbtestage.SelectedItem.Text + "\\Current_Order\\");
            StreamWriter sw = new StreamWriter(OrderPath + "\\" + ddlbtejob.SelectedItem.Text + "\\" + Formatted_ISBN.Replace("-", "") + "\\" + ddlbtestage.SelectedItem.Text + "\\Current_Order\\" + BookId + ".xml");
            sw.Write(mainstr);
            sw.Close();
            if (File.Exists("D:\\temp_order.xml"))
            {
                File.Delete("D:\\temp_order.xml");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
           "</script>");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }
    protected void btnbtecancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Action.aspx");
    }
    protected void selectOrder(object sender, EventArgs e)
    {
        Formatted_ISBN = txtbteisbn.Text.Trim();

        if (Formatted_ISBN.Contains("-"))
        {
            Fill_Grid_onchange();
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Please enter formatted ISBN...');" + System.Environment.NewLine +
            "</script>");
            return;
        }
        
        
    }
}
