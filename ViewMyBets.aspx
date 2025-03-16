<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMyBets.aspx.cs" Inherits="SourDuckWannaBet.ViewMyBets" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Pending Bets</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        h1 {
            color: #333;
        }
        .grid-container {
            margin-top: 50px;
        }
        .btn-action {
            padding: 5px 10px;
            margin-right: 5px;
        }
        .no-bets {
            color: #777;
            font-style: italic;
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
    <form id="frmViewPendingBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div class="grid-container">
            <h1>My Pending Bets</h1>
            <div>
                <asp:GridView ID="gvBets" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowCommand="gvBets_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="From User" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Your Stake" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Their Stake" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="btn-action"
                                    CommandName="AcceptBet" CommandArgument='<%# Eval("BetID") %>' 
                                    Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                                <asp:Button ID="btnDeny" runat="server" Text="Deny" CssClass="btn-action"
                                    CommandName="DenyBet" CommandArgument='<%# Eval("BetID") %>' 
                                    Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="You have no pending bets." CssClass="no-bets" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>