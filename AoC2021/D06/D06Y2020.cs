namespace AoC2021.D06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D06Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D06");
        }

        protected override void Execute(string[] inputLines)
        {
            Dictionary<int, List<char>> groups = new Dictionary<int, List<char>>();
            int i = 0;
            groups[i] = new List<char>();
            foreach (string line in inputLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // new line
                    i++;
                    groups[i] = new List<char>();
                }
                else
                {
                    // add to current question
                    foreach(char c in line)
                    {
                        if (!groups[i].Contains(c))
                        {
                            groups[i].Add(c);
                        }
                    }
                }
            }

            Console.WriteLine($"{groups.Count} groups, {groups.Sum(kvp => kvp.Value.Count)}");

            List<Group> groupList = new List<Group>();
            groupList.Add(new Group(0));
            i = 0;
            foreach(string line in inputLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    i++;
                    groupList.Add(new Group(groups.Count));
                }
                else
                {
                    groupList[i].PeopleCount++;
                    List<char> uniqueAnswers = new List<char>();
                    foreach (char c in line)
                    {
                        if (!uniqueAnswers.Contains(c))
                        {
                            uniqueAnswers.Add(c);

                            Question existing = groupList[i].Questions.FirstOrDefault(q => q.Id == c);
                            if (null == existing)
                            {
                                groupList[i].Questions.Add(new Question(c));
                            }
                            else
                            {
                                existing.YesCount++;
                            }
                        }
                    }
                }
            }

            int totalEveryone = 0;
            foreach (Group g in groupList)
            {
                totalEveryone += g.Questions.Count(q => q.YesCount == g.PeopleCount);
            }

            Console.WriteLine($"Part 2: {totalEveryone}");
        }

        public class Group
        {
            public int Id { get; set; }

            public List<Question> Questions { get; set; }

            public int PeopleCount { get; set; }

            public Group(int id)
            {
                this.Id = id;
                this.Questions = new List<Question>();
            }
        }

        public class Question
        {
            public char Id { get; set; }

            public int YesCount { get; set; }

            public Question(char id)
            {
                this.Id = id;
                this.YesCount = 1;
            }
        }
    }
}
