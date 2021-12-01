namespace Utils._2021.GameConsole
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Assembler
    {
        public static Results Run(List<Instruction> instructions)
        {
            Results r = new Results();
            int index = 0;
            while (0 <= index
                && instructions.Count > index
                && !instructions.Any(t => t.ExecutionCount > 1)
                && r.IterationCount < 8000)
            {
                Instruction inst = instructions[index];

                // Part 1 breaks before a single line is executed a second time
                if (inst.ExecutionCount >= 1)
                {
                    r.ExitMode = ExitType.DuplicateExecution;
                    break;
                }

                switch (inst.Op)
                {
                    case Operations.acc:
                        if (inst.IsPositive)
                        {
                            r.Accumulator += inst.OpValue;
                        }
                        else
                        {
                            r.Accumulator -= inst.OpValue;
                        }

                        inst.ExecutionCount++;
                        index++;
                        break;
                    case Operations.jmp:
                        if (inst.IsPositive)
                        {
                            index += inst.OpValue;
                        }
                        else
                        {
                            index -= inst.OpValue;
                        }

                        inst.ExecutionCount++;
                        break;
                    case Operations.nop:
                        index++;
                        inst.ExecutionCount++;
                        break;
                }

                if (index >= instructions.Count)
                {
                    r.ExitMode = ExitType.Natural;
                    break;
                }

                r.IterationCount++;
            }

            return r;
        }

        public static List<Instruction> CopyInst(List<Instruction> instructions)
        {
            List<Instruction> tempInstructions = new List<Instruction>(instructions.Count);
            foreach (Instruction inst in instructions)
            {
                tempInstructions.Add(new Instruction(inst));
            }

            return tempInstructions;
        }
    }
}
