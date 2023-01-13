<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:mf="http://example.com/mf"
    exclude-result-prefixes="xs mf"
    version="2.0">
    
    
    <xsl:variable name="article-name" select="article/*[2]"/>
    <xsl:variable name="fname" select="tokenize(base-uri(), '/')[last()]"/>
    <xsl:variable name="fnamewithoutext" select="replace($fname, '.xml','')"/>
    <xsl:variable name="vLower" select="'abcdefgijklmnopqrstuvwxyz'"/>
    <xsl:variable name="vUpper" select="'ABCDEFGIJKLMNOPQRSTUVWXYZ'"/>
    <xsl:output method="xml" omit-xml-declaration="no"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    
    
    
    <xsl:template match="article">
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:function name="mf:group" as="node()*">
        <xsl:param name="elements" as="element()*"/>
        <xsl:param name="level" as="xs:integer"/>
        <xsl:for-each-group select="$elements" group-starting-with="*[local-name() eq concat('h', $level)]">
            <xsl:choose>
                <xsl:when test="self::*[local-name() eq concat('h', $level)]">
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:if test="$article-name[local-name()='chronique']">
                        <xsl:if test="not(preceding-sibling::h1)">
                        <xsl:text disable-output-escaping="yes"><![CDATA[<corpschr>]]></xsl:text>
                        <xsl:text>&#x0A;</xsl:text>
                    </xsl:if>
                    </xsl:if>
                    <xsl:element name="{concat(if($article-name[local-name()='commdo']) then 'd' else if($article-name[local-name()='echeado']) then 'd' else 'div', $level, if($article-name[local-name()='chronique']) then 'chr' else if($article-name[local-name()='alerte']) then 'alerte' else if($article-name[local-name()='apercu']) then 'apercu' else if($article-name[local-name()='commdo']) then 'commdo' else if($article-name[local-name()='comment']) then 'com' else if($article-name[local-name()='echeado']) then 'echeado' else if($article-name[local-name()='essentiel']) then 'ess' else if($article-name[local-name()='etude']) then 'etu' else if($article-name[local-name()='fiprat']) then 'fiprat' else if($article-name[local-name()='formrfn']) then 'rfn' else if($article-name[local-name()='texte']) then 'txt' else '')}">
                        <xsl:text>&#x0A;</xsl:text>
                            <xsl:if test="string-length()!=0">
                                <tit>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al><xsl:apply-templates/></al>
                                    <xsl:text>&#x0A;</xsl:text>
                                </tit>
                            </xsl:if>
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
    
    <xsl:template match="chronique|alerte|apercu|artdossier|commdo|comment|echeado|essentiel|etude|falerte|fiprat|formrfn|indices|panojuri|repere|texte">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates select="theme" mode="th-move"/>
            <xsl:apply-templates select="num" mode="num-move"/>
            <xsl:sequence select="mf:group(*, 1)"/>
            <xsl:if test="$article-name[local-name()='chronique']">
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</corpschr>]]></xsl:text>
            </xsl:if>
            <temprefdo>
            <xsl:apply-templates select="//refsdossier"/>
            </temprefdo>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="theme"/>
    <xsl:template match="theme" mode="th-move">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="num"/>
    <xsl:template match="num" mode="num-move">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
   
    
    
    <xsl:template match="imag|prenom|nom|qual">
        <xsl:text>&#x0A;</xsl:text>
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    
    <xsl:template match="al[ancestor::resume[following-sibling::*[1][self::l[@typenum='tiret']]]]" priority="11">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
            <xsl:apply-templates select="ancestor::resume/following-sibling::*[1][self::l[@typenum='tiret']]" mode="list-fr-move"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="l[@typenum='tiret'][preceding-sibling::*[1][self::resume]]"/>
    
    <xsl:template match="l[@typenum='tiret']" mode="list-fr-move">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="al[contains(text(), 'SOMMAIRE/PLAN')]" priority="9">
        <plan/>
    </xsl:template>
    <xsl:template match="al[emph1[contains(text(), '[[SOMMAIRE')]]" priority="9">
        <plan/>
        <xsl:text>&#x0A;</xsl:text>
    </xsl:template>
    
    <xsl:template match="al[ancestor::chronique|ancestor::alerte]">
        <xsl:copy>
            <xsl:copy-of select="@*"/>
            <xsl:for-each-group select="node()" group-adjacent="local-name()='emph3'">
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
    
    <xsl:template match="mc">
        <xsl:choose>
            <xsl:when test="contains(., '[invisibles]')">
                <xsl:if test="not(preceding-sibling::*[1][self::mc])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[<motscles visible="non">]]></xsl:text>
                </xsl:if>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:for-each select="tokenize(., '¤')">
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:if test="not(position() &gt; 6)">
                            <xsl:element name="{concat('mc', position())}">
                                <xsl:value-of select="normalize-space(replace(., '[ ]?\[invisibles\]',''))"/>
                            </xsl:element>
                        </xsl:if>
                        <xsl:if test="position()=last()">
                            <xsl:text>&#x0A;</xsl:text>
                        </xsl:if>
                    </xsl:for-each>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::mc])">
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</motscles>]]></xsl:text>
                </xsl:if>
            </xsl:when>
            <xsl:when test="contains(., '[invisible]')">
                <xsl:if test="not(preceding-sibling::*[1][self::mc])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[<motscles visible="non">]]></xsl:text>
                </xsl:if>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:for-each select="tokenize(., '¤')">
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:if test="not(position() &gt; 6)">
                        <xsl:element name="{concat('mc', position())}">
                            <xsl:value-of select="normalize-space(replace(., '[ ]?\[invisible\]',''))"/>
                        </xsl:element>
                        </xsl:if>
                        <xsl:if test="position()=last()">
                            <xsl:text>&#x0A;</xsl:text>
                        </xsl:if>
                    </xsl:for-each>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::mc])">
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</motscles>]]></xsl:text>
                </xsl:if>
            </xsl:when>
            <xsl:otherwise>
                <xsl:if test="not(preceding-sibling::*[1][self::mc])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[<motscles>]]></xsl:text>
                </xsl:if>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:for-each select="tokenize(., '¤')">
                        <xsl:text>&#x0A;</xsl:text>
                        <xsl:if test="not(position() &gt; 6)">
                        <xsl:element name="{concat('mc', position())}">
                            <xsl:value-of select="normalize-space(.)"/>
                        </xsl:element>
                        </xsl:if>
                        <xsl:if test="position()=last()">
                            <xsl:text>&#x0A;</xsl:text>
                        </xsl:if>
                    </xsl:for-each>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::mc])">
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</motscles>]]></xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="break"/>
    <xsl:template match="il/@level"/>
    
    
    
    <xsl:template match="//text()">
        <xsl:value-of select="replace(., 'Ã´', '&amp;ocirc;')"/>
    </xsl:template>
    
    <xsl:template match="observ">
        <xsl:if test="./sourcex">
            <xsl:apply-templates select="./sourcex"/>
        </xsl:if>
        <xsl:if test="./extrait">
            <xsl:apply-templates select="./extrait"/>
        </xsl:if>
        <xsl:copy>
            <xsl:apply-templates select="node() except (sourcex, auteur, extrait, mc, refbib, refjp, reftxt, annexes, divbib, divref, refsdossier, refsjc, refslnf)"/>
        </xsl:copy>
        <xsl:if test="./auteur">
            <xsl:apply-templates select="./auteur" mode="move-auteur"/>
        </xsl:if>
        <xsl:if test="./mc">
            <xsl:apply-templates select="./mc"/>
        </xsl:if>
        <xsl:apply-templates select="./refbib|./refjp|./reftxt|./annexes|./divbib|./divref|./refsdossier|./refsjc|./refslnf"/>
    </xsl:template>
    
    <xsl:template match="il[@level='1']">
        <xsl:if test="not(preceding-sibling::*[1][self::il[@level='1']]) and not(preceding-sibling::*[1][self::il[@level='2']]) and not(preceding-sibling::*[1][self::il[@level='3']])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<l typenum="puce">]]></xsl:text>
        </xsl:if>
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::il[@level='2']]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<il>]]></xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<al>]]></xsl:text>
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <al>
                        <xsl:apply-templates/>
                    </al>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::il])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="il[@level='2']">
        <xsl:if test="preceding-sibling::*[1][self::il[@level='1']]">
            <xsl:text disable-output-escaping="yes"><![CDATA[<l typenum="tiret">]]></xsl:text>
        </xsl:if>
        <!--for coming level 2 list-->
        <xsl:if test="not(preceding-sibling::*[1][self::il])">
            <xsl:text disable-output-escaping="yes"><![CDATA[<l typenum="tiret"><il><al><l typenum="tiret">]]></xsl:text>
        </xsl:if>
        <xsl:choose>
            <xsl:when test="following-sibling::*[1][self::il[@level='3']]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<il>]]></xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<al>]]></xsl:text>
                <xsl:apply-templates/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <al>
                        <xsl:apply-templates/>
                    </al>
                </xsl:copy>
                <!--for level 2 list-->
                <!--<xsl:if test="not(following-sibling::*[1][self::il]) and not(preceding-sibling::il[@level='1'])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
                </xsl:if>-->
                <xsl:if test="not(following-sibling::*[1][self::il])">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</al>]]></xsl:text>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</il>]]></xsl:text>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
                </xsl:if>
                <xsl:if test="following-sibling::*[1][self::il[@level='1']]">
                    <xsl:text disable-output-escaping="yes"><![CDATA[</l></al></il>]]></xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="il[@level='3']">
        <xsl:if test="preceding-sibling::*[1][self::il[@level='2']]">
            <xsl:text disable-output-escaping="yes"><![CDATA[<l typenum="etoile">]]></xsl:text>
        </xsl:if>
        <xsl:copy>
            <al>
                <xsl:apply-templates/>
            </al>
        </xsl:copy>
        <xsl:if test="following-sibling::*[1][self::il[@level='1']]">
            <xsl:text disable-output-escaping="yes"><![CDATA[</l></al></il></l></al></il>]]></xsl:text>
        </xsl:if>
        <xsl:if test="following-sibling::*[1][self::il[@level='2']]">
            <xsl:text disable-output-escaping="yes"><![CDATA[</l></al></il>]]></xsl:text>
        </xsl:if>
        <xsl:if test="not(following-sibling::*[1][self::il])">
            <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</al>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</il>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</al>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</il>]]></xsl:text>
            <xsl:text>&#x0A;</xsl:text>
            <xsl:text disable-output-escaping="yes"><![CDATA[</l>]]></xsl:text>
        </xsl:if>
    </xsl:template>
    
    <xsl:template match="pn">
        <xsl:copy>
            <xsl:apply-templates select="node()[name()!='mc' and name()!='reftxt' and name()!='refsjc' and name()!='refslnf']"/>
        </xsl:copy>
        <xsl:apply-templates select="./mc|./reftxt|./refsjc|./refslnf"/>
    </xsl:template>
    
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
    
    
    
    
    <xsl:template match="//apimag[not(parent::imag)]">
        <xsl:text>&#x00A;</xsl:text>
        <imag>
            <xsl:text>&#x00A;</xsl:text>
            <xsl:variable name="imgname"><xsl:value-of select="$fnamewithoutext"/><xsl:text>_</xsl:text><xsl:value-of select="count(preceding::apimag) + 1"/><xsl:text>.tif</xsl:text></xsl:variable>
            <apimag fic="{$imgname}" scale="68"/>
            <xsl:text>&#x00A;</xsl:text>
        </imag>
        <xsl:text>&#x00A;</xsl:text>
        <!--<xsl:choose>
            <xsl:when test="preceding-sibling::*[1][name()='auteur']">
                
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#x00A;</xsl:text>
                <imag>
                    <xsl:text>&#x00A;</xsl:text>
                    <xsl:variable name="imgname"><xsl:value-of select="$fnamewithoutext"/><xsl:text>_</xsl:text><xsl:value-of select="count(preceding::apimag) + 1"/><xsl:text>.tif</xsl:text></xsl:variable>
                    <apimag fic="{$imgname}" scale="68"/>
                    <xsl:text>&#x00A;</xsl:text>
                </imag>
                <xsl:text>&#x00A;</xsl:text>
            </xsl:otherwise>
        </xsl:choose>-->
    </xsl:template>
    
    
    
    
    
    <xsl:template match="//fnote">
        <xsl:variable name="fnoteid" select="@id"/>
        <xsl:choose>
            <xsl:when test="//apnd/@refid = $fnoteid">
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    
    
    
    <xsl:template match="auteur" mode="move-auteur">
        <xsl:choose>
            <xsl:when test="parent::alerte[@type='veille']"></xsl:when>
            <xsl:when test="following-sibling::*[1][self::num]"></xsl:when>
            <xsl:when test="preceding-sibling::*[1][name()='introaut']">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="preceding-sibling::*[1][name()='introaut']"/>
                    <xsl:apply-templates select="imag,prenom,nom,qual"/>
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::al])">
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:if>
            </xsl:when>
            <xsl:otherwise>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:copy>
                    <xsl:apply-templates select="imag,prenom,nom,qual"/>
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:copy>
                <xsl:if test="not(following-sibling::*[1][self::al])">
                    <xsl:text>&#x0A;</xsl:text>
                </xsl:if>
            </xsl:otherwise>
        </xsl:choose>
        
    </xsl:template>
    
    <xsl:template match="//cita-bl">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::cita-bl]) and following-sibling::*[1][self::cita-bl]">
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[<cita-bl>]]></xsl:text>
                <al>
                    <xsl:apply-templates/>
                </al>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::cita-bl] and not(following-sibling::*[1][self::cita-bl])">
                <al>
                    <xsl:apply-templates/>
                </al>
                <xsl:text>&#x0A;</xsl:text>
                <xsl:text disable-output-escaping="yes"><![CDATA[</cita-bl>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::cita-bl]) and not(following-sibling::*[1][self::cita-bl])">
                <xsl:copy>
                    <al>
                        <xsl:apply-templates/>
                    </al>
                </xsl:copy>
            </xsl:when>
            <xsl:otherwise>
                <al>
                    <xsl:apply-templates/>
                </al>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="refjp_papl[not(parent::divref)]|reftxt_papl[not(parent::divref)]|refbib_papl[not(parent::divref)]"/>
    
    <xsl:template match="//divref/divref">
        <xsl:apply-templates/>
    </xsl:template>
    
    <xsl:template match="//formule">
        <xsl:choose>
            <xsl:when test="not(preceding-sibling::*[1][self::formule]) and following-sibling::*[1][self::formule]">
                <xsl:text disable-output-escaping="yes"><![CDATA[<fannexe><formule>]]></xsl:text>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:when>
            <xsl:when test="preceding-sibling::*[1][self::formule] and not(following-sibling::*[1][self::formule])">
                <xsl:apply-templates select="node()|@*"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[</formule></fannexe>]]></xsl:text>
            </xsl:when>
            <xsl:when test="not(preceding-sibling::*[1][self::formule]) and not(following-sibling::*[1][self::formule])">
                <xsl:apply-templates select="node()|@*"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:apply-templates select="node()|@*"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="//nb-bl">
        <xsl:choose>
            <xsl:when test="preceding-sibling::pn">
                <pn>
                    <xsl:copy>
                        <xsl:apply-templates select="node()|@*"/>
                    </xsl:copy>
                </pn>
            </xsl:when>
            <xsl:when test="preceding-sibling::p">
                <p>
                    <xsl:copy>
                        <xsl:apply-templates select="node()|@*"/>
                    </xsl:copy>
                </p>
            </xsl:when>
            <xsl:otherwise>
                <xsl:copy>
                    <xsl:apply-templates select="node()|@*"/>
                </xsl:copy>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>
    
</xsl:stylesheet>