namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class MusicHubDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<SongPerformer> SongsPerformers { get; set; }
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SongPerformer>()
                .HasKey(pk => new { pk.PerformerId, pk.SongId });

            //builder.Entity<Album>()
            //    .HasOne(p => p.Producer)
            //    .WithMany(a => a.Albums)
            //    .HasForeignKey(fk => fk.ProducerId);

            //builder.Entity<Song>()
            //    .HasOne(a => a.Album)
            //    .WithMany(s => s.Songs)
            //    .HasForeignKey(fk => fk.AlbumId);

            //builder.Entity<Song>()
            //    .HasOne(a => a.Writer)
            //    .WithMany(s => s.Songs)
            //    .HasForeignKey(fk => fk.WriterId);

            //builder.Entity<SongPerformer>()
            //    .HasOne(s => s.Song)
            //    .WithMany(sp => sp.SongPerformers)
            //    .HasForeignKey(fk => fk.SongId);

            //builder.Entity<SongPerformer>()
            //   .HasOne(s => s.Performer)
            //   .WithMany(sp => sp.PerformerSongs)
            //   .HasForeignKey(fk => fk.PerformerId);

        }
    }
}
