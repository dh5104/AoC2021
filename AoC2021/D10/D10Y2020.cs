namespace AoC2021.D10
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D10Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D10");
        }

        protected override void Execute(string[] inputLines)
        {
            List<int> adapters = new List<int>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line) && int.TryParse(line, out int lineVal))
                {
                    adapters.Add(lineVal);
                }
            }
            
            // normal ascending sort
            adapters.Sort();

            int currVoltage = 0;
            int singles = 0;
            int triples = 0;
            for (int i = 0; i < adapters.Count; i++)
            {
                if (adapters[i] - currVoltage == 1)
                {
                    singles++;
                }
                else if (adapters[i] - currVoltage == 3)
                {
                    triples++;
                }

                currVoltage = adapters[i];
            }

            // add the end which is always a +3
            triples++;

            Console.WriteLine($"{singles} single and {triples} triple");
            Console.WriteLine($"Part 1: {singles * triples}");

            List<int> p2 = new List<int>();
            p2.Add(0);
            p2.AddRange(adapters);
            List<List<int>> sequences = new List<List<int>>();

            for (int i = 0; i < p2.Count; i++)
            {
                int current = p2[i];
                List<int> sequence = new List<int>();
                sequence.Add(current);
                for (int j = 1; j < p2.Count - i; j++)
                {
                    int peek = p2[j + i];
                    if (peek - current == 1)
                    {
                        sequence.Add(peek);
                        current = peek;
                    }
                    else
                    {
                        break;
                    }
                }

                if (sequence.Count > 2)
                {
                    sequences.Add(sequence);
                    i += sequence.Count - 1;
                }
            }

            // in each of these sequences, say 8 9 10 11 12, the first and last
            //  will be required, so the choices are really between including
            //  9, 10, and 11 in the path.  This *could* have been included in the loop
            //  above, but I needed it clear, as it's not simple 2^n
            long branches = 1;
            foreach (List<int> sequence in sequences)
            {
                switch (sequence.Count)
                {
                    case 3:
                        // can only either include the middle number or not
                        branches *= 2;
                        break;
                    case 4:
                        // 2^2 choices
                        branches *= 4;
                        break;
                    case 5:
                        // 2^3 choices, but one overlap, so 7.
                        branches *= 7;
                        break;
                    default:
                        // don't expect this one, but it would
                        //  need to be calculated from 2^n minus overlaps
                        throw new ApplicationException($"Did not expect a sequence of {sequence.Count}, need to handle this case.");
                }
            }

            Console.WriteLine($"Part 2: {branches} possible paths");
        }
    }
}
