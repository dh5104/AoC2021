using System;

namespace GlobalUtils
{
    public static class Debug
    {
        public static bool EnableDebugOutput { get; set; } = false;

        public static void Write(object obj)
        {
            if (EnableDebugOutput)
                Console.Write(obj);
        }

        public static void WriteLine(object obj)
        {
            if (EnableDebugOutput)
                Console.WriteLine(obj);
        }

        public static void WriteLine()
        {
            if (EnableDebugOutput)
                Console.WriteLine();
        }
    }
}
