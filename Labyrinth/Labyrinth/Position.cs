using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Position
    {
        public Vector2 coordinates;
        public int steps;
        public Position(Vector2 coordinates, int steps)
        {
            this.coordinates = coordinates;
            this.steps = steps;
        }
    }
}
