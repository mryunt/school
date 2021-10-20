using Homework.Business.Abstract;
using Homework.DAL.Context;
using Homework.DAL.Dtos.Student;
using Homework.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Business.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly HomeworkDbContext _homeworkDbContext;
        public StudentService(HomeworkDbContext homeworkDbContext)
        {
            _homeworkDbContext = homeworkDbContext;
        }
        public async Task<List<GetListStudentDto>> GetStudenList()
        {
            return await _homeworkDbContext.Students.Where(p => !p.IsDeleted).Select(p => new GetListStudentDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname
            }).ToListAsync();
        }
        public async Task<GetStudentDto> GetStudentById(int id)
        {
            return await _homeworkDbContext.Students.Where(p => !p.IsDeleted && p.Id == id).Select(p => new GetStudentDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddStudent(AddStudentDto addStudentDto)
        {
            var addStudent = new Student
            {
                Name = addStudentDto.Name,
                Surname = addStudentDto.Surname
            };
            await _homeworkDbContext.Students.AddAsync(addStudent);
            return await _homeworkDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            var currentStudent = await _homeworkDbContext.Students.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentStudent != null)
            {
                currentStudent.Name = updateStudentDto.Name;
                currentStudent.Surname = updateStudentDto.Surname;

                currentStudent.MDate = DateTime.Now;

                _homeworkDbContext.Students.Update(currentStudent);
                return await _homeworkDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteStudent(int id)
        {
            var currentStudent = await _homeworkDbContext.Students.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentStudent != null)
            {
                currentStudent.IsDeleted = true;
                return await _homeworkDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
