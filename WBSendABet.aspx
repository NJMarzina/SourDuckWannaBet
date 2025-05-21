<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBSendABet.aspx.cs" Inherits="SourDuckWannaBet.WBSendABet" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WBSend A Bet</title>
    <link rel="icon" type="image/x-icon" href="https://sliykwxeogrnrqgysvrh.supabase.co/storage/v1/object/sign/images/WannaBet_GoldDuck.png?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1cmwiOiJpbWFnZXMvV2FubmFCZXRfR29sZER1Y2sucG5nIiwiaWF0IjoxNzQ3MTg2NTkyLCJleHAiOjE3Nzg3MjI1OTJ9.2wQRIf3mnARn5k25gYARTScThqFjj4NDbLhX-iT27XE" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <style>
        * { box-sizing: border-box; }
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding-top: 70px;
            background-color: #f9f9f9;
        }

        #header {
            width: 100%;
            background-color: gold;
            padding: 10px 0;
            text-align: center;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1000;
        }

        .form-container {
            max-width: 700px;
            margin: 0 auto;
            background-color: #fff;
            padding: 30px 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            display: flex;
            flex-direction: column;
            min-height: 90vh;
        }

        h2 {
            text-align: center;
            margin-bottom: 30px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        label {
            display: block;
            margin-bottom: 6px;
            font-weight: bold;
        }

        input[type="text"], textarea, select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 14px;
        }

        textarea { resize: vertical; }

        .btn-primary {
            background-color: gold;
            color: black;
            border: none;
            padding: 10px 20px;
            border-radius: 4px;
            font-size: 14px;
            cursor: pointer;
        }

        .btn-primary:hover {
            background-color: #e6c200;
        }

        .form-actions {
            margin-top: auto;
            text-align: center;
        }

        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        input[readonly] {
            background-color: #f0f0f0;
            cursor: not-allowed;
        }

        @media (max-width: 600px) {
            .form-container {
                padding: 20px 15px;
                margin: 0 10px;
            }

            .btn-primary {
                width: 100%;
            }

            .form-actions {
                text-align: center;
            }
        }
    </style>
</head>
<body>
    <form id="frmWBSendABet" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Dashboard" OnClick="btnIndex_Click" />
        </div>

        <div class="form-container">
            <h2>Send A Bet</h2>

            <div class="form-group">
                <label for="ddlFriendList">Friend List:</label>
                <asp:DropDownList ID="ddlFriendList" runat="server" OnSelectedIndexChanged="ddlFriendList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtRecipientUsername">Recipient Username:</label>
                <asp:TextBox ID="txtRecipientUsername" runat="server" CssClass="username-autocomplete"></asp:TextBox>
                <asp:HiddenField ID="hdnRecipientUserID" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtBetA_Amount">Your Bet Amount (Bet A):</label>
                <asp:TextBox ID="txtBetA_Amount" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtBetB_Amount">Recipient's Bet Amount (Bet B):</label>
                <asp:TextBox ID="txtBetB_Amount" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtDescription">Bet Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" placeholder="Describe the terms of your bet clearly..."></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtSenderResult">Your Expected Outcome:</label>
                <asp:TextBox ID="txtSenderResult" runat="server" placeholder="e.g., Team A wins, Over 30 points, etc."></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtReceiverResult">Recipient's Expected Outcome:</label>
                <asp:TextBox ID="txtReceiverResult" runat="server" placeholder="e.g., Team B wins, Under 30 points, etc."></asp:TextBox>
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSendBet" runat="server" Text="Send Bet" CssClass="btn-primary" OnClick="btnSendBet_Click" />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
