<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="EncycloSearch.aspx.cs" Inherits="EncycloSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="login_box login_interface login_spacer">
<div class="login_form two_column">

    <div class="heading">
<asp:Label ID="lblHeadingsearche" runat="server" Text="Chercher Fiche"></asp:Label><asp:Label ID="lblheading2fiche" runat="server" Text="Chercher Fich" Visible="false"></asp:Label>
        </div>

    <div class="row">
 <asp:Label ID="lblCategorye" Text="Category" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2" ></asp:DropDownList>
</div>
    

<div class="row">
<asp:Label ID="lblcollectione" Text="collection" runat="server"></asp:Label>
<asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_2"></asp:DropDownList>

</div>



<div class="row">
    <asp:Label ID="lblTypeofiteme" Text="Type of item" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlitemtype" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
<asp:Label ID="lblDTDiteme" Text="DTD item" runat="server"></asp:Label>
<asp:DropDownList ID="ddldtditem" runat="server" CssClass="select_2"></asp:DropDownList>
</div>




<div class="row">
 <asp:Label ID="lblNatureofdemande" Text="Nature of demand" runat="server"></asp:Label>
<asp:DropDownList ID="ddldemandnature" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblDelaibacke" Text="Delai back" runat="server"></asp:Label>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2"></asp:DropDownList>
</div>
       <div class="row">
 <asp:Label ID="lblsearchuser" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlusersearch" runat="server" CssClass="select_2"></asp:DropDownList>
           
</div>
<div class="row colle full">
 <asp:Label ID="lblTitlefesce" Text="Title fesc" runat="server"></asp:Label>
<asp:TextBox ID="txttitle" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row full">
 <asp:Label ID="lblnewFolioe" Text="new Folio" runat="server"></asp:Label>
<asp:TextBox ID="txtfolio" runat="server" CssClass="input_2"></asp:TextBox>
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

            &nbsp;


<div class="row btn_row">
<div class="center">
    <asp:Button ID="btnSearch" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Search" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
</div>

<div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>

</div>

</asp:Content>

