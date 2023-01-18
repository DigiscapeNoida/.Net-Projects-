<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IOS Press (Ver 1.0)</title>


    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="bootstrap-3.3.7-dist/js/jquery-3.1.1.min.js"></script>
    <script src="bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-3.3.7-dist/js/JqueryUI.js"></script>
    <link href="bootstrap-3.3.7-dist/js/ThemeUI.CSS/themeUICSS.css" rel="stylesheet" />
    <style type="text/css">
        .pad5 {
            padding-right: 5px !important;
            padding-left: 5px !important;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="XX-Large" Height="27px"
            Style="z-index: 100; left: 384px; position: absolute; top: 77px" Text="Online Flow Sheet"
            Width="265px"></asp:Label>
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/CompLogo.gif" Style="z-index: 101; left: 869px; position: absolute; top: 23px" />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Journal ID" Style="z-index: 102; left: 18px; position: absolute; top: 154px" Height="19px"></asp:Label>
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<br />
            <asp:Label ID="Labelmanu" runat="server" Text="Manuscript" Style="z-index: 102; left: 18px; position: absolute; top: 184px" Height="19px"></asp:Label>
            <br />


            <asp:Label ID="Label2" runat="server" Text="Article ID" Style="z-index: 103; left: 18px; position: absolute; top: 214px" Height="19px" Width="61px"></asp:Label>
            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtAID" runat="server" Style="z-index: 104; left: 111px; position: absolute; top: 214px" Height="17px" Width="155px"></asp:TextBox>
            <asp:Label ID="lblErrorMsg" runat="server" Font-Size="Large" Style="z-index: 105; left: 237px; position: absolute; top: 335px"
                Width="240px" ForeColor="Red"></asp:Label>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Style="z-index: 115; left: 887px; position: absolute; top: 160px"
                Text="Logout" Width="91px" />
            <hr style="height: 1px" />
            <br />
            <asp:Label ID="Label3" runat="server" Style="z-index: 107; left: 18px; position: absolute; top: 246px"
                Text="Vol Issue" Width="60px"></asp:Label>
            <asp:TextBox ID="txtVIS" runat="server" Height="17px" OnTextChanged="TextBox3_TextChanged"
                Style="z-index: 108; left: 111px; position: absolute; top: 246px" Width="155px"></asp:TextBox>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/td_logo.jpg" Style="z-index: 109; left: 14px; position: absolute; top: 10px" />
            &nbsp;&nbsp;
        <asp:DropDownList ID="DDL" runat="server" DataValueField="All" Style="z-index: 110; left: 113px; position: absolute; top: 160px"
            Width="155px">
            <asp:ListItem Selected="True">ALL</asp:ListItem>
            <asp:ListItem>CHM</asp:ListItem>
            <asp:ListItem>HSM</asp:ListItem>
            <asp:ListItem>IFS</asp:ListItem>
            <asp:ListItem>JRS</asp:ListItem>
            <asp:ListItem>JVR</asp:ListItem>
        </asp:DropDownList>
            &nbsp;<br />
            <asp:DropDownList ID="ddl_manu" runat="server" DataValueField="All" Style="z-index: 110; left: 113px; position: absolute; top: 184px"
                Width="155px">
                <asp:ListItem Selected="True">ALL</asp:ListItem>
                <asp:ListItem>In Production</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <br />
            &nbsp;
            <asp:Button ID="Button1" runat="server" Text="Display Info" OnClick="Button1_Click1" Style="z-index: 111; left: 360px; position: absolute; top: 160px" Width="184px" Height="22px" />
        </div>
        <asp:DataGrid ID="MyDataGrid" Style="Z-INDEX: 112; LEFT: 15px; POSITION: absolute; TOP: 300px"
            runat="server" AllowSorting="True" HorizontalAlign="Justify" BackColor="White" AutoGenerateColumns="False"
            HeaderStyle-BackColor="#aaaadd" Font-Size="8pt" Font-Name="Verdana" CellPadding="4" BorderWidth="1px"
            BorderColor="#3366CC" Font-Names="Verdana" ShowFooter="True" BorderStyle="None" Height="128px"
            Width="467px" OnSelectedIndexChanged="MyDataGrid_SelectedIndexChanged" OnItemCommand="Grid_DeleteCommand">
            <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
            <ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
            <HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
            <FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
            <Columns>
                <asp:BoundColumn DataField="JID" HeaderText="JID">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AID" HeaderText="AID">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="VolIss" HeaderText="Publication details (vol/issue/page nrs)">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Title" HeaderText="Title">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>

                <%-- Edited by kshitij to add new fields --%>



                <asp:BoundColumn DataField="ArticleType" HeaderText=" Article Type ">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Reprints" HeaderText=" Reprints" >
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="WaterMark" HeaderText=" WaterMark">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="AutoCorrectionReminder" HeaderText=" Auto Correction Reminder" >
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="ColorFigure" HeaderText=" Color Figure " >
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SpecialIssueName" HeaderText=" Special Issue Name ">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="HardCopy" HeaderText=" Hard Copy ">
                    <HeaderStyle HorizontalAlign="Center" CssClass="pad5" />
                </asp:BoundColumn>

                <%-- Done  --%>

                <asp:BoundColumn DataField="DtReceipt" HeaderText="Date of Receipt">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Authors" HeaderText="Authors">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TSPages" HeaderText="Typeset Pages">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtProofsSent_Author" HeaderText="Date of PDF proof sent to authors">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtCorrectionsRec_Author" HeaderText="Date of corrections received from authors">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AuthorReq" HeaderText="Special Reqirements from Authors">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtProofsSent_Editor" HeaderText="Date of proof sent to Editor-in-Chief">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtCorrectionsRec_Editor" HeaderText="Date of corrections received from Editor-in-Chief">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtProofsSent_Editorial" HeaderText="Date of proof sent to Editorial Assistant">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DtCorrectionsRec_Editorial" HeaderText="Date of corrections received from Editorial Assistant">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IssComDt" HeaderText="Confirmation Date to issue compilation from IOS">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CRCDeliveryDt" HeaderText="Date of CRC delivery to IOS">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PrintPDFDT" HeaderText="Confirmation Date of Print/Screen pdf">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PrintPDF2IOSDt" HeaderText="Date of Print/Screen pdf sent to IOS">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ReminderDate" HeaderText="Reminder Sent Date">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TsRemarks" HeaderText="Thomson Remarks">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OAO" HeaderText="Open Access Option"></asp:BoundColumn>

                <asp:BoundColumn DataField="JournalID" Visible="false" HeaderText="Open Access Option"></asp:BoundColumn>


                <%-- Added By kshitij to delete data --%>
                <asp:ButtonColumn Text="Delete" runat="server" ButtonType="PushButton" HeaderText="Delete" CommandName="Delete"></asp:ButtonColumn>



            </Columns>
            <PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Export to Excel" Style="z-index: 113; left: 361px; position: absolute; top: 185px" Height="22px" Width="184px" />
        <asp:Button ID="Button2" runat="server" Text="Display Report" OnClick="Button2_Click" Style="z-index: 114; left: 361px; position: absolute; top: 216px" Height="22px" Width="184px" Visible="False" />
        &nbsp; &nbsp;


            <%-- Modal Window --%>
        <div class="container">
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Are you sure you want to delete the row?</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="col-lg-12">
                                    <p>You will need to sign in as Admin to delete the row.</p>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <asp:TextBox runat="server" ID="txt_UserName"  CssClass="input-sm" PlaceHolder="User Name" Style="width: 100%;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvcandidate" ValidationGroup="Group1" runat="server" ControlToValidate ="txt_UserName" ErrorMessage="User Name Field Can't be empty" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-12">
                                        <asp:TextBox runat="server" ID="txt_Password" TextMode="Password" PlaceHolder="Password"  CssClass="input-sm" Style="width: 100%;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Group1"  runat="server" ControlToValidate ="txt_Password" ErrorMessage="Password Field Can't be empty" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                
                                <div class="col-lg-4">
                                    <%--<asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Group1"  />--%>
                                    <asp:Button runat="server" ID="btn_AdminLogin" ValidationGroup="Group1" OnClick="btn_AdminLogin_Click" CssClass="btn-primary btn-sm" Text="Save"   Style="width: 100%;" />
                                </div>
                                    </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>

        </div>





    </form>
</body>
</html>
