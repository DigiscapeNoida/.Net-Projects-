<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="EMCOrder.aspx.cs" Inherits="EMCOrder" MasterPageFile="MasterPage.master"%>
<asp:Content ID="Title" ContentPlaceHolderID="PageTitle" runat="server">Welcome to XML Order Creation and Integration Application</asp:Content>
<asp:Content ID="UserWelcome" ContentPlaceHolderID="UserMaster" runat="server"><asp:Label ID="lblUser" runat="server" Text="" Font-Size="small" Font-Bold="True"></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" Runat="Server">
<form id="form1" runat="server">
<table width="100%" style="height: 250px">
    <tr>
	<td colspan="3" style="text-align:center;">
		<hr id="Hr9" runat="server" color="#000099"/>
		<asp:Label ID="lblbook" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Size="9pt" ForeColor="BlueViolet" Text="Article Details"  BackColor="Transparent"></asp:Label><hr id="Hr10" runat="server" color="#000099" />
    </td>
	</tr>	
	<tr>
         <td colspan="3" style="text-align:left;">
            <asp:Label   ID="lblTraiteTitle" runat="server" Text="TraitéTitle" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtTraiteTitle" runat="server" Enabled ="false" Wrap="false"   Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="900px"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td colspan="3" style="text-align:center;">
        <hr id="Hr1" runat="server" color="#000099"/>
    </td> 
    </tr>
    <tr valign="top">
	    <td width="20%">
			    <table style="width: 100%;">
			    <tr>
	                <td><asp:Label   ID="lblTracode" runat="server" Text="Tracode" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:DropDownList ID="cmbTracode" runat="server" Font-Bold="True" 
                            Font-Names="Verdana" ForeColor="Black" 
                            onselectedindexchanged="cmbTracode_SelectedIndexChanged" 
                            AutoPostBack="True" Height="18px" Width="160px"></asp:DropDownList></td>
                    <td></td>
                </tr>
                 <tr>
	                <td><asp:Label   ID="lblTrajid" runat="server" Text="Trajid" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtTrajid" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTrajid" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblStage" runat="server" Text="Stage" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbStage"  runat="server" Height="20px" Width="160px" 
                            Font-Size="8pt" Font-Names="Verdana" AutoPostBack="True" 
                            onselectedindexchanged="cmbStage_SelectedIndexChanged">
	                            <asp:ListItem>S100</asp:ListItem>
	                            <asp:ListItem>S200</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblTraRoot" runat="server" Text="TraRoot" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtTraRoot" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTraRoot" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblAid" runat="server" Text="Aid" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtAid" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            ForeColor="Black" AutoPostBack="True" ontextchanged="txtAid_TextChanged"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtAid" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblFasnumero" runat="server" Text="Fasnumero" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtFasnumero" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFasnumero" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblPii" runat="server" Text="Pii" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtPii" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblDoi" runat="server" Text="Doi" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtDoi" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblItemTitle" runat="server" Text="Titre" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtItemTitle" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtItemTitle" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblItemSubtitle" runat="server" Text="ItemSubtitle" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtItemSubtitle" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblColorModel" runat="server" Text="ColorModel" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtColorModel" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblIssn" runat="server" Text="Issn" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtIssn" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
                    <td></td>	                
                </tr>
                <tr>
	                <td><asp:Label   ID="lblIsbn" runat="server" Text="Isbn" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtIsbn" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblTypesettingModelFormat" runat="server" Text="TypesettingModelFormat" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtTypesettingModelFormat" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblArticleType" runat="server" Text="ArticleType" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtArticleType" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Text="Article"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblMajNo" runat="server" Text="MajNo" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtMajNo" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                 <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtMajNo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblMajCote" runat="server" Text="MajCôte" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtMajCote" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtMajCote" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblMajAnne" runat="server" Text="MajAnne" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtMajAnne" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMajAnne" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblVol" runat="server" Text="Vol" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtVol" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblChaptre" runat="server" Text="Chapitre" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtChaptre" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblSubchaptre" runat="server" Text="Sous-Chapitre" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtSubchaptre" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbPagesCommande" runat="server" Text="Nb Pages Commandées" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbPagesCommande" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:TextBox></td>
	                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtNbPagesCommande" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
        </table>
        </td>
        <td width="40%">
		    <table style="width:100%">
		        <tr>
	                <td><asp:Label   ID="lblNbPagesEstimate" runat="server" Text="Nb Pages Estimées :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbPagesEstimate" runat="server" 
                            Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbTableau" runat="server" Text="Nb Tableaux :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbTableau" runat="server" 
                            Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbFig" runat="server" Text="Nb Légendes Fig. :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbFig" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbPhoto" runat="server" Text="Nb Photos/Radios" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbPhoto" runat="server" Font-Bold="True" 
                            Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbDessin" runat="server" Text="Nb Dessins/Schémas :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbDessin" runat="server" 
                            Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbArbrePapier" runat="server" Text="Nb Arbres décisionnels (papier) :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbArbrePapier" runat="server" 
                            Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbEncadreT1" runat="server" Text="Nb Encadrés (type 1) :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbEncadreT1" runat="server" 
                            Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbEncadreT2" runat="server" Text="Nb Encadrés (type 2) :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbEncadreT2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbBiblio" runat="server" Text="Nb Références biblio. :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbBiblio" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbBiblioSs" runat="server" Text="Nb Références biblio. non appelées :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbBiblioSs" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbSavoirPlus" runat="server" Text="Nb Pour en savoir plus :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbSavoirPlus" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbArbreIntractif" runat="server" Text="Nb Arbres décisionnels (interactifs) :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbArbreIntractif" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbIconoSup" runat="server" Text="Nb Iconos supplémentaires :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbIconoSup" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbVideo" runat="server" Text="Nb Vidéos/Animations :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbVideo" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbDocLegaux" runat="server" Text="Nb Documents légaux :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbDocLegaux" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbFichePatient" runat="server" Text="Nb Informations au patient :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbFichePatient" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"  Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbFicheTech" runat="server" Text="Nb Informations supplémentaires :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbFicheTech" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"  Width="92px"></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbAutoeval" runat="server" Text="Nb Auto-évaluations :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbAutoeval" runat="server" Font-Bold="True" 
                            Font-Names="Verdana" ForeColor="Black"  Width="92px"></asp:TextBox></td>
                </tr>
    			<tr>
	                <td><asp:Label   ID="lblNbClinique" runat="server" Text="Nb Cas cliniques :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbClinique" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbQuotidien" runat="server" Text="Nb Au quotidien :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbQuotidien" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
		    </table>
        </td>
        <td width="40%">
            <table  width="100%">
                <tr>
	                <td><asp:Label   ID="lblLblvide" runat="server" Text="Autre Nb (Lblvide):" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtLblvide" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblNbvide" runat="server" Text="Autre Nb (vide):" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtNbvide" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblResumeEn" runat="server" Text="Résumé Anglais :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbResumeEn"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
	                
                </tr>
                <tr>
	                <td><asp:Label   ID="lblResumeFr" runat="server" Text="Résumé Français :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbResumeFr"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblMcEn" runat="server" Text="Mots Clés Anglais :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbMcEn"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
	                
                </tr>
                <tr>
	                <td><asp:Label   ID="lblMcFr" runat="server" Text="Mots Clés Français :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbMcFr"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblTitreEn" runat="server" Text="Titre Anglais :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                    <asp:DropDownList ID="cmbTitreEn"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblAppelIcono" runat="server" Text="Icono Appelée :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                 <td>
	                    <asp:DropDownList ID="cmbAppelIcono"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblAppelBiblio" runat="server" Text="Références Biblio Appelées :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                   <asp:DropDownList ID="cmbAppelBiblio"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                        </asp:DropDownList>
	                </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblIconoOk" runat="server" Text="Icono Exploitable :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                 <asp:DropDownList ID="cmbIconoOk"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                     </asp:DropDownList>
                     </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblArbreDeci" runat="server" Text="Arbres Décisionnels :" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td>
	                 <asp:DropDownList ID="cmbArbreDeci"  runat="server" Height="20px" Width="92px" Font-Size="8pt" Font-Names="Verdana">
	                        <asp:ListItem>-Select-</asp:ListItem>
	                        <asp:ListItem>OK</asp:ListItem>
	                        <asp:ListItem>KO</asp:ListItem>
                     </asp:DropDownList>
                     </td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblPrincAuthorNom" runat="server" Text="Auteur Principal(Nom)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtPrincAuthorNom" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblPrincAuthorPnom" runat="server" Text="Auteur Principal(Pnom)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtPrincAuthorPnom" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblPrincAuthorAff" runat="server" Text="Auteur Principal(Aff)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtPrincAuthorAff" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                 <tr>
	                <td><asp:Label   ID="lblSecondAuthorNom" runat="server" Text="Auteurs Secondaires(Nom)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtSecondAuthorNom" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblSecondAuthorPnom" runat="server" Text="Auteurs Secondaires(Pnom)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtSecondAuthorPnom" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblSecondAuthorAff" runat="server" Text="Auteurs Secondaires (Aff)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtSecondAuthorAff" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblCorrAuthorPhone" runat="server" Text="Auteur de Correspondance(Phone)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtCorrAuthorPhone" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblCorrAuthorFax" runat="server" Text="Auteur de Correspondance (Fax)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtCorrAuthorFax" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr>
                <tr>
	                <td><asp:Label   ID="lblCorrAuthorEmail" runat="server" Text="Auteur de Correspondance(Email)" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="Small"></asp:Label></td>
	                <td><asp:TextBox ID="txtCorrAuthorEmail" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Width="92px" ></asp:TextBox></td>
                </tr> 
                 <tr>
                    <td>
                        <asp:Label ID="lblUpload" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            Font-Size="7.5pt" ForeColor="Black" 
                            Style="height: 18px;" Text="Upload Files"></asp:Label>                    
                    </td>
                    <td></td>
                </tr>
                <tr>
                     <td><asp:FileUpload ID="flUpload" runat="server" /></td>
                     <td><asp:RequiredFieldValidator ID="RequiredFieldValidator10"  ControlToValidate="flUpload" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator></td>
                </tr>  
	        </table>
        </td>
    </tr>        
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="CmdXmlGenerate" runat="server" Text="Generate XML Order" 
                onclick="CmdXmlGenerate_Click" EnableViewState="False"  />            
        </td>
    </tr>
</table>
</form>
</asp:Content>
