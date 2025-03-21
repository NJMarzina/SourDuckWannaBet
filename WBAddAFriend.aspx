<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WBAddAFriend.aspx.cs" Inherits="SourDuckWannaBet.WBAddAFriend" Async="true"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add a Friend</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
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
        
        .container {
            margin-top: 60px;
            padding: 20px;
            max-width: 800px;
            margin-left: auto;
            margin-right: auto;
        }
        
        .section {
            background-color: white;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        
        h2 {
            color: #333;
            margin-top: 0;
        }
        
        .form-group {
            margin-bottom: 15px;
        }
        
        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        
        .btn {
            display: inline-block;
            padding: 8px 15px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        
        .btn-success {
            background-color: #28a745;
        }
        
        .btn-danger {
            background-color: #dc3545;
        }
        
        .status {
            color: #28a745;
            margin: 10px 0;
        }
        
        .error {
            color: #dc3545;
            margin: 10px 0;
        }
        
        .friend-item, .request-item {
            padding: 10px;
            border-bottom: 1px solid #eee;
        }
        
        .friend-item:last-child, .request-item:last-child {
            border-bottom: none;
        }
        
        .button-group {
            text-align: right;
        }
        
        .empty-message {
            color: #666;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="frmWBAddAFriend" runat="server">
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Dashboard" OnClick="btnIndex_Click" CssClass="btn" />
        </div>
        
        <div class="container">
            <!-- Send Friend Request Section -->
            <div class="section">
                <h2>Send Friend Request</h2>
                <div class="form-group">
                    <label for="txtFriendUsername">Friend's Username:</label>
                    <asp:TextBox ID="txtFriendUsername" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSendRequest" runat="server" Text="Send Request" OnClick="btnSendRequest_Click" CssClass="btn btn-success" />
                </div>
                <asp:Label ID="lblStatus" runat="server" CssClass="status"></asp:Label>
            </div>
            
            <!-- Pending Friend Requests Section -->
            <div class="section">
                <h2>Pending Friend Requests</h2>
                <asp:Label ID="lblPendingError" runat="server" CssClass="error"></asp:Label>
                
                <asp:Repeater ID="rptPendingRequests" runat="server">
                    <HeaderTemplate>
                        <div class="request-list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="request-item">
                            <div>
                                <strong><%# Eval("SenderUsername") %></strong> wants to be your friend
                                <br />
                                <small>Sent on: <%# Eval("CreatedAt") %></small>
                            </div>
                            <div class="button-group">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" 
                                    CommandName="Accept" CommandArgument='<%# Eval("FriendID") %>' 
                                    OnCommand="btnAccept_Command" CssClass="btn btn-success" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" 
                                    CommandName="Reject" CommandArgument='<%# Eval("FriendID") %>' 
                                    OnCommand="btnReject_Command" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            
            <!-- Friends List Section -->
            <div class="section">
                <h2>My Friends</h2>
                <asp:Label ID="lblFriendsError" runat="server" CssClass="error"></asp:Label>
                
                <asp:Repeater ID="rptFriends" runat="server">
                    <HeaderTemplate>
                        <div class="friends-list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="friend-item">
                            <strong><%# Eval("FriendUsername") %></strong>
                            <br />
                            <small>Friends since: <%# Eval("AcceptDate") %></small>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>

<!-- had emptydatatemplate for friends lines 154 and 178-->