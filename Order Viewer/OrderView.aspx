<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderView.aspx.cs" Inherits="OrderView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Viewer</title>
</head>
<body style="margin:0px;font-family:'Arial Unicode MS'">
    <form id="form1" runat="server">
    <div></div>
        <asp:LinkButton ID="lnksave" runat="server" Font-Size="Larger" ForeColor="Blue"
            Style="z-index: 101; left: 507px; position: absolute; top: 0px" OnClick="lnksave_Click">Save</asp:LinkButton>
        <asp:LinkButton ID="lnkprint" runat="server" CausesValidation="False" Font-Size="Large"
            ForeColor="Blue" OnClick="lnkprint_Click" OnClientClick=" window.print();return false;"
            Style="z-index: 100; left: 465px; position: absolute; top: 0px">Print</asp:LinkButton>
        &nbsp; &nbsp;
        <asp:Xml ID="Xml1" runat="server"></asp:Xml>
    </form>
</body>
</html>
