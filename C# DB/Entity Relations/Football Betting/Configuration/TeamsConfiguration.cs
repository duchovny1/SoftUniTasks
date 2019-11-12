using System;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Betting.Configuration
{
    
    public class TeamsConfugration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasOne(t => t.PrimaryKitColor)
                .WithMany(t => t.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId);

            builder.HasOne(t => t.SecondaryKitColor)
                .WithMany(t => t.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);

            builder.HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);

        }
    }
}
