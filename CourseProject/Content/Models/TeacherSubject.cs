using CourseProject.Core;
using CourseProject.Core.Attributes;

namespace CourseProject.Content.Models
{
    [Name("Преподаватель-Предмет")]
    public class TeacherSubject : Model
    {
        [Name("ID Связи")]
        public int Id { get; set; }

        [Name("ID Преподавателя")]
        [Source(typeof(Teacher), "FullName")]
        public int TeacherId { get; set; }

        [Name("ID Группы")]
        [Source(typeof(Group), "Name")]
        public int GroupId { get; set; }

        [Name("ID Предмета")]
        [Source(typeof(Subject), "Name")]
        public int SubjectId { get; set; }

        public TeacherSubject(int id, int teacherid, int groupid, int subjectid)
        {
            Id = id;
            TeacherId = teacherid;
            GroupId = groupid;
            SubjectId = subjectid;
        }

        public TeacherSubject() { }
    }
}
