<?xml version="1.0" encoding="utf-8"?>
<!--    
    Elsevier stylesheet for journal stylesheets in XML format
    Version 2014.1p3

    Copyright (c) 2009-2014 Elsevier Ltd    
-->
<!--
    Permission to copy and distribute verbatim copies of this document is granted, 
    provided this notice is included in all copies, but changing it is not allowed. 
-->

<xsl:stylesheet version="1.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:jss="http://www.elsevier.com/xml/schema/journalStylesheets"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:strip-space elements="*"/>

    <xsl:output method="html" encoding="UTF-8"/>

    <xsl:template match="/">
        <html>
            <head>
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                <title>Journal Style Sheet</title>
                <!--<link href="http://elschnappd001:8080/ast/jss/tabs.css" rel="stylesheet" type="text/css"/>-->
                <style type="text/css">
                    .heading{
                        font-weight:bold;
                        color:#B8860B;
                        font-size:200%
                    }
                    .head2{
                        color:MediumVioletRed;
                        background-color: #D0D0D0;
                    }
                    .base_head2{
                    color:MediumVioletRed;
                    }
                    
                    .head3{
                        font-variant:small-caps;
                        color:#8B4513;
                        text-align:left;
                    }
                    .head4{
                        font-style:italic;
                        color:#C00000;
                        text-align:left;
                    }
                    .head5{
                        font-variant:small-caps;
                        font-weight:bold;
                        color:blue;
                        text-align:centre;
                    }
                    .lhs-head{
                        color:brown;
                        font-variant:small-caps;
                        font-weight:bold;
                    }
                    .lhs-thead{
                        color:#008B8B;
                        font-variant:small-caps;
                    }
                    .lhs{
                        color:#2F4F4F;
                    }
                    .rhs{
                        color:#A52A2A;
                    }
                    .rhs_nodata{
                    color:#FA5858;
                    }
                    .lhsb{
                        color:brown;
                        font-weight:bold;
                    }
                    .rhsb{
                        color:green;
                        font-weight:bold;
                    }
                    .tbl1{
                        color:#808080;
                    }
                    .rhs-wd{
                        color:red;
                        font-weight:bold;
                    }
                    .rhs-nd{
                        color:red;
                        font-style:italic;
                    }
                    body{
                        background-color:#FAFAD2;
                        font-family:'Cambria';
                        margin-left:10%;
                        margin-right:10%;
                    }</style>
            </head>
            <body onkeyup="keyDown=0" onkeydown="keyDown=1">
                <h2 align="center">
                    <table width="1000pt">
                        <tr>
                            <td align="left" width="1%">
                                <img src="http://www.elsevier.com/__data/assets/image/0003/177726/logo.gif"/>
                            </td>
                            <td align="center">
                                <font style="font-size:14pt; color:#B8860B; font-weight:bold">
                                    <xsl:text>Journal Style Sheet for the Journal</xsl:text><br/>
                                </font>
                                <font class="heading">
                                    <xsl:value-of select="jss:journalStylesheet/jss:baseData/jss:ptsData/jss:journalTitle"/>
                                </font>
                            </td>
                        </tr>
                    </table>
                </h2>
          <!--      <ol id="toc">
                    <li>
                        <a href="#main">
                            <span>MAIN</span>
                        </a>
                    </li>

                    <li>
                        <a href="#cu">
                            <span>C&amp;U</span>
                        </a>
                    </li>
                    <li>
                        <a href="#s0">
                            <span>S0</span>
                        </a>
                    </li>
                    <li>
                        <a href="#s100">
                            <span>S100</span>
                        </a>
                    </li>
                    <li>
                        <a href="#s200">
                            <span>S200</span>
                        </a>
                    </li>
                    <li>
                        <a href="#p100">
                            <span>P100</span>
                        </a>
                    </li>
                    <li>
                        <a href="#s300">
                            <span>S300</span>
                        </a>
                    </li>
                    <li>
                        <a href="#print">
                            <span>PRINT</span>
                        </a>
                    </li>
                    <li>
                        <a href="#despatch">
                            <span>DESPATCH</span>
                        </a>
                    </li>
                    <li>
                        <a href="#editors">
                            <span>PROOFING</span>
                        </a>
                    </li>
                    <li>
                        <a href="#stdtxt">
                            <span>STANDARD TEXTS</span>
                        </a>
                    </li>
                </ol>-->
                <div class="content" id="main">
                    <xsl:call-template name="main"/>
                </div>
                <!--<div class="content" id="pit">
                    <xsl:call-template name="pit"/>
                </div>-->
                <div class="content" id="cu">
                    <xsl:call-template name="cu"/>
                </div>
                <div class="content" id="s0">
                    <xsl:call-template name="s0"/>
                </div>
                <div class="content" id="s100">
                    <xsl:call-template name="s100"/>
                </div>
                <div class="content" id="s200">
                    <xsl:call-template name="s200"/>
                </div>
                <div class="content" id="p100">
                    <xsl:call-template name="p100"/>
                </div>
                <div class="content" id="s300">
                    <xsl:call-template name="s300"/>
                </div>
                <div class="content" id="print">
                    <xsl:call-template name="print"/>
                </div>
                <div class="content" id="despatch">
                    <xsl:call-template name="despatch"/>
                </div>
                <div class="content" id="editors">
                    <xsl:call-template name="editors"/>
                </div>
                <div class="content" id="stdtxt">
                    <xsl:call-template name="stdtxt"/>
                </div>
<!--                <script src="activatables.js" type="text/javascript"/>
                <script type="text/javascript">
                    activatables('page', ['main', 'cu', 's0', 's100', 's200', 'p100', 's300', 'editors', 'print', 'despatch', 'stdtxt']);
                </script>-->

                <table width="1000" border="1" style="margin-top:20pt;"><tr><td colspan="2" align="center" style="font-weight:bold;">Version 2014.1 (8-Jan-2014) | © 2014 Elsevier | Contact: Rob van Fucht <span style="font-size:14; font-weight:normal">(R.Fucht@elsevier.com)</span>, Gopinath Rajasekaran <span style="font-size:14; font-weight:normal">(g.rajasekaran@elsevier.com)</span>, <br/>Vanaja Sriram <span style="font-size:14; font-weight:normal">(v.sriram@elsevier.com)</span></td></tr></table>
              
            </body>
        </html>
    </xsl:template>

    <!-- Base Data -->

    <xsl:template match="jss:journalStylesheet/jss:baseData" name="main">
        
        
        
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">BASE DATA</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">PTS Data and JM Edit Data</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Journal code</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:ptsData/jss:journalCode"/>
                    </font>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhsb">
                        <xsl:text>Date of last PTS report</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhsb">
                        <xsl:apply-templates select="//jss:ptsData/jss:ptsReportDate"/>
                    </font>
                    <xsl:if test="//jss:ptsData/jss:ptsReportDate=''">
                        <font class="rhs_nodata">
                            <xsl:text>Contains no data</xsl:text>
                        </font>
                    </xsl:if>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhsb">
                        <xsl:text>Non-PTS data last modified on</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:dateModified=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhsb">
                                <xsl:apply-templates select="//jss:JM-EditData/jss:dateModified"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhsb">
                        <xsl:text>Modified by</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                    <xsl:when test="//jss:JM-EditData/jss:modifiedBy=''">
                        <font class="rhs_nodata">
                            <xsl:text>Contains no data</xsl:text>
                        </font>
                    </xsl:when>
                        <xsl:otherwise>
                            <font class="rhsb">
                                <xsl:apply-templates select="//jss:JM-EditData/jss:modifiedBy"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal number</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:ptsData/jss:journalNumber"/>
                    </font>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal title</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:ptsData/jss:journalTitle"/>
                    </font>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>PMG</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:pmg!=''">
                            <font class="rhs">
                            <xsl:value-of select="//jss:ptsData/jss:pmg"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:pmg=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>PMC</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:pmc!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:pmc"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:pmc=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>ISSN</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:issn!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:issn"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:issn=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>epGroup</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:epGroup!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:epGroup"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:epGroup=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Production site</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:productionSite!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:productionSite"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:productionSite=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal DOI code</xsl:text>
                    </font>
                </td>
                <td>
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalDOICode!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalDOICode"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalDOICode=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal DOI content type</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalDOIContentType!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalDOIContentType"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalDOIContentType=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>eSubmission content type</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:eSubmissionContentType!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:eSubmissionContentType"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:eSubmissionContentType=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Expiry date</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:expiryDate!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:expiryDate"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:expiryDate=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copyright Rem Exec</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyrightRemExec!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:copyrightRemExec"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightRemExec=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Colour Conf Rem Exec</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:colourConfRemExec!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:colourConfRemExec"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:colourConfRemExec=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Missing Items Rem Exec</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:missingItemsRemExec!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:missingItemsRemExec"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:missingItemsRemExec=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Author Proof Rem Exec</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:authorProofRemExec!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:authorProofRemExec"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:authorProofRemExec=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Editor Proof Rem Exec</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:editorProofRemExec!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:editorProofRemExec"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:editorProofRemExec=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Typeset Red PTS</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:typesetRedPTS!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:typesetRedPTS"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:typesetRedPTS=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Typeset Red Calc</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:typesetRedCalc!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:typesetRedCalc"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:typesetRedCalc=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Average item pages</xsl:text>
                    </font>
                </td>
                <td>
                   
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:averageItemPages!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:averageItemPages"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:averageItemPages=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Typeset model</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:typesetModel!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:typesetModel"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:typesetModel=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Typesetter</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:typesetter!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:typesetter"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:typesetter=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                  </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal manager</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalManager!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalManager"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalManager=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal LSM (PSP)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalLSM!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalLSM"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalLSM=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copyright holder</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'S'">
                            <font class="rhs">
                                <xsl:text>Society owned</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'E'">
                            <font class="rhs">
                                <xsl:text>Elsevier owned</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightHolder = 'J'">
                            <font class="rhs">
                                <xsl:text>Joint copyright ownership</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:ptsData/jss:copyrightHolder"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Society affiliation</xsl:text>
                    </font>
                </td>
                <td>
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:societyAffiliation!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:societyAffiliation"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:societyAffiliation=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copyright Statement</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyrightStatement!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:copyrightStatement"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightStatement=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Reference style</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:refStyle!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:refStyle"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:refStyle=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Free reference style at submission</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:freeReferenceStyle = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:freeReferenceStyle = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!-- <xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Reference style comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:referenceStyleComment/jss:p">
                            <xsl:for-each select="//jss:JM-EditData/jss:referenceStyleComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                                <xsl:if test="//jss:JM-EditData/jss:referenceStyleComment/jss:p=''">
                                    <font class="rhs_nodata"><xsl:text>Contains no data</xsl:text></font>
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:referenceStyleComment">
                            <font class="rhs_nodata"><xsl:text>Contains no data</xsl:text></font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:JM-EditData/jss:referenceStyleComment"/>
                            </font>
                        </xsl:otherwise>
                        
               
                    </xsl:choose>
                </td>
            </tr>
            <!--<xsl:if test="//jss:ptsData/jss:typesetModel!=EU5Gdc">-->
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Abbreviate journal name in reference</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:abbJNRef = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:abbJNRef = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <!--</xsl:if>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>OPCO</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:opco!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:opco"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:opco=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Printer</xsl:text>
                    </font>
                </td>
                <td>

                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:printer!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:printer"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:printer=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Print run</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:printRun!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:printRun"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:printRun=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Paper type inside</xsl:text>
                    </font>
                </td>
                <td>
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:paperTypeInside!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:paperTypeInside"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:paperTypeInside=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Paper type cover</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:paperTypeCover!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:paperTypeCover"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:paperTypeCover=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Print type</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:printType!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:printType"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:printType=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cover finishing</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:coverFinishing!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:coverFinishing"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:coverFinishing=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Bind Type</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:bindType!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:bindType"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:bindType=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Back margin</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:backMargin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:backMargin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:backMargin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Head margin</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:headMargin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:headMargin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:headMargin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Right-hand start</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:righthandStart = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:righthandStart = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Trim size</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:trimSize!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:trimSize"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:trimSize=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Time Based Publ Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:timeBasedPublInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:timeBasedPublInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Early Based Publ Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:earlyBasedPublInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:earlyBasedPublInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Fixed Plan Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:fixedPlanInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:fixedPlanInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Scan S0 required?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:scanS0 = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:scanS0 = 1">
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
                            <xsl:when test="//jss:JM-EditData/jss:scanS0Comment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:scanS0Comment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:scanS0Comment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CAP light plus journal?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:capLightPlus = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:capLightPlus = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>MF only?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:mfIndicator = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:mfIndicator = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>MF comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:mfIndicatorComment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:mfIndicatorComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:mfIndicatorComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copy-Edit Level</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyEditLevel!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:copyEditLevel"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyEditLevel=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Adv Copy Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:advCopyInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:advCopyInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>S5 required</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:s5Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:s5Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>S100 required</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:s100Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:s100Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>S200 required</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:s200Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:s200Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>S300 required</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:s300Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:s300Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CrossMark required for S5 stage?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:cmS5Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:cmS5Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CrossMark required for S100 stage?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:cmS100Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:cmS100Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CrossMark required for S200 stage?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:cmS200Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:cmS200Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CrossMark required for S250 stage?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:cmS250Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:cmS250Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>CrossMark required for S300 stage?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:cmS300Required = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:cmS300Required = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Author proof required</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:authorProofRequired = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:authorProofRequired = 1">
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
                            <xsl:when test="//jss:ptsData/jss:editorProofRequired = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:editorProofRequired = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Track Off Sys Labels Req</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:trackOffSysLabelsReq = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:trackOffSysLabelsReq = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Delta Label Run Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:deltaLabelRunInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:deltaLabelRunInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Language</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:ptsData/jss:language"/>
                    </font>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>e Suite Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:eSuiteInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:eSuiteInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>External Login</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:extLogin = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:extLogin = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>
                    <tr>
                        <td><font class="lhs"><xsl:text>External Login Comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:extLoginComment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:extLoginComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:extLoginComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional OP Paid Col</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:additionalOPPaidCol!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:additionalOPPaidCol"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:additionalOPPaidCol=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal OP Price List</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalOPPriceList!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalOPPriceList"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalOPPriceList=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal page charges</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalPageCharges!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalPageCharges"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalPageCharges=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal Paid Col Page Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalPaidColPageInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalPaidColPageInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copyright Tran Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyrightTranInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightTranInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copyright Tran Online Ind</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyrightTranOnlineInd = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyrightTranOnlineInd = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Default item PIT</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:defaultItemPIT!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:defaultItemPIT"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:defaultItemPIT=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Default issue PIT</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:defaultIssuePIT!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:defaultIssuePIT"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:defaultIssuePIT=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Default Item Prod</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:defaultItemProd!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:defaultItemProd"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:defaultItemProd=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Default Issue Prod</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:defaultIssueProd!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:defaultIssueProd"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:defaultIssueProd=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Alpha Journal Ind</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:alphaJournalInd = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:alphaJournalInd = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal Admin</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalAdmin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalAdmin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalAdmin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Int Journal Admin</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:intJournalAdmin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:intJournalAdmin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:intJournalAdmin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Copy editor</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:copyEditor!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:copyEditor"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:copyEditor=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal Team Manager</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalTeamManager!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:journalTeamManager"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalTeamManager=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Print LSM</xsl:text>
                    </font>
                </td>
                <td>
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:printLSM!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:printLSM"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:printLSM=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Binder</xsl:text>
                    </font>
                </td>
                <td>
                    
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:binder!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:binder"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:binder=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Issue sender</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:issueSender!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:issueSender"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:issueSender=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Offprint sender</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:offprintSender!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:offprintSender"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:offprintSender=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Back issue storage</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:backIssueStorage!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:backIssueStorage"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:backIssueStorage=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Offprint printer</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:offprintPrinter!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:offprintPrinter"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:offprintPrinter=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Offprint finisher</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:offprintFinisher!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:offprintFinisher"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:offprintFinisher=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Required ABP?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:ABP = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:ABP = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal Payment Online</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:journalPaymentOnline = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:journalPaymentOnline = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>E-Sub. Acronym (primary)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:eSubAcr!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:eSubAcr"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:eSubAcr=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>In-house MC</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:ptsData/jss:inHouseMC = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:ptsData/jss:inHouseMC = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Zero Warehousing</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:zeroWarehousing = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:zeroWarehousing = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Location of SLA document</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:slaLocation"/></font></td>
                    </tr>-->
            <!--<tr>
                        <td><font class="lhs"><xsl:text>SLA Comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:slaComment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:slaComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:slaComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                            </td>
                    </tr>-->
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Service level agreement</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:sla = 0">
                                <font class="rhs"><xsl:text>No</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:JM-EditData/jss:sla = 1">
                                <font class="rhs"><xsl:text>Yes</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose></td>
                    </tr>-->
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Who does the mastercopying?</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:mastercopying = 0">
                                <font class="rhs-nd"><xsl:text>N/A</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:JM-EditData/jss:mastercopying = 1">
                                <font class="rhs"><xsl:text>elsevier internal</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:JM-EditData/jss:mastercopying = 2">
                                <font class="rhs"><xsl:text>supplier/typesetter</xsl:text></font>
                            </xsl:when>
                            <xsl:when test="//jss:JM-EditData/jss:mastercopying = 3">
                                <font class="rhs"><xsl:text>other</xsl:text></font>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs-wd"><xsl:text>Not a valid data</xsl:text></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <!--<tr>
                        <td><font class="lhs"><xsl:text>Mastercopying comment</xsl:text></font></td>
                        <td><xsl:choose>
                            <xsl:when test="//jss:JM-EditData/jss:mastercopyingComment/jss:p">
                                <xsl:for-each select="//jss:JM-EditData/jss:mastercopyingComment/jss:p">
                                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise>
                                <font class="rhs"><xsl:apply-templates select="//jss:JM-EditData/jss:mastercopyingComment"/></font>
                            </xsl:otherwise>
                        </xsl:choose>
                        </td>
                    </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Approved non-standard production?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:approvedNonStandard = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:approvedNonStandard = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Approved non-standard comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:approvedNonStandardComment/jss:p">
                            <xsl:for-each
                                select="//jss:JM-EditData/jss:approvedNonStandardComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:approvedNonStandardComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:JM-EditData/jss:approvedNonStandardComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special Issues?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:specialIssues = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:specialIssues = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special issues comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:specialIssuesComment/jss:p">
                            <xsl:for-each select="//jss:JM-EditData/jss:specialIssuesComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:specialIssuesComment=''">
                                <font class="rhs_nodata">
                                    <xsl:text>Contains no data</xsl:text>
                                </font>
                            </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:JM-EditData/jss:specialIssuesComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>


            <!--            <xsl:template match="jss:journalStylesheet/jss:pit" name="pit">-->
            <!--                <table border="4" width="1000pt">-->
            <tr>
                <th class="base_head2" colspan="2">Doc and Section Heads</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Doc Head</th>
            </tr>
            <tr>
                <th width="30%" class="lhs-thead">Dochead PIT</th>
                <th class="lhs-thead">Dochead Description</th>
                
                <!--<th width="30%" class="lhs-thead">Dochead Expired</th>-->
            </tr>
            <xsl:for-each select="//jss:docSecInfo/jss:docHeads/jss:docHead">
                <xsl:if test="jss:docheadExpired = 0">
                    <tr>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:docheadPIT"/>
                            </font>
                        </td>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:docheadDescription"/>
                            </font>
                        </td>
                    </tr>
                </xsl:if>
            </xsl:for-each>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Dochead Comments</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:docheadComments/jss:p">
                            <xsl:for-each select="//jss:JM-EditData/jss:docheadComments/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        
                        <xsl:when test="//jss:JM-EditData/jss:docheadComments=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:JM-EditData/jss:docheadComments"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <!--</table>
                <br/>
                <table border="4" width="1000pt">-->
            <tr>
                <th class="head3" colspan="2">Section Head</th>
            </tr>
            <tr>
                <th width="30%" class="lhs-thead">Section Code</th>
                <th class="lhs-thead">Section Description</th>
            </tr>
            <xsl:for-each select="//jss:docSecInfo/jss:sectionHeads/jss:sectionHead">
                <xsl:if test="jss:sectionExpired = 0">
                    <tr>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:sectionCode"/>
                            </font>
                        </td>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:sectionDescription"/>
                            </font>
                        </td>
                    </tr>
                </xsl:if>
            </xsl:for-each>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Section Comments</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:JM-EditData/jss:sectionComments/jss:p">
                            <xsl:for-each select="//jss:JM-EditData/jss:sectionComments/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:JM-EditData/jss:sectionComments=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:JM-EditData/jss:sectionComments"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <!--                </table>-->
            <!--                <br/>-->
            <!--                <table border="4" width="1000pt">-->
            <tr>
                <th class="head3" colspan="2">CrossMark PITs</th>
            </tr>
            <tr>
                <td colspan="2">
                    
                    
                    
                    <xsl:text>&#8195;</xsl:text>
                    <xsl:for-each select="//jss:docSecInfo/jss:crossMark/jss:cmPIT">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/>
                            <xsl:text>&#8194;&#9830;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>
            </tr>
            <!--</table>
                <br/>
            </xsl:template>-->


            <tr>
                <th class="base_head2" colspan="2">PILOT</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Is the journal included in any Pilot</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:pilot/jss:pilotIncluded = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:pilot/jss:pilotIncluded = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Pilot Specific Information</xsl:text>
                    </font>
                </td>

                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:pilot/jss:pilotSpecificComment/jss:p">
                            <xsl:for-each select="//jss:pilot/jss:pilotSpecificComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:pilot/jss:pilotSpecificComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:pilot/jss:pilotSpecificComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>

            <tr>
                <th class="base_head2" colspan="2">ADDITIONAL INFO</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Additional information</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:otherInstructions/jss:p">
                            <xsl:for-each select="//jss:otherInstructions/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:otherInstructions=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:otherInstructions"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- S0 -->

    <xsl:template match="jss:journalStylesheet/jss:s0" name="s0">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S0</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Item receipt</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Scan S0 required?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:scanS0 = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:scanS0 = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Scan S0 comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:scanS0Comment/jss:p">
                            <xsl:for-each select="//jss:itemreceipt/jss:scanS0Comment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:scanS0Comment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:itemreceipt/jss:scanS0Comment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>External Login</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:extLogin = 0">
                        <font class="rhs">  
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:extLogin = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>External Login Comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:extLoginComment/jss:p">
                            <xsl:for-each select="//jss:itemreceipt/jss:extLoginComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:extLoginComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:itemreceipt/jss:extLoginComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Quick book in?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:quickBook = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:quickBook = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Quick book comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:quickBookComment/jss:p">
                            <xsl:for-each select="//jss:itemreceipt/jss:quickBookComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:itemreceipt/jss:quickBookComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:itemreceipt/jss:quickBookComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                <td><font class="lhs"><xsl:text>Items received via EES?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEES = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEES = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>EES comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEESComment/jss:p">
                        <xsl:for-each select="//jss:itemreceipt/jss:receivedEESComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:itemreceipt/jss:receivedEESComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Items received via Omega?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedOmega = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:itemreceipt/jss:receivedOmega = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Omega comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedOmegaComment/jss:p">
                        <xsl:for-each select="//jss:itemreceipt/jss:receivedOmegaComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:itemreceipt/jss:receivedOmegaComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Items received via Email?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEmail = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEmail = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Email comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedEmailComment/jss:p">
                        <xsl:for-each select="//jss:itemreceipt/jss:receivedEmailComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:itemreceipt/jss:receivedEmailComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Items received via FTP/Courier?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedFTP = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:itemreceipt/jss:receivedFTP = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>FTP comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:receivedFTPComment/jss:p">
                        <xsl:for-each select="//jss:itemreceipt/jss:receivedFTPComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:itemreceipt/jss:receivedFTPComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>Special Issues?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:itemreceipt/jss:specialIssues = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:itemreceipt/jss:specialIssues = 1">
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
                    <xsl:when test="//jss:itemreceipt/jss:specialIssuesComment/jss:p">
                        <xsl:for-each select="//jss:itemreceipt/jss:specialIssuesComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:itemreceipt/jss:specialIssuesComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Other item receipt instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:itemreceipt/jss:otherReceipt/jss:p">
                            <xsl:for-each select="//jss:itemreceipt/jss:otherReceipt/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        
                        <xsl:when test="//jss:itemreceipt/jss:otherReceipt=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:itemreceipt/jss:otherReceipt"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        <!--    <tr>
                <th class="head3" colspan="2">Correspondence</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Can supplier send correspondence to the author?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:correspondence/jss:correspondenceAuthor = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:correspondenceAuthor = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Correspondence author comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                            <xsl:for-each
                                select="//jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:correspondence/jss:correspondenceAuthorComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>What to do when an item is missing?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:correspondence/jss:missingItemCorrespondence = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:missingItemCorrespondence = 1">
                            <font class="rhs">
                                <xsl:text>Send "missing item" letter to the author</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:missingItemCorrespondence = 2">
                            <font class="rhs">
                                <xsl:text>Log a problem for the journal manager</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        <tr>
                <th class="head3" colspan="2">Offprints</th>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>No of paper offprints</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:paperOffprints"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>E-offprints?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:offprints/jss:eOffprint = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:offprints/jss:eOffprint = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Additional free paper offprints for paid colour</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:additionalFreePaper"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Extra journal specific offprint instructions</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:offprints/jss:extraOffprintInstructions/jss:p">
                        <xsl:for-each select="//jss:offprints/jss:extraOffprintInstructions/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:extraOffprintInstructions"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Number of free copies of issue to author</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:freeIssues"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Free Issues Comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:offprints/jss:freeIssuesComment/jss:p">
                        <xsl:for-each select="//jss:offprints/jss:freeIssuesComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:freeIssuesComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Number of free issues for special issues</xsl:text></font></td><td><font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:freeIssuesSI"/></font></td>
            </tr>
            <tr>
                <td><font class="lhs"><xsl:text>Free issues special issues comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:offprints/jss:freeIssuesSIComment/jss:p">
                        <xsl:for-each select="//jss:offprints/jss:freeIssuesSIComment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:offprints/jss:freeIssuesSIComment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <tr>
                <th class="head3" colspan="2">Additional information</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional information about the selection of sections</xsl:text>
                    </font>
                </td>
                <!-- <td><xsl:for-each select="//jss:section/jss:p|jss:section">
                    <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                </xsl:for-each>
                </td> -->
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:section!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:section"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:section=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional journal-specific login instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalLoginInstructions/jss:p">
                            <xsl:for-each select="//jss:additionalLoginInstructions/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalLoginInstructions=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:additionalLoginInstructions"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- PIT -->

    <xsl:template match="jss:journalStylesheet/jss:pit" name="pit">
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
            <xsl:for-each select="//jss:docHeads/jss:docHead">
                <xsl:if test="jss:docheadExpired = 0">
                    <tr>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:docheadPIT"/>
                            </font>
                        </td>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:docheadDescription"/>
                            </font>
                        </td>
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
                <td>
                    <font class="lhs">
                        <xsl:text>Dochead Comments</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:docHeads/jss:docheadComments/jss:p">
                            <xsl:for-each select="//jss:docHeads/jss:docheadComments/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:docHeads/jss:docheadComments=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:docHeads/jss:docheadComments"/>
                            </font>
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
            <xsl:for-each select="//jss:sectionHeads/jss:sectionHead">
                <xsl:if test="jss:sectionExpired = 0">
                    <tr>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:sectionCode"/>
                            </font>
                        </td>
                        <td>
                            <font class="tbl1">
                                <xsl:apply-templates select="jss:sectionDescription"/>
                            </font>
                        </td>
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
                <td>
                    <font class="lhs">
                        <xsl:text>Section Comments</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:sectionHeads/jss:sectionComments/jss:p">
                            <xsl:for-each select="//jss:sectionHeads/jss:sectionComments/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:sectionHeads/jss:sectionComments=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:sectionHeads/jss:sectionComments"
                                />
                            </font>
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
                <td colspan="2">
                    <xsl:text>&#8195;</xsl:text>
                    <xsl:for-each select="//jss:crossMark/jss:cmPIT">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/>
                            <xsl:text>&#8194;&#9830;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>


    <!-- C&U -->

    <xsl:template match="jss:journalStylesheet/jss:cu" name="cu">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="4">C&amp;U</th>
            </tr>
            <tr>
                <th class="head3" colspan="4">History</th>
            </tr>
            
                
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Is History Required</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:historyRequired = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyRequired = 1">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>

                        <xsl:when test="//jss:history/jss:historyRequired = 2">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>

                        <xsl:when test="//jss:history/jss:historyRequired = 3">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific article history text</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:journalSpecificHistoryText/jss:p">
                            <xsl:for-each
                                select="//jss:history/jss:journalSpecificHistoryText/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistoryText=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:history/jss:journalSpecificHistoryText"/>
                            </font>
                        </xsl:otherwise>
                        
                        
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td colspan="4"><font class="rhs">Regular Issue</font></td>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Received date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:received = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:received = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:received = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:received = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:received = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
        <!--                <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
   
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Revised date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:revised = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:revised = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:revised = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:revised = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:history/jss:regularIssue/jss:revised = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Accepted date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:accepted = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:accepted = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:accepted = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:regularIssue/jss:accepted = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:history/jss:regularIssue/jss:accepted = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        
                        
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Relevant PITS</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                    <xsl:when test="//jss:history/jss:regularIssue/jss:PITs_ri=''">
                        <font class="rhs_nodata">
                            <xsl:text>Contains no data</xsl:text>
                        </font>
                    </xsl:when>                        
                    </xsl:choose>
                <xsl:for-each select="//jss:history/jss:regularIssue/jss:PITs_ri/jss:PIT_ri">
                    <font class="tbl1">
                        <xsl:apply-templates select="."/>
                        <xsl:text>;&#8194;</xsl:text>
                    </font>
                </xsl:for-each>
                </td>    
            </tr>
            
            <tr>
                <td  colspan="4"><font class="rhs">Special Issue</font></td>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Special issue received date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:received_si = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:received_si = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:received_si = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:received_si = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:received_si = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special issue revised date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:revised_si = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:revised_si = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:revised_si = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:revised_si = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:revised_si = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special issue accepted date</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:accepted_si = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:accepted_si = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:accepted_si = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:accepted_si = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:accepted_si = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        
                        
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Relevant PITS</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:specialIssue/jss:PITs_si=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                    </xsl:choose>
                    <xsl:for-each select="//jss:history/jss:specialIssue/jss:PITs_si/jss:PIT_si">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/>
                            <xsl:text>;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>    
            </tr>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal specific history comments</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:journalSpecificHistoryComment/jss:p">
                            <xsl:for-each select="//jss:history/jss:journalSpecificHistoryComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistoryComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:history/jss:journalSpecificHistoryComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
    
       <!--<tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special issue history comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:specialIssueHistoryComment/jss:p">
                            <xsl:for-each
                                select="//jss:history/jss:specialIssueHistoryComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:history/jss:specialIssueHistoryComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific history</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 4">
                            <font class="rhs">
                                <xsl:text>Query JA if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:journalSpecificHistory = 5">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>-->
 
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>History dates for items linked to an item group</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:historyItemGroup = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyItemGroup = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyItemGroup = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyItemGroup = 3">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyItemGroup = 4">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>History item group comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:historyItemGroupComment/jss:p">
                            <xsl:for-each select="//jss:history/jss:historyItemGroupComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:historyItemGroupComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:history/jss:historyItemGroupComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Online publication date</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:onlinePubDate = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:onlinePubDate = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Online publication date comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:history/jss:onlinePubDateComment/jss:p">
                            <xsl:for-each select="//jss:history/jss:onlinePubDateComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:history/jss:onlinePubDateComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:history/jss:onlinePubDateComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Abstract</th>
            </tr>
            
            <tr>
                <td>
                <font class="lhs">
                    <xsl:text>Is abstract required</xsl:text>
                </font>
                    </td>
                <td colspan="3">
                <xsl:choose>
                    <xsl:when test="//jss:abstract/jss:abstractRequirement = 0">
                        <font class="rhs">
                            <xsl:text>No</xsl:text>
                        </font>
                    </xsl:when>
                    <xsl:when test="//jss:abstract/jss:abstractRequirement = 1">
                        <font class="rhs">
                            <xsl:text>Yes</xsl:text>
                        </font>
                    </xsl:when>
<!--                    <xsl:otherwise>
                        <font class="rhs">
                            <xsl:text>Not a valid data</xsl:text>
                        </font>
                    </xsl:otherwise>-->
                </xsl:choose>
                </td>
            </tr>
            <tr>
                <td  colspan="4">
                    <font class="rhs">
                        <xsl:text>PIT specific requirement</xsl:text>
                    </font>
                </td>
                </tr>

                <xsl:if test="//jss:abstractPITspecificRequirement/@action='1'">
            <tr>
                <td><font class="lhs"><xsl:text>Use if supplied</xsl:text></font></td>
                <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                    <font class="tbl1"><xsl:for-each select="//jss:abstractPITspecificRequirement[@action='1']/jss:abstractPITS/jss:abstractPIT">
                     <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                    </xsl:for-each></font>
                    <br/>
                    <font class="rhs"><b><xsl:text>Word Count: </xsl:text></b></font>
                    <font class="tbl1"><xsl:apply-templates select="//jss:abstractPITspecificRequirement[@action='1']/jss:PITspecificWordCount"/></font>
                    <br/>
                    <font class="rhs"><b><xsl:text>Structuring: </xsl:text></b></font>
                    <font class="tbl1">  <xsl:choose>
                        <xsl:when test="//jss:abstractPITspecificRequirement[@action='1']/jss:abstractStructure = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:abstractPITspecificRequirement[@action='1']/jss:abstractStructure = 1">
                            <font class="tbl1">
                                <xsl:text>Structured</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:abstractPITspecificRequirement[@action='1']/jss:abstractStructure = 2">
                            <font class="tbl1">
                                <xsl:text>Unstructured</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:abstractPITspecificRequirement[@action='1']/jss:abstractStructure = 3">
                            <font class="tbl1">
                                <xsl:text>Use as supplied</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    </font>
                    
                </td>
            </tr>
                </xsl:if>
            <xsl:if test="//jss:abstractPITspecificRequirement/@action='2'">
                <tr>
                    <td><font class="lhs"><xsl:text>Query JM if not supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1"><xsl:for-each select="//jss:abstractPITspecificRequirement[@action='2']/jss:abstractPITS/jss:abstractPIT">
                            <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                        <br/>
                        <font class="rhs"><b><xsl:text>Word Count: </xsl:text></b></font>
                        <font class="tbl1"><xsl:apply-templates select="//jss:abstractPITspecificRequirement[@action='2']/jss:PITspecificWordCount"/></font>
                        <br/>
                            <font class="rhs"><b><xsl:text>Structuring: </xsl:text></b></font>
                        <font class="tbl1">  <xsl:choose>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='2']/jss:abstractStructure = 0">
                                <font class="rhs-nd">
                                    <xsl:text>Option not selected</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='2']/jss:abstractStructure = 1">
                                <font class="tbl1">
                                    <xsl:text>Structured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='2']/jss:abstractStructure = 2">
                                <font class="tbl1">
                                    <xsl:text>Unstructured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='2']/jss:abstractStructure = 3">
                                <font class="tbl1">
                                    <xsl:text>Use as supplied</xsl:text>
                                </font>
                            </xsl:when>
                        </xsl:choose>
                        </font>
                    </td>
                </tr>
            </xsl:if>
            <xsl:if test="//jss:abstractPITspecificRequirement/@action='3'">
                <tr>
                    <td><font class="lhs"><xsl:text>Send missing items letter if not supplied </xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <xsl:for-each select="//jss:abstractPITspecificRequirement[@action='3']/jss:abstractPITS/jss:abstractPIT">
                            <font class="tbl1"><xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if></font>
                        </xsl:for-each>
                        <br/>
                        <font class="rhs"><b><xsl:text>Word Count: </xsl:text></b></font>
                        <font class="tbl1"><xsl:apply-templates select="//jss:abstractPITspecificRequirement[@action='3']/jss:PITspecificWordCount"/></font>
                        <br/>
                            <font class="rhs"><b><xsl:text>Structuring: </xsl:text></b></font>
                        <font class="tbl1">  <xsl:choose>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='3']/jss:abstractStructure = 0">
                                <font class="rhs-nd">
                                    <xsl:text>Option not selected</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='3']/jss:abstractStructure = 1">
                                <font class="tbl1">
                                    <xsl:text>Structured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='3']/jss:abstractStructure = 2">
                                <font class="tbl1">
                                    <xsl:text>Unstructured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='3']/jss:abstractStructure = 3">
                                <font class="tbl1">
                                    <xsl:text>Use as supplied</xsl:text>
                                </font>
                            </xsl:when>
                        </xsl:choose>
                        </font>
                    </td>
                </tr>
            </xsl:if>
            
            <xsl:if test="//jss:abstractPITspecificRequirement/@action='4'">
                <tr>
                    <td><font class="lhs"><xsl:text>Query author on AQF</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <xsl:for-each select="//jss:abstractPITspecificRequirement[@action='4']/jss:abstractPITS/jss:abstractPIT">
                            <font class="tbl1"><xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if></font>
                        </xsl:for-each><br/>
                        <font class="rhs"><b><xsl:text>Word Count: </xsl:text></b></font>
                        <font class="tbl1"><xsl:apply-templates select="//jss:abstractPITspecificRequirement[@action='4']/jss:PITspecificWordCount"/></font>
                        <br/>
                            <font class="rhs"><b><xsl:text>Structuring: </xsl:text></b></font>
                        <font class="tbl1">  <xsl:choose>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='4']/jss:abstractStructure = 0">
                                <font class="rhs-nd">
                                    <xsl:text>Option not selected</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='4']/jss:abstractStructure = 1">
                                <font class="tbl1">
                                    <xsl:text>Structured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='4']/jss:abstractStructure = 2">
                                <font class="tbl1">
                                    <xsl:text>Unstructured</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="//jss:abstractPITspecificRequirement[@action='4']/jss:abstractStructure = 3">
                                <font class="tbl1">
                                    <xsl:text>Use as supplied</xsl:text>
                                </font>
                            </xsl:when>
                        </xsl:choose>
                        </font>
                        
                    </td>
                </tr>
            </xsl:if>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific abstract requirements comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:abstract/jss:abstractComment/jss:p">
                            <xsl:for-each
                                select="//jss:abstract/jss:abstractComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:abstract/jss:abstractComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:abstract/jss:abstractComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
                
            <tr>
                <th class="head3" colspan="4">Graphical Abstract</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific graphical abstract requirements</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when
                            test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 1">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when
                            test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 2">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstract = 2">
                        <font class="rhs"><xsl:text>Mandatory</xsl:text></font>
                    </xsl:when>-->
 <!--                       <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific graphical abstract requirements comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when
                            test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment/jss:p">
                            <xsl:for-each
                                select="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:graphicalAbstract/jss:journalSpecificGraphicalAbstractComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>GA Format if Colour</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 1">
                            <font class="rhs">
                                <xsl:text>E-ONLY-COLOUR</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 2">
                            <font class="rhs">
                                <xsl:text>COLOUR-IN-PRINT billing No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:gaFormat = 3">
                            <font class="rhs">
                                <xsl:text>COLOUR</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific GA TOC</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 1">
                            <font class="rhs">
                                <xsl:text>E-only</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 2">
                            <font class="rhs">
                                <xsl:text>Print only</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOC = 3">
                            <font class="rhs">
                                <xsl:text>Electronic and Print</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific GA TOC comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when
                            test="//jss:graphicalAbstract/jss:journalSpecificGATOCComment/jss:p">
                            <xsl:for-each
                                select="//jss:graphicalAbstract/jss:journalSpecificGATOCComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATOCComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:graphicalAbstract/jss:journalSpecificGATOCComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific GA Title Page</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePage = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePage = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific GA Title Page comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when
                            test="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment/jss:p">
                            <xsl:for-each
                                select="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:graphicalAbstract/jss:journalSpecificGATitlePageComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Keywords</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Keywords requirement</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:keywords/jss:keywordRequirement = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordRequirement = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            
            
            <tr>
                <td  colspan="4">
                    <font class="rhs">
                        <xsl:text>Keyword PIT specific requirement</xsl:text>
                    </font>
                </td>
            </tr>
     
            
            
            <xsl:if test="//jss:keywordPITspecificRequirement/@action='1'">
                <tr>
                    <td><font class="lhs"><xsl:text>Use if supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1"><xsl:for-each select="//jss:keywordPITspecificRequirement[@action='1']/jss:keywordPITs/jss:keywordPIT">
                            <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                    </td>
                </tr>
            </xsl:if>
            <xsl:if test="//jss:keywordPITspecificRequirement/@action='2'">
                <tr>
                    <td><font class="lhs"><xsl:text>Query JM if not supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1"><xsl:for-each select="//jss:keywordPITspecificRequirement[@action='2']/jss:keywordPITs/jss:keywordPIT">
                            <xsl:apply-templates/>
                            <xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                    </td>
                </tr>
            </xsl:if>
            
            
            <xsl:if test="//jss:keywordPITspecificRequirement/@action='3'">
                <tr>
                    <td><font class="lhs"><xsl:text>Send missing items letter if not supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1"><xsl:for-each select="//jss:keywordPITspecificRequirement[@action='3']/jss:keywordPITs/jss:keywordPIT">
                            <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                    </td>
                </tr>
            </xsl:if>
            
            <xsl:if test="//jss:keywordPITspecificRequirement/@action='4'">
                <tr>
                    <td><font class="lhs"><xsl:text>Query author on AQF if not supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1">
                        <xsl:for-each select="//jss:keywordPITspecificRequirement[@action='4']/jss:keywordPITs/jss:keywordPIT">
                            <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                    </td>
                </tr>
            </xsl:if>
            
            <xsl:if test="//jss:keywordPITspecificRequirement/@action='5'">
                <tr>
                    <td><font class="lhs"><xsl:text>Do not use even if supplied</xsl:text></font></td>
                    <td><font class="rhs"><b><xsl:text>PITs: </xsl:text></b></font>
                        <font class="tbl1">
                        <xsl:for-each select="//jss:keywordPITspecificRequirement[@action='5']/jss:keywordPITs/jss:keywordPIT">
                            <xsl:apply-templates/><xsl:if test="position()!=last()"><xsl:text>; </xsl:text></xsl:if>
                        </xsl:for-each></font>
                    </td>
                </tr>
            </xsl:if>

         
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Keyword source</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:keywords/jss:keywordSource = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordSource = 1">
                            <font class="rhs">
                                <xsl:text>Free text</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordSource = 2">
                            <font class="rhs">
                                <xsl:text>Journal-specific list (add details to comments field)</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            
            
            <tr>
                <td  colspan="4">
                    <font class="rhs">
                        <xsl:text>Keyword count requirement</xsl:text>
                    </font>
                </td>
            </tr>
            
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Minimum number of keywords</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:keywords/jss:keywordCount/jss:keywordsMin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:keywords/jss:keywordCount/jss:keywordsMin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordCount/jss:keywordsMin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Maximum number of keywords</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:keywords/jss:keywordCount/jss:keywordsMax!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:keywords/jss:keywordCount/jss:keywordsMax"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordCount/jss:keywordsMax=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific keywords requirements comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:keywords/jss:keywordComments/jss:p">
                            <xsl:for-each
                                select="//jss:keywords/jss:keywordComments/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:keywords/jss:keywordComments=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:keywords/jss:keywordComments"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Highlights</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific highlights requirements</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 0">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 1">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:when test="//jss:highlights/jss:journalSpecificHighlights = 2">
                        <font class="rhs"><xsl:text>Mandatory</xsl:text></font>
                    </xsl:when>-->
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific highlights requirements comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:highlights/jss:journalSpecificHighlightsComment/jss:p">
                            <xsl:for-each
                                select="//jss:highlights/jss:journalSpecificHighlightsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:highlights/jss:journalSpecificHighlightsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:highlights/jss:journalSpecificHighlightsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Classification</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Classification requirements</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 1">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 2">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 3">
                            <font class="rhs">
                                <xsl:text>Send missing item letter if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 4">
                            <font class="rhs">
                                <xsl:text>Query author on AQF if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classification/jss:classificationRequirement = 5">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Classification source</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:classification/jss:classificationSource"
                        />
                    </font>
                </td>
            </tr>-->
            
            <tr>
                <td  colspan="4">
                    <font class="rhs">
                        <xsl:text>Classification Count</xsl:text>
                    </font>
                </td>
            </tr>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Minimum number of classification codes</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:classificationCount/jss:classificationMin!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:classificationCount/jss:classificationMin"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classificationCount/jss:classificationMin=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Maximum number of classification codes</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    
                    <xsl:choose>
                        <xsl:when test="//jss:classificationCount/jss:classificationMax!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:classificationCount/jss:classificationMax"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:classificationCount/jss:classificationMax=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Classification Comments</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when
                            test="//jss:classification/jss:classificationComment/jss:p">
                            <xsl:for-each
                                select="//jss:classification/jss:classificationComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        
                        <xsl:when test="//jss:classification/jss:classificationComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:classification/jss:classificationComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Colour</th>
            </tr>
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Waiving instruction for colour figures in print</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:colourFiguresInPrint = 0">
                            <font class="rhs">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:colourFiguresInPrint = 1">
                            <font class="rhs">
                                <xsl:text>Free color for all figures</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:colour/jss:colourFiguresInPrint = 2">
                            <font class="rhs">
                                <xsl:text>E-only journals</xsl:text>
                            </font>
                        </xsl:when>

                        <xsl:when test="//jss:colour/jss:colourFiguresInPrint = 3">
                            <font class="rhs">
                                <xsl:text>Editor/Author/Society decision</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:colour/jss:colourFiguresInPrint = 4">
                            <font class="rhs">
                                <xsl:text>Others</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>

            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Paid colour per page?</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:printedColour = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:printedColour = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Free Printed Colour</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:freePrintedColour = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:freePrintedColour = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>No Free Web Colour</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:noFreeWebColour = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:noFreeWebColour = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>First unit free of charge?</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:firstPageFOC = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:firstPageFOC = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>First unit free of charge comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:firstPageFOCComment/jss:p">
                            <xsl:for-each select="//jss:colour/jss:firstPageFOCComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:firstPageFOCComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:colour/jss:firstPageFOCComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cost for first colour unit</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:costFirstColour!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:colour/jss:costFirstColour"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:costFirstColour=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cost for next colour unit</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:costNextColour!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:colour/jss:costNextColour"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:costNextColour=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Currency Code</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:currencyCode!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:colour/jss:currencyCode"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:currencyCode=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Colour artwork exceptions</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:colourArtworkExceptions/jss:p">
                            <xsl:for-each select="//jss:colour/jss:colourArtworkExceptions/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                            <xsl:if test="//jss:colour/jss:colourArtworkExceptions/jss:p=''">
                                <font class="rhs_nodata">
                                    <xsl:text>Contains no data</xsl:text>
                                </font>
                            </xsl:if>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:colourArtworkExceptions=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
         
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:colour/jss:colourArtworkExceptions"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Which colour figure letter to the author?</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 1">
                            <font class="rhs">
                                <xsl:text>Colour letter</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:colourFiguresCorrespondence = 2">
                            <font class="rhs">
                                <xsl:text>Colour confirmation letter</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Extra journal-specific colour instructions</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:extraColourInstructions/jss:p">
                            <xsl:for-each select="//jss:colour/jss:extraColourInstructions/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:extraColourInstructions=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:colour/jss:extraColourInstructions"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>-->
            
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Colour Comments</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:colour/jss:colourComment/jss:p">
                            <xsl:for-each
                                select="//jss:colour/jss:colourComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:colour/jss:colourComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:colour/jss:colourComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            
            <tr>
                <th class="head3" colspan="4">Author Correspondence</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Can suppliers contact author directly?</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    
                    <xsl:choose>
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 1">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Forms</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 2">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Missing Item</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 3">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Others</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 4">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Forms and Missing Item</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 5">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Forms and Others</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 6">
                            <font class="rhs">
                                <xsl:text>Yes, contact author for Missing Item and Others</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:correspondence/jss:contactAuthor = 7">
                            <font class="rhs">
                                <xsl:text>No, contact JM</xsl:text>
                            </font>
                        </xsl:when>
<!--                       <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>
-->                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific Author correspondence comments</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                            <xsl:for-each
                                select="//jss:correspondence/jss:correspondenceAuthorComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:correspondence/jss:correspondenceAuthorComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:correspondence/jss:correspondenceAuthorComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="4">Offprints and free issues</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>No of paper offprints</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:paperOffprints!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:paperOffprints"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:paperOffprints=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>E-offprints?</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:eOffprint = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:eOffprint = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional free paper offprints for paid colour</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:additionalFreePaper!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:additionalFreePaper"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:additionalFreePaper=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Extra journal specific offprint instructions</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:offprints/jss:extraOffprintInstructions/jss:p">
                            <xsl:for-each
                                select="//jss:offprints/jss:extraOffprintInstructions/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:offprints/jss:extraOffprintInstructions=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:offprints/jss:extraOffprintInstructions"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Number of free copies of issue to author</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:freeIssues!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:freeIssues"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:freeIssues=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Free Issues Comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:offprints/jss:freeIssuesComment/jss:p">
                            <xsl:for-each select="//jss:offprints/jss:freeIssuesComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:offprints/jss:freeIssuesComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:offprints/jss:freeIssuesComment"
                                />
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Number of free issues for special issues</xsl:text>
                    </font>
                </td>
                
                

                <td colspan="3">
                    
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:freeIssuesSI!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:ptsData/jss:freeIssuesSI"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:freeIssuesSI=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>

                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Free issues special issues comment</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:offprints/jss:freeIssuesSIComment/jss:p">
                            <xsl:for-each select="//jss:offprints/jss:freeIssuesSIComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:offprints/jss:freeIssuesSIComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:offprints/jss:freeIssuesSIComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>

            <!--<tr>
                <th class="head3" colspan="4">Open Access</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Open Access type</xsl:text>
                    </font>
                </td>
                <td colspan="3">
                    <xsl:choose>
                        <xsl:when test="//jss:OA/jss:openAccessType = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:OA/jss:openAccessType = 1">
                            <font class="rhs">
                                <xsl:text>Full open access journal - Automated</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:OA/jss:openAccessType = 2">
                            <font class="rhs">
                                <xsl:text>Full open access journal - Manual</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:OA/jss:openAccessType = 3">
                            <font class="rhs">
                                <xsl:text>Open access item</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>-->

        </table>
		      <br/>
    </xsl:template>


    <!-- S100 -->

    <xsl:template match="jss:journalStylesheet/jss:s100" name="s100">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S100</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Heading</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Numbering style</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:heading/jss:numberingStyle = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:heading/jss:numberingStyle = 1">
                            <font class="rhs">
                                <xsl:text>Numbered</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:heading/jss:numberingStyle = 2">
                            <font class="rhs">
                                <xsl:text>UnNumbered</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:heading/jss:numberingStyle = 3">
                            <font class="rhs">
                                <xsl:text>Non-Standard, see comment</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Numbering style comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:heading/jss:NSComment/jss:p">
                            <xsl:for-each select="//jss:heading/jss:NSComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        
                        <xsl:when test="//jss:heading/jss:NSComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:heading/jss:NSComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Sections in small font</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Motto" paragraph</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:motto = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:motto = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Motto comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:mottoComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:mottoComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:mottoComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:mottoComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Case Report" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:caseReport = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:caseReport = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Case report comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:caseReportComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:caseReportComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:caseReportComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:caseReportComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Note added in proof" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:noteinProof = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:noteinProof = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Note added in proof comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:noteinProofComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:noteinProofComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:noteinProofComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:noteinProofComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Materials used" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:materialsMethods = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:materialsMethods = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Materials used" comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:materialsMethodsComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:materialsMethodsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:materialsMethodsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:roles/jss:materialsMethodsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Results" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:results = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:results = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Results comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:resultsComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:resultsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:resultsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:resultsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Introduction" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:introSec = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:introSec = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Introduction comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:introSecComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:introSecComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:introSecComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:introSecComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Background" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:background = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:background = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Background comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:backgroundComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:backgroundComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:backgroundComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:backgroundComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Conclusion/Conclusions" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:conclusion = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:conclusion = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Conclusion/Conclusions comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:conclusionComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:conclusionComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:conclusionComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:conclusionComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Discussion" section</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:discussion = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:discussion = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>"Discussion" comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:roles/jss:discussionComment/jss:p">
                            <xsl:for-each select="//jss:roles/jss:discussionComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:roles/jss:discussionComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:roles/jss:discussionComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Introduction</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Introduction</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:intro/jss:introduction = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:intro/jss:introduction = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>

            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Title Requirement</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:intro/jss:titlereq = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:intro/jss:titlereq = 1">
                            <font class="rhs">
                                <xsl:text>Use as supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:intro/jss:titlereq = 2">
                            <font class="rhs">
                                <xsl:text>Rename as Background</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:intro/jss:titlereq = 3">
                            <font class="rhs">
                                <xsl:text>Rename as Summary</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>PIT List</xsl:text>
                    </font>
                </td>
                <td>
                    <!--<xsl:text>&#8195;</xsl:text>-->
    
                    <xsl:choose>
                        <xsl:when test="//jss:intro/jss:introPITs=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                    <xsl:for-each select="//jss:intro/jss:introPITs/jss:introPIT">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/>
                            <xsl:text>;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>
            </tr>

            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Introduction comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:intro/jss:introductionComment/jss:p">
                            <xsl:for-each select="//jss:intro/jss:introductionComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:intro/jss:introductionComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:intro/jss:introductionComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Conflict of Interest</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Conflict of Interest</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:CoI/jss:ConflictInterest = 0">
                            <font class="rhs">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:CoI/jss:ConflictInterest = 1">
                            <font class="rhs">
                                <xsl:text>Use if Supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:CoI/jss:ConflictInterest = 2">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:CoI/jss:ConflictInterest = 3">
                            <font class="rhs">
                                <xsl:text>Query author on AQF if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:when test="//jss:CoI/jss:ConflictInterest = 4">
                            <font class="rhs">
                                <xsl:text>Query JM if not supplied</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>PIT List</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:CoI/jss:CoIPITs=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>                        
                    </xsl:choose>
                    <!--<xsl:text>&#8195;</xsl:text>-->
                    <xsl:for-each select="//jss:CoI/jss:CoIPITs/jss:CoIPIT">
                        <font class="tbl1">
                            <xsl:apply-templates select="."/>
                            <xsl:text>;&#8194;</xsl:text>
                        </font>
                    </xsl:for-each>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Conflict of Interest comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:CoI/jss:CoIComment/jss:p">
                            <xsl:for-each select="//jss:CoI/jss:CoIComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:CoI/jss:CoIComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:CoI/jss:CoIComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Biographical Information</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Required author details</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 1">
                            <font class="rhs">
                                <xsl:text>Author biography only</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 2">
                            <font class="rhs">
                                <xsl:text>Author photograph only</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 3">
                            <font class="rhs">
                                <xsl:text>Author biography and photograph</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 4">
                            <font class="rhs">
                                <xsl:text>Use if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 5">
                            <font class="rhs">
                                <xsl:text>Do not use even if supplied</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:authorDetails = 6">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Author details comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:bioInfo/jss:ADComment/jss:p">
                            <xsl:for-each select="//jss:bioInfo/jss:ADComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:bioInfo/jss:ADComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:bioInfo/jss:ADComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Non-standard label requirements</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Non-standard Fig./Table/Textbox label requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:floatLabel/jss:NSFloatLabel/jss:p">
                            <xsl:for-each select="//jss:floatLabel/jss:NSFloatLabel/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:floatLabel/jss:NSFloatLabel=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:floatLabel/jss:NSFloatLabel"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Non-Standard artwork requirements</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific artwork requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:artwork/jss:journalSpecificArtwork = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:artwork/jss:journalSpecificArtwork = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific artwork requirements comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:artwork/jss:journalSpecificArtworkComment/jss:p">
                            <xsl:for-each
                                select="//jss:artwork/jss:journalSpecificArtworkComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:artwork/jss:journalSpecificArtworkComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:artwork/jss:journalSpecificArtworkComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Latex</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Latex journal</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:latexInfo/jss:latex = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:latexInfo/jss:latex = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Proportion of manuscripts arriving in LaTeX </xsl:text>
                    </font>
                </td>
                <td>
                    
                    <xsl:choose>
                        <xsl:when test="//jss:latexInfo/jss:latexFrequency!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:latexInfo/jss:latexFrequency"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:latexInfo/jss:latexFrequency=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Latex comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:latexInfo/jss:latexComment/jss:p">
                            <xsl:for-each select="//jss:latexInfo/jss:latexComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:latexInfo/jss:latexComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:latexInfo/jss:latexComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            
            <tr>
                <tr>
                    <th class="head3" colspan="2">Additional information</th>
                </tr>
                
                <td>
                    <font class="lhs">
                        <xsl:text>Additional journal-specific S100 typesetting requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalS100/jss:p">
                            <xsl:for-each select="//jss:additionalS100/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalS100=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:additionalS100"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- S200 -->

    <xsl:template match="jss:journalStylesheet/jss:s200" name="s200">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S200</th>
            </tr>
            <tr>
                <th class="head3" colspan="2">Mastercopy</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>In-house MC</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:ptsData/jss:inHouseMC = 0">
                            <font class="rhs">
                                <xsl:text>supplier/typesetter</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:ptsData/jss:inHouseMC = 1">
                            <font class="rhs">
                                <xsl:text>elsevier internal</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Mastercopying comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:mc/jss:mastercopyingComment/jss:p">
                            <xsl:for-each select="//jss:mc/jss:mastercopyingComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:mc/jss:mastercopyingComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:mc/jss:mastercopyingComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <!--<tr>
                <td width="30%"><font class="lhs"><xsl:text>VIP in S200?</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:vip/jss:vipS200 = 0">
                        <font class="rhs"><xsl:text>No</xsl:text></font>
                    </xsl:when>
                    <xsl:when test="//jss:vip/jss:vipS200 = 1">
                        <font class="rhs"><xsl:text>Yes</xsl:text></font>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:text>N/A</xsl:text></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <!--<tr>
                <td><font class="lhs"><xsl:text>VIP S200 comment</xsl:text></font></td>
                <td><xsl:choose>
                    <xsl:when test="//jss:vip/jss:vipS200Comment/jss:p">
                        <xsl:for-each select="//jss:vip/jss:vipS200Comment/jss:p">
                            <font class="rhs"><xsl:apply-templates select="."/><br/></font>
                        </xsl:for-each>
                    </xsl:when>
                    <xsl:otherwise>
                        <font class="rhs"><xsl:apply-templates select="//jss:vip/jss:vipS200Comment"/></font>
                    </xsl:otherwise>
                </xsl:choose>
                </td>
            </tr>-->
            <tr>
                <th class="head3" colspan="2">Additional information</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional journal-specific S200 typesetting requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalS200/jss:p">
                            <xsl:for-each select="//jss:additionalS200/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalS200=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:additionalS200"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- P100 -->

    <xsl:template match="jss:journalStylesheet/jss:p100" name="p100">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">P100</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Delivery requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:deliveryRequirements = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:deliveryRequirements = 1">
                            <font class="rhs">
                                <xsl:text>Cover and Prelims only</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:deliveryRequirements = 2">
                            <font class="rhs">
                                <xsl:text>Cover to cover</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:deliveryRequirements = 3">
                            <font class="rhs">
                                <xsl:text>Other, see comments</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional journal-specific P100 typesetting requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalP100/jss:p">
                            <xsl:for-each select="//jss:additionalP100/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalP100=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:additionalP100"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- S300 -->

    <xsl:template match="jss:journalStylesheet/jss:s300" name="s300">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">S300</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Journal-specific cover artwork requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:journalSpecificCover = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificCover = 1">
                            <font class="rhs">
                                <xsl:text>Standard</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificCover = 2">
                            <font class="rhs">
                                <xsl:text>Different cover image for each issue</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificCover = 3">
                            <font class="rhs">
                                <xsl:text>Different cover image for each volume</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificCover = 4">
                            <font class="rhs">
                                <xsl:text>Other (see comments)</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal specific cover comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:journalSpecificCoverComment/jss:p">
                            <xsl:for-each select="//jss:journalSpecificCoverComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificCoverComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:journalSpecificCoverComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Different cover for special issues?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:specialIssueCover = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:specialIssueCover = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special issue cover comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:specialIssueCoverComment/jss:p">
                            <xsl:for-each select="//jss:specialIssueCoverComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:specialIssueCoverComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:specialIssueCoverComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Run-on items?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:runOnItems = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItems = 1">
                            <font class="rhs">
                                <xsl:text>None</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItems = 2">
                            <font class="rhs">
                                <xsl:text>Book reviews</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItems = 3">
                            <font class="rhs">
                                <xsl:text>Correspondence</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItems = 4">
                            <font class="rhs">
                                <xsl:text>Book reviews and correspondence</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItems = 5">
                            <font class="rhs">
                                <xsl:text>Other (see comments)</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Run-on items comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:runOnItemsComment/jss:p">
                            <xsl:for-each select="//jss:runOnItemsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:runOnItemsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:runOnItemsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific indexing instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:journalSpecificIndex = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificIndex = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific indexing comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:journalSpecificIndexComment/jss:p">
                            <xsl:for-each select="//jss:journalSpecificIndexComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:journalSpecificIndexComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:journalSpecificIndexComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Style Printed Issue</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article title</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleTitle = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleTitle = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleTitle = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleTitle = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article dochead</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleDochead = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleDochead = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleDochead = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleDochead = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article section titles</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapArticleSectionTitle = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of issue section headings first order (H1)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH1 = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH1 = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH1 = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH1 = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of issue section headings second order (H2)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH2 = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH2 = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH2 = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapIssueH2 = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of special issue information</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapSI = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapSI = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapSI = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:printCapSI = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional print style information</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:stylePrintedIssue/jss:additionalPrint/jss:p">
                            <xsl:for-each select="//jss:stylePrintedIssue/jss:additionalPrint/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:stylePrintedIssue/jss:additionalPrint=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:stylePrintedIssue/jss:additionalPrint"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Style e-issue</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article title</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleTitle = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleTitle = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleTitle = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleTitle = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article dochead</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleDochead = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleDochead = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleDochead = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleDochead = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of article section titles</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleSectionTitle = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleSectionTitle = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleSectionTitle = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapArticleSectionTitle = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of issue section headings first order (H1)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH1 = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH1 = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH1 = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH1 = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of issue section headings second order (H2)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH2 = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH2 = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH2 = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapIssueH2 = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Capitalization of special issue information</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:eCapSI = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapSI = 1">
                            <font class="rhs">
                                <xsl:text>sentence case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapSI = 2">
                            <font class="rhs">
                                <xsl:text>Title case</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:eCapSI = 3">
                            <font class="rhs">
                                <xsl:text>All caps</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional electronic style information</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:style-e-issue/jss:additionalE/jss:p">
                            <xsl:for-each select="//jss:style-e-issue/jss:additionalE/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:style-e-issue/jss:additionalE=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:style-e-issue/jss:additionalE"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <th class="head3" colspan="2">Additional information</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Additional journal-specific typesetting requirements</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalJournalSpecificTypesetting/jss:p">
                            <xsl:for-each select="//jss:additionalJournalSpecificTypesetting/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalJournalSpecificTypesetting=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:additionalJournalSpecificTypesetting"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- PROOFING -->

    <xsl:template match="jss:journalStylesheet/jss:editors" name="editors">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">PROOFING</th>
            </tr>
            
            <tr>
                <th class="head3" colspan="2">Proofing</th>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Journal-specific proofing instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofing = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofing = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Send proofs to</xsl:text>
                    </font>
                </td>
                <td>
                    <font class="rhs">
                        <xsl:apply-templates select="//jss:proofing/jss:proofingGeneric/jss:proofsSentTo"/>
                    </font>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Proofs send to comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofingComment/jss:p">
                            <xsl:for-each
                                select="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofingComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofingComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:proofing/jss:proofingGeneric/jss:journalSpecificProofingComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Text of PDF proof email</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 0">
                            <font class="rhs-nd">
                                <xsl:text>Option not selected</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 1">
                            <font class="rhs">
                                <xsl:text>Standard (English) text</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 2">
                            <font class="rhs">
                                <xsl:text>Use standard letter in language of article</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 3">
                            <font class="rhs">
                                <xsl:text>Standard text (French translation)</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 4">
                            <font class="rhs">
                                <xsl:text>Standard text (German translation)</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 5">
                            <font class="rhs">
                                <xsl:text>Standard text (Italian translation)</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 6">
                            <font class="rhs">
                                <xsl:text>Standard text (Spanish translation)</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofText = 7">
                            <font class="rhs">
                                <xsl:text>Non-standard (see comments)</xsl:text>
                            </font>
                        </xsl:when>
<!--                        <xsl:otherwise>
                            <font class="rhs-wd">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Text of PDF proof email comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofTextComment/jss:p">
                            <xsl:for-each
                                select="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofTextComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                            <xsl:if test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofTextComment/jss:p=''">
                                <font class="rhs_nodata"><xsl:text>Contains no data</xsl:text></font>
                            </xsl:if>
                            
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofTextComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates
                                    select="//jss:proofing/jss:proofingGeneric/jss:changesPDFProofTextComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Web Hosted Proofing</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:webHostedProofing = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:proofing/jss:proofingGeneric/jss:webHostedProofing = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            
            <xsl:for-each select="//jss:editor">
                <tr>
                    <td colspan="2">
                        <font class="lhs-head">
                            <xsl:text>Editor: </xsl:text>
                        </font>
                        <font class="rhs">
                            <xsl:apply-templates select="jss:editorName"/>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        <font class="lhs">
                            <xsl:text>Name of the editor</xsl:text>
                        </font>
                    </td>
                    <td>
                        <font class="rhs">
                            <xsl:apply-templates select="jss:editorName"/>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="lhs">
                            <xsl:text>Email address</xsl:text>
                        </font>
                    </td>
                    <td>
                        <font class="rhs">
                            <xsl:apply-templates select="jss:editorEmail"/>
                        </font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="lhs">
                            <xsl:text>Receives proof?</xsl:text>
                        </font>
                    </td>
                    <td>
                        <xsl:choose>
                            <xsl:when test="jss:emailProof = 0">
                                <font class="rhs-nd">
                                    <xsl:text>Option not selected</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:emailProof = 1">
                                <font class="rhs">
                                    <xsl:text>All proofs</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:emailProof = 2">
                                <font class="rhs">
                                    <xsl:text>Some proofs (see comments)</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:emailProof = 3">
                                <font class="rhs">
                                    <xsl:text>Never</xsl:text>
                                </font>
                            </xsl:when>
<!--                            <xsl:otherwise>
                                <font class="rhs-wd">
                                    <xsl:text>Not a valid data</xsl:text>
                                </font>
                            </xsl:otherwise>-->
                        </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="lhs">
                            <xsl:text>Receives bcc proof?</xsl:text>
                        </font>
                    </td>
                    <td>
                        <xsl:choose>
                            <xsl:when test="jss:bccProof = 0">
                                <font class="rhs">
                                    <xsl:text>No</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:bccProof = 1">
                                <font class="rhs">
                                    <xsl:text>Yes</xsl:text>
                                </font>
                            </xsl:when>
<!--                            <xsl:otherwise>
                                <font class="rhs">
                                    <xsl:text>Not a valid data</xsl:text>
                                </font>
                            </xsl:otherwise>-->
                        </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="lhs">
                            <xsl:text>Sends Corrections?</xsl:text>
                        </font>
                    </td>
                    <td>
                        <xsl:choose>
                            <xsl:when test="jss:sendsCorrections = 0">
                                <font class="rhs-nd">
                                    <xsl:text>Option not selected</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:sendsCorrections = 1">
                                <font class="rhs">
                                    <xsl:text>Always</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:sendsCorrections = 2">
                                <font class="rhs">
                                    <xsl:text>Sometimes</xsl:text>
                                </font>
                            </xsl:when>
                            <xsl:when test="jss:sendsCorrections = 3">
                                <font class="rhs">
                                    <xsl:text>Never</xsl:text>
                                </font>
                            </xsl:when>
<!--                            <xsl:otherwise>
                                <font class="rhs-wd">
                                    <xsl:text>Not a valid data</xsl:text>
                                </font>
                            </xsl:otherwise>-->
                        </xsl:choose>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font class="lhs">
                            <xsl:text>Comment</xsl:text>
                        </font>
                    </td>
                    <td>
                        <xsl:choose>
                            <xsl:when test="jss:proofingComment/jss:p">
                                <xsl:for-each select="jss:proofingComment/jss:p">
                                    <font class="rhs">
                                        <xsl:apply-templates select="."/>
                                        <br/>
                                    </font>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:when test="jss:proofingComment=''">
                                <font class="rhs_nodata">
                                    <xsl:text>Contains no data</xsl:text>
                                </font>
                            </xsl:when>
                            
                            <xsl:otherwise>
                                <font class="rhs">
                                    <xsl:apply-templates select="jss:proofingComment"/>
                                </font>
                            </xsl:otherwise>
                        </xsl:choose>
                    </td>
                </tr>
            </xsl:for-each>
      
            
        </table>
        <br/>
    </xsl:template>

    <!-- Print -->

    <xsl:template match="jss:journalStylesheet/jss:print" name="print">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">PRINT</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Multiple versions?</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:multipleVersion = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:multipleVersion = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Multiple versions comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:multipleVersionComment/jss:p">
                            <xsl:for-each select="//jss:multipleVersionComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:multipleVersionComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:multipleVersionComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Tip ons (glued on the front cover)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:tipOns = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:tipOns = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Tip ons comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:tipOnsComment/jss:p">
                            <xsl:for-each select="//jss:tipOnsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:tipOnsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:tipOnsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Tip ins (bound in order cards)</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:tipIns = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:tipIns = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Tip ins comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:tipInsComment/jss:p">
                            <xsl:for-each select="//jss:tipInsComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:tipInsComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:tipInsComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cover printer per issue</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:coverPerIssue = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:coverPerIssue = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cover printer per issue comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:coverPerIssueComment/jss:p">
                            <xsl:for-each select="//jss:coverPerIssueComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:coverPerIssueComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:coverPerIssueComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cover printer as stock</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:coverStock = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:coverStock = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Cover printer as stock comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:coverStockComment/jss:p">
                            <xsl:for-each select="//jss:coverStockComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:coverStockComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:coverStockComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Advance approval</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:advanceApproval = 0">
                            <font class="rhs">
                                <xsl:text>No</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:advanceApproval = 1">
                            <font class="rhs">
                                <xsl:text>Yes</xsl:text>
                            </font>
                        </xsl:when>
                        <!--<xsl:otherwise>
                            <font class="rhs">
                                <xsl:text>Not a valid data</xsl:text>
                            </font>
                        </xsl:otherwise>-->
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Advance approval comment</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:advanceApprovalComment/jss:p">
                            <xsl:for-each select="//jss:advanceApprovalComment/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        
                        <xsl:when test="//jss:advanceApprovalComment=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:advanceApprovalComment"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Other journal-specific printer instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:additionalPrinter/jss:p">
                            <xsl:for-each select="//jss:additionalPrinter/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:additionalPrinter=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:additionalPrinter"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- Despatch -->

    <xsl:template match="jss:journalStylesheet/jss:despatch" name="despatch">
        <table border="4" width="1000pt">
            <tr>
                <th class="head2" colspan="2">DESPATCH</th>
            </tr>
            <tr>
                <td width="30%">
                    <font class="lhs">
                        <xsl:text>Direct from printer</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:directPrinter!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:directPrinter"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:directPrinter=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Special packaging</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:specialPackaging!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:specialPackaging"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:specialPackaging=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Priority</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:priority!=''">
                            <font class="rhs">
                                <xsl:value-of select="//jss:priority"/>
                            </font>
                        </xsl:when>
                        <xsl:when test="//jss:priority=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                    </xsl:choose>
                </td>
            </tr>
            <tr>
                <td>
                    <font class="lhs">
                        <xsl:text>Other journal-specific despatch instructions</xsl:text>
                    </font>
                </td>
                <td>
                    <xsl:choose>
                        <xsl:when test="//jss:otherJournalSpecificDespatch/jss:p">
                            <xsl:for-each select="//jss:otherJournalSpecificDespatch/jss:p">
                                <font class="rhs">
                                    <xsl:apply-templates select="."/>
                                    <br/>
                                </font>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:when test="//jss:otherJournalSpecificDespatch=''">
                            <font class="rhs_nodata">
                                <xsl:text>Contains no data</xsl:text>
                            </font>
                        </xsl:when>
                        <xsl:otherwise>
                            <font class="rhs">
                                <xsl:apply-templates select="//jss:otherJournalSpecificDespatch"/>
                            </font>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </tr>
        </table>
        <br/>
    </xsl:template>

    <!-- Standard Texts -->

    <xsl:template match="jss:journalStylesheet/jss:standardText" name="stdtxt">
        <table border="4" width="1000pt">
            <tr>
               <td>
        <h2 align="center" class="head2">Standard Texts</h2>

        <h3 align="left" class="head3">Author enquiries</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:authorEnquiries/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:authorEnquiries/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:authorEnquiries/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Language (Usage and Editing services)</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:enLanguageHelpService/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:enLanguageHelpService/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:enLanguageHelpService/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Reference to a complete Guide for Authors</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:refCompleteGfA/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:refCompleteGfA/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:refCompleteGfA/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">No page charges</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:noPageCharges/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:noPageCharges/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:noPageCharges/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Funding body agreements and policies</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:fundBdyAgreePol/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:fundBdyAgreePol/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:fundBdyAgreePol/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Advertising information</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:advertisingInformation/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:advertisingInformation/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:advertisingInformation/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Printers' indicia</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:printersIndicia/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:printersIndicia/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:printersIndicia/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Sponsored supplements and/or commercial reprints</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:sponSuppComRepr/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:sponSuppComRepr/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:sponSuppComRepr/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">USA mailing notice ('Dropshipment')</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:dropshipment/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:dropshipment/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:dropshipment/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">SciVerse ScienceDirect logo and text</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:choose>
            <xsl:when test="//jss:sciSDLogo/@presence = '0' ">
                <font class="head5">Ignore</font>
            </xsl:when>
            <xsl:when test="//jss:sciSDLogo/@presence = '1' ">
                <font class="head5">Required</font>
            </xsl:when>
            <xsl:otherwise>
                <font class="head5">Not a valid data</font>
            </xsl:otherwise>
        </xsl:choose>

        <br/>
        <h3 align="left" class="head3">Paper quality</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:paperQuality/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:paperQuality/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:paperQuality/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Orders, claims and product enquiries</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:ordersClaimsJournalEnquiries/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:ordersClaimsJournalEnquiries/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:ordersClaimsJournalEnquiries/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Publication (Subscription) information</h3>
        <xsl:text>&#8195;</xsl:text>
        <xsl:if test="//jss:publicationInformation/jss:sectiontitle">
            <font class="head4">
                <xsl:value-of select="//jss:publicationInformation/jss:sectiontitle"/>
                <xsl:text>: </xsl:text>
            </font>
        </xsl:if>
        <xsl:for-each select="//jss:publicationInformation/jss:p">
            <xsl:apply-templates select="."/>
            <br/>
            <br/>
        </xsl:for-each>

        <br/>
        <h3 align="left" class="head3">Copyright, permissions, disclaimer</h3>
        <xsl:for-each select="//jss:copyPermDiscl">
            <xsl:if test="./jss:sectiontitle">
                <xsl:text>&#8195;</xsl:text>
                <font class="head4">
                    <xsl:value-of select="./jss:sectiontitle"/>
                    <xsl:text>: </xsl:text>
                </font>
            </xsl:if>
            <xsl:for-each select="./jss:p">
                <xsl:apply-templates select="."/>
                <br/>
                <br/>
            </xsl:for-each>
        </xsl:for-each>
</td>
                </tr>
            </table>
    </xsl:template>




    <!-- Other Instructions
    
    <xsl:template match="jss:journalStylesheet/jss:otherInstructions" name="otherInstructions">
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
