<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="EncyclopediaUpdate.aspx.cs" Inherits="EncyclopediasEntry1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="login_box login_interface">
<div class="login_form">

<asp:Label ID="lblHeadinge" runat="server" Text="Log a booklet (Encyclopedia)"></asp:Label><asp:Label ID="lblheading2e" runat="server" Text="Encyclopedia" Visible="false"></asp:Label>

    <div class="row">
 <asp:Label ID="lblCategorye" Text="Category" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2" ></asp:DropDownList>
</div>
    

<div class="row">
<asp:Label ID="lblcollectione" Text="collection" runat="server"></asp:Label>
<asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_2"></asp:DropDownList>

</div>

<div class="row colle">
 <asp:Label ID="lblTitlefesce" Text="Title fesc" runat="server"></asp:Label>
<asp:TextBox ID="txttitle" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblTypeofiteme" Text="Type of item" runat="server"></asp:Label>
<asp:DropDownList ID="ddlitemtype" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
<asp:Label ID="lblDTDiteme" Text="DTD item" runat="server"></asp:Label>
<asp:DropDownList ID="ddldtditem" runat="server" CssClass="select_2"></asp:DropDownList>
</div>

<div class="row">
 <asp:Label ID="lblnewFolioe" Text="new Folio" runat="server"></asp:Label>
<asp:TextBox ID="txtfolio" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblReferencee" Text="Reference" runat="server"></asp:Label>
<asp:TextBox ID="txtreference" runat="server" CssClass="input_2"></asp:TextBox>
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
 <asp:Label ID="lblNameofapplicante" Text="Name of applicant" runat="server"></asp:Label>
<asp:TextBox ID="txtapplicationname" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblNotificationforsupe" Text="Notification for sup" runat="server"></asp:Label>
<asp:TextBox ID="txtsupnotification" runat="server" CssClass="input_2"></asp:TextBox>
</div>
    <div class="row">
 <asp:Label ID="lblDueDatee" Text="Due Date" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtduedate" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
</div>
<div class="row">
<asp:Label ID="lblCommente" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblLoadaFilee" Text="Load a file" runat="server" Visible="false"></asp:Label>
<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" Visible="false" /><br /></div>
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
<asp:Label ID="hiddeenval" runat="server" Visible="false"></asp:Label>
</div>

</div>

</asp:Content>

