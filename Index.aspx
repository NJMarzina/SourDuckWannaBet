<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SourDuckWannaBet.Index" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marzy SourDuck WannaBet Index</title>
    <style>
    table {
        border-collapse: collapse;
        width: 100%;
    }
    #left {
        float: left;
        width: 20%;
    }
    #left2 {
        float: left;
        width: 10%;
        text-align: center;
        padding: 1rem, 1rem, 1rem, 1rem;
    }
    #left3 {
        float: left;
        width: 15%;
        text-align: center;
        padding: 1rem, 1rem, 1rem, 1rem;
    }
    #right {
        float: right;
    }
</style>
</head>
<body>
    <form id="frmIndex" runat="server">
        <div>
            <h1>Index designed as a portal for developers to view all processes in WannaBet.</h1>
        </div>
            
        <div id="left">
            <h2>User Registration</h2>
            <p>Enter user details and click 'Register' to add to the database.</p>

            <table>
                <tr>
                    <td><label for="txtUsername">Username:</label></td>
                    <td><input type="text" id="txtUsername" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtPassword">Password:</label></td>
                    <td><input type="password" id="txtPassword" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtFirstName">First Name:</label></td>
                    <td><input type="text" id="txtFirstName" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtLastName">Last Name:</label></td>
                    <td><input type="text" id="txtLastName" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtEmail">Email:</label></td>
                    <td><input type="email" id="txtEmail" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtPhoneNumber">Phone Number:</label></td>
                    <td><input type="text" id="txtPhoneNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtBalance">Balance:</label></td>
                    <td><input type="text" id="txtBalance" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtNumWins">Number of Wins:</label></td>
                    <td><input type="text" id="txtNumWins" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtNumLoses">Number of Loses:</label></td>
                    <td><input type="text" id="txtNumLoses" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtNumBets">Number of Bets:</label></td>
                    <td><input type="text" id="txtNumBets" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtUserType">User Type:</label></td>
                    <td><input type="text" id="txtUserType" runat="server" /></td>
                </tr>
                <tr>
                    <td><label for="txtSubscription">Subscription:</label></td>
                    <td><input type="text" id="txtSubscription" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <!-- Change from <input> to <asp:Button> -->
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="RegisterButton_OnClick" />
                    </td>
                </tr>
            </table>

            <div id="message"></div> <!-- Message area to display success or failure -->
        </div>

        <div id="left2">
            <h2>View All Users</h2>
            <asp:Button ID="btnViewAllUsers" runat="server" Text="View All Users" OnClick="btnViewAllUsers_OnClick" />
            <p>View all users and their information</p>
        </div>

        <div id="left2">
            <h2>Send a Bet</h2>
            <asp:Button ID="btnSendABet" runat="server" Text="Send a Bet" OnClick="btnSendABet_OnClick" />
            <p>Send a bet from username A to username B</p>
        </div>

        <div id="left2">
            <h2>View All Bets</h2>
            <asp:Button ID="btnViewAllBets" runat="server" Text="View All Bets" OnClick="btnViewAllBets_OnClick" />
            <p>View all bets in database</p>
            <p>Can accept/deny</p>
        </div>

        <div id="left2">
            <h2>View My Bets</h2>
            <asp:Button ID="btnViewBets" runat="server" Text="View My Bets" OnClick="btnViewBets_OnClick" />
            <p>View all bets in database associated with userID=1</p>
            <p>Can accept/deny</p>
        </div>

        <div id="left3">
            <h2>View Selected Bets</h2>
            <asp:Button ID="btnViewSelectedBets" runat="server" Text="View Selected Bets" OnClick="btnViewSelectedBets_OnClick" />
            <p>View bets of specific Username</p>
            <p>Can accept/deny</p>
        </div>

        <div id="left3">
            <h2>BetsController.cs Demos</h2>
            <asp:Button ID="btnBetsControllerDemos" runat="server" Text="View BetsController" OnClick="btnBetsControllerDemos_OnClick" />
        </div>

        <div id="left2">
            <h2>Backup Users Table</h2>
            <asp:Button ID="btnBackupUsersTable" runat="server" Text="Backup Users" OnClick="btnBackupUsersTable_OnClick" />
            <p>Backup all info in users and duplicate it into users_backup</p>
        </div>

        <div id="left2">
            <asp:Button ID="btnMarzyBlog" runat="server" Text="Dev Log" OnClick="btnMarzyBlog_OnClick" />
        </div>

        <div id="left">
            <h2>Bulletin:</h2>
            <h5>TODO List:</h5>
            <p>
                Add a button on this index which just takes the users table, and duplicates (backs up) it into the users_backup table
            </p>
            <p>
                Bet now has money/balance aspect, actually subtracting from users account to be put on hold through the wait for usernameB
            </p>
            <p>
                Adding +1 to amount of bet if the bet is accepted for both users
            </p>
            <p>
                Security for bets, adding a mediator, 'are you sure' modifications, can be voided
            </p>
            <p>
                Actual registration and user can remain signed in upon login
            </p>
            <p>
                Add all actions in BetsController to BetsControllerDemos.aspx
            </p>
        </div>

        
    </form>
</body>
</html>
<!--a nathan marzina production.-->
