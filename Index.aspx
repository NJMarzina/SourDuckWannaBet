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
        </div>

        <div id="left2">
            <h2>Send a Bet</h2>
            <asp:Button ID="btnSendABet" runat="server" Text="Send a Bet" OnClick="btnSendABet_OnClick" />
        </div>

        <div id="left2">
            <h2>View Bets</h2>
            <asp:Button ID="btnViewBets" runat="server" Text="View Bets" OnClick="btnViewBets_OnClick" />
        </div>

        <div id="left">
            <h2>Bulletin:</h2>
            <h5>TODO List:</h5>
            <p>
                Create Data Model
            </p>
            <p>
                Ability to enter usernameA to send bet(message) to usernameB
                Future integration would have usernameA just be the user signed in
            </p>
            <p>
                Ability to view bets sent to you
            </p>
            <p>
                Bet now has money/balance aspect, actually subtracting from users account to be put on hold through the wait for usernameB
            </p>
            <p>
                Ability to accept or deny bet (straight up)
            </p>
            <p>
                Security for bets, adding a mediator, 'are you sure' modifications, can be voided
            </p>
            <p>
                Actual registration and user can remain signed in upon login
            </p>
        </div>
    </form>
</body>
</html>
