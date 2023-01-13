<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    exclude-result-prefixes="xs"
    version="2.0">
    
    <xsl:output method="xml" omit-xml-declaration="no"/>
    
    <xsl:variable name="article-name" select="article/*[2]"/>
    
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="daterev">
        <xsl:variable name="date" select="tokenize(replace(., ' ', ' '), ' ')"/>
        <daterev>
            <xsl:choose>
                <xsl:when test="count($date) = 2">
                    <xsl:choose>
                        <xsl:when test="matches(.,' ')">
                            <xsl:attribute name="annee">
                                <xsl:value-of select="substring-after(., ' ')"/>
                            </xsl:attribute>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:attribute name="annee">
                                <xsl:value-of select="substring-after(., ' ')"/>
                            </xsl:attribute>
                        </xsl:otherwise>
                    </xsl:choose>
                    
                    <xsl:attribute name="mois">
                        <xsl:choose>
                            <xsl:when test="contains(., '-')">
                                <xsl:choose>
                                    <xsl:when test="lower-case(substring-before(., '-'))='janvier'">01</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='février'">02</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='mars'">03</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='avril'">04</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='mai'">05</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='juin'">06</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='juillet'">07</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='août'">08</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='septembre'">09</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='octobre'">10</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='novembre'">11</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., '-'))='décembre'">12</xsl:when>
                                    <xsl:otherwise>
                                        <xsl:value-of select="'0'"/>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:when>
                            <xsl:when test="not(contains(., '-'))">
                                <xsl:choose>
                                    <xsl:when test="lower-case(substring-before(., ' '))='janvier'">01</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='février'">02</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='mars'">03</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='avril'">04</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='mai'">05</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='juin'">06</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='juillet'">07</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='août'">08</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='septembre'">09</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='octobre'">10</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='novembre'">11</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='décembre'">12</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='janvier'">01</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='février'">02</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='mars'">03</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='avril'">04</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='mai'">05</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='juin'">06</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='juillet'">07</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='août'">08</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='septembre'">09</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='octobre'">10</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='novembre'">11</xsl:when>
                                    <xsl:when test="lower-case(substring-before(., ' '))='décembre'">12</xsl:when>
                                    <xsl:otherwise>
                                        <xsl:value-of select="'0'"/>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:when>
                        </xsl:choose>
                    </xsl:attribute>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:attribute name="annee">
                        <xsl:value-of select="$date[3]"/>
                    </xsl:attribute>
                    <xsl:attribute name="mois">
                        <xsl:choose>
                            <xsl:when test="$date[2]='Janvier'">01</xsl:when>
                            <xsl:when test="$date[2]='Février'">02</xsl:when>
                            <xsl:when test="$date[2]='Mars'">03</xsl:when>
                            <xsl:when test="$date[2]='Avril'">04</xsl:when>
                            <xsl:when test="$date[2]='Mai'">05</xsl:when>
                            <xsl:when test="$date[2]='Juin'">06</xsl:when>
                            <xsl:when test="$date[2]='Juillet'">07</xsl:when>
                            <xsl:when test="$date[2]='Août'">08</xsl:when>
                            <xsl:when test="$date[2]='Septembre'">09</xsl:when>
                            <xsl:when test="$date[2]='Octobre'">10</xsl:when>
                            <xsl:when test="$date[2]='Novembre'">11</xsl:when>
                            <xsl:when test="$date[2]='Décembre'">12</xsl:when>
                            <xsl:when test="$date[2]='janvier'">01</xsl:when>
                            <xsl:when test="$date[2]='février'">02</xsl:when>
                            <xsl:when test="$date[2]='mars'">03</xsl:when>
                            <xsl:when test="$date[2]='avril'">04</xsl:when>
                            <xsl:when test="$date[2]='mai'">05</xsl:when>
                            <xsl:when test="$date[2]='juin'">06</xsl:when>
                            <xsl:when test="$date[2]='juillet'">07</xsl:when>
                            <xsl:when test="$date[2]='août'">08</xsl:when>
                            <xsl:when test="$date[2]='septembre'">09</xsl:when>
                            <xsl:when test="$date[2]='octobre'">10</xsl:when>
                            <xsl:when test="$date[2]='novembre'">11</xsl:when>
                            <xsl:when test="$date[2]='décembre'">12</xsl:when>
                            <xsl:otherwise>
                                <xsl:value-of select="'0'"/>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:attribute>
                    
                    <xsl:attribute name="jour">
                        <xsl:value-of select="format-number(number($date[1]),'00')"/>
                    </xsl:attribute>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:apply-templates/>
        </daterev>
    </xsl:template>
    
    
    <xsl:template match="chronique|alerte|apercu|artdossier|commdo|comment|echeado|essentiel|etude|falerte|fiprat|formrfn|indices|panojuri|repere|texte">
            <xsl:copy>
                <xsl:copy-of select="@*"/>
                <xsl:for-each-group select="*" group-starting-with="pnchr|h1|h2|h3|h4">
                    <xsl:choose>
                        <xsl:when test="self::pnchr">
                            <xsl:text>&#x0A;</xsl:text>
                            <xsl:choose>
                                <xsl:when test="$article-name[local-name()='etude']">
                                    <xsl:choose>
                                        <xsl:when test="current-group()/observ/tinoye/sampletit">
                                            <xsl:apply-templates select="current-group()/observ/tinoye/sampletit"/>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <sampletit>ssaammppllee</sampletit>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    <pn><xsl:text>&#x0A;</xsl:text>
                                        <xsl:apply-templates select="current-group()/observ/tinoye"/>
                                        <xsl:copy-of select="remove(current-group(),1)"/>
                                        <xsl:text>&#x0A;</xsl:text>
                                    </pn>
                                </xsl:when>
                                <xsl:when test="$article-name[local-name()='texte']">
                                    <p><xsl:text>&#x0A;</xsl:text>
                                        <xsl:copy-of select="remove(current-group(),1)"/>
                                        <xsl:text>&#x0A;</xsl:text>
                                    </p>
                                </xsl:when>
                                <xsl:otherwise>
                                    <pnchr>
                                        <xsl:text>&#x0A;</xsl:text>
                                        <xsl:if test="sourcex[child::refsource]">
                                            <xsl:apply-templates select="current()/sourcex"/>
                                        </xsl:if>
                                        <xsl:text>&#x0A;</xsl:text>
                                        <observ>
                                            <xsl:apply-templates select="current-group()/observ/tinoye"/>
                                            <xsl:copy-of select="remove(current-group(),1)"/>
                                            <xsl:text>&#x0A;</xsl:text>
                                        </observ><xsl:text>&#x0A;</xsl:text>
                                    </pnchr>
                                </xsl:otherwise>
                            </xsl:choose>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:copy-of select="current-group()"/>
                        </xsl:otherwise>
                    </xsl:choose>
                </xsl:for-each-group>
            </xsl:copy>
    </xsl:template>
    <!--<xsl:template match="//sommdossier">
        <xsl:copy>
            <xsl:variable name="ligne">
            <xsl:text disable-output-escaping="yes"><![CDATA[<ligne>]]></xsl:text>
            </xsl:variable>
            <xsl:value-of select="replace()"/>
        </xsl:copy>
    </xsl:template>-->
    <!--<xsl:template match="text()[.='\[\[sommdossier\]\]']">
        <xsl:text disable-output-escaping="yes"><![CDATA[<sommdossier/>]]></xsl:text>
    </xsl:template>-->
    
<!--    <xsl:template match="al[name(following-sibling::*[1]) = 'il']">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()" />
            <xsl:for-each-group select="following-sibling::*" group-adjacent="name()">
                <xsl:if test="position()=1">
                    <xsl:copy-of select="current-group()" />
                </xsl:if>
            </xsl:for-each-group>
        </xsl:copy>
    </xsl:template>
    
    <xsl:template match="il[preceding-sibling::*[1] = (preceding-sibling::al[1] | preceding-sibling::il[1])]" />
    -->
    
    
    
</xsl:stylesheet>