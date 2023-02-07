<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncyclopediaReviseEntry.aspx.cs" Inherits="EncyclopediaReviseEntry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="Stylesheet/css/style.css">
    <link rel="stylesheet" type="text/css" href="Stylesheet/font/font.css">

</head>
<body>
    <form id="form1" runat="server">
   
<div class="login_box login_interface">
<div class="login_form">


<asp:Label ID="lblHeading" runat="server" Text="Log correction"></asp:Label><strong class="bracket">(</strong><asp:Label ID="lblheading2" runat="server" Text="Encyclopedia"></asp:Label><strong class="bracket">)</strong>



<div class="row colle">
<asp:Label ID="lblId" Text="Leonardo id" runat="server"></asp:Label>
<asp:TextBox ID="txtleonardid" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblTitlefesc" Text="Title fesc" runat="server"></asp:Label>
<asp:TextBox ID="txttitlefesc" runat="server" CssClass="input_2"></asp:TextBox>
</div>


<div class="row">
 <asp:Label ID="lblComment" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="textarea_1"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblLoadafile" Text="Load a file" runat="server"></asp:Label>
<div id="Div1"><asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" /><br /></div>
            &nbsp;
</div>

<div class="row">
<div class="center">
    <asp:Button ID="btnSend" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="To send" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
</div>
     <div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
<asp:Label ID="hiddenval" runat="server" Visible="false"></asp:Label>
</div>


</div>


    </form>
</body>
</html>
