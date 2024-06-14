using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Models;

namespace PolyglotAPI.Data
{
    public class UserProfileContext : DbContext
    {
        public UserProfileContext(DbContextOptions<UserProfileContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
