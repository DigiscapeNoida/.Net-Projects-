<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="DossierLanding.aspx.cs" Inherits="LexisNexis.DossierLanding" %>

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
                            background: "url(../images/ClearFilter.png) center center no-repeat",
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
    <style type="text/css">
        th.filter
        {
	        background: url(../images/Down.png) 95% center no-repeat;
	        cursor:pointer;
        }
    </style>
    <div id="divFilter"></div>
    <div class="table_content">
        <asp:GridView ID="grdViewOrders" runat="server"
            AllowPaging="True" AutoGenerateColumns="False" TabIndex="1"
            DataKeyNames="Encyc_id" Width="100%" GridLines="None"
            CellPadding="3" CellSpacing="1" AllowSorting="true" BorderWidth="1px" 
            onpageindexchanging="grdViewOrders_PageIndexChanging" OnSorting="grdViewOrders_Sorting">
            <Columns>
                <asp:BoundField DataField="Encyc_id" HeaderText="ID" 
                    SortExpression="Encyc_id" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Openreceiveddate" HeaderText=" Date" 
                    SortExpression="Openreceiveddate" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Collection_title" HeaderText="Ship Name" 
                    SortExpression="Collection_title" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="folio" HeaderText="folio" 
                    SortExpression="folio" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="item_type" HeaderText="item_type" 
                    SortExpression="item_type" DataFormatString="{0:#0.00}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="itemdtd" HeaderText="itemdtd" 
                    SortExpression="itemdtd" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="title" HeaderText="title" 
                    SortExpression="title" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
               <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>  
                </asp:TemplateField>  
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:Button ID="Refresh" runat="server" Text="Refresh" OnClick="Refresh_Click" Visible="false"  />
        <asp:HiddenField ID="hndSelectedValue" runat="server" Value="" />
    </div>

</asp:Content>

