<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Action.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width:500px;
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
    <h2 style="color:Yellow">Action To Be Performed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
<div id="action" align="center" style="color: #0000FF; overflow: auto; ">
<center>
<table  class="style1"> 
    <tr>
    <td align="right">
    
        <asp:Label ID="lblwelcome" runat="server" Text="WELCOME : " ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Label ID="lblshowemailid" runat="server" Text="Label" ForeColor="Blue" Font-Bold="true"></asp:Label>
   
    </td>
     <td></td>
    </tr>
    <tr>
    <td></td>
     <td></td>
    </tr>
    <tr>
    <td></td>
     <td></td>
    </tr>
    <tr>
        <td  align= "left" > 
            <asp:Label ID="lblaction1" runat="server" Text="Click here for new Book Xml order : " Font-Bold="True"></asp:Label>
        </td>
        <td align="left" > 
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Click here</asp:LinkButton>          
        </td>
    </tr> 
    <tr>
    <td></td>
     <td></td>
    </tr>
    <tr>
        <td align="left"> 
            <asp:Label ID="Label1" runat="server" Text="Click here to edit existing order :" Font-Bold="True"></asp:Label>
        </td>
        <td align="left"> 
            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Click here</asp:LinkButton>          
        </td>
    </tr>  
    <tr>
    <td></td>
     <td></td>
    </tr>     
    </table>
 </center>
 </div>
</asp:Content>

