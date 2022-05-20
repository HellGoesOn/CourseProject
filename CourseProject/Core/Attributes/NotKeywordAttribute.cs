using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class NotKeywordAttribute : Attribute
    {
    }
}
