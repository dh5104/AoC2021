namespace AoC2021.D09
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using Utils._2021.GameConsole;

    [Exercise("")]
    class D09Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D09");
        }

        protected override void Execute(string[] inputLines)
        {
            List<long> nums = new List<long>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (long.TryParse(line, out long n))
                    {
                        nums.Add(n);
                    }
                }
            }

            Console.WriteLine($"{nums.Count} nums loaded.");

            if (nums.Count > 25)
            {
                long part2Target = 0;
                int hint = 0;
                for (int i = 25; i < nums.Count; i++)
                {
                    long part1Target = nums[i];
                    long a;
                    bool found = false;
                    for (int j = i-1; j >= 0; j--)
                    {
                        a = nums[j];
                        if (nums.Any(n => part1Target == a + n))
                        {
                            found = true;
                            Debug.WriteLine($"Found {part1Target} with {j}th element plus another.");
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"{part1Target} does not have an set in the last 25.");
                        part2Target = part1Target;
                        hint = i;
                        break;
                    }
                }

                long p2Answer = 0;
                if (part2Target > 0)
                {
                    for (int i = hint; i >= 0; i--)
                    {
                        List<long> elements = new List<long>();
                        for(int j = i - 1; j >= 0; j--)
                        {
                            elements.Add(nums[j]);
                            long sum = elements.Sum();
                            if (sum == part2Target)
                            {
                                // highest and lowest values added together
                                elements.Sort();
                                p2Answer = elements.First() + elements.Last();
                                break;
                            }
                            else if (sum > part2Target)
                            {
                                // not the collection.
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine($"Part 2: {p2Answer}");
            }
        }
    }
}
