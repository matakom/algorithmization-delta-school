using System.Numerics;

namespace Labyrinth
{
    internal class Program
    {
        static Solution[,] stepsGrid;
        static void Main(string[] args)
        {
            //int[,] labyrinth = { { 1, 0, 0, 0 }, { 1, 1, 0, 1 }, { 0, 1, 0, 0 }, { 1, 1, 1, 1 } };
            //int[,] labyrinth = { { 1, 1, 0, 0 }, { 0, 1, 0, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
            int[,] labyrinth = {
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
};



            PrintLabyrinth(labyrinth);


            int height = labyrinth.GetLength(0);
            int width = labyrinth.GetLength(1);

            stepsGrid = new Solution[height, width];
            for (int i = 0; i < stepsGrid.GetLength(0); i++)
            {
                for (int j = 0; j < stepsGrid.GetLength(1); j++)
                {
                    stepsGrid[i, j] = new Solution();
                    stepsGrid[i, j].steps = int.MaxValue;
                }

            }

            Vector2 startPosition = new Vector2(0, 0);
            Vector2 endPosition = new Vector2(19, 19);
            stepsGrid[(int)startPosition.Y, (int)startPosition.X].steps = 0;

            SolveLabyrinthRecursion(labyrinth, startPosition, endPosition);

            int steps = stepsGrid[(int)endPosition.Y, (int)endPosition.X].steps;
            if (steps == -1)
            {
                Console.WriteLine("There is no path possible");
            }
            else
            {
                List<Vector2> stepList = stepsGrid[(int)endPosition.Y, (int)endPosition.X].history;
                for (int i = 0; i < stepList.Count; i++)
                {
                    Console.WriteLine(stepList[i]);
                }
                Console.WriteLine("Steps: " + steps);
            }

        }
        static int SolveLabyrinthLoop(int[,] labyrinth, Vector2 startPosition, Vector2 endPosition)
        {
            List<Position> positions = new List<Position> { new Position(startPosition, 0) };
            labyrinth[(int)positions[0].coordinates.Y, (int)positions[0].coordinates.X] = 0;

            int height = labyrinth.GetLength(0);
            int width = labyrinth.GetLength(1);

            while (positions.Count > 0)
            {
                int steps = positions[0].steps;
                int x = (int)positions[0].coordinates.X;
                int y = (int)positions[0].coordinates.Y;
                Console.WriteLine($"Coordinates: X: {x} | Y:{y}");
                if (positions[0].coordinates == endPosition)
                {
                    return positions[0].steps;
                }
                // Down
                if (y + 1 < height && labyrinth[y + 1, x] == 1)
                {
                    positions.Add(new Position(new Vector2(x, y + 1), steps + 1));
                    labyrinth[y + 1, x] = 0;
                }
                // Up
                if (y - 1 >= 0 && labyrinth[y - 1, x] == 1)
                {
                    positions.Add(new Position(new Vector2(x, y - 1), steps + 1));
                    labyrinth[y - 1, x] = 0;
                }
                // Right
                if (x + 1 < width && labyrinth[y, x + 1] == 1)
                {
                    positions.Add(new Position(new Vector2(x + 1, y), steps + 1));
                    labyrinth[y, x + 1] = 0;
                }
                // Left
                if (x - 1 >= 0 && labyrinth[y, x - 1] == 1)
                {
                    positions.Add(new Position(new Vector2(x - 1, y), steps + 1));
                    labyrinth[y, x - 1] = 0;
                }
                positions.RemoveAt(0);
            }
            return -1;
        }
        static void SolveLabyrinthRecursion(int[,] labyrinth, Vector2 position, Vector2 endPosition)
        {
            //PrintStepsGrid(stepsGrid);

            int height = labyrinth.GetLength(0);
            int width = labyrinth.GetLength(1);

            int x = (int)position.X;
            int y = (int)position.Y;

            // Down
            if (y + 1 < height && labyrinth[y + 1, x] == 1)
            {
                if (stepsGrid[y, x].steps + 1 < stepsGrid[y + 1, x].steps)
                {
                    Vector2 newPosition = new Vector2(x, y + 1);
                    stepsGrid[y + 1, x].steps = stepsGrid[y, x].steps + 1;
                    stepsGrid[y + 1, x].history = new List<Vector2>(stepsGrid[y, x].history);
                    stepsGrid[y + 1, x].history.Add(newPosition);
                    SolveLabyrinthRecursion(labyrinth, newPosition, endPosition);
                }
            }
            // Up
            if (y - 1 >= 0 && labyrinth[y - 1, x] == 1)
            {
                if (stepsGrid[y, x].steps + 1 < stepsGrid[y - 1, x].steps)
                {
                    Vector2 newPosition = new Vector2(x, y - 1); 
                    stepsGrid[y - 1, x].steps = stepsGrid[y, x].steps + 1;
                    stepsGrid[y - 1, x].history = new List<Vector2>(stepsGrid[y, x].history); 
                    stepsGrid[y - 1, x].history.Add(newPosition);
                    SolveLabyrinthRecursion(labyrinth, newPosition, endPosition);
                }
            }
            // Right
            if (x + 1 < width && labyrinth[y, x + 1] == 1)
            {
                if (stepsGrid[y, x].steps + 1 < stepsGrid[y, x + 1].steps)
                {
                    Vector2 newPosition = new Vector2(x + 1, y);
                    stepsGrid[y, x + 1].steps = stepsGrid[y, x].steps + 1;
                    stepsGrid[y, x + 1].history = new List<Vector2>(stepsGrid[y, x].history);
                    stepsGrid[y, x + 1].history.Add(newPosition);
                    SolveLabyrinthRecursion(labyrinth, newPosition, endPosition);
                }
            }
            // Left
            if (x - 1 >= 0 && labyrinth[y, x - 1] == 1)
            {
                if (stepsGrid[y, x].steps + 1 < stepsGrid[y, x - 1].steps)
                {
                    Vector2 newPosition = new Vector2(x - 1, y);
                    stepsGrid[y, x - 1].steps = stepsGrid[y, x].steps + 1;
                    stepsGrid[y, x - 1].history = new List<Vector2>(stepsGrid[y, x].history);
                    stepsGrid[y, x - 1].history.Add(newPosition);
                    SolveLabyrinthRecursion(labyrinth, newPosition, endPosition);
                }
            }
        }
        static void PrintLabyrinth(int[,] labyrinth)
        {
            int height = labyrinth.GetLength(0);
            int width = labyrinth.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (labyrinth[i, j] == 0)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("█");
                    }
                }
                Console.Write("\n");
            }
        }
        static void PrintStepsGrid(Solution[,] steps)
        {
            for (int i = 0; i < steps.GetLength(0); i++)
            {
                for (int j = 0; j < steps.GetLength(1); j++)
                {
                    if (steps[i, j].steps == int.MaxValue)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(steps[i, j].steps);
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-----------------");
        }
    }
}
