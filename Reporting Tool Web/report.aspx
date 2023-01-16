<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="WebApplication2.report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <table align="center" style="background-color:#fcfdb4;">
                <tr>
                    <td colspan="2" align="center"><h1 style="background-color:#FBF07A;">LN REPORT</h1></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><h3>Select report type </h3>
                        
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CellPadding="22" ForeColor="#0000ff" BackColor="#FBF07A" Font-Size="Large" OnSelectedIndexChanged="Reset_Click" AutoPostBack="true">
                            <asp:ListItem Text="Revues"  Value="rev"></asp:ListItem>
                            <asp:ListItem Text="Encyclopedia" Value="enc"></asp:ListItem>
                            <asp:ListItem Text="Fiches" Value="ficj"></asp:ListItem>
                            <asp:ListItem Text="Fiches FP" Value="ficf"></asp:ListItem>
                            <asp:ListItem Text="Synthesis" Value="syn"></asp:ListItem>
                        </asp:RadioButtonList>                      
                        <br /><br />
                    </td>
                </tr>
                <tr>
                    <td width="400" align="center" valign="top">SELECT START DATE<br />
                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">  
        </asp:Calendar>
            </td>
                    <td width="400" align="center" valign="top">SELECT END DATE<br />
                        <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged">  
        </asp:Calendar>
           </td>
                </tr>
                <tr>
                    <td align="center"><asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;</td>
                    <td align="center"> <asp:Label ID="Label2" runat="server" Text=""></asp:Label>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br /><br />
                        <asp:Button ID="Button1" runat="server" Text="Get Report" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><br />
                        </td>
                    </tr>
            </table>
        </div>
    </form>
</body>
</html>
