namespace AoC2021.D01
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;

    [Exercise("Template")]
    class Template : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D01");
        }

        protected override void Execute(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Debug.WriteLine(line);
                }
            }

            Console.WriteLine("__");
        }
    }
}
