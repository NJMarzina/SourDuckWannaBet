<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarzyBlog.aspx.cs" Inherits="SourDuckWannaBet.MarzyBlog" Async="true"%>

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
            width: 65%;
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
            text-align: left;
        }

        /* Blog Post Styling (Flexbox for body and image) */
        .blog-post {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 40px; /* Increased space between posts */
            padding: 30px;
            background-color: #f5f5f5;
            border-radius: 12px; /* More rounded corners */
            border: 1px solid #ddd;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1); /* Drop shadow */
        }

        .blog-body {
            flex: 1;
            margin-right: 20px;
        }

        .blog-image {
            flex-basis: 25%;
            max-width: 25%;
            height: auto;
            margin-left: 20px;
            border-radius: 5px;
            object-fit: cover;
            text-align: justify;
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

        /* New blog post form styling */
        .blog-form {
            background-color: #f5f5f5;
            padding: 20px;
            margin-bottom: 30px;
            border-radius: 5px;
            border: 1px solid #ddd;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .form-control-textarea {
            min-height: 150px;
        }

        .btn-primary {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 1em;
            cursor: pointer;
            border-radius: 5px;
        }

        .btn-primary:hover {
            background-color: #218838;
        }

        .status-message {
            padding: 10px;
            margin-top: 10px;
            border-radius: 5px;
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
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

            <!-- New Blog Post Form -->
            <div class="blog-form">
                <h3>Add New Blog Entry</h3>

                <asp:Label ID="lblStatus" runat="server" CssClass="status-message" Visible="false"></asp:Label>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtHeader">Header (Optional)</asp:Label>
                    <asp:TextBox ID="txtHeader" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtBody">Content</asp:Label>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" CssClass="form-control form-control-textarea"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtImageUrl">Image URL (Optional)</asp:Label>
                    <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Button ID="btnAddPost" runat="server" Text="Add Entry" CssClass="btn-primary" OnClick="btnAddPost_Click" />
            </div>

            <!-- Blog Content -->
            <asp:PlaceHolder ID="blogContent" runat="server">
                <!-- Example Blog Post 1 -->
                <div class="blog-post">
                    <div class="blog-body">
                        <h3>Entry 2: 3/16/2025 @9:21pm</h3>
                        <p>
                            He was coding while simultaneously drinking and blogging! Nah well I'm not drinking, but I do like this little blog page.
                            I just finished making this button on the index page which allows us to backup all of the users from the users table,
                            and copies any new users and updates previous users in the users_backup table.
                        </p>
                    </div>
                    <img class="blog-image" src="~/images/selfie1.jpg" alt="Selfie" />
                </div>

                <!-- Example Blog Post 2 -->
                <div class="blog-post">
                    <div class="blog-body">
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
                            <li>MarzyBlog.aspx</li>
                        </ul>

                        <h4>Controllers</h4>
                        <ul>
                            <li>UsersController.cs</li>
                            <li>BetsController.cs</li>
                            <li>TransactionsController.cs</li>
                            <li>NotificationsController.cs</li>
                            <li>MessagesController.cs</li>
                        </ul>

                        <h4>Utilities</h4>
                        <ul>
                            <li>SupabaseServices.cs</li>
                        </ul>
                    </div>
                    <img class="blog-image" src="~/images/selfie2.jpg" alt="Selfie 2" />
                </div>
            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
