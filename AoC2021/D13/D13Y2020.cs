namespace AoC2021.D13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Math;

    [Exercise("")]
    class D13Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D13");
        }

        protected override void Execute(string[] inputLines)
        {

            int timestamp = int.Parse(inputLines[0]);
            List<string> busNumbers = inputLines[1].Split(new char[] { ',', 'x' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int earliestPossible = int.MaxValue;
            int earliestBusId = 0;
            for (int i = 0; i < busNumbers.Count; i++)
            {
                int busTiming = int.Parse(busNumbers[i]);
                int divisible = timestamp / busTiming;
                int t = divisible * busTiming;
                if (t < earliestPossible)
                {
                    t += busTiming;
                }

                if (t < earliestPossible)
                {
                    earliestPossible = t;
                    earliestBusId = busTiming;
                }
            }
            
            Debug.WriteLine($"Earliest Possible = {earliestPossible} on bus {earliestBusId}");
            Console.WriteLine($"{(earliestPossible-timestamp)*earliestBusId}");

            List<string> fullBusNumbers = inputLines[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            
            // list of buses with real numbers
            List<long> actualBuses = new List<long>();

            // the timing remainders is basically the bus number minus the index, so if bus 58 arrives second (offset of 1min),
            //  we need to solve for a remainder of 57.
            List<long> timingRemainders = new List<long>();
            long largestTime = 0;
            long smallestTime = long.MaxValue;
            for (int i = 0; i < fullBusNumbers.Count; i++)
            {
                if (fullBusNumbers[i] != "x" && long.TryParse(fullBusNumbers[i], out long num))
                {
                    actualBuses.Add(num);

                    long offset = -i;
                    while (offset < 0)
                    {
                        offset += num;
                    }

                    timingRemainders.Add(offset);

                    if (largestTime < num)
                    {
                        largestTime = num;
                    }

                    if (smallestTime > num)
                    {
                        smallestTime = num;
                    }
                }
            }

            Debug.WriteLine($"{{{string.Join(",", actualBuses)}}} buses");
            Debug.WriteLine($"{{{string.Join(",", timingRemainders)}}} rems");
            long crt = ChineseRemainderTheorem.Solve(actualBuses.ToArray(), timingRemainders.ToArray());
            Console.WriteLine($"Part 2: CRT of {crt}");
        }
    }
}
