using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Configuration
{
    public class BetsConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasOne(u => u.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
