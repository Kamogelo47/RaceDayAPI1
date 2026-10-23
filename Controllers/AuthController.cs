
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.DTOs;
using RaceDayAPI.Filters;
using RaceDayAPI.Services;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        private readonly PasswordService _passwordService;

        public AuthController(
            RaceDayDbContext context,
            PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // GET: api/Auth/status
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new
            {
                Message = "RaceDayAPI authentication is running."
            });
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Email.ToLower() == email);

            if (existingUser != null)
            {
                return Conflict(new
                {
                    Message = "An account with this email already exists."
                });
            }

            var user = new Models.User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = email,
                Phone = request.Phone,

                // Public registration cannot create Organiser accounts
                Role = "Participant",

                PasswordHash = _passwordService.HashPassword(
                    request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                Message = "Registration successful.",
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Email.ToLower() == email);

            if (user == null ||
                !_passwordService.VerifyPassword(
                    user.PasswordHash,
                    request.Password))
            {
                return Unauthorized(new
                {
                    Message = "Invalid email or password."
                });
            }

            HttpContext.Session.Clear();

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserRole", user.Role);

            return Ok(new
            {
                Message = "Login successful.",
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            });
        }

        // POST: api/Auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Ok(new
            {
                Message = "Logout successful."
            });
        }

        // GET: api/Auth/profile-test
        // Requires a logged-in user
        [HttpGet("profile-test")]
        [SessionAuthorize]
        public IActionResult ProfileTest()
        {
            return Ok(new
            {
                Message = "You are authenticated.",
                UserID = HttpContext.Session.GetInt32("UserID"),
                Role = HttpContext.Session.GetString("UserRole")
            });
        }

        // GET: api/Auth/organiser-test
        // Requires a logged-in Organiser
        [HttpGet("organiser-test")]
        [SessionAuthorize("Organiser")]
        public IActionResult OrganiserTest()
        {
            return Ok(new
            {
                Message = "Welcome to the Organiser area.",
                Role = HttpContext.Session.GetString("UserRole")
            });
        }
    }
}
