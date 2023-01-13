<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:xlink="http://www.w3.org/1999/xlink"
    xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main"
    xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing"
    xmlns:num="http://whatever"
    exclude-result-prefixes="xs w wp xlink num"
    version="3.0">
    
    
    <xsl:output method="xml" omit-xml-declaration="no"/>
    
    <xsl:param name="get-path" as="xs:string" select="substring-before(base-uri(.), 'word')"/>
    <xsl:param name="file-name" as="xs:string" select="substring-after(tokenize(substring-before(base-uri(.), '/word'), '/')[last()], 'new_')"/>
    <xsl:param name="footnote-file" select="if (doc-available(concat($get-path,'word/','footnotes.xml'))) then doc(concat($get-path,'word/','footnotes.xml'))/w:footnotes else ''"/>
    

    <xsl:function name="num:roman" as="xs:integer">
        <xsl:param name="r" as="xs:string"/>
        <xsl:param name="s"/>
        <xsl:choose>
            <xsl:when test="ends-with($r,'cm')">
                <xsl:sequence select="900 + num:roman(substring($r,1,string-length($r)-2), 900)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'m')">
                <xsl:sequence select="1000+ num:roman(substring($r,1,string-length($r)-1), 1000)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'cd')">
                <xsl:sequence select="400+ num:roman(substring($r,1,string-length($r)-2), 400)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'d')">
                <xsl:sequence select="500+ num:roman(substring($r,1,string-length($r)-1), 500)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'xc')">
                <xsl:sequence select="90+ num:roman(substring($r,1,string-length($r)-2), 90)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'c')">
                <xsl:sequence select="(if(100 ge number($s)) then 100 else -100)+ num:roman(substring($r,1,string-length($r)-1), 100)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'xl')">
                <xsl:sequence select="40+ num:roman(substring($r,1,string-length($r)-2), 40)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'l')">
                <xsl:sequence select="50+ num:roman(substring($r,1,string-length($r)-1), 50)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'ix')">
                <xsl:sequence select="9+ num:roman(substring($r,1,string-length($r)-2), 9)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'x')">
                <xsl:sequence select="(if(10 ge number($s)) then 10 else -10) + num:roman(substring($r,1,string-length($r)-1), 10)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'iv')">
                <xsl:sequence select="4+ num:roman(substring($r,1,string-length($r)-2), 4)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'v')">
                <xsl:sequence select="5+ num:roman(substring($r,1,string-length($r)-1), 5)"/>
            </xsl:when>
            <xsl:when test="ends-with($r,'i')">
                <xsl:sequence select="(if(1 ge number($s)) then 1 else -1)+ num:roman(substring($r,1,string-length($r)-1), 1)"/>
            </xsl:when>
            <xsl:otherwise>
                <xsl:sequence select="0"/>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:function>
    <xsl:param name="countart" select="count(//w:pStyle[@w:val='NumArticle'])"/>
    <xsl:template match="w:document/w:body">
        <xsl:variable name="sommdoss">
            
        <xsl:for-each-group select="*" group-starting-with="w:tbl[w:tr/w:tc/w:p/w:r/w:t[.='Métadonnées']]">
            <xsl:variable name="num-article">
                <xsl:for-each select="current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]/w:r">
                    <xsl:choose>
                        <xsl:when test="matches(w:t, '^ivxlcdm|ii[^i]|iiii+|xx+|cccc+|v[^i]|[^i]?i[vx][ivxlcdm]|[^i]?i[^vix]')">
                            <xsl:value-of select="num:roman(., 0)"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="w:t"/>
                        </xsl:otherwise>
                    </xsl:choose>
                    <!--<xsl:value-of select="w:t"/>-->
                </xsl:for-each>
            </xsl:variable>
            <xsl:variable name="file-first-name">
                <!--<xsl:for-each select="current-group()/w:tr[2]/w:tc[2]/w:p//w:r">-->
                <xsl:variable name="filetxt">
                    <xsl:value-of select="current-group()[1]/w:tr[2]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                <xsl:value-of select="substring-before($filetxt, '_')"/>
                <!--</xsl:for-each>-->
            </xsl:variable>
            <!--<xsl:variable name="get-year">
                <xsl:for-each select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">
                    <!-\-<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-\->
                    <xsl:value-of select="if(contains(w:t, '-')) then concat('20', format-number(number(substring-before(w:t, '-')), '00')) else concat('20', format-number(number(w:t), '00'))"/>
                </xsl:for-each>
            </xsl:variable>-->
            <xsl:variable name="get-year">
                <!--<xsl:for-each select="current-group()[1]/w:tr[3]/w:tc[2]/w:p//w:r[position()= last()]">-->
                <!--<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-->
                <xsl:variable name="yeartext">
                    <xsl:value-of select="current-group()[1]/w:tr[3]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                
                <xsl:value-of select="if(matches($yeartext, '[0-9]{4}')) then replace($yeartext, '(.*)[0-9]{2}([0-9]{2})', '$2') else '20'"/>
                
                <!--</xsl:for-each>-->
            </xsl:variable>
            <xsl:variable name="article-num">
                <!--<xsl:for-each select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">-->
                <!--<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-->
                <xsl:variable name="artnum">
                    <xsl:value-of select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                <xsl:value-of select="if(contains($artnum, '-')) then format-number(number(substring-before($artnum, '-')), '00') else format-number(number($artnum), '00')"/>
                <!--</xsl:for-each>-->
            </xsl:variable>
            
            <xsl:variable name="article-name">
                <xsl:for-each select="current-group()/w:tr[5]/w:tc[2]//w:p/w:r">
                    <xsl:variable name="article-element" select="substring-before(w:t, ' ')"/>
                    <xsl:variable name="article-attribute" select="substring-before(substring-after(w:t, '('), ')')"/>
                    <xsl:value-of select="w:t"/>
                </xsl:for-each>
            </xsl:variable>
            <xsl:variable name="article-name1">
                <!--<xsl:for-each select="current-group()/w:tr[5]/w:tc[2]//w:p/w:r">-->
                <xsl:variable name="artext"><xsl:value-of select="current-group()/w:tr[5]/w:tc[2]//w:p//text()"/></xsl:variable>
                <xsl:variable name="article-type" select="substring-before($artext, ' ')"/>
                <xsl:variable name="article" select="substring-before(substring-after($artext, '('), ')')"/>
                <xsl:choose>
                    <xsl:when test="$article='couverture'">cv</xsl:when>
                    <!--<xsl:when test="$article='texte'">te</xsl:when>-->
                    <xsl:when test="$article='texte'">tx</xsl:when>
                    <xsl:when test="$article='panojuri'">pj</xsl:when>
                    <xsl:when test="$article='essentiel'">es</xsl:when>
                    <xsl:when test="$article='repere'">re</xsl:when>
                    <xsl:when test="$article='comment'">cm</xsl:when>
                    <xsl:when test="$article='alerte'">al</xsl:when>
                    <xsl:when test="$article='falerte'">fa</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='entretien'">en</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='tableronde'">en</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='etude'">et</xsl:when>
                    <xsl:when test="$article='artdossier'">do</xsl:when>
                    <xsl:when test="$article='etude' and not($article-type='tableronde' or $article-type='entretien' or $article-type='etude')">ed</xsl:when>
                    <xsl:when test="$article='chronique'">ch</xsl:when>
                    <xsl:when test="$article='commentaire'">cm</xsl:when>
                    <xsl:when test="$article='formrfn'">fo</xsl:when>
                    <xsl:when test="$article='fiprat'">fp</xsl:when>
                    <xsl:when test="$article='indices'">in</xsl:when>
                    <xsl:when test="$article='apercu' and $article-type='propos'">lp</xsl:when>
                    <xsl:when test="$article='apercu' and not($article-type='propos')">ap</xsl:when>
                    <xsl:when test="$article='propos'">lp</xsl:when>
                    <xsl:when test="$article='tables'">ta(2)</xsl:when>
                    <xsl:when test="$article='echeancier'">ec(2)</xsl:when>
                </xsl:choose>
                <!--</xsl:for-each>-->
            </xsl:variable>
            <xsl:if test="$article-name1 != 'do'">
            <xsl:variable name="titlenm" select="current-group()/self::w:p[./w:pPr/w:pStyle[@w:val='Titre']]//text()"/>
            
            <xsl:variable name="artid" select="concat($file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else format-number(position(), '00000'))"/>
            <xsl:text>[[ligne]]</xsl:text>
            <xsl:text>[[artid]]</xsl:text><xsl:value-of select="$artid"/><xsl:text>[[/artid]]</xsl:text>
            <xsl:text>[[titreArticle]]</xsl:text><xsl:value-of select="$titlenm"/><xsl:text>[[/titreArticle]]</xsl:text>
                <xsl:for-each select="current-group()/self::w:p[./w:pPr/w:pStyle[@w:val='AuteurprnomNOM']]">
                    <xsl:variable name="prenom" select=".//text()"/>
                    <xsl:text>[[prenomAuteur]]</xsl:text><xsl:value-of select="$prenom"/><xsl:text>[[prenomAuteur]]</xsl:text>
                </xsl:for-each>
            <xsl:text>[[/ligne]]</xsl:text>
            </xsl:if>
        </xsl:for-each-group>
            
        </xsl:variable>
        <xsl:for-each-group select="*" group-starting-with="w:tbl[w:tr/w:tc/w:p/w:r/w:t[.='Métadonnées']]">
            <xsl:variable name="num-article">
                <xsl:for-each select="current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]/w:r">
                    <xsl:choose>
                        <xsl:when test="matches(w:t, '^ivxlcdm|ii[^i]|iiii+|xx+|cccc+|v[^i]|[^i]?i[vx][ivxlcdm]|[^i]?i[^vix]')">
                            <xsl:value-of select="num:roman(., 0)"/>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="w:t"/>
                        </xsl:otherwise>
                    </xsl:choose>
                    <!--<xsl:value-of select="w:t"/>-->
                </xsl:for-each>
            </xsl:variable>
            <xsl:variable name="file-first-name">
                <!--<xsl:for-each select="current-group()/w:tr[2]/w:tc[2]/w:p//w:r">-->
                <xsl:variable name="filetxt">
                    <xsl:value-of select="current-group()[1]/w:tr[2]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                <xsl:value-of select="substring-before($filetxt, '_')"/>
                <!--</xsl:for-each>-->
            </xsl:variable>
            <!--<xsl:variable name="get-year">
                <xsl:for-each select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">
                    <!-\-<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-\->
                    <xsl:value-of select="if(contains(w:t, '-')) then concat('20', format-number(number(substring-before(w:t, '-')), '00')) else concat('20', format-number(number(w:t), '00'))"/>
                </xsl:for-each>
            </xsl:variable>-->
            <xsl:variable name="get-year">
                <!--<xsl:for-each select="current-group()[1]/w:tr[3]/w:tc[2]/w:p//w:r[position()= last()]">-->
                    <!--<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-->
                <xsl:variable name="yeartext">
                    <xsl:value-of select="current-group()[1]/w:tr[3]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                    
                <xsl:value-of select="if(matches($yeartext, '[0-9]{4}')) then replace($yeartext, '(.*)[0-9]{2}([0-9]{2})', '$2') else '20'"/>
                    
                <!--</xsl:for-each>-->
            </xsl:variable>
            <xsl:variable name="article-num">
                <!--<xsl:for-each select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">-->
                    <!--<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-->
                <xsl:variable name="artnum">
                    <xsl:value-of select="current-group()[1]/w:tr[4]/w:tc[2]/w:p//text()"/>
                </xsl:variable>
                <xsl:value-of select="if(contains($artnum, '-')) then format-number(number(substring-before($artnum, '-')), '00') else format-number(number($artnum), '00')"/>
                <!--</xsl:for-each>-->
            </xsl:variable>
            
            <xsl:variable name="article-name">
                <xsl:for-each select="current-group()/w:tr[5]/w:tc[2]//w:p/w:r">
                    <xsl:variable name="article-element" select="substring-before(w:t, ' ')"/>
                    <xsl:variable name="article-attribute" select="substring-before(substring-after(w:t, '('), ')')"/>
                    <xsl:value-of select="w:t"/>
                </xsl:for-each>
            </xsl:variable>
            <xsl:variable name="article-name1">
                <!--<xsl:for-each select="current-group()/w:tr[5]/w:tc[2]//w:p/w:r">-->
                <xsl:variable name="artext"><xsl:value-of select="current-group()/w:tr[5]/w:tc[2]//w:p//text()"/></xsl:variable>
                <xsl:variable name="article-type" select="substring-before($artext, ' ')"/>
                <xsl:variable name="article" select="substring-before(substring-after($artext, '('), ')')"/>
                    <xsl:choose>
                        <xsl:when test="$article='couverture'">cv</xsl:when>
                        <!--<xsl:when test="$article='texte'">te</xsl:when>-->
                        <xsl:when test="$article='texte'">tx</xsl:when>
                        <xsl:when test="$article='panojuri'">pj</xsl:when>
                        <xsl:when test="$article='essentiel'">es</xsl:when>
                        <xsl:when test="$article='repere'">re</xsl:when>
                        <xsl:when test="$article='comment'">cm</xsl:when>
                        <xsl:when test="$article='alerte'">al</xsl:when>
                        <xsl:when test="$article='falerte'">fa</xsl:when>
                        <xsl:when test="$article='etude' and $article-type='entretien'">en</xsl:when>
                        <xsl:when test="$article='etude' and $article-type='tableronde'">en</xsl:when>
                        <xsl:when test="$article='etude' and $article-type='etude'">et</xsl:when>
                        <xsl:when test="$article='artdossier'">do</xsl:when>
                        <xsl:when test="$article='etude' and not($article-type='tableronde' or $article-type='entretien' or $article-type='etude')">ed</xsl:when>
                        <xsl:when test="$article='chronique'">ch</xsl:when>
                        <xsl:when test="$article='commentaire'">cm</xsl:when>
                        <xsl:when test="$article='formrfn'">fo</xsl:when>
                        <xsl:when test="$article='fiprat'">fp</xsl:when>
                        <xsl:when test="$article='indices'">in</xsl:when>
                        <xsl:when test="$article='apercu' and $article-type='propos'">lp</xsl:when>
                        <xsl:when test="$article='apercu' and not($article-type='propos')">ap</xsl:when>
                        <xsl:when test="$article='propos'">lp</xsl:when>
                        <xsl:when test="$article='tables'">ta(2)</xsl:when>
                        <xsl:when test="$article='echeancier'">ec(2)</xsl:when>
                    </xsl:choose>
                <!--</xsl:for-each>-->
            </xsl:variable>
            <xsl:variable name="article-type" select="substring-before(substring-after($article-name, '('), ' )')"/>
            <xsl:variable name="nonumart" select="abs(position() - $countart)"/>
            
            <!--            if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]/w:r[1]/normalize-space(w:t)) then format-number($num-article, '00000') else concat('_', format-number(position(), '00')))-->
            <xsl:result-document href="{concat($get-path, $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else format-number($nonumart, '00000'))}.xml">
            <xsl:text>&#x0A;</xsl:text>
                <article>
                <xsl:text>&#x0A;</xsl:text>
                <!-- Body -->
                <xsl:if test="local-name(.)='tbl'">
                    <metaart>
                        <xsl:text>&#x0A;</xsl:text>
                        <publ>
                            <nomrev>
                                <xsl:value-of select="w:tr[2]/w:tc[2]/w:p/w:sdt/w:sdtContent/w:r/substring-before(w:t, '_')"/>
                                <xsl:value-of select="w:tr[2]/w:tc[2]/w:p/w:r/substring-before(w:t, '_')"/>
                            </nomrev>
                            <daterev>
                                <xsl:for-each select="w:tr[3]/w:tc[2]/w:p/w:r">
                                    <xsl:value-of select="w:t"/>
                                </xsl:for-each>
                            </daterev>
                            <revnumr>
                                <xsl:value-of select="w:tr[4]/w:tc[2]/w:p/w:r/w:t"/>
                            </revnumr>
                            <numeltr></numeltr><partrev></partrev>
                            <pagerev></pagerev>
                            <partie></partie>
                            <xsl:text>&#x0A;</xsl:text>
                        </publ>
                        <xsl:text>&#x0A;</xsl:text>
                    </metaart>
                    <xsl:text>&#x0A;</xsl:text>
                    <xsl:text disable-output-escaping="yes"><![CDATA[<]]></xsl:text>
                    <xsl:value-of select="concat(substring-before(substring-after($article-name, '('), ')'), ' type=&quot;')"/>
                    <xsl:value-of select="normalize-space(substring-before($article-name, '('))"/>
                    <xsl:text disable-output-escaping="yes"><![CDATA[">]]></xsl:text>
                </xsl:if>
                
                <xsl:for-each select="current-group()[self::w:p]|current-group()[self::w:sdt]|current-group()[self::w:tbl[not(w:tr/w:tc/w:p/w:r/w:t[1]/text()='Métadonnées')]]">
                    <xsl:if test="local-name(.)='p'">
                        <xsl:choose>
                            <xsl:when test="count(.//w:r) = 0 and not(w:pPr/w:pStyle[@w:val='Paragraphenum-Titrenoy']) and not(w:pPr/w:pStyle[@w:val='Titre-niv1'])"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Chemindefer']"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-Partie']"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D1Part']"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D2Part']"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D3Part']"/>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='NumArticle']">
                                <xsl:if test=".[preceding-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='AuteurprnomNOM']]]">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                                <num>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="normalize-space(w:t)"/>
                                    </xsl:for-each>
                                </num>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Theme']">
                                <theme>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </theme>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Sur-titre']">
                                <surtit>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </surtit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre']">
                                <xsl:text>&#x0A;</xsl:text>
                                <tit>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:choose>
                                                <xsl:when test="w:rPr[w:i]">
                                                    <emph2>
                                                        <xsl:value-of select="w:t"/>
                                                    </emph2>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:value-of select="w:t"/>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:for-each>
                                    </al>
                                    <xsl:text>&#x0A;</xsl:text>
                                </tit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Title']">
                                <xsl:text>&#x0A;</xsl:text>
                                <tit>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                    <xsl:text>&#x0A;</xsl:text>
                                    
                                </tit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Subtitle']">
                                <xsl:text>&#x0A;</xsl:text>
                                <sstit1>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                    <xsl:text>&#x0A;</xsl:text>
                                </sstit1>
                                <xsl:text>&#x0A;</xsl:text>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Sous-titre']">
                                <xsl:text>&#x0A;</xsl:text>
                                <sstit1>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                    <xsl:text>&#x0A;</xsl:text>
                                </sstit1>
                                <xsl:text>&#x0A;</xsl:text>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Point-cl']">
                                <xsl:if test="./not(preceding-sibling::*[1][self::w:p[w:pPr/w:pStyle[@w:val='Point-cl']]])">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[<pointcles>]]></xsl:text>
                                    <xsl:text>&#x0A;</xsl:text>
                                </xsl:if>
                                <pointcle>
                                    <xsl:for-each select="w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:i]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </pointcle>
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:if test="./not(following-sibling::*[1][self::w:p[w:pPr/w:pStyle[@w:val='Point-cl']]])">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</pointcles>]]></xsl:text>
                                </xsl:if>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='RefDO']">
                                <refdo>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refdo>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='RefSrce']">
                                <xsl:text>&#x0A;</xsl:text>
                                <sourcex>
                                <refsource type="autre">
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refsource>
                                </sourcex>
                            </xsl:when>
                            <xsl:when test="if(w:ins) then w:ins/w:r/w:t[starts-with(., '&lt;EXTRAIT') or starts-with(., 'EXTRAIT')] else w:r/w:t[starts-with(., '&lt;EXTRAIT') or starts-with(., 'EXTRAIT')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<sourcex><extrait>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="if(w:ins) then w:ins/w:r/w:t[starts-with(., '&lt;FinEXTRAIT') or starts-with(., 'FinEXTRAIT')] else w:r/w:t[starts-with(., '&lt;FinEXTRAIT') or starts-with(., 'FinEXTRAIT')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[</extrait></sourcex>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="if(w:ins) then w:ins/w:r/w:t[starts-with(., '&lt;CORPS')] else w:r/w:t[starts-with(., '&lt;CORPS')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<!--<corps>-->]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="if(w:ins) then w:ins/w:r/w:t[starts-with(., '&lt;Fin-CORPS') or starts-with(., 'Fin-CORPS')] else w:r/w:t[starts-with(., '&lt;Fin-CORPS') or starts-with(., 'Fin-CORPS')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<!--</corps>-->]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="if(w:ins) then w:ins/w:r/w:t[starts-with(., '[[SOMMAIRE') or starts-with(., '[[SOMMAIRE/PLAN]]')] else w:r/w:t[starts-with(., '[[SOMMAIRE') or starts-with(., '[[SOMMAIRE/PLAN]]')]">
                                <sommdossier>
                                <xsl:value-of select="$sommdoss"/>
                                </sommdossier>
                                <!--<xsl:text disable-output-escaping="yes"><![CDATA[<plan/>]]></xsl:text>-->
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Extrait-titre']">
                                <tit>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </tit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Typedenote']">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:choose>
                                    <xsl:when test="matches(.//w:r[1]/w:t, 'notescom')">
                                        <!--<notescom type="note">
                                            <xsl:for-each select="w:r">
                                                <xsl:apply-templates select="w:t"/>
                                            </xsl:for-each>
                                        </notescom>-->
                                        <xsl:choose>
                                            <xsl:when test="matches(.//w:r[2]/w:t, '\(notes\)') or matches(.//w:r[1]/w:t, 'type=&quot;notes&quot;') or matches(.//w:r[2]/w:t, 'type=&quot;notes&quot;')">
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="note"/>
]]></xsl:text>
                                            </xsl:when>
                                            <xsl:when test="matches(.//w:r[2]/w:t, '\(observation\)') or matches(.//w:r[1]/w:t, 'type=&quot;observation&quot;') or matches(.//w:r[2]/w:t, 'type=&quot;observation&quot;')">
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="observation"/>
]]></xsl:text>
                                            </xsl:when>
                                            <xsl:when test="matches(.//w:r[2]/w:t, '(conclusions)') or matches(.//w:r[1]/w:t, 'type=&quot;conclusions&quot;') or matches(.//w:r[2]/w:t, 'type=&quot;conclusions&quot;')">
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="conclusions"/>
]]></xsl:text>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="note"/>
]]></xsl:text>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:when>
                                    
                                    <xsl:when test="matches(.//w:r[1]/w:t, 'concom')">
                                        <xsl:text disable-output-escaping="yes"><![CDATA[<concom type="conclusion"/>
]]></xsl:text>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <xsl:text disable-output-escaping="yes"><![CDATA[<notescom type="note"/>
]]></xsl:text>
                                    </xsl:otherwise>
                                </xsl:choose>
                                
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Auteurintro']">
                                <xsl:text>&#x0A;</xsl:text>
                                <introaut>
                                    <xsl:for-each select="w:r">
                                        <xsl:apply-templates select="w:t"/>
                                    </xsl:for-each>
                                </introaut>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='AuteurprnomNOM']">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<auteur>]]></xsl:text>
                                <xsl:for-each select="w:r">
                                    <xsl:choose>
                                        <xsl:when test="w:rPr/w:color[not(@w:val='auto')]">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <imag>
                                                <xsl:text>&#x0A;</xsl:text>
                                                <apimag fic="{w:t}" scale="40"/>
                                                <xsl:text>&#x0A;</xsl:text>
                                            </imag>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <prenom>
                                                <xsl:value-of select="w:t"/>
                                            </prenom>
                                        </xsl:otherwise>
                                    </xsl:choose>
<!--                                    <xsl:if test="w:t[not(@xml:space='preserve')] and position()=1">
                                        <prenom>
                                            <xsl:value-of select="w:t"/>
                                        </prenom>
                                    </xsl:if>
                                    <xsl:if test="w:t[not(@xml:space='preserve')] and position()=3">
                                        <nom>
                                            <xsl:value-of select="w:t"/>
                                        </nom>
                                    </xsl:if>-->
                                </xsl:for-each>
                                <xsl:if test="count(w:r)=0">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                                <xsl:if test="./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Mots-Cles']] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Paragraphesimple']] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Paragraphenum-Titrenoy']] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Chapeau']] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[contains(@w:val, 'Titre-niv')]] or ./following-sibling::*[1][self::w:bookmarkEnd] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[contains(@w:val, 'BR-ref-dossier')]] or ./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[contains(@w:val, 'RefSrce')]] or ./following-sibling::*[1][self::w:tbl/w:tblPr/w:tblStyle[contains(@w:val, 'TableGrid')]]">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                                <xsl:if test="./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='AuteurprnomNOM']]">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                                <xsl:if test="count(./following-sibling::*) = 0">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                                <xsl:if test="./following-sibling::*[1][self::w:tbl[w:tr/w:tc/w:p/w:r/w:t[.='Métadonnées']]]">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                            </xsl:when>
                            
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Auteurqualit']">
                                <xsl:text>&#x0A;</xsl:text>
                                <qual>
                                <xsl:for-each select="w:r">
                                    <xsl:apply-templates select="w:t"/>
                                </xsl:for-each>
                                </qual>
                                <xsl:if test="not(./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Auteurqualit']])">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:text disable-output-escaping="yes"><![CDATA[</auteur>]]></xsl:text>
                                </xsl:if>
                            </xsl:when>
                            
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Chapeau']">
                                <xsl:text>&#x0A;</xsl:text>
                                <resume>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <al>
                                        <!--<emph1>-->
                                        <xsl:for-each select="if(w:ins) then w:ins/w:r else w:r">
                                            <!--<xsl:choose>
                                                <xsl:when test="w:rPr/w:i">
                                                    <!-\-<xsl:text disable-output-escaping="yes"><![CDATA[</emph1>]]></xsl:text>-\->
                                                    <emph3>
                                                        <xsl:value-of select="w:t"/>
                                                    </emph3>
                                                    <!-\-<xsl:text disable-output-escaping="yes"><![CDATA[<emph1>]]></xsl:text>-\->
                                                </xsl:when>
                                                <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep'] or w:footnoteReference]">
                                                    <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:value-of select="w:t"/>
                                                </xsl:otherwise>
                                            </xsl:choose>-->
                                            <xsl:choose>
                                                <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                    <emph1>
                                                        <xsl:value-of select="w:t"/>
                                                    </emph1>
                                                </xsl:when>
                                                <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                    <emph2>
                                                        <xsl:value-of select="w:t"/>
                                                    </emph2>
                                                </xsl:when>
                                                <xsl:when test="w:rPr[w:i and w:b]">
                                                    <emph3>
                                                        <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                    </emph3>
                                                </xsl:when>
                                                <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                    <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:value-of select="w:t"/>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:for-each>
                                        <!--</emph1>-->
                                    </al>
                                    <xsl:text>&#x0A;</xsl:text>
                                </resume>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre-niv1']">
<!--                                <xsl:if test="./preceding-sibling::*[1][self::w:p/w:pPr/w:pStyle[contains(@w:val, 'Liste-niv')]]">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</al>]]></xsl:text>
                                </xsl:if>-->
                                <xsl:choose>
                                    <xsl:when test="w:r">
                                        <h1>
                                            <xsl:for-each select="w:r">
                                                <!--<xsl:value-of select="w:t"/>-->
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[w:i]">
                                                        <emph3>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph3>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </h1>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <h1><sampletit><xsl:text>ssaammppllee</xsl:text></sampletit></h1>
                                    </xsl:otherwise>
                                </xsl:choose>
                                
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Paragraphenum-Titrenoy']">
                                <xsl:choose>
                                    <xsl:when test="w:r">
                                        <pnchr><observ><tinoye><xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each></tinoye></observ></pnchr>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <pnchr><observ><tinoye><sampletit>ssaammppllee</sampletit></tinoye></observ></pnchr>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre-niv2']">
                                <xsl:text>&#x0A;</xsl:text>
                                <h2>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </h2>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre-niv3']">
                                <xsl:text>&#x0A;</xsl:text>
                                <h3>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </h3>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre-niv4']">
                                <xsl:text>&#x0A;</xsl:text>
                                <h4>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </h4>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Titre-intro']">
                                <!--<tit>
                                    <al>Introduction</al>
                                </tit>-->
                                <xsl:text>&#x0A;</xsl:text>
                                <pn_intro><al>
                                    <xsl:for-each select="w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al></pn_intro>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Intro']">
                                <!--<tit>
                                    <al>Introduction</al>
                                </tit>-->
                                <xsl:text>&#x0A;</xsl:text>
                                <pn_intro><al>
                                    <xsl:for-each select="w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al></pn_intro>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Paragraphesimple']">
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:choose>
                                    <xsl:when test="preceding-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Intro']]">
                                        <divintro><pn><al>
                                            <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[not(w:b) and w:i]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:i and w:b]">
                                                        <emph3>
                                                            <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                        </emph3>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:rStyle[@w:val='FootnoteReference']] or w:footnoteReference">
                                                        <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                    </xsl:when>
                                                    <xsl:when test="w:sym">
                                                        <xsl:text><![CDATA[&#x]]></xsl:text>
                                                        <xsl:value-of select="w:sym/@w:char"/>
                                                        <xsl:text><![CDATA[;]]></xsl:text>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </al></pn></divintro>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <al>
                                            <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                        <emph1>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph1>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:i and w:b]">
                                                        <emph3>
                                                            <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                        </emph3>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                        <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                    </xsl:when>
                                                    <xsl:when test="w:sym">
                                                        <xsl:text disable-output-escaping="yes">&amp;#x</xsl:text>
                                                        <xsl:value-of select="w:sym/@w:char"/>
                                                        <xsl:text><![CDATA[;]]></xsl:text>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </al>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Emev']">
                                <xsl:text>&#x0A;</xsl:text>
                                <al emev="oui">
                                    <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Citation-bloc']">
                                <xsl:text>&#x0A;</xsl:text>
                                <cita-bl>
                                    <xsl:for-each select="w:r">
                                        <!--<xsl:value-of select="w:t"/>-->
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </cita-bl>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Liste-niv1']">
                                <xsl:text>&#x0A;</xsl:text>
                                <il level="1">
                                    <xsl:for-each select="w:r">
                                        <!--<xsl:value-of select="w:t"/>-->
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </il>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Liste-niv2']">
                                <xsl:text>&#x0A;</xsl:text>
                                <il level="2">
                                    <xsl:for-each select="w:r">
                                        <!--<xsl:value-of select="w:t"/>-->
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </il>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Liste-niv3']">
                                <xsl:text>&#x0A;</xsl:text>
                                <il level="3">
                                    <xsl:for-each select="w:r">
                                        <!--<xsl:value-of select="w:t"/>-->
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </il>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='c01pointnumerotealtn']">
                                <xsl:for-each select="w:r">
                                    <imag>
                                        <xsl:text>&#x0A;</xsl:text>
                                        <xsl:element name="apimag">
                                            <xsl:attribute name="fic">
                                                <xsl:value-of select="w:t"/>
                                            </xsl:attribute>
                                        </xsl:element>
                                    </imag>
                                </xsl:for-each>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:jc[@w:val='both']">
                                <xsl:text>&#x0A;</xsl:text>
                                <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </al>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='NBBL-encadr-titre']">
                                <tit>
                                    <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                    </al>
                                </tit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='NBBL-normal']">
<!--                                <xsl:element name="nb-bl">
                                        <xsl:attribute name="titre">
                                            <xsl:value-of select="lower-case(./preceding-sibling::*[1][self::w:sdt/w:sdtContent/w:p/w:r/w:t])"/>
                                        </xsl:attribute>-->
                                    <al>
                                        <xsl:for-each select="if(w:ins) then w:ins/w:r else w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[w:i]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                        </xsl:for-each>
                                        <xsl:if test="w:moveTo">
                                            <xsl:for-each select="w:moveTo/w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[w:i]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </xsl:if>
                                    </al>
                                <xsl:if test="not(./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='NBBL-normal']])">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</nb-bl>]]></xsl:text>
                                </xsl:if>
                                <!--</xsl:element>-->
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='NBBL-encadr-titre']">
                                <tit>
                                    <xsl:for-each select="w:t">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </tit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Relance']">
                                <al><relance>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </relance></al>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Exergue']">
                                <xsl:text>&#x0A;</xsl:text>
                                <exergue>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </exergue>
                                <xsl:text>&#x0A;</xsl:text>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Locuteur']">
                                <xsl:text>&#x0A;</xsl:text>
                                <loc>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </loc>
                                <xsl:text>&#x0A;</xsl:text>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Intertitre']">
                                <intertit>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </intertit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Intertitre0']">
                                <intertit>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </intertit>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='chronique']">
                                <neant/>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Image-legende']">
                                <legimag>
                                    <al>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </al>
                                </legimag>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Image']">
                                <apimag>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </apimag>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Image-titre']">
                                <tiimag>
                                    <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                    </al>
                                </tiimag>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Tableau-legende']">
                                <legtab>
                                    <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                    </al>
                                </legtab>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Tableau-titre']">
                                <titab>
                                    <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                    </al>
                                </titab>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='BR-dossier']">
                                <refsdossier>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refsdossier>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='BR-ref-dossier']">
                                <refsdossier>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refsdossier>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='BR-JC']">
                                <refsjc>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refsjc>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='BR-LNF']">
                                <refslnf>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refslnf>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='BR-textes']">
                                <reftxt>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </reftxt>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Formule-alinea']">
                                <formule><al-f>
                                    <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., 'zonedesaisie')] and preceding-sibling::w:r/w:t[starts-with(., '[[')]">
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<zoneczi/>]]></xsl:text>
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., '[[')] and following-sibling::w:r/w:t[starts-with(., 'zonedesaisie')]">
                                                
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., ']]')] and preceding-sibling::w:r/w:t[starts-with(., 'zonedesaisie')]">
                                                
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., '...ligne de point...')] and preceding-sibling::w:r/w:t[starts-with(., '[[')]">
                                                <xsl:text disable-output-escaping="yes"><![CDATA[<ligpoint/>]]></xsl:text>
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., '[[')] and following-sibling::w:r/w:t[starts-with(., '...ligne de point...')]">
                                                
                                            </xsl:when>
                                            <xsl:when test="w:t[starts-with(., ']]')] and preceding-sibling::w:r/w:t[starts-with(., '...ligne de point...')]">
                                                
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al-f></formule>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Formule-intertitre']">
                                <formule><tbase><intertiff><al-f>
                                    <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al-f></intertiff></tbase></formule>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Formule-observ']">
                                <formule><observff>
                                    <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </observff></formule>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Formule-titre']">
                                <formule><tiff>
                                    <al-f>
                                    <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                <emph1>
                                                    <xsl:value-of select="w:t"/>
                                                </emph1>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                <emph2>
                                                    <xsl:value-of select="w:t"/>
                                                </emph2>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:i and w:b]">
                                                <emph3>
                                                    <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                </emph3>
                                            </xsl:when>
                                            <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    </al-f>
                                </tiff></formule>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='PAPL-div']">
                                <divref>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </divref>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='PAPL-bib']">
                                <refbib_papl>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refbib_papl>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='PAPL-JP']">
                                <refjp_papl>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </refjp_papl>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='PAPL-textes']">
                                <reftxt_papl>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </reftxt_papl>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Conclusion']">
                                <divconcl>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </divconcl>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Conclusion']">
                                <divconcl>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </divconcl>
                            </xsl:when>
                            <xsl:when test="w:r[1]/w:t[starts-with(., '&lt;ANNEXE')] or w:r[2]/w:t[starts-with(., 'ANNEXE')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<annexes><annexe>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="w:r[1]/w:t[starts-with(., '&lt;FinANNEXE')] or w:r[2]/w:t[starts-with(., 'FinANNEXE')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[</annexe></annexes>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="w:r[1]/w:t[starts-with(., '&lt;BIBLIO')] or w:r[2]/w:t[starts-with(., 'BIBLIO')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<divbib><div1bib>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="w:r[1]/w:t[starts-with(., '&lt;FinBIBLIO')] or w:r[2]/w:t[starts-with(., 'FinBIBLIO')]">
                                <xsl:text disable-output-escaping="yes"><![CDATA[</div1bib></divbib>]]></xsl:text>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Ligne de point']">
                                <ligpoint>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </ligpoint>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Zone de saisie']">
                                <zoneczi>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </zoneczi>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Note de bas de page']">
                                <fnotes>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </fnotes>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:rPr[w:rFonts[@w:ascii='Times New Roman' and @w:hAnsi='Times New Roman'] and w:sz[@w:val='24']]">
                                <xsl:text>&#x0A;</xsl:text>
                                <al>
                                    <xsl:for-each select="w:r">
                                        <xsl:choose>
                                            <xsl:when test="w:rPr[w:rFonts[@w:ascii='Times New Roman' and @w:hAnsi='Times New Roman']]">
                                                <xsl:value-of select="w:t"/>
                                            </xsl:when>
                                        </xsl:choose>
                                    </xsl:for-each>
                                </al>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='Mots-Cles']">
                                <mc>
                                    <xsl:for-each select="w:r">
                                        <xsl:value-of select="w:t"/>
                                    </xsl:for-each>
                                </mc>
                            </xsl:when>
                            <xsl:when test="w:pPr/w:pStyle[@w:val='NBBL-type']">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<nb-bl titre="]]></xsl:text>
                                <!--<xsl:value-of select="if(w:ins) then w:ins/w:r/w:t else w:r/w:t"/>-->
                                <xsl:variable name="nbbltype" select="lower-case(.//text())"/>
                                <xsl:choose>
                                    <xsl:when test="$nbbltype eq 'exemple'">
                                        <xsl:text>exemple</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="$nbbltype eq 'conseil pratique'">
                                        <xsl:text>conseilpratique</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="$nbbltype eq 'encadré'">
                                        <xsl:text>encadre</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="$nbbltype eq 'pour aller plus loin'">
                                        <xsl:text>pourallerplusloin</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="$nbbltype eq 'remarque'">
                                        <xsl:text>remarque</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="$nbbltype eq 'attention'">
                                        <xsl:text>attention</xsl:text>
                                    </xsl:when>
                                </xsl:choose>
                                <xsl:text disable-output-escaping="yes"><![CDATA[">]]></xsl:text>
                                <!--<xsl:if test="not(./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='NBBL-normal']])">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</nb-bl>]]></xsl:text>
                                </xsl:if>-->
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:text>&#x0A;</xsl:text>
                                <xsl:choose>
                                    <xsl:when test="preceding-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Intro']]">
                                        <divintro><pn><al>
                                            <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[not(w:b) and w:i]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:i and w:b]">
                                                        <emph3>
                                                            <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                        </emph3>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                        <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </al></pn></divintro>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <al>
                                            <xsl:for-each select="if(w:ins[not(preceding-sibling::w:r) and not(following-sibling::*)]) then w:ins/w:r else w:r|w:ins/w:r">
                                                <xsl:choose>
                                                    <xsl:when test="w:rPr[w:b and not(w:i)]">
                                                        <emph1>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph1>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[not(w:b) and w:i and not(w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference'])]">
                                                        <emph2>
                                                            <xsl:value-of select="w:t"/>
                                                        </emph2>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:i and w:b]">
                                                        <emph3>
                                                            <xsl:value-of select="replace(w:t, 'ô', '&#x00F4;')"/>
                                                        </emph3>
                                                    </xsl:when>
                                                    <xsl:when test="w:rPr[w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']] or w:footnoteReference">
                                                        <apnd refid="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_', w:footnoteReference/@w:id)}"/>
                                                    </xsl:when>
                                                    <xsl:otherwise>
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:otherwise>
                                                </xsl:choose>
                                            </xsl:for-each>
                                        </al>
                                    </xsl:otherwise>
                                </xsl:choose>
                            </xsl:otherwise>
                        
                        </xsl:choose>
                    </xsl:if>
                    <xsl:if test="local-name(.)='sdt'">
                        <xsl:choose>
                            <xsl:when test="w:sdtContent/w:p/w:pPr/w:pStyle[@w:val='NBBL-type']">
                                <xsl:text disable-output-escaping="yes"><![CDATA[<nb-bl titre="]]></xsl:text>
                                <!--<xsl:value-of select="if(w:ins) then w:sdtContent/w:p/w:ins/w:r/w:t else lower-case(w:sdtContent/w:p/w:r/w:t)"/>-->
                                <xsl:variable name="nbbltype" select=".//text()"/>
                                <xsl:choose>
                                    <xsl:when test="lower-case($nbbltype[1]) eq 'exemple'">
                                        <xsl:text>exemple</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="starts-with(lower-case($nbbltype[1]), 'conseil')">
                                        <xsl:text>conseilpratique</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="lower-case($nbbltype[1]) eq 'encadré'">
                                        <xsl:text>encadre</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="starts-with(lower-case($nbbltype[1]),'pour')">
                                        <xsl:text>pourallerplusloin</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="lower-case($nbbltype[1]) eq 'remarque'">
                                        <xsl:text>remarque</xsl:text>
                                    </xsl:when>
                                    <xsl:when test="lower-case($nbbltype[1]) eq 'attention'">
                                        <xsl:text>attention</xsl:text>
                                    </xsl:when>
                                </xsl:choose>
                                <xsl:text disable-output-escaping="yes"><![CDATA[">]]></xsl:text>
                                <!--<xsl:if test="not(./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='NBBL-normal']])">
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</nb-bl>]]></xsl:text>
                                </xsl:if>-->
                            </xsl:when>
                            <xsl:when test="w:sdtContent/w:p/w:pPr/w:pStyle[@w:val='Solution']">
                                <xsl:variable name="sdtval" select="w:sdtContent/w:p//text()"/>
                                <xsl:text disable-output-escaping="yes"><![CDATA[<solution type="]]></xsl:text>
                                <xsl:value-of select="if(w:ins) then w:sdtContent/w:p/w:ins/w:r/w:t else $sdtval"/>
                                <xsl:text disable-output-escaping="yes"><![CDATA["/>]]></xsl:text>
                            </xsl:when>
                        </xsl:choose>
                    </xsl:if>
                    <xsl:if test="local-name(.)='tbl'">
                        <xsl:text>&#x0A;</xsl:text>
                        <tab>
                            <xsl:variable name="table-attributes" select="w:tblPr/w:tblStyle/@w:val"/>
                            <xsl:variable name="c-count" select="count(w:tblGrid/w:gridCol)"/>
                            <xsl:variable name="r-count" select="count(w:tr)"/>
                            <xsl:variable name="t-groupstyle" select="w:tblPr/w:tblLook/number(@w:firstRow+@w:lastRow+@w:firstColumn+@w:lastColumn+@w:noHBand+@w:noVBand)"/>
                            <xsl:text>&#x0A;</xsl:text>
                            <table frame="all" pgwide="1">
                                <xsl:text>&#x0A;</xsl:text>
                                <tgroup cols="{$c-count}" tgroupstyle="{$t-groupstyle}" colsep="1" rowsep="0">
                                    <xsl:if test="w:tblGrid">
                                        <xsl:for-each select="w:tblGrid/w:gridCol">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <colspec colnum="{position()}" colname="{concat('col', position())}" colwidth="{if(@w:w &gt; '4000') then '2*' else '1*'}"/>
                                        </xsl:for-each>
                                        <xsl:for-each select="w:tblGrid/w:gridCol">
                                            <xsl:variable name="position" select="position()"/>
                                            <xsl:for-each select="$position to $c-count - 1">
                                                <xsl:text>&#x0A;</xsl:text>
                                                <spanspec namest="{concat('col', $position)}" nameend="{concat('col', $position + position())}" spanname="{concat($position,'TO', $position + position())}"/>
                                            </xsl:for-each>
                                        </xsl:for-each>
                                    </xsl:if>
                                    <xsl:choose>
                                        <xsl:when test="w:tr[1]/w:trPr/w:tblHeader">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <xsl:text disable-output-escaping="yes"><![CDATA[<thead>]]></xsl:text>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:text>&#x0A;</xsl:text>
                                            <xsl:text disable-output-escaping="yes"><![CDATA[<tbody>]]></xsl:text>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    <xsl:for-each select="w:tr">
                                        <xsl:variable name="tralign" select="w:trPr/w:jc/@w:val"/>
                                        <xsl:choose>
                                            <xsl:when test="w:trPr/w:tblHeader">
                                                <xsl:text>&#x0A;</xsl:text>
                                                <row>
                                                    <xsl:if test="parent::w:tbl/w:tblPr/w:tblBorders">
                                                        <xsl:attribute name="rowsep" select="'1'"/>
                                                    </xsl:if>
                                                    <xsl:for-each select="w:tc">
                                                        <xsl:variable name="tcalign" select="if(w:p/w:pPr/w:jc/@w:val) then w:p/w:pPr/w:jc/@w:val else 'left'"/>
                                                        <xsl:text>&#x0A;</xsl:text>
                                                        <entry align="{$tcalign}">
                                                            <xsl:for-each select="w:p">
                                                                <al>
                                                                    <xsl:for-each select="w:r">
                                                                        <xsl:choose>
                                                                            <xsl:when test="w:rPr/w:b">
                                                                                <emph1>
                                                                                    <xsl:value-of select="w:t"/>
                                                                                </emph1>
                                                                            </xsl:when>
                                                                            <xsl:otherwise>
                                                                                <xsl:value-of select="w:t"/>
                                                                            </xsl:otherwise>
                                                                        </xsl:choose>
                                                                    </xsl:for-each>
                                                                </al>
                                                            </xsl:for-each>
                                                            <!--<xsl:text>&#x0A;</xsl:text>-->
                                                        </entry>
                                                        <xsl:if test="position()=last()">
                                                            <xsl:text>&#x0A;</xsl:text>
                                                        </xsl:if>
                                                    </xsl:for-each>
                                                </row>
                                                <xsl:if test="w:trPr/w:tblHeader and not(./following-sibling::*[1][self::w:tr/w:trPr/w:tblHeader])">
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <xsl:text disable-output-escaping="yes"><![CDATA[</thead>]]></xsl:text>
                                                    <xsl:text disable-output-escaping="yes"><![CDATA[<tbody>]]></xsl:text>
                                                </xsl:if>
                                            </xsl:when>
                                            <xsl:when test="not(w:trPr/w:tblHeader)">
                                                <xsl:text>&#x0A;</xsl:text>
                                                <row>
                                                    <xsl:if test="parent::w:tbl/w:tblPr/w:tblBorders and ./preceding-sibling::w:tblPr/w:tblStyle/@w:val[not(contains(.,'reg10'))]">
                                                        <xsl:attribute name="charoff" select="'rowsep=1'"/>
                                                    </xsl:if>
                                                    <xsl:for-each select="w:tc">
                                                        <xsl:variable name="tcalign" select="if(w:p/w:pPr/w:jc/@w:val) then w:p/w:pPr/w:jc/@w:val else 'left'"/>
                                                        <xsl:text>&#x0A;</xsl:text>
                                                        <entry align="{$tcalign}">
                                                            <xsl:for-each select="w:p">
                                                                <al>
                                                                    <xsl:for-each select="w:r">
                                                                        <xsl:choose>
                                                                            <xsl:when test="w:rPr/w:b">
                                                                                <emph1>
                                                                                    <xsl:value-of select="w:t"/>
                                                                                </emph1>
                                                                            </xsl:when>
                                                                            <xsl:otherwise>
                                                                                <xsl:value-of select="w:t"/>
                                                                            </xsl:otherwise>
                                                                        </xsl:choose>
                                                                    </xsl:for-each>
                                                                </al>
                                                            </xsl:for-each>
                                                            <!--<xsl:text>&#x0A;</xsl:text>-->
                                                        </entry>
                                                    </xsl:for-each>
                                                    <xsl:text>&#x0A;</xsl:text>
                                                </row>
                                            </xsl:when>
                                        </xsl:choose>
                                    </xsl:for-each>
                                    <xsl:text>&#x0A;</xsl:text>
                                    <xsl:text disable-output-escaping="yes"><![CDATA[</tbody>]]></xsl:text>
                                    <xsl:text>&#x0A;</xsl:text>
                                </tgroup>
                            </table>
                        </tab>
                    </xsl:if>
                    <!--<xsl:if test="local-name(.)='w:tbl'">
                        <xsl:choose>
                            <xsl:when test="w:tr/w:tc/w:p/w:r/w:t[.='Chemin de fer']">
                                <a/>
                            </xsl:when>
                            <xsl:when test="w:tr/w:tc/w:p/w:r/w:t[.='Auteur']">
                                <b/>
                            </xsl:when>
                        </xsl:choose>
                    </xsl:if>-->
                </xsl:for-each>
                
                <xsl:text disable-output-escaping="yes"><![CDATA[</]]></xsl:text>
                <xsl:value-of select="substring-before(substring-after($article-name, '('), ')')"/>
                <xsl:text disable-output-escaping="yes"><![CDATA[>]]></xsl:text>
                
                <!--Footnote-->
                <xsl:if test="$footnote-file">
                    <fnotes>
                        <xsl:for-each select="$footnote-file/w:footnote[w:p/w:r/w:rPr/w:rStyle[@w:val='Appelnotedebasdep' or @w:val='FootnoteReference']]">
                            <xsl:text>&#x0A;</xsl:text>
                            <fnote id="{concat('f', $file-first-name, $get-year, $article-num, $article-name1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article, '00000') else concat('_', format-number(position(), '00000')), '_',@w:id)}">
                                <al>
                                   <xsl:for-each select="w:p//w:r">
                                       <xsl:choose>
                                           <xsl:when test="w:rPr[w:i]">
                                               <emph2>
                                                   <xsl:value-of select="w:t"/>
                                               </emph2>
                                           </xsl:when>
                                           <xsl:otherwise>
                                               <xsl:value-of select="w:t"/>
                                           </xsl:otherwise>
                                       </xsl:choose>
                                   </xsl:for-each>
                                </al>
                            </fnote>
                        </xsl:for-each>
                    </fnotes>
                </xsl:if>
            </article>
        </xsl:result-document>
        </xsl:for-each-group>

         <!--For meta information-->
        <xsl:variable name="file-first-name_meta">
            <!--<xsl:for-each select="w:tbl[1]/w:tr[2]/w:tc[2]/w:p//w:r">
                <xsl:value-of select="substring-before(w:t, '_')"/>
            </xsl:for-each>-->
            <xsl:variable name="filetxt">
                <xsl:value-of select="w:tbl[1]/w:tr[2]/w:tc[2]/w:p//text()"/>
            </xsl:variable>
            <xsl:value-of select="substring-before($filetxt, '_')"/>
        </xsl:variable>
        <!--<xsl:variable name="get-year-meta">
            <xsl:for-each select="w:tbl[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">
                <!-\-<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-\->
                <xsl:value-of select="if(contains(w:t, '-')) then concat('20', format-number(number(substring-before(w:t, '-')), '00')) else concat('20', format-number(number(w:t), '00'))"/>
            </xsl:for-each>
        </xsl:variable>-->
        
        <xsl:variable name="get-year-meta">
            <!--<xsl:for-each select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p//w:r[position()= last()]">
                <!-\-<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-\->
                <xsl:value-of select="if(matches(w:t, '[0-9]{4}')) then replace(w:t, '(.*)[0-9]{2}([0-9]{2})', '$2') else '20'"/>
            </xsl:for-each>-->
            <xsl:variable name="yeartext">
                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p//text()"/>
            </xsl:variable>
            
            <xsl:value-of select="if(matches($yeartext, '[0-9]{4}')) then replace($yeartext, '(.*)[0-9]{2}([0-9]{2})', '$2') else '20'"/>
        </xsl:variable>
        <xsl:variable name="article-num-meta">
            <!--<xsl:for-each select="w:tbl[1]/w:tr[4]/w:tc[2]/w:p//w:r[normalize-space(w:t) and last()]">
                <!-\-<xsl:value-of select="replace(w:t, '(.+?)([0-9]{4})', '$2')"/>-\->
                <xsl:value-of select="if(contains(w:t, '-')) then format-number(number(substring-before(w:t, '-')), '00') else format-number(number(w:t), '00')"/>
            </xsl:for-each>-->
            <xsl:variable name="artnum">
                <xsl:value-of select="w:tbl[1]/w:tr[4]/w:tc[2]/w:p//text()"/>
            </xsl:variable>
            <xsl:value-of select="if(contains($artnum, '-')) then format-number(number(substring-before($artnum, '-')), '00') else format-number(number($artnum), '00')"/>
        </xsl:variable>
        
        <xsl:variable name="article-name_meta">
            <xsl:for-each select="w:tbl[1]/w:tr[5]/w:tc[2]//w:p/w:r">
                <xsl:value-of select="w:t"/>
            </xsl:for-each>
        </xsl:variable>
        <xsl:variable name="article-name_meta1">
            <!--<xsl:for-each select="w:tbl[1]/w:tr[5]/w:tc[2]//w:p/w:r">-->
            <xsl:variable name="artext"><xsl:value-of select="w:tbl[1]/w:tr[5]/w:tc[2]//w:p//text()"/></xsl:variable>
            <xsl:variable name="article-type" select="substring-before($artext, ' ')"/>
            <xsl:variable name="article" select="substring-before(substring-after($artext, '('), ')')"/>
                <xsl:choose>
                    <xsl:when test="$article='couverture'">cv</xsl:when>
                    <!--<xsl:when test="$article='texte'">te</xsl:when>-->
                    <xsl:when test="$article='texte'">tx</xsl:when>
                    <xsl:when test="$article='panojuri'">pj</xsl:when>
                    <xsl:when test="$article='essentiel'">es</xsl:when>
                    <xsl:when test="$article='repere'">re</xsl:when>
                    <xsl:when test="$article='comment'">cm</xsl:when>
                    <xsl:when test="$article='alerte'">al</xsl:when>
                    <xsl:when test="$article='falerte'">fa</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='entretien'">en</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='tableronde'">en</xsl:when>
                    <xsl:when test="$article='etude' and $article-type='etude'">et</xsl:when>
                    <xsl:when test="$article='artdossier'">do</xsl:when>
                    <!--<xsl:when test="$article='dossier' and $article-type='etude'">ed</xsl:when>-->
                    <xsl:when test="$article='etude' and not($article-type='tableronde' or $article-type='entretien' or $article-type='etude')">ed</xsl:when>
                    <xsl:when test="$article='chronique'">ch</xsl:when>
                    <xsl:when test="$article='commentaire'">cm</xsl:when>
                    <xsl:when test="$article='formrfn'">fo</xsl:when>
                    <xsl:when test="$article='fiprat'">fp</xsl:when>
                    <xsl:when test="$article='indices'">in</xsl:when>
                    <xsl:when test="$article='apercu' and $article-type='propos'">lp</xsl:when>
                    <xsl:when test="$article='apercu' and not($article-type='propos')">ap</xsl:when>
                    <xsl:when test="$article='apercu'">ap</xsl:when>
                    <xsl:when test="$article='propos'">lp</xsl:when>
                    <xsl:when test="$article='tables'">ta(2)</xsl:when>
                    <xsl:when test="$article='echeancier'">ec(2)</xsl:when>
                </xsl:choose>
            <!--</xsl:for-each>-->
        </xsl:variable>
        
        <!--<xsl:variable name="article-type_meta" select="substring-before(substring-after($article-name, 'article'), ' (')"/>-->
        <xsl:result-document href="{concat($get-path, $file-first-name_meta, $get-year-meta, $article-num-meta, $article-name_meta1, '_metadata')}.xml">
            <revue>
                <xsl:text>&#x0A;</xsl:text>
                <meta>
                    <xsl:attribute name="articletype"><xsl:value-of select="$article-name_meta1"/></xsl:attribute>
                    <xsl:text>&#x0A;</xsl:text>
                    <typerevue>
                        <!--<xsl:value-of select="w:tbl[1]/w:tr[2]/w:tc[2]/w:p//w:r/substring-before(w:t, '_')"/>-->
                        <xsl:value-of select="$file-first-name_meta"/>
                    </typerevue>
                    <xsl:text>&#x0A;</xsl:text>
                    <parution>
                        <xsl:text>&#x0A;</xsl:text>
                        <numero>
                            <xsl:value-of select="w:tbl[1]/w:tr[4]/w:tc[2]/w:p/w:r/w:t"/>
                        </numero>
                        <daterevue>
                            <xsl:variable name="datetext">
                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p//text()"/>
                            </xsl:variable>
                            <xsl:if test="matches($datetext, '^[0-9]+')">
                                <jour>
                                <xsl:value-of select="normalize-space(replace($datetext, '^([0-9]{1,2})(.*)', '$1'))"/>
                                </jour>
                            </xsl:if>
                            <mois>
                                <xsl:value-of select="normalize-space(replace($datetext, '[0-9]*(.*)([0-9]{4})', '$1'))"/>
                            </mois>
                            <annee>
                                <xsl:value-of select="if(matches($datetext, '[0-9]{4}')) then replace($datetext, '(.*)([0-9]{4})', '$2') else '20'"/>
                            </annee>
                            
                            <!--<xsl:choose>
                                <xsl:when test="count(w:tbl[1]/w:tr[3]/w:tc[2]/w:p//w:r)=2">
                                    <mois>
                                        <xsl:choose>
                                            <xsl:when test="not(starts-with(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')) and contains(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')">
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[1]/w:t"/><xsl:value-of select="substring-before(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[1]/w:t"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                        
                                    </mois>
                                    <annee>
                                        <xsl:choose>
                                            <xsl:when test="not(starts-with(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')) and contains(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')">
                                                <xsl:value-of select="substring-after(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, ' ')"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/normalize-space(w:t)"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                    </annee>
                                </xsl:when>
                                <xsl:when test="count(w:tbl[1]/w:tr[3]/w:tc[2]/w:p//w:r)=3">
                                    <xsl:choose>
                                        <xsl:when test="matches(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, '-') and not(starts-with(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t/text(), '-'))">
                                            <mois>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t"/>
                                            </mois>
                                        </xsl:when>
                                        <xsl:when test="starts-with(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t/text(), '-')">
                                            <mois>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[1]/normalize-space(w:t)"/>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/normalize-space(w:t)"/>
                                            </mois>
                                        </xsl:when>
                                        <xsl:when test="starts-with(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t/text(), '&#x00A0;')">
                                            <mois>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[3]/substring-before(w:t, ' ')"/>
                                            </mois>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <mois>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t"/>
                                            </mois>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    <xsl:choose>
                                        <xsl:when test="matches(w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[2]/w:t, '&#x00A0;')">
                                            <annee>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[3]/substring-after(w:t, ' ')"/>
                                            </annee>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <annee>
                                                <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r[3]/normalize-space(w:t)"/>
                                            </annee>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    
                                </xsl:when>
                                <xsl:otherwise>
                                    <mois>
                                        <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r/substring-before(w:t, ' ')"/>
                                    </mois>
                                    <annee>
                                        <xsl:value-of select="w:tbl[1]/w:tr[3]/w:tc[2]/w:p/w:r/substring-after(w:t, ' ')"/>
                                    </annee>
                                </xsl:otherwise>
                            </xsl:choose>-->
                        </daterevue>
                        <xsl:text>&#x0A;</xsl:text>
                    </parution>
                </meta>
                <chfer>
                <xsl:for-each-group select="*" group-starting-with="w:tbl[w:tr/w:tc/w:p/w:r/w:t[.='Métadonnées']]">
                    <xsl:variable name="artext"><xsl:value-of select="current-group()/w:tr[5]/w:tc[2]//w:p//text()"/></xsl:variable>
                    <xsl:variable name="article-type" select="substring-before($artext, ' ')"/>
                    <xsl:variable name="article" select="substring-before(substring-after($artext, '('), ')')"/>
                    <xsl:variable name="article-name_meta1">
                        <!--<xsl:for-each select="current-group()/w:tr[5]/w:tc[2]//w:p/w:r">-->
                        
                        <xsl:choose>
                            <xsl:when test="$article='couverture'">cv</xsl:when>
                            <!--<xsl:when test="$article='texte'">te</xsl:when>-->
                            <xsl:when test="$article='texte'">tx</xsl:when>
                            <xsl:when test="$article='panojuri'">pj</xsl:when>
                            <xsl:when test="$article='essentiel'">es</xsl:when>
                            <xsl:when test="$article='repere'">re</xsl:when>
                            <xsl:when test="$article='comment'">cm</xsl:when>
                            <xsl:when test="$article='alerte'">al</xsl:when>
                            <xsl:when test="$article='falerte'">fa</xsl:when>
                            <xsl:when test="$article='etude' and $article-type='entretien'">en</xsl:when>
                            <xsl:when test="$article='etude' and $article-type='tableronde'">en</xsl:when>
                            <xsl:when test="$article='etude' and $article-type='etude'">et</xsl:when>
                            <xsl:when test="$article='artdossier'">do</xsl:when>
                            <xsl:when test="$article='etude' and not($article-type='tableronde' or $article-type='entretien' or $article-type='etude')">ed</xsl:when>
                            <xsl:when test="$article='chronique'">ch</xsl:when>
                            <xsl:when test="$article='commentaire'">cm</xsl:when>
                            <xsl:when test="$article='formrfn'">fo</xsl:when>
                            <xsl:when test="$article='fiprat'">fp</xsl:when>
                            <xsl:when test="$article='indices'">in</xsl:when>
                            <xsl:when test="$article='apercu' and $article-type='propos'">lp</xsl:when>
                            <xsl:when test="$article='apercu' and not($article-type='propos')">ap</xsl:when>
                            <xsl:when test="$article='propos'">lp</xsl:when>
                            <xsl:when test="$article='tables'">ta(2)</xsl:when>
                            <xsl:when test="$article='echeancier'">ec(2)</xsl:when>
                        </xsl:choose>
                        <!--</xsl:for-each>-->
                    </xsl:variable>
                    <xsl:variable name="num-article-meta">
                        <xsl:for-each select="current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]/w:r">
                            <xsl:choose>
                                <xsl:when test="matches(w:t, '^ivxlcdm|ii[^i]|iiii+|xx+|cccc+|v[^i]|[^i]?i[vx][ivxlcdm]|[^i]?i[^vix]')">
                                    <xsl:value-of select="num:roman(., 0)"/>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="w:t"/>
                                </xsl:otherwise>
                            </xsl:choose>
                            <!--<xsl:value-of select="w:t"/>-->
                        </xsl:for-each>
                    </xsl:variable>
                    <xsl:variable name="maintitle">
                        <xsl:for-each select="current-group()[self::w:p[w:pPr[w:pStyle[@w:val='Title' or @w:val='Titre']]]][preceding-sibling::*[1][self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]]/w:r">
                            <xsl:value-of select="w:t"/>
                        </xsl:for-each>
                    </xsl:variable>
                        
                    
                    <xsl:choose>
                        <xsl:when test="$article-type='anoteregalement'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<danoter>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='etude' and $article-type='entretien'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<pentretien>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='etude' and $article-type='etude'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<petude>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='comment'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<pcomment>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='chronique'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<pchronique>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='fiprat'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<pfiprat>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='repere'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<prepere>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='alerte'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<palerte>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='texte'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<ptexte>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='artdossier'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[<pdossier>]]></xsl:text>
                        </xsl:when>
                    </xsl:choose>
                    <!--<xsl:element name="maintitle"><xsl:value-of select="$maintitle[1]"/></xsl:element>-->
                    <xsl:for-each select="current-group()[self::w:p]">
                        <xsl:if test="local-name()='p'">
                            <xsl:choose>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-Partie']">
                                    <xsl:text>&#x0A;</xsl:text>
                                    <palte>
                                        <xsl:variable name="cdfpart" select=".//text()"/>
                                        <!--<xsl:value-of select="concat(substring($cdfpart, 1,1), lower-case(substring($cdfpart, 2)))"/>-->
                                        <xsl:call-template name="CamelCase">
                                            <xsl:with-param name="text"><xsl:value-of select="$cdfpart"/></xsl:with-param>
                                        </xsl:call-template>
                                     </palte>
                                </xsl:when>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D1Part']">
                                    <xsl:choose>
                                        <xsl:when test="current-group()//w:pPr/w:pStyle[@w:val='cdf-Partie']">
                                            <xsl:choose>
                                                <xsl:when test="count(//w:r)=1">
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <h1><xsl:value-of select="concat(substring(w:r/w:t, 1,1), lower-case(substring(w:r/w:t, 2)))"/></h1>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <h1>
                                                        <xsl:for-each select="w:r">
                                                            <xsl:value-of select="w:t"/>
                                                        </xsl:for-each>
                                                    </h1>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:choose>
                                                <xsl:when test="count(//w:r)=1">
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <h1><xsl:value-of select="concat(substring(w:r/w:t, 1,1), lower-case(substring(w:r/w:t, 2)))"/></h1>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <h1>
                                                        <xsl:for-each select="w:r">
                                                            <xsl:value-of select="w:t"/>
                                                        </xsl:for-each>
                                                    </h1>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                    
                                </xsl:when>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='Title' or @w:val='Titre']">
                                    <xsl:choose>
                                        <xsl:when test="count(//w:r)=1">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <maintitle><xsl:value-of select="concat(substring(w:r/w:t, 1,1), lower-case(substring(w:r/w:t, 2)))"/></maintitle>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <maintitle>
                                                <xsl:for-each select="w:r">
                                                    <xsl:value-of select="w:t"/>
                                                </xsl:for-each>
                                            </maintitle>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                </xsl:when>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='AuteurprnomNOM'] and not(./following-sibling::*[1][self::w:p/w:pPr/w:pStyle[@w:val='Mots-Cles']]) and position()!=last()">
                                    <!--<xsl:for-each select="w:r">-->
                                    <auteur>
                                    <prenom>
                                        <xsl:variable name="prenom" select="self::w:p//text()"/>
                                        <xsl:value-of select="$prenom"/>
                                        <!--<xsl:choose>
                                            <xsl:when test="not(w:r[1]/w:t)">
                                                <xsl:value-of select="substring-before(w:r[2]/w:t, ' ')"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="substring-before(w:r[1]/w:t, ' ')"/>
                                            </xsl:otherwise>
                                        </xsl:choose>-->
                                    </prenom>
                                    </auteur>
                                    <!--<nom>
                                        <xsl:choose>
                                            <xsl:when test="not(w:r[1]/w:t)">
                                                <xsl:value-of select="lower-case(substring-after(w:r[2]/normalize-space(w:t), ' '))"/>
                                            </xsl:when>
                                            <xsl:otherwise>
                                                <xsl:value-of select="lower-case(substring-after(w:r[1]/normalize-space(w:t), ' '))"/>
                                            </xsl:otherwise>
                                        </xsl:choose>
                                        <xsl:if test="not(w:r[3])">
                                            <xsl:value-of select="w:r[2]/w:t"/>
                                        </xsl:if>
                                        <xsl:if test="w:r[3]">
                                        <xsl:value-of select="concat(' ', w:r[3]/w:t)"/>
                                        </xsl:if>
                                    </nom>-->
                                    <!--</xsl:for-each>-->
                                </xsl:when>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='Auteurqualit']">
                                    <xsl:text>&#x0A;</xsl:text>
                                    <qual>
                                        <xsl:for-each select="w:r">
                                            <xsl:value-of select="w:t"/>
                                        </xsl:for-each>
                                    </qual>
                                </xsl:when>
                                <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D2Part']">
                                    <xsl:choose>
                                        <xsl:when test="current-group()//w:pPr/w:pStyle[@w:val='cdf-Partie']">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <xsl:if test="w:r">
                                                <h2>
                                                    <xsl:for-each select="w:r">
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:for-each>
                                                </h2>
                                            </xsl:if>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:choose>
                                                <xsl:when test="current-group()//w:pPr/w:pStyle[@w:val='cdf-D1Part']">
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <xsl:if test="w:r">
                                                        <h2>
                                                            <xsl:for-each select="w:r">
                                                                <xsl:value-of select="w:t"/>
                                                            </xsl:for-each>
                                                        </h2>
                                                    </xsl:if>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <xsl:if test="w:r">
                                                        <h2>
                                                            <xsl:for-each select="w:r">
                                                                <xsl:value-of select="w:t"/>
                                                            </xsl:for-each>
                                                        </h2>
                                                    </xsl:if>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                </xsl:when>
                                
                                <xsl:when test="w:pPr/w:pStyle[@w:val='cdf-D3Part']">
                                    <xsl:choose>
                                        <xsl:when test="current-group()//w:pPr/w:pStyle[@w:val='cdf-Partie']">
                                            <xsl:text>&#x0A;</xsl:text>
                                            <xsl:if test="w:r">
                                                <h3>
                                                    <xsl:for-each select="w:r">
                                                        <xsl:value-of select="w:t"/>
                                                    </xsl:for-each>
                                                </h3>
                                            </xsl:if>
                                        </xsl:when>
                                        <xsl:otherwise>
                                            <xsl:choose>
                                                <xsl:when test="current-group()//w:pPr/w:pStyle[@w:val='cdf-D2Part']">
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <xsl:if test="w:r">
                                                        <h3>
                                                            <xsl:for-each select="w:r">
                                                                <xsl:value-of select="w:t"/>
                                                            </xsl:for-each>
                                                        </h3>
                                                    </xsl:if>
                                                </xsl:when>
                                                <xsl:otherwise>
                                                    <xsl:text>&#x0A;</xsl:text>
                                                    <xsl:if test="w:r">
                                                        <h3>
                                                            <xsl:for-each select="w:r">
                                                                <xsl:value-of select="w:t"/>
                                                            </xsl:for-each>
                                                        </h3>
                                                    </xsl:if>
                                                </xsl:otherwise>
                                            </xsl:choose>
                                        </xsl:otherwise>
                                    </xsl:choose>
                                </xsl:when>
                                <!--<xsl:when test="w:pPr/w:pStyle[@w:val='NumArticle']">
                                    <xsl:text>&#x0A;</xsl:text>
                                    <xsl:if test="w:r">
                                        <refart fic="{concat($file-first-name_meta, $get-year-meta, $article-num-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else concat('_', format-number(position(), '00')))}.xml" id="{concat($file-first-name_meta, $get-year-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else concat('_', format-number(position(), '00')))}"/>
                                    </xsl:if>
                                </xsl:when>-->
                                <!--<xsl:when test="w:pPr/w:pStyle[@w:val='NumArticle']">
                                    <xsl:text>&#x0A;</xsl:text>
                                    <xsl:if test="w:r">
                                        <refart fic="{concat($file-first-name_meta, $get-year-meta, $article-num-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}.xml" id="{concat($file-first-name_meta, $get-year-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}"/>
                                    </xsl:if>
                                </xsl:when>-->
                            </xsl:choose>
                            <!--<refart fic="{concat($file-first-name_meta, $get-year-meta, $article-num-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}.xml" id="{concat($file-first-name_meta, $get-year-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}"/>-->
                        </xsl:if>
                    </xsl:for-each>
                    <refart fic="{concat($file-first-name_meta, $get-year-meta, $article-num-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}.xml" id="{concat($file-first-name_meta, $get-year-meta, $article-name_meta1, if(current-group()[self::w:p[w:pPr[w:pStyle[@w:val='NumArticle']]]]) then format-number($num-article-meta, '00000') else format-number(position() - $countart, '00000'))}"/>
                    <xsl:choose>
                        <xsl:when test="$article-type='anoteregalement'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</danoter>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='etude' and $article-type='entretien'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</pentretien>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='etude' and $article-type='etude'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</petude>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='comment'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</pcomment>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='chronique'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</pchronique>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='fiprat'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</pfiprat>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='repere'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</prepere>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='alerte'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</palerte>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='texte'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</ptexte>]]></xsl:text>
                        </xsl:when>
                        <xsl:when test="$article='artdossier'">
                            <xsl:text disable-output-escaping="yes"><![CDATA[</pdossier>]]></xsl:text>
                        </xsl:when>
                    </xsl:choose>
                </xsl:for-each-group>
                </chfer>
            </revue>
        </xsl:result-document>
    </xsl:template>
    
    <xsl:template name="CamelCase">
        <xsl:param name="text"/>
        <!--<xsl:choose>
            <xsl:when test="contains($text,' ')">
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="substring-before($text,' ')"/>
                </xsl:call-template>
                <xsl:text> </xsl:text>
                <xsl:variable name="afttext" select="substring-after($text,' ')"/>
                <xsl:choose>
                    <xsl:when test="contains($afttext, '’')">
                        <xsl:call-template name="CamelCase">
                            <xsl:with-param name="text" select="substring-before($afttext,'’')"/>
                        </xsl:call-template>
                        <xsl:text>’</xsl:text>
                        <xsl:call-template name="CamelCase">
                            <xsl:with-param name="text" select="substring-after($afttext,'’')"/>
                        </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:call-template name="CamelCase">
                            <xsl:with-param name="text" select="substring-after($text,' ')"/>
                        </xsl:call-template>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:when>
            <xsl:otherwise>-->
                <xsl:call-template name="CamelCaseWord">
                    <xsl:with-param name="text" select="$text"/>
                </xsl:call-template>
            <!--</xsl:otherwise>
        </xsl:choose>-->
    </xsl:template>
    
    <xsl:template name="CamelCaseWord">
        <xsl:param name="text"/>
        <xsl:value-of select="translate(substring($text,1,1),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" /><xsl:value-of select="translate(substring($text,2,string-length($text)-1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" />
    </xsl:template>
    
</xsl:stylesheet>