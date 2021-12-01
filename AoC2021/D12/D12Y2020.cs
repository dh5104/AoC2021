namespace AoC2021.D12
{
    using System;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using static GlobalUtils.Spatial.GridUtils;

    [Exercise("")]
    class D12Y2021 : FileSelectionConsole, IExercise
    {
        private enum Command
        {
            N,
            S,
            E,
            W,
            L,
            R,
            F,
        }

        public void Execute()
        {
            Start("D12");
        }

        protected override void Execute(string[] inputLines)
        {
            int x = 0;
            int y = 0;
            GridUtils.Direction direction = Direction.E;
            foreach(string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (Enum.TryParse<Command>(line[0].ToString(), out Command order))
                    {
                        int amount = int.Parse(line.Substring(1));
                        switch(order)
                        {
                            case Command.N:
                                y += amount;
                                break;
                            case Command.S:
                                y -= amount;
                                break;
                            case Command.E:
                                x += amount;
                                break;
                            case Command.W:
                                x -= amount;
                                break;
                            case Command.L:
                            case Command.R:
                                for (int i = 0; i < amount/90; i++)
                                {
                                    switch (direction)
                                    {
                                        case Direction.N:
                                            direction = order == Command.R ? Direction.E : Direction.W;
                                            break;
                                        case Direction.S:
                                            direction = order == Command.R ? Direction.W : Direction.E;
                                            break;
                                        case Direction.E:
                                            direction = order == Command.R ? Direction.S: Direction.N;
                                            break;
                                        case Direction.W:
                                            direction = order == Command.R ? Direction.N : Direction.S;
                                            break;
                                    }
                                }

                                break;
                            case Command.F:
                                switch(direction)
                                {
                                    case Direction.N:
                                        y += amount;
                                        break;
                                    case Direction.S:
                                        y -= amount;
                                        break;
                                    case Direction.E:
                                        x += amount;
                                        break;
                                    case Direction.W:
                                        x -= amount;
                                        break;
                                }

                                break;
                        }
                    }
                }

                Debug.WriteLine($"ship at {x},{y} = {Math.Abs(x) + Math.Abs(y)}");
            }

            Console.WriteLine($"Part 1: {x},{y} = {Math.Abs(x) + Math.Abs(y)}");

            int shipx = 0;
            int shipy = 0;
            int wpx = 10;
            int wpy = 1;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (Enum.TryParse<Command>(line[0].ToString(), out Command order))
                    {
                        int amount = int.Parse(line.Substring(1));
                        int curwpx = wpx;
                        int curwpy = wpy;
                        switch (order)
                        {
                            case Command.N:
                                wpy += amount;
                                break;
                            case Command.S:
                                wpy -= amount;
                                break;
                            case Command.E:
                                wpx += amount;
                                break;
                            case Command.W:
                                wpx -= amount;
                                break;
                            case Command.L:
                                switch (amount % 360)
                                {
                                    case 90:
                                        wpx = -curwpy;
                                        wpy = curwpx;
                                        break;
                                    case 180:
                                        // swap signs on both values
                                        wpx = -wpx;
                                        wpy = -wpy;
                                        break;
                                    case 270:
                                        wpx = curwpy;
                                        wpy = -curwpx;
                                        break;
                                }
                                break;
                            case Command.R:
                                switch (amount % 360)
                                {
                                    case 90:
                                        wpx = curwpy;
                                        wpy = -curwpx;
                                        break;
                                    case 180:
                                        // swap signs on both values
                                        wpx = -wpx;
                                        wpy = -wpy;
                                        break;
                                    case 270:
                                        wpx = -curwpy;
                                        wpy = curwpx;
                                        break;
                                }
                                break;
                            case Command.F:
                                // move towards the waypoint n times
                                for (int i = 0; i < amount; i++)
                                {
                                    shipx += wpx;
                                    shipy += wpy;
                                }
                                break;
                        }
                    }
                }

                Debug.WriteLine($"ship at {shipx},{shipy}, waypoint at {wpx},{wpy}");
            }

            Console.WriteLine($"Part 2: ship {shipx},{shipy} = {Math.Abs(shipx) + Math.Abs(shipy)}");
        }
    }
}
