<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Login_Register" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <h2 style="color:Yellow"> Registration Form &nbsp; &nbsp; 
           <asp:Label ID="lblsbmitmsg" 
               runat="server" Text="Submitted Successfully" Font-Bold="True" 
               ForeColor="#009933" Font-Size="Large" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
           </asp:ScriptManager>
       </h2>
       
    <table style="width: 719px">
    <tr>
        <td >
            <asp:Label ID="lblUserId" runat="server" Text="User ID" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUserId" runat="server" MaxLength="5" ></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtUserId"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter User Id." />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtUserId" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
        </td>
     </tr>
    
    <tr>
        <td >
            <asp:Label ID="lblUserPwd" runat="server" Text="User Password" Font-Bold="True"></asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password" ></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtUserPwd"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter Password." />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtUserPwd" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblConfPwd" runat="server" Text="Confirm Password" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtConfPwd" runat="server" TextMode="Password" ></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtUserPwd" ControlToValidate="txtConfPwd" 
            ErrorMessage="CompareValidator">Not Matched</asp:CompareValidator>
        </td>
    </tr>  
     <tr>
        <td>
            <asp:Label ID="lblrole" runat="server" Text=" Role " Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlrole" runat="server" Width="100px"></asp:DropDownList>
        </td>
     </tr>
     <tr>
        <td>
            <asp:Label ID="lbllocation" runat="server" Text=" Location" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddllocation" runat="server" Width="100px"></asp:DropDownList>
        </td>
     </tr>
    <%--<tr><td ><asp:Label ID="lblName" runat="server" Text=" User Full Name" Font-Bold="True"></asp:Label></td>
    <td><asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
        </td></tr>
    --%>
    <tr>
        <td >
            <asp:Label ID="lblEmailId" runat="server" Text="Your Email-Id" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmailId" runat="server" ></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEmailId"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter Email Id." />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtEmailId" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtEmailId" ErrorMessage="RegularExpressionValidator" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email-Id 
            is not well formed</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
    <td align="right" colspan="2">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="82px" 
            Font-Bold="True" BackColor="#FFCC99" onclick="btnSubmit_Click" />
    </td>
    </tr>
    </table>
    
    
</asp:Content>

