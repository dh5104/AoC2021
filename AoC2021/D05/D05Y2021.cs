namespace AoC2021.D05
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D05Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D05");
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
            Console.WriteLine($"");
            Console.WriteLine($"Ans: ");

            Console.WriteLine("Part 1");
            Console.WriteLine($"");
            Console.WriteLine($"Ans: ");
        }
    }
}
