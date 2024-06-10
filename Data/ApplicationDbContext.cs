using static PolyglotAPI.Models.Structure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PolyglotAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Language-Course relationship
            modelBuilder.Entity<Language>()
                .HasMany(l => l.Courses)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Course-Module relationship
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Modules)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Module-Lesson relationship
            modelBuilder.Entity<Module>()
                .HasMany(m => m.Lessons)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson-Question relationship
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Question-Option relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Question-Answer relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
