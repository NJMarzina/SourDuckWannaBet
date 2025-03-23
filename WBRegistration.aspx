<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBRegistration.aspx.cs" Inherits="SourDuckWannaBet.WBRegistration" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WBRegistration</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        #container {
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 300px;
            text-align: center;
        }
        h1 {
            color: #333;
            font-size: 24px;
            margin-bottom: 20px;
        }
        label {
            display: block;
            text-align: left;
            margin-bottom: 5px;
            color: #555;
        }
        input[type="text"], input[type="password"], input[type="email"], input[type="tel"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .button {
            width: 100%;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            margin-bottom: 10px;
        }
        .btn-register {
            background-color: gold;
            color: black;
        }
        .btn-login {
            background-color: transparent;
            color: blue;
            text-decoration: underline;
            cursor: pointer;
            border: none;
            font-size: 14px;
            margin-top: 10px;
        }
        .error {
            color: red;
            font-size: 14px;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="frmWBRegistration" runat="server">
        <div id="container">
            <h1>Wanna Bet Registration</h1>
            <p>Fill in the details to create your account.</p>

            <!-- Username -->
            <div>
                <label for="txtUsername">Username:</label>
                <input type="text" id="txtUsername" runat="server" />
            </div>

            <!-- First Name -->
            <div>
                <label for="txtFirstName">First Name:</label>
                <input type="text" id="txtFirstName" runat="server" />
            </div>

            <!-- Last Name -->
            <div>
                <label for="txtLastName">Last Name:</label>
                <input type="text" id="txtLastName" runat="server" />
            </div>

            <!-- Email -->
            <div>
                <label for="txtEmail">Email:</label>
                <input type="email" id="txtEmail" runat="server" />
            </div>

            <!-- Phone Number -->
            <div>
                <label for="txtPhoneNumber">Phone Number:</label>
                <input type="tel" id="txtPhoneNumber" runat="server" />
            </div>

            <!-- Password -->
            <div>
                <label for="txtPassword">Password:</label>
                <input type="password" id="txtPassword" runat="server" />
            </div>

            <!-- Register Button -->
            <div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button btn-register" OnClick="btnRegister_Click" />
            </div>

            <!-- Login Button (if user has an account) -->
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Already have an account? Login" CssClass="button btn-login" OnClick="btnLogin_Click" />
            </div>

            <!-- Error Label for displaying validation messages -->
            <div>
                <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
