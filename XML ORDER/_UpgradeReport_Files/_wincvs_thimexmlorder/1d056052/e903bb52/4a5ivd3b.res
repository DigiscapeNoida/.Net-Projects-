        ??  ??                  ?H      ?????e                 
<STYLE type=text/css>.ctl00_ctl00_Nav_TreeView1_0
{
	TEXT-DECORATION: none
}
.ctl00_ctl00_Nav_TreeView1_1 {
	FONT-SIZE: 1em
}
.ctl00_ctl00_Nav_TreeView1_2 {
	
}
.ctl00_ctl00_Nav_TreeView1_3 {
	FONT-WEIGHT: bold; FONT-SIZE: small
}
.ctl00_ctl00_Nav_TreeView1_4 {
	
}
.ctl00_ctl00_Nav_TreeView1_5 {
	FONT-WEIGHT: bold
}
.ctl00_ctl00_Nav_TreeView1_6 {
	
}
.ctl00_ctl00_Nav_TreeView1_7 {
	
}
.ctl00_ctl00_Nav_TreeView1_8 {
	
}
.ctl00_ctl00_Nav_TreeView1_9 {
	
}
.ctl00_ctl00_Nav_TreeView1_10 {
	
}
</STYLE>
    <script id="clientEventHandlersJS" language="javascript" type="text/javascript">
        function ErrMsg() {

            var stage   = document.getElementById('ctl00_PageBody_cmbStage');
            var client  = document.getElementById('ctl00_PageBody_cmbAccount');
            var JID     = document.getElementById('ctl00_PageBody_cmbJID').value;
            var PDFName = document.getElementById("ctl00_PageBody_txtPDFName").value

            
            if (stage.value == "FRESH" && client.value == "THIEME") {
                if (document.getElementById("ctl00_PageBody_txtCorAuthName").value == "") {
                    alert('Please enter correspondence author name.');
                    return false;
                }
                else if (document.getElementById("ctl00_PageBody_txtPDFName").value == "" && client.value == "THIEME") {
                    alert('Please enter eproof pdf name.');
                    return false;
                }
                else {
                    var CorEmail = document.getElementById("ctl00_PageBody_txtCorAuthEmail").value;

                    if (CorEmail == "") {
                        alert('Please enter correspondence author email.');
                        return false;
                    }
                    else {
                        try {
                            var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                            if (reg.test(CorEmail) == false) {
                                alert("Aouthor's mail address is invalid");
                                return false;
                            }
                        }
                        catch (err) {
                            alert(err.message);
                        }
                    }
                }
                if (true)//(PDFName.toUpperCase().indexOf(JID)!=0)
                {
                    try
                    {
                        var RmngPdfName = PDFName.toUpperCase().replace(JID, "");
                        var ChkStr = RmngPdfName.substr(0, 1)
                        var patt = new RegExp("[0-9-]");
                        if (patt.test(ChkStr) == false)
                        {
                            alert('pdf name must be start with JID.');
                            return false;
                        }
                    }
                    catch (err)
                    {
                        alert(err.message);
                        return false;
                    }
                    
                }
            }
            else if (stage.value == "-Select-") {
                alert("Please select the stage.");
                return false;
            }

            
            if (document.getElementById("ctl00_PageBody_txtAID").value == "") {
                alert("Article AID must be filled.");
                return false;
            }
            else if (document.getElementById("ctl00_PageBody_txtPages").value == "") {
                alert("MSS pages must be filled.");
                return false;
            }
            else if (document.getElementById("ctl00_PageBody_flUpload").value == "") {
                alert("Uploaded file path  must be filled.");
                return false;
            }
            else if (document.getElementById("ctl00_PageBody_flUpload").value.indexOf(".zip", 0) == -1) {
                alert("Uploded file must be zip.");
                return false;
            }
            return true;
        }
     </script>

<BODY bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
<!-- Start of Toolbar -->
<DIV>

<TABLE id=ctl00_ctl00_Table1 
style="WIDTH: 100%; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: none" 
cellSpacing=0 cellPadding=0 border=0>
  <TBODY>
  <TR id=ctl00_ctl00_TableRow2>
    <TD id=ctl00_ctl00_TableCell1 vAlign=top rowSpan=2><IMG 
      id=ctl00_ctl00_Image1 
      style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" 
      src="ImagesAndStyleSheet/xmlBan.jpg">
      </TD>
    <TD id=ctl00_ctl00_TableCell2 vAlign=top><IMG id=ctl00_ctl00_Image2 
      style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" 
      src="ImagesAndStyleSheet/banner_curve.gif"></TD>
    <TD id=ctl00_ctl00_TableCell3 style="BACKGROUND-COLOR: white"></TD></TR>
  <TR id=ctl00_ctl00_TableRow3>
    <TD id=ctl00_ctl00_TableCell4 vAlign=top><IMG id=ctl00_ctl00_BannerFade 
      style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" 
      src="ImagesAndStyleSheet/banner_fade.gif"></TD>
    <TD class=bannerbg id=ctl00_ctl00_TableCell5 vAlign=top colSpan=5></TD></TR>
  <TR id=ctl00_ctl00_TableRow4>
    <TD class=toolbar id=ctl00_ctl00_TableCell6 
    style="WHITE-SPACE: nowrap; HEIGHT: 25px" vAlign=middle colSpan=2>
      <TABLE id=ctl00_ctl00_Table3 
      style="WIDTH: 100%; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: none" 
      cellSpacing=0 cellPadding=0 border=0>
        <TBODY>
        <TR id=ctl00_ctl00_TableRow5>
          <TD id=ctl00_ctl00_TableCell8 
            style="WHITE-SPACE: nowrap">&nbsp;&nbsp; <A class=toolbar 
            id=ctl00_ctl00_HyperLink1 
            href="GenerateOrder.aspx">Home</A> 
            &nbsp;&nbsp;|&nbsp;&nbsp; 
             
            &nbsp;&nbsp;|&nbsp;&nbsp; 
            <A class=toolbar id=ctl00_ctl00_HyperLink4 href="IssueXML.aspx">Create Issue Order</A> 
            &nbsp;&nbsp;|&nbsp;&nbsp; 
            <A class=toolbar id=ctl00_ctl00_HyperLink3 href="LogData.aspx">Order Logs</A> 
            &nbsp;&nbsp;|&nbsp;&nbsp; 
            <A class=toolbar id=ctl00_ctl00_HyperLink5 href=ItemReport.aspx>Item Status</A> 
            &nbsp;&nbsp;|&nbsp;&nbsp; 
            <A class=toolbar id=ctl00_ctl00_HyperLink6 href="OrderViewer.aspx">XML Browser</A>
            
            </TD></TR>
            </TBODY>
            </TABLE>
            </TD>
    <TD class=toolbar id=ctl00_ctl00_TableCell9 
    style="WIDTH: 100%; WHITE-SPACE: nowrap" vAlign=middle
      align=right>&nbsp;&nbsp;|&nbsp;&nbsp; Welcome&nbsp;&nbsp;|&nbsp;&nbsp;</TD>
    <TD class=toolbar id=ctl00_ctl00_TableCell10 
    style="WIDTH: 100%; WHITE-SPACE: nowrap" vAlign=middle align=right> &nbsp;&nbsp; </TD></TR></TBODY></TABLE>
<TABLE class=sectiontitle id=ctl00_ctl00_Table7 
style="WIDTH: 100%; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: none" 
cellSpacing=0 cellPadding=0 border=0>
  <TBODY>
  <TR id=ctl00_ctl00_TableRow7>
    <TD id=ctl00_ctl00_TableCell7 style="HEIGHT: 100%">
      <H1><SPAN id=ctl00_ctl00_BannerTitle>   </SPAN><SPAN id=ctl00_ctl00_BannerSubTitle></SPAN></H1></TD>
    <TD class=headerLinkBackground id=ctl00_ctl00_TableCell22 
    style="HEIGHT: 100%" vAlign=middle align=right>
    <a href="Default.aspx" onclick=""><font color=white><b>Logout...</b></font></a></TD></TR></TBODY></TABLE><!-- End of Toolbar -->
</DIV>
        

<TABLE id=ctl00_ctl00_Table21 
style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; WIDTH: 100%; BORDER-BOTTOM: 1px solid; BORDER-COLLAPSE: collapse" 
cellSpacing=0 cellPadding=0 border=0>
  <TBODY>
  <TR id=ctl00_ctl00_TableRow1 vAlign=top align=left>
    <TD id=ctl00_ctl00_NavCell1 vAlign=top align=left>


      <TABLE cellSpacing=0 cellPadding=1 width="100%" bgColor=#ffffff 
        border=0><TBODY>
        <TR>
          <TD align=center colSpan=12>

          </TD></TR></TBODY>

      </TABLE>

      <TABLE class=footerBG1 cellSpacing=0 cellPadding=1 width="100%" bgColor=#ffffff border=0>
        <TBODY>
        <TR>
          <TD align=center bgColor=#0066cc colSpan=13><A class=footerLinks 
            href="http://www.thomsondigital.com"><marquee behavior=alternate><font color=white>Hosted by Software Development Department, Thomson Digital (A Division of Thomson Press India Ltd.)</font></marquee></A></TD></TR>
        <tr><td></td></tr>
     </TBODY></TABLE>
</body>
</html>

<body bottommargin="0" leftmargin="0"  background="background.jpg" text="red" style="width:100%">
    
<table width="100%" height="94" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
<!-- the LOGO is below, edit blanklogo.jpg / or .psd and save as a different
name to create a logo.  Then change the src="" below to the new file name -->
		<td width="150" height="94"><img src="templogo.jpg" width="150" height="94" border="0" alt=""></td>
<!-- END OF LOGO ----------------------------------------------------------->
		<td width="368" height="94"><img src="topbar1.jpg" width="368" height="94" border="0" alt=""></td>
		<td width="100%" height="94" background="topbar1bg.jpg" align=right>&nbsp; 
<!--		
		
		
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

<body bottommargin="0" leftmargin="0"  background="background.jpg" text="red">
<table width="100%" height="94" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
<!-- the LOGO is below, edit blanklogo.jpg / or .psd and save as a different
name to create a logo.  Then change the src="" below to the new file name -->
		<td width="150" height="94"><img src="templogo.jpg" width="150" height="94" border="0" alt=""></td>
<!-- END OF LOGO ----------------------------------------------------------->
		<td width="368" height="94"><img src="topbar1.jpg" width="368" height="94" border="0" alt=""></td>
		<td width="100%" height="94" background="topbar1bg.jpg" align=right>&nbsp; 
            

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
		<td width="368" style="height: 33px"><img src="3buttonarea.jpg" width="427" height="33" border="0" alt=""></td>
		<td width="100%" background="3buttonareabg.jpg" style="height: 33px">&nbsp;</td>
	</tr>
</table>



<table cellpadding="0" cellspacing="0" border="0" style="width: 96%">
	<tr valign="top">

		<td width="207" style="height: 188px" align=right>

<a href=""><img src="tempbuttons/home.jpg" width="207" height="33" border="0" alt=""></a><BR>
<a href="OrderDetails.aspx"><img src="tempbuttons/ogen.jpg" width="207" height="33" border="0" alt="" id="IMG3" language="javascript" onclick="return IMG2_onclick()"></a><BR>
<a href="ViewLog.aspx"><img src="tempbuttons/oviw.jpg" width="207" height="33" border="0" alt="" id="IMG4" language="javascript" onclick="return IMG1_onclick()"></a><BR>
<a href=""><img src="tempbuttons/Abtus.jpg" width="207" border="0" alt="" style="height: 29px; z-index: 101; left: 0px;  top: 397px;"></a><BR>
<a href="ChangePassword.aspx"><img src="tempbuttons/ChangePassword.jpg" width="207" height="33" border="0" alt="" style="z-index: 100; left: 0px;  top: 364px"></a><BR>
<a href="UpdateOrder.aspx"><img src="tempbuttons/UpdateOrder.jpg" width="207" border="0" alt="" style="height: 32px; z-index: 103; left: 0px;  top: 332px;"></a><BR>
		</td>

	
	
		<td style="height: 188px; width: 20px;">&nbsp;&nbsp;&nbsp;</td>

		<td style="height: 350px; width: 96%;">
                
            <a href="orderdetails.aspx"><img src="tempbuttons/view.jpg" border="0" alt="" id="Img1" language="javascript" onclick="return IMG1_onclick()" style="z-index: 104; left: 412px;  top: 381px; width: 65px; height: 25px;"></a>

</td>
		
</tr>
		</table>			
							


</body>
</html>





<body bottommargin="0" leftmargin="0"  background="background.jpg" text="red">
    <table width="100%" height="94" cellpadding="0" cellspacing="0" border="0">
	<tr valign="top">
<!-- the LOGO is below, edit blanklogo.jpg / or .psd and save as a different
name to create a logo.  Then change the src="" below to the new file name -->
		<td width="150" height="94"><img src="templogo.jpg" width="150" height="94" border="0" alt=""></td>
<!-- END OF LOGO ----------------------------------------------------------->
		<td width="368" height="94"><img src="topbar1.jpg" width="368" height="94" border="0" alt=""></td>
		<td width="100%" height="94" background="topbar1bg.jpg" align=right>&nbsp; 
<!--		
		
		
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



<table cellpadding="0" cellspacing="0" border="0" style="width: 96%">
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

		<td width="100%" style="height: 450px;">
        <iframe  src="LogData.aspx" id="report" width='100%' height='100%' frameborder="0" scrolling="auto" style="width: 102%;height:102%">
	    </iframe>
</td>
		
</tr>
		</table>			
							


</body>
</html>






