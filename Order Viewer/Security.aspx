<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Viewer</title>
    <style type="text/css">
        #form1
        {
            height: 504px;
        }
        
        .style1
        {
            width: 100%;
            height: 473px;
        }
        .style2
        {
            height: 117px;
        }
        
        .style4
        {
            width: 446px;
        }
        
        .style9
        {
            width: 103px;
        }
        .style11
        {
            width: 190px;
        }
        .style12
        {
            height: 6px;
            width: 190px;
        }
        .style13
        {
            height: 6px;
            width: 103px;
        }
        .style14
        {
            height: 6px;
        }
        .style15
        {
            width: 190px;
            height: 45px;
        }
        .style16
        {
            width: 103px;
            height: 45px;
        }
        .style17
        {
            width: 446px;
            height: 45px;
        }
        .style18
        {
            height: 45px;
        }
        
        .style19
        {
            width: 190px;
            height: 27px;
        }
        .style20
        {
            width: 103px;
            height: 27px;
        }
        .style21
        {
            width: 446px;
            height: 27px;
        }
        .style22
        {
            height: 27px;
        }
        .style23
        {
            width: 190px;
            height: 36px;
        }
        .style24
        {
            width: 103px;
            height: 36px;
        }
        .style25
        {
            width: 446px;
            height: 36px;
        }
        .style26
        {
            height: 36px;
        }
        .style27
        {
            width: 190px;
            height: 39px;
        }
        .style28
        {
            width: 103px;
            height: 39px;
        }
        .style29
        {
            width: 446px;
            height: 39px;
        }
        .style30
        {
            height: 39px;
        }
        
        .style31
        {
            height: 6px;
            width: 446px;
        }
        
    </style>
</head>
<body style="font-family:'Arial Unicode MS'" bgcolor="#f7f7f7">
<form runat="server">

    <table class="style1">
        <tr>
            <td class="style2" colspan="5">
             <asp:Image ID="Image1" runat="server" ImageUrl="~/Header1.jpg" 
                    style="top: 21px; left: 16px; position: absolute; height: 117px; width: 964px; bottom: 346px;" />
                <asp:LinkButton ID="lnkHome" runat="server" CausesValidation="false"  onclick="lnkHome_Click" 
                    style="top: 117px; left: 663px; position: absolute; height: 1px; width: 67px">Home</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="style12">
                </td>
            <td class="style13">
                </td>
            <td class="style31">
                </td>
            <td class="style14">
                </td>
            <td class="style14">
                </td>
        </tr>
        <tr>
            <td class="style15">
                </td>
            <td class="style16">
                </td>
            <td class="style17" valign="middle" align="center" style="font-size:large">
                <b>For Admin Users Only</b></td>
            <td class="style18">
                </td>
            <td class="style18">
                </td>
        </tr>
        <tr>
            <td class="style12">
                </td>
            <td class="style13">
                </td>
            <td  valign="middle" align="left" class="style31" 
                
                
                style="border-top-style: solid; border-bottom-style: solid; border-top-width: 1pt; border-bottom-width: 1pt; border-top-color: #FF0000; border-bottom-color: #FF0000">
                 <asp:Label   ID="lbluid" runat="server" Text="User Name" 
                    style="height: 20px; width: 124px" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;
                 <asp:TextBox ID="txtuserid" runat="server" 
                    style="height: 20px; width: 158px" Width="150px"></asp:TextBox>
                    &nbsp;&nbsp;
                 <asp:Button  ID="cmdlogin" runat="server" Text="Log in" 
                    style="height: 30px; width: 74px;" Font-Bold="True" 
                     onclick="cmdlogin_Click" />
                    &nbsp;
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="* Required." ControlToValidate="txtuserid"
                    style="height: 20px; width: 14px"></asp:RequiredFieldValidator>
                 </td>
            <td class="style14">
                </td>
            <td class="style14">
                </td>
        </tr>
        <tr>
            <td class="style19">
                </td>
            <td class="style20">
                </td>
            <td class="style21">
                </td>
            <td class="style22">
                </td>
            <td class="style22">
                </td>
        </tr>
        <tr>
            <td class="style23">
                </td>
            <td class="style24">
                </td>
            <td class="style25">
                </td>
            <td class="style26">
                </td>
            <td class="style26">
                </td>
        </tr>
        <tr>
            <td class="style27">
                </td>
            <td class="style28">
                </td>
            <td class="style29">
                </td>
            <td class="style30">
                </td>
            <td class="style30">
                </td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</form>
</body>
</html>
