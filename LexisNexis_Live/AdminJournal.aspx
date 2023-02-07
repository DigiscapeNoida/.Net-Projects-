<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="AdminJournal.aspx.cs" Inherits="LexisNexis.EncyclopediasLanding" EnableEventValidation="false" %>

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

        <li><a href="#" onclick= "Redirect('AdminDossier.aspx')"> <asp:Label ID="lbldossiermenu" runat="server" text="Dossier"></asp:Label> </a> </li>

        <li><a href="#"  onclick= "Redirect('AdminEncyclopedia.aspx')">  <asp:Label ID="lblencyclomenu" runat="server" text="Encyclopédies"></asp:Label> </a>  </a> </li>

        <li><a href="#"  onclick= "Redirect('AdminFiche.aspx')"> <asp:Label ID="lblFichemenu" runat="server" text="Fiches de mise à jour"></asp:Label> </a> </li>

        <li class="active"><a href="#"  onclick= "Redirect('AdminJournal.aspx')"> <asp:Label ID="lbljournalmenu" runat="server" text="Revues"></asp:Label> </a> </li>
         

    </ul>


<div class="box_heading"  style="display:none">
<asp:Label ID="lbljournaladminhead" Text="Revues" runat="server"></asp:Label>
  
</div>




<div class="box1"   style="display:none">
<asp:Label ID="lblTask" Text="Task" runat="server"></asp:Label>
    <asp:DropDownList ID="ddltask" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddltask_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>

<div class="table_container">
    <div class="box2"   style="padding-top:0; margin:0">
    
<asp:Button ID="btnExportExcel" runat="server" CssClass="button_one" Text="export this page" OnClick="btnExportExcel_Click" />
<asp:Button ID="btnExportExcelAll" runat="server" CssClass="button_one" Text="Export All" OnClick="btnExportExcelAll_Click" />
<asp:Button ID="btnremove" runat="server" CssClass="button_one" Visible="true" Text="Remove" OnClick="btnremove_Click"  OnClientClick="Confirm()"  />
</div>
<div class="table_heading"   style="display:none">
<div class="box1 journal_landing">

<div class="col">

    <asp:Label ID="lblChoseProductencyclo" Text="Choisissez votre produit" runat="server"></asp:Label>
     <asp:DropDownList ID="ddlProduct" runat="server" CssClass="select_1" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" ></asp:DropDownList>
</div>

<div class="col">

<asp:Label ID="lblcollection" Text="JID" runat="server"></asp:Label>
     <div class="pop_drp_dwn">
          <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="100px" Width="300px" 
                            
                             AutoPostBack="true" Visible="true" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
               </asp:CheckBoxList>
         </div>
 <asp:DropDownList ID="ddlreview" runat="server" CssClass="select_1" OnSelectedIndexChanged="ddlreview_SelectedIndexChanged" AutoPostBack="True" Visible="false"></asp:DropDownList>
</div>

  
     <asp:Button ID="btnLookfor" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnLookfor_Click" />
    <asp:Button ID="btnremovefilter" runat="server" CssClass="button_one" Text="Remove all filters"  Visible="true" OnClick="btnremovefilter_Click"  />
    <asp:Button ID="btnRefresh" runat="server" CssClass="button_one" Text="Search" Visible="true" OnClick="btnRefresh_Click"  />
</div>

<div class="box2">
    
  </div>
</div>

  <div style="height:10px"></div>
<div id="divFilter"></div>
    <div  class="table_content">
        <asp:GridView ID="grdViewOrders" runat="server"
            AllowPaging="True" AutoGenerateColumns="False" TabIndex="1"
            DataKeyNames="Articleid" Width="100%" GridLines="None"
            CellPadding="0" CellSpacing="0" AllowSorting="true" BorderWidth="1px" 
            onpageindexchanging="grdViewOrders_PageIndexChanging" OnSorting="grdViewOrders_Sorting" OnRowCommand="grdViewOrders_RowCommand" OnRowDataBound="grdViewOrders_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Articleid" HeaderText="ID" 
                    SortExpression="Articleid" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                  <asp:BoundField DataField="journal_Name" HeaderText="Revues" Visible="false"
                    SortExpression="journal_Name" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="AID" HeaderText="AID" Visible="false"
                    SortExpression="AID" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="ArticleTitle" HeaderText="ArticleTitle" 
                    SortExpression="ArticleTitle"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                  <asp:BoundField DataField="AuthorName" HeaderText="Author" Visible="true"
                    SortExpression="AuthorName"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                  <asp:BoundField DataField="ArticleType" HeaderText="ArticleType" Visible="true"
                    SortExpression="ArticleType"  >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                  <asp:BoundField DataField="Publishing_Number" HeaderText="PublicationNumber" Visible="true"
                    SortExpression="Publishing_Number"  >
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
                <asp:BoundField DataField="IN_DATE" HeaderText="In date" 
                    SortExpression="IN_DATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="DUEDATE" HeaderText="Due date" 
                    SortExpression="DUEDATE" DataFormatString="{0:dd-MMM-yyyy}" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                 <asp:BoundField DataField="Delivery_Date" HeaderText="complete date" 
                    SortExpression="Delivery_Date" DataFormatString="{0:dd-MMM-yyyy}" >
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
                 <asp:BoundField DataField="fullname" HeaderText="Encyclouserid" 
                    SortExpression="fullname" >
                <ItemStyle Width="20%" />
                </asp:BoundField>
                
               <asp:TemplateField HeaderText="comment">
                   <ItemTemplate>
                  <asp:ImageButton ID="img1" runat="server" CommandArgument='<%# Eval("comments") %>' CommandName="comment" ImageUrl="~/images/icon-comment.png" />
                       </ItemTemplate>
               </asp:TemplateField>
                
               <asp:TemplateField HeaderText="attachments">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="Download" Text='<%# Eval("filename") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>
                  <asp:TemplateField HeaderText="TD attachments" Visible="false">  
                                    <ItemTemplate>  
                                        <asp:LinkButton ID="lnkTDDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="TDDownload" Text='<%# Eval("tdfilename") %>' />  
                                    </ItemTemplate>  
                                </asp:TemplateField>     
               <asp:TemplateField HeaderText="Action" Visible="true">  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk" runat="server" ToolTip='<%# Eval("STAGE")%>' />
                            <asp:Literal ID="litID" runat="server" Text='<%# Eval("Articleid")%>' Visible="false"></asp:Literal>
                        <asp:Literal ID="userid" runat="server" Text='<%# Eval("userid")%>' Visible="false"></asp:Literal>
                    </ItemTemplate>  
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Action" Visible="false">  
                                    <ItemTemplate>  
                                        <asp:Literal ID="stageid" runat="server" Text='<%# Eval("STAGE")%>' Visible="false"></asp:Literal>
                            <asp:Literal ID="litID1" runat="server" Text='<%# Eval("Articleid")%>' Visible="false"></asp:Literal>
                        <asp:Literal ID="userid1" runat="server" Text='<%# Eval("userid")%>' Visible="false"></asp:Literal>

                                        <asp:LinkButton ID="lnkvalidate" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="Validate" Text="Archiver" Visible="false" OnClientClick = "return confirm('Attention ! Le document sera supprimé de la plateforme dans 7 jours. Continuer ?')" OnClick="ValidateFile" /> &nbsp;&nbsp;
                                         <asp:LinkButton ID="lnkcancel" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="rework" Text="Correction" Visible="false" OnClick="CancelFile" />  &nbsp;&nbsp;
                                         <asp:LinkButton ID="lnktdupload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articlerid") %>'  
                                            CommandName="Upload" Text="Upload" Visible="false"  /> 
                                          <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="Edit1" Text="Edit" Visible="false" OnClick="UpdateFile" />  &nbsp;&nbsp;

                                        <asp:LinkButton ID="lnkdelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Articleid") %>'  
                                            CommandName="delete1" Text="Annuler" Visible="false" OnClick="deleteFile" OnClientClick="return confirm('Confirmez-vous cette annulation ?')" /> 
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
</asp:Content>

