using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.DAL.Dtos.StudentLesson
{
    public class GetListStudentLessonDto
    {
        public int Id{ get; set; }
        public string LessonName { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
    }
}
