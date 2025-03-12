using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class Dice
    {
        public int[] Faces { get; }

        public Dice(int[] faces)
        {
            Faces = faces;
        }

        public int Roll(int faceIndex)
        {
            return Faces[faceIndex];
        }
    }
}
