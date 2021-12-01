
namespace GlobalUtils.Spatial
{
    public class GenericPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
        public string Key
        {
            get
            {
                return $"{this.X},{this.Y},{this.Z}";
            }
        }

        public GenericPoint(int x, int y, int z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public GenericPoint(GenericPoint p)
            : this(p.X, p.Y, p.Z)
        {

        }

        public override string ToString()
        {
            return this.Key;
        }

        public override bool Equals(object obj)
        {
            bool areEqual = false;
            if (obj is GenericPoint otherPoint)
            {
                areEqual = this.X == otherPoint.X && this.Y == otherPoint.Y && this.Z == otherPoint.Z;
            }
            
            return areEqual;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
