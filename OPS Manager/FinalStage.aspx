<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalStage.aspx.cs" Inherits="OPSManager.FinalStage" MasterPageFile="~/Site.Master"  StylesheetTheme="Skin01"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">  
  
    <Div style="margin-bottom: 0px"><cc1:ToolkitScriptManager ID="ScriptManager1" runat="server"></cc1:ToolkitScriptManager>  
   <table align="center" style="border: thin solid black; font-size: 10pt; font-family: Arial; width: 100%; height: 0;">
        <tr>            
            <td style="background-color:white; " align="left" valign="middle" width= "545px;"> &nbsp; &nbsp; &nbsp;Select JID &nbsp; &nbsp;<asp:TextBox ID="txtSearch" runat="server"  Width="276px" Height="15px"  />
                 &nbsp;<asp:Button ID="Button3" runat="server" Text="Search"    Width="100px" Height="28px" OnClick="Button3_Click1"  />
            </td>            
            <td style="font-family:Arial; Font-Size:16pt; background-color:white;" valign="left" >
                <asp:Label ID="Jname" runat="server" Text="Article Journal Name"  Width="459px" Height="27px" style="margin-left: 0px"  ></asp:Label>
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
              function ErrMsg() {
                  if (confirm("Are you sure to Update?") == true) {
                      return true;
                  } else {
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
                                 <td style="padding-left: 10px;" colspan="3" >Select Aid to&nbsp; move the article to final InProgress.</td>
                             </tr>
                         <tr>
                             <td style="padding-left: 10px;width:10%">&nbsp;
                                 <asp:Label ID="Label4" runat="server" Text="Aid" ToolTip="Select Aid" Width="100%"></asp:Label>
                             </td>
                             <td>
                                 <asp:DropDownList ID="DDList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDList_SelectedIndexChanged" ToolTip="Select Aid" Width="100%">
                                 </asp:DropDownList>
                             </td>
                             <td style="padding-left: 10px;width:20%">&nbsp;</td>
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
                             <td style="; padding-left: 10px;" >
                                  &nbsp;</td>
                             <td class="auto-style11" >
                                 &nbsp;</td>
                             <td >
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="; padding-left: 10px;">                         
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
                            <asp:Button ID="Button1" runat="server" Text="InProcess" OnClick="Button1_Click"  OnClientClick="return ErrMsg();"  CausesValidation="true" Width="105px" />
                        <asp:HiddenField ID="RevID" runat="server" />
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


