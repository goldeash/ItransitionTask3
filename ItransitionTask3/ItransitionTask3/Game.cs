using System;
using System.Collections.Generic;
using System.Linq;

namespace NonTransitiveDiceGame
{
    public class Game
    {
        private readonly List<Dice> _diceList;
        private readonly FairRandomGenerator _randomGenerator = new FairRandomGenerator();

        public Game(List<Dice> diceList)
        {
            _diceList = diceList;
        }

        public void Start()
        {
            DisplayColoredMessage("Let's determine who makes the first move.", ConsoleColor.Yellow);

            var (computerNumber, hmac, key) = _randomGenerator.GenerateFairRandom(0, 2);
            DisplayColoredMessage($"I selected a random value in the range 0..1 (HMAC={hmac}).", ConsoleColor.Blue);

            int userNumber = GetUserInput("Try to guess my selection.", new[] { "0", "1" });
            int result = _randomGenerator.CalculateResult(computerNumber, userNumber, 2);

            DisplayColoredMessage($"The result is {computerNumber} + {userNumber} = {result} (mod 2).", ConsoleColor.Green);
            DisplayColoredMessage($"My selection: {result} (KEY={BitConverter.ToString(key).Replace("-", "")}).", ConsoleColor.Green);

            bool userMovesFirst = result == 0;
            DisplayColoredMessage(userMovesFirst ? "You make the first move." : "I make the first move.", ConsoleColor.Yellow);

            PlayRound(userMovesFirst);
        }

        private void PlayRound(bool userMovesFirst)
        {
            int userDiceIndex, computerDiceIndex;

            if (userMovesFirst)
            {
                userDiceIndex = GetUserDiceChoice("Choose your dice:");
                computerDiceIndex = GetComputerDiceChoice(userDiceIndex);
                DisplayColoredMessage($"I choose the [{string.Join(",", _diceList[computerDiceIndex].Faces)}] dice.", ConsoleColor.Cyan);
            }
            else
            {
                computerDiceIndex = GetComputerDiceChoice();
                DisplayColoredMessage($"I choose the [{string.Join(",", _diceList[computerDiceIndex].Faces)}] dice.", ConsoleColor.Cyan);

                userDiceIndex = GetUserDiceChoice("Choose your dice:", computerDiceIndex);
            }

            var userDice = _diceList[userDiceIndex];
            var computerDice = _diceList[computerDiceIndex];

            DisplayColoredMessage($"You choose the [{string.Join(",", userDice.Faces)}] dice.", ConsoleColor.Green);

            DisplayColoredMessage("It's time for throws.", ConsoleColor.Yellow);

            int computerThrow = PerformThrow("my", computerDice);
            int userThrow = PerformThrow("your", userDice);

            DetermineWinner(userThrow, computerThrow);
        }

        private int GetUserInput(string prompt, string[] validOptions)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                for (int i = 0; i < validOptions.Length; i++)
                {
                    Console.WriteLine($"{i} - {validOptions[i]}");
                }
                Console.WriteLine("X - exit");
                Console.WriteLine("? - help");

                string userInput = Console.ReadLine()!;
                if (userInput == "X") Environment.Exit(0);
                if (userInput == "?")
                {
                    ProbabilityTable.Display(_diceList);
                    continue;
                }
                if (int.TryParse(userInput, out int userNumber) && userNumber >= 0 && userNumber < validOptions.Length)
                {
                    return userNumber;
                }
                DisplayColoredMessage("Invalid input. Please enter a valid option.", ConsoleColor.Red);
            }
        }

        private int GetUserDiceChoice(string prompt, int? excludedIndex = null)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < _diceList.Count; i++)
            {
                if (excludedIndex == null || i != excludedIndex)
                {
                    Console.WriteLine($"{i} - {string.Join(",", _diceList[i].Faces)}");
                }
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            while (true)
            {
                string userInput = Console.ReadLine()!;
                if (userInput == "X") Environment.Exit(0);
                if (userInput == "?")
                {
                    ProbabilityTable.Display(_diceList);
                    continue;
                }
                if (int.TryParse(userInput, out int userDiceIndex) && userDiceIndex >= 0 && userDiceIndex < _diceList.Count && (excludedIndex == null || userDiceIndex != excludedIndex))
                {
                    return userDiceIndex;
                }
                DisplayColoredMessage("Invalid input. Please enter a valid dice index.", ConsoleColor.Red);
            }
        }

        private int GetComputerDiceChoice(int? excludedIndex = null)
        {
            int computerDiceIndex;
            do
            {
                var (computerDiceRandom, _, _) = _randomGenerator.GenerateFairRandom(0, _diceList.Count);
                computerDiceIndex = computerDiceRandom;
            } while (excludedIndex != null && computerDiceIndex == excludedIndex);

            return computerDiceIndex;
        }

        private int PerformThrow(string player, Dice dice)
        {
            var (computerNumber, hmac, key) = _randomGenerator.GenerateFairRandom(0, dice.Faces.Length);
            DisplayColoredMessage($"I selected a random value in the range 0..{dice.Faces.Length - 1} (HMAC={hmac}).", ConsoleColor.Blue);

            int userNumber = GetUserInput($"Add your number modulo {dice.Faces.Length}.", Enumerable.Range(0, dice.Faces.Length).Select(x => x.ToString()).ToArray());
            int result = _randomGenerator.CalculateResult(computerNumber, userNumber, dice.Faces.Length);

            DisplayColoredMessage($"The result is {computerNumber} + {userNumber} = {result} (mod {dice.Faces.Length}).", ConsoleColor.Green);

            DisplayColoredMessage($"My number is {result} (KEY={BitConverter.ToString(key).Replace("-", "")}).", ConsoleColor.Green);
            int throwResult = dice.Roll(result);
            DisplayColoredMessage($"{player} throw is {throwResult}.", ConsoleColor.Cyan);

            return throwResult;
        }

        private void DetermineWinner(int userThrow, int computerThrow)
        {
            if (userThrow > computerThrow)
            {
                DisplayColoredMessage($"You win ({userThrow} > {computerThrow})!", ConsoleColor.Green);
            }
            else if (userThrow < computerThrow)
            {
                DisplayColoredMessage($"I win ({computerThrow} > {userThrow})!", ConsoleColor.Red);
            }
            else
            {
                DisplayColoredMessage("It's a tie!", ConsoleColor.Yellow);
            }
        }

        private void DisplayColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}