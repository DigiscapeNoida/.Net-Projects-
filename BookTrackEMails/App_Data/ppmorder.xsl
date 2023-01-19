<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    
    <xsl:output method="html"/>
    
<!--
    Stylesheet to view PPM supplier orders in HTML via BUD
    Version 0.5  5 June 2013
    Copyright (c) 2008-2013 Elsevier B.V.
    
    v0.1  21 April 2008
    First draft by MRu - for testing
    This draft version does not contain mappings for covers, jackets, endpapers, inserts, etc
    
    v0.2  12 June 2008
    Various small changes to track development of order DTD
    
    v0.3  19 June 2008
    Fix by Infosys to work around Klopotek export style of <br> tags
    
    v0.4  23 July 2008
    Added new work order types
    
    v0.5  5 June 2013
    Displaying new elements series-type, manifestations, language and biblio-ref
-->
    
            
    <!--################################################################################-->
    <xsl:template match="/">
        <!-- root -->
        <xsl:apply-templates select="orders"/>
    </xsl:template>
    
    <!--################################################################################-->
    
    <xsl:template match="orders">
        <html>
            <head></head>
            <body style="font-family:'Arial Unicode MS'">
                <xsl:for-each select="order">
                    
                    <h3>
                        <xsl:choose>
                            <xsl:when test="@type='work-order'">
                                <xsl:choose>
                                    <xsl:when test="stage/@step='ARTWORK-ORDER'">ARTWORK WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='COMBINED-MFG-ORDER'">COMBINED MANUFACTURING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='CONVERSION-ORDER'">CONVERSION WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='COVER-DESIGN-ORDER'">COVER DESIGN WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='COVER-MFG-ORDER'">COVER MANUFACTURING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='COPYEDIT-ORDER'">COPYEDITING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='DESIGN-ORDER'">DESIGN WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='FREELANCE-ORDER'">FREELANCE WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='FULL-SERVICE-ORDER'">FULL-SERVICE WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='GENERIC-ORDER'">GENERIC WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='INDEXING-ORDER'">INDEXING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='MS-DEV-ORDER '">MANUSCRIPT DEVELOPMENT WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='MEDIA-DEV-ORDER'">MEDIA DEVELOPMENT WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='MEDIA-PROOF-ORDER'">MEDIA PROOFING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='MEDIA-REPL-ORDER'">MEDIA REPLICATION WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='PRINT-BIND-ORDER '">PRINT/BIND WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='PROJECT-MGMT-ORDER'">PROJECT MANAGEMENT WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-ORDER'">PROOFREADING WORK ORDER</xsl:when>
                                    <xsl:when test="stage/@step='TYPESET-ORDER'">TYPESETTING WORK ORDER</xsl:when>
                                </xsl:choose>
                            </xsl:when>
                            <xsl:when test="@type='instruction'">
                                <xsl:choose>
                                    <xsl:when test="stage/@step='CAST-OFF-BOOK'">CAST-OFF BOOK INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='CAST-OFF-BATCH'">CAST-OFF BATCH INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='SAMPLE-PAGES'">GENERATE SAMPLE PAGES INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='COPYEDIT-BOOK'">COPYEDIT BOOK INSTRUCTION (copyediting work order)</xsl:when>
                                    <xsl:when test="stage/@step='COPYEDIT-BATCH'">COPYEDIT BATCH INSTRUCTION (copyediting work order)</xsl:when>
                                    <xsl:when test="stage/@step='1ST-ARTWORK'">GENERATE DRAFT ARTWORK INSTRUCTION (artwork work order)</xsl:when>
                                    <xsl:when test="stage/@step='CORRECT-ARTWORK'">GENERATE CORRECTED ARTWORK INSTRUCTION (artwork work order)</xsl:when> 
                                    <xsl:when test="stage/@step='TYPESET-BOOK'">GENERATE 1ST PROOFS BOOK INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='TYPESET-BATCH'">GENERATE 1ST PROOFS BATCH INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-1PR-BOOK'">PROOFREAD 1ST PROOFS BOOK INSTRUCTION (proofreading work order)</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-1PR-BATCH'">PROOFREAD 1ST PROOFS BATCH INSTRUCTION (proofreading work order)</xsl:when> 
                                    <xsl:when test="stage/@step='2ND-PROOF-BOOK'">GENERATE 2ND PROOFS BOOK INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='2ND-PROOF-BATCH'">GENERATE 2ND PROOFS BATCH INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-2PR-BOOK'">PROOFREAD 2ND PROOFS BOOK INSTRUCTION (proofreading work order)</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-2PR-BATCH'">PROOFREAD 2ND PROOFS BATCH INSTRUCTION (proofreading work order)</xsl:when> 
                                    <xsl:when test="stage/@step='PAGE-PROOF-BOOK'">GENERATE PAGE PROOFS BOOK INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='PROOFREAD-PAGE-PROOF'">PROOFREAD PAGE PROOFS BOOK INSTRUCTION (proofreading work order)</xsl:when>
                                    <xsl:when test="stage/@step='INDEX-BOOK'">INDEX BOOK INSTRUCTION (indexing work order)</xsl:when>
                                    <xsl:when test="stage/@step='INDEX-BATCH'">INDEX BATCH INSTRUCTION (indexing work order)</xsl:when>
                                    <xsl:when test="stage/@step='TYPESET-INDEX'">TYPESET INDEX INSTRUCTION (typesetting/full-service work order)</xsl:when>  
                                    <xsl:when test="stage/@step='2ND-PAGE-PROOF'">GENERATE 2ND PAGE PROOFS INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='FINAL-PROOF'">GENERATE FINAL PROOFS INSTRUCTION (typesetting/full-service work order)</xsl:when>
                                    <xsl:when test="stage/@step='PRINT-READY-FILES'">GENERATE PRINT-READY FILES INSTRUCTION (typesetting/full-service work order)</xsl:when> 
                                    <xsl:when test="stage/@step='Q300'">GENERATE Q300 DATASET INSTRUCTION (typesetting, full-service or conversion work order)</xsl:when>
                                    <xsl:when test="stage/@step='H300'">GENERATE H300 DATASET INSTRUCTION (typesetting, full-service or conversion work order)</xsl:when>
                                    <xsl:when test="stage/@step='O300'">GENERATE O300 DATASET INSTRUCTION (typesetting, full-service or conversion work order)</xsl:when>
                                    <xsl:when test="stage/@step='H350'">GENERATE H350 DATASET INSTRUCTION (typesetting, full-service or conversion work order)</xsl:when>
                                </xsl:choose>
                            </xsl:when>
                                
                        </xsl:choose>
                        
                    </h3>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="0">

                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">WORK ORDER NUMBER</xsl:with-param><xsl:with-param name="element" select="order-no"/>
                                    </xsl:call-template>

                                    <xsl:if test="@type='instruction'">
                                        <xsl:call-template name="simple-row">
                                            <xsl:with-param name="name">INSTRUCTION NUMBER</xsl:with-param><xsl:with-param name="element" select="instruction-no"/>
                                        </xsl:call-template>
                                    </xsl:if>

                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Book title/subtitle</xsl:with-param><xsl:with-param name="element" select="book-details/book-title"/>
                                    </xsl:call-template>
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name"/><xsl:with-param name="element" select="book-details/subtitle"/>
                                    </xsl:call-template>
                                    
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Short title</xsl:with-param><xsl:with-param name="element" select="book-details/short-title"/>
                                    </xsl:call-template>

                                    <xsl:call-template name="composite-row">
                                        <xsl:with-param name="name">Lead author</xsl:with-param>
                                        <xsl:with-param name="component1" select="book-details/originators/originator[originator-type='Author']/first-name[1]"/>
                                        <xsl:with-param name="component2" select="book-details/originators/originator[originator-type='Author']/last-name[1]"/>
                                    </xsl:call-template>

                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">PII</xsl:with-param><xsl:with-param name="element" select="book-info/pii"/>
                                    </xsl:call-template>

                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">ISBN</xsl:with-param><xsl:with-param name="element" select="book-info/isbn"/>
                                    </xsl:call-template>
                                    
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">MRW code</xsl:with-param><xsl:with-param name="element" select="book-info/mrw-code"/>
                                    </xsl:call-template>
                                    
                                    <xsl:if test="book-info/series-info">
                                        <xsl:apply-templates select="book-info/series-info"/>
                                    </xsl:if>
                                    
                                    <xsl:call-template name="insert-manifestations"/>
                                    
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Project manager</xsl:with-param><xsl:with-param name="element" select="order-details/production-position/project/product-manager"/>
                                    </xsl:call-template>
                                    
                                    <xsl:apply-templates select="executor"/>
                                    
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Production site</xsl:with-param><xsl:with-param name="element" select="prod-site"/>
                                    </xsl:call-template>

                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Due date</xsl:with-param><xsl:with-param name="element" select="due-date"/>
                                    </xsl:call-template>
                                    
                                    <xsl:if test="stage[@step='Q300' or @step='H300' or @step='O300' or @step='H350']">
                                        <xsl:call-template name="simple-row">
                                            <xsl:with-param name="name">Dataset version no</xsl:with-param><xsl:with-param name="element" select="book-info/version-no"/>
                                        </xsl:call-template>
                                    </xsl:if>
                                    
                                    <xsl:call-template name="simple-row">
                                        <xsl:with-param name="name">Remarks</xsl:with-param><xsl:with-param name="element" select="remarks"/>
                                    </xsl:call-template>

                                </table>
                            </td>

                        </tr>
                    </table>

                    <h4>Additional book information</h4>
                    <xsl:apply-templates select="book-details"/>

                    <h4>Additional notes</h4>
                    <xsl:apply-templates select="order-details/production-position/project/production-notes"/>

                    <h4>Details of the order</h4>

                    <h5>Delivery details</h5>
                    <xsl:apply-templates select="delivery-addresses"/>
                    
                    <h5>Dates and quantities</h5>
                    <xsl:apply-templates select="order-details/production-position"/>
                    <xsl:apply-templates select="order-details/production-position/project"/>
                    
                    <h5>Key measurements</h5>
                    <xsl:apply-templates select="international-measures"/>
                    
                    <h5>Production specification</h5>
                    <xsl:apply-templates select="order-details/production-position/project/specification"/>
                    
<!--                <h5>Covers, jackets and endpapers</h5>
                    To be added... -->
                    
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>

    
    <!--################################################################################-->
    <!-- Series membership information -->
    
    <xsl:template match="series-info">
        
        <tr>
            <td valign="top">Parent series</td>
            <td colspan="2">
                <table bgcolor="#EEEEEE">

                    <xsl:call-template name="simple-row">
                        <xsl:with-param name="name">Title</xsl:with-param><xsl:with-param name="element" select="series-title"/>
                    </xsl:call-template>

                    <xsl:call-template name="simple-row">
                        <xsl:with-param name="name">ISSN</xsl:with-param><xsl:with-param name="element" select="issn"/>
                    </xsl:call-template>

                    <xsl:call-template name="simple-row">
                        <xsl:with-param name="name">JID</xsl:with-param><xsl:with-param name="element" select="jid"/>
                    </xsl:call-template>

                    <xsl:call-template name="simple-row">
                        <xsl:with-param name="name">PII</xsl:with-param><xsl:with-param name="element" select="pii"/>
                    </xsl:call-template>
                    
                    <xsl:call-template name="composite-row">
                        <xsl:with-param name="name">Volume</xsl:with-param>
                        <xsl:with-param name="component1" select="volume-name"/>
                        <xsl:with-param name="component2" select="volume-number"/>
                    </xsl:call-template>
                    
                    <xsl:if test="count(originators/originator[contains(originator-type,'Editor')])>0">
                        <xsl:apply-templates mode="editors" select="originators"/>
                    </xsl:if>

                </table>
                
            </td>
            
        </tr>
        
    </xsl:template>


    <!--################################################################################-->
    <!-- Executor information -->
    
    <xsl:template match="executor">
            
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">
                <xsl:choose>
                    <xsl:when test="@type='TYPESETTER'">Typesetter</xsl:when>
                    <xsl:when test="@type='PRINTER'">Printer</xsl:when>
                    <xsl:when test="@type='COPYEDITOR'">Copyeditor</xsl:when>
                    <xsl:when test="@type='PROOFREADER'">Proofreader</xsl:when>
                    <xsl:when test="@type='DESIGNER'">Designer</xsl:when>
                    <xsl:when test="@type='MULTIMEDIA'">Multimedia</xsl:when>
                    <xsl:when test="@type='INDEXER'">Indexer</xsl:when>
                </xsl:choose>
            </xsl:with-param>
            <xsl:with-param name="element" select="name"/>
        </xsl:call-template>
       
        <xsl:call-template name="composite-row">
            <xsl:with-param name="name">attn. </xsl:with-param>
            <xsl:with-param name="component1" select="contact-person/first-name"/>
            <xsl:with-param name="component2" select="contact-person/last-name"/>
        </xsl:call-template>       

        <xsl:call-template name="simple-row">
            <xsl:with-param name="name"/><xsl:with-param name="element" select="label"/>
        </xsl:call-template>

    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Production notes -->

    <xsl:template match="production-notes">
        
        <table width="75%">
            <xsl:for-each select="production-note[note-status='Released']">
                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name" select="note-name"/>
                    <xsl:with-param name="element" select="note-text"/>
                </xsl:call-template>
            </xsl:for-each>
        </table>
        
    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Book details -->
    
    <xsl:template match="book-details">

        <table>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Book type</xsl:with-param><xsl:with-param name="element" select="version-type"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Division</xsl:with-param><xsl:with-param name="element" select="division"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Ownership</xsl:with-param><xsl:with-param name="element" select="ownership"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Language</xsl:with-param><xsl:with-param name="element" select="language"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Imprint</xsl:with-param><xsl:with-param name="element" select="imprint"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Copyright year</xsl:with-param><xsl:with-param name="element" select="copyright-yr"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Edition number</xsl:with-param><xsl:with-param name="element" select="edition-no"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">PMG</xsl:with-param><xsl:with-param name="element" select="pmg"/>
            </xsl:call-template>
           
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Binding</xsl:with-param><xsl:with-param name="element" select="version-binding"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Medium</xsl:with-param><xsl:with-param name="element" select="medium"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Number of chapters</xsl:with-param><xsl:with-param name="element" select="no-of-chapters"/>
            </xsl:call-template>
            
            <xsl:if test="count(originators/originator[contains(originator-type,'Editor')])>0">
                <xsl:apply-templates select="originators" mode="editors"/>
            </xsl:if>
            
            <xsl:if test="count(originators/originator[contains(originator-type,'Author')])>0">
                <xsl:apply-templates select="originators" mode="authors"/>
            </xsl:if>
            
        </table>

    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Originators -->
    
    
    <xsl:template match="originators" mode="editors">
        <tr>
            <td valign="top">Editors</td><td valign="top"><xsl:text xml:space="preserve"> : </xsl:text></td>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <xsl:for-each select="originator[contains(originator-type,'Editor')]">
                        <tr>
                            <td valign="top">
                                <xsl:value-of select="first-name"/><xsl:text> </xsl:text><xsl:value-of select="last-name"/>
                                <xsl:text> (</xsl:text><xsl:value-of select="originator-type"/><xsl:text>)</xsl:text>
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </td>
        </tr>
    </xsl:template>
    
    <xsl:template match="originators" mode="authors">
        <tr>
            <td valign="top">Editors</td><td valign="top"><xsl:text xml:space="preserve"> : </xsl:text></td>
            <td>
                <table cellspacing="0" cellpadding="0">
                    <xsl:for-each select="originator[contains(originator-type,'Author')]">
                        <tr>
                            <td valign="top">
                                <xsl:value-of select="first-name"/><xsl:text> </xsl:text><xsl:value-of select="last-name"/>
                                <xsl:text> (</xsl:text><xsl:value-of select="originator-type"/><xsl:text>)</xsl:text>
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </td>
        </tr>
    </xsl:template>


    <!--################################################################################-->
    <!-- Delivery addresses -->
    
    <xsl:template match="delivery-addresses">
        <xsl:for-each select="delivery-address">
            <table>

                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">Delivery type</xsl:with-param><xsl:with-param name="element" select="delivery-type"/>
                </xsl:call-template>

                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">Quantity</xsl:with-param><xsl:with-param name="element" select="quantity"/>
                </xsl:call-template>

                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">Note</xsl:with-param><xsl:with-param name="element" select="note"/>
                </xsl:call-template>

                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">Information</xsl:with-param><xsl:with-param name="element" select="info"/>
                </xsl:call-template>
                
                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">Address</xsl:with-param><xsl:with-param name="element" select="label"/>
                </xsl:call-template>

            </table>
        </xsl:for-each>
    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Order details - this section to be completed -->
    
    <xsl:template match="production-position">
        <!-- status -->
        <!-- finish-state? -->
        
        <table>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Order quantity</xsl:with-param><xsl:with-param name="element" select="quantity-ordered"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Planned print run</xsl:with-param><xsl:with-param name="element" select="plan-print-run"/>
            </xsl:call-template>
            
            <xsl:call-template name="composite-row">
                <xsl:with-param name="name">Unit price</xsl:with-param>
                <xsl:with-param name="component1" select="currency"/>
                <xsl:with-param name="component2" select="unit-price"/>
            </xsl:call-template>
            
            <xsl:call-template name="composite-row">
                <xsl:with-param name="name">Order value</xsl:with-param>
                <xsl:with-param name="component1" select="currency"/>
                <xsl:with-param name="component2" select="order-line-total"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Product part</xsl:with-param><xsl:with-param name="element" select="product-part"/>
            </xsl:call-template>
            
            
            <!-- materials? (material) -->
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Current delivery date</xsl:with-param><xsl:with-param name="element" select="current-delivery-date"/>
            </xsl:call-template>
            
            <!-- unit? -->
            
        </table>
    </xsl:template>


    <!--################################################################################-->
    <!-- Material details - this section to be completed -->
    
    <xsl:template match="material">
        <!--    
        status
        finish-state
        order-number
        isbn?
        short-title
        material-type
        material-dimension 
        description
        quantity-order
        quantity-consumption
        cost-type
        rating-unit
        required-from
        required-to
        dimensions? (dimension (attribute, value) )
        -->
    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Project details - this section to be refined -->
    
    <xsl:template match="project">
        <table>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">PPM project number</xsl:with-param><xsl:with-param name="element" select="project-number"/>
            </xsl:call-template>


            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Reprint type</xsl:with-param><xsl:with-param name="element" select="reprint-type"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Registration status</xsl:with-param><xsl:with-param name="element" select="registration-status"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Third-party edition</xsl:with-param><xsl:with-param name="element" select="third-party-edition"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Approval date</xsl:with-param><xsl:with-param name="element" select="approved-on"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Planned publication date</xsl:with-param><xsl:with-param name="element" select="plan-publication-date"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Planned delivery date</xsl:with-param><xsl:with-param name="element" select="plan-delivery-date"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Active publication date</xsl:with-param><xsl:with-param name="element" select="active-publication-date"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Active delivery date</xsl:with-param><xsl:with-param name="element" select="active-delivery-date"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Nominal print run</xsl:with-param><xsl:with-param name="element" select="nominal-print-run"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Planned print run</xsl:with-param><xsl:with-param name="element" select="plan-print-run"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Actual print run</xsl:with-param><xsl:with-param name="element" select="actual-print-run"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Note</xsl:with-param><xsl:with-param name="element" select="note"/>
            </xsl:call-template>
        
<!--        segments? (segment ( description, segment-quantity, comment? ) ) -->
        </table>
    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- Specification details - this section to be completed -->
    
    <xsl:template match="specification">
        <table>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Project classification</xsl:with-param><xsl:with-param name="element" select="spec-classification"/>
            </xsl:call-template>

            <xsl:call-template name="composite-row">
                <xsl:with-param name="name">Page extent</xsl:with-param>
                <xsl:with-param name="component1" select="prod-extent"/>
                <xsl:with-param name="component2"><xsl:text>(</xsl:text><xsl:value-of select="prod-extent/@status"/><xsl:text>)</xsl:text></xsl:with-param>
            </xsl:call-template>

            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Pages</xsl:with-param>
                <xsl:with-param name="element" select="pages-arab"/>
                <xsl:with-param name="attribute">(arabic)</xsl:with-param>
            </xsl:call-template>
            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name"/>
                <xsl:with-param name="element" select="pages-roman"/>
                <xsl:with-param name="attribute">(roman)</xsl:with-param>
            </xsl:call-template>            

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Pages per sheet</xsl:with-param><xsl:with-param name="element" select="pages-per-sheet"/>
            </xsl:call-template>

            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Weight</xsl:with-param>
                <xsl:with-param name="element" select="weight"/>
                <xsl:with-param name="attribute" select="weight/@unit"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Manuscript type</xsl:with-param><xsl:with-param name="element" select="manuscript-type"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Manuscript pages</xsl:with-param><xsl:with-param name="element" select="manuscript-pages"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Copyedit level</xsl:with-param><xsl:with-param name="element" select="copy-ed-level"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Tagging</xsl:with-param><xsl:with-param name="element" select="tagging"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Text design type</xsl:with-param><xsl:with-param name="element" select="text-design-type"/>
            </xsl:call-template>
            
<!--    supplier-a
        supplier-b -->

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Binding style</xsl:with-param><xsl:with-param name="element" select="binding-style"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Binding method</xsl:with-param><xsl:with-param name="element" select="binding-method"/>
            </xsl:call-template>
            
            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Spine thickness</xsl:with-param>
                <xsl:with-param name="element" select="spine-thickness"/>
                <xsl:with-param name="attribute" select="spine-thickness/@unit"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Spine style</xsl:with-param><xsl:with-param name="element" select="spine-style"/>
            </xsl:call-template>
            
            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Block thickness</xsl:with-param>
                <xsl:with-param name="element" select="block-thickness"/>
                <xsl:with-param name="attribute" select="block-thickness/@unit"/>
            </xsl:call-template>
            
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Format</xsl:with-param><xsl:with-param name="element" select="format"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Trim size</xsl:with-param><xsl:with-param name="element" select="trim-size"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Bibliographical reference</xsl:with-param><xsl:with-param name="element" select="biblio-ref"/>
            </xsl:call-template>

            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Production method</xsl:with-param><xsl:with-param name="element" select="production-method"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Interior colours</xsl:with-param><xsl:with-param name="element" select="interior-colors"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Print from</xsl:with-param><xsl:with-param name="element" select="print-from"/>
            </xsl:call-template>
            
            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Manual weight</xsl:with-param>
                <xsl:with-param name="element" select="manual-weight"/>
                <xsl:with-param name="attribute" select="manual-weight/@unit"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Typeset modifications up to</xsl:with-param><xsl:with-param name="element" select="types-mod"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Typeset format</xsl:with-param><xsl:with-param name="element" select="types-form"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Typesetting difficulty</xsl:with-param><xsl:with-param name="element" select="typesetting-diffic"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Font</xsl:with-param><xsl:with-param name="element" select="font"/>
            </xsl:call-template>
                
            <xsl:call-template name="row-with-attribute">
                <xsl:with-param name="name">Font size</xsl:with-param>
                <xsl:with-param name="element" select="fontsize"/>
                <xsl:with-param name="attribute" select="fontsize/@unit"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Leading</xsl:with-param><xsl:with-param name="element" select="leading"/>
            </xsl:call-template>
            
            <xsl:if test="type-area">
                <xsl:call-template name="row-label">
                    <xsl:with-param name="name">Typographic area</xsl:with-param>
                </xsl:call-template>
                
                <xsl:call-template name="row-with-attribute">
                    <xsl:with-param name="name">.Height</xsl:with-param>
                    <xsl:with-param name="element" select="type-area/height"/>
                    <xsl:with-param name="attribute" select="type-area/height/@unit"/>
                </xsl:call-template>
                    
                <xsl:call-template name="row-with-attribute">
                    <xsl:with-param name="name">.Width</xsl:with-param>
                    <xsl:with-param name="element" select="type-area/width"/>
                    <xsl:with-param name="attribute" select="type-area/width/@unit"/>
                </xsl:call-template>
                    
                <xsl:call-template name="row-with-attribute">
                    <xsl:with-param name="name">.Head margin</xsl:with-param>
                    <xsl:with-param name="element" select="type-area/head-margin"/>
                    <xsl:with-param name="attribute" select="type-area/head-margin/@unit"/>
                </xsl:call-template>
                    
                <xsl:call-template name="row-with-attribute">
                    <xsl:with-param name="name">.Gutter margin</xsl:with-param>
                    <xsl:with-param name="element" select="type-area/gutter-margin"/>
                    <xsl:with-param name="attribute" select="type-area/gutter-margin/@unit"/>
                </xsl:call-template>
                    
                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">.Columns</xsl:with-param><xsl:with-param name="element" select="type-area/columns"/>
                </xsl:call-template>
                    
                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">.No of characters</xsl:with-param><xsl:with-param name="element" select="type-area/no-of-chars"/>
                </xsl:call-template>
                    
                <xsl:call-template name="simple-row">
                    <xsl:with-param name="name">.Picture share</xsl:with-param><xsl:with-param name="element" select="type-area/pic-share"/>
                </xsl:call-template>
            </xsl:if>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Tables (b/w)</xsl:with-param><xsl:with-param name="element" select="tables-bw"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Tables (colour)</xsl:with-param><xsl:with-param name="element" select="tables-color"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Formulae (math)</xsl:with-param><xsl:with-param name="element" select="formulas-math"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Formulae (chemical)</xsl:with-param><xsl:with-param name="element" select="formulas-chem"/>
            </xsl:call-template>
    
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Advertisements (internal)</xsl:with-param><xsl:with-param name="element" select="adv-internal"/>
            </xsl:call-template>
            
            <xsl:call-template name="simple-row">
                <xsl:with-param name="name">Advertisements (external)</xsl:with-param><xsl:with-param name="element" select="adv-external"/>
            </xsl:call-template>

            <xsl:if test="illustrations">
                <xsl:call-template name="row-label">
                    <xsl:with-param name="name">Illustrations</xsl:with-param>
                </xsl:call-template>
                <xsl:for-each select="illustrations/illustration">
                   <xsl:call-template name="simple-row">
                       <xsl:with-param name="name">
                           <xsl:value-of select="type"/>
                           <xsl:text xml:space="preserve"> - </xsl:text>
                           <xsl:value-of select="category"/>
                       </xsl:with-param>
                       <xsl:with-param name="element" select="quantity"/>
                   </xsl:call-template>
               </xsl:for-each>
            </xsl:if>
            
            <xsl:if test="costs">
                <xsl:call-template name="row-label">
                    <xsl:with-param name="name">Costs</xsl:with-param>
                </xsl:call-template>
                <xsl:for-each select="cost">
                    <xsl:call-template name="row-with-attribute">
                        <xsl:with-param name="name" select="center"/>
                        <xsl:with-param name="element" select="hours"/>
                        <xsl:with-param name="attribute">hours</xsl:with-param>   
                    </xsl:call-template>
                </xsl:for-each>
            </xsl:if>
    
            <xsl:if test="extras">
                <xsl:call-template name="row-label">
                    <xsl:with-param name="name">Extras</xsl:with-param>
                </xsl:call-template>
                <xsl:for-each select="extra">
                    <xsl:call-template name="composite-row">
                        <xsl:with-param name="name" select="named"/>
                        <xsl:with-param name="component1" select="extra-material"/>
                        <xsl:with-param name="component2" select="color"/>
                    </xsl:call-template>
                </xsl:for-each>
            </xsl:if>
    
        </table>
    </xsl:template>
    
    
    <!--################################################################################-->
    <!-- International measures -->
    
    <xsl:template match="international-measures">
        
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Weight (<xsl:value-of select="measure[1]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[1]/weight"/>
        </xsl:call-template>
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Weight (<xsl:value-of select="measure[2]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[2]/weight"/>
        </xsl:call-template>        
        
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Format (<xsl:value-of select="measure[1]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[1]/format"/>
        </xsl:call-template>
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Format (<xsl:value-of select="measure[2]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[2]/format"/>
        </xsl:call-template>
        
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Trim size (<xsl:value-of select="measure[1]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[1]/trim-size"/>
        </xsl:call-template>
        <xsl:call-template name="simple-row">
            <xsl:with-param name="name">Trim size (<xsl:value-of select="measure[2]/@system"/>)</xsl:with-param>
            <xsl:with-param name="element" select="measure[2]/trim-size"/>
        </xsl:call-template>
        
    </xsl:template>
    
    <!--################################################################################-->
    <!-- Cover details - this section to be completed -->
    
    <xsl:template match="cover">
        <!--    
        cover-status
        design
        picture-source
        texts? ( text ( text-type, text-page, text-text ) )
        cover-binding? ( material-type, binding-spec, finishing, paper-quality, print-from, per-sheet, colors-ext?, colors-int?, note-color-int?, note-color-ext?, weight-per-1000-sheets, bulk, paper-req-in-sheet, paper-req-in-kg, grain, grammage, board-thickness, basic-size-width, basic-size-height )
        jacket? ( material-type, binding-spec, finishing, paper-quality, print-from, per-sheet, colors-ext?, colors-int?, note-color-ext?, note-color-int?, weight-per-1000-sheets?, bulk?, paper-req-in-sheet?, paper-req-in-kg?, grain?, grammage?, basic-size-width?, basic-size-height? )
        end-papers? ( material-type, biding-spec, finishing?, paper-quality?, print-from?, per-sheet?, colors-front?, colors-back?, note-color-front?, note-color-back?, weight-per-1000-sheets?, bulk?, paper-req-in-sheet?, paper-req-in-kg?, grain?, grammage?, basic-size-width?, basic-size-height? )
        -->        
    </xsl:template>


    <!--################################################################################-->
    <!-- Parts details - this section to be completed -->
    
    <xsl:template match="parts">
        <!--
        sections ( section ( grammage, colors, bulk-text, bulk, grain, extent, printed-sheet, bleed, weight-per-1000-sheets?, paper-quality, material-type, name, pages-per-sheet, plate-sets, total-sheets?, paper-req-in-kg?, pages-from?, pages-to?, sheet-combi?, pms?, additionals?, bd-sheets? ) )
        bounds ( bound-inserts?, special-processes?, manuscript-parts? )
        inserts? ( insert ( name, finishing, per-sheet, colors-recto?, colors-verso?, pms-recto?, pms-verso?, material-type, paper-quality, weight-per-1000-sheets?, insert-spec, print-form, volume, paper-req-in-sheet?, paper-req-in-kg?, grain, grammage, basic-size-width?, basic-size-height?, order-type1?, order-type2?, order-type3? ) )
        -->        
    </xsl:template>


    <!--################################################################################-->
    <!-- Simple and composite templates to add elements to table row (if source element exists) -->
    
    <xsl:template name="simple-row">
        <xsl:param name="element"/>
        <xsl:param name="name"/>
            <xsl:if test="string-length($element)>0">
                <tr>
                    <td valign="top"><xsl:value-of select="$name"/></td>
                    <td valign="top"><xsl:if test="string-length($name)>0"><xsl:text xml:space="preserve"> : </xsl:text></xsl:if></td>
                    <td valign="top">
						<xsl:call-template name="substitutebr">
							<xsl:with-param name="text" select="$element"/>
							<xsl:with-param name="replace">&lt;br/></xsl:with-param>
							<xsl:with-param name="disable-output-escaping" select="yes"/>
						</xsl:call-template>
					</td>
                </tr>    
            </xsl:if>    
    </xsl:template>
    
    <xsl:template name="row-with-attribute">
        <xsl:param name="name"/>
        <xsl:param name="element"/>
        <xsl:param name="attribute"/>
        <xsl:if test="string-length($element)>0">
            <tr>
                <td valign="top"><xsl:value-of select="$name"/></td><td valign="top"><xsl:if test="string-length($name)>0"><xsl:text xml:space="preserve"> : </xsl:text></xsl:if></td>
                <td valign="top">
                    <xsl:value-of select="$element"/><xsl:text> </xsl:text><xsl:value-of select="$attribute"/>
                </td>
            </tr>    
        </xsl:if> 
    </xsl:template>
    
    <xsl:template name="composite-row">
        <xsl:param name="name"/>
        <xsl:param name="component1"/>
        <xsl:param name="component2"/>
        <xsl:if test="string-length($component1)>0 or string-length($component2)>0">
            <tr>
                <td valign="top"><xsl:value-of select="$name"/></td><td valign="top"><xsl:if test="string-length($name)>0"><xsl:text xml:space="preserve"> : </xsl:text></xsl:if></td>
                <td valign="top">
                    <xsl:if test="string-length($component1)>0">
                        <xsl:value-of select="$component1"/><xsl:if test="string-length($component2)>0"><xsl:text> </xsl:text></xsl:if>
                    </xsl:if>
                    <xsl:if test="string-length($component2)>0">
                        <xsl:value-of select="$component2"/>
                    </xsl:if>
                </td>
            </tr>
        </xsl:if>
    </xsl:template>

    <xsl:template name="row-label">
        <xsl:param name="name"/>
            <tr>
                <td valign="top"><i><xsl:value-of select="$name"/></i></td>
            </tr>
    </xsl:template>

	<xsl:template name="substitutebr">
		<xsl:param name="text"/>
		<xsl:param name="replace"/>
		<xsl:param name="disable-output-escaping">no</xsl:param>

		<xsl:choose>
			<xsl:when test="string-length($replace) = 0 and $disable-output-escaping = 'yes'">
				<xsl:value-of select="$text" disable-output-escaping="yes"/>
			</xsl:when>
			<xsl:when test="string-length($replace) = 0">
				<xsl:value-of select="$text"/>
			</xsl:when>
			<xsl:when test="contains($text, $replace)">

				<xsl:variable name="before" select="substring-before($text, $replace)"/>
				<xsl:variable name="after" select="substring-after($text, $replace)"/>

				<xsl:choose>
					<xsl:when test="$disable-output-escaping = &quot;yes&quot;">
						<xsl:value-of select="$before" disable-output-escaping="yes"/>
						<br/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$before"/>
						<br/>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:call-template name="substitutebr">
					<xsl:with-param name="text" select="$after"/>
					<xsl:with-param name="replace" select="$replace"/>
					<xsl:with-param name="disable-output-escaping" select="$disable-output-escaping"/>
				</xsl:call-template>
			</xsl:when>
			<xsl:when test="$disable-output-escaping = &quot;yes&quot;">
				<xsl:value-of select="$text" disable-output-escaping="no"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$text"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

    <!-- JMi -->
    <xsl:template name="insert-manifestations">
        <tr>
            <td valign="top">
                <xsl:text>Manifestations</xsl:text>
            </td>
            <td valign="top">
                <xsl:text xml:space="preserve"> : </xsl:text>
            </td>
            <td valign="top" bgcolor="#EEEEEE">
                <xsl:text>DAC Master: </xsl:text>
                <xsl:value-of select="book-info/manifestations/manifestation[@dac-master='yes']/isbn"/>
            </td>
        </tr>
        <xsl:for-each select="book-info/manifestations/manifestation">
            <tr>
                <td/>
                <td/>
                <td>
                    <xsl:value-of select="concat(isbn, ' (',version-type,')')"/>
                </td>
            </tr>
        </xsl:for-each>
    </xsl:template>
    
</xsl:stylesheet>