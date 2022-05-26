using CourseProject.Core;
using CourseProject.Core.Attributes;

namespace CourseProject.Content.Models
{
    [Name("Преподаватели")]
    public class Teacher : Model
    {
        [Name("ID Преподавателя")]
        public int Id { get; set; }

        [Name("ФИО Преподавателя")]
        public string FullName { get; set; }

        public Teacher(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public Teacher() { }
    }
}
