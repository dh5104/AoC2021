namespace AoC2021.D14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using static GlobalUtils.Spatial.GridUtils;

    [Exercise("Day 14: Extended Polymerization")]
    class D14Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D14");
        }

        Dictionary<string, string> rules = new Dictionary<string, string>();
        protected override void Execute(string[] inputLines)
        {
            string startingPolymer = inputLines[0];
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("->"))
                {
                    string[] parts = line.Split(new char[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    rules[parts[0]] = parts[1];
                }
            }

            // Brute Force method, just make the actual string.
            string polymer = startingPolymer;
            for (int i = 0; i < 10; i++)
            {
                string newVersion = string.Empty;
                for (int j = 0; j < polymer.Length; j++)
                {
                    char current = polymer[j];
                    newVersion += current;

                    if (j < polymer.Length - 1)
                    {
                        char next = polymer[j + 1];
                        string ruleKey = $"{current}{next}";

                        if (rules.ContainsKey(ruleKey))
                        {
                            newVersion += rules[ruleKey];
                        }
                    }
                }

                polymer = newVersion;
            }

            var part1Result = polymer
                            .ToUpper()
                            .Where(char.IsLetter)
                            .GroupBy(c => c)
                            .Select(g => new { Letter = g.Key, Count = g.Count() })
                            .OrderByDescending(g => g.Count);

            long p1Answer = part1Result.First().Count - part1Result.Last().Count;
            Console.WriteLine("Part 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: {p1Answer} ");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");

            polymer = startingPolymer;

            // Break the string into letter pairs, so we can match those pairs
            //  against the rules.  The last character is only in one pair, just
            //  like the first.
            Dictionary<string, long> pairCounts = new Dictionary<string, long>();

            // OK so this is an important one, and will eventually have the answer.  We'll use
            //  the pairCounts to track what we need to add, but we need to keep track of the individual
            //  character counts on their own, as the pairs have duplicate instances of each character.
            Dictionary<char, long> charCounts = new Dictionary<char, long>();
            for (int j = 0; j < polymer.Length; j++)
            {
                char current = polymer[j];
                if (j < polymer.Length - 1)
                {
                    char next = polymer[j + 1];
                    string ruleKey = $"{current}{next}";

                    if (!pairCounts.ContainsKey(ruleKey))
                    {
                        pairCounts[ruleKey] = 1;
                    }
                    else
                    {
                        pairCounts[ruleKey] += 1;
                    }
                }

                if (charCounts.ContainsKey(current))
                {
                    charCounts[current] += 1;
                }
                else
                {
                    charCounts[current] = 1;
                }
            }

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> newPairCounts = new Dictionary<string, long>();
                foreach (var rule in rules)
                {
                    if (pairCounts.ContainsKey(rule.Key))
                    {
                        // increment by what we have existing for the count, into both new pairs.
                        long incrementQty = pairCounts[rule.Key];

                        // increment char counts
                        if (!charCounts.ContainsKey(rule.Value[0]))
                        {
                            charCounts[rule.Value[0]] = incrementQty;
                        }
                        else
                        {
                            charCounts[rule.Value[0]] += incrementQty;
                        }

                        // increment both new pairs.
                        string firstNewPair = $"{rule.Key[0]}{rule.Value}";
                        if (newPairCounts.ContainsKey(firstNewPair))
                        {
                            newPairCounts[firstNewPair] += incrementQty;
                        }
                        else
                        {
                            newPairCounts[firstNewPair] = incrementQty;
                        }

                        string secondNewPair = $"{rule.Value}{rule.Key[1]}";
                        if (newPairCounts.ContainsKey(secondNewPair))
                        {
                            newPairCounts[secondNewPair] += incrementQty;
                        }
                        else
                        {
                            newPairCounts[secondNewPair] = incrementQty;
                        }
                    }
                }

                // reassign the new pairs, but don't modify the char count dictionary.
                pairCounts = newPairCounts;
            }

            long largestCount = charCounts.Values.Max();
            long smallestcount = charCounts.Values.Min();

            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: {largestCount - smallestcount}");
            Console.ResetColor();
        }
    }
}
