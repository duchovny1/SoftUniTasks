namespace P03_SalesDatabase.Data
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        private const string ConnectionSting = "Server=.\\SQLEXPRESS;Database=Sales Database;Integrated Security=true;";

        public SalesContext()
        {

        }

        public SalesContext (DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public  DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionSting);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set unicode off at email property 

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            // all relations in table

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Sales)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Sales)
                .WithOne(s => s.Store)
                .HasForeignKey(s => s.StoreId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.StoreId);

            //set GETDATE() function for default value to Date property in Class Sale

            modelBuilder.Entity<Sale>()
                .Property(p => p.Date)
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
