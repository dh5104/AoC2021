/// <summary>
/// This and many files in util were taken from Tyler Coles, thanks for sharing!
/// </summary>
namespace AoC2021
{
    using System.Collections.Generic;
    using GlobalUtils;

    class Program : ExerciseSelectionConsole
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>
            {
                new ConsoleOption
                {
                    Text = "Enable Debug Output",
                    CommandOverride = 'D',
                    Enabled = () => Debug.EnableDebugOutput,
                    Handler = () => Debug.EnableDebugOutput = !Debug.EnableDebugOutput,
                }
            };
        }
    }
}
