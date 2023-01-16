<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="LN_Report.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:30%; margin: 100px auto; vertical-align:auto; font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size:20px; padding:20px;">
            <table align="center" style="border-bottom:1px solid #FBF07A; padding-bottom:10px; border-left:1px solid #FBF07A; border-right:1px solid #FBF07A; border-top:1px solid #FBF07A; padding-left:10px;padding-right:10px;padding-top:10px">
                <tr><td colspan="2" style="background-color:#FBF07A; font-size:25px; padding:5px; font-weight:600;" align="center">LN REPORT
                    </td>
                    </tr>
                <tr><td colspan="2"> <br />
                    </td>
                    </tr>
                <tr><td colspan="2" style="color:red;"> <asp:Label ID="lblerror" runat="server" Text=""></asp:Label><br />
                    </td>
                </tr>
                    <tr><td colspan="2"> <br />
                    </td>
                    </tr>
                    <tr>
                <td width="150" align="left">User ID</td><td width="150" align="left" style="padding:5px;">
                    <asp:TextBox ID="txtid" runat="server" Width="300px" Height="25px" Font-Size="Large"></asp:TextBox></td>
                    </tr>
                <tr>
                <td width="150" align="left">Password</td><td width="150" align="left" style="padding:5px;"><asp:TextBox ID="txtpwd" runat="server" TextMode="Password" Width="300px" Height="25px" Font-Size="Large"></asp:TextBox></td>
                    </tr>
                <tr><td>
                    </td><td align="left"><br /><asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" Height="30px" Width="70px" Font-Size="Large" /></td></tr>
                
            </table>
        </div>
    </form>
</body>
</html>
