﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PolyglotAPI.Data;

#nullable disable

namespace PolyglotAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240617034340_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PolyglotAPI.Data.Models.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EnglishTranslation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Transcript")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UrlKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Badge", b =>
                {
                    b.Property<int>("BadgeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BadgeId"));

                    b.Property<DateTime>("AwardedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BadgeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BadgeId");

                    b.HasIndex("UserId");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Flashcard", b =>
                {
                    b.Property<int>("FlashcardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FlashcardId"));

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FlashcardId");

                    b.HasIndex("UserId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UrlKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.InteractiveStorybook", b =>
                {
                    b.Property<int>("StorybookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StorybookId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StorybookId");

                    b.ToTable("InteractiveStorybooks");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NotificationId"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AudioId")
                        .HasColumnType("integer");

                    b.Property<int?>("ImageId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AudioId");

                    b.HasIndex("ImageId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Progress", b =>
                {
                    b.Property<int>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProgressId"));

                    b.Property<int>("CompletionPercentage")
                        .HasColumnType("integer");

                    b.Property<int?>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProgressId");

                    b.HasIndex("CourseId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("AudioId")
                        .HasColumnType("integer");

                    b.Property<int?>("ImageId")
                        .HasColumnType("integer");

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AudioId");

                    b.HasIndex("ImageId");

                    b.HasIndex("LessonId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RatingId"));

                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsThumbsUp")
                        .HasColumnType("boolean");

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RatingId");

                    b.HasIndex("ContentId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.StoryChoice", b =>
                {
                    b.Property<int>("ChoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ChoiceId"));

                    b.Property<string>("ChoiceText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Outcome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StorybookId")
                        .HasColumnType("integer");

                    b.HasKey("ChoiceId");

                    b.HasIndex("StorybookId");

                    b.ToTable("StoryChoices");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("PlanName")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserGeneratedContent", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGeneratedContents");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserProfile", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NativeLanguage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguage2")
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguage3")
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguageLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguageLevel2")
                        .HasColumnType("text");

                    b.Property<string>("TargetLanguageLevel3")
                        .HasColumnType("text");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Audio", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Badge", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("Badges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Course", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Language", "Language")
                        .WithMany("Courses")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Flashcard", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("Flashcards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Lesson", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Module", "Module")
                        .WithMany("Lessons")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Module", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Notification", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Option", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Audio", "Audio")
                        .WithMany()
                        .HasForeignKey("AudioId");

                    b.HasOne("PolyglotAPI.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("PolyglotAPI.Data.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("Image");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Progress", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("PolyglotAPI.Data.Models.Module", "Module")
                        .WithMany("Progresses")
                        .HasForeignKey("ModuleId");

                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("Progresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Question", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Audio", "Audio")
                        .WithMany()
                        .HasForeignKey("AudioId");

                    b.HasOne("PolyglotAPI.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("PolyglotAPI.Data.Models.Lesson", "Lesson")
                        .WithMany("Questions")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("Image");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Rating", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Lesson", "Lesson")
                        .WithMany("Ratings")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolyglotAPI.Data.Models.Module", null)
                        .WithMany("Ratings")
                        .HasForeignKey("ModuleId");

                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.StoryChoice", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.InteractiveStorybook", "Storybook")
                        .WithMany("Choices")
                        .HasForeignKey("StorybookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storybook");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Subscription", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithOne("Subscription")
                        .HasForeignKey("PolyglotAPI.Data.Models.Subscription", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserGeneratedContent", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("UserGeneratedContents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserRole", b =>
                {
                    b.HasOne("PolyglotAPI.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolyglotAPI.Data.Models.UserProfile", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Course", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.InteractiveStorybook", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Language", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Lesson", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Module", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Progresses");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("PolyglotAPI.Data.Models.UserProfile", b =>
                {
                    b.Navigation("Badges");

                    b.Navigation("Flashcards");

                    b.Navigation("Notifications");

                    b.Navigation("Progresses");

                    b.Navigation("Ratings");

                    b.Navigation("Subscription")
                        .IsRequired();

                    b.Navigation("UserGeneratedContents");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
