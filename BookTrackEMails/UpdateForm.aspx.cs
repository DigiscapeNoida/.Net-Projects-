using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data.OracleClient;



public partial class UpdateForm : System.Web.UI.Page
{
    //OleDbConnection con;
    //OleDbCommand cmd;
    //OleDbDataReader Dr;
    //OleDbDataAdapter da;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader Dr;
    SqlDataAdapter da;
    private int cid = 0;

    GlbClasses objGlbCls = new GlbClasses();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        cid = Convert.ToInt32(Request.QueryString["CID"].ToString());
        if (!IsPostBack)
        {
            BindControlvalues();
        }
        
    }
    private void BindControlvalues()
    {
        try
        {
            con = new SqlConnection(objGlbCls.objData.GetConnectionString());
            con.Open();
            cmd = new SqlCommand("select * from chapter_info where CID=" + cid, con);
            da = new SqlDataAdapter(cmd);
            con.Close();
            DataSet ds = new DataSet();
            da.Fill(ds);
            txtcid.Text = ds.Tables[0].Rows[0][0].ToString();
            txtcno.Text = ds.Tables[0].Rows[0][2].ToString();
            txtpii.Text = ds.Tables[0].Rows[0][3].ToString();
            txtdoi.Text = ds.Tables[0].Rows[0][4].ToString();
            txtfrompage.Text = ds.Tables[0].Rows[0][7].ToString();
            txttopage.Text = ds.Tables[0].Rows[0][8].ToString();
            txttitle.Text = ds.Tables[0].Rows[0][9].ToString();
            txtaid.Text = ds.Tables[0].Rows[0][13].ToString();
            txtmsspage.Text = ds.Tables[0].Rows[0][14].ToString();
            txtfigure.Text = ds.Tables[0].Rows[0][16].ToString();
            txtchapno.Text = ds.Tables[0].Rows[0][17].ToString();
        }
        catch(Exception ex)
        {
        }
        finally
        {

        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        con = new SqlConnection(objGlbCls.objData.GetConnectionString());
        con.Open();
        cmd = new SqlCommand("update chapter_info set CID='" + txtcid.Text + "',CNO='" + txtcno.Text + "',PII='" + txtpii.Text + "',DOI='" + txtdoi.Text + "',FROM_PAGE='" + txtfrompage.Text + "',TO_PAGE='" + txttopage.Text + "',TITLE='" + txttitle.Text + "',AID='" + txtaid.Text + "',MSS_PAGE='" + txtmsspage.Text + "',FIGURES='" + txtfigure.Text + "',CHP_NO='" + txtchapno.Text + "' where CID=" + cid, con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        if (result == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccess", "javascript:Showalert('" + txtcid.Text + "')", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Order_Viewer.aspx");
    }
}
