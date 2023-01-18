<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Xml Order Creation and Integration</title>
</head>
<body bottommargin="0" leftmargin="0"  background="background.jpg" text="red" style="width:100%">
    
<table width="100%" height="94" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
<!-- the LOGO is below, edit blanklogo.jpg / or .psd and save as a different
name to create a logo.  Then change the src="" below to the new file name -->
		<td width="150" height="94"><img src="templogo.jpg" width="150" height="94" border="0" alt=""></td>
<!-- END OF LOGO ----------------------------------------------------------->
		<td width="368" height="94"><img src="topbar1.jpg" width="368" height="94" border="0" alt=""></td>
		<td width="100%" height="94" background="topbar1bg.jpg" align=right>&nbsp; 
<!--		<asp:LoginName ID="LoginName1" runat="server" Font-Size="Medium" style="z-index: 100; left: 581px;  top: 60px" Width="108px" />-->
                <asp:Label ID="Label2" runat="server" Text="Welcome " Font-Size="Medium" Font-Bold="True"></asp:Label>
		    <asp:LoginName ID="LoginName2" runat="server" Font-Size="Medium" Width="108px" Font-Bold="True" />
            <form id="Form1" runat="server">
                <asp:LinkButton ID="lblLogout" runat="server" Font-Bold="False" Font-Names="verdana"
                    Font-Size="Small" Height="22px" OnClick="lblLogout_Click" Width="55px">Logout</asp:LinkButton>
            </form>
		
		
		</td>
	</tr>
</table>
<table width="100%" height="91" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
		<td width="613" height="91"><img src="2maincolorarea.jpg" width="613" height="91" border="0" alt=""></td>
		<td width="640" height="80" background="img1.jpg">&nbsp;</td>
	</tr>
</table>
<table width="100%" height="33" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
<!-- IMPORTANT.. edit blankbutton1.jpg or .psd and save as a different
name to create the FIRST button on this menu.  Then change the src="" below to the new file name.
The blankbutton.jpg is a different graphic and shouldn't be edited for placement here -->
		<!--<td width="246" height="33"><a href=""><img src="tempbuttonsh.jpg" width="407" height="33" border="0" alt=""></a></td>-->
<!-- end of first button code -->
		<td width="368" height="33"><img src="3buttonarea.jpg" width="427" height="33" border="0" alt=""></td>
		<td width="100%" height="33" background="3buttonareabg.jpg">&nbsp;</td>
	</tr>
</table>



<table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
	<tr valign="top">

		<td width="207" style="height: 188px" align=right>

<a href=""><img src="tempbuttons/home.jpg" width="207" height="33" border="0" alt=""></a><BR>
<a href="OrderDetails.aspx"><img src="tempbuttons/ogen.jpg" width="207" height="33" border="0" alt="" id="IMG3" language="javascript" onclick="return IMG2_onclick()"></a><BR>
<a href="ViewLog.aspx"><img src="tempbuttons/oviw.jpg" width="207" height="33" border="0" alt="" id="IMG4" language="javascript" onclick="return IMG1_onclick()"></a><BR>
<a href=""><img src="tempbuttons/Abtus.jpg" width="207" border="0" alt="" style="height: 29px; z-index: 101; left: 0px;  top: 410px;"></a><BR>
<a href="ChangePassword.aspx"><img src="tempbuttons/ChangePassword.jpg" width="207" height="33" border="0" alt="" style="z-index: 100; left: 0px;  top: 377px"></a><BR>
<a href="UpdateOrder.aspx"><img src="tempbuttons/UpdateOrder.jpg" width="207" border="0" alt="" style="height: 32px; z-index: 102; left: 0px;  top: 348px;"></a><BR>
		</td>

	
	
		<td width="20" style="height: 188px">&nbsp;&nbsp;&nbsp;</td>

		<td width="100%" style="height: 500px;">
		<iframe src="OrderCreator.aspx" id="result"  width='100%' height='100%' frameborder="0" scrolling="auto" ></iframe>
</td>
		
</tr>
		</table>		
							


</body>
</html>
