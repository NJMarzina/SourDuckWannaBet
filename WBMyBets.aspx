﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBMyBets.aspx.cs" Inherits="SourDuckWannaBet.WBMyBets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Bets</title>
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
    .container {
        max-width: 600px;
        margin: 0 auto;
        margin-top: 50px;
    }
    </style>
</head>
<body>
    <form id="frmWBMyBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Dashboard" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <h1>My Bets</h1>
            <h5> coming soon. </h5>
        </div>
    </form>
</body>
</html>
