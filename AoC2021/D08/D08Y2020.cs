namespace AoC2021.D08
{
    using System;
    using System.Collections.Generic;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using Utils._2021.GameConsole;

    [Exercise("")]
    class D08Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D08");
        }

        protected override void Execute(string[] inputLines)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (string line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    instructions.Add(new Instruction(line));
                }
            }

            Console.WriteLine($"{instructions.Count} instructions parsed from input.");
            var results = Assembler.Run(Assembler.CopyInst(instructions));
            Console.WriteLine($"Part 1: acc = {results.Accumulator}");

            for (int i = 0; i < instructions.Count; i++)
            {
                List<Instruction> tempInstructions = Assembler.CopyInst(instructions);
                
                bool changed = false;
                if (tempInstructions[i].Op == Operations.jmp)
                {
                    tempInstructions[i].Op = Operations.nop;
                    changed = true;
                }
                else if (tempInstructions[i].Op == Operations.nop)
                {
                    tempInstructions[i].Op = Operations.jmp;
                    changed = true;
                }

                if (changed)
                {
                    var loopedResults = Assembler.Run(tempInstructions);
                    if (loopedResults.ExitMode == ExitType.Natural)
                    {
                        Console.WriteLine($"Swapping inst {i} led to a natural execution.");
                        Console.WriteLine($"Part 2: {loopedResults.Accumulator}");
                        break;
                    }
                }
            }
        }
    }
}
