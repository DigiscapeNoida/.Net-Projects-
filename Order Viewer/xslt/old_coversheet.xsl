<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<!--
    version 1.1.3
    Copyright (c) 2003-2007 Elsevier B.V.
    $Id: coversheet.xsl,v 1.8 2007/08/09 12:39:26 jmigchie Exp $
-->
	<!--
    v1.1.3  9 August 2007  [PTSIII release 2007.2]
    No changes from v1.1.2

    v1.1.2  15 December 2006  [PTSIII release 2006.3]
    No changes from v1.1.1

    v1.1.1  27 October 2006  [PTSIII release 2006.3]
    Changes with respect to v1.0
    * JMi: added Embargo until Stage (PTS 2006.3, CR 1483, PTSIII Order DTD v1.20)

    v1.0  27 July 2006  [PTSIII release 2006.2]
    Changes with respect to previous version (which was owned by the PTSII developers and was part of PTSIII 2006.1)
    * WVr (Wim de Vries 04-05-2006 ... 17-05-2006)
      - added pretty print view of the xml code at the end of the html doc
      - put the html open and close tags at the correct position
      - replaced <p/> with <p></p> (the first is invalid SGML/HTML4.01)
      - moved PCDATA from head-element to an H3 element (head may only contain elements, not PCDATA)
      - added font 'Arial Unicode MS', currently the best to view all kind of special characters
    * Show element righthand-start above 'copyright status'
    * Split out 'issue-remark' into 'remark' and 'response'

-->
	<!--################################################################################-->
	<xsl:template match="/">
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
					<h3>COVERSHEET</h3>
					<table width="100%">
						<table>
							<tr>
								<td>Date:</td>
								<td>
									<xsl:apply-templates select="time"/>
								</td>
							</tr>
							<tr>
								<td>PII:</td>
								<td>
									<xsl:value-of select="//pii"/>
								</td>
							</tr>
							<tr>
								<xsl:apply-templates select="item-info/doi"/>
							</tr>
							<tr>
								<td>Version Nr:</td>
								<td>
									<xsl:value-of select="//version-no"/>
								</td>
							</tr>
							<tr>
								<td>For:</td>
								<td>
									<xsl:value-of select="prod-site"/>
								</td>
							</tr>
							<tr>
								<td>Supplier:</td>
								<td>
									<xsl:value-of select="//exec-name"/>
								</td>
							</tr>
						</table>
						<tr>
							<td colspan="2">
								<hr/>
							</td>
						</tr>
						<table width="100%">
							<tr>
								<td>
									<table>
										<tr>
											<td>Item:</td>
											<td>
												<xsl:value-of select="//jid"/>
												<xsl:text/>
												<xsl:value-of select="//aid"/>
											</td>
										</tr>
										<tr>
											<td width="24%"># Item pages:</td>
											<td width="25%">
												<xsl:value-of select="//no-mns-pages"/>
											</td>
											<td width="22%">Publication item type:</td>
											<td>
												<xsl:value-of select="//orders/order/item-info/pit"/>
											</td>
										</tr>
										<tr>
											<td># Graphics:</td>
											<td>
												<xsl:value-of select="//no-phys-figs"/>
											</td>
											<td>batch-member:</td>
											<td>
												<xsl:apply-templates select="//batch-member[1]"/>
											</td>
											<td>
												<xsl:apply-templates select="//subitem[1]"/>
											</td>
										</tr>
										<tr valign="top">
											<td># B/W graphics:</td>
											<td>
												<xsl:value-of select="//no-bw-figs"/>
											</td>
											<td>Refers to PII:</td>
											<td>
												<xsl:value-of select="//refers-to-document[1]/pii"/>
												<xsl:if test="//refers-to-document[2]/pii!=''">, <br>
														<xsl:value-of select="//refers-to-document[2]/pii"/>
													</br>
												</xsl:if>
												<xsl:if test="//refers-to-document[3]/pii!=''"> ...</xsl:if>
											</td>
										</tr>
										<tr valign="top">
											<td># Web colour graphics:</td>
											<td>
												<xsl:value-of select="//no-web-colour-figs"/>
												<xsl:for-each select="//figure">
												<xsl:choose>
												<xsl:when test="figure-type[text()='COLOUR']">
												<font><b>[<xsl:value-of select="figure-nr"/>]</b></font>
												</xsl:when>
												</xsl:choose>
												</xsl:for-each>
											</td>
											<td>Refers to DOI:</td>
											<td>
												<xsl:value-of select="//refers-to-document[1]/doi"/>
												<xsl:if test="//refers-to-document[2]/doi!=''">, <br>
														<xsl:value-of select="//refers-to-document[2]/doi"/>
													</br>
												</xsl:if>
												<xsl:if test="//refers-to-document[3]/doi!=''"> ...</xsl:if>
											</td>
										</tr>
										<tr>
											<td># Printed colour graphics:</td>
											<td>
												<xsl:value-of select="//no-colour-figs"/>
												<xsl:for-each select="//figure">
												<xsl:choose>
												<xsl:when test="figure-type[text()='COLOUR-IN-PRINT']">
												<font><b>[<xsl:value-of select="figure-nr"/>]</b></font>
												</xsl:when>
												</xsl:choose>
												</xsl:for-each>
											</td>
											<td>E Submission Item Nr:</td>
											<td>
												<xsl:value-of select="//e-submission-item-nr"/>
											</td>
										</tr>
										<tr>
											<td>Righthand start:</td>
											<td>
												<xsl:value-of select="//righthand-start"/>
											</td>
											<td/>
											<td/>
										</tr>
										<tr>
											<td>Copyright status:</td>
											<td>
												<xsl:value-of select="//copyright-status"/>
											</td>
											<td>EO Item Nr:</td>
											<td>
												<xsl:value-of select="//eo-item-nr"/>
											</td>
										</tr>
										<tr>
											<td>Copyright received date:</td>
											<td>
												<xsl:for-each select="//copyright-recd-date">
													<xsl:apply-templates select="date"/>
												</xsl:for-each>
											</td>
											<td>Editor:</td>
											<td>
												<xsl:value-of select="//editor"/>
											</td>
										</tr>
										<tr>
											<td>Production type as sent:</td>
											<td>
												<xsl:value-of select="//prd-type-as-sent"/>
											</td>
											<td>Date received:</td>
											<td>
												<xsl:for-each select="//received-date">
													<xsl:apply-templates select="date"/>
												</xsl:for-each>
											</td>
										</tr>
										<tr>
											<td>Online Version:</td>
											<td>
												<xsl:value-of select="//online-version/@type"/>
											</td>
											<td>Date revised:</td>
											<td>
												<xsl:for-each select="//revised-date">
													<xsl:apply-templates select="date"/>
												</xsl:for-each>
											</td>
										</tr>
										<tr>
											<td>Corrections:</td>
											<td>
												<xsl:value-of select="//corrections/@type"/>
											</td>
											<td>Date accepted:</td>
											<td>
												<xsl:for-each select="//accept-date">
													<xsl:apply-templates select="date"/>
												</xsl:for-each>
											</td>
										</tr>
										<tr>
											<td>Embargo Expiry Time:</td>
											<td>
												<xsl:value-of select="//embargo"/>
											</td>
										</tr>
										<tr>
											<td>Embargo until Stage:</td>
											<td>
												<xsl:value-of select="//embargo-until-stage"/>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<xsl:apply-templates select="//e-components"/>
						<tr>
							<td colspan="2">
								<hr/>
							</td>
						</tr>
						<table>
							<tr>
								<td>Item title:</td>
								<td>
									<xsl:value-of select="//item-title"/>
								</td>
							</tr>
							<tr>
								<td>Item group:</td>
								<td>
									<xsl:value-of select="//item-group"/>
								</td>
							</tr>
							<tr>
								<td>Item group description:</td>
								<td>
									<xsl:value-of select="//item-group-description"/>
								</td>
							</tr>
							<tr>
								<td>Section:</td>
								<td>
									<xsl:value-of select="//section"/>
								</td>
							</tr>
							<tr>
								<td>Dochead:</td>
								<td>
									<xsl:value-of select="//dochead"/>
								</td>
							</tr>
							<tr>
								<td>First author:</td>
								<td>
									<xsl:value-of select="//first-author"/>
								</td>
							</tr>
							<tr>
								<td>Corr. author:</td>
								<td>
									<xsl:apply-templates select="//corr-author"/>
								</td>
							</tr>
						</table>
						<tr>
							<td colspan="2">
								<hr/>
							</td>
						</tr>
						<table>
							<tr>
								<td>
									<xsl:apply-templates select="//funding"/>
								</td>
							</tr>
						</table>
						<table width="100%">
							<tr>
								<td colspan="2">
									<hr/>
								</td>
							</tr>
							<tr>
								<td valign="top">Item remarks:</td>
								<td valign="top">
									<xsl:apply-templates select="//item-remarks"/>
								</td>
							</tr>
							<tr>
								<td valign="top">Issue remarks:</td>
								<td valign="top">
									<xsl:apply-templates select="//issue-remarks"/>
								</td>
							</tr>
						</table>
					</table>
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
			<xsl:attribute name="style"><xsl:value-of select="concat('position: relative; margin-left:', $anc ,'%;', 'overflow: scroll;')"/></xsl:attribute>
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
	<!--This section is for formatting the corr-author name-->
	<xsl:template match="corr-author">
		<xsl:value-of select="degree"/>
		<xsl:text/>
		<xsl:value-of select="fnm"/>
		<xsl:text/>
		<xsl:value-of select="snm"/>
		<tr>
			<td>
				<xsl:apply-templates select="./aff"/>
			</td>
		</tr>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the corr-author address-->
	<xsl:template match="aff">
		<tr>
			<td/>
			<td>
				<xsl:value-of select="organization"/>
			</td>
		</tr>
		<tr>
			<td/>
			<td>
				<xsl:value-of select="inst-contd"/>
			</td>
		</tr>
		<tr>
			<td/>
			<td>
				<xsl:value-of select="institute"/>
			</td>
		</tr>
		<tr>
			<td/>
			<td>
				<xsl:value-of select="address"/>
			</td>
		</tr>
		<tr>
			<td/>
			<td>
				<xsl:value-of select="address-contd"/>
			</td>
		</tr>
		<xsl:choose>
			<xsl:when test="zipcode/@zipcode-pos='NONE'">
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cty"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cny"/>
					</td>
				</tr>
			</xsl:when>
			<xsl:when test="zipcode/@zipcode-pos='BEFORECTY'">
				<tr>
					<td/>
					<td>
						<xsl:value-of select="zipcode"/>
						<xsl:text/>
						<xsl:value-of select="cty"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cny"/>
					</td>
				</tr>
			</xsl:when>
			<xsl:when test="zipcode/@zipcode-pos='AFTERCTY'">
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cty"/>
						<xsl:text/>
						<xsl:value-of select="zipcode"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cny"/>
					</td>
				</tr>
			</xsl:when>
			<xsl:when test="zipcode/@zipcode-pos='BEFORECNY'">
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cty"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="zipcode"/>
						<xsl:text/>
						<xsl:value-of select="cny"/>
					</td>
				</tr>
			</xsl:when>
			<xsl:when test="zipcode/@zipcode-pos='AFTERCNY'">
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cty"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cny"/>
						<xsl:text/>
						<xsl:value-of select="zipcode"/>
					</td>
				</tr>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cty"/>
						<xsl:text/>
						<xsl:value-of select="zipcode"/>
					</td>
				</tr>
				<tr>
					<td/>
					<td>
						<xsl:value-of select="cny"/>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
		<tr>
			<td>Phone:</td>
			<td>
				<xsl:value-of select="//corr-author/aff/tel"/>
			</td>
		</tr>
		<tr>
			<td>Fax:</td>
			<td>
				<xsl:value-of select="//corr-author/aff/fax"/>
			</td>
		</tr>
		<tr>
			<td>E-mail:</td>
			<td>
				<xsl:value-of select="//corr-author/aff/ead"/>
			</td>
		</tr>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the item-remarks field-->
	<xsl:template match="item-remarks">
		<xsl:for-each select="item-remark">
			<tr valign="top">
				<td width="10%">Remark:</td>
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
				<td width="10%">Response:</td>
				<xsl:choose>
					<xsl:when test="diff/remark!=''">
						<td width="40%">
							<font color="blue">
								<xsl:value-of select="response"/>
							</font>
						</td>
					</xsl:when>
					<xsl:otherwise>
						<td width="40%">
							<xsl:value-of select="response"/>
						</td>
					</xsl:otherwise>
				</xsl:choose>
			</tr>
		</xsl:for-each>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the issue-remarks field-->
	<xsl:template match="issue-remarks">
		<xsl:for-each select="issue-remark">
			<xsl:text>Remark: </xsl:text>
			<xsl:value-of select="./remark"/>
			<br/>
			<xsl:text>Response: </xsl:text>
			<xsl:value-of select="./response"/>
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
	<xsl:template match="batch-member"> Present </xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the refers-to pii field-->
	<xsl:template match="refers-to-document/pii">
		<tr>
			<td>
				<xsl:value-of select="//refers-to-document[1]/pii"/>
			</td>
		</tr>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the refers-to-doi field-->
	<xsl:template match="refers-to-doi">
		<xsl:apply-templates select="doi"/>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the refers-to-doi/doi field-->
	<xsl:template match="refers-to-doi/doi">
		<td>Refers_to DOI:</td>
		<td>
			<xsl:value-of select="../doi"/>
		</td>
	</xsl:template>
	<!--################################################################################-->
	<!--This section is for formatting the e-components field-->
	<xsl:template match="e-components">
		<hr/>E-COMPONENTS: <table width="100%">
			<tr>
				<td colspan="5"/>
			</tr>
			<xsl:for-each select="e-component">
				<tr>
					<td width="15%">
						<xsl:value-of select="e-component-nr"/>
					</td>
					<td width="15%">
						<xsl:value-of select="e-component-format"/>
					</td>
					<td width="70%">
						<xsl:value-of select="e-component-remarks"/>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
	<!--#####################################################################################-->
	<!-- This section is for formatting the grant-number field-->
	<xsl:template match="funding">
		<tr>
			<td>Funded By:</td>
			<td>
				<xsl:value-of select="funded-by"/>
			</td>
		</tr>
		<tr>
			<td>Grant number(s):</td>
			<xsl:for-each select="//grant-number">
				<td>
					<xsl:value-of select="."/>;</td>
			</xsl:for-each>
		</tr>
		<table width="100%">
			<tr>
				<td colspan="8"/>
			</tr>
			<td width="25%">Principal-Investigator(s):</td>
			<xsl:for-each select="//principal-investigator">
				<tr>
					<td>
						<xsl:value-of select="fnm"/>
						<xsl:text/>
						<xsl:value-of select="snm"/>
						<td>
							<xsl:value-of select="aff/ead"/>
						</td>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
</xsl:stylesheet>
