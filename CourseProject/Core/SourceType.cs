using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Core
{
    public class SourceType
    {
        public readonly Type Source;

        public readonly string FieldName;

        public SourceType(Type t, string s)
        {
            Source = t;
            FieldName = s;
        }
    }
}
