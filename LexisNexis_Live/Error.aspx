<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="LexisNexis.on_Error" EnableEventValidation="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">

   
    
    
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
                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="tp_info_btn" OnClick="btnLogout_Click" />



                    <strong class="spacer_br"></strong>

                    <div style="padding: 0px 0px 0px 0px"></div>

                </div>

                <div class="table_container">

                    <div style="height: 10px"></div>
                    <div id="divFilter"></div>
                    <div class="table_content">
                        <asp:Label ID="lblerror" CssClass="bold" runat="server" Text="Nous sommes désolés de votre désagrément. Nous avons un problème technique dans cette section"></asp:Label>
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

