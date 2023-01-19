<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:output method="xml" doctype-system="ppmorder12.dtd"
		doctype-public="-//ES//DTD PPM order DTD version 1.2//EN//XML" encoding="UTF-8" indent="yes"/>

	<!--
Stylesheet: Elsevier PEAK - EWII - Conversion Supplier Order 
OF @ Klopotek & Partner

=================================================================================
Copyright 2008 by
Klopotek & Partner GmbH
Schlueterstrasse 39
10629 Berlin
Germany

This document is copyright protected. Any whole or partial use,
which is not allowed by copyright or contractual agreement,
requires prior written permission from Klopotek & Partner.
This applies in particular to the reproduction, duplication,
editing, translating and digitizing of copyright material.
Every precaution has been taken to ensure that the information
concerning hardware and software explained in this document
is accurate and correct.
However, errors and/or omissions cannot be entirely excluded.
Liability is therefore not assumed for incorrect information.
The right to make technical changes is always reserved.

=================================================================================

8.8.2	OF	#132386: Stylesheets: mandatory elements

8.8.3	OF	#132406: Stylesheet supplierOrder.xsl does not run

8.8.4	OF	#132444: Stylesheet supplierOrder.xsl :currency

8.8.6	OF  #132853: O_PORORD jid with prefix BS
			#132859: O_PORORD missing or wrong conversion

8.8.7   MRu 28/4/08 
		Added doctype information and hardwired order/instruction types
		Corrected many mistakes and mapped several elements to @term rather than element
		Refer to elementool issues 710, 711, 716 & 717 for unresolved minor problems
		
8.8.10  MRu 14/5/08
		Further corrections as a result of testing (elementool issues 747-750)
		Added attribute project/@els-reprint-no (fix required as a result of reprint number CR concept)
		Added mappings for new DTD elements book-info/mrw-code and book-details/division

8.8.12  MRu 4/6/08
		Changes as a result of Klopotek bug fixes 132909 and 134111
		Explicitly mapped delivery-addresses to remove id_location attribute (not required)
		Added mappings for new DTD elements book-details/ownership and ./no-of-chapters
		
8.8.13  MRu 26/6/08
		Further changes to harmonize with PPM output and make XML more consistent
		Renamed element project/note project/printrun-note
		Added new element production-position/order-line-notes
		Changed definition of project/prod-extent to match DTD
		Removed element production-position/status (not required)
		
8.8.14  MRu 1/7/08
		Emergency patch to remove elements task-no and task (not required and not in DTD)
		
8.8.15	MRu 2 July 2008
		Added new work order type FULL-SERVICE-ORDER
		
8.8.16  MRu 23 July 2008
		Added BS: patch as in document BS_note.pdf
		
8.8.17	MRu 6 March 2009
		Changed public identifier to point to ppmorder11.dtd

8.9.0   JMi  4 May 2012
        Changed public identifier to ppmorder12.dtd
        Added mappings for new DTD element book-info/manifestations
        (A manifestation element is only created when the isbn is present)

8.9.1   JMi  13 July 2012
        A manifestation element is only created when the isbn is present _and_
        when the  edition is the edition of the dac master
        
8.9.2   JMi  8 August 2012/14 January 2013
        Mapped language_1/@temp to language (the latter element was added to v1.2r3 of the PPM Order DTD)

8.9.3   JMi  18 March 2013
        Mapped specification/biblio_ref to specification/biblio-ref (the latter element was added to v1.2r4 of the PPM Order DTD)

8.9.4   BJ 30 May 2013
        Mapped production-position/project/bibl_remark element in the raw xml to specification/biblio-ref in the PPM order XML.  

-->

	<!-- order types -->
	<xsl:variable name="SUBTYPE_ORDER" select="'order'"/>
	<xsl:variable name="SUBTYPE_INSTR" select="'instruction'"/>

	<!-- classifications -->
	<xsl:variable name="CL_PAPRLOC" select="'PAPRLOC'"/>
	<xsl:variable name="CL_EXECTYPE" select="'EXECTYPE'"/>
	<xsl:variable name="CL_EXECCODE" select="'EXECCODE'"/>
	<xsl:variable name="CL_MRWCODE" select="'MRWCODE'"/>
	<xsl:variable name="CL_PANCHAP" select="'PANCHAP'"/>


	<!-- ************************************** -->
	<!-- *** Root-Template                  *** -->
	<!-- ************************************** -->

	<xsl:template match="/message">

		<xsl:variable name="order" select="body/supplierOrder/order"/>
		<xsl:variable name="subtype" select="$order/@sub_type"/>
		<xsl:variable name="position" select="$order/positions/production_position[1]"/>
		<xsl:variable name="instruction" select="$order/instructions/instruction[1]"/>

		<xsl:element name="orders">
			<xsl:element name="order">

				<xsl:attribute name="type">
					<xsl:choose>
						<xsl:when test="$subtype = $SUBTYPE_ORDER">work-order</xsl:when>
						<xsl:when test="$subtype = $SUBTYPE_INSTR">instruction</xsl:when>
					</xsl:choose>
				</xsl:attribute>

				<xsl:apply-templates select="$order/userinfo"/>
				<xsl:apply-templates select="header/timestamp"/>

				<!-- ********************************************************** -->
				<!-- Hardcoded mappings for <stage> added by MRu, 15 April 2008 -->
				<!-- ********************************************************** -->

				<xsl:element name="stage">
					<xsl:attribute name="step">
						<xsl:choose>
							<xsl:when test="$subtype = $SUBTYPE_ORDER">
								<!--<xsl:value-of select="$position/order_type/@term" />-->
								<xsl:choose>
									<xsl:when test="$position/order_type='ORART'"
										>ARTWORK-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORCMB'"
										>COMBINED-MFG-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORCON'"
										>CONVERSION-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORCVD'"
										>COVER-DESIGN-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORCVM'"
										>COVER-MFG-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORCPY'"
										>COPYEDIT-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORDES'"
										>DESIGN-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORFRE'"
										>FREELANCE-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORFUL'"
										>FULL-SERVICE-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORGEN'"
										>GENERIC-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORIDX'"
										>INDEXING-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORMSD'"
										>MS-DEV-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORMED'"
										>MEDIA-DEV-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORMEP'"
										>MEDIA-PROOF-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORMER'"
										>MEDIA-REPL-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORPRB'"
										>PRINT-BIND-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORPRJ'"
										>PROJECT-MGMT-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORPRF'"
										>PROOFREAD-ORDER</xsl:when>
									<xsl:when test="$position/order_type='ORTYP'"
										>TYPESET-ORDER</xsl:when>
								</xsl:choose>
							</xsl:when>
							<xsl:when test="$subtype = $SUBTYPE_INSTR">
								<!--<xsl:value-of select="$instruction/instruction_type/@term" />-->
								<xsl:choose>
									<xsl:when test="$instruction/instruction_type='COBK'"
										>CAST-OFF-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='COBT'"
										>CAST-OFF-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='TYSP'"
										>SAMPLE-PAGES</xsl:when>
									<xsl:when test="$instruction/instruction_type='CEBK'"
										>COPYEDIT-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='CEBT'"
										>COPYEDIT-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='ART1'"
										>1ST-ARTWORK</xsl:when>
									<xsl:when test="$instruction/instruction_type='ART2'"
										>CORRECT-ARTWORK</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY1BK'"
										>TYPESET-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY1BT'"
										>TYPESET-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='PR1BK'"
										>PROOFREAD-1PR-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='PR1BT'"
										>PROOFREAD-1PR-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY2BK'"
										>2ND-PROOF-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY2BT'"
										>2ND-PROOF-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='PR2BK'"
										>PROOFREAD-2PR-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='PR2BT'"
										>PROOFREAD-2PR-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY3BK'"
										>PAGE-PROOF-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='PR3BK'"
										>PROOFREAD-PAGE-PROOF</xsl:when>
									<xsl:when test="$instruction/instruction_type='IDXBK'"
										>INDEX-BOOK</xsl:when>
									<xsl:when test="$instruction/instruction_type='IDXBT'"
										>INDEX-BATCH</xsl:when>
									<xsl:when test="$instruction/instruction_type='TY4BK'"
										>2ND-PAGE-PROOF</xsl:when>
									<xsl:when test="$instruction/instruction_type='TYFBK'"
										>FINAL-PROOF</xsl:when>
									<xsl:when test="$instruction/instruction_type='TYIDX'"
										>TYPESET-INDEX</xsl:when>
									<xsl:when test="$instruction/instruction_type='TYPDF'"
										>PRINT-READY-FILES</xsl:when>
									<xsl:when test="$instruction/instruction_type='Q300'"
										>Q300</xsl:when>
									<xsl:when test="$instruction/instruction_type='H300'"
										>H300</xsl:when>
									<xsl:when test="$instruction/instruction_type='O300'"
										>O300</xsl:when>
									<xsl:when test="$instruction/instruction_type='H350'"
										>H350</xsl:when>
								</xsl:choose>
							</xsl:when>
						</xsl:choose>
					</xsl:attribute>
				</xsl:element>

				<xsl:apply-templates select="$order/../supplier"/>

				<xsl:element name="order-no">
					<xsl:value-of select="$order/order_number"/>
				</xsl:element>

				<xsl:element name="instruction-no">
					<xsl:value-of select="$instruction/instruction_no"/>
				</xsl:element>

				<!-- for future use -->
				<!--        <xsl:choose>
				<xsl:when test="$subtype = $SUBTYPE_ORDER">
					<xsl:apply-templates select="$order/task_id" mode="copy_current">
						<xsl:with-param name="pa_name">task-no</xsl:with-param>
					</xsl:apply-templates>
				</xsl:when>
				<xsl:when test="$subtype = $SUBTYPE_INSTR">
					<xsl:apply-templates select="$instruction/task_id" mode="copy_current">
						<xsl:with-param name="pa_name">task-no</xsl:with-param>
					</xsl:apply-templates>
				</xsl:when>					
				</xsl:choose>
			<xsl:choose>
				<xsl:when test="$subtype = $SUBTYPE_ORDER">
					<xsl:apply-templates select="$order/task" mode="copy_current"/>
				</xsl:when>
				<xsl:when test="$subtype = $SUBTYPE_INSTR">
					<xsl:apply-templates select="$instruction/task" mode="copy_current"/>
				</xsl:when>					
				</xsl:choose>	-->

				<xsl:apply-templates select="$position/product"/>

				<xsl:apply-templates select="$position/exp_ship_date"/>

				<xsl:choose>
					<xsl:when test="$subtype = $SUBTYPE_ORDER">
						<xsl:apply-templates select="$order/note" mode="copy_current">
							<xsl:with-param name="pa_name">remarks</xsl:with-param>
						</xsl:apply-templates>
					</xsl:when>
					<xsl:when test="$subtype = $SUBTYPE_INSTR">
						<xsl:apply-templates select="$instruction/instruction_note"
							mode="copy_current">
							<xsl:with-param name="pa_name">remarks</xsl:with-param>
						</xsl:apply-templates>
					</xsl:when>
				</xsl:choose>

				<xsl:element name="prod-site">
					<xsl:value-of
						select="$position/product/classifications/classification[code=$CL_PAPRLOC]/value_simple_data_type"
					/>
				</xsl:element>

				<xsl:apply-templates select="$order/delivery_addresses"/>

				<xsl:apply-templates select="$position/product/edition"/>

				<xsl:apply-templates select="$position"/>

			</xsl:element>
		</xsl:element>

	</xsl:template>

	<!-- ************************************** -->
	<!-- *** Matched Templates              *** -->
	<!-- ************************************** -->

	<!-- ***** -->
	<!-- * A * -->
	<!-- ***** -->

	<xsl:template match="additionals">
		<xsl:element name="additionals">
			<xsl:for-each select="additional">
				<xsl:element name="additional">
					<xsl:apply-templates select="type_of_data" mode="copy_term"/>
					<xsl:apply-templates select="medium_type" mode="copy_term"/>
					<xsl:apply-templates select="format" mode="copy_current"/>
					<xsl:apply-templates select="condition" mode="copy_term"/>
					<xsl:apply-templates select="medium_name" mode="copy_current"/>
					<!-- #132859 -->
					<xsl:apply-templates select="additional" mode="copy_current">
						<xsl:with-param name="pa_name">add-additional</xsl:with-param>
					</xsl:apply-templates>
					<xsl:apply-templates select="note" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * B * -->
	<!-- ***** -->

	<xsl:template match="bounds">
		<xsl:element name="bounds">
			<xsl:apply-templates select="bound_inserts"/>
			<xsl:apply-templates select="spec_procs"/>
			<xsl:apply-templates select="m_parts"/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="bound_inserts">
		<xsl:element name="bound-inserts">
			<xsl:for-each select="bound_insert">
				<xsl:element name="bound-insert">
					<xsl:apply-templates select="name" mode="copy_current"/>
					<!-- #132859 -->
					<xsl:apply-templates select="comment" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * C * -->
	<!-- ***** -->

	<xsl:template match="colors">
		<xsl:element name="colors">

			<xsl:if test="../scaled_colors">
				<xsl:attribute name="scale">
					<xsl:choose>
						<xsl:when test="../scaled_colors='Y'">
							<xsl:text>yes</xsl:text>
						</xsl:when>
						<xsl:when test="../scaled_colors='N'">
							<xsl:text>no</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:attribute>
			</xsl:if>

			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>


	<xsl:template match="colors_ext">
		<!-- #132859 -->
		<xsl:variable name="elemName">
			<xsl:choose>
				<xsl:when test="name(..)='end_papers'">colors-front</xsl:when>
				<xsl:otherwise>colors-ext</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>

		<xsl:element name="{$elemName}">

			<xsl:if test="../scale_color_ext">
				<xsl:attribute name="scale">
					<xsl:choose>
						<xsl:when test="../scale_color_ext='Y'">
							<xsl:text>yes</xsl:text>
						</xsl:when>
						<xsl:when test="../scale_color_ext='N'">
							<xsl:text>no</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:attribute>
			</xsl:if>

			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="colors_int">
		<!-- #132859 -->
		<xsl:variable name="elemName">
			<xsl:choose>
				<xsl:when test="name(..)='end_papers'">colors-back</xsl:when>
				<xsl:otherwise>colors-int</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>

		<xsl:element name="{$elemName}">

			<xsl:if test="../scale_color_int">
				<xsl:attribute name="scale">
					<xsl:choose>
						<xsl:when test="../scale_color_int='Y'">
							<xsl:text>yes</xsl:text>
						</xsl:when>
						<xsl:when test="../scale_color_int='N'">
							<xsl:text>no</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:attribute>
			</xsl:if>

			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>


	<xsl:template match="colors_recto">
		<xsl:element name="colors-recto">

			<xsl:if test="../scale_color_recto">
				<xsl:attribute name="scale">
					<xsl:choose>
						<xsl:when test="../scale_color_recto='Y'">
							<xsl:text>yes</xsl:text>
						</xsl:when>
						<xsl:when test="../scale_color_recto='N'">
							<xsl:text>no</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:attribute>
			</xsl:if>

			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>


	<xsl:template match="colors_verso">
		<xsl:element name="colors-verso">

			<xsl:if test="../scale_color_verso">
				<xsl:attribute name="scale">
					<xsl:choose>
						<xsl:when test="../scale_color_verso='Y'">
							<xsl:text>yes</xsl:text>
						</xsl:when>
						<xsl:when test="../scale_color_verso='N'">
							<xsl:text>no</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:attribute>
			</xsl:if>

			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>


	<xsl:template match="container">

		<xsl:element name="series-info">

			<xsl:element name="pii">
				<xsl:value-of select="dac_key"/>
			</xsl:element>

			<xsl:element name="issn">
				<xsl:value-of select="issn"/>
			</xsl:element>

			<xsl:apply-templates select="series_abbreviation"/>
			<xsl:apply-templates select="series_title" mode="copy_current"/>

			<xsl:apply-templates select="volume_no" mode="copy_current">
				<xsl:with-param name="pa_name">volume-number</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="volume_name" mode="copy_current"/>
			<xsl:apply-templates select="originators"/>

		</xsl:element>
	</xsl:template>

	<xsl:template match="costs">

		<xsl:element name="costs">
			<xsl:for-each select="cost">
				<xsl:element name="cost">
					<xsl:apply-templates select="center" mode="copy_term"/>
					<xsl:apply-templates select="hours" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<xsl:template match="cover">

		<xsl:element name="cover">
			<xsl:apply-templates select="cover_status" mode="copy_term"/>
			<xsl:apply-templates select="design" mode="copy_term"/>
			<xsl:apply-templates select="picture_source" mode="copy_term"/>

			<xsl:apply-templates select="texts"/>
			<xsl:apply-templates select="binding"/>
			<xsl:apply-templates select="jacket"/>
			<xsl:apply-templates select="end_papers"/>

		</xsl:element>

	</xsl:template>

	<!-- ***** -->
	<!-- * D * -->
	<!-- ***** -->

	<xsl:template match="dac_master">
		<xsl:element name="manifestation">
			<xsl:attribute name="dac-master">
				<xsl:text>yes</xsl:text>
			</xsl:attribute>
			<xsl:apply-templates select="isbn" mode="copy_current"/>
			<xsl:apply-templates select="version_type" mode="copy_term"/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="delivery_addresses">

		<xsl:element name="delivery-addresses">
			<xsl:for-each select="delivery_address">
				<xsl:element name="delivery-address">
					<xsl:apply-templates select="delivery_type" mode="copy_term"/>
					<xsl:apply-templates select="quantity" mode="copy_current"/>
					<xsl:apply-templates select="note" mode="copy_current"/>
					<xsl:apply-templates select="info" mode="copy_current"/>
					<xsl:apply-templates select="label" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>

	</xsl:template>

	<xsl:template match="division">
		<xsl:element name="pmg">
			<xsl:value-of select="substring(., 2, string-length(.)-1)"/>
		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * E * -->
	<!-- ***** -->

	<xsl:template match="edition">
		<xsl:element name="book-details">

			<xsl:apply-templates select="edition_type" mode="copy_term">
				<xsl:with-param name="pa_name">version-type</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="medium" mode="copy_term"/>

			<xsl:apply-templates select="title" mode="copy_current">
				<xsl:with-param name="pa_name">book-title</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="short_title" mode="copy_current"/>

			<!-- TODO sub_head or work_title -->
			<xsl:apply-templates select="sub_head" mode="copy_current">
				<xsl:with-param name="pa_name">subtitle</xsl:with-param>
			</xsl:apply-templates>

			<xsl:variable name="impression" select="../impression"/>

			<xsl:apply-templates select="$impression/edition_no" mode="copy"/>
			<xsl:apply-templates select="division"/>

			<xsl:apply-templates select="binding" mode="copy_term">
				<xsl:with-param name="pa_name">version-binding</xsl:with-param>
			</xsl:apply-templates>

			<!--			<xsl:apply-templates select="language_1" mode="copy_without_attr">
				<xsl:with-param name="pa_name">language</xsl:with-param>
			</xsl:apply-templates>
-->
			<xsl:apply-templates select="language_1" mode="copy_term">
				<xsl:with-param name="pa_name">language</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="$impression/imprint" mode="copy_term"/>

			<xsl:apply-templates select="$impression/copyright_year" mode="copy">
				<xsl:with-param name="pa_name">copyright-yr</xsl:with-param>
			</xsl:apply-templates>

			<xsl:if test="publisher">
				<xsl:element name="division">
					<xsl:choose>
						<xsl:when test="publisher='BU2'">
							<xsl:text>Science and Technology</xsl:text>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>Health Sciences</xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:element>
			</xsl:if>

			<xsl:apply-templates select="$impression/ownership" mode="copy_term"/>

			<xsl:if test="../classifications/classification[code=$CL_PANCHAP]">
				<xsl:element name="no-of-chapters">
					<xsl:value-of
						select="../classifications/classification[code=$CL_PANCHAP]/value_decimal"/>
				</xsl:element>
			</xsl:if>

			<xsl:apply-templates select="originators"/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="exp_ship_date">
		<xsl:element name="due-date">
			<xsl:element name="date">
				<xsl:attribute name="day">
					<xsl:value-of select="substring(., 9,2)"/>
				</xsl:attribute>
				<xsl:attribute name="month">
					<xsl:value-of select="substring(., 6,2)"/>
				</xsl:attribute>
				<xsl:attribute name="year">
					<xsl:value-of select="substring(., 1,4)"/>
				</xsl:attribute>
			</xsl:element>
		</xsl:element>
	</xsl:template>


	<xsl:template match="extended">
		<xsl:element name="prod-extent">
			<xsl:attribute name="status">
				<xsl:choose>
					<xsl:when test=".='FIX'">Fixed</xsl:when>
					<xsl:otherwise>Ca.</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
			<xsl:value-of select="./@termFull"/>
		</xsl:element>
	</xsl:template>

	<xsl:template match="extras">
		<xsl:element name="extras">
			<xsl:for-each select="extra">
				<xsl:element name="extra">
					<xsl:apply-templates select="named" mode="copy_term"/>

					<xsl:apply-templates select="material" mode="copy_current">
						<xsl:with-param name="pa_name">extra-material</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="color" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<!-- ***** -->
	<!-- * I * -->
	<!-- ***** -->

	<xsl:template match="illustrations">
		<xsl:element name="illustrations">
			<xsl:for-each select="illustration">
				<xsl:element name="illustration">
					<xsl:apply-templates select="type" mode="copy_term"/>
					<xsl:apply-templates select="category" mode="copy_term"/>
					<xsl:apply-templates select="quantity" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<xsl:template match="international_measures">
		<xsl:element name="international-measures">
			<xsl:for-each select="measure">
				<xsl:element name="measure">

					<xsl:apply-templates select="@type" mode="copy">
						<xsl:with-param name="pa_name">system</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="weight" mode="copy"/>
					<xsl:apply-templates select="format" mode="copy_term"/>
					<xsl:apply-templates select="trim_size" mode="copy_term"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="inserts">
		<xsl:element name="inserts">
			<xsl:for-each select="insert">
				<xsl:element name="insert">
					<xsl:apply-templates select="name" mode="copy_current"/>
					<xsl:apply-templates select="finishing" mode="copy_term"/>
					<xsl:apply-templates select="per_sheet" mode="copy_term"/>
					<xsl:apply-templates select="colors_recto"/>
					<xsl:apply-templates select="colors_verso"/>
					<xsl:apply-templates select="pms_recto" mode="copy_current"/>
					<xsl:apply-templates select="pms_verso" mode="copy_current"/>
					<xsl:apply-templates select="material_type" mode="copy_current"/>
					<xsl:apply-templates select="paper_quality" mode="copy_term"/>

					<!-- #132859 -->
					<xsl:apply-templates select="weight" mode="copy">
						<xsl:with-param name="pa_name">weight-per-1000-sheets</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="specification" mode="copy_term">
						<xsl:with-param name="pa_name">insert-spec</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="print_form" mode="copy_term"/>
					<!-- #132859 -->
					<xsl:apply-templates select="volume" mode="copy_term"/>
					<xsl:apply-templates select="paper_req_in_sheet" mode="copy"/>

					<xsl:apply-templates select="paper_req_in_kg" mode="copy"/>
					<xsl:apply-templates select="grain" mode="copy_term"/>
					<xsl:apply-templates select="grammage" mode="copy_term"/>
					<!-- #132859 -->
					<xsl:apply-templates select="basic_size_width" mode="copy"/>
					<xsl:apply-templates select="basic_size_height" mode="copy"/>
					<!-- #134111 -->
					<xsl:apply-templates select="order_type1" mode="copy_term"/>
					<xsl:if test="order_type1/@remark">
						<xsl:element name="order-type1-remark">
							<xsl:value-of select="order_type1/@remark"/>
						</xsl:element>
					</xsl:if>
					<xsl:apply-templates select="order_type2" mode="copy_term"/>
					<xsl:if test="order_type2/@remark">
						<xsl:element name="order-type2-remark">
							<xsl:value-of select="order_type2/@remark"/>
						</xsl:element>
					</xsl:if>
					<xsl:apply-templates select="order_type3" mode="copy_term"/>
					<xsl:if test="order_type3/@remark">
						<xsl:element name="order-type3-remark">
							<xsl:value-of select="order_type3/@remark"/>
						</xsl:element>
					</xsl:if>

				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<!-- ***** -->
	<!-- * J * -->
	<!-- ***** -->

	<xsl:template match="jacket | binding | end_papers">
		<xsl:variable name="name">
			<xsl:call-template name="element_name">
				<xsl:with-param name="pa_name">
					<xsl:if test="name(.)='binding'">cover-binding</xsl:if>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:element name="{$name}">
			<xsl:apply-templates select="material_type" mode="copy_current"/>

			<xsl:apply-templates select="specification" mode="copy_term">
				<xsl:with-param name="pa_name">binding-spec</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="finishing" mode="copy_term"/>
			<xsl:apply-templates select="paper_quality" mode="copy_term"/>
			<xsl:apply-templates select="print_from" mode="copy_term"/>
			<xsl:apply-templates select="per_sheet" mode="copy_term"/>
			<xsl:apply-templates select="colors_ext"/>
			<xsl:apply-templates select="colors_int"/>

			<!-- #132859 -->
			<xsl:choose>
				<xsl:when test="name(.)='end_papers'">
					<xsl:apply-templates select="note_color_ext" mode="copy_current">
						<xsl:with-param name="pa_name">note-color-front</xsl:with-param>
					</xsl:apply-templates>
					<xsl:apply-templates select="note_color_int" mode="copy_current">
						<xsl:with-param name="pa_name">note-color-back</xsl:with-param>
					</xsl:apply-templates>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates select="note_color_ext" mode="copy_current"/>
					<xsl:apply-templates select="note_color_int" mode="copy_current"/>
				</xsl:otherwise>
			</xsl:choose>

			<xsl:apply-templates select="weight" mode="copy">
				<xsl:with-param name="pa_name">weight-per-1000-sheets</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="bulk" mode="copy_current"/>
			<xsl:apply-templates select="paper_req_in_sheet" mode="copy_current"/>
			<xsl:apply-templates select="paper_req_in_kg" mode="copy"/>
			<xsl:apply-templates select="grain" mode="copy_term"/>
			<xsl:apply-templates select="grammage" mode="copy_term"/>
			<xsl:apply-templates select="board_thickness" mode="copy_term"/>
			<xsl:apply-templates select="basic_size_width" mode="copy"/>
			<xsl:apply-templates select="basic_size_height" mode="copy"/>

		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * M * -->
	<!-- ***** -->


	<xsl:template match="m_parts">
		<xsl:element name="manuscript-parts">
			<xsl:for-each select="m_part">
				<xsl:element name="manuscript-part">
					<xsl:apply-templates select="name" mode="copy_current"/>
					<xsl:apply-templates select="authors" mode="copy_without_attr"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="materials">
		<xsl:element name="materials">
			<xsl:for-each select="material">
				<xsl:element name="material">

					<xsl:apply-templates select="status" mode="copy_term"/>
					<xsl:apply-templates select="finish_state" mode="copy_term"/>
					<xsl:apply-templates select="order_number" mode="copy_current"/>
					<xsl:apply-templates select="isbn" mode="copy_current"/>
					<xsl:apply-templates select="short_title" mode="copy_current"/>
					<xsl:apply-templates select="material_type" mode="copy_current"/>

					<xsl:apply-templates select="dimension" mode="copy_term">
						<xsl:with-param name="pa_name">material-dimension</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="description" mode="copy_current"/>
					<xsl:apply-templates select="quantity_order" mode="copy_current"/>
					<xsl:apply-templates select="quantity_consumption" mode="copy_current"/>
					<xsl:apply-templates select="cost_type" mode="copy_current"/>
					<xsl:apply-templates select="rating_unit" mode="copy_current"/>
					<xsl:apply-templates select="required_from" mode="copy_current"/>
					<xsl:apply-templates select="required_to" mode="copy_current"/>

					<xsl:apply-templates select="dimensions" mode="copy"/>

				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<!-- ***** -->
	<!-- * O * -->
	<!-- ***** -->

	<xsl:template match="originators">
		<xsl:element name="originators">
			<xsl:for-each select="originator">
				<xsl:element name="originator">

					<xsl:attribute name="sort-order">
						<xsl:value-of select="@sort_no"/>
					</xsl:attribute>

					<xsl:element name="originator-type">
						<xsl:value-of select="type/@term"/>
					</xsl:element>

					<xsl:apply-templates select="first_name" mode="copy"/>

					<xsl:element name="last-name">
						<xsl:value-of select="name"/>
					</xsl:element>

				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * P * -->
	<!-- ***** -->

	<xsl:template match="parallel_version">
		<!-- JMi 13 July 2012 -->
		<xsl:if test="isbn and edition_no=../../dac_master/edition_no">
			<xsl:element name="manifestation">
				<xsl:apply-templates select="isbn" mode="copy_current"/>
				<xsl:apply-templates select="version_type" mode="copy_term"/>
			</xsl:element>
		</xsl:if>
	</xsl:template>
	<xsl:template match="parallel_versions">
		<xsl:apply-templates/>
	</xsl:template>


	<xsl:template match="parts">
		<xsl:element name="parts">
			<xsl:apply-templates select="sections"/>
			<xsl:apply-templates select="bounds"/>
			<xsl:apply-templates select="inserts"/>
		</xsl:element>
	</xsl:template>


	<xsl:template match="product">
		<xsl:element name="book-info">

			<xsl:element name="pii">
				<xsl:value-of select="impression/dac_key"/>
			</xsl:element>

			<xsl:apply-templates select="impression/isbn" mode="copy_current"/>

			<xsl:if test="individual_classifications/classification[code=$CL_MRWCODE]">
				<xsl:element name="mrw-code">
					<xsl:value-of
						select="individual_classifications/classification[code=$CL_MRWCODE]/value_string"
					/>
				</xsl:element>
			</xsl:if>

			<!-- TODO assigned bibliographic (=main) series -->
			<xsl:apply-templates select="containers/container[@product_type='R']"/>

			<!-- JMi 7 October 2011 -->
			<xsl:element name="manifestations">
				<xsl:apply-templates select="dac_master"/>
				<xsl:apply-templates select="parallel_versions"/>
			</xsl:element>

		</xsl:element>
	</xsl:template>


	<xsl:template match="production_notes">
		<xsl:element name="production-notes">
			<xsl:for-each select="production_note">
				<xsl:element name="production-note">
					<xsl:apply-templates select="name" mode="copy_term">
						<xsl:with-param name="pa_name">note-name</xsl:with-param>
					</xsl:apply-templates>
					<xsl:apply-templates select="status" mode="copy_term">
						<xsl:with-param name="pa_name">note-status</xsl:with-param>
					</xsl:apply-templates>
					<xsl:apply-templates select="text" mode="copy_current">
						<xsl:with-param name="pa_name">note-text</xsl:with-param>
					</xsl:apply-templates>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="production_position">
		<xsl:element name="order-details">

			<xsl:element name="production-position">

				<!--			<xsl:apply-templates select = "status" mode="copy_term"/> -->
				<xsl:apply-templates select="finish_state" mode="copy_term"/>
				<xsl:apply-templates select="quantity_ordered" mode="copy_current"/>
				<xsl:apply-templates select="plan_print_run" mode="copy_current"/>

				<xsl:apply-templates select="main_currency_amounts/price" mode="copy_current">
					<xsl:with-param name="pa_name">unit-price</xsl:with-param>
				</xsl:apply-templates>

				<xsl:apply-templates select="main_currency_amounts/total" mode="copy_current">
					<xsl:with-param name="pa_name">order-line-total</xsl:with-param>
				</xsl:apply-templates>

				<xsl:apply-templates select="main_currency_amounts/currency" mode="copy_current"/>
				<xsl:apply-templates select="product_part" mode="copy_term"/>

				<xsl:apply-templates select="materials"/>

				<xsl:apply-templates select="current_delivery_date" mode="copy_current"/>
				<xsl:apply-templates select="unit" mode="copy_current"/>
				<xsl:apply-templates select="note" mode="copy_current">
					<xsl:with-param name="pa_name">order-line-notes</xsl:with-param>
				</xsl:apply-templates>

				<xsl:apply-templates select="project"/>

			</xsl:element>

		</xsl:element>
	</xsl:template>


	<xsl:template match="project">

		<xsl:element name="project">

			<xsl:apply-templates select="@id" mode="copy"/>
			<xsl:apply-templates select="@impression_id" mode="copy"/>
			<!-- #126666: Print Run Number -->
			<xsl:attribute name="els-reprint-no">
				<!-- #133725: change name _ into - -->
				<xsl:value-of select="@reprint_no + 1"/>
			</xsl:attribute>

			<xsl:apply-templates select="project_number" mode="copy_current"/>
			<xsl:apply-templates select="reprint_type" mode="copy_term"/>
			<xsl:apply-templates select="status" mode="copy_term"/>
			<xsl:apply-templates select="registration_status" mode="copy_term"/>
			<xsl:apply-templates select="product_manager" mode="copy_term"/>
			<xsl:apply-templates select="approved_on" mode="copy_current"/>
			<xsl:apply-templates select="third_party_edition" mode="copy_current"/>
			<xsl:apply-templates select="plan_publication_date" mode="copy_current"/>
			<!-- #132859 -->
			<xsl:apply-templates select="plan_delivery_date" mode="copy_current"/>
			<xsl:apply-templates select="active_publication_date" mode="copy_current"/>
			<xsl:apply-templates select="active_delivery_date" mode="copy_current"/>
			<xsl:apply-templates select="nominal_print_run" mode="copy_current"/>
			<xsl:apply-templates select="plan_print_run" mode="copy_current"/>
			<xsl:apply-templates select="actual_print_run" mode="copy_current"/>
			<xsl:apply-templates select="note" mode="copy_current">
				<xsl:with-param name="pa_name">printrun-note</xsl:with-param>
			</xsl:apply-templates>
			<xsl:apply-templates select="../main_currency_amounts/currency" mode="copy_current"/>

			<xsl:apply-templates select="segments"/>
			<xsl:apply-templates select="specification"/>
			<xsl:apply-templates select="cover"/>
			<xsl:apply-templates select="parts"/>
			<xsl:apply-templates select="production_notes"/>

		</xsl:element>
	</xsl:template>


	<!-- ***** -->
	<!-- * S * -->
	<!-- ***** -->

	<xsl:template match="sections">
		<xsl:element name="sections">
			<xsl:for-each select="section">
				<xsl:element name="section">

					<xsl:apply-templates select="grammage" mode="copy_term"/>
					<xsl:apply-templates select="colors"/>
					<xsl:apply-templates select="bulk_text" mode="copy_term"/>

					<xsl:apply-templates select="bulk" mode="copy_current"/>
					<xsl:apply-templates select="grain" mode="copy_term"/>
					<xsl:apply-templates select="extent" mode="copy_current"/>
					<xsl:apply-templates select="printed_sheet" mode="copy_current"/>
					<xsl:apply-templates select="bleed" mode="copy_term"/>

					<!-- #132859 -->
					<xsl:apply-templates select="weight" mode="copy">
						<xsl:with-param name="pa_name">weight-per-1000-sheets</xsl:with-param>
					</xsl:apply-templates>

					<xsl:apply-templates select="paper_quality" mode="copy_term"/>
					<xsl:apply-templates select="material_type" mode="copy_current"/>
					<xsl:apply-templates select="name" mode="copy_term"/>
					<xsl:apply-templates select="pages_per_sheet" mode="copy_term"/>
					<xsl:apply-templates select="plate_sets" mode="copy_current"/>
					<xsl:apply-templates select="total_sheets" mode="copy_current"/>
					<!-- #132909 -->
					<xsl:apply-templates select="total_pages" mode="copy_current"/>
					<xsl:apply-templates select="paper_req_kg" mode="copy">
						<xsl:with-param name="pa_name">paper-req-in-kg</xsl:with-param>
					</xsl:apply-templates>
					<xsl:apply-templates select="pages_from" mode="copy_current"/>
					<xsl:apply-templates select="pages_to" mode="copy_current"/>
					<xsl:apply-templates select="sheet_combi" mode="copy_current"/>
					<xsl:apply-templates select="pms" mode="copy_current"/>

					<xsl:apply-templates select="additionals"/>

					<xsl:apply-templates select="bd_sheets" mode="copy_without_attr"/>

				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="segments">
		<xsl:element name="segments">
			<xsl:for-each select="segment">
				<xsl:element name="segment">

					<xsl:apply-templates select="description" mode="copy_term"/>

					<xsl:element name="segment-quantity">
						<xsl:attribute name="approx">
							<xsl:choose>
								<xsl:when test="approx='Y'">
									<xsl:text>yes</xsl:text>
								</xsl:when>
								<xsl:when test="approx='N'">
									<xsl:text>no</xsl:text>
								</xsl:when>
							</xsl:choose>

						</xsl:attribute>
						<xsl:attribute name="calc">
							<xsl:choose>
								<xsl:when test="calc='Y'">
									<xsl:text>yes</xsl:text>
								</xsl:when>
								<xsl:when test="calc='N'">
									<xsl:text>no</xsl:text>
								</xsl:when>
							</xsl:choose>
						</xsl:attribute>
						<xsl:value-of select="quantity"/>
					</xsl:element>

					<xsl:apply-templates select="comment" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="series_abbreviation">
		<xsl:element name="jid">
			<!-- #132853 -->
			<!-- <xsl:if test = "not(starts-with(translate(. , 'BS', 'bs'), 'bs:'))">BS:</xsl:if> -->
			<xsl:choose>
				<xsl:when test="starts-with(.,'bs:')">
					<xsl:text>BS:</xsl:text>
					<xsl:value-of select="substring(.,4)"/>
				</xsl:when>
				<xsl:when test="starts-with(.,'BS:')">
					<xsl:value-of select="."/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:text>BS:</xsl:text>
					<xsl:value-of select="."/>
				</xsl:otherwise>
			</xsl:choose>
			<!-- <xsl:value-of select="." /> -->
		</xsl:element>
	</xsl:template>


	<xsl:template match="spec_procs">
		<xsl:element name="special-processes">
			<xsl:for-each select="spec_proc">
				<xsl:element name="special-process">
					<xsl:apply-templates select="name" mode="copy_current"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>


	<xsl:template match="specification">
		<xsl:element name="specification">

			<xsl:apply-templates select="classification" mode="copy_term">
				<xsl:with-param name="pa_name">spec-classification</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="extended"/>
			<xsl:apply-templates select="pages_arab" mode="copy_current"/>
			<xsl:apply-templates select="pages_roman" mode="copy_current"/>
			<xsl:apply-templates select="pages_per_sheet" mode="copy_term"/>
			<xsl:apply-templates select="weight" mode="copy"/>
			<xsl:apply-templates select="manuscript_type" mode="copy_term"/>
			<!-- #132859 -->
			<xsl:apply-templates select="manuscript_pages" mode="copy_current"/>
			<xsl:apply-templates select="copy_ed_level" mode="copy_term"/>
			<xsl:apply-templates select="tagging" mode="copy_term"/>
			<xsl:apply-templates select="typesetting_diffic" mode="copy_term"/>
			<xsl:apply-templates select="text_design_type" mode="copy_term"/>
			<xsl:apply-templates select="supplier_a" mode="copy_current"/>
			<xsl:apply-templates select="supplier_b" mode="copy_current"/>
			<xsl:apply-templates select="binding_method" mode="copy_term"/>
			<xsl:apply-templates select="spine_thickness" mode="copy"/>
			<xsl:apply-templates select="format" mode="copy_term"/>
			<xsl:apply-templates select="trim_size" mode="copy_term"/>

			<xsl:element name="biblio-ref">
				<xsl:apply-templates select="../bibl_remark"/>
			</xsl:element>

			<xsl:apply-templates select="production_method" mode="copy_term"/>
			<xsl:apply-templates select="interior_colors" mode="copy_term"/>
			<xsl:apply-templates select="print_from" mode="copy_term"/>
			<xsl:apply-templates select="manual_weight" mode="copy"/>
			<xsl:apply-templates select="types_mod" mode="copy_term"/>
			<xsl:apply-templates select="types_form" mode="copy_term"/>
			<xsl:apply-templates select="font" mode="copy_term"/>
			<xsl:apply-templates select="fontsize" mode="copy"/>
			<xsl:apply-templates select="leading" mode="copy"/>
			<xsl:apply-templates select="binding_style" mode="copy_term"/>
			<xsl:apply-templates select="spine_style" mode="copy_term"/>
			<xsl:apply-templates select="block_thickness" mode="copy"/>

			<xsl:apply-templates select="type_area"/>

			<xsl:apply-templates select="tables_bw" mode="copy_current"/>
			<xsl:apply-templates select="tables_color" mode="copy_current"/>
			<xsl:apply-templates select="formulas_math" mode="copy_current"/>
			<xsl:apply-templates select="formulas_chem" mode="copy_current"/>
			<xsl:apply-templates select="adv_internal" mode="copy_current"/>
			<xsl:apply-templates select="adv_external" mode="copy_current"/>

			<xsl:apply-templates select="costs"/>
			<xsl:apply-templates select="extras"/>
			<xsl:apply-templates select="illustrations"/>
			<xsl:apply-templates select="international_measures"/>

		</xsl:element>
	</xsl:template>


	<xsl:template match="supplier">

		<xsl:element name="executor">

			<xsl:attribute name="type">
				<xsl:value-of
					select="classifications/classification[code=$CL_EXECTYPE]/value_simple_data_type"
				/>
			</xsl:attribute>

			<xsl:attribute name="addressee">yes</xsl:attribute>

			<xsl:element name="exec-code">
				<xsl:value-of
					select="classifications/classification[code=$CL_EXECCODE]/value_string"/>
			</xsl:element>

			<xsl:element name="label">
				<xsl:value-of select="label"/>
			</xsl:element>

			<xsl:apply-templates select="name" mode="copy_current"/>

			<xsl:element name="salutation">
				<xsl:value-of select="salutation"/>
			</xsl:element>

			<xsl:apply-templates select="contact_person" mode="copy"/>

		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * T * -->
	<!-- ***** -->


	<xsl:template match="texts">
		<xsl:element name="texts">
			<xsl:for-each select="text">
				<xsl:element name="text">
					<xsl:apply-templates select="text_type" mode="copy_term"/>

					<xsl:apply-templates select="page" mode="copy_term">
						<xsl:with-param name="pa_name">text-page</xsl:with-param>
					</xsl:apply-templates>

					<!-- #132859 -->
					<xsl:apply-templates select="text" mode="copy_current">
						<xsl:with-param name="pa_name">text-text</xsl:with-param>
					</xsl:apply-templates>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>

	<xsl:template match="timestamp">
		<!-- <timestamp>2007-07-05T16:10:15Z</timestamp> -->
		<xsl:element name="time">
			<xsl:attribute name="day">
				<xsl:value-of select="substring(., 9,2)"/>
			</xsl:attribute>
			<xsl:attribute name="month">
				<xsl:value-of select="substring(., 6,2)"/>
			</xsl:attribute>
			<xsl:attribute name="year">
				<xsl:value-of select="substring(., 1,4)"/>
			</xsl:attribute>
			<xsl:attribute name="hr">
				<xsl:value-of select="substring(., 12,2)"/>
			</xsl:attribute>
			<xsl:attribute name="min">
				<xsl:value-of select="substring(., 15,2)"/>
			</xsl:attribute>
			<xsl:attribute name="sec">
				<xsl:value-of select="substring(., 18,2)"/>
			</xsl:attribute>
		</xsl:element>
	</xsl:template>


	<xsl:template match="type_area">
		<xsl:element name="type-area">
			<xsl:apply-templates select="height" mode="copy"/>
			<xsl:apply-templates select="width" mode="copy"/>
			<xsl:apply-templates select="head_margin" mode="copy"/>
			<xsl:apply-templates select="gutter_margin" mode="copy"/>

			<xsl:apply-templates select="col" mode="copy">
				<xsl:with-param name="pa_name">columns</xsl:with-param>
			</xsl:apply-templates>

			<xsl:apply-templates select="no_of_chars" mode="copy"/>
			<xsl:apply-templates select="pic_share" mode="copy"/>
		</xsl:element>
	</xsl:template>

	<!-- ***** -->
	<!-- * U * -->
	<!-- ***** -->

	<xsl:template match="userinfo">
		<xsl:element name="user-info">
			<xsl:apply-templates mode="copy"/>
		</xsl:element>
	</xsl:template>


	<!-- ************************* -->
	<!-- * Copy and rename nodes * -->
	<!-- ************************* -->

	<!-- copy only current node -->
	<xsl:template match="*" mode="copy_current">
		<xsl:param name="pa_name"/>

		<xsl:variable name="name">
			<xsl:call-template name="element_name">
				<xsl:with-param name="pa_name">
					<xsl:value-of select="$pa_name"/>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:element name="{$name}">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>

	<!-- copy only current node but use @term attribute-->
	<!-- added by MRu, 1-May-08 -->

	<xsl:template match="*" mode="copy_term">
		<xsl:param name="pa_name"/>

		<xsl:variable name="name">
			<xsl:call-template name="element_name">
				<xsl:with-param name="pa_name">
					<xsl:value-of select="$pa_name"/>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:element name="{$name}">
			<xsl:value-of select="./@term"/>
		</xsl:element>
	</xsl:template>

	<!-- copy complete node without attributes-->
	<xsl:template match="*" mode="copy_without_attr">
		<xsl:param name="pa_name"/>

		<xsl:if test="./node()">
			<xsl:variable name="name">
				<xsl:call-template name="element_name">
					<xsl:with-param name="pa_name">
						<xsl:value-of select="$pa_name"/>
					</xsl:with-param>
				</xsl:call-template>
			</xsl:variable>

			<xsl:element name="{$name}">
				<xsl:apply-templates mode="copy_without_attr"/>
			</xsl:element>
		</xsl:if>
	</xsl:template>

	<!-- copy complete node -->
	<xsl:template match="*" mode="copy">
		<xsl:param name="pa_name"/>

		<xsl:variable name="name">
			<xsl:call-template name="element_name">
				<xsl:with-param name="pa_name">
					<xsl:value-of select="$pa_name"/>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:element name="{$name}">
			<xsl:apply-templates select="@*" mode="copy"/>
			<xsl:apply-templates mode="copy"/>
		</xsl:element>
	</xsl:template>

	<!-- Attributes -->
	<xsl:template match="@*" mode="copy">
		<xsl:param name="pa_name"/>

		<xsl:variable name="name">
			<xsl:call-template name="element_name">
				<xsl:with-param name="pa_name">
					<xsl:value-of select="$pa_name"/>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:attribute name="{$name}">
			<xsl:value-of select="."/>
		</xsl:attribute>
	</xsl:template>

	<!-- PCData -->
	<xsl:template match="text()" mode="copy">
		<xsl:value-of select="."/>
	</xsl:template>


	<!-- ************************************** -->
	<!-- *** Called Templates               *** -->
	<!-- ************************************** -->


	<xsl:template name="element_name">
		<xsl:param name="pa_name"/>

		<xsl:choose>
			<xsl:when test="$pa_name!=''">
				<xsl:value-of select="$pa_name"/>
			</xsl:when>

			<xsl:otherwise>
				<xsl:value-of select="translate(local-name(), '_', '-')"/>
			</xsl:otherwise>
		</xsl:choose>

	</xsl:template>

</xsl:stylesheet>
