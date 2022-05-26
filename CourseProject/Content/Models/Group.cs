using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseProject.Content.Models
{
    [Name("Группы")]
    [NotKeyword]
    public class Group : Model
    {
        [Name("ID группы")]
        public int Id { get; set; }

        [Name("Название группы")]
        public string Name { get; set; }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Group() { }
    }
}
