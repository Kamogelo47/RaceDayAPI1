
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RaceDayAPI.Data;
using RaceDayAPI.Filters;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SessionAuthorize]
    public class ProfilesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ProfilesController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            // Users may only view their own profile
            int? loggedInUserId =
                HttpContext.Session.GetInt32("UserID");

            if (loggedInUserId == null)
                return Unauthorized(new
                {
                    message = "Please log in to continue."
                });

            if (loggedInUserId.Value != id)
                return Forbid();

            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.UserID == id)
                .Select(u => new
                {
                    u.UserID,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Phone,
                    u.Role,
                    u.DateCreated
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new
                {
                    message = "Profile not found."
                });

            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfile(
            int id,
            [FromBody] UpdateProfileRequest request)
        {
            int? loggedInUserId =
                HttpContext.Session.GetInt32("UserID");

            if (loggedInUserId == null)
                return Unauthorized(new
                {
                    message = "Please log in to continue."
                });

            if (loggedInUserId.Value != id)
                return Forbid();

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new
                {
                    message = "Profile not found."
                });

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Phone = request.Phone;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Profile updated successfully.",
                user.UserID,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Phone,
                user.Role
            });
        }
    }

    public class UpdateProfileRequest
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }
    }
}
