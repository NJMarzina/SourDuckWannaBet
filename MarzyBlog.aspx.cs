using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace SourDuckWannaBet
{
    public partial class MarzyBlog : System.Web.UI.Page
    {
        private MessagesController _messagesController;

        protected void Page_Load(object sender, EventArgs e)
        {
            _messagesController = new MessagesController();

            if (!IsPostBack)
            {
                // Load messages from the session or database
                LoadBlogMessagesSync();
            }
        }

        // Synchronous wrapper for async operation
        private void LoadBlogMessagesSync()
        {
            try
            {
                // Use Task.Run and Wait to synchronously wait for the async operation
                Task<List<Message>> task = Task.Run(() => _messagesController.GetAllMessagesAsync());
                task.Wait(); // This can be a problem if it takes too long, but it's a simple solution for now

                List<Message> messages = task.Result;
                DisplayBlogMessages(messages);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                lblStatus.Text = $"Error loading messages: {ex.Message}";
                lblStatus.Visible = true;
            }
        }

        private void DisplayBlogMessages(List<Message> messages)
        {
            // Clear existing content
            blogContent.Controls.Clear();

            if (messages == null || messages.Count == 0)
            {
                // If no messages, show the default content that was already in the ASPX
                return;
            }

            // Sort messages by creation date (newest first)
            var sortedMessages = messages.OrderByDescending(m => m.CreatedAt).ToList();

            foreach (var message in sortedMessages)
            {
                // Create entry header
                var entryHeader = new HtmlGenericControl("h3");
                entryHeader.InnerText = $"Entry: {message.MessageID} {message.CreatedAt.ToString("M/d/yyyy @h:mm tt")}";
                blogContent.Controls.Add(entryHeader);

                // Create post header if exists
                if (!string.IsNullOrEmpty(message.Header))
                {
                    var header = new HtmlGenericControl("h4");
                    header.InnerText = message.Header;
                    blogContent.Controls.Add(header);
                }

                // Create paragraph for body
                var paragraph = new HtmlGenericControl("p");
                paragraph.InnerText = message.Body;
                blogContent.Controls.Add(paragraph);

                // Add image if exists - as a separate control after the paragraph
                if (!string.IsNullOrEmpty(message.Image))
                {
                    var image = new Image();
                    image.ImageUrl = message.Image;
                    image.CssClass = "blog-image";
                    blogContent.Controls.Add(image);
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnAddPost_Click(object sender, EventArgs e)
        {
            try
            {
                var message = new Message
                {
                    UserID = 1, // Hardcoded as requested
                    Header = txtHeader.Text,
                    Body = txtBody.Text,
                    Image = txtImageUrl.Text,
                    CreatedAt = DateTime.Now
                };

                // Use Task.Run to execute async method and wait for it synchronously
                Task<int> task = Task.Run(() => _messagesController.AddMessageAsync(message));
                task.Wait();

                // Clear form fields
                txtHeader.Text = string.Empty;
                txtBody.Text = string.Empty;
                txtImageUrl.Text = string.Empty;

                // Reload messages
                LoadBlogMessagesSync();

                // Show success message
                lblStatus.Text = "Blog post added successfully!";
                lblStatus.Visible = true;
            }
            catch (AggregateException aex)
            {
                // Unwrap the inner exception
                Exception ex = aex.InnerException ?? aex;
                lblStatus.Text = $"Error adding blog post: {ex.Message}";
                lblStatus.Visible = true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error adding blog post: {ex.Message}";
                lblStatus.Visible = true;
            }
        }
    }
}