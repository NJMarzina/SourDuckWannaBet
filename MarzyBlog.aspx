<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarzyBlog.aspx.cs" Inherits="SourDuckWannaBet.MarzyBlog" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marzy Blog</title>
    <style>
        /* Existing CSS styles... */

        /* New styles for the message board */
        .message-form {
            margin-bottom: 20px;
        }

        .message-form input, .message-form textarea {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .message-form button {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 1em;
            cursor: pointer;
            border-radius: 5px;
        }

        .message-form button:hover {
            background-color: #0056b3;
        }

        .message-board {
            margin-top: 20px;
        }

        .message {
            background-color: #f9f9f9;
            padding: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .message h3 {
            margin-top: 0;
        }

        .message img {
            max-width: 100%;
            height: auto;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="frmMarzyBlog" runat="server">
        <!-- Header -->
        <div id="header">
            <asp:Button ID="btnIndex" runat="server" Text="Index" OnClick="btnIndex_Click" />
        </div>

        <!-- Main Content Container -->
        <div class="container">
            <h1>Dev Log</h1>
            <h2>a nathan marzina production</h2>

            <!-- Message Form -->
            <div class="message-form">
                <h3>Add a New Message</h3>
                <asp:TextBox ID="txtHeader" runat="server" placeholder="Header"></asp:TextBox>
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="5" placeholder="Body"></asp:TextBox>
                <asp:TextBox ID="txtImageUrl" runat="server" placeholder="Image URL"></asp:TextBox>
                <asp:Button ID="btnAddMessage" runat="server" Text="Add Message" OnClick="btnAddMessage_Click" />
            </div>

            <!-- Message Board -->
            <div class="message-board">
                <h3>Messages</h3>
                <asp:Repeater ID="rptMessages" runat="server">
                    <ItemTemplate>
                        <div class="message">
                            <h3><%# Eval("Header") %></h3>
                            <p><%# Eval("Body") %></p>
                            <asp:Image ID="imgMessage" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Visible='<%# !string.IsNullOrEmpty(Eval("ImageUrl").ToString()) %>' />
                            <p><small>Posted on: <%# Eval("CreatedAt", "{0:MM/dd/yyyy HH:mm}") %></small></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>