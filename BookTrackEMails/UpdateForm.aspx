<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="UpdateForm.aspx.cs" Inherits="UpdateForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2 style="color:Yellow">Edit Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
<script type="text/javascript">
    function Showalert(username) {
        alert("This "+username + ' details updated successfully.');
        if (alert) {
            window.location = 'Order_Viewer.aspx';
        }
    }
</script>
<style type="text/css">
        .tb8 {
	    width: 230px;
	    border: 1px solid #3366FF;
	    border-left: 4px solid #3366FF;
        }
        .tb7 {
	    width: 230px;
	    border: 1px solid #3366FF;
	    border-left: 4px solid #3366FF;
        }
    </style>
    <div>
<center>
<table cellSpacing="1" cellPadding="0" align="center" bgColor="#f6691f" border="1">
				<tr>
					<td bgColor="#ffffff">
						<table cellSpacing="2" cellPadding="2" align="center" border="0">
							<%--<tr>
								<td>Doc Sub Type</td>
								<td><asp:dropdownlist id="drplstTitle" runat="server">
										<asp:ListItem Value="--Selet--">--Selet--</asp:ListItem>
										<asp:ListItem Value="Mr.">App</asp:ListItem>
										<asp:ListItem Value="Miss.">Chp</asp:ListItem>
										<asp:ListItem Value="Mrs.">Htu</asp:ListItem>
										<asp:ListItem Value="Dr.">Pre</asp:ListItem>
									</asp:dropdownlist></td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td></td>
								<td></td>
							</tr>--%>
							<tr>
								<td>CID</td>
								<td><asp:textbox id="txtcid" runat="server"></asp:textbox></td>
								<td></td>
								<td>CNO</td>
								<td><asp:textbox id="txtcno" runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>PII</td>
								<td><asp:textbox id="txtpii" runat="server"></asp:textbox></td>
								<td></td>
								<td>DOI</td>
								<td><asp:textbox id="txtdoi" runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>AID</td>
								<td><asp:textbox id="txtaid" runat="server"></asp:textbox></td>
								<td></td>
								<td>Doc Sub Type</td>
								<td><asp:dropdownlist id="ddlsubtype" runat="server" Width="128px">
										<asp:ListItem Value="--Selet--">--Selet--</asp:ListItem>
										<asp:ListItem Value="app">app</asp:ListItem>
										<asp:ListItem Value="ack">ack</asp:ListItem>
										<asp:ListItem Value="bib">bib</asp:ListItem>
										<asp:ListItem Value="bio">bio</asp:ListItem>
										<asp:ListItem Value="chp">chp</asp:ListItem>
										<asp:ListItem Value="ctr">ctr</asp:ListItem>
										<asp:ListItem Value="exm">exm</asp:ListItem>
										<asp:ListItem Value="gls">gls</asp:ListItem>
										<asp:ListItem Value="htu">htu</asp:ListItem>
										<asp:ListItem Value="idx">idx</asp:ListItem>
										<asp:ListItem Value="pre">pre</asp:ListItem>
										<asp:ListItem Value="rev">rev</asp:ListItem>
										<asp:ListItem Value="for">for</asp:ListItem>
										<asp:ListItem Value="tbk">tbk</asp:ListItem>
										<asp:ListItem Value="itr">itr</asp:ListItem>
										<asp:ListItem Value="mis">mis</asp:ListItem>
										<asp:ListItem Value="edb">edb</asp:ListItem>
										<asp:ListItem Value="ind">ind</asp:ListItem>
										<asp:ListItem Value="cop">cop</asp:ListItem>
										<asp:ListItem Value="edi">edi</asp:ListItem>
										<asp:ListItem Value="con">con</asp:ListItem>
										<asp:ListItem Value="ocn">ocn</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td>MSS Page</td>
								<td><asp:textbox id="txtmsspage" runat="server"></asp:textbox></td>
								<td></td>
								<td>Title</td>
								<td><asp:textbox id="txttitle" runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>From Page</td>
								<td><asp:textbox id="txtfrompage" runat="server"></asp:textbox></td>
								<td></td>
								<td>To Page</td>
								<td><asp:textbox id="txttopage" runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>Figure</td>
								<td><asp:textbox id="txtfigure" runat="server"></asp:textbox></td>
								<td></td>
								<td>Chapter No.</td>
								<td><asp:textbox id="txtchapno" runat="server"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
</table>
<table align="center">
				<tr>
					<td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                            onclick="btnUpdate_Click" /></td>
					<td></td>
					<td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                            onclick="btnCancel_Click" /></td>
				</tr>
			</table>
</center>
</div>
</asp:Content>

