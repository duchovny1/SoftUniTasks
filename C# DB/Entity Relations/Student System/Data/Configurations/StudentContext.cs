using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_System.Data.Configurations
{
    public class StudentContext : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .IsUnicode(false);
            
                
        }
    }
}
