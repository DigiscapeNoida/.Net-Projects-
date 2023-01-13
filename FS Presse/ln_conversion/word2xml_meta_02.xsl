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
    
    <xsl:output method="xml" omit-xml-declaration="no" version="1.0" encoding="iso-8859-1" use-character-maps="SA"/>
    
    <xsl:variable name="article-name" select="//tit[1]/al"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    
    <xsl:template match="div1">
        <xsl:choose>
            <xsl:when test="parent::palerte">
                <d1palerte>
                    <xsl:apply-templates select="node()[name()!='qual-group']"/>
                </d1palerte>
            </xsl:when>
            <xsl:when test="tit/al[.='Commentaires'] or //meta[@articletype='cm']">
                <d1pcomment>
                    <xsl:apply-templates select="node()[name()!='qual-group']"/>
                </d1pcomment>
            </xsl:when>
            <!--<xsl:when test="tit/al[.='Dossier']">
                <pdossier>
                    <xsl:apply-templates select="node()[name()!='qual-group']"/>
                </pdossier>
            </xsl:when>-->
            <xsl:when test="parent::prepere">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2']"/>
            </xsl:when>
            <xsl:when test="parent::ptexte">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2']"/>
            </xsl:when>
            <xsl:when test="parent::pentretien">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2']"/>
                <xsl:apply-templates select=".//refart"/>
            </xsl:when>
            <xsl:when test="parent::pchronique">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2']"/>
                <xsl:apply-templates select=".//refart"/>
            </xsl:when>
            <xsl:when test="parent::pfiprat">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2']"/>
                <xsl:apply-templates select=".//refart"/>
            </xsl:when>
            <xsl:when test="parent::pdossier">
                <xsl:apply-templates select="node()[name()!='auteur' and name()!='qual-group' and name()!='div2' and name()!='refart']"/>
                <xsl:apply-templates select="//refart"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="div2">
        <xsl:choose>
            <xsl:when test="ancestor::palerte">
                <d2palerte>
                    <xsl:apply-templates select="node()[name()!='qual-group']"/>
                </d2palerte>
            </xsl:when>
            <xsl:when test="$article-name[.='Commentaires']">
                <d2pcomment>
                    <!--<xsl:choose>
                        <xsl:when test="child::div3">
                            <xsl:apply-templates select="tit, auteur, refart"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <danoter>
                            <xsl:apply-templates select="tit, auteur, refart"/>
                            </danoter>
                        </xsl:otherwise>
                    </xsl:choose>-->
                    <xsl:apply-templates select="node()[name()!='qual-group']"/>
                </d2pcomment>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="tit[parent::div2]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    <!--<xsl:template match="auteur[parent::div2]|refart[parent::div2]">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>-->
    
    
    
    <xsl:template match="div3">
        <xsl:choose>
            <xsl:when test="ancestor::palerte">
                <d3palerte>
                    <xsl:apply-templates/>
                </d3palerte>
            </xsl:when>
            <xsl:when test="ancestor::pcomment">
                <d3pcomment>
                    <xsl:apply-templates/>
                </d3pcomment>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="refart">
        <!--<xsl:if test="following-sibling::*[1][self::refart]">-->
            <xsl:text>&#x0A;</xsl:text>
        <!--</xsl:if>-->
        <xsl:copy>
            <xsl:copy-of select="@*"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//auteur">
        <xsl:choose>
            <xsl:when test="preceding-sibling::refart or preceding-sibling::petude or preceding-sibling::prepere or preceding-sibling::pformule"></xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                    <xsl:apply-templates select="following-sibling::*[1][self::qual-group]"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <!--<xsl:template match="auteur[preceding-sibling::refart]"/>
    <xsl:template match="auteur[preceding-sibling::petude]"/>
    <xsl:template match="auteur[preceding-sibling::prepere]"/>
    <xsl:template match="auteur[preceding-sibling::pformule]"/>-->
    
    <xsl:template match="auteur[//tit/al[.='Dossier']]"/>
    <!--<xsl:template match="qual-group">
        <xsl:choose>
            <xsl:when test="//tit/al[.='Dossier']">
                
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>-->
    <xsl:template match="nom">
        <xsl:copy>
            <!--<xsl:apply-templates select="normalize-space(.)"/>-->
            <xsl:variable name="nomtxt"><xsl:value-of select="replace(.,'[ ]?\[photo\]','')"/></xsl:variable>
            <xsl:call-template name="CamelCase">
                <xsl:with-param name="text"><xsl:value-of select="$nomtxt"/></xsl:with-param>
            </xsl:call-template>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="@articletype"/>
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
            <xsl:when test="contains($text,'-')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'-')"/>
                </xsl:call-template>
                <xsl:text>-</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'-')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,'’')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'’')"/>
                </xsl:call-template>
                <xsl:text>’</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'’')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,'''')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,'''')"/>
                </xsl:call-template>
                <xsl:text>'</xsl:text>
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-after($text,'''')"/>
                </xsl:call-template>
            </xsl:when>
            <xsl:when test="contains($text,' ')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,' ')"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:call-template name="CamelCaseWord">
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
    
    <xsl:template match="//qual-group">
        <xsl:choose>
            <xsl:when test="preceding-sibling::refart"></xsl:when>
            <xsl:otherwise><xsl:apply-templates/></xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="qual">
        <xsl:choose>
            <xsl:when test="preceding-sibling::refart"></xsl:when>
            <xsl:otherwise>
                <xsl:element name="qual"><xsl:apply-templates/></xsl:element>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//pentretien|//prepere|//palerte|//petude|//pdossier|//pentretien|//pchronique|//pcomment|//pformule|//pfiprat|//ppanojuri|//ptexte|//pindices|//ptheme|//dossier|//pdo|//phraseintro|//pjcpg|//prevue">
        <xsl:choose>
            <xsl:when test="local-name()='pcomment'">
                <xsl:choose>
                    <xsl:when test="following-sibling::*[1][self::pcomment] and not(preceding-sibling::*[1][self::pcomment])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<pcommentgroup>]]></xsl:text>
                        <xsl:choose>
                            <xsl:when test="child::div1">
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group' and name()!='auteur']|@*"/>
                                </xsl:copy>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group']|@*"/>
                                </xsl:copy>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::pcomment]) and preceding-sibling::*[1][self::pcomment]">
                        <xsl:choose>
                            <xsl:when test="child::div1">
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group' and name()!='auteur']|@*"/>
                                </xsl:copy>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group']|@*"/>
                                </xsl:copy>
                            </xsl:otherwise>
                        </xsl:choose>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</pcommentgroup>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::pcomment]) and not(preceding-sibling::*[1][self::pcomment])">
                        <xsl:choose>
                            <xsl:when test="child::div1">
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group' and name()!='auteur']|@*"/>
                                </xsl:copy>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group']|@*"/>
                                </xsl:copy>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:choose>
                            <xsl:when test="child::div1">
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group' and name()!='auteur']|@*"/>
                                </xsl:copy>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:copy>
                                    <xsl:apply-templates select="node()[name()!='qual-group']|@*"/>
                                </xsl:copy>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <xsl:choose>
                    <xsl:when test="child::div1">
                        <xsl:copy>
                            <xsl:apply-templates select="node()[name()!='qual-group' and name()!='auteur']|@*"/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:copy>
                            <xsl:apply-templates select="node()[name()!='qual-group']|@*"/>
                        </xsl:copy>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="chfer">
        <xsl:copy>
        <xsl:apply-templates select="prepere | palerte | petude | pdossier | pentretien | pchronique | pcomment | pformule | pfiprat | ppanojuri | ptexte | pindices | ptheme | dossier | imag | reftabgrp | pdo | phraseintro | pjcpg | prevue"/>
        </xsl:copy>
    </xsl:template>

</xsl:stylesheet>