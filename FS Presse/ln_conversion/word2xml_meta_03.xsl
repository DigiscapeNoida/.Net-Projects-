<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <xsl:character-map name="SA">
        <xsl:output-character character="&#x00E9;" string="&amp;eacute;"/>
        <xsl:output-character character="&#x00E8;" string="&amp;egrave;"/>
        <xsl:output-character character="&#x00E0;" string="&amp;agrave;"/>
        <xsl:output-character character="&#x00B0;" string="&amp;deg;"/>
        <xsl:output-character character="&#x00A0;" string="&amp;nbsp;"/>
        <xsl:output-character character="&#x00C0;" string="&amp;Agrave;"/>
        <xsl:output-character character="&#x00E7;" string="&amp;ccedil;"/>
        <xsl:output-character character="&#x00EA;" string="&amp;ecirc;"/>
        <xsl:output-character character="&#x00F4;" string="&amp;ocirc;"/>
        <xsl:output-character character="&#x00C9;" string="&amp;Eacute;"/>
        <xsl:output-character character="&#x00FB;" string="&amp;ucirc;"/>
        <xsl:output-character character="&#x00E2;" string="&amp;acirc;"/>
        <xsl:output-character character="&#x2026;" string="&amp;#x2026;"/>
        <xsl:output-character character="&#x00C8;" string="&amp;Egrave;"/>
        <xsl:output-character character="&#x00C0;" string="&amp;Agrave;"/>
        <xsl:output-character character="&#x00C1;" string="&amp;Aacute;"/>
        <xsl:output-character character="&#x2022;" string="&amp;bull;"/>
        <xsl:output-character character="&#x00D9;" string="&amp;Ugrave;"/>
        <xsl:output-character character="&#x00DA;" string="&amp;Uacute;"/>
        <xsl:output-character character="&#x0152;" string="&amp;OElig;"/>
        <xsl:output-character character="&#x0153;" string="&amp;oelig;"/>
        <xsl:output-character character="&#x00C6;" string="&amp;AElig;"/>
        <xsl:output-character character="&#x00E6;" string="&amp;aelig;"/>
        <xsl:output-character character="&#x00D7;" string="&amp;times;"/>
        <xsl:output-character character="&#x00D7;" string="&amp;times;"/>
        <xsl:output-character character="&#x00F7;" string="&amp;divide;"/>
        <xsl:output-character character="&#x00D1;" string="&amp;Ntilde;"/>
        <xsl:output-character character="&#x00F1;" string="&amp;ntilde;"/>
        <xsl:output-character character="î" string="&amp;icirc;"/>
    </xsl:character-map>
    
    <xsl:output method="xml" omit-xml-declaration="no" version="1.0" encoding="iso-8859-1" doctype-public="-//EJC//DTD XML presse//FR" doctype-system="revue.dtd" use-character-maps="SA"/>
    
    <xsl:variable name="article-name" select="//div1[1]/tit/al"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    
    <xsl:template match="//pcommentgroup/pcomment">
        <d1pcomment>
            <xsl:apply-templates/>
        </d1pcomment>
    </xsl:template>
    
    <xsl:template match="//pcommentgroup/pcomment/d1pcomment">
        <d2pcomment>
            <xsl:apply-templates/>
        </d2pcomment>
    </xsl:template>
    
    <xsl:template match="//pcommentgroup">
        <pcomment>
            <xsl:apply-templates/>
        </pcomment>
    </xsl:template>
    
    <xsl:template match="danoter">
        <xsl:choose>
            <xsl:when test="preceding-sibling::danoter"></xsl:when>
            <xsl:otherwise>
                <xsl:text>&#10;</xsl:text>
                <xsl:element name="danoter">
                    <xsl:text>&#10;</xsl:text>
                    <xsl:element name="tit">
                        <xsl:text>&#10;</xsl:text>
                        <xsl:element name="al">&#x00C0; noter &#x00E9;galement</xsl:element>
                        <xsl:text>&#10;</xsl:text>
                    </xsl:element>
                    <xsl:apply-templates/>
                    <xsl:apply-templates select="following-sibling::danoter" mode="DA"/>
                    <xsl:text>&#10;</xsl:text>
                </xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="danoter" mode="DA">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="refart[not(child::text())]">
        <xsl:element name="refart">
            <xsl:attribute name="fic"><xsl:value-of select="@fic"/></xsl:attribute>
            <xsl:attribute name="id"><xsl:value-of select="substring-before(@fic, '.xml')"/></xsl:attribute>
        </xsl:element>
    </xsl:template>

</xsl:stylesheet>