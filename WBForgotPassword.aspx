<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBForgotPassword.aspx.cs" Inherits="SourDuckWannaBet.WBForgotPassword" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/x-icon" href="https://sliykwxeogrnrqgysvrh.supabase.co/storage/v1/object/sign/images/WannaBet_GoldDuck.png?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1cmwiOiJpbWFnZXMvV2FubmFCZXRfR29sZER1Y2sucG5nIiwiaWF0IjoxNzQ3MTg2NTkyLCJleHAiOjE3Nzg3MjI1OTJ9.2wQRIf3mnARn5k25gYARTScThqFjj4NDbLhX-iT27XE" />
    <title>Forgot Password</title>
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
        input[type="text"], input[type="email"] {
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
        .btn-recover {
            background-color: gold;
            color: black;
        }
        .btn-back {
            background-color: transparent;
            color: blue;
            text-decoration: underline;
            cursor: pointer;
            border: none;
            font-size: 14px;
            margin-top: 10px;
        }
        .error-message {
            color: red;
            margin-bottom: 10px;
        }
        .success-message {
            color: green;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="frmForgotPassword" runat="server">
        <div id="container">
            <h1>Wanna Bet</h1>
            <p>Enter your email address to recover your password.</p>
            <div>
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            </div>
            <div>
                <label for="txtEmail">Email Address:</label>
                <input type="email" id="txtEmail" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnRecover" runat="server" Text="Recover Password" CssClass="button btn-recover" OnClick="btnRecover_Click" />
            </div>
            <div>
                <asp:Button ID="btnBackToLogin" runat="server" Text="Back to Login" CssClass="btn-back" OnClick="btnBackToLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>