using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_System.Data.Configurations
{
    public class ResourceContext : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(x => x.Url)
                .IsUnicode
                (false);

            builder.HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(fk => fk.ResourceId);


        }
    }
}
