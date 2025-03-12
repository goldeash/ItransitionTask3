using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace NonTransitiveDiceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: At least 3 dice configurations are required.");
                Console.WriteLine("Example: game.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
                Console.ResetColor();
                return;
            }

            try
            {
                var diceList = DiceParser.ParseDice(args);
                var game = new Game(diceList);
                game.Start();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}