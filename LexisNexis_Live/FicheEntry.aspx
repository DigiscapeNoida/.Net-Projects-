<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FicheEntry.aspx.cs" Inherits="FicheEntry" %>

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


<h1>Log update records</h1>

<div class="table_content">
    <asp:GridView ID="grdViewEntry" runat="server"
            AllowPaging="false" AutoGenerateColumns="False" TabIndex="1"
            DataKeyNames="FID" Width="100%" GridLines="None"
            CellPadding="3" CellSpacing="1" AllowSorting="true" BorderWidth="1px" 
            >
            <Columns>
                <asp:BoundField DataField="FID" HeaderText="ID" 
                    SortExpression="FID" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
               
                <asp:BoundField DataField="Sgm_Filename" HeaderText="File Name" 
                    SortExpression="Sgm_Filename" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
               <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk" runat="server" />
                         <asp:Literal ID="litID" runat="server" Text='<%# Eval("FRID")%>' Visible="false"></asp:Literal>
                    </ItemTemplate>  
                </asp:TemplateField>  
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="table_paging"/>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle  BackColor="#D85354" Font-Bold="True" ForeColor="#fff"/>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
</div>


<div class="row">
<asp:Label ID="lblNotificationforsupe" Text="Notification for sup" runat="server"></asp:Label>
<asp:TextBox ID="txtnotification" runat="server" CssClass="input_2"></asp:TextBox>

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



    </form>
</body>
</html>
