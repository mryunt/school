using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.DAL.Entities
{
    public class StudentLesson : Audit, IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public int LessonId{ get; set; }
        public Lesson LessonFK { get; set; }
        public int StudentId { get; set; }
        public Student StudentFK { get; set; }
        public bool IsDeleted { get; set; }
    }
}
