<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarzyBlog.aspx.cs" Inherits="SourDuckWannaBet.MarzyBlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marzy Blog</title>
    <style>
        /* Header style (unchanged) */
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

        /* Body styling */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f9f9f9;
            color: #333;
            line-height: 1.6;
        }

        /* Container styling */
        .container {
            width: 80%;
            max-width: 1200px;
            margin: 80px auto 20px; /* Adjusted for header */
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        /* Headings */
        h1 {
            font-size: 2.5em;
            margin-bottom: 10px;
            color: #222;
        }

        h2 {
            font-size: 1.8em;
            margin-bottom: 15px;
            color: #444;
        }

        h3 {
            font-size: 1.5em;
            margin-top: 20px;
            margin-bottom: 10px;
            color: #555;
        }

        h4 {
            font-size: 1.2em;
            margin-top: 15px;
            margin-bottom: 10px;
            color: #666;
        }

        /* Paragraphs */
        p {
            margin-bottom: 20px;
            font-size: 1em;
            color: #333;
        }

        /* Lists */
        ul, li {
            margin-bottom: 10px;
            padding-left: 20px;
        }

        ul {
            list-style-type: disc;
        }

        li {
            font-size: 1em;
            color: #333;
        }

        /* Button styling */
        #btnIndex {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 1em;
            cursor: pointer;
            border-radius: 5px;
        }

        #btnIndex:hover {
            background-color: #0056b3;
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
            <h3>Entry 1: 3/16/2025 @8:05pm</h3>
            <p>
                This is really me being Zuckkk. I have been tracking all of the progress thus far via GitHub,
                but I really want to be able to see visuals and track what's been going on day-to-day
                and not have to scroll through my git. So basically, we have a bunch of pages, controllers, and models created.
            </p>

            <h4>Models</h4>
            <ul>
                <li>User.cs</li>
                <li>Bet.cs</li>
                <li>Transaction.cs</li>
                <li>Notification.cs</li>
                <li>Mediation.cs</li>
            </ul>

            <h4>.aspx Pages</h4>
            <ul>
                <li>Index.aspx</li>
                <li>ViewAllUsers.aspx</li>
                <li>SendABet.aspx</li>
                <li>ViewAllBets.aspx</li>
                <li>ViewMyBets.aspx</li>
                <li>ViewSelectedBets.aspx</li>
                <li>BetsControllerDemos.aspx</li>
            </ul>

            <h4>Controllers</h4>
            <ul>
                <li>UsersController.cs</li>
                <li>BetsController.cs</li>
                <li>TransactionsController.cs</li>
                <li>NotificationsController.cs</li>
            </ul>

            <h4>Utilities</h4>
            <ul>
                <li>SupabaseServices.cs</li>
            </ul>
        </div>
    </form>
</body>
</html>