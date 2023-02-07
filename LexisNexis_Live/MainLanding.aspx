<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="MainLanding.aspx.cs" Inherits="MainLanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    

    <div class="body_container">

<div class="wrapper">

<div class="page_content">
<div class="page_header">
    
 <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="button_one" OnClick="btnLogout_Click" Visible="false"  />
 <asp:Label ID="lblname" runat="server" Text="" ></asp:Label><asp:Label ID="lblUserName" runat="server" Text ="connected Username"></asp:Label>


</div>

<div class="box1">
    <div class="row">
    </div>
 </div>
    
     <div class="box2">
          <div class="row">
             <asp:LinkButton ID="lnkjournal" runat="server" Text="Journals"  style="padding-left:100px;padding-top:200px;font-family:Arial;font-weight:bold; font-size:12pt" OnClick="lnkjournal_Click"></asp:LinkButton>
              <br />
             </div>
         </div>

 
    

     <div class="box2">
          <div class="row">
           <asp:LinkButton ID="lnkfiche" runat="server" Text="Fiches"  style="padding-left:100px;padding-top:200px;font-family:Arial;font-weight:bold; font-size:12pt" OnClick="lnkfiche_Click"></asp:LinkButton>
              <br />
             </div>
         </div>
    


         <div class="box2">
          <div class="row">
             <asp:LinkButton ID="lnkencyclo" runat="server" Text="Encyclopedia"  style="padding-left:100px;padding-top:200px;font-family:Arial;font-weight:bold; font-size:12pt" OnClick="lnkencyclo_Click"></asp:LinkButton>
              <br />
             </div>
         </div>



    <div class="box2">
       <div class="row">
        <asp:LinkButton ID="lnkdosier" runat="server" Text="Dossiers" style="padding-left:100px;padding-top:200px;font-family:Arial;font-weight:bold; font-size:12pt" OnClick="lnkdosier_Click"></asp:LinkButton>
           <br />
    </div>
 </div>

    


   
    </div>
        </div>
        </div>
   
</asp:Content>

