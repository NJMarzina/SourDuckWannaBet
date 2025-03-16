<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSelectedBets.aspx.cs" Inherits="SourDuckWannaBet.ViewSelectedBets" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Selected Bets</title>
    <style>
        #header {
            width: 100%;
            background-color: gold;
            padding: 10px 0;
            margin: 0;
            text-align: center;
        }
        
        table {
            border-collapse: collapse;
            width: 100%;
            margin-top: 20px;
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
        
        .search-box {
            margin: 20px 0;
            padding: 15px;
            background-color: #f5f5f5;
            border-radius: 5px;
        }
        
        .search-box label {
            font-weight: bold;
            margin-right: 10px;
        }
        
        .search-box input[type="text"] {
            padding: 5px;
            width: 200px;
            margin-right: 10px;
        }
        
        .btn-search {
            background-color: #4CAF50;
            color: white;
            padding: 5px 15px;
            border: none;
            cursor: pointer;
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
        
        .no-results {
            margin-top: 20px;
            font-size: 16px;
            color: #666;
        }
    </style>
</head>
<body>
    <form id="frmViewSelectedBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <h1>View User Bets</h1>
            
            <div class="search-box">
                <asp:Label ID="lblUsername" runat="server" Text="Enter Username:" AssociatedControlID="txtUsername"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-search" />
            </div>
            
            <asp:Panel ID="pnlUserInfo" runat="server" Visible="false">
                <h2>User: <asp:Label ID="lblUserDisplayName" runat="server"></asp:Label></h2>
                <p>User ID: <asp:Label ID="lblUserID" runat="server"></asp:Label></p>
            </asp:Panel>
            
            <asp:Panel ID="pnlBets" runat="server" Visible="false">
                <h3>Bets</h3>
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
                                <asp:Panel ID="pnlActions" runat="server">
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
                <asp:Label ID="lblNoBets" runat="server" Text="No bets found for this user." Visible="false" CssClass="no-results"></asp:Label>
            </asp:Panel>
            
            <asp:Panel ID="pnlNoUser" runat="server" Visible="false">
                <p class="no-results">User not found. Please enter a valid username.</p>
            </asp:Panel>
        </div>
    </form>
</body>
</html>