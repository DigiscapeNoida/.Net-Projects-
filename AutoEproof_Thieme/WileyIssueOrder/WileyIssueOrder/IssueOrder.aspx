<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueOrder.aspx.cs" Inherits="WileyIssueOrder.IssueOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td style ="width:20%">
                    Issue Info</td>
                <td style ="width:20%">
                    AIDs</td>
                <td style ="width:10%">&nbsp;</td>
                <td style ="width:10%">&nbsp;</td>
                <td style ="width:60%">&nbsp;</td>
            </tr>
            <tr>
                <td style ="width:20%">
                    <table class="auto-style1">
                        <tr>
                            <td>Client</td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Volume</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" width="77px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Issue</td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" width="77px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Stage</td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" width="77px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Coverdate</td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" width="77px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DisplayCover</td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" width="77px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>StartPage</td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" width="77px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style ="width:10%">
                    <asp:TextBox ID="TextBox5" runat="server" Height="167px" Width="154px"></asp:TextBox>
                </td>
                <td style ="width:10%">
                    <asp:Button ID="Button1" runat="server" Text="Validate" Width="136px" />
                    <br/>
                    <asp:Button ID="Button2" runat="server" Text="GetArticleDetails" width="136px" />
                    <br/>
                    <asp:Button ID="Button3" runat="server" Text="GenerateIssueOrder" width="136px" />
                    <br/>
                </td>
                <td style ="width:60%">
                    <asp:Repeater ID="Repeater1" runat="server">
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>Issue Remarks</td>
                <td colspan ="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan ="5">
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
