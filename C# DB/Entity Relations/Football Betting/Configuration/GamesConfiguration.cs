using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace Football_Betting.Configuration
{
    public class GamesConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(g => g.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(g => g.BetId);

            builder.HasOne(t => t.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(fk => fk.HomeTeamId);

            builder.HasOne(t => t.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(fk => fk.AwayTeamId);

        }
    }
}
