<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    <xsl:variable name="article-name" select="article/*[2]"/>
    <xsl:variable name="fname" select="tokenize(base-uri(), '/')[last()]"/>
    <xsl:variable name="fnamewithoutext" select="replace($fname, '.xml','')"/>
    <xsl:output method="xml" omit-xml-declaration="no"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <!--<xsl:template match="//*[child::refbib]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::refbib)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <divref>
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:apply-templates select="current-group()"/>
                            <xsl:text>&#x0A;</xsl:text>
                        </divref>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>-->
    
    <!--<xsl:template match="//*[child::pn]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::pn)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <pn>
                            <xsl:apply-templates select="current-group()"/>
                        </pn>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>-->
    
    <xsl:template match="//pn">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::pn] and not(preceding-sibling::*[1][self::pn])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<pn>]]></xsl:text>
                <xsl:apply-templates select="node()[name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='annexes' and name()!='formule']"/>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::pn]) and preceding-sibling::*[1][self::pn]">
                <xsl:apply-templates select="node()[name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='annexes' and name()!='formule']"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</pn>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::pn]) and not(preceding-sibling::*[1][self::pn])">
                <xsl:copy>
                    <xsl:apply-templates select="node()[name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='annexes' and name()!='formule']"/>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="//p">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='annexes' and name()!='formule' and name()!='motscles']"/>
        </xsl:copy>
    </xsl:template>
    <!--<xsl:template match="//pn">
        <xsl:apply-templates/>
    </xsl:template>-->
    
    
    
    <xsl:template match="//*[child::sourcex]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::sourcex)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <sourcex>
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:apply-templates select="current-group()"/>
                        </sourcex>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="//sourcex">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="theme">
        <xsl:choose>
            <xsl:when test="matches(./al/text(),'invisible')">
                <xsl:text>&#x0A;</xsl:text>
                <theme visible="non">
                    <xsl:apply-templates/>
                    <xsl:text>&#x0A;</xsl:text>
                </theme>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:apply-templates/>
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:copy>
                <xsl:text>&#x0A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="refsource">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::refsource]) and following-sibling::*[1][self::refsource]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<grouprefsource>]]></xsl:text>
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches(./text(),'invisible')">
                            <xsl:attribute name="visible">non</xsl:attribute>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::refsource] and not(following-sibling::*[1][self::refsource])">
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches(./text(),'invisible')">
                            <xsl:attribute name="visible">non</xsl:attribute>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</grouprefsource>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::refsource]) and not(following-sibling::*[1][self::refsource])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<grouprefsource>]]></xsl:text>
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches(./text(),'invisible')">
                            <xsl:attribute name="visible">non</xsl:attribute>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</grouprefsource>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:choose>
                        <xsl:when test="matches(./text(),'invisible')">
                            <xsl:attribute name="visible">non</xsl:attribute>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:apply-templates select="node()|@*"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>
    
    
    <xsl:template match="//theme/al">
        <xsl:copy>
            <xsl:value-of select="replace(.,' \[invisible\]','')"/>
        </xsl:copy>
    </xsl:template>
    
    <!--<xsl:template match="//*[child::tiff]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::tiff)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <tiff>
                            <xsl:apply-templates select="current-group()"/>
                        </tiff>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>-->
    <xsl:template match="//tiff[parent::formule][not(position()=1)]">
        <xsl:text disable-output-escaping="yes"><![CDATA[</formule>]]></xsl:text>
        <xsl:text>&#x0A;</xsl:text>
        <xsl:text disable-output-escaping="yes"><![CDATA[<formule>]]></xsl:text>
        <xsl:copy>
        <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>

    <xsl:template match="//*[child::al[preceding-sibling::loc[not(parent::pn)]]]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="*" group-adjacent="boolean(self::al)">
                <xsl:choose>
                    <xsl:when test="current-grouping-key()">
                        <xsl:text>&#x0A;</xsl:text>
                        <p>
                            <xsl:apply-templates select="current-group()"/>
                        </p>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:apply-templates select="current-group()"/>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    
    
    <!--<xsl:template match="al-f">
        <xsl:choose>
            <xsl:when test="parent::formule and preceding-sibling::*[1][self::tiff or self::observff]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="parent::formule and not(preceding-sibling::*[1][self::al-f])">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="parent::formule and following-sibling::*[1][self::tiff or self::observff]">
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:when test="parent::formule and position() =last()">
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>-->
    
    <xsl:template match="//al-f">
        <xsl:choose>
            <xsl:when test="parent::formule and not(preceding-sibling::*[1][self::al-f]) and following-sibling::*[1][self::al-f]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:copy>
                <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="parent::formule and preceding-sibling::*[1][self::al-f] and not(following-sibling::*[1][self::al-f])">
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:when test="parent::formule and not(preceding-sibling::*[1][self::al-f]) and not(following-sibling::*[1][self::al-f])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<tbase>]]></xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</tbase>]]></xsl:text>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    
    <!--<xsl:template match="//auteur[parent::comment][2]"/>-->

    <xsl:template match="nom">
        <xsl:copy>
        <xsl:value-of select="replace(., '[ ]?\[(initiales|photo)\]', '')"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="text()">
        <xsl:value-of select="replace(., '&#10;&#10;+', '&#10;')"/>
    </xsl:template>
    
    <xsl:template match="nomrev">
        <xsl:copy>
            <xsl:apply-templates select="normalize-space(.)"/>
        </xsl:copy>
    </xsl:template>
        
    <xsl:template match="//notescom">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='auteur' and name()!='annexes' and name()!='motscles' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier' and name()!='org']|@*"/>
        </xsl:copy>
        <xsl:apply-templates select="./auteur|./motscles|./annexes|./motscles|./reftxt|./refsjc|./refslnf|./refsdossier|./org"/>
        
    </xsl:template>
    <xsl:template match="//concom">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='motscles' and name()!='annexes' and name()!='motscles' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf' and name()!='refsdossier']|@*"/>
        </xsl:copy>
        <xsl:apply-templates select="./motscles|./annexes|./motscles|./reftxt|./refsjc|./refslnf|./refsdossier"/>
    </xsl:template>
    <xsl:template match="divintro[string-length()=0]"/>
    
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
     
    <xsl:template match="//panojuri">
        <xsl:copy>
            <xsl:attribute name="type">panojuri</xsl:attribute>
            <xsl:apply-templates select="node()|@*[name()!='type']"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="al[name(following-sibling::*[1]) = 'list']|cita-bl[name(following-sibling::*[1]) = 'list']">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()" />
            <xsl:for-each-group select="following-sibling::*" group-adjacent="name()">
                <xsl:if test="position()=1">
                    <xsl:copy-of select="current-group()" />
                </xsl:if>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    
    <!--<xsl:template match="list[preceding-sibling::*[1] = (preceding-sibling::al[1] | preceding-sibling::cita-bl[1])]" />-->
    
    <xsl:template match="list[preceding-sibling::*[1][self::al or self::cita-bl]]" />
    
    
    <!--<xsl:template match="//al[not(parent::theme)]">
        <xsl:choose>
            <xsl:when test="preceding-sibling::*[1][self::motscles] and following-sibling::*[1][self::al]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notes>]]></xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                <xsl:apply-templates/>
                </xsl:copy>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::motscles] and not(following-sibling::*[1][self::al])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<notes>]]></xsl:text>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</notes>]]></xsl:text>
            </xsl:when>
            <xsl:when test="preceding-sibling::motscles and not(following-sibling::*[1][self::al]) and preceding-sibling::*[1][self::al]">
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
                <xsl:text disable-output-escaping="yes"><![CDATA[</notes>]]></xsl:text>
            </xsl:when>
            <xsl:when test="parent::div1chr or parent::div2chr">
                <xsl:choose>
                    <xsl:when test="not(preceding-sibling::*[1][self::al]) and following-sibling::*[1][self::al] and not(preceding-sibling::*[1][self::nb-bl]) and not(preceding-sibling::*[1][self::cita-bl])  and not(following-sibling::*[1][self::div2chr])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<pnchr><observ>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and preceding-sibling::*[1][self::al] and not(following-sibling::*[1][self::nb-bl]) and not(following-sibling::*[1][self::cita-bl]) and not(following-sibling::*[1][self::div2chr])">
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</observ></pnchr>]]></xsl:text>
                    </xsl:when>
                    <xsl:when test="not(following-sibling::*[1][self::al]) and not(preceding-sibling::*[1][self::al])">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<pnchr><observ>]]></xsl:text>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                        <xsl:text disable-output-escaping="yes"><![CDATA[</observ></pnchr>]]></xsl:text>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:copy>
                            <xsl:copy-of select="@*"/>
                            <xsl:apply-templates/>
                        </xsl:copy>
                    </xsl:otherwise>
                
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:copy-of select="@*"/>
                    <xsl:apply-templates/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>-->
    
    <xsl:template match="//falerte/pnchr/observ">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:template match="//falerte/pnchr">
        <xsl:apply-templates/>
    </xsl:template>
    <xsl:template match="//loc[parent::pn]">
        <xsl:text disable-output-escaping="yes"><![CDATA[</pn>]]></xsl:text>
        <xsl:copy>
            <xsl:apply-templates select="node()|@*"/>
        </xsl:copy>
        <xsl:text disable-output-escaping="yes"><![CDATA[<pn>]]></xsl:text>
    </xsl:template>
    <xsl:template match="//fannexe">
        <xsl:choose>
            <xsl:when test="parent::div1rfn">
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:choose>
                    <xsl:when test="parent::annexe">
                        <xsl:apply-templates/>
                    </xsl:when>
                    <xsl:otherwise>
                        <annexes>
                            <xsl:apply-templates/>
                        </annexes>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    <xsl:template match="annexe[child::fannexe]">
        <xsl:apply-templates/>
    </xsl:template>
        
    <xsl:template match="//divref">
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::divref] and not(preceding-sibling::*[1][self::divref])">
                <xsl:text disable-output-escaping="yes"><![CDATA[<divref>]]></xsl:text>
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::divref]) and preceding-sibling::*[1][self::divref]">
                <xsl:apply-templates/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</divref>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(following-sibling::*[1][self::divref]) and not(preceding-sibling::*[1][self::divref])">
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