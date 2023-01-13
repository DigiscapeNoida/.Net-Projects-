<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    
    <xsl:output method="xml" omit-xml-declaration="no"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="palerte"><xsl:apply-templates/></xsl:template>
    <xsl:template match="pcomment"><xsl:apply-templates/></xsl:template>
    <xsl:template match="pchronique"><xsl:apply-templates/></xsl:template>
    <xsl:template match="petude"><xsl:apply-templates/></xsl:template>
	<xsl:template match="prepere"><xsl:apply-templates/></xsl:template>
    <xsl:template match="pdossier"><xsl:apply-templates/></xsl:template>
    <xsl:template match="pfiprat"><xsl:apply-templates/></xsl:template>
    <xsl:template match="pentretien"><xsl:apply-templates/></xsl:template>
    
    <xsl:template match="chfer">
        <xsl:element name="chfer">
            <xsl:choose>
                <xsl:when test="descendant::pcomment">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pcomment">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::pchronique">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pchronique">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::pdossier and descendant::petude">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pdossier">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::pdossier">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pdossier">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::petude">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="petude">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
				<xsl:when test="descendant::prepere">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="prepere">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::pfiprat">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pfiprat">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:when test="descendant::pentretien">
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="pentretien">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:for-each-group select="*" group-starting-with="descendant::palte">
                        <xsl:element name="palerte">
                            <xsl:apply-templates select="current-group()"/>
                        </xsl:element>
                    </xsl:for-each-group>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:element>
    </xsl:template>
    
    <xsl:template match="palte">
        <xsl:element name="tit">
            <xsl:text>&#10;</xsl:text>
            <xsl:element name="al">
                <xsl:apply-templates/>
            </xsl:element>
            <xsl:text>&#10;</xsl:text>
        </xsl:element>
    </xsl:template>
    
    <xsl:template match="auteur">
        <xsl:choose>
            <xsl:when test="preceding-sibling::maintitle"></xsl:when>
            <xsl:otherwise>
                <xsl:element name="auteur"><xsl:apply-templates/></xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="qual">
        <xsl:choose>
            <xsl:when test="preceding-sibling::maintitle"></xsl:when>
            <xsl:otherwise>
                <xsl:element name="qual"><xsl:apply-templates/></xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="maintitle"></xsl:template>
    
    
</xsl:stylesheet>