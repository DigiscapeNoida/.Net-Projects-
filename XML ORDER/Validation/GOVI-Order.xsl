<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!--
    Wiley Journal XML Order Stylesheet version 1.0
-->

    <!--################################################################################-->
    <xsl:template match="/">
        <xsl:apply-templates select="orders"/>
    </xsl:template>

    <!--################################################################################-->
    <xsl:template match="orders">
        <html>
            <head>
                <script>
function expColl(dF,statusObj,object)
 {
 if (statusObj.status == 1)
  { //1 is open, so close now
  dF.appendChild(object.parentNode.removeChild(object.parentNode.lastChild));
  object.removeChild(object.lastChild); //remove minus !!!second takes away plus of first!?
  object.appendChild(document.createTextNode('+')); //put + in its place
  statusObj.status = 0;
  }
 else
  { //open
  object.removeChild(object.lastChild); //remove plus
  object.appendChild(document.createTextNode('-')); //put minus in its place
  object.parentNode.appendChild(dF);
  statusObj.status = 1;
  }
 }
var part0 = document.createDocumentFragment();
var expanded0 = {status:1};
</script>
            </head>

            <body style="font-family:'Arial Unicode MS'" onload="expColl(part0,expanded0,document.getElementById('xmlcode'))">
                <xsl:for-each select="order">
                  <p align="right">
                    <font size="16pt">
                    <xsl:value-of select="//jid"/>
                      <xsl:text>   </xsl:text>
                  </font>
                  </p>
                  <p align="right">

                        <font size="5pt"  color="voilet"><xsl:value-of select="//aid"/>
                          <xsl:text>(</xsl:text>
                          <xsl:value-of select="//doi"/>
                          <xsl:text>)</xsl:text>
                        </font>
                  </p>
                  <table frame="all" border="1">
                    <tr>
                      <td valign="top">
                        <h2>
                          <font color="darkblue">
                            <b>Remarks:</b>
                          </font>
                        </h2>
                      </td>
                      <td valign="top">
                          <font color="gray">
                            <h2>
                              <xsl:apply-templates select="item-info/item-remarks/item-remark/remark"/>
                            </h2>
                          </font>
                      </td>
                    </tr>
                  </table>
                  <table width="100%" frame="all" border="1">
                    <tr>
                      <td width="60%">
                        <table width="100%">
                          <tr>
                            <td width="50%">
                              <b>Order Creation Time:</b>
                            </td>
                            <td width="50%">
                              <xsl:apply-templates select="time"/>
                            </td>
                          </tr>
                          <tr>
                            <td width="50%">
                              <b>JID:</b>
                            </td>
                            <td width="50%">
                              <xsl:apply-templates select="item-info/jid"/>
                            </td>
                          </tr>
                          <tr>
                            <td width="50%">
                              <b>AID:</b>
                            </td>
                            <td width="50%">
                              <xsl:apply-templates select="item-info/aid"/>
                            </td>
                          </tr>
                          <tr>
                            <td width="50%">
                              <b>DOI:</b>
                            </td>
                            <td width="50%">
                              <xsl:apply-templates select="item-info/doi"/>
                            </td>
                          </tr>
                          <tr>
                            <td width="50%">
                              <b>Production Site:</b>
                            </td>
                            <td width="50%">
                              <xsl:value-of select="prod-site"/>
                            </td>
                          </tr>
                        </table>
                        <table width="100%">
                          <tr>
                            <td>
                              <table width="100%" height="100%">
                                <tr>
                                  <td width="50%">
                                    <b>MSS:</b>
                                  </td>
                                  <td width="50%">
                                    <xsl:value-of select="//no-mns-pages"/>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <b>Date received:</b>
                                  </td>
                                  <td>
                                    <xsl:for-each select="//received-date">
                                      <xsl:apply-templates select="date"/>
                                    </xsl:for-each>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <b>Due Date:</b>
                                  </td>
                                  <td>
                                    <xsl:for-each select="//due-date">
                                      <xsl:apply-templates select="date"/>
                                    </xsl:for-each>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <b>Article Type:</b>
                                  </td>
                                  <td>
                                    <xsl:value-of select="//arttype"/>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <b>PIT:</b>
                                  </td>
                                  <td>
                                    <xsl:value-of select="//pit"/>
                                  </td>
                                </tr>
                              </table>
                      </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>

    <!-- create pretty print -->
    <xsl:template match="*" mode="xml-source">
        <xsl:variable name="anc" select="count(ancestor::*) div 2"/>
        <div>
            <xsl:attribute name="style">
                <xsl:value-of select="concat('position: relative; margin-left:', $anc ,'%;', 'overflow: scroll;')"/>
            </xsl:attribute>
            <span style="color:red">
                <xsl:text>&lt;</xsl:text>
                <xsl:value-of select="name()"/>
                <xsl:for-each select="@*">
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="name()"/>
                    <xsl:text>="</xsl:text>
                    <xsl:value-of select="."/>
                    <xsl:text>"</xsl:text>
                </xsl:for-each>
                <xsl:text>&gt;</xsl:text>
            </span>
            <xsl:apply-templates mode="xml-source"/>
            <span style="color:red">
                <xsl:text>&lt;/</xsl:text>
                <xsl:value-of select="name()"/>
                <xsl:text>&gt;</xsl:text>
            </span>
        </div>
    </xsl:template>


    <!--################################################################################-->
    <!--This section is for formatting the corr-author name-->
    <xsl:template match="corr-author">
        <xsl:value-of select="degree"/>
        <xsl:text/>
        <xsl:value-of select="fnm"/>
        <xsl:text/>
        <xsl:value-of select="snm"/>
        <tr>
            <td>
                <xsl:apply-templates select="./aff"/>
            </td>
        </tr>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the corr-author address-->
    <xsl:template match="aff">
        <tr>
            <td/>
            <td>
                <xsl:value-of select="organization"/>
            </td>
        </tr>
        <tr>
            <td/>
            <td>
                <xsl:value-of select="inst-contd"/>
            </td>
        </tr>
        <tr>
            <td/>
            <td>
                <xsl:value-of select="institute"/>
            </td>
        </tr>
        <tr>
            <td/>
            <td>
                <xsl:value-of select="address"/>
            </td>
        </tr>
        <tr>
            <td/>
            <td>
                <xsl:value-of select="address-contd"/>
            </td>
        </tr>
        <xsl:choose>
            <xsl:when test="zipcode/@zipcode-pos='NONE'">
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cty"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cny"/>
                    </td>
                </tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='BEFORECTY'">
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="zipcode"/>
                        <xsl:text/>
                        <xsl:value-of select="cty"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cny"/>
                    </td>
                </tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='AFTERCTY'">
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cty"/>
                        <xsl:text/>
                        <xsl:value-of select="zipcode"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cny"/>
                    </td>
                </tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='BEFORECNY'">
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cty"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="zipcode"/>
                        <xsl:text/>
                        <xsl:value-of select="cny"/>
                    </td>
                </tr>
            </xsl:when>
            <xsl:when test="zipcode/@zipcode-pos='AFTERCNY'">
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cty"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cny"/>
                        <xsl:text/>
                        <xsl:value-of select="zipcode"/>
                    </td>
                </tr>
            </xsl:when>
            <xsl:otherwise>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cty"/>
                        <xsl:text/>
                        <xsl:value-of select="zipcode"/>
                    </td>
                </tr>
                <tr>
                    <td/>
                    <td>
                        <xsl:value-of select="cny"/>
                    </td>
                </tr>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the item-remarks field-->
    <xsl:template match="item-remarks">
        <xsl:for-each select="item-remark">
            <tr valign="top">
                <td width="10%">Remark:</td>
                <td width="40%">
                    <xsl:value-of select="remark"/>
                </td>
                <td width="10%">Response:</td>
                <td width="40%">
                    <xsl:value-of select="response"/>
                </td>
            </tr>
        </xsl:for-each>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the issue-remarks field-->
    <xsl:template match="issue-remarks">
        <xsl:for-each select="issue-remark">
          <b><xsl:text>Remark: </xsl:text>
          </b>
            <xsl:value-of select="./remark"/>
            <br/>
          <b><xsl:text>Response: </xsl:text>
          </b>
            <xsl:value-of select="./response"/>
        </xsl:for-each>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting any date element so it returns as (eg) 12-Dec-2003 -->
    <xsl:template match="date">
        <xsl:if test="@day!=''">
            <xsl:value-of select="@day"/>-<xsl:if test="@month='01'">Jan-</xsl:if>
            <xsl:if test="@month='02'">Feb-</xsl:if>
            <xsl:if test="@month='03'">Mar-</xsl:if>
            <xsl:if test="@month='04'">Apr-</xsl:if>
            <xsl:if test="@month='05'">May-</xsl:if>
            <xsl:if test="@month='06'">Jun-</xsl:if>
            <xsl:if test="@month='07'">Jul-</xsl:if>
            <xsl:if test="@month='08'">Aug-</xsl:if>
            <xsl:if test="@month='09'">Sep-</xsl:if>
            <xsl:if test="@month='10'">Oct-</xsl:if>
            <xsl:if test="@month='11'">Nov-</xsl:if>
            <xsl:if test="@month='12'">Dec-</xsl:if>
            <xsl:value-of select="@yr"/>
        </xsl:if>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting any date element so it returns as (eg) 12-Dec-2003 -->
    <xsl:template match="time">
        <xsl:if test="@day!=''">
            <xsl:value-of select="@day"/>-<xsl:if test="@month='01'">Jan-</xsl:if>
            <xsl:if test="@month='02'">Feb-</xsl:if>
            <xsl:if test="@month='03'">Mar-</xsl:if>
            <xsl:if test="@month='04'">Apr-</xsl:if>
            <xsl:if test="@month='05'">May-</xsl:if>
            <xsl:if test="@month='06'">Jun-</xsl:if>
            <xsl:if test="@month='07'">Jul-</xsl:if>
            <xsl:if test="@month='08'">Aug-</xsl:if>
            <xsl:if test="@month='09'">Sep-</xsl:if>
            <xsl:if test="@month='10'">Oct-</xsl:if>
            <xsl:if test="@month='11'">Nov-</xsl:if>
            <xsl:if test="@month='12'">Dec-</xsl:if>
            <xsl:value-of select="@yr"/>
        </xsl:if>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the member-ids field-->
    <xsl:template match="batch-member"> Present </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the refers-to pii field-->
    <xsl:template match="refers-to-document/pii">
        <tr>
            <td>
                <xsl:value-of select="//refers-to-document[1]/pii"/>
            </td>
        </tr>
    </xsl:template>
    <!--################################################################################-->
    <!--This section is for formatting the refers-to-doi field-->
    <xsl:template match="refers-to-doi">
        <xsl:apply-templates select="doi"/>
    </xsl:template>
    <!--################################################################################-->
    <!--This section is for formatting the refers-to-doi/doi field-->

    <xsl:template match="refers-to-doi/doi">
        <td>Refers_to DOI:</td>
        <td>
            <xsl:value-of select="../doi"/>
        </td>
    </xsl:template>

    <!--################################################################################-->
    <!--This section is for formatting the e-components field-->
    <xsl:template match="e-components">
        <hr/>E-COMPONENTS: <table width="100%">
            <tr>
                <td colspan="5"/>
            </tr>
            <xsl:for-each select="e-component">
                <tr>
                    <td width="15%">
                        <xsl:value-of select="e-component-nr"/>
                    </td>
                    <td width="15%">
                        <xsl:value-of select="e-component-format"/>
                    </td>
                    <td width="70%">
                        <xsl:value-of select="e-component-remarks"/>
                    </td>
                </tr>
            </xsl:for-each>
        </table>
    </xsl:template>

    <!--#####################################################################################-->
    <!-- This section is for formatting the grant-number field-->
    <xsl:template match="funding">
        <tr>
            <td>Funded By:</td>
            <td>
                <xsl:value-of select="funded-by"/>
            </td>
        </tr>
        <tr>
            <td>Grant number(s):</td>
            <xsl:for-each select="//grant-number">
                <td>
                    <xsl:value-of select="."/>;</td>
            </xsl:for-each>
        </tr>
        <table width="100%">
            <tr>
                <td colspan="8"/>
            </tr>
            <td width="25%">Principal-Investigator(s):</td>
            <xsl:for-each select="//principal-investigator">
                <tr>
                    <td>
                        <xsl:value-of select="fnm"/>
                        <xsl:text/>
                        <xsl:value-of select="snm"/>
                        <td>
                            <xsl:value-of select="aff/ead"/>
                        </td>
                    </td>
                </tr>
            </xsl:for-each>
        </table>
    </xsl:template>

</xsl:stylesheet>
