<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="TDUploadFile1.aspx.cs" Inherits="TDUploadFile1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="login_box login_interface">
<div class="login_form">


<asp:Label ID="lblHeadingtd" runat="server" Text="TD"></asp:Label><strong class="bracket">(</strong><asp:Label ID="lblheading2td" runat="server" Text="Encyclopedia"></asp:Label><strong class="bracket">)</strong>



<div class="row colle">
<asp:Label ID="lblIdtd" Text="Leonardo id" runat="server"></asp:Label>
<asp:TextBox ID="txtleonardid" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblTitlefesctd" Text="Title fesc" runat="server"></asp:Label>
<asp:TextBox ID="txttitlefesc" runat="server" CssClass="input_2"></asp:TextBox>
</div>


<div class="row">
 <asp:Label ID="lblCommenttd" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="textarea_1"></asp:TextBox>
</div>

<div class="row">
 <asp:Label ID="lblLoadafiletd" Text="Load a file" runat="server"></asp:Label>
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
<asp:Label ID="lblmessage" runat="server" Text="" ></asp:Label>
<asp:Label ID="hiddenval" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hiddenval1" runat="server" Visible="false"></asp:Label>
</div>

</div>
</asp:Content>

