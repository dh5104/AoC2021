namespace AoC2021.D18
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class Template : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D18");
        }

        protected override void Execute(string[] inputLines)
        {
            ////long part1 = 0;
            ////foreach (string line in inputLines)
            ////{
            ////    if (!string.IsNullOrWhiteSpace(line))
            ////    {
            ////        string equation = line;
            ////        Regex parens = new Regex(@"(\([\d\+\ \*]*\))");
            ////        Match parenCheck = parens.Match(equation);
            ////        while (parenCheck.Success)
            ////        {
            ////            long paramResults = FlatMath(parenCheck.Value);
            ////            equation = equation.Replace(parenCheck.Value, paramResults.ToString());
            ////            parenCheck = parens.Match(equation);
            ////        }

            ////        part1 += FlatMath(equation);
            ////    }
            ////}

            ////Console.WriteLine($"Part 1: {part1}");

            long part2 = 0;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string equation = line;
                    Regex parens = new Regex(@"(\([\d\+\ \*]*\))");
                    Match parenCheck = parens.Match(equation);
                    while (parenCheck.Success)
                    {
                        long paramResults = InverseMath(parenCheck.Value);
                        equation = equation.Replace(parenCheck.Value, paramResults.ToString());
                        parenCheck = parens.Match(equation);
                    }

                    long lineResult = InverseMath(equation);
                    Debug.WriteLine($"{lineResult} from {line}");
                    part2 += lineResult;
                }
            }

            Console.WriteLine($"Part 2: {part2}");
        }

        private long InverseMath(string equation)
        {
            Regex addition = new Regex(@"(\d+\ \+\ \d+)");
            Match additionCheck = addition.Match(equation);
            while (additionCheck.Success)
            {
                equation = equation.Replace(additionCheck.Value, Add(additionCheck.Value).ToString());
                additionCheck = addition.Match(equation);
            }

            return FlatMath2(equation);
        }

        private long Add(string line)
        {
            long result = 0;
            string[] parts = line.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3 && parts[1] == "+")
            {
                result = long.Parse(parts[0]) + long.Parse(parts[2]);
            }
            else
            {
                throw new ApplicationException($"Unknown state for {line}.");
            }

            return result;
        }

        private long FlatMath2(string line)
        {
            long result = 0;
            if (line.Length > 0)
            {
                string[] parts = line.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 3)
                {
                    result = long.Parse(parts[0]);
                    for (int i = 1; i < parts.Length; i += 2)
                    {
                        long rightOperand = long.Parse(parts[i + 1]);
                        switch (parts[i])
                        {
                            case "*":
                                result *= rightOperand;
                                break;
                            default:
                                throw new ArgumentException($"Unexpected operator {parts[i]} from \"{line}\"");
                        }
                    }
                }
                else if (parts.Length == 1 && long.TryParse(parts[0], out long alreadyAnswered))
                {
                    result = alreadyAnswered;
                }
                else
                {
                    throw new ApplicationException($"Unknown state for {line}.");
                }
            }

            return result;
        }

        private long FlatMath(string line)
        {
            long result = 0;
            if (line.Length > 0)
            {
                string[] parts = line.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 3)
                {
                    result = long.Parse(parts[0]);
                    for (int i = 1; i < parts.Length; i += 2)
                    {
                        long rightOperand = long.Parse(parts[i + 1]);
                        switch (parts[i])
                        {
                            case "*":
                                result *= rightOperand;
                                break;
                            case "+":
                                result += rightOperand;
                                break;
                            default:
                                throw new ArgumentException($"Unexpected operator {parts[i]} from \"{line}\"");
                        }
                    }
                }
                else if (parts.Length == 1 && long.TryParse(parts[0], out long alreadyAnswered))
                {
                    result = alreadyAnswered;
                }
            }

            return result;
        }
    }
}
