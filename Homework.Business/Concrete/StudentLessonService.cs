using Homework.Business.Abstract;
using Homework.DAL.Context;
using Homework.DAL.Dtos.StudentLesson;
using Homework.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Concrete
{
    public class StudentLessonService : IStudentLessonService
    {
        private readonly HomeworkDbContext _homeworkDbContext;
        public StudentLessonService(HomeworkDbContext homeworkDbContext)
        {
            _homeworkDbContext = homeworkDbContext;
        }
        public async Task<List<GetListStudentLessonDto>> StudentLessonList()
        {
            return await _homeworkDbContext.StudentLessons.Include(p => p.StudentFK).Include(p => p.LessonFK).Where(p => !p.IsDeleted)
                .Select(p => new GetListStudentLessonDto
                {
                    Id = p.Id,
                    LessonName = p.LessonFK.Name,
                    StudentName = p.StudentFK.Name,
                    StudentSurname = p.StudentFK.Surname
                }
                ).ToListAsync();
        }
        public async Task<GetStudentLessonDto> GetStudentLessonById(int id)
        {
            return await _homeworkDbContext.StudentLessons.Include(p => p.StudentFK).Include(p => p.LessonFK).Where(p => !p.IsDeleted && p.Id == id)
                .Select(p => new GetStudentLessonDto
                {
                    LessonName = p.LessonFK.Name,
                    StudentName = p.StudentFK.Name,
                    StudentSurname = p.StudentFK.Surname
                }).FirstOrDefaultAsync();
        }
        public async Task<int> AddStudentLesson(AddStudentLessonDto addStudentLesson)
        {
            var newStudentLesson = new StudentLesson
            {
                StudentId = addStudentLesson.StudentId,
                LessonId = addStudentLesson.LessonId
            };
            await _homeworkDbContext.StudentLessons.AddAsync(newStudentLesson);
            return await _homeworkDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateStudentLesson(int id, UpdateStudentLessonDto updateStudentLesson)
        {
            var currentStudentLesson = await _homeworkDbContext.StudentLessons.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            if (currentStudentLesson != null)
            {
                currentStudentLesson.StudentId = updateStudentLesson.StudentId;
                currentStudentLesson.LessonId = updateStudentLesson.LessonId;
                currentStudentLesson.MDate = DateTime.Now;
                _homeworkDbContext.StudentLessons.Update(currentStudentLesson);
                return await _homeworkDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteStudentLesson(int id)
        {
            var currentStudentLesson = await _homeworkDbContext.StudentLessons.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentStudentLesson != null)
            {
                currentStudentLesson.IsDeleted = true;
                return await _homeworkDbContext.SaveChangesAsync();

            }
            return -1;
        }
    }
}
