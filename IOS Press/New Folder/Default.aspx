<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IOS Press (Ver 1.0)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="XX-Large" Height="27px"
            Style="z-index: 100; left: 384px; position: absolute; top: 77px" Text="Online Flow Sheet"
            Width="265px"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/CompLogo.gif" Style="z-index: 101;
            left: 869px; position: absolute; top: 23px" />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Journal ID" style="z-index: 102; left: 18px; position: absolute; top: 154px" Height="19px"></asp:Label>
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<br />
        <asp:Label ID="Label2" runat="server" Text="Article ID" style="z-index: 103; left: 18px; position: absolute; top: 186px" Height="19px" Width="61px"></asp:Label>
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtAID" runat="server" style="z-index: 104; left: 111px; position: absolute; top: 184px" Height="17px" Width="155px"></asp:TextBox>
        <asp:Label ID="lblErrorMsg" runat="server" Font-Size="Large" Style="z-index: 105;
            left: 237px; position: absolute; top: 335px"
            Width="240px" ForeColor="Red"></asp:Label>
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Style="z-index: 115;
            left: 887px; position: absolute; top: 143px" Text="Logout" Width="91px" />
        <hr style="height: 1px" />
        <br />
        <asp:Label ID="Label3" runat="server" Style="z-index: 107; left: 18px; position: absolute;
            top: 220px" Text="Vol Issue" Width="60px"></asp:Label>
        <asp:TextBox ID="txtVIS" runat="server" Height="17px" OnTextChanged="TextBox3_TextChanged"
            Style="z-index: 108; left: 111px; position: absolute; top: 216px" Width="155px"></asp:TextBox>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/td_logo.jpg" Style="z-index: 109;
            left: 14px; position: absolute; top: 10px" />
        &nbsp;&nbsp;
        <asp:DropDownList ID="DDL" runat="server" DataValueField="All" Style="z-index: 110;
            left: 113px; position: absolute; top: 154px" Width="155px">
            <asp:ListItem Selected="True">ALL</asp:ListItem>
            <asp:ListItem>CHM</asp:ListItem>
            <asp:ListItem>HSM</asp:ListItem>
            <asp:ListItem>IFS</asp:ListItem>
            <asp:ListItem>JRS</asp:ListItem>
            <asp:ListItem>JVR</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="Display Info" OnClick="Button1_Click1" style="z-index: 111; left: 360px; position: absolute; top: 154px" Width="184px" Height="22px" /></div>
        <asp:datagrid id="MyDataGrid" style="Z-INDEX: 112; LEFT: 15px; POSITION: absolute; TOP: 245px"
				runat="server" AllowSorting="True" HorizontalAlign="Justify" BackColor="White" AutoGenerateColumns="False"
				HeaderStyle-BackColor="#aaaadd" Font-Size="8pt" Font-Name="Verdana" CellPadding="4" BorderWidth="1px"
				BorderColor="#3366CC" Font-Names="Verdana" ShowFooter="True" BorderStyle="None" Height="128px"
				Width="467px" OnSelectedIndexChanged="MyDataGrid_SelectedIndexChanged">
				<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
				<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
				<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="JID" HeaderText="JID" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="AID" HeaderText="AID" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="VolIss" HeaderText="Publication details (vol/issue/page nrs)"  HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtReceipt" HeaderText="Date of Receipt" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="Authors" HeaderText="Authors" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>					
					<asp:BoundColumn DataField="TSPages" HeaderText="Typeset Pages" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtProofsSent_Author" HeaderText="Date of PDF proof sent to authors" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtCorrectionsRec_Author" HeaderText="Date of corrections received from authors" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="AuthorReq" HeaderText="Special Reqirements from Authors" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtProofsSent_Editor" HeaderText="Date of proof sent to Editor-in-Chief" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtCorrectionsRec_Editor" HeaderText="Date of corrections received from Editor-in-Chief" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtProofsSent_Editorial" HeaderText="Date of proof sent to Editorial Assistant" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="DtCorrectionsRec_Editorial" HeaderText="Date of corrections received from Editorial Assistant" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>										
					<asp:BoundColumn DataField="IssComDt" HeaderText="Confirmation Date to issue compilation from IOS" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="CRCDeliveryDt" HeaderText="Date of CRC delivery to IOS" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="PrintPDFDT" HeaderText="Confirmation Date of Print/Screen pdf" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="PrintPDF2IOSDt" HeaderText="Date of Print/Screen pdf sent to IOS" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
					<asp:BoundColumn DataField="TsRemarks" HeaderText="Thomson Remarks" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Export to Excel" style="z-index: 113; left: 361px; position: absolute; top: 185px" Height="22px" Width="184px" />
        <asp:Button ID="Button2" runat="server" Text="Display Report" OnClick="Button2_Click" style="z-index: 114; left: 361px; position: absolute; top: 216px" Height="22px" Width="184px" Visible="False" />&nbsp; &nbsp;
    </form>
</body>
</html>
