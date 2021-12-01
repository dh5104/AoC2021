namespace AoC2021.D14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlobalUtils;
    using GlobalUtils.Attributes;
    using GlobalUtils.Spatial;
    using static GlobalUtils.Spatial.GridUtils;

    [Exercise("")]
    class D14Y2021 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D14");
        }

        protected override void Execute(string[] inputLines)
        {
            ulong[] memory = new ulong[100000];
            string mask = string.Empty;
            foreach(string line in inputLines)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Substring(7);
                    if (mask.Length < 36)
                    {
                        mask = mask.PadLeft(36, 'X');
                    }
                }
                else if (line.Contains("mem"))
                {
                    string memoryLine = line.Substring(4);
                    string[] memoryChunks = memoryLine.Split(new char[] { ' ', '=', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    int address = int.Parse(memoryChunks[0]);
                    if (address >= memory.Length)
                    {
                        throw new ApplicationException($"Memory address {address} will not fit into the allocated memory array of length {memory.Length}.");
                    }

                    string valueToAssign = memoryChunks[1];

                    char[] maskedValues = this.ToBinary(valueToAssign).ToCharArray();
                    for (int i = mask.Length-1; i >= 0; i--)
                    {
                        char maskChar = mask[i];
                        if (maskChar != 'X')
                        {
                            maskedValues[i] = maskChar;
                        }
                    }

                    memory[address] = this.FromBinary(maskedValues);
                }
            }

            ulong sum = 0;
            foreach(ulong registerValue in memory)
            {
                sum += registerValue;
            }

            Console.WriteLine($"Part 1: {sum}");

            // -------- Part 2 ---------------------------------- //

            Dictionary<ulong, ulong> p2MemBank = new Dictionary<ulong, ulong>();

            foreach (string line in inputLines)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Substring(7);
                    if (mask.Length < 36)
                    {
                        mask = mask.PadLeft(36, 'X');
                    }
                }
                else if (line.Contains("mem"))
                {
                    string memoryLine = line.Substring(4);
                    string[] memoryChunks = memoryLine.Split(new char[] { ' ', '=', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    int address = int.Parse(memoryChunks[0]);
                    if (address >= memory.Length)
                    {
                        throw new ApplicationException($"Memory address {address} will not fit into the allocated memory array of length {memory.Length}.");
                    }

                    string valueToAssign = memoryChunks[0];

                    // apply the regular mask for all non-x's, but collect the x's.
                    List<int> floatLocations = new List<int>();
                    char[] maskedValues = this.ToBinary(valueToAssign).ToCharArray();
                    for (int i = mask.Length - 1; i >= 0; i--)
                    {
                        char maskChar = mask[i];
                        if (maskChar == 'X')
                        {
                            floatLocations.Add(i);
                        }
                        else if (maskChar == '1')
                        {
                            maskedValues[i] = maskChar;
                        }
                    }

                    ulong valueToWrite = ulong.Parse(memoryChunks[1]);
                    ulong combinations = (ulong)Math.Pow(2, floatLocations.Count);
                    for (ulong i = 0; i < combinations; i++)
                    {
                        string miniMask = ToBinary(i);
                        for (int j = 0; j < floatLocations.Count; j++)
                        {
                            maskedValues[floatLocations[j]] = miniMask[miniMask.Length - j - 1];
                        }

                        if (Debug.EnableDebugOutput)
                        {
                            Debug.WriteLine($"[{this.FromBinary(maskedValues)}] = {valueToWrite} ({new string(maskedValues)}).");
                        }

                        p2MemBank[this.FromBinary(maskedValues)] = valueToWrite;
                    }
                }
            }

            ulong part2Sum = 0;
            foreach (ulong registerValue in p2MemBank.Values)
            {
                part2Sum += registerValue;
            }

            Console.WriteLine($"Part 2: {part2Sum}");
        }

        private string ToBinary(string stringValue)
        {
            return this.ToBinary(UInt64.Parse(stringValue));
        }

        private string ToBinary(ulong value)
        {
            // 0000 0000 0000 0000 0000 0000 0000 0000 1011
            string binValue = Convert.ToString((long)value, 2);
            return binValue.PadLeft(36, '0');
        }

        private ulong FromBinary(char[] valueArray)
        {
            return this.FromBinary(new string(valueArray));
        }

        private ulong FromBinary(string value)
        {
            // from base 2
            return Convert.ToUInt64(value, 2);
        }
    }
}
