<%@ Page Language="C#"  MasterPageFile="FullMaster.master" EnableEventValidation="false"  AutoEventWireup="true" CodeFile="LoginReport.aspx.cs" Inherits="LoginReport" Title="Untitled Page" %>


<asp:Content ContentPlaceHolderID=ContentBody ID="body" runat="server">
  <form id="form1" runat="server">
  <div>
  <table width="100%"><tr>
  <td><asp:Button  ID="Excel" Text="SaveExcel" runat="server" OnClick="Excel_Click"/></td>
  
  </tr></table>
    <hr />
    

 <asp:GridView ID="GridView1" runat="server" Style="z-index: 100; position: relative;" AllowSorting="true" OnSorted="GridView1_Sorted" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound">
<RowStyle ForeColor="#000066"></RowStyle>

<FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>

<PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066"></PagerStyle>

<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

<HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>



</asp:GridView>
    <asp:SqlDataSource ID="TISDataSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT * FROM JW_PTS WHERE JW_PTS.STAGE = 'FRESH' AND STATUS = 'OPEN'">
    </asp:SqlDataSource>
      &nbsp;&nbsp;
    </div>
</form>
</asp:Content>