using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.DAL.Entities
{
    public class Lesson : Audit, IEntity, ISoftDeleted
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public int Credit { get; set; }
        public ICollection<StudentLesson> StudentLessons{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
