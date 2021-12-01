namespace AoC2021.D07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D07Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D07");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Bag> bagSpecs = new List<Bag>();
            foreach (string line in inputLines)
            {
                var first = line.Split("bags contain");
                Bag b = new Bag()
                {
                    Color = first[0].Trim()
                };

                if (first[1] != " no other bags.")
                {
                    var contentsRaw = first[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string content in contentsRaw)
                    {
                        string c = content.Trim();
                        // 3 bright white bags
                        int firstSpace = c.IndexOf(' ');
                        int qty = int.Parse(c.Substring(0, firstSpace));
                        string color = c.Substring(firstSpace + 1);
                        color = color.Substring(0, color.IndexOf(" bag"));
                        b.Contents.Add(new Tuple<string, int>(color, qty));
                    }
                }

                bagSpecs.Add(b);
            }

            Debug.WriteLine($"{bagSpecs.Count} unique bags.");

            List<Bag> finalBagList = new List<Bag>();
            List<Bag> goldHolders = bagSpecs.Where(bs => bs.Contents.Any(bsc => bsc.Item1 == "shiny gold")).ToList();
            Console.WriteLine($"{goldHolders.Count} base gold holding bags");
            finalBagList.AddRange(goldHolders);
            foreach(Bag b in goldHolders)
            {
                foreach(Bag toAdd in GetParents(b, bagSpecs))
                {
                    if (!finalBagList.Any(fbl => fbl.Color == toAdd.Color))
                    {
                        finalBagList.Add(toAdd);
                    }
                }
            }

            Console.WriteLine($"Part 1: {finalBagList.Count} final bags.");

            
            int subBags = GetSubBags("shiny gold", bagSpecs);
            Console.WriteLine($"Part 2: {subBags} bags within shiny gold.");
        }

        private int GetSubBags(string bagColor, List<Bag> allBags)
        {
            int subBags = 0;
            Bag b = allBags.FirstOrDefault(b => b.Color == bagColor);
            if (null != b)
            {
                if (b.Contents.Count > 0)
                {
                    foreach (var child in b.Contents)
                    {
                        subBags += child.Item2 + child.Item2 * GetSubBags(child.Item1, allBags);
                    }
                }
            }
            
            return subBags;
        }

        private List<Bag> GetParents(Bag b, List<Bag> allBags)
        {
            List<Bag> parents = allBags.Where(ab => ab.Contents.Any(bc => bc.Item1 == b.Color)).ToList();
            List<Bag> toAdd = new List<Bag>();
            foreach (Bag p in parents)
            {
                toAdd.AddRange(GetParents(p, allBags));
            }

            parents.AddRange(toAdd);

            return parents;
        }

        public class Bag
        {
            public string Color { get; set; }

            public List<Tuple<string, int>> Contents { get; set; }

            public Bag()
            {
                this.Contents = new List<Tuple<string, int>>();
            }
        }
    }
}
