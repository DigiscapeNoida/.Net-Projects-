<%@ Page Language="C#" MasterPageFile="FullMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Login to XML Order" %>
<asp:Content ID="Title"        ContentPlaceHolderID="TitleThis" runat="server">Login to XML Order Creation and Integration Application</asp:Content>

<asp:Content ID="UserLogin"    ContentPlaceHolderID="UserThis" runat="server">New User</asp:Content>

<asp:Content ID="OrderContent" ContentPlaceHolderID="ContentBody" Runat="Server">
    <form id="form1" runat="server">
        <table style="height:100%; width:100%">
            <tr>
                <td colspan="2">
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                </td>
            </tr>
            <tr>
                   <td style="color:Blue;vertical-align:top;text-align:left">
                    <img  alt="View Source"  src="ImagesAndStyleSheet/login.jpg"border="0" style="width: 109px; height: 97px"/>
                    </td>
                    <td>
                    <p>
                    &nbsp;Account:&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="cmbCustomer"  runat="server" Height="20px" 
                           Width="106px" Font-Size="8pt" TabIndex="1" Font-Names="Verdana" AutoPostBack="false"></asp:DropDownList>
                    </p>
                    <asp:Login TabIndex="2" ID="Login1" 
                               runat="server"                       Font-Size="X-Small"  
                               OnAuthenticate="Login1_Authenticate" ForeColor="Navy" 
                               LoginButtonText="Sign Up"            EnableTheming="True" 
                               BorderStyle="None" >
                        <TextBoxStyle Width="150px" />
                        <LoginButtonStyle  BackColor="Gray" BorderColor="Black" />
                        <LabelStyle BackColor="White" BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <TitleTextStyle HorizontalAlign="Center"  Wrap="False" />
                    </asp:Login>
            </td></tr>
            
            <tr>
                <td colspan="2">
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                <br/>
                </td>
            </tr>
        </table>        
    </form>
</asp:Content>

