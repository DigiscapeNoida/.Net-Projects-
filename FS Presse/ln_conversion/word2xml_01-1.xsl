<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    exclude-result-prefixes="xs"
    version="2.0">
    
    <xsl:output method="xml" omit-xml-declaration="no"/>


    <xsl:template match="* | @* | node()">
        <xsl:copy copy-namespaces="no">
            <xsl:apply-templates select="text() | * | @* | node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="pnchr">
        <xsl:element name="pnchr">
            <xsl:choose>
                <xsl:when test="preceding-sibling::*[1][self::sourcex/refsource] and preceding-sibling::*[2][self::sourcex/refsource]">
                    <xsl:apply-templates select="preceding-sibling::*[2]" mode="REF"/>
                    <xsl:apply-templates select="preceding-sibling::*[1]" mode="REF"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:apply-templates select="preceding-sibling::*[1]" mode="REF"/>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:apply-templates/>
        </xsl:element>
    </xsl:template>
    
    <xsl:template match="sourcex[child::refsource]">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::pnchr] or following-sibling::*[2][self::pnchr]"></xsl:when>
            <xsl:otherwise>
                <xsl:element name="sourcex">
                    <xsl:apply-templates/>
                </xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="sourcex" mode="REF">
        <xsl:element name="sourcex">
            <xsl:apply-templates/>
        </xsl:element>
    </xsl:template>

</xsl:stylesheet>