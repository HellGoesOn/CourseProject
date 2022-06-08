using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CourseProject.Core
{
    public abstract class Model
    {
        internal int id;

        public bool WasUpdated { get; set; } = false;

        public bool IsNew { get; set; } = false;

        public bool ToBeRemoved { get; set; } = false;

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

            int id = -1;
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != null)
                        values[i] = Convert.ChangeType(values[i], types[i]);
                    else
                        values[i] = Default(types[i]);

                    if (i == 0)
                        id = (int)values[i];
                }
            }
            catch
            {
                MessageBox.Show("Введён неверный формат данных");

                model = (Model)ctor.Invoke(new object[values.Length]);
                model.ToBeRemoved = true;
                return model;
            }

            model = (Model)ctor.Invoke(values);

            model.WasUpdated = true;

            if (id != -1)
                model.id = id;
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

            int id = -1;

            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != null)
                        values[i] = Convert.ChangeType(values[i], types[i]);
                    else
                        values[i] = Default(types[i]);

                    if (i == 0)
                        id = (int)values[i];
                }
            }
            catch
            {
                MessageBox.Show("Введён неверный формат данных");

                model = (Model)ctor.Invoke(new object[values.Length]);
                model.ToBeRemoved = true;
                return model;
            }

            model = (Model)ctor.Invoke(values);
            model.IsNew = true;

            if (id != -1)
                model.id = id;

            return model;
        }

        public override string ToString()
        {
            string s = "";
            Type t = GetType();

            PropertyInfo[] props = t.GetProperties().Where(x => x.GetCustomAttribute<NameAttribute>() != null && x.Name != "Id").ToArray();

            foreach(PropertyInfo prop in props)
            {
                SourceAttribute attr = prop.GetCustomAttribute<SourceAttribute>();

                if(attr != null)
                {
                    var st = MainForm.instance.Storages[attr.SourceType];

                    var md = st.Find(x => x.id == (int)prop.GetValue(this));

                    if(md != null)
                        s += attr.SourceType.GetProperty(attr.FieldName).GetValue(md);
                }
                else
                    s += " " + prop.GetValue(this).ToString();
            }

            return s;
        }

        public static object Default(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
