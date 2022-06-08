using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Content.Models
{
    public class ClassType : Model
    {
        [Name("Bruv")]
        public int Id { get; set; }

        [Name("Вид занятия")]
        public string Name { get; set; }

        public ClassType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public ClassType() { }
    }
}
