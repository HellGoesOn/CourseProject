using System;

namespace CourseProject.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NameAttribute : Attribute
    {
        public readonly string Name;

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
