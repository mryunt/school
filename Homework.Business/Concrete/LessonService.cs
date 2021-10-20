using Homework.Business.Abstract;
using Homework.DAL.Context;
using Homework.DAL.Dtos.Lesson;
using Homework.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Business.Concrete
{
    public class LessonService : ILessonService
    {
        private readonly HomeworkDbContext _homeworkDbContext;
        public LessonService(HomeworkDbContext homeworkDbContext)
        {
            _homeworkDbContext = homeworkDbContext;
        }

        public async Task<List<GetListLessonDto>> GetLessonList()
        {
            return await _homeworkDbContext.Lessons.Where(p => !p.IsDeleted)
                   .Select(p => new GetListLessonDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Credit = p.Credit
                   }).ToListAsync();
        }

        public async Task<GetLessonDto> GetLessonById(int id)
        {
            return await _homeworkDbContext.Lessons.Where(p => !p.IsDeleted && p.Id == id)
                  .Select(p => new GetLessonDto
                  {
                      Name = p.Name,
                      Credit = p.Credit
                  }).FirstOrDefaultAsync();
        }

        public async Task<int> AddLesson(AddLessonDto addLesson)
        {
            var newLesson = new Lesson
            {
                Name = addLesson.Name,
                Credit = addLesson.Credit
            };
            await _homeworkDbContext.Lessons.AddAsync(newLesson);
            return await _homeworkDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateLesson(int id, UpdateLessonDto updateLesson)
        {
            var currentLesson = await _homeworkDbContext.Lessons.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            if (currentLesson != null)
            {
                currentLesson.Name = updateLesson.Name;
                currentLesson.Credit = updateLesson.Credit;
                currentLesson.MDate = DateTime.Now;
                _homeworkDbContext.Lessons.Update(currentLesson);
                return await _homeworkDbContext.SaveChangesAsync();
            }
            //Bu id' ye ait ders bulunamadı.
            return -1;
        }

        public async Task<int> DeleteLesson(int id)
        {
            var lesson = await _homeworkDbContext.Lessons.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (lesson != null)
            {
                lesson.IsDeleted = true;
                return await _homeworkDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
