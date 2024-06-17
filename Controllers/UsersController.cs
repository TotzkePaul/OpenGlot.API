using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Data;
using System.Security.Claims;

namespace PolyglotAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using PolyglotAPI.Data.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUsers()
        {
            _logger.LogInformation("Getting all users");
            var users = await _context.UserProfiles.ToListAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUser(string id)
        {
            _logger.LogInformation($"Getting user with ID: {id}");
            var user = await _context.UserProfiles
                                     .Include(u => u.UserRoles)
                                     .ThenInclude(ur => ur.Role)
                                     .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                _logger.LogWarning($"User with ID: {id} not found");
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpPut]
        public async Task<ActionResult<UserProfile>> AddUser(UserProfile user)
        {
            _logger.LogInformation("Adding a new user");
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserProfile user)
        {
            if (id != user.UserId)
            {
                _logger.LogWarning($"User ID mismatch: {id} != {user.UserId}");
                return BadRequest();
            }

            // Check if users is 18 years or older
            if (user.DateOfBirth.Date.AddYears(18) > DateTime.Today.Date)
            {
                return BadRequest("User must be 18 years or older");
            }
            // Check if user is updating their own profile
            if (user.UserId != User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
            {
                return Unauthorized();
            }
            // Check if user's target language is different from their native language
            if (user.NativeLanguage == user.TargetLanguage ||
                user.NativeLanguage == user.TargetLanguage2 ||
                user.NativeLanguage == user.TargetLanguage3)
            {
                return BadRequest("Target language must be different from native language");
            }

            _logger.LogInformation($"Updating user with ID: {id}");
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    _logger.LogWarning($"User with ID: {id} not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID: {id} not found");
                return NotFound();
            }

            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Users/5/Roles
        [HttpGet("{id}/Roles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRoles(string id)
        {
            _logger.LogInformation($"Getting roles for user with ID: {id}");
            var roles = await _context.UserRoles
                                      .Where(ur => ur.UserId == id)
                                      .Include(ur => ur.Role)
                                      .ToListAsync();

            return Ok(roles);
        }

        // POST: api/Users/5/Roles
        [HttpPost("{id}/Roles")]
        public async Task<IActionResult> AddUserRole(string id, UserRole userRole)
        {
            if (id != userRole.UserId)
            {
                _logger.LogWarning($"User ID mismatch: {id} != {userRole.UserId}");
                return BadRequest();
            }

            _logger.LogInformation($"Adding role to user with ID: {id}");
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5/Roles/3
        [HttpDelete("{id}/Roles/{roleId}")]
        public async Task<IActionResult> RemoveUserRole(string id, int roleId)
        {
            _logger.LogInformation($"Removing role with ID: {roleId} from user with ID: {id}");
            var userRole = await _context.UserRoles
                                         .FirstOrDefaultAsync(ur => ur.UserId == id && ur.RoleId == roleId);

            if (userRole == null)
            {
                _logger.LogWarning($"Role with ID: {roleId} not found for user with ID: {id}");
                return NotFound();
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.UserProfiles.Any(e => e.UserId == id);
        }
    }
}
