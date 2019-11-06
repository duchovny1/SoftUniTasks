using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        private const string connectionString = "Server=.\\SQLEXPRESS;Database=Hospital Database;Integrated Security=true;";
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Visitation>()
            //    .HasOne(d => d.Doctor);


            builder.Entity<Patient>()
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);

            builder.Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);

            builder.Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);



            builder.Entity<Medicament>()
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Medicament)
                .HasForeignKey(p => p.MedicamentId);

            builder.Entity<Visitation>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(p => p.PatientId);

            builder.Entity<PatientMedicament>()
                 .HasKey(pm => new { pm.PatientId, pm.Medicament });



            builder.Entity<Visitation>()
                         .HasOne(v => v.Patient)
                         .WithMany(p => p.Visitations)
                         .HasForeignKey(v => v.PatientId);

            builder.Entity<Visitation>()
                .HasOne(v => v.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(x => x.DoctorId);


        }
    }
}
