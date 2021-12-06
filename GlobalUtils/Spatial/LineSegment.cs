using System.Collections.Generic;
using System.Linq;

namespace GlobalUtils.Spatial
{
    public class LineSegment
    {
        public LineSegment(int x1, int y1, int x2, int y2)
        {
            if (x1 < x2)
            {
                this.X1 = x1;
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2;
            }
            else if (x1 > x2)
            {
                this.X1 = x2;
                this.Y1 = y2;
                this.X2 = x1;
                this.Y2 = y1;
            }
            else if (x1 == x2 && y1 <= y2)
            {
                this.X1 = x1;
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2;
            }
            else
            {
                this.X1 = x2;
                this.Y1 = y2;
                this.X2 = x1;
                this.Y2 = y1;
            }

            if (x1 == x2)
            {
                this.IsVertical = true;
            }
            else if (y1 == y2)
            {
                this.IsHorizontal = true;
            }
            else if (System.Math.Abs(x2 - x1) == System.Math.Abs(y2 - y1))
            {
                this.IsDiagonal = true;
                this.DiagonalPoints = new List<GenericPoint>();
                int numPoints = 1 + System.Math.Abs(x2 - x1);

                bool increasing = this.Y1 < this.Y2;

                // x is already assigned as going up based on the sorting above.
                for (int i = 0; i < numPoints; i++)
                {
                    if (increasing)
                    {
                        this.DiagonalPoints.Add(new GenericPoint(this.X1 + i, this.Y1 + i));
                    }
                    else
                    {
                        this.DiagonalPoints.Add(new GenericPoint(this.X1 + i, this.Y1 - i));
                    }
                }
            }
        }

        public int X1 { get; private set; }

        public int Y1 { get; private set; }

        public int X2 {  get; private set;}

        public int Y2 {  get; private set;}

        public bool IsVertical { get; private set; }

        public bool IsHorizontal { get; private set; }

        public bool IsDiagonal { get; private set; }

        public List<GenericPoint> DiagonalPoints { get; private set; }

        public bool In(int x, int y)
        {
            bool withinLine = false;
            if (this.IsVertical && x == this.X1)
            {
                withinLine = (y >= this.Y1 && y <= this.Y2);
            }
            else if (this.IsHorizontal && y == this.Y1)
            {
                withinLine = (x >= this.X1 && x <= this.X2);
            }
            else if (this.IsDiagonal && x >= this.X1 && x <= this.X2)
            {
                withinLine = this.DiagonalPoints.Any(pt => pt.X == x && pt.Y == y);
            }

            return withinLine;
        }
    }
}
