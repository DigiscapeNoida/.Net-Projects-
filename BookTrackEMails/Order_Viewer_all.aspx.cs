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
using Excel = Microsoft.Office.Interop.Excel;

public partial class Order_Viewer : System.Web.UI.Page
{
    private String lblChapVal = "";
    bool VTC = false;
    public static string SortField;
    public int Indx;
    string PIIIsbn;
    string PIIAid;
    string BID;
    int ClcNum;
    int[] WF = new int[15];
    int[] WFEX = new int[18];
    int i;
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    DataTable dt;
    DataView dv;
    GlbClasses objGlbCls = new GlbClasses();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbldate.Text = DateTime.Now.ToString();
        if (IsPostBack == false)
        {
            Fill_Cmbbookid();
            Read_Copyright();
            Read_CopyrightText();
            colorbind();
        }
        if (Session["bid"] != null && Session["stage"] != null && Session["JT1"] != null)
        {
            ddlbookid.SelectedValue = Session["bid"].ToString();
            Select_BookData(Session["bid"].ToString());
            Select_ChapterData(Session["bid"].ToString());  
            Session.Remove("bid");
            Session.Remove("stage");
            Session.Remove("JT1");
        }
    }
    private void colorbind()
    {
        ddlordercolor.Items.Insert(0, "1");
        ddlordercolor.Items.Insert(1, "2");
        ddlordercolor.Items.Insert(2, "3");
        ddlordercolor.Items.Insert(3, "4");
    }
    private void Fill_Cmbbookid()
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select bid from Book_Info where location='" + Session["location"].ToString() + "' order by Creation_Date desc";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            ddlbookid.Items.Clear();
            ddlbookid.Items.Add("<---Select Book ID--->");
            while (Dr.Read())
            {
                ddlbookid.Items.Add(Dr["bid"].ToString());
            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");
        }
        finally
        {
            if (con != null) con.Close();
            if (cmd != null) cmd.Dispose();
        }
    }
    private void Read_Copyright()
    {
        StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/CopyrightType.txt"));
        ddlordercopyrighttype.Items.Clear();
        while (sr.Peek() > -1)
        {
            ddlordercopyrighttype.Items.Add(sr.ReadLine().Trim());
        }
        sr.Close();
    }
    private void Read_CopyrightText()
    {
        StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/CopyrightText.txt"));
        ddlordercopyrightowner.Items.Clear();
        while (sr.Peek() > -1)
        {
            ddlordercopyrightowner.Items.Add(sr.ReadLine().Trim());
        }
        sr.Close();
    }
    protected void ddlbookid_SelectedIndexChanged(object sender, EventArgs e)
    {
        databind();
    }
    private void databind()
    {
        if (ddlbookid.SelectedItem.Text != "<---Select Book ID--->")
        {
            string[] arr;
            arr = ddlbookid.SelectedItem.Text.Split('_');
            BID = ddlbookid.SelectedItem.Text;
            Session["BID"] = BID;
            Select_BookData(BID);
            //ddlordercopyrighttype.SelectedValue = "---Select---";
            //ddlordercopyrighttype.SelectedValue = "---Select---";
            //txtordercopyrightyear.Text = "";
            Select_ChapterData(BID);
        }
    }
    private void Select_BookData(string bid)
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Select booktitle,pii,doi,subtitle,stage,voleditor,isbn,edition,imprint,job_type,Pagination_platform,Color,Trim_Size,lang,COPYRIGHT_LINE,COPYRIGHT,COPYRIGHTTEXT,YEAR from Book_Info where bid='" + bid + "'";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            if (Dr.HasRows == false)
            {
                txtorderisbn.Text = "";
                txtorderbooktitle.Text = "";
                txtorderbooksubtitle.Text = "";
                txtorderbookeditors.Text = "";
                txtorderjobtype.Text = "";
                txtorderlanguage.Text = "";
                //txtordercolor.Text = "";
                ddlordercolor.Text = "";
                txtorderpii.Text = "";
                txtorderdoi.Text = "";
                txtordertrimsize.Text = "";
                ddlordercopyrightowner.Text = "";
                ddlordercopyrighttype.Text = "";
                txtordercopyrightyear.Text = "";
                txtordercopyrightline.Text = "";
                txtorderimprint.Text = "";
                txtorderstage.Text = "";
                txtorderplatform.Text = "";
                txtoredredition.Text = "";
                //linksave.Enabled = false;
            }
            while (Dr.Read())
            {
                txtorderbooktitle.Text = Dr["booktitle"].ToString();

                txtorderpii.Text = Dr["pii"].ToString();
                if (Dr["pii"].ToString().Length != 0 && Dr["doi"].ToString().Length == 0)
                {
                    txtorderdoi.Text = "10.1016/" + txtorderpii.Text.Trim();
                }
                else
                {
                    txtorderdoi.Text = Dr["doi"].ToString();
                }

                txtorderbooksubtitle.Text = Dr["subtitle"].ToString();
                txtorderstage.Text = Dr["stage"].ToString();
                txtorderbookeditors.Text = Dr["voleditor"].ToString();
                txtorderisbn.Text = Dr["isbn"].ToString();
                txtoredredition.Text = Dr["edition"].ToString();
                txtorderimprint.Text = Dr["imprint"].ToString();
                txtorderjobtype.Text = Dr["job_type"].ToString();
                txtorderplatform.Text = Dr["Pagination_Platform"].ToString();
               
                txtordertrimsize.Text = Dr["Trim_Size"].ToString();
                txtorderlanguage.Text = Dr["lang"].ToString();
                txtordercopyrightline.Text = Dr["COPYRIGHT_LINE"].ToString();
                ddlordercopyrighttype.Text = Dr["COPYRIGHT"].ToString();
                ddlordercopyrightowner.Text = Dr["COPYRIGHTTEXT"].ToString();
                txtordercopyrightyear.Text = Dr["YEAR"].ToString();
                ddlordercolor.Text = Dr["Color"].ToString();
                //linksave.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");

        }
        finally
        {
            con.Close();
            cmd.Dispose();

        }
    }
    private void Select_ChapterData(string bid)
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("CID", typeof(string)));
            dt.Columns.Add(new DataColumn("CNO", typeof(string)));
            dt.Columns.Add(new DataColumn("PII", typeof(string)));
            dt.Columns.Add(new DataColumn("DOI", typeof(string)));
            dt.Columns.Add(new DataColumn("AID", typeof(string)));
            dt.Columns.Add(new DataColumn("DOCSUBTYPE", typeof(string)));
            dt.Columns.Add(new DataColumn("MSS_PAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("FROM_PAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TO_PAGE", typeof(string)));
            //dt.Columns.Add(new DataColumn("Figures", typeof(string)));
            dt.Columns.Add(new DataColumn("TITLE", typeof(string)));
            dt.Columns.Add(new DataColumn("Chp_No", typeof(string)));
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            //sqlstr = "Select CID,CNO,PII,DOI,AID,DOCSUBTYPE,COPYRIGHT,COPYRIGHTTEXT,MSS_PAGE,FROM_PAGE,TO_PAGE,Figures,TITLE,YEAR,CHP_NO from Chapter_Info where bid='" + cmbbookid.SelectedItem.Text + "' and Stage='" + cmbstage.SelectedItem.Text + "'";
            sqlstr = "Select CID,CNO,PII,DOI,AID,DOCSUBTYPE,COPYRIGHT,COPYRIGHTTEXT,MSS_PAGE,FROM_PAGE,TO_PAGE,TITLE,YEAR,CHP_NO from Chapter_Info where bid='" + bid + "'";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                DataRow drw = dt.NewRow();
                drw[0] = Dr["CID"].ToString();
                drw[1] = Dr["CNO"].ToString();
                drw[2] = Dr["PII"].ToString();
                drw[3] = Dr["DOI"].ToString();
                drw[4] = Dr["AID"].ToString();
                drw[5] = Dr["DOCSUBTYPE"].ToString();
                drw[6] = Dr["MSS_PAGE"].ToString();
                drw[7] = Dr["FROM_PAGE"].ToString();
                drw[8] = Dr["TO_PAGE"].ToString();
                drw[9] = Dr["TITLE"].ToString();
                drw[10] = Dr["CHP_NO"].ToString();
                dt.Rows.Add(drw);
            }
            dv = new DataView(dt);
            Session["table"] = dt;
            if (SortField == null)
            {
                SortField = "cno";
            }
            dv.Sort = SortField;

            gv.DataSource = dv;
            gv.DataBind();

        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");

        }
        finally
        {
            con.Close();
            cmd.Dispose();

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
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        Select_ChapterData(Session["BID"].ToString());
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int r;
        string cid;
        r = e.RowIndex;
        try
        {
            cid = gv.Rows[r].Cells[0].Text;
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "Delete from Chapter_Info where cid='" + cid + "'";
            cmd = new SqlCommand(sqlstr, con);
            int result= cmd.ExecuteNonQuery();
            if (result == 1)
            {
                lblbsg.Visible = true;
                lblbsg.Text = "Record deleted successfully";
            }
            gv.EditIndex = -1;
            con.Close();
            Select_ChapterData(Session["BID"].ToString());
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string cid;
            //r = e.RowIndex;    
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            int r = gvrow.RowIndex;
            cid = gv.Rows[r].Cells[0].Text;
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr = "Delete from Chapter_Info where cid='" + cid + "'";
            cmd = new SqlCommand(sqlstr, con);
            int result = cmd.ExecuteNonQuery();
            gv.EditIndex = -1;
            con.Close();
            if (result == 1)
            {
                Select_ChapterData(Session["BID"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Details deleted successfully')", true);
                lblbsg.Visible = true;
                lblbsg.Text = "Record deleted successfully";
            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ds = (DropDownList)e.Row.Cells[5].FindControl("ddldoctype");
        String test = e.Row.Cells[5].Text;
        String anotherTest = e.Row.Cells.Count.ToString();
        if (ds != null)
        {
            
            StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/DocSubtype.txt"));
            ds.Items.Clear();
            while (sr.Peek() > -1)
            {
                ds.Items.Add(sr.ReadLine().Trim());
            }
            ListItem lstItem = ds.Items.FindByText(lblChapVal);
            ds.SelectedIndex = ds.Items.IndexOf(lstItem);
            sr.Close();
        }
        Session["gridcount"] = Convert.ToString(gv.Rows.Count);
        
    }
    //protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        con = new OleDbConnection(objGlbCls.objData.GetConnectionString());
    //        con.Open();
    //        string sqlstr;
    //        string bookid1 = ddlbookid.SelectedItem.Text;
    //        string cid1 = get_cid().ToString();
    //        string cno1 = get_cno(ddlbookid.SelectedItem.Text);
    //        string aid1 = get_aid(ddlbookid.SelectedItem.Text);
    //        string pii1 = "";
    //        if (CheckExtendedIsbn(txtorderisbn.Text) == true)
    //        {
    //            pii1 = GeneratPII_Extended(txtorderisbn.Text, aid1);
    //        }
    //        else
    //        {
    //            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
    //            "alert('Wrong ISBN. Please check.....');" + System.Environment.NewLine +
    //            "</script>");
    //        }
    //        string doi1 = 10.1016 + "/" + pii1;
    //        string stage1 = cmbstage.SelectedItem.Text;
    //        string stage1 = txtorderstage.Text;
    //        string dc = "chp";
    //        string copyright1 = "";
    //        string copyrighttext1 = "";
    //        sqlstr = "insert into chapter_info(bid,cid,cno,pii,doi,aid,Stage,DocSubtype,Copyright,CopyrightText,mss_page,from_page,to_page,Figures,Chp_No) values('" + bookid1 + "','" + cid1 + "','" + cno1.Replace("-", "") + "','" + pii1 + "','" + doi1 + "','" + aid1 + "','" + stage1 + "','" + dc + "','" + copyright1 + "','" + copyrighttext1 + "','0','0','0','0','---')";
    //        cmd = new OleDbCommand(sqlstr, con);
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
    //        "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
    //        "</script>");

    //    }
    //    finally
    //    {
    //        con.Close();
    //        cmd.Dispose();
    //        Select_ChapterData(BID);

    //    }

    //}
    private void Check_VTC(string title)
    {
        if (title.Length != 0)
        {
            if (title.ToUpper() == title)
            {
                VTC = false;
                return;
            }
            if (title.ToLower() == title)
            {
                VTC = false;
                return;
            }
            char[] subword1 = title.ToCharArray();
            for (int k = 0; k <= subword1.Length - 1; k++)
            {
                if (k == 0)
                {
                    if (subword1[0].ToString().ToUpper() == subword1[0].ToString())
                    {
                        VTC = true;
                    }
                }
                else
                {
                    if (title.Replace(subword1[0].ToString(), "").ToLower() == title.Replace(subword1[0].ToString(), ""))
                    {
                        VTC = true;
                        break;
                    }
                    else
                    {
                        VTC = false;
                        break;
                    }
                }
            }
            if (VTC == true)
            {
                return;
            }
            StreamReader sr1 = new StreamReader(Server.MapPath("IgnoreCase.txt"));
            string str = sr1.ReadToEnd();
            //string[] arrs;
            //string[] sep ={ "\r\n" };
            //arrs = str.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            sr1.Close();
            string[] word = title.Split(' ');
            for (int i = 0; i <= word.Length - 1; i++)
            {
                if (str.IndexOf("#" + word[i].ToString() + "#") < 0)
                {
                    char[] subword = word[i].ToCharArray();
                    for (int j = 0; j <= subword.Length - 1; j++)
                    {
                        string wd = subword[j].ToString();
                        if (j == 0)
                        {
                            if (wd.ToUpper() == wd)
                            {
                                VTC = true;
                            }
                            else
                            {
                                VTC = false;
                                return;
                            }
                        }
                        else
                        {
                            if (wd.ToLower() == wd)
                            {
                                VTC = true;
                            }
                            else
                            {
                                VTC = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

    }
    private void Update_CPYear(string yr, string bid, string CPT, string CP)
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "update Chapter_Info set YEAR='" + yr + "',COPYRIGHTTEXT='" + CPT + "',COPYRIGHT='" + CP + "' where bid='" + bid + "'";
            cmd = new SqlCommand(sqlstr, con);
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }
        finally
        {
            con.Close();
        }
    }
    private int get_cid(string bookid)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        string sqlstr1 = "select max(cid) from chapter_info where bid='" + bookid + "'";
        cmd = new SqlCommand(sqlstr1, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        int cid = Convert.ToInt32(Dr[0].ToString());
        Dr.Close();
        con.Close();
        cmd.Dispose();
        return cid + 1;
    }
    private int get_cid_missing(string bookid,string aid)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        string sqlstr1 = "select cid from chapter_info where bid='" + bookid + "' and aid='" + aid + "'";
        cmd = new SqlCommand(sqlstr1, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        int cid = Convert.ToInt32(Dr[0].ToString());
        Dr.Close();
        con.Close();
        cmd.Dispose();
        return cid + 1;
    }
    private string get_cno(string bookid)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        string sqlstr1 = "select max(cno) from chapter_info where bid='" + bookid + "'";
        cmd = new SqlCommand(sqlstr1, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        string cno = Dr[0].ToString();
        string cn = cno;
        cn = cn.Replace("c", "");
        int intcno = 0;
        intcno = Convert.ToInt32(cn) + 5;
        cno = intcno.ToString();
        if (cno.Length == 2)
        {
            cno = "c00" + cno;
        }
        else if (cno.Length == 3)
        {
            cno = "c0" + cno;
        }
        else
        {
            cno = "c" + cno;
        }
        Dr.Close();
        con.Close();
        cmd.Dispose();
        return cno;
    }
    private string get_cno_missing(string bookid,string aid)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        string sqlstr1 = "select cno from chapter_info where bid='" + bookid + "' and aid='" + aid + "'";
        cmd = new SqlCommand(sqlstr1, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        string cno = Dr[0].ToString();
        string cn = cno;
        cn = cn.Replace("c", "");
        int intcno = 0;
        intcno = Convert.ToInt32(cn) + 5;
        cno = intcno.ToString();
        if (cno.Length == 2)
        {
            cno = "c00" + cno;
        }
        else if (cno.Length == 3)
        {
            cno = "c0" + cno;
        }
        else
        {
            cno = "c" + cno;
        }
        Dr.Close();
        con.Close();
        cmd.Dispose();
        return cno;
    }
    private string get_aid(string bookid)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        string sqlstr1 = "select max(aid) from chapter_info where bid='" + bookid + "'";
        cmd = new SqlCommand(sqlstr1, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        string aid = Dr[0].ToString();
        string id = aid;
        int intid = Convert.ToInt32(id) + 1;
        aid = intid.ToString();
        if (aid.Length == 1)
        {
            aid = "0000" + aid;
        }
        else if (aid.Length == 2)
        {
            aid = "000" + aid;
        }
        else if (aid.Length == 3)
        {
            aid = "00" + aid;
        }
        else if (aid.Length == 4)
        {
            aid = "0" + aid;
        }

        Dr.Close();
        con.Close();
        cmd.Dispose();
        return aid;
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
    protected void lnkvieworder_Click(object sender, EventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            Session["bkid"] = ddlbookid.SelectedItem.Text;
            Session["stg"] = txtorderstage.Text;
            Session["JT"] = txtorderjobtype.Text;
        }

        Response.Redirect("XmlOrderForm_all.aspx");
    }
    protected void lnkinsertmore_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
    }
    protected void lnkchpno_Click(object sender, EventArgs e)
    {
        
       Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +"alert('You are not authorised user....');" + System.Environment.NewLine +"</script>");
       
    }
    private bool CheckAidValueInGrid(string InsertAid)
    {
        try
        {
            int cnt = gv.Rows.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (InsertAid == gv.Rows[i].Cells[4].Text)
                {
                    return true; ;
                }
            }

        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");
        }
        return false;
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        Select_ChapterData(BID);
    }
    protected void btnsearchbyisbn_Click(object sender, ImageClickEventArgs e)
    {
        //ddlbookid.Enabled = false;
        if (txtsearchisbn.Text.Trim() == "")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
             "alert('Please Enter Isbn No');" + System.Environment.NewLine +
             "</script>");
        }
        else
        {
            string bid;
            if ((bid = objGlbCls.objData.CheckSearchIsbn(txtsearchisbn.Text.Trim())) != "")
            {
                BID = bid;
                ddlbookid.Text = BID;
                Session["BID"] = BID;
                Select_BookData(BID);
                //ddlordercopyrighttype.SelectedValue = "---Select---";
                //ddlordercopyrighttype.SelectedValue = "---Select---";
                //txtordercopyrightyear.Text = "";
                Select_ChapterData(BID);
            }
            else
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
             "alert('This isbn does not exists in the database');" + System.Environment.NewLine +
             "</script>");
            }

        }
    }
    protected void linkaddnewrecord_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
    }
    protected void linkmissingrecord_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
    }
    private bool checkindatabase(string aid)
    {
        try
        {
            string bookid = Session["BID"].ToString();
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "select aid from chapter_info where aid='" + aid + "' and bid='" + bookid + "'";
            cmd = new SqlCommand(sqlstr, con);
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (aid == Dr["aid"].ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        gv.EditIndex = (int)e.NewEditIndex;
        Label lblChp = (Label)gv.Rows[gv.EditIndex].Cells[5].FindControl("lbldoctype");
        if(lblChp!=null)
            if(lblChp.Text!="" && lblChp!=null)
                lblChapVal=lblChp.Text;
        
        Select_ChapterData(Session["BID"].ToString());
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int r;
            string cid;
            string cno, pii, doi, aid, docsubtype, mss_page, from_page, to_page, title, chapterno;
            r = e.RowIndex;
            cid = gv.Rows[r].Cells[0].Text;
            //cno = ((TextBox)gv.Rows[r].Cells[1].Controls[0]).Text;
            cno = gv.Rows[r].Cells[1].Text;
            pii = gv.Rows[r].Cells[2].Text;
            doi = gv.Rows[r].Cells[3].Text;
            aid = gv.Rows[r].Cells[4].Text;

            DropDownList ds1 = (DropDownList)gv.Rows[r].Cells[5].FindControl("ddldoctype");
            docsubtype = ds1.SelectedItem.Text;
            mss_page = ((TextBox)gv.Rows[r].Cells[6].Controls[0]).Text;
            from_page = ((TextBox)gv.Rows[r].Cells[7].Controls[0]).Text;
            to_page = ((TextBox)gv.Rows[r].Cells[8].Controls[0]).Text;
            if (IsNumeric(from_page) == true && IsNumeric(to_page) == true)
            {
                if (from_page.Length != 0 && to_page.Length != 0)
                {
                    if (Convert.ToInt32(from_page) > Convert.ToInt32(to_page))
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                        "alert('From-Page should always less then To-Page');" + System.Environment.NewLine +
                        "</script>");
                        return;
                    }
                }
            }
            title = ((TextBox)gv.Rows[r].Cells[9].Controls[0]).Text;
            chapterno = ((TextBox)gv.Rows[r].Cells[10].Controls[0]).Text;
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "update Chapter_Info set CNO='" + cno + "',DOCSUBTYPE='" + docsubtype + "',MSS_Page='" + mss_page + "',From_Page='" + from_page + "',TO_PAGE='" + to_page + "',TITLE='" + title + "',CHP_NO='" + chapterno + "' where cid='" + cid + "'";
            cmd = new SqlCommand(sqlstr, con);
            int result= cmd.ExecuteNonQuery();
            gv.EditIndex = -1;
            con.Close();
            //Update_CPYear(year, Session["BID"].ToString(), copyrighttext, copyright);
            if (result == 1)
            {
                Select_ChapterData(Session["BID"].ToString());
            }
            ds1.Enabled = false;
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
           "</script>");
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    protected void btnuploadexcel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
    }
    private string ExcelValidation( string Xlspath)
    {
        if (File.Exists(Xlspath))
        {
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Xlspath + ";Extended Properties=Excel 8.0";
            string SSQL = "SELECT * from [Sheet1$]";
            OleDbDataAdapter oleDA = new OleDbDataAdapter(SSQL, conn);
            OleDbConnection oleCon = new OleDbConnection(conn);
            String[] shName = GetSheetNames(oleCon);
            DataSet ds = new DataSet();
            oleDA.Fill(ds);
            int row_excel = ds.Tables[0].Rows.Count;
            int row_grid = gv.Rows.Count;
            if (row_excel== row_grid)
            {
                return "0";
            }
            else if(row_excel > row_grid)
            {
                return "1";
            }
            else if (row_excel < row_grid)
            {
                return "2";
            }
            else
            {
                return "4";
            }
        }
        else
        {
            return "3";
        }   
    }
    private List<String> importData()
    {
        int iCount = gv.Rows.Count;
        List<String> lstAID = new List<String>();
        for (int i = 0; i < iCount; i++)
        {
            if (!lstAID.Contains(gv.Rows[i].Cells[4].Text)) ;
            lstAID.Add(gv.Rows[i].Cells[4].Text);
        }
        return lstAID;

    }
    private String[] GetSheetNames(OleDbConnection oleCon)
    {
        String[] strSheetArr = null;

        if (oleCon.State == ConnectionState.Closed)
        {
            oleCon.Open();
        }
        DataTable dt = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        strSheetArr = new String[dt.Rows.Count];
        for (i = 0; i < dt.Rows.Count; i++)
        {
            strSheetArr[i] = dt.Rows[i]["TABLE_NAME"].ToString();
        }
        return strSheetArr;
    }
    public bool importExcelSheet(string filepath)
    {
        if (filepath != "")
        {
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
            //string SSQL = "SELECT FirstName, LastName, Emailid,PhoneNo from [Sheet1$]";
            string SSQL = "SELECT * from [Sheet1$]";
            OleDbDataAdapter oleDA = new OleDbDataAdapter(SSQL, conn);
            OleDbConnection oleCon = new OleDbConnection(conn);
            String[] shName = GetSheetNames(oleCon);
            DataSet ds = new DataSet();
            oleDA.Fill(ds);
            List<String> lstGVAid = importData();
            oleCon.Close();
            for (int i = 0; i <= gv.Rows.Count - 1; i++)
            {
                string abc = gv.Rows[i].Cells[4].Text;
                DataRow[] results = ds.Tables[0].Select("AID =" + abc);
                gv.Rows[i].Cells[6].Text = results[0].ItemArray[1].ToString();
                gv.Rows[i].Cells[7].Text = results[0].ItemArray[2].ToString();
                gv.Rows[i].Cells[8].Text = results[0].ItemArray[3].ToString();
                gv.Rows[i].Cells[9].Text = results[0].ItemArray[4].ToString();
                gv.Rows[i].Cells[10].Text = results[0].ItemArray[5].ToString();
                gv.Rows[i].Cells[5].Text = results[0].ItemArray[6].ToString();
                UpdateGridviewAfterExcel(abc, gv.Rows[i].Cells[5].Text, gv.Rows[i].Cells[6].Text, gv.Rows[i].Cells[7].Text, gv.Rows[i].Cells[8].Text, gv.Rows[i].Cells[9].Text, gv.Rows[i].Cells[10].Text);
            }
            return true;
        }
        else
        {
            //lblbsg.Visible = true;
            //lblbsg.Text = "Please select only excel sheet";
            return false;
        }
    }
    public void UpdateGridviewAfterExcel(string aid, string doc,string mss, string from, string to, string title, string chpno)
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            sqlstr = "update Chapter_Info set docsubtype='" + doc + "', mss_page='" + mss + "',from_page='" + from + "',to_page='" + to + "',title='" + title + "',CHP_NO='" + chpno + "' where aid='" + aid + "'";
            cmd = new SqlCommand(sqlstr, con);
            cmd.ExecuteNonQuery();
            gv.EditIndex = -1;
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
    }
    protected void linksave_Click(object sender, EventArgs e)
    {

        if (ddlordercopyrightowner.SelectedItem.Text == "---Select--" || ddlordercopyrighttype.SelectedItem.Text == "---Select--" || txtordercopyrightyear.Text.Length == 0)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Please first fill copyright text/Year at book level.... ');" + System.Environment.NewLine +
            "</script>");
            return;
        }
        if (txtoredredition.Text == "0" || txtoredredition.Text == "")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Edition should not be 0. It should be more than 0 in case of complete order');" + System.Environment.NewLine +
            "</script>");
            return;
        }
        if (IsNumeric(txtordercopyrightyear.Text) == true)
        {
            if (txtordercopyrightyear.Text.Length != 0)
            {
                if (Convert.ToInt32(txtordercopyrightyear.Text) > 2017 || Convert.ToInt32(txtordercopyrightyear.Text) < 1900)
                {
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                    "alert('Year should be a valid year.');" + System.Environment.NewLine +
                    "</script>");
                    return;
                }
            }
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('Year should be a valid year.');" + System.Environment.NewLine +
            "</script>");
            return;
        }
        string duplicate = "";
        string oldprocess = "";
        for (int p = 0; p <= gv.Rows.Count - 1; p++)
        {
            oldprocess = oldprocess + gv.Rows[p].Cells[10].Text;
        }
        oldprocess = oldprocess.Replace("---", "");
        if (oldprocess.Length != 0)
        {
            for (int t = 0; t <= gv.Rows.Count - 1; t++)
            {
                DropDownList dsn = (DropDownList)gv.Rows[t].Cells[5].FindControl("ddldoctype");

                duplicate = duplicate + " " + gv.Rows[t].Cells[10].Text;
            }
            duplicate = duplicate.Replace("---", "").Trim();
            duplicate = duplicate.Replace("  ", " ").Trim();
            duplicate = duplicate.Replace("  ", " ").Trim();
            duplicate = duplicate.Replace("  ", " ").Trim();
            duplicate = duplicate.Replace("  ", " ").Trim();
            duplicate = duplicate.Replace("  ", " ").Trim();
            string[] duplarr = duplicate.Split(' ');
            for (int t = 0; t <= duplarr.Length - 1; t++)
            {
                int findx = 0;
                int lindx = 0;
                findx = duplicate.IndexOf(" " + duplarr[t] + " ");
                lindx = duplicate.LastIndexOf(" " + duplarr[t] + " ");
                if (findx != -1 && lindx != -1)
                {
                    if (findx != lindx)
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                        "alert('Duplicate Chapter No, Please check...');" + System.Environment.NewLine +
                        "</script>");
                        // return;
                    }
                }
            }
        }

        try
        {
            string year = System.Configuration.ConfigurationSettings.AppSettings["CopyrightYear"]; 
            string changeyear = txtordercopyrightline.Text;
            string[] arr = changeyear.Split(' ');
            string finalarr="";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == year)
                {
                    arr[i] = txtordercopyrightyear.Text;
                }
                finalarr = finalarr+" "+arr[i];
            }

            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr = "";


            string BOOK_TITLE = txtorderbooktitle.Text;
            string BOOK_SUBTITLE = txtorderbooksubtitle.Text;
            if(BOOK_TITLE.Contains("'"))
            {
                BOOK_TITLE=BOOK_TITLE.Replace("'", "''");
            }
            if (BOOK_SUBTITLE.Contains("'"))
            {
                BOOK_SUBTITLE = BOOK_SUBTITLE.Replace("'", "''");
            }

            sqlstr = "update Book_Info set booktitle='" + BOOK_TITLE + "',subtitle='" + BOOK_SUBTITLE + "',pii='" + txtorderpii.Text + "',doi='" + txtorderdoi.Text + "',stage='" + txtorderstage.Text + "',voleditor = '" + txtorderbookeditors.Text + "',ISBN='" + txtorderisbn.Text + "',edition='" + txtoredredition.Text + "',imprint='" + txtorderimprint.Text + "',Job_type='" + txtorderjobtype.Text + "',Pagination_Platform='" + txtorderplatform.Text + "',Color='" + ddlordercolor.Text + "',Trim_Size='" + txtordertrimsize.Text + "',LANG='" + txtorderlanguage.Text + "',COPYRIGHT_LINE='" + finalarr.Trim() + "',COPYRIGHT='" + ddlordercopyrighttype.Text + "',COPYRIGHTTEXT='" + ddlordercopyrightowner.Text + "',YEAR='" + txtordercopyrightyear.Text + "' where bid='" + Session["BID"].ToString() + "' ";
            cmd = new SqlCommand(sqlstr, con);
            cmd.ExecuteNonQuery();
            gv.EditIndex = -1;
            Generate_XmlOrder();
            Session["bkid"] = Session["BID"].ToString();
            Session["stg"] = txtorderstage.Text;
            Session["JT"] = txtorderjobtype.Text;
            Session["info"] = "Information for given bookid and stage has been updated successfully in database and XML Order. Click on the corresponding link button to perform the right action.";
            Response.Redirect("Information.aspx");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    private void Generate_XmlOrder()
    {
        try{
        XmlDocument XMLDoc = new XmlDocument();
        XmlElement XMLorders;
        XmlElement XMLorder;
        XmlElement bookmeta;
        XmlElement bookpii;
        XmlElement bookdoi;
        XmlElement bookisbn;
        XmlElement bookvoledtr;
        XmlElement booktitle;
        XmlElement booksubtitle;
        XmlElement bookedition;
        XmlElement bookcolor;
        XmlElement booktrimsize;
        XmlElement bookcopyrightline;
        XmlElement booklanguage;
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
        XmlAttribute yr;
        XmlAttribute cptype;
        XmlElement msspage;
        XmlElement paginationplatform;
        XmlElement clabel;
        XMLorders = XMLDoc.CreateElement("orders");
        XMLDoc.AppendChild(XMLorders);

        XMLorder = XMLDoc.CreateElement("order");
        XMLorders.AppendChild(XMLorder);

        bookmeta = XMLDoc.CreateElement("book-metadata");
      
        if (txtorderpii.Text.Length != 0)
        {
            bookpii = XMLDoc.CreateElement("pii");
            bookpii.InnerText = txtorderpii.Text;
            bookmeta.AppendChild(bookpii);
        }
        if (txtorderdoi.Text.Length != 0)
        {
            bookdoi = XMLDoc.CreateElement("doi");
            bookdoi.InnerText = txtorderdoi.Text;
            bookmeta.AppendChild(bookdoi);
        }
        if (txtorderisbn.Text.Length != 0)
        {
            bookisbn = XMLDoc.CreateElement("isbn");
            bookisbn.InnerText = txtorderisbn.Text;
            bookmeta.AppendChild(bookisbn);
        }
        if (txtorderbookeditors.Text.Length != 0)
        {
            bookvoledtr = XMLDoc.CreateElement("volume-editor");
            bookvoledtr.InnerText = txtorderbookeditors.Text;
            bookmeta.AppendChild(bookvoledtr);
        }
        if (txtorderbooktitle.Text.Length != 0)
        {
            booktitle = XMLDoc.CreateElement("book-title");
            booktitle.InnerText = txtorderbooktitle.Text;
            bookmeta.AppendChild(booktitle);
        }
        if (txtorderbooksubtitle.Text.Length != 0)
        {
            booksubtitle = XMLDoc.CreateElement("subtitle");
            booksubtitle.InnerText = txtorderbooksubtitle.Text;
            bookmeta.AppendChild(booksubtitle);
        }
        if (txtoredredition.Text.Length != 0)
        {
            bookedition = XMLDoc.CreateElement("edition");
            bookedition.InnerText = txtoredredition.Text;
            bookmeta.AppendChild(bookedition);
        }
        if (txtorderlanguage.Text.Length != 0)
        {
            booklanguage = XMLDoc.CreateElement("lang");
            booklanguage.InnerText = txtorderlanguage.Text;
            bookmeta.AppendChild(booklanguage);
        }
        if (txtorderimprint.Text.Length != 0)
        {
            bookimprint = XMLDoc.CreateElement("imprint");
            bookimprint.InnerText = txtorderimprint.Text;
            bookmeta.AppendChild(bookimprint);
        }
        if (txtorderstage.Text.Length != 0)
        {
            bookstage = XMLDoc.CreateElement("stage");
            bookstage.InnerText = txtorderstage.Text;
            bookmeta.AppendChild(bookstage);
        }
        if (txtorderjobtype.Text.Length != 0)
        {
            jobtype = XMLDoc.CreateElement("jobType");
            jobtype.InnerText = txtorderjobtype.Text;
            bookmeta.AppendChild(jobtype);
        }
        if (txtorderplatform.Text.Length != 0)
        {
            paginationplatform = XMLDoc.CreateElement("pagination-platform");
            paginationplatform.InnerText = txtorderplatform.Text;
            bookmeta.AppendChild(paginationplatform);
        }
        if (ddlordercolor.Text.Length != 0)
        {
            bookcolor = XMLDoc.CreateElement("color");
            bookcolor.InnerText = ddlordercolor.Text;
            bookmeta.AppendChild(bookcolor);
        }
        if (txtordertrimsize.Text.Length != 0)
        {
            booktrimsize = XMLDoc.CreateElement("Trim-Size");
            booktrimsize.InnerText = txtordertrimsize.Text;
            bookmeta.AppendChild(booktrimsize);
        }
        if (txtordercopyrightline.Text.Length != 0)
        {
            bookcopyrightline = XMLDoc.CreateElement("copyrightline");
            bookcopyrightline.InnerText = txtordercopyrightline.Text;
            bookmeta.AppendChild(bookcopyrightline);
        }
        copyright = XMLDoc.CreateElement("copyright");
        cptype = XMLDoc.CreateAttribute("type");
        if (ddlordercopyrighttype.SelectedItem.Text == "---Select---")
        {
            cptype.InnerText = "";
        }
        else
        {
            cptype.InnerText = ddlordercopyrighttype.SelectedItem.Text;
        }

        copyright.SetAttributeNode(cptype);
        yr = XMLDoc.CreateAttribute("year");
        yr.InnerText = txtordercopyrightyear.Text;
        copyright.SetAttributeNode(yr);
        if (ddlordercopyrightowner.SelectedItem.Text == "---Select---")
        {
            copyright.InnerText = "";
        }
        else
        {
            copyright.InnerText = ddlordercopyrightowner.SelectedItem.Text;
        }
        if (txtordercopyrightyear.Text.Length != 0 && txtordercopyrightyear.Text != "&nbsp;" && copyright.InnerText.Length != 0 && copyright.InnerText.Length != 0)
        {
            bookmeta.AppendChild(copyright);
        }

        XMLorder.AppendChild(bookmeta);
        //int X;
        chapters = XMLDoc.CreateElement("chapters");
        //XMLorder.AppendChild(chapters);
        //DecVar.globcount = gv.Rows.Count;
        for (int X = 0; X <= gv.Rows.Count - 1; X++)
        {

                chapterinfo = XMLDoc.CreateElement("chapter-info");
                cid = XMLDoc.CreateElement("cid");
                cid.InnerText = gv.Rows[X].Cells[0].Text;
                chapterinfo.AppendChild(cid);

                cno = XMLDoc.CreateElement("cno");
                cno.InnerText = gv.Rows[X].Cells[1].Text;
                chapterinfo.AppendChild(cno);

                if (gv.Rows[X].Cells[10].Text != "---")
                {
                    clabel = XMLDoc.CreateElement("clabel");
                    clabel.InnerText = gv.Rows[X].Cells[10].Text;
                    chapterinfo.AppendChild(clabel);
                }

                cpii = XMLDoc.CreateElement("pii");
                cpii.InnerText = gv.Rows[X].Cells[2].Text;
                chapterinfo.AppendChild(cpii);

                doi = XMLDoc.CreateElement("doi");
                doi.InnerText = gv.Rows[X].Cells[3].Text;
                chapterinfo.AppendChild(doi);

                aid = XMLDoc.CreateElement("aid");
                aid.InnerText = gv.Rows[X].Cells[4].Text;
                chapterinfo.AppendChild(aid);

                docsubtype = XMLDoc.CreateElement("docsubtype");
                chp1 = XMLDoc.CreateAttribute("type");
                Label ds2 = (Label)gv.Rows[X].Cells[5].FindControl("lbldoctype");
                if (ds2.Text == "---")
                {
                    chp1.InnerText = "";
                }
                else
                {
                    chp1.InnerText = ds2.Text;
                }
                docsubtype.SetAttributeNode(chp1);
                if (chp1.InnerText.Length != 0)
                {
                    chapterinfo.AppendChild(docsubtype);
                }
                
                if (gv.Rows[X].Cells[9].Text.Length != 0 && gv.Rows[X].Cells[9].Text != "&nbsp;")
                {
                    ctitle = XMLDoc.CreateElement("title");
                    ctitle.InnerText = gv.Rows[X].Cells[9].Text;
                    chapterinfo.AppendChild(ctitle);
                }
                if (gv.Rows[X].Cells[6].Text != "0" && gv.Rows[X].Cells[6].Text != "&nbsp;")
                {
                    msspage = XMLDoc.CreateElement("mss-page");
                    msspage.InnerText = gv.Rows[X].Cells[6].Text;
                    chapterinfo.AppendChild(msspage);

                }
                if (gv.Rows[X].Cells[7].Text != "0" && gv.Rows[X].Cells[7].Text != "&nbsp;")
                {
                    frompage = XMLDoc.CreateElement("from-page");
                    frompage.InnerText = gv.Rows[X].Cells[7].Text;
                    chapterinfo.AppendChild(frompage);
                }
                if (gv.Rows[X].Cells[8].Text != "0" && gv.Rows[X].Cells[8].Text != "&nbsp;")
                {
                    topage = XMLDoc.CreateElement("to-page");
                    topage.InnerText = gv.Rows[X].Cells[8].Text;
                    chapterinfo.AppendChild(topage);
                }
               
                //copyright = XMLDoc.CreateElement("copyright");
                //cptype = XMLDoc.CreateAttribute("type");
                //if (ddlordercopyrighttype.SelectedItem.Text == "---Select---")
                //{
                //    cptype.InnerText = "";
                //}
                //else
                //{
                //    cptype.InnerText = ddlordercopyrighttype.SelectedItem.Text;
                //}

                //copyright.SetAttributeNode(cptype);
                //yr = XMLDoc.CreateAttribute("year");
                //yr.InnerText = txtordercopyrightyear.Text;
                //copyright.SetAttributeNode(yr);
                //if (ddlordercopyrightowner.SelectedItem.Text == "---Select---")
                //{
                //    copyright.InnerText = "";
                //}
                //else
                //{
                //    copyright.InnerText = ddlordercopyrightowner.SelectedItem.Text;
                //}
                //if (txtordercopyrightyear.Text.Length != 0 && txtordercopyrightyear.Text != "&nbsp;" && copyright.InnerText.Length != 0 && copyright.InnerText.Length != 0)
                //{
                //    chapterinfo.AppendChild(copyright);
                //}
                chapters.AppendChild(chapterinfo);
                XMLorder.AppendChild(chapters);

            }
            XMLDoc.Save("C:\\temp_order.xml");
            XMLDoc = null;
            string DTDpath = Server.MapPath("~/App_Data/order.dtd");
            string Xslpath = Server.MapPath("~/App_Data/order.xsl");
            string OrderPath = "";
            if (Session["Location"].ToString() == "NSEZ")
            {
                OrderPath = System.Configuration.ConfigurationManager.AppSettings["orderpath"].ToString();
            }
            else if (Session["Location"].ToString() == "CHN")
            {
                OrderPath = System.Configuration.ConfigurationManager.AppSettings["orderpathConversion"].ToString();
            }
            string str, mainstr;
            str = "<?xml version=" + (char)34 + "1.0" + (char)34 + " encoding=" + (char)34 + "utf-8" + (char)34 + "?>" + "\r\n";
            str = str + "<!DOCTYPE orders SYSTEM " + (char)34 + DTDpath + (char)34 + ">";
            str = str + "<?xml-stylesheet type=" + (char)34 + "text/xsl" + (char)34 + " href=" + (char)34 + Xslpath + (char)34 + "?>";
            StreamReader sr = new StreamReader("C:\\temp_order.xml");
            mainstr = sr.ReadToEnd();
            mainstr = mainstr.Replace(" />", "/>");
            sr.Close();
            mainstr = str + "\r\n" + mainstr;

            if (File.Exists(OrderPath + "\\" + txtorderjobtype.Text + "\\" + txtorderisbn.Text.Replace("-", "") + "\\" + txtorderstage.Text + "\\Current_Order\\" + Session["BID"].ToString() + ".xml"))
            {
                string[] arr;
                int i;
                arr = Directory.GetFiles(OrderPath + "\\" + txtorderjobtype.Text + "\\" + txtorderisbn.Text.Replace("-", "") + "\\" + txtorderstage.Text + "\\");
                i = arr.Length;
                i = i + 1;
                File.Copy(OrderPath + "\\" + txtorderjobtype.Text + "\\" + txtorderisbn.Text.Replace("-", "") + "\\" + txtorderstage.Text + "\\Current_Order\\" + Session["BID"].ToString() + ".xml", OrderPath + "\\"+txtorderjobtype.Text + "\\" + txtorderisbn.Text.Replace("-", "") + "\\" + txtorderstage.Text + "\\" + Session["BID"].ToString() + "_" + i + ".xml", true);
                StreamWriter sw = new StreamWriter(OrderPath + "\\" + txtorderjobtype.Text + "\\" + txtorderisbn.Text.Replace("-", "") + "\\" + txtorderstage.Text + "\\Current_Order\\" + Session["BID"].ToString() + ".xml");
                mainstr = mainstr.Replace("&amp;nbsp;", "");
                //mainstr = mainstr.Replace("&amp;", "&");
                sw.Write(mainstr);
                sw.Close();
            }
            //------------------------   
            if (File.Exists("C:\\temp_order.xml"))
            {
                File.Delete("C:\\temp_order.xml");
            }
        
    }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('" + ex.Message.ToString() + "');" + System.Environment.NewLine +
            "</script>");
        }
    }


    protected void txtorderstage_TextChanged(object sender, EventArgs e)
    {

    }
}

