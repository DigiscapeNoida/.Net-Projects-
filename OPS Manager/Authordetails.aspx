<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Authordetails.aspx.cs" Inherits="OPSManager.Authordetails" StylesheetTheme="Skin01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">  
  
    <Div style="margin-bottom: 0px"><cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>  
  
         <table  style="border: thin solid black; font-size: 10pt; font-family: Arial; width: 100%">
        <tr>            
            <td style="width:20%">Client&nbsp;
                <asp:DropDownList ID="DDLClient" onchange="javascript:SetContextKey1();" runat="server" Height="26px">
                    <asp:ListItem Value="--Select--"></asp:ListItem>
                    <asp:ListItem Value="JWVCH"></asp:ListItem>
                    <asp:ListItem Value="JWUK"></asp:ListItem>
                    <asp:ListItem Value="JWUSA"></asp:ListItem>
                    <asp:ListItem Value="SINGAPORE"></asp:ListItem>
                    <asp:ListItem Value="LWW"></asp:ListItem>
                    <asp:ListItem Value="Thieme"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td  style="width:40%">Select JID &nbsp;<asp:TextBox ID="txtSearch" runat="server"  Width="176px" Height="15px" onkeyup="SetContextKey()"  />
                 &nbsp;<asp:Button ID="Button3" runat="server" Text="Search"    Width="100px" Height="28px" OnClick="Button3_Click1"   />
            </td>            
            <td >
                <asp:Label ID="Jname" runat="server" Text="Article Journal Name"  Width="459px" Height="27px" style="margin-left: 0px"  ></asp:Label>
            </td>
         
        </tr>      
    </table>
       <cc1:AutoCompleteExtender   
    ID="AutoCompleteExtender1"   
    TargetControlID="txtSearch"   
    runat="server" UseContextKey="True"  ServiceMethod="GetCompletionList" MinimumPrefixLength="2" />         
        </div>
 </asp:Content>
   
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >  
          <script   type="text/javascript">

              function SetContextKey1() {
                  $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=DDLClient.ClientID %>").value);
                }

              function SetContextKey()
              {                                 
                  $find('<%=AutoCompleteExtender2.ClientID%>').set_contextKey($get("<%= txtSearch.ClientID %>").value);
              }

              function ErrMsg() {

                  var cornamee = document.getElementById('<%=  Corname.ClientID  %>').value;
                  if (cornamee == "") {
                      alert("Please Enter Cor Name.");
                      return false;
                  }

                  //   var ipdfNaame = document.getElementById('<%=  pdfNaame.ClientID  %>').value;
                  //  if (ipdfNaame == "") {
                  //      alert("Please Enter PDF Name");
                  //      return false;
                  //  }
                  
                  var Email = document.getElementById('<%= CprEmail.ClientID  %>').value;
                  if (Email == "") {
                      alert("Please Enter (from Email).");
                      return false;
                  }
                  else {
                      try {

                          var maiils = Email.split(",");
                          if (maiils.length < 2) {
                              maiils = Email.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("FromEmail is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }


                  var Email = document.getElementById('<%= EmailCC.ClientID  %>').value;
                  if (Email == "") {
                     
                      return true ;
                  }
                  else {
                      try {

                          var maiils = Email.split(",");
                          if (maiils.length < 2) {
                              maiils = Email.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("CC Email is not valid.");
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
     &nbsp;<form method="post" Width="100%"  >
        <asp:Panel ID="Panel1" runat="server" Height="447px" Width="100%" style="margin-left: 0px">
             <table  style="border-color: #808080; font-size: 10pt; font-family: Arial; width:100%">
                 <tr style="width:100%">
                     <td   style="width:100%">         
                    <table  style="border: thin solid #808080; font-size: 10pt; font-family: Arial; width:100% ">

                         <tr>
                                 <td style="width:20%;padding-left: 10px;" >&nbsp;
                                     <asp:Label ID="Label4" runat="server" Text="Aid" ToolTip="Select Aid" Width="100%"></asp:Label>
                                 </td>
                                 <td style="font-weight: bold; text-transform: capitalize; color: #008000;">
                                     <asp:TextBox ID="txtAid" runat="server" Width="100%"   onkeyup="SetContextKey()"   ></asp:TextBox>
                                 </td>
                                 <td  style="font-weight: bold; text-transform: capitalize; color: #008000;width:30%">
                                         <cc1:AutoCompleteExtender   
                                              ID="AutoCompleteExtender2"    
                                                 TargetControlID="txtAid"   
                                                runat="server"  UseContextKey="True"  ServiceMethod="GetCompletionListAid" MinimumPrefixLength="1"  />            
                                  </td>
                             </tr>
                         <tr>
                             <td style="padding-left: 10px;" >                      
                                 <asp:Label ID="Label1" runat="server" Text="Cor Name" Width="100%" ToolTip="Correspondence author name"  ></asp:Label>
                             </td>
                             <td class="auto-style11" >
                                 <asp:TextBox ID="Corname" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;" >                        
                                 <asp:Label ID="Label2" runat="server" Text="Title" Width="100%" ></asp:Label>
                             </td>
                             <td class="auto-style11" >
                                 <asp:TextBox ID="Title" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;" >
                                  &nbsp;<asp:Label ID="Label3" runat="server" Text="Cor Email" Width="100%"  ></asp:Label>
                             </td>
                             <td class="auto-style11" >
                                 <asp:TextBox ID="CprEmail" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;">                         
                                 <asp:Label ID="Label5" runat="server" Text="Cor Email CC" Width="100%"></asp:Label>
                             </td>
                             <td class="auto-style11">
                                 <asp:TextBox ID="EmailCC" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;">
                                 <asp:Label ID="PDFName" runat="server" Text="PDF Name" Width="100%"></asp:Label>
                             </td>
                             <td class="auto-style11">
                                 <asp:TextBox ID="pdfNaame" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td></td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;">&nbsp;</td>
                             <td class="auto-style11">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                   </table>
           </td>                 
                 </tr>
                 <tr>
                       <td style="float: right;text-align:right" width="100%" ">
                     <asp:Button ID="Button2" runat="server" Text="Edit"   OnClick="Edit_Click"     CausesValidation="false" Width="105px" />
                     <asp:Button ID="Button1" runat="server" Text="Update" OnClick="Button1_Click"  OnClientClick="return ErrMsg();"  CausesValidation="true" Width="105px" />
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
    </style>

</asp:Content>

