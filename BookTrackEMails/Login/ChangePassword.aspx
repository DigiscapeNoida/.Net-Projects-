<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Login_ChangePassword" %>
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
<h2 style="color:Yellow">Change Your Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
<div>

            <center>            
            
           <table>
           <tr>
           <td>
               <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/td1.bmp"/>
           </td>
           </tr>
           </table>
            <table cellSpacing="1" cellPadding="0" align="center" bgColor="#f6691f" border="1" >
                <tr>
					<td bgColor="#ffffff">
				     <table cellSpacing="2" cellPadding="2" align="center" border="1">
	
						<tr>
						<td colspan="2">
                            <asp:TextBox ID="txtuserid" runat="server" Width="200px" BorderColor="#999966" BorderWidth="1px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtuserid"
                                                        WatermarkCssClass="watermarked" WatermarkText="User Id." />
                            </td>
                         </tr>
                         <tr>
						<td  colspan="2">
                            <asp:TextBox ID="txtoldpassword" runat="server" Width="200px" BorderColor="#999966" BorderWidth="1px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtoldpassword"
                                                        WatermarkCssClass="watermarked" WatermarkText="Old Password" />
                            </td>
                         </tr>
                         <tr>
						<td  colspan="2">
                            <asp:TextBox ID="txtnewpassword" runat="server" Width="200px" BorderColor="#999966" BorderWidth="1px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtnewpassword"
                                                        WatermarkCssClass="watermarked" WatermarkText="New Password" />
                            </td>
                            </tr>
                            <tr>
						<td  colspan="2">
                            <asp:TextBox ID="txtconnewpassword" runat="server" Width="200px" BorderColor="#999966" BorderWidth="1px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtconnewpassword"
                                                        WatermarkCssClass="watermarked" WatermarkText="Confirm Password" />
                                                     <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                        ControlToCompare="txtnewpassword" ControlToValidate="txtconnewpassword" 
                                                        ErrorMessage="CompareValidator">Not Matched</asp:CompareValidator>--%>
                            </td>
                            </tr>
                           <tr> 
                           <td align="center" >
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" BackColor="#FFCC99" 
                                    onclick="btnsubmit_Click"/>
						    </td>
						    <td align="center">
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" BackColor="#FFCC99" 
                                    onclick="btncancel_Click" OnClientClick="return closeWindow();"/>
						    </td>
						</tr>
						<tr>
						<td colspan="2" align="center">
                            <asp:Label ID="lblmsg" runat="server" Text="Label" Font-Bold="True" Font-Size="Smaller" ForeColor="Green" Visible="False"></asp:Label>
						</td>
						</tr>
						</tr>
					 </table>
				</td>
			</tr>
          </table>
       </center>
   </div>

</asp:Content>

