namespace AoC2021.D15
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D15Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D15");
        }

        protected override void Execute(string[] inputLines)
        {
            string singleLineInput = inputLines[0];
            List<int> startingNums = singleLineInput.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();

            List<int> spoken = new List<int>();
            
            int lastSpoken = 0;
            for (int i = 0; i < 2020; i++)
            {
                if (i < startingNums.Count)
                {
                    Debug.WriteLine($"{i}: {startingNums[i]}");
                    lastSpoken = startingNums[i];
                }
                else
                {
                    int? last1 = null;
                    int? last2 = null;
                    for (int j = spoken.Count-1; j >= 0; j--)
                    {
                        if (spoken[j] == lastSpoken)
                        {
                            if (null == last1)
                            {
                                last1 = j;
                            }
                            else if (null == last2)
                            {
                                last2 = j;
                                break;
                            }
                        }
                    }

                    int toSpeak = -1;
                    if (null == last1 || null == last2)
                    {
                        toSpeak = 0;
                    }
                    else
                    {
                        toSpeak = last1.Value - last2.Value;
                    }

                    Debug.WriteLine($"{i}: {toSpeak}");
                    lastSpoken = toSpeak;
                }

                spoken.Add(lastSpoken);
            }
            Console.WriteLine($"Part 1: {lastSpoken}");

            Dictionary<int, IndexTracker> counts = new Dictionary<int, IndexTracker> ();
            for (int i = 0; i < 30000000; i++)
            {
                int toSpeak = -1;
                
                if (i < startingNums.Count)
                {
                    toSpeak = startingNums[i];
                }
                else
                {
                    toSpeak = counts.ContainsKey(lastSpoken) ? counts[lastSpoken].GetSpeak() : 0;
                }
                
                if (Debug.EnableDebugOutput && i % 100000 == 0)
                {
                    Debug.WriteLine($"{i}: {toSpeak}");
                }

                if (!counts.ContainsKey(toSpeak))
                {
                    counts[toSpeak] = new IndexTracker(i);
                }
                else
                {
                    counts[toSpeak].SetLast1(i);
                }

                lastSpoken = toSpeak;
            }

            Console.WriteLine($"Part 2: {lastSpoken}");
        }

        private class IndexTracker
        {
            public int Last1 { get; set; }
            public int Last2 { get; set; }

            public IndexTracker(int firstOccurrenceIndex)
            {
                this.Last2 = -1;
                this.Last1 = firstOccurrenceIndex;
            }

            public void SetLast1(int index)
            {
                this.Last2 = (int)this.Last1;
                this.Last1 = index;
            }

            public int GetSpeak()
            {
                if (0 > this.Last2)
                {
                    return 0;
                }
                else
                {
                    return (this.Last1 - this.Last2);
                }
            }
        }
    }
}
