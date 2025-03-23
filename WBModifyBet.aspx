<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBModifyBet.aspx.cs" Inherits="SourDuckWannaBet.WBModifyBet" Async="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify Bet</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }
        .container {
            width: 70%;
            margin: 50px auto;
            padding: 20px;
            background-color: white;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        h1 {
            text-align: center;
            color: #333;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }
        input[type="text"], input[type="number"], textarea {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }
        textarea {
            resize: vertical;
            min-height: 100px;
        }
        .btn {
            padding: 10px 20px;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .btn-secondary {
            background-color: #6c757d;
        }
        .btn-secondary:hover {
            background-color: #5a6268;
        }
        .error {
            color: red;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="frmWBModifyBet" runat="server">
        <div class="container">
            <h1>Modify Bet</h1>
            <asp:HiddenField ID="hiddenBetID" runat="server" />
            <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" />

            <div class="form-group">
                <label for="txtBetAmountA">Bet A Amount</label>
                <input type="number" id="txtBetAmountA" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtBetAmountB">Bet B Amount</label>
                <input type="number" id="txtBetAmountB" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description</label>
                <textarea id="txtDescription" runat="server"></textarea>
            </div>

            <div class="form-group">
                <label for="txtSenderResult">Sender Result</label>
                <input type="text" id="txtSenderResult" runat="server" />
            </div>

            <div class="form-group">
                <label for="txtReceiverResult">Receiver Result</label>
                <input type="text" id="txtReceiverResult" runat="server" />
            </div>

            <div class="form-group">
                <label for="ddlStatus">Bet Status</label>
                <asp:DropDownList id="ddlStatus" runat="server">
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    <asp:ListItem Value="Accepted">Accepted</asp:ListItem>
                    <asp:ListItem Value="Declined">Declined</asp:ListItem>
                </asp:DropDownList>

            </div>

            <div class="form-group" style="text-align: center;">
                <button type="button" class="btn" runat="server" OnClick="btnUpdateBet_Click">Update Bet</button>
                <button type="button" class="btn btn-secondary" runat="server" OnClick="btnCancel_Click">Cancel</button>
            </div>
        </div>
    </form>
</body>
</html>
