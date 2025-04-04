using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using SourDuckWannaBet.Controllers;
using Utilities;

namespace SourDuckWannaBet
{
    public partial class GRDice : System.Web.UI.Page
    {
        protected User CurrentUser;
        protected double CurrentBalance;
        private GameRoomUtilities _gameRoomUtilities;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await InitializeGame();
            }
        }

        private async Task InitializeGame()
        {
            try
            {
                // Initialize HttpClient
                var httpClient = new HttpClient();

                // Initialize GameRoomUtilities with HttpClient
                _gameRoomUtilities = new GameRoomUtilities(httpClient);

                // Load user data using existing controller
                var usersController = new UsersController(httpClient);
                if (Request.Cookies["UserID"] != null)
                {
                    CurrentUser = await usersController.GetUserByUserIDAsync(long.Parse(Request.Cookies["UserID"].Value));
                    if (CurrentUser != null)
                    {
                        CurrentBalance = CurrentUser.Balance;
                        lblBalance.Text = $"Balance: ${CurrentBalance:F2}";
                    }
                    else
                    {
                        Response.Redirect("WBLogin.aspx");
                    }
                }
                else
                {
                    Response.Redirect("WBLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error initializing game: {ex.Message}";
            }
        }

        protected async void btnRoll_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure _gameRoomUtilities is initialized and CurrentUser is loaded
                if (_gameRoomUtilities == null)
                {
                    lblError.Text = "Game utilities are not initialized. Please try again.";
                    return;
                }

                if (CurrentUser == null)
                {
                    lblError.Text = "User is not logged in. Please log in to play.";
                    return;
                }

                // Reload user data (just in case)
                var httpClient = new HttpClient();
                var usersController = new UsersController(httpClient);
                CurrentUser = await usersController.GetUserByUserIDAsync(long.Parse(Request.Cookies["UserID"].Value));
                CurrentBalance = CurrentUser.Balance;

                lblError.Text = "";
                lblResult.Text = "";

                // Validate input
                if (string.IsNullOrEmpty(txtGuess.Text) || string.IsNullOrEmpty(txtBetAmount.Text))
                {
                    lblError.Text = "Please enter both a guess and bet amount";
                    return;
                }

                // Get user input
                int sides = int.Parse(ddlSides.SelectedValue);
                int guess = int.Parse(txtGuess.Text);
                double betAmount = double.Parse(txtBetAmount.Text);

                // Validate guess is within range
                if (guess < 1 || guess > sides)
                {
                    lblError.Text = $"Your guess must be between 1 and {sides}";
                    return;
                }

                // Validate bet amount
                if (betAmount <= 0)
                {
                    lblError.Text = "Bet amount must be greater than zero";
                    return;
                }

                if (betAmount > CurrentBalance)
                {
                    lblError.Text = "You don't have enough balance for this bet";
                    return;
                }

                // Create the dice game
                var diceGame = await _gameRoomUtilities.CreateDiceGameAsync(
                    CurrentUser.UserID,
                    sides,
                    guess,
                    decimal.Parse(betAmount.ToString())
                );

                // Generate random result
                Random rnd = new Random();
                int result = rnd.Next(1, sides + 1);

                // Update the game result
                diceGame.Result = result;

                // Calculate balance change based on result
                double balanceChange = -betAmount; // Default to loss
                if (result == guess)
                {
                    // Win - double the bet amount
                    balanceChange = betAmount;
                }

                // Update user balance
                await _gameRoomUtilities.UpdateUserBalanceAsync(
                    CurrentUser.UserID.ToString(),
                    decimal.Parse(balanceChange.ToString())
                );

                // Update UI
                CurrentBalance += balanceChange;
                lblBalance.Text = $"Balance: ${CurrentBalance:F2}";

                // Show result
                string resultText = $"You rolled: {result}<br/>";
                resultText += guess == result
                    ? $"<span style='color:green'>You won ${betAmount:F2}!</span>"
                    : $"<span style='color:red'>You lost ${betAmount:F2}</span>";
                lblResult.Text = resultText;

                // Update the user object in our current session
                CurrentUser.Balance = CurrentBalance;
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
            }
        }
    }
}
