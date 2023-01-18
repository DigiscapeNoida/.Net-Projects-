#pragma checksum "D:\WinCVS\ThimeXMLORDER\ChangePassword.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "73D89E01B8D7DC22CD1150C5876235CF1FFEE7C7"

#line 1 "D:\WinCVS\ThimeXMLORDER\ChangePassword.aspx.cs"
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool IsAlphaNumericSpecialChar(String strToCheck)
    {
        Regex objAlphaNumericPattern = new Regex("@[^a-zA-Z0-9~`!@#$%^&*?_]");
        return objAlphaNumericPattern.IsMatch(strToCheck);
    }

    //protected void ChangePasswordControl_ChangingPassword(object sender, EventArgs e)
    //{
    //    SqlConnection sqlConn = null;
    //    SqlCommand sqlCmd;
    //    string strSQL = "", strConnection = "";
    //    try
    //    {
    //        if (Regex.Match(ChangePasswordControl.NewPassword, @"(?=.{7,})(?=(.*\d){1,})(?=(.*\W){1,})").Success)
    //        {
    //        }
    //        else
    //        {
    //            return;
    //        }
    //        //if (!IsAlphaNumericSpecialChar(ChangePasswordControl.ConfirmNewPassword) || (!IsAlphaNumericSpecialChar(ChangePasswordControl.ConfirmNewPassword)))
    //        //{
    //        //    //ChangePasswordControl.PasswordRequiredErrorMessage = "Password should be combination of alphanumeric with special character.\nPlease change your password.\n\n(As per password policy).";

    //        //    return;
    //        //}
    //        if (ChangePasswordControl.NewPassword != ChangePasswordControl.ConfirmNewPassword)
    //        {
    //            ChangePasswordControl.ConfirmPasswordCompareErrorMessage = "New password not matched with confirm password. Please try again.";
    //            return;
    //        }

    //        // get the connection string from web.config and open a connection
    //        // to the database
    //        strConnection = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
    //        sqlConn = new SqlConnection(strConnection);
    //        sqlConn.Open();

    //        // check to see if the user exists in the database
    //        strSQL = "Update Login set Password='" + ChangePasswordControl.ConfirmNewPassword + "' Where LoginID='" + ChangePasswordControl.UserName + "'";
    //        sqlCmd = new SqlCommand(strSQL, sqlConn);
    //        sqlCmd.ExecuteNonQuery();


    //    } // try
    //    catch (Exception ex)
    //    {
    //        ChangePasswordControl.ConfirmPasswordCompareErrorMessage = ex.Message;
    //    }
    //    finally
    //    {
    //        // cleanup
       
    //        if (sqlConn != null)
    //        {
    //            sqlConn.Close();
    //        }
    //    } // finally
    //}



    protected void ChangePasswordBtn_Click(object sender, EventArgs e)
    {
        SqlConnection sqlConn = null;
        SqlCommand sqlCmd;
        SqlCommand sqlCmdUpdate;
        SqlDataReader dr;
        string strSQL = "", strConnection = "";
        Err.Text = "";
        try
        {
            if (this.LoginID.Text =="")
            {
                Err.Text = "Please enter login ID.";
                return;
            }
            if (this.Password.Text  == "")
            {
                Err.Text = "Please enter password.";
                return;
            }
            if (this.NewPassword.Text == "")
            {
                Err.Text = "Please enter new password.";
                return;
            }
            if (this.NewPassword.Text.Length >14 || this.NewPassword.Text.Length<5)
            {
                Err.Text = "Password should be of Minimum 5 character and Maximum 14 Characters.";
                return;
            }
            if (this.ConfirmPassword.Text.Length > 14 || this.ConfirmPassword.Text.Length < 5)
            {
                Err.Text = "Password should be of Minimum 5 character and Maximum 14 Characters.";
                return;
            }

            if (this.ConfirmPassword.Text == "")
            {
                Err.Text = "Please enter confirmation password.";
                return;
            }
            if (Regex.Match(this.NewPassword.Text, @"(?=.{5,})(?=(.*\d){1,})(?=(.*\W){1,})").Success)
            {
            }
            else
            {
                Err.Text = "Password should be combination of alphanumeric with special character (Minimum 5 and Maximum 14 characters).\nPlease change your password.\n\n(As per password policy).";
                return;
            }

            if (this.Password.Text == this.ConfirmPassword.Text)
            {
                Err.Text = "New password and old passwords are same. Please try again.";
                return;
            }

            if (this.NewPassword.Text != this.ConfirmPassword.Text)
            {
                Err.Text = "New password not matched with confirm password. Please try again.";
                return;
            }
            if (this.NewPassword.Text.Length < 7 || this.ConfirmPassword.Text.Length < 7)
            {
                Err.Text = "New password length should be >= 7 charecters. Please try again.";
                return;
            }

            // get the connection string from web.config and open a connection
            // to the database
            strConnection = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            sqlConn = new SqlConnection(strConnection);
            sqlConn.Open();

            // check to see if the user exists in the database
            strSQL = "select * from Login where Password='" + Password.Text + "' and LoginID='" + LoginID.Text + "'";
            sqlCmd = new SqlCommand(strSQL, sqlConn);
            dr = sqlCmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.HasRows)
                {
                    dr.Close();
                    strSQL = "Update Login set Password='" + ConfirmPassword.Text + "' Where LoginID='" + LoginID.Text + "'";
                    sqlCmdUpdate = new SqlCommand(strSQL, sqlConn);
                    sqlCmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    Err.Text = "Login ID not found in database";
                    return;
                }
            }

            this.LoginID.Text = "";
            this.Password.Text = "";
            this.NewPassword.Text = "";
            this.ConfirmPassword.Text = "";
            this.RegisterClientScriptBlock("alert", "<script>alert('Your Password has been changed!')</script>");
        } // try
        catch (Exception ex)
        {
            Err.Text = ex.Message;
        }
        finally
        {
            // cleanup

            if (sqlConn != null)
            {
                sqlConn.Close();
            }
        } // finally

    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        this.LoginID.Text = "";
        this.Password.Text = "";
        this.NewPassword.Text = "";
        this.ConfirmPassword.Text = "";
        Err.Text = "";
    }
}


#line default
#line hidden
