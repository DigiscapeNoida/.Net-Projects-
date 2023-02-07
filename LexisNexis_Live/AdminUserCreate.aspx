<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="AdminUserCreate.aspx.cs" Inherits="AdminUserCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

      <script language="javascript" type="text/javascript">
          function Validate() {
              var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
              var emailAddress = document.getElementById("<%= txtuserid.ClientID %>").value;
              var firstname = document.getElementById("<%= txtfname.ClientID %>").value;
              var lastname = document.getElementById("<%= txtlname.ClientID %>").value;
              if (emailAddress == "") {
                  alert("Merci d'insérer l'utilisateur");
                  return false;
              }
              if (firstname == "") {
                  alert("Merci d'insérer le prénom");
                  return false;
              }
              if (lastname == "") {
                  alert("Merci d'insérer le nom");
                  return false;
              }

      var valid = emailRegex.test(emailAddress);
      if (!valid) {
          alert("Merci d'insérer une adresse valide");
          return false;
      } else
          return true;
          }


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
        function Redirect(page) {

            window.location.href = page;
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="body_container">

        <div class="wrapper">

            <div class="page_content" style="border: 1px solid #D8D8D8; border-radius: 6px 6px 0 0;">
                <div class="page_header">

                    <asp:Button ID="BtnProducts" runat="server" CssClass="button_one prdct_btn" Text="Products" OnClick="BtnProducts_Click" Visible="True" />


                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="tp_info_btn" OnClick="btnLogout_Click" />

                    <small id='osx-modal'><a href='#' class='osx tp_info_btn' style="color: #fff" id="anchor1">
                        <asp:Label ID="changepassword" runat="server" Text=""></asp:Label></a></small>



                    <strong class="spacer_br"></strong>

                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label><asp:Label ID="lblUserName" runat="server" Text="connected Username"></asp:Label>
                    <div style="padding: 0px 0px 0px 0px"></div>
                </div>




                <div class="sub_page_header" style="border-bottom: 1px solid #D8D8D8;">



                 <!--  <ul class="tp_tab">
                        <li class="active"><a href="#" onclick="Redirect('AdminUserCreate.aspx')">
                            <asp:Label ID="lblcreateusermenu" runat="server" Text="Create User"></asp:Label>
                        </a></li>
                        <!------
                        <li><a href="#" onclick="Redirect('AdminDossier.aspx')">
                            <asp:Label ID="lbldossiermenu" runat="server" Text="Dossier"></asp:Label>
                        </a></li>

                        <li><a href="#" onclick="Redirect('AdminEncyclopedia.aspx')">
                            <asp:Label ID="lblencyclomenu" runat="server" Text="Encyclopédies"></asp:Label>
                        </a></a> </li>

                        <li><a href="#" onclick="Redirect('AdminFiche.aspx')">
                            <asp:Label ID="lblFichemenu" runat="server" Text="Fiches de mise à jour"></asp:Label>
                        </a></li>

                        <li><a href="#" onclick="Redirect('AdminJournal.aspx')">
                            <asp:Label ID="lbljournalmenu" runat="server" Text="Revues"></asp:Label>
                        </a></li>

                           
                        <%--<li><a href="#" onclick="Redirect('AdminJournal.aspx')">
                            <asp:Label ID="lbl_products" runat="server" Text="products"></asp:Label>
                        </a></li>--%>

                    </ul>     ------->


                    <div class="box_heading">
                        <asp:Label ID="lbluseradminhead" Text="User access create" runat="server" Visible="false"></asp:Label>

                    </div>
                </div>


                <div class="table_container">
                    <div class="table_heading">
                        <div class="box1">

                            <div class="col">
                                <asp:Label ID="lblUserID" Text="User ID:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtuserid"  runat="server" CssClass="input_2"></asp:TextBox>
                            </div>

                            <div class="col">
                                <asp:Label ID="lblfname" Text="First Name:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtfname" runat="server" CssClass="input_2"></asp:TextBox>
                            </div>

                            <div class="col">
                                <asp:Label ID="lbllname" Text="Last Name:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtlname" runat="server" CssClass="input_2"></asp:TextBox>
                            </div>
                            <div class="col" style="display: none">
                                <asp:Label ID="lblrole" Text="Role:" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlrole" runat="server" CssClass="select_1" AutoPostBack="True">
                                    <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                    <asp:ListItem Text="Lexis Nexis" Value="LN"></asp:ListItem>
                                    <asp:ListItem Text="Thomson" Value="TDM"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <asp:Label ID="lblprodsite" Text="Production Site:" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlprodsite" runat="server" CssClass="select_1" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <asp:Button ID="btnUserCreate" runat="server" CssClass="button_one" Text="Create" Visible="true" OnClick="btnUserCreate_Click" OnClientClick="return Validate();" />
                            <asp:Button ID="btnUserCancel" runat="server" CssClass="button_one" Text="Cancel" Visible="true" OnClick="btnUserCancel_Click" OnClientClick="Confirm()" />
                            </div>
                        </div>

                        <div class="box2">
                            <asp:Label ID="lblmessgae" Text="" runat="server" Font-Bold="true"></asp:Label>
                            
                            

                        </div>
                    </div>

                    <div style="height: 10px;"></div>
                    <div id="divFilter"></div>

                    <div class="table_content">

                        <asp:GridView ID="grdUser" runat="server"
                            AllowPaging="false" AutoGenerateColumns="False" TabIndex="1"
                            DataKeyNames="USERID" Width="100%" GridLines="None"
                            CellPadding="0" CellSpacing="0" AllowSorting="false" BorderWidth="1px" OnRowCancelingEdit="grdUser_RowCancelingEdit" OnRowEditing="grdUser_RowEditing" OnRowUpdating="grdUser_RowUpdating">
                            <Columns>

                                  <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txt_UserID" Text='<%#Eval("USERID")%>' Enabled="false" Style="width: 100%; border: none; padding: 5px; text-align: center;"></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                           <asp:BoundField DataField="USERID" ReadOnly="true" HeaderText="USER ID"
                                    SortExpression="USERID">
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="FIRSTNAME"  HeaderText="FIRST NAME"
                                    SortExpression="FIRSTNAME" Visible="true">
                                    <ItemStyle Width="20%" CssClass="inpt_fld" />
                                </asp:BoundField>

                                <asp:BoundField DataField="LASTNAME" HeaderText="LAST NAME"
                                    SortExpression="LASTNAME">
                                    <ItemStyle Width="20%" CssClass="inpt_fld" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblrole" runat="server" Text='<%#Eval("ROLEID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="inpt_fld2" ID="txtrole" runat="server" Text='<%#Eval("ROLEID")%>' ReadOnly="true"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              

                                <%--  Edited by kshitij for dropdown Harding values because dropdown can't be binded with grid refresh --%>
                                <asp:TemplateField HeaderText="PROD TYPE" Visible="true">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbl_Proditem" Text='<%#Eval("PRODID") %>'></asp:Label>

                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:DropDownList CssClass="inpt_fld2"  ID="edit_ddlprodsite" runat="server" style="z-index:3000;">
                                            
                                        </asp:DropDownList>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField DataField="PRODID" HeaderText="PROD TYPE" Visible="true"
                                    SortExpression="PRODID">
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>--%>

                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <HeaderTemplate>
                                        <asp:Button ID="Deleteimg" runat="server" CausesValidation="false" OnClick="Deleteimg_Click" Text="Supprimer" OnClientClick="Confirm()" />
                                    </HeaderTemplate>
                                </asp:TemplateField>

                                <%-- Edited by Kshitij 20-Feb-2017 --%>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button CssClass="gry_btn" ID="btn_Edit" runat="server" Text="modifier" CommandName="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button CssClass="gry_btn" ID="btn_Update" runat="server" Text="mettre à jour" CommandName="Update" />
                                        <asp:Button CssClass="gry_btn" ID="btn_Cancel" runat="server" Text="Annuler" CommandName="Cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="table_paging" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B12C2D" Font-Bold="True" ForeColor="#fff" />
                            <EditRowStyle/>
                        </asp:GridView>



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




</asp:Content>

