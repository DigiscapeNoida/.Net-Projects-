<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="FicheLanding.aspx.cs" Inherits="LexisNexis.EncyclopediasLanding" EnableEventValidation="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    
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
                    url: "http://" + ip + ":8081/Fiche.svc/data?ColumnName=" + colName,
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
                            // width: "25px",
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
     <script type = "text/javascript">
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

         function openBox() {

             $('.modal').css("display", "block");
                 
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


  </script>
    <style type="text/css">
        th.filter
        {
	        background: url(images/Down.png) 95% center no-repeat;
	        cursor:pointer;
        }
    </style>

     <style type="text/css">
        

        .btn {
            background: #428bca;
            border: #357ebd solid 1px;
            border-radius: 3px;
            color: #fff;
            display: inline-block;
            font-size: 14px;
            padding: 8px 15px;
            text-decoration: none;
            text-align: center;
            min-width: 60px;
            position: relative;
            transition: color .1s ease;
            /* top: 40em;*/
        }

            .btn:hover {
                background: #357ebd;
            }

            .btn.btn-big {
                font-size: 18px;
                padding: 15px 20px;
                min-width: 100px;
            }

        .btn-close {
            color: #aaa;
            font-size: 30px;
            text-decoration: none;
            position: absolute;
            right: 9px;
            top: 6px;
            color:#000!important;
        }

            .btn-close:hover {
                color: #919191;
            }

        .modal:before {
            content: "";
            display: none;
            background: rgba(0, 0, 0, 0.6);
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 10;
        }

        .modal:target:before {
            display: block;
        }

        .modal:target .modal-dialog {
            -webkit-transform: translate(0, 0);
            -ms-transform: translate(0, 0);
            transform: translate(0, 0);
            top: 20%;
        }

        .modal-dialog {
            background: #E1E1E1;
            border: #333 solid 1px;
            border-radius: 5px;
            margin-left: -200px;
            position: fixed;
            left: 50%;
            top: -100%;
            z-index: 11;
            width: 450px;
            -webkit-transform: translate(0, -500%);
            -ms-transform: translate(0, -500%);
            transform: translate(0, -500%);
            -webkit-transition: -webkit-transform 0.3s ease-out;
            -moz-transition: -moz-transform 0.3s ease-out;
            -o-transition: -o-transform 0.3s ease-out;
            transition: transform 0.3s ease-out;
            padding:16px;
            box-shadow:0px 0px 5px 2px #888;
        }

         .modal-footer .red_btn
         {
            margin: 0 0 0 9px
         }

         .modal-header h2{ text-align:center;}

        .modal-body {
            padding: 20px;
        }

         .modal-body table tr td span
         {font-family: "open_sanssemibold";
          
display: inline-block;
         }

        .modal-header,
        .modal-footer {
            padding: 10px 20px;
        }

        .modal-header {
            border-bottom: #ccc solid  1px;
        }

            .modal-header h2 {
                font-size: 20px;
            }

        .modal-footer {
            border-top: #ccc solid 1px;
            padding:16px 5px 5px 5px;
            text-align: right;
        }
        /*ADDED TO STOP SCROLLING TO TOP*/
        #close {
            display: none;
        }

         .modal .star
         { color:red;
         }


    </style>
    <div class="body_container">

<div class="wrapper">

<div class="page_content">

    

<div class="page_header">

<asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="tp_info_btn" OnClick="btnLogout_Click" />

    <small id='osx-modal'><a href='#' class='osx tp_info_btn' style="color:#fff" id="anchor1" ><asp:Label ID="changepassword" runat="server" text=""></asp:Label></a></small>

    

    <strong class="spacer_br"></strong>

<asp:Label ID="lblname" runat="server" Text="" ></asp:Label><asp:Label ID="lblUserName"  runat="server" Text ="connected Username"></asp:Label>
    <div style="padding:0px 0px 0px 0px"> </div> 

</div>

<div class="sub_page_header">

<div class="box2">
    <asp:Button ID="btnSubmit" runat="server" CssClass="button_one" Visible="false" Text="Insert" OnClick="btnSubmit_Click" />
<asp:Button ID="btnExportExcel" runat="server" CssClass="button_one" Text="export this page" OnClick="btnExportExcel_Click" />
<asp:Button ID="btnExportExcelAll" runat="server" CssClass="button_one" Text="Export All" OnClick="btnExportExcelAll_Click" />
</div>

<div class="box1">
<asp:Label ID="lblTask" Text="Task" runat="server" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddltask" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddltask_SelectedIndexChanged" Visible="false"></asp:DropDownList>
</div>
</div>

<div class="table_container">
<div class="table_heading">
<div class="box1">

<div class="col">

    <asp:Label ID="lblChoseProductencyclo" Text="Choisissez votre produit" runat="server"></asp:Label>
     <asp:DropDownList ID="ddlProduct" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" ></asp:DropDownList>
</div>

<div class="col">

<asp:Label ID="lblcollection" Text="collection" runat="server" Visible="false"></asp:Label>
 <asp:DropDownList ID="ddlCollection" runat="server" CssClass="select_1" Visible="false" OnSelectedIndexChanged="ddlCollection_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
</div>

<div class="col">

    <asp:Label ID="lblWriting" Text="Writing" runat="server" Visible="false"></asp:Label>
 <asp:DropDownList ID="ddlwriting" runat="server" Visible="false" CssClass="select_1" OnSelectedIndexChanged="ddlwriting_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
     <asp:Button ID="btnLookfor" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnLookfor_Click" />
    <asp:Button ID="btnremovefilter" runat="server" CssClass="button_one" Text="Remove all filters"  Visible="true" OnClick="btnremovefilter_Click"  />
     <asp:Button ID="btnRefresh" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnRefresh_Click"  />
</div>

<div class="box2">
    <asp:Button ID="btnlncomplete" runat="server" CssClass="button_one" Visible="false" Text="Complete" OnClick="btnlncomplete_Click" />
    <asp:Button ID="btntdcomplete" runat="server" CssClass="button_one" Visible="false" Text="Complete" OnClick="btntdcomplete_Click" />
  <asp:Button ID="btnsendprod" runat="server" CssClass="button_one" Visible="false" Text="Send prod" OnClick="btnsendprod_Click" />
    <asp:Button ID="btnremove" runat="server" CssClass="button_one" Visible="false" Text="Remove" OnClick="btnremove_Click"  OnClientClick="Confirm()"   />
    <asp:Button ID="openButton" runat="server" CssClass="button_one" Text="Envoyer fiche(s) en prod"   OnClick="openButton_Click" /></div>


</div>

  <div style="height:10px"></div>
<div id="divFilter"></div>
    <div  class="table_content">
        <asp:GridView ID="grdViewOrders" runat="server"
            AllowPaging="True" AutoGenerateColumns="False" TabIndex="1"
            DataKeyNames="FID" Width="100%" GridLines="None"
            CellPadding="0" CellSpacing="0" AllowSorting="true" BorderWidth="1px" 
            onpageindexchanging="grdViewOrders_PageIndexChanging" OnSorting="grdViewOrders_Sorting" OnRowCommand="grdViewOrders_RowCommand" OnRowDataBound="grdViewOrders_RowDataBound">
            <Columns>
                <asp:BoundField DataField="FID" HeaderText="ID" 
                    SortExpression="FID" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                  <asp:BoundField DataField="FTITLE" HeaderText="Title Fesc" 
                    SortExpression="FTITLE" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="FOLIO" HeaderText="No Folio" 
                    SortExpression="FOLIO" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="DEMANDTYPE" HeaderText="Nature of demand" 
                    SortExpression="DEMANDTYPE"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="tat" HeaderText="Time limit" 
                    SortExpression="tat"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="ITERATION" HeaderText="Iteration" 
                    SortExpression="ITERATION" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                
                <asp:BoundField DataField="INDATE" HeaderText="In date" 
                    SortExpression="INDATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="DUEDATE" HeaderText="Due date" 
                    SortExpression="DUEDATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="DELIVERED_DATE" HeaderText="complete date" 
                    SortExpression="DELIVERED_DATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>

                <asp:BoundField DataField="PAGECOUNT" HeaderText="Number of Pages" 
                    SortExpression="PAGECOUNT" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                   <asp:BoundField DataField="STAGE" HeaderText="Status" 
                    SortExpression="STAGE" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                 <asp:BoundField DataField="fullname" HeaderText="full name" 
                    SortExpression="fullname" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
             
                <asp:TemplateField HeaderText="comment">
                   <ItemTemplate>
                  <asp:ImageButton ID="img1" runat="server" CommandArgument='<%# Eval("comments") %>' CommandName="comment" ImageUrl="~/images/icon-comment.png" />
                       </ItemTemplate>
               </asp:TemplateField>
                 <asp:BoundField DataField="combi" HeaderText="Combi" 
                    SortExpression="combi" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:BoundField DataField="codecoll" HeaderText="Code coll." 
                    SortExpression="codecoll" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:BoundField DataField="nummac" HeaderText="Num. mac." 
                    SortExpression="nummac" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:BoundField DataField="artfas" HeaderText="Art. fas." 
                    SortExpression="artfas" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:BoundField DataField="numfas" HeaderText="Num. fasc" 
                    SortExpression="numfas" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:TemplateField HeaderText="sgm file">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnksgmlfile" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="sgmlDownload" Text='<%# Eval("Sgm_Filename") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>
              
               <asp:TemplateField HeaderText="attachments">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="Download" Text='<%# Eval("filesname") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>
                     <asp:TemplateField HeaderText="TD attachments">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkTDDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="TDDownload" Text='<%# Eval("tdfilename") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Error file">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkerrorfile" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Sgm_Filename") %>'  
                                            CommandName="errorfile" Text="" />  
                                    </ItemTemplate>  
                                </asp:TemplateField> 
               <asp:TemplateField HeaderText="task">  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk" runat="server" ToolTip='<%# Eval("STAGE")%>' />
                            <asp:Literal ID="litID" runat="server" Text='<%# Eval("FID")%>' Visible="false"></asp:Literal>
                          <asp:Literal ID="litfrid" runat="server" Text='<%# Eval("FRID")%>' Visible="false"></asp:Literal>
                        <asp:Literal ID="userid" runat="server" Text='<%# Eval("userid")%>' Visible="false"></asp:Literal>
                         <asp:Literal ID="litsgmlfile" runat="server" Text='<%# Eval("Sgm_Filename")%>' Visible="false"></asp:Literal>
                    </ItemTemplate>  
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Action">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkvalidate" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="Validate" Text="Valider" Visible="false" OnClientClick = "return confirm('Validez-vous cet item ?')" OnClick="ValidateFile" /> &nbsp;&nbsp;
                                         <asp:LinkButton ID="lnkcancel" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="rework" Text="Correction" Visible="false" OnClick="CancelFile" />  &nbsp;&nbsp;
                                         <asp:LinkButton ID="lnktdupload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FRID") %>'  
                                            CommandName="Upload" Text="Upload" Visible="false"  /> 
                                          <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FID") %>'  
                                            CommandName="Edit1" Text="Edit" Visible="false" OnClick="UpdateFile" /> 
                                    </ItemTemplate>  
                                </asp:TemplateField>  
            </Columns>
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="table_paging"/>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle  BackColor="#D85354" Font-Bold="True" ForeColor="#fff"/>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
        <asp:Button ID="Refresh" runat="server" Text="" OnClick="Refresh_Click" Visible="true" />
        <asp:HiddenField ID="hndSelectedValue" runat="server" Value="" />
    </div>


</div>



</div>


</div>
        
</div>
    <!-- Modal -->
            <div class="modal" id="modal-one" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-header">
                        <h2>Envoyer le(s) fiche(s) en production</h2>
                        <a href="#close" class="btn-close" aria-hidden="true">×</a>
                        <!--CHANGED TO "#close"-->
                    </div>
                    <div class="modal-body">

                        <table width="100%">
                            <tr width="100%">
                                 <td width="30%"><asp:Label ID="lblitemhead" Text="Votre sélection" runat="server"></asp:Label></td>
                                 <td width="70%"><asp:Label ID="lbllistfilename" Text="" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="height:10px" colspan="2"></td>
                            </tr>
                            <tr width="100%">
                                <td width="30%"> <asp:Label ID="lblNotificationforsupe" Text="Notification for sup" runat="server"></asp:Label></td>
                                <td width="70%"> <label ><asp:TextBox runat="server" ID="txtnotification" style="padding:5px; width:100%;" ></asp:TextBox></label></td>
                            </tr>
                            <tr width="100%">
                               <td width="30%"> <asp:Label ID="lblCommentejournal" Text="Comment" runat="server"></asp:Label></td>
                                 <td width="70%">     <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox></td>
                            </tr>

                            <tr width="100%">
                                <td width="30%"> <asp:Label ID="lblLoadaFileefiche" Text="Load a file" runat="server"></asp:Label></td>
                                <td width="70%"> <asp:FileUpload ID="FileUpload1" runat="server" style="width:100%; padding:10px;" /></td>
                            </tr>
                           
                        </table>
                        
                        <br />
                        
                    </div>
                    <div class="modal-footer">
                          <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" Text="Close" OnClientClick="window.location='#close';  return false;" />
                        &nbsp;
                        <asp:Button ID="btnSend" runat="server" CssClass="red_btn" Text="save file" style="text-decoration:none" onclick="btnSend_Click" />
                       &nbsp;   &nbsp;
                        <!--CHANGED TO "#close"-->
                    </div>
                </div>
            </div>
        
        <!-- /Modal -->


     <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
       
        <tr>
            <td colspan="2">
                <div id='container'>
                    <div id='content'>
                        <!-- modal content -->
                        <div id="osx-modal-content">
                            <div id="osx-modal-title">
                                <asp:Label ID="dossierchangepwdhead" runat="server" Text="Change Password"></asp:Label></div>
                            <div class="close">
                                <a href="#" class="simplemodal-close">x</a></div>
                            <div id="osx-modal-data">
                                <div style="background-color: #fff; width: 370px; margin: 0px auto">
                                    <table width="370" border="0" align="center" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="width: 100%; font-weight: normal">
                                              
                                                
                                            </td>
                                        </tr>
                                        <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td>
                                          
                                                <label for="User Id">
                                                  <asp:Label ID="dossieroldpwd" runat="server" Text="Old Password:"></asp:Label><font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <input id="oldpassword" maxlength="100"  size="20" type="text" tabindex="1" runat="server"
                                                    />
                                                    <input id="uid" maxlength="100" name="uid" size="20" type="text" tabindex="1"
                                                    value="" visible="false" runat="server" />
                                                     <input type="hidden" id="hidDivId" runat="server" />
                                            </td>
                                        </tr>
                                         <tr><td colspan="2" style="height:5pt"></td> </tr>
                                        <tr>
                                            <td>
                                                <label for="password ">
                                                  <asp:Label ID="lblnewpwd" runat="server" Text="New Password:"></asp:Label> <font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <input id="newpassword" maxlength="100" name="newpassword" size="20" type="password" tabindex="2"
                                                    value="" runat="server" />
                                            </td>
                                        </tr>
                                         <tr><td colspan="2" style="height:5pt"></td> </tr>
                                          <tr>
                                            <td>
                                                <label for="password ">
                                                  <asp:Label ID="lblconfirmnewpwd" runat="server" Text="Confirm New Password:"></asp:Label> <font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <input id="newpassword1" maxlength="100" name="newpassword1" size="20" type="password" tabindex="2"
                                                    value="" runat="server" />
                                            </td>
                                        </tr>
                                     <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <div>
                                                <asp:Button ID="btnchangepassword" runat="server" Text="Reset Password" OnClientClick="loginclick();" />
                                                  
                                                  
                                                </div>
                                            </td>
                                        </tr>
                                         <tr><td colspan="2" style="height:10pt"></td> </tr>
                                         <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:Label ID="lblchange" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <div id="lblmsg" style="color: Red; ">
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
</asp:Content>

