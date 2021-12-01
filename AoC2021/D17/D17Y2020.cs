namespace AoC2021.D17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;

    [Exercise("")]
    class D17Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D17");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Cube> activeCubes = new List<Cube>();
            for (int y = 0; y < inputLines.Length; y++)
            {
                for(int x = 0; x < inputLines[y].Length; x++)
                {
                    if (inputLines[y][x] == '#')
                    {
                        activeCubes.Add(new Cube(x, y, 0));
                        //activeCubes.Add(new Cube(x, y, 1));
                        //activeCubes.Add(new Cube(x, y, -1));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                DimRanges ranges = new DimRanges(activeCubes);
                List<Cube> newActives = new List<Cube>();

                for (int x = ranges.MinX-1; x <= ranges.MaxX + 1; x++)
                {
                    for (int y = ranges.MinY - 1; y <= ranges.MaxY + 1; y++)
                    {
                        for (int z = ranges.MinZ - 1; z <= ranges.MaxZ + 1; z++)
                        {
                            // find the cubes in each direction of the current one
                            int activeCount = 0;

                            ////// exact neighbors only, differing by only one position, which means it's only checking 6 locs:
                            ////activeCount += this.IsActive(activeCubes, x + 1, y, z) ? 1 : 0;
                            ////activeCount += this.IsActive(activeCubes, x, y + 1, z) ? 1 : 0;
                            ////activeCount += this.IsActive(activeCubes, x, y, z + 1) ? 1 : 0;
                            ////activeCount += this.IsActive(activeCubes, x - 1, y, z) ? 1 : 0;
                            ////activeCount += this.IsActive(activeCubes, x, y - 1, z) ? 1 : 0;
                            ////activeCount += this.IsActive(activeCubes, x, y, z - 1) ? 1 : 0;

                            activeCount += this.IsActive(activeCubes, x + 1, y + 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y + 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y + 1, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y - 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y - 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x + 1, y - 1, z - 1) ? 1 : 0;

                            activeCount += this.IsActive(activeCubes, x, y + 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y + 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y + 1, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y, z + 1) ? 1 : 0;
                            // nope - this is our current spot: activeCount += this.IsActive(activeCubes, x, y, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y - 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y - 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x, y - 1, z - 1) ? 1 : 0;

                            activeCount += this.IsActive(activeCubes, x - 1, y + 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y + 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y + 1, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y, z - 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y - 1, z + 1) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y - 1, z) ? 1 : 0;
                            activeCount += this.IsActive(activeCubes, x - 1, y - 1, z - 1) ? 1 : 0;

                            if (activeCount == 3)
                            {
                                newActives.Add(new Cube(x, y, z));
                            }
                            else if (activeCount == 2 && this.IsActive(activeCubes, x, y, z))
                            {
                                // persist it as active
                                newActives.Add(new Cube(x, y, z));
                            }
                        }
                    }
                }

                activeCubes.Clear();
                activeCubes.AddRange(newActives);

                if (Debug.EnableDebugOutput)
                {
                    DimRanges debug_r = new DimRanges(newActives);
                    for (int z = debug_r.MinZ - 1; z <= debug_r.MaxZ + 1; z++)
                    {
                        Debug.WriteLine($"z={z}");
                        for (int y = debug_r.MinY - 1; y <= debug_r.MaxY + 1; y++)
                        {
                            for (int x = debug_r.MinX - 1; x <= debug_r.MaxX + 1; x++)
                            {
                            
                                if (IsActive(activeCubes, x, y, z))
                                {
                                    Debug.Write("#");
                                }
                                else
                                {
                                    Debug.Write(".");
                                }
                            }
                            Debug.WriteLine();
                        }
                    }
                }
            }
            
            Console.WriteLine($"Part 1: {activeCubes.Count} active cubes after 6 cycles.");

            List<Hypercube> activeHypercubes = new List<Hypercube>();
            for (int y = 0; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    if (inputLines[y][x] == '#')
                    {
                        activeHypercubes.Add(new Hypercube(x, y, 0, 0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                DimRanges ranges = new DimRanges(activeHypercubes);
                List<Hypercube> newActiveHCs = new List<Hypercube>();

                for (int x = ranges.MinX - 3; x <= ranges.MaxX + 3; x++)
                {
                    for (int y = ranges.MinY - 3; y <= ranges.MaxY + 3; y++)
                    {
                        for (int z = ranges.MinZ - 3; z <= ranges.MaxZ + 3; z++)
                        {
                            for (int w = ranges.MinW - 3; w <= ranges.MaxW + 3; w++)
                            {
                                int activeneighbors = this.ActiveNeighborCount(activeHypercubes, x, y, z, w);
                                if (3 == activeneighbors || (2 == activeneighbors && this.IsActive(activeHypercubes, x, y, z, w)))
                                {
                                    newActiveHCs.Add(new Hypercube(x, y, z, w));
                                }
                            }
                        }
                    }
                }

                activeHypercubes.Clear();
                activeHypercubes.AddRange(newActiveHCs);

                if (Debug.EnableDebugOutput)
                {
                    DimRanges debug_r = new DimRanges(newActiveHCs);
                    for (int w = debug_r.MinW - 1; w <= debug_r.MaxW + 1; w++)
                    {
                        for (int z = debug_r.MinZ - 1; z <= debug_r.MaxZ + 1; z++)
                        {
                            Debug.WriteLine($"z={z}, w={w}");
                            for (int y = debug_r.MinY - 1; y <= debug_r.MaxY + 1; y++)
                            {
                                for (int x = debug_r.MinX - 1; x <= debug_r.MaxX + 1; x++)
                                {

                                    if (IsActive(activeHypercubes, x, y, z, w))
                                    {
                                        Debug.Write("#");
                                    }
                                    else
                                    {
                                        Debug.Write(".");
                                    }
                                }
                                Debug.WriteLine();
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Part 2: {activeHypercubes.Count} active hypercubes after 6 cycles.");
        }

        private int ActiveNeighborCount(List<Hypercube> hypercubes, int x, int y, int z, int w)
        {
            int actives = hypercubes.Count(hc => hc.X >= x - 1
                            && hc.X <= x + 1
                            && hc.Y <= y + 1
                            && hc.Y >= y - 1
                            && hc.Z <= z + 1
                            && hc.Z >= z - 1
                            && hc.W <= w + 1
                            && hc.W >= w - 1);

            if (actives > 0 && this.IsActive(hypercubes, x, y, z, w))
            {
                actives--;
            }

            return actives;
        }

        private bool IsActive(List<Hypercube> actives, int x, int y, int z, int w)
        {
            return actives.Any(hc => hc.X == x && hc.Y == y && hc.Z == z && hc.W == w);
        }

        private bool IsActive(List<Cube> actives, int x, int y, int z)
        {
            return actives.Any(c => c.X == x && c.Y == y && c.Z == z);
        }

        public class DimRanges
        {
            public int MinX { get; set; }
            public int MaxX { get; set; }
            public int MinY { get; set; }
            public int MaxY { get; set; }
            public int MinZ { get; set; }
            public int MaxZ { get; set; }

            public int MinW { get; set; }

            public int MaxW { get; set; }

            public DimRanges(List<Cube> cubes)
            {
                this.MinX = int.MaxValue;
                this.MinY = int.MaxValue;
                this.MinZ = int.MaxValue;

                this.MaxX = int.MinValue;
                this.MaxY = int.MinValue;
                this.MaxZ = int.MinValue;

                foreach (Cube c in cubes)
                {
                    if (c.X < this.MinX)
                    {
                        this.MinX = c.X;
                    }

                    if (c.X > this.MaxX)
                    {
                        this.MaxX = c.X;
                    }

                    if (c.Y < this.MinY)
                    {
                        this.MinY = c.Y;
                    }

                    if (c.Y > this.MaxY)
                    {
                        this.MaxY = c.Y;
                    }

                    if (c.Z < this.MinZ)
                    {
                        this.MinZ = c.Z;
                    }

                    if (c.Z > this.MaxZ)
                    {
                        this.MaxZ = c.Z;
                    }
                }
            }
            public DimRanges(List<Hypercube> cubes)
            {
                this.MinX = int.MaxValue;
                this.MinY = int.MaxValue;
                this.MinZ = int.MaxValue;
                this.MinW = int.MaxValue;

                this.MaxX = int.MinValue;
                this.MaxY = int.MinValue;
                this.MaxZ = int.MinValue;
                this.MaxW = int.MinValue;

                foreach (Hypercube c in cubes)
                {
                    if (c.X < this.MinX)
                    {
                        this.MinX = c.X;
                    }

                    if (c.X > this.MaxX)
                    {
                        this.MaxX = c.X;
                    }

                    if (c.Y < this.MinY)
                    {
                        this.MinY = c.Y;
                    }

                    if (c.Y > this.MaxY)
                    {
                        this.MaxY = c.Y;
                    }

                    if (c.Z < this.MinZ)
                    {
                        this.MinZ = c.Z;
                    }

                    if (c.Z > this.MaxZ)
                    {
                        this.MaxZ = c.Z;
                    }

                    if (c.W < this.MinW)
                    {
                        this.MinW = c.W;
                    }

                    if (c.W > this.MaxW)
                    {
                        this.MaxW = c.W;
                    }
                }
            }
        }

        public class Cube : GenericPoint
        {
            public Cube(int x, int y, int z = 0)
                : base (x, y, z)
            {
            }

            public Cube(Cube p)
                : this (p.X, p.Y, p.Z)
            {
            }
        }

        public class Hypercube : GenericPoint
        {
            public int W { get; set; }

            public Hypercube(int x, int y, int z, int w)
                : base (x, y, z)
            {
                this.W = w;
            }
        }
    }
}
