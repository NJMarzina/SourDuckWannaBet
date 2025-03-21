<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBDashboard.aspx.cs" Inherits="SourDuckWannaBet.WBDashboard" Async="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wanna Bet Dashboard</title>
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
            max-width: 1400px; /* Increased max-width */
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
        }

        .action-button {
            background-color: #FFD700; /* Gold color */
            color: #333;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .action-button:hover {
            background-color: #e6b800; /* Darker gold on hover */
        }

        /* Bet list styling */
        .bet-list {
            padding: 20px;
        }

        /* Enhanced bet-container styling */
.bet-container {
    background-color: #ffffff;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
    padding: 20px;
    transition: transform 0.2s, box-shadow 0.2s;
    cursor: pointer;
}

.bet-container:hover {
    transform: translateY(-5px);
    box-shadow: 0 6px 15px rgba(0, 0, 0, 0.2);
}

/* Bet header section */
.bet-header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
    padding-bottom: 10px;
    border-bottom: 2px solid #f0f0f0;
    font-size: 22px;
    font-weight: bold;
}

/* Description styling */
.bet-description {
    margin-bottom: 15px;
    font-size: 20px;
    color: #333;
    line-height: 1.5;
    font-weight: 450;
    text-decoration: underline;
}

/* Stakes styling */
.bet-stakes {
    background-color: #f7f7f7;
    padding: 10px;
    border-radius: 6px;
    margin-bottom: 15px;
    font-size: 16px;
}

/* Winner selection buttons */
.winner-buttons {
    display: flex;
    justify-content: space-between;
    gap: 10px;
}

.winner-button {
    width: 48%;
    padding: 12px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-weight: bold;
    font-size: 14px;
    transition: background-color 0.3s, transform 0.3s;
}

.winner-button:hover {
    transform: translateY(-2px);
}

/* Button styles */
.sender-win {
    background-color: #4CAF50; /* Green for sender win */
    color: white;
}

.sender-win:hover {
    background-color: #45a049;
}

.receiver-win {
    background-color: #2196F3; /* Blue for receiver win */
    color: white;
}

.receiver-win:hover {
    background-color: #1e88e5;
}


        /* No bets message styling */
        .no-bets-message {
            text-align: center;
            margin-top: 50px;
            color: #777;
            font-size: 18px;
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
        #content1 {
            width: 80%;
            text-align: center;
            margin-left: 10%;
        }
        #content2 {
            width: 80%;
            text-align: center;
            margin-left: 10%;
        }
    </style>
    <script>
        // Function to toggle the dropdown menu when clicking the hamburger icon
        function toggleMenu() {
            var menu = document.getElementById("dropdownMenu");
            menu.classList.toggle("show");
        }

        // Close the dropdown if the user clicks outside of it
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
    <form id="frmDashboard" runat="server">
        <div class="dashboard-container">
            <!-- Header -->
            <div class="header">
                <div style="display: flex; align-items: center;">
                    <div class="hamburger-menu" onclick="toggleMenu()">☰</div>
                    <div class="site-title">
                        Wanna Bet <%= Request.Cookies["Username"]?.Value %>?
                    </div>
                </div>
                <div class="user-balance">
                    Balance: $<%= Request.Cookies["Balance"]?.Value %>
                </div>
            </div>

            <!-- Dropdown menu -->
            <div id="dropdownMenu" class="dropdown-menu">
                <a href="Index.aspx">Home</a>
                <a href="Profile.aspx">My Profile</a>
                <a href="MyBets.aspx">My Bets</a>
                <a href="Friends.aspx">Friends</a>
                <a href="WBLogout.aspx">Logout</a>
            </div>

            <div id="content1">
                <h3>
                    Welcome to your dashboard, <%= Request.Cookies["Username"]?.Value %>!
                </h3>
                <p>
                    Here you can view your active bets, manage your profile, and interact with your friends.
                </p>
                <p>
                    Enjoy betting and good luck!
                </p>
            </div>

            <!-- Actions bar -->
            <div class="actions-bar">
                <asp:Button ID="btnSendBet" runat="server" Text="Send A Bet" CssClass="action-button" OnClick="btnSendBet_Click" />
                <asp:Button ID="btnAnotherButton" runat="server" Text="Another Button" CssClass="action-button" />
                <asp:Button ID="btnAddFriend" runat="server" Text="Add A Friend" CssClass="action-button" OnClick="btnAddFriend_Click" />
            </div>
            <div id="content2">
                <h3>
                    Active Bets
                </h3>
                <asp:Repeater ID="rptAcceptedBets" runat="server" OnItemDataBound="rptAcceptedBets_ItemDataBound">
    <ItemTemplate>
        <div class="bet-container">
            <div class="bet-header">
                <asp:Label ID="lblSenderUsername" runat="server" Text="" CssClass="sender-name"></asp:Label>
                <span> vs </span>
                <asp:Label ID="lblReceiverUsername" runat="server" Text="" CssClass="receiver-name"></asp:Label>
            </div>
            <div class="bet-description">
                <%# Eval("Description") %>
            </div>
            <div class="bet-stakes">
                $<%# Eval("BetA_Amount", "{0:F2}") %> vs $<%# Eval("BetB_Amount", "{0:F2}") %><br />
                To win: $<%# Eval("Pending_Bet", "{0:F2}") %>
            </div>
            <div class="winner-buttons">
                <asp:Button ID="btnSenderWin" runat="server" CssClass="winner-button sender-win" 
                    CommandName="SenderWin" CommandArgument='<%# Eval("BetID") %>' 
                    OnCommand="BetWinner_Command" />
                <asp:Button ID="btnReceiverWin" runat="server" CssClass="winner-button receiver-win" 
                    CommandName="ReceiverWin" CommandArgument='<%# Eval("BetID") %>' 
                    OnCommand="BetWinner_Command" />
            </div>
            <div class="bet-date">
                Created: <%# Eval("Created_at", "{0:MMM dd, yyyy hh:mm tt}") %>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
