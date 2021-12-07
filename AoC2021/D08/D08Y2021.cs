namespace AoC2021.D08
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using Utils._2021.GameConsole;

    [Exercise("")]
    class D08Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D08");
        }

        protected override void Execute(string[] inputLines)
        {
            List<int> numbers = ParseUtils.InputAsInts(inputLines, false);
            List<long> longNumbers = ParseUtils.InputAsLongs(inputLines, false);
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                }
            }

            Console.WriteLine("Part 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: ");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");

            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: ");
            Console.ResetColor();
        }
    }
}
