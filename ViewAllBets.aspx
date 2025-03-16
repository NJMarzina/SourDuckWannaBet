<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllBets.aspx.cs" Inherits="SourDuckWannaBet.ViewAllBets" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View All Bets</title>
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
            margin-top: 50px;
        }
        h1 {
            color: #333;
        }
        .btn-accept {
            background-color: #4CAF50;
            color: white;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
            margin-right: 5px;
        }
        .btn-deny {
            background-color: #f44336;
            color: white;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
        }
        .status-pending {
            color: #ff9800;
            font-weight: bold;
        }
        .status-accepted {
            color: #4CAF50;
            font-weight: bold;
        }
        .status-denied {
            color: #f44336;
            font-weight: bold;
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
    <form id="frmViewAllBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <h1>All Bets</h1>
            <asp:Panel ID="pnlBets" runat="server">
                <asp:GridView ID="gvBets" runat="server" AutoGenerateColumns="false" CssClass="table" 
                    OnRowCommand="gvBets_RowCommand" OnRowDataBound="gvBets_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="Sender ID" />
                        <asp:BoundField DataField="UserID_Receiver" HeaderText="Receiver ID" />
                        <asp:BoundField DataField="Created_at" HeaderText="Created" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Bet A Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Bet B Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Pending_Bet" HeaderText="Pending Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Sender_Result" HeaderText="Sender Result" />
                        <asp:BoundField DataField="Receiver_Result" HeaderText="Receiver Result" />
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Last Updated" DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Panel ID="pnlActions" runat="server" Visible='<%# Eval("Status").ToString() == "Pending" %>'>
                                    <asp:Button ID="btnAccept" runat="server" Text="Accept" 
                                        CommandName="AcceptBet" 
                                        CommandArgument='<%# Eval("BetID") %>' 
                                        CssClass="btn-accept" />
                                    <asp:Button ID="btnDeny" runat="server" Text="Deny" 
                                        CommandName="DenyBet" 
                                        CommandArgument='<%# Eval("BetID") %>' 
                                        CssClass="btn-deny" />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="No bets found in the database." Visible="false"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>