<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetsControllerDemos.aspx.cs" Inherits="SourDuckWannaBet.BetsControllerDemos" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetsControllerDemos</title>
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
        .container {
            margin-top: 50px;
        }
        .section {
            margin-bottom: 20px;
        }
        .section h2 {
            margin-bottom: 10px;
        }
        .section input[type="text"], .section input[type="number"] {
            margin-right: 10px;
            margin-bottom: 10px;
            display: block;
        }
        .section button {
            margin-right: 10px;
        }
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
    </style>
</head>
<body>
    <form id="frmBetsControllerDemos" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <div class="section">
                <h2>Get All Bets</h2>
                <asp:Button ID="btnGetAllBets" runat="server" Text="Get All Bets" OnClick="btnGetAllBets_Click" />
                <asp:GridView ID="gvAllBets" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="Sender ID" />
                        <asp:BoundField DataField="UserID_Receiver" HeaderText="Receiver ID" />
                        <asp:BoundField DataField="Created_at" HeaderText="Created At" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Bet A Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Bet B Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Pending_Bet" HeaderText="Pending Bet" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Sender_Result" HeaderText="Sender Result" />
                        <asp:BoundField DataField="Receiver_Result" HeaderText="Receiver Result" />
                        <asp:BoundField DataField="Sender_Balance_Change" HeaderText="Sender Balance Change" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Receiver_Balance_Change" HeaderText="Receiver Balance Change" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="UserID_Mediator" HeaderText="Mediator ID" />
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Updated At" DataFormatString="{0:d}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="No bets found." Visible="false"></asp:Label>
            </div>

            <div class="section">
                <h2>Get Bets by User ID</h2>
                <asp:TextBox ID="txtUserId" runat="server" placeholder="Enter User ID"></asp:TextBox>
                <asp:Button ID="btnGetBetsByUserId" runat="server" Text="Get Bets" OnClick="btnGetBetsByUserId_Click" />
                <asp:GridView ID="gvBetsByUserId" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="BetID" HeaderText="Bet ID" />
                        <asp:BoundField DataField="UserID_Sender" HeaderText="Sender ID" />
                        <asp:BoundField DataField="UserID_Receiver" HeaderText="Receiver ID" />
                        <asp:BoundField DataField="Created_at" HeaderText="Created At" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="BetA_Amount" HeaderText="Bet A Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="BetB_Amount" HeaderText="Bet B Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Pending_Bet" HeaderText="Pending Bet" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Sender_Result" HeaderText="Sender Result" />
                        <asp:BoundField DataField="Receiver_Result" HeaderText="Receiver Result" />
                        <asp:BoundField DataField="Sender_Balance_Change" HeaderText="Sender Balance Change" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Receiver_Balance_Change" HeaderText="Receiver Balance Change" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="UserID_Mediator" HeaderText="Mediator ID" />
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Updated At" DataFormatString="{0:d}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoBetsByUserId" runat="server" Text="No bets found for this user." Visible="false"></asp:Label>
            </div>

            <div class="section">
                <h2>Add Bet</h2>
                <asp:TextBox ID="txtBetSenderId" runat="server" placeholder="Sender ID"></asp:TextBox>
                <asp:TextBox ID="txtBetReceiverId" runat="server" placeholder="Receiver ID"></asp:TextBox>
                <asp:TextBox ID="txtBetA_Amount" runat="server" placeholder="Bet A Amount"></asp:TextBox>
                <asp:TextBox ID="txtBetB_Amount" runat="server" placeholder="Bet B Amount"></asp:TextBox>
                <asp:TextBox ID="txtPendingBet" runat="server" placeholder="Pending Bet"></asp:TextBox>
                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description"></asp:TextBox>
                <asp:TextBox ID="txtStatus" runat="server" placeholder="Status"></asp:TextBox>
                <asp:TextBox ID="txtSenderResult" runat="server" placeholder="Sender Result"></asp:TextBox>
                <asp:TextBox ID="txtReceiverResult" runat="server" placeholder="Receiver Result"></asp:TextBox>
                <asp:TextBox ID="txtSenderBalanceChange" runat="server" placeholder="Sender Balance Change"></asp:TextBox>
                <asp:TextBox ID="txtReceiverBalanceChange" runat="server" placeholder="Receiver Balance Change"></asp:TextBox>
                <asp:TextBox ID="txtUserIDMediator" runat="server" placeholder="Mediator ID"></asp:TextBox>
                <asp:Button ID="btnAddBet" runat="server" Text="Add Bet" OnClick="btnAddBet_Click" />
                <asp:Label ID="lblAddBetResult" runat="server" Text=""></asp:Label>
            </div>

            <div class="section">
                <h2>Update Bet Status</h2>
                <asp:TextBox ID="txtBetId" runat="server" placeholder="Enter Bet ID"></asp:TextBox>
                <asp:DropDownList ID="ddlBetStatus" runat="server">
                    <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                    <asp:ListItem Text="Denied" Value="Denied"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnUpdateBetStatus" runat="server" Text="Update Status" OnClick="btnUpdateBetStatus_Click" />
                <asp:Label ID="lblUpdateBetStatusResult" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>