<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="DossierUpdate.aspx.cs" Inherits="DossierEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="wrapper">
          

<div class="login_box">
   <div class="login_form">
   
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
 <asp:Label ID="lblLoadafile" Text="Load a file" runat="server" Visible="false"></asp:Label>
 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" Visible="false" /><br /></div>
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
<asp:Label ID="hiddenfeild" runat="server" Visible="false"></asp:Label>
</div>
</div>
   
</div>

</div>
</div>
</asp:Content>

