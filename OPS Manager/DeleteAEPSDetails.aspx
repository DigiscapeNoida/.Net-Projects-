<%@ Page Language="C#"  MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="DeleteAEPSDetails.aspx.cs" Inherits="OPSManager.DeleteAEPSDetails" StylesheetTheme="Skin01"  ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">  
  
    <Div style="margin-bottom: 0px"><cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>  
   <table style="border: thin solid black;text-align:center;font-size: 10pt; font-family: Arial; width: 100%; height: 0;">
        <tr>
            <td class="auto-style15" >  
                Client
                &nbsp;         
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
            <td style="background-color:white;text-align:center" >&nbsp;Select JID &nbsp; &nbsp;<asp:TextBox onkeyup="SetContextKey2()" ID="txtSearch"   runat="server"  Width="176px" Height="15px"  />
                 &nbsp;<asp:Button ID="Button3" runat="server" Text="Search"    Width="100px" Height="28px" OnClick="Button3_Click1"  />
            </td>            
            <td style="font-family:Arial; Font-Size:16pt; background-color:white;"class="auto-style16" >
                <asp:Label ID="Jname" runat="server" Text="Article Journal Name"  Width="270px" Height="27px" style="margin-left: 0px" ></asp:Label>
            </td>
         
        </tr>      
    </table>
       <cc1:AutoCompleteExtender   
    ID="AutoCompleteExtender1"   
    TargetControlID="txtSearch"   
    runat="server" UseContextKey="True"   ServiceMethod="GetCompletionList" MinimumPrefixLength="2" />            
    </Div>               
 
 </asp:Content>
   
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >  
          <script   type="text/javascript">            

              function SetContextKey() {              
                  $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=DDLClient.ClientID %>").value);                  
                 }
              function SetContextKey2() {
                  
                  $find('<%=AutoCompleteExtender2.ClientID%>').set_contextKey($get("<%=HFopsid.ClientID %>").value);

                 }

              function ErrMsg() {

                  var aid = document.getElementById('<%=  txtaid.ClientID  %>').value;
                  if (aid == "")
                  {
                      alert("Aid Cant be blank.!!!");
                      return false;
                  }

                  var opsid = document.getElementById('<%=  HFopsid.ClientID  %>').value;
                  if (opsid == "") {
                      alert("OPSID Cant be blank.!!!");
                      return false;
                  }
                  if (confirm("Are you sure to delete?") == true) {
                      return true;
                  }
                  else {
                      return false;
                  }                  
              }
     </script> 
    &nbsp;<form method="post" Width="100%"  >
        <asp:Panel ID="Panel1" runat="server" Height="447px" Width="100%" style="margin-left: 0px">
             <table  style="border-color: #808080; font-size: 10pt; font-family: Arial; width:100%">
                 <tr style="width:100%">
                     <td   style="width:100%">         
                    <table  style="border: thin solid #808080; font-size: 10pt; font-family: Arial; width:100% ">

                         <tr>
                                 <td style="padding-left: 10px;width:10%" >&nbsp;
                                     <asp:Label ID="Label4" runat="server" Text="Aid" ToolTip="Select Aid" Width="100%"></asp:Label>
                                 </td>
                                 <td >
                                     <asp:TextBox ID="txtaid" onkeydown="SetContextKey2()"  runat="server" Width="100%"></asp:TextBox>
                                 </td>
                                 <td  style="padding-left: 10px;width:20%">
                                      <cc1:AutoCompleteExtender   
                                              ID="AutoCompleteExtender2"    
                                                 TargetControlID="txtaid"   
                                                runat="server"  UseContextKey="True"  ServiceMethod="GetCompletionListAid" MinimumPrefixLength="1"  />      
                                 </td>
                             </tr>
                         <tr>
                             <td style="padding-left: 10px;" >                      
                                 <asp:Label ID="Label1" runat="server" Text="Client" Width="100%" ToolTip="Correspondence author name"  ></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtclient" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;">                        
                                 <asp:Label ID="Label2" runat="server" Text="Title" Width="100%" ></asp:Label>
                             </td>
                             <td class="auto-style13" >
                                 <asp:TextBox ID="Title" runat="server" Width="100%"  TextMode="multiline" Height="43px"></asp:TextBox>
                             </td>
                             <td class="auto-style12" >
                                 </td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;" >
                                  &nbsp;</td>
                             <td class="auto-style11" >
                                 &nbsp;</td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;">                         
                                 &nbsp;</td>
                             <td class="auto-style11">
                                 &nbsp;</td>
                             <td></td>
                         </tr>
                   </table>
                   </td>                 
                 </tr>
                   <tr>
                       <td style="float: right;text-align:right" width="100%" ">                   
                            <asp:Button ID="Button1" runat="server" Text="Detele" OnClick="Button1_Click"  OnClientClick="return ErrMsg();"  CausesValidation="true" Width="105px" />
                        <asp:HiddenField ID="HFopsid" runat="server" />
                        </td>
                  </tr>

               </table>               
        </asp:Panel>
        
      </form>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">

    <style type="text/css">
        .auto-style11 {
            width: 40%;
        }
        .auto-style12 {
            height: 72px;
        }
        .auto-style13 {
            width: 40%;
            height: 72px;
        }
        .auto-style14 {
            width: 527px;
            height: 46px;
        }
        .auto-style15 {
            width: 177px;
            height: 46px;
        }
        .auto-style16 {
            height: 46px;
        }
    </style>

</asp:Content>



