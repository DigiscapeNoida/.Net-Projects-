
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="DossierSearch.aspx.cs" Inherits="DossierSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="wrapper">
          
<div class="login_box login_spacer">
   <div class="login_form two_column">
   
       <div class="heading">
    <asp:Label ID="lblHeadingsearch" runat="server" Text="Search dossier"></asp:Label>
       </div>

       <div class="row">
 <asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2"></asp:DropDownList>
           
</div>
      
       <div class="row spacer">
      <asp:Label ID="lblstagedossiersearch" Text="Stage" runat="server"></asp:Label>
<asp:DropDownList ID="ddlstage" runat="server" CssClass="select_2">
    <asp:ListItem>----------</asp:ListItem>
                 <asp:ListItem>Dossier envoyé à Thomson</asp:ListItem>
                 <asp:ListItem>Annulé</asp:ListItem>
    <asp:ListItem>Livré sur Back Office</asp:ListItem>
</asp:DropDownList>
     </div> 
       
<div class="row">
 <asp:Label ID="lblDelaibacke" Text="Delai back" runat="server"></asp:Label>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2"></asp:DropDownList>
</div>
         <div class="row">
 <asp:Label ID="lblsearchuser" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlusersearch" runat="server" CssClass="select_2"></asp:DropDownList>
           
</div>
       <div class="row full">
    <asp:Label ID="lbldossiersearchid" Text="ID" runat="server"></asp:Label>
<asp:TextBox ID="txtid" runat="server" CssClass="input_2"></asp:TextBox>
    </div>
<div class="row full">
    <asp:Label ID="lblAuthor" Text="Author" runat="server"></asp:Label>
<asp:TextBox ID="txtauthor" runat="server" CssClass="input_2"></asp:TextBox>
    </div>

<div class="row full">
<asp:Label ID="lblFolderTitle" Text="Folder Title" runat="server"></asp:Label>
<asp:TextBox ID="txtfoldertitle" runat="server" CssClass="input_2" ></asp:TextBox>
  
</div>
<div class="row">
<asp:Label ID="lbldossierdeliverydatefrom" Text="date d'échéance de" runat="server"></asp:Label>
<asp:TextBox ID="txtfromduedate" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtfromduedate','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
   
</div>
<div class="row">
     <asp:Label ID="lbldossierdeliverydateto" Text="date d'échéance à" runat="server"></asp:Label>
<asp:TextBox ID="txttoduedate" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txttoduedate','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
     </div>
       <div class="row">
<asp:Label ID="lbldossierlogindatefrom" Text="date d'échéance de" runat="server"></asp:Label>
<asp:TextBox ID="txtlogindatefrom" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtlogindatefrom','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
   
</div>
<div class="row">
     <asp:Label ID="lbldossierlogindateto" Text="date d'échéance à" runat="server"></asp:Label>
<asp:TextBox ID="txtlogindateto" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtlogindateto','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
     </div>
 <div class="row">
      <asp:Label ID="lblDeclination" Text="Declination" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlDeclination" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
     </div>

<div class="row">
<div class="center">
    <asp:Button ID="btnSearch" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Search" OnClick="btnSend_Click"  />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click"  />

</div>
    
<div class="row">
<asp:Label ID="hiddenfeild" runat="server" Visible="false"></asp:Label>
</div>
</div>
   
</div>

</div>
</div>
</asp:Content>

