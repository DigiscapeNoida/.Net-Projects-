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
      

  <!--################################################################################-->
  <xsl:template match="/">
    <xsl:apply-templates select="orders"/>
  </xsl:template>

  <!--################################################################################-->
  <xsl:template match="orders">
    <xsl:for-each select="order">
      <html>
        <head>COVERSHEET</head>
      </html>
      <p/>

      <table width="100%">
        <table>
           <tr><td>Date:</td><td><xsl:apply-templates select="time"/></td></tr>
           <tr><td>PII:</td><td><xsl:value-of select="//pii"/></td></tr>
           <tr><td>Online Version:</td><td><xsl:value-of select="//online-version/@type"/></td></tr>
           <tr><xsl:apply-templates select="item-info/doi"/></tr>
           <tr><td>Version Nr:</td><td><xsl:value-of select="//version-no"></xsl:value-of></td></tr>
         	<xsl:if test="//embargo">
	           <tr><td>Embargo:</td><td><xsl:value-of select="//embargo"></xsl:value-of></td></tr>
	      </xsl:if>     
           <tr><td>For:</td><td><xsl:value-of select="prod-site"/></td></tr>
           <tr><td>Supplier:</td><td><xsl:value-of select="//exec-name"/></td></tr>
        </table>

        <tr><td colspan="2"><hr/></td></tr>

        <table width="100%">
        <tr>
          <td>
            <table>
              <tr><td>Item:</td><td ><xsl:value-of select="//jid"/> <xsl:value-of select="//aid"/></td></tr>
              
              <tr><td width="24%"># Item pages:</td><td width="25%"><xsl:value-of select="//no-mns-pages"/></td>
                    <td width="22%">Publication item :</td><td><xsl:value-of select="//pit"/></td></tr>
                    
              <tr><td># Graphics:</td><td><xsl:value-of select="//no-phys-figs"/></td>
                    <td>Sub Item PII:</td><td><xsl:apply-templates select="//subitem[1]"/></td></tr>
                    
              <tr valign="top"><td># B/W graphics:</td><td><xsl:value-of select="//no-bw-figs"/></td>

                    <td>Refers to PII:</td>
                    <td><xsl:value-of select="//refers-to-document[1]/pii"/>
                    <xsl:if test="//refers-to-document[2]/pii!=''">, <br><xsl:value-of select="//refers-to-document[2]/pii"/></br></xsl:if>
                    <xsl:if test="//refers-to-document[3]/pii!=''"> ...</xsl:if>
                    </td>
                    </tr>
                    
              <tr valign="top"><td># Web colour graphics:</td><td><xsl:value-of select="//no-web-colour-figs"/>
<xsl:for-each select="//figure">
<xsl:choose>
<xsl:when test="figure-type[text()='COLOUR']">
<font><b>[<xsl:value-of select="figure-nr"/>]</b></font>
</xsl:when>
</xsl:choose>
</xsl:for-each>
</td>

                    <td>Refers to DOI:</td>
                    <td><xsl:value-of select="//refers-to-document[1]/doi"/>
                    <xsl:if test="//refers-to-document[2]/doi!=''">, <br><xsl:value-of select="//refers-to-document[2]/doi"/></br></xsl:if>
                    <xsl:if test="//refers-to-document[3]/doi!=''"> ...</xsl:if>
                    </td>
                    </tr>
              
              <tr><td># Printed colour graphics:</td><td><xsl:value-of select="//no-colour-figs"/>
<xsl:for-each select="//figure">
<xsl:choose>
<xsl:when test="figure-type[text()='COLOUR-IN-PRINT']">
<font><b>[<xsl:value-of select="figure-nr"/>]</b></font>
</xsl:when>
</xsl:choose>
</xsl:for-each>
</td>
                    <td>E Submission Item Nr:</td><td><xsl:value-of select="//e-submission-item-nr"/></td></tr> 
                    
               <tr><td>Copyright status:</td><td><xsl:value-of select="//copyright-status"/></td>
                     <td>EO Item Nr:</td><td><xsl:value-of select="//eo-item-nr"/></td></tr>                      
                     
               <tr><td>Copyright received date:</td> <td><xsl:for-each select="//copyright-recd-date"><xsl:apply-templates select="date"/></xsl:for-each></td>      
                     <td>Editor:</td><td><xsl:value-of select="//editor"/></td></tr>                      
                     
               <tr><td>Production type as sent:</td><td><xsl:value-of select="//prd-type-as-sent"/></td>              
                     <td>Date received:</td><td><xsl:for-each select="//received-date"><xsl:apply-templates select="date"/></xsl:for-each></td></tr>                       

                     
               <tr><td>Corrections:</td><td><xsl:value-of select="//corrections/@type"></xsl:value-of></td>
                      <td>Date revised:</td><td><xsl:for-each select="//revised-date"><xsl:apply-templates select="date"/></xsl:for-each></td></tr>                      
                      
               <tr><td></td><td></td> <td>Date accepted:</td>
                      <td><xsl:for-each select="//accept-date"><xsl:apply-templates select="date"/></xsl:for-each></td></tr>                       
            </table>
          </td>
        </tr>
</table>

      <xsl:apply-templates select="//e-components"/>

      <tr><td colspan="2"><hr/></td></tr>
	<table>
          <tr><td>Item title:</td><td><xsl:value-of select="//item-title"/></td></tr>
          <tr><td>DOCHead:</td><td><xsl:value-of select="//dochead"/></td></tr>
          <tr><td>Item group:</td><td><xsl:value-of select="//item-group"/></td></tr>
          <tr><td>Item group description:</td><td><xsl:value-of select="//item-group-description"/></td></tr>
          <tr><td>Section:</td><td><xsl:value-of select="//section"/></td></tr>
          <tr><td>First author:</td><td><xsl:value-of select="//first-author"/></td></tr>
          <tr><td>Corr. author:</td><td><xsl:apply-templates select="//corr-author"/></td></tr>
	</table>
	<hr/>
	<table width="100%">
          <tr><td valign="top">Item Remarks :</td><td valign="top"><xsl:apply-templates select="//item-remarks"/></td></tr>
          <tr><td valign="top">Issue Remarks :</td><td valign="top"><xsl:apply-templates select="//issue-remarks"/></td></tr>
	</table>
      </table>
    </xsl:for-each>
  </xsl:template>
  
  <!--################################################################################-->
  <!--This section is for formatting the corr-author name-->

  <xsl:template match="corr-author">
    <xsl:value-of select="degree"/> <xsl:text> </xsl:text>  <xsl:value-of select="fnm"/>  <xsl:text> </xsl:text> <xsl:value-of select="snm"/>
          <tr><td><xsl:apply-templates select="./aff"/></td></tr>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the corr-author address-->

  <xsl:template match="aff">
          <tr><td/><td><xsl:value-of select="organization"/></td></tr>
          <tr><td/><td><xsl:value-of select="inst-contd"/></td></tr>
          <tr><td/><td><xsl:value-of select="institute"/></td></tr>
          <tr><td/><td><xsl:value-of select="address"/></td></tr>
          <tr><td/><td><xsl:value-of select="address-contd"/></td></tr>
          <xsl:choose>
            <xsl:when test="zipcode/@zipcode-pos='NONE'">
              <tr><td/><td><xsl:value-of select="cty"/></td></tr>
              <tr><td/><td><xsl:value-of select="cny"/></td></tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='BEFORECTY'">
              <tr><td/><td><xsl:value-of select="zipcode"/> <xsl:text> </xsl:text>  <xsl:value-of select="cty"/></td></tr>
              <tr><td/><td><xsl:value-of select="cny"/></td></tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='AFTERCTY'">
              <tr><td/><td><xsl:value-of select="cty"/> <xsl:text> </xsl:text> <xsl:value-of select="zipcode"/></td></tr>
              <tr><td/><td><xsl:value-of select="cny"/></td></tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='BEFORECNY'">
              <tr><td/><td><xsl:value-of select="cty"/></td></tr>
              <tr><td/><td><xsl:value-of select="zipcode"/> <xsl:text> </xsl:text>  <xsl:value-of select="cny"/></td></tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='AFTERCNY'">
              <tr><td/><td><xsl:value-of select="cty"/></td></tr>
              <tr><td/><td><xsl:value-of select="cny"/> <xsl:text> </xsl:text>  <xsl:value-of select="zipcode"/></td></tr>
            </xsl:when>
            <xsl:otherwise>
              <tr><td/><td><xsl:value-of select="cty"/> <xsl:text> </xsl:text>  <xsl:value-of select="zipcode"/></td></tr>
              <tr><td/><td><xsl:value-of select="cny"/></td></tr>
            </xsl:otherwise>
          </xsl:choose>
          <tr><td>Phone:</td><td><xsl:value-of select="//corr-author/aff/tel"/></td></tr>
          <tr><td>Fax:</td><td><xsl:value-of select="//corr-author/aff/fax"/></td></tr>
          <tr><td>E-mail:</td><td><xsl:value-of select="//corr-author/aff/ead"/></td></tr>
  </xsl:template>

  <!--################################################################################-->
  <!--This section is for formatting the item-remarks field-->

  <xsl:template match="item-remarks">
     <xsl:for-each select="item-remark">
     <tr valign="top">
	 <td width="10%">Remark:</td>
       
       <td width="40%">
       <xsl:if test="remark-type">
       	<b>[<xsl:value-of select="remark-type"/>]&#32;&#32;</b> 
       </xsl:if>
       <xsl:value-of select="remark"/>
       </td>
       
       <td width="10%">Response:</td>
       <td width="40%"><xsl:value-of select="response"/></td>
     </tr>
     </xsl:for-each>
  </xsl:template>

  <!--################################################################################-->

  <!--This section is for formatting the issue-remarks field-->
  
  <xsl:template match="issue-remarks">
     <xsl:for-each select="issue-remark">
       <xsl:value-of select="."/><br/> <!-- Show all issue-remarks -->
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
  <hr/>E-COMPONENTS:
    <table width="100%">
      <tr><td colspan="5"></td></tr>
      <xsl:for-each select="e-component">
            <tr>
                <td width="15%"><xsl:value-of select="e-component-nr"/></td>
                <td width="15%"><xsl:value-of select="e-component-format"/></td>
                <td width="70%"><xsl:value-of select="e-component-remarks"/></td>
            </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>