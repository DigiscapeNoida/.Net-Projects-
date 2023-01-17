<?xml version='1.0' encoding='utf-8'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!-- New name space required for position method -->
<!-- <xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl"> -->

<!-- This file formats the coversheet-->
<!-- Jan 03 IMcG Many changes for components and general display issues -->
<!-- Jan 04 BHW Changed namespace to match other stylesheets and made changes necessary for this namespace
      Added copyright rec'd date
      Rationalised date formats so that we have only one date format template not 5 (!)
      -->
<!-- BHW 10 March 2004 Added Check for corrections result   
      Removed #Logical Graphics
      Rename #Physical Graphics to #Graphics 
      Added space between City and Postcode, Forename and Surname-->
      
	<xsl:variable name="totalbatches" select="count(//batch-member)"/>
	<xsl:variable name="myJID" select="//jid"/>
	<xsl:variable name="myAID" select="//aid"/>

  <!--################################################################################-->
  <xsl:template match="/">
    <xsl:apply-templates select="orders"/>
  </xsl:template>

  <!--################################################################################-->
  <xsl:template match="orders">
    <xsl:for-each select="order">
      <html>
        <head>COVERSHEET
						<img src="http://172.16.2.19/OrderViewer/xslt/duck.jpg" align="right"/>
						<h4>
							<font color="red">
								<b>
								<xsl:text>COVERSHEET (Duck and Duckling) Prepared by Software Development</xsl:text>
								</b>
							</font>
						</h4>
</head>
      </html>
      <p/>
      <table width="100%">
        <table >
           <tr><td>Date:</td><td><xsl:choose>
<xsl:when test="//different//time"><font color="red"><b><u><xsl:apply-templates select="//different//time"/></u></b></font></xsl:when>
<xsl:when test="//delete//time"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//time"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//time"><font color="green"><b><u><xsl:apply-templates select="//insert//time"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//time"/></xsl:otherwise>
</xsl:choose></td></tr>
           <tr><td>PII:</td><td><xsl:choose>
<xsl:when test="//item-info/different/pii"><font color="red"><b><u><xsl:apply-templates select="//item-info/different/pii"/></u></b></font></xsl:when>
<xsl:when test="//item-info/delete/pii"><font color="blue"><b><s><u><xsl:apply-templates select="//item-info/delete/pii"/></u></s></b></font></xsl:when> 
<xsl:when test="//item-info/insert/pii"><font color="green"><b><u><xsl:apply-templates select="//item-info/insert/pii"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-info/pii"/></xsl:otherwise>
</xsl:choose></td></tr>
           <tr><td>Online Version:</td><td><xsl:choose>
<xsl:when test="//item-info/different/online-version/@type"><font color="red"><b><u><xsl:apply-templates select="//item-info/different/online-version/@type"/></u></b></font></xsl:when>
<xsl:when test="//item-info/delete/online-version/@type"><font color="blue"><b><s><u><xsl:apply-templates select="//item-info/delete/online-version/@type"/></u></s></b></font></xsl:when> 
<xsl:when test="//item-info/insert/online-version/@type"><font color="green"><b><u><xsl:apply-templates select="//item-info/insert/online-version/@type"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-info/online-version/@type"/></xsl:otherwise>
</xsl:choose></td></tr>
           <tr><td>DOI:</td><td><xsl:choose>
<xsl:when test="//different/item-info/doi|//item-info/different/doi"><font color="red"><b><u><xsl:apply-templates select="//different/item-info/doi|//item-info/different/doi"/></u></b></font></xsl:when>
<xsl:when test="//delete/item-info/doi|//item-info/delete/doi"><font color="blue"><b><s><u><xsl:apply-templates select="//delete/item-info/doi|//item-info/delete/doi"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert/item-info/doi|//item-info/insert/doi"><font color="green"><b><u><xsl:apply-templates select="//insert/item-info/doi|//item-info/insert/doi"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="item-info/doi"/></xsl:otherwise>
</xsl:choose></td></tr>
           <tr><td>Version Nr:</td><td><xsl:choose>
<xsl:when test="//different//version-no"><font color="red"><b><u><xsl:apply-templates select="//different//version-no"/></u></b></font></xsl:when>
<xsl:when test="//delete//version-no"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//version-no"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//version-no"><font color="green"><b><u><xsl:apply-templates select="//insert//version-no"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//version-no"/></xsl:otherwise>
</xsl:choose></td></tr>
		<tr><xsl:choose>
<xsl:when test="//different//embargo"><td>Embargo:</td><td><font color="red"><b><u><xsl:apply-templates select="//different//embargo"/></u></b></font></td></xsl:when>
<xsl:when test="//delete//embargo"><td>Embargo:</td><td><font color="blue"><b><s><u><xsl:apply-templates select="//delete//embargo"/></u></s></b></font></td></xsl:when> 
<xsl:when test="//insert//embargo"><td>Embargo:</td><td><font color="green"><b><u><xsl:apply-templates select="//insert//embargo"/></u></b></font></td></xsl:when> 
<xsl:when test="//embargo"><td>Embargo:</td><td><xsl:apply-templates select="//embargo"/></td></xsl:when>
</xsl:choose>></tr>


           <tr><td>For:</td><td><xsl:choose>
<xsl:when test="//different//prod-site"><font color="red"><b><u><xsl:apply-templates select="//different//prod-site"/></u></b></font></xsl:when>
<xsl:when test="//delete//prod-site"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//prod-site"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//prod-site"><font color="green"><b><u><xsl:apply-templates select="//insert//prod-site"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="prod-site"/></xsl:otherwise>
</xsl:choose></td></tr>
           <tr><td>Supplier:</td><td><xsl:choose>
<xsl:when test="//different//executor/exec-name|//executor/different/exec-name"><font color="red"><b><u><xsl:value-of select="//different//executor/exec-name|//executor/different/exec-name"/></u></b></font></xsl:when>
<xsl:when test="//delete//executor/exec-name|//executor/delete/exec-name"><font color="blue"><b><s><u><xsl:value-of select="//delete//executor/exec-name|//executor/delete/exec-name"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//executor/exec-name|//executor/insert/exec-name"><font color="green"><b><u><xsl:value-of select="//insert//executor/exec-name|//executor/insert/exec-name"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:value-of select="//exec-name[1]"/></xsl:otherwise>
</xsl:choose></td></tr>
        </table>

        <tr><td colspan="2"><hr/></td></tr>

        <table width="100%">
        <tr>
          <td>
            <table>
              <tr><td>Item:</td><td><xsl:choose>
<xsl:when test="//different//jid"><font color="red"><b><u><xsl:apply-templates select="//different//jid"/></u></b></font></xsl:when>
<xsl:when test="//delete//jid"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//jid"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//jid"><font color="green"><b><u><xsl:apply-templates select="//insert//jid"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//jid"/></xsl:otherwise>
</xsl:choose> <xsl:choose>
<xsl:when test="item-info/different/aid"><font color="red"><b><u><xsl:apply-templates select="item-info/different/aid"/></u></b></font></xsl:when>
<xsl:when test="item-info/delete/aid"><font color="blue"><b><s><u><xsl:apply-templates select="item-info/delete/aid"/></u></s></b></font></xsl:when> 
<xsl:when test="item-info/insert/aid"><font color="green"><b><u><xsl:apply-templates select="item-info/insert/aid"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="item-info/aid"/></xsl:otherwise>
</xsl:choose></td></tr>
              
              <tr><td width="24%"># Item pages:</td><td width="25%"><xsl:choose>
<xsl:when test="//different//no-mns-pages"><font color="red"><b><u><xsl:apply-templates select="//different//no-mns-pages"/></u></b></font></xsl:when>
<xsl:when test="//delete//no-mns-pages"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//no-mns-pages"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//no-mns-pages"><font color="green"><b><u><xsl:apply-templates select="//insert//no-mns-pages"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//no-mns-pages"/></xsl:otherwise>
</xsl:choose></td>
                    <td width="22%">Publication item type:</td><td><xsl:choose>
<xsl:when test="//different//pit"><font color="red"><b><u><xsl:apply-templates select="//item-info/different//pit"/></u></b></font></xsl:when>
<xsl:when test="//delete//pit"><font color="blue"><b><s><u><xsl:apply-templates select="//item-info/delete//pit"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//pit"><font color="green"><b><u><xsl:apply-templates select="//item-info/insert//pit"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-info/pit"/></xsl:otherwise>
</xsl:choose></td></tr>
                    
              <tr><td># Graphics:</td><td><xsl:choose>
<xsl:when test="//different//no-phys-figs"><font color="red"><b><u><xsl:apply-templates select="//different//no-phys-figs"/></u></b></font></xsl:when>
<xsl:when test="//delete//no-phys-figs"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//no-phys-figs"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//no-phys-figs"><font color="green"><b><u><xsl:apply-templates select="//insert//no-phys-figs"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//no-phys-figs"/></xsl:otherwise>
</xsl:choose></td>
                    <td>Sub Item PII:</td><td><xsl:choose>
<xsl:when test="//different//subitem[1]"><font color="red"><b><u><xsl:apply-templates select="//different//subitem[1]"/></u></b></font></xsl:when>
<xsl:when test="//delete//subitem[1]"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//subitem[1]"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//subitem[1]"><font color="green"><b><u><xsl:apply-templates select="//insert//subitem[1]"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//subitem[1]"/></xsl:otherwise>
</xsl:choose></td></tr>
                    
              <tr valign="top"><td># B/W graphics:</td><td><xsl:choose>
<xsl:when test="//different//no-bw-figs"><font color="red"><b><u><xsl:apply-templates select="//different//no-bw-figs"/></u></b></font></xsl:when>
<xsl:when test="//delete//no-bw-figs"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//no-bw-figs"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//no-bw-figs"><font color="green"><b><u><xsl:apply-templates select="//insert//no-bw-figs"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//no-bw-figs"/></xsl:otherwise>
</xsl:choose></td>

                    <td>Refers to PII:</td>
                    <td>
<xsl:choose>
<xsl:when test="//different//refers-to-document[1]/pii|//refers-to-document[1]/different/pii"><font color="red"><b><u><xsl:value-of select="//different//refers-to-document[1]/pii|//refers-to-document[1]/different/pii"/></u></b></font></xsl:when>
<xsl:when test="//delete//refers-to-document[1]/pii|//refers-to-document[1]/delete/pii"><font color="blue"><b><s><u><xsl:value-of select="//delete//refers-to-document[1]/pii|//refers-to-document[1]/delete/pii"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[1]/pii|//refers-to-document[1]/insert/pii"><font color="green"><b><u><xsl:value-of select="//insert//refers-to-document[1]/pii|//refers-to-document[1]/insert/pii"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:value-of select="//refers-to-document[1]/pii[1]"/></xsl:otherwise>
</xsl:choose>
<xsl:if test="//refers-to-document[2]/pii!=''">, <br><xsl:choose>
<xsl:when test="//different//refers-to-document[2]/pii[1]|//refers-to-document[2]/different/pii[1]"><font color="red"><u><b><xsl:value-of select="//different//refers-to-document[2]/pii[1]|//refers-to-document[2]/different/pii[1]"/></b></u></font></xsl:when>
<xsl:when test="//delete//refers-to-document[2]/pii[1]|//refers-to-document[2]/delete/pii[1]"><font color="blue"><u><b><s><xsl:value-of select="//delete//refers-to-document[2]/pii[1]|//refers-to-document[2]/delete/pii[1]"/></s></b></u></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[2]/pii[1]|//refers-to-document[2]/insert/pii[1]"><font color="green"><u><b><xsl:value-of select="//insert//refers-to-document[2]/pii[1]|//refers-to-document[2]/insert/pii[1]"/></b></u></font></xsl:when> 
<xsl:otherwise><xsl:value-of select="//refers-to-document[2]/pii[1]"/></xsl:otherwise>
</xsl:choose></br></xsl:if>
<xsl:if test="//refers-to-document[3]/pii!=''"><xsl:choose>
<xsl:when test="//different//refers-to-document[3]/pii[1]|//refers-to-document[3]/different/pii[1]"><font color="red"><b> ...</b></font></xsl:when>
<xsl:when test="//delete//refers-to-document[3]/pii[1]|//refers-to-document[3]/delete/pii[1]"><font color="blue"><b><s> ...</s></b></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[3]/pii[1]|//refers-to-document[3]/insert/pii[1]"><font color="green"><b> ...</b></font></xsl:when> 
<xsl:otherwise> ...</xsl:otherwise>
</xsl:choose></xsl:if>

<!--		    <xsl:value-of select="//refers-to-document[1]/pii"/>
                    <xsl:if test="//refers-to-document[2]/pii!=''">, <br><xsl:value-of select="//refers-to-document[2]/pii"/></br></xsl:if>
                    <xsl:if test="//refers-to-document[3]/pii!=''"> ...</xsl:if>-->
                    </td>
                    </tr>
                    
              <tr valign="top"><td># Web colour graphics:</td><td><xsl:choose>
<xsl:when test="//different//no-web-colour-figs"><font color="red"><b><u><xsl:apply-templates select="//different//no-web-colour-figs"/></u></b></font></xsl:when>
<xsl:when test="//delete//no-web-colour-figs"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//no-web-colour-figs"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//no-web-colour-figs"><font color="green"><b><u><xsl:apply-templates select="//insert//no-web-colour-figs"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//no-web-colour-figs"/></xsl:otherwise>
</xsl:choose></td>

                    <td>Refers to DOI:</td>
<td><xsl:choose>
<xsl:when test="//different//refers-to-document[1]/doi|//refers-to-document[1]/different/doi"><font color="red"><b><u><xsl:value-of select="//different//refers-to-document[1]/doi|//refers-to-document[1]/different/doi"/></u></b></font></xsl:when>
<xsl:when test="//delete//refers-to-document[1]/doi|//refers-to-document[1]/delete/doi"><font color="blue"><b><s><u><xsl:value-of select="//delete//refers-to-document[1]/doi|//refers-to-document[1]/delete/doi"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[1]/doi|//refers-to-document[1]/insert/doi"><font color="green"><b><u><xsl:value-of select="//insert//refers-to-document[1]/doi|//refers-to-document[1]/insert/doi"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:value-of select="//refers-to-document[1]/doi[1]"/></xsl:otherwise>
</xsl:choose>
<xsl:if test="//refers-to-document[2]/doi!=''">, <br><xsl:choose>
<xsl:when test="//different//refers-to-document[2]/doi[1]|//refers-to-document[2]/different/doi[1]"><font color="red"><u><b><xsl:value-of select="//different//refers-to-document[2]/doi[1]|//refers-to-document[2]/different/doi[1]"/></b></u></font></xsl:when>
<xsl:when test="//delete//refers-to-document[2]/doi[1]|//refers-to-document[2]/delete/doi[1]"><font color="blue"><u><b><s><xsl:value-of select="//delete//refers-to-document[2]/doi[1]|//refers-to-document[2]/delete/doi[1]"/></s></b></u></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[2]/doi[1]|//refers-to-document[2]/insert/doi[1]"><font color="green"><u><b><xsl:value-of select="//insert//refers-to-document[2]/doi[1]|//refers-to-document[2]/insert/doi[1]"/></b></u></font></xsl:when> 
<xsl:otherwise><xsl:value-of select="//refers-to-document[2]/doi[1]"/></xsl:otherwise>
</xsl:choose></br></xsl:if>
<xsl:if test="//refers-to-document[3]/doi!=''"><xsl:choose>
<xsl:when test="//different//refers-to-document[3]/doi[1]|//refers-to-document[3]/different/doi[1]"><font color="red"><b> ...</b></font></xsl:when>
<xsl:when test="//delete//refers-to-document[3]/doi[1]|//refers-to-document[3]/delete/doi[1]"><font color="blue"><b><s> ...</s></b></font></xsl:when> 
<xsl:when test="//insert//refers-to-document[3]/doi[1]|//refers-to-document[3]/insert/doi[1]"><font color="green"><b> ...</b></font></xsl:when> 
<xsl:otherwise> ...</xsl:otherwise>
</xsl:choose></xsl:if>


<!--<xsl:value-of select="//refers-to-document[1]/doi"/>
                    <xsl:if test="//refers-to-document[2]/doi!=''">, <br><xsl:value-of select="//refers-to-document[2]/doi"/></br></xsl:if>
                    <xsl:if test="//refers-to-document[3]/doi!=''"> ...</xsl:if>-->
                    </td>
                    </tr>
              
              <tr><td># Printed colour graphics:</td><td><xsl:choose>
<xsl:when test="//different//no-colour-figs"><font color="red"><b><u><xsl:apply-templates select="//different//no-colour-figs"/></u></b></font></xsl:when>
<xsl:when test="//delete//no-colour-figs"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//no-colour-figs"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//no-colour-figs"><font color="green"><b><u><xsl:apply-templates select="//insert//no-colour-figs"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//no-colour-figs"/></xsl:otherwise>
</xsl:choose></td>
                    <td>E Submission Item Nr:</td><td><xsl:choose>
<xsl:when test="//different//e-submission-item-nr"><font color="red"><b><u><xsl:apply-templates select="//different//e-submission-item-nr"/></u></b></font></xsl:when>
<xsl:when test="//delete//e-submission-item-nr"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//e-submission-item-nr"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//e-submission-item-nr"><font color="green"><b><u><xsl:apply-templates select="//insert//e-submission-item-nr"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//e-submission-item-nr"/></xsl:otherwise>
</xsl:choose></td></tr> 
                    
               <tr><td>Copyright status:</td><td><xsl:choose>
<xsl:when test="//different//copyright-status"><font color="red"><b><u><xsl:apply-templates select="//different//copyright-status"/></u></b></font></xsl:when>
<xsl:when test="//delete//copyright-status"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//copyright-status"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//copyright-status"><font color="green"><b><u><xsl:apply-templates select="//insert//copyright-status"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//copyright-status"/></xsl:otherwise>
</xsl:choose></td>
                     <td>EO Item Nr:</td><td><xsl:choose>
<xsl:when test="//different//eo-item-nr"><font color="red"><b><u><xsl:apply-templates select="//different//eo-item-nr"/></u></b></font></xsl:when>
<xsl:when test="//delete//eo-item-nr"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//eo-item-nr"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//eo-item-nr"><font color="green"><b><u><xsl:apply-templates select="//insert//eo-item-nr"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//eo-item-nr"/></xsl:otherwise>
</xsl:choose></td></tr>
                     
               <tr><td>Copyright received date:</td> <td><xsl:choose>
<xsl:when test="//different//copyright-recd-date/date|//copyright-recd-date//different/date"><font color="red"><b><u><xsl:apply-templates select="//different//copyright-recd-date/date|//copyright-recd-date//different/date"/></u></b></font></xsl:when>
<xsl:when test="//delete//copyright-recd-date/date|//copyright-recd-date//delete/date"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//copyright-recd-date/date|//copyright-recd-date//delete/date"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//copyright-recd-date/date|//copyright-recd-date//insert/date"><font color="green"><b><u><xsl:apply-templates select="//insert//copyright-recd-date/date|//copyright-recd-date//insert/date"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//copyright-recd-date/date"/></xsl:otherwise>
</xsl:choose></td>      
                     <td>Editor:</td><td><xsl:choose>
<xsl:when test="//different//editor"><font color="red"><b><u><xsl:apply-templates select="//different//editor"/></u></b></font></xsl:when>
<xsl:when test="//delete//editor"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//editor"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//editor"><font color="green"><b><u><xsl:apply-templates select="//insert//editor"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//editor"/></xsl:otherwise>
</xsl:choose></td></tr>                      
                     
               <tr><td>Production type as sent:</td><td><xsl:choose>
<xsl:when test="//different//prd-type-as-sent"><font color="red"><b><u><xsl:apply-templates select="//different//prd-type-as-sent"/></u></b></font></xsl:when>
<xsl:when test="//delete//prd-type-as-sent"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//prd-type-as-sent"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//prd-type-as-sent"><font color="green"><b><u><xsl:apply-templates select="//insert//prd-type-as-sent"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//prd-type-as-sent"/></xsl:otherwise>
</xsl:choose></td>              
                     <td>Date received:</td><td><xsl:choose>
<xsl:when test="//different//received-date/date|//received-date//different/date"><font color="red"><b><u><xsl:apply-templates select="//different//received-date/date|//received-date//different/date"/></u></b></font></xsl:when>
<xsl:when test="//delete//received-date/date|//received-date//delete/date"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//received-date/date|//received-date//delete/date"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//received-date/date|//received-date//insert/date"><font color="green"><b><u><xsl:apply-templates select="//insert//received-date/date|//received-date//insert/date"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//received-date/date"/></xsl:otherwise>
</xsl:choose></td></tr>
                     
               <tr><td>Corrections:</td><td><xsl:choose>
<xsl:when test="//different//corrections/@type"><font color="red"><b><u><xsl:apply-templates select="//different//corrections/@type"/></u></b></font></xsl:when>
<xsl:when test="//delete//corrections/@type"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corrections/@type"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corrections/@type"><font color="green"><b><u><xsl:apply-templates select="//insert//corrections/@type"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corrections/@type"/></xsl:otherwise>
</xsl:choose></td>
                      <td>Date revised:</td><td><xsl:choose>
<xsl:when test="//different//revised-date/date|//revised-date//different/date"><font color="red"><b><u><xsl:apply-templates select="//different//revised-date/date|//revised-date//different/date"/></u></b></font></xsl:when>
<xsl:when test="//delete//revised-date/date|//revised-date//delete/date"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//revised-date/date|//revised-date//delete/date"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//revised-date/date|//revised-date//insert/date"><font color="green"><b><u><xsl:apply-templates select="//insert//revised-date/date|//revised-date//insert/date"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//revised-date/date"/></xsl:otherwise>
</xsl:choose></td></tr>
                      
               <tr><td></td><td></td> <td>Date accepted:</td>
                      <td><xsl:choose>
<xsl:when test="//different//accept-date/date|//accept-date//different/date"><font color="red"><b><u><xsl:apply-templates select="//different//accept-date/date|//accept-date//different/date"/></u></b></font></xsl:when>
<xsl:when test="//delete//accept-date/date|//accept-date//delete/date"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//accept-date/date|//accept-date//delete/date"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//accept-date/date|//accept-date//insert/date"><font color="green"><b><u><xsl:apply-templates select="//insert//accept-date/date|//accept-date//insert/date"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//accept-date/date"/></xsl:otherwise>
</xsl:choose></td></tr>                       
            </table>
          </td>
        </tr>
</table>
<xsl:apply-templates select="//e-components"/>

      <tr><td colspan="2"><hr/></td></tr>
	<table>
          <tr><td>Item title:</td><td><xsl:choose>
<xsl:when test="//different//item-title"><font color="red"><b><u><xsl:apply-templates select="//different//item-title"/></u></b></font></xsl:when>
<xsl:when test="//delete//item-title"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//item-title"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//item-title"><font color="green"><b><u><xsl:apply-templates select="//insert//item-title"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-title"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>Item group:</td><td><xsl:choose>
<xsl:when test="//different//item-group"><font color="red"><b><u><xsl:apply-templates select="//different//item-group"/></u></b></font></xsl:when>
<xsl:when test="//delete//item-group"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//item-group"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//item-group"><font color="green"><b><u><xsl:apply-templates select="//insert//item-group"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-group"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>Item group description:</td><td><xsl:choose>
<xsl:when test="//different//item-group-description"><font color="red"><b><u><xsl:apply-templates select="//different//item-group-description"/></u></b></font></xsl:when>
<xsl:when test="//delete//item-group-description"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//item-group-description"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//item-group-description"><font color="green"><b><u><xsl:apply-templates select="//insert//item-group-description"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//item-group-description"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>Section:</td><td><xsl:choose>
<xsl:when test="//different//section"><font color="red"><b><u><xsl:apply-templates select="//different//section"/></u></b></font></xsl:when>
<xsl:when test="//delete//section"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//section"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//section"><font color="green"><b><u><xsl:apply-templates select="//insert//section"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//section"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>First author:</td><td><xsl:apply-templates select="//first-author"/></td></tr>
          <tr><td>Corr. author:</td><td><xsl:apply-templates select="//corr-author"/></td></tr>
	</table>
	<hr/>
	<table width="100%">
          <tr><td valign="top">Item Remarks:</td><td valign="top"><xsl:apply-templates select="//item-remarks"/></td></tr>
          <tr><td valign="top">Issue remarks:</td><td valign="top"><xsl:apply-templates select="//issue-remarks"/></td></tr>
	</table>

				<tr>
					<td colspan="2">
						<hr/>
					</td>
				</tr>
				<!--Modification done by Rakesh Pandey on 26/10/05 for Duck & Ducklings -->
				<table width="100%" border="1">
					<xsl:if test="//batch">
						<font color="#0000FF"><b>Ducklings Information</b></font>
						<b><xsl:text>:   There are  </xsl:text></b>
						<font color="red"><b>
							<xsl:value-of select="$totalbatches"/></b>
						</font>
						<xsl:text> </xsl:text>
						<b>
							<xsl:text>ducklings in </xsl:text><font color="red">
							<xsl:value-of select="$myJID"/><xsl:text>/</xsl:text><xsl:value-of select="$myAID"/></font><xsl:text>, details are as follows :</xsl:text>
							
						</b>
						<tr>
							<td valign="top">
								<xsl:apply-templates select="//item-info"/>
							</td>
						</tr>
					</xsl:if>
				</table>
      </table>
    </xsl:for-each>
  </xsl:template>
<xsl:template match="item-info">
		<tr bgcolor="#00008B">
			<td width="10%" align="center">
				<font color="white">
					<b>Sr. No.</b>
				</font>
			</td>
			<td width="10%" align="center">
				<font color="white">
					<b>AID</b>
				</font>
			</td>
			<td width="10%" align="center">
				<font color="white">
					<b>PIT</b>
				</font>
			</td>
			<td width="30%" align="center">
				<font color="white">
					<b>PII</b>
				</font>
			</td>
			<td width="30%" align="center">
				<font color="white">
					<b>DOI</b>
				</font>
			</td>
		</tr>
		<xsl:for-each select="batch">
			<xsl:for-each select="batch-member">
				<tr valign="top">
					<td width="5%" align="center">
							<xsl:number value="position()" format="1. "/>
					</td>
					<td width="10%" align="center">
<xsl:choose>
<xsl:when test="different/aid"><font color="red"><b><u><xsl:apply-templates select="different/aid"/></u></b></font></xsl:when>
<xsl:when test="delete/aid"><font color="blue"><b><s><u><xsl:apply-templates select="delete/aid"/></u></s></b></font></xsl:when> 
<xsl:when test="insert/aid"><font color="green"><b><u><xsl:apply-templates select="insert/aid"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="aid"/></xsl:otherwise>
</xsl:choose>
					</td>
					<td width="10%" align="center">
<xsl:choose>
<xsl:when test="different/pit"><font color="red"><b><u><xsl:apply-templates select="different/pit"/></u></b></font></xsl:when>
<xsl:when test="delete/pit"><font color="blue"><b><s><u><xsl:apply-templates select="delete/pit"/></u></s></b></font></xsl:when> 
<xsl:when test="insert/pit"><font color="green"><b><u><xsl:apply-templates select="insert/pit"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="pit"/></xsl:otherwise>
</xsl:choose>					</td>
					<td width="30%" align="center">
<xsl:choose>
<xsl:when test="different/pii"><font color="red"><b><u><xsl:apply-templates select="different/pii"/></u></b></font></xsl:when>
<xsl:when test="delete/pii"><font color="blue"><b><s><u><xsl:apply-templates select="delete/pii"/></u></s></b></font></xsl:when> 
<xsl:when test="insert/pii"><font color="green"><b><u><xsl:apply-templates select="insert/pii"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="pii"/></xsl:otherwise>
</xsl:choose>					</td>
					<td width="30%" align="center">
<xsl:choose>
<xsl:when test="different/doi"><font color="red"><b><u><xsl:apply-templates select="different/doi"/></u></b></font></xsl:when>
<xsl:when test="delete/doi"><font color="blue"><b><s><u><xsl:apply-templates select="delete/doi"/></u></s></b></font></xsl:when> 
<xsl:when test="insert/doi"><font color="green"><b><u><xsl:apply-templates select="insert/doi"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="doi"/></xsl:otherwise>
</xsl:choose>					</td>
				</tr>
			</xsl:for-each>
		</xsl:for-each>
	</xsl:template>  
  <!--################################################################################-->
  <!--This section is for formatting the corr-author name-->

  <xsl:template match="corr-author">
    <xsl:choose>
<xsl:when test="//different//corr-author/degree|//corr-author/different//degree"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author/degree|//corr-author/different//degree"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author/degree|//corr-author/delete//degree"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author/degree|//corr-author/delete//degree"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author/degree|//corr-author/insert//degree"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author/degree|//corr-author/insert//degree"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/degree"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//corr-author/fnm|//corr-author/different//fnm"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author/fnm|//corr-author/different//fnm"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author/fnm|//corr-author/delete//fnm"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author/fnm|//corr-author/delete//fnm"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author/fnm|//corr-author/insert//fnm"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author/fnm|//corr-author/insert//fnm"/></u></b></font></xsl:when>
<xsl:otherwise><xsl:apply-templates select="//corr-author/fnm"/></xsl:otherwise>
</xsl:choose>  <xsl:text> </xsl:text> <xsl:choose>
<xsl:when test="//different//corr-author/snm|//corr-author/different//snm"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author/snm|//corr-author/different//snm"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author/snm|//corr-author/delete//snm"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author/snm|//corr-author/delete//snm"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author/snm|//corr-author/insert//snm"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author/snm|//corr-author/insert//snm"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/snm"/></xsl:otherwise>
</xsl:choose>
          <tr><td><xsl:apply-templates select="./aff"/></td></tr>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the first-author address-->

  <xsl:template match="first-author">
    <xsl:choose>
<xsl:when test="//different//first-author/degree|//first-author/different//degree"><font color="red"><b><u><xsl:apply-templates select="//different//first-author/degree|//first-author/different//degree"/></u></b></font></xsl:when>
<xsl:when test="//delete//first-author/degree|//first-author/delete//degree"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//first-author/degree|//first-author/delete//degree"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//first-author/degree|//first-author/insert//degree"><font color="green"><b><u><xsl:apply-templates select="//insert//first-author/degree|//first-author/insert//degree"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//first-author/degree"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//first-author/fnm|//first-author/different//fnm"><font color="red"><b><u><xsl:apply-templates select="//different//first-author/fnm|//first-author/different//fnm"/></u></b></font></xsl:when>
<xsl:when test="//delete//first-author/fnm|//first-author/delete//fnm"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//first-author/fnm|//first-author/delete//fnm"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//first-author/fnm|//first-author/insert//fnm"><font color="green"><b><u><xsl:apply-templates select="//insert//first-author/fnm|//first-author/insert//fnm"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//first-author/fnm"/></xsl:otherwise>
</xsl:choose>  <xsl:text> </xsl:text> <xsl:choose>
<xsl:when test="//different//first-author/snm|//first-author/different//snm"><font color="red"><b><u><xsl:apply-templates select="//different//first-author/snm|//first-author/different//snm"/></u></b></font></xsl:when>
<xsl:when test="//delete//first-author/snm|//first-author/delete//snm"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//first-author/snm|//first-author/delete//snm"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//first-author/snm|//first-author/insert//snm"><font color="green"><b><u><xsl:apply-templates select="//insert//first-author/snm|//first-author/insert//snm"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//first-author/snm"/></xsl:otherwise>
</xsl:choose>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the corr-author address-->

  <xsl:template match="aff">
          <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//organization|//corr-author//different//organization"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//organization|//corr-author//different//organization"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//organization|//corr-author//delete//organization"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//organization|//corr-author//delete//organization"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//organization|//corr-author//insert//organization"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//organization|//corr-author//insert//organization"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/aff/organization"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//inst-contd|//corr-author//different//inst-contd"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//inst-contd|//corr-author//different//inst-contd"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//inst-contd|//corr-author//delete//inst-contd"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//inst-contd|//corr-author//delete//inst-contd"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//inst-contd|//corr-author//insert//inst-contd"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//inst-contd|//corr-author//insert//inst-contd"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/aff/inst-contd"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//institute|//corr-author//different//institute"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//institute|//corr-author//different//institute"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//institute|//corr-author//delete//institute"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//institute|//corr-author//delete//institute"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//institute|//corr-author//insert//institute"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//institute|//corr-author//insert//institute"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/aff/institute"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//address|//corr-author//different//address"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//address|//corr-author//different//address"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//address|//corr-author//delete//address"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//address|//corr-author//delete//address"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//address|//corr-author//insert//address"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//address|//corr-author//insert//address"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author/aff/address"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//address-contd|//corr-author//different//address-contd"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//address-contd|//corr-author//different//address-contd"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//address-contd|//corr-author//delete//address-contd"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//address-contd|//corr-author//delete//address-contd"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//address-contd|//corr-author//insert//address-contd"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//address-contd|//corr-author//insert//address-contd"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//address-contd"/></xsl:otherwise>
</xsl:choose></td></tr>
          <xsl:choose>
            
<xsl:when test="zipcode/@zipcode-pos='NONE'">
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:when>

            
<xsl:when test="zipcode/@zipcode-pos='BEFORECTY'">
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//zipcode|//corr-author//different//zipcode"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//zipcode|//corr-author//different//zipcode"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//zipcode|//corr-author//delete//zipcode"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//zipcode|//corr-author//delete//zipcode"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//zipcode|//corr-author//insert//zipcode"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//zipcode|//corr-author//insert//zipcode"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//zipcode"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:when>

            
<xsl:when test="zipcode/@zipcode-pos='AFTERCTY'">
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text> <xsl:choose>
<xsl:when test="//different//corr-author//zipcode|//corr-author//different//zipcode"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//zipcode|//corr-author//different//zipcode"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//zipcode|//corr-author//delete//zipcode"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//zipcode|//corr-author//delete//zipcode"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//zipcode|//corr-author//insert//zipcode"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//zipcode|//corr-author//insert//zipcode"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//zipcode"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:when>

            
<xsl:when test="zipcode/@zipcode-pos='BEFORECNY'">
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//zipcode|//corr-author//different//zipcode"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//zipcode|//corr-author//different//zipcode"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//zipcode|//corr-author//delete//zipcode"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//zipcode|//corr-author//delete//zipcode"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//zipcode|//corr-author//insert//zipcode"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//zipcode|//corr-author//insert//zipcode"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//zipcode"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:when>

            
<xsl:when test="zipcode/@zipcode-pos='AFTERCNY'">
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//corr-author//zipcode|//corr-author//different//zipcode"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//zipcode|//corr-author//different//zipcode"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//zipcode|//corr-author//delete//zipcode"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//zipcode|//corr-author//delete//zipcode"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//zipcode|//corr-author//insert//zipcode"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//zipcode|//corr-author//insert//zipcode"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//zipcode"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:when>

            <xsl:otherwise>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cty|//corr-author//different//cty"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cty|//corr-author//different//cty"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cty|//corr-author//delete//cty"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cty|//corr-author//delete//cty"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cty|//corr-author//insert//cty"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cty|//corr-author//insert//cty"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cty"/></xsl:otherwise>
</xsl:choose> <xsl:text> </xsl:text>  <xsl:choose>
<xsl:when test="//different//corr-author//zipcode|//corr-author//different//zipcode"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//zipcode|//corr-author//different//zipcode"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//zipcode|//corr-author//delete//zipcode"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//zipcode|//corr-author//delete//zipcode"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//zipcode|//corr-author//insert//zipcode"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//zipcode|//corr-author//insert//zipcode"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//zipcode"/></xsl:otherwise>
</xsl:choose></td></tr>
              <tr><td/><td><xsl:choose>
<xsl:when test="//different//corr-author//cny|//corr-author//different//cny"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//cny|//corr-author//different//cny"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//cny|//corr-author//delete//cny"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//cny|//corr-author//delete//cny"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//cny|//corr-author//insert//cny"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//cny|//corr-author//insert//cny"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//cny"/></xsl:otherwise>
</xsl:choose></td></tr>
            </xsl:otherwise>

          </xsl:choose>
          <tr><td>Phone:</td><td><xsl:choose>
<xsl:when test="//different//corr-author//tel|//corr-author//different//tel"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//tel|//corr-author//different//tel"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//tel|//corr-author//delete//tel"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//tel|//corr-author//delete//tel"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//tel|//corr-author//insert//tel"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//tel|//corr-author//insert//tel"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//tel"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>Fax:</td><td><xsl:choose>
<xsl:when test="//different//corr-author//fax|//corr-author//different//fax"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//fax|//corr-author//different//fax"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//fax|//corr-author//delete//fax"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//fax|//corr-author//delete//fax"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//fax|//corr-author//insert//fax"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//fax|//corr-author//insert//fax"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//fax"/></xsl:otherwise>
</xsl:choose></td></tr>
          <tr><td>E-mail:</td><td><xsl:choose>
<xsl:when test="//different//corr-author//ead|//corr-author//different//ead"><font color="red"><b><u><xsl:apply-templates select="//different//corr-author//ead|//corr-author//different//ead"/></u></b></font></xsl:when>
<xsl:when test="//delete//corr-author//ead|//corr-author//delete//ead"><font color="blue"><b><s><u><xsl:apply-templates select="//delete//corr-author//ead|//corr-author//delete//ead"/></u></s></b></font></xsl:when> 
<xsl:when test="//insert//corr-author//ead|//corr-author//insert//ead"><font color="green"><b><u><xsl:apply-templates select="//insert//corr-author//ead|//corr-author//insert//ead"/></u></b></font></xsl:when> 
<xsl:otherwise><xsl:apply-templates select="//corr-author//aff//ead"/></xsl:otherwise>
</xsl:choose></td></tr>
  </xsl:template>




  <!--################################################################################-->
  <!--This section is for formatting the item-remarks field-->

  <xsl:template match="item-remarks">
     <xsl:for-each select="item-remark">
     <tr valign="top">
  	 <td width="10%">Remark:</td>
       <td width="40%">        
       <xsl:choose>
<xsl:when test="diff/remark!=''"><td width="40%"><font color="red"><xsl:value-of select="diff/remark"/></font></td></xsl:when><xsl:otherwise><td width="40%"><xsl:value-of select="remark"/></td></xsl:otherwise>
</xsl:choose></td>
       <td width="10%">Response:</td>
       <td width="40%"> <xsl:choose>
<xsl:when test="different/response"><font color="red"><b><u><!--<xsl:number/>--> <xsl:value-of select="different/response"/></u></b></font></xsl:when>
<xsl:when test="delete/response"><font color="blue"><b><s><u><!--<xsl:number/>--> <xsl:value-of select="delete/response"/></u></s></b></font></xsl:when> 
<xsl:when test="insert/response"><font color="green"><b><u><!--<xsl:number/>--> <xsl:value-of select="insert/response"/></u></b></font></xsl:when> 
<xsl:otherwise><!--<xsl:number/>--> <xsl:value-of select="response"/></xsl:otherwise>
</xsl:choose></td>
     </tr>
     </xsl:for-each>
  </xsl:template>
  <!--################################################################################-->

  <!--This section is for formatting the issue-remarks field-->
  
  <xsl:template match="issue-remarks">
     <xsl:for-each select="issue-remark">
       <xsl:value-of select="."/><br/> <!-- Show all issue-remarks -->
       <tr valign="top">
					<td width="5%">Remark:</td>
					<xsl:choose>
						<xsl:when test="diff/remark!=''">
							<td width="40%">
								<font color="red">
									<xsl:value-of select="diff/remark"/>
								</font>
							</td>
						</xsl:when>
						<xsl:otherwise>
							<td width="40%">
								<xsl:value-of select="remark"/>
							</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>
				<tr valign="top">
					<td width="5%">Response:</td>
					<xsl:choose>
						<xsl:when test="diff/remark!=''">
							<td width="95%">
								<font color="blue">
									<xsl:value-of select="response"/>
								</font>
							</td>
						</xsl:when>
						<xsl:otherwise>
							<td width="95%">
								<xsl:value-of select="response"/>
							</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>

     </xsl:for-each>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting any date element so it returns as (eg) 12-Dec-2003 -->

  <xsl:template match="date">
  <xsl:if test="@day!=''">
    <xsl:value-of select="@day"/>-<xsl:if test="@month='01'">Jan-</xsl:if>
    <xsl:if test="@month='02'">Feb-</xsl:if>
    <xsl:if test="@month='03'">Mar-</xsl:if>
    <xsl:if test="@month='04'">Apr-</xsl:if>
    <xsl:if test="@month='05'">May-</xsl:if>
    <xsl:if test="@month='06'">Jun-</xsl:if>
    <xsl:if test="@month='07'">Jul-</xsl:if>
    <xsl:if test="@month='08'">Aug-</xsl:if>
    <xsl:if test="@month='09'">Sep-</xsl:if>
    <xsl:if test="@month='10'">Oct-</xsl:if>
    <xsl:if test="@month='11'">Nov-</xsl:if>
    <xsl:if test="@month='12'">Dec-</xsl:if>
    <xsl:value-of select="@yr"/>
    </xsl:if>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting any date element so it returns as (eg) 12-Dec-2003 -->

  <xsl:template match="time">
  <xsl:if test="@day!=''">
    <xsl:value-of select="@day"/>-<xsl:if test="@month='01'">Jan-</xsl:if>
    <xsl:if test="@month='02'">Feb-</xsl:if>
    <xsl:if test="@month='03'">Mar-</xsl:if>
    <xsl:if test="@month='04'">Apr-</xsl:if>
    <xsl:if test="@month='05'">May-</xsl:if>
    <xsl:if test="@month='06'">Jun-</xsl:if>
    <xsl:if test="@month='07'">Jul-</xsl:if>
    <xsl:if test="@month='08'">Aug-</xsl:if>
    <xsl:if test="@month='09'">Sep-</xsl:if>
    <xsl:if test="@month='10'">Oct-</xsl:if>
    <xsl:if test="@month='11'">Nov-</xsl:if>
    <xsl:if test="@month='12'">Dec-</xsl:if>
    <xsl:value-of select="@yr"/>
    </xsl:if>
  </xsl:template>
  <!--################################################################################-->
  <!--This section is for formatting the member-ids field-->

  <xsl:template match="subitem">
  Present
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the refers-to pii field-->

  <xsl:template match="refers-to-document/pii">
    <tr><td><xsl:value-of select="//refers-to-document[1]/pii"/></td></tr>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the refers-to-doi field-->

  <xsl:template match="refers-to-doi">
    <xsl:apply-templates select="doi"/>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the refers-to-doi/doi field-->

  <xsl:template match="refers-to-doi/doi">
    <td>Refers_to DOI:</td><td><xsl:value-of select="../doi"/></td>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the e-components field-->
<xsl:template match="e-components">
<table>
<tr>
<td><xsl:for-each select="//e-component-nr"><p><xsl:value-of select="."/></p></xsl:for-each></td>
<td><xsl:for-each select="//e-component-format"><p><xsl:value-of select="."/></p></xsl:for-each></td>
<td><xsl:for-each select="//e-component-remarks"><p><xsl:value-of select="."/></p></xsl:for-each></td>
</tr></table>
</xsl:template>

</xsl:stylesheet>