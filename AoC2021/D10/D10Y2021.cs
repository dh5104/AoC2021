namespace AoC2021.D10
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 10: Syntax Scoring")]
    class D10Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D10");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Tuple<char, char>> Pairs = new List<Tuple<char, char>>()
            {
                new Tuple<char, char>('[',']'),
                new Tuple<char, char>('<','>'),
                new Tuple<char, char>('{','}'),
                new Tuple<char, char>('(',')'),
            };

            char[] closingChars = new char[] { ']', '}', '>', ')' };
            List<string> pairs = new List<string>();
            pairs.Add("[]");
            pairs.Add("<>");
            pairs.Add("{}");
            pairs.Add("()");
            List<int> adapters = new List<int>();
            long part1Score = 0;
            long part2Score = 0;
            List<long> part2LineScores = new List<long>();
            Console.WriteLine("Part 1");
            foreach (string line in inputLines)
            {
                string originalLine = line;
                string workingLine = line;
                bool found = true;

                // strip matching pairs out
                while (found)
                {
                    found = false;
                    foreach (var pair in pairs)
                    {
                        int indexAt = workingLine.IndexOf(pair);
                        if (0 <= indexAt)
                        {
                            found = true;
                            workingLine = workingLine.Remove(indexAt, 2);
                            break;
                        }
                    }
                }

                // after that the only closing characters are the ones mismatched.
                int firstBadClosing = workingLine.IndexOfAny(closingChars);
                if (0 < firstBadClosing)
                {
                    switch (workingLine[firstBadClosing])
                    {
                        case ')':
                            part1Score += 3;
                            break;
                        case ']':
                            part1Score += 57;
                            break;
                        case '}':
                            part1Score += 1197;
                            break;
                        case '>':
                            part1Score += 25137;
                            break;
                    }
                    ////char pairedFirstChar = workingLine[firstBadClosing - 1];
                    ////Tuple<char, char> a = Pairs.FirstOrDefault(p => p.Item1 == pairedFirstChar);
                    ////switch(a?.Item2)
                    ////{
                    ////    case ')':
                    ////        part1Score += 3;
                    ////        break;
                    ////    case ']':
                    ////        part1Score += 57;
                    ////        break;
                    ////    case '}':
                    ////        part1Score += 1197;
                    ////        break;
                    ////    case '>':
                    ////        part1Score += 25137;
                    ////        break;
                    ////}

                    Debug.WriteLine($"{originalLine} | {workingLine} : {part1Score}");
                }
                else
                {
                    // incomplete strings
                    string completionString = string.Empty;
                    long thisScore = 0;
                    for (int i = workingLine.Length - 1; i >= 0; i--)
                    {
                        Tuple<char, char> a = Pairs.FirstOrDefault(p => p.Item1 == workingLine[i]);
                        switch (a?.Item2)
                        {
                            case ')':
                                thisScore = thisScore * 5 + 1;
                                break;
                            case ']':
                                thisScore = thisScore * 5 + 2;
                                break;
                            case '}':
                                thisScore = thisScore * 5 + 3;
                                break;
                            case '>':
                                thisScore = thisScore * 5 + 4;
                                break;
                        }
                    }
                    part2LineScores.Add(thisScore);
                    Debug.WriteLine($"{originalLine} | {workingLine} === {thisScore}");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: {part1Score}");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");

            part2LineScores.Sort();
            part2Score = part2LineScores[part2LineScores.Count / 2];
            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: {part2Score}");
            Console.ResetColor();
        }
    }
}
