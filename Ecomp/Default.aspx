<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Panel ID="pnlAdd" runat="server" Visible="True" Font-Size="Medium">
            Provide ISBN:  
            <asp:TextBox ID="tbisbn" runat="server"></asp:TextBox>
            
            
             <br />          
             <asp:LinkButton ID="lbtnSubmit" runat="server" onclick="lbtnSubmit_Click">Submit</asp:LinkButton>
             &nbsp;&nbsp;&nbsp;
            
            </asp:Panel>
        
<asp:GridView ID="gridviewDetails" runat="server" AutoGenerateColumns="false" 
     PageSize="10">
    <Columns>
        <asp:BoundField ItemStyle-Width="150px" DataField="PII" HeaderText="PII" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Filename" HeaderText="Filename" />
        <asp:BoundField ItemStyle-Width="150px" DataField="TypeSignal" HeaderText="TypeSignal" />
        <asp:BoundField ItemStyle-Width="150px" DataField="DownloadDate" HeaderText="DownloadDate" />
        <asp:BoundField ItemStyle-Width="150px" DataField="URL" HeaderText="URL" />
        <asp:BoundField ItemStyle-Width="150px" DataField="Remarks" HeaderText="Remarks" />
    </Columns> 
<HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />                                 
</asp:GridView>                                

<%--        <asp:GridView ID="gridviewDetails" runat="server" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"             
            >
        <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>              
 

                <asp:BoundField DataField="PII" HeaderText="PII" />
                <asp:BoundField DataField="Filename" HeaderText="Filename" />
                <asp:BoundField DataField="TypeSignal" HeaderText="TypeSignal" />
                <asp:BoundField DataField="DownloadDate" HeaderText="DownloadDate" />
                <asp:BoundField DataField="URL" HeaderText="URL" />                
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                
                
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>--%>
    </div>
    </form>
</body>
</html>
