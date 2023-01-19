<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="XmlOrderForm_all.aspx.cs" Inherits="XmlOrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<script language="javascript" type="text/javascript">
    function Browsexml()
    {
        popupWindow = window.open("Browse.aspx", 'popUpWindow', 'height=400,width=600,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
    }
</script>--%>
    <h2 style="color:Yellow">View Order&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
<div id="div" style="overflow: auto">
    <center>
    <table  border="0" style="width:800px; height: 100px;" align="center">
    <tr>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:200px">Please Select Book ID</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">Internal Order</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">PPM Order</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">Edit Order</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">Home</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">Browse</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:100px">Download</td>
    </tr>
      <tr>
        <td align="center">
            <asp:DropDownList ID="ddlbookid" runat="server"  Width="200px" 
                onselectedindexchanged="ddlbookid_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </td>
        <td align="center">
            <asp:RadioButton ID="rdinternaloder" runat="server" Text="Internal" 
                GroupName="A" AutoPostBack="True" 
                oncheckedchanged="rdinternaloder_CheckedChanged" Checked="True" />   
        </td>
        <td align="center">
            <asp:RadioButton ID="rdppmorder" runat="server" Text="PPM" 
                AutoPostBack="True" GroupName="A" 
                oncheckedchanged="rdppmorder_CheckedChanged"/>
        </td>
        <td align="center"><asp:LinkButton ID="lnkeditorder" runat="server" Width="100px" 
                onclick="lnkvieworder_Click">Edit Order</asp:LinkButton>
        </td>
        <td align="center">
            <asp:LinkButton ID="lnkinsertmore" runat="server" Width="100px" 
                onclick="lnkinsertmore_Click">Home</asp:LinkButton>
        </td>
        <td align="center">
            <asp:LinkButton ID="lnkbrowse" runat="server" Width="100px" 
                onclick="lnkbrowse_Click" >Browse</asp:LinkButton>
        </td>
        <td align="center">
             <asp:LinkButton ID="lnkdownload" runat="server" Width="100px" 
                 onclick="lnkdownload_Click">Download</asp:LinkButton>
        </td>
      </tr>
     <tr>
         <td align="center" colspan="7" style="color:White;background-color:#6B696B;font-weight:bold;">Book Level Information</td>
     </tr>
     <tr>
        <td colspan="7">
            <asp:TextBox ID="txtorder" runat="server" TextMode="MultiLine" Width="800px" 
                Height="500px" ReadOnly="True" BackColor="Silver" BorderColor="#FF3300"></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td  colspan="7">
         <asp:LinkButton ID="lnkparse" runat="server" onclick="lnkparse_Click" 
             Visible="False">Click here to check whether the xml order is parsed or not........</asp:LinkButton></td>
     </tr>
     <tr>
     <td  colspan="5">
         <asp:Label ID="lbldtd" runat="server" 
             Text="To download .dtd or xsl file click the match link."></asp:Label>
         <td>
             <asp:LinkButton ID="lnkdtd" runat="server" onclick="lnldtd_Click">Order.dtd</asp:LinkButton></td>
         
         <td>
             <asp:LinkButton ID="lnkxls" runat="server" onclick="lnkxls_Click">Order.xls</asp:LinkButton></td>
     </tr>
</table>
</center>
</div>
</asp:Content>

