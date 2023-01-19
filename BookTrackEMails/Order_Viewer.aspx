<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage/Template.master" AutoEventWireup="true" CodeFile="Order_Viewer.aspx.cs" Inherits="Order_Viewer"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <head id="Head1">
    <title>Test Confirm Dialog</title>
	<link type="text/css" href="Css/ui-lightness/jquery-ui-1.8.16.custom.css" rel="stylesheet" />	
	<script type="text/javascript" src="Jquery/jquery-1.6.2.min.js"></script>
	<script type="text/javascript" src="Jquery/jquery-ui-1.8.16.custom.min.js"></script>
	<script type="text/javascript" src="Jquery/jquery.msgBox.v1.js"></script>
    <script type="text/javascript">
    function ConfirmOnDelete()
    {
      if (confirm("Are you sure you want to Submit?") == true)
      return true;
      else
      return false;
    }
    function ConfirmationBox()
    {
	  
      var result = confirm('Are you sure you want to delete Details... This process is terminated for time being.');
      if (result)
      {
		//  return false;
         return true;
      }
      else 
      {
          return false;
      }
    }
    </script>
</head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="JavaScript" type="text/javascript">
    var message = 'Right Click is disabled';
    function clickIE() {
        if (event.button == 2) {
            alert(message); return false;
        } 
    }
    function clickNS(e) {
        if (document.layers || (document.getElementById && !document.all)) {
            if (e.which == 2 || e.which == 3) { alert(message); return false; }
        }
    }
    if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
    else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
    document.oncontextmenu = new Function('alert(message);return false')
     </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h2 style="color:Yellow">Order Viewer</h2>
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
        .style1
        {
            height: 26px;
        }
    </style>
<center>
<div id="main" align="center">
    <div id="search" align="center">
        <table>
        <tr>
         <td align="right">
             <asp:RadioButton ID="rdbBookseries" runat="server" Text="Book Series" 
                 GroupName="filters" oncheckedchanged="rdpBookseriesChanged" AutoPostBack="true" />
         </td>
         <td align="right">
             <asp:RadioButton ID="rdbBook" runat="server" Text="Book"  GroupName="filters"
                 oncheckedchanged="rdbBookChanged" AutoPostBack="true" />
         </td>
         <td align="right">
             <asp:RadioButton ID="rdbAll" runat="server" Text="All" GroupName="filters" 
                 oncheckedchanged="rdpAllChanged" AutoPostBack="true" />
         </td>
         <td></td>
         <td>|</td>
         <td></td>
        <td align="right">
            <asp:Label ID="lblsearch" runat="server" Text="Search By ISBN" Font-Bold="True" Font-Size="Medium"></asp:Label>
         </td>
            <td align="right">
            <asp:TextBox ID="txtsearchisbn" runat="server" Height="22px" Width="282px" 
                    BorderColor="#0000CC"></asp:TextBox>
                    
             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtsearchisbn"
                                                        WatermarkCssClass="watermarked" WatermarkText="Please Enter ISBN" />
            </td>
             <td align="right">
                 <asp:ImageButton ID="btnsearchbyisbn" runat="server" Height="30px" 
                     ImageUrl="~/Images/search.png" onclick="btnsearchbyisbn_Click" OnClientClick="return searchbox();"/>
            </td>
        </tr>
    </table>
    </div>
    <%-- </div>--%>
        <table  border="0" style="width:950px; height: 302px;" align="center">
      <tr>
    <td colspan="2" align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:300px">Please Select Book ID</td>
          <%--<asp:CommandField ShowEditButton="True" ButtonType="Link"  />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />--%>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:150px">Apply Chp Nos.</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:150px">View Order</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:150px">Home</td>
    <td align="center" style="color:White;background-color:#6B696B;font-weight:bold; width:150px">Date</td>
    </tr>
      <tr>
        <td align="center" colspan="2">
            <asp:DropDownList ID="ddlbookid" runat="server"  Width="300px" 
                onselectedindexchanged="ddlbookid_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
          </td>
        <td align="center">
            <%-- </div>--%>
            <asp:LinkButton ID="lnkchpno" runat="server" Width="150px" onclick="lnkchpno_Click">Apply Chp Nos.</asp:LinkButton>
        </td>
        <td align="center"><asp:LinkButton ID="lnkvieworder" runat="server" Width="150px" 
                onclick="lnkvieworder_Click">View Order</asp:LinkButton></td>
        <td align="center"><asp:LinkButton ID="lnkinsertmore" runat="server" Width="150px" 
                onclick="lnkinsertmore_Click">Home</asp:LinkButton> </td>
        <td align="center">
            <asp:Label ID="lbldate" runat="server" Text="Label" ForeColor="Green"></asp:Label>
            <%--<asp:CommandField ShowEditButton="True" ButtonType="Link"  />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />--%></td>
  
      </tr>
      <tr>
         <td align="center" colspan="6" style="color:White;background-color:#6B696B;font-weight:bold;">Book Level Information</td>
      </tr>
      <tr>
        <td align="left"><asp:Label ID="lblorderisbn" runat="server" Text="ISBN*" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderisbn" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="True"></asp:TextBox></td>
        <td align="left"><asp:Label ID="Label7" runat="server" Text="Job Type*" Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtorderjobtype" runat="server" CssClass="tb7" Width="150px" 
                Height="20px" ReadOnly="True"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderlanguage" runat="server" Text="Book Language*" Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtorderlanguage" runat="server" CssClass="tb7" Width="150px" 
                Height="20px" ReadOnly="True"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="left"><asp:Label ID="lblorderbooktitle" runat="server" Text="Book Title*" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderbooktitle" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="false"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderbooksubtitle" runat="server" Text="Book Subtitle" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderbooksubtitle" runat="server" 
                CssClass="tb7" Width="150px" Height="20px" AutoPostBack="True" 
                ontextchanged="txtorderbooksubtitle_TextChanged"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderbookeditors" runat="server" Text="Book Authors" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderbookeditors" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="left" class="style1"><asp:Label ID="lblorderpii" runat="server" Text="PII*" Font-Bold="True"></asp:Label></td>
        <td align="left" class="style1">
            <asp:TextBox ID="txtorderpii" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
        <td align="left" class="style1"><asp:Label ID="lblordercolor" runat="server" Text="Color" Font-Bold="True"></asp:Label></td>
        <td align="left" class="style1"><%-- </div>--%>
            <asp:DropDownList ID="ddlordercolor" runat="server" Width="146px" Height="20px">
            </asp:DropDownList>
        </td>
        <td align="left" class="style1"><asp:Label ID="lblorderplatform" runat="server" Text="Platform" Font-Bold="True"></asp:Label></td>
        <td align="left" class="style1"><asp:TextBox ID="txtorderplatform" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="left"><asp:Label ID="lblorderdoi" runat="server" Text="DOI*" Font-Bold="True"></asp:Label></td>
        <td align="left">
            <asp:TextBox ID="txtorderdoi" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="True"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderstage" runat="server" Text="Stage*" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderstage" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="True"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderedition" runat="server" Text="Edition" Font-Bold="True"></asp:Label>
            *</td>
        <td><asp:TextBox ID="txtoredredition" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="left"><asp:Label ID="lblordertrimsize" runat="server" Text="Trim Size" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtordertrimsize" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblorderimprint" runat="server" Text="Imprint" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtorderimprint" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
        <td align="left"><asp:Label ID="lblcopyrightline" runat="server" Text="Copyright Line" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtordercopyrightline" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
      <tr>
        <td align="left"><asp:Label ID="Label24" runat="server" Text="Copyright Type*" Font-Bold="True"></asp:Label></td>
        <td align="left"> <asp:DropDownList ID="ddlordercopyrighttype" runat="server" Width="150px" AutoPostBack="True">
            </asp:DropDownList></td>
          <td align="left"><asp:Label ID="Label25" runat="server" Text="Copyright Owner*" Font-Bold="True"></asp:Label></td>
        <td><asp:DropDownList ID="ddlordercopyrightowner" runat="server" Width="150px" 
                AutoPostBack="True">
            </asp:DropDownList></td>
        <td align="left"><asp:Label ID="Label26" runat="server" Text="Copyright Year*" Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtordercopyrightyear" runat="server" CssClass="tb7" Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
      <tr>
      <td align="left"><asp:Label ID="Label4" runat="server" Text="Series Title" 
              Font-Bold="True"></asp:Label></td>
      <td align="left">
          <asp:TextBox ID="txtPatentTit" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="false"></asp:TextBox></td>
        <td align="left" ><asp:Label ID="Label1" runat="server" Text="ISSN" Font-Bold="True"></asp:Label></td>
        <td align="left"> 
            <asp:TextBox ID="txtISSN" runat="server" CssClass="tb7" 
                Width="150px" Height="20px" ReadOnly="false"></asp:TextBox></td>
        <td align="left"><asp:Label ID="Label2" runat="server" Text="JID" Font-Bold="True"></asp:Label></td>
        <td id="txtJid">
            <asp:TextBox ID="txtJid" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>        
      </tr>
      <tr>
        <td align="left"><asp:Label ID="Label3" runat="server" Text="Volume No" 
                Font-Bold="True"></asp:Label></td>        
        <td align="left">
            <asp:TextBox ID="txtVolNo" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
          <td align="left">
              <asp:Label ID="Label8" runat="server" Text="PM Name*" 
                Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtPMname" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
        <td align="left">
            <asp:Label ID="Label9" runat="server" Text="PM Email*" 
                Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtPmemail" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
      </tr>
       <tr>
        <td align="left">
            <asp:Label ID="Label5" runat="server" Text="Design Name" 
                Font-Bold="True"></asp:Label></td>        
        <td align="left" style="margin-left: 80px">
            <asp:TextBox ID="txtDesignName" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
          <td align="left">
              <asp:Label ID="Label6" runat="server" Text="Reference Style" 
                Font-Bold="True"></asp:Label></td>
        <td><asp:TextBox ID="txtReferenceStyle" runat="server" CssClass="tb7" 
                Width="150px" Height="20px"></asp:TextBox></td>
        <td align="left"> <asp:Label ID="Label10" runat="server" Text="Spelling" 
                Font-Bold="True"></asp:Label></td>
        <td  align="left">
         <asp:DropDownList ID="DDlistSpelling" runat="server">
                <asp:ListItem Value="Select">---Select---</asp:ListItem>
                <asp:ListItem>UK</asp:ListItem>
                <asp:ListItem>US</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>

     
       <tr>
	       <td align="left"><asp:Label ID="lblAbs" runat="server" Text="Abstract*" 
                Font-Bold="True"></asp:Label>  </td>
        <td align="left"><asp:DropDownList ID="ddlabstract" runat="server">
                <asp:ListItem Value="Select">---Select---</asp:ListItem>
                <asp:ListItem>Print and Online</asp:ListItem>
                <asp:ListItem>Online only</asp:ListItem> 
				<asp:ListItem>Not Required</asp:ListItem> 				
            </asp:DropDownList></td>
    
        <td align="left"> <asp:Label ID="LbleISBN" runat="server" Text="eISBN" 
                Font-Bold="True"></asp:Label></td>        
        <td align="left">  <asp:TextBox ID="txteISBN" runat="server" CssClass="tb7"   Width="150px" Height="20px">
                           
            </asp:TextBox></td>
        <td align="left"><asp:Label ID="lblPCW" runat="server" Text=" PC Workflow*" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:DropDownList ID="ddlpcw" runat="server">
                <asp:ListItem Value="Select">---Select---</asp:ListItem>
                <asp:ListItem>PC 1.0</asp:ListItem>
                <asp:ListItem>PC 2.0</asp:ListItem>                
                <asp:ListItem>Non-PC</asp:ListItem>                
         </asp:DropDownList></td>
      </tr>
	 
       <tr>
        
        <td align="left"><asp:Label ID="lblAnK" runat="server" Text="Keywords*" 
                Font-Bold="True"></asp:Label>  </td>
        <td align="left"><asp:DropDownList ID="ddlkeywords" runat="server">
                <asp:ListItem Value="Select">---Select---</asp:ListItem>
                <asp:ListItem>Print and Online</asp:ListItem>
                <asp:ListItem>Online only</asp:ListItem>                
				<asp:ListItem>Not Required</asp:ListItem> 				
            </asp:DropDownList></td>
      </tr>


       <tr>
        <td align="left"> <asp:Label ID="Label11" runat="server" Text="Author Name(Opener)*" 
                Font-Bold="True"></asp:Label></td>        
        <td align="left">  <asp:DropDownList ID="DDListAName" runat="server">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem>YES</asp:ListItem>
                <asp:ListItem>NO</asp:ListItem>                
            </asp:DropDownList></td>
        <td align="left"><asp:Label ID="Label12" runat="server" Text="Affiliation(Opener)*" 
                Font-Bold="True"></asp:Label>  </td>
        <td align="left"><asp:DropDownList ID="DDListAffilation" runat="server">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem>YES</asp:ListItem>
                <asp:ListItem>NO</asp:ListItem>              
            </asp:DropDownList></td>
        <td align="left"><asp:Label ID="Label13" runat="server" Text=" Mini-TOC(Opener)*" Font-Bold="True"></asp:Label></td>
        <td align="left"><asp:DropDownList ID="DDListToc" runat="server">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem>YES</asp:ListItem>
                <asp:ListItem>NO</asp:ListItem>                
            </asp:DropDownList></td>
      </tr>
      <tr>
         <td align="center" colspan="6" style="color:White;background-color:#6B696B;font-weight:bold;">Chapter Level Information</td>
      </tr>
      <tr style="background-color:ActiveBorder" >
        <td align="left" colspan="2">
            <%--<asp:CommandField ShowEditButton="True" ButtonType="Link"  />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />--%>
                <asp:TextBox ID="txtaddnewrecord" runat="server" Height="25px" Width="52px" BackColor="#FFCC99" 
                MaxLength="5"></asp:TextBox>&nbsp;
                <asp:LinkButton
                    ID="linkaddnewrecord" runat="server" onclick="linkaddnewrecord_Click" 
                Width="213px" Height="20px">Add New Record(one or more)</asp:LinkButton>
        </td>
          <%-- </div>--%>
        <td align="left" colspan="2">
         <asp:TextBox ID="txtmissingrecord" runat="server" Height="25px" Width="52px" BackColor="#FFCC99" 
                MaxLength="5"></asp:TextBox>&nbsp;
        <asp:LinkButton
            ID="linkmissingrecord" runat="server" Height="20px" 
                onclick="linkmissingrecord_Click">Insert Missing AID</asp:LinkButton>&nbsp;
        </td>
        <td align="left" colspan="1.5">
            <asp:FileUpload ID="Excelfile" runat="server" Height="30px"  
                BackColor="#FFCC99" Width="192px" />
            <%--<asp:CommandField ShowEditButton="True" ButtonType="Link"  />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />--%>
        </td>
     <td align="right" colspan=".5">
        <asp:Button ID="btnuploadexcel" runat="server" Text="UPLOAD" BackColor="#FFCC99" 
                onclick="btnuploadexcel_Click" Height="30px" Width="100px" /> 
        </td>
     </tr>
     <tr>
         <td align="center" colspan="6" style="color:White;background-color:#6B696B;font-weight:bold;">Chapter Level Information</td>
      </tr> 
      <tr  style="background-color:ActiveBorder" >
     <td colspan="6" align="center">
      <asp:Label ID="lblbsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
     </td>
     </tr>          
   </table>
   <%-- </div>--%>
    <div id="BC" style="overflow: scroll; height: 160px; width: 975px;" 
        align="center">
        <table width="950px">
        <tr>
        <td align="center">
            &nbsp;</td>
        </tr>
        </table>
        <asp:GridView ID="gv" runat="server" Width="1101px"
                AutoGenerateColumns="False"  cellpadding="2" GridLines="Vertical" 
                ShowFooter="True" onrowcancelingedit="gv_RowCancelingEdit"
                onrowdeleting="gv_RowDeleting" onrowediting="gv_RowEditing"
                onrowupdating="gv_RowUpdating"
           
            onpageindexchanging="gv_PageIndexChanging" onrowdatabound="gv_RowDataBound" 
            PageSize="5" Height="160px" >
            <PagerSettings FirstPageText="First" LastPageText="Last" 
                Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous" />
            <Columns>
                        <asp:BoundField DataField="CID" SortExpression="CID" ReadOnly="True" HeaderText="CID"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CNO" SortExpression="CNO" ReadOnly="True" HeaderText="CNO"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="30px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PII" ReadOnly="True" HeaderText="PII"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DOI" ReadOnly="True" HeaderText="DOI"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AID" ReadOnly="True" HeaderText="AID"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Doc Subtype" SortExpression="DOCSUBTYPE">                
                            <ItemTemplate>
                                <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("DOCSUBTYPE")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddldoctype" runat="server">                                
                                </asp:DropDownList>
                            </EditItemTemplate>             
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="MSS_PAGE" HeaderText="MSS Page"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FROM_PAGE" HeaderText="From Page"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TO_PAGE" HeaderText="To Page"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TITLE" HeaderText="Title"  HeaderStyle-HorizontalAlign="Left"
                    ControlStyle-Width="50px">
                            <ControlStyle Width="50px" />
                            <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Chp_No" HeaderText="Chapter No"  HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="50px">
                             <ControlStyle Width="50px" />
                             <HeaderStyle Width="63px"></HeaderStyle>
                        </asp:BoundField>
                        <%--<asp:CommandField ShowEditButton="True" ButtonType="Link"  />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />--%>
                        <asp:TemplateField HeaderText="Part-Page" SortExpression="PARTPAGE">
                        
                           <ItemTemplate>
                                <asp:Label ID="lblPartPage" runat="server" Text='<%# Bind("PARTPAGE")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlList" runat="server">                                
                                    <asp:ListItem>NO</asp:ListItem>
                                    <asp:ListItem>YES</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Edit" />    
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete" OnClick="lnkdelete_Click" OnClientClick="return ConfirmationBox();"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" Text="Update" />
                                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#FA8328" />
                    <SelectedRowStyle BackColor="#FEE3C6" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FEE3C6" ForeColor="Black" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#FA8328" Font-Bold="True" ForeColor="Black" />
                    <AlternatingRowStyle BackColor="#FEE3C6" />
                    <EditRowStyle BackColor="White" />
            </asp:GridView>
    </div> 
    <div id="save" align="center">
        <table width="1000px">
            <tr>
                <td align="center">
                <asp:LinkButton ID="linksave" runat="server" Width="400px" class="confirmLink"  ForeColor="Blue"
                rel="Are you sure you want to Submit?" onclick="linksave_Click" OnClientClick="return ConfirmOnDelete();" >Click here to save the above order information</asp:LinkButton> </td>
            </tr>
            <tr>
                <td align="center"><marquee bgcolor=orange  direction=right behavior=ALTERNATE >THOMSON DIGITAL</marquee></td>
            </tr> 
        </table>
    </div>
</div>
</center>
</asp:Content>

