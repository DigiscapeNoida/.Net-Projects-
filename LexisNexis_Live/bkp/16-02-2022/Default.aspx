<%@ Page Title="LexisNexis Login" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script
  src="https://code.jquery.com/jquery-3.4.1.js"
  integrity="sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU="
  crossorigin="anonymous"></script>
     <script type="text/javascript">

         function loginclick() {

             
              if (document.getElementById('ContentPlaceHolder1_userid').value == "") {
                  alert('Insérer nom d\'utilisateur valide pour réinitialisation mot de passe ');
                 return;
             }


              var userid = document.getElementById('ContentPlaceHolder1_userid');
             // alert(userid.value);
             PageMethods.RegisterUser(userid.value,  OnSuccess, onError);
         
             function OnSuccess(response) {                 
                 document.getElementById('ContentPlaceHolder1_lblchange').innerHTML = response.toString();
                 document.getElementById('ContentPlaceHolder1_userid').value = "";
             }

             function onError(response) {
                 alert('Something wrong.');
             }
         }


  </script>

    <div class="login_box">

<div class="login_form">

    
<%--<h1>Login</h1>--%>
    <asp:Label ID="lbllogin" runat="server" Text ="Login"></asp:Label>
    
<div class="form_row">
    <asp:Label ID="lblloginid" runat="server" Text="login id"></asp:Label>
  <asp:TextBox ID="txtlogin" runat="server" TextMode="email" class="input_1" ></asp:TextBox>
</div>


<div class="form_row">
    <asp:Label ID="lblpassword" runat="server" Text="Password"></asp:Label>
 <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" AutoCompleteType="Disabled"   class="input_1"></asp:TextBox>
</div>
    <br />
    <asp:Label ID="lbl_error" runat="server" CssClass="table_header" Text ="Détails de connexion incorrects"></asp:Label>
<div class="form_row" width="99%">
    <asp:textbox ID="txtCaptcha" runat="server"></asp:textbox>
    <BotDetect:WebFormsCaptcha  ID="captchaBox" runat="server" UserInputID="txtCaptcha" AutoClearInput="true" />
</div>
 <asp:Button ID="btnLogin" runat="server" Text="Login" class="red_btn" OnClick="btnLogin_Click" />

<%--<a href="#" class="forget_password">Forget Password</a>--%>
<asp:LinkButton ID="lbkForgetPassword" Text="Forget Password" class='osx forget_password'  runat="server"></asp:LinkButton>


</div>

</div>

 <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td width="64%" align="left" valign="top">
               
            </td>
            <td width="36%" align="left" valign="top">
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id='container'>
                    <div id='content'>
                        <!-- modal content -->
                        <div id="osx-modal-content">
                            <div id="osx-modal-title">
                               LexisNexis </div>
                            <div class="close">
                                <a href="#" class="simplemodal-close">x</a></div>
                            <div id="osx-modal-data">
                                <div style="background-color: #fff; width: 370px; margin: 0px auto">
                                    <table width="370" border="0" align="center" cellpadding="5" cellspacing="0">
                                          <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%; font-weight: normal">
                                               
                                             <asp:Label ID="mainresetpwd" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                     
                                          <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td>
                                          
                                               
                                                    <asp:Label ID="lblresetuid" runat="server" Text=""></asp:Label> <font size="1" color="red">*</font>
                                            </td>
                                            <td>
                                                <input id="userid" maxlength="150" name="userid" size="20" style="width:200px" type="text" tabindex="1"
                                                    value="" runat="server" />
                                            </td>
                                        </tr>
                                       
                                     <tr><td colspan="2" style="height:10pt"></td> </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <div>
                                                <asp:Button ID="btnresetpassword" runat="server" Text="Submit" OnClientClick="loginclick();" />
                                                    
                                                  
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

