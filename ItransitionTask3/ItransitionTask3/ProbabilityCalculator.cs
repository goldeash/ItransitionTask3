using NonTransitiveDiceGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class ProbabilityCalculator
    {
        public static double CalculateWinProbability(Dice dice1, Dice dice2)
        {
            int wins = 0;
            int losses = 0;

            foreach (var face1 in dice1.Faces)
            {
                foreach (var face2 in dice2.Faces)
                {
                    if (face1 > face2) wins++;
                    else if (face1 < face2) losses++;
                }
            }

            return (double)wins / (wins + losses);
        }
    }
}
