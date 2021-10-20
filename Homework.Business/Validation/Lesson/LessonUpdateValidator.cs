using FluentValidation;
using Homework.DAL.Dtos.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Business.Validation.Lesson
{
    public class LessonUpdateValidator : AbstractValidator<UpdateLessonDto>
    {
        public LessonUpdateValidator()
        {

        }
    }
}
