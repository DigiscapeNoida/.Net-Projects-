<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="AdminDossier.aspx.cs" Inherits="LexisNexis.AdminDossier" EnableEventValidation="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    
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

   
   <script type="text/javascript">
       function Redirect(page) {

           window.location.href = page;
       }
</script>
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
     <ul class="tp_tab">
        <li><a href="#" onclick="Redirect('AdminUserCreate.aspx')"><asp:Label ID="lblcreateusermenu" runat="server" text="Create User"></asp:Label> </a> </li>

        <li class="active"><a href="#" onclick= "Redirect('AdminDossier.aspx')"> <asp:Label ID="lbldossiermenu" runat="server" text="Dossier"></asp:Label> </a> </li>

        <li><a href="#"  onclick= "Redirect('AdminEncyclopedia.aspx')">  <asp:Label ID="lblencyclomenu" runat="server" text="Encyclopédies"></asp:Label> </a>  </a> </li>

        <li><a href="#"  onclick= "Redirect('AdminFiche.aspx')"> <asp:Label ID="lblFichemenu" runat="server" text="Fiches de mise à jour"></asp:Label> </a> </li>

        <li><a href="#"  onclick= "Redirect('AdminJournal.aspx')"> <asp:Label ID="lbljournalmenu" runat="server" text="Revues"></asp:Label> </a> </li>
         

    </ul>


<div class="box_heading" style="display:none">
<asp:Label ID="lbldossieradminhead" Text="Dossier" runat="server" ></asp:Label>
  
</div>


<div class="box1" style="display:none">
<asp:Label ID="lblTask" Text="Task" runat="server" Visible="false"></asp:Label>
    <asp:DropDownList ID="ddltask" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddltask_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>


<div class="table_container">
    <div class="box2" style="padding-top:0; margin:0">
    <asp:Button ID="btnSubmit" runat="server" CssClass="button_one" Text="Insert" OnClick="btnSubmit_Click" Visible="false" />
<asp:Button ID="btnExportExcel" runat="server" CssClass="button_one" Text="export this page" OnClick="btnExportExcel_Click" />
<asp:Button ID="btnExportExcelAll" runat="server" CssClass="button_one" Text="Export All" OnClick="btnExportExcelAll_Click" />
         <asp:Button ID="btnremove" runat="server" CssClass="button_one" Text="Remove" Visible="true" OnClick="btnremove_Click" OnClientClick="Confirm()" />
</div>

<div class="table_heading" style="display:none">
<div class="box1">

<div class="col">
    <asp:Label ID="lblChooseProduct" Text="Choisissez votre produit :" runat="server"></asp:Label>
     <asp:DropDownList ID="ddlProduct" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" ></asp:DropDownList>
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
    <asp:Button ID="btnremovefilter" runat="server" CssClass="button_one" Text="Remove all filters"  Visible="true" OnClick="btnremovefilter_Click" />
     <asp:Button ID="btnRefresh" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnRefresh_Click"  />
</div>

<div class="box2">
     <asp:Button ID="btnComplete" runat="server" CssClass="button_one" Text="Complete" Visible="false" OnClick="btnComplete_Click" />
    <asp:Button ID="btnsendprod" runat="server" CssClass="button_one" Text="Send prod" Visible="false" OnClick="btnsendprod_Click" />
   

</div>
</div>

   <div style="height:10px;"></div>
<div id="divFilter"></div>
   
     <div  class="table_content">
       
        <asp:GridView ID="grdViewOrders" runat="server"
            AllowPaging="True" AutoGenerateColumns="False" TabIndex="1"
            DataKeyNames="DID" Width="100%" GridLines="None"
            CellPadding="0" CellSpacing="0" AllowSorting="true" BorderWidth="1px" 
            onpageindexchanging="grdViewOrders_PageIndexChanging" OnSorting="grdViewOrders_Sorting" OnRowCommand="grdViewOrders_RowCommand" OnRowDataBound="grdViewOrders_RowDataBound">
            <Columns>
                <asp:BoundField DataField="DID" HeaderText="ID" 
                    SortExpression="DID" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="DECLINATION" HeaderText="Declination" 
                    SortExpression="DECLINATION" Visible="false">
                <ItemStyle Width="20%" />
                </asp:BoundField>
               
                <asp:BoundField DataField="CTITLE" HeaderText="Folder Title" 
                    SortExpression="CTITLE" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                  <asp:BoundField DataField="Author" HeaderText="Author" Visible="true"
                    SortExpression="Author"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="DEMANDTYPE" HeaderText="Demand Type" 
                    SortExpression="DEMANDTYPE" Visible="false" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="DURATION" HeaderText="Duration" 
                    SortExpression="DURATION">
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
                 <asp:BoundField DataField="DUEDATE" HeaderText="Return Date" 
                    SortExpression="DUEDATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="Delivered_Date" HeaderText="complete date" 
                    SortExpression="Delivered_Date" DataFormatString="{0:dd-MMM-yyyy}" Visible="false">
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="PAGECOUNT" HeaderText="Page Count" 
                    SortExpression="PAGECOUNT" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                 <asp:BoundField DataField="STAGE" HeaderText="Stage" 
                    SortExpression="STAGE" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="fullname" HeaderText="Userid" 
                    SortExpression="fullname" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="comment">
                   <ItemTemplate>
                  <asp:ImageButton ID="img1" Visible="false" runat="server" CommandArgument='<%# Eval("remarks") %>' CommandName="comment" ImageUrl="~/images/icon-comment.png" />
                     
                       </ItemTemplate>
               </asp:TemplateField>
                 <asp:TemplateField HeaderText="Attachments" Visible="false">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'  
                                            CommandName="Download" Text='<%# Eval("filename") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>   
               <asp:TemplateField HeaderText="Action">  
                    <ItemTemplate>  
                         <asp:LinkButton ID="lnkdelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'  
                                            CommandName="delete1" Text="Annuler" Visible="false" OnClick="deleteFile" OnClientClick="return confirm('Confirmez-vous cette annulation ?')" /> 
                        <asp:CheckBox ID="chk" runat="server" ToolTip='<%# Eval("STAGE")%>' Visible="true" />
                         <asp:Literal ID="stageid" runat="server" Text='<%# Eval("STAGE")%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="litID" runat="server" Text='<%# Eval("DID")%>' Visible="false"></asp:Literal>
                        <asp:Literal ID="userid" runat="server" Text='<%# Eval("userid")%>' Visible="false"></asp:Literal>
                    </ItemTemplate>  
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="commentaire" Visible="true">  
                                    <ItemTemplate>  
                                          <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DID") %>'  
                                            CommandName="Edit1" Text="Edit" Visible="false" OnClick="editfile"  />
                                        <asp:LinkButton ID="lnkcomment1111" CssClass="osx1" runat="server" Text="commentaire"  OnClientClick=<%# "clickev('"+ Eval("DID")+"'); " %> ></asp:LinkButton>
                                         <asp:Literal ID="ttt" runat="server" Text='<%# Eval("DID")%>' Visible="false"></asp:Literal>
                                    </ItemTemplate>  
                                </asp:TemplateField>    
            </Columns>
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="table_paging"/>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle  BackColor="#B12C2D" Font-Bold="True" ForeColor="#fff"/>
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


    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
       
        <tr>
            <td colspan="2">
                <div id='container1'>
                    <div id='content1'>
                        <!-- modal content -->
                        <div id="osx-modal-content1">
                            <div id="osx-modal-title1">
                                <asp:Label ID="lbldossiercommenthead" runat="server" Text="Insert Comment"></asp:Label></div>
                            <div class="close">
                                <a href="#" class="simplemodal-close">x</a></div>
                            <div id="osx-modal-data1">
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
                                                  <asp:Label ID="lbldossiercomment" runat="server" Text="Insert Comment"></asp:Label><font size="1" color="red">*</font></label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="comment" TextMode="MultiLine" Width="250px" Height="100px" runat="server"></asp:TextBox>
                                                    <input id="Text2" maxlength="100" name="uid" size="20" type="text" tabindex="1"
                                                    value="" visible="false" runat="server" />
                                                     <input type="hidden" id="Hidden1" runat="server" />
                                            </td>
                                        </tr>
                                       
                                     <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <div>
                                                <asp:Button ID="btnaddcomment" runat="server" Text="Reset Password" OnClientClick="loginclick1();" />
                                                  
                                                  
                                                </div>
                                            </td>
                                        </tr>
                                         <tr><td colspan="2" style="height:10pt"></td> </tr>
                                         <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:Label ID="lbladdcomment" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                 <input type="hidden" id="hiddendidval" runat="server" />
                                                <div id="Div2" style="color: Red; ">
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

