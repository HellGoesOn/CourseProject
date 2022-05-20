using CourseProject.Core;
using CourseProject.Core.Attributes;

namespace CourseProject.Content.Models
{
    public class TeacherSubject : Model
    {
        [Name("ID Связи")]
        public int Id { get; set; }

        [Name("ID Преподавателя")]
        public int TeacherId { get; set; }

        [Name("ID Группы")]
        public int GroupId { get; set; }

        [Name("ID Предмета")]
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
