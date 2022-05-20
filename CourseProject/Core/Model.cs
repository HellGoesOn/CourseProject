using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CourseProject.Core
{
    public abstract class Model
    {
        public bool WasUpdated { get; set; } = false;

        public bool IsNew { get; set; } = false;

        public static Model UpdateModel(Type type, params object[] values)
        {
            Model model; //= (Model)Activator.CreateInstance(type);

            List<Type> types = new List<Type>();

            PropertyInfo[] props = type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (prop.GetCustomAttribute(typeof(NameAttribute)) == null)
                    continue;

                types.Add(prop.PropertyType);
            }

            ConstructorInfo ctor = type.GetConstructor(types.ToArray());
            
            for(int i = 0; i < values.Length; i++)
            {
                values[i] = Convert.ChangeType(values[i], types[i]);
            }

            model = (Model)ctor.Invoke(values);

            model.WasUpdated = true;
            return model;
        }

        public static Model CreateNewModel(Type type, params object[] values)
        {
            Model model; //= (Model)Activator.CreateInstance(type);

            List<Type> types = new List<Type>();

            PropertyInfo[] props = type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (prop.GetCustomAttribute(typeof(NameAttribute)) == null)
                    continue;

                types.Add(prop.PropertyType);
            }

            ConstructorInfo ctor = type.GetConstructor(types.ToArray());

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Convert.ChangeType(values[i], types[i]);
            }

            model = (Model)ctor.Invoke(values);
            model.IsNew = true;

            return model;
        }
    }
}
