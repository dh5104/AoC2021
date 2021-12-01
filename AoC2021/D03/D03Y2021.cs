namespace AoC2021.D03
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;

    [Exercise("")]
    class D03Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D03");
        }

        protected override void Execute(string[] inputLines)
        {
            List<GenericPoint> trees = new List<GenericPoint>();
            int maxX = 0;
            int maxY = inputLines.Length - 1;
            for (int y = maxY; y >= 0; y--)
            {
                string thisLine = inputLines[maxY - y];
                maxX = thisLine.Length;
                for (int x = 0; x < thisLine.Length; x++)
                {
                    if (thisLine[x] == '#')
                    {
                        GenericPoint newTree = new GenericPoint(x, y);
                        trees.Add(newTree);
                    }
                }
            }

            // sled, because toboggan is too many chars
            GenericPoint sled = new GenericPoint(0, maxY);
            int treesHit = 0;
            while (sled.Y > 0)
            {
                sled.Y--;
                sled.X = (sled.X + 3) % maxX;
                if (trees.Any(t => t.Equals(sled)))
                {
                    treesHit++;
                }
            }

            var ansPart1 = treesHit;
            Console.WriteLine($"Part 1: {ansPart1} trees hit.");

            // Item1: X (or right)
            // Item2: Y (or down)
            List<Tuple<int, int>> slopes = new List<Tuple<int, int>>();
            slopes.Add(new Tuple<int, int>(1, 1));
            slopes.Add(new Tuple<int, int>(3, 1));
            slopes.Add(new Tuple<int, int>(5, 1));
            slopes.Add(new Tuple<int, int>(7, 1));
            slopes.Add(new Tuple<int, int>(1, 2));
            
            long ansPart2 = 1;

            foreach (Tuple<int, int> slope in slopes)
            {
                // reset skiier & trees
                treesHit = 0;
                sled.X = 0;
                sled.Y = maxY;
                while (sled.Y > 0)
                {
                    sled.Y -= slope.Item2;
                    sled.X = (sled.X + slope.Item1) % maxX;
                    if (trees.Any(t => t.Equals(sled)))
                    {
                        treesHit++;
                    }
                }

                Debug.WriteLine($"Slope Right {slope.Item1}, Down {slope.Item2} hit {treesHit} trees.");
                ansPart2 *= treesHit;
            }

            Console.WriteLine($"Part 2: {ansPart2}");
        }
    }
}
