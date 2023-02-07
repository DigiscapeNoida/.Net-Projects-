using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using TD.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Resources;

public partial class AdminTDUserCreate : System.Web.UI.Page
{
    private ResourceManager rm;
    private bool chkedit = false;
    CultureInfo ci;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[SESSION.LOGGED_USER] != null)
        {
            chkedit = false;
            lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
            if (!IsPostBack)
            {
                bindproduct();
                Binduser();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }
    protected void Page_PreRender(object sender, System.EventArgs e)
    {


        if (Session[SESSION.LOGGED_USER] != null && chkedit == false)
        {

            Binduser();

        }


    }
    private void bindproduct()
    {
        ddlprodsite.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getproduct, paramlist);
        ddlprodsite.DataSource = ds;
        ddlprodsite.DataTextField = "proddesc";
        ddlprodsite.DataValueField = "prodid";
        ddlprodsite.DataBind();
        // ddlProduct.Items.Insert(0, new ListItem("-Select-", "-1"));
    }
    private void Binduser()
    {
        SqlParameter[] paramlist = new SqlParameter[1];
         if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
         {
             paramlist[0] = new SqlParameter("roleid", "LN");
         }
         else if (Session[SESSION.LOGGED_ROLE].ToString() == "TAD")
         {
             paramlist[0] = new SqlParameter("roleid", "TDM");
         }
         else
         {
             paramlist[0] = new SqlParameter("roleid", "");
         }

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.usp_getUser, paramlist);
        grdUser.DataSource = ds;
        grdUser.DataBind();
    }
    protected void btnUserCreate_Click(object sender, EventArgs e)
    {

      //  CultureInfo ci;
        string lang = "";
        if (Session["lang"] != null)
        {
            lang = Session["lang"].ToString();
        }
        else
        {
            //lang = "en-US";
            Session["lang"] = "fr-FR";
            lang = "fr-FR";
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        
        // check user exist
        SqlParameter[] paramListchk = new SqlParameter[1];
        paramListchk[0] = new SqlParameter("userid", txtuserid.Text.Trim());
        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.chkuserExist, paramListchk);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //lblmessgae.Text = "User Already Exist!";
           string msg = rm.GetString("User_already_exist", ci);
           Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + msg + "')", true);
        }
        else
        {

            // create user



            SqlParameter[] paramList = new SqlParameter[5];
            paramList[0] = new SqlParameter("userid", txtuserid.Text.Trim());
            paramList[1] = new SqlParameter("@fname", txtfname.Text.Trim());
            paramList[2] = new SqlParameter("@lname", txtlname.Text.Trim());
            paramList[3] = new SqlParameter("@roleid", "TDM");//ddlrole.SelectedValue.ToString()
            paramList[4] = new SqlParameter("@prodid", ddlprodsite.SelectedValue.ToString());

            int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.createUser, paramList);
            if (rowAffected > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + rm.GetString("User_created_successfully", ci) + "')", true);
                // lblmessgae.Text = rm.GetString("User_created_successfully", ci);

            }


            txtfname.Text = "";
            txtlname.Text = "";
            txtuserid.Text = "";
            ddlprodsite.SelectedIndex = -1;
            ddlrole.SelectedIndex = -1;
            Binduser();
        }

    }

    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
        txtfname.Text = "";
        txtlname.Text = "";
        txtuserid.Text = "";
        ddlprodsite.SelectedIndex = -1;
        ddlrole.SelectedIndex = -1;
        lblmessgae.Text = "";
        
    }
    protected void Deleteimg_Click(object sender, EventArgs e)
    {
         string confirmValue = Request.Form["confirm_value"];
         if (confirmValue == "Yes")
         {
             if (Session[SESSION.LOGGED_USER] != null && Session[SESSION.LOGGED_USER].ToString() != "")
             {

                 string lang = "";
                 if (Session["lang"] != null)
                 {
                     lang = Session["lang"].ToString();
                 }
                 else
                 {
                     //lang = "en-US";
                     Session["lang"] = "fr-FR";
                     lang = "fr-FR";
                 }
                 Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
                 rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
                 ci = Thread.CurrentThread.CurrentCulture;

                 bool isSelected = false;
                 bool isSelectedchk = false;
                 int Rowindex = 0;
                 string str = "";
                 for (int i = 0; i < grdUser.Rows.Count; i++)
                 {
                     GridViewRow row = grdUser.Rows[i];
                     bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                     if (isChecked)
                     {
                         string txtUser = str = grdUser.Rows[i].Cells[0].Text;
                        

                         SqlParameter[] paramList = new SqlParameter[1];
                         paramList[0] = new SqlParameter("userid", txtUser);// grdUser.Rows[i].Cells[0].Text
                         int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.deleteUser, paramList);
                         if (rowAffected > 0)
                         {

                             isSelected = true;
                         }

                         //if (str == "")
                         //{
                         //    str = grdUser.Rows[i].Cells[0].Text;// +"'";

                         //}
                         //else
                         //{
                         //    str = str + ",'" + grdUser.Rows[i].Cells[0].Text +"'";
                         //}

                     }

                 }
                 if (isSelected == true)
                 {
                     Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + rm.GetString("User_deleted_successfully", ci) + "')", true);
                     // lblmessgae.Text = rm.GetString("User_deleted_successfully", ci);
                 }
                 if (isSelectedchk == false)
                 {
                     Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + rm.GetString("Please_select_item_for_delete_user", ci) + "')", true);
                     // lblmessgae.Text = rm.GetString("Please_select_item_for_delete_user", ci);
                 }
                

                 Binduser();
             }

         }

    }

    protected void grdUser_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {

        //NewEditIndex property used to determine the index of the row being edited.  
        grdUser.EditIndex = e.NewEditIndex;

        Label lblprodid = (Label)grdUser.Rows[e.NewEditIndex].FindControl("lbl_Proditem");
        string Prod = lblprodid.Text;

        Binduser();


        DropDownList ddlprod = (DropDownList)grdUser.Rows[e.NewEditIndex].FindControl("edit_ddlprodsite");
        if (ddlprod != null)
        {
            ddlprod.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getproduct, paramlist);
            ddlprod.DataSource = ds;
            ddlprod.DataTextField = "proddesc";
            ddlprod.DataValueField = "prodid";
            ddlprod.DataBind();

            ddlprod.SelectedValue = Prod;

            //ddlprod.SelectedItem.Text = "Revues";
            chkedit = true;
            // ddlprod.SelectedValue = Prod;
        }






    }



    protected void grdUser_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

        // Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "$('#myModal').modal('show');", true);

        string lang = "";
        if (Session["lang"] != null)
        {
            lang = Session["lang"].ToString();
        }
        else
        {
            //lang = "en-US";
            Session["lang"] = "fr-FR";
            lang = "fr-FR";
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;




        DropDownList edit_prodsite = grdUser.Rows[e.RowIndex].FindControl("edit_ddlprodsite") as DropDownList;
        //  edit_prodsite.SelectedValue = lblprodid.Text;
        string prodId = edit_prodsite.SelectedValue;

        string firstName = Convert.ToString(e.NewValues["FIRSTNAME"]);
        string LastName = Convert.ToString(e.NewValues["LASTNAME"]);
        string roleId = Convert.ToString(e.NewValues["ROLEID"]);

        TextBox txtrole = grdUser.Rows[e.RowIndex].FindControl("txtrole") as TextBox;

        //string prodId = Convert.ToString(e.NewValues["PRODID"]);

        string user = grdUser.Rows[e.RowIndex].Cells[0].Text;

        string abc = string.Empty;
        string query = "Update  login set FIRSTNAME = '" + firstName + "', LASTNAME = '" + LastName + "', ROLEID='" + txtrole.Text.Trim() + "', PRODID='" + prodId + "', ACTIVE='Yes' where USERID = '" + user + "'";


        int result = DataAccess.ExecuteNonQuery(query);

        if (result > 0)
        {
            string msg = rm.GetString("User_Updated_successfully", ci);
            Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + msg + "')", true);
        }
        else
        {
            string msg = rm.GetString("User_UpdationFailed", ci);
            Page.ClientScript.RegisterStartupScript(GetType(), "modelBox", "alert('" + msg + "')", true);
        }
        chkedit = false;

        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        grdUser.EditIndex = -1;
        //Call ShowData method for displaying updated data  
        Binduser();

    }
    protected void grdUser_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        chkedit = false;
        grdUser.EditIndex = -1;
        Binduser();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {

        Session[SESSION.LOGGED_USER] = null;
        Response.Redirect("Default.aspx");

    }

    [WebMethod(EnableSession = true)]
    public static string RegisterUser(string userid, string password, string newpassword)
    {

        //int uid = Convert.ToInt16(userid);
        SqlParameter[] paramList = new SqlParameter[3];
        paramList[0] = new SqlParameter("uiid", userid);// SqlDbType.VarChar, 50);//, userid
        //paramList[0].Value = userid;
        paramList[1] = new SqlParameter("oldpass", password);// SqlDbType.VarChar, 50);//, password
        //paramList[1].Value = password;
        paramList[2] = new SqlParameter("newpass", newpassword);// SqlDbType.VarChar, 50);//, newpassword
        //paramList[2].Value = newpassword;
        int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.changepassword, paramList);

        if (rowAffected > 0)
        {
            return "Mot de passe modifié";
        }
        else
        {
            return "ancien mot de passe incorrect";
        }

    }

}