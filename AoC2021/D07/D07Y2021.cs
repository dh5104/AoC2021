namespace AoC2021.D07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 7: ")]
    class D07Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D07");
        }

        protected override void Execute(string[] inputLines)
        {
            List<int> numbers = new List<int>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach(string part in parts)
                    {
                        numbers.Add(int.Parse(part));
                    }
                }
            }

            int min = numbers.Min();
            int max = numbers.Max();
            Dictionary<int, int> numbers2 = new Dictionary<int, int>();
            for (int i = min; i <= max; i++)
            {
                int fuelCost = 0;
                foreach (int number in numbers)
                {
                    fuelCost += System.Math.Abs(number - i);
                }

                numbers2[i] = fuelCost;
            }


            Console.WriteLine("Part 1");
            Console.WriteLine($"");
            Console.WriteLine($"Ans: {numbers2.Values.Min()}");

            Dictionary<int, int> numbers3 = new Dictionary<int, int>();
            Dictionary<int, int> costs = new Dictionary<int, int>();
            int lowestTotalCost = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                int fuelCost = 0;
                foreach (int number in numbers)
                {
                    int difference = System.Math.Abs(number - i);
                    if (!costs.ContainsKey(difference))
                    {
                        int thisCost = 0;
                        for (int j = 1; j <= difference; j++)
                        {
                            thisCost += j;
                        }

                        costs[difference] = thisCost;
                    }

                    fuelCost += costs[difference];
                    if (fuelCost > lowestTotalCost)
                    {
                        break;
                    }
                }

                numbers3[i] = fuelCost;
                if (fuelCost < lowestTotalCost)
                {
                    lowestTotalCost = fuelCost;
                }
            }
            Console.WriteLine("Part 2");
            Console.WriteLine($"");
            Console.WriteLine($"Ans: {numbers3.Values.Min()}");
        }
    }
}
