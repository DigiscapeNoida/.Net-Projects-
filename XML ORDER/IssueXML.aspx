<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="IssueXML.aspx.cs"
    Inherits="IssueXML" MasterPageFile="~/MasterPage.master" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    Welcome to XML Order Creation and Integration Application</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="UserMaster" runat="server">
    <asp:Label ID="lblUser" runat="server" Text="" Font-Size="small" Font-Bold="True"></asp:Label></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBody" runat="Server">
    <div>
        <style type="text/css">
            .style2
            {
                height: 86px;
                width: 127px;
            }
            .style5
            {
                height: 187px;
            }
            .style20
            {
                width: 100%;
            }
        </style>
        <form id="form1" name="form1" runat="server">
        <table width="100%">
            <tr>
                <td colspan="3" align="center">
                    <hr id="Hr9" runat="server" color="#000099" style="z-index: 226; position: relative" />
                    <asp:Label ID="lblbook" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                        Font-Size="9pt" ForeColor="BlueViolet" Style="z-index: 101; position: relative;
                        top: 0px; left: 0px;" Text="Article Details" Width="96px" BackColor="Transparent"></asp:Label><hr
                            id="Hr10" runat="server" color="#000099" style="z-index: 226; position: relative;
                            top: 0px; left: 0px;" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="Journal Title:" Font-Bold="True" Font-Italic="False"
                        Font-Names="Verdana" Font-Size="8.5pt" ForeColor="#003300" Style="z-index: 101;
                        position: relative; top: 0px; left: 0px;" BackColor="Transparent"></asp:Label>
                    <asp:Label ID="LblJIDTitle" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana"
                        Font-Size="8.5pt" ForeColor="BlueViolet" Style="z-index: 101; position: relative;
                        top: 0px; left: 0px;" BackColor="Transparent"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr valign="top">
                <td class="style5">
                    <table class="style20">
                        <tr>
                            <td width="15%">
                                <asp:Label ID="lblAccount" runat="server" Font-Size="7.5pt" Style="z-index: 125;
                                    position: relative; top: 0px; left: 0px; height: 12px;" Text="Account:" Width="58px"
                                    Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlAccount" runat="server" Height="20px" Style="z-index: 136;
                                    position: relative; top: 0px; left: 3px; width: 166px;" Font-Size="8pt" AutoPostBack="True"
                                    TabIndex="1" Font-Names="Verdana" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <asp:Label ID="lblVol" runat="server" Font-="" Font-Bold="True" Font-Size="7.5pt"
                                    ForeColor="Black" Height="6px" Names="Verdana" Style="z-index: 123; position: relative"
                                    Text="Volume No:" Width="68px"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtVolume" runat="server" BorderStyle="Groove" Font-Names="Verdana"
                                    Font-Size="8pt" Height="15px" Style="z-index: 120; position: relative; top: 1px;
                                    left: 0px;" TabIndex="8" Width="164px"></asp:TextBox>
                            </td>
                            <td width="30%">
                                <asp:CheckBox ID="chkWOC" runat="server" Visible="true" OnCheckedChanged="chkWOC_CheckedChanged"
                                    Text="Woc" AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <asp:Label ID="lblJID" runat="server" Height="6px" Style="z-index: 124; position: relative;
                                    top: 0px; left: 0px;" Text="JID:" Width="58px" Font-Size="7.5pt" Font-Bold="True"
                                    Font-Names="Verdana" ForeColor="Black"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlJID" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                    Font-Size="8pt" Height="20px" OnSelectedIndexChanged="ddlJID_SelectedIndexChanged"
                                    Style="z-index: 137; position: relative; left: 1px; top: 0px; width: 166px;"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <asp:Label ID="lblIssue" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt"
                                    ForeColor="Black" Height="6px" Style="z-index: 123; position: relative" Text="Issue No:"
                                    Width="58px"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtIssueNo" runat="server" BorderStyle="Groove" Font-="" Font-Size="8pt"
                                    Height="15px" Names="Verdana" Style="z-index: 120; position: relative" TabIndex="8"
                                    Width="164px"></asp:TextBox>
                            </td>
                            <td width="30%">
                                <asp:FileUpload ID="IssueflUpload"  runat="server"  BorderStyle="Groove" Font-Bold="false" Font-Size="8pt" Height="25px"  />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  ControlToValidate="IssueflUpload" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator>
                               <%-- <asp:Button ID="Button1" Visible="true" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#666666" Height="28px" OnClick="btnFMS_Click" TabIndex="49" Text="Browse Material"
                                    Width="118px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <asp:Label ID="lblRevise" runat="server" Font-="" Font-Bold="True" Font-Size="7.5pt"
                                    ForeColor="Black" Height="19px" Names="Verdana" Style="z-index: 109; position: relative"
                                    Text="Work Flow:" Width="89px"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlWorkFlow" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                    Font-Size="8pt" Height="20px" OnSelectedIndexChanged="ddlWorkFlow_SelectedIndexChanged"
                                    Style="z-index: 136; position: relative" TabIndex="1" Width="166px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <asp:Label ID="lblApage" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt"
                                    ForeColor="Black" Height="19px" Style="z-index: 119; position: relative" Text="StartPage"
                                    Width="89px" Visible="false"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtsPages" runat="server" BorderStyle="Groove" Font-="" Font-Size="8pt"
                                    Height="15px" Names="Verdana" Style="z-index: 120; position: relative" TabIndex="8"
                                    Width="164px" Visible="false"></asp:TextBox>
                            </td>
                            <td width="30%">
                                <asp:Button ID="Button2" runat="server" Enabled="true" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="8pt" ForeColor="#666666" Height="28px" OnClick="cmdGenerate_Click"
                                    Text="Generate Order" Width="118px" />
                            </td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle">
                            <asp:Label ID="lblRemarks" runat="server" Font-="" Font-Bold="True" Font-Names="Verdana"
                                    ForeColor="Black" Height="19px" Size="7.5pt" Text="Remarks" Width="89px"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                             <asp:TextBox ID="txtRemarks" runat="server" BorderStyle="Groove" Font-="" Font-Size="8pt"
                                    Height="15px" Names="Verdana" Style="z-index: 120; position: relative" TabIndex="4"
                                    Width="164px"></asp:TextBox>
                        </td>
                        <td>
                         <asp:Label ID="lblFMSStatus" runat="server" Font-="" Font-Bold="True" Font-Names="Verdana"
                                    ForeColor="Black" Visible="false" Height="19px" Size="7.5pt" Text="FMS Status" Width="89px"></asp:Label>
                        </td>
                        <td align="left" valign="middle" colspan="2">
                            <%-- <asp:FileUpload ID="fileUpload" runat="server" Style="z-index: 103; position: relative" Visible="False" />--%>
                        </td>
                        </tr>
                        
                        <tr>
                            <td width="15%" style="vertical-align: top;">
                                <asp:Label ID="lblArticle" runat="server" Font-="" Font-Bold="True" Font-Names="Verdana"
                                    ForeColor="Black" Height="19px" Size="7.5pt" Text="AID" Width="89px"></asp:Label>
                            </td>
                            <td width="15%" valign="top">
                                <asp:TextBox ID="txtAID" runat="server" BorderStyle="Groove" Font-Names="Verdana"
                                    Font-Size="8pt" Style="z-index: 118; position: relative; top: 1px; left: 0px;
                                    height: 154px; width: 173px;" TabIndex="5" TextMode="MultiLine"></asp:TextBox>
                             </td>
                            <td width="15%" valign="top">
                                    <div id="Grid" runat="server" visible="false" style="border-style: solid; border-width: thin; float: none;  display: table-cell; width: 60px; height: 149px;" align="left">
                                        
                                    </div>
                            </td>
                            <td colspan="3" style="width: 45%; vertical-align:top; ">
                                <asp:GridView ID="grvFMSDetails" runat="server" Width="100%" Style="margin-bottom: 0px"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Jid" HeaderText="JID" />
                                        <asp:BoundField DataField="Aid" HeaderText="AID" />
                                        <asp:BoundField DataField="StartPage" HeaderText="StartPage" />
                                        <asp:BoundField DataField="EndPage" HeaderText="EndPage" />
                                    </Columns>
                                    <FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#FFCCCC" ForeColor="#333333" VerticalAlign="Top" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="style2" width="5%" style="vertical-align: top; text-align: left">
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="8.5pt" ForeColor="#990000"
                        Style="z-index: 101; position: relative; top: 0px; left: 0px;" BackColor="Transparent"
                        Text=""></asp:Label>
                    <asp:Label ID="lblDisplay" runat="server" Font-Bold="True" Width="300px" Font-Italic="False"
                        Font-Names="Verdana" Font-Size="8.5pt" ForeColor="BlueViolet" Style="z-index: 101;
                        position: relative; top: 0px; left: 0px;" BackColor="Transparent"></asp:Label>
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                   
                </td>
            </tr>
        </table>
        </form>
    </div>
</asp:Content>
