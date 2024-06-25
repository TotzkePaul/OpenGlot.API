namespace PolyglotAPI.Data.Models
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;

    public class UserProfile
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NativeLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public string TargetLanguageLevel { get; set; }
        public string? TargetLanguage2 { get; set; }
        public string? TargetLanguageLevel2 { get; set; }
        public string? TargetLanguage3 { get; set; }
        public string? TargetLanguageLevel3 { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TimeZone { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
        public ICollection<Badge>? Badges { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Flashcard>? Flashcards { get; set; }
        public ICollection<UserGeneratedContent>? UserGeneratedContents { get; set; }
        public Subscription? Subscription { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public enum UserRole
    {
        User,
        Reviewer,
        Creator,
        Admin,
        SuperAdmin
    }
}
