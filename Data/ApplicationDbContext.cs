using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Data.Models;

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
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<UserGeneratedContent> UserGeneratedContents { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<InteractiveStorybook> InteractiveStorybooks { get; set; }
        public DbSet<StoryChoice> StoryChoices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserProfile configuration
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasMany(e => e.Ratings)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Progresses)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Badges)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Notifications)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.Flashcards)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                entity.HasMany(e => e.UserGeneratedContents)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
            });

            // Rating configuration
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.RatingId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Ratings)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Lesson)
                      .WithMany(e => e.Ratings)
                      .HasForeignKey(e => e.ContentId);
            });

            // Progress configuration
            modelBuilder.Entity<Progress>(entity =>
            {
                entity.HasKey(e => e.ProgressId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Progresses)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Module)
                      .WithMany(e => e.Progresses)
                      .HasForeignKey(e => e.ModuleId);

                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId);
            });

            // Language configuration
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Courses)
                      .WithOne(e => e.Language)
                      .HasForeignKey(e => e.LanguageId);
            });

            // Course configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Language)
                      .WithMany(e => e.Courses)
                      .HasForeignKey(e => e.LanguageId);

                entity.HasMany(e => e.Modules)
                      .WithOne(e => e.Course)
                      .HasForeignKey(e => e.CourseId);
            });

            // Module configuration
            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Course)
                      .WithMany(e => e.Modules)
                      .HasForeignKey(e => e.CourseId);

                entity.HasMany(e => e.Lessons)
                      .WithOne(e => e.Module)
                      .HasForeignKey(e => e.ModuleId);
            });

            // Lesson configuration
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Module)
                      .WithMany(e => e.Lessons)
                      .HasForeignKey(e => e.ModuleId);

                entity.HasMany(e => e.Questions)
                      .WithOne(e => e.Lesson)
                      .HasForeignKey(e => e.LessonId);

                entity.HasMany(e => e.Ratings)
                      .WithOne(e => e.Lesson)
                      .HasForeignKey(e => e.ContentId);
            });

            // Question configuration
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Lesson)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(e => e.LessonId);

                entity.HasOne(e => e.Audio)
                      .WithMany()
                      .HasForeignKey(e => e.AudioId);

                entity.HasOne(e => e.Image)
                      .WithMany()
                      .HasForeignKey(e => e.ImageId);

                entity.HasMany(e => e.Options)
                      .WithOne(e => e.Question)
                      .HasForeignKey(e => e.QuestionId);
            });

            // Audio configuration
            modelBuilder.Entity<Audio>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // Image configuration
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // Option configuration
            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Audio)
                      .WithMany()
                      .HasForeignKey(e => e.AudioId);

                entity.HasOne(e => e.Image)
                      .WithMany()
                      .HasForeignKey(e => e.ImageId);
            });

            // Badge configuration
            modelBuilder.Entity<Badge>(entity =>
            {
                entity.HasKey(e => e.BadgeId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Badges)
                      .HasForeignKey(e => e.UserId);
            });

            // Notification configuration
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Notifications)
                      .HasForeignKey(e => e.UserId);
            });

            // Flashcard configuration
            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.HasKey(e => e.FlashcardId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Flashcards)
                      .HasForeignKey(e => e.UserId);
            });

            // UserGeneratedContent configuration
            modelBuilder.Entity<UserGeneratedContent>(entity =>
            {
                entity.HasKey(e => e.ContentId);

                entity.HasOne(e => e.User)
                      .WithMany(e => e.UserGeneratedContents)
                      .HasForeignKey(e => e.UserId);
            });

            // Subscription configuration
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.HasOne(e => e.User)
                      .WithOne(e => e.Subscription)
                      .HasForeignKey<Subscription>(e => e.UserId);
            });

            // InteractiveStorybook configuration
            modelBuilder.Entity<InteractiveStorybook>(entity =>
            {
                entity.HasKey(e => e.StorybookId);

                entity.HasMany(e => e.Choices)
                      .WithOne(e => e.Storybook)
                      .HasForeignKey(e => e.StorybookId);
            });

            // StoryChoice configuration
            modelBuilder.Entity<StoryChoice>(entity =>
            {
                entity.HasKey(e => e.ChoiceId);

                entity.HasOne(e => e.Storybook)
                      .WithMany(e => e.Choices)
                      .HasForeignKey(e => e.StorybookId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
