<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LNDeliveryUpdate.aspx.cs" Inherits="LNDeliveryUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            Provide ÀrticleID:  
            <asp:TextBox ID="tbArticleID" runat="server"></asp:TextBox>
            
            Provide Date and Time:  
            <asp:TextBox ID="tbdate" runat="server"></asp:TextBox>

        <br />
         
             <br />          
             <asp:LinkButton ID="lbtnSubmit" runat="server" onclick="lbtnSubmit_Click">Submit</asp:LinkButton>
    </div>
    </form>
</body>
</html>
