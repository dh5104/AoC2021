namespace AoC2021.D04
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Day 4: Giant Squid")]
    class D04Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D04");
        }

        protected override void Execute(string[] inputLines)
        {
            string[] callNumbersRaw = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<Board> boards = new List<Board>();
            Board currentBoard = null;
            for (int i = 1; i < inputLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(inputLines[i]))
                {
                    if (null != currentBoard)
                    {
                        boards.Add(currentBoard);

                    }
                    currentBoard = new Board();
                }
                else
                {
                    currentBoard.AddRow(inputLines[i]);
                }
            }

            if (null != currentBoard)
            {
                boards.Add(currentBoard);
            }

            
            int score = 0;
            foreach (string callNumRaw in callNumbersRaw)
            {
                int callNum = int.Parse(callNumRaw);
                if (boards.Any(b => b.Score == 0))
                {
                    foreach (Board b in boards.Where(b => b.Score == 0))
                    {
                        b.Call(callNum);
                        if (b.IsWinner())
                        {
                            if (score == 0)
                            {
                                score = b.CalculateScore(); 
                                Console.WriteLine("Part 1");
                                Console.WriteLine($"Winner found on {callNum}: {score}");
                                Console.WriteLine($"Ans: {callNum * score}");
                            }
                            else
                            {
                                score = b.CalculateScore();
                                Console.WriteLine($"Winner found on {callNum}: {score}");
                                Console.WriteLine($"Ans: {callNum * score}");
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public class Board
        {
            public Board()
            {
                this.Numbers = new List<List<int>>();
            }

            public List<List<int>> Numbers { get; set; }

            public int Score { get; set; }

            public void AddRow(string row)
            {
                string[] nums = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> newNumRow = new List<int>();
                foreach (string rawNumber in nums)
                {
                    newNumRow.Add(int.Parse(rawNumber));
                }

                this.Numbers.Add(newNumRow);
            }

            public void Call(int c)
            {
                for (int i = 0; i < this.Numbers.Count; i++)
                {
                    for (int j = 0; j < this.Numbers[i].Count; j++)
                    {
                        if (this.Numbers[i][j] == c)
                        {
                            this.Numbers[i][j] = -1;
                        }
                    }
                }
            }

            public bool IsWinner()
            {
                bool isWinner = false;
                if (this.Numbers.Any(nr => nr.All(n => n == -1)))
                {
                    isWinner = true;
                }
                else
                {
                    // check columns
                    for (int i = 0; i < this.Numbers[0].Count; i++)
                    {
                        if (this.Numbers.All(nr => nr[i] == -1))
                        {
                            isWinner = true;
                            break;
                        }
                    }
                }

                return isWinner;
            }

            public int CalculateScore()
            {
                this.Score = this.Numbers.Sum(nr => nr.Where(n => n > 0).Sum());
                return this.Score;
            }
        }
    }
}
