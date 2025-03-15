<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMyBets.aspx.cs" Inherits="SourDuckWannaBet.ViewMyBets" Async="true" %>
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
            margin-top: 20px;
        }
        .btn-action {
            padding: 5px 10px;
            margin-right: 5px;
        }
        .no-bets {
            color: #777;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>My Pending Bets</h1>
            <div class="grid-container">
                <asp:GridView ID="gvBets" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    OnRowCommand="gvBets_RowCommand" DataKeyNames="BetID">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="From User" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Your Stake" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Their Stake" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Created_at" HeaderText="Date Created" DataFormatString="{0:MM/dd/yyyy}" />
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
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="You have no pending bets." CssClass="no-bets" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>