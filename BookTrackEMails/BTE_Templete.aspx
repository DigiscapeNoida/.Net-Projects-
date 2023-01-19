<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="BTE_Templete.aspx.cs" Inherits="BTE_Templete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Css/chromestyle.css" rel="stylesheet" type="text/css" />
    <link href="../Css/styles.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .tb5 {
            background: url(Images/rounded.gif) no-repeat top left;
            height: 22px;
            width: 133px;
        }

        .tb5a {
            border: 0;
            width: 133px;
            margin-top: 3px;
        }

        .tb8 {
            width: 230px;
            border: 1px solid #3366FF;
            border-left: 4px solid #3366FF;
        }

        .tb7 {
            width: 230px;
            border: 1px solid #3366FF;
            border-left: 4px solid #3366FF;
        }

        .tb6 {
            width: 133px;
            background: transparent url('Images/bg.jpg') no-repeat;
            color: #747862;
            height: 20px;
            border: 0;
            padding: 4px 8px;
            margin-bottom: 0px;
        }

        .style1 {
            height: 24px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        var message = 'Right Click is disabled';
        function clickIE() {
            if (event.button == 2) {
                alert(message); return false;
            }
        }
        function clickNS(e) {
            if (document.layers || (document.getElementById && !document.all)) {
                if (e.which == 2 || e.which == 3) { alert(message); return false; }
            }
        }
        if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
        else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
        document.oncontextmenu = new Function('alert(message);return false') </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    `&nbsp;&nbsp;&nbsp;
    
    <script language="javascript" type="text/javascript">
        function languagepopup() {
            if (document.getElementById("<%=ddlbtelanguage.ClientID%>").value == "0") {
                alert("Please select Language");
                document.getElementById("<%=ddlbtelanguage.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=txtbtevolumewithissue.ClientID%>").value == "") {
             alert("Please Enter Volume/ISSUE PII");
             document.getElementById("<%=txtbtevolumewithissue.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=txtbookeditors.ClientID%>").value == "") {
             alert("Please Enter Book Authors");
             document.getElementById("<%=txtbookeditors.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=txtbooktitle.ClientID%>").value == "") {
             alert("Please Enter Book Title");
             document.getElementById("<%=txtbooktitle.ClientID%>").focus();
             return false;
         }
      <%-- else if (document.getElementById("<%=txtPmemail.ClientID%>").value == "") {
             alert("Please Enter PM Email");
             document.getElementById("<%=txtPmemail.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=txtPMName.ClientID%>").value == "") {
             alert("Please Enter PM Name");
             document.getElementById("<%=txtPMName.ClientID%>").focus();
             return false;
         }--%>
            return true;
        }
        function basicPopup() {
            debugger;
            if (document.getElementById("<%=txtbteisbn.ClientID%>").value == "") {
             alert("ISBN Feild can not be blank");
             document.getElementById("<%=txtbteisbn.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=txtbtechp.ClientID%>").value == "") {
             alert("No of chapters can not be blank");
             document.getElementById("<%=txtbtechp.ClientID%>").focus();
             return false;
         }
         else if (document.getElementById("<%=ddlbtestage.ClientID%>").value == "0") {
             alert("Please select Stage");
             document.getElementById("<%=ddlbtestage.ClientID%>").focus();
             return false;
         }

         else if (document.getElementById("<%=ddlbtejob.ClientID%>").value == "0") {
             alert("Please select Job Type");
             document.getElementById("<%=ddlbtejob.ClientID%>").focus();
             return false;
         }
    return true;
}
    </script>
    <h2 style="color: Yellow">Book Information</h2>
    <center>
        <div id="btetable">
            <table border="0" style="width: 600px; height: 100px;" align="center">
                <tr>
                    <td align="center" colspan="6" style="color: White; background-color: #6B696B; font-weight: bold;">Please Fill All The Entry</td>
                </tr>
                <tr>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">formatted ISBN*</td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">HUB AID</td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">No. of Items*</td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">Stage*</td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">Job Type*</td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">
                        <asp:Button ID="btnbteextract" runat="server" Text="Extract" Font-Bold="True"
                            BackColor="#FFCC99" Width="133px" Height="32px"
                            OnClick="btnbteextract_Click" OnClientClick="return basicPopup();" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtbteisbn" CssClass="tb8" runat="server" align="center"
                            Width="133px" AutoPostBack="True" OnTextChanged="selectOrder"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbtehubid" CssClass="tb8" runat="server" align="center"
                            Width="133px" ReadOnly="True">X0001</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbtechp" CssClass="tb8" runat="server" align="center" Width="133px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbtestage" runat="server" align="center" Width="133px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbtejob" runat="server" align="center" Width="133px">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="color: White; background-color: #6B696B; font-weight: bold; width: 100px">
                        <asp:Button ID="btnbtecancel" runat="server" Text="Cancel" Font-Bold="True"
                            BackColor="#FFCC99" Width="133px" Height="32px" OnClick="btnbtecancel_Click" /></td>
                </tr>
                <tr>
                    <td align="center" colspan="6" style="color: White; background-color: #6B696B; font-weight: bold;">Please Fill Book Infornation</td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblbtelanguage" runat="server" Text="Project Language*" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddlbtelanguage" runat="server" Width="133px">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="lblisbn" runat="server" Text="Imprint"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtimprint" runat="server" CssClass="tb7" Width="133px"
                            Height="20px" ReadOnly="True"></asp:TextBox></td>

                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblbooktitle" runat="server" Text="Book Title*" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbooktitle" runat="server" CssClass="tb7" Width="133px" Height="20px"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="lblbooksubtitle" runat="server" Text="Book Subtitle" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbooksubtitle" runat="server" CssClass="tb7" Width="133px" Height="20px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblbtevolumewithissue" runat="server" Text="Book PII*" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbtevolumewithissue" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                    <td>
                        <asp:HiddenField ID="txtowner" runat="server" />
                    </td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="lblbookeditors" runat="server" Text="Book Authors*" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbookeditors" runat="server" CssClass="tb7" Width="133px" Height="20px"></asp:TextBox></td>
                </tr>
                <%#getCount()%>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblISSN" runat="server" Text="ISSN" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtISSN" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                    <td>
                        <asp:HiddenField ID="txtediton" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="txtyears" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Label ID="lblseriesTitle" runat="server" Text="Series Title"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtSeriesTit" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblvol_no" runat="server" Text="Volume No" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtVol" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                    <td>
                        <asp:HiddenField ID="txtsize" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="txtcolor" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Label ID="lblbookeditors3" runat="server" Text="Jid"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtJID" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" Text="PM Name" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtPMName" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="PM Email"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtPmemail" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="Design Name" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txttDesignName" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" Text=" Spelling "
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="DDlistSpelling" runat="server">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem>UK</asp:ListItem>
                            <asp:ListItem>US</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                        <asp:Label ID="Label6" runat="server" Text="Author Name(Opener)"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left" class="style1">
                        <asp:DropDownList ID="DDListAName" runat="server">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style1"></td>
                    <td class="style1"></td>
                    <td align="left" class="style1">
                        <asp:Label ID="Label7" runat="server" Text="Affiliation(Opener)"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left" class="style1">
                        <asp:DropDownList ID="DDListAffilation" runat="server">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label8" runat="server" Text=" Mini-TOC(Opener)" Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="DDListToc" runat="server">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td align="left">
                        <asp:Label ID="Label9" runat="server" Text=" Reference Style"
                            Font-Bold="True"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtRefStyle" runat="server" CssClass="tb7"
                            Width="133px" Height="20px"></asp:TextBox></td>
                </tr>

                <tr>
                    <%-- <td colspan="11"></td>--%>
                </tr>
                <%#getCount()%>
                <tr>
                    <td align="center" colspan="6" style="color: White; background-color: #6B696B; font-weight: bold;">PII/DOI INFORMATION</td>
                </tr>
                <tr>
                    <%-- <td colspan="11"></td>--%>
                </tr>
            </table>
        </div>
    </center>
    <center>
        <div id="btegrid">
            <table width="600px">
                <tr>
                    <td>
                        <div id="BTE" style="overflow: scroll; width: 835px; height: 150px;">
                            <asp:GridView ID="GridView1" runat="server" Width="825px"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ShowFooter="true"
                                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server">
								<%#getCount()%>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PII" HeaderText="PII" ReadOnly="True" />
                                    <asp:BoundField DataField="DOI" HeaderText="DOI" ReadOnly="True" />
                                </Columns>
                                <FooterStyle BackColor="#FA8328" />
                                <SelectedRowStyle BackColor="#FEE3C6" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FEE3C6" ForeColor="Black" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#FA8328" Font-Bold="True" ForeColor="Black" />
                                <AlternatingRowStyle BackColor="#FEE3C6" />
                                <EditRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="linkbtnsave" runat="server" Font-Bold="True" ForeColor="Blue"
                            OnClick="linkbtnsave_Click" OnClientClick="return languagepopup();">Click here to save the above order information</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <marquee bgcolor="orange" direction="right" behavior="ALTERNATE">THOMSON DIGITAL</marquee>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>

