using System;
using System.Net.Http;
using System.Web.UI;
using Models;
using SourDuckWannaBet.Controllers;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class MarzyBlog : System.Web.UI.Page
    {
        private MessagesController _messagesController;
        private SupabaseServices _supabaseServices;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get Supabase URL and Key from configuration or environment variables
            string supabaseUrl = "https://your-supabase-url.supabase.co"; // Replace with actual URL
            string supabaseKey = "your-supabase-key"; // Replace with actual API Key

            // Initialize SupabaseServices and MessagesController
            _supabaseServices = new SupabaseServices(new HttpClient(), supabaseUrl, supabaseKey);
            _messagesController = new MessagesController(_supabaseServices);

            if (!IsPostBack)
            {
                LoadMessages();
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected async void btnAddMessage_Click(object sender, EventArgs e)
        {
            var message = new Message
            {
                UserID = 1, // Hardcoded for now
                Header = txtHeader.Text,
                Body = txtBody.Text,
                ImageUrl = txtImageUrl.Text,
                CreatedAt = DateTime.UtcNow
            };

            bool success = await _messagesController.AddMessageAsync(message);

            if (success)
            {
                LoadMessages();
            }
            else
            {
                // Handle error
            }
        }

        private async void LoadMessages()
        {
            var messages = await _messagesController.GetAllMessagesAsync();     /////
            rptMessages.DataSource = messages;
            rptMessages.DataBind();
        }
    }
}
