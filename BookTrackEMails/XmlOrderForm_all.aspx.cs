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
using System.Drawing;


public partial class XmlOrderForm : System.Web.UI.Page
{
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    GlbClasses objGlbCls = new GlbClasses();
    string orderpath;
    string[] isbn_temp;
    string isbn_text;
    string Current_Stage;
    string Current_Jobtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        if (Session["location"].ToString() == "NSEZ")
        {
           orderpath = System.Configuration.ConfigurationManager.AppSettings["orderpath"].ToString();
        }
        else if (Session["location"].ToString() == "CHN")
        {
            orderpath = System.Configuration.ConfigurationManager.AppSettings["orderpathConversion"].ToString();
        }

        if (IsPostBack == false)
        {
            Fill_Cmbbookid();
        }
        if (Session["BID"] != null)
        {
            ddlbookid.SelectedValue = Session["BID"].ToString();
            Open_Order(ddlbookid.SelectedValue);
            Session.Remove("BID");
            //Session.Remove("stg");
            //Session.Remove("JT");
        }
    }
    private void Fill_Cmbbookid()
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            string sqlstr;
            //sqlstr = "Select bid from Book_Info where location='" + Session["Location"].ToString() + "'";
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
            con.Close();
            cmd.Dispose();

        }
    }
    
    protected void lnkparse_Click(object sender, EventArgs e)
    {
        string Bookid = "";
        Session["bkid"] = ddlbookid.SelectedItem.Text;
        Session["stg"] = Current_Stage;
        if (ddlbookid.SelectedItem.Text != "<---Select Book ID--->")
        {
            string[] arr;
            Bookid = ddlbookid.SelectedItem.Text;
            arr = Bookid.Split('_');
            Current_Jobtype = arr[0];
            isbn_text = arr[1];
            Current_Stage = arr[2];
        }
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(orderpath + "\\" + Current_Jobtype + "\\" + isbn_text + "\\" + Current_Stage + "\\Current_Order\\" + Bookid + ".xml");
            Session["info"] = "Parsed successfully";
        }
        catch (XmlException ex)
        {
            //Session["msg"] = ex.Message;
            //Session["ln"] = ex.LineNumber;
            //Session["lp"] = ex.LinePosition;
            Session["info"] = "Parsing Error:-" + Session["msg"].ToString();
        }
        Response.Redirect("Information.aspx");
    }
    protected void lnldtd_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/App_Data/order.dtd");
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('File does not exist........');" + System.Environment.NewLine +
            "</script>");
        }        
    }
    protected void lnkxls_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/App_Data/order.xsl");
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "alert('File does not exist........');" + System.Environment.NewLine +
            "</script>");
        }        
    }
    protected void ddlbookid_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbookid.SelectedItem.Text != "<---Select Book ID--->")
        {
            Open_Order(ddlbookid.SelectedItem.Text);
        }
    }
    protected void lnkvieworder_Click(object sender, EventArgs e)
    {
        if (txtorder.Text.Length != 0)
        {
            Session["bid"] = ddlbookid.SelectedItem.Text;
            string[] arr= ddlbookid.SelectedItem.Text.Split('_');
            Session["stage"] = arr[0];
            Session["JT1"] = arr[2];
        }
        else
        {

        }
        Response.Redirect("Order_Viewer.aspx");

    }
    protected void lnkinsertmore_Click(object sender, EventArgs e)
    {
        Session.Remove("bid");
        Session.Remove("stage");
        Session.Remove("JT1");
        if (Session["location"].ToString() == "CHN")
        {
            //Response.Redirect("~/ConversionInput.aspx");
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
        }
        else
        {
            //Response.Redirect("~/Action.aspx");
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('You are not authorised user....');" + System.Environment.NewLine + "</script>");
        }  
    }
    protected void lnkbrowse_Click(object sender, EventArgs e)
    {
        string Bookid = "";
        if (txtorder.Text.Length > 0)
        {
            if (ddlbookid.SelectedItem.Text != "<---Select Book ID--->")
            {   
                string[] arr;
                Bookid = ddlbookid.SelectedItem.Text;
                arr = Bookid.Split('_');
                Current_Jobtype = arr[0];
                isbn_text = arr[1];
                Current_Stage = arr[2];
            }
            if (rdinternaloder.Checked == true)
            {
                Session["path"] = orderpath + "\\" + Current_Jobtype + "\\" + isbn_text + "\\" + Current_Stage + "\\Current_Order\\" + Bookid + ".xml";
            }
            else if (rdppmorder.Checked == true)
            {
                if (Directory.Exists(orderpath + "\\" + "PPM" + "\\" + isbn_text + "\\" + "TYPESET-ORDER\\Current_Order"))
                {
                    string[] PPM_Fls = null;
                    PPM_Fls = Directory.GetFiles(orderpath + "\\" + "PPM" + "\\" + isbn_text + "\\" + "TYPESET-ORDER\\Current_Order");

                    for (int i = 0; i < PPM_Fls.Length; i++)
                    {
                        string fname = Path.GetFileName(PPM_Fls[i]).ToLower();
                        if (fname.StartsWith("kup"))
                        {
                            Session["path"] = PPM_Fls[0].ToString();
                        }
                    }

                }
                else
                {
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +"alert('PPM Order does not exist........');" + System.Environment.NewLine +"</script>");
                }
            }
            else
            {
               // Session["path"] = "";
            }
            Response.Redirect("Browse.aspx");
            //openWIndow("Browse.aspx", "", Session["path"].ToString());
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine + "alert('XML Order does not exist........');" + System.Environment.NewLine +"</script>");
        }
    }
    private void openWIndow(String FileName, String WindowName, String qString)
    {
        String fileNQuery = FileName + "?value=" + qString;
        String script = @"<script language=""javascript"">" + "window.open(" + fileNQuery + WindowName + "," + "menubar=Yes,toolbar=No,resizable=Yes,scrollbars=Yes,status=yes" + " );" + "</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", script);
    }

    protected void lnkdownload_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtorder.Text.Length > 0)
            {
                if (ddlbookid.SelectedItem.Text != "<---Select Book ID--->")
                {
                    isbn_temp = ddlbookid.SelectedItem.Text.Split('_');
                    isbn_text = isbn_temp[1].ToString();
                }
                string path = "";
                if (rdinternaloder.Checked == true)
                {
                    path = orderpath + "\\"+isbn_temp[0] + "\\" + isbn_text + "\\" + isbn_temp[2] + "\\" + Current_Stage + "\\Current_Order\\" + ddlbookid.SelectedItem.Text + ".xml";
                }
                else if (rdppmorder.Checked == true)
                {
                    if (Directory.Exists(orderpath + "\\PPM\\" + isbn_text + "\\TYPESET-ORDER\\Current_Order"))
                    {
                        string[] PPM_Fls = null;
                        PPM_Fls = Directory.GetFiles(orderpath + "\\PPM\\" + isbn_text + "\\TYPESET-ORDER\\Current_Order");
                        if (PPM_Fls.Length > 0)
                        {
                            for (int i = 0; i < PPM_Fls.Length; i++)
                            {
                                string OrderFileName=Path.GetFileName(PPM_Fls[i]).ToLower();
                                if (OrderFileName.StartsWith("kup"))
                                {
                                    path = PPM_Fls[i].ToString();
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                          "alert('Order is not available on the server........');" + System.Environment.NewLine +
                         "</script>");
                        }

                    }
                    else
                    {
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                          "alert('Order is not available on the server........');" + System.Environment.NewLine +
                         "</script>");
                    }
                }
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                    "alert('File does not exist........');" + System.Environment.NewLine +
                    "</script>");
                }
            }
            else
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "alert('XML Order does not exist........');" + System.Environment.NewLine +
                "</script>");
            }
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void rdinternaloder_CheckedChanged(object sender, EventArgs e)
    {
        Open_Order(ddlbookid.SelectedItem.Text);
    }
    protected void rdppmorder_CheckedChanged(object sender, EventArgs e)
    {
        Open_PPM_Order(ddlbookid.SelectedItem.Text);
    }
    private void Open_Order(string Bookid)
    { string ttxt;
        if (Bookid != "<---Select Book ID--->")
        {
            string[] arr;
            arr = Bookid.Split('_');
            Current_Jobtype = arr[0];
            isbn_text = arr[1];
            Current_Stage = arr[2];

        }
        if (File.Exists(orderpath + "\\" + Current_Jobtype + "\\" + isbn_text + "\\" + Current_Stage + "\\Current_Order\\" + Bookid + ".xml"))
        {
            StreamReader sr = new StreamReader(orderpath + "\\" + Current_Jobtype + "\\" + isbn_text + "\\" + Current_Stage + "\\Current_Order\\" + Bookid + ".xml",System.Text.Encoding.Default);
            txtorder.Visible = true;
            lnkparse.Visible = true;
            lnkdownload.Visible = true;
            lnkbrowse.Visible = true;
			ttxt=sr.ReadToEnd();
            txtorder.Text = ttxt;
            sr.Close();
            rdinternaloder.Checked = true;
            rdppmorder.Checked = false;
            //cmdupdate.Enabled = true;
            lnkdtd.Visible = true;
            lnkxls.Visible = true;
            lbldtd.Visible = true;
        }
        else
        {
            txtorder.Text = txtorder.Text + "File Not Found";
            txtorder.ForeColor = Color.Red;
            lnkdtd.Visible = false;
            lnkxls.Visible = false;
            lbldtd.Visible = false;
        }
    }
    private void Open_PPM_Order(string Bookid)
    {string ttxt;
        if (Bookid != "<---Select Book ID--->")
        {
            string[] arr;
            arr = Bookid.Split('_');
            Current_Jobtype = arr[0];
            isbn_text = arr[1];
            Current_Stage = arr[2];
        }
        if (Directory.Exists(orderpath + "\\" + "PPM" + "\\" + isbn_text + "\\" + "TYPESET-ORDER\\Current_Order"))
        { 
            string[] PPM_Fls = null;
            PPM_Fls = Directory.GetFiles(orderpath + "\\" + "PPM" + "\\" + isbn_text + "\\" + "TYPESET-ORDER\\Current_Order");
            if (PPM_Fls.Length > 0)
            {
                for (int i = 0; i < PPM_Fls.Length; i++)
                {
                    string fname = Path.GetFileName(PPM_Fls[i]).ToLower();
                    if (fname.StartsWith("kup"))
                    {
                        StreamReader sr = new StreamReader(PPM_Fls[i].ToString(),System.Text.Encoding.Default);
                        txtorder.Visible = true;
                        lnkparse.Visible = true;
                        lnkdownload.Visible = true;
                        lnkbrowse.Visible = true;
						ttxt=sr.ReadToEnd();
                        txtorder.Text = ttxt;
                        sr.Close();
                        rdinternaloder.Checked = false;
                    }
                }
            }
            else
            {
                txtorder.Text = "PPM Order is not available in the sever..";
                txtorder.ForeColor = Color.Red;
            }
        }
        else
        {
            txtorder.Text = "PPM Order is not available in the sever..";
            txtorder.ForeColor = Color.Red;
        }
    }
}
