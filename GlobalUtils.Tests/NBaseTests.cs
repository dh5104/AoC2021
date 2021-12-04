

namespace GlobalUtils.Tests
{
    using System;
    using FluentAssertions;
    using GlobalUtils.NumBaseUtils;
    using Xunit;

    public class NBaseTests
    {
        [Theory]
        [InlineData("1010", 2, 10)]
        [InlineData("48098123", 10, 48098123)]
        [InlineData("1010", 3, 30)]
        [InlineData("1fe3", 16, 8163)]
        public void TestBaseConversions(string nbaseNumber, int numberBase, long expectedValue)
        {
            NBase nbaseNum = new NBase(nbaseNumber, numberBase);
            long decValue = nbaseNum.ToDecimal();
            decValue.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("1234", 2, 0, true)]
        [InlineData("1234", 2, 1, false)]
        [InlineData("1234", 2, 2, false)]
        [InlineData("1234", 2, 3, false)]
        [InlineData("1234", 3, 3, false)]
        [InlineData("1234", 4, 3, false)]
        [InlineData("1234", 5, 3, true)]
        [InlineData("fe", 10, 0, false)]
        [InlineData("fe", 16, 0, true)]
        public void TestConversionRangeChecks(string nbaseNumber, int numberBase, int index, bool expectedValid)
        {
            NBase nbaseNum = new NBase(nbaseNumber, numberBase);
            bool errored = false;
            try
            {
                int valueAt = nbaseNum.GetDecimalValueAt(index);
            }
            catch (ApplicationException)
            {
                errored = true;
            }

            errored.Should().NotBe(expectedValid);
        }
    }
}
