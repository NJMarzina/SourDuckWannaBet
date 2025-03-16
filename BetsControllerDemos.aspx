<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetsControllerDemos.aspx.cs" Inherits="SourDuckWannaBet.BetsControllerDemos" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetsControllerDemos</title>
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
            margin-top: 50px;
        }
        .section {
            margin-bottom: 20px;
        }
        .section h2 {
            margin-bottom: 10px;
        }
        .section input[type="text"], .section input[type="number"] {
            margin-right: 10px;
        }
        .section button {
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="frmBetsControllerDemos" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <div class="section">
                <h2>Get All Bets</h2>
                <asp:Button ID="btnGetAllBets" runat="server" Text="Get All Bets" OnClick="btnGetAllBets_Click" />
                <asp:Label ID="lblAllBets" runat="server" Text=""></asp:Label>
            </div>

            <div class="section">
                <h2>Get Bets by User ID</h2>
                <asp:TextBox ID="txtUserId" runat="server" placeholder="Enter User ID"></asp:TextBox>
                <asp:Button ID="btnGetBetsByUserId" runat="server" Text="Get Bets" OnClick="btnGetBetsByUserId_Click" />
                <asp:Label ID="lblBetsByUserId" runat="server" Text=""></asp:Label>
            </div>

            <div class="section">
                <h2>Add Bet</h2>
                <asp:TextBox ID="txtBetSenderId" runat="server" placeholder="Sender ID"></asp:TextBox>
                <asp:TextBox ID="txtBetReceiverId" runat="server" placeholder="Receiver ID"></asp:TextBox>
                <asp:TextBox ID="txtBetAmount" runat="server" placeholder="Amount"></asp:TextBox>
                <asp:Button ID="btnAddBet" runat="server" Text="Add Bet" OnClick="btnAddBet_Click" />
                <asp:Label ID="lblAddBetResult" runat="server" Text=""></asp:Label>
            </div>

            <div class="section">
                <h2>Update Bet Status</h2>
                <asp:TextBox ID="txtBetId" runat="server" placeholder="Enter Bet ID"></asp:TextBox>
                <asp:DropDownList ID="ddlBetStatus" runat="server">
                    <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                    <asp:ListItem Text="Denied" Value="Denied"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnUpdateBetStatus" runat="server" Text="Update Status" OnClick="btnUpdateBetStatus_Click" />
                <asp:Label ID="lblUpdateBetStatusResult" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>