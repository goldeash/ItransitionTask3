using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NonTransitiveDiceGame
{
    public static class ProbabilityTable
    {
        public static void Display(List<Dice> diceList)
        {
            // Create a table
            var table = new Table();

            // Add columns with headers
            table.AddColumn(new TableColumn("[bold cyan]User dice v[/]").Centered());
            foreach (var dice in diceList)
            {
                table.AddColumn(new TableColumn($"[bold]{string.Join(",", dice.Faces)}[/]").Centered());
            }

            // Add rows
            for (int i = 0; i < diceList.Count; i++)
            {
                var row = new List<string> { $"[bold]{string.Join(",", diceList[i].Faces)}[/]" };
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j)
                    {
                        row.Add("[grey]-[/]"); // Diagonal placeholder
                    }
                    else
                    {
                        double probability = ProbabilityCalculator.CalculateWinProbability(diceList[i], diceList[j]);
                        row.Add($"[green]{probability:F4}[/]");
                    }
                }
                table.AddRow(row.ToArray());
            }

            // Render the table
            AnsiConsole.Write(table);
        }
    }
}