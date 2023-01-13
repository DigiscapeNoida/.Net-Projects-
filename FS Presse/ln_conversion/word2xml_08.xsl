<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <xsl:variable name="fname" select="tokenize(base-uri(), '/')[last()]"/>
    <xsl:variable name="fnamewithoutext" select="replace(replace($fname,'_final',''), '.xml','')"/>
    
	<xsl:output method="xml" omit-xml-declaration="no"/>
	
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//apnd">
        <xsl:copy>
            <xsl:attribute name="refid">
                <xsl:text>f</xsl:text><xsl:value-of select="$fnamewithoutext"/><xsl:text>_</xsl:text><xsl:value-of select="count(preceding::apnd) + 1"/>
            </xsl:attribute>
            <xsl:apply-templates select="node()|@*[name()!='refid']"/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//fnote">
        <xsl:copy>
            <xsl:attribute name="id">
                <xsl:text>f</xsl:text><xsl:value-of select="$fnamewithoutext"/><xsl:text>_</xsl:text><xsl:value-of select="count(preceding::fnote) + 1"/>
            </xsl:attribute>
            <xsl:apply-templates select="node()|@*[name()!='id']"/>
        </xsl:copy>
    </xsl:template>
	
    <xsl:template match="//auteur">
		<xsl:choose>
            <xsl:when test="following-sibling::*[1][self::imag]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <imag>
                        <xsl:copy-of select="following-sibling::*[1][self::imag]/@*"/>
                    <xsl:apply-templates select="following-sibling::*[1][self::imag]/node()"/>
                    </imag>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//imag[preceding-sibling::*[1][self::auteur]]"/>

	<!--<xsl:template match="al[name(following-sibling::*[1]) = 'list']|cita-bl[name(following-sibling::*[1]) = 'list']">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()" />
            <xsl:for-each-group select="following-sibling::*" group-adjacent="name()">
                <xsl:if test="position()=1">
                    <xsl:copy-of select="current-group()" />
                </xsl:if>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    
    <!-\-<xsl:template match="list[preceding-sibling::*[1] = (preceding-sibling::al[1] | preceding-sibling::cita-bl[1])]" />-\->
    
    <xsl:template match="list[preceding-sibling::*[1][self::al or self::cita-bl]]" />-->
    <xsl:template match="//al">
        <xsl:choose>
            <xsl:when test="parent::annexe">
                <p>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                        <xsl:choose>
                            <xsl:when test="matches(.,'\[coche\]')">
                                <xsl:apply-templates select="replace(.,'\[coche\]','')"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:apply-templates select="node()"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:copy> 
                </p>
            </xsl:when>
            <!--<xsl:when test="parent::div1txt or parent::div2txt or parent::div3txt or parent::div4txt or parent::div5txt or parent::div6txt or parent::div7txt or parent::div8txt or parent::div9txt or parent::div10txt">
                <p>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                        <xsl:choose>
                            <xsl:when test="matches(.,'\[coche\]')">
                                <xsl:apply-templates select="replace(.,'\[coche\]','')"/>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:apply-templates select="node()"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:copy> 
                </p>
            </xsl:when>-->
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:choose>
                        <xsl:when test="matches(.,'\[coche\]')">
                            <xsl:apply-templates select="replace(.,'\[coche\]','')"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//list">
        <xsl:choose>
            <xsl:when test="parent::al">
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:otherwise>
                <al>
                    <xsl:apply-templates/>
                </al>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//divintro">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::divintro] and not(preceding-sibling::*[1][self::divintro])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<divintro>]]></xsl:text>
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::divintro]) and preceding-sibling::*[1][self::divintro]">
                <xsl:apply-templates/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</divintro>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::divintro]) and not(preceding-sibling::*[1][self::divintro])">
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
</xsl:stylesheet>