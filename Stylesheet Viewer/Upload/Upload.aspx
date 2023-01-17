<%@ Page MasterPageFile= "~/MasterPage.master" Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 419px">
        <tr>
            <td style="height: 98px; width: 147px;">
            </td>
            <td style="width: 583px; height: 98px">
            </td>
            <td style="height: 98px" align="right" valign="top">
                <asp:LinkButton ID="HomeLinkButton" runat="server" BackColor="White" 
                    ForeColor="Blue" onclick="HomeLinkButton_Click">Home</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LogoutLinkButton" runat="server" ForeColor="Blue" 
                    onclick="LogoutLinkButton_Click">Signout</asp:LinkButton>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 69px; width: 147px;" align="right" valign="middle">
                </td>
            <td style="width: 583px; height: 69px" align="left" valign="middle">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="Zip fIle path:"
                    Width="102px"></asp:Label>
                &nbsp;&nbsp;    
                <asp:FileUpload ID="FileUpload1" runat="server" 
                style="position: relative; width: 276px; height: 24px; top: 1px; left: -10px;" 
                BorderStyle  = "Solid"  BorderWidth="1px" >
                </asp:FileUpload>
                <asp:Button  ID="Button1" runat="server" OnClientClick="return ErrMsg();"  onclick="Button1_Click" 
                style="position: relative; height: 25px; width: 73px; top: 2px; left: 11px;" 
                Text="Upload" />
            </td>
            <td style="height: 69px">
                </td>
        </tr>
        <tr>
            <td style="width: 147px" valign="top">
             <asp:Panel ID="Panel1" runat="server"  
                    
                    style="position: relative; top: 8px; left: 6px; height: 142px; width: 304px;">
                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; float: left; height: 107px; clear:left;">
                     <tr>
                         <td style="height: 25px" align="center" colspan="2">
                              <asp:Label ID="LblUploadedFileStatus" runat="server" BorderStyle="None" Font-Bold="True" 
                                  ForeColor="Blue"></asp:Label></td>
                     </tr>
                     <tr>
                         <td style="float: left; width: 147px;" align="left">
                             <asp:Label ID="LblFileName"  runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="float: left;">
                             <asp:Label ID="LblFileNameValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="height: 23px; width: 147px;" align="left">
                             <asp:Label ID="LblTotalFiles" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="height: 23px">
                             <asp:Label ID="LblTotalFilesValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="height: 17px; width: 147px;" align="left">
                             <asp:Label ID="LblXMLFILES" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="height: 17px">
                             <asp:Label ID="LblXMLFILESValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td align="left" style="width: 147px">
                             <asp:Label ID="LblTXTFILES" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left">
                             <asp:Label ID="LblTXTFILESValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                 </table>
            </asp:Panel>
            </td>
            <td style="width: 583px" valign="top">
            <table border="1" width="100%" style="vertical-align:top">
            <asp:Repeater OnItemCreated= "LogStatusRepeater_ItemCreated" id="Repeater1" runat="server"  >
            <HeaderTemplate>
                  <tr>
                    <td><b>S.No.</b></td>
                    <td><b>File Name</b></td>
                    <td><b>Status</b></td>
                    <td><b>FMSStatus</b></td>
                  </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                  <td> <%# DataBinder.Eval(Container.DataItem, "Seqno") %> </td>
                  <td> <%# DataBinder.Eval(Container.DataItem, "FileName") %> </td>
                  <td><asp:Label id="statusLabel" runat= "server"> <%# DataBinder.Eval(Container.DataItem, "Result") %> </asp:Label></td>
                  <td><asp:Label id="FMSStatusLabel" runat= "server"> <%# DataBinder.Eval(Container.DataItem, "FMSResult") %> </asp:Label></td>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content> 
