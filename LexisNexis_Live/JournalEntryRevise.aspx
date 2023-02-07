<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalEntryRevise.aspx.cs" Inherits="JournalEntryRevise" %>

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


<h1>Log correction (Article)</h1>




<div class="row colle">
<label>Leonardo id</label>
<asp:TextBox ID="txtleonardid" runat="server" CssClass="input_2" ></asp:TextBox>
</div>

<div class="row">
<label>Article Title</label>
<asp:TextBox ID="txtarticletitle" runat="server" CssClass="input_2"></asp:TextBox>
</div>


<div class="row">
<label>Comment</label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<label>Load a file</label>
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



</div>


    </form>
</body>
</html>
