using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using Student_System.Data.Configurations;

namespace P01_StudentSystem.Data
{
 
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext(DbContextOptions options) : base(options)
        {
        }

        public StudentSystemContext()
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseContext());
            modelBuilder.ApplyConfiguration(new HomeworkSubmissionsContext());
            modelBuilder.ApplyConfiguration(new ResourceContext());
            modelBuilder.ApplyConfiguration(new StudentContext());
            modelBuilder.ApplyConfiguration(new StudentCourseContext());
        }



    }
}
