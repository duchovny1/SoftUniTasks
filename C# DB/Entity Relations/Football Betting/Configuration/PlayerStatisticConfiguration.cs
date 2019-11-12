using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace Football_Betting.Configuration
{ 
   
    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(k => new { k.PlayerId, k.GameId });

            builder.HasOne(p => p.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(fk => fk.PlayerId);

            builder.HasOne(p => p.Game)
               .WithMany(p => p.PlayerStatistics)
               .HasForeignKey(fk => fk.GameId);

        }
    }
}
