<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncyclopediasEntry.aspx.cs" Inherits="EncyclopediasEntry" %>

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

<asp:Label ID="lblHeading" runat="server" Text="Log a booklet (Encyclopedia)"></asp:Label><asp:Label ID="lblheading2" runat="server" Text="Encyclopedia" Visible="false"></asp:Label>

    <div class="row">
 <asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
<asp:Label ID="lblcollection" Text="collection" runat="server"></asp:Label>
<asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_2"></asp:DropDownList>

</div>

<div class="row colle">
 <asp:Label ID="lblTitlefesc" Text="Title fesc" runat="server"></asp:Label>
<asp:TextBox ID="txttitle" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblTypeofitem" Text="Type of item" runat="server"></asp:Label>
<asp:DropDownList ID="ddlitemtype" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
<asp:Label ID="lblDTDitem" Text="DTD item" runat="server"></asp:Label>
<asp:DropDownList ID="ddldtditem" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblnewFolio" Text="new Folio" runat="server"></asp:Label>
<asp:TextBox ID="txtfolio" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblReference" Text="Reference" runat="server"></asp:Label>
<asp:TextBox ID="txtreference" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblNatureofdemand" Text="Nature of demand" runat="server"></asp:Label>
<asp:DropDownList ID="ddldemandnature" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblDelaiback" Text="Delai back" runat="server"></asp:Label>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblNameofapplicant" Text="Name of applicant" runat="server"></asp:Label>
<asp:TextBox ID="txtapplicationname" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblNotificationforsup" Text="Notification for sup" runat="server"></asp:Label>
<asp:TextBox ID="txtsupnotification" runat="server" CssClass="input_2"></asp:TextBox>
</div>
    <div class="row">
 <asp:Label ID="lblDueDate" Text="Due Date" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtduedate" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
</div>
<div class="row">
<asp:Label ID="lblComment" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblLoadaFile" Text="Load a file" runat="server"></asp:Label>
<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" /><br /></div>
            &nbsp;


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

</div>

</div>


    </form>



</body>
</html>
