namespace AoC2021.D09
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using Utils._2021.GameConsole;

    [Exercise("Day 9: Smoke Basin")]
    class D09Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D09");
        }

        protected override void Execute(string[] inputLines)
        {
            List<List<Point>> grid = new List<List<Point>>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    List<Point> row = new List<Point>();
                    int y = grid.Count;
                    for (int x = 0; x < line.Length; x++)
                    {
                        row.Add(new Point(x, y, (int)char.GetNumericValue(line[x])));
                    }

                    grid.Add(row);
                }
            }

            List<Point> lowestPoints = new List<Point>();
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    Point p = grid[y][x];
                    if (p.Z >= 9)
                    {
                        p.IsLowest = false;
                    }
                    else if (p.Z == 0)
                    {
                        p.IsLowest = true;
                        lowestPoints.Add(p);
                    }
                    else
                    {
                        // check surroundings
                        int northValue = this.GetValueOfPoint(grid, x, y + 1);
                        int southValue = this.GetValueOfPoint(grid, x, y - 1);
                        int eastValue = this.GetValueOfPoint(grid, x + 1, y);
                        int westValue = this.GetValueOfPoint(grid, x - 1, y);
                        if (p.Z < northValue
                            && p.Z < southValue
                            && p.Z < eastValue
                            &&  p.Z < westValue)
                        {
                            p.IsLowest = true;
                            lowestPoints.Add(p);
                        }
                    }
                }
                Console.WriteLine();
            }

            GridUtils.PrintGrid(grid);
            int countOfLowest = grid.Sum(r => r.Where(p => true == p.IsLowest).Sum(p => p.Z + 1));

            Console.WriteLine("Part 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: {countOfLowest}");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");

            List<List<Point>> basins = new List<List<Point>>(); 
            foreach(var p in lowestPoints)
            {
                List<Point> points = this.GetFlowPoints(grid, new List<Point>() { p }, p.X, p.Y);
                basins.Add(points);
            }
            var biggestBasins = basins.OrderByDescending(b => b.Count).Take(3);
            long answer = biggestBasins.ElementAt(0).Count * biggestBasins.ElementAt(1).Count * biggestBasins.ElementAt(2).Count;
            Console.WriteLine("Part 2");
            Console.WriteLine($"{basins.Count} basins.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: {answer}");
            Console.ResetColor();
        }

        private List<Point> GetFlowPoints(List<List<Point>> grid, List<Point> alreadyGathered, int x, int y)
        {
            List<Point> points = alreadyGathered ?? new List<Point>();
            Point northValue = this.GetPoint(grid, x, y + 1);
            if (null != northValue 
                && northValue.Z < 9
                && !points.Any(p => p.X == northValue.X && p.Y == northValue.Y))
            {
                points.Add(northValue);
                points = this.GetFlowPoints(grid, points, northValue.X, northValue.Y);
            }

            Point southValue = this.GetPoint(grid, x, y - 1);
            if (null != southValue
                && southValue.Z < 9
                && !points.Any(p => p.X == southValue.X && p.Y == southValue.Y))
            {
                points.Add(southValue);
                points = this.GetFlowPoints(grid, points, southValue.X, southValue.Y);
            }

            Point eastValue = this.GetPoint(grid, x + 1, y);
            if (null != eastValue
                && eastValue.Z < 9
                && !points.Any(p => p.X == eastValue.X && p.Y == eastValue.Y))
            {
                points.Add(eastValue);
                points = this.GetFlowPoints(grid, points, eastValue.X, eastValue.Y);
            }

            Point westValue = this.GetPoint(grid, x - 1, y);
            if (null != westValue
                && westValue.Z < 9
                && !points.Any(p => p.X == westValue.X && p.Y == westValue.Y))
            {
                points.Add(westValue);
                points = this.GetFlowPoints(grid, points, westValue.X, westValue.Y);
            }

            return points;
        }

        private Point GetPoint(List<List<Point>> grid, int x, int y)
        {
            Point p = null;
            if (x >= 0 && x < grid[0].Count)
            {
                if (y >= 0 && y < grid.Count)
                {
                    p = grid[y][x];
                }
            }

            return p;
        }

        private int GetValueOfPoint(List<List<Point>> grid, int x, int y)
        {
            if (x >= 0 && x < grid[0].Count)
            {
                if (y >= 0 && y < grid.Count)
                {
                    return grid[y][x].Z;
                }
            }

            return 10;
        }

        public class Point : GenericPoint
        {
            public bool? IsLowest { get; set; }

            public Point(int x, int y, int height)
                : base (x, y, height)
            {
            }

            public override string ToString()
            {
                if (true == this.IsLowest)
                {
                    return "*" + this.Z.ToString();
                }
                else
                {
                    return this.Z.ToString();
                }
            }
        }
    }
}
