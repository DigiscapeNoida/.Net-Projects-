<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="JournalSearch.aspx.cs" Inherits="EncycloSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <link href="Stylesheet/Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="Stylesheet/Autocomplete/jquery-1.5.js" type="text/javascript"></script>
    <script src="Stylesheet/Autocomplete/jquery-ui.js" type="text/javascript"></script>
       <style>

       .ui-button { margin-left: -1px; }

       .ui-button-icon-only .ui-button-text { padding: 0.35em; } 

       .ui-autocomplete-input { margin: 0; padding: 0.48em 0 0.47em 0.45em; }

       .auto_cmplt .ui-button{float:left; width:30px; height:31px; background:url(images/drp_arw_1.png) no-repeat center center; background-size:20px; border:1px solid #000;}

           .auto_cmplt.srch_tp_artcle{float:left;}

           .auto_cmplt.srch_tp_artcle{width:467px;}

           .auto_cmplt.srch_tp_artcle .ui-autocomplete-input{width:434px;}

       </style>
    <script>





        function optionSelected(selectedValue) {

            document.title = selectedValue;

        }



        (function ($) {

            $.widget("ui.combobox", {

                _create: function () {

                    var self = this,

                               select = this.element.hide(),

                               selected = select.children(":selected"),

                               value = selected.val() ? selected.text() : "";

                    var input = this.input = $("<input>")

                               .insertAfter(select)

                               .val(value)

                               .autocomplete({

                                   delay: 0,

                                   minLength: 0,

                                   source: function (request, response) {

                                       var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");

                                       response(select.children("option").map(function () {

                                           var text = $(this).text();

                                           if (this.value && (!request.term || matcher.test(text)))

                                               return {

                                                   label: text.replace(

                                                                        new RegExp(

                                                                               "(?![^&;]+;)(?!<[^<>]*)(" +

                                                                               $.ui.autocomplete.escapeRegex(request.term) +

                                                                               ")(?![^<>]*>)(?![^&;]+;)", "gi"

                                                                        ), "<strong>$1</strong>"),

                                                   value: text,

                                                   option: this

                                               };

                                       }));

                                   },

                                   select: function (event, ui) {

                                       ui.item.option.selected = true;

                                       self._trigger("selected", event, {

                                           item: ui.item.option

                                       });

                                       //JK

                                       optionSelected(ui.item.option.value);



                                   },

                                   change: function (event, ui) {

                                       if (!ui.item) {

                                           var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),

                                                           valid = false;

                                           select.children("option").each(function () {

                                               if ($(this).text().match(matcher)) {

                                                   this.selected = valid = true;

                                                   return false;

                                               }

                                           });

                                           if (!valid) {

                                               // remove invalid value, as it didn’t match anything

                                               $(this).val("");

                                               select.val("");

                                               input.data("autocomplete").term = "";

                                               return false;

                                           }

                                       }

                                   }

                               })

                               .addClass("ui-widget ui-widget-content ui-corner-left");



                    input.data("autocomplete")._renderItem = function (ul, item) {

                        return $("<li></li>")

                                      .data("item.autocomplete", item)

                                      .append("<a>" + item.label + "</a>")

                                       .appendTo(ul);

                    };



                    this.button = $("<button type='button'>&nbsp;</button>")

                               .attr("tabIndex", -1)

                               .attr("title", "Show All Items")

                               .insertAfter(input)

                               .button({

                                   icons: {

                                       primary: "ui-icon-triangle-1-s"

                                   },

                                   text: false

                               })

                               .removeClass("ui-corner-all")

                               .addClass("ui-corner-right ui-button-icon")

                                  .click(function () {

                                      // close if already visible

                                      if (input.autocomplete("widget").is(":visible")) {

                                          input.autocomplete("close");

                                          return;

                                      }



                                      // pass empty string as value to search for, displaying all results

                                      input.autocomplete("search", "");

                                      input.focus();

                                  });

                },



                destroy: function () {

                    this.input.remove();

                    this.button.remove();

                    this.element.show();

                    $.Widget.prototype.destroy.call(this);

                }

            });

        })(jQuery);



        $(function () {

            $('#MainContent_ddlreview').combobox();

            $("#MainContent_toggle").click(function () {

                $('#MainContent_ddlreview').toggle();

            });

        });

    </script>
<div class="login_box login_interface login_spacer">
<div class="login_form two_column">

    <div class="heading">
<asp:Label ID="lblHeadingsearchjournal" runat="server" Text="Log a booklet (Encyclopedia)"></asp:Label><asp:Label ID="lblheading2e" runat="server" Text="article" Visible="false"></asp:Label>
        </div>
    
         <div class="row">
 <asp:Label ID="lblsearchuser" Text="Category" runat="server"></asp:Label>
<asp:DropDownList ID="ddlusersearch" runat="server" CssClass="select_2"></asp:DropDownList>
           
</div>
     <div class="row">
    <asp:Label ID="lbljournalstage" Text="Stage" runat="server"></asp:Label>
<asp:DropDownList ID="ddlstagesearch" runat="server" CssClass="select_2">
    <asp:ListItem Text="----------" Value="-1"></asp:ListItem>
    <asp:ListItem Text="En préparation" Value="En préparation"></asp:ListItem>
    <asp:ListItem Text="Contenu préparé" Value="Contenu préparé"></asp:ListItem>
    <asp:ListItem Text="Contenu corrigé" Value="Contenu corrigé"></asp:ListItem>
    <asp:ListItem Text="Archivé" Value="Archivé"></asp:ListItem>
</asp:DropDownList>
    </div>

    <div class="row full">
 <asp:Label ID="lblreview" Text="Category" runat="server"></asp:Label>

             <div class="demo auto_cmplt srch_tp_artcle">
<div class="ui-widget">
    <asp:DropDownList ID="ddlreview" runat="server" Visible="true">
    </asp:DropDownList>
   
</div>
        

</div>

        <asp:DropDownList ID="ddlreview1" runat="server" CssClass="select_2" Visible="false"></asp:DropDownList>
</div>
    
  

      <div class="row full">
    <asp:Label ID="lbljournalchid" Text="ID" runat="server"></asp:Label>
<asp:TextBox ID="txtid" runat="server" CssClass="input_2"></asp:TextBox>
    </div>
<div class="row full">
    <asp:Label ID="lbljournalAuthor" Text="Author" runat="server"></asp:Label>
<asp:TextBox ID="txtauthor" runat="server" CssClass="input_2"></asp:TextBox>
    </div>

<div class="row full">
<asp:Label ID="lbljournalArticleTitle" Text="Folder Title" runat="server"></asp:Label>
<asp:TextBox ID="txtarticletitle" runat="server" CssClass="input_2" ></asp:TextBox>
  
</div>
    <div class="row full">
<asp:Label ID="lbljournalartciletype" Text="Folder Type" runat="server"></asp:Label>
<asp:TextBox ID="txtarticletype" runat="server" CssClass="input_2" ></asp:TextBox>
  
</div>

    <div class="row full">
<asp:Label ID="lbljournalpubnum" Text="Publication Number" runat="server"></asp:Label>
<asp:TextBox ID="txtpublicationnumber" runat="server" CssClass="input_2" ></asp:TextBox>
  
</div>


    <div class="row">
<asp:Label ID="lbljournallogindatefrom" Text="date d'échéance de" runat="server"></asp:Label>
<asp:TextBox ID="txtlogindatefrom" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtlogindatefrom','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
   
</div>
<div class="row">
     <asp:Label ID="lbljournallogindateto" Text="date d'échéance à" runat="server"></asp:Label>
<asp:TextBox ID="txtlogindateto" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtlogindateto','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
     </div>


    <div class="row">
<asp:Label ID="lbljournaldeliverydatefrom" Text="date d'échéance de" runat="server"></asp:Label>
<asp:TextBox ID="txtfromduedate" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtfromduedate','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
   
</div>
         

<div class="row">
     <asp:Label ID="lbljournaldeliverydateto" Text="date d'échéance à" runat="server"></asp:Label>
<asp:TextBox ID="txttoduedate" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txttoduedate','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
     </div>
  

     <div class="row">
<asp:Label ID="lbljournalcompletedatefrom" Text="date d'échéance de" runat="server"></asp:Label>
<asp:TextBox ID="txtcompletedatefrom" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtcompletedatefrom','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
   
</div>
<div class="row">
     <asp:Label ID="lbljournalcompletedateto" Text="date d'échéance à" runat="server"></asp:Label>
<asp:TextBox ID="txtcompletedateto" runat="server" CssClass="input_2"></asp:TextBox><a href="javascript:NewCssCal('MainContent_txtcompletedateto','ddmmyyyy')">
                    <img alt="Pick a date" border="0" height="16" src="images/cal.gif" width="16" /></a>
     </div>
            &nbsp;


<div class="row btn_row">
<div class="center">
    <asp:Button ID="btnSearch" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Search" OnClick="btnSend_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
</div>

<div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>

</div>

</asp:Content>

