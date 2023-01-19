using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Register : System.Web.UI.Page
{
    GlbClasses objGlbCls = new GlbClasses();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Rolebind();
            Locationbind();
        }
        lblsbmitmsg.Visible = false;
    }
    private void Rolebind()
    {
        ddlrole.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlrole.Items.Insert(1, "ADMIN");
        ddlrole.Items.Insert(2, "LOCAL");
    }
    private void  Locationbind()
    {
        ddllocation.Items.Insert(0, new ListItem("--Select--", "0"));
        ddllocation.Items.Insert(1, "NSEZ");
        ddllocation.Items.Insert(2, "CHN");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (SaveNewUser() == true)
        {
            Response.Redirect("Login.aspx");
        }
    }
    private void ResetControls()
    {
        txtUserId.Text = string.Empty;
        txtUserPwd.Text = string.Empty;
        //ddlrole.Text = string.Empty;
        //ddllocation.Text = string.Empty;
        txtEmailId.Text = string.Empty;
    }
    private bool SaveNewUser()
    {
        try
        {
            string userId = txtUserId.Text.Trim().ToString();
            string userPwd=txtUserPwd.Text.Trim().ToString();
            string userRole=ddlrole.Text.Trim().ToString();
            string userLocation=ddllocation.Text.Trim().ToString();
            string userEmailId = txtEmailId.Text.Trim().ToString();

            if (objGlbCls.objData.CheckUserId(userId) == false)
            {
                if (objGlbCls.objData.InsertInfo(userId, userPwd, userRole, userLocation, userEmailId) == true)
                {
                    ResetControls();
                    lblsbmitmsg.Visible = true;
                    lblsbmitmsg.Text="Submitted Successfully";
                    return true;
                }
                else
                {
                   // ResetControls();
                   // lblsbmitmsg.Visible = true;
                    //lblsbmitmsg.Text = "User Already Exits";
                }
            }
            else
            {
                ResetControls();
                lblsbmitmsg.Visible = true;
                lblsbmitmsg.Text = "User Already Exits";
                return false;
            }
        }
        catch (Exception ex)
        {
           
        }
        return false;
    }
   
}
