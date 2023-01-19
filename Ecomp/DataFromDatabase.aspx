<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataFromDatabase.aspx.cs" Inherits="CSASPNETGridView.DataFromDatabase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Proof Central Dataset Information</title>
</head>
<body>
    <form id="form1" runat="server">
    <div><h1>Proof Central Dataset Information</h1></div>
    <div>

        <h3>Dataset Information</h3>
         <asp:Panel ID="pnlAdd" runat="server" Visible="True" Font-Size="Medium">
            Provide ISBN:  
            <asp:TextBox ID="tbisbn" runat="server"></asp:TextBox>
            
            Provide PII:  
            <asp:TextBox ID="tbpii" runat="server"></asp:TextBox>
            <br />
             <br />
         
             <br />          
             <asp:LinkButton ID="lbtnSubmit" runat="server" onclick="lbtnSubmit_Click">Submit</asp:LinkButton>
             &nbsp;&nbsp;&nbsp;
            
        </asp:Panel>
        <asp:GridView ID="gvPerson" runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            onpageindexchanging="gvPerson_PageIndexChanging" 
            onrowcancelingedit="gvPerson_RowCancelingEdit" 
            onrowdatabound="gvPerson_RowDataBound" onrowdeleting="gvPerson_RowDeleting" 
            onrowediting="gvPerson_RowEditing" onrowupdating="gvPerson_RowUpdating"            
            onsorting="gvPerson_Sorting">
        <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>              
 

                <asp:BoundField DataField="isbn" HeaderText="ISBN" ReadOnly="True" 
                    SortExpression="ISBN" />

                <asp:BoundField DataField="PII" HeaderText="PII" />                
                <asp:BoundField DataField="DSCSTATUS" HeaderText="Dataset Status" />
                <asp:BoundField DataField="TOMBKNO" HeaderText="Dataset ID" />
                
                <asp:BoundField DataField="IsUploaded" HeaderText="IsUploaded" />
                <asp:BoundField DataField="UploadDate" HeaderText="UploadDate" />
                
                
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>
    
         <br />
         <h3>Signal Information</h3>
         <br />
            Provide PII:
            <asp:TextBox ID="tbsignalpii" runat="server"></asp:TextBox>
            <br />    
         <br />
         <asp:LinkButton ID="LinkButton2" runat="server" onclick="lbtnSelect_Click">Submit</asp:LinkButton>
         <br />

        <asp:GridView ID="gvSignalInfo" runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            onpageindexchanging="gvPerson_PageIndexChanging" 
            onrowcancelingedit="gvPerson_RowCancelingEdit" 
            onrowdatabound="gvPerson_RowDataBound" onrowdeleting="gvPerson_RowDeleting" 
            onrowediting="gvPerson_RowEditing" onrowupdating="gvPerson_RowUpdating" 
            onsorting="gvPerson_Sorting">
        <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>                
                <asp:BoundField DataField="Filename" HeaderText="Filename" />
                <asp:BoundField DataField="TypeSignal" HeaderText="TypeSignal" />
                <asp:BoundField DataField="DownloadDate" HeaderText="DownloadDate" />
                <asp:BoundField DataField="URL" HeaderText="URL" />                
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />                                
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>
         <br />
         <br />
    
        <br />
       <!-- <asp:LinkButton ID="lbtnAdd" runat="server" onclick="lbtnAdd_Click">AddNew</asp:LinkButton>-->
        <br />
        <br />
       
    </div>
    </form>
</body>
</html>
