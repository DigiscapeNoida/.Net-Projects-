<%@ Page Language="C#" MasterPageFile="FullMaster.master"  AutoEventWireup="true"  CodeFile="ItemReport.aspx.cs" Inherits="ItemReport" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID=UserThis Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID=TitleThis Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID=ContentBody Runat="Server">
<form id="form1" runat="server">
<div>
 <asp:Panel ID="MainPanel" runat="server" Height="100%" Width="100%" GroupingText="Select Criteria" Font-Names=verdana Font-Size=small>
<br />

        <asp:Label Font-Bold=true ID="Label1" runat="server" Style="z-index: 103;
        left: 0px; position: relative; top: 0px" Text="JID"></asp:Label>&nbsp;
            <asp:TextBox ID="txtJID" runat="server" Style="z-index: 100;
        left: 0px; position: relative; top: 0px"></asp:TextBox>

    <asp:Label Font-Bold=true ID="Label2" runat="server" Style="position: relative;" Text="AID"></asp:Label>
    <asp:TextBox ID="txtAID" runat="server" Style="position: relative;"></asp:TextBox>
        
            <asp:Label Font-Bold=true ID="Label3" runat="server" Style="position: relative;"  Text="Stage"></asp:Label>
        <asp:DropDownList ID="cmbSTAGE" runat="server" Style="position: relative;" Width="143px" Font-Size=small>
    </asp:DropDownList>
    
 <asp:Label Font-Bold=true ID="Label4" runat="server" Style="position: relative;"  Text="Status"></asp:Label><asp:DropDownList ID="cmbSTATUS" runat="server"
        Height="23px" Width="144px" Font-Size=small>
    </asp:DropDownList>
<br />
<br />
 <asp:Panel  ID="PanelSendDate" runat="server" Height="50px" Width="70%" GroupingText="Send Date" HorizontalAlign=center>
    <asp:Label Font-Bold=true ID="Label5" runat="server" Style="position: relative;" Text="From Date"></asp:Label>
    <asp:TextBox ID="txtFrmSend" runat="server" Style="position: relative;"></asp:TextBox>
    <asp:Button ID="cmdFrmSend" runat="server" Height="16px" OnClick="cmdFrmSend_Click" Style="z-index: 111;
    position: relative" Text="+" Width="22px" Font-Bold="True" Font-Size="Small" TabIndex="9" />

    <asp:Label Font-Bold=true ID="Label6" runat="server" Style="position: relative;"  Text="To Date"></asp:Label>
    <asp:TextBox ID="txtToSend" runat="server" Style="position: relative;"></asp:TextBox>
    <asp:Button ID="cmdToSend" runat="server" Height="16px" OnClick="cmdToSend_Click" Style="z-index: 111;
    position: relative" Text="+" Width="22px" Font-Bold="True" Font-Size="Small" TabIndex="9" />
     </asp:Panel>
        <asp:Calendar ID="cal" runat="server" Style="position: relative;" Visible=false OnSelectionChanged=cal_SelectionChanged></asp:Calendar> <br />
<br />

    <asp:Panel ID="PanelDueDate" runat="server" Height="50px" Width="70%" GroupingText="Due Date" HorizontalAlign=center>
    <asp:Label Font-Bold=true ID="Label7" runat="server" Style="position: relative;"  Text="From Date"></asp:Label>
    <asp:TextBox ID="txtFromDue" runat="server" Style="position: relative;"></asp:TextBox>
    <asp:Button ID="cmdFrmDue" runat="server" Height="16px" OnClick="cmdFrmDue_Click" Style="z-index: 111;
    position: relative" Text="+" Width="22px" Font-Bold="True" Font-Size="Small" TabIndex="9" />

    <asp:Label Font-Bold=true ID="Label8" runat="server" Style="position: relative;"  Text="To Date"></asp:Label>
    <asp:TextBox ID="txtToDue" runat="server" Style="position: relative;"></asp:TextBox>
    <asp:Button ID="cmdToDue" runat="server" Height="16px" OnClick="cmdToDue_Click" Style="z-index: 111;
    position: relative" Text="+" Width="22px" Font-Bold="True" Font-Size="Small" TabIndex="9" />
     </asp:Panel>
     <br />
    <center><asp:Button ID="cmdShow" runat="server" OnClick="cmdShow_Click" Style="z-index: 111;
    position: relative" Text=" Show " Font-Bold="True" Font-Size="Small" TabIndex="9"  /></center>
     

</asp:Panel>   
     
</div>
</form>
</asp:Content>

