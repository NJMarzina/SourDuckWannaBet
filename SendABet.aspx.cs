using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using SourDuckWannaBet.Controllers;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace SourDuckWannaBet
{
    public partial class SendABet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // TODO: Future authentication system
                // This section will be updated when login functionality is implemented
                // Currently running with manual sender selection
            }
        }

        /*
        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            // Register the asynchronous task
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Validate input
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||    // New field for sender
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    // TEMPORARY: Manual sender selection until login system is implemented
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID
                    long recipientUserID;
                    if (!long.TryParse(hdnRecipientUserID.Value, out recipientUserID))
                    {
                        lblStatus.Text = "Invalid recipient. Please select a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        Created_at = DateTime.Now,
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                    };

                    // Set amounts based on bet type
                    if (isOneSided)
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount.Text);
                        bet.BetB_Amount = 0;
                        bet.Pending_Bet = bet.BetA_Amount;
                    }
                    else
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount2.Text);
                        bet.BetB_Amount = Convert.ToDouble(txtReceiverAmount.Text);
                        bet.Pending_Bet = bet.BetA_Amount + bet.BetB_Amount;
                    }

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        await betsController.AddBetAsync(bet);
                    }

                    // Show success message
                    lblStatus.Text = "Bet sent successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    hdnRecipientUserID.Value = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Debug: Check the value of hdnRecipientUserID
                var recipientUserIDValue = hdnRecipientUserID.Value;
                System.Diagnostics.Debug.WriteLine($"Recipient User ID: {recipientUserIDValue}");

                // Validate input
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID
                    long recipientUserID;
                    if (!long.TryParse(hdnRecipientUserID.Value, out recipientUserID))
                    {
                        lblStatus.Text = "Invalid recipient. Please select a valid username.";
                        return;
                    }

                    // Rest of your code...
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Debug: Check the value of hdnRecipientUserID
                var recipientUserIDValue = hdnRecipientUserID.Value;
                System.Diagnostics.Debug.WriteLine($"Recipient User ID: {recipientUserIDValue}");

                // Validate input
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID
                    long recipientUserID;
                    if (!long.TryParse(hdnRecipientUserID.Value, out recipientUserID))
                    {
                        lblStatus.Text = "Invalid recipient. Please select a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        Created_at = DateTime.Now,
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                    };

                    // Set amounts based on bet type
                    if (isOneSided)
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount.Text);
                        bet.BetB_Amount = 0;
                        bet.Pending_Bet = bet.BetA_Amount;
                    }
                    else
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount2.Text);
                        bet.BetB_Amount = Convert.ToDouble(txtReceiverAmount.Text);
                        bet.Pending_Bet = bet.BetA_Amount + bet.BetB_Amount;
                    }

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        await betsController.AddBetAsync(bet);
                    }

                    // Show success message
                    lblStatus.Text = "Bet sent successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    hdnRecipientUserID.Value = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Debug: Check the value of hdnRecipientUserID
                var recipientUserIDValue = hdnRecipientUserID.Value;
                System.Diagnostics.Debug.WriteLine($"Recipient User ID: {recipientUserIDValue}");

                // Log all usernames fetched from the database
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var users = await usersController.GetAllUsersAsync();
                    var usernames = users.Select(u => u.Username).ToList();
                    System.Diagnostics.Debug.WriteLine("Usernames fetched from database: " + string.Join(", ", usernames));
                }

                // Validate input
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID
                    long recipientUserID;
                    if (!long.TryParse(hdnRecipientUserID.Value, out recipientUserID))
                    {
                        // Log the state before showing the error
                        System.Diagnostics.Debug.WriteLine("hdnRecipientUserID value before error: " + hdnRecipientUserID.Value);
                        lblStatus.Text = "Invalid recipient. Please select a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        Created_at = DateTime.Now,
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                    };

                    // Set amounts based on bet type
                    if (isOneSided)
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount.Text);
                        bet.BetB_Amount = 0;
                        bet.Pending_Bet = bet.BetA_Amount;
                    }
                    else
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount2.Text);
                        bet.BetB_Amount = Convert.ToDouble(txtReceiverAmount.Text);
                        bet.Pending_Bet = bet.BetA_Amount + bet.BetB_Amount;
                    }

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        await betsController.AddBetAsync(bet);
                    }

                    // Show success message
                    lblStatus.Text = "Bet sent successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    hdnRecipientUserID.Value = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    System.Diagnostics.Debug.WriteLine($"Sender Username: {txtSenderUsername.Text}");
                    System.Diagnostics.Debug.WriteLine($"Recipient Username: {txtRecipientUsername.Text}");
                    System.Diagnostics.Debug.WriteLine($"Sender User ID: {await GetUserIDByUsernameAsync(txtSenderUsername.Text)}");
                    System.Diagnostics.Debug.WriteLine($"Recipient User ID: {await GetUserIDByUsernameAsync(txtRecipientUsername.Text)}");

                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID from username input
                    long recipientUserID = await GetUserIDByUsernameAsync(txtRecipientUsername.Text);
                    if (recipientUserID == -1)
                    {
                        lblStatus.Text = "Invalid recipient. Please enter a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        Created_at = DateTime.Now,
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                    };

                    // Set amounts based on bet type
                    if (isOneSided)
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount.Text);
                        bet.BetB_Amount = 0;
                        bet.Pending_Bet = bet.BetA_Amount;
                    }
                    else
                    {
                        bet.BetA_Amount = Convert.ToDouble(txtSenderAmount2.Text);
                        bet.BetB_Amount = Convert.ToDouble(txtReceiverAmount.Text);
                        bet.Pending_Bet = bet.BetA_Amount + bet.BetB_Amount;
                    }

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        await betsController.AddBetAsync(bet);
                    }

                    // Show success message
                    lblStatus.Text = "Bet sent successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID from username input
                    long recipientUserID = await GetUserIDByUsernameAsync(txtRecipientUsername.Text);
                    if (recipientUserID == -1)
                    {
                        lblStatus.Text = "Invalid recipient. Please enter a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        BetA_Amount = isOneSided ? Convert.ToDouble(txtSenderAmount.Text) : Convert.ToDouble(txtSenderAmount2.Text),
                        BetB_Amount = isOneSided ? 0 : Convert.ToDouble(txtReceiverAmount.Text),
                        Pending_Bet = isOneSided ? Convert.ToDouble(txtSenderAmount.Text) : Convert.ToDouble(txtSenderAmount2.Text) + Convert.ToDouble(txtReceiverAmount.Text),
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                    };

                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        var result = await betsController.AddBetAsync(bet);

                        if (result is OkObjectResult okResult)
                        {
                            lblStatus.Text = "Bet sent successfully!";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblStatus.Text = "Error sending bet. Please try again.";
                        }
                    }

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }
        */

        protected void btnSendBet_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                // Validate input fields
                if (string.IsNullOrEmpty(txtSenderUsername.Text) ||
                    string.IsNullOrEmpty(txtRecipientUsername.Text) ||
                    string.IsNullOrEmpty(txtDescription.Text) ||
                    string.IsNullOrEmpty(txtSenderResult.Text) ||
                    string.IsNullOrEmpty(txtReceiverResult.Text))
                {
                    lblStatus.Text = "Please fill out all required fields.";
                    return;
                }

                // Validate amounts based on bet type
                bool isOneSided = ddlBetType.SelectedValue == "one-sided";
                if (isOneSided && string.IsNullOrEmpty(txtSenderAmount.Text))
                {
                    lblStatus.Text = "Please enter your bet amount.";
                    return;
                }
                else if (!isOneSided && (string.IsNullOrEmpty(txtSenderAmount2.Text) || string.IsNullOrEmpty(txtReceiverAmount.Text)))
                {
                    lblStatus.Text = "Please enter both bet amounts for a two-sided bet.";
                    return;
                }

                try
                {
                    // Get sender user ID from username input
                    long senderUserID = await GetUserIDByUsernameAsync(txtSenderUsername.Text);
                    if (senderUserID == -1)
                    {
                        lblStatus.Text = "Invalid sender. Please enter a valid username.";
                        return;
                    }

                    // Get recipient user ID from username input
                    long recipientUserID = await GetUserIDByUsernameAsync(txtRecipientUsername.Text);
                    if (recipientUserID == -1)
                    {
                        lblStatus.Text = "Invalid recipient. Please enter a valid username.";
                        return;
                    }

                    // Create a new bet object
                    var bet = new Bet
                    {
                        UserID_Sender = senderUserID,    // Use the retrieved sender ID
                        UserID_Receiver = recipientUserID,
                        BetA_Amount = isOneSided ? Convert.ToDouble(txtSenderAmount.Text) : Convert.ToDouble(txtSenderAmount2.Text),    // remove 2 sided
                        BetB_Amount = isOneSided ? 0 : Convert.ToDouble(txtReceiverAmount.Text),
                        Pending_Bet = isOneSided ? Convert.ToDouble(txtSenderAmount.Text) : Convert.ToDouble(txtSenderAmount2.Text) + Convert.ToDouble(txtReceiverAmount.Text),
                        Description = txtDescription.Text,
                        Status = "Pending",
                        Sender_Result = txtSenderResult.Text,
                        Receiver_Result = txtReceiverResult.Text,
                        Sender_Balance_Change = 0, // Will be updated when bet is settled
                        Receiver_Balance_Change = 0, // Will be updated when bet is settled
                        //UserID_Mediator = chkNeedMediator.Checked ? 0 : -1 // 0 means mediator needed but not assigned, -1 means no mediator
                        UserID_Mediator = 1
                    };
                    //TODO Update UserID_Mediator will be null if not checked, or if checked prompt user to enter username then retrieve.
                    //will not run if mediator is -1


                    // Use a BetsController to save the bet
                    using (var httpClient = new HttpClient())
                    {
                        var betsController = new BetsController(httpClient);
                        var result = await betsController.AddBetAsync(bet);

                        // Check the result type
                        if (result is OkObjectResult okResult)
                        {
                            lblStatus.Text = "Bet sent successfully!";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (result is BadRequestObjectResult badRequestResult)
                        {
                            lblStatus.Text = $"Error sending bet: {badRequestResult.Value}";
                        }
                        else
                        {
                            lblStatus.Text = "Error sending bet. Please try again.";
                        }
                    }

                    // Clear the form
                    txtSenderUsername.Text = "";  // Clear the sender field
                    txtRecipientUsername.Text = "";
                    txtSenderAmount.Text = "";
                    txtSenderAmount2.Text = "";
                    txtReceiverAmount.Text = "";
                    txtDescription.Text = "";
                    txtSenderResult.Text = "";
                    txtReceiverResult.Text = "";
                    chkNeedMediator.Checked = false;
                    ddlBetType.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error sending bet: " + ex.Message;
                }
            }));
        }

        private async Task<long> GetUserIDByUsernameAsync(string username)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var users = await usersController.GetAllUsersAsync();
                    var user = users.FirstOrDefault(u => u.Username == username);
                    return user != null ? user.UserID ?? -1 : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user ID: {ex.Message}");
                return -1;
            }
        }

        [WebMethod]
        public static long GetUserIDByUsername(string username)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var usersController = new UsersController(httpClient);
                    var users = usersController.GetAllUsersAsync().Result;
                    var user = users.FirstOrDefault(u => u.Username == username);
                    return user != null ? user.UserID ?? -1: -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user ID: {ex.Message}");
                return -1;
            }
        }

        // Your existing WebMethods remain unchanged
        [WebMethod]
        public static List<string> GetUsernames()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var _usersController = new UsersController(httpClient);
                    var users = _usersController.GetAllUsersAsync().Result;
                    return users.Select(u => u.Username).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting usernames: {ex.Message}");
                return new List<string>();
            }
        }
    }
}