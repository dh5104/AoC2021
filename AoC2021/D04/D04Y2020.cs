namespace AoC2021.D04
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("")]
    class D04Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D04");
        }

        public HashSet<string> codes = new HashSet<string>()
        {
            "byr:",
            "iyr:",
            "eyr:",
            "hgt:",
            "hcl:",
            "ecl:",
            "pid:",
            //"cid:",
        };

        protected override void Execute(string[] inputLines)
        {
            List<string> passports = new List<string>();
            string currData = string.Empty;
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    currData = $"{currData} {line}";
                }
                else
                {
                    passports.Add(currData);
                    currData = string.Empty;
                }
            }
            
            if (!string.IsNullOrWhiteSpace(currData))
            {
                passports.Add(currData);
            }

            int part1ValidCount = 0;
            foreach(string passport in passports)
            {
                int score = 0;
                var dataChunks = passport.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i=0; i < dataChunks.Length; i++)
                {
                    dataChunks[i] = dataChunks[i].Trim().Substring(0, 4);
                }

                foreach(string code in codes)
                {
                    if (dataChunks.Any(d => string.Equals(d, code, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        score++;
                    }
                }

                if (score >= codes.Count)
                {
                    part1ValidCount++;
                }

                Debug.WriteLine($"{passport} - {score}, T: {part1ValidCount}");
            }

            Console.WriteLine($"Part 1: {part1ValidCount} valid passports");

            int part2ValidCount = 0;
            foreach (string passport in passports)
            {
                var dataChunks = passport.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                bool valid = true;

                bool hasbyr = false;
                bool hasiyr = false;
                bool haseyr = false;
                bool hashgt = false;
                bool hashcl = false;
                bool hasecl = false;
                bool haspid = false;

                foreach(string chunk in dataChunks)
                {
                    string fieldId = chunk.Trim().Substring(0, 3);
                    string value = chunk.Trim().Substring(4);

                    switch(fieldId)
                    {
                        case "byr":
                            int birthYear = int.Parse(value);
                            valid &= birthYear >= 1920 && birthYear <= 2002;
                            if (valid)
                            {
                                hasbyr = true;
                            }
                            break;
                        case "iyr":
                            int issueYear = int.Parse(value);
                            valid &= issueYear >= 2010 && issueYear <= 2020;
                            if (valid)
                            {
                                hasiyr = true;
                            }
                            break;
                        case "eyr":
                            int expYear = int.Parse(value);
                            valid &= expYear >= 2020 && expYear <= 2030;
                            if (valid)
                            {
                                haseyr = true;
                            }
                            break;
                        case "hgt":
                            if (value.Contains("cm") || value.Contains("in"))
                            {
                                string end = value.Substring(value.Length - 2, 2);
                                int height = int.Parse(value.Substring(0, value.Length - end.Length));
                                if (end == "cm")
                                {
                                    valid &= height >= 150 && height <= 193;
                                }
                                else
                                {
                                    valid &= height >= 59 && height <= 76;
                                }
                            }
                            else
                            {
                                valid = false;
                            }

                            if (valid)
                            {
                                hashgt = true;
                            }
                            break;
                        case "hcl":
                            if (value[0] == '#' && value.Length == 7)
                            {
                                char[] alphanums = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
                                for(int i = 1; i < value.Length; i++)
                                {
                                    valid &= alphanums.Contains(value[i]);
                                }
                            }
                            else
                            {
                                valid = false;
                            }

                            if (valid)
                            {
                                hashcl = true;
                            }
                            break;
                        case "ecl":
                            valid &= value == "amb"
                                        || value == "blu"
                                        || value == "brn"
                                        || value == "gry"
                                        || value == "grn"
                                        || value == "hzl"
                                        || value == "oth";

                            if (valid)
                            {
                                hasecl = true;
                            }
                            break;
                        case "pid":
                            valid &= value.Length == 9 && int.TryParse(value, out int junk);

                            if (valid)
                            {
                                haspid = true;
                            }
                            break;
                    }

                    if (!valid)
                    {
                        continue;
                    }
                }

                if (valid && hasbyr && hasiyr && haseyr && hashgt && hashcl && hasecl && haspid)
                {
                    part2ValidCount++;
                }

                Debug.WriteLine($"{passport}, T: {part2ValidCount}");
            }

            Console.WriteLine($"Part 2: {part2ValidCount} valid passports");
        }
    }
}
