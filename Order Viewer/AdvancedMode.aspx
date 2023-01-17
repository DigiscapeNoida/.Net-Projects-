<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="AdvancedMode.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Viewer</title>
</head>
<body style="font-family:'Arial Unicode MS'" bgcolor="#f7f7f7">
    <form id="form1" runat="server">
    
    
        <div style="Z-INDEX: 101; LEFT: 0px; OVERFLOW: auto; WIDTH: 21%; POSITION: absolute; TOP: 129px; HEIGHT: 180px"><asp:TreeView ID="TreeView1" runat="server" Width="187px" style="z-index: 100; left: 1px; position: absolute; top: 1px" BorderStyle="None" ForeColor="White" ImageSet="Contacts" NodeIndent="10" Height="133px" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BackColor="#C0C0FF">
            <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
            <HoverNodeStyle Font-Underline="False" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" Font-Bold="True" ForeColor="#0000C0" />
            <NodeStyle Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
            <RootNodeStyle ForeColor="#400040" />
        </asp:TreeView>
    </div>
       
    
    <iframe id="ifm" frameborder="0" runat="server" style="z-index: 102; left: 0px; width: 44px; position: absolute; top: 102px; height: 25px" visible="false"></iframe>
    <div style="Z-INDEX: 101; LEFT: 217px; OVERFLOW: auto; WIDTH: 80%; POSITION: absolute; TOP: 128px; HEIGHT: 181px">
        &nbsp;
        <asp:GridView ID="GridView1" runat="server" Style="z-index: 100; left: 0px; position: absolute;
            top: 1px" Width="766px" AutoGenerateColumns="False" CellPadding="4" Font-Names="Times New Roman" Font-Size="Small" Height="89px" OnRowDataBound="GridView1_RowDataBound" Font-Bold="True" ForeColor="#333333" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns> 
            <asp:TemplateField>            
            <ItemTemplate>
                <asp:RadioButton ID="rborder" runat="server" OnCheckedChanged="View_Order" AutoPostBack="true" GroupName="a" Checked="false"/>
            </ItemTemplate>
            </asp:TemplateField>                
            <asp:BoundField HeaderText="JIDAID" DataField="JIDAID" /> 
            <asp:BoundField HeaderText="Stage" DataField="Stage" /> 
            <asp:BoundField HeaderText="REV" DataField="REV" /> 
            <asp:BoundField HeaderText="Response" DataField="Response" /> 
            <asp:BoundField HeaderText="OrderPath" DataField="OrderPath" />          
            </Columns>      
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
            <HeaderStyle BackColor="#C0C0FF" Font-Bold="True" ForeColor= "Black" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
       
    </div>
         <hr id="h1" runat="server" style="z-index: 101; left: 0px; position: absolute; top: 312px; width: 1003px; height: 3px;"></hr>
       
    
        <table id="TABLE1" border="0" cellpadding="0" cellspacing="0" onclick="return TABLE1_onclick()"
            style="z-index: 100; left: 0px; width: 102%; border-top-style: none; border-right-style: none;
            border-left-style: none; position: absolute; top: 0px; height: 10%; border-bottom-style: none">
            <tr style="width: 100%; height: 128px; background-color: #f2f2f2">
                <td colspan="2" style="background-image: url(header1.jpg); background-repeat: no-repeat">
                    <asp:LinkButton ID="lnknormal" runat="server" CausesValidation="False" Font-Size="Large"
                        ForeColor="Black" Style="z-index: 102; left: 688px; position: absolute;
                        top: 98px" OnClick="lnknormal_Click" 
                        ToolTip="Click here to go to Normal Mode.">NormalMode</asp:LinkButton>&nbsp;&nbsp;
                    <!--img src="header.gif" style="width: 100%; position: relative" alt="Order Viewer" /-->
                    <asp:LinkButton ID="lnkprint" runat="server" CausesValidation="False" Font-Size="Large"
                        ForeColor="Black" OnClick="lnkprint_Click" OnClientClick=" window.print();return false;"
                        
                        Style="z-index: 100; left: 635px; position: absolute; top: 98px; width: 42px; right: 340px;">Print</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Xml ID="Xml1" runat="server"></asp:Xml>
        
       
    
    </form>
     
</body>
</html>
