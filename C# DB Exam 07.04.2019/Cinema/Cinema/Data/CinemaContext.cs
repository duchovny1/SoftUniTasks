namespace Cinema.Data
{
    using Cinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Projection> Projections { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public CinemaContext()  { }

        public CinemaContext(DbContextOptions options)
            : base(options)   { }

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
            // relations in Seats Table done here
            builder.Entity<Seat>()
                .HasOne(h => h.Hall)
                .WithMany(h => h.Seats)
                .HasForeignKey(h => h.HallId);


            // relations in Projections table done here
            //builder.Entity<Projection>()
            //    .HasKey(pr => new { pr.MovieId, pr.HallId });

            builder.Entity<Projection>()
                 .HasOne(h => h.Hall)
                 .WithMany(h => h.Projections)
                 .HasForeignKey(fk => fk.HallId);

            builder.Entity<Projection>()
                 .HasOne(h => h.Movie)
                 .WithMany(h => h.Projections)
                 .HasForeignKey(fk => fk.MovieId);

            //relations in Tickets table

            //builder.Entity<Ticket>()
            //    .HasKey(ck => new { ck.ProjectionId, ck.CustomerId });

            builder.Entity<Ticket>()
                .HasOne(p => p.Projection)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.ProjectionId);

            builder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(c => c.CustomerId);

            builder.Entity<Projection>()
                .HasMany(p => p.Tickets)
                .WithOne(p => p.Projection);

        }
    }
}