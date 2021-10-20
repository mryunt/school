using FluentValidation;
using Homework.DAL.Dtos.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Validation.Lesson
{
    public class LessonAddValidator : AbstractValidator<AddLessonDto>
    {
        public LessonAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ders İsmi Alanı Boş Bırakılamaz!")
                .MaximumLength(20).WithMessage("20 Karakterden Fazla Girilemez");
            RuleFor(p => p.Credit).NotEmpty().WithMessage("Dersin Kredisi Boş Bırakılamaz!");
        }
    }
}
