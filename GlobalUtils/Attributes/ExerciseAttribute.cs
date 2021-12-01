using System;

namespace GlobalUtils.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExerciseAttribute : Attribute
    {
        public string Name { get; }

        public ExerciseAttribute(string name) : base()
        {
            Name = name;
        }
    }
}
