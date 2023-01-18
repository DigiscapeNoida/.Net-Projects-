<%@ Page Language="C#" MasterPageFile="FullMaster.master" AutoEventWireup="true" CodeFile="LogData.aspx.cs" Inherits="LogData" %>

<asp:Content ID="LogOrder" ContentPlaceHolderID=ContentBody runat="server">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblmsg" runat="server" ForeColor=red Font-Bold=true ></asp:Label>
        <asp:GridView ID="gvLog" runat="server" Style="z-index: 100; left: 0px; 
            top: 0px; background-color:#669999; border:1; font-size: x-small; font-family: Verdana;" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</asp:Content>