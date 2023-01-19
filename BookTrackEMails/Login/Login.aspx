<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width:700px;
            border: 1px solid #FFFF00;
        }
         .tb7 {
	    width: 150px;
	    border: 1px solid #3366FF;
	    border-left: 4px solid #3366FF;
        }
 </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script language="javascript" type="text/javascript">
    function Generatepopup()
    {
        popupWindow = window.open("ForgetPassword.aspx", 'popUpWindow', 'height=340,width=600,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
    }
    function Generatepopup1() {
        popupWindow = window.open("ChangePassword.aspx", 'popUpWindow', 'height=520,width=500,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
    }
</script>
    <h2 style="color:Yellow">Login&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblvalidinfo" Text="Invalid UserName or Password" runat="server" 
            Font-Bold="True" Font-Size="Smaller" ForeColor="Red" Visible="False"></asp:Label>
                    </h2>

<center>
  <table class="style1">
    <tr>
      <td>
          <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/thomsondigital.gif" 
              Height="250px" Width="477px" />
      </td>
    <td>
    <table>
         <tr>
              <td align="center" colspan="2" style="color:White;background-color:#6B696B;font-weight:bold;">LogIn Here</td>                                       
         </tr>                             
         <tr>
              <td align="left"><asp:Label ID="UserName" runat="server" Font-Bold="True">UserID</asp:Label></td>
              <td align="left"><asp:TextBox ID="txtUserName" runat="server" CssClass="tb7"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                  ControlToValidate="txtUserName" ErrorMessage="User Name is required." 
                  ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
               </td>
          </tr>
          <tr>
               <td align="left"><asp:Label ID="Password" runat="server" Font-Bold="True">Password</asp:Label></td>
               <td align="left"><asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" CssClass="tb7">balram@123</asp:TextBox>
                   <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                   ControlToValidate="txtUserPassword" ErrorMessage="Password is required." 
                   ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                </td>
           </tr>
           <tr><td colspan="2" align="center"><asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." /></td>
           </tr>
           <tr><td align="center" colspan="2">
               <asp:LinkButton ID="lnkchangeyourpassword" runat="server" 
               onclick="lnkchangeyourpassword_Click" OnClientClick="return Generatepopup1();">Change your password</asp:LinkButton>
               </td>                      
               </tr>
            <tr>
                <td align="center" colspan="2"><asp:LinkButton ID="lnkforgatpassword" runat="server" onclick="lnkforgatpassword_Click" OnClientClick="return Generatepopup();">I can not access my account</asp:LinkButton>
                </td>
            </tr>
            <tr><td align="center" colspan="2"><asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Sign In" 
                ValidationGroup="Login1" Font-Bold="True" 
                BackColor="#FFCC99" onclick="btnLogin_Click" />
                </td>                     
            </tr>
            <%--<tr><td colspan="2" align="center"><asp:HyperLink ID="hyperNewuser" runat="server" NavigateUrl="~/Login/Register.aspx" Text="New User ? Register Now"></asp:HyperLink></td>
            </tr>--%>
    </table>
</td>
</tr>
</table>
   
 </center>
</asp:Content>

