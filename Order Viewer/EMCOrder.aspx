<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EMCOrder.aspx.cs" 

Inherits="AutoCompleteTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" 

TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
 "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <script type ="text/javascript">
        function SetAIDText() 
        {
            var ele= document.getElementById("txtAID");
            ele.value="";
        
        }
        function CheckJID()
        {
            var select = document.getElementById('ddlStage');
            if (select.options[select.selectedIndex].value == "-Select-") 
                return false;
            else
                return true;
        }
        function SetContextKey() 
        {
            var ele= document.getElementById("txtJID");
            $find('AutoCompleteAID').set_contextKey(ele.value);
        }
        </script>
    <title>EMC Order Viewer</title>
</head>
<body bgcolor="#f7f7f7" style="margin:0px">
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table border="0"  cellpadding="0" cellspacing="0"   style="border-style: none; width: 100%; height: 100%" id="TABLE1" >
            
        <tr style="width: 100%; height: 128px; background-color: #f2f2f2">
            <td  style=" background-image: url('Header1.jpg'); background-repeat: no-repeat"></td>
        </tr>    
        <tr style="width:100%;height: 100%; text-align:center; background-color: #990000; color: #FFFFFF;">
             <td>
               <asp:Label ID="Label1" runat="server" Text="JID" Font-Bold="True" ForeColor="White"></asp:Label>
               <asp:TextBox ID="txtJID" runat="server" onkeyup="SetAIDText()"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvJID" runat="server" ErrorMessage="* Required." 
                                                    ControlToValidate="txtJID"  EnableClientScript="true" 
                                                    EnableTheming="False" EnableViewState="False" Display="Dynamic" 
                                                     Font-Bold="True" ForeColor="White"></asp:RequiredFieldValidator>
                                                    
               <ajaxToolkit:AutoCompleteExtender ID="autoComplete1" runat="server"  TargetControlID="txtJID" ServiceMethod="GetJID"     MinimumPrefixLength="1"></ajaxToolkit:AutoCompleteExtender>
        
                <asp:Label ID="Label2" runat="server" Text="AID" Font-Bold="True" ForeColor="White"></asp:Label>
                
                <asp:TextBox ID="txtAID" runat="server"  onkeyup="SetContextKey()"    AutoPostBack="True" ontextchanged="txtAID_TextChanged"></asp:TextBox>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required." 
                                                    ControlToValidate="txtAID"  EnableClientScript="true" 
                                                    EnableTheming="False" EnableViewState="False" Display="Dynamic" 
                                                    Font-Bold="True" ForeColor="White"></asp:RequiredFieldValidator>
               
               
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteAID" runat="server" TargetControlID="txtAID"    ServiceMethod="GetAID" ContextKey ="" MinimumPrefixLength="1"></ajaxToolkit:AutoCompleteExtender>
        
                <asp:Label ID="Label3" runat="server" Text="STAGE" Font-Bold="True" ForeColor="White"></asp:Label>
                <asp:DropDownList ID="ddlStage" runat="server" AutoPostBack="True" onselectedindexchanged="ddlStage_SelectedIndexChanged">
                    <asp:ListItem>-Select-</asp:ListItem>
                </asp:DropDownList>
         </td>                
        </tr>
        <tr>
            <td>
                <asp:Xml ID="Xml1" runat="server"></asp:Xml>
            </td>
        </tr>
        </table>
    </div>
       &nbsp;&nbsp;
    </form>
</body>
</html>
