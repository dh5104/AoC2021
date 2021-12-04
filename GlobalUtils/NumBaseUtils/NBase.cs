namespace GlobalUtils.NumBaseUtils
{
    using System;

    public class NBase
    {
        public int NumberBase { get; set; }

        public string Number { get; set; }

        public NBase(string raw, int numberBase = 2)
        {
            if (!string.IsNullOrWhiteSpace(raw))
            {
                this.Number = raw.ToUpper();
            }
            else
            {
                throw new ArgumentNullException(nameof(raw));
            }

            if (numberBase <= 36)
            {
                this.NumberBase = numberBase;
            }
            else
            {
                throw new ArgumentException($"Number base must be 36 or less.  Requested: {numberBase}");
            }
        }

        public virtual long ToDecimal()
        {
            return this.ConvertFromBase();
        }

        public char GetDigitAt(int index)
        {
            if (index >= 0 && index < this.Number.Length)
            {
                return this.Number[index];
            }
            else
            {
                throw new ArgumentException($"This number \"{this.Number}\" does not have a character at index {index}.");
            }
        }

        public int GetDecimalValueAt(int index)
        {
            char thisDigit = this.GetDigitAt(index);
            int decimalCharValue = 0;
            if (char.IsNumber(thisDigit))
            {
                decimalCharValue = (int)char.GetNumericValue(thisDigit);
            }
            else if (char.IsLetter(thisDigit))
            {
                decimalCharValue = 10 + thisDigit - 'A';
            }
            else
            {
                throw new ApplicationException($"Char '{thisDigit}' is not expected, or supported in numeric base conversions.");
            }

            if (decimalCharValue >= this.NumberBase)
            {
                throw new ApplicationException($"This number \"{this.Number}\" is base {this.NumberBase}, and cannot have '{thisDigit}' ({decimalCharValue}).");
            }

            return decimalCharValue;
        }

        internal long ConvertFromBase()
        {
            long gamma = 0;
            for (int i = 0; i < this.Number.Length; i++)
            {
                // this loop goes forward to support the math, but you read N-base from right to left, so
                //  start at the end and work backwards.
                int decimalCharValue = this.GetDecimalValueAt(this.Number.Length - i - 1);
                
                if (decimalCharValue > 0)
                {
                    gamma += decimalCharValue * (long)System.Math.Pow(this.NumberBase, i);
                }
            }

            return gamma;
        }
    }
}
