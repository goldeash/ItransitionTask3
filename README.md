# ItransitionTask3

This is a C# console application that implements a game with non-transitive dice, supporting arbitrary dice values and fair random number generation.

Description
The program allows you to:

Define custom dice via command-line arguments (e.g., 2,2,4,4,9,9).
Determine who makes the first move using fair random number generation (HMAC).
Play a game where the user and the computer choose different dice and roll them.
Display a probability table of winning chances for each pair of dice.
Use colored output to enhance the user experience.

----------------------------

How to Play:
Start the game: 
Open the command line and type: dotnet run "Dice1" "Dice2"
Example: dotnet run "1,2,3,4,5,6" "2,2,4,4,6,6"

Determine the first player:
The program uses HMAC (SHA3) to fairly decide the first player.
The HMAC key is displayed for verification.

Rolling the dice:
Each player rolls their dice.
The program displays the results.

Determine the winner:
The player with the higher value wins.
In case of a tie, a reroll happens.

Probability table:
A table with each player's winning probability is shown.

----------------------------

Requirements:

.NET 6.0 or higher.
Command-line support.
