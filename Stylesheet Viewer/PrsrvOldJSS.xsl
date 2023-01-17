<?xml version="1.0" encoding="utf-8"?>
<!--    
    Elsevier stylesheet for journal stylesheets in XML format
    Version 1.8
    
    Copyright (c) 2009-2011 Elsevier Ltd
    $Id: journalStylesheet.xsd, v1.8 2010/08/11 19:50:30 Gopinath $
-->
<!--
    Permission to copy and distribute verbatim copies of this document is granted, 
    provided this notice is included in all copies, but changing it is not allowed. 
-->

<xsl:stylesheet version="1.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:jss="http://www.elsevier.com/xml/schema/journalStylesheets" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:strip-space elements="*"/>

    <xsl:output method="html" encoding="UTF-8"/>

    <xsl:template match="/">
        <html>
            <head>
                <style type="text/css"> 
                    .heading {font-weight:bold; color: #B8860B; font-size:200%}
                    .head2 {color: MediumVioletRed;}
                    .head3 {font-variant:small-caps; color: #8B4513; text-align:left;}
                    .head4 {font-style:italic; color: #C00000; text-align:left;}
                    .head5 {font-variant:small-caps; font-weight:bold; color: blue; text-align:centre;}
                    .lhs-head {color: brown; font-variant:small-caps; font-weight:bold;}
                    .lhs-thead {color: #008B8B; font-variant:small-caps;}
                    .lhs {color: #2F4F4F;}
                    .rhs {color: #A52A2A;}
                    .lhsb {color: brown; font-weight:bold;}
                    .rhsb {color: green; font-weight:bold;}
                    .tbl1 {color: #808080;}
                    .rhs-wd {color: red; font-weight:bold;}
                    .rhs-nd {color: red; font-style:italic;}
                    body {background-color:#F0F0F0; font-family:'Arial Unicode MS'; margin-left: 10%; margin-right: 10%;}
                </style>
            </head>
            <body onkeyup = "keyDown=0" onkeydown = "keyDown=1">
                <h2 align="center">
                    <table width="1000pt">
                        <tr>
                            <td align="left" width="1%"><img src="logo.gif"/></td>
                            <td align="center"><font class="heading"><xsl:value-of select="jss:journalStylesheet/jss:baseData/jss:ptsData/jss:journalCode"/> STYLESHEET</font></td>
                        </tr>
                    </table>
                </h2>
                <xsl:apply-templates/>
            </body>
        </html>
    </xsl:template>
    
    <!-- Base Data -->
    
    <xsl:template match="jss:journalStylesheet/jss:baseData">
        <table border="4" width="1000pt">
                    <tr>
                        <th class="head2" colspan="2">BASE DATA</th>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">PTS Data and JM Edit Data</th>
                    </tr>
                    <tr>
                        <td width="30%"><font class="lhs"><xsl:text>Journal code</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalCode"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhsb"><xsl:text>Date of last PTS report</xsl:text></font></td><td><font class="rhsb"><xsl:apply-templates select="jss:ptsData/jss:ptsReportDate"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhsb"><xsl:text>Non-PTS data last modified on</xsl:text></font></td><td><font class="rhsb"><xsl:apply-templates select="jss:JM-EditData/jss:dateModified"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhsb"><xsl:text>Modified by</xsl:text></font></td><td><font class="rhsb"><xsl:apply-templates select="jss:JM-EditData/jss:modifiedBy"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal number</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalNumber"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal title</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalTitle"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>PMG</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:pmg"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>PMC</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:pmc"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>ISSN</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:issn"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>epGroup</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:epGroup"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Production site</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:productionSite"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal DOI code</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalDOICode"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal DOI content type</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalDOIContentType"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>eSubmission content type</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:eSubmissionContentType"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Expiry date</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:expiryDate"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copyright Rem Exec</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:copyrightRemExec"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Colour Conf Rem Exec</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:colourConfRemExec"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Missing Items Rem Exec</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:missingItemsRemExec"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Author Proof Rem Exec</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:authorProofRemExec"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Editor Proof Rem Exec</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:editorProofRemExec"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Typeset Red PTS</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:typesetRedPTS"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Typeset Red Calc</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:typesetRedCalc"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Average item pages</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:averageItemPages"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Typeset model</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:typesetModel"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Typesetter</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:typesetter"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal manager</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalManager"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal LSM (PSP)</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalLSM"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copyright holder</xsl:text></font></td>
<td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'S'">
                                <font class="rhs"><xsl:text>Society owned</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'E'">
                                <font class="rhs"><xsl:text>Elsevier owned</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'J'">
                                <font class="rhs"><xsl:text>Joint copyright ownership</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:ptsData/jss:copyrightHolder"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copyright Statement</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:ptsData/jss:copyrightStatement"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Society affiliation</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:societyAffiliation"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Reference style</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:ptsData/jss:refStyle"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Reference style comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:referenceStyleComment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:referenceStyleComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:referenceStyleComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
            <!--<xsl:if test="jss:ptsData/jss:typesetModel!=EU5Gdc">-->
                 <!--   <tr>
                        <td><font class="lhs"><xsl:text>Abbreviate journal name in reference</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:abbJNRef = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:abbJNRef = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
            </xsl:if>-->
                    <tr>
                        <td><font class="lhs"><xsl:text>OPCO</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:opco"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Printer</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:printer"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Print run</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:printRun"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Paper type inside</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:paperTypeInside"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Paper type cover</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:paperTypeCover"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Print type</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:printType"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Cover finishing</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:coverFinishing"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Bind Type</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:bindType"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Back margin</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:backMargin"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Head margin</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:headMargin"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Right-hand start</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:righthandStart = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:righthandStart = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Trim size</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:trimSize"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Time Based Publ Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:timeBasedPublInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:timeBasedPublInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Early Based Publ Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:earlyBasedPublInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:earlyBasedPublInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Fixed Plan Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:fixedPlanInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:fixedPlanInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Scan S0 required?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:scanS0 = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:scanS0 = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Scan S0 comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:scanS0Comment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:scanS0Comment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:scanS0Comment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>CAP light plus journal?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:capLightPlus = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:capLightPlus = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>MF only?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:mfIndicator = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:mfIndicator = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>MF comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:mfIndicatorComment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:mfIndicatorComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:mfIndicatorComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copy-Edit Level</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:copyEditLevel"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Adv Copy Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:advCopyInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:advCopyInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>S5 required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:s5Required = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:s5Required = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>S100 required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:s100Required = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:s100Required = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>S200 required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:s200Required = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:s200Required = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>S300 required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:s300Required = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:s300Required = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
            <tr>
                <td><font class="lhs"><xsl:text>CrossMark required for S5 stage?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:ptsData/jss:cmS5Required = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:ptsData/jss:cmS5Required = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>CrossMark required for S100 stage?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:ptsData/jss:cmS100Required = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:ptsData/jss:cmS100Required = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>CrossMark required for S200 stage?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:ptsData/jss:cmS200Required = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:ptsData/jss:cmS200Required = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>CrossMark required for S250 stage?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:ptsData/jss:cmS250Required = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:ptsData/jss:cmS250Required = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>CrossMark required for S300 stage?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:ptsData/jss:cmS300Required = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:ptsData/jss:cmS300Required = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>

<!--                    <tr>
                        <td><font class="lhs"><xsl:text>Author proof required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:authorProofRequired = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:authorProofRequired = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Editor proof required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:editorProofRequired = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:editorProofRequired = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr> -->
                    <tr>
                        <td><font class="lhs"><xsl:text>Track Off Sys Labels Req</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:trackOffSysLabelsReq = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:trackOffSysLabelsReq = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Delta Label Run Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:deltaLabelRunInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:deltaLabelRunInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Language</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:language"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>e Suite Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:eSuiteInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:eSuiteInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>ext Login</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:extLogin = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:extLogin = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>ext Login Comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:extLoginComment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:extLoginComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:extLoginComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Additional OP Paid Col</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:additionalOPPaidCol"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal OP Price List</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalOPPriceList"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal page charges</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalPageCharges"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal Paid Col Page Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:journalPaidColPageInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:journalPaidColPageInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copyright Tran Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:copyrightTranInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:copyrightTranInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copyright Tran Online Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:copyrightTranOnlineInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:copyrightTranOnlineInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Default item PIT</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:defaultItemPIT"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Default issue PIT</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:defaultIssuePIT"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Default Item Prod</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:defaultItemProd"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Default Issue Prod</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:defaultIssueProd"/></font></td>
                    </tr>
                    <!--<tr>
                        <td><font class="lhs"><xsl:text>Alpha Journal Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:alphaJournalInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:alphaJournalInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal Admin</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalAdmin"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Int Journal Admin</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:intJournalAdmin"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Copy editor</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:copyEditor"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal Team Manager</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:journalTeamManager"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Print LSM</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:printLSM"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Binder</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:binder"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Issue sender</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:issueSender"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Offprint sender</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:offprintSender"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Back issue storage</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:backIssueStorage"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Offprint printer</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:offprintPrinter"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Offprint finisher</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:offprintFinisher"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Required ABP?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:ABP = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:ABP = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal Payment Online</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:journalPaymentOnline = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:journalPaymentOnline = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>E-Sub. Acronym (primary)</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:ptsData/jss:eSubAcr"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>In-house MC</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:ptsData/jss:inHouseMC = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:ptsData/jss:inHouseMC = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Zero Warehousing</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:zeroWarehousing = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:zeroWarehousing = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Location of SLA document</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:slaLocation"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>SLA Comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:slaComment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:slaComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:slaComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                            </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Service level agreement</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:sla = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:JM-EditData/jss:sla = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Who does the mastercopying?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:mastercopying = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:JM-EditData/jss:mastercopying = 1">
                                <font class="rhs"><xsl:text>elsevier internal</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:JM-EditData/jss:mastercopying = 2">
                                <font class="rhs"><xsl:text>supplier/typesetter</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:JM-EditData/jss:mastercopying = 3">
                                <font class="rhs"><xsl:text>other</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Mastercopying comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:mastercopyingComment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:mastercopyingComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:mastercopyingComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Approved non-standard production?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:approvedNonStandard = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:JM-EditData/jss:approvedNonStandard = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Approved non-standard comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:JM-EditData/jss:approvedNonStandardComment/jss:p">
                                <xsl:for-each select="jss:JM-EditData/jss:approvedNonStandardComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:JM-EditData/jss:approvedNonStandardComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
         
            <tr>
                <th class="head2" colspan="2">OTHER INSTRUCTIONS</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Other instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:otherInstructions/jss:p">
                        <xsl:for-each select="//jss:otherInstructions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:otherInstructions/."/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>


    </xsl:template>

   
    <!-- PIT -->
    
    <xsl:template match="jss:journalStylesheet/jss:pit">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">PIT</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Doc Head</th>
            </tr>
            <tr>
                <th width="30%" class="lhs-thead">Dochead PIT</th>
                <th class="lhs-thead">Dochead Description</th>
                <!--<th width="30%" class="lhs-thead">Dochead Expired</th>-->
            </tr>
            <xsl:for-each select="jss:docHeads/jss:docHead">
                <xsl:if test="jss:docheadExpired = 0">
                <tr>
                    <td><font class="tbl1"><xsl:apply-templates select="jss:docheadPIT"/></font></td>
                    <td><font class="tbl1"><xsl:apply-templates select="jss:docheadDescription"/></font></td>
                    <!--<td><font class="tbl1"><xsl:choose>
                        <xsl:when test="jss:docheadExpired = 0">
                            <font class="rhs"><xsl:text>No</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:docheadExpired = 1">
                            <font class="rhs"><xsl:text>Yes</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose></font></td>-->
                </tr>
                </xsl:if>
            </xsl:for-each>
            <tr>
                <td><font class="lhs"><xsl:text>Dochead Comments</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:docHeads/jss:docheadComments/jss:p">
                        <xsl:for-each select="jss:docHeads/jss:docheadComments/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:docHeads/jss:docheadComments"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
        <table border="4" width="1000pt">
            <tr>
                <th class="head3" colspan="2">Section Head</th>
            </tr>
            <tr>
                <th width="30%" class="lhs-thead">Section Code</th>
                <th class="lhs-thead">Section Description</th>
                <!--<th width="30%" class="lhs-thead">Section Expired</th>-->
            </tr>
            <xsl:for-each select="jss:sectionHeads/jss:sectionHead">
                <xsl:if test="jss:sectionExpired = 0">
                <tr>
                    <td><font class="tbl1"><xsl:apply-templates select="jss:sectionCode"/></font></td>
                    <td><font class="tbl1"><xsl:apply-templates select="jss:sectionDescription"/></font></td>
                    <!--<td><font class="tbl1"><xsl:choose>
                        <xsl:when test="jss:sectionExpired = 0">
                            <font class="rhs"><xsl:text>No</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sectionExpired = 1">
                            <font class="rhs"><xsl:text>Yes</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose></font></td>-->
                </tr>
                </xsl:if>
            </xsl:for-each>
            <tr>
                <td><font class="lhs"><xsl:text>Section Comments</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:sectionHeads/jss:sectionComments/jss:p">
                        <xsl:for-each select="jss:sectionHeads/jss:sectionComments/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:sectionHeads/jss:sectionComments"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
	<table border="4" width="1000pt">
            <tr>
                <th class="head3" colspan="2">CrossMark PITs</th>
            </tr>
            <tr>
                <td colspan="2"><xsl:text>&#8195;</xsl:text>
                    <xsl:for-each select="//jss:crossMark/jss:cmPIT">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/><xsl:text>&#8194;&#9830;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    

<!--Anwar-->
    <!-- C&U -->
    
    <xsl:template match="jss:journalStylesheet/jss:cu" name="cu">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">C&amp;U</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">History</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Received date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:received = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Revised date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:revised = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Accepted date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:accepted = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>General history comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:generalHistoryComment/jss:p">
                        <xsl:for-each select="//jss:history/jss:generalHistoryComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:history/jss:generalHistoryComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Special issue received date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:received_si = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received_si = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received_si = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received_si = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received_si = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:received_si = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special issue revised date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:revised_si = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised_si = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised_si = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised_si = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised_si = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:revised_si = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special issue accepted date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:accepted_si = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted_si = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted_si = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted_si = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted_si = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:accepted_si = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special issue history comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:specialIssueHistoryComment/jss:p">
                        <xsl:for-each select="//jss:history/jss:specialIssueHistoryComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:history/jss:specialIssueHistoryComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific history</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:journalSpecificHistory = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific article history text</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:journalSpecificHistoryText/jss:p">
                        <xsl:for-each select="//jss:history/jss:journalSpecificHistoryText/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:history/jss:journalSpecificHistoryText"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>History dates for items linked to an item group</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 4">
                        <font class="rhs"><xsl:text>Query JA if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:historyItemGroup = 5">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>History item group comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:historyItemGroupComment/jss:p">
                        <xsl:for-each select="//jss:history/jss:historyItemGroupComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:history/jss:historyItemGroupComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Online publication date</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:onlinePubDate = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:history/jss:onlinePubDate = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Online publication date comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:history/jss:onlinePubDateComment/jss:p">
                        <xsl:for-each select="//jss:history/jss:onlinePubDateComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:history/jss:onlinePubDateComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Abstract</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific abstract requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:abstract/jss:journalSpecificAbstract = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:abstract/jss:journalSpecificAbstract = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific abstract requirements comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:abstract/jss:journalSpecificAbstractComment/jss:p">
                        <xsl:for-each select="//jss:abstract/jss:journalSpecificAbstractComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:abstract/jss:journalSpecificAbstractComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Graphical Abstract</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific graphical abstract requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 1">
                        <font class="rhs"><xsl:text>Optional</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 2">
                        <font class="rhs"><xsl:text>Mandatory</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 3">
                        <font class="rhs"><xsl:text>Not Required</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific graphical abstract requirements comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment/jss:p">
                        <xsl:for-each select="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>GA Format if Colour</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 1">
                        <font class="rhs"><xsl:text>E-ONLY-COLOUR</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 2">
                        <font class="rhs"><xsl:text>COLOUR-IN-PRINT billing No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 3">
                        <font class="rhs"><xsl:text>COLOUR</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific GA TOC</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 0">
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 1">
                        <font class="rhs"><xsl:text>E-only</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 2">
                        <font class="rhs"><xsl:text>Print only</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 3">
                        <font class="rhs"><xsl:text>Electronic and Print</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific GA TOC comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOCComment/jss:p">
                        <xsl:for-each select="//jss:graphicalAbstract/jss:journalSpecificGATOCComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:graphicalAbstract/jss:journalSpecificGATOCComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific GA Title Page</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePage = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePage = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific GA Title Page comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment/jss:p">
                        <xsl:for-each select="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Keywords</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific keyword requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 4">
                        <font class="rhs"><xsl:text>Send missing item letter if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 5">
                        <font class="rhs"><xsl:text>If missing, do not problem; query author on the AQF</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywords = 6">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Keyword source</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:keywords/jss:keywordSource = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:keywordSource = 1">
                        <font class="rhs"><xsl:text>Free text</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:keywords/jss:keywordSource = 2">
                        <font class="rhs"><xsl:text>Journal-specific list (add details to comments field)</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Minimum number of keywords</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:keywords/jss:keywordsMin"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Maximum number of keywords</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:keywords/jss:keywordsMax"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific keywords requirements comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:keywords/jss:journalSpecificKeywordsComment/jss:p">
                        <xsl:for-each select="//jss:keywords/jss:journalSpecificKeywordsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:keywords/jss:journalSpecificKeywordsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Highlights</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific highlights requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 0">
                        <font class="rhs"><xsl:text>Not Required</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 1">
                        <font class="rhs"><xsl:text>Optional</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 2">
                        <font class="rhs"><xsl:text>Mandatory</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific highlights requirements comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:highlights/jss:journalSpecificHighlightsComment/jss:p">
                        <xsl:for-each select="//jss:highlights/jss:journalSpecificHighlightsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:highlights/jss:journalSpecificHighlightsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Classification</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific classification requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 1">
                        <font class="rhs"><xsl:text>Do not use even if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 2">
                        <font class="rhs"><xsl:text>Use if supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 3">
                        <font class="rhs"><xsl:text>Query JM if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 4">
                        <font class="rhs"><xsl:text>Send missing item letter if not supplied</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 5">
                        <font class="rhs"><xsl:text>If missing, do not problem; query author on the AQF</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassification = 6">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Classification source</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:classification/jss:classificationSource"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Minimum number of classification codes</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:classification/jss:classificationMin"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Maximum number of classification codes</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:classification/jss:classificationMax"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal-specific classification requirements comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:classification/jss:journalSpecificClassificationComment/jss:p">
                        <xsl:for-each select="//jss:classification/jss:journalSpecificClassificationComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:classification/jss:journalSpecificClassificationComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Colour</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Paid colour per page?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:printedColour = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:printedColour = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Free Printed Colour</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:freePrintedColour = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:freePrintedColour = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>No Free Web Colour</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:noFreeWebColour = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:noFreeWebColour = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>First unit free of charge?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:firstPageFOC = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:firstPageFOC = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>First unit free of charge comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:firstPageFOCComment/jss:p">
                        <xsl:for-each select="//jss:colour/jss:firstPageFOCComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:colour/jss:firstPageFOCComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cost for first colour unit</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:colour/jss:costFirstColour"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cost for next colour unit</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:colour/jss:costNextColour"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Currency Code</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:colour/jss:currencyCode"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Colour artwork exceptions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:colourArtworkExceptions/jss:p">
                        <xsl:for-each select="//jss:colour/jss:colourArtworkExceptions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:colour/jss:colourArtworkExceptions"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Which colour figure letter to the author?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 1">
                        <font class="rhs"><xsl:text>Colour letter</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 2">
                        <font class="rhs"><xsl:text>Colour confirmation letter</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Extra journal-specific colour instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:colour/jss:extraColourInstructions/jss:p">
                        <xsl:for-each select="//jss:colour/jss:extraColourInstructions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:colour/jss:extraColourInstructions"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
    <br/>
    </xsl:template>
<!--Anwar-->
    
    <!-- S0 -->
    
    <xsl:template match="jss:journalStylesheet/jss:s0">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S0</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Item receipt</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Quick book in?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:quickBook = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:quickBook = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Quick book comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:quickBookComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:quickBookComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:quickBookComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Items received via EES?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedEES = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:receivedEES = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>EES comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedEESComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:receivedEESComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:receivedEESComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Items received via Omega?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedOmega = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:receivedOmega = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Omega comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedOmegaComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:receivedOmegaComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:receivedOmegaComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Items received via Email?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedEmail = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:receivedEmail = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Email comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedEmailComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:receivedEmailComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:receivedEmailComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Items received via FTP/Courier?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedFTP = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:receivedFTP = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>FTP comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:receivedFTPComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:receivedFTPComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:receivedFTPComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special Issues?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:specialIssues = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:itemreceipt/jss:specialIssues = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special issues comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:specialIssuesComment/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:specialIssuesComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:specialIssuesComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Other item receipt instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:itemreceipt/jss:otherReceipt/jss:p">
                        <xsl:for-each select="jss:itemreceipt/jss:otherReceipt/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:itemreceipt/jss:otherReceipt"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Correspondence</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Can supplier send correspondence to the author?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:correspondence/jss:correspondenceAuthor = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:correspondence/jss:correspondenceAuthor = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Correspondence author comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                        <xsl:for-each select="jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:correspondence/jss:correspondenceAuthorComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>What to do when an item is missing?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:correspondence/jss:missingItemCorrespondence = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:correspondence/jss:missingItemCorrespondence = 1">
                        <font class="rhs"><xsl:text>Send "missing item" letter to the author</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:correspondence/jss:missingItemCorrespondence = 2">
                        <font class="rhs"><xsl:text>Log a problem for the journal manager</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
             <tr>
                <th class="head3" colspan="2">Offprints</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>No of paper offprints</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:offprints/jss:paperOffprints"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>E-offprints?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:offprints/jss:eOffprint = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:offprints/jss:eOffprint = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional free paper offprints for paid colour</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:offprints/jss:additionalFreePaper"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Extra journal specific offprint instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:offprints/jss:extraOffprintInstructions/jss:p">
                        <xsl:for-each select="jss:offprints/jss:extraOffprintInstructions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:offprints/jss:extraOffprintInstructions"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Number of free copies of issue to author</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:offprints/jss:freeIssues"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Free Issues Comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:offprints/jss:freeIssuesComment/jss:p">
                        <xsl:for-each select="jss:offprints/jss:freeIssuesComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:offprints/jss:freeIssuesComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Number of free issues for special issues</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:offprints/jss:freeIssuesSI"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Free issues special issues comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:offprints/jss:freeIssuesSIComment/jss:p">
                        <xsl:for-each select="jss:offprints/jss:freeIssuesSIComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:offprints/jss:freeIssuesSIComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Additional information</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional information about the selection of sections</xsl:text></font></td>
                <!-- <td><xsl:for-each select="jss:section/jss:p|jss:section">
                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                </xsl:for-each>
                </td> -->
                <td><font class="rhs"><xsl:apply-templates select="jss:section"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional journal-specific login instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:additionalLoginInstructions/jss:p">
                        <xsl:for-each select="jss:additionalLoginInstructions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:additionalLoginInstructions"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    

     <!-- S100 -->
    
    <xsl:template match="jss:journalStylesheet/jss:s100">
                        <table border="4" width="1000pt">
                    <tr>
                        <th class="head2" colspan="2">S100</th>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Heading</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Numbering style</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:heading/jss:numberingStyle = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:heading/jss:numberingStyle = 1">
                                <font class="rhs"><xsl:text>Numbered</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:heading/jss:numberingStyle = 2">
                                <font class="rhs"><xsl:text>UnNumbered</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:heading/jss:numberingStyle = 3">
                                <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Numbering style comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:heading/jss:NSComment/jss:p">
                                <xsl:for-each select="jss:heading/jss:NSComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:heading/jss:NSComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Roles</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>"Motto" paragraph</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:motto = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:roles/jss:motto = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Motto comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:mottoComment/jss:p">
                                <xsl:for-each select="jss:roles/jss:mottoComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:roles/jss:mottoComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>"Case Report" section</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:caseReport = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:roles/jss:caseReport = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Case report comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:caseReportComment/jss:p">
                                <xsl:for-each select="jss:roles/jss:caseReportComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:roles/jss:caseReportComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>"Note added in proof" section</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:noteinProof = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:roles/jss:noteinProof = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Note added in proof comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:noteinProofComment/jss:p">
                                <xsl:for-each select="jss:roles/jss:noteinProofComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:roles/jss:noteinProofComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>"Materials methods" section in smaller font</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:materialsMethods = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:roles/jss:materialsMethods = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Materials methods comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:materialsMethodsComment/jss:p">
                                <xsl:for-each select="jss:roles/jss:materialsMethodsComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:roles/jss:materialsMethodsComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>"Results" section</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:results = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:roles/jss:results = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Results comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:roles/jss:resultsComment/jss:p">
                                <xsl:for-each select="jss:roles/jss:resultsComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:roles/jss:resultsComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Introduction</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Introduction</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:intro/jss:introduction = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:intro/jss:introduction = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Introduction comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:intro/jss:introductionComment/jss:p">
                                <xsl:for-each select="jss:intro/jss:introductionComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:intro/jss:introductionComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Conflict of Interest</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Conflict of Interest</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:CoI/jss:ConflictInterest = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:CoI/jss:ConflictInterest = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Conflict of Interest comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:CoI/jss:CoIComment/jss:p">
                                <xsl:for-each select="jss:CoI/jss:CoIComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:CoI/jss:CoIComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Biographical Information</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Required author details</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:bioInfo/jss:authorDetails = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:bioInfo/jss:authorDetails = 1">
                                <font class="rhs"><xsl:text>Author biography only</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:bioInfo/jss:authorDetails = 2">
                                <font class="rhs"><xsl:text>Author photograph only</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:bioInfo/jss:authorDetails = 3">
                                <font class="rhs"><xsl:text>Author biography and photograph</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:bioInfo/jss:authorDetails = 4">
                                <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Author details comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:bioInfo/jss:ADComment/jss:p">
                                <xsl:for-each select="jss:bioInfo/jss:ADComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:bioInfo/jss:ADComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Non-standard label requirements</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Non-standard Fig./Table/Textbox label requirements</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:floatLabel/jss:NSFloatLabel/jss:p">
                                <xsl:for-each select="jss:floatLabel/jss:NSFloatLabel/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:floatLabel/jss:NSFloatLabel"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Artwork</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal-specific artwork requirements</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:artwork/jss:journalSpecificArtwork = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:artwork/jss:journalSpecificArtwork = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal-specific artwork requirements comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:artwork/jss:journalSpecificArtworkComment/jss:p">
                                <xsl:for-each select="jss:artwork/jss:journalSpecificArtworkComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:artwork/jss:journalSpecificArtworkComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Latex</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Latex journal</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:latexInfo/jss:latex = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:latexInfo/jss:latex = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Proportion of manuscripts arriving in LaTeX </xsl:text></font></td>
                        <td><font class="rhs"><xsl:apply-templates select="//jss:latexInfo/jss:latexFrequency"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Latex comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:latexInfo/jss:latexComment/jss:p">
                                <xsl:for-each select="jss:latexInfo/jss:latexComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:latexInfo/jss:latexComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Proofing</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal-specific proofing instructions</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:proofing/jss:journalSpecificProofing = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:journalSpecificProofing = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Send proofs to</xsl:text></font></td>
                         <td><font class="rhs"><xsl:apply-templates select="//jss:proofing/jss:proofsSentTo"/></font></td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Proofs send to comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:proofing/jss:journalSpecificProofingComment/jss:p">
                                <xsl:for-each select="jss:proofing/jss:journalSpecificProofingComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:proofing/jss:journalSpecificProofingComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Text of PDF proof email</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 1">
                                <font class="rhs"><xsl:text>Standard (English) text</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 2">
                                <font class="rhs"><xsl:text>Use standard letter in language of article</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 3">
                                <font class="rhs"><xsl:text>Standard text (French translation)</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 4">
                                <font class="rhs"><xsl:text>Standard text (German translation)</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 5">
                                <font class="rhs"><xsl:text>Standard text (Italian translation)</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 6">
                                <font class="rhs"><xsl:text>Standard text (Spanish translation)</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:proofing/jss:changesPDFProofText = 7">
                                <font class="rhs"><xsl:text>Non-standard (see comments)</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Text of PDF proof email comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:proofing/jss:changesPDFProofTextComment/jss:p">
                                <xsl:for-each select="jss:proofing/jss:changesPDFProofTextComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:proofing/jss:changesPDFProofTextComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                            <tr>
                                <td><font class="lhs"><xsl:text>Web Hosted Proofing</xsl:text></font></td>
                                <td><xsl:choose>
                                    <xsl:when test="jss:proofing/jss:webHostedProofing = 0">
                                        <font class="rhs"><xsl:text>No</xsl:text></font>
                                    </xsl:when>
                                    <xsl:when test="jss:proofing/jss:webHostedProofing = 1">
                                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                                    </xsl:otherwise>
                                </xsl:choose>
                                </td>
                            </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Additional journal-specific S100 typesetting requirements</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:additionalS100/jss:p">
                                <xsl:for-each select="jss:additionalS100/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:additionalS100"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
            </table>
        <br/>
    </xsl:template>
    
    <!-- S200 -->
    
    <xsl:template match="jss:journalStylesheet/jss:s200">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S200</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">VIP</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>VIP in S200?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:vip/jss:vipS200 = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:vip/jss:vipS200 = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>VIP S200 comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:vip/jss:vipS200Comment/jss:p">
                        <xsl:for-each select="jss:vip/jss:vipS200Comment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:vip/jss:vipS200Comment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional journal-specific S200 typesetting requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:additionalS200/jss:p">
                        <xsl:for-each select="jss:additionalS200/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:additionalS200"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    
    <!-- P100 -->
    
    <xsl:template match="jss:journalStylesheet/jss:p100">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">P100</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Delivery requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:deliveryRequirements = 0">
                        <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:deliveryRequirements = 1">
                        <font class="rhs"><xsl:text>Cover and Prelims only</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:deliveryRequirements = 2">
                        <font class="rhs"><xsl:text>Everything</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:deliveryRequirements = 3">
                        <font class="rhs"><xsl:text>Other, see comments</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional journal-specific P100 typesetting requirements</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:additionalP100/jss:p">
                        <xsl:for-each select="jss:additionalP100/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:additionalP100"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    
    <!-- S300 -->
    
    <xsl:template match="jss:journalStylesheet/jss:s300">
        <table border="4" width="1000pt">
                    <tr>
                        <th class="head2" colspan="2">S300</th>
                    </tr>
                    <tr>
                        <td width="30%"><font class="lhs"><xsl:text>Journal-specific cover artwork requirements</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:journalSpecificCover = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:journalSpecificCover = 1">
                                <font class="rhs"><xsl:text>Standard</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:journalSpecificCover = 2">
                                <font class="rhs"><xsl:text>Different cover image for each issue</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:journalSpecificCover = 3">
                                <font class="rhs"><xsl:text>Different cover image for each volume</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:journalSpecificCover = 4">
                                <font class="rhs"><xsl:text>Other (see comments)</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Journal specific cover comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:journalSpecificCoverComment/jss:p">
                        <xsl:for-each select="jss:journalSpecificCoverComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:journalSpecificCoverComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Different cover for special issues?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:specialIssueCover = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:specialIssueCover = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                            </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Special issue cover comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:specialIssueCoverComment/jss:p">
                                <xsl:for-each select="jss:specialIssueCoverComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:specialIssueCoverComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Run-on items?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:runOnItems = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:runOnItems = 1">
                                <font class="rhs"><xsl:text>None</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:runOnItems = 2">
                                <font class="rhs"><xsl:text>Book reviews</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:runOnItems = 3">
                                <font class="rhs"><xsl:text>Correspondence</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:runOnItems = 4">
                                <font class="rhs"><xsl:text>Book reviews and correspondence</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:runOnItems = 5">
                                <font class="rhs"><xsl:text>Other (see comments)</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Run-on items comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:runOnItemsComment/jss:p">
                                <xsl:for-each select="jss:runOnItemsComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:runOnItemsComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal-specific indexing instructions</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:journalSpecificIndex = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:journalSpecificIndex = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Journal-specific indexing comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:journalSpecificIndexComment/jss:p">
                                <xsl:for-each select="jss:journalSpecificIndexComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:journalSpecificIndexComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Style Printed Issue</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article title</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleTitle = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleTitle = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleTitle = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleTitle = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article dochead</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleDochead = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleDochead = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleDochead = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleDochead = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article section titles</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of issue section headings first order (H1)</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH1 = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH1 = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH1 = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH1 = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of issue section headings second order (H2)</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH2 = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH2 = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH2 = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapIssueH2 = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of special issue information</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapSI = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapSI = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapSI = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:stylePrintedIssue/jss:printCapSI = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Additional print style information</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:stylePrintedIssue/jss:additionalPrint/jss:p">
                                <xsl:for-each select="jss:stylePrintedIssue/jss:additionalPrint/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:stylePrintedIssue/jss:additionalPrint"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Style e-issue</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article title</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleTitle = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleTitle = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleTitle = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleTitle = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article dochead</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleDochead = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleDochead = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleDochead = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleDochead = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of article section titles</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleSectionTitle = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleSectionTitle = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleSectionTitle = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapArticleSectionTitle = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of issue section headings first order (H1)</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH1 = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH1 = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH1 = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH1 = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of issue section headings second order (H2)</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH2 = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH2 = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH2 = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapIssueH2 = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Capitalization of special issue information</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:eCapSI = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapSI = 1">
                                <font class="rhs"><xsl:text>sentence case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapSI = 2">
                                <font class="rhs"><xsl:text>Title case</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="jss:style-e-issue/jss:eCapSI = 3">
                                <font class="rhs"><xsl:text>All caps</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Additional electronic style information</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:style-e-issue/jss:additionalE/jss:p">
                                <xsl:for-each select="jss:style-e-issue/jss:additionalE/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:style-e-issue/jss:additionalE"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <th class="head3" colspan="2">Additional</th>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>Additional journal-specific typesetting requirements</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="jss:additionalJournalSpecificTypesetting/jss:p">
                                <xsl:for-each select="jss:additionalJournalSpecificTypesetting/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="jss:additionalJournalSpecificTypesetting"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
        </table>
        <br/>
    </xsl:template>
    
    <!-- Editor -->
    
    <xsl:template match="jss:journalStylesheet/jss:editors">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">EDITOR</th>
            </tr>
            <xsl:for-each select="jss:editor">
                <tr>
                    <td colspan="2"><font class="lhs-head"><xsl:text>Editor: </xsl:text></font><font class="rhs"><xsl:apply-templates select="jss:editorName"/></font></td>
                </tr>
                <tr>
                    <td width="30%"><font class="lhs"><xsl:text>Name of the editor</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:editorName"/></font></td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Email address</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:editorEmail"/></font></td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Receives proof?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:emailProof = 0">
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 1">
                            <font class="rhs"><xsl:text>All proofs</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 2">
                            <font class="rhs"><xsl:text>Some proofs (see comments)</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 3">
                            <font class="rhs"><xsl:text>Never</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Receives bcc proof?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:bccProof = 0">
                            <font class="rhs"><xsl:text>No</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:bccProof = 1">
                            <font class="rhs"><xsl:text>Yes</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs"><xsl:text>N/A</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Sends Corrections?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:sendsCorrections = 0">
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 1">
                            <font class="rhs"><xsl:text>Always</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 2">
                            <font class="rhs"><xsl:text>Sometimes</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 3">
                            <font class="rhs"><xsl:text>Never</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Comment</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:proofingComment/jss:p">
                            <xsl:for-each select="jss:proofingComment/jss:p">
                                <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs"><xsl:apply-templates select="jss:proofingComment"/></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
            </xsl:for-each>
        </table>
        <br/>
    </xsl:template>
    
    <!-- Print -->
    
    <xsl:template match="jss:journalStylesheet/jss:print">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">PRINT</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Multiple versions?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:multipleVersion = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:multipleVersion = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Multiple versions comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:multipleVersionComment/jss:p">
                        <xsl:for-each select="jss:multipleVersionComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:multipleVersionComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ons (glued on the front cover)</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipOns = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:tipOns = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ons comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipOnsComment/jss:p">
                        <xsl:for-each select="jss:tipOnsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:tipOnsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ins (bound in order cards)</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipIns = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:tipIns = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ins comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipInsComment/jss:p">
                        <xsl:for-each select="jss:tipInsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:tipInsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer per issue</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverPerIssue = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:coverPerIssue = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer per issue comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverPerIssueComment/jss:p">
                        <xsl:for-each select="jss:coverPerIssueComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:coverPerIssueComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer as stock</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverStock = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:coverStock = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer as stock comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverStockComment/jss:p">
                        <xsl:for-each select="jss:coverStockComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:coverStockComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Advance approval</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:advanceApproval = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:advanceApproval = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Advance approval comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:advanceApprovalComment/jss:p">
                        <xsl:for-each select="jss:advanceApprovalComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:advanceApprovalComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Other journal-specific printer instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:additionalPrinter/jss:p">
                        <xsl:for-each select="jss:additionalPrinter/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:additionalPrinter"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    
    <!-- Despatch -->
    
    <xsl:template match="jss:journalStylesheet/jss:despatch">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">DESPATCH</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Direct from printer</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:directPrinter"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special packaging</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:specialPackaging"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Priority</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:priority"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Other journal-specific despatch instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:otherJournalSpecificDespatch/jss:p">
                        <xsl:for-each select="jss:otherJournalSpecificDespatch/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:otherJournalSpecificDespatch"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- Editor -->
    
    <xsl:template match="jss:journalStylesheet/jss:editors">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">EDITOR</th>
            </tr>
            <xsl:for-each select="jss:editor">
                <tr>
                    <td colspan="2"><font class="lhs-head"><xsl:text>Editor: </xsl:text></font><font class="rhs"><xsl:apply-templates select="jss:editorName"/></font></td>
                </tr>
                <tr>
                    <td width="30%"><font class="lhs"><xsl:text>Name of the editor</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:editorName"/></font></td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Email address</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:editorEmail"/></font></td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Receives proof?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:emailProof = 0">
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 1">
                            <font class="rhs"><xsl:text>All proofs</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 2">
                            <font class="rhs"><xsl:text>Some proofs (see comments)</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:emailProof = 3">
                            <font class="rhs"><xsl:text>Never</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Receives bcc proof?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:bccProof = 0">
                            <font class="rhs"><xsl:text>No</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:bccProof = 1">
                            <font class="rhs"><xsl:text>Yes</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs"><xsl:text>N/A</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Sends Corrections?</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:sendsCorrections = 0">
                            <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 1">
                            <font class="rhs"><xsl:text>Always</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 2">
                            <font class="rhs"><xsl:text>Sometimes</xsl:text></font>
                        </xsl:when>
                        <xsl:when test="jss:sendsCorrections = 3">
                            <font class="rhs"><xsl:text>Never</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td><font class="lhs"><xsl:text>Comment</xsl:text></font></td>
                    <td><xsl:choose>
                        <xsl:when test="jss:proofingComment/jss:p">
                            <xsl:for-each select="jss:proofingComment/jss:p">
                                <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs"><xsl:apply-templates select="jss:proofingComment"/></font>
                        </xsl:otherwise>
                    </xsl:choose>
                    </td>
                </tr>
            </xsl:for-each>
        </table>
        <br/>
    </xsl:template>
    
    <!-- Print -->
    
    <xsl:template match="jss:journalStylesheet/jss:print">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">PRINT</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Multiple versions?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:multipleVersion = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:multipleVersion = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Multiple versions comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:multipleVersionComment/jss:p">
                        <xsl:for-each select="jss:multipleVersionComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:multipleVersionComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ons (glued on the front cover)</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipOns = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:tipOns = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ons comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipOnsComment/jss:p">
                        <xsl:for-each select="jss:tipOnsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:tipOnsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ins (bound in order cards)</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipIns = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:tipIns = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Tip ins comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:tipInsComment/jss:p">
                        <xsl:for-each select="jss:tipInsComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:tipInsComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer per issue</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverPerIssue = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:coverPerIssue = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer per issue comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverPerIssueComment/jss:p">
                        <xsl:for-each select="jss:coverPerIssueComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:coverPerIssueComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer as stock</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverStock = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:coverStock = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Cover printer as stock comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:coverStockComment/jss:p">
                        <xsl:for-each select="jss:coverStockComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:coverStockComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Advance approval</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:advanceApproval = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="jss:advanceApproval = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Advance approval comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:advanceApprovalComment/jss:p">
                        <xsl:for-each select="jss:advanceApprovalComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:advanceApprovalComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Other journal-specific printer instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:additionalPrinter/jss:p">
                        <xsl:for-each select="jss:additionalPrinter/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:additionalPrinter"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>
    
    <!-- Despatch -->
    
    <xsl:template match="jss:journalStylesheet/jss:despatch">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">DESPATCH</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Direct from printer</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:directPrinter"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Special packaging</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:specialPackaging"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Priority</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="jss:priority"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Other journal-specific despatch instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:otherJournalSpecificDespatch/jss:p">
                        <xsl:for-each select="jss:otherJournalSpecificDespatch/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="jss:otherJournalSpecificDespatch"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    
    <!-- Standard Texts -->
    
    <xsl:template match="jss:journalStylesheet/jss:standardText" name="stdtxt">
        <h2 align="center" class="head2">Standard Texts</h2>
        
        <h3 align="left" class="head3">Author enquiries</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:authorEnquires/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:authorEnquires/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:authorEnquires/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Language (Usage and Editing services)</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:enLanguageHelpService/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:enLanguageHelpService/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:enLanguageHelpService/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Reference to a complete Guide for Authors</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:refCompleteGfA/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:refCompleteGfA/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:refCompleteGfA/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">No page charges</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:noPageCharges/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:noPageCharges/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:noPageCharges/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Funding body agreements and policies</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:fundBdyAgreePol/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:fundBdyAgreePol/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:fundBdyAgreePol/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Advertising information</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:advertisingInformation/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:advertisingInformation/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:advertisingInformation/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Printers' indicia</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:printersIndicia/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:printersIndicia/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:printersIndicia/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Sponsored supplements and/or commercial reprints</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:sponSuppComRepr/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:sponSuppComRepr/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:sponSuppComRepr/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">USA mailing notice ('Dropshipment')</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:dropshipment/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:dropshipment/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:dropshipment/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">SciVerse ScienceDirect logo and text</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:choose>
            <xsl:when test="//jss:sciSDLogo/@presence = '0' ">
                <font class="head5">Ignore</font>
            </xsl:when>
            <xsl:when test="//jss:sciSDLogo/@presence = '1' ">
                <font class="head5">Required</font>
            </xsl:when>
            <xsl:otherwise>
                <font class="head5">N/A</font>
            </xsl:otherwise>
        </xsl:choose>
        
        <br/><h3 align="left" class="head3">Paper quality</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:paperQuality/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:paperQuality/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:paperQuality/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Orders, claims and product enquiries</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:ordersClaimsJournalEnquiries/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:ordersClaimsJournalEnquiries/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:ordersClaimsJournalEnquiries/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Publication (Subscription) information</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:publicationInformation/jss:sectiontitle">
            <font class="head4"><xsl:value-of select="//jss:publicationInformation/jss:sectiontitle"/><xsl:text>: </xsl:text></font>
        </xsl:if>
        <xsl:for-each select="//jss:publicationInformation/jss:p">
            <xsl:apply-templates select="."/><br/><br/>
        </xsl:for-each>
        
        <br/><h3 align="left" class="head3">Copyright, permissions, disclaimer</h3>
        <xsl:for-each select="//jss:copyPermDiscl">
            <xsl:if test="./jss:sectiontitle">
                <xsl:text>&#8195;</xsl:text>
                <font class="head4"><xsl:value-of select="./jss:sectiontitle"/><xsl:text>: </xsl:text></font>
            </xsl:if>            
            <xsl:for-each select="./jss:p">
                <xsl:apply-templates select="."/><br/><br/>
            </xsl:for-each>
        </xsl:for-each>
        
    </xsl:template>
 

    <!-- Other Instructions 
    
    <xsl:template match="jss:journalStylesheet/jss:otherInstructions">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">OTHER INSTRUCTIONS</th>
            </tr>
            <tr>
                <td width="30%"><font class="lhs"><xsl:text>Other instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="jss:p">
                        <xsl:for-each select="jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:value-of select="."/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
        </table>
    </xsl:template> 
    -->
</xsl:stylesheet>
