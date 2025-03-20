﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBLogin.aspx.cs" Inherits="SourDuckWannaBet.WBLogin" Async="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WBLogin</title>
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
        p {
            color: #555;
            margin-bottom: 20px;
        }
        label {
            display: block;
            text-align: left;
            margin-bottom: 5px;
            color: #555;
        }
        input[type="text"], input[type="password"] {
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
        .btn-login {
            background-color: gold;
            color: black;
        }
        .btn-register {
            background-color: gold;
            color: black;
        }
        .btn-forgot {
            background-color: transparent;
            color: blue;
            text-decoration: underline;
            cursor: pointer;
            border: none;
            font-size: 14px;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="frmWBLogin" runat="server">
        <div id="container">
            <h1>Wanna Bet</h1>
            <p>Enter your username and password to login.</p>
            <div>
                <label for="txtUsername">Username:</label>
                <input type="text" id="txtUsername" runat="server" />
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <input type="password" id="txtPassword" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button btn-login" OnClick="btnLogin_Click" />
            </div>
            <div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button btn-register" OnClick="btnRegister_Click" />
            </div>
            <div>
                <asp:Button ID="btnForgotPassword" runat="server" Text="Forgot Password?" CssClass="btn-forgot" OnClick="btnForgotPassword_Click" />
            </div>
        </div>
    </form>
</body>
</html>
