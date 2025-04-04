<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GRDashboard.aspx.cs" Inherits="SourDuckWannaBet.GRDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Game Room Dashboard</title>
    <style>
        /* General body styling */
        body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f5f5f5;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        /* Container for the dashboard */
        .dashboard-container {
            width: 95%;
            max-width: 1400px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
            overflow: hidden;
        }

        /* Header styling */
        .header {
            background-color: #333;
            color: white;
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 2px solid #444;
        }

        .site-title {
            font-size: 24px;
            font-weight: bold;
        }

        .user-balance {
            font-size: 18px;
            background-color: #4CAF50;
            padding: 10px 20px;
            border-radius: 20px;
        }

        /* Actions bar styling */
        .actions-bar {
            display: flex;
            justify-content: space-between;
            padding: 20px;
            background-color: white;
            border-bottom: 1px solid #ddd;
            text-align: center;
        }

        .action-button {
            background-color: #FFD700;
            color: #333;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .action-button:hover {
            background-color: #e6b800;
        }

        .game-button {
            background-color: #747bf6;
            color: white;
            border: none;
            padding: 15px 30px;
            font-size: 18px;
            cursor: pointer;
            border-radius: 5px;
            margin: 10px;
            transition: background-color 0.3s;
        }

        .game-button:hover {
            background-color: #2a468e;
        }

        /* Game buttons container */
        .games-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 30px;
        }

        /* Hamburger menu styling */
        .hamburger-menu {
            cursor: pointer;
            font-size: 24px;
            margin-right: 15px;
        }

        /* Dropdown menu styling */
        .dropdown-menu {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
            z-index: 1;
        }

        .dropdown-menu a {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }

        .dropdown-menu a:hover {
            background-color: #f1f1f1;
        }

        .show {
            display: block;
        }

        .welcome-message {
            text-align: center;
            padding: 20px;
            font-size: 20px;
        }
    </style>
    <script>
        function toggleMenu() {
            var menu = document.getElementById("dropdownMenu");
            menu.classList.toggle("show");
        }

        window.onclick = function (event) {
            if (!event.target.matches('.hamburger-menu')) {
                var dropdowns = document.getElementsByClassName("dropdown-menu");
                for (var i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.classList.contains('show')) {
                        openDropdown.classList.remove('show');
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <!-- Header -->
            <div class="header">
                <div style="display: flex; align-items: center;">
                    <div class="hamburger-menu" onclick="toggleMenu()">☰</div>
                    <div class="site-title">
                        Game Room <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="user-balance">
                    Balance: $<asp:Label ID="lblBalance" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <!-- Dropdown menu -->
            <div id="dropdownMenu" class="dropdown-menu">
                <a href="WBDashboard.aspx">Home</a>
                <a href="WBProfile.aspx">My Profile</a>
                <a href="WBMyBets.aspx">My Bets</a>
                <a href="WBAddAFriend.aspx">Friends</a>
                <a href="WBLogout.aspx">Logout</a>
            </div>

            <div class="welcome-message">
                <h2>Welcome to the Game Room!</h2>
                <p>Choose a game to play below.</p>
            </div>

            <!-- Actions bar -->
            <div class="actions-bar">
                <asp:Button ID="btnReturnToDashboard" runat="server" Text="Return to Dashboard" CssClass="action-button" OnClick="btnReturnToDashboard_Click" />
            </div>

            <!-- Game buttons -->
            <div class="games-container">
                <asp:Button ID="btnCoinFlip" runat="server" Text="Play Coin Flip" CssClass="game-button" OnClick="btnCoinFlip_Click" />
                <asp:Button ID="btnDice" runat="server" Text="Play Dice" CssClass="game-button" OnClick="btnDice_Click" />
                <asp:Button ID="btnBlackjack" runat="server" Text="Play Blackjack" CssClass="game-button" OnClick="btnBlackjack_Click" />
            </div>
        </div>
    </form>
</body>
</html>