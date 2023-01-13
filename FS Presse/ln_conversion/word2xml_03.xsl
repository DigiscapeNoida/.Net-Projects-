<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    exclude-result-prefixes="xs"
    version="2.0">
    
    <xsl:output method="xml" omit-xml-declaration="no"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    
    <xsl:template match="//il">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::il] and not(preceding-sibling::*[1][self::il])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<list>]]></xsl:text>
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::il]) and preceding-sibling::*[1][self::il]">
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</list>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::il]) and not(preceding-sibling::*[1][self::il])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<list>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</list>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    
<!--    <xsl:template match="divintro">
        <xsl:if test="not(preceding-sibling::*[1][self::divintro])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[<divintro>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
            <tit>
                <al>Introduction</al>
            </tit>
        </xsl:if>
            <xsl:apply-templates/>
        <xsl:if test="not(following-sibling::*[1][self::divintro])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</divintro>]]></xsl:text>
        </xsl:if>
    </xsl:template>-->
    
    <xsl:template match="pn_intro">
        <xsl:choose>
            <xsl:when test="not(child::al[string-length()=0])">
                <xsl:if test="not(preceding-sibling::*[1][self::pn_intro]) and not(preceding-sibling::*[1][self::nb-bl])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[<divintro>]]></xsl:text>
                    <!--<xsl:text disable-output-escaping="yes"><![CDATA[<tit><al>Introduction</al></tit>]]></xsl:text>-->
                </xsl:if>
                <pn>
                    <xsl:apply-templates/>
                </pn>
                <xsl:if test="not(following-sibling::*[1][self::pn_intro]) and not(following-sibling::*[1][self::nb-bl])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</divintro>]]></xsl:text>
                </xsl:if>
            </xsl:when>
            <xsl:otherwise>
                
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>
    
    <xsl:template match="//auteur">
        <xsl:choose>
            <xsl:when test="following-sibling::theme and parent::comment[@type='commentaires']">
                
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:for-each-group select="*" group-adjacent="boolean(self::prenom)">
                        <xsl:choose>
                            <xsl:when test="current-grouping-key()">
                                <prenom>
                                    <xsl:apply-templates select="current-group()"/>
                                </prenom>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:apply-templates select="current-group()"/>
                            </xsl:otherwise>
                        </xsl:choose>
                        
                    </xsl:for-each-group>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//auteur/prenom">
        <xsl:apply-templates/>
    </xsl:template>
    
    <!--<xsl:template match="//*[child::cita-bl]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::cita-bl)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <cita-bl>
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:apply-templates select="current-group()"/>
                            <xsl:text>&#x0A;</xsl:text>
                        </cita-bl>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>-->
    
    <xsl:template match="//pointcles[not(ancestor::alerte or ancestor::apercu)]">
        <resume>
            <xsl:apply-templates/>
        </resume>
    </xsl:template>
    
    <xsl:template match="//pointcle[not(ancestor::alerte or ancestor::apercu)]">
        <al>
            <xsl:apply-templates/>
        </al>
    </xsl:template>
    
    <xsl:template match="annexes[ancestor::formrfn]">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="//divref">
        <xsl:copy>
            <!--<xsl:apply-templates/>-->
            <xsl:apply-templates select="following-sibling::reftxt_papl"/>
            <xsl:apply-templates select="following-sibling::refjp_papl"/>
            <xsl:apply-templates select="following-sibling::refbib_papl"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//refbib_papl">
        <xsl:choose>
			<xsl:when test="preceding-sibling::divref">
				<xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
			</xsl:when>
			<xsl:when test="not(preceding-sibling::*[1][self::refbib_papl]) and following-sibling::*[1][self::refbib_papl]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<divref>]]></xsl:text>
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::refbib_papl] and not(following-sibling::*[1][self::refbib_papl])">
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</divref>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::refbib_papl]) and not(following-sibling::*[1][self::refbib_papl])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<divref>]]></xsl:text>
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</divref>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//imag[child::apimag[@fic='[prenomnom]']]"/>
    <xsl:template match="//imag[child::apimag[@fic='[']]"/>
    <xsl:template match="//imag[child::apimag[@fic=']']]"/>
    
    <xsl:template match="//sommdossier">
        <xsl:choose>
            <xsl:when test="parent::chronique">
                <xsl:text disable-output-escaping="yes"><![CDATA[<plan/>]]></xsl:text>
            </xsl:when>
            <xsl:when test="parent::commdo">
                <sommaire>
                    <xsl:apply-templates/>
                </sommaire>
            </xsl:when>
            <xsl:when test="parent::etude">
                <sommaire>
                    <xsl:apply-templates/>
                </sommaire>
            </xsl:when>
            <xsl:when test="parent::formrfn">
                <somform>
                    <xsl:apply-templates/>
                </somform>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <!--<xsl:template match="prenom[not(following-sibling::*[1][self::prenom])]">
        <prenom>
            <xsl:value-of select="substring-before(., ' ')"/>
        </prenom>
        <xsl:text>&#x00A;</xsl:text>
        <nom>
            <xsl:value-of select="normalize-space(substring-after(., ' '))"/>
        </nom>
    </xsl:template>
    
    <xsl:template match="prenom[preceding-sibling::*[1][self::prenom] and following-sibling::*[1][self::qual]]">
        <nom>
            <xsl:value-of select="."/>
        </nom>
    </xsl:template>-->
    
    <!--<xsl:template match="//sampletit[ancestor::etude]"/>-->
    
    
</xsl:stylesheet>