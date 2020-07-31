using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GridHoleSizes
{
    class Program
    {
        static void Main(string[] args)
        {
            // create grid
            const int iCount = 10;
            const int jCount = 10;
            var grid = new int[iCount, jCount] {
                   { 1, 1, 0, 0, 0, 1, 1, 1, 1, 0},
                   { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0},
                   { 1, 1, 0, 1, 1, 1, 1, 1, 1, 0},
                   { 1, 0, 0, 1, 0, 0, 1, 1, 1, 0},
                   { 1, 1, 0, 1, 1, 1, 1, 1, 1, 0},
                   { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0},
                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                   { 1, 1, 0, 0, 1, 1, 1, 1, 1, 0},
                   { 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
                   { 1, 1, 0, 0, 1, 1, 1, 1, 1, 1}
            };

            // Init hole list
            var lstHoles = new List<int>();

            for (int k = 0; k < grid.GetLength(0); k++)
            {
                for (int l = 0; l < grid.GetLength(1); l++)
                {
                    var currPos = new Point(k, l);

                    var currVal = GetHoleSize(ref grid, currPos);

                    if (currVal > 0)
                        lstHoles.Add(currVal);
                }
            }

            if (lstHoles.Count > 0)
            {
                Console.Write("[ ");
                var counter = 0;
                foreach (var hole in lstHoles.OrderBy(i => i))
                {
                    if (counter != 0)
                        Console.Write(", ");

                    Console.Write($"{hole}");

                    counter++;
                }
                Console.Write(" ]");
            }
            else
                Console.WriteLine("No holes found.");

            Console.ReadLine();
        }

        /// <summary>
        /// Recursively gets the size of a hole
        /// </summary>
        /// <param name="grid">Ref to updated grid</param>
        /// <param name="pos">Position</param>
        /// <returns>Hole size</returns>
        public static int GetHoleSize(ref int[,] grid, Point pos)
        {
            int holeSize = 0;

            try
            {
                // Boundary checks
                var isValidPos = true;
                if (pos.X < 0 || pos.X > grid.GetLength(0) - 1 || pos.Y < 0 || pos.Y > grid.GetLength(1) - 1)
                    isValidPos = false;

                // If is a valid position
                if (isValidPos)
                {
                    var currentVal = grid[pos.X, pos.Y];

                    // If the current position is a hole
                    if (currentVal == 0)
                    {
                        // Set current hole size
                        // Init to 1
                        holeSize = 1;

                        // Close current hole position to avoid being counted twice
                        grid[pos.X, pos.Y] = 1;

                        var posUpVal = new Point(pos.X - 1, pos.Y);
                        var posDownVal = new Point(pos.X + 1, pos.Y);
                        var posLeftVal = new Point(pos.X, pos.Y - 1);
                        var posRightVal = new Point(pos.X, pos.Y + 1);

                        holeSize += GetHoleSize(ref grid, posUpVal);
                        holeSize += GetHoleSize(ref grid, posDownVal);
                        holeSize += GetHoleSize(ref grid, posLeftVal);
                        holeSize += GetHoleSize(ref grid, posRightVal);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return holeSize;
        }
    }
}
