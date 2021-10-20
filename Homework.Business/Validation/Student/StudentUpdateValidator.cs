using FluentValidation;
using Homework.DAL.Dtos.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Validation.Student
{
    public class StudentUpdateValidator :AbstractValidator<UpdateStudentDto>
    {
        public StudentUpdateValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Öğrenci Adı boş geçilemez.")
                .MaximumLength(20).WithMessage("Öğrenci Adı en fazla 20 karakter olabilir.");
            RuleFor(p => p.Surname).NotEmpty().WithMessage("Öğrenci Soyadı boş geçilemez.")
                .MaximumLength(20).WithMessage("Öğrenci Soyadı en fazla 20 karakter olabilir.");
        }
    }
}
