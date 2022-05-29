using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CourseProject.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SourceAttribute : Attribute
    {
        public readonly Type SourceType;

        public readonly string FieldName;

        public SourceAttribute(Type t, string f)
        {
            SourceType = t;
            FieldName = f;
        }

        public static string GetFullSauce(PropertyInfo inf, Model o)
        {
            string output = "";

            SourceAttribute source = inf.GetCustomAttribute<SourceAttribute>();

            if(source == null)
                return "";

            SourceAttribute src = null;
            var infos = source.SourceType.GetProperties().Where(x => (src = x.GetCustomAttribute<SourceAttribute>()) != null);

            if(!infos.Any())
                output += source.SourceType.GetProperty(source.FieldName).GetValue(o);

            foreach (var info in infos)
            {
                var sts = MainForm.instance.Storages;

                int id = (int)info.GetValue(o);

                var obj = sts[src.SourceType].Find(x => x.id == id);

                if(obj != null)
                    output += obj.GetType().GetProperty(src.FieldName).GetValue(obj);

                var bruv = infos.Last().Name;

                if (info.Name != bruv)
                    output += " - ";
            }



            return output;
        }
    }
}
