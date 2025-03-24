<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBModifyBet.aspx.cs" Inherits="SourDuckWannaBet.WBModifyBet" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify Bet</title>
    <style>
        .container {
            width: 50%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-control {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }
        .btn {
            padding: 10px 15px;
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 4px;
        }
    </style>
</head>
<body>
    <form id="frmWBModifyBet" runat="server">
        <div class="container">
            <h2>Modify Bet</h2>
            
            <div class="form-group">
                <label>Bet Description:</label>
                <asp:Label ID="lblBetDescription" runat="server" Text="[Bet Description]"></asp:Label>
            </div>
            
            <div class="form-group">
                <label>Participants:</label>
                <asp:Label ID="lblSenderVsReceiver" runat="server" Text="[Sender vs Receiver]"></asp:Label>
            </div>
            
            <div class="form-group">
                <label for="txtBetA_Amount">Your Bet Amount:</label>
                <asp:TextBox ID="txtBetA_Amount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtBetB_Amount">Their Bet Amount:</label>
                <asp:TextBox ID="txtBetB_Amount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtSender_Result">Your Predicted Result:</label>
                <asp:TextBox ID="txtSender_Result" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtReceiver_Result">Their Predicted Result:</label>
                <asp:TextBox ID="txtReceiver_Result" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn" OnClick="btnSave_Click" />
            </div>
        </div>
    </form>
</body>
</html>