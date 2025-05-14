<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBSendABet.aspx.cs" Inherits="SourDuckWannaBet.WBSendABet" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WBSend A Bet</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .form-container {
            max-width: 600px;
            margin: 0 auto;
            margin-top: 50px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"], textarea, select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            overflow-x: hidden;
        }
        #header {
            width: 100%;
            background-color: gold;
            padding: 10px 0;
            margin: 0;
            text-align: center;
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
        }
        input[readonly] {
            background-color: #f0f0f0;
            cursor: not-allowed;
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
                <label for="txtFriendList">Friend List:</label>
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
            
            
            
            <div class="form-group">
                <asp:Button ID="btnSendBet" runat="server" Text="Send Bet" CssClass="btn-primary" OnClick="btnSendBet_Click" />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>