namespace AoC2021.D05
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D05Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D05");
        }

        protected override void Execute(string[] inputLines)
        {
            List<int> allSeats = new List<int>();
            int highestId = -1;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    int lower = 0;
                    int higher = 127;
                    for (int i = 0; i < 8; i++)
                    {
                        int diff = (int)Math.Pow(2, 6 - i);
                        switch (line[i])
                        {
                            case 'F':
                                // lower half
                                higher = higher - diff;
                                break;
                            case 'B':
                                // upper half
                                lower = lower + diff;
                                break;

                        }
                    }

                    
                    int lowseat = 0;
                    int highseat = 7;
                    for (int j = 0; j < 3; j++)
                    {
                        int diff = (int)Math.Pow(2, 2 - j);
                        switch(line[j + 7])
                        {
                            case 'L':
                                // lower half
                                highseat = highseat - diff;
                                break;
                            case 'R':
                                // upper half
                                lowseat = lowseat + diff;
                                break;
                        }
                    }

                    int seatID = lower * 8 + lowseat;
                    Debug.WriteLine($"row {lower}, seat {lowseat}, seat ID {seatID}");

                    if (seatID > highestId)
                    {
                        highestId = seatID;
                    }

                    allSeats.Add(seatID);
                }
            }

            Console.WriteLine($"Part 1: {highestId}");

            // lowest to highest
            allSeats.Sort();
            foreach(int seat in allSeats)
            {
                Debug.WriteLine(seat);
            }

            for(int i = 0; i < allSeats.Count - 1; i++)
            {
                int diffLow = allSeats[i] - allSeats[i + 1];
                if (diffLow != -1)
                {
                    Console.WriteLine($"Part 2: {allSeats[i + 1] - 1}");
                    break;
                }
            }

        }
    }
}
