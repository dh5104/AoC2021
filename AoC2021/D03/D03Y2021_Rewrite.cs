namespace AoC2021.D03
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.NumBaseUtils;
    using GlobalUtils.Spatial;

    [Exercise("Day 3: Binary Diagnostic (R)")]
    class D03Y2021_Rewrite : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D03");
        }

        protected override void Execute(string[] inputLines)
        {

            int oneCount = 0;
            string finalBin = "";
            string finalBinInverse = "";
            for (int i = 0; i < inputLines[0].Length; i++)
            {
                oneCount = 0;
                foreach (string line in inputLines)
                {
                    if (line[i] == '1')
                    {
                        oneCount++;
                    }
                }

                if (oneCount > inputLines.Length / 2)
                {
                    finalBin += "1";
                    finalBinInverse += "0";
                }
                else
                {
                    finalBin += "0";
                    finalBinInverse += "1";
                }
            }

            Binary gamma = new Binary(finalBin);
            Binary epsilon = gamma.Inverse();

            Console.WriteLine("Part 1");
            Console.WriteLine($"G{gamma}, e{epsilon}");
            Console.WriteLine($"Ans: {gamma.ToDecimal() * epsilon.ToDecimal()}");

            List<string> inputCopy = new List<string>(inputLines);
            for (int i = 0; i < inputCopy[0].Length && 1 < inputCopy.Count; i++)
            {
                var num1 = inputCopy.Count(l => l[i] == '1');
                if (num1 >= inputCopy.Count / 2.0)
                {
                    inputCopy = inputCopy.Where(l => l[i] == '1').ToList();
                }
                else
                {
                    inputCopy = inputCopy.Where(l => l[i] == '0').ToList();
                }
            }

            string oxygenBin = inputCopy[0];

            Console.WriteLine("Part 2");
            Console.WriteLine($"Ox: {oxygenBin} ({BinToDecimal(oxygenBin)})");

            inputCopy = new List<string>(inputLines);
            for (int i = 0; i < inputCopy[0].Length && 1 < inputCopy.Count; i++)
            {
                var num1 = inputCopy.Count(l => l[i] == '1');
                if (num1 < inputCopy.Count / 2.0)
                {
                    inputCopy = inputCopy.Where(l => l[i] == '1').ToList();
                }
                else
                {
                    inputCopy = inputCopy.Where(l => l[i] == '0').ToList();
                }
            }

            string co2Bin = inputCopy[0];
            Console.WriteLine($"CO2: {co2Bin} ({BinToDecimal(co2Bin)})");

            Console.WriteLine($"Ans: {BinToDecimal(co2Bin) * BinToDecimal(oxygenBin)}");
        }

        private long BinToDecimal(string binValue)
        {
            int gamma = 0;
            for (int i = 0; i < binValue.Length; i++)
            {
                if ('1' == binValue[binValue.Length - i - 1])
                {
                    gamma += (int)Math.Pow(2, i);
                }
            }

            return gamma;
        }
    }
}
