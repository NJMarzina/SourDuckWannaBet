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
                <asp:CheckBox ID="chkNeedMediator" runat="server" Text="Request a mediator for this bet" OnCheckedChanged="chkNeedMediator_CheckedChanged" />
                <asp:TextBox ID="txtMediatorID" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnSendBet" runat="server" Text="Send Bet" CssClass="btn-primary" OnClick="btnSendBet_Click" />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            // Load usernames for autocomplete
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/WBSendABet.aspx/GetUsernames") %>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log("Usernames fetched from server:", data.d); // Log all usernames

                    $(".username-autocomplete").autocomplete({
                        source: data.d,
                        minLength: 2,
                        select: function (event, ui) {
                            console.log("Selected username:", ui.item.value); // Log selected username

                            // When a username is selected, get the corresponding user ID
                            $.ajax({
                                type: "POST",
                                url: '<%=ResolveUrl("~/WBSendABet.aspx/GetUserIDByUsername") %>',
                                data: JSON.stringify({ username: ui.item.value }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    console.log("User ID fetched for username:", ui.item.value, "is", data.d); // Log fetched user ID
                                    $("#<%= hdnRecipientUserID.ClientID %>").val(data.d);
                                    console.log("hdnRecipientUserID set to:", data.d); // Log hidden field value
                                },
                                error: function (xhr, status, error) {
                                    console.error("Error getting user ID: " + error);
                                }
                            });
                        }
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error loading usernames: " + error);
                }
            });
        });
    </script>
</body>
</html>