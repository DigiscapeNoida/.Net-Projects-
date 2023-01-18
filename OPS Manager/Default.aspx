<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OPSManager._Default" StylesheetTheme="Skin01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">  
  
    <Div style="margin-bottom: 0px"><cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>  
   <table  style="border: thin solid black;text-align:center; font-size: 10pt; font-family: Arial; width: 100%">
        <tr> 
             <td style="width:20%" >  
                Client               
                <asp:DropDownList ID="DDLClient" onchange="javascript:SetContextKey();" runat="server" Height="22px" Width="100px">
                    <asp:ListItem Value="--Select--"></asp:ListItem>
                    <asp:ListItem Value="JWVCH"></asp:ListItem>
                    <asp:ListItem Value="JWUK"></asp:ListItem>
                    <asp:ListItem Value="JWUSA"></asp:ListItem>
                    <asp:ListItem Value="SINGAPORE"></asp:ListItem>
                    <asp:ListItem Value="LWW"></asp:ListItem>
                    <asp:ListItem Value="Thieme"></asp:ListItem>
                </asp:DropDownList>
            </td>                         
            <td style="width:40%" >Select JID &nbsp; &nbsp;<asp:TextBox ID="txtSearch" runat="server"  Width="176px" Height="15px"/>
                 &nbsp;<asp:Button ID="Button3" runat="server" Text="Search"    Width="100px" Height="28px" OnClick="Button3_Click1"  />
            </td>            
            <td>
                <asp:Label ID="Jname" runat="server" Text="Article Journal Name"  Width="239px" Height="27px" style="margin-left: 0px"  ></asp:Label>
            </td>
         
        </tr>      
    </table>
       <cc1:AutoCompleteExtender   
    ID="AutoCompleteExtender1"   
    TargetControlID="txtSearch"   
    runat="server" UseContextKey="True"  ServiceMethod="GetCompletionList" MinimumPrefixLength="2" />            
    </Div>               
 
 </asp:Content>
   
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >  
          <script   type="text/javascript">
              function SetContextKey() {
                  //  alert("Sachin");
                  $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=DDLClient.ClientID %>").value);

              }
              function CheckBlank() {

                  var col = document.getElementById('<%=DDLClient.ClientID %>')
                  if (col[col.selectedIndex].value == "--Select--") {
                      alert("Please Select Client");
                      return false;
                  }
                  return true;

              }
              function ErrMsg() {

                  var col = document.getElementById('<%=DDLClient.ClientID %>')
                  if (col[col.selectedIndex].value == "--Select--")
                 {
                     alert("Please Select Client");
                     return false;              
                  }
                  
                  
                  var PEedit = document.getElementById('<%=  PEEditor.ClientID  %>').value;
                  if (PEedit == "") {
                      alert("Please Enter PE Name.");
                      return false;
                  }
                  var Desig = document.getElementById('<%=  Designation.ClientID  %>').value;
                  if (Desig == "") {
                      alert("Please Enter Designation");
                      return false;
                  }
                  var intPEname = document.getElementById('<%=  inttPename.ClientID  %>').value;
                  if (intPEname == "") {
                      alert("Please Enter Internal PE Name");
                      return false;
                  }


                  var frmEmail = document.getElementById('<%= FromMail.ClientID  %>').value;
                  if (frmEmail == "") {
                      alert("Please Enter (from Email).");
                      return false;
                  }
                  else {
                      try {

                          var maiils = frmEmail.split(",");
                          if (maiils.length < 2) {
                              maiils = frmEmail.split(";");
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

                  var ccEmail = document.getElementById('<%= CCEmail.ClientID  %>').value;
                  if (ccEmail == "") {


                  }
                  else {
                      try {
                          
                          var maiils = ccEmail.split(",");

                          if (maiils.length<2)
                          {
                              maiils = ccEmail.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("CCEmail is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }

                  var PEEmail = document.getElementById('<%= PEEmail.ClientID  %>').value;
                  if (PEEmail == "") {
                      alert("Please Enter (PE Email).");
                      return false;
                  }
                  else {
                      try {

                          var maiils = PEEmail.split(",");
                          if (maiils.length < 2) {
                              maiils = PEEmail.split(";");
                          }

                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("PE Email is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }


                  var PREmail = document.getElementById('<%= PrMail.ClientID  %>').value;
                  if (PREmail == "") {

                  }
                  else {
                      try {
                          var maiils = PREmail.split(",");
                          if (maiils.length < 2) {
                              maiils = PREmail.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("PRE Email is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }
                  var batmail = document.getElementById('<%=  batchamail.ClientID  %>').value;
                  if (batmail == "") {

                  }
                  else {
                      try {
                          var maiils = batmail.split(",");
                          if (maiils.length < 2) {
                              maiils = batmail.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("Batch Email is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }
                  var reviewmail = document.getElementById('<%= reviewMail.ClientID  %>').value;
                  if (reviewmail == "") {

                  }
                  else {
                      try {
                          var maiils = reviewmail.split(",");
                          if (maiils.length < 2) {
                              maiils = reviewmail.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("Reviewer Email is not valid.");
                                  return false;
                              }
                          }
                      }
                      catch (err) {
                          alert(err.message);
                          return false;
                      }
                  }

                  var inttPemailwmail = document.getElementById('<%= inttPemail.ClientID  %>').value;
                  if (inttPemailwmail == "") {
                      alert("Please Enter (Internal PE Email).");
                      return false;
                  }
                  else {
                      try {
                          var maiils = inttPemailwmail.split(",");
                          if (maiils.length < 2) {
                              maiils = inttPemailwmail.split(";");
                          }
                          for (i = 0; i < maiils.length; i++) {
                              var reg = /^\w+([-+.'&]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                              if (reg.test(maiils[i]) == false) {
                                  alert("Internal PE mail is not valid.");
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
        <asp:Panel runat="server" Height="447px" Width="100%" style="margin-left: 0px">
             <table  style="border-color: #808080; font-size: 10pt; font-family: Arial; width:100%">
                 <tr style="width:100%">
                     <td   style="width:100%">         
                    <table  style="border: thin solid #808080; font-size: 10pt; font-family: Arial; width:100% ">

                         <tr>
                                 <td style="font-weight: bold; text-transform: capitalize; color: #008000;width:10% " ></td>
                                 <td style="font-weight: bold; text-transform: capitalize; color: #008000;width:40%">
                                     <asp:Label ID="Label12" runat="server" Text="PE Details"></asp:Label>
                                 </td>
                                 <td style="font-weight: bold; text-transform: capitalize; color: #008000;width:10%"></td>
                                 <td  style="font-weight: bold; text-transform: capitalize; color: #008000;width:40%">
                                     <asp:Label ID="Label13" runat="server" Text="Mail Details"></asp:Label>
                                 </td>
                             </tr>
                         <tr>
                             <td style="padding-left: 10px;" >                      
                                 <asp:Label ID="Label1" runat="server" Text="Name" Width="100%"  ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="PEEditor" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td style="padding-left: 10px;">                         
                                 <asp:Label ID="Label5" runat="server" Text="From Mail" Width="100%"  ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="FromMail" runat="server" Width="100%" TextMode="MultiLine" Height="16px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;" >                        
                                 <asp:Label ID="Label2" runat="server" Text="Prod Email" Width="100%" ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="PEEmail" runat="server" Width="100%" TextMode="MultiLine" Height="16px"></asp:TextBox>
                             </td>
                             <td style="; padding-left: 10px;" >                       
                                 <asp:Label ID="Label6" runat="server" Text="CC Email" Width="100%"  ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="CCEmail" runat="server" Width="100%" TextMode="MultiLine" Height="16px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td style="padding-left: 10px;" >
                                  &nbsp;<asp:Label ID="Label3" runat="server" Text="Personal   Mail:" Width="100%"  ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="PrMail" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td style="; padding-left: 10px;" >                         
                                 &nbsp;<asp:Label ID="Label7" runat="server" Text="Batch Mail:" Width="100%"  ></asp:Label>
                             </td>
                             <td >
                                 <asp:TextBox ID="batchamail" runat="server" Width="100%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;">                         
                                 <asp:Label ID="Label4" runat="server" Text="Designation" Width="100%" ></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="Designation" runat="server" Width="100%"></asp:TextBox>
                             </td>
                             <td style="; padding-left: 10px;"></td>
                             <td></td>
                         </tr>
                   </table>
           </td>                 
                 </tr>
                 <tr style="width:100%">
                    <td  style="width:100%">
                   <table style="border: thin solid #808080; font-size: 10pt; font-family: Arial; width: 100%; height: 115px; margin-left: 0px;">
                                <tr style="width: 100%;">
                                     <td style="font-weight: bold; text-transform: capitalize; color: #008000" width="10%"></td>
                                     <td style="font-weight: bold; text-transform: capitalize; color: #008000" width="40%">
                                         <asp:Label ID="Label14" runat="server" Text="Reviewer Info"></asp:Label>
                                     </td>
                                     <td style="font-weight: bold; text-transform: capitalize; color: #008000" width="10%"></td>
                                     <td style="font-weight: bold; text-transform: capitalize; color: #008000" width="40%">
                                         <asp:Label ID="Label15" runat="server" Text="Internal PE Info"></asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td style="padding-left: 10px;">
                                         <asp:Label ID="Label8" runat="server" Text="Role" Width="100%"  ></asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="Role" runat="server" Width="100%"></asp:TextBox>
                                     </td>
                                     <td style="padding-left: 10px;">
                                         <asp:Label ID="Label10" runat="server" Text="PE Name" Width="100%"  ></asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="inttPename" runat="server" Width="100%"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td style="padding-left: 10px;">
                                         <asp:Label ID="Label9" runat="server" Text="Reviewer Mail:" Width="100%" ></asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="reviewMail" runat="server" Width="100%"></asp:TextBox>
                                     </td>
                                     <td style="padding-left: 10px;">
                                         <asp:Label ID="Label11" runat="server" Text="PE Mail" Width="100%" ></asp:Label>
                                     </td>
                                     <td >
                                         <asp:TextBox ID="inttPemail" runat="server" Width="100%" ></asp:TextBox>
                                     </td>
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
            width: 147px;
            height: 50px;
        }
        .auto-style12 {
            width: 484px;
            height: 50px;
        }
        .auto-style13 {
            height: 50px;
        }
    </style>

</asp:Content>

