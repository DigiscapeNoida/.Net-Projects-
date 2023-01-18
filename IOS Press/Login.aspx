<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IOS Press (Ver 1.0)</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function Password1_onclick() {

}

function HR1_onclick() {

}

    // ]]>
var backlen = history.length;
        history.go(-backlen);
        

</script>
<script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
 </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtPassword" runat="server" OnTextChanged="TextBox1_TextChanged"
            Style="z-index: 100; left: 480px; position: absolute; top: 250px" TabIndex="2" TextMode="Password" Width="145px" Height="18px"></asp:TextBox>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Style="z-index: 110; left: 400px;
            position: absolute; top: 338px" Text="Invalid login/Password" Visible="False"
            Width="146px"></asp:Label>
        &nbsp; &nbsp;
        <asp:TextBox ID="txtUName" runat="server" Style="z-index: 102; left: 480px; position: absolute;
            top: 220px" Height="18px" TabIndex="1" Width="145px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="z-index: 103;
            left: 552px; position: absolute; top: 282px" Text="Login" Width="80px" TabIndex="3" Height="22px" />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="XX-Large" Height="27px"
            Style="z-index: 104; left: 384px; position: absolute; top: 77px" Text="Online Flow Sheet"
            Width="265px"></asp:Label>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/CompLogo.gif" Style="z-index: 105;
            left: 869px; position: absolute; top: 23px" />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/td_logo.jpg" Style="z-index: 106;
            left: 21px; position: absolute; top: 10px" />
        &nbsp;
        <hr style="z-index: 109; left: 4px; position: absolute; top: 140px; width: 993px; height: 2px;" id="HR1" onclick="return HR1_onclick()" />
        <asp:Label ID="Label1" runat="server" Style="z-index: 107; left: 399px; position: absolute;
            top: 222px" Text="Username"></asp:Label>
        <asp:Label ID="Label2" runat="server" Style="z-index: 108; left: 399px; position: absolute;
            top: 252px" Text="Password"></asp:Label>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
