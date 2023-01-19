<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UniQQuestion.aspx.cs" Inherits="UniQQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList 
            ID="ComboBox1"
            DataSourceID="SqlDataSource1" 
            DataTextField="Exam/Class" 
            DataValueField="Id" 
            MaxLength="0" 
            runat="server" >
        </asp:DropDownList>
        <asp:DropDownList 
            ID="ComboBox2"
            DataSourceID="SqlDataSource2" 
            DataTextField="Subject" 
            DataValueField="Id" 
            MaxLength="0" 
            runat="server" >
        </asp:DropDownList>
        <asp:DropDownList 
            ID="ComboBox3"
            DataSourceID="SqlDataSource3" 
            DataTextField="Topic" 
            DataValueField="Id" 
            MaxLength="0" 
            runat="server" >
        </asp:DropDownList>
        <asp:DropDownList 
            ID="ComboBox4"
            DataSourceID="SqlDataSource4" 
            DataTextField="Complexity" 
            DataValueField="Id" 
            MaxLength="0" 
            runat="server" >
        </asp:DropDownList>
        <asp:TextBox TextBoxID="TextBox1" Text="No of Question"  runat="server" ></asp:TextBox>
    </div>
    </form>
</body>
</html>
