<%@ Page Language="C#" MasterPageFile="FullMaster.master" AutoEventWireup="true" CodeFile="GenerateOrder.aspx.cs" Inherits="GenerateOrder" Title="Untitled Page" %>
<asp:Content ID="Title" ContentPlaceHolderID=TitleThis runat="server">Welcome to XML Order Creation and Integration Application</asp:Content>
<asp:Content ID="UserWelcome" ContentPlaceHolderID=UserThis runat="server"><asp:Label ID="lblUser" runat="server" Text="User Not Found" Font-Size="small" Font-Bold="True"></asp:Label></asp:Content>
<asp:Content ID="OrderContent" ContentPlaceHolderID=ContentBody Runat="Server">

<A><IMG 
      alt="View Source" 
      src="ImagesAndStyleSheet/Login.jpg" 
      border=0></A>
     
          <H4>Goals and Objectives</H4>
      <P>This application can generate xml order at login stage and simultaneously login the article in TIS Database. So you just need to generate the xml order and your article will be login into TIS Database also.
        Entries can be done in all stages (Fresh, Revise, Printer, FAX, SGML etc.).
</P>

      <P></P>
      <H4>Recent Updates:</H4>
      <ul>
      <li>Now user can update any existing order and application will do the version control automatically for xml orders.</li>
      <li>User can also check for orders by clicking on <b><u>Order Logs</u></b> menu.</li>
      <li>To read the details about your xml order, Please click on <b><u>XML Browser</u></b> menu.</li>
      </ul>
      
      <P></P>
      <P></P>
      <H4>Features:</H4>
      <ul>
      <li>Application reads the input from input location and provides necessary details.</li>
      <li>Xml Order is Generated automatically.</li>
      <li>Xml Order is Validated for correctness.</li>
      <li>Article is automatically integrated into TIS.</li>
      <li>Xml Order will be used by FMS.</li>
      </ul>
      

</asp:Content>

