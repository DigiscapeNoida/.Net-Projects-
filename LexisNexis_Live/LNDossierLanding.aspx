<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="LNDossierLanding.aspx.cs" Inherits="LNDossierLanding" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="Server">

    <!--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />-->
      
   <!-- <link href="Stylesheet/css/bootstrap.css" rel="stylesheet" type="text/css" /> !--->
   
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
 
    <script language="javascript" type="text/javascript">

   
        $(document).ready(function () {
            $('.filter').click(function () {

                var pos = $(this).position();
                var width = $(this).outerWidth();
                var height = $(this).outerHeight();
                var colName = $(this).attr("alt");

                jQuery.support.cors = true;
                var ip = window.location.hostname;

                $.ajax({

                    type: "GET",
                   // url: "http://" + ip + ":49430/Service1.svc/data?ColumnName=" + colName,
                    url: "http://" + ip + ":8081/Service1.svc/data?ColumnName=" + colName,
                    processData: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    success: function (response) {

                        var filterdiv = $("<div id='divFilterList'></div>");
                        var div = $("<div></div>");
                        var valuesdiv = $("<div id='filterChkBox'></div>");
                        var table = $("<table />");
                        var SelectedValue = {};
                        var hiddenControlValue = $('#MainContent_hndSelectedValue').val();

                        if (hiddenControlValue.length > 0) {
                            var alreadySelectedValueInfo = jQuery.parseJSON(hiddenControlValue);
                            for (var index = 0; index < alreadySelectedValueInfo.DataCollection.length; index++) {
                                if (alreadySelectedValueInfo.DataCollection[index].ColumnName == colName) {
                                    SelectedValue = alreadySelectedValueInfo.DataCollection[index].SelectedValue;
                                }
                            }
                        }

                        $.each(response, function (index, obj) {
                            var IsSelected = false;
                            for (var index = 0; index < SelectedValue.length; index++) {
                                if (SelectedValue[index] == obj.Id)
                                    IsSelected = true;
                            }

                            if (IsSelected == true)
                                table.append("<tr><td><input name='options' checked='checked' type='checkbox' value='" + obj.Id + "'>" + obj.Value + "</td></tr>");
                            else
                                table.append("<tr><td><input name='options' type='checkbox' value='" + obj.Id + "'>" + obj.Value + "</td></tr>");
                        });

                        valuesdiv.append(table);
                        div.append(valuesdiv);
                        filterdiv.append(div);
                        $('body').append(filterdiv);

                        var buttondiv = $("<div></div>").css({
                            textAlign: "right",
                            paddingTop: "5px",
                            paddingRight: "5px"
                        }).show();

                        var clearFilterBtn = $("<button>.</button>");
                        clearFilterBtn.css({
                            height: "22px",
                            width: "25px",
                            border: "none",
                            background: "url(images/ClearFilter.png) center center no-repeat",
                            cursor: "pointer"
                        });
                        buttondiv.append(clearFilterBtn);

                        var okBtn = $("<button>OK</button>");
                        okBtn.css({ height: "22px", width: "70px" });
                        buttondiv.append(okBtn);

                        var cancelBtn = $("<button>Cancel</button>");
                        cancelBtn.css({ height: "22px", width: "70px" });
                        buttondiv.append(cancelBtn);

                        cancelBtn.click(function () {
                            var container = $("#divFilterList");
                            container.detach();
                        });

                        okBtn.click(function () {

                            var SelectedDataCollection = {};

                            var ArrSelectedColumnsInfo = new Array(); // It hold all column selected info

                            var SelectedData = {};
                            SelectedData["ColumnName"] = colName;

                            var ArrSelectedValue = new Array();
                            $('#filterChkBox table tr td input:checked').each(function () {
                                ArrSelectedValue.push($(this).val());

                            });
                            SelectedData["SelectedValue"] = ArrSelectedValue;

                            var IsExist = false;
                            var alreadySelectedValueInfo;

                            if (hiddenControlValue.length > 0) {
                                var alreadySelectedValueInfo = jQuery.parseJSON(hiddenControlValue);
                                for (var index = 0; index < alreadySelectedValueInfo.DataCollection.length; index++) {
                                    if (alreadySelectedValueInfo.DataCollection[index].ColumnName == colName) {
                                        alreadySelectedValueInfo.DataCollection[index] = SelectedData;
                                        SelectedDataCollection = alreadySelectedValueInfo;
                                        IsExist = true;
                                        break;
                                    }
                                }
                            }

                            if (IsExist == false) {
                                var alreadySelectedValueInfo = new Array();
                                if (hiddenControlValue.length > 0) {
                                    alreadySelectedValueInfo = jQuery.parseJSON(hiddenControlValue);
                                    if (SelectedData.SelectedValue.length > 0)
                                        alreadySelectedValueInfo["DataCollection"].push(SelectedData);
                                    SelectedDataCollection = alreadySelectedValueInfo;
                                }
                                else {
                                    alreadySelectedValueInfo.push(SelectedData);
                                    SelectedDataCollection["DataCollection"] = alreadySelectedValueInfo;
                                }
                            }

                            $('#<%= hndSelectedValue.ClientID %>').val(JSON.stringify(SelectedDataCollection));
                            document.getElementById('<%= Refresh.ClientID %>').click();
                        });

                        clearFilterBtn.click(function () {
                            if (hiddenControlValue.length > 0) {
                                var alreadySelectedValueInfo = jQuery.parseJSON(hiddenControlValue);
                                for (var index = 0; index < alreadySelectedValueInfo.DataCollection.length; index++) {
                                    if (alreadySelectedValueInfo.DataCollection[index].ColumnName == colName) {
                                        alreadySelectedValueInfo.DataCollection[index].selectedValue = new Array();
                                        $('#<%= hndSelectedValue.ClientID %>').val(JSON.stringify(alreadySelectedValueInfo));
                                        break;
                                    }
                                }
                            }
                            document.getElementById('<%= Refresh.ClientID %>').click();
                        });

                        filterdiv.append(buttondiv);

                        var tblwidth = table.outerWidth();
                        valuesdiv.css({ width: tblwidth + "px" }).show();

                        div.css({
                            overflow: "auto",
                            textAlign: "left",
                            paddingLeft: "5px",
                            paddingTop: "1px",
                            marginLeft: "1px",
                            marginTop: "1px",
                            backgroundColor: "White",
                            border: "1px solid gray",
                            height: "207px",
                            width: "200px"
                        }).show();

                        var left = 0;
                        if (pos.left + width - 200 - 10 < 0)
                            left = 0;
                        else
                            left = pos.left + width - 200 - 10;

                        filterdiv.css({
                            position: "absolute",
                            textAlign: "left",
                            backgroundColor: "white",
                            border: "1px solid black",
                            color: "black",
                            height: "243px",
                            width: "209px",
                            top: pos.top + height + 1 + "px",
                            left: left + "px"
                        }).show();
                    },
                    error: function (xhr, status, error) {
                        alert(error);
                    }
                });

            });
            $(document).mouseup(function (e) {
                var container = $("#divFilterList");

                if (container.has(e.target).length === 0) {
                    container.detach();
                }
            });
        });

 
    </script>


    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Validez-vous cette(s) suppression(s) ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">

        function loginclick() {
            //  alert(document.getElementById('MainContent_oldpassword').value);

            if (document.getElementById('MainContent_oldpassword').value == "") {
                alert('Insérer ancien mot de passe');
                return;
            }
            else if (document.getElementById('MainContent_newpassword').value == "") {
                alert('Ce champ ne peut pas être vide');
                return;
            }


            if (document.getElementById('MainContent_newpassword').value != document.getElementById('MainContent_newpassword1').value) {
                alert('les mots de passe saisis doivent être identiques');
                return;
            }


            var userid = document.getElementById('MainContent_hidDivId');
            // alert(userid.value);
            var password = document.getElementById('MainContent_oldpassword');
            var newpassword = document.getElementById('MainContent_newpassword');
            // alert(userid);
            //  var ddd=document.getElementById('hidDivId');
            //  alert(ddd.value);
            PageMethods.RegisterUser(userid.value, password.value, newpassword.value, OnSuccess, onError);

            // 

            function OnSuccess(response) {
                // alert(response.toString());
                document.getElementById('MainContent_lblchange').innerHTML = response.toString();

                document.getElementById('MainContent_oldpassword').value = "";
                document.getElementById('MainContent_newpassword').value = "";
                document.getElementById('MainContent_newpassword1').value = "";


            }
            function onError(response) {
                alert('Something wrong.');
            }
        }

        function loginclick1() {

            // alert(document.getElementById('MainContent_comment').value);
            if (document.getElementById('MainContent_comment').value == "") {
                alert('Insérer commentaire');
                return;
            }


            var comment = document.getElementById('MainContent_comment');
            var did = document.getElementById('MainContent_hiddendidval');
            // alert(comment.value);
            //  alert(did.value);
            PageMethods.InsertComment(comment.value, did.value, OnSuccess, onError);


            function OnSuccess(response) {

                //document.getElementById('ContentPlaceHolder1_lblchange').innerHTML = response.toString();
                document.getElementById('MainContent_comment').value = "";
                document.getElementById('MainContent_lbladdcomment').innerHTML = response.toString();

            }
            function onError(response) {
                alert('Something wrong.');
            }
        }
        function clickev(DID) {

            document.getElementById('MainContent_hiddendidval').value = DID;

            PageMethods.getComment(DID, OnSuccess, onError);
            function OnSuccess(response) {

                //document.getElementById('ContentPlaceHolder1_lblchange').innerHTML = response.toString();
                document.getElementById('MainContent_comment').value = response.toString();


            }
            function onError(response) {
                document.getElementById('MainContent_comment').value = "";
            }
        }
    </script>

    <style type="text/css">
        th.filter {
            background: url(images/Down.png) 95% center no-repeat;
            cursor: pointer;
        }
    </style>


    <div class="body_container">

        <div class="wrapper">

            <div class="page_content">
                <div class="page_header">

                    
                     <asp:Button ID="BtnUsers"  runat="server" CssClass="prdct_btn" Text="Create User" OnClick="BtnUsers_Click" Visible="True" />
                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="tp_info_btn" OnClick="btnLogout_Click" />

                    <small id='osx-modal'><a href='#' class='osx tp_info_btn' style="color: #fff" id="anchor1">
                        <asp:Label ID="changepassword" runat="server" Text=""></asp:Label></a></small>
                    <strong class="spacer_br"></strong>

                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label><asp:Label ID="lblUserName" runat="server" Text="connected Username"></asp:Label>
                    <div style="padding: 0px 0px 0px 0px"></div>
                </div>

                <div class="sub_page_header">

                    <div class="box2">
                       
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button_one" Text="Insert" OnClick="btnSubmit_Click" Visible="False" />
                        <asp:Button ID="btnExportExcel" runat="server" CssClass="button_one" Text="export this page" OnClick="btnExportExcel_Click" />
                        <asp:Button ID="btnExportExcelAll" runat="server" CssClass="button_one" Text="Export All" OnClick="btnExportExcelAll_Click" />
                    </div>

                    <div class="box1">
                        <asp:Label ID="lblTask" Text="Task" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddltask" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddltask_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>


                <div class="table_container">
                    <div class="table_heading">
                        <div class="box1">

                            <div class="col">
                                <asp:Label ID="lblChooseProduct" Text="Choisissez votre produit :" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col">
                                <asp:Label ID="lblDeclination" Text="Declination" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_1" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlCollection_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col">
                                <asp:Label ID="lblWriting" Text="Writing" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlwriting" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlwriting_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col">
                                <asp:Label ID="Label1" Text="Stage" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlstage" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlstage_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                    <asp:ListItem>Show All Item</asp:ListItem>
                                    <asp:ListItem>Send for Production</asp:ListItem>
                                    <asp:ListItem>Article Completed</asp:ListItem>
                                    <asp:ListItem>Article in process</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnLookfor" runat="server" CssClass="button_one" Text="Search" OnClick="btnLookfor_Click" Visible="true" />
                            <asp:Button ID="btnremovefilter" runat="server" CssClass="button_one" Text="Remove all filters" Visible="true" OnClick="btnremovefilter_Click" />
                            <asp:Button ID="btnRefresh" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnRefresh_Click" />
                        </div>

                        <div class="box2">
                            <asp:Button ID="Btn_Cancel" runat="server" CssClass="button_one" Text="Cancel   " Visible="true" OnClick="Btn_Cancel_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="Btn_Delete" runat="server" CssClass="button_one" Text="Delete" Visible="true" OnClick="Btn_Delete_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btnComplete" runat="server" CssClass="button_one" Text="Complete" Visible="false" OnClick="btnComplete_Click" />
                            <asp:Button ID="btnsendprod" runat="server" CssClass="button_one" Text="Send prod" Visible="false" OnClick="btnsendprod_Click" />
                           

                        </div>
                    </div>

                    <div style="height: 10px;"></div>
                    <div id="divFilter"></div>
                    <div class="table_content">

                        <asp:GridView ID="grdViewOrders" runat="server"
                            AllowPaging="True" AutoGenerateColumns="False" TabIndex="1"
                            DataKeyNames="DID" Width="100%" GridLines="None"
                            CellPadding="0" CellSpacing="0" AllowSorting="true" BorderWidth="1px"
                            OnPageIndexChanging="grdViewOrders_PageIndexChanging" OnSorting="grdViewOrders_Sorting" OnRowCommand="grdViewOrders_RowCommand" OnRowDataBound="grdViewOrders_RowDataBound">
                            <Columns>

                                <asp:BoundField DataField="DID" HeaderText="ID" ReadOnly="true"
                                    SortExpression="DID">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DECLINATION" HeaderText="Declination"
                                    SortExpression="DECLINATION" Visible="false">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CTITLE" HeaderText="Folder Title"
                                    SortExpression="CTITLE">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Author" HeaderText="Author" Visible="true"
                                    SortExpression="Author">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DEMANDTYPE" HeaderText="Demand Type"
                                    SortExpression="DEMANDTYPE" Visible="false">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DURATION" HeaderText="Duration"
                                    SortExpression="DURATION">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITERATION" HeaderText="Iteration"
                                    SortExpression="ITERATION">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INDATE" HeaderText="In date"
                                    SortExpression="INDATE" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DUEDATE" HeaderText="Return Date"
                                    SortExpression="DUEDATE" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Delivered_Date" HeaderText="complete date"
                                    SortExpression="Delivered_Date" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAGECOUNT" HeaderText="Page Count"
                                    SortExpression="PAGECOUNT">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STAGE" HeaderText="Stage"
                                    SortExpression="STAGE">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fullname" HeaderText="Userid"
                                    SortExpression="fullname">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="comment">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="img1" Visible="false" runat="server" CommandArgument='<%# Eval("remarks") %>' CommandName="comment" ImageUrl="~/images/icon-comment.png" />
                                         <asp:Literal ID="litimg" runat="server" Text='<%# Eval("remarks")%>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachments">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'
                                            CommandName="Download" Text='<%# Eval("filename") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'
                                            CommandName="delete1" Text="Annuler" Visible="false" OnClick="deleteFile" OnClientClick="return confirm('Confirmez-vous cette annulation ?')" />
                                        <asp:CheckBox ID="chk" runat="server" ToolTip='<%# Eval("STAGE")%>' Visible="false" />
                                        <asp:Literal ID="stageid" runat="server" Text='<%# Eval("STAGE")%>' Visible="false"></asp:Literal>
                                        <asp:Literal ID="litID" runat="server" Text='<%# Eval("DID")%>' Visible="false"></asp:Literal>
                                        <asp:Literal ID="userid" runat="server" Text='<%# Eval("userid")%>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="commentaire" Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'
                                            CommandName="Edit1" Text="Edit" Visible="false" OnClick="editfile" />
                                        <asp:LinkButton ID="lnkcomment1111" CssClass="osx1" runat="server" Text="commentaire" OnClientClick=<%# "clickev('"+ Eval("DID")+"'); " %>></asp:LinkButton>
                                        <asp:Literal ID="ttt" runat="server" Text='<%# Eval("DID")%>' Visible="false"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- Edited by Kshitij 22-Feb-2017 for edit and update delete and cancel --%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Edit" runat="server" CssClass="gry_btn" Text="modifier" CommandName="showModal" CommandArgument='<%# Eval("DID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="table_paging" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#D85354" Font-Bold="True" ForeColor="#fff" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>


                        <asp:Button ID="Refresh" runat="server" Text="" OnClick="Refresh_Click" Visible="true" />
                        <asp:HiddenField ID="hndSelectedValue" runat="server" Value="" />
                    </div>


                </div>



            </div>


        </div>

    </div>
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">

        <tr>
            <td colspan="2">
                <div id='container'>
                    <div id='content'>
                        <!-- modal content -->
                        <div id="osx-modal-content">
                            <div id="osx-modal-title">
                                <asp:Label ID="dossierchangepwdhead" runat="server" Text="Change Password"></asp:Label>
                            </div>
                            <div class="close">
                                <a href="#" class="simplemodal-close">x</a>
                            </div>
                            <div id="osx-modal-data">
                                <div style="background-color: #fff; width: 370px; margin: 0px auto">
                                    <table width="370" border="0" align="center" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="width: 100%; font-weight: normal"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <label for="User Id">
                                                    <asp:Label ID="dossieroldpwd" runat="server" Text="Old Password:"></asp:Label><font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <input id="oldpassword" maxlength="100" size="20" type="text" tabindex="1" runat="server" />
                                                <input id="uid" maxlength="100" name="uid" size="20" type="text" tabindex="1"
                                                    value="" visible="false" runat="server" />
                                                <input type="hidden" id="hidDivId" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5pt"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label for="password ">
                                                    <asp:Label ID="lblnewpwd" runat="server" Text="New Password:"></asp:Label>
                                                    <font size="1" color="red">*</font>
                                                </label>
                                            </td>
                                            <td>
                                                <input id="newpassword" maxlength="100" name="newpassword" size="20" type="password" tabindex="2"
                                                    value="" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 5pt"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label for="password ">
                                                    <asp:Label ID="lblconfirmnewpwd" runat="server" Text="Confirm New Password:"></asp:Label>
                                                    <font size="1" color="red">*</font>
                                                </label>
                                            </td>
                                            <td>
                                                <input id="newpassword1" maxlength="100" name="newpassword1" size="20" type="password" tabindex="2"
                                                    value="" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <div>
                                                    <asp:Button ID="btnchangepassword" runat="server" Text="Reset Password" OnClientClick="loginclick();" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:Label ID="lblchange" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <div id="lblmsg" style="color: Red;">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>


    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">

        <tr>
            <td colspan="2">
                <div id='container1'>
                    <div id='Div1'>
                        <!-- modal content -->
                        <div id="osx-modal-content1">
                            <div id="osx-modal-title1">
                                <asp:Label ID="lbldossiercommenthead" runat="server" Text="Insert Comment"></asp:Label>
                            </div>
                            <div class="close">
                                <a href="#" class="simplemodal-close">x</a>
                            </div>
                            <div id="osx-modal-data1">
                                <div style="background-color: #fff; width: 370px; margin: 0px auto">
                                    <table width="370" border="0" align="center" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="width: 100%; font-weight: normal"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <label for="User Id">
                                                    <asp:Label ID="lbldossiercomment" runat="server" Text="Insert Comment"></asp:Label><font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="comment" TextMode="MultiLine" Width="250px" Height="100px" runat="server"></asp:TextBox>
                                                <input id="Text2" maxlength="100" name="uid" size="20" type="text" tabindex="1"
                                                    value="" visible="false" runat="server" />
                                                <input type="hidden" id="Hidden1" runat="server" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <div>
                                                    <asp:Button ID="btnaddcomment" runat="server" Text="Reset Password" OnClientClick="loginclick1();" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 10pt"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:Label ID="lbladdcomment" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <input type="hidden" id="hiddendidval" runat="server" />
                                                <div id="Div2" style="color: Red;">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>

     <%-- Edited by kshitij 24 Feb 2017, Modal window--%>

    <style>
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
      /*  .modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 60%;
        } */

        /* The Close Button */
        .close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
            margin:-10px;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

       
    </style>

    <!-- The Modal -->
    <div id="myModal" class="modal">

        <!-- Modal content -->
        <div class="modal-content" style="height:420px;">
            <span class="close" onclick="Close()">&times;</span>
            
                
                    <asp:Label ID="lblAdmindossierupdate" Text="Edit Dossier Article" runat="server"></asp:Label>
                
            
                <div class="row">
                    <div class="col">
                    <asp:Label ID="lblAdmindossierid" Text="ID" runat="server"></asp:Label>
                        </div>
                       
                    <div class="col"> 
                    <asp:Label ID="Txt_ID" runat="server" CssClass="form-control input-sm text-center"></asp:Label>
                      </div>

                    </div>      
                    
            <div class="row"> 
                    
                <div class="col"> 
             <asp:Label ID="lblFolderTitle" Text="Folder Title" runat="server"></asp:Label>
                    </div>
                       
                       <div class="col"> 
                           <asp:TextBox ID="txtfoldertitle" runat="server" CssClass="input_2" ></asp:TextBox>
                        </div>

                   </div>
                        
                       <div class="row">
                            <div class="col">
                            <asp:Label ID="lblAuthor" Text="Author" runat="server"></asp:Label>
                           </div>
                        
                       <div class="col">
                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="input_2"></asp:TextBox>
                       </div>

                           </div>

                          <div class="row">
                              <div class="col">
                            <asp:Label ID="lblmailnotification" Text="Additional mail notification" runat="server" Visible="false"></asp:Label>
                           </div>

                              <div class="col">
                          <asp:TextBox ID="txtmailnotification" runat="server" CssClass="input_2" Visible="false"></asp:TextBox>
                                  </div>
                        </div>

                    
             <div class="row">
                 <div class="col">
                            <asp:Label ID="lblComment" Text="Comment" runat="server"></asp:Label>
                        </div>
                       
                 <div class="col">
                          <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="textarea_1"></asp:TextBox>
                     </div>

                      </div>

            <div class="row">
                 <asp:Label ID="lblLoadafile" Text="Load a file" runat="server"></asp:Label>
                            <asp:FileUpload ID="FileUpload1" CssClass="fileUpload"
                                runat="server"></asp:FileUpload><br />
                            <%--OnClick="UploadButton_Click"--%>
                            <asp:Button ID="UploadButton"
                                Text="Upload file"
                                OnClick="UploadButton_Click"
                                CssClass="btn btn-warning"
                                runat="server" Visible="false"></asp:Button>
                    
                             </div>
            <div class="row">

                <div class="col">
                            <asp:Label ID="UploadStatusLabel"
                                runat="server">
                            </asp:Label>
                    </div>

               

                </div>       

                

            <div class="row center">
                
                    <asp:Button ID="Btn_Save" runat="server" CssClass="btn-primary btn red_btn" Text="Save Changes" OnClick="Btn_Save_Click" />
                    <asp:Button ID="Btn_Close" runat="server" CssClass="btn-danger btn red_btn" Text="Close" OnClick="Btn_Close_Click" />
                
           </div>

            </div>

        </div>

        

        <script type="text/javascript">



            function Close() {
                var modal = document.getElementById('myModal');
                modal.style.display = "none";
            }


            function showModal() {
                //alert(2);
                var modal = document.getElementById('myModal');
                modal.style.display = "block";
                return false;
            }

           


        </script>
</asp:Content>

