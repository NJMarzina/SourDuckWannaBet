<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBModifyBet.aspx.cs" Inherits="SourDuckWannaBet.WBModifyBet" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/x-icon" href="https://sliykwxeogrnrqgysvrh.supabase.co/storage/v1/object/sign/images/WannaBet_GoldDuck.png?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1cmwiOiJpbWFnZXMvV2FubmFCZXRfR29sZER1Y2sucG5nIiwiaWF0IjoxNzQ3MTg2NTkyLCJleHAiOjE3Nzg3MjI1OTJ9.2wQRIf3mnARn5k25gYARTScThqFjj4NDbLhX-iT27XE" />
    <title>Modify Bet</title>
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
                <label for="txtBetA_Amount">Your (updated) Wager:</label>
                <asp:TextBox ID="txtBetA_Amount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtBetB_Amount">Their (updated) Wager:</label>
                <asp:TextBox ID="txtBetB_Amount" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtReceiver_Result">Your Proposed Outcome:</label>
                <asp:TextBox ID="txtReceiver_Result" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtSender_Result">Their Proposed Outcome:</label>
                <asp:TextBox ID="txtSender_Result" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn" OnClick="btnSave_Click" />
            </div>
        </div>
    </form>
</body>
</html>
