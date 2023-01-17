<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Title="Stylesheet Viewer" %>
                                                
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <table style="width: 100%; height: 100%">
        <tr>
            <td bgcolor="white" align ="left" style="width: 50%;height:"100%">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/mission.jpg" 
                    style="position: relative  absolute; top: -3px; left: -165px;" Height="373px" Width="351px"/>
            </td>
               <td  bgcolor="white" "width: 50%;height:"100%" align="right">
                <asp:Login ID="Login1" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1px" LoginButtonText="Log in" 
                    onauthenticate="Login1_Authenticate" TitleText="Log in" RememberMeSet="True">
                    <TextBoxStyle Width="175" /> 
                </asp:Login>
                </td>
        </tr>
        </table>
</asp:Content>