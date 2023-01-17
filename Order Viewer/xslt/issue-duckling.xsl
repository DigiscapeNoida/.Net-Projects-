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
				<xsl:if test="stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY' ]">
					<!--<head>ISSUE PAGINATION SHEET<img src="http://172.16.2.19/orderviewer/xslt/duck.jpg" alt="Duck and Duckling"/></head>-->
					<table width="100%">
						<tr>
							<td>
								<b>ISSUE PAGINATION SHEET</b>
							</td>
							<td align="right">
								<img src="http://172.16.2.19/OrderViewer/xslt/duck.jpg" align="right" alt="Duck and Duckling"/>
							</td>
						</tr>
					</table>
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
								<td>: <xsl:choose>
										<xsl:when test="different/po-number">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/po-number"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/po-number">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/po-number"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/po-number">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/po-number"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="po-number"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal</td>
								<td>: <xsl:choose>
										<xsl:when test="different/jid">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/jid"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/jid">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/jid"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/jid">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/jid"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//jid"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal title</td>
								<td>: <xsl:choose>
										<xsl:when test="different/journal-title">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/journal-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/journal-title">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/journal-title"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/journal-title">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/journal-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//journal-title"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Volume/Issue</td>
								<td>: 
                  <!-- Format the Volume/ Issue values -->
									<xsl:choose>
										<xsl:when test="different/vol-from">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/vol-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/vol-from">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/vol-from"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/vol-from">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/vol-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//vol-from"/>
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="different/vol-to">
											<font color="red">
												<b>
													<u>
														<xsl:apply-templates select="different/vol-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/vol-to">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:apply-templates select="delete/vol-to"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/vol-to">
											<font color="green">
												<b>
													<u>
														<xsl:apply-templates select="insert/vol-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:apply-templates select="//vol-to"/>
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="different/iss-from">
											<font color="red">
												<b>
													<u>
														<xsl:apply-templates select="different/iss-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/iss-from">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:apply-templates select="delete/iss-from"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/iss-from">
											<font color="green">
												<b>
													<u>
														<xsl:apply-templates select="insert/iss-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:apply-templates select="//iss-from"/>
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="different/iss-to">
											<font color="red">
												<b>
													<u>
														<xsl:apply-templates select="different/iss-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/iss-to">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:apply-templates select="delete/iss-to"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/iss-to">
											<font color="green">
												<b>
													<u>
														<xsl:apply-templates select="insert/iss-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:apply-templates select="//iss-to"/>
										</xsl:otherwise>
									</xsl:choose>
									<xsl:choose>
										<xsl:when test="different/supp">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/supp"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/supp">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/supp"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/supp">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/supp"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//supp"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Journal no.</td>
								<td>: <xsl:choose>
										<xsl:when test="different/journal-no">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/journal-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/journal-no">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/journal-no"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/journal-no">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/journal-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//journal-no"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">ISSN</td>
								<td>: <xsl:choose>
										<xsl:when test="different/issn">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/issn"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/issn">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/issn"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/issn">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/issn"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//issn"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Production site</td>
								<td>: <xsl:choose>
										<xsl:when test="different/prod-site">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/prod-site"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/prod-site">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/prod-site"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/prod-site">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/prod-site"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="prod-site"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Billing site</td>
								<td>: <xsl:choose>
										<xsl:when test="different/opco">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/opco"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/opco">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/opco"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/opco">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/opco"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="opco"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<xsl:choose>
										<xsl:when test="different/executor[@type='ES']">
											<font color="red">
												<b>
													<u>
														<xsl:apply-templates select="different/executor[@type='ES']"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/executor[@type='ES']">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:apply-templates select="delete/executor[@type='ES']"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/executor[@type='ES']">
											<font color="green">
												<b>
													<u>
														<xsl:apply-templates select="insert/executor[@type='ES']"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:apply-templates select="executor[@type='ES']"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">Cover date</td>
								<td>: <xsl:choose>
										<xsl:when test="different/cover-date-printed">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/cover-date-printed"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/cover-date-printed">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/cover-date-printed"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/cover-date-printed">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/cover-date-printed"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//cover-date-printed"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<td valign="top">EFFECT cover date</td>
								<td>: <xsl:choose>
										<xsl:when test="different/effect-cover-date">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/effect-cover-date"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/effect-cover-date">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/effect-cover-date"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/effect-cover-date">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/effect-cover-date"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//effect-cover-date"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<!--<tr>
								<td valign="top">Due date</td>
								<td>: <xsl:for-each select="//due-date">
										<xsl:choose>
											<xsl:when test="different/date">
												<font color="red">
													<b>
														<u>
															<xsl:apply-templates select="different/date"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="delete/date">
												<font color="blue">
													<b>
														<s>
															<u>
																<xsl:apply-templates select="delete/date"/>
															</u>
														</s>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="insert/date">
												<font color="green">
													<b>
														<u>
															<xsl:apply-templates select="insert/date"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:otherwise>
												<xsl:apply-templates select="date"/>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:for-each>
								</td>
							</tr>-->
							<tr>
								<td valign="top">Version Nr</td>
								<td>: <xsl:choose>
										<xsl:when test="different/general-info/version-no">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/general-info/version-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/general-info/version-no">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/general-info/version-no"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/general-info/version-no">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/general-info/version-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//general-info/version-no"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
							<tr>
								<xsl:if test="//embargo">
									<td valign="top">Embargo</td>
									<td>:&#32;<xsl:choose>
											<xsl:when test="//general-info/different/embargo">
												<font color="red">
													<b>
														<u>
															<xsl:value-of select="//general-info/different/embargo"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="//general-info/delete/embargo">
												<font color="blue">
													<b>
														<s>
															<u>
																<xsl:value-of select="//general-info/delete/embargo"/>
															</u>
														</s>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="//general-info/insert/embargo">
												<font color="green">
													<b>
														<u>
															<xsl:value-of select="//general-info/insert/embargo"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="//general-info/embargo">
												<xsl:value-of select="//general-info/embargo"/>
											</xsl:when>
										</xsl:choose>
									</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//paper-type-interior">
									<td valign="top">Paper type Interior</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/paper-type-interior">
											 <font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/paper-type-interior"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/paper-type-interior">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/paper-type-interior"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/paper-type-interior">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/paper-type-interior"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/paper-type-interior">
											<xsl:value-of select="//general-info/paper-type-interior"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//paper-type-cover">
									<td valign="top">Paper type cover</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/paper-type-cover">
											 <font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/paper-type-cover"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/paper-type-cover">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/paper-type-cover"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/paper-type-cover">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/paper-type-cover"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/paper-type-cover">
											 <xsl:value-of select="//general-info/paper-type-cover"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//cover-finishing">
									<td valign="top">Cover finishing </td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/cover-finishing">
											 <font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/cover-finishing"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/cover-finishing">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/cover-finishing"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/cover-finishing">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/cover-finishing"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/cover-finishing">
											<xsl:value-of select="//general-info/cover-finishing"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//typeset-model">
									<td valign="top">Typeset Model </td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/typeset-model">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/typeset-model"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/typeset-model">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/typeset-model"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/typeset-model">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/typeset-model"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/typeset-model">
											<xsl:value-of select="//general-info/typeset-model"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//righthand-start">
									<td valign="top">Righthand start</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/righthand-start">
											 <font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/righthand-start"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/righthand-start">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/righthand-start"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/righthand-start">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/righthand-start"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/righthand-start">
											<xsl:value-of select="//general-info/righthand-start"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//issue-weight">
									<td valign="top">Issue Weight</td>
							
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/issue-weight">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/issue-weight"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/issue-weight">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/issue-weight"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/issue-weight">
											 <font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/issue-weight"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/issue-weight">
											<xsl:value-of select="//general-info/issue-weight"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//spine-width">
									<td valign="top">Spine Width</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/spine-width">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/spine-width"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/spine-width">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/spine-width"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/spine-width">
											 <font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/spine-width"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/spine-width">
											 <xsl:value-of select="//general-info/spine-width"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//trimmed-size">
									<td valign="top">Trimmed Size</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/trimmed-size">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/trimmed-size"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/trimmed-size">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/trimmed-size"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/trimmed-size">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/trimmed-size"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/trimmed-size">
											<xsl:value-of select="//general-info/trimmed-size"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//head-margin">
									<td valign="top">Head Margin</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/head-margin">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/head-margin"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/head-margin">
											 <font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/head-margin"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/head-margin">
											 <font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/head-margin"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/head-margin">
											<xsl:value-of select="//general-info/head-margin"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<xsl:if test="//back-margin">
									<td valign="top">Back Margin</td>
								
								<td>:&#32;
									<xsl:choose>
										<xsl:when test="//general-info/different/back-margin">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="//general-info/different/back-margin"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/delete/back-margin">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="//general-info/delete/back-margin"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/insert/back-margin">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="//general-info/insert/back-margin"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="//general-info/back-margin">
											<xsl:value-of select="//general-info/back-margin"/>
										</xsl:when>
									</xsl:choose>
								</td>
								</xsl:if>
							</tr>
							<tr>
								<td valign="top">Issue Proof via :</td>
								<td>: <xsl:choose>
										<xsl:when test="different/general-info/corrections/@type">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/general-info/corrections/@type"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/general-info/corrections/@type">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/general-info/corrections/@type"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/general-info/corrections/@type">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/general-info/corrections/@type"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="//general-info/corrections/@type"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<xsl:choose>
							<xsl:when test="different/issue-info/general-info">
								<font color="red">
									<b>
										<u>
											<xsl:apply-templates select="different/issue-info/general-info"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/issue-info/general-info">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:apply-templates select="delete/issue-info/general-info"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/issue-info/general-info">
								<font color="green">
									<b>
										<u>
											<xsl:apply-templates select="insert/issue-info/general-info"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:apply-templates select="issue-info/general-info"/>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td valign="top">
						<xsl:if test="stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
							<xsl:choose>
								<xsl:when test="different/executor[@type='TYPESETTER']">
									<font color="red">
										<b>
											<u>
												<xsl:apply-templates select="different/executor[@type='TYPESETTER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/executor[@type='TYPESETTER']">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:apply-templates select="delete/executor[@type='TYPESETTER']"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/executor[@type='TYPESETTER']">
									<font color="green">
										<b>
											<u>
												<xsl:apply-templates select="insert/executor[@type='TYPESETTER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:apply-templates select="executor[@type='TYPESETTER']"/>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:if>
						<xsl:if test="stage[@step='OFFPRINTS']">
							<xsl:choose>
								<xsl:when test="different/executor[@type='PRINTER']">
									<font color="red">
										<b>
											<u>
												<xsl:apply-templates select="different/executor[@type='PRINTER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/executor[@type='PRINTER']">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:apply-templates select="delete/executor[@type='PRINTER']"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/executor[@type='PRINTER']">
									<font color="green">
										<b>
											<u>
												<xsl:apply-templates select="insert/executor[@type='PRINTER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:apply-templates select="executor[@type='PRINTER']"/>
								</xsl:otherwise>
							</xsl:choose>
							<xsl:choose>
								<xsl:when test="different/executor[@type='BINDER']">
									<font color="red">
										<b>
											<u>
												<xsl:apply-templates select="different/executor[@type='BINDER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/executor[@type='BINDER']">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:apply-templates select="delete/executor[@type='BINDER']"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/executor[@type='BINDER']">
									<font color="green">
										<b>
											<u>
												<xsl:apply-templates select="insert/executor[@type='BINDER']"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:apply-templates select="executor[@type='BINDER']"/>
								</xsl:otherwise>
							</xsl:choose>
						</xsl:if>
					</td>
					<td valign="top">
				  Printer:<br/>
						<font color="red">
							<b>
								<xsl:value-of select="executor[@type='PRINTER']/different/exec-name"/>
							</b>
						</font>
						<font color="blue">
							<b>
								<xsl:value-of select="executor[@type='PRINTER']/delete/exec-name"/>
							</b>
						</font>
						<font color="green">
							<b>
								<xsl:value-of select="executor[@type='PRINTER']/insert/exec-name"/>
							</b>
						</font>
						<xsl:value-of select="executor[@type='PRINTER']/exec-name"/>
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
            Held Issue: DO NOT DESPATCH UNTIL <xsl:choose>
											<xsl:when test="different/date">
												<font color="red">
													<b>
														<u>
															<xsl:apply-templates select="different/date"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="delete/date">
												<font color="blue">
													<b>
														<s>
															<u>
																<xsl:apply-templates select="delete/date"/>
															</u>
														</s>
													</b>
												</font>
											</xsl:when>
											<xsl:when test="insert/date">
												<font color="green">
													<b>
														<u>
															<xsl:apply-templates select="insert/date"/>
														</u>
													</b>
												</font>
											</xsl:when>
											<xsl:otherwise>
												<xsl:apply-templates select="date"/>
											</xsl:otherwise>
										</xsl:choose>
									</xsl:for-each>
								</b>
							</font>
							<br/>
						</td>
					</tr>
				</xsl:if>
				<br/>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="different/issue-info">
					<font color="red">
						<b>
							<u>
								<xsl:apply-templates select="different/issue-info"/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="delete/issue-info">
					<font color="blue">
						<b>
							<s>
								<u>
									<xsl:apply-templates select="delete/issue-info"/>
								</u>
							</s>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="insert/issue-info">
					<font color="green">
						<b>
							<u>
								<xsl:apply-templates select="insert/issue-info"/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates select="issue-info"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<!-- ********************* -->
		<xsl:call-template name="rkp"/>
		<!-- ********************-->
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the Volume/ Issue field-->
	<xsl:template match="//vol-to">
    -<xsl:choose>
			<xsl:when test="different/vol-to">
				<font color="red">
					<b>
						<u>
							<xsl:value-of select="different/vol-to"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="delete/vol-to">
				<font color="blue">
					<b>
						<s>
							<u>
								<xsl:value-of select="delete/vol-to"/>
							</u>
						</s>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="insert/vol-to">
				<font color="green">
					<b>
						<u>
							<xsl:value-of select="insert/vol-to"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="//vol-to"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="refers-to-document">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template name="insert-refers-to-document">
		<xsl:param name="type"/>
		<xsl:for-each select="refers-to-document">
			<xsl:for-each select="refers-to-document_ROW">
				<!-- "Hack" to ensure correct rendering in case of the issue pagination sheet -->
                <xsl:value-of select="concat(jid, ' ', aid, ', ', pii)"/>
				<xsl:choose>
					<xsl:when test="doi">
						<xsl:value-of select="concat(' (', doi, ', ', pit, ')')"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="concat(' (', pit, ')')"/>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:if test="position()!=last()">
					<xsl:choose>
						<xsl:when test="$type='item'">
							<xsl:text>;</xsl:text>
							<br/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>; </xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:if>
			</xsl:for-each>
            <xsl:value-of select="concat(jid, ' ', aid, ', ', pii)"/>
<br/>
			<xsl:choose>
				<xsl:when test="doi">
					<xsl:value-of select="concat(' (', doi, ', ', pit, ')')"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="concat(' (', pit, ')')"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="position()!=last()">
				<xsl:choose>
					<xsl:when test="$type='item'">
						<xsl:text>;</xsl:text>
						<br/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:text>; </xsl:text>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="//iss-from">
    /<xsl:choose>
			<xsl:when test="different/iss-from">
				<font color="red">
					<b>
						<u>
							<xsl:value-of select="different/iss-from"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="delete/iss-from">
				<font color="blue">
					<b>
						<s>
							<u>
								<xsl:value-of select="delete/iss-from"/>
							</u>
						</s>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="insert/iss-from">
				<font color="green">
					<b>
						<u>
							<xsl:value-of select="insert/iss-from"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="//iss-from"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="//iss-to">
    -<xsl:choose>
			<xsl:when test="different/iss-to">
				<font color="red">
					<b>
						<u>
							<xsl:value-of select="different/iss-to"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="delete/iss-to">
				<font color="blue">
					<b>
						<s>
							<u>
								<xsl:value-of select="delete/iss-to"/>
							</u>
						</s>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="insert/iss-to">
				<font color="green">
					<b>
						<u>
							<xsl:value-of select="insert/iss-to"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="//iss-to"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting any date element so it returns as (eg) 12-Dec-2003 -->
	<xsl:template match="date">
		<xsl:if test="@day!=''">
			<xsl:choose>
				<xsl:when test="different/@day">
					<font color="red">
						<b>
							<u>
								<xsl:value-of select="different/@day"/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="delete/@day">
					<font color="blue">
						<b>
							<s>
								<u>
									<xsl:value-of select="delete/@day"/>
								</u>
							</s>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="insert/@day">
					<font color="green">
						<b>
							<u>
								<xsl:value-of select="insert/@day"/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="@day"/>
				</xsl:otherwise>
			</xsl:choose>
-<xsl:if test="@month='01'">Jan-</xsl:if>
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
			<td>: <xsl:choose>
					<xsl:when test="different/exec-name">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/exec-name"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/exec-name">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/exec-name"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/exec-name">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/exec-name"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="exec-name"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
		<tr>
			<td>Phone no.</td>
			<td>: <xsl:choose>
					<xsl:when test="different/aff/tel">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/aff/tel"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/aff/tel">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/aff/tel"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/aff/tel">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/aff/tel"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="aff/tel"/>
					</xsl:otherwise>
				</xsl:choose>
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
					<xsl:choose>
						<xsl:when test="different/aff/organization">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/organization">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/organization"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/organization">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/organization"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">attn. <xsl:choose>
						<xsl:when test="different/exec-name">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/exec-name">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/exec-name"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/exec-name">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="exec-name"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<xsl:choose>
						<xsl:when test="different/aff/address">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/address"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/address">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/address"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/address">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/address"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/address"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<xsl:choose>
						<xsl:when test="different/aff/address-contd">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/address-contd"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/address-contd">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/address-contd"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/address-contd">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/address-contd"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/address-contd"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<xsl:choose>
						<xsl:when test="different/aff/cty">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/cty"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/cty">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/cty"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/cty">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/cty"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/cty"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="different/aff/zipcode">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/zipcode"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/zipcode">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/zipcode"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/zipcode">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/zipcode"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/zipcode"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<xsl:choose>
						<xsl:when test="different/aff/cny">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/cny"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/cny">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/cny"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/cny">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/cny"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/cny"/>
						</xsl:otherwise>
					</xsl:choose>
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
					<xsl:choose>
						<xsl:when test="different/aff/organization">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/organization">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/organization"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/organization">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/organization"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">attn. <xsl:choose>
						<xsl:when test="different/exec-name">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/exec-name">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/exec-name"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/exec-name">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="exec-name"/>
						</xsl:otherwise>
					</xsl:choose>
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
					<xsl:choose>
						<xsl:when test="different/aff/organization">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/aff/organization">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/aff/organization"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/aff/organization">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/aff/organization"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="aff/organization"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">attn. <xsl:choose>
						<xsl:when test="different/exec-name">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/exec-name">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/exec-name"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/exec-name">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/exec-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="exec-name"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
		</table>
	</xsl:template>
	<!--################################################################################-->
	<xsl:template match="issue-info">
		<xsl:choose>
			<xsl:when test="different/issue-content">
				<font color="red">
					<b>
						<u>
							<xsl:apply-templates select="different/issue-content"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="delete/issue-content">
				<font color="blue">
					<b>
						<s>
							<u>
								<xsl:apply-templates select="delete/issue-content"/>
							</u>
						</s>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="insert/issue-content">
				<font color="green">
					<b>
						<u>
							<xsl:apply-templates select="insert/issue-content"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="issue-content"/>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
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
		<xsl:choose>
			<xsl:when test="different/issue-remarks">
				<font color="red">
					<b>
						<u>
							<xsl:apply-templates select="different/issue-remarks"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="delete/issue-remarks">
				<font color="blue">
					<b>
						<s>
							<u>
								<xsl:apply-templates select="delete/issue-remarks"/>
							</u>
						</s>
					</b>
				</font>
			</xsl:when>
			<xsl:when test="insert/issue-remarks">
				<font color="green">
					<b>
						<u>
							<xsl:apply-templates select="insert/issue-remarks"/>
						</u>
					</b>
				</font>
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="issue-remarks"/>
			</xsl:otherwise>
		</xsl:choose>
		<p/>
		<tr>
			<td colspan="20">
				<xsl:choose>
					<xsl:when test="different/special-issue">
						<font color="red">
							<b>
								<u>
									<xsl:apply-templates select="different/special-issue"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/special-issue">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:apply-templates select="delete/special-issue"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/special-issue">
						<font color="green">
							<b>
								<u>
									<xsl:apply-templates select="insert/special-issue"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates select="//special-issue"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
		<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
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
		<xsl:for-each select="issue-remarks/issue-remark[remark-type='ADV' or different/remark-type='ADV' or delete/remark-type='ADV'  or insert/remark-type='ADV' or @type='ADV'] ">
			<xsl:choose>
				<xsl:when test="different/.">
					<font color="red">
						<b>
							<u>
								<xsl:value-of select="different/."/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="delete/.">
					<font color="blue">
						<b>
							<s>
								<u>
									<xsl:value-of select="delete/."/>
								</u>
							</s>
						</b>
					</font>
				</xsl:when>
				<xsl:when test="insert/.">
					<font color="green">
						<b>
							<u>
								<xsl:value-of select="insert/."/>
							</u>
						</b>
					</font>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="."/>
				</xsl:otherwise>
			</xsl:choose>
			<br/>
			<p/>
		</xsl:for-each>
		<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
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
			<xsl:for-each select="issue-remark[remark-type!='ADV' or different/remark-type!='ADV' or delete/remark-type!='ADV'  or insert/remark-type!='ADV'  or @type!='ADV'] ">
				<tr valign="top">
					<td width="5%">Remark:</td>					
						<xsl:choose>
					<xsl:when test="diff/remark!=''">
						<td width="95%">
							<font color="red">
								<xsl:value-of select="diff/remark"/>
							</font>
						</td>
					</xsl:when>
					<xsl:otherwise>
						<td width="95%">
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
		<xsl:variable name="prefix-null" select="$interior[(prefix=' ')
 ]"/>
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
					<td valign="top">Special Issue ID : <xsl:choose>
							<xsl:when test="different/special-issue/special-issue-id">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/special-issue/special-issue-id"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/special-issue/special-issue-id">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/special-issue/special-issue-id"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/special-issue/special-issue-id">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/special-issue/special-issue-id"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="special-issue/special-issue-id"/>
							</xsl:otherwise>
						</xsl:choose>
					</td>
				</tr>
				<tr>
					<td valign="top">Prelims : <xsl:choose>
							<xsl:when test="different/no-pages-prelims">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-prelims"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-prelims">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-prelims"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-prelims">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-prelims"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="no-pages-prelims"/>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="no-pages-prelims &gt;0">
             ( <xsl:choose>
								<xsl:when test="different/page-ranges/page-range[@type='PRELIM']/first-page">
									<font color="red">
										<b>
											<u>
												<xsl:value-of select="different/page-ranges/page-range[@type='PRELIM']/first-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/page-ranges/page-range[@type='PRELIM']/first-page">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:value-of select="delete/page-ranges/page-range[@type='PRELIM']/first-page"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/page-ranges/page-range[@type='PRELIM']/first-page">
									<font color="green">
										<b>
											<u>
												<xsl:value-of select="insert/page-ranges/page-range[@type='PRELIM']/first-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="page-ranges/page-range[@type='PRELIM']/first-page"/>
								</xsl:otherwise>
							</xsl:choose>
 - <xsl:choose>
								<xsl:when test="different/page-ranges/page-range[@type='PRELIM']/last-page">
									<font color="red">
										<b>
											<u>
												<xsl:value-of select="different/page-ranges/page-range[@type='PRELIM']/last-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/page-ranges/page-range[@type='PRELIM']/last-page">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:value-of select="delete/page-ranges/page-range[@type='PRELIM']/last-page"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/page-ranges/page-range[@type='PRELIM']/last-page">
									<font color="green">
										<b>
											<u>
												<xsl:value-of select="insert/page-ranges/page-range[@type='PRELIM']/last-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="page-ranges/page-range[@type='PRELIM']/last-page"/>
								</xsl:otherwise>
							</xsl:choose>)
            </xsl:if>
					</td>
				</tr>
				<!-- This works, but doesn't bring stuff back in the right order -->
				<tr>
					<td>Interior : <xsl:choose>
							<xsl:when test="different/no-pages-interior">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-interior"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-interior">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-interior"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-interior">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-interior"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="//no-pages-interior"/>
							</xsl:otherwise>
						</xsl:choose>
  (
	<xsl:for-each select="$prefix-null">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-a">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-b">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-c">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-d">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-e">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-f">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-g">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-h">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-i">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-j">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-k">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-l">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-m">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-n">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-o">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-p">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-q">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-r">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-s">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-t">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-u">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-v">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-w">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-x">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-y">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-z">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$prefix-ele">
							<xsl:if test="position() =1">
								<xsl:choose>
									<xsl:when test="different/./page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/./page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/./page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/./page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/./page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="./page-from"/>
									</xsl:otherwise>
								</xsl:choose>
-</xsl:if>
							<xsl:if test="position() =last()">
								<xsl:value-of select="concat(./page-to,'; ') "/>
							</xsl:if>
						</xsl:for-each>		
	)</td>
				</tr>
				<tr>
					<td>Extra     :    <xsl:choose>
							<xsl:when test="different/no-pages-extra">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-extra"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-extra">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-extra"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-extra">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-extra"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="no-pages-extra"/>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="no-pages-extra &gt;0">       				
       (<xsl:for-each select="$extra">
								<xsl:if test="position() =1">
									<xsl:choose>
										<xsl:when test="different/./page-from">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/./page-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/./page-from">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/./page-from"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/./page-from">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/./page-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="./page-from"/>
										</xsl:otherwise>
									</xsl:choose>
-</xsl:if>
								<xsl:if test="position() =last()">
									<xsl:choose>
										<xsl:when test="different/./page-to">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/./page-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/./page-to">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/./page-to"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/./page-to">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/./page-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="./page-to"/>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:if>
							</xsl:for-each>)
              </xsl:if>
					</td>
				</tr>
				<tr>
					<td>Insert  : <xsl:choose>
							<xsl:when test="different/no-pages-insert">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-insert"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-insert">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-insert"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-insert">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-insert"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="no-pages-insert"/>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="no-pages-insert &gt;0">       								
       (<xsl:for-each select="$insert">
								<xsl:if test="position() =1">
									<xsl:choose>
										<xsl:when test="different/./page-from">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/./page-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/./page-from">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/./page-from"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/./page-from">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/./page-from"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="./page-from"/>
										</xsl:otherwise>
									</xsl:choose>
-</xsl:if>
								<xsl:if test="position() =last()">
									<xsl:choose>
										<xsl:when test="different/./page-to">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/./page-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/./page-to">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/./page-to"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/./page-to">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/./page-to"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="./page-to"/>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:if>
							</xsl:for-each>)
     </xsl:if>
					</td>
				</tr>
				<tr>
					<td valign="top">Backmatter  : <xsl:choose>
							<xsl:when test="different/no-pages-bm">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-bm"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-bm">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-bm"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-bm">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-bm"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="no-pages-bm"/>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:if test="no-pages-bm &gt;0">        
        ( <xsl:choose>
								<xsl:when test="different/page-ranges/page-range[@type='BACKMATTER']/first-page">
									<font color="red">
										<b>
											<u>
												<xsl:value-of select="different/page-ranges/page-range[@type='BACKMATTER']/first-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/page-ranges/page-range[@type='BACKMATTER']/first-page">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:value-of select="delete/page-ranges/page-range[@type='BACKMATTER']/first-page"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/page-ranges/page-range[@type='BACKMATTER']/first-page">
									<font color="green">
										<b>
											<u>
												<xsl:value-of select="insert/page-ranges/page-range[@type='BACKMATTER']/first-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/first-page"/>
								</xsl:otherwise>
							</xsl:choose>
 - <xsl:choose>
								<xsl:when test="different/page-ranges/page-range[@type='BACKMATTER']/last-page">
									<font color="red">
										<b>
											<u>
												<xsl:value-of select="different/page-ranges/page-range[@type='BACKMATTER']/last-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/page-ranges/page-range[@type='BACKMATTER']/last-page">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:value-of select="delete/page-ranges/page-range[@type='BACKMATTER']/last-page"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/page-ranges/page-range[@type='BACKMATTER']/last-page">
									<font color="green">
										<b>
											<u>
												<xsl:value-of select="insert/page-ranges/page-range[@type='BACKMATTER']/last-page"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="page-ranges/page-range[@type='BACKMATTER']/last-page"/>
								</xsl:otherwise>
							</xsl:choose>
 )        
       </xsl:if>
					</td>
				</tr>
				<tr>
					<td valign="top">Total pages  : <xsl:choose>
							<xsl:when test="different/no-pages-total">
								<font color="red">
									<b>
										<u>
											<xsl:value-of select="different/no-pages-total"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="delete/no-pages-total">
								<font color="blue">
									<b>
										<s>
											<u>
												<xsl:value-of select="delete/no-pages-total"/>
											</u>
										</s>
									</b>
								</font>
							</xsl:when>
							<xsl:when test="insert/no-pages-total">
								<font color="green">
									<b>
										<u>
											<xsl:value-of select="insert/no-pages-total"/>
										</u>
									</b>
								</font>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="no-pages-total"/>
							</xsl:otherwise>
						</xsl:choose>
					</td>
				</tr>
				<xsl:if test="//stage[@step='OFFPRINTS']">
					<tr>
						<td valign="top">Free Issues : <xsl:choose>
								<xsl:when test="different/no-issues-free">
									<font color="red">
										<b>
											<u>
												<xsl:value-of select="different/no-issues-free"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="delete/no-issues-free">
									<font color="blue">
										<b>
											<s>
												<u>
													<xsl:value-of select="delete/no-issues-free"/>
												</u>
											</s>
										</b>
									</font>
								</xsl:when>
								<xsl:when test="insert/no-issues-free">
									<font color="green">
										<b>
											<u>
												<xsl:value-of select="insert/no-issues-free"/>
											</u>
										</b>
									</font>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="//no-issues-free"/>
								</xsl:otherwise>
							</xsl:choose>
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
			<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
				<tr>
					<th valign="top" align="left">Manuscr. id</th>
					<th valign="top" align="left">Online Version</th>
					<th valign="top" align="left" width="150">DOI number</th>
					<th valign="top" align="left">Ver. no</th>
					<th valign="top" align="left">Title</th>
					<th valign="top" align="left">Cop typ</th>
					<th valign="top" align="left">Corr. author</th>
					<th valign="top" align="left">Ed. office no.</th>
					<th valign="top" align="left">PIT</th>
					<th valign="top" align="left">Prd typ</th>
					<th valign="top" align="left">Page from</th>
					<th valign="top" align="left">Page to</th>
					<th valign="top" align="left">#pgs</th>
					<th valign="top" align="left">Clr</th>
					<th valign="top" align="left">Print Col Graph Nrs</th>
					<th valign="top" align="left"># e- comps</th>
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
					<th valign="top" align="left">Ver. no</th>
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
			<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
				<tr>
					<td colspan="20">
						<hr/>
					</td>
				</tr>
			</xsl:if>
			<xsl:if test="//stage[@step='OFFPRINTS']">
				<tr>
					<td colspan="16">
						<hr/>
					</td>
				</tr>
			</xsl:if>
			<xsl:for-each select="row">
				<!--Issue sheet-->
				<!--=================================================================-->
				<xsl:if test="//stage[@step='P100' or @step='S300' or @step='F300' or @step='P100RESUPPLY' or @step='F300RESUPPLY' or @step='S300RESUPPLY']">
					<xsl:if test="@type='ce'">
						<tr>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/aid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/aid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/aid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/aid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="aid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/online-version/@type">
										<font color="red">
											<xsl:value-of select="different/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="delete/online-version/@type">
										<font color="blue">
											<xsl:value-of select="delete/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="insert/online-version/@type">
										<font color="green">
											<xsl:value-of select="insert/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="online-version/@type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top" width="150">
								<xsl:choose>
									<xsl:when test="different/doi">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/doi">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/doi"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/doi">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="doi"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>



							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/version-no">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/version-no">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/version-no"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/version-no">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="version-no"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/item-title">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/item-title">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/item-title"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/item-title">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="item-title"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/copyright-status">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/copyright-status">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/copyright-status"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/copyright-status">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="copyright-status"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author/degree">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/degree">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/degree"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/degree">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/degree"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/fnm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/fnm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/fnm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/fnm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/fnm"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/snm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/snm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/snm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/snm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/snm"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/eo-item-nr">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/eo-item-nr">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/eo-item-nr"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/eo-item-nr">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="eo-item-nr"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
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
							<td valign="top">
								<xsl:value-of select="no-e-components"/>
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


				<xsl:if test="refers-to-document">
					<tr>
						<td colspan="8">
							<i>
								<xsl:text>&#x2014;&#xA0;Refers to document(s): </xsl:text>
							</i>
							<xsl:call-template name="insert-refers-to-document"/>
						</td>
					</tr>
				</xsl:if>

					</xsl:if>
					<xsl:if test="@type='non-ce'">
						<tr>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/aid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/aid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/aid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/aid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="aid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/online-version/@type">
										<font color="red">
											<xsl:value-of select="different/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="delete/online-version/@type">
										<font color="blue">
											<xsl:value-of select="delete/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="insert/online-version/@type">
										<font color="green">
											<xsl:value-of select="insert/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="online-version/@type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top" width="150">
								<xsl:choose>
									<xsl:when test="different/doi">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/doi">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/doi"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/doi">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="doi"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>

							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/version-no">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/version-no">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/version-no"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/version-no">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="version-no"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/item-title">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/item-title">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/item-title"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/item-title">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="item-title"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/copyright-status">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/copyright-status">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/copyright-status"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/copyright-status">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="copyright-status"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author/degree">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/degree">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/degree"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/degree">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/degree"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/fnm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/fnm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/fnm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/fnm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/fnm"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/snm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/snm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/snm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/snm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/snm"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/eo-item-nr">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/eo-item-nr">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/eo-item-nr"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/eo-item-nr">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="eo-item-nr"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
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
							<td valign="top">
								<xsl:value-of select="no-e-components"/>
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
								<xsl:choose>
									<xsl:when test="different/aid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/aid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/aid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/aid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/aid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="aid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/online-version/@type">
										<font color="red">
											<xsl:value-of select="different/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="delete/online-version/@type">
										<font color="blue">
											<xsl:value-of select="delete/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:when test="insert/online-version/@type">
										<font color="green">
											<xsl:value-of select="insert/online-version/@type"/>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="online-version/@type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top" width="150">
								<xsl:choose>
									<xsl:when test="different/doi">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/doi">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/doi"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/doi">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="doi"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>

							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/version-no">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/version-no">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/version-no"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/version-no">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/version-no"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="version-no"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/item-title">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/item-title">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/item-title"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/item-title">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/item-title"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="item-title"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/copyright-status">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/copyright-status">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/copyright-status"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/copyright-status">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/copyright-status"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="copyright-status"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author/degree">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/degree">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/degree"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/degree">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/degree"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/degree"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/fnm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/fnm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/fnm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/fnm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/fnm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/fnm"/>
									</xsl:otherwise>
								</xsl:choose>
								<xsl:choose>
									<xsl:when test="different/corr-author/snm">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author/snm">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author/snm"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author/snm">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author/snm"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author/snm"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/eo-item-nr">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/eo-item-nr">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/eo-item-nr"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/eo-item-nr">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/eo-item-nr"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="eo-item-nr"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
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
							<td valign="top">
								<xsl:value-of select="no-e-components"/>
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
								<xsl:value-of select="pdf-pages"/>
							</td>
							<td colspan="2"/>
						</tr>
					</xsl:if>
					<xsl:if test="@type='remark'">
						<tr>
							<td colspan="2"/>
							<td/>
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
							<td/>
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
							<td/>
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
					<xsl:if test="@type='h3'">
<tr><td/></tr>
						<tr>
							<td colspan="2"/>
							<td/>
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
								<xsl:choose>
									<xsl:when test="different/doi">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/doi">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/doi"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/doi">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/doi"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="doi"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<font color="blue">
									<xsl:choose>
										<xsl:when test="different/version-no">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/version-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/version-no">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/version-no"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/version-no">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/version-no"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="version-no"/>
										</xsl:otherwise>
									</xsl:choose>
								</font>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-paid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-paid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-paid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-paid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-paid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-free">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-free">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-free"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-free">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-free"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-tot">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-tot">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-tot"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-tot">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-tot"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-issues-paid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-issues-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-issues-paid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-issues-paid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-issues-paid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-issues-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-issues-paid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/covers">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/covers">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/covers"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/covers">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="covers"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-charge">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-charge">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-charge"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-charge">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-charge"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
					</xsl:if>
					<xsl:if test="@type='non-ce'">
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-paid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-paid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-paid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-paid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-paid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-free">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-free">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-free"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-free">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-free"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-tot">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-tot">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-tot"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-tot">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-tot"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<!-- Required? sps_main needs to popluate non-ce cursor. Plus remove remark?  <td valign="top"><xsl:value-of select="no-issues-paid"/></td> -->
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/covers">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/covers">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/covers"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/covers">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="covers"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-charge">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-charge">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-charge"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-charge">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-charge"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/remark">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/remark"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/remark">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/remark"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/remark">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/remark"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="remark"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
					</xsl:if>
					<xsl:if test="@type='advert'">
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/corr-author">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/corr-author">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/corr-author"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/corr-author">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/corr-author"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="corr-author"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pit">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pit">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pit"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pit">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pit"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pit"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/prd-type">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/prd-type">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/prd-type"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/prd-type">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/prd-type"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="prd-type"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-paid">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-paid">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-paid"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-paid">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-paid"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-paid"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-free">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-free">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-free"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-free">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-free"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-free"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-offprints-tot">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-offprints-tot">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-offprints-tot"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-offprints-tot">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-offprints-tot"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-offprints-tot"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<!-- Required? sps_main needs to popluate non-ce cursor. Plus remove remark?  <td valign="top"><xsl:value-of select="no-issues-paid"/></td> -->
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/covers">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/covers">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/covers"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/covers">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/covers"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="covers"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/no-colour-figs">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/no-colour-figs">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/no-colour-figs"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/no-colour-figs">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/no-colour-figs"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="no-colour-figs"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-charge">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-charge">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-charge"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-charge">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-charge"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-charge"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/remark">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/remark"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/remark">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/remark"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/remark">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/remark"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="remark"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
					</xsl:if>
					<xsl:if test="@type='blank'">
						<tr>
							<td/>
							<td valign="top">Blank page</td>
							<td colspan="3"/>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-from">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-from">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-from"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-from">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-from"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-from"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/page-to">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/page-to">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/page-to"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/page-to">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/page-to"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="page-to"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td valign="top">
								<xsl:choose>
									<xsl:when test="different/pdf-pages">
										<font color="red">
											<b>
												<u>
													<xsl:value-of select="different/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="delete/pdf-pages">
										<font color="blue">
											<b>
												<s>
													<u>
														<xsl:value-of select="delete/pdf-pages"/>
													</u>
												</s>
											</b>
										</font>
									</xsl:when>
									<xsl:when test="insert/pdf-pages">
										<font color="green">
											<b>
												<u>
													<xsl:value-of select="insert/pdf-pages"/>
												</u>
											</b>
										</font>
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="pdf-pages"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<td colspan="7"/>
						</tr>
					</xsl:if>
					<xsl:if test="@type='remark'">
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<i>
									<xsl:choose>
										<xsl:when test="different/item-title">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/item-title">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/item-title"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/item-title">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="item-title"/>
										</xsl:otherwise>
									</xsl:choose>
								</i>
							</td>
							<td colspan="12"/>
						</tr>
					</xsl:if>
					<xsl:if test="@type='h1'">
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<i>
									<xsl:choose>
										<xsl:when test="different/item-title">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/item-title">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/item-title"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/item-title">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="item-title"/>
										</xsl:otherwise>
									</xsl:choose>
								</i>
							</td>
							<td colspan="12"/>
							<td/>
						</tr>
					</xsl:if>

					<xsl:if test="@type='h2'">
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<i>
									<xsl:choose>
										<xsl:when test="different/item-title">
											<font color="red">
												<b>
													<u>
														<xsl:value-of select="different/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/item-title">
											<font color="blue">
												<b>
													<s>
														<u>
															<xsl:value-of select="delete/item-title"/>
														</u>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/item-title">
											<font color="green">
												<b>
													<u>
														<xsl:value-of select="insert/item-title"/>
													</u>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="item-title"/>
										</xsl:otherwise>
									</xsl:choose>
								</i>
							</td>
							<td colspan="12"/>
							<td/>
						</tr>
					</xsl:if>

					<xsl:if test="@type='h3'">
<tr><td/></tr>
						<tr>
							<td colspan="2"/>
							<td/>
							<td valign="top">
								<i>
									<xsl:choose>
										<xsl:when test="different/item-title">
											<font color="red">
												<b>
													<i>
														<xsl:value-of select="different/item-title"/>
													</i>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="delete/item-title">
											<font color="blue">
												<b>
													<s>
														<i>
															<xsl:value-of select="delete/item-title"/>
														</i>
													</s>
												</b>
											</font>
										</xsl:when>
										<xsl:when test="insert/item-title">
											<font color="green">
												<b>
													<i>
														<xsl:value-of select="insert/item-title"/>
													</i>
												</b>
											</font>
										</xsl:when>
										<xsl:otherwise>
											<i><xsl:value-of select="item-title"/></i>
										</xsl:otherwise>
									</xsl:choose>
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
				<td valign="top">: <xsl:choose>
						<xsl:when test="different/special-issue-id">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/special-issue-id"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/special-issue-id">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/special-issue-id"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/special-issue-id">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/special-issue-id"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="special-issue-id"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<li/>Title</td>
				<td valign="top">: <xsl:choose>
						<xsl:when test="different/full-name">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/full-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/full-name">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/full-name"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/full-name">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/full-name"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="full-name"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:choose>
						<xsl:when test="different/conference">
							<font color="red">
								<b>
									<u>
										<xsl:apply-templates select="different/conference"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/conference">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:apply-templates select="delete/conference"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/conference">
							<font color="green">
								<b>
									<u>
										<xsl:apply-templates select="insert/conference"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="conference"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<li/>(Guest) Editors</td>
				<td valign="top">: <xsl:choose>
						<xsl:when test="different/editors">
							<font color="red">
								<b>
									<u>
										<xsl:value-of select="different/editors"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="delete/editors">
							<font color="blue">
								<b>
									<s>
										<u>
											<xsl:value-of select="delete/editors"/>
										</u>
									</s>
								</b>
							</font>
						</xsl:when>
						<xsl:when test="insert/editors">
							<font color="green">
								<b>
									<u>
										<xsl:value-of select="insert/editors"/>
									</u>
								</b>
							</font>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="editors"/>
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
		</table>
	</xsl:template>
	<xsl:template match="conference">
		<tr>
			<td valign="top">
				<li/>Conference abbr.</td>
			<td valign="top">: <xsl:choose>
					<xsl:when test="different/abbr-name">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/abbr-name"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/abbr-name">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/abbr-name"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/abbr-name">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/abbr-name"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="abbr-name"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Conference location</td>
			<td valign="top">: <xsl:choose>
					<xsl:when test="different/venue">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/venue"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/venue">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/venue"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/venue">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/venue"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="venue"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Conference date</td>
			<td valign="top">: <xsl:choose>
					<xsl:when test="different/effect-date">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/effect-date"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/effect-date">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/effect-date"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/effect-date">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/effect-date"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="effect-date"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<li/>Sponsored by</td>
			<td valign="top">: <xsl:choose>
					<xsl:when test="different/sponsor">
						<font color="red">
							<b>
								<u>
									<xsl:value-of select="different/sponsor"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="delete/sponsor">
						<font color="blue">
							<b>
								<s>
									<u>
										<xsl:value-of select="delete/sponsor"/>
									</u>
								</s>
							</b>
						</font>
					</xsl:when>
					<xsl:when test="insert/sponsor">
						<font color="green">
							<b>
								<u>
									<xsl:value-of select="insert/sponsor"/>
								</u>
							</b>
						</font>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="sponsor"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
	</xsl:template>
	<!-- ****************************************************************************-->
	<xsl:template name="rkp">
		<table width="100%">
			<tr>
				<td>
					<font size="4" color="red">
						<xsl:text>Duck and Duckling Items</xsl:text>
					</font>
				</td>
				<!--<td align="right"><img src="duck.jpg" alt="Duck and Duckling"/></td>-->
			</tr>
		</table>
		<xsl:for-each select="//issue-content">
			<xsl:for-each select="node()">
				<xsl:if test="name()='row'">
					<xsl:for-each select="node()">
						<xsl:if test="name()='batch'">
							<xsl:variable name="rkp_aid" select="batch-member/aid"/>
							<font color="blue">
								<b>
									<xsl:value-of select="substring-before($rkp_aid,'.')"/>
								</b>
							</font>
							<xsl:call-template name="duck"/>
						</xsl:if>
					</xsl:for-each>
				</xsl:if>
			</xsl:for-each>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="duck">
		<table border="1" width="100%" bgcolor="#EEEEE0">
			<thead bgcolor="#ECE5B6">
				<tr>
					<td align="center">
						<b>AID</b>
					</td>
					<td align="center">
						<b>PIT</b>
					</td>
					<td align="center">
						<b>PII</b>
					</td>
					<td align="center">
						<b>DOI</b>
					</td>
				</tr>
			</thead>
			<xsl:for-each select="node()">
				<tr>
					<xsl:if test="name()='batch-member'">
						<xsl:for-each select="node()">
							<xsl:if test="name()='aid'">
								<td align="center">
									<xsl:value-of select="node()"/>
								</td>
							</xsl:if>
							<xsl:if test="name()='pit'">
								<td align="center">
									<xsl:value-of select="node()"/>
								</td>
							</xsl:if>
							<xsl:if test="name()='pii'">
								<td align="center">
									<xsl:value-of select="node()"/>
								</td>
							</xsl:if>
							<xsl:if test="name()='doi'">
								<td align="center">
									<xsl:value-of select="node()"/>
								</td>
							</xsl:if>
						</xsl:for-each>
					</xsl:if>
				</tr>
			</xsl:for-each>
		</table>
		<br/>
	</xsl:template>
</xsl:stylesheet>
