using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalUtils
{
    public static class ParseUtils
    {
        public static List<int> InputAsInts(string[] inputLines, bool sort = false)
        {
            List<int> numbers = new List<int>();
            foreach (string line in inputLines)
            {
                if (int.TryParse(line, out int parsedInt))
                {
                    numbers.Add(parsedInt);
                }
            }

            if (sort)
            {
                numbers.Sort();
            }

            return numbers;
        }

        public static List<long> InputAsLongs(string[] inputLines, bool sort = false)
        {
            List<long> numbers = new List<long>();
            foreach (string line in inputLines)
            {
                if (long.TryParse(line, out long parsedInt))
                {
                    numbers.Add(parsedInt);
                }
            }

            if (sort)
            {
                numbers.Sort();
            }

            return numbers;
        }
    }
}
