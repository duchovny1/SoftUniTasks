using Football_Betting.Configuration;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Configuration;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{

    public class FootballBettingContext : DbContext
    {

        private const string ConectionString = "Server=.\\SQLEXPRESS;Database=Sales Database;Integrated Security=true;";
        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }


        public FootballBettingContext(DbContextOptions options)
           : base(options)
        {
        }

        public FootballBettingContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BetsConfiguration());
            modelBuilder.ApplyConfiguration(new GamesConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerStatisticConfiguration());
            modelBuilder.ApplyConfiguration(new TeamsConfugration());
            modelBuilder.ApplyConfiguration(new TownConfiguration());
        }

    }
}
