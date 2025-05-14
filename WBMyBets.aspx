<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBMyBets.aspx.cs" Inherits="SourDuckWannaBet.WBMyBets" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="image/x-icon" href="https://sliykwxeogrnrqgysvrh.supabase.co/storage/v1/object/sign/images/WannaBet_GoldDuck.png?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1cmwiOiJpbWFnZXMvV2FubmFCZXRfR29sZER1Y2sucG5nIiwiaWF0IjoxNzQ3MTg2NTkyLCJleHAiOjE3Nzg3MjI1OTJ9.2wQRIf3mnARn5k25gYARTScThqFjj4NDbLhX-iT27XE" />
    <title>My Bets</title>
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
    <form id="frmWBMyBets" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Dashboard" OnClick="btnIndex_Click" />
        </div>
        <div class="container">
            <h1>My Bets</h1>
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
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblNoBets" runat="server" Text="You don't have any bets yet." Visible="false"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
