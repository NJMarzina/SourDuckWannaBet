<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SourDuckWannaBet.Index" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marzy SourDuck WannaBet Index</title>
</head>
<body>
    <form id="frmIndex" runat="server">
        <div>
            <h1>Index designed as a portal for developers to view all processes in WannaBet.</h1>

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

            <h3>View All Users</h3>
            <asp:Button ID="btnViewAllUsers" runat="server" Text="View All Users" OnClick="btnViewAllUsers_OnClick" />

        </div>
    </form>
</body>
</html>
