using NonTransitiveDiceGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public static class DiceParser
    {
        public static List<Dice> ParseDice(string[] args)
        {
            var diceList = new List<Dice>();
            foreach (var arg in args)
            {
                try
                {
                    var faces = arg.Split(',').Select(int.Parse).ToArray();
                    if (faces.Length == 0)
                    {
                        throw new ArgumentException("Dice must have at least one face.");
                    }
                    diceList.Add(new Dice(faces));
                }
                catch (FormatException)
                {
                    throw new ArgumentException($"Invalid dice configuration: '{arg}'. All values must be integers.");
                }
            }
            return diceList;
        }
    }
}
