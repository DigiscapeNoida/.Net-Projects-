<%@ Page Language="C#" EnableViewState="true"  AutoEventWireup="true" CodeFile="OrderViewer.aspx.cs" Inherits="OrderViewer" MasterPageFile="MasterPage.master"%>

<asp:Content ID="Title" ContentPlaceHolderID=PageTitle runat="server">Welcome to XML Order Creation and Integration Application</asp:Content>
<asp:Content ID="UserWelcome" ContentPlaceHolderID=UserMaster runat="server"><span id="aa"></span> <asp:Label ID="lblUser" runat="server" Text="" Font-Size="small" Font-Bold="True"></asp:Label></asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID=PageBody Runat="Server">



    <form id="form1" runat="server" >
    <div>
        <table border="0"  cellpadding="0" cellspacing="0" style="width: 100%; height: 5%; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;" id="TABLE1" onclick="return TABLE1_onclick()">
<!--            <tr style="height:110px; width: 100%; background-color: #f2f2f2;">
 <td colspan="2" style="background-image:url(header.gif);background-repeat:no-repeat; background-position:center" width=100% >
                </td>
            </tr>-->
            <tr style="width: 22%;height: 100%" >
                <td style="height: 99px; width: 238px">                   

                </td>
                <td style="width: 74%;height: 99px " valign="top" align=left>
                        <table style="width: 100%; height: 40px" >
                            <tr>
<td style="width:28%; height: 39%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px" valign=top><asp:Label ID="Label4" runat="server" Text="Account" Font-Bold="True" ForeColor=gray></asp:Label>
                                    </td>
                                            <td style="width: 39px">                                                <asp:DropDownList ID="cmbAccount" runat="server" Style="position: relative" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="cmbAccount_SelectedIndexChanged">
                                                    <asp:ListItem>JWUSA</asp:ListItem>
                                                    <asp:ListItem>JWUK</asp:ListItem>
                                                    <asp:ListItem>JWVCH</asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;">
                                    </td>
                                            <td style="width: 100%; height: 19px;" >
                                    </td>
                                        </tr>
                                    </table>
                                    </td>
                                <td style="width:28%; height: 39%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 139px; height: 25px;" valign=top><asp:Label ID="Label1" runat="server" Text="JID" Font-Bold="True" ForeColor=gray></asp:Label>
                                    </td>
                                            <td style="width: 39px; height: 25px;">
                                                <asp:DropDownList ID="cmbJID" runat="server" Style="position: relative" Width="102px" OnSelectedIndexChanged="cmbJID_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 139px; height: 19px;">
                                    </td>
                                            <td style="width: 100%; height: 19px;" >
                                    </td>
                                        </tr>
                                    </table>
                                    </td>
                                <td style="width: 32%; height: 39%;" valign="top" >
                                    <table style="height: 20px">
                                        <tr>
                                        <td style="width: 100px; height: 15px;" valign=top>
                                    <asp:Label ID="Label3" runat="server" Text="STAGE" Font-Bold="True" ForeColor=gray></asp:Label>
                                    </td>
                                            <td style="width: 42px; height: 15px;">
                                            <asp:DropDownList ID="cmbStage" runat="server" Style="position: relative" Width="155px">
                                                <asp:ListItem>Fresh</asp:ListItem>
                                                <asp:ListItem>MCE</asp:ListItem>
                                                <asp:ListItem>Revise</asp:ListItem>
                                                <asp:ListItem>FAX</asp:ListItem>
                                                <asp:ListItem>SGML</asp:ListItem>
                                                <asp:ListItem>Web</asp:ListItem>
                                                <asp:ListItem>Printer</asp:ListItem>
                                                </asp:DropDownList>

                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 46px; height: 14px;">
                                    </td>
                                            <td style="width: 100%; height: 14px;">
</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="height: 39%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 46px" valign=top>
                                            <asp:Label ID="Label2" runat="server" Text="AID/ISSUE" Font-Bold="True" ForeColor=gray></asp:Label>
                                    </td>
                                            <td style="width: 28px">
                                                <asp:TextBox ID="txtAID" runat="server" Style="position: relative"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan=2 style="width: 100px; height: 19px;">
                                            </td>
                                        </tr>
                                    </table>
                                    </td>                                
                                <td style="width: 100%; height: 39%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px; height: 20px;">
                                    </td>
                                            <td style="width: 100px; height: 20px;"><asp:ImageButton ID="ImageButton5" runat="server" Style="position: relative; z-index: 100;" ImageUrl="button_view.gif" OnClick="ImageButton5_Click" />&nbsp;<br />
                                                
                                    </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;">
                                            </td>
                                            <td style="width: 100px; height: 19px;">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Style="position: relative" ImageUrl="btnhome.gif" OnClick="ImageButton1_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                
                                <td valign=top>
</td>
                           </tr>
                            <tr>                                
                            </tr>
                        </table>
                    <asp:Label ID="txtOrderNotFound" runat="server" Style="position:relative" Width="723px" Font-Bold="True" ForeColor="OrangeRed"></asp:Label></td>
            </tr>
        </table>
	<asp:Xml ID="Xml1" runat="server"></asp:Xml>
    </div>
    </form>

</asp:Content>