<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryForm.aspx.cs" Inherits="EntryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function HR1_onclick() {

}

// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="XX-Large" Height="27px"
            Style="z-index: 100; left: 384px; position: absolute; top: 77px" Text="Online Flow Sheet"
            Width="265px"></asp:Label>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/CompLogo.gif" Style="z-index: 101;
            left: 869px; position: absolute; top: 23px" />
        <hr id="HR1" style="z-index: 154; left: 6px; position: absolute; top: 126px; height: 4px; width: 977px;" onclick="return HR1_onclick()" />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/td_logo.jpg" Style="z-index: 102;
            left: 14px; position: absolute; top: 10px" />
        <asp:Label ID="Label1" runat="server" Style="z-index: 103; left: 89px; position: absolute;
            top: 142px" Text="JID" Width="32px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Style="z-index: 104; left: 294px; position: absolute;
            top: 142px" Text="AID" Width="55px"></asp:Label>
        <asp:Label ID="lbl1" runat="server" Style="z-index: 105; left: 502px; position: absolute;
            top: 517px" Text="Date of Print/Screen pdf sent to IOS" Width="229px"></asp:Label>
        <asp:Label ID="Label5" runat="server" Style="z-index: 106; left: 29px; position: absolute;
            top: 249px" Text="Typeset Pages" Width="92px"></asp:Label>
        <asp:Label ID="Label6" runat="server" Style="z-index: 107; left: 73px; position: absolute;
            top: 196px" Text="Authors" Width="30px"></asp:Label>
        &nbsp;
        <asp:Label ID="Label8" runat="server" Style="z-index: 108; left: 19px; position: absolute;
            top: 222px" Text="Date of Receipt" Width="102px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Style="z-index: 109; left: 86px; position: absolute;
            top: 169px" Text="Title" Width="35px"></asp:Label>
        <asp:Label ID="Label10" runat="server" Style="z-index: 110; left: 539px; position: absolute;
            top: 138px" Text="Publication details (vol/issue/page nrs)" Width="121px" Height="47px"></asp:Label>
        &nbsp; &nbsp;
        <asp:TextBox ID="txtAID" runat="server" OnTextChanged="TextBox9_TextChanged" Style="z-index: 111;
            left: 371px; position: absolute; top: 141px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtVolIss" runat="server" Style="z-index: 112; left: 672px; position: absolute;
            top: 149px" Height="15px" OnTextChanged="txtVolIss_TextChanged" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtTitle" runat="server" OnTextChanged="TextBox2_TextChanged" Style="z-index: 113;
            left: 131px; position: absolute; top: 168px" Width="396px" Height="15px"></asp:TextBox>
        <asp:TextBox ID="txtAuthors" runat="server" OnTextChanged="TextBox3_TextChanged" Style="z-index: 114;
            left: 131px; position: absolute; top: 195px" Width="396px" Height="15px"></asp:TextBox>
        <asp:TextBox ID="txtTypesetPages" runat="server" Style="z-index: 115; left: 131px; position: absolute;
            top: 248px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtReceiptDt" runat="server" Style="z-index: 116; left: 131px; position: absolute;
            top: 221px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtPrintConfirmationDt" runat="server" OnTextChanged="TextBox6_TextChanged" Style="z-index: 117;
            left: 323px; position: absolute; top: 514px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtPrintDtIOS" runat="server" Style="z-index: 118; left: 736px; position: absolute;
            top: 514px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtThomsonRemarks" runat="server" Style="z-index: 119; left: 323px; position: absolute;
            top: 541px" Height="55px" OnTextChanged="txtThomsonRemarks_TextChanged" Width="568px"></asp:TextBox>
        <asp:Label ID="lblConfirmationDt" runat="server" Style="z-index: 120; left: 78px; position: absolute;
            top: 515px" Text="Confirmation Date of Print/Screen pdf" Width="235px"></asp:Label>
        <asp:Label ID="Label13" runat="server" Style="z-index: 121; left: 547px; position: absolute;
            top: 491px" Text="Date of CRC delivery to IOS" Width="184px"></asp:Label>
        <asp:Label ID="Label14" runat="server" Style="z-index: 122; left: 13px; position: absolute;
            top: 488px" Text="Confirmation Date to issue compilation from IOS" Width="300px"></asp:Label>
        <asp:Label ID="Label15" runat="server" Style="z-index: 123; left: 103px; position: absolute;
            top: 419px" Text="Special requirements from authors" Width="210px"></asp:Label>
        <asp:Label ID="Label16" runat="server" Style="z-index: 124; left: 460px; position: absolute;
            top: 376px" Text="from Editorial Assistant" Width="150px"></asp:Label>
        <asp:Label ID="Label17" runat="server" Style="z-index: 125; left: 473px; position: absolute;
            top: 346px" Text="from Editor-in-Chief" Width="137px"></asp:Label>
        <asp:Label ID="Label18" runat="server" Style="z-index: 126; left: 527px; position: absolute;
            top: 318px" Text="from authors" Width="83px"></asp:Label>
        <asp:Label ID="Label19" runat="server" Style="z-index: 127; left: 81px; position: absolute;
            top: 374px" Text="to Editorial Assistant" Width="134px"></asp:Label>
        <asp:Label ID="Label20" runat="server" Style="z-index: 128; left: 97px; position: absolute;
            top: 346px" Text="to Editor-in-Chief" Width="118px"></asp:Label>
        <asp:Label ID="Label21" runat="server" Style="z-index: 129; left: 146px; position: absolute;
            top: 318px" Text="to authors" Width="69px"></asp:Label>
        <asp:Label ID="Label11" runat="server" Style="z-index: 130; left: 518px; position: absolute;
            top: 289px" Text="Date of corrections received" Width="198px" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label22" runat="server" Style="z-index: 131; left: 158px; position: absolute;
            top: 540px" Text="Remarks from typesetter" Width="155px"></asp:Label>
        <asp:TextBox ID="txttoAuthors" runat="server" Style="z-index: 132; left: 228px; position: absolute;
            top: 317px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txttoEditor" runat="server" Style="z-index: 133; left: 228px; position: absolute;
            top: 345px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txttoEditorial" runat="server" Style="z-index: 134; left: 228px; position: absolute;
            top: 373px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtfromAuthors" runat="server" Style="z-index: 135; left: 620px; position: absolute;
            top: 317px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtfromEditor" runat="server" Style="z-index: 136; left: 620px; position: absolute;
            top: 345px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtfromEditorial" runat="server" Style="z-index: 137; left: 620px; position: absolute;
            top: 373px" Height="15px" OnTextChanged="txtfromEditorial_TextChanged" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtSR" runat="server" Style="z-index: 138; left: 323px; position: absolute;
            top: 417px" Width="568px" Height="55px"></asp:TextBox>
        <asp:TextBox ID="txtCompilationDt" runat="server" Style="z-index: 139; left: 323px; position: absolute;
            top: 488px" Height="15px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtCRCDt" runat="server" Style="z-index: 140; left: 736px; position: absolute;
            top: 488px" Height="15px" OnTextChanged="txtCRCDt_TextChanged" Width="155px"></asp:TextBox>
        <hr id="Hr2" style="z-index: 151; left: 6px; position: absolute; top: 278px; height: 4px; width: 977px;" onclick="return HR1_onclick()" />
        <asp:Label ID="Label7" runat="server" Style="z-index: 141; left: 138px; position: absolute;
            top: 292px" Text="Date of PDF proof sent" Width="168px" Font-Bold="True"></asp:Label>
        <hr id="Hr3" style="z-index: 152; left: 9px; position: absolute; top: 405px; height: 4px; width: 977px;" onclick="return HR1_onclick()" /><hr id="Hr5" style="z-index: 153; left: 6px; position: absolute; top: 615px; height: 4px; width: 977px;" onclick="return HR1_onclick()" />
        &nbsp;&nbsp;
        <asp:Button ID="cmdClear" runat="server" OnClick="Button1_Click" Style="z-index: 142;
            left: 146px; position: absolute; top: 675px" Text="Clear" Height="24px" Width="109px" />
        <asp:Button ID="cmdLogout" runat="server" Style="z-index: 143; left: 663px; position: absolute;
            top: 675px" Text="Logout" Height="24px" OnClick="cmdLogout_Click" Width="109px" />
        <asp:Button ID="cmdAdd" runat="server" Style="z-index: 144; left: 275px; position: absolute;
            top: 675px" Text="Add" Height="24px" OnClick="cmdAdd_Click" Width="109px" />
        <asp:Button ID="cmdUpdate" runat="server" OnClick="cmdUpdate_Click" Style="z-index: 145;
            left: 404px; position: absolute; top: 675px" Text="Update" Height="24px" Width="109px" />
        <asp:Button ID="cmdView" runat="server" Height="24px" OnClick="cmdView_Click" Style="z-index: 146;
            left: 886px; position: absolute; top: 139px" Text="View Info" Width="89px" />
        <asp:TextBox ID="txtJIDDescription" runat="server" Height="15px" Style="z-index: 147;
            left: 388px; position: absolute; top: 221px" Width="138px"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Style="z-index: 148; left: 305px; position: absolute;
            top: 222px" Text="Description" Width="52px"></asp:Label>
        <asp:DropDownList ID="cmbJID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
            Style="z-index: 149; left: 131px; position: absolute; top: 140px" Width="112px">
        </asp:DropDownList>
        <asp:Button ID="cmdReports" runat="server" OnClick="cmdReports_Click" Style="z-index: 155;
            left: 533px; position: absolute; top: 675px" Text="Reports" Width="109px" Height="24px" />
        <asp:TextBox ID="txtTo" runat="server" Height="15px" Style="z-index: 139; left: 220px;
            position: absolute; top: 628px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtCC" runat="server" Height="15px" Style="z-index: 139; left: 425px;
            position: absolute; top: 628px" Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtBCC" runat="server" Height="15px" OnTextChanged="TextBox6_TextChanged"
            Style="z-index: 117; left: 637px; position: absolute; top: 628px" Width="155px"></asp:TextBox>
        <hr id="Hr4" style="z-index: 153; left: 6px; position: absolute; top: 660px; height: 4px; width: 977px;" onclick="return HR1_onclick()" />
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Style="z-index: 120; left: 73px;
            position: absolute; top: 629px" Text="e-Mail Address" Width="108px"></asp:Label>
        <asp:Label ID="Label23" runat="server" Style="z-index: 120; left: 184px; position: absolute;
            top: 629px" Text="To" Width="28px"></asp:Label>
        <asp:Label ID="Label24" runat="server" Style="z-index: 120; left: 594px; position: absolute;
            top: 629px" Text="BCC" Width="28px"></asp:Label>
        <asp:Label ID="Label25" runat="server" Style="z-index: 120; left: 389px; position: absolute;
            top: 629px" Text="CC" Width="28px"></asp:Label>
    
    </div>
    </form>
</body>
</html>
