<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DossierEntry1.aspx.cs" Inherits="DossierEntry1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="Stylesheet/css/style.css">
    <link rel="stylesheet" type="text/css" href="Stylesheet/font/font.css">

</head>
<body>
    
    <div class="wrapper">

<div class="login_box">
   <div class="login_form">
   <form id="form1" runat="server">
    <asp:Label ID="lblHeading" runat="server" Text="login a dossier"></asp:Label>
       <div class="row">
 <asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblDeclination" Text="Declination" runat="server"></asp:Label>
<asp:DropDownList ID="ddlDeclination" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row colle">
<asp:Label ID="lblFolderTitle" Text="Folder Title" runat="server"></asp:Label>
<asp:TextBox ID="txtfoldertitle" runat="server" CssClass="input_2" ></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblAuthor" Text="Author" runat="server"></asp:Label>
<asp:TextBox ID="txtauthor" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblmailnotification" Text="Additional mail notification" runat="server"></asp:Label>
<asp:TextBox ID="txtmailnotification" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblComment" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="textarea_1"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblLoadafile" Text="Load a file" runat="server"></asp:Label>
 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" /><br /></div>
            &nbsp;


<div class="row">
<div class="center">
    <asp:Button ID="btnSend" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="To send" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
    
<div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>
</div>
    </form>
</div>

</div>
</div>

</body>
</html>
