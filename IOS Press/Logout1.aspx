<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout1.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var backlen = history.length;
        history.go(-backlen);
        window.location.href = "Login.aspx";
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
