<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 style="color:Yellow">Action Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
<center>
  <table border="0" cellpadding="5" style="width:500px; height: 198px;" align="center">
    <tr>
       <td align="center" colspan="4" style="color:White;background-color:#6B696B;font-weight:bold;">Message : 
           <asp:Label ID="lblinfomesg" runat="server" Text="XXXX"></asp:Label>
       </td>                                        
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblinformationbookid" runat="server" Text="Book ID" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinfobookid" runat="server" Text="XXXX" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinformationln" runat="server" Text="Line Number" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinfoln" runat="server" Text="XXXX" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblinformationstage" runat="server" Text="Stage" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinfostage" runat="server" Text="XXXX" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinformationlp" runat="server" Text="Line Position" Font-Bold="True"></asp:Label></td>
        <td>
            <asp:Label ID="lblinfolp" runat="server" Text="XXXX" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
       <td align="center" colspan="4" style="color:White;background-color:#6B696B;font-weight:bold;">
           <asp:LinkButton ID="lnkeditorder" runat="server" onclick="lnkeditorder_Click">Edit Order</asp:LinkButton>&nbsp;
           <asp:LinkButton ID="lnkvieworder" runat="server" onclick="lnkvieworder_Click">View Order</asp:LinkButton>&nbsp;
           <asp:LinkButton ID="lnkcacel" runat="server" onclick="lnkcacel_Click">Cancel</asp:LinkButton>
       </td>                                 
    </tr>
  </table>
</center>
</asp:Content>

