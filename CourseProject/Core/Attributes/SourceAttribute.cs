using System;

namespace CourseProject.Core.Attributes
{
    public class SourceAttribute : Attribute
    {
        public readonly Type SourceType;

        public readonly string FieldName;

        public SourceAttribute(Type t, string f)
        {
            SourceType = t;
            FieldName = f;
        }
    }
}
