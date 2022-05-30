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

        [Name("Препод. - группа - предмет")]
        [Source(typeof(TeacherSubject), "Id")]
        public  int IDTeacherSubject { get; set; }

        [Name("День проведения пары")]
        public DateTime Day { get; set; }

        public Schedule(int id, int idTchSbj, DateTime day)
        {
            Id = id;
            IDTeacherSubject = idTchSbj;
            Day = day;
        }

        public Schedule() { }
    }
}
