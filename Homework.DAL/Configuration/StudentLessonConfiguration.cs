using Homework.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.DAL.Configuration
{
    public class StudentLessonConfiguration : IEntityTypeConfiguration<StudentLesson>
    {
        public void Configure(EntityTypeBuilder<StudentLesson> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.LessonId).IsRequired();
            builder.Property(p => p.StudentId).IsRequired();

            builder.HasOne(p => p.StudentFK).WithMany(p => p.StudentLessons).HasForeignKey(p => p.StudentId);
            builder.HasOne(p => p.LessonFK).WithMany(p => p.StudentLessons).HasForeignKey(p => p.LessonId);
        }
    }
}
