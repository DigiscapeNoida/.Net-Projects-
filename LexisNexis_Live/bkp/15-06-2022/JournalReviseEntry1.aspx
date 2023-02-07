<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="JournalReviseEntry1.aspx.cs" Inherits="EncyclopediaReviseEntry1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="login_box login_interface">
<div class="login_form spacer">


<asp:Label ID="lblHeadingRevisedrv" runat="server" Text="Log correction"></asp:Label>
    


<div class="row colle">
<asp:Label ID="lblIder" Text="ID" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txtleonardid" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
</div>

    <div class="row colle">
<asp:Label ID="lblreview" Text="Review" runat="server"></asp:Label>
<asp:TextBox ID="txtreview" runat="server" CssClass="input_2"></asp:TextBox>
</div>
    
<div class="row colle">
<asp:Label ID="lbljournalarticletitle" Text="Article Title" runat="server"></asp:Label>
<asp:TextBox ID="txtarticletitle" runat="server" CssClass="input_2" ></asp:TextBox>
</div>
    <div class="row colle">
         
<asp:Label ID="lbljournalAuthor" Text="Author" runat="server"></asp:Label>
            
<asp:TextBox ID="txtjournalauthor" runat="server" CssClass="input_2" ></asp:TextBox>
        
</div>
    <div class="row colle">
<asp:Label ID="lbljournaltobedone" Text="Work to be done" runat="server"></asp:Label>
<asp:DropDownList ID="ddlworktobedone" runat="server" CssClass="select_2" AutoPostBack="false">
    <asp:ListItem Text="préparation+balisage FSS" Value="préparation+balisage FSS" Selected="True"></asp:ListItem>
    <asp:ListItem Text="préparation uniquement" Value="préparation uniquement"></asp:ListItem>
    <asp:ListItem Text="balisage FSS uniquement" Value="balisage FSS uniquement"></asp:ListItem>
</asp:DropDownList>
</div>
     <div class="row colle">
         
<asp:Label ID="lbljournalartciletype" Text="Article Type" runat="server"></asp:Label>
            
<asp:TextBox ID="txtarticletype" runat="server" CssClass="input_2" ></asp:TextBox>
         
</div>
     <div class="row colle">
<asp:Label ID="lbljournalpubnum" Text="Publication Number" runat="server"></asp:Label>
<asp:TextBox ID="txtpubnum" runat="server" CssClass="input_2" ></asp:TextBox>
</div>
    <div class="row colle">
     <div class="col"> 
 <asp:Label ID="lblDelaijournal" Text="Delai back" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2" AutoPostBack="True" ></asp:DropDownList>
   
</div>
 

 <div class="row">
<asp:Label ID="lblNotificationforsupejournal" Text="Notification for sup" runat="server"></asp:Label>
<asp:TextBox ID="txtsupnotification" runat="server" CssClass="input_2"></asp:TextBox>
</div>


<div class="row">
<asp:Label ID="lblTitlefescer" Text="Title fesc" runat="server" Visible="false"></asp:Label>
<asp:TextBox ID="txttitlefesc" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
</div>

     <div class="row colle">
<div class="col">
 <asp:Label ID="lblCommenter" Text="Comment" runat="server"></asp:Label><span class="star">*</span>
    </div>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcomment"
        ErrorMessage="Please insert Comment" ForeColor="Red" Width="1px">*</asp:RequiredFieldValidator>

         </div>
    <div class="row colle">
<div class="col">
 <asp:Label ID="lblLoadafileer" Text="Load a file" runat="server"></asp:Label><span class="star">*</span>
</div>
<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="*" ControlToValidate="FileUpload1"
    runat="server" Display="Dynamic" ForeColor="Red" Width="1px" >*</asp:RequiredFieldValidator><br />
           
</div>
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
    </div>
     <div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
<asp:Label ID="hiddenval" runat="server" Visible="false"></asp:Label>
</div>


</div>

</asp:Content>

