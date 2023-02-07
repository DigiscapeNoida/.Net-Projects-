<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="EncyclopediasEntry1.aspx.cs" Inherits="EncyclopediasEntry1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="login_box login_interface">
<div class="login_form">


<asp:Label ID="lblHeadinge" runat="server" Text="Log a booklet (Encyclopedia)"></asp:Label><asp:Label ID="lblheading2e" runat="server" Text="Encyclopedia" Visible="false"></asp:Label>


 <div class="row colle">
        <div class="col"> 
 <asp:Label ID="lblCategorye" Text="Category" runat="server"></asp:Label><span class="star">*</span>
            </div>
        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="select_2" ></asp:DropDownList>
         <asp:RequiredFieldValidator ControlToValidate="ddlcategory" ID="RequiredFieldValidator1"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>
    

<div class="row">
<asp:Label ID="lblcollectione" Text="collection" runat="server"></asp:Label>
<asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_2"></asp:DropDownList>

</div>

<div class="row colle">
     <div class="col"> 
 <asp:Label ID="lblTitlefesce" Text="Title fesc" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:TextBox ID="txttitle" runat="server" CssClass="input_2"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttitle"
        ErrorMessage="Please insert Book Title!" ForeColor="Red" Width="1px">*</asp:RequiredFieldValidator>
</div>

<div class="row" >
 <asp:Label ID="lblTypeofiteme" Text="Type of item" runat="server" Visible="false"></asp:Label>
<asp:DropDownList ID="ddlitemtype" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
</div>

<div class="row colle">
     <div class="col"> 
<asp:Label ID="lblDTDiteme" Text="DTD item" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:DropDownList ID="ddldtditem" runat="server" CssClass="select_2"></asp:DropDownList>
     <asp:RequiredFieldValidator ControlToValidate="ddldtditem" ID="RequiredFieldValidator3"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>

<div class="row">
 <asp:Label ID="lblnewFolioe" Text="new Folio" runat="server"></asp:Label>
<asp:TextBox ID="txtfolio" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblReferencee" Text="Reference" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtreference" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
</div>

<div class="row colle">
     <div class="col"> 
 <asp:Label ID="lblNatureofdemande" Text="Nature of demand" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:DropDownList ID="ddldemandnature" runat="server" CssClass="select_2"></asp:DropDownList>
    <asp:RequiredFieldValidator ControlToValidate="ddldemandnature" ID="RequiredFieldValidator4"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>

<div class="row colle">
     <div class="col"> 
 <asp:Label ID="lblDelaibacke" Text="Delai back" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2" AutoPostBack="True" ></asp:DropDownList>
      <asp:RequiredFieldValidator ControlToValidate="ddldelai" ID="RequiredFieldValidator5"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>
    <div class="row">
<asp:Label ID="lblencyclodateheure" Text="Délai" runat="server"></asp:Label>
<asp:Label ID="lblencycloheureval" Text="" runat="server"></asp:Label>

</div>
<div class="row">
 <asp:Label ID="lblNameofapplicante" Text="Name of applicant" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtapplicationname" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
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

<div class="row colle">
     <div class="col"> 
<asp:Label ID="lblLoadaFileeEncyclo" Text="Load a file" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="*" ControlToValidate="FileUpload1"
    runat="server" Display="Dynamic" ForeColor="Red" Width="1px" >*</asp:RequiredFieldValidator><br /></div>
     <div class="row">
           <asp:Label ID="Dossiervalidationmessage" runat="server" Text=""></asp:Label>
       </div>
            &nbsp;


<div class="row">
<div class="center">
    <asp:Button ID="btnSend" runat="server" CssClass="red_btn" CausesValidation="true" 
                    Text="To send" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
</div>

<div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>

</div>

</asp:Content>

