using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Data;
using PolyglotAPI.Models;
using System.Security.Claims;

namespace PolyglotAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserProfileContext _context;

        public UsersController(UserProfileContext context)
        {
            _context = context;
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetUserProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var username = User.Claims.FirstOrDefault(c => c.Type == "cognito:username")?.Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var userProfile = null as UserProfile;//  _context.UserProfiles.FirstOrDefault(u => u.UserId == userId);

            if (userProfile == null)
            {
                // Optionally, create a new user profile if not found
                userProfile = new UserProfile
                {
                    UserId = userId,
                    Username = username,
                    Email = email,
                    NativeLanguage = "EN",
                    TargetLanguage = "CN",
                    DateOfBirth = DateTime.Today.Date.AddYears(-21),
                    TimeZone = "+0000"
                };
                //_context.UserProfiles.Add(userProfile);
                //_context.SaveChanges();
            }

            return Ok(userProfile);
        }

        [HttpPost("profile")]
        [Authorize]
        public IActionResult UpdateUserProfile(UserProfile updatedProfile)
        {
            // Check if users is 18 years or older
            if (updatedProfile.DateOfBirth.Date.AddYears(18) > DateTime.Today.Date)
            {
                return BadRequest("User must be 18 years or older");
            }
            // Check if user is updating their own profile
            if (updatedProfile.UserId != User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
            {
                return Unauthorized();
            }
            // Check if user's target language is different from their native language
            if (updatedProfile.NativeLanguage == updatedProfile.TargetLanguage ||
                updatedProfile.NativeLanguage == updatedProfile.TargetLanguage2 ||
                updatedProfile.NativeLanguage == updatedProfile.TargetLanguage3)
            {
                return BadRequest("Target language must be different from native language");
            }


            //var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //{
            //    return Unauthorized();
            //}

            //var userProfile = _context.UserProfiles.FirstOrDefault(u => u.UserId == userId);

            //if (userProfile == null)
            //{
            //    return NotFound();
            //}
            //userProfile.NativeLanguage = updatedProfile.NativeLanguage;
            //userProfile.TargetLanguage = updatedProfile.TargetLanguage;
            //userProfile.DateOfBirth = updatedProfile.DateOfBirth;
            //userProfile.TimeZone = updatedProfile.TimeZone;

            //_context.UserProfiles.Update(userProfile);
            //_context.SaveChanges();

            return Ok(updatedProfile);
        }
    }
}
