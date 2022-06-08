using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CourseProject.Core
{
    public static class FieldExtension
    {
        public static bool Has<T>(this PropertyInfo p)
            where T : Attribute
        {
            return p.GetCustomAttribute<T>() != null;
        }

        public static bool HasNo<T>(this PropertyInfo p)
            where T : Attribute
        {
            bool result = p.GetCustomAttribute<T>() == null;
            return result;
        }
    }
}
