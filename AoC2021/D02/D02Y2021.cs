namespace AoC2021.D02
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D02Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D02");
        }

        protected override void Execute(string[] inputLines)
        {
            int validCount = 0;
            int validPart2Count = 0;
            foreach (string line in inputLines)
            {
                PwTest rule = new PwTest(line);
                if (rule.IsValid)
                {
                    validCount++;
                }
                
                if (rule.IsValidPart2)
                {
                    validPart2Count++;
                }
            }

            Console.WriteLine($"{validCount} rules valid for part 1");
            Console.WriteLine($"{validPart2Count} rules valid for part 2");
        }

        public class PwTest
        {
            public int MinCount { get; set; }
            public int MaxCount { get; set; }
            public char RequiredChar { get; set; }
            public string PasswordToValidate { get; set; }
            public bool IsValid { get; set; }
            public bool IsValidPart2 { get; set; }

            public PwTest(string inputLine)
            {
                // 2-4 c: cdgdkcd
                // [0]-[1] [2]: [3]
                string[] parts = inputLine.Split(new char[] { ' ', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);
                this.MinCount = int.Parse(parts[0]);
                this.MaxCount = int.Parse(parts[1]);
                this.RequiredChar = char.Parse(parts[2]);
                this.PasswordToValidate = parts[3];

                int numInstRQ = this.PasswordToValidate.Where(c => c == this.RequiredChar).Count();
                if (numInstRQ >= this.MinCount && numInstRQ <= this.MaxCount)
                {
                    this.IsValid = true;
                }

                bool hasPrimary = this.PasswordToValidate[this.MinCount - 1] == RequiredChar;
                bool hasSecondary = this.PasswordToValidate[this.MaxCount - 1] == RequiredChar;
                
                // yeah XOR baby!
                if ((hasPrimary ^ hasSecondary))
                {
                    this.IsValidPart2 = true;
                }
            }
        }
    }
}
