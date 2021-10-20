using Homework.DAL.Dtos.StudentLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Abstract
{
    public interface IStudentLessonService
    {
        public Task<List<GetListStudentLessonDto>> StudentLessonList();
        public Task<GetStudentLessonDto> GetStudentLessonById(int id);
        public Task<int> AddStudentLesson(AddStudentLessonDto addStudentLesson);
        public Task<int> UpdateStudentLesson(int id, UpdateStudentLessonDto updateStudentLesson);
        public Task<int> DeleteStudentLesson(int id);
    }
}
