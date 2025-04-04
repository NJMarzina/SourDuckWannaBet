using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Models
{
    [Table("dice_game")]
    public class DiceGame : BaseModel
    {
        [PrimaryKey("diceID", false)]
        public int DiceID { get; set; }

        [Column("userID")]
        public long UserID { get; set; }

        [Column("sides")]
        public int Sides { get; set; }

        [Column("guess")]
        public int Guess { get; set; }

        [Column("result")]
        public int Result { get; set; }

        [Column("bet_amount")]
        public double BetAmount { get; set; }

        [Column("balance_change")]
        public double BalanceChange { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Helper method to play the game
        public void PlayGame(int guess, double betAmount)
        {
            if (guess < 1 || guess > Sides)
                throw new ArgumentOutOfRangeException(nameof(guess), $"Guess must be between 1 and {Sides}");

            if (betAmount <= 0)
                throw new ArgumentException("Bet amount must be positive", nameof(betAmount));

            var dice = new Dice(Sides);
            Result = dice.Roll();
            Guess = guess;
            BetAmount = betAmount;
            CreatedAt = DateTime.UtcNow;

            // Calculate winnings (simple 1:1 payout if guessed correctly)
            BalanceChange = (guess == Result) ? betAmount : -betAmount;
        }

        // Static factory method to create a new game
        public static DiceGame CreateNewGame(long userId, int sides, int guess, double betAmount)
        {
            var game = new DiceGame
            {
                UserID = userId,
                Sides = sides
            };

            game.PlayGame(guess, betAmount);
            return game;
        }
    }
}