<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMyBets.aspx.cs" Inherits="SourDuckWannaBet.ViewMyBets" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View My Bets</title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #f2f2f2;
        }
        tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        tr:hover {
            background-color: #eaeaea;
        }
        .container {
            margin: 20px;
        }
        h1 {
            color: #333;
        }
        .action-buttons {
            display: flex;
            gap: 5px;
        }
    </style>
</head>
<body>
    <form id="frmViewMyBets" runat="server">
        <div class="container">
            <h1>My Received Bets</h1>
            <asp:Panel ID="pnlBets" runat="server">
                <asp:GridView ID="gvBets" runat="server" AutoGenerateColumns="false" CssClass="table" OnRowCommand="gvBets_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="Sender ID" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Bet A Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Bet B Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("BetID") %>' CssClass="btn-accept" />
                                    <asp:Button ID="btnDeny" runat="server" Text="Deny" CommandName="Deny" CommandArgument='<%# Eval("BetID") %>' CssClass="btn-deny" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="No bets found for you." Visible="false"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>