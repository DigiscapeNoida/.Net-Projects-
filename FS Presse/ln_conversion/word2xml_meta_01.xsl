<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <xsl:character-map name="SA">
        <xsl:output-character character="&#x00E9;" string="&amp;#x00E9;"/>
        <xsl:output-character character="&#x00E8;" string="&amp;#x00E8;"/>
        <xsl:output-character character="&#x00E0;" string="&amp;#x00E0;"/>
        <xsl:output-character character="&#x00B0;" string="&amp;#x00B0;"/>
        <xsl:output-character character="&#x00A0;" string="&amp;#x00A0;"/>
        <xsl:output-character character="&#x00C0;" string="&amp;#x00C0;"/>
        <xsl:output-character character="&#x00E7;" string="&amp;#x00E7;"/>
        <xsl:output-character character="&#x00EA;" string="&amp;#x00EA;"/>
        <xsl:output-character character="&#x00F4;" string="&amp;#x00F4;"/>
        <xsl:output-character character="&#x00C9;" string="&amp;#x00C9;"/>
        <xsl:output-character character="&#x00FB;" string="&amp;#x00FB;"/>
        <xsl:output-character character="&#x00E2;" string="&amp;#x00E2;"/>
        <xsl:output-character character="&#x2026;" string="&amp;#x2026;"/>
        <xsl:output-character character="&#x00C8;" string="&amp;#x00C8;"/>
        <xsl:output-character character="&#x00C0;" string="&amp;#x00C0;"/>
        <xsl:output-character character="&#x00C1;" string="&amp;#x00C1;"/>
        <xsl:output-character character="&#x2022;" string="&amp;#x2022;"/>
        <xsl:output-character character="&#x00D9;" string="&amp;#x00D9;"/>
        <xsl:output-character character="&#x00DA;" string="&amp;#x00DA;"/>
        <xsl:output-character character="&#x0152;" string="&amp;#x0152;"/>
        <xsl:output-character character="&#x0153;" string="&amp;#x0153;"/>
        <xsl:output-character character="&#x00C6;" string="&amp;#x00C6;"/>
        <xsl:output-character character="&#x00E6;" string="&amp;#x00E6;"/>
        <xsl:output-character character="&#x00D7;" string="&amp;#x00D7;"/>
        <xsl:output-character character="&#x00F7;" string="&amp;#x00F7;"/>
        <xsl:output-character character="&#x00D1;" string="&amp;#x00D1;"/>
        <xsl:output-character character="&#x00F1;" string="&amp;#x00F1;"/>
    </xsl:character-map>
    
    <xsl:output method="xml" omit-xml-declaration="no" use-character-maps="SA"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:variable name="article-name" select="revue/chfer//h1"/>
    
    <xsl:function name="mf:group" as="node()*">
        <xsl:param name="elements" as="element()*"/>
        <xsl:param name="level" as="xs:integer"/>
        <xsl:for-each-group select="$elements" group-starting-with="*[local-name() eq concat('h', $level)]">
            <xsl:choose>
                <xsl:when test="self::*[local-name() eq concat('h', $level)]">
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:element name="{concat('div', $level)}">
                        <xsl:text>&#x0A;</xsl:text>
                        <tit>
                            <xsl:text>&#x0A;</xsl:text>
                            <al><xsl:apply-templates/></al>
                            <xsl:text>&#x0A;</xsl:text>
                        </tit>
                        <xsl:sequence select="mf:group(current-group() except ., $level + 1)"/>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:element>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:apply-templates select="current-group()"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:for-each-group>
    </xsl:function>
    
    <xsl:template match="//chfer/pentretien|//chfer/prepere|//chfer/palerte|//chfer/petude|//chfer/pdossier|//chfer/pentretien|//chfer/pchronique|//chfer/pcomment|//chfer/pformule|//chfer/pfiprat|//chfer/ppanojuri|//chfer/ptexte|//chfer/pindices|//chfer/ptheme|//chfer/dossier|//chfer/pdo|//chfer/phraseintro|//chfer/pjcpg|//chfer/prevue">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:sequence select="mf:group(*, 1)"/>
        </xsl:copy>
    </xsl:template>
    
    <!--<xsl:template match="prenom">
        <xsl:choose>
            <xsl:when test="$article-name[.='Alertes']"/>
            <xsl:otherwise>
                <xsl:if test="not(preceding::prenom) or preceding-sibling::*[1][self::refart] or preceding-sibling::*[1][self::qual] or preceding-sibling::*[1][self::h3]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<auteur>]]></xsl:text>
                </xsl:if>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <xsl:template match="nom">
        <xsl:choose>
            <xsl:when test="$article-name[.='Alertes']"/>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::qual]) and not(following-sibling::*[1][self::prenom])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="qual">
        <xsl:choose>
            <xsl:when test="$article-name[.='Alertes']"/>
            <xsl:otherwise>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::qual])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>-->
    
    
    <xsl:template match="prenom[not(following-sibling::*[1][self::prenom])]">
        <prenom>
            <xsl:value-of select="substring-before(., ' ')"/>
        </prenom>
        <xsl:text>&#x00A;</xsl:text>
        <nom>
            <xsl:variable name="nomtxt"><xsl:value-of select="lower-case(normalize-space(substring-after(., ' ')))"/></xsl:variable>
            <!--<xsl:value-of select="substring(.,1,1)"/>
            <xsl:value-of select="translate(substring(.,2),$vUpper,$vLower)"/>-->
            <xsl:call-template name="CamelCase">
                <xsl:with-param name="text"><xsl:value-of select="$nomtxt"/></xsl:with-param>
            </xsl:call-template>
        </nom>
    </xsl:template>
    
    <xsl:template match="prenom[preceding-sibling::*[1][self::prenom] and following-sibling::*[1][self::qual]]">
        <nom>
            <xsl:variable name="nomtxt"><xsl:value-of select="."/></xsl:variable>
            <xsl:call-template name="CamelCase">
                <xsl:with-param name="text"><xsl:value-of select="$nomtxt"/></xsl:with-param>
            </xsl:call-template>
            <!--<xsl:value-of select="lower-case(.)"/>-->
        </nom>
    </xsl:template>
    
    <xsl:template name="CamelCase">
        <xsl:param name="text"/>
        <xsl:choose>
            <xsl:when test="contains($text,' ')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,' ')"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:call-template name="CamelCase">
                    <xsl:with-param name="text" select="substring-after($text,' ')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="$text"/>
                </xsl:call-template>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template name="CamelCaseWord">
        <xsl:param name="text"/>
        <xsl:value-of select="translate(substring($text,1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" /><xsl:value-of select="translate(substring($text,2,string-length($text)-1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
    </xsl:template>
    
    
    <xsl:template match="refart">
        <xsl:choose>
            <!--<xsl:when test="contains(@fic, 'et0')">
                <xsl:text>&#x0A;</xsl:text>
                <petude><tit><al>Étude</al></tit>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                    </xsl:copy>
                </petude>
            </xsl:when>-->
            <!--<xsl:when test="contains(@fic, 're0')">
                <xsl:text>&#x0A;</xsl:text>
                <prepere><tit><al>Repère</al></tit>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                    </xsl:copy>
                </prepere>
            </xsl:when>-->
            <xsl:when test="contains(@fic, 'fo0')">
                <xsl:text>&#x0A;</xsl:text>
                <pformule><tit><al>Repère</al></tit>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:copy>
                        <xsl:copy-of select="@*"/>
                    </xsl:copy>
                </pformule>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//qual">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::qual] and not(preceding-sibling::*[1][self::qual])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<qual-group>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::qual] and preceding-sibling::*[1][self::qual]">
                <xsl:copy>
                    <xsl:apply-templates select="node()"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::qual]) and preceding-sibling::*[1][self::qual]">
                <xsl:copy>
                <xsl:apply-templates select="node()"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</qual-group>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::qual]) and not(preceding-sibling::*[1][self::qual])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<qual-group>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</qual-group>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//pentretien/maintitle|//prepere/maintitle|//palerte/maintitle|//petude/maintitle|//pdossier/maintitle|//pentretien/maintitle|//pchronique/maintitle|//pcomment/maintitleeeee|//pformule/maintitle|//pfiprat/maintitle|//ppanojuri/maintitle|//ptexte/maintitle|//pindices/maintitle|//ptheme/maintitle|//dossier/maintitle|//pdo/maintitle|//phraseintro/maintitle|//pjcpg/maintitle|//prevue/maintitle">
        <xsl:choose>
            <xsl:when test="not(parent::*/h1)">
                <tit><al>
                    <xsl:apply-templates/>
                </al></tit>
            </xsl:when>
            <xsl:otherwise>
                
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

	<xsl:template match="//pcomment/maintitle"></xsl:template>


</xsl:stylesheet>