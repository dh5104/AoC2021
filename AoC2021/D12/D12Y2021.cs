namespace AoC2021.D12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using static GlobalUtils.Spatial.GridUtils;

    [Exercise("Day 12: Passage Pathing")]
    class D12Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D12");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Paths> paths = new List<Paths>();
            Dictionary<string, List<Cave>> connections = new Dictionary<string, List<Cave>>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries);
                    paths.Add(new Paths(parts[0], parts[1]));

                    if (parts[1] != "start")
                    {
                        if (!connections.ContainsKey(parts[0]))
                        {
                            connections.Add(parts[0], new List<Cave>());
                        }


                        connections[parts[0]].Add(new Cave(parts[1]));
                    }

                    if (parts[0] != "start")
                    {
                        if (!connections.ContainsKey(parts[1]))
                        {
                            connections.Add(parts[1], new List<Cave>());
                        }

                        connections[parts[1]].Add(new Cave(parts[0]));
                    }
                }
            }

            List<List<Cave>> Routes = new List<List<Cave>>();              
            foreach(Paths path in paths.Where(p => p.Point1.Name == "start" || p.Point2.Name == "start"))
            {
                if (path.Point1.Name == "start")
                {
                    Routes.Add(new List<Cave>() { path.Point1, path.Point2 });
                }
                else
                {
                    Routes.Add(new List<Cave>() { path.Point2, path.Point1 });
                }
            }

            List<List<Cave>> CompletedRoutes = new List<List<Cave>>();
            while (Routes.Count > 0)
            {
                List<List<Cave>> UpdatedRoutes = new List<List<Cave>>();
                foreach(List<Cave> route in Routes)
                {
                    Cave lastCaveInPath = route.Last();
                    if (lastCaveInPath.Name == "end")
                    {
                        CompletedRoutes.Add(route);
                    }
                    else
                    {
                        foreach(Cave potential in connections[lastCaveInPath.Name])
                        {
                            bool addThis = false;
                            if (potential.isLarge && potential.Name != lastCaveInPath.Name)
                            {
                                addThis = true;
                            }
                            else
                            {
                                // only add the smaller caves if we haven't been there before
                                if (!route.Any(r => r.Name == potential.Name))
                                {
                                    addThis = true;
                                }
                            }

                            if (addThis)
                            {
                                List<Cave> newPath = new List<Cave>();
                                newPath.AddRange(route);
                                newPath.Add(potential);
                                UpdatedRoutes.Add(newPath);
                            }
                        }
                    }
                }

                Routes.Clear();
                Routes.AddRange(UpdatedRoutes);
            }

            if (Debug.EnableDebugOutput)
            {
                foreach(List<Cave> routes in CompletedRoutes)
                {
                    Debug.WriteLine(string.Join(",", routes.Select(r => r.Name)));
                }
            }

            Console.WriteLine("Part 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: {CompletedRoutes.Count}");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");

            List<Route> Routes2 = new List<Route>();
            foreach (Paths path in paths.Where(p => p.Point1.Name == "start" || p.Point2.Name == "start"))
            {
                Route route = new Route();
                if (path.Point1.Name == "start")
                {
                    route.AddCave(path.Point1);
                    route.AddCave(path.Point2);
                }
                else
                {
                    route.AddCave(path.Point2);
                    route.AddCave(path.Point1);
                }

                Routes2.Add(route);
            }

            List<Route> CompletedRoutes2 = new List<Route>();
            while (Routes2.Count > 0)
            {
                List<Route> UpdatedRoutes = new List<Route>();
                foreach (Route route in Routes2)
                {
                    if (route.CurrentCave() == "end")
                    {
                        bool duplicate = false;
                        foreach(Route completed in CompletedRoutes2.Where(c => c.Caves.Count == route.Caves.Count))
                        {
                            bool matches = true;
                            for(int x = 0; x < route.Caves.Count; x++)
                            {
                                if (completed.Caves[x] != route.Caves[x])
                                {
                                    matches = false;
                                    break;
                                }
                            }

                            if (matches)
                            {
                                duplicate = true;
                                break;
                            }
                        }

                        if (!duplicate)
                        {
                            CompletedRoutes2.Add(route);
                        }
                    }
                    else
                    {
                        foreach (Cave potential in connections[route.CurrentCave()])
                        {
                            bool addThis = false;
                            if (potential.Name != route.CurrentCave() && potential.Name != "start")
                            {
                                if (potential.isLarge)
                                {
                                    addThis = true;
                                }
                                else
                                {
                                    // still add the small cave if we've never been there
                                    if (!route.Caves.Any(r => r.Name == potential.Name))
                                    {
                                        addThis = true;
                                    }
                                    // now we can go to one small cave twice, as long as it's not 'start' or 'end'
                                    // and we haven't gone to another small cave twice.
                                    else if (!route.DoubleVisitedSmallCave)
                                    {
                                        addThis = true;
                                    }
                                }
                            }

                            if (addThis)
                            {
                                Route newPath = new Route();
                                newPath.Caves.AddRange(route.Caves);
                                newPath.DoubleVisitedSmallCave = route.DoubleVisitedSmallCave;
                                newPath.AddCave(potential);
                                UpdatedRoutes.Add(newPath);
                            }
                        }
                    }
                }

                Routes2.Clear();
                Routes2.AddRange(UpdatedRoutes);
            }

            if (Debug.EnableDebugOutput)
            {
                foreach (Route routes in CompletedRoutes2)
                {
                    Console.WriteLine(routes);
                }
            }

            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: {CompletedRoutes2.Count} ");
            Console.ResetColor();
        }

        public class Paths
        {
            public Cave Point1 { get; set; }
            public Cave Point2 { get; set; }

            public Paths(string name1, string name2)
            {
                Point1 = new Cave(name1);
                Point2 = new Cave(name2);
            }
        }

        public class Cave
        {
            public string Name { get; set; }
            public bool isLarge { get; set; }

            public override string ToString()
            {
                return this.Name;
            }

            public Cave(string name)
            {
                this.Name = name;
                if (char.IsUpper(this.Name[0]))
                {
                    isLarge = true;
                }
            }
        }

        public class Route
        {
            public Route()
            {
                this.Caves = new List<Cave>();
            }

            public List<Cave> Caves { get; set; }
            public bool DoubleVisitedSmallCave { get; set; }

            public void AddCave(Cave c)
            {
                if (false == c.isLarge && this.Caves.Any(tc => tc.Name == c.Name))
                {
                    this.DoubleVisitedSmallCave = true;
                }

                this.Caves.Add(c);
            }

            public string CurrentCave()
            {
                return this.Caves.Last().Name;
            }

            public override string ToString()
            {
                return string.Join(",", this.Caves.Select(r => r.Name));
            }
        }

    }
}
