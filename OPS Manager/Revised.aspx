<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Revised.aspx.cs" Inherits="OPSManager.Revised" StylesheetTheme="Skin01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="FeaturedContent">
    <div style="height: 82px">
        <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <table align="center" style="border: thin solid black; font-size: 10pt; font-family: Arial; width: 100%; height: 0;">
            <tr>
                 <td style="width:20%">  
                Client                  
                <asp:DropDownList ID="DDLClient" onchange="javascript:SetContextKey();" runat="server" Height="26px">
                    <asp:ListItem Value="--Select--"></asp:ListItem>
                    <asp:ListItem Value="JWVCH"></asp:ListItem>
                    <asp:ListItem Value="JWUK"></asp:ListItem>
                    <asp:ListItem Value="JWUSA"></asp:ListItem>
                    <asp:ListItem Value="SINGAPORE"></asp:ListItem>
                    <asp:ListItem Value="LWW"></asp:ListItem>
                    <asp:ListItem Value="Thieme"></asp:ListItem>
                </asp:DropDownList>
            </td>          
                <td style="width:40%">Select JID &nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="179px" Height="16px" />
                    <asp:Button ID="Button4" runat="server" Text="Search" Width="100px" Height="28px" OnClick="Button3_Click" />
                </td>
                <td >
                    <asp:Label ID="Jname" runat="server" Text="Article Journal Name" Width="374px" Height="27px"></asp:Label>
                </td>
            </tr>
        </table>

        <cc1:AutoCompleteExtender
            ID="AutoCompleteExtender1"
            TargetControlID="txtSearch"
            runat="server" UseContextKey="True" ServiceMethod="GetCompletionList" MinimumPrefixLength="2" />
    </div>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function SetContextKey() {           
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=DDLClient.ClientID %>").value);
          }
        function ErrMsgFax() {

            var Corname = document.getElementById('<%=  FaxCorrespondence.ClientID  %>').value;
            if (Corname == "") {
                alert("Please Enter Fax Correspodence Name");
                return false;
            }

            var J_mailfrom = document.getElementById('<%= FaxMailfrom.ClientID  %>').value;
            if (J_mailfrom == "") {
                alert("Please Enter Fax mail From.");
                return false;
            }
            else {
                try {

                    var maiils = J_mailfrom.split(",");
                    if (maiils.length < 2) {
                        maiils = J_mailfrom.split(";");
                    }
                    for (i = 0; i < maiils.length; i++) {
                        var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                        if (reg.test(maiils[i]) == false) {
                            alert("Fax Mail From is not valid.");
                            return false;
                        }
                    }
                }
                catch (err) {
                    alert(err.message);
                    return false;
                }
            }

            var J_MailCC = document.getElementById('<%= faxMailCC.ClientID  %>').value;
            if (J_MailCC == "") {
                alert("Please Enter Fax mail CC.");
                return false;
            }
            else {
                try {

                    var maiils = J_MailCC.split(",");                 
                    if (maiils.length < 2) {
                        maiils = J_MailCC.split(";");
                    }
                    for (i = 0; i < maiils.length; i++) {
                        var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                        if (reg.test(maiils[i]) == false) {
                            alert("Fax Mail CC is not valid.");
                            return false;
                        }
                    }
                }
                catch (err) {
                    alert(err.message);
                    return false;
                }
            }
            var J_MailTo = document.getElementById('<%= FaxMailto.ClientID  %>').value;
             if (J_MailTo == "") {
                 alert("Please Enter fax mail TO.");
                 return false;
             }
             else {
                 try {

                     var maiils = J_MailTo.split(",");
                     if (maiils.length < 2) {
                         maiils = J_MailTo.split(";");
                     }
                     for (i = 0; i < maiils.length; i++) {
                         var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                         if (reg.test(maiils[i]) == false) {
                             alert("Fax Mail TO is not valid.");
                             return false;
                         }
                     }
                 }
                 catch (err) {
                     alert(err.message);
                     return false;
                 }
             }
             return true;

        }


        function ErrMsg() {


            var Corname = document.getElementById('<%=  CorrName.ClientID  %>').value;
            if (Corname == "") {
                alert("Please Enter Correspodence Name");
                return false;
            }


            var J_mailfrom = document.getElementById('<%= mailfrom.ClientID  %>').value;
            if (J_mailfrom == "") {
                alert("Please Enter mail From.");
                return false;
            }
            else {
                try {

                    var maiils = J_mailfrom.split(",");
                    if (maiils.length < 2) {
                        maiils = J_mailfrom.split(";");
                    }
                    for (i = 0; i < maiils.length; i++) {
                        var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                        if (reg.test(maiils[i]) == false) {
                            alert("Mail From is not valid.");
                            return false;
                        }
                    }
                }
                catch (err) {
                    alert(err.message);
                    return false;
                }
            }

            var J_MailCC = document.getElementById('<%= MailCC.ClientID  %>').value;
             if (J_MailCC == "") {
                 alert("Please Enter mail CC.");
                 return false;
             }
             else {
                 try {

                     var maiils = J_MailCC.split(",");
                     if (maiils.length < 2) {
                         maiils = J_MailCC.split(";");
                     }
                     for (i = 0; i < maiils.length; i++) {
                         var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                         if (reg.test(maiils[i]) == false) {
                             alert("Mail CC is not valid.");
                             return false;
                         }
                     }
                 }
                 catch (err) {
                     alert(err.message);
                     return false;
                 }
             }
             var J_MailTo = document.getElementById('<%= MailTo.ClientID  %>').value;
             if (J_MailTo == "") {
                 alert("Please Enter mail TO.");
                 return false;
             }
             else {
                 try {

                     var maiils = J_MailTo.split(",");
                     if (maiils.length < 2) {
                         maiils = J_MailTo.split(";");
                     }
                     for (i = 0; i < maiils.length; i++) {
                         var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                         if (reg.test(maiils[i]) == false) {
                             alert("Mail TO is not valid.");
                             return false;
                         }
                     }
                 }
                 catch (err) {
                     alert(err.message);
                     return false;
                 }
             }
             return true;
         }
    </script>
    <form method="post"  Width="100%" height ="100%">
        <asp:Panel ID="Panel1" runat="server" Width="100%">
            <table style="border: thin solid #808080; font-size: 10pt; font-family: Arial;Width:100%; height: 400px;">
                <tr style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: medium; background-color: #CC3399; font-weight: normal; color: #FFFFFF; Width:100%">
                    <td style="text-align:center;Width:50%">Revise </td>
                    <td style="text-align:center;Width:50%">Fax </td>
                </tr>
                    <tr style="width:100%">
                    <td style="width:50%">
                        <table style="border-color: #808080; font-size: 10pt; font-family: Arial; margin-left: 0px;Width:100%">
                            <tr style="width:100%">

                                <td style="Width:20%"></td>
                                <td style="font-weight: bold; text-transform: capitalize; color: #008000; Width:80%">
                                    <asp:Label ID="Label1" runat="server" Text="Correspondence Info(Revise)"></asp:Label>
                                </td>
                            </tr>
                            <tr Width:"100%">
                                <td style="padding-left: 10px;" Width:"20%">
                                    <asp:Label ID="Label2" runat="server" Text="Cor Name" Width="100%" ToolTip="Correspondence Name"></asp:Label>
                                </td>
                                <td Width:"80%">
                                    <asp:TextBox ID="CorrName" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Label ID="Label3" runat="server" Text="MailFrom :" Width="100%"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="mailfrom" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Label ID="Label4" runat="server" Text="MailTo :" Width="100%"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="MailTo" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Label ID="Label5" runat="server" Text="MailCC :" Width="100%"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="MailCC" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align ="right" >                                    
                                        <asp:Button ID="Button2" runat="server" CausesValidation="false" OnClick="Button2_Click" Text="Edit" Width="105px" />
                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" OnClick="Button1_Click" OnClientClick="return ErrMsg();" Text="Update" Width="105px" />                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" >
                                    <asp:HiddenField ID="OPSid" runat="server" />
                                </td>
                            </tr>
               

            </table>
                   </td>
                    <td style="width:50%">
                        <table style="border-color: #808080; font-size: 10pt; font-family: Arial; margin-left: 0px; padding-left: 10px; width:100%">
                            <tr>
                                <td  ></td>
                                <td style="font-weight: bold; text-transform: capitalize; color: #008000;width:100%">
                                    <asp:Label ID="Label6" runat="server" Text="Correspondence Info(Fax)"></asp:Label>
                                </td>
                                <tr>
                                    <td style="padding-left: 10px;"  width="20%">
                                        <asp:Label ID="Label7" runat="server" Text="Cor Name" Width="100%" ToolTip="Correspondence Name" ></asp:Label>
                                    </td>
                                    <td width="80%">
                                        <asp:TextBox ID="FaxCorrespondence" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            <tr>
                                <td style="padding-left: 10px;" >
                                    <asp:Label ID="Label8" runat="server" Text="MailFrom :" Width="100%"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="FaxMailfrom" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Label ID="Label9" runat="server" Text="MailTo :" Width="100%"></asp:Label>
                                </td>
                                <td class="auto-style57">
                                    <asp:TextBox ID="FaxMailto" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <asp:Label ID="Label10" runat="server" Text="MailCC :" Width="100%"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="faxMailCC" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align ="right" >
                                   
                                        <asp:Button ID="BtnEdit" runat="server" CausesValidation="false" Text="Edit" Width="105px" OnClick="BtnEdit_Click" />
                                        <asp:Button ID="BtnUpdate" runat="server" CausesValidation="False" Text="Update" Width="105px" OnClick="BtnUpdate_Click"  OnClientClick="return ErrMsgFax();"/>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="auto-style75">
                                    <asp:HiddenField ID="OPSidFax" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>           
        </asp:Panel>
    </form>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
  
    <style type="text/css">
        .auto-style12 {
            width: 555px;
        }
        .auto-style13 {
            width: 217px;
        }
    </style>
  
</asp:Content>







