using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace Football_Betting.Configuration
{
    

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(fk => fk.TeamId);

            builder.HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(fk => fk.PositionId);
              
        }
    }
}
