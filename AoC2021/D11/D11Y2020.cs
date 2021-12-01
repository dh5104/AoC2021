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

    [Exercise("")]
    class D11Y2021 : FileSelectionConsole, IExercise
    {
        private const char Floor = '.';
        private const char SeatEmpty = 'L';
        private const char SeatOccupied = '#';
        private const char NoChar = ' ';

        public void Execute()
        {
            Start("D11");
        }

        protected override void Execute(string[] inputLines)
        {
            List<List<char>> grid = new List<List<char>>();
            for (int y = inputLines.Length-1; y >= 0; y--)
            {
                grid.Add(inputLines[y].ToCharArray().ToList());
            }

            if (Debug.EnableDebugOutput)
            {
                CharGridUtils.PrintCharGrid(grid);
            }

            int changeCount = 0;
            int runCount = 0;
            List<List<char>> endStateGrid = CharGridUtils.CopyCharGrid(grid);
            while (changeCount > 0 || runCount == 0)
            {
                changeCount = 0;
                List<List<char>> newState = CharGridUtils.CopyCharGrid(endStateGrid);
                for (int y = 0; y < endStateGrid.Count; y++)
                {
                    for(int x = 0; x < endStateGrid[y].Count; x++)
                    {
                        char thisSpot = endStateGrid[y][x];
                        if (thisSpot != Floor)
                        {
                            List<char> surroundings = new List<char>
                            {
                                // NW, N, NE
                                this.SafeGetChar(endStateGrid, x - 1, y + 1),
                                this.SafeGetChar(endStateGrid, x, y + 1),
                                this.SafeGetChar(endStateGrid, x + 1, y + 1),

                                // W & E
                                this.SafeGetChar(endStateGrid, x - 1, y),
                                this.SafeGetChar(endStateGrid, x + 1, y),

                                // SW, S, SE
                                this.SafeGetChar(endStateGrid, x - 1, y - 1),
                                this.SafeGetChar(endStateGrid, x, y - 1),
                                this.SafeGetChar(endStateGrid, x + 1, y - 1),
                            };

                            int adjacentOccupied = surroundings.Count(c => c == SeatOccupied);

                            // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                            if (thisSpot == SeatEmpty && adjacentOccupied == 0)
                            {
                                changeCount++;
                                newState[y][x] = SeatOccupied;
                            }
                            // If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                            else if (thisSpot == SeatOccupied && adjacentOccupied >= 4)
                            {
                                changeCount++;
                                newState[y][x] = SeatEmpty;
                            }
                        }
                    }
                }

                endStateGrid = newState;
                runCount++;
                if (Debug.EnableDebugOutput)
                {
                    CharGridUtils.PrintCharGrid(endStateGrid);
                }

                Debug.WriteLine($"{changeCount} changes in run {runCount}.");
            }

            Console.WriteLine($"Part 1: {CharGridUtils.Count(endStateGrid, SeatOccupied)} occupied seats.");

            changeCount = 0;
            runCount = 0;
            List<List<char>> endStatePart2 = CharGridUtils.CopyCharGrid(grid);
            while (changeCount > 0 || runCount == 0)
            {
                changeCount = 0;
                List<List<char>> newState = CharGridUtils.CopyCharGrid(endStatePart2);
                for (int y = 0; y < endStatePart2.Count; y++)
                {
                    for (int x = 0; x < endStatePart2[y].Count; x++)
                    {
                        char thisSpot = endStatePart2[y][x];
                        if (thisSpot != Floor)
                        {
                            List<char> surroundings = new List<char>
                            {
                                this.LookForSeat(endStatePart2, x, y, Direction.NW),
                                this.LookForSeat(endStatePart2, x, y, Direction.N),
                                this.LookForSeat(endStatePart2, x, y, Direction.NE),
                                this.LookForSeat(endStatePart2, x, y, Direction.W),
                                this.LookForSeat(endStatePart2, x, y, Direction.E),
                                this.LookForSeat(endStatePart2, x, y, Direction.SW),
                                this.LookForSeat(endStatePart2, x, y, Direction.S),
                                this.LookForSeat(endStatePart2, x, y, Direction.SE),
                            };

                            int adjacentOccupied = surroundings.Count(c => c == SeatOccupied);

                            // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                            if (thisSpot == SeatEmpty && adjacentOccupied == 0)
                            {
                                changeCount++;
                                newState[y][x] = SeatOccupied;
                            }
                            // If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                            else if (thisSpot == SeatOccupied && adjacentOccupied >= 5)
                            {
                                changeCount++;
                                newState[y][x] = SeatEmpty;
                            }
                        }
                    }
                }

                endStatePart2 = newState;
                runCount++;
                if (Debug.EnableDebugOutput)
                {
                    CharGridUtils.PrintCharGrid(endStatePart2);
                }

                Debug.WriteLine($"{changeCount} changes in run {runCount}.");
            }

            Console.WriteLine($"Part 2: {CharGridUtils.Count(endStatePart2, SeatOccupied)} occupied seats.");
        }

        private char LookForSeat(List<List<char>> charGrid, int x, int y, Direction direction)
        {
            Tuple<int, int> lookPoint = GridUtils.MovePoint(x, y, direction);
            char ret = this.SafeGetChar(charGrid, lookPoint.Item1, lookPoint.Item2);
            while (ret != NoChar
                && ret != SeatEmpty
                && ret != SeatOccupied)
            {
                lookPoint = GridUtils.MovePoint(lookPoint, direction);
                ret = this.SafeGetChar(charGrid, lookPoint.Item1, lookPoint.Item2);
            }

            return ret;
        }

        private char SafeGetChar(List<List<char>> charGrid, int x, int y)
        {
            char ret = NoChar;
            if (y >= 0 && y < charGrid.Count)
            {
                if (x >= 0 && x < charGrid[y].Count)
                {
                    ret = charGrid[y][x];
                }
            }

            return ret;
        }
    }
}
