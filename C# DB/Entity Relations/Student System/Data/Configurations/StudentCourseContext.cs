using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace Student_System.Data.Configurations
{
   public class StudentCourseContext : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });

            builder.HasOne(e => e.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(fk => fk.StudentId);

            builder.HasOne(e => e.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(fk => fk.CourseId);


        }
    }
}
