namespace GlobalUtils.Spatial
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public static class CharGridUtils
    {
        public static void PrintCharGrid(List<List<char>> grid)
        {
            for (int y = grid.Count - 1; y >= 0; y--)
            {
                Console.WriteLine(string.Join("", grid[y]));
            }
        }

        public static List<List<char>> CopyCharGrid(List<List<char>> source)
        {
            List<List<char>> copy = new List<List<char>>();
            for(int y = 0; y < source.Count; y++)
            {
                List<char> row = new List<char>();
                row.AddRange(source[y]);
                copy.Add(row);
            }

            return copy;
        }

        public static int Count(List<List<char>> source, char target)
        {
            int occurrences = 0;
            for (int y = source.Count - 1; y >= 0; y--)
            {
                occurrences += source[y].Count(c => c == target);
            }

            return occurrences;
        }
    }
}
