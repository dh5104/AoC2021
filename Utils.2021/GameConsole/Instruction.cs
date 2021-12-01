namespace Utils._2021.GameConsole
{
    using System;

    public class Instruction
    {
        /// <summary>
        /// Gets or sets the type of operation the instruction is asking for
        /// </summary>
        public Operations Op { get; set; }

        /// <summary>
        /// True if the value parameter contained a positive sign
        /// </summary>
        public bool IsPositive { get; set; }

        /// <summary>
        /// The integer value parameter for the operation
        /// </summary>
        public int OpValue { get; set; }

        /// <summary>
        /// The number of times this instruction was executed
        /// </summary>
        public int ExecutionCount { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Instruction"/> using the raw input
        ///  from the puzzles.  The format is typically "[operation] [sign][value]"
        /// </summary>
        /// <param name="rawInputLine">the puzzle input line</param>
        public Instruction(string rawInputLine)
        {
            string[] parts = rawInputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (Enum.TryParse<Operations>(parts[0], out Operations chosenOp))
            {
                this.Op = chosenOp;
                this.OpValue = int.Parse(parts[1].Substring(1));
                this.IsPositive = parts[1][0] == '+';
            }
            else
            {
                throw new ApplicationException($"Failed to parse line, failed to get an operation from \"{rawInputLine}\".");
            }
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="i">an existing instruction to copy</param>
        public Instruction(Instruction i)
        {
            this.Op = i.Op;
            this.OpValue = i.OpValue;
            this.IsPositive = i.IsPositive;
        }
    }
}
