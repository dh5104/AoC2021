namespace AoC2021.D05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;

    [Exercise("Day 5: Hydrothermal Venture")]
    class D05Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D05");
        }

        protected override void Execute(string[] inputLines)
        {
            List<LineSegment> lines = new List<LineSegment>();
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;
            foreach (string line in inputLines)
            {
                char[] breakChars = { ' ', ',', '-', '>' };
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(breakChars, StringSplitOptions.RemoveEmptyEntries);
                    LineSegment s = new LineSegment(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                    lines.Add(s);
                    if (s.X1 < minX)
                    {
                        minX = s.X1;
                    }

                    if (s.X1 > maxX)
                    {
                        maxX = s.X1;
                    }

                    if (s.X2 < minX)
                    {
                        minX = s.X2;
                    }

                    if (s.X2 > maxX)
                    {
                        maxX = s.X2;
                    }

                    if (s.Y1 < minY)
                    {
                        minY = s.Y1;
                    }

                    if (s.Y1 > maxY)
                    {
                        maxY = s.Y1;
                    }

                    if (s.Y2 < minY)
                    {
                        minY = s.Y2;
                    }

                    if (s.Y2 > maxY)
                    {
                        maxY = s.Y2;
                    }
                }
            }

            long overlapCount = 0; 
            for (int j = minY; j <= maxY; j++)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    int thisPointCount = 0;
                    foreach (var a in lines)
                    {
                        if (a.In(i, j))
                        {
                            thisPointCount++;
                            if (2 <= thisPointCount)
                            {
                                overlapCount++;
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine($"Y Line {j}: {overlapCount}");
            }

            Console.WriteLine("Part 2");
            Console.WriteLine($"Ans: {overlapCount}");
        }
    }
}
