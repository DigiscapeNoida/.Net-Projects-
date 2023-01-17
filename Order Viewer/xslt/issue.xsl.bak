<?xml version='1.0' encoding='utf-8'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--
    version 1.5  26 May 2009
    Copyright (c) 2003-2008 Elsevier B.V.
    $Id: issue.xsl,v 1.18 2009/05/26 11:18:47 jmigchie Exp $
-->
<!--
    v1.5  26 May 2009  [PTSIII release 2009.1]
    No changes from v1.4

    v1.4  11 November 2008  [PTSIII release 2008.2, EWII release 2008.3]
    Changes with respect to v1.3.1
    * JMi: Displaying element opco
    
    v1.3.1  6 May 2008
    * Intermediate release for suppliers to solve empty prefix problem

    v1.3  11 April 2008  [PTSIII release 2008.0, EWII release 2008.1]
    Changes with respect to v1.2
    * JMi: Added display of no-issues-paid in case or non-ce rows
    * JMi: Added heading in case of stage SEND-ISSUE or SEND-OFF-SYSTEM-ISSUE
    * JMi: Displaying new element e-suite
    
    v1.2  10 December 2007  [PTSIII release 2007.3]
    Changes with respect to v1.1.3
    * JMi: Added support for element suffix
    * JMi: Displaying element remark-type
    * JMi: Displaying new element binding-type below cover-finishing
    
    v1.1.3  31 July 2007  [PTSIII release 2007.2]
    Changes with respect to v1.1.2
    * JMi: Added template put-offprint-payment (CR 1108-13)
    * JMi: Added aid and pii for advert rows (30 August 2007)
    
    v1.1.2  15 December 2006  [PTSIII release 2006.3]
    Changes with respect to v1.1.1
    * JMi: Added display for rows of type advert (based on copy of the ce part)

    v1.1.1  27 October 2006  [PTSIII release 2006.3]
    Changes with respect to v1.0
    * JMi: Added template for executor[@type='PSP'] (PTS 2006.3, CR 1401, PTSIII Order DTD v1.20)
    * JMi: Adapted for changed element 'item-remark' in case of s (PTS 2006.2)
    * JMi: Adapted for step SEND-OFFPRINTS

    v1.0  27 July 2006  [PTSIII release 2006.2]
    Changes with respect to previous version (which was owned by the PTSII developers and was part of PTSIII 2006.1)
    * WVr (Wim de Vries 08-03-2006, 04-05-2006)
      - added pretty print view of the xml code at the end of the html doc
      - put the html open and close tags at the correct position
      - replaced <p/> with <p></p> (the first is invalid SGML/HTML4.01)
      - moved PCDATA from head-element to an H3 element (head may only contain elements, not PCDATA)
      -  added some script/function in head to un-view the pretty print of the xml, when user wants to print
      - added font 'Arial Unicode MS', currently the best to view all kind of special characters
    * Adapted for stage Q300 (hardcoded in several places)
    * Show elements 'paper-type-interior', 'paper-type-cover' and 'cover-finishing', below 'total pages'
    * Adapted for rowtypes h3 and h4 (underlined italic and italic, respectively)
    * Show elements 'typeset-model', 'righthand-start', 'issue-weight', 'spine-width', 'trimmed-size', 'head-margin', 'back-margin', below 'cover-finishing'

-->

    <!--################################################################################-->
    <xsl:template match="/">
        <!-- root -->
        <xsl:apply-templates select="orders"/>
    </xsl:template>

    <!--################################################################################-->
    <xsl:template match="orders">
        <html>
            <head>
                <script>
function expColl(dF,statusObj,object)
 {
 if (statusObj.status == 1)
  { //1 is open, so close now
  dF.appendChild(object.parentNode.removeChild(object.parentNode.lastChild));
  object.removeChild(object.lastChild); //remove minus !!!second takes away plus of first!?
  object.appendChild(document.createTextNode('+')); //put + in its place
  statusObj.status = 0;
  }
 else
  { //open
  object.removeChild(object.lastChild); //remove plus
  object.appendChild(document.createTextNode('-')); //put minus in its place
  object.parentNode.appendChild(dF);
  statusObj.status = 1;
  }
 }
var part0 = document.createDocumentFragment();
var expanded0 = {status:1};
</script>
            </head>
            <body style="font-family:'Arial Unicode MS'" onload="expColl(part0,expanded0,document.getElementById('xmlcode'))">
                <xsl:for-each select="order">
                    <!-- Insert header -->
                    <h3>
                        <xsl:if test="stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">ISSUE PAGINATION SHEET</xsl:if>
                        <xsl:if test="stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">OFFPRINTS SHEET</xsl:if>
                        <xsl:if test="stage[@step='SEND-ISSUE']">SEND ISSUE</xsl:if>
                        <xsl:if test="stage[@step='SEND-OFF-SYSTEM-ISSUE']">SEND OFF-SYSTEM ISSUE</xsl:if>
                    </h3>
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td valign="top">PURCHASE ORDER NUMBER</td>
                                        <td>: <xsl:value-of select="po-number"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Journal</td>
                                        <td>: <xsl:value-of select="//jid"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Journal title</td>
                                        <td>: <xsl:value-of select="//journal-title"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Volume/Issue</td>
                                        <td>: <!-- Format the Volume/ Issue values -->
                                            <xsl:value-of select="//vol-from"/>
                                            <xsl:apply-templates select="//vol-to"/>
                                            <xsl:apply-templates select="//iss-from"/>
                                            <xsl:apply-templates select="//iss-to"/>
                                            <xsl:value-of select="//supp"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Journal no.</td>
                                        <td>: <xsl:value-of select="//journal-no"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">ISSN</td>
                                        <td>: <xsl:value-of select="//issn"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Production site</td>
                                        <td>: <xsl:value-of select="prod-site"/></td>
                                    </tr>
                                    <tr>
                                        <td valing="top">Opco</td>
                                        <td>: <xsl:value-of select="opco"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <xsl:apply-templates select="executor[@type='PSP']"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <xsl:apply-templates select="executor[@type='ES']"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Cover date</td>
                                        <td>: <xsl:value-of select="//cover-date-printed"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">EFFECT cover date</td>
                                        <td>: <xsl:value-of select="//effect-cover-date"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Due date</td>
                                        <td>: <xsl:for-each select="//due-date">
                                                <xsl:apply-templates select="date"/>
                                            </xsl:for-each></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Embargo expiry time</td>
                                        <td>:<xsl:value-of select="//general-info/embargo"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Version Nr</td>
                                        <td>: <xsl:value-of select="//general-info/version-no"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Issue Production type</td>
                                        <td>: <xsl:value-of select="//general-info/issue-production-type"/></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Issue Proof via</td>
                                        <td>: <xsl:value-of select="//general-info/corrections/@type"/></td>
                                    </tr>
                                </table>
                            </td>

                            <td valign="top">
                                <xsl:apply-templates select="issue-info/general-info"/>
                            </td>
                            <td valign="top">
                                <xsl:if test="stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
                                    <xsl:apply-templates select="executor[@type='TYPESETTER']"/>
                                </xsl:if>
                                <xsl:if test="stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                                    <xsl:apply-templates select="executor[@type='PRINTER']"/>
                                    <xsl:apply-templates select="executor[@type='BINDER']"/>
                                    <xsl:apply-templates select="executor[@type='TYPESETTER']"/>
                                </xsl:if>
                            </td>
   				<td valign="top">
				  Printer:<br/> <xsl:value-of select="executor[@type='BINDER']/exec-name"/>
				</td>
                        </tr>
                    </table>
                    <p/>
                    <p/>

                    <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                        <xsl:if test="//hold-until-date/date/@month!=''">
                            <tr>
                                <td>
                                    <font size="4">
                                        <b>
                                            <xsl:for-each select="//hold-until-date"> Held Issue: DO NOT DESPATCH UNTIL <xsl:apply-templates select="date"/>
                                            </xsl:for-each>
                                        </b>
                                    </font>
                                    <br/>
                                </td>
                            </tr>
                        </xsl:if>
                        <br/>
                    </xsl:if>

                    <xsl:apply-templates select="issue-info"/>
                </xsl:for-each>
                <!--<div id="" style="background-color:#D7FFCF; position: relative; left:0.3%; border-style:solid; border-width:1; padding:5">
                    <span style="font-size:70%">XML code</span>
                    <br/>
                    <span id="xmlcode" onclick="expColl(part0,expanded0,this)" style="color:red; font-weight:900; font-size:150%; font-family:courier" onmouseover="this.style.cursor='hand'">-</span>
                    <xsl:apply-templates select="/" mode="xml-source"/>
                </div>-->
            </body>
        </html>
    </xsl:template>

    <!-- create pretty print -->
    <xsl:template match="*" mode="xml-source">
        <xsl:variable name="anc" select="count(ancestor::*) div 2"/>
        <div>
            <xsl:attribute name="style">
                <xsl:value-of select="concat('position: relative; margin-left:', $anc ,'%;', 'overflow: scroll;')"/>
            </xsl:attribute>
            <span style="color:red">
                <xsl:text>&lt;</xsl:text>
                <xsl:value-of select="name()"/>
                <xsl:for-each select="@*">
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="name()"/>
                    <xsl:text>="</xsl:text>
                    <xsl:value-of select="."/>
                    <xsl:text>"</xsl:text>
                </xsl:for-each>
                <xsl:text>&gt;</xsl:text>
            </span>
            <xsl:apply-templates mode="xml-source"/>
            <span style="color:red">
                <xsl:text>&lt;/</xsl:text>
                <xsl:value-of select="name()"/>
                <xsl:text>&gt;</xsl:text>
            </span>
        </div>
    </xsl:template>



    <!--################################################################################-->
    <!--This section is for formatting the Volume/ Issue field-->

    <xsl:template match="//vol-to"> -<xsl:value-of select="//vol-to"/>
    </xsl:template>

    <xsl:template match="//iss-from"> /<xsl:value-of select="//iss-from"/>
    </xsl:template>

    <xsl:template match="//iss-to"> -<xsl:value-of select="//iss-to"/>
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

    <!--################################################################################-->
    <!--This section is for the executor information -->

    <xsl:template match="executor"/>
    <xsl:template match="executor[@type='ES']">
        <tr>
            <td>Contact person</td>
            <td>: <xsl:value-of select="exec-name"/></td>
        </tr>
        <tr>
            <td>Phone no.</td>
            <td>: <xsl:value-of select="aff/tel"/></td>
        </tr>
    </xsl:template>
    <xsl:template match="executor[@type='PSP']">
        <tr>
            <td>Local Supplier Manager</td>
            <td>: <xsl:value-of select="exec-name"/></td>
        </tr>
        <tr>
            <td>Phone no.</td>
            <td>: <xsl:value-of select="aff/tel"/></td>
        </tr>
    </xsl:template>
    <xsl:template match="executor[@type='TYPESETTER']">
        <table>
            <tr>
                <td valign="top">Typesetter:</td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/organization"/>
                </td>
            </tr>
            <tr>
                <td valign="top">attn. <xsl:value-of select="exec-name"/></td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/address"/>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/address-contd"/>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/cty"/>
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="aff/zipcode"/>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/cny"/>
                </td>
            </tr>
        </table>
    </xsl:template>
    <xsl:template match="executor[@type='PRINTER']">
        <table>
            <tr>
                <td valign="top">Printer</td>
                <td valign="top">:</td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/organization"/>
                </td>
            </tr>
            <tr>
                <td valign="top">attn. <xsl:value-of select="exec-name"/></td>
            </tr>
        </table>
    </xsl:template>
    <xsl:template match="executor[@type='BINDER']">
        <table>
            <tr>
                <td valign="top">Binder</td>
                <td valign="top">:</td>
            </tr>
            <tr>
                <td valign="top">
                    <xsl:value-of select="aff/organization"/>
                </td>
            </tr>
            <tr>
                <td valign="top">attn. <xsl:value-of select="exec-name"/></td>
            </tr>
        </table>
    </xsl:template>

    <!--################################################################################-->

    <xsl:template match="issue-info">
        <xsl:apply-templates select="issue-content"/>
        <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
            <tr>
                <td colspan="14">
                    <hr/>
                </td>
            </tr>
        </xsl:if>
        <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
            <tr>
                <td colspan="15">
                    <hr/>
                </td>
            </tr>
        </xsl:if> REMARKS:<p/>
        <xsl:if test="//buffer-status/@status='yes'">
            <tr>
                <td valign="top">
                    <font size="4">
                        <b>Buffer issue: do not despatch to the warehouse until instructed by the Production Site.</b>
                    </font>
                    <br/>
                </td>
            </tr>
        </xsl:if>
        <xsl:apply-templates select="issue-remarks"/>
        <p/>
        <tr>
            <td colspan="20">
                <xsl:apply-templates select="//special-issue"/>
            </td>
        </tr>
        <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
            <tr>
                <td colspan="14">
                    <hr/>
                </td>
            </tr>
        </xsl:if>
        <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
            <tr>
                <td colspan="15">
                    <hr/>
                </td>
            </tr>
        </xsl:if> ADVERT DETAILS:<p/>
        <xsl:for-each select="issue-remarks/issue-remark[remark-type='Advert details' or remark-type='ADV']">
            <xsl:value-of select="."/>
            <br/>
            <p/>
        </xsl:for-each>
        <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
            <tr>
                <td colspan="14">
                    <hr/>
                </td>
            </tr>
        </xsl:if>
        <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
            <tr>
                <td colspan="15">
                    <hr/>
                </td>
            </tr>
        </xsl:if>
    </xsl:template>

    <!--################################################################################-->
    <xsl:template match="issue-remarks">
        <table width="100%">
            <xsl:for-each select="issue-remark[remark-type != 'ADV' and remark-type != 'Advert details']">
                <tr valign="top">
                    <td width="5%">Remark:</td>
                    <td width="95%">
                        <b>(<xsl:value-of select="remark-type"/>)</b>
                        <xsl:text> </xsl:text>
                        <xsl:choose>
						<xsl:when test="diff/remark!=''">

								<font color="red">
									<xsl:value-of select="diff/remark"/>
								</font>

						</xsl:when>
						<xsl:otherwise>
								<xsl:value-of select="remark"/>
						</xsl:otherwise>
					</xsl:choose>
                    </td>
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
        </table>
    </xsl:template>
    <!--################################################################################-->
    <xsl:template match="general-info">

        <xsl:variable name="extra" select="//row[prefix='EX']"/>
        <xsl:variable name="insert" select="//row[prefix='IN']"/>
        <xsl:variable name="interior" select="//row[string-length(prefix)&lt;=1]"/>
        <xsl:variable name="interior-ce" select="$interior[@type='ce' or @type='blank']"/>
        <xsl:variable name="interior-suffix" select="//row[string-length(suffix)&lt;=1]"/><!-- JM -->

        <!-- BHW -->
        <!-- and not(@type='blank') -->


        <xsl:variable name="prefix-null" select="$interior[(prefix=' ' or prefix='')]"/>
        <xsl:variable name="prefix-a" select="$interior[prefix='A']"/>
        <xsl:variable name="prefix-b" select="$interior[prefix='C']"/>
        <xsl:variable name="prefix-c" select="$interior[prefix='B']"/>
        <xsl:variable name="prefix-d" select="$interior[prefix='D']"/>
        <xsl:variable name="prefix-e" select="$interior[prefix='E']"/>
        <xsl:variable name="prefix-f" select="$interior[prefix='F']"/>
        <xsl:variable name="prefix-g" select="$interior[prefix='G']"/>
        <xsl:variable name="prefix-h" select="$interior[prefix='H']"/>
        <xsl:variable name="prefix-i" select="$interior[prefix='I']"/>
        <xsl:variable name="prefix-j" select="$interior[prefix='J']"/>
        <xsl:variable name="prefix-k" select="$interior[prefix='K']"/>
        <xsl:variable name="prefix-l" select="$interior[prefix='L']"/>
        <xsl:variable name="prefix-m" select="$interior[prefix='M']"/>
        <xsl:variable name="prefix-n" select="$interior[prefix='N']"/>
        <xsl:variable name="prefix-o" select="$interior[prefix='O']"/>
        <xsl:variable name="prefix-p" select="$interior[prefix='P']"/>
        <xsl:variable name="prefix-q" select="$interior[prefix='Q']"/>
        <xsl:variable name="prefix-r" select="$interior[prefix='R']"/>
        <xsl:variable name="prefix-s" select="$interior[prefix='S']"/>
        <xsl:variable name="prefix-t" select="$interior[prefix='T']"/>
        <xsl:variable name="prefix-u" select="$interior[prefix='U']"/>
        <xsl:variable name="prefix-v" select="$interior[prefix='V']"/>
        <xsl:variable name="prefix-w" select="$interior[prefix='W']"/>
        <xsl:variable name="prefix-x" select="$interior[prefix='X']"/>
        <xsl:variable name="prefix-y" select="$interior[prefix='Y']"/>
        <xsl:variable name="prefix-z" select="$interior[prefix='Z']"/>
        <xsl:variable name="prefix-ele" select="$interior[prefix='e']"/>

        <xsl:variable name="suffix-null" select="$interior-suffix[(suffix=' ' or suffix='')]"/><!-- JM -->
        <xsl:variable name="suffix-a" select="$interior-suffix[suffix='A']"/>
        <xsl:variable name="suffix-b" select="$interior-suffix[suffix='C']"/>
        <xsl:variable name="suffix-c" select="$interior-suffix[suffix='B']"/>
        <xsl:variable name="suffix-d" select="$interior-suffix[suffix='D']"/>
        <xsl:variable name="suffix-e" select="$interior-suffix[suffix='E']"/>
        <xsl:variable name="suffix-f" select="$interior-suffix[suffix='F']"/>
        <xsl:variable name="suffix-g" select="$interior-suffix[suffix='G']"/>
        <xsl:variable name="suffix-h" select="$interior-suffix[suffix='H']"/>
        <xsl:variable name="suffix-i" select="$interior-suffix[suffix='I']"/>
        <xsl:variable name="suffix-j" select="$interior-suffix[suffix='J']"/>
        <xsl:variable name="suffix-k" select="$interior-suffix[suffix='K']"/>
        <xsl:variable name="suffix-l" select="$interior-suffix[suffix='L']"/>
        <xsl:variable name="suffix-m" select="$interior-suffix[suffix='M']"/>
        <xsl:variable name="suffix-n" select="$interior-suffix[suffix='N']"/>
        <xsl:variable name="suffix-o" select="$interior-suffix[suffix='O']"/>
        <xsl:variable name="suffix-p" select="$interior-suffix[suffix='P']"/>
        <xsl:variable name="suffix-q" select="$interior-suffix[suffix='Q']"/>
        <xsl:variable name="suffix-r" select="$interior-suffix[suffix='R']"/>
        <xsl:variable name="suffix-s" select="$interior-suffix[suffix='S']"/>
        <xsl:variable name="suffix-t" select="$interior-suffix[suffix='T']"/>
        <xsl:variable name="suffix-u" select="$interior-suffix[suffix='U']"/>
        <xsl:variable name="suffix-v" select="$interior-suffix[suffix='V']"/>
        <xsl:variable name="suffix-w" select="$interior-suffix[suffix='W']"/>
        <xsl:variable name="suffix-x" select="$interior-suffix[suffix='X']"/>
        <xsl:variable name="suffix-y" select="$interior-suffix[suffix='Y']"/>
        <xsl:variable name="suffix-z" select="$interior-suffix[suffix='Z']"/>
        <xsl:variable name="suffix-ele" select="$interior-suffix[suffix='e']"/>
        
        <td valign="top">
            <table>
                <tr>
                    <td valign="top">Special Issue ID : <xsl:value-of select="special-issue/special-issue-id"/></td>
                </tr>
                <tr>
                    <td valign="top">Prelims : <xsl:value-of select="no-pages-prelims"/>
                        <xsl:if test="no-pages-prelims &gt;0"> ( <xsl:value-of select="page-ranges/page-range[@type='PRELIM']/first-page"/> - <xsl:value-of select="page-ranges/page-range[@type='PRELIM']/last-page"/> ) </xsl:if>
                    </td>
                </tr>

                <!-- This works, but doesn't bring stuff back in the right order -->
                <tr>
                    <td>Interior : 
                        <xsl:value-of select="//no-pages-interior"/> ( 
                        <xsl:for-each select="$prefix-null">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-a">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-b">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-c">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-d">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-e">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-f">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-g">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-h">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-i">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-j">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-k">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-l">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-m">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-n">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-o">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-p">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-q">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-r">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-s">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-t">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-u">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-v">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-w">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-x">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-y">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-z">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$prefix-ele">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-null">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-a"><!-- JM -->
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-b">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-c">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-d">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-e">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-f">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-g">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-h">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-i">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-j">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-k">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-l">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-m">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-n">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-o">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-p">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-q">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-r">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-s">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-t">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-u">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-v">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-w">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-x">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-y">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-z">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        <xsl:for-each select="$suffix-ele">
                            <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                            <xsl:if test="position()=last()">
                                <xsl:value-of select="concat(./page-to,'; ')"/>
                            </xsl:if>
                        </xsl:for-each>
                        )</td>
                </tr>

                <tr>
                    <td>Extra : <xsl:value-of select="no-pages-extra"/>
                        <xsl:if test="no-pages-extra &gt;0"> (<xsl:for-each select="$extra">
                                <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                                <xsl:if test="position()=last()">
                                    <xsl:value-of select="./page-to"/>
                                </xsl:if>
                            </xsl:for-each>) </xsl:if>
                    </td>
                </tr>

                <tr>
                    <td>Insert : <xsl:value-of select="no-pages-insert"/>
                        <xsl:if test="no-pages-insert &gt;0"> (<xsl:for-each select="$insert">
                                <xsl:if test="position()=1"><xsl:value-of select="./page-from"/>-</xsl:if>
                                <xsl:if test="position()=last()">
                                    <xsl:value-of select="./page-to"/>
                                </xsl:if>
                            </xsl:for-each>) </xsl:if>
                    </td>
                </tr>

                <tr>
                    <td valign="top">Backmatter : <xsl:value-of select="no-pages-bm"/>
                        <xsl:if test="no-pages-bm &gt;0"> ( <xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/first-page"/> - <xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/last-page"/> ) </xsl:if>
                    </td>
                </tr>
                <tr>
                    <td valign="top">#Printed pages: <xsl:value-of select="no-pages-print"/></td>
                </tr>
                <tr>
                    <td valign="top">#Web PDF pages : <xsl:value-of select="no-pages-web"/></td>
                </tr>
                <tr>
                    <td valign="top">Total pages : <xsl:value-of select="no-pages-total"/></td>
                </tr>
                <tr>
                    <td valign="top">Paper type interior : <xsl:value-of select="paper-type-interior"/></td>
                </tr>
                <tr>
                    <td valign="top">Paper type cover : <xsl:value-of select="paper-type-cover"/></td>
                </tr>
                <tr>
                    <td valign="top">Cover finishing : <xsl:value-of select="cover-finishing"/></td>
                </tr>
                <tr>
                    <td valign="top">Binding type: <xsl:value-of select="binding-type"/></td>
                </tr>
                <tr>
                    <td valign="top">Typeset model : <xsl:value-of select="//typeset-model"/></td>
                </tr>
                <tr>
                    <td valign="top">Righthand start : <xsl:value-of select="//righthand-start"/></td>
                </tr>
                <tr>
                    <td valign="top">Issue weight : <xsl:value-of select="//issue-weight"/></td>
                </tr>
                <tr>
                    <td valign="top">Spine width : <xsl:value-of select="//spine-width"/></td>
                </tr>
                <tr>
                    <td valign="top">Trimmed size : <xsl:value-of select="//trimmed-size"/></td>
                </tr>
                <tr>
                    <td valign="top">Head margin : <xsl:value-of select="//head-margin"/></td>
                </tr>
                <tr>
                    <td valign="top">Back margin : <xsl:value-of select="//back-margin"/></td>
                </tr>
                <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                    <!--tr-->
                    <!--td valign="top">Free Issues : <xsl:value-of select="//no-issues-free" /></td-->
                    <!--/tr-->
                </xsl:if>

            </table>
        </td>
    </xsl:template>

    <!--################################################################################-->
    <!-- This section is for displaying all rows on the screen for the issue OR offprint -->
    <xsl:template match="issue-content">
        <table style="font-size:9pt" width="100%">
            <!--    <table style="font-size:9pt; font-family:Courier" width="100%"> -->
            <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
                <tr>
                    <th valign="top" align="left">Ms. id</th>
                    <th valign="top" align="left" width="150">DOI number</th>
                    <th valign="top" align="left">Ver. no</th>
                    <th valign="top" align="left">Title</th>
                    <th valign="top" align="left">Cop typ</th>
                    <th valign="top" align="left">Corr. author</th>
                    <th valign="top" align="left">Ed. office no.</th>
                    <th valign="top" align="left">PIT</th>
                    <th valign="top" align="left">Prd typ</th>
                    <th valign="top" align="left">Pg from</th>
                    <th valign="top" align="left">Pg to</th>
                    <th valign="top" align="left">#pgs</th>
                    <th valign="top" align="left">Web Pg Fr</th>
                    <th valign="top" align="left">Web Pg To</th>
                    <th valign="top" align="left">E-suite</th>
                    <th valign="top" align="left">Clr</th>
                    <th valign="top" align="left">Offpr paym recd</th>
                    <th valign="top" align="left">Print Col Graph Nrs</th>
                    <th valign="top" align="left">Onl Pub Date</th>
                    <th valign="top" align="left">Copyrt Rec Date</th>
                    <th valign="top" align="left">Remarks</th>
                </tr>
            </xsl:if>
            <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                <tr>
                    <th valign="top" align="left">MS. id</th>
                    <th valign="top" align="left" width="150">PII number</th>
                    <th valign="top" align="left">Corr. author</th>
                    <th valign="top" align="left">PIT</th>
                    <th valign="top" align="left">Prd typ</th>
                    <th valign="top" align="left">Pg from</th>
                    <th valign="top" align="left">Pg to</th>
                    <th valign="top" align="left">#Pgs</th>
                    <th valign="top" align="left">#offp paid</th>
                    <th valign="top" align="left">#offp free</th>
                    <th valign="top" align="left">#offp total</th>
                    <th valign="top" align="left">#Free iss</th>
                    <th valign="top" align="left">#paid iss</th>
                    <th valign="top" align="left">Cov</th>
                    <th valign="top" align="left">E-suite</th>
                    <th valign="top" align="left">Clr</th>
                    <th valign="top" align="left">Offpr paym recd</th>
                    <th valign="top" align="left">Pg charge</th>
                </tr>
            </xsl:if>
            <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
                <tr>
                    <td colspan="21">
                        <hr/>
                    </td>
                </tr>
            </xsl:if>
            <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                <tr>
                    <td colspan="18">
                        <hr/>
                    </td>
                </tr>
            </xsl:if>

            <xsl:for-each select="row">
                <!--Issue sheet-->
                <!--=================================================================-->
                <xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' or @step='Q300']">
                    <xsl:if test="@type='ce'">
                        <tr>
                            <td valign="top">
                                <xsl:value-of select="aid"/>
                            </td>
                            <td valign="top" width="150">
                                <xsl:value-of select="doi"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="version-no"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="item-title"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="copyright-status"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="corr-author/degree"/>
                                <xsl:text> </xsl:text>
                                <xsl:value-of select="corr-author/fnm"/>
                                <xsl:text> </xsl:text>
                                <xsl:value-of select="corr-author/snm"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="eo-item-nr"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pit"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="prd-type"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="first-e-page"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="last-e-page"/>
                            </td>
                            <!-- <td valign="top"><xsl:value-of select="no-issues-free"/></td>-->
                            <td valign="top">
                                <xsl:value-of select="e-suite"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-colour-figs"/>
                            </td>
                            <td valign="top">
                                <xsl:call-template name="put-offprint-payment"/>
                            </td>
                            <td valign="top">
                                <!-- If xml built via button on form then colour-fig-nr-print is a level lower -->
                                <xsl:choose>
                                    <xsl:when test="colour-fig-nr-print/colour-fig-nr-print_ROW[@num='1']">
                                        <xsl:for-each select="colour-fig-nr-print/colour-fig-nr-print_ROW">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() !=last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:for-each select="colour-fig-nr-print">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() !=last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </td>

                            <td width="8%" valign="top">
                                <xsl:for-each select="online-publ-date">
                                    <xsl:apply-templates select="date"/>
                                </xsl:for-each>
                            </td>

                            <td valign="top">
                                <xsl:for-each select="copyright-recd-date">
                                    <xsl:apply-templates select="date"/>
                                </xsl:for-each>
                            </td>

                            <td valign="top">
                                <xsl:value-of select="remark"/>
                            </td>
                        </tr>
                    </xsl:if>

                    <xsl:if test="@type='advert'">
                        <!-- Added. Originally added by RvF. -->
                        <tr>
                            <td valign="top">
                                <xsl:value-of select="aid"/>
                            </td>
                            <td valign="top" width="150">
                                <xsl:value-of select="doi"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="version-no"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="item-title"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="copyright-status"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="corr-author"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="eo-item-nr"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pit"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="prd-type"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <!--left two empty cells for first-e-page and last-e-page -->
                            <td valign="top" align="left"/>
                            <td valign="top" align="left"/> 
                            <!-- <td valign="top"><xsl:value-of select="no-issues-free"/></td>-->
                            <td valign="top">
                                <xsl:value-of select="e-suite"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-colour-figs"/>
                            </td>
                            <td valign="top">
                                <xsl:call-template name="put-offprint-payment"/>
                            </td>
                            <td valign="top">
                                <!-- If xml built via button on form then colour-fig-nr-print is a level lower -->
                                <xsl:choose>
                                    <xsl:when test="colour-fig-nr-print/colour-fig-nr-print_ROW[@num='1']">
                                        <xsl:for-each select="colour-fig-nr-print/colour-fig-nr-print_ROW">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() !=last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:for-each select="colour-fig-nr-print">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() !=last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </td>
                            <!-- Two empty cells for Online Pub Date and Copyright Rec'd Date-->
                            <td/>
                            <td/>
                            <td valign="top">
                                <xsl:value-of select="remark"/>
                            </td>
                        </tr>
                    </xsl:if>

                    <xsl:if test="@type='non-ce'">
                        <tr>
                            <td colspan="2"/>
                            <td valign="top">
                                <xsl:value-of select="version-no"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="item-title"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="copyright-status"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="corr-author"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="eo-item-nr"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pit"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="prd-type"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <!--left two empty cells for first-e-page and last-e-page -->
                            <td valign="top" align="left"/>
                            <td valign="top" align="left"/> 
                            <!-- <td valign="top"><xsl:value-of select="no-issues-free"/></td>-->
                            <td valign="top">
                                <xsl:value-of select="e-suite"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-colour-figs"/>
                            </td>
                            <td valign="top">
                                <xsl:call-template name="put-offprint-payment"/>
                            </td>
                            <td valign="top">
                                <!-- If xml built via button on form then colour-fig-nr-print is a level lower -->
                                <xsl:choose>
                                    <xsl:when test="colour-fig-nr-print/colour-fig-nr-print_ROW[@num='1']">
                                        <xsl:for-each select="colour-fig-nr-print/colour-fig-nr-print_ROW">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() != last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:for-each select="colour-fig-nr-print">
                                            <xsl:value-of select="."/>
                                            <xsl:if test="position() != last()">, </xsl:if>
                                        </xsl:for-each>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </td>
                            <!-- Two empty cells for Online Pub Date and Copyright Rec'd Date-->
                            <td/>
                            <td/>
                            <td valign="top">
                                <xsl:value-of select="remark"/>
                            </td>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='blank'">
                        <tr>
                            <td/>
                            <td valign="top">Blank page</td>
                            <td colspan="7"/>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <td colspan="4"/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='remark'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="item-title"/>
                                </i>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="pit"/>
                                </i>
                            </td>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="prd-type"/>
                                </i>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h1'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="item-title"/>
                                </b>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="pit"/>
                                </b>
                            </td>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="prd-type"/>
                                </b>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='he'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="item-title"/>
                                </b>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="pit"/>
                                </b>
                            </td>
                            <td valign="top">
                                <b>
                                    <xsl:value-of select="prd-type"/>
                                </b>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h2'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <u>
                                    <xsl:value-of select="item-title"/>
                                </u>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <u>
                                    <xsl:value-of select="pit"/>
                                </u>
                            </td>
                            <td valign="top">
                                <u>
                                    <xsl:value-of select="prd-type"/>
                                </u>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h3'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <u>
                                    <i>
                                        <xsl:value-of select="item-title"/>
                                    </i>
                                </u>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <u>
                                    <i>
                                        <xsl:value-of select="pit"/>
                                    </i>
                                </u>
                            </td>
                            <td valign="top">
                                <u>
                                    <i>
                                        <xsl:value-of select="prd-type"/>
                                    </i>
                                </u>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h4'">
                        <tr>
                            <td colspan="3"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="item-title"/>
                                </i>
                            </td>
                            <td colspan="3"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="pit"/>
                                </i>
                            </td>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="prd-type"/>
                                </i>
                            </td>
                            <td colspan="2"/>
                            <td valign="top"><!--<i><xsl:value-of select="pdf-pages"/></i>--></td>
                            <td valign="top"><!--<i><xsl:value-of select="no-colour-figs"/></i>--></td>
                            <td/>
                        </tr>
                    </xsl:if>
                </xsl:if>

                <!-- offprint sheet -->
                <!--=================================================================-->
                <xsl:if test="//stage[@step='OFFPRINTS' or @step='SEND-OFFPRINTS']">
                    <xsl:if test="@type='ce'">
                        <tr>
                            <td valign="top">
                                <xsl:value-of select="aid"/>
                            </td>
                            <td valign="top" width="150">
                                <xsl:value-of select="doi"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="corr-author"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pit"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="prd-type"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-paid"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-free"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-tot"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-issues-free"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-issues-paid"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="covers"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="e-suite"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-colour-figs"/>
                            </td>
                            <td valign="top">
                                <xsl:call-template name="put-offprint-payment"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-charge"/>
                            </td>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='non-ce'">
                        <tr>
                            <td colspan="2"/>
                            <td valign="top">
                                <xsl:value-of select="corr-author"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pit"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="prd-type"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-paid"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-free"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-offprints-tot"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-issues-free"/>
                            </td>
                            <!-- Required? sps_main needs to popluate non-ce cursor. Plus remove remark?  <td valign="top"><xsl:value-of select="no-issues-paid"/></td> -->
                            <td valign="top">
                                <xsl:value-of select="no-issues-paid"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="covers"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="e-suite"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="no-colour-figs"/>
                            </td>
                            <td valign="top">
                                <xsl:call-template name="put-offprint-payment"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-charge"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="remark"/>
                            </td>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='blank'">
                        <tr>
                            <td/>
                            <td valign="top">Blank page</td>
                            <td colspan="3"/>
                            <td valign="top">
                                <xsl:value-of select="page-from"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="page-to"/>
                            </td>
                            <td valign="top">
                                <xsl:value-of select="pdf-pages"/>
                            </td>
                            <td colspan="7"/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='remark'">
                        <tr>
                            <td colspan="2"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="item-title"/>
                                </i>
                            </td>
                            <td colspan="12"/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h1'">
                        <tr>
                            <td colspan="2"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="item-title"/>
                                </i>
                            </td>
                            <td colspan="12"/>
                            <td/>
                        </tr>
                    </xsl:if>
                    <xsl:if test="@type='h2'">
                        <tr>
                            <td colspan="2"/>
                            <td valign="top">
                                <i>
                                    <xsl:value-of select="item-title"/>
                                </i>
                            </td>
                            <td colspan="12"/>
                            <td/>
                        </tr>
                    </xsl:if>
                </xsl:if>
            </xsl:for-each>
            <!--=================================================================-->
        </table>
    </xsl:template>

    <!--################################################################################-->
    <!-- This section is meant for the special issue -->

    <xsl:template match="special-issue">
        <tr>
            <td>Special issue / conference information</td>
        </tr>
        <table>
            <tr>
                <td valign="top"><li/>Working title</td>
                <td valign="top">: <xsl:value-of select="special-issue-id"/></td>
            </tr>
            <tr>
                <td valign="top"><li/>Title</td>
                <td valign="top">: <xsl:value-of select="full-name"/></td>
            </tr>
            <tr>
                <td>
                    <xsl:apply-templates select="conference"/>
                </td>
            </tr>
            <tr>
                <td valign="top"><li/>(Guest) Editors</td>
                <td valign="top">: <xsl:value-of select="editors"/></td>
            </tr>
        </table>
    </xsl:template>

    <xsl:template match="conference">
        <tr>
            <td valign="top"><li/>Conference abbr.</td>
            <td valign="top">: <xsl:value-of select="abbr-name"/></td>
        </tr>
        <tr>
            <td valign="top"><li/>Conference location</td>
            <td valign="top">: <xsl:value-of select="venue"/></td>
        </tr>
        <tr>
            <td valign="top"><li/>Conference date</td>
            <td valign="top">: <xsl:value-of select="effect-date"/></td>
        </tr>
        <tr>
            <td valign="top"><li/>Sponsored by</td>
            <td valign="top">: <xsl:value-of select="sponsor"/></td>
        </tr>
    </xsl:template>

    <xsl:template name="put-offprint-payment">
        <!-- current element node is row -->
        <xsl:choose>
            <xsl:when test="offprint-payment">
                <xsl:value-of select="offprint-payment/@payment"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>N/A</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!--################################################################################-->
	<xsl:template match="diff">
		<font size="4" color="red">
			<xsl:value-of select="."/>
		</font>
	</xsl:template>
</xsl:stylesheet>
