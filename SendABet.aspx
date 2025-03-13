<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendABet.aspx.cs" Inherits="SourDuckWannaBet.SendABet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send A Bet</title>
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
        .bet-type-container {
            margin-top: 15px;
            padding: 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
            background-color: #f9f9f9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Send A Bet</h2>
            
            <div class="form-group">
                <label for="txtRecipientUsername">Recipient Username:</label>
                <asp:TextBox ID="txtRecipientUsername" runat="server" CssClass="username-autocomplete"></asp:TextBox>
                <asp:HiddenField ID="hdnRecipientUserID" runat="server" />
            </div>
            
            <div class="form-group">
                <label for="ddlBetType">Bet Type:</label>
                <asp:DropDownList ID="ddlBetType" runat="server" AutoPostBack="false">
                    <asp:ListItem Text="One-sided Bet" Value="one-sided" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Two-sided Bet" Value="two-sided"></asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <div id="oneSidedBetContainer" class="bet-type-container">
                <div class="form-group">
                    <label for="txtSenderAmount">Your Bet Amount:</label>
                    <asp:TextBox ID="txtSenderAmount" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                </div>
            </div>
            
            <div id="twoSidedBetContainer" class="bet-type-container" style="display:none;">
                <div class="form-group">
                    <label for="txtSenderAmount2">Your Bet Amount (Bet A):</label>
                    <asp:TextBox ID="txtSenderAmount2" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtReceiverAmount">Recipient's Bet Amount (Bet B):</label>
                    <asp:TextBox ID="txtReceiverAmount" runat="server" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <label for="txtDescription">Bet Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" placeholder="Describe the terms of your bet clearly..."></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="ddlSenderResult">Your Expected Outcome:</label>
                <asp:TextBox ID="txtSenderResult" runat="server" placeholder="e.g., Team A wins, Over 30 points, etc."></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="ddlReceiverResult">Recipient's Expected Outcome:</label>
                <asp:TextBox ID="txtReceiverResult" runat="server" placeholder="e.g., Team B wins, Under 30 points, etc."></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:CheckBox ID="chkNeedMediator" runat="server" Text="Request a mediator for this bet" />
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
                url: '<%=ResolveUrl("~/SendABet.aspx/GetUsernames") %>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $(".username-autocomplete").autocomplete({
                        source: data.d,
                        minLength: 2,
                        select: function(event, ui) {
                            // When a username is selected, get the corresponding user ID
                            $.ajax({
                                type: "POST",
                                url: '<%=ResolveUrl("~/SendABet.aspx/GetUserIDByUsername") %>',
                                data: JSON.stringify({ username: ui.item.value }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    $("#<%= hdnRecipientUserID.ClientID %>").val(data.d);
                                }
                            });
                        }
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error loading usernames: " + error);
                }
            });
            
            // Toggle bet type containers
            $("#<%= ddlBetType.ClientID %>").change(function() {
                if ($(this).val() === "one-sided") {
                    $("#oneSidedBetContainer").show();
                    $("#twoSidedBetContainer").hide();
                } else {
                    $("#oneSidedBetContainer").hide();
                    $("#twoSidedBetContainer").show();
                }
            });
        });
    </script>
</body>
</html>