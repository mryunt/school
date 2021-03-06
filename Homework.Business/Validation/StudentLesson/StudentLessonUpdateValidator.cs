using FluentValidation;
using Homework.DAL.Dtos.StudentLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Validation.StudentLesson
{
    public class StudentLessonUpdateValidator : AbstractValidator<UpdateStudentLessonDto>
    {
        public StudentLessonUpdateValidator()
        {
            RuleFor(p => p.LessonId).NotEmpty().WithMessage("Ders ID' si Boş Bırakılamaz.");
            RuleFor(p => p.StudentId).NotEmpty().WithMessage("Öğrenci ID' si Boş Bırakılamaz.");
        }
    }
}
