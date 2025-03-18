<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAcceptedBets.aspx.cs" Inherits="SourDuckWannaBet.ViewAcceptedBets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Accepted Bets</title>
    <style>
        #header {
            width: 100%;
            background-color: gold;
            padding: 10px 0;
            margin: 0;
            text-align: center;
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
        }
    </style>
</head>
<body>
    <form id="frmViewAcceptedBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div>
        </div>
    </form>
</body>
</html>
