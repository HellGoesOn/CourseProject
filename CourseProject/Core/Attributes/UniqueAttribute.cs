using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UniqueAttribute : Attribute
    {
    }
}
