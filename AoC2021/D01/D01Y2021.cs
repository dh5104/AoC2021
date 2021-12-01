namespace AoC2021.D01
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 1: Sonar Sweep")]
    class D01Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D01");
        }

        protected override void Execute(string[] inputLines)
        {
            List<int> numbers = ParseUtils.InputAsInts(inputLines, false);
            int numIncreases = 0;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    numIncreases++;
                }
            }

            Console.WriteLine($"Part1: {numIncreases}");
            Console.WriteLine("____");

            int numIncreasesPart2 = 0;
            int lastthreeMeasure = 0;
            for (int i = 2; i < numbers.Count; i++)
            {
                int threeMeasureTotal = numbers[i] + numbers[i - 1] + numbers[i - 2];
                if (threeMeasureTotal > lastthreeMeasure && lastthreeMeasure > 0)
                {
                    numIncreasesPart2++;
                }

                lastthreeMeasure = threeMeasureTotal;
            }
            Console.WriteLine($"Part 2: {numIncreasesPart2}");
        }
    }
}
