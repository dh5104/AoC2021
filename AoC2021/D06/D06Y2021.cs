namespace AoC2021.D06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 06: Lanternfish")]
    class D06Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D06");
        }

        protected override void Execute(string[] inputLines)
        {
            string[] parts = inputLines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<int> fishes = new List<int>();
            foreach(string part in parts)
            {
                if (int.TryParse(part, out int newFish))
                {
                    fishes.Add(newFish);
                }
            }

            for (int i = 0; i < 80; i++)
            {
                List<int> newCycleFish = new List<int>();
                List<int> babyFish = new List<int>();
                foreach(int fish in fishes)
                {
                    if (fish == 0)
                    {
                        babyFish.Add(8);
                        newCycleFish.Add(6);
                    }
                    else
                    {
                        newCycleFish.Add((fish-1));
                    }
                }

                newCycleFish.AddRange(babyFish);
                fishes = new List<int>(newCycleFish);
                newCycleFish.Clear();
                babyFish.Clear();

                Console.WriteLine($"After {i}: {fishes.Count}");
            }

            Console.WriteLine("Part 1");
            Console.WriteLine($"Ans: {fishes.Count}");

            fishes = new List<int>();
            Dictionary<int, long> counts = new Dictionary<int, long>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 },
            };
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int newFish))
                {
                    counts[newFish] += 1;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                long spawnCount = counts[0];
                counts[0] = counts[1];
                counts[1] = counts[2];
                counts[2] = counts[3];
                counts[3] = counts[4];
                counts[4] = counts[5];
                counts[5] = counts[6];
                counts[6] = counts[7] + spawnCount;
                counts[7] = counts[8];
                counts[8] = spawnCount;
            }

            Console.WriteLine("Part 2");
            foreach(var count in counts)
            {
                Console.WriteLine($"{count.Key}:{count.Value}");
            }

            Console.WriteLine($"Ans: {counts.Values.Sum()}");
        }
    }
}
