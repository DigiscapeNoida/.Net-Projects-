<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="JournalEntry.aspx.cs" Inherits="JournalEntry" %>

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
     <div class="login_box login_interface">
<div class="login_form spacer">

    <asp:Label ID="lblHeadingjournal" runat="server" Text="Log a booklet (Encyclopedia)"></asp:Label>


<div class="row colle">
     <div class="col"> 
<asp:Label ID="lblreview" Text="Review" runat="server"></asp:Label><span class="star">*</span>
         </div>
   
             <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="100px" Width="300px" 
                            
                             AutoPostBack="false" Visible="false">
               </asp:CheckBoxList>
            
     <div class="demo auto_cmplt">
<div class="ui-widget">
    <asp:DropDownList ID="ddlreview" runat="server" Visible="true">
    </asp:DropDownList>
     <asp:RequiredFieldValidator ControlToValidate="ddlreview" ID="RequiredFieldValidator1"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>
        

</div>
<asp:DropDownList ID="ddlreview1" runat="server" CssClass="select_2" Visible="false" ></asp:DropDownList>
    <asp:TextBox ID="txtaid" runat="server" CssClass="input_2" Visible="false" ></asp:TextBox>
     


   



</div>

<div class="row colle">
<asp:Label ID="lbljournalarticletitle" Text="Article Title" runat="server"></asp:Label>
<asp:TextBox ID="txtarticletitle" runat="server" CssClass="input_2" ></asp:TextBox>
</div>
    
<div class="row colle">
<asp:Label ID="lbljournaltobedone" Text="Work to be done" runat="server"></asp:Label>
<asp:DropDownList ID="ddlworktobedone" runat="server" CssClass="select_2" AutoPostBack="false">
    <asp:ListItem Text="préparation+balisage FSS" Value="préparation+balisage FSS" Selected="True"></asp:ListItem>
    <asp:ListItem Text="préparation uniquement" Value="préparation uniquement"></asp:ListItem>
    <asp:ListItem Text="balisage FSS uniquement" Value="balisage FSS uniquement"></asp:ListItem>
</asp:DropDownList>
</div>
    <div class="row colle">
         <div class="col"> 
<asp:Label ID="lbljournalAuthor" Text="Author" runat="server"></asp:Label><span class="star">*</span>
             </div>
<asp:TextBox ID="txtjournalauthor" runat="server" CssClass="input_2" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtjournalauthor"
        ErrorMessage="Please insert Book Title!" ForeColor="Red" Width="1px">*</asp:RequiredFieldValidator>
</div>
     <div class="row colle">
          <div class="col"> 
<asp:Label ID="lbljournalartciletype" Text="Article Type" runat="server"></asp:Label><span class="star">*</span>
              </div>
<asp:TextBox ID="txtarticletype" runat="server" CssClass="input_2" ></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtarticletype"
        ErrorMessage="Please insert Book Title!" ForeColor="Red" Width="1px">*</asp:RequiredFieldValidator>
</div>
     <div class="row colle">
<asp:Label ID="lbljournalpubnum" Text="Publication Number" runat="server"></asp:Label>
<asp:TextBox ID="txtpubnum" runat="server" CssClass="input_2" ></asp:TextBox>
</div>
    <div class="row colle">
     <div class="col"> 
 <asp:Label ID="lblDelaijournal" Text="Delai back" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:DropDownList ID="ddldelai" runat="server" CssClass="select_2" AutoPostBack="True" OnSelectedIndexChanged="ddldelai_SelectedIndexChanged" ></asp:DropDownList>
      <asp:RequiredFieldValidator ControlToValidate="ddldelai" ID="RequiredFieldValidator5"
                    ErrorMessage="please select value" ForeColor="Red" InitialValue="-1" Width="1px" runat="server">*</asp:RequiredFieldValidator>
</div>
    <div class="row">
<asp:Label ID="lblJournaldateheure" Text="Délai" runat="server"></asp:Label>
<asp:Label ID="lblJournalheureval" Text="" runat="server"></asp:Label>

</div>

 <div class="row">
<asp:Label ID="lblNotificationforsupejournal" Text="Notification for sup" runat="server"></asp:Label>
<asp:TextBox ID="txtsupnotification" runat="server" CssClass="input_2"></asp:TextBox>
</div>

<div class="row">
<asp:Label ID="lblCommentejournal" Text="Comment" runat="server"></asp:Label>
<asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" CssClass="input_2"></asp:TextBox>
</div>

<div class="row colle">
     <div class="col"> 
<asp:Label ID="lblLoadaFilejournal" Text="Load a file" runat="server"></asp:Label><span class="star">*</span>
         </div>
<asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileUpload" />
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="*" ControlToValidate="FileUpload1"
    runat="server" Display="Dynamic" ForeColor="Red" Width="1px" >*</asp:RequiredFieldValidator><br /></div>
     <div class="row">
           <asp:Label ID="Dossiervalidationmessage" runat="server" Text=""></asp:Label>
       </div>
            &nbsp;


<div class="row">
<div class="center">
    <asp:Button ID="btnSendJournal" runat="server" CssClass="red_btn" CausesValidation="true" 
                    Text="To send" OnClick="btnSendJournal_Click" />
    <asp:Button ID="btnCancel" runat="server" CssClass="red_btn" CausesValidation="false" 
                    Text="Cancel" OnClick="btnCancel_Click" />

</div>
</div>

    </div>
            <div class="row">
<asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>

</div>
</div>
</asp:Content>

