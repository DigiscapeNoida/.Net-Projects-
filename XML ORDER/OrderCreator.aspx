<%@ Page Language="C#"    ResponseEncoding="utf-8"     EnableViewState="true"  AutoEventWireup="true" CodeFile="OrderCreator.aspx.cs" Inherits="OrderCreator" MasterPageFile="MasterPage.master"  %>
<asp:Content ID="Title"       ContentPlaceHolderID="PageTitle"  runat="server">Welcome to XML Order Creation and Integration Application</asp:Content>
<asp:Content ID="UserWelcome" ContentPlaceHolderID="UserMaster" runat="server"><asp:Label ID="lblUser" runat="server" Text="" Font-Size="small" Font-Bold="True"></asp:Label></asp:Content>
<asp:Content ID="Content1"    ContentPlaceHolderID="PageBody"   Runat="Server">

    <form id="form1" name="form1" runat="server">
    <table>
        <tr>
            <td colspan=2>
                <asp:TextBox ID="txtTitle" runat="server" BorderStyle="Groove" Height="16px" Style="z-index: 121; position: relative" TabIndex="7" Width="376px" Font-Size="12pt" Enabled="False" BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" Font-Bold="True"></asp:TextBox>
            </td>
            <td colspan=2>
                <asp:Label ID="LblJTitle" runat="server" Style="z-index: 122; position: relative" Width="629px" Font-Bold="True" Font-Names="Verdana" Font-Size="15pt" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan=5>
                <hr id="Hr9" runat="server" color="#000099" style="z-index: 226; position: relative" /><asp:Label ID="lblbook" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=BlueViolet Style="z-index: 101; position: relative" Text="Article Details" Width="96px" BackColor="Transparent"></asp:Label><hr id="Hr10" runat="server" color="#000099" style="z-index: 226; position: relative" />
            </td>
        </tr>
        <tr valign="top">
            <td  style="width: 25%">
                <table style="width: 95%; height: 164px">
                    <tr>
                        <td>
                            <asp:Label ID="lblAccount" runat="server" Font-Size="7.5pt" Height="6px" Style="z-index: 125; position: relative" Text="Account:" Width="58px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbAccount"  runat="server" Height="20px" Style="z-index: 136;
                                    position: relative" Width="106px" Font-Size="8pt" AutoPostBack="True" TabIndex="1" Font-Names="Verdana">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px">
                            <asp:Label ID="lblJID" runat="server" Height="6px" Style="z-index: 124;
                            position: relative" Text="JID:" Width="58px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 27px">
                            <asp:DropDownList ID="cmbJID"  runat="server" Height="20px" Style="z-index: 137; 
                            position: relative" Width="106px" Font-Size="8pt" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="cmbJID_SelectedIndexChanged" Font-Names="Verdana">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <asp:Label ID="lblStage" runat="server" Font-Size="7.5pt" Height="6px" Style="z-index: 138;
                                    position: relative" Text="Stage :" Width="58px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                                <asp:DropDownList ID="cmbStage"  runat="server" Height="20px" Style="z-index: 139;
                                    position: relative" Width="106px" Font-Size="8pt" TabIndex="3" Font-Names="Verdana" OnSelectedIndexChanged="cmbStage_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>Fresh</asp:ListItem>
                                    <asp:ListItem>Fresh (Only CE)</asp:ListItem>
                                    <asp:ListItem>MCE</asp:ListItem>
                                    <asp:ListItem>Revise</asp:ListItem>
                                    <asp:ListItem>FAX</asp:ListItem>
                                    <asp:ListItem>FAXRESUPPLY</asp:ListItem>
                                    <asp:ListItem>Cover-TOC</asp:ListItem>
                                    <asp:ListItem>SGML</asp:ListItem>
                                    <asp:ListItem>SGMLRESUPPLY</asp:ListItem>
                                    <asp:ListItem>Web</asp:ListItem>
                                    <asp:ListItem>Printer</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td style="height: 28px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDOI" runat="server" Height="6px" Style="z-index: 123;
                                position: relative" Text="DOI:" Width="58px" Font-Size="7.5pt" 
                                Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDOI" runat="server" BorderStyle="Groove" Height="15px" Style="z-index: 120;
                                position: relative; top: 0px; left: 0px; width: 106px;" TabIndex="8" 
                                Font-Size="8pt" Font-Names="Verdana" OnTextChanged="txtDOI_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="height: 28px"><asp:RequiredFieldValidator ID="RequiredValidator1"  ControlToValidate="txtDOI" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator><%--<asp:Label ID="lblDOIError" runat="server" ForeColor="Red" Visible="false"></asp:Label>--%></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVol" runat="server" Height="6px" Style="z-index: 123;
                                position: relative" Text="Volume No:" Width="58px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVol" runat="server" BorderStyle="Groove" Height="15px" Style="z-index: 120;
                                position: relative" TabIndex="8" Width="106px" Font-Size="8pt" 
                                Font-Names="Verdana"></asp:TextBox>
                        </td>
                        <td style="height: 28px"><asp:RequiredFieldValidator ID="RequiredValidator2"  ControlToValidate="txtVol" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIssue" runat="server" Height="6px" Style="z-index: 123;
                                position: relative" Text="Issue No:" Width="58px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssue" runat="server" BorderStyle="Groove" Height="15px" Style="z-index: 120;
                                position: relative" TabIndex="8" Width="106px" Font-Size="8pt" 
                                Font-Names="Verdana"></asp:TextBox>
                        </td>
                        <td style="height: 28px"><asp:RequiredFieldValidator ID="RequiredValidator3"  ControlToValidate="txtIssue" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblFig" runat="server" Height="19px" Style="z-index: 144; position: relative" Text="No of Figures" Width="59px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:DropDownList ID="cmbFig"  runat="server" Height="20px" 
                                Style="z-index: 140; position: relative" Width="106px" Font-Size="8pt" 
                                TabIndex="14" Font-Names="Verdana"></asp:DropDownList>
                        </td>
                        <td style="height: 28px"></td>
                    </tr>
                </table>
            </td>
            <td  style="width: 25%">
                <table  style="width: 100%">
                    <tr>
                        <td style="width: 116px">
                            <asp:Label ID="lblAID" runat="server" Height="19px" Style="z-index: 119;
                                position: relative; top: 0px; left: 0px; width: 115px;" Text="Article ID:" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 116px">
                            <asp:TextBox ID="txtAID" runat="server" BorderStyle="Groove" Style="z-index: 118;
                                position: relative; top: 0px; left: 3px; width: 132px; height: 20px;" TabIndex="5" 
                                Font-Size="8pt"  Font-Names="Verdana" AutoPostBack="True" 
                                ontextchanged="txtAID_TextChanged"></asp:TextBox>
                        </td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ControlToValidate="txtAID" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width:116px">
                        <asp:Label ID="lblRecive" runat="server" Height="19px" Style="z-index: 108;
                            position: relative; top: 0px; left: 0px; width: 89px;" Text="Received Date" 
                                Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 163px">
                            <asp:DropDownList Enabled="true" ID="ReciveDay"   runat="server" Height="18px" Width="36px" ></asp:DropDownList>
                            <asp:DropDownList Enabled="true"  ID="ReciveMonth" runat="server" Height="18px" Width="36px"></asp:DropDownList>
                            <asp:DropDownList Enabled="true"  ID="ReciveYear"  runat="server" Height="17px" Width="64px"></asp:DropDownList>
                            
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px">
                        <asp:Label ID="lblRevise" runat="server" Height="19px" Style="z-index: 109; 
                            position: relative" Text="Revised Date" Width="89px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 163px">
                            <asp:DropDownList Enabled="true"  ID="ReviseDay"   runat="server" Height="18px" Width="36px" ></asp:DropDownList>
                            <asp:DropDownList Enabled="true" ID="ReviseMonth" runat="server" Height="18px" Width="36px"></asp:DropDownList>
                            <asp:DropDownList Enabled="true" ID="ReviseYear" runat="server" Height="18px" Width="64px"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px">
                            <asp:Label ID="lblAccepted" runat="server" Height="19px" Style="z-index: 105; position: relative" Text="Accepted Date" Width="89px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 163px">
                            <asp:DropDownList Enabled="true"  ID="AcceptedDay"   runat="server" 
                                Height="18px" Width="36px" 
                                onselectedindexchanged="AcceptedDay_SelectedIndexChanged" ></asp:DropDownList>
                            <asp:DropDownList Enabled="true"  ID="AcceptedMonth" runat="server" Height="18px" Width="36px"></asp:DropDownList>
                            <asp:DropDownList Enabled="true"  ID="AcceptedYear"  runat="server" Height="18px" Width="64px"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px">
                            <asp:Label ID="lblActualDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt"  ForeColor="Black" Height="19px" Style="z-index: 115; position: relative" Text="Actual Due Date" Width="109px"></asp:Label>
                        </td>
                        <td style="width: 163px">
                            <asp:TextBox ID="txtActualDate" runat="server" BorderStyle="Groove" 
                                Enabled="False" Font-Names="Verdana" Font-Size="8pt" Height="19px" 
                                Style="z-index: 114; position: relative; top: 0px; left: 0px; width: 131px;" 
                                TabIndex="5" OnTextChanged="txtActualDate_TextChanged"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px">
                            <asp:Label ID="Label8" runat="server" Font-Size="7.5pt" Height="19px" Style="z-index: 116;
                                position: relative" Text="Internal Due Date" Width="109px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 163px">
                            <asp:TextBox ID="txtDueDate" runat="server" BorderStyle="Groove" 
                                Enabled="False" Font-Size="8pt" 
                                Style="z-index: 113; position: relative; top: 2px; left: 0px; height: 19px; width: 131px;" 
                                TabIndex="5" Font-Names="Verdana"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Workflow</td>
                        <td colspan="2"><asp:DropDownList ID="cmbMCE"  runat="server" Height="20px" Style="z-index: 103; position: relative;" Width="200px" Font-Size="8pt" TabIndex="1" Font-Names="Verdana"></asp:DropDownList></td>
                    </tr>
                    <tr>
                       <td colspan="3">
                        <asp:Label ID="lblRmrks" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="txtPlnrRemark" runat="server"  BorderStyle="Groove" Height="155px" Width="100%" Font-Size="8pt" Enabled="True" Font-Names="Verdana"  TextMode="MultiLine"></asp:TextBox>
                       </td>
                    </table>
            </td>
            <td  style="width: 30%">
                
                <table width=100% > 
                    <tr>
                        <td>
                            <asp:Label ID="lblMns" runat="server" Height="19px" Text="Manuscript Pages"  Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label></td><td>
                            <asp:TextBox ID="txtPages" runat="server" BorderStyle="Groove" Height="15px"                    
                                TabIndex="13" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  ControlToValidate="txtIssue" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px"  Text="Article Category"></asp:Label></td><td>
						<asp:DropDownList ID="cmbCategory"  runat="server" 
                            
                            Font-Size="8pt" TabIndex="1" Font-Names="Verdana"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="lblArtType" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px" Text="Article Type"></asp:Label></td><td>
						<asp:DropDownList ID="cmbArtType"  runat="server" 
                            
                            Font-Size="8pt" TabIndex="1" Font-Names="Verdana"></asp:DropDownList>
                    </td>
                </tr>
                   <tr>
                        <td colspan="2">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet   BackColor="Transparent"></asp:Label>
                        </td>
                   </tr>
                   <tr>
                        <td>
                            <asp:Label ID="lblAuFN" runat="server" Font-Size="7.5pt" Height="18px"  Text="First Name"  Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAUFN" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px"  TabIndex="16" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuSN" runat="server" Font-Size="7.5pt" Height="18px"  Text="LastName:"  Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAUSN" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px"  TabIndex="17"  Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuDegree" runat="server" Font-Size="7.5pt" Height="18px"  Text="Degree"  Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuDeg" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px"  TabIndex="15"  Font-Names="Verdana"></asp:TextBox>
                        </td>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet  Text="Corresponding Author Details"  BackColor="Transparent"></asp:Label>
                        </td>
                   </tr>
                    <tr>
                        <td><asp:Label ID="lblCorAuthName" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px"  Text="Cor Author Name"></asp:Label></td>
                        <td>
						  <asp:TextBox ID="txtCorAuthName" runat="server" BorderStyle="Groove" Height="15px" 
                                 
                                TabIndex="13" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  ControlToValidate="txtCorAuthName" runat="server"  ErrorMessage="! Please enter the Cor Author Name">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCorAuthEmail" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px"  Text="Cor Author Email"></asp:Label></td><td>
					  <asp:TextBox ID="txtCorAuthEmail" runat="server" BorderStyle="Groove" Height="15px" 
                                
                                TabIndex="13" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  ControlToValidate="txtCorAuthEmail" runat="server"  ErrorMessage="! Please enter the email">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regValidator" runat="server" ErrorMessage="! Cor Author email should be in proper manner." ForeColor="Red" ControlToValidate="txtCorAuthEmail" ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$" ValidationGroup="register">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                      <asp:Label ID="lblCorCCEmail" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px"  Text="eProof CC Email"></asp:Label></td><td>
					  <asp:TextBox ID="txtCorCCEmail" runat="server" BorderStyle="Groove" Height="15px" 
                                
                                TabIndex="13" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="regCCValidator" runat="server" ErrorMessage="! Cor CC email should be in proper manner." ForeColor="Red" ControlToValidate="txtCorCCEmail" ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$" ValidationGroup="register">*</asp:RegularExpressionValidator>
                      </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPDFName" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px"  Text="PDF File Name"></asp:Label></td><td>
					    <asp:TextBox ID="txtPDFName" runat="server" BorderStyle="Groove" Height="15px" 
                                
                                TabIndex="13" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  ControlToValidate="txtPDFName" runat="server"  ErrorMessage="! Please enter PDF file name.">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                    <tr>
                    <td>
                    <asp:Label ID="lblUpload" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            Font-Size="7.5pt" ForeColor="Black"         Text="Upload Input Files" ></asp:Label>                        
                    </td>
                    <td><asp:FileUpload ID="flUpload"  runat="server"  />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  ControlToValidate="flUpload" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator></td>
                 </tr>
                    <tr >
                        <td colspan="2" >
                <asp:Button ID="cmdGenerate" runat="server" Font-Names="Verdana" Height="22px"  Text="Generate XML Order"  OnClick="cmdGenerate_Click" TabIndex="49" OnClientClick="return ErrMsg();" />
            </td>
                        </tr>
            </table>
        </td></tr>
        <tr>
                <td colspan="3">
                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="Black" Height="18px" Text="Article Title"></asp:Label>
                    <asp:TextBox ID="txtArticleTitle" runat="server" Width="1146px"></asp:TextBox>
                </td>
            </tr>
    </table>
<br />
    <table width="100%" style="display: none">
        <tr>
            <td valign=top colspan=5>
            <hr id="Hr3" runat="server" color="#000099" style="z-index: 226; position: relative" />
            </td>
        </tr>
        <tr style="text-align:center;">
            <td style="width: 280px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet Style="z-index: 190;position: relative" Text="Editor Details" Width="164px" BackColor="Transparent"></asp:Label>
            </td>
            <td></td>
            <td >
             
             </td>
            <td></td>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet Style="z-index: 202; position: relative" Text="Corresponding Author Details" Width="251px" BackColor="Transparent"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan=5>
                <hr id="Hr8" runat="server" color="#000099" style="z-index: 226; position: relative" />
            </td>
        </tr>
    </table>
        
    <table width="100%" style="display: none">
        <tr>
            <td valign=top>
                <table width=30%>
                    <tr>
                        <td>
                            <asp:Label ID="lblEdName" runat="server" Height="13px" Style="z-index: 148; position: relative" Text="Editor Name:" Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDName" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 145; position: relative" TabIndex="18" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEDDesign" runat="server" Height="13px" Style="z-index: 147; position: relative" Text="Designation " Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDDesign" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 146; position: relative" TabIndex="19" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOrganization" runat="server" Height="13px" Style="z-index: 152; position: relative" Text="Organization " Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDOrganization" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 149; position: relative" TabIndex="20" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblInstitute" runat="server" Height="13px" Style="z-index: 151; position: relative" Text="Institute " Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDInstitute" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 150; position: relative" TabIndex="21" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblEDAddress" runat="server" Height="13px" Style="z-index: 189; position: relative" Text="Address " Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>  
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDAddress" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 153;position: relative" TabIndex="22" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblEDZip" runat="server" Height="13px" Style="z-index: 188; position: relative" Text="Zipcode " Width="72px" Font-Size="7.5pt" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDZipcode" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 154;position: relative" TabIndex="23" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td>
                            <asp:Label ID="LblEDCity" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 187; position: relative" Text="City " Width="64px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDCity" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 155; position: relative" TabIndex="24" Width="172px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEDCNY" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 157; position: relative" Text="Cnycode " Width="64px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDCnycode" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 156; position: relative" TabIndex="25" Width="172px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEDTel" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 186; position: relative" Text="Tel " Width="64px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDTel" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 158; position: relative" TabIndex="26" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEDFax" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 185; position: relative" Text="Fax " Width="64px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDFax" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 159;position: relative" TabIndex="27" Width="172px" Font-Size="8pt" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEDEAD" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 184; position: relative" Text="E-Mail " Width="64px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtEDMail" runat="server" BorderStyle="Groove" Height="13px" Style="z-index: 160; position: relative" TabIndex="28" Width="172px" Font-Size="8pt" Font-Names="Verdana" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>

                <table width=5%>
                    </table>
            </td>
           
            <td>
                <table width=30%>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 205; position: relative" Text="Degree" Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorAuDeg" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 204; position: relative" TabIndex="29" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 208; position: relative" Text="First Name" Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorAuFN" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 203; position: relative" TabIndex="30" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 207; position: relative" Text="LastName:" Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorAuSN" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 206; position: relative" TabIndex="31" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInsti" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 212; position: relative" Text="Institution " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuInstitute" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 211; position: relative" TabIndex="33" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDept" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 213; position: relative" Text="Department :" Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuDept" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 209; position: relative" TabIndex="32" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuaddress" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 216; position: relative" Text="Address " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuAdd" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 210; position: relative" TabIndex="34" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblauzip" runat="server" Font-Size="7.5pt" Height="13px" Style="z-index: 215; position: relative" Text="Zipcode " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuzip" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 214; position: relative" TabIndex="35" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAucity" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 183; position: relative" Text="City " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuCity" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 174; position: relative" TabIndex="36" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuCny" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 176; position: relative" Text="Cnycode " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuCny" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 175; position: relative" TabIndex="37" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuTel" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 182; position: relative" Text="Tel " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuTel" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 177; position: relative" TabIndex="38" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuFax" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 181; position: relative" Text="Fax " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuFax" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 178; position: relative" TabIndex="39" Width="136px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuEmail" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 180; position: relative" Text="E-Mail " Width="78px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuEAD" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 179; position: relative" TabIndex="40" Width="137px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table width="100%" style="display: none">
        <tr>
            <td colspan=5>
                <hr id="Hr11" runat="server" color="#000099" style="z-index: 226; position: relative" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet Style="z-index: 191; position: relative" Text="E-Proofing Details" Width="164px" BackColor="Transparent"></asp:Label>
            </td>
            <td></td>
            <td>
                <table><tr><td width="80%"></td><td><asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor=blueviolet Style="z-index: 201; position: relative" Text="Figure Details" Width="164px" BackColor="Transparent"></asp:Label></td></tr></table>
            </td>
            <td></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan=5>
                <hr id="Hr12" runat="server" color="#000099" style="z-index: 226; position: relative" />
            </td>
        </tr>
    </table>

    <table width="100%" style="display: none">
        <tr>
            <td>
                <table width="40%">
                    <tr>
                        <td>
                            <asp:Label ID="lblToAddress" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 173; position: relative" Text="To Address " Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 163; position: relative" TabIndex="41" Width="144px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCC" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 169; position: relative" Text="CC Mail " Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCC" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="13px" Style="z-index: 168; position: relative" TabIndex="42" Width="144px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCTA" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 172; position: relative" Text="CTA" Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbCTA"  runat="server" Height="20px" Style="z-index: 141; position: relative" Width="150px" Font-Size="8pt" TabIndex="43" Font-Names="Verdana">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCID" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 171; position: relative" Text="CID " Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbCID"  runat="server" Height="20px" Style="z-index: 142; position: relative" Width="150px" Font-Size="8pt" TabIndex="44" Font-Names="Verdana">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOffprint" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 170; position: relative" Text="Offprint " Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbOFFPrint"  runat="server" Height="20px" Style="z-index: 143; position: relative" Width="150px" Font-Size="8pt" TabIndex="45" Font-Names="Verdana">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAttach" runat="server" Font-Size="7.5pt" Height="18px" Style="z-index: 167; position: relative" Text="Addtional Attachment " Width="71px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdditionalText" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="23px" Style="z-index: 166; position: relative" TabIndex="46" Width="144px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRemarks" runat="server" Font-Size="7.5pt" Height="27px" Style="z-index: 165; position: relative" Text="Remarks " Width="72px" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" BorderStyle="Groove" Font-Size="8pt" Height="86px" Style="z-index: 164; position: relative" TabIndex="47" TextMode="MultiLine" Width="248px" Font-Names="Verdana"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            
            <td valign="top">
                <table width="40%">
                    <tr valign=top>
                        <td valign=top>
                        <asp:DataGrid ID="dgFigure" runat="server" style="z-index: 218; position:relative" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small" ForeColor="Black" GridLines="Vertical" Height="1px" Width="375px" >
                            <FooterStyle BackColor="#CCCC99" />
                            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <Columns>
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:ImageButton ID="BtnAddFigure" runat="server" Style="z-index: 102; position: relative" Height="25px" ImageUrl="ImagesAndStyleSheet/button_view.gif" OnClick="BtnAddFigure_Click" Width="96px" />
                        </td>
                    </tr>
                    <tr>
                        <td align=right>
                            <asp:LinkButton ID="cmdView" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7.5pt" ForeColor="DarkBlue" Height="18px" Style="z-index: 104;
                                left: 0px; position: relative; top: 0px"  >View Workflow</asp:LinkButton>

                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 27px" colspan="2">
                    <asp:Label ID="lblError" runat="server" Font-Size="Small" Style="z-index: 221; position: relative" Width="159px" ForeColor="Black" Height="23px"></asp:Label>
            </td>
            
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
            </td>
        </tr>
    </table>
</form>
    
</asp:Content>