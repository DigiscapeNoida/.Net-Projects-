<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="DossierEntry.aspx.cs" Inherits="DossierEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="wrapper">
          

<div class="login_box">
   <div class="login_form">
   
       
    <asp:Label ID="lblHeading" runat="server" Text="login a dossier"></asp:Label>
       

       <div class="row" >
 
           <div class="col">           <asp:Label ID="lblCategory" Text="Category" runat="server" Visible="false"></asp:Label><!--<span class="star">*</span>-->

               </div>

<asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
           <asp:RequiredFieldValidator ControlToValidate="ddlcategory" ID="RequiredFieldValidator1"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server" Visible="false">*</asp:RequiredFieldValidator>
</div>

<div class="row">
 <asp:Label ID="lblDeclination" Text="Declination" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlDeclination" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
</div>

<div class="row colle">

    <div class="col"> 
<asp:Label ID="lblFolderTitle" Text="Folder Title" runat="server"></asp:Label><span class="star">*</span>
        </div>

<asp:TextBox ID="txtfoldertitle" runat="server" CssClass="input_2" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfoldertitle"
        ErrorMessage="Please insert Book Title!" ForeColor="Red" Width="1px">*</asp:RequiredFieldValidator>
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
<asp:Label ID="lbldosierpublication" Text="Dossier Publication Date" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtdosierpublicationdate" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
           <!--<a href="javascript:NewCssCal('MainContent_txtdosierpublicationdate','mmddyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal111.gif" width="16" /></a>-->
</div>
 <div class="row">
<asp:Label ID="lbldossierdelai" Text="Deali" runat="server"></asp:Label>
<asp:TextBox ID="txtdosierdelai" runat="server" CssClass="input_2" Text="Courant" Enabled="false"></asp:TextBox>
</div>
<div class="row">
<asp:Label ID="lbldosierdateheure" Text="Délai" runat="server"></asp:Label>
<asp:Label ID="lbldosierheureval" Text="" runat="server"></asp:Label>

</div>

<div class="row">
<asp:Label ID="lblComment" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="textarea_1"></asp:TextBox>
</div>

<div class="row">
<div class="col"> 
 <asp:Label ID="lblLoadafileDosier" Text="Load a file" runat="server"></asp:Label><span class="star">*</span>
    </div>

 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="*" ControlToValidate="FileUpload1"
    runat="server" Display="Dynamic" ForeColor="Red" Width="1px" >*</asp:RequiredFieldValidator>
    <br /></div>
       <div class="row">
           <asp:Label ID="Dossiervalidationmessage" runat="server" Text=""></asp:Label>
       </div>
            &nbsp;


<div class="row">
<div class="center">
    <asp:Button ID="btnSend" runat="server" CssClass="red_btn" CausesValidation="true" 
                    Text="To send" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click"  />

</div>
    
<div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>
</div>
   
</div>

</div>
</div>
</asp:Content>

