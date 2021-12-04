namespace GlobalUtils.NumBaseUtils
{
    public class Binary : NBase
    {
        public Binary(string raw)
            : base(raw, 2)
        {
        }

        public override long ToDecimal()
        {
            return this.ConvertFromBase();
        }

        public Binary Inverse()
        {
            string invertedValue = string.Empty;
            for (int i = 0; i < this.Number.Length; i++)
            {
                invertedValue += ((1 + this.GetDecimalValueAt(i)) % 2).ToString();
            }

            return new Binary(invertedValue);
        }
    }
}
