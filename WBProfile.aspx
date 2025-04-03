<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBProfile.aspx.cs" Inherits="SourDuckWannaBet.WBProfile" Async="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Profile</title>
    <style>
        /* Global Styles */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7fa;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 50%;
            margin: 30px auto;
            padding: 25px;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            box-sizing: border-box;
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 25px;
            font-size: 24px;
            font-weight: 600;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-weight: 600;
            font-size: 14px;
            color: #555;
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            margin: 0;
            border-radius: 4px;
            border: 1px solid #ddd;
            font-size: 14px;
            color: #333;
            box-sizing: border-box;
            transition: border-color 0.3s ease;
        }

        .form-control:focus {
            outline: none;
            border-color: #4CAF50;
            box-shadow: 0 0 5px rgba(76, 175, 80, 0.4);
        }

        .btn {
            width: 100%;
            padding: 14px;
            background-color: #4CAF50;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #45a049;
        }

        .btn:active {
            background-color: #388e3c;
        }

        /* Responsive Styles */
        @media (max-width: 768px) {
            .container {
                width: 90%;
                padding: 20px;
            }

            h2 {
                font-size: 20px;
            }
        }
    </style>
</head>
<body>
    <form id="frmWBProfile" runat="server">
        <div class="container">
            <h2>Edit Profile</h2>

            <div class="form-group">
                <label for="txtFirstName">First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtLastName">Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPhoneNumber">Phone Number:</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn" OnClick="btnSave_Click" />
            </div>
        </div>
    </form>
</body>
</html>
