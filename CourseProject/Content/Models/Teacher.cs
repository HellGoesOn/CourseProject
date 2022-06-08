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

        [Name("Рабочих часов")]
        [IgnoreUpdate]
        public int TotalWorkHours { get; set; }

        public Teacher(int id, string fullName, int totalWorkHours)
        {
            Id = id;
            FullName = fullName;
            TotalWorkHours = totalWorkHours;
        }

        public Teacher() { }
    }
}
