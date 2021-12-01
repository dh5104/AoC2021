namespace AoC2021.D16
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D16Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D16");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Rule> rules = new List<Rule>();
            List<int> myTicketNums = new List<int>();
            List<List<int>> tickets = new List<List<int>>();
            for (int i = 0; i < inputLines.Length; i++)
            {
                string line = inputLines[i];
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] instrParts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string label = instrParts[0];
                    string details = instrParts.Length > 1 ? instrParts[1] : null;
                    switch(label)
                    {
                        case "your ticket":
                            for (int j = 1; j < (inputLines.Length-i); j++)
                            {
                                string nextLine = inputLines[i + j];
                                if (!string.IsNullOrWhiteSpace(nextLine))
                                {
                                    myTicketNums.AddRange(nextLine.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)));
                                }
                                else
                                {
                                    break;
                                }
                            }

                            i += 1;
                            break;
                        case "nearby tickets":
                            for (int j = 1; j < (inputLines.Length - i); j++)
                            {
                                string nextLine = inputLines[i + j];
                                if (!string.IsNullOrWhiteSpace(nextLine))
                                {
                                    tickets.Add(nextLine.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList());
                                }
                                else
                                {
                                    break;
                                }
                            }

                            i += tickets.Count;
                            break;
                        default:
                            rules.Add(new Rule(label, details));
                            break;
                    }
                }
            }

            Debug.WriteLine($"{tickets.Count} tickets to process, with {rules.Count} rules.");
            List<List<int>> validatedTickets = new List<List<int>>();
            int ticketScanningErrorRate = 0;
            foreach(List<int> ticket in tickets)
            {
                bool wholeTicketPass = true;
                foreach(int num in ticket)
                {
                    bool passed = false;
                    foreach(Rule r in rules)
                    {
                        if (!passed)
                        {
                            passed = r.IsValid(num);
                        }
                        else
                        {
                            break;
                        }
                    }

                    wholeTicketPass &= passed;
                    if (!passed)
                    {
                        ticketScanningErrorRate += num;
                    }
                }

                if (wholeTicketPass)
                {
                    validatedTickets.Add(ticket);
                }
            }

            Console.WriteLine($"Part 1: error rate = {ticketScanningErrorRate}");
            
            foreach(List<int> ticket in validatedTickets)
            {
                Debug.WriteLine(string.Join(',', ticket));
            }

            List<List<Rule>> categories = new List<List<Rule>>();
            for (int i = 0; i < validatedTickets[0].Count; i++)
            {
                List<Rule> positionSet = new List<Rule>(rules);
                categories.Add(positionSet);
            }

            for (int i = 0; i < validatedTickets.Count; i++)
            {
                for (int j = 0; j < validatedTickets[i].Count; j++)
                {
                    int numToCheck = validatedTickets[i][j];
                    if (categories[j].Count > 1)
                    {
                        List<Rule> validatedRules = new List<Rule>();
                        for (int k = 0; k < categories[j].Count; k++)
                        {
                            Rule ruleToCheck = categories[j][k];
                            if (ruleToCheck.IsValid(numToCheck))
                            {
                                validatedRules.Add(ruleToCheck);
                            }
                            else
                            {
                                Console.WriteLine($"Inv \"{ruleToCheck.Class}\" for ticket {i} position {j}: {numToCheck}.");
                            }
                        }

                        if (validatedRules.Count != categories[j].Count)
                        {
                            categories[j].Clear();
                            categories[j].AddRange(validatedRules);
                        }
                    }
                }
            }

            if (Debug.EnableDebugOutput)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"Category {i}:");
                    Console.WriteLine(string.Join(",", categories[i].Select(r => r.Class)));
                }
            }

            if (categories.Any(c => c.Count > 1))
            {
                List<Category> properCats = new List<Category>();
                Dictionary<int, string> finalized = new Dictionary<int, string>();

                for (int i = 0; i < categories.Count; i++)
                {
                    properCats.Add(new Category(i, categories[i]));
                }

                while(properCats.Count > 0)
                {
                    int smi = this.Smallest(properCats);
                    if (smi >= 0 && properCats[smi].Labels.Count == 1)
                    {
                        string finalizedLabel = properCats[smi].Labels[0];
                        finalized[properCats[smi].Position] = finalizedLabel;
                        properCats.RemoveAt(smi);
                        foreach(Category c in properCats)
                        {
                            if (c.Labels.Contains(finalizedLabel))
                            {
                                c.Labels.Remove(finalizedLabel);
                            }
                        }
                    }
                }

                long p2answer = 1;
                foreach(KeyValuePair<int, string> kvp in finalized)
                {
                    Console.WriteLine($"{kvp.Value} = {myTicketNums[kvp.Key]}");
                    if (kvp.Value.Contains("departure"))
                    {
                        p2answer *= myTicketNums[kvp.Key];
                    }
                }

                Console.WriteLine(p2answer);
            }
        }

        private int Smallest(List<Category> cats)
        {
            int smallestcount = int.MaxValue;
            int smIndex = -1;
            for (int i = 0; i < cats.Count; i++)
            {
                if (cats[i].Labels.Count < smallestcount)
                {
                    smallestcount = cats[i].Labels.Count;
                    smIndex = i;
                }
            }

            return smIndex;
        }

        public class Category
        {
            public int Position { get; set; }

            public List<string> Labels { get; set; }

            public Category(int i, List<Rule> rules)
            {
                this.Labels = rules.Select(r => r.Class).ToList();
                this.Position = i;
            }
        }

        public class Rule
        {
            public string Class { get; set; }

            public List<Tuple<int,int>> ValidRanges { get; set; }

            public Rule(string ruleName, string rawRuleDetails)
            {
                this.Class = ruleName;
                this.ValidRanges = new List<Tuple<int, int>>();

                if (string.IsNullOrWhiteSpace(rawRuleDetails))
                {
                    throw new ArgumentNullException(nameof(rawRuleDetails));
                }
                else
                { 
                    // arrival platform: 46-428 or 449-949
                    string[] rawRangeParts = rawRuleDetails.Split(new char[] { ' ', '-', 'o', 'r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < rawRangeParts.Length; i += 2)
                    {
                        int low = int.Parse(rawRangeParts[i]);
                        int high = int.Parse(rawRangeParts[i + 1]);
                        if (high < low)
                        {
                            throw new ArgumentException($"input {low} is not lower than {high} for: \"{rawRuleDetails}\"");
                        }

                        this.ValidRanges.Add(new Tuple<int, int>(low, high));
                    }
                }
            }

            public bool IsValid(int num)
            {
                bool passed = false;

                foreach (Tuple<int, int> valRange in this.ValidRanges)
                {
                    if (num >= valRange.Item1 && num <= valRange.Item2)
                    {
                        passed = true;
                        break;
                    }
                }

                return passed;
            }
        }
    }
}
