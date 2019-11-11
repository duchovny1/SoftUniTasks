using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_System.Data.Configurations
{
    public class HomeworkSubmissionsContext : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasOne(h => h.Course)
                 .WithMany(c => c.HomeworkSubmissions)
                 .HasForeignKey(fk => fk.CourseId);

            builder.HasOne(s => s.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(fk => fk.StudentId);
        }
    }
}
