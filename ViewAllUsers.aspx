<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllUsers.aspx.cs" Inherits="SourDuckWannaBet.ViewAllUsers" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View All Users</title>
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
    </style>
</head>
<body>
    <form id="frmViewAllUsers" runat="server">
        <div class="container">
            <h1>All Users</h1>
            <asp:Panel ID="pnlUsers" runat="server">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="ID" />
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                        <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="NumWins" HeaderText="Wins" />
                        <asp:BoundField DataField="NumLoses" HeaderText="Losses" />
                        <asp:BoundField DataField="NumBets" HeaderText="Total Bets" />
                        <asp:BoundField DataField="CreatedAt" HeaderText="Created" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="UserType" HeaderText="Type" />
                        <asp:BoundField DataField="Subscription" HeaderText="Subscription" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoUsers" runat="server" Text="No users found in the database." Visible="false"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>