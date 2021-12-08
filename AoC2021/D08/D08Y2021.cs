namespace AoC2021.D08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using Utils._2021.GameConsole;

    [Exercise("Day 8: Seven Segment Search")]
    class D08Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D08");
        }

        protected override void Execute(string[] inputLines)
        {
            Dictionary<List<DigitDisplay>, string[]> lines = new Dictionary<List<DigitDisplay>, string[]>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                    string input = parts[0];
                    string output = parts[1];
                    string[] inputParts = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string[] outputParts = output.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    List<DigitDisplay> lineInputs = new List<DigitDisplay>();
                    foreach(string inputPart in inputParts)
                    {
                        lineInputs.Add(new DigitDisplay(inputPart));
                    }

                    lines.Add(lineInputs, outputParts);
                }
            }

            int numunique = lines.Values.Sum(os => os.Count(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7));
            Console.WriteLine("Part 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ans: {numunique}");
            Console.ResetColor();
            Console.WriteLine($"_______________________________________");
            List<long> answers = new List<long>();
            foreach(var line in lines)
            {
                DigitDisplay this7 = line.Key.FirstOrDefault(t => t.UniqueCount == 7);
                DigitDisplay this4 = line.Key.FirstOrDefault(t => t.UniqueCount == 4);
                DigitDisplay this3 = line.Key.FirstOrDefault(t => t.UniqueCount == 3);
                DigitDisplay this2 = line.Key.FirstOrDefault(t => t.UniqueCount == 2);
                DigitDisplay actual6 = null;
                var the6s = line.Key.Where(t => t.UniqueCount == 6);
                foreach(var this6 in the6s)
                {
                    if (this4.Segments.All(s => this6.Segments.Contains(s)))
                    {
                        this6.NumericalValue = 9;
                    }
                    else if (this2.Segments.All(s => this6.Segments.Contains(s)))
                    {
                        this6.NumericalValue = 0;
                    }
                    else
                    {
                        this6.NumericalValue = 6;
                        actual6 = this6;
                    }
                }

                var the5s = line.Key.Where(t => t.UniqueCount == 5);
                foreach(var this5 in the5s)
                {
                    if (this3.Segments.All(s => this5.Segments.Contains(s)))
                    {
                        this5.NumericalValue = 3;
                    }
                    // all of 5 is in 6
                    else if (this5.Segments.All(s => actual6.Segments.Contains(s)))
                    {
                        this5.NumericalValue = 5;
                    }
                    else
                    {
                        this5.NumericalValue = 2;
                    }
                }

                string rawOutNum = string.Empty;
                foreach(string output in line.Value)
                {
                    if (output.Length == 2)
                    {
                        rawOutNum += this2.NumericalValue.ToString();
                    }
                    else if (output.Length == 3)
                    {
                        rawOutNum += this3.NumericalValue.ToString();
                    }
                    else if (output.Length == 4)
                    {
                        rawOutNum += this4.NumericalValue.ToString();
                    }
                    else if (output.Length == 7)
                    {
                        rawOutNum += this7.NumericalValue.ToString();
                    }
                    else
                    {
                        rawOutNum += line.Key.FirstOrDefault(k => k.UniqueCount == output.Length && k.Segments.All(s => output.Contains(s))).NumericalValue.ToString();
                    }
                }
                answers.Add(long.Parse(rawOutNum));
                Console.WriteLine(rawOutNum);
            }
            Console.WriteLine("Part 2");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ans: {answers.Sum()} ");
            Console.ResetColor();
        }

        Dictionary<int, DigitDisplay> master = new Dictionary<int, DigitDisplay>()
        {
            { 0, new DigitDisplay("abcefg") }, // 6
            { 1, new DigitDisplay("cf") }, // 2
            { 2, new DigitDisplay("acdeg") }, // 5
            { 3, new DigitDisplay("acdfg") }, // 5
            { 4, new DigitDisplay("bcdf") }, // 4
            { 5, new DigitDisplay("abdfg") }, // 5
            { 6, new DigitDisplay("abdefg") }, // 6
            { 7, new DigitDisplay("acf") }, // 3
            { 8, new DigitDisplay("abcdefg") }, // 7
            { 9, new DigitDisplay("abcdfg") }, // 6
        };

        public class DigitDisplay
        {
            public int NumericalValue { get; set; }
            public int UniqueCount { get; set; }
            public List<char> Segments { get; set; }

            public DigitDisplay(string raw)
            {
                this.Segments = raw.ToCharArray().ToList();
                this.UniqueCount = this.Segments.Count;
                if (this.UniqueCount == 2)
                {
                    this.NumericalValue = 1;
                }
                else if (this.UniqueCount == 3)
                {
                    this.NumericalValue = 7;
                }
                else if (this.UniqueCount == 4)
                {
                    this.NumericalValue = 4;
                }
                else if (this.UniqueCount == 7)
                {
                    this.NumericalValue = 8;
                }
            }
        }
    }
}
