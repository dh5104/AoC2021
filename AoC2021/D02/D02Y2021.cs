namespace AoC2021.D02
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 02: Dive!")]
    class D02Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D02");
        }

        protected override void Execute(string[] inputLines)
        {
            int horz = 0;
            int depth = 0;
            foreach (string line in inputLines)
            {
                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int distance = int.Parse(parts[1]);
                switch(parts[0])
                {
                    case "forward":
                        horz += distance;
                        break;
                    case "down":
                        depth += distance;
                        break;
                    case "up":
                        depth -= distance;
                        break;
                }
            }

            Console.WriteLine($"Part 1: Horizontal: {horz}, Depth: {depth}");
            Console.WriteLine($"Answer: {horz * depth}");

            horz = 0;
            depth = 0;
            int aim = 0;
            foreach (string line in inputLines)
            {
                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int distance = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        horz += distance;
                        depth += (distance * aim);
                        break;
                    case "down":
                        aim += distance;
                        break;
                    case "up":
                        aim -= distance;
                        break;
                }
            }

            Console.WriteLine($"Part 2: Horizontal: {horz}, Depth: {depth}, Aim: {aim}");
            Console.WriteLine($"Answer: {horz * depth}");
        }
    }
}
