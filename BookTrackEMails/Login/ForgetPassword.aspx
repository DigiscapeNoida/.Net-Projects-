<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="Login_ForgetPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript">
        var redirectTimerId = 0;
        function closeWindow() {
            window.opener = top;
            redirectTimerId = window.setTimeout('redirect()', 2000);
            window.close();
        }

        function stopRedirect() {
            window.clearTimeout(redirectTimerId);
        }

        function redirect() {
            window.location = 'default.aspx';
        }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h2 style="color:Yellow">Forget Your Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
    <div>
            <center>
            <table cellSpacing="1" cellPadding="0" align="center" bgColor="#f6691f" border="1">
                <tr>
					<td bgColor="#ffffff">
						<table cellSpacing="2" cellPadding="2" align="center" border="1">
						<tr>
                            <td align= "center" colspan="2" style="color:White;background-color:#6B696B;font-weight:bold;" >Please Enter Your Email Id.</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblmailid" runat="server" Text="Please Enter Your Email Id." Width="200px"></asp:Label></td>
                            <td><asp:TextBox ID="txtmailid" runat="server" Width="200px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtmailid"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter Your Email Id." /><br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="txtmailid" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                        ControlToValidate="txtmailid" ErrorMessage="RegularExpressionValidator" 
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email-Id 
                                                        is not well formed</asp:RegularExpressionValidator>
                            
                            
                            </td>
                        </tr>
                        <%--<tr>
                            <td colspan="2"><asp:Label ID="Label1" runat="server" Text="OR" Width="500px"></asp:Label></td>
                        </tr>--%>
                        <%--<tr>
                            <td><asp:Label ID="lbluserid" runat="server" Text="Please enter the user id" Width="250px"></asp:Label></td>
                            <td><asp:TextBox ID="txtuserid" runat="server" Width="250px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtuserid"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter User Id." />
                                                        
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txtuserid" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                        ControlToValidate="txtuserid" ErrorMessage="RegularExpressionValidator" 
                                                        ValidationExpression="[0-9]">User-Id 
                                                        is not well formed</asp:RegularExpressionValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center"><asp:Button ID="btnsubmit" runat="server" Text="Submit"  
                                    BackColor="#FFCC99" onclick="btnsubmit_Click"/></td>
                            <td align="center"><asp:Button ID="btncancel" runat="server" Text="Cancel"  
                                    BackColor="#FFCC99" onclick="btncancel_Click" OnClientClick="return closeWindow();"/></td>
                        </tr>
                        <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblmsg" runat="server" Text="Your password has been e-mailed to you." Font-Bold="True" Font-Size="Smaller" ForeColor="Green" Visible="False"></asp:Label>
                        </td>
                        </tr>
					</table>
				</td>
			</tr>
          </table>
       </center>
   </div>
</asp:Content>

