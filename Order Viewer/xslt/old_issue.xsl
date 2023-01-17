<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<!-- New name space required for position method -->
	<!-- <xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl"> -->
	<!-- This file formats the issue sheet and the offprint sheet -->
	<!-- AGW 11 APR 2003 CR 849 Added new elements no-issues-free and no-issues-paid. Removed offprint remarks -->
	<!-- AGW 09 APR 2003 CR 907 Added new element colour-fig-nr-print -->
	<!-- IMcG 10 DEC 2002 CR 876 Added conference sponsor -->
	<!-- BHW 26 Jan 2004 CR 676 Remove 'Free issues' section.
      Add new variables to deal with listing of page ranges for Interior section 
      Add range listing for Interior, Extra and Insert sections
      Add Onl Publ Date and Copyright Rec'd date Column
      Removed the 'Due Date' template and created a generic date template instead.-->
	<!-- BHW 10 March 2004 CR 936, 676, etc 
      Add Check Issue Proof result
      Rename Col Fig Nrs to Print Col Graph Nrs
      Fixed display of Interior page ranges-->
	<!-- BHW 22 April 2004
CR 1146 Included blank pages in variable $interior
Fixed alignment problems with page ranges.
-->
	<!-- BHW 06 May 2004 CR 1050 Minor change to layout of Remarks and Response fields -->
	<!-- BHW 09 August 2004 CR 1050 Display warning for Offprints if Held Until Date is not null -->
	<!-- BHW 24 Nov 2004 Reinstate 'Free Issues' section, but only for Offprint Orders.
      Add varibale 'prefix-ele' to cater for 'e-only' page ranges-->
	<!--################################################################################-->
	<xsl:template match="/">
		<!-- root -->
		<xsl:apply-templates select="orders"/>
	</xsl:template>
	<!--################################################################################-->
	<xsl:template match="orders">
		<xsl:for-each select="order">
			<html>
				<!-- Insert header -->
				<xsl:if test="stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' ]">
					<head>ISSUE PAGINATION SHEET</head>
				</xsl:if>
				<xsl:if test="stage[@step='OFFPRINTS']">
					<head>OFFPRINTS SHEET</head>
				</xsl:if>
			</html>
			<p/>
			<table width="100%">
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td valign="top">PURCHASE ORDER NUMBER</td>
								<td>: <xsl:value-of select="po-number"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal</td>
								<td>: <xsl:value-of select="//jid"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Issue Production Type</td>
								<td>: <xsl:value-of select="//issue-production-type"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal title</td>
								<td>: <xsl:value-of select="//journal-title"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Volume/Issue</td>
								<td>: 
                  <!-- Format the Volume/ Issue values -->
									<xsl:value-of select="//vol-from"/>
									<xsl:apply-templates select="//vol-to"/>
									<xsl:apply-templates select="//iss-from"/>
									<xsl:apply-templates select="//iss-to"/>
									<xsl:value-of select="//supp"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal no.</td>
								<td>: <xsl:value-of select="//journal-no"/>
								</td>
							</tr>
							<tr>
								<td valign="top">ISSN</td>
								<td>: <xsl:value-of select="//issn"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Production site</td>
								<td>: <xsl:value-of select="prod-site"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Billing site</td>
								<td>: <xsl:value-of select="opco"/>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<xsl:apply-templates select="executor[@type='ES']"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Cover date</td>
								<td>: <xsl:value-of select="//cover-date-printed"/>
								</td>
							</tr>
							<tr>
								<td valign="top">EFFECT cover date</td>
								<td>: <xsl:value-of select="//effect-cover-date"/>
								</td>
							</tr>							
							<tr>
								<td valign="top">Version Nr</td>
								<td>: <xsl:value-of select="//general-info/version-no"/>
								</td>
							</tr>
							<tr>
								<xsl:if test="//embargo">
									<td valign="top">Embargo</td>
									<td>: <xsl:value-of select="//embargo"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//paper-type-interior">
									<td valign="top">Paper type interior</td>
									<td>: <xsl:value-of select="//paper-type-interior"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//paper-type-cover">
									<td valign="top">Paper type Cover</td>
									<td>: <xsl:value-of select="//paper-type-cover"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//cover-finishing">
									<td valign="top">Cover finishing</td>
									<td>: <xsl:value-of select="//cover-finishing"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//typeset-model">
									<td valign="top">Typeset model</td>
									<td>: <xsl:value-of select="//typeset-model"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//righthand-start">
									<td valign="top">Righthand start </td>
									<td>: <xsl:value-of select="//righthand-start"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//issue-weight">
									<td valign="top">Issue weight  </td>
									<td>: <xsl:value-of select="//issue-weight"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//spine-width">
									<td valign="top">Spine width  </td>
									<td>: <xsl:value-of select="//spine-width"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//trimmed-size">
									<td valign="top">Trimmed size</td>
									<td>: <xsl:value-of select="//trimmed-size"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//head-margin">
									<td valign="top">Head margin</td>
									<td>: <xsl:value-of select="//head-margin"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//back-margin">
									<td valign="top">Back margin</td>
									<td>: <xsl:value-of select="//back-margin"/>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<td valign="top">Issue Proof via :</td>
								<td>: <xsl:value-of select="//general-info/corrections/@type"/>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<xsl:apply-templates select="issue-info/general-info"/>
					</td>
					<td valign="top">
						<xsl:if test="stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
							<xsl:apply-templates select="executor[@type='TYPESETTER']"/>
						</xsl:if>
						<xsl:if test="stage[@step='OFFPRINTS']">
							<xsl:apply-templates select="executor[@type='PRINTER']"/>
							<xsl:apply-templates select="executor[@type='BINDER']"/>
						</xsl:if>
					</td>
					<td valign="top">
	  Printer:<br/>
						<xsl:value-of select="executor[@type='BINDER']/exec-name"/>
					</td>
				</tr>
			</table>
			<p/>
			<p/>
			<xsl:if test="//stage[@step='OFFPRINTS']">
				<xsl:if test="//hold-until-date/date/@month!=''">
					<tr>
						<td>
							<font size="4">
								<b>
									<xsl:for-each select="//hold-until-date">
            Held Issue: DO NOT DESPATCH UNTIL <xsl:apply-templates select="date"/>
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
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the Volume/ Issue field-->
	<xsl:template match="//vol-to">
    -<xsl:value-of select="//vol-to"/>
	</xsl:template>
	<xsl:template match="//iss-from">
    /<xsl:value-of select="//iss-from"/>
	</xsl:template>
	<xsl:template match="//iss-to">
    -<xsl:value-of select="//iss-to"/>
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
			<td>: <xsl:value-of select="exec-name"/>
			</td>
		</tr>
		<tr>
			<td>Phone no.</td>
			<td>: <xsl:value-of select="aff/tel"/>
			</td>
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
				<td valign="top">attn. <xsl:value-of select="exec-name"/>
				</td>
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
				<td valign="top">attn. <xsl:value-of select="exec-name"/>
				</td>
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
				<td valign="top">attn. <xsl:value-of select="exec-name"/>
				</td>
			</tr>
		</table>
	</xsl:template>
	<!--################################################################################-->
	<xsl:template match="issue-info">
		<xsl:apply-templates select="issue-content"/>
		<xsl:if test="//stage[@step='P100' or @step='Q300'or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
			<tr>
				<td colspan="14">
					<hr/>
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="//stage[@step='OFFPRINTS']">
			<tr>
				<td colspan="15">
					<hr/>
				</td>
			</tr>
		</xsl:if>

    REMARKS:<p/>
		<xsl:apply-templates select="issue-remarks"/>
		<p/>
		<tr>
			<td colspan="20">
				<xsl:apply-templates select="//special-issue"/>
			</td>
		</tr>
		<xsl:if test="//stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
			<tr>
				<td colspan="14">
					<hr/>
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="//stage[@step='OFFPRINTS']">
			<tr>
				<td colspan="15">
					<hr/>
				</td>
			</tr>
		</xsl:if>
    ADVERT DETAILS:<p/>
		<xsl:for-each select="issue-remarks/issue-remark[remark-type='ADV' or @type='ADV'] ">
			<xsl:value-of select="."/>
			<br/>
			<p/>
		</xsl:for-each>
		<xsl:if test="//stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
			<tr>
				<td colspan="14">
					<hr/>
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="//stage[@step='OFFPRINTS']">
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
			<xsl:for-each select="issue-remark[remark-type!='ADV' or @type!='ADV']">
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
		</table>
	</xsl:template>
	<!--################################################################################-->
	<xsl:template match="general-info">
		<xsl:variable name="extra" select="//row[prefix='EX']"/>
		<xsl:variable name="insert" select="//row[prefix='IN']"/>
		<xsl:variable name="interior" select="//row[string-length(prefix)&lt;=1]"/>
		<xsl:variable name="interior-ce" select="$interior[@type='ce' or@type='blank' ]"/>
		<!-- BHW -->
		<!-- and not(@type='blank') -->
		<xsl:variable name="prefix-null" select="$interior[(prefix=' ')]"/>
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
		<td valign="top">
			<table>
				<tr>
					<td valign="top">Special Issue ID : <xsl:value-of select="special-issue/special-issue-id"/>
					</td>
				</tr>
				<tr>
					<td valign="top">Prelims : <xsl:value-of select="no-pages-prelims"/>
						<xsl:if test="no-pages-prelims &gt;0">
             ( <xsl:value-of select="page-ranges/page-range[@type='PRELIM']/first-page"/> - <xsl:value-of select="page-ranges/page-range[@type='PRELIM']/last-page"/> )
            </xsl:if>
					</td>
				</tr>
				<!-- This works, but doesn't bring stuff back in the right order -->
				<tr>
					<td>Interior : <xsl:value-of select="//no-pages-interior"/>  (
	<xsl:for-each select="$prefix-null">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-a">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-b">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-c">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-d">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-e">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-f">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-g">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-h">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-i">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-j">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-k">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-l">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-m">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-n">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-o">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-p">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-q">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-r">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-s">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-t">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-u">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-v">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-w">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-x">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-y">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-z">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-ele">
							<xsl:if test="position() =1">
								<xsl:value-of select="./page-from"/>-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>		
	)</td>
				</tr>
				<tr>
					<td>Extra     :    <xsl:value-of select="no-pages-extra"/>
						<xsl:if test="no-pages-extra &gt;0">       				
       (<xsl:for-each select="$extra">
								<xsl:if test="position() =1">
									<xsl:value-of select="./page-from"/>-</xsl:if>
								<xsl:if test="position() =last()">
									<xsl:value-of select="./page-to"/>
								</xsl:if>
							</xsl:for-each>)
              </xsl:if>
					</td>
				</tr>
				<tr>
					<td>Insert  : <xsl:value-of select="no-pages-insert"/>
						<xsl:if test="no-pages-insert &gt;0">       								
       (<xsl:for-each select="$insert">
								<xsl:if test="position() =1">
									<xsl:value-of select="./page-from"/>-</xsl:if>
								<xsl:if test="position() =last()">
									<xsl:value-of select="./page-to"/>
								</xsl:if>
							</xsl:for-each>)
     </xsl:if>
					</td>
				</tr>
				<tr>
					<td valign="top">Backmatter  : <xsl:value-of select="no-pages-bm"/>
						<xsl:if test="no-pages-bm &gt;0">        
        ( <xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/first-page"/> - <xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/last-page"/> )        
       </xsl:if>
					</td>
				</tr>
				<tr>
					<td valign="top">Total pages  : <xsl:value-of select="no-pages-total"/>
					</td>
				</tr>
				<xsl:if test="//stage[@step='OFFPRINTS']">
					<tr>
						<td valign="top">Free Issues : <xsl:value-of select="//no-issues-free"/>
						</td>
					</tr>
				</xsl:if>
			</table>
		</td>
	</xsl:template>
	<!--################################################################################-->
	<!-- This section is for displaying all rows on the screen for the issue OR offprint -->
	<xsl:template match="issue-content">
		<table style="font-size:9pt" width="100%">
			<!--    <table style="font-size:9pt; font-family:Courier" width="100%"> -->
			<xsl:if test="//stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
				<tr>
					<th valign="top" align="left">Manuscr. id</th>
					<th valign="top" align="left">Online Version</th>
					<th valign="top" align="left" width="150">DOI number</th>
					<th valign="top" align="left">Title</th>
					<th valign="top" align="left">Cop typ</th>
					<th valign="top" align="left">Corr. author</th>
					<th valign="top" align="left">Ed. office no.</th>
					<th valign="top" align="left">PIT</th>
					<th valign="top" align="left">Prd typ</th>
					<th valign="top" align="left">Page from</th>
					<th valign="top" align="left">Page to</th>
					<th valign="top" align="left">First-e-page</th>
					<th valign="top" align="left">Last-e-page</th>
					<th valign="top" align="left">#pgs</th>
					<th valign="top" align="left">Clr</th>
					<th valign="top" align="left">Print Col Graph Nrs</th>
					<th valign="top" align="left">Onl Pub Date</th>
					<th valign="top" align="left">Copyright Rec Date</th>
					<th valign="top" align="left">Remarks</th>
				</tr>
			</xsl:if>
			<xsl:if test="//stage[@step='OFFPRINTS']">
				<tr>
					<th valign="top" align="left">Manuscr. id</th>
					<th valign="top" align="left">Online Version</th>
					<th valign="top" align="left" width="150">DOI number</th>
					<th valign="top" align="left">Corr. author</th>
					<th valign="top" align="left">PIT</th>
					<th valign="top" align="left">Prd typ</th>
					<th valign="top" align="left">Page from</th>
					<th valign="top" align="left">Page to</th>
					<th valign="top" align="left">#pgs</th>
					<th valign="top" align="left">#offp paid</th>
					<th valign="top" align="left">#offp free</th>
					<th valign="top" align="left">#offp total</th>
					<th valign="top" align="left">#paid iss</th>
					<th valign="top" align="left">Cov</th>
					<th valign="top" align="left">Clr</th>
					<th valign="top" align="left">Page charge</th>
				</tr>
			</xsl:if>
			<xsl:if test="//stage[@step='P100' or @step='Q300' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
				<tr>
					<td colspan="19">
						<hr/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="//stage[@step='OFFPRINTS']">
				<tr>
					<td colspan="18">
						<hr/>
					</td>
				</tr>
			</xsl:if>
			<xsl:for-each select="row">
				<!--Issue sheet-->
				<!--=================================================================-->
				<xsl:if test="//stage[@step='P100' or @step='S300' or @step='Q300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
					<xsl:if test="@type='ce'">
						<tr>
							<td valign="top">
								<xsl:value-of select="aid"/>
							</td>
							<td valign="top">
								<xsl:value-of select="online-version/@type"/>
							</td>
							<td valign="top" width="150">
								<xsl:value-of select="doi"/>
							</td>
							<td valign="top">
								<xsl:value-of select="item-title"/>
							</td>
							<td valign="top">
								<xsl:value-of select="copyright-status"/>
							</td>
							<td valign="top">
								<xsl:value-of select="corr-author/degree"/>
								<xsl:value-of select="corr-author/fnm"/>
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
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
					<xsl:if test="@type='non-ce'">
						<tr>
							<td valign="top">
								<xsl:value-of select="aid"/>
							</td>
							<td valign="top">
								<xsl:value-of select="online-version/@type"/>
							</td>
							<td valign="top" width="150">
								<xsl:value-of select="doi"/>
							</td>
							<td valign="top">
								<xsl:value-of select="item-title"/>
							</td>
							<td valign="top">
								<xsl:value-of select="copyright-status"/>
							</td>
							<td valign="top">
								<xsl:value-of select="corr-author/degree"/>
								<xsl:value-of select="corr-author/fnm"/>
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
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
						<tr>
							<td valign="top">
								<xsl:value-of select="aid"/>
							</td>
							<td valign="top">
								<xsl:value-of select="online-version/@type"/>
							</td>
							<td valign="top" width="150">
								<xsl:value-of select="doi"/>
							</td>
							<td valign="top">
								<xsl:value-of select="item-title"/>
							</td>
							<td valign="top">
								<xsl:value-of select="copyright-status"/>
							</td>
							<td valign="top">
								<xsl:value-of select="corr-author/degree"/>
								<xsl:value-of select="corr-author/fnm"/>
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
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
					<xsl:if test="@type='blank'">
						<tr>
							<td/>
							<td valign="top">Blank page</td>
							<td colspan="6"/>
							<td valign="top">
								<xsl:value-of select="page-from"/>
							</td>
							<td valign="top">
								<xsl:value-of select="page-to"/>
							</td>
							<td valign="top">
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>

							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td colspan="2"/>
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
							<td valign="top">
								<!--<i><xsl:value-of select="pdf-pages"/></i>-->
							</td>
							<td valign="top">
								<!--<i><xsl:value-of select="no-colour-figs"/></i>-->
							</td>
							<td/>
						</tr>
					</xsl:if>
					<xsl:if test="@type='h1'">
						<tr>
							<td colspan="2"/>
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
							<td valign="top">
								<!--<i><xsl:value-of select="pdf-pages"/></i>-->
							</td>
							<td valign="top">
								<!--<i><xsl:value-of select="no-colour-figs"/></i>-->
							</td>
							<td/>
						</tr>
					</xsl:if>
					<xsl:if test="@type='h2'">
						<tr>
							<td colspan="2"/>
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
							<td valign="top">
								<!--<i><xsl:value-of select="pdf-pages"/></i>-->
							</td>
							<td valign="top">
								<!--<i><xsl:value-of select="no-colour-figs"/></i>-->
							</td>
							<td/>
						</tr>
					</xsl:if>
				</xsl:if>
				<!-- offprint sheet -->
				<!--=================================================================-->
				<xsl:if test="//stage[@step='OFFPRINTS']">
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
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
								<xsl:value-of select="no-issues-paid"/>
							</td>
							<td valign="top">
								<xsl:value-of select="covers"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
							</td>
							<td valign="top">
								<xsl:value-of select="page-charge"/>
							</td>
						</tr>
					</xsl:if>
					<xsl:if test="@type='advert'">
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-p."/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-free"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-tot"/>
							</td>
							<!-- Required? sps_main needs to popluate non-ce cursor. Plus remove remark?  <td valign="top"><xsl:value-of select="no-issues-paid"/></td> -->
							<td valign="top">
								<xsl:value-of select="covers"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
							</td>
							<td valign="top">
								<xsl:value-of select="page-charge"/>
							</td>
							<td valign="top">
								<xsl:value-of select="remark"/>
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
							</td>

							<td valign="top">
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-p."/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-free"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-offprints-tot"/>
							</td>
							<!-- Required? sps_main needs to popluate non-ce cursor. Plus remove remark?  <td valign="top"><xsl:value-of select="no-issues-paid"/></td> -->
							<td valign="top">
								<xsl:value-of select="covers"/>
							</td>
							<td valign="top">
								<xsl:value-of select="no-colour-figs"/>
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
								<xsl:value-of select="first-e-page"/>
							</td>
							<td valign="top">
								<xsl:value-of select="last-e-page"/>
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
				<td valign="top">
					<li/>Working title</td>
				<td valign="top">: <xsl:value-of select="special-issue-id"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<li/>Title</td>
				<td valign="top">: <xsl:value-of select="full-name"/>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="conference"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<li/>(Guest) Editors</td>
				<td valign="top">: <xsl:value-of select="editors"/>
				</td>
			</tr>
		</table>
	</xsl:template>
	<xsl:template match="conference">
		<tr>
			<td valign="top">
				<li/>Conference abbr.</td>
			<td valign="top">: <xsl:value-of select="abbr-name"/>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Conference location</td>
			<td valign="top">: <xsl:value-of select="venue"/>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Conference date</td>
			<td valign="top">: <xsl:value-of select="effect-date"/>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Sponsored by</td>
			<td valign="top">: <xsl:value-of select="sponsor"/>
			</td>
		</tr>
	</xsl:template>
	<!--################################################################################-->
	<xsl:template match="diff">
		<font size="4" color="red">
			<xsl:value-of select="."/>
		</font>
	</xsl:template>
</xsl:stylesheet>
