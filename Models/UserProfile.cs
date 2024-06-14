using System.ComponentModel.DataAnnotations;

namespace PolyglotAPI.Models
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }
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
    }
}
