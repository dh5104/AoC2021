namespace AoC2021.D13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Math;
    using GlobalUtils.Spatial;

    [Exercise("Day 13: Transparent Origami")]
    class D13Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D13");
        }

        protected override void Execute(string[] inputLines)
        {
            List<GenericPoint> points = new List<GenericPoint>();
            List<Tuple<string, int>> folds = new List<Tuple<string, int>>();
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line.Contains("fold"))
                    {
                        int assignmentIndex = line.IndexOf("=");
                        int value = int.Parse(line.Substring(assignmentIndex + 1));
                        string plane = line.Substring(assignmentIndex - 1, 1);
                        folds.Add(new Tuple<string, int>(plane, value));
                    }
                    else
                    {
                        string[] parts = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                        int x = int.Parse(parts[0]); ;
                        int y = int.Parse(parts[1]);
                        points.Add(new GenericPoint(x, y));
                        if (x > maxX)
                        {
                            maxX = x;
                        }

                        if (x < minX)
                        {
                            minX = x;
                        }

                        if (y > maxY)
                        {
                            maxY = y;
                        }

                        if (y < minY)
                        {
                            minY = y;
                        }
                    }
                }
            }

            for (int i = 0; i < folds.Count; i++)
            {
                int foldLine = folds[i].Item2;
                switch (folds[i].Item1)
                {
                    // vertical fold along the x
                    case "x":
                        List<GenericPoint> newPoints = new List<GenericPoint>();
                        foreach (GenericPoint point in points)
                        {
                            if (point.X > foldLine)
                            {
                                int newX = foldLine - (int)Math.Abs(point.X - foldLine);
                                if (newX < 0)
                                {
                                    throw new ApplicationException("Did not expect to go negative");
                                }

                                // if there's already a point there...
                                if (!points.Any(p => p.X == newX && p.Y == point.Y))
                                {
                                    Debug.WriteLine($"{i}: Moving {point.X},{point.Y} to {newX},{point.Y}.");
                                    point.X = newX;
                                    newPoints.Add(point);
                                }
                                else
                                {
                                    Debug.WriteLine($"{i}: {newX},{point.Y} already exists, dropping {point.X},{point.Y}");
                                }
                            }
                            else
                            {
                                newPoints.Add(point);
                            }
                        }

                        points.Clear();
                        points.AddRange(newPoints);
                        break;
                    case "y":
                        List<GenericPoint> newPoints2 = new List<GenericPoint>();

                        // fold along the x plane, so change the Y's
                        foreach (GenericPoint point in points)
                        {
                            if (point.Y > foldLine)
                            {
                                int newY = foldLine - (int)Math.Abs(point.Y - foldLine);
                                if (newY < 0)
                                {
                                    throw new ApplicationException("Did not expect to go negative");
                                }

                                // if there's already a point there...
                                if (!points.Any(p => p.X == point.X && p.Y == newY))
                                {
                                    Debug.WriteLine($"{i}: Moving {point.X},{point.Y} to {point.X},{newY}.");
                                    point.Y = newY;
                                    newPoints2.Add(point);
                                }
                                else
                                {
                                    Debug.WriteLine($"{i}: {point.X},{newY} already exists, dropping {point.X},{point.Y}");
                                }
                            }
                            else
                            {
                                newPoints2.Add(point);
                            }
                        }

                        points.Clear();
                        points.AddRange(newPoints2);
                        break;
                }

                if (i == 0)
                {
                    Console.WriteLine("Part 1");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Ans: {points.Count}");
                    Console.ResetColor();
                    Console.WriteLine($"_______________________________________");
                }
            }

            this.PrintGrid(GridUtils.MakeGrid(points));

            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: ");
            Console.ResetColor();
        }

        private void PrintGrid<T>(List<List<T>> points) where T : GenericPoint
        {
            // assumes a fully populated square grid, meaning all the lines have the same contents.
            for (int y = 0; y < points.Count; y++)
            {
                if (points[y].Any(p => null != p))
                { 
                    for (int x = 0; x < points[y].Count; x++)
                    {
                        if (null != points[y][x])
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                } 
            }
        }
    }
}
