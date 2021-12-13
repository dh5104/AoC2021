
namespace GlobalUtils.Spatial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class GridUtils
    {
        public static List<List<T>> MakeGrid<T>(int size, T defaultValue)
        {
            List<List<T>> grid = new List<List<T>>();
            for (int y = 0; y < size; y++)
            {
                List<T> line = new List<T>();
                for (int x = 0; x < size; x++)
                {
                    line.Add(defaultValue);
                }

                grid.Add(line);
            }

            return grid;
        }

        public static List<List<GenericPoint>> MakeGrid(List<GenericPoint> flatPoints)
        {
            List<List<GenericPoint>> grid = new List<List<GenericPoint>>();
            int newMaxX = flatPoints.Max(p => p.X);
            int newMinX = flatPoints.Min(p => p.X);
            int newMaxY = flatPoints.Max(p => p.Y);
            int newMinY = flatPoints.Min(p => p.Y);
            for (int y = newMinY; y <= newMaxY; y++)
            {
                List<GenericPoint> line = new List<GenericPoint>();
                for (int x = newMinX; x <= newMaxX; x++)
                {
                    line.Add(null);
                }

                grid.Add(line);
            }

            foreach (GenericPoint point in flatPoints)
            {
                grid[point.Y][point.X] = point;
            }

            return grid;
        }

        public static void PrintGrid<T>(List<List<T>> points) where T : GenericPoint
        {
            // assumes a fully populated square grid, meaning all the lines have the same contents.
            for(int y = 0; y < points.Count; y++)
            {
                for (int x = 0; x < points[y].Count; x++)
                {
                    Console.Write(points[y][x].ToString());
                }
                Console.WriteLine();
            }
        }

        public static void PrintGrid<T>(Dictionary<string, T> points) where T : GenericPoint
        {
            int maxX = points.Values.Max(t => t.X);
            int minX = points.Values.Min(t => t.X);
            int maxY = points.Values.Max(t => t.Y);
            int minY = points.Values.Min(t => t.Y);

            for (int y = maxY; y >= minY; y--)
            {
                StringBuilder line = new StringBuilder();
                for (int x = minX; x <= maxX; x++)
                {
                    string pointToDraw = $"{x},{y},0";
                    if (points.ContainsKey(pointToDraw))
                    {
                        line.Append(points[pointToDraw].ToString());
                    }
                    else
                    {
                        line.Append('_');
                    }
                }

                Console.WriteLine(line.ToString());
            }

            Console.WriteLine($"{points.Count} points, <{maxX} => {minX}, {maxY} => {minY}>");
            Console.WriteLine();
        }

        public static void PrintGrid<T>(Dictionary<Tuple<int, int>, T> points, Tuple<int, int> pointOfInterest = null)
        {
            int maxX = points.Keys.Max(t => t.Item1);
            int minX = points.Keys.Min(t => t.Item1);
            int maxY = points.Keys.Max(t => t.Item2);
            int minY = points.Keys.Min(t => t.Item2);

            for (int y = maxY; y >= minY; y--)
            {
                StringBuilder line = new StringBuilder();
                for (int x = minX; x <= maxX; x++)
                {
                    if (null != pointOfInterest && pointOfInterest.Item1 == x && pointOfInterest.Item2 == y)
                    {
                        line.Append('X');
                    }
                    else
                    {
                        Tuple<int, int> pointToDraw = new Tuple<int, int>(x, y);
                        if (points.ContainsKey(pointToDraw))
                        {
                            line.Append(points[pointToDraw].ToString());
                        }
                        else
                        {
                            line.Append('.');
                        }
                    }
                }

                Console.WriteLine(line.ToString());
            }

            Console.WriteLine($"{points.Count} points, <{maxX} => {minX}, {maxY} => {minY}>");
            Console.WriteLine();
        }

        public static Tuple<int, int> MovePoint(int x, int y, Direction direction)
        {
            return MovePoint(new Tuple<int, int>(x, y), direction);
        }

        public static Tuple<int, int> MovePoint(Tuple<int, int> point, Direction direction)
        {
            int x = point.Item1;
            int y = point.Item2;
            switch (direction)
            {
                case Direction.N:
                    y++;
                    break;
                case Direction.S:
                    y--;
                    break;
                case Direction.E:
                    x++;
                    break;
                case Direction.W:
                    x--;
                    break;
                case Direction.NW:
                    y++;
                    x--;
                    break;
                case Direction.NE:
                    y++;
                    x++;
                    break;
                case Direction.SW:
                    y--;
                    x--;
                    break;
                case Direction.SE:
                    y--;
                    x++;
                    break;
            }

            return new Tuple<int, int>(x, y);
        }

        public enum Direction
        {
            N = 1,
            S = 2,
            E = 3,
            W = 4,
            NE = 5,
            NW = 6,
            SE = 7,
            SW = 8,
        }
    }
}
