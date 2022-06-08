using CourseProject.Core;
using CourseProject.Core.Attributes;
using System;

namespace CourseProject.Content.Models
{
    [Name("Пары")]
    public class Schedule : Model
    {
        [Name("ID пары")]
        public int Id { get; set; }

        [Name("Пара")]
        [Source(typeof(TeacherSubject), "Id")]
        public  int IDTeacherSubject { get; set; }

        [Name("День проведения пары")]
        public DateTime Day { get; set; }

        [Name("Вид занятия")]
        [Source(typeof(ClassType), "Name")]
        public int ClassTypeId { get; set; }

        public Schedule(int id, int idTchSbj, DateTime day, int classTypeId)
        {
            Id = id;
            IDTeacherSubject = idTchSbj;
            Day = day;
            ClassTypeId = classTypeId;
        }

        public Schedule() { }
    }
}
