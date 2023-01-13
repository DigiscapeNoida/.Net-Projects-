<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <xsl:variable name="article-name" select="article/*[2]"/>
    
    <xsl:output method="xml" omit-xml-declaration="no"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="@spanname[.='']"/>
    
    <xsl:template match="divintro|loc">
        <xsl:if test="$article-name[local-name()='etude'] and preceding-sibling::*[1][self::resume or self::sommaire or self::sourcex or self::cita-bl[not(preceding-sibling::*[1][self::al])]]">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="div1alerte[position()=last()]">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib"/>
    </xsl:template>
    <xsl:template match="div1txt[position()=last()]">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:text>&#x0A;</xsl:text>
        <xsl:text disable-output-escaping="yes"><![CDATA[</corpstxt>]]></xsl:text>
        <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib"/>
    </xsl:template>
    <xsl:template match="div1chr[position()=last()]">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:choose>
            <xsl:when test=".//pnchr">
                <xsl:apply-templates select=".//annexes|.//divbib"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates select=".//annexes|.//motscles|.//divbib"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="div1apercu">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:if test="not(following-sibling::*[1][self::div1apercu])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpsap>]]></xsl:text>
        </xsl:if>
        <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib"/>
    </xsl:template>
    
    <xsl:template match="div1etu">
        <!--<xsl:if test="not(preceding-sibling::divintro) and not(preceding-sibling::*[1][self::div1etu or self::pn or self::al or self::loc or self::nb-bl])">-->
        <xsl:if test="preceding-sibling::divintro and not(preceding-sibling::div1etu)">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpsetu>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:if test="not(preceding-sibling::*[1][self::div1etu])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:if test="not(following-sibling::*[1][self::div1etu])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpsetu>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib|.//fannexe[not(parent::annexe)]"/>
    </xsl:template>
    
    <xsl:template match="div1fiprat">
        <xsl:if test="not(preceding-sibling::divintro) and not(preceding-sibling::*[1][self::div1fiprat])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[<corpsfiprat>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='divref']"/>
        </xsl:copy>
        <xsl:if test="not(following-sibling::*[1][self::div1fiprat])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpsfiprat>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:if test="not(following-sibling::*[1][self::div1fiprat])">
            <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt[not(parent::divref)]|.//refsjc|.//refslnf|.//refsdossier|.//refbib[not(parent::divref)]|.//divref"/>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="div1ess">
        <xsl:if test="not(preceding-sibling::divintro and preceding-sibling::resume) and not(preceding-sibling::*[1][self::div1ess])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[<corpsess>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib']"/>
        </xsl:copy>
        <xsl:if test="not(following-sibling::*[1][self::div1ess])">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpsess>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:if test="not(following-sibling::*[1][self::div1ess])">
            <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib[not(parent::divref)]|.//divref"/>
        </xsl:if>
    </xsl:template>
    
    
    
    <xsl:template match="div1com|div1rfn">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='refsource' and name()!='reftxt' and name()!='refsjc' and name()!='refjp' and name()!='refslnf' and name()!='refsdossier' and name()!='refbib' and name()!='auteur']"/>
        </xsl:copy>
        <xsl:apply-templates select=".//motscles|.//annexes|.//refsource[not(ancestor::annexe)]|.//reftxt|.//refsjc|.//refjp|.//refslnf|.//refsdossier|.//refbib|.//auteur"/>
    </xsl:template>
    
    <xsl:template match="al[string-length()=0]"/>
    <xsl:template match="pn[string-length()=0]"/>
    
    <xsl:template match="resume">
        <xsl:if test="not(preceding-sibling::*[1][self::resume])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<resume>]]></xsl:text>
        </xsl:if>
        <xsl:apply-templates/>
        <xsl:if test="not(following-sibling::*[1][self::resume])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</resume>]]></xsl:text>
        </xsl:if>
    </xsl:template>
    
    <!--<xsl:template match="al">
        <!-\-<xsl:if test="preceding-sibling::*[1][self::al]">
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>-\->
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates select="node()|@*"/>
        </xsl:copy>
    </xsl:template>-->
    
    
    
    <xsl:template match="tit[preceding-sibling::*[1][self::num]]">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="emph1">
        <xsl:if test="not(preceding-sibling::node()[1][self::emph1])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<emph1>]]></xsl:text>
        </xsl:if>
        <xsl:apply-templates/>
        <xsl:if test="not(following-sibling::node()[1][self::emph1])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</emph1>]]></xsl:text>
        </xsl:if>
    </xsl:template>
    
    
    <xsl:template match="emph2">
        <xsl:if test="not(preceding-sibling::node()[1][self::emph2])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<emph2>]]></xsl:text>
        </xsl:if>
        <xsl:apply-templates/>
        <xsl:if test="not(following-sibling::node()[1][self::emph2])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</emph2>]]></xsl:text>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="emph3">
        <xsl:if test="not(preceding-sibling::node()[1][self::emph3])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<emph3>]]></xsl:text>
        </xsl:if>
        <xsl:apply-templates/>
        <xsl:if test="not(following-sibling::node()[1][self::emph3])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</emph3>]]></xsl:text>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="pnchr[parent::apercu]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::pointcles or self::resume or self::auteur or self::sourcex[preceding-sibling::*[1][self::pointcles or self::resume or self::auteur]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsap>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpsap>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <xsl:template match="al[ancestor::chronique|ancestor::alerte]">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="node()" group-adjacent="local-name()='emph2'">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:copy>
                            <xsl:apply-templates select="current-group()/node()"/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
        <!--        <xsl:if test="following-sibling::*[1][self::al]">
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>-->
    </xsl:template>
    
    <xsl:template match="al[parent::apercu]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::pointcles or self::resume or self::auteur or self::sourcex[preceding-sibling::*[1][self::pointcles or self::resume or self::auteur]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsap>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpsap>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <xsl:template match="al[parent::texte]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::tit or self::sourcex or self::resume]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpstxt>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles or self::auteur]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpstxt>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="p[parent::texte]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::tit or self::sourcex or self::resume]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpstxt>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles or self::auteur]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpstxt>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="list[parent::texte]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::tit or self::sourcex or self::resume]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpstxt>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles or self::auteur]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpstxt>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="al[parent::etude]|pn[parent::etude]|nb-bl[parent::etude]|loc[parent::etude]|sampletit[parent::etude][following-sibling::*[1][self::pn]]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::resume]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::sourcex[preceding-sibling::*[1][self::resume or self::auteur]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::imag[preceding-sibling::*[1][self::sourcex[preceding-sibling::*[1][self::resume or self::auteur]]]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::legimag[preceding-sibling::*[1][self::imag[preceding-sibling::*[1][self::sourcex[preceding-sibling::*[1][self::resume or self::auteur]]]]]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::auteur]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsetu>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpsetu>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="al[parent::essentiel]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::resume]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsess>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::sourcex]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsess>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::pointcles]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsess>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</corpsess>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="al[parent::repere]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::auteur]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsrep><p>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::imag[preceding-sibling::*[1][self::auteur]]]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsrep><p>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::relance[not(following-sibling::al)]]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</p></corpsrep>]]></xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::motscles]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</p></corpsrep>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="al[parent::artdossier]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::auteur] and count(parent::artdossier/al) = 1">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<introdossier>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</introdossier>]]></xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::auteur] and not(count(parent::artdossier/al) = 1)">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<introdossier>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:when test="following-sibling::*[1][self::sommdossier] and count(parent::artdossier/al) != 1">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</introdossier>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//sommdossier">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::al]) and not(count(preceding-sibling::al) = 0)">
                <xsl:text disable-output-escaping="yes"><![CDATA[</introdossier>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>
    
    <xsl:template match="sstit1">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="prenom[preceding-sibling::*[1][self::prenom]]">
        <nom>
            <xsl:apply-templates/>
        </nom>
    </xsl:template>
    
    <xsl:template match="prenom[.='']|nom[.='']"/>
    
    <xsl:template match="text()">
        <xsl:value-of select="replace(., '&#10;&#10;+', '&#10;')"/>
    </xsl:template>
    
    <xsl:template match="tinoye">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
        <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
    
    <!--<xsl:template match="pointcles[preceding-sibling::*[1][self::auteur]]"/>-->
    <xsl:template match="pointcles" mode="pointcles">
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//auteur">
        <xsl:choose>
            <xsl:when test="ancestor::comment and preceding::concom and not(ancestor::div1com)">
                <xsl:choose>
                    <xsl:when test="matches(.,'initiales')">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="child::imag/apimag/@fic='initiales'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="child::imag/apimag/@fic='[initiales]'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="following-sibling::*[1][self::imag/apimag/@fic='initiales']">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur>
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates select="node()|@*"/>
                        </auteur>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:when test="ancestor::comment and preceding::notescom and not(ancestor::div1com)">
                <xsl:choose>
                    <xsl:when test="matches(.,'initiales')">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="child::imag/apimag/@fic='initiales'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="child::imag/apimag/@fic='[initiales]'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:when test="following-sibling::*[1][self::imag/apimag/@fic='initiales']">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <auteur sign="initiales">
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates/>
                        </auteur>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:if test="not(preceding-sibling::*[1][self::auteur])">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                            <xsl:text>&#x0A;</xsl:text>
                        </xsl:if>
                        <auteur>
                            <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                <introaut>
                                    <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                </introaut>
                            </xsl:if>
                            <xsl:apply-templates select="node()|@*"/>
                        </auteur>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <!--<xsl:choose>
                    <xsl:when test="following-sibling::*[1][self::pointcles]">
                        <xsl:choose>
                            <xsl:when test="matches(.,'initiales')">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:apply-templates select="following-sibling::*[1][self::pointcles]" mode="pointcles"/>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="child::imag/apimag/@fic='initiales'">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:apply-templates select="following-sibling::*[1][self::pointcles]" mode="pointcles"/>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="child::imag/apimag/@fic='[initiales]'">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:apply-templates select="following-sibling::*[1][self::pointcles]" mode="pointcles"/>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="following-sibling::*[1][self::imag/apimag/@fic='initiales']">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:apply-templates select="following-sibling::*[1][self::pointcles]" mode="pointcles"/>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="string-length()=0">
                                
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:apply-templates select="following-sibling::*[1][self::pointcles]" mode="pointcles"/>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur>
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates select="node()|@*"/>
                                </auteur>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:when>
                    <xsl:otherwise>-->
                        <xsl:choose>
                            <xsl:when test="matches(.,'initiales')">
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="child::imag/apimag/@fic='initiales'">
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="child::imag/apimag/@fic='[initiales]'">
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:when test="following-sibling::*[1][self::imag/apimag/@fic='initiales']">
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur sign="initiales">
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates/>
                                </auteur>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:text>&#x0A;</xsl:text>
                                <auteur>
                                    <xsl:if test="preceding-sibling::*[1][self::introaut]">
                                        <introaut>
                                            <xsl:apply-templates select="preceding-sibling::*[1][self::introaut]/node()"/>
                                        </introaut>
                                    </xsl:if>
                                    <xsl:apply-templates select="node()|@*"/>
                                </auteur>
                            </xsl:otherwise>
                        </xsl:choose>
                    <!--</xsl:otherwise>
                </xsl:choose>-->
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="imag[child::apimag/@fic='initiales']"/>
    <xsl:template match="imag[child::apimag/@fic='[initiales]']"/>
    <xsl:template match="imag[child::apimag/@fic=' [initiales]']"/>
    
    
    <xsl:template match="pnchr">
        <xsl:if test="./refsource">
            <xsl:apply-templates select=".//refsource"/>
        </xsl:if>
        <xsl:if test="./extrait">
            <xsl:apply-templates select=".//extrait"/>
        </xsl:if>
        <xsl:copy>
            <xsl:choose>
                <xsl:when test="not(ancestor::div1chr)">
                    <xsl:apply-templates select="node() except (refsource, extrait, divref, refsdossier, refjp)"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:apply-templates select="node() except (refsource, extrait, divref, divbib, annexes, refsdossier, refjp)"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:copy>
        <xsl:choose>
            <xsl:when test="not(ancestor::div1chr)">
                <xsl:apply-templates select="./refsource|./extrait|./divref|./divbib|./annexes|./refsdossier|./refjp"/>
            </xsl:when>
        </xsl:choose>
        
    </xsl:template>
    
    <xsl:template match="nb-bl">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//cita-bl">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::auteur[preceding-sibling::*[1][self::pointcles or self::resume or self::sourcex or self::tit]]] and parent::apercu">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<corpsap>]]></xsl:text>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="tit[parent::nb-bl]">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
        <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
    <!--<xsl:template match="al[parent::nb-bl]">
        <xsl:if test="not(preceding-sibling::*[1][self::tit])">
            <xsl:text>&#x0A;</xsl:text>
        </xsl:if>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
        <xsl:text>&#x0A;</xsl:text>
    </xsl:template>-->
    
    <!--<xsl:template match="al[preceding-sibling::*[1][self::nb-bl]]">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>-->
    
    <xsl:template match="relance">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
        <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
    
    <xsl:template match="//sourcex">
        <xsl:choose>
            <xsl:when test="parent::alerte or parent::pnchr or parent::fiprat or parent::panojuri or parent::texte or parent::commdo or parent::corpscommdo or parent::essentiel or parent::div1chr or parent::div2chr or parent::div3chr or parent::div4chr or parent::annexe">
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:when test="parent::apercu">
                <xsl:choose>
                    <xsl:when test="following-sibling::*[1][self::motscles]">
                        <xsl:text disable-output-escaping="yes"><![CDATA[</corpsap>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:apply-templates/>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:apply-templates/>
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="fnotes[.='']"/>
    
    <xsl:template match="//introaut[not(parent::auteur)]"/>
    <xsl:template match="//comment[@type]">
        <xsl:variable name="titval" select="//tit[parent::comment][1]/al"/>
        <xsl:choose>
            <xsl:when test="//notescom[not(following::auteur[not(ancestor::div1com)])]">
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches($titval, '\[coche\]')">
                                <xsl:attribute name="is"><xsl:text>coche</xsl:text></xsl:attribute>
                                <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                                <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</notescom>]]></xsl:text>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="//concom[not(following::auteur[not(ancestor::div1com)])]">
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches($titval, '\[coche\]')">
                            <xsl:attribute name="is"><xsl:text>coche</xsl:text></xsl:attribute>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</concom>]]></xsl:text>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                    <xsl:choose>
                        <xsl:when test="matches($titval, '\[coche\]')">
                            <xsl:copy>
                                <xsl:attribute name="is"><xsl:text>coche</xsl:text></xsl:attribute>
                                <xsl:apply-templates select="node()|@*"/>
                            </xsl:copy>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:copy>
                                <xsl:apply-templates select="node()|@*"/>
                            </xsl:copy>
                        </xsl:otherwise>
                    </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <xsl:template match="notescom">
        <xsl:choose>
            <xsl:when test="@type='note'">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="note">]]></xsl:text>
            </xsl:when>
            <xsl:when test="@type='observation'">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="observation">]]></xsl:text>
            </xsl:when>
            <xsl:when test="@type='conclusions'">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="conclusions">]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="note">]]></xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="concom">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::tit]">
                <xsl:choose>
                    <xsl:when test="@type='note'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="@type='observation'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="@type='conclusions'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <xsl:choose>
                    <xsl:when test="@type='note'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<tit><al>CONCLUSIONS</al></tit>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="@type='observation'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<tit><al>CONCLUSIONS</al></tit>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="@type='conclusions'">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<tit><al>CONCLUSIONS</al></tit>]]></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:text disable-output-escaping="yes"><![CDATA[<tit><al>CONCLUSIONS</al></tit>]]></xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <xsl:template match="refsdossier[not(parent::temprefdo)]"/>
    <xsl:template match="//temprefdo">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:template match="refbib_papl">
        <refbib>
            <xsl:apply-templates/>
        </refbib>
    </xsl:template>
    <xsl:template match="refjp_papl">
        <refjp>
            <xsl:apply-templates/>
        </refjp>
    </xsl:template>
    <xsl:template match="reftxt_papl">
        <reftxt>
            <xsl:apply-templates/>
        </reftxt>
    </xsl:template>
    <xsl:template match="//annexes">
        <xsl:copy>
            <xsl:copy-of select="preceding-sibling::*[1][self::formule]"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    <xsl:template match="//formule[following-sibling::*[1][self::annexes]]"/>
    
    
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
    
    <xsl:template match="list[preceding-sibling::*[1][self::al or self::cita-bl]]" />
    -->
    
</xsl:stylesheet>