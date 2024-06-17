using PolyglotAPI.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PolyglotAPI.Data.Repos
{


    public interface IUserRepository
    {
        Task<IEnumerable<UserProfile>> GetAllUsersAsync();
        Task<UserProfile> GetUserByIdAsync(string userId);
        Task AddUserAsync(UserProfile user);
        Task UpdateUserAsync(UserProfile user);
        Task DeleteUserAsync(string userId);

        Task<IEnumerable<UserRole>> GetUserRolesAsync(string userId);
        Task AddUserRoleAsync(UserRole userRole);
        Task RemoveUserRoleAsync(string userId, int roleId);
    }



public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsersAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> GetUserByIdAsync(string userId)
        {
            return await _context.UserProfiles
                                 .Include(u => u.UserRoles)
                                 .ThenInclude(ur => ur.Role)
                                 .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task AddUserAsync(UserProfile user)
        {
            await _context.UserProfiles.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserProfile user)
        {
            _context.UserProfiles.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.UserProfiles.FindAsync(userId);
            if (user != null)
            {
                _context.UserProfiles.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync(string userId)
        {
            return await _context.UserRoles
                                 .Where(ur => ur.UserId == userId)
                                 .Include(ur => ur.Role)
                                 .ToListAsync();
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRoleAsync(string userId, int roleId)
        {
            var userRole = await _context.UserRoles
                                         .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }

}
