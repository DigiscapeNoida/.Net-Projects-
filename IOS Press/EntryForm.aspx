<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryForm.aspx.cs" Inherits="EntryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="bootstrap-3.3.7-dist/js/jquery-3.1.1.min.js"></script>
    <script src="bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-3.3.7-dist/js/JqueryUI.js"></script>
    <link href="bootstrap-3.3.7-dist/js/ThemeUI.CSS/themeUICSS.css" rel="stylesheet" />

    <%--  for date picker
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>

    <script>
        $(function () {
            $(".datepicker-field").datepicker({
                dateFormat: 'dd/mm/yy',
                showWeek: false,
                changeMonth: true,
                changeYear: true,
                firstDay: 1
            });
        });
  </script>


    <script language="javascript" type="text/javascript">
        // <!CDATA[
        /*
        $(document).ready(function () {
            $("#save_ArticleType").click(function () {
            
                //$('#myModal').modal('show');
            });

        });
        */

        $(document).ready(function () {
            $('.datepicker-field').attr("placeholder", "Date (dd/mm/yyyy)");
            $('.datepicker-field').datepicker();
            
        });

        function validateArticleType() {
            //alert($("#txt_OtherArticleType").val().trim());
            if ($("#txt_OtherArticleType").val().trim() == "") {
                alert("Other Article Type textbox Cannot be empty");
                return false;
            }
            return true;

        }
        function HR1_onclick() {

        }

        // ]]>
    </script>

</head>
<body>
    <form id="form1" runat="server">

      <%--  <p>Date: <input type="text" id="datepicker"></p>--%>


        <%-- edited by kshitij --%>

        <div class="container" style="padding-top: 30px;">
            <div class="row">
                <div class="col-lg-4">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/td_logo.jpg" />
                </div>

                <div class="col-lg-4 text-center">
                    <h1>Online Flow Sheet</h1>
                </div>
                <div class="col-lg-4 text-right">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/CompLogo.gif" Style="" />
                </div>
            </div>
            <div class="row" style="padding-top: 30px;">
                <div class="col-lg-3">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <asp:Label ID="Label1" runat="server" Text="JID" Style="width: 100%;"></asp:Label>
                        </div>
                        <div class="col-lg-9">
                            <asp:DropDownList ID="cmbJID" runat="server" Style="width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-lg-3">
                    <div class="form-group">
                        <div class="col-lg-2 text-right">
                            <asp:Label ID="Label2" runat="server" Style="width: 100%;" Text="AID"></asp:Label>
                        </div>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtAID" runat="server" OnTextChanged="TextBox9_TextChanged" Style="width: 100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <div class="col-lg-5 text-right">
                            <asp:Label ID="Label10" runat="server" Text="Publication details (vol/issue/page nrs)" Width="100%" Style="font-size: 12px;"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtVolIss" runat="server" OnTextChanged="txtVolIss_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-2">

                    <asp:Button ID="cmdView" runat="server" OnClick="cmdView_Click" CssClass="btn btn-primary" Text="View Info" Width="100%" />
                </div>


            </div>

            <div class="row" style="padding-top: 30px;">
                <div class="col-lg-6">
                    <div class="form-horizontal">
                        <div class="col-lg-2 text-left">
                            <asp:Label ID="Label9" runat="server"
                                Text="Title" CssClass="text-left"></asp:Label>
                        </div>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtTitle" runat="server" OnTextChanged="TextBox2_TextChanged"
                                Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="form-horizontal">
                        <div class="col-lg-3 text-left">
                            <asp:Label ID="Label6" runat="server"
                                Text="Authors" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtAuthors" runat="server" OnTextChanged="TextBox3_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>


            </div>
            <div class="row" style="padding-top: 30px;">

                <div class="col-lg-6">
                    <div class="form-horizontal">
                        <div class="col-lg-4 text-left">
                            <asp:Label ID="Label27" runat="server" Text="Open Access option"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtOAO" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>


                <div class="col-lg-6">
                    <div class="form-horizontal">
                        <div class="col-lg-5 text-left">
                            <asp:Label ID="Label8" runat="server" Text="Date of Receipt"  Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtReceiptDt" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>
            </div>


            <div class="row" style="padding-top: 30px;">
                <div class="col-lg-4">
                    <div class="form-horizontal">
                        <div class="col-lg-4 text-left">
                            <asp:Label ID="Label3" runat="server" Text="Description" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtJIDDescription" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-4">
                    <div class="form-horizontal">
                        <div class="col-lg-5 text-left">
                            <asp:Label ID="Label5" runat="server" Text="Typeset Pages" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtTypesetPages" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-4">
                    <div class="form-horizontal">
                        <div class="col-lg-5 text-left">
                            <asp:Label ID="Label26" runat="server" Text="Reminder Date" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:TextBox ID="txtReminderDate" runat="server" Width="100%" CssClass="datepicker-field"></asp:TextBox>

                        </div>
                    </div>

                </div>

            </div>
        </div>
        <hr style="border-color: #efefef;" />

        <%-- Updated form on 24 - Jan - 2017 --%>

        <div class="container" style="padding-top: 20px;">
            <div class="row">
                <div class="form-group">
                    <div class="col-lg-6">
                        <div class="col-lg-4">
                            <h5>Article Type</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="dd_ArticleType" runat="server" Style="width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="dd_ArticleType_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4 text-right">
                            <h5>Reprints</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txt_Reprints" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4">
                            <h5>PDF without WaterMark</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="dd_watermark" runat="server" Style="width: 100%;">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4 text-right">
                            <h5>Auto Correction Reminder</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txt_AuthorCR" MaxLength="25" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4">
                            <h5>Color Figure</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txt_ColorFigure" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4 text-right">
                            <h5>Special Issue Name</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txt_SpecialIssueName" MaxLength="25" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="col-lg-4">
                            <h5>Hard Copy</h5>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txr_HardCopy" MaxLength="20" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <hr style="border-color: #efefef;" />
        <div class="container" style="padding-bottom: 10px;">
            <div class="row">
                <div class="col-lg-6 text-center">
                    <h4>Date of PDF proof sent   </h4>
                </div>
                <div class="col-lg-6 text-center">
                    <h4>Date of corrections received </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label21" runat="server" Text="to authors" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txttoAuthors" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label18" runat="server" Text="from authors" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtfromAuthors" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>


                <div class="row" style="padding: 5px;">&nbsp;</div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label20" runat="server" Text="to Editor-in-Chief" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txttoEditor" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label17" runat="server" Text="from Editor-in-Chief" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtfromEditor" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>


                <div class="row" style="padding: 5px;">&nbsp;</div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label19" runat="server" Text="to Editorial Assistant" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txttoEditorial" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-4">
                            <asp:Label ID="Label16" runat="server" Text="from Editorial Assistant" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtfromEditorial" runat="server" CssClass="datepicker-field" OnTextChanged="txtfromEditorial_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <hr style="border-color: #efefef;" />

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <asp:Label ID="Label15" runat="server" Text="Special requirements from authors" Style="font-weight: 600; font-size: 110%;" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-9">
                            <asp:TextBox ID="txtSR" TextMode="MultiLine" runat="server" Width="100%" Height="55px"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-6">
                            <asp:Label ID="Label14" runat="server" Text="Confirmation Date to issue compilation from IOS" Style="font-size: 85%;" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtCompilationDt" CssClass="datepicker-field" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-6">
                            <asp:Label ID="Label13" runat="server" Text="Date of CRC delivery to IOS" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtCRCDt" runat="server" CssClass="datepicker-field" OnTextChanged="txtCRCDt_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="padding-top: 10px;">
                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-6">
                            <asp:Label ID="lblConfirmationDt" runat="server" Text="Confirmation Date of Print/Screen pdf" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtPrintConfirmationDt" runat="server" CssClass="datepicker-field" OnTextChanged="TextBox6_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <div class="col-lg-6">
                            <asp:Label ID="lbl1" runat="server" Text="Date of Print/Screen pdf sent to IOS" Width="100%"></asp:Label>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtPrintDtIOS" runat="server" CssClass="datepicker-field" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row" style="padding-top: 15px;">
                <div class="col-lg-12">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <asp:Label ID="Label22" runat="server" Text="Remarks from typesetter" Style="font-weight: 600; font-size: 110%;" Width="100%"></asp:Label>

                        </div>
                        <div class="col-lg-9">

                            <asp:TextBox ID="txtThomsonRemarks" TextMode="MultiLine" runat="server" Height="55px" OnTextChanged="txtThomsonRemarks_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <hr style="border-color: #efefef;" />

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h4>e-Mail Address</h4>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-4">
                    <div class="form-group">
                        <h6 class="col-lg-1">TO</h6>
                        <div class="col-lg-11">
                            <asp:TextBox ID="txtTo" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <h6 class="col-lg-1">CC </h6>
                        <div class="col-lg-11">
                            <asp:TextBox ID="txtCC" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <h6 class="col-lg-1">BCC </h6>
                        <div class="col-lg-11">
                            <asp:TextBox ID="txtBCC" runat="server" OnTextChanged="TextBox6_TextChanged" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <hr style="border-color: #efefef;" />

        <div class="container">

            <div class="row">

                <div class="col-lg-12">
                    <div class="form-group">
                        <div class="col-lg-3">
                            <asp:Button ID="cmdClear" runat="server" OnClick="Button1_Click" Text="Clear" Width="100%" CssClass="btn-primary btn" />
                        </div>
                        <%--
                        <div class="col-lg-2">
                            <asp:Button ID="cmdAdd" runat="server" Text="Add" OnClick="cmdAdd_Click" Width="100%" CssClass="btn-primary btn" />
                        </div>--%>

                        <div class="col-lg-3">
                            <asp:Button ID="cmdUpdate" runat="server" OnClick="cmdUpdate_Click" Text="Save" Width="100%" CssClass="btn-primary btn" />
                        </div>

                        <div class="col-lg-3">
                            <asp:Button ID="cmdReports" runat="server" OnClick="cmdReports_Click" Text="Reports" Width="100%" CssClass="btn-primary btn" />
                        </div>


                        <div class="col-lg-3">
                            <asp:Button ID="cmdLogout" runat="server" Text="Logout" OnClick="cmdLogout_Click" Width="100%" CssClass="btn-primary btn" />
                        </div>

                    </div>
                </div>

            </div>

        </div>

        <hr style="border-color: #efefef;" />


        <%-- Modal Window --%>
        <div class="container">
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Article Type (Others)</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="col-lg-12">
                                    <p>Please specify the article type</p>
                                </div>
                                <div class="col-lg-8">
                                    <asp:TextBox runat="server" ID="txt_OtherArticleType" MaxLength="20" CssClass="input-sm" Style="width: 100%;"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button runat="server" ID="save_ArticleType" CssClass="btn-primary btn-sm" Text="Save" OnClick="save_ArticleType_Click" OnClientClick="return validateArticleType();" Style="width: 100%;" />
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
