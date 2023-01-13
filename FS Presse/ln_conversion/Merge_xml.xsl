<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs" version="2.0">

    <xsl:output method="xml" omit-xml-declaration="no" version="1.0" encoding="iso-8859-1" doctype-public="-//EJC//DTD XML article de presse//FR" doctype-system="article.dtd"/>
    <xsl:variable name="merge" select="collection('.?select=*_final.xml;recurssion=yes')"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="/">
        <xsl:result-document href="Article_merge.xml">
            <root>
              <xsl:for-each select="$merge">
                  <xsl:text>&#x0A;</xsl:text>
                  <xsl:apply-templates/>
              </xsl:for-each>
            </root>
        </xsl:result-document>
    </xsl:template>
    
    <!--<xsl:template match="//text()">
        <xsl:analyze-string select="." regex="([ucirc ])">
            <xsl:matching-substring>
                <xsl:choose>
                    <xsl:when test="regex-group(1)">
                        <xsl:value-of select="'sandeep'"/>
                    </xsl:when>
                </xsl:choose>
            </xsl:matching-substring>
            <xsl:non-matching-substring>
                <xsl:value-of select="."/>
            </xsl:non-matching-substring>
        </xsl:analyze-string>
    </xsl:template>-->

</xsl:stylesheet>
