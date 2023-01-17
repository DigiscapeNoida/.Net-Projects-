<%@ Page MasterPageFile="~/MasterPage.master" EnableEventValidation ="false" Language="C#" AutoEventWireup="true" CodeFile="JSSView.aspx.cs" Inherits="JSSView" Theme="MyTheme"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="border-style: none; padding: 0px; width: 100%; height: 350px; margin: 0px; ">
        <tr align ="center">
             <td valign="top" style="width:15%" rowspan="3" >
                 <table style="width:100%; height: 434px;">
                   <tr>
                       <td class="tdLHead" style="background-image:url('image/menu.png'); width: 100%;height: 25px;" 
                           align="center">
                            Features
                        </td>
                  </tr>
<!--                   <tr>
                        <td class="tdLHead" style="background-image:url('image/menu.png'); width: 100%; height: 25px;" >
                            Features
                        </td>
                   </tr>-->
                   <tr>
                        <td style="background-image:url('image/menu.png'); width: 100%; height: 25px;">
                            <asp:LinkButton ID="HomeLinkButton" style="text-decoration:none" runat="server" 
                                onclick="HomeLinkButton_Click">Home</asp:LinkButton>
                        </td>
                    </tr>
                   <tr>
                        <td style="background-image:url('image/menu.png'); width: 100%; height: 25px;">
                            <asp:LinkButton ID="JurnalLst" style="text-decoration:none" runat="server" 
                             onclick="JurnalLst_Click">List of journals</asp:LinkButton>
                        </td>
                    </tr>
                   <tr>
                        <td style="background-image:url('image/menu.png'); width: 100%; height: 25px;">
                            <asp:LinkButton ID="Upld" style="text-decoration:none" runat="server" 
                                onclick="Upld_Click">Upload Stylesheet</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-image:url('image/menu.png'); width: 100%; height: 25px;">
                            <asp:LinkButton ID="UpdtPrdctnSite" style="text-decoration:none" runat="server"  OnClientClick="return CheckConfirm();"
                             onclick="UpdtPrdctnSite_Click">Update Database</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                         <td style="background-image:url('image/menu.png'); width: 100%; height: 25px;">
                            <asp:LinkButton ID="LogoutButton" style="text-decoration:none" runat="server" 
                             onclick="LogoutButton_Click">Log out</asp:LinkButton>
                         </td>
                    </tr>
                                       <tr> 
                        <td style=" background-color:Aqua; background-image:url('image/b-bg.gif'); width:100%;height:200px" class="tdText" valign="top"><br />
                        
<!--                            This webform contains the standard copy-edit, mastercopying requirement and information about standard texts to be used in articles, issue prelim and backmatter pages of journals.-->
                        </td>
                   </tr>

                </table>
              </td>
             <td align= "center" style="background-image:url('image/menu.png'); height: 25px; width:85%;" valign="middle">
                 <asp:Label SkinID="MnuLbl" ID="Label1" runat="server" Text="Production Site"></asp:Label>
                 &nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="ProductionSiteDropDownList" runat="server" 
                     AutoPostBack="True" 
                     onselectedindexchanged="ProductionSiteDropDownList_SelectedIndexChanged"></asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;
                 <asp:Label SkinID="MnuLbl" ID="Label2" runat="server" Text="Journal Code"></asp:Label>
                 &nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="JournalCodeDropDownList" runat="server" 
                     AutoPostBack="True" 
                     onselectedindexchanged="JournalCodeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;                     
                 <asp:Label SkinID="MnuLbl" ID="JournalCodeLabel" runat="server" Font-Bold="True" Text="View"></asp:Label>
                 &nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="ViewDropDownList" runat="server" 
                     onselectedindexchanged="ViewDropDownList_SelectedIndexChanged">
                   
                 </asp:DropDownList>
             </td> 
        </tr>
        <tr id="menuRow"  runat="server"  valign="middle" >
             <td align ="center" style="background-image:url('image/menu.png'); height: 25px; width:85%; font-family: verdana;">
                <asp:LinkButton Font-Size ="11px"  ID="MAINHyperLink" runat="server" 
                     onclick="ProcessSection">MAIN</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="PITHyperLink" runat="server" 
                     onclick="ProcessSection">PIT</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="S0HyperLink" runat="server" 
                     onclick="ProcessSection">S0</asp:LinkButton>
                &nbsp;&nbsp;
                <!--CU hyperLonk is added by Rahul-->
                
                <asp:LinkButton ID="CUHyperLink" runat="server" 
                     onclick="ProcessSection">C&amp;U</asp:LinkButton>
                &nbsp;&nbsp;
                
                <asp:LinkButton ID="S100HyperLink" runat="server" 
                     onclick="ProcessSection">S100</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="S200HyperLink" runat="server" 
                     onclick="ProcessSection">S200</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="P100HyperLink" runat="server" 
                     onclick="ProcessSection">P100</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="S300HyperLink" runat="server" 
                     onclick="ProcessSection">S300</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="PrintHyperLink" runat="server" 
                     onclick="ProcessSection">PRINT</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="DispatchHyperLink" runat="server" Font-Size="Small" 
                     onclick="ProcessSection">DISPATCH</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="EditorHyperLink" runat="server" Font-Size="Small" 
                     onclick="ProcessSection">EDITOR</asp:LinkButton>
                 &nbsp;&nbsp;
                <asp:LinkButton ID="OthrInfoHyperLink" runat="server" 
                     onclick="ProcessSection">OTHER INFORMATION</asp:LinkButton>&nbsp; &nbsp;
                  &nbsp;&nbsp;
                 <asp:LinkButton ID="SaveAsExcel" runat="server" 
                 onclick="ProcessSaveAsExcel">SaveAsExcel</asp:LinkButton>&nbsp; &nbsp;
               
             </td> 
        </tr>
        <tr>
            <td align ="left"  valign="top" style=" height:100%;width:85%">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex=" 0">
                    <asp:View ID="NormalView" runat="server">
                        <table style="width:100%;">
                             <tr align="center">
                                 <td class="heading">
                                      <asp:Label ID="NrmlJIDName" runat="server" EnableTheming="false" ></asp:Label>
                                  </td> 
                              </tr> 
                              <tr align="left">
                                  <td>
                                      
                                    <asp:PlaceHolder  id="ifm"  runat ="server"  visible="true"></asp:PlaceHolder>                                      
                                    <asp:Xml id="xml1" runat="server"  TransformSource="~/jss.xsl" />
                                  </td>
                             </tr>
                        </table>     
                    </asp:View>
                    <asp:View ID="DiffView" runat="server">
                         <table style="width:100%; height: 13px;">
                           <tr align="center">
                                 <td style="background-color:#F5F5F5;padding: 0px; margin: 0px" class="heading">
                                     <asp:Label  ID="JIDName" runat="server"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
                            <td>
                             <table border="1" style="background-color:#F5F5F5;width:100%;">
                                <asp:PlaceHolder ID="VerColumn" runat="server">
                                </asp:PlaceHolder>   
                                 <asp:Repeater ID="Repeater1" runat="server" onitemcreated="Repeater1_ItemCreated">
                                     <HeaderTemplate>
                                         <tr style="width:100%;">
                                             <th align="center" colspan="<%# SpanValue %>" 
                                                 style="color: brown; Background: skyblue">
                                                 <b>BASE DATA</b>
                                              </th>
                                         </tr>
                                         <tr style="width:100%;">
                                             <th align="left" colspan="<%# SpanValue %>" style="color:brown">
                                                 <b>PTS Data and JM Edit Data</b>
                                             </th>
                                         </tr>
                                     </HeaderTemplate>
                                     <ItemTemplate>
                                         <asp:PlaceHolder ID="thing1" runat="server"></asp:PlaceHolder>
                                     </ItemTemplate>
                                 </asp:Repeater>
                             </table>
                             </td>
                             </tr>
                            <tr>
                                <td align ="left" valign="top"  >&nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table  style="background-color:#F5F5F5;" border="1" width="100%">
                                    <asp:Repeater ID="Repeater2" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr>
                                            <th style="color:brown;Background:skyblue" align ="center" colspan='<%# SpanValue %>'><b>S0</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                        <asp:PlaceHolder ID="thing1" runat="server"> 
                                        </asp:PlaceHolder> 
                                     </ItemTemplate>
                                    </asp:Repeater>
                                   </table>
                                </td>
                            </tr>
                            <tr>
                                 <td  align ="left" valign="top" bgcolor="White" >
                                 </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                 <table style="background-color:#F5F5F5;" border="1" width="100%" >
                                    <asp:Repeater ID="Repeater3" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                           <tr>
                                                <th  colspan="3" style="Background:skyblue;color:brown" align="center"><b>PIT</b></th>
                                           </tr> 
                                           <tr>
                                                <th style="color:brown"><b>Doc Head</b></th>
                                          </tr>
                                          <tr align="center">
                                                <th>Dochead PIT</th>
                                                <th>Dochead Description</th>
                                                <th>Dochead Expired</th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                          <asp:PlaceHolder ID="thing1" runat="server"> 
                                                 <tr>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column1") %> </td>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column2") %> </td>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column3") %> </td>
                                                 </tr>
                                         </asp:PlaceHolder>
                                         <asp:PlaceHolder ID="thing2" runat="server"> 
                                             <tr>
                                                  <td  width="20%"> <b><%# DataBinder.Eval(Container.DataItem, "ColumnHead")%> </b></td>
                                                  <td colspan="2"> <%# DataBinder.Eval(Container.DataItem, "Column1") %> </td>
                                             </tr> 
                                        </asp:PlaceHolder> 
                                    </ItemTemplate>
                                    </asp:Repeater>
                                 </table>
                               </td>  
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" ></td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                 <table style="background-color:#F5F5F5;" border="1"  width="100%">
                                    <asp:Repeater ID="Repeater4" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                           <tr>
                                                <th colspan="3" style="Background:skyblue;color:brown" ><b>Section Head</b></th>
                                          </tr>
                                          <tr align="center">
                                                <th>Section Code</th>
                                                <th>Section Description</th>
                                                <th>Section Expired</th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                          <asp:PlaceHolder ID="thing1" runat="server"> 
                                                 <tr>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column1") %> </td>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column2") %> </td>
                                                      <td>    <%# DataBinder.Eval(Container.DataItem, "Column3") %> </td>
                                                 </tr>
                                         </asp:PlaceHolder>
                                         <asp:PlaceHolder ID="thing2" runat="server"> 
                                             <tr>
                                                  <td  width="20%"> <b><%# DataBinder.Eval(Container.DataItem, "ColumnHead")%> </b></td>
                                                  <td colspan="2">     <%# DataBinder.Eval(Container.DataItem, "Column1") %> </td>
                                             </tr> 
                                        </asp:PlaceHolder> 
                                    </ItemTemplate>
                                    </asp:Repeater>
                                 </table>
                               </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                                              <!-- added by rahul-->    
                            
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                        <asp:Repeater ID="Repeater13" runat="server" onitemcreated="Repeater1_ItemCreated">
                                             <HeaderTemplate>
                                                  <tr align="center">
                                                    <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>C&U</b></th>
                                                  </tr>
                                             </HeaderTemplate>
                                             <ItemTemplate> 
                                              <asp:PlaceHolder ID="thing1" runat="server"> 
                                              </asp:PlaceHolder> 
                                             </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                       
                          <!-- added by rahul-->   
                            
                            
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5;" border="1" width="100%">
                                    <asp:Repeater ID="Repeater5" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr align="center">
                                            <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>S100</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                        <asp:PlaceHolder ID="thing1" runat="server"> 
                                        </asp:PlaceHolder> 
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater6" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr align="center">
                                            <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>S200</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                        <asp:PlaceHolder ID="thing1" runat="server"> 
                                        </asp:PlaceHolder> 
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color: #F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater7" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr align="center">
                                            <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>P100</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                              <asp:PlaceHolder ID="thing1" runat="server"> 
                                              </asp:PlaceHolder> 
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater8" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr align="center">
                                            <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>S300</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                              <asp:PlaceHolder ID="thing1" runat="server"> 
                                              </asp:PlaceHolder> 
                                     </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                        <asp:Repeater ID="Repeater9" runat="server" onitemcreated="Repeater1_ItemCreated">
                                             <HeaderTemplate>
                                                  <tr align="center">
                                                    <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>PRINT</b></th>
                                                  </tr>
                                             </HeaderTemplate>
                                             <ItemTemplate> 
                                              <asp:PlaceHolder ID="thing1" runat="server"> 
                                              </asp:PlaceHolder> 
                                             </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>                                    
                            
      
                             
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                        <asp:Repeater ID="Repeater12" runat="server" onitemcreated="Repeater1_ItemCreated">
                                             <HeaderTemplate>
                                                  <tr align="center">
                                                    <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>EDITOR</b></th>
                                                  </tr>
                                             </HeaderTemplate>
                                             <ItemTemplate> 
                                              <asp:PlaceHolder ID="thing1" runat="server"> 
                                              </asp:PlaceHolder> 
                                             </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater10" runat="server" onitemcreated="Repeater1_ItemCreated">
                                    <HeaderTemplate>
                                          <tr align="center">
                                            <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>DESPATCH</b></th>
                                          </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate> 
                                         <asp:PlaceHolder ID="thing1" runat="server"> 
                                         </asp:PlaceHolder> 
                                     </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                  <%--  add by munesh--%>
                             <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater14" runat="server" onitemcreated="Repeater1_ItemCreated">
                                        <HeaderTemplate>
                                             <tr align="center">
                                                <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>STANDARD TEXT</b></th>
                                             </tr>
                                        </HeaderTemplate>
                                         <ItemTemplate> 
                                             <asp:PlaceHolder ID="thing1" runat="server"> 
                                             </asp:PlaceHolder> 
                                         </ItemTemplate>
                                     </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                   <table style="background-color:#F5F5F5" border="1" width="100%">
                                    <asp:Repeater ID="Repeater11" runat="server" onitemcreated="Repeater1_ItemCreated">
                                        <HeaderTemplate>
                                             <tr align="center">
                                                <th style="Background:skyblue;color:brown"colspan='<%# SpanValue %>'><b>OTHER INSTRUCTIONS</b></th>
                                             </tr>
                                        </HeaderTemplate>
                                         <ItemTemplate> 
                                             <asp:PlaceHolder ID="thing1" runat="server"> 
                                             </asp:PlaceHolder> 
                                         </ItemTemplate>
                                     </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td  align ="left" valign="top" >
                                    &nbsp;</td>
                            </tr>
                         </table>
                    </asp:View>
                    <asp:View ID="JrnlListView" runat="server">
                      <asp:GridView runat="server" ID="JIDDetailDatagrid" HeaderStyle-Font-Bold="true"  
                            AllowSorting="true" onsorting="JIDDetailDatagrid_Sorting" 
                            ondatabound="JIDDetailDatagrid_DataBound" >
                         <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                         <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                         <AlternatingRowStyle BackColor="White" />
                        <Columns>
                        <asp:TemplateField HeaderText="S. NO.">
                          <%--<ItemStyle Width="8%" />--%>
                          <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                          </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                        
                        <%--<asp:DataList ID="JrnlListDataList" runat="server"
                                   forecolor="#000000"
                                   backcolor="#ffffff"
                                   cellpadding="3"
                                   gridlines="none"
                                   width="100%" >
                            <itemstyle Font-Names="tahoma,arial,sans-serif"
                                       font-size="12"
                                        backcolor="BlanchedAlmond" />
                            <alternatingitemstyle Font-Names="tahoma,arial,sans-serif"
                                       font-size="12"
                                       backcolor= "Beige" />
                            <HeaderStyle Font-Bold="true" BackColor= "Silver"   ForeColor="Brown" Font-Size="Large" />
                            <HeaderTemplate>
                                    JID List
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <%# Container.DataItem  %>
                            </ItemTemplate>
                        </asp:DataList>--%>
                    </asp:View> 
                    <asp:View ID="UploadView" runat="server">
                        <table style="width: 100%; height: 100%">
                            <tr>
            <td style="height: 162px; " align="center" valign="bottom" colspan="2">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" 
                    Text="Zip fIle path:" Width="102px"></asp:Label>
                &nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" 
                    style="position: relative; width: 276px; height: 24px; top: 1px; left: -10px;" />
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    OnClientClick="return ErrMsg();" 
                    style="position: relative; height: 25px; width: 73px; top: 2px; left: 11px;" 
                    Text="Upload" Enabled="false" />
                </td>
        </tr>
        <tr>
            <td style="width: 147px" valign="top">
             <asp:Panel ID="Panel1" runat="server"  
                    
                    style="position: relative; top: 8px; left: 6px; height: 142px; width: 304px;">
                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; float: left; height: 107px; clear:left;">
                     <tr>
                         <td style="height: 25px" align="center" colspan="2">
                              <asp:Label ID="LblUploadedFileStatus" runat="server" BorderStyle="None" Font-Bold="True" 
                                  ForeColor="Blue"></asp:Label></td>
                     </tr>
                     <tr>
                         <td style="float: left; width: 147px;" align="left">
                             <asp:Label ID="LblFileName"  runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="float: left;">
                             <asp:Label ID="LblFileNameValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="height: 23px; width: 147px;" align="left">
                             <asp:Label ID="LblTotalFiles" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="height: 23px">
                             <asp:Label ID="LblTotalFilesValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="height: 17px; width: 147px;" align="left">
                             <asp:Label ID="LblXMLFILES" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left" style="height: 17px">
                             <asp:Label ID="LblXMLFILESValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td align="left" style="width: 147px">
                             <asp:Label ID="LblTXTFILES" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                         <td align="left">
                             <asp:Label ID="LblTXTFILESValue" runat="server"></asp:Label>
                         </td>
                     </tr>
                 </table>
            </asp:Panel>
            </td>
            <td style="width: 583px" valign="top">
            <table border="1" width="100%" style="vertical-align:top">
            <asp:Repeater OnItemCreated= "LogStatusRepeater_ItemCreated" id="LogStatusRepeater" 
                    runat="server"  >
            <HeaderTemplate>
                  <tr>
                    <td><b>S.No.</b></td>
                    <td><b>File Name</b></td>
                    <td><b>Status</b></td>
                    <td><b>FMSStatus</b></td>
                  </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                  <td> <%# DataBinder.Eval(Container.DataItem, "Seqno") %> </td>
                  <td> <%# DataBinder.Eval(Container.DataItem, "FileName") %> </td>
                  <td><asp:Label id="statusLabel" runat= "server"> <%# DataBinder.Eval(Container.DataItem, "Result") %> </asp:Label></td>
                  <td><asp:Label id="FMSstatusLabel" runat= "server"> <%# DataBinder.Eval(Container.DataItem, "FMSResult") %> </asp:Label></td>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </td>
        </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="DefaultView" runat="server">
                    <table style="width:100%;">
                             <tr align="center" valign="middle">
                                 <td>
                                <asp:Image ImageAlign="AbsMiddle" runat="server" ImageUrl="~/image/e-book.jpg" 
                                    ID="aa" Height="309px" Width="320px" />
                                </td>
                            </tr>
                            </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        
    </table>
</asp:Content>    



