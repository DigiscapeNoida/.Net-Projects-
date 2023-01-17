<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Viewer</title>
    <style type="text/css">
        .style1
        {
            width: 372px;
            height: 290px;
        }
        #form1
        {
            height: 758px;
        }
        .style4
        {
            height: 110px;
        }
        .style5
        {
            height: 17px;
        }
    </style>
</head>
<body style="font-family:'Arial Unicode MS'" bgcolor="whitesmoke">
    <form id="form1" runat="server">
    <div style="height: 663px; width: 1182px; top: 42px; left: 38px; position: absolute;" 
        align="center">
        <table bgcolor="whitesmoke" 
            
            style="border: thin solid #000000; height: 555px; width: 962px; margin-top:50px; margin-left: 40px; line-height: normal;"  >
            <tr>
                <td  colspan="3" 
                    style="border-bottom-style: solid;  border-bottom-color: #000000; border-bottom-width: 1.5pt;" 
                    class="style4">
                    <asp:Image ID="Image1" runat="server" Height="129px" ImageUrl="~/Header1.jpg" 
                        Width="880px" />
                </td>
            </tr>
            <tr>
                <td class="style1" >
                    </td>
                <td align="center" class="style1" valign="middle">
                    <asp:Login ID="Login1" runat="server" BorderColor="Black" BorderStyle="Groove" 
                        BorderWidth="1px" LoginButtonText="Log in" 
                        onauthenticate="Login1_Authenticate" TitleText="Log in" Font-Bold="True" 
                        ForeColor="Black" DisplayRememberMe="False">
                        <TextBoxStyle Width="175" />
                        <LoginButtonStyle BorderStyle="Solid" Font-Bold="True" />
                        <LabelStyle ForeColor="Black" />
                    </asp:Login>
                </td>
                <td align="center" class="style1" valign="middle">
                &nbsp;
                </td>
            </tr>
            <tr valign="top">
            <td colspan="3" valign="bottom" align="center" 
                    style="color:#000000; " class="style5">
                Copyright &copy; 2011 <b>Thomson</b> <span style="color:Red;"><b>Digital</b></span>. All right reserved.&nbsp;
            </td>
            </tr>
        </table>
       </div>
    </form>
</body>
</html>
