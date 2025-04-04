using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dice
    {
        private readonly Random _random;
        private readonly int _sides;

        // Constructor with default of 6 sides
        public Dice(int sides = 6)
        {
            if (sides <= 0)
            {
                throw new ArgumentException("Dice must have at least 1 side", nameof(sides));
            }
            _sides = sides;
            _random = new Random();
        }

        // Roll the dice once and return the result
        public int Roll()
        {
            // Fixed syntax error with asterisks
            return _random.Next(1, _sides + 1);
        }

        // Roll the dice multiple times and return the results
        public int[] RollMultiple(int times)
        {
            if (times <= 0)
            {
                throw new ArgumentException("Must roll at least once", nameof(times));
            }
            int[] results = new int[times];
            for (int i = 0; i < times; i++)
            {
                results[i] = Roll();
            }
            return results;
        }

        // Property to get the number of sides
        public int Sides => _sides;
    }
}