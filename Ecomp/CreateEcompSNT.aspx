<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateEcompSNT.aspx.cs" Inherits="CreateEcompSNT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>  
                <tr>  
                    <td>Title<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>  
                    </td>  
  
               </tr>  
                <tr>  
                    <td>ISBN<span style="color:red">*</span></td>  
                     <td> <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox></td>  
                </tr>  
                <tr>  
                    <td>Contact Name<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>  
                    </td>  
                </tr>  
                 
                <tr>  
                    <td>Contact Email<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtContactEmail" runat="server" ReadOnly></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>Recipient Name<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtRecipientName" runat="server" ></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>Recipient Email<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtRecipientEmail" runat="server" ReadOnly></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>SurName<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtSurName" runat="server"></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>MailCC</td>  
                    <td>  
                        <asp:TextBox ID="txtMailCC" runat="server"></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>MailBCC</td>  
                    <td>  
                        <asp:TextBox ID="txtMailBCC" runat="server"></asp:TextBox>  
                    </td>  
                </tr>
               <%-- <tr>  
                    <td>Cover Path<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtCoverPath" runat="server"></asp:TextBox>  
                    </td>  
                </tr>
                <tr>  
                    <td>Pdf Path<span style="color:red">*</span></td>  
                    <td>  
                        <asp:TextBox ID="txtPdfPath" runat="server"></asp:TextBox>  
                    </td>  
                </tr>--%>
                <tr>  
                    <td>Upload cover<span style="color:red">*</span></td>  
                    <td>  
                       <asp:FileUpload ID="FileUpload1" runat="server"  Font-Size="Medium" Height="38px" Width="301px" />  
                    </td>  
                </tr> 
                <tr>  
                    <td>Upload pdf<span style="color:red">*</span></td>  
                    <td>  
                       <asp:FileUpload ID="FileUpload2" runat="server"  Font-Size="Medium" Height="38px" Width="301px" />  
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnsubmitClick" />  
                    </td>  
                </tr>  
                
            </table>  
    </div>
    </form>
</body>
</html>
