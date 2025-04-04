<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GRDice.aspx.cs" Inherits="SourDuckWannaBet.GRDice" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dice Game</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }
        .game-container {
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            text-align: center;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .dice-options {
            margin: 20px 0;
            padding: 15px;
            background-color: #f9f9f9;
            border-radius: 5px;
        }
        .result {
            font-size: 24px;
            margin: 20px 0;
            min-height: 50px;
            padding: 10px;
        }
        .balance {
            font-weight: bold;
            margin-bottom: 20px;
            font-size: 18px;
            color: #333;
        }
        input[type="number"], select {
            padding: 8px;
            margin: 5px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .btn {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 10px;
        }
        .btn:hover {
            background-color: #45a049;
        }
        .error {
            color: #f44336;
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="game-container">
            <h1>Dice Game</h1>
            
            <div class="balance">
                <asp:Label ID="lblBalance" runat="server" Text="Balance: $0.00"></asp:Label>
            </div>
            
            <div class="dice-options">
                <div>
                    <asp:Label runat="server" Text="Select Dice Type:"></asp:Label>
                    <asp:DropDownList ID="ddlSides" runat="server">
                        <asp:ListItem Value="6">6-sided</asp:ListItem>
                        <asp:ListItem Value="10">10-sided</asp:ListItem>
                        <asp:ListItem Value="20">20-sided</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div style="margin-top: 15px;">
                    <asp:Label runat="server" Text="Your Guess (number):"></asp:Label>
                    <asp:TextBox ID="txtGuess" runat="server" TextMode="Number" min="1" max="20"></asp:TextBox>
                </div>
                
                <div style="margin-top: 15px;">
                    <asp:Label runat="server" Text="Bet Amount ($):"></asp:Label>
                    <asp:TextBox ID="txtBetAmount" runat="server" TextMode="Number" step="0.01" min="0.01"></asp:TextBox>
                </div>
            </div>
            
            <asp:Button ID="btnRoll" runat="server" Text="Roll Dice" OnClick="btnRoll_Click" CssClass="btn" />
            
            <div class="result">
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            </div>
            
            <div class="error">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>