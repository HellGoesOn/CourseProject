using CourseProject.Core;
using CourseProject.Core.Attributes;

namespace CourseProject.Content.Models
{
    public class Subject : Model
    {
        [Name("ID предмета")]
        public int Id { get; set; }

        [Name("Название предмета")]
        public string Name { get; set; }

        public Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Subject() { }
    }
}
