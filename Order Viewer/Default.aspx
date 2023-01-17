<%@ Page EnableViewState="true" Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Order Viewer</title>
    <meta charset="utf-8"/> 
    <meta http-equiv="Pragma" content="no-cache" /> 
    <meta http-equiv="Expires" content="-1" /> 
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <script type="text/javascript" language="javascript">
        function ClearTextboxes()
        {
            document.getElementById('txtOrderNotFound').value = '';
            return false;
        }
        function TABLE1_onclick()
        {
          return true;
        }
    </script>
</head>
<body bgcolor="#f7f7f7" style="margin:0px">
    <form id="form1" runat="server" >
    <div>
        <table border="0"  cellpadding="0" cellspacing="0" style="width: 100%; height: 10%; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;" id="TABLE1" onclick="return TABLE1_onclick();">
            
            <tr style="width: 100%; height: 128px; background-color: #f2f2f2">
                <td colspan="2" style=" background-image: url('Header1.jpg'); background-repeat: no-repeat">
                </td>
            <tr>    
            <tr>
                <td colspan="2" style="text-align:center">
                    <!--img src="header.gif" style="width: 100%; position: relative" alt="Order Viewer" /-->
                    <a href="EMCOrder.aspx" id="EmcLink"><b>EMCOrder</b></a>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkadvanced"  runat="server" Font-Size="Large" CausesValidation="False"  ForeColor="Black" OnClick="lnkadvanced_Click"  ToolTip="Click here to go Advanced Mode">AdvancedMode</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnksave"      runat="server" Font-Size="Large" ForeColor="Black" OnClick="lnksave_Click" ToolTip="Save">Save</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkprint"     runat="server" Font-Size="Large" CausesValidation="False"  ForeColor="Black" OnClientClick=" window.print();return false;"   OnClick="lnkprint_Click"  ToolTip="Print">Print</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="LogoutButton" runat="server" Font-Size="Large" onclick="LogoutButton_Click"  CausesValidation="False" >Log out</asp:LinkButton>
                </td>
            </tr>
            <%--<tr style="width:100% ;height:10px">
            <td colspan="2" style="height: 0">
            <table style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; background-color: #f2f2f2;" cellpadding="0" cellspacing="0">
            <tr>
                                <td style="width: 1%; height:34px ;border-width:0"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/saveXML.gif" OnClick="ImageButton1_Click" /></td>
                                <td style="width: 1%; height:34px;border-width:0"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/print.gif" OnClientClick=" window.print();return false;" OnClick="ImageButton3_Click" /></td>                                  
                                <td style="width: 2%; height:34px;border-width:0"><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/home.gif"  OnClick="ImageButton4_Click" CausesValidation="False"/></td>
                                <td style="width: 1%; height:34px;border-width:0"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/blank.gif" CausesValidation="False" /></td>
                                <td colspan="2" style="width: 9%; height:34px;border-width:0;background-image:url(blank.gif); background-position:center s" ></td>
            </tr>
            </table>
            </td>
             #0e5282
            </tr>--%>
            <tr style="width: 22%;height: 100%; background-color: #990000; color: #FFFFFF;">
                <td style="height: 99px; width: 238px"  >                   
                    <asp:ListBox ID="lbOrderList" runat="server" Width="100%"  Height="118px" OnSelectedIndexChanged="lbOrderList_SelectedIndexChanged" AutoPostBack="True" ToolTip="These orders match the input"></asp:ListBox>                    
                </td>
                <td style="width: 74%;height: 99px " valign="top">
                        <table style="width: 100%; height: 100px" >
                            <tr>
                                <td style="width:28%; height: 54%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px"><asp:Label ID="Label1" runat="server" Text="JID" Font-Bold="True" ForeColor="White"></asp:Label>
                                    </td>
                                            <td style="width: 39px">
                                                <asp:TextBox ID="txtJID" runat="server" BorderStyle="Solid" OnTextChanged="txtJID_TextChanged" BorderColor="SeaGreen" BorderWidth="1px"  ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;">
                                    </td>
                                            <td style="width: 100%; height: 19px;" >
                                    <asp:RequiredFieldValidator ID="rfvJID" runat="server" ErrorMessage="* Required." 
                                                    ControlToValidate="txtJID"  Width="155px" EnableClientScript="true" 
                                                    EnableTheming="False" EnableViewState="False" Display="Dynamic" 
                                                    style="position: relative" Font-Bold="True" ForeColor="White"></asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                                    </td>
                                <td style="width: 32%; height: 54%;" valign="top" >
                                    <table>
                                        <tr>
                                            <td style="width: 46px">
                                            <asp:Label ID="Label2" runat="server" Text="AID/ISSUE" Font-Bold="True" ForeColor="White"></asp:Label>
                                    </td>
                                            <td style="width: 28px">
                                                <asp:TextBox ID="txtAid" runat="server" BorderStyle="Solid" BorderColor="SeaGreen" BorderWidth="1px" ></asp:TextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 46px; height: 14px;">
                                    </td>
                                            <td style="width: 100%; height: 14px;">
                                    <asp:RequiredFieldValidator ID="rfvAid" runat="server" ErrorMessage="* Required." 
                                                    ControlToValidate="txtAid"  Width="154px" EnableClientScript="true" 
                                                    EnableTheming="False" EnableViewState="False" Display="Dynamic" 
                                                    style="position: relative" Font-Bold="True" ForeColor="White"></asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="height: 54%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px; height: 21px;">
                                    <asp:Label ID="Label3" runat="server" Text="STAGE" Font-Bold="True" ForeColor="White"></asp:Label>
                                    </td>
                                            <td style="width: 42px; height: 21px;">
                                    <asp:TextBox ID="txtStage" runat="server" BorderStyle="Solid" BorderColor="SeaGreen" BorderWidth="1px" ></asp:TextBox>
                                    </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;">
                                            </td>
                                            <td style="width: 42px; height: 19px;">
                                            </td>
                                        </tr>
                                    </table>
                                    </td>                                
                                <td style="width: 100%; height: 54%;" valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 100px; height: 20px;">
                                    </td>
                                            <td style="width: 100px; height: 20px;">
                                    <asp:Button ID="btnViewOrder" runat="server" Text="View Order" BackColor="White" 
                                                    OnClientClick="javascript:ClearTextboxes();"
                                                    BorderColor="SeaGreen" BorderStyle="Solid" Font-Bold="True" 
                                                    OnClick="btnViewOrder_Click" Height="22px" style="position: relative" 
                                                    ToolTip="Click to view order" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;">
                                            </td>
                                            <td style="width: 100px; height: 19px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>                                
                            </tr>
                            </table>
                    <asp:Label ID="txtOrderNotFound"   runat="server" Style="position:relative" 
                            Width="723px" Font-Bold="True" ForeColor="White"></asp:Label></td>
                              
            </tr>
            
        </table>
    </div>
        <asp:Xml ID="Xml1" runat="server"></asp:Xml>
        &nbsp;&nbsp;
    </form>
</body>
</html>
