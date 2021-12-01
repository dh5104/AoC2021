using System;

namespace GlobalUtils
{
    public class ConsoleOption
    {
        public Action Handler { get; set; }

        public Func<bool> Enabled { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a character that *should* exist within
        ///  the <see cref="Text"/> specified that acts as the command
        ///  character.  Case sensitive.
        /// </summary>
        public char CommandOverride { get; set; }

        public char Command
        {
            get
            {
                // default is '\0' (U+0000)
                if (this.CommandOverride == default(char))
                {
                    return this.Text[0];
                }
                else
                {
                    return this.CommandOverride;
                }
            }
        }

        public string ConsoleText
        {
            get
            {
                string text = this.Text;

                // default is '\0' (U+0000)
                if (this.CommandOverride == default(char))
                {
                    text = ConsoleCodes.Option(text);
                }
                else
                {
                    text = text.Replace(this.CommandOverride.ToString(), $"{ConsoleCodes.UNDERLINE}{this.CommandOverride}{ConsoleCodes.UNUNDERLINE}");
                }

                return text;
            }
        }

        public Func<bool> Predicate { get; set; }
    }
}
