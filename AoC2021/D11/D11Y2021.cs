namespace AoC2021.D11
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using Utils._2021.GameConsole;
    using static GlobalUtils.Spatial.GridUtils;

    [Exercise("Day 11: Dumbo Octopus")]
    class D11Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D11");
        }

        protected override void Execute(string[] inputLines)
        {
            List<List<Octopus>> octopodus = new List<List<Octopus>>();
            int rowNum = 0;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    List<Octopus> thisRow = new List<Octopus>();
                    for (int x = 0; x < line.Length; x++)
                    {
                        Octopus o = new Octopus(x, rowNum, (int)char.GetNumericValue(line[x]));
                        thisRow.Add(o);
                    }

                    rowNum++;
                    octopodus.Add(thisRow);
                }
            }

            int firstSynchFlashStep = -1;
            int totalOctopusCount = octopodus.Sum(o => o.Count);
            long totalFlashes = 0;
            for(int step = 1; step <= 1000 && firstSynchFlashStep < 0;  step++)
            {
                List<Octopus> flashed = new List<Octopus>();
                for (int y = 0; y < octopodus.Count; y++)
                {
                    for (int x = 0; x < octopodus[y].Count; x++)
                    {
                        if (octopodus[y][x].IncreaseValue())
                        {
                            flashed.Add(octopodus[y][x]);
                        }
                    }
                }

                foreach(Octopus o in flashed)
                {
                    this.ProcessFlash(octopodus, o.X, o.Y);
                }

                int flashesThisRound = 0;
                for (int y = 0; y < octopodus.Count; y++)
                {
                    for (int x = 0; x < octopodus[y].Count; x++)
                    {
                        if (octopodus[y][x].Flashed)
                        {
                            flashesThisRound++;
                            octopodus[y][x].Flashed = false;
                            octopodus[y][x].Value = 0;
                        }
                    }
                }

                if (flashesThisRound >= totalOctopusCount && firstSynchFlashStep < 0)
                {
                    firstSynchFlashStep = step;
                    Console.WriteLine("Part 2");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ans: {firstSynchFlashStep} ");
                    Console.ResetColor();
                }

                totalFlashes += flashesThisRound;

                if (Debug.EnableDebugOutput)
                {
                    Debug.WriteLine($"Step {step}: {flashesThisRound}, total: {totalFlashes}");
                    for (int y = 0; y < octopodus.Count; y++)
                    {
                        for (int x = 0; x < octopodus[y].Count; x++)
                        {
                            Debug.Write(octopodus[y][x].Value);
                        }
                        Debug.WriteLine();
                    }
                    Debug.WriteLine();
                }

                if (step == 100)
                {
                    Console.WriteLine("Part 1");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Ans: {totalFlashes} ");
                    Console.ResetColor();
                    Console.WriteLine($"_______________________________________");
                }
            }
        }

        private void ProcessFlash(List<List<Octopus>> collection, int x, int y)
        {
            foreach (Octopus s in this.GetSurroundingOctopodus(collection, x, y))
            {
                bool alsoFlashed = s.IncreaseValue();
                if (alsoFlashed)
                {
                    this.ProcessFlash(collection, s.X, s.Y);
                }
            }
        }

        private List<Octopus> GetSurroundingOctopodus(List<List<Octopus>> collection, int x, int y)
        {
            List<Octopus> surrounding = new List<Octopus>();

            Octopus nw = this.GetPoint(collection, x - 1, y - 1);
            if (nw != null && !nw.Flashed)
            {
                surrounding.Add(nw);
            }

            Octopus n = this.GetPoint(collection, x, y - 1);
            if (n != null && !n.Flashed)
            {
                surrounding.Add(n);
            }

            Octopus ne = this.GetPoint(collection, x + 1, y - 1);
            if (ne != null && !ne.Flashed)
            {
                surrounding.Add(ne);
            }

            Octopus w = this.GetPoint(collection, x - 1, y);
            if (w != null && !w.Flashed)
            {
                surrounding.Add(w);
            }

            Octopus e = this.GetPoint(collection, x + 1, y);
            if (e != null && !e.Flashed)
            {
                surrounding.Add(e);
            }

            Octopus sw = this.GetPoint(collection, x - 1, y + 1);
            if (sw != null && !sw.Flashed)
            {
                surrounding.Add(sw);
            }

            Octopus s = this.GetPoint(collection, x, y + 1);
            if (s != null && !s.Flashed)
            {
                surrounding.Add(s);
            }

            Octopus se = this.GetPoint(collection, x + 1, y + 1);
            if (se != null && !se.Flashed)
            {
                surrounding.Add(se);
            }

            return surrounding;
        }

        private Octopus GetPoint(List<List<Octopus>> collection, int x, int y)
        {
            Octopus retValue = null;
            if (y >= 0 && y < collection.Count)
            {
                if (x >=0 && x < collection[y].Count)
                {
                    retValue = collection[y][x];
                }
            }

            return retValue;
        }

        public class Octopus : GenericPoint
        {
            public int Value { get; set; }
            public bool Flashed { get; set; }

            public Octopus(int x, int y, int initialValue)
                : base(x, y)
            {
                this.Value = initialValue;
            }

            public bool IncreaseValue()
            {
                if (!this.Flashed)
                {
                    this.Value += 1;
                    if (this.Value > 9)
                    {
                        this.Flashed = true;
                    }

                    return this.Flashed;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
