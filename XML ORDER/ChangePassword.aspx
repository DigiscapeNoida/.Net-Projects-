<%@ Page Language="C#" MasterPageFile="FullMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Change Password" %>
<asp:Content ID="Title" ContentPlaceHolderID=TitleThis runat="server">Change Password</asp:Content>
<asp:Content ID="UserLogin" ContentPlaceHolderID=UserThis runat="server">New User</asp:Content>
<asp:Content ID="OrderContent" ContentPlaceHolderID=ContentBody Runat="Server">
<form id=form1 runat="server">

<table align="center">
<tr>
<td><asp:Label ID="Label3" runat="server" ForeColor="#507CD1" Font-Names="Verdana"
         Font-Size=smaller >Enter Your Login: </asp:Label></td>
<td style="width: 165px"><asp:TextBox runat="server" ID="LoginID"></asp:TextBox></td>
<td style="width: 22px"></td>
</tr>
<tr>
<td><asp:Label ID="lb1" runat="server" ForeColor="#507CD1" Font-Names="Verdana"
         Font-Size=smaller >Enter Your Old Password: </asp:Label></td>
<td style="width: 165px"><asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox></td>
<td style="width: 22px"></tr>
<tr>
<td><asp:Label ID="Label1" runat="server" Font-Names="Verdana"
         Font-Size=smaller  ForeColor="#507CD1">Enter Your New Password: </asp:Label></td>
<td style="width: 165px"><asp:TextBox runat="server" ID="NewPassword" TextMode="Password" ></asp:TextBox></td>
<td style="width: 22px"></td>
</tr>
<tr>
<td><asp:Label ID="Label2" Font-Names="Verdana"
         Font-Size=smaller runat="server" ForeColor="#507CD1">Confirm Your New Password: </asp:Label></td>
<td style="width: 165px"><asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password"></asp:TextBox></td>
<td style="width: 22px"></td>
</tr>
<tr>
<td align="center"><asp:Button runat="server" ID="ChangePasswordBtn" Text="Change Password" OnClick="ChangePasswordBtn_Click" />
</td>
<td style="width: 165px" align="center" colspan=2><asp:button runat="server" ID="Cancel" Text="Cancel" OnClick="Cancel_Click"/></td>
</tr>
<tr>
<td colspan=3><asp:Label ID="Err" Font-Names="Verdana"
         Font-Size="0.8em" runat="server" ForeColor="red"></asp:Label></td>
</tr>
</table>



</form>
</asp:Content>


