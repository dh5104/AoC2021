namespace GlobalUtils.Math
{
    using System;
    using System.Linq;
 
    ////class Program
    ////{
    ////    static void Main(string[] args)
    ////    {
    ////        int[] n = { 3, 5, 7 };
    ////        int[] a = { 2, 3, 2 };

    ////        int result = ChineseRemainderTheorem.Solve(n, a);

    ////        int counter = 0;
    ////        int maxCount = n.Length - 1;
    ////        while (counter <= maxCount)
    ////        {
    ////            Console.WriteLine($"{result} ≡ {a[counter]} (mod {n[counter]})");
    ////            counter++;
    ////        }
    ////    }
    ////}

    public static class ChineseRemainderTheorem
    {
        public static long Solve(long[] numbers, long[] remainders)
        {
            long prod = numbers.Aggregate(1, (long i, long j) => i * j);
            long p;
            long sm = 0;
            for (long i = 0; i < numbers.Length; i++)
            {
                p = prod / numbers[i];
                sm += remainders[i] * ModularMultiplicativeInverse(p, numbers[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }

        public static int Solve(int[] numbers, int[] remainders)
        {
            int prod = numbers.Aggregate(1, (i, j) => i * j);
            int p;
            int sm = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                p = prod / numbers[i];
                sm += remainders[i] * ModularMultiplicativeInverse(p, numbers[i]) * p;
            }
            return sm % prod;
        }

        private static int ModularMultiplicativeInverse(int a, int mod)
        {
            int b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }
}