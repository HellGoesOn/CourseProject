using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Core
{
    public class ComboBoxItem
    {
        public readonly string ItemName;

        public readonly object Value;

        public ComboBoxItem(string name, object value)
        {
            ItemName = name;
            Value = value;
        }

        public override string ToString()
        {
            return ItemName;
        }
    }
}
