<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAcceptedBets.aspx.cs" Inherits="SourDuckWannaBet.ViewAcceptedBets" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Accepted Bets</title>
    <style>
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
        .bet-container {
            margin-top: 60px;
            padding: 20px;
            border: 1px solid #ccc;
            margin-bottom: 10px;
        }
        .bet-description {
            font-weight: bold;
        }
        .bet-stakes {
            margin: 10px 0;
        }
        .winner-buttons {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="frmViewAcceptedBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div>
            <asp:Repeater ID="rptAcceptedBets" runat="server" OnItemCommand="rptAcceptedBets_ItemCommand">
                <ItemTemplate>
                    <div class="bet-container">
                        <div class="bet-description">
                            Description: <%# Eval("Description") %>
                        </div>
                        <div class="bet-stakes">
                            Stakes: <%# Eval("BetA_Amount") %> (Sender) vs <%# Eval("BetB_Amount") %> (Receiver)
                        </div>
                        <div class="winner-buttons">
                            <asp:Button ID="btnSenderWin" runat="server" Text="Sender Wins" CommandName="SelectWinner" CommandArgument='<%# Eval("BetID") + "|" + Eval("UserID_Sender") %>' />
                            <asp:Button ID="btnReceiverWin" runat="server" Text="Receiver Wins" CommandName="SelectWinner" CommandArgument='<%# Eval("BetID") + "|" + Eval("UserID_Receiver") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>

<!--Change this so that if u accept it first it goes from accepted to accepted1
    if accepted1 it will appear first in the view and will show what last person chose
    make a new table called bet_result??
    bet_resultID
    betID
    accepter1ID
    pendingID
    created_at
    updated_at-->