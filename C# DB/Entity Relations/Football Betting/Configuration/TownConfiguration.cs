using System;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Betting.Configuration
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

        }
    }
}
