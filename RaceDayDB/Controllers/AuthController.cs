using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.DTOs;
using RaceDayDB.Models;
using System.Security.Claims;


namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public AuthController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,
                Role = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)
                    ?.Value
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Logged out successfully.");
        }

        [HttpPost("register-runner")]
        public IActionResult RegisterRunner(RegisterRunnerDTO dto)
        {
            if (_context.Runners.Any(r => r.Email == dto.Email))
            {
                return BadRequest("Email already exists.");
            }

            var runner = new Runner
            {
                Name = dto.Name,
                Age = dto.Age,
                Gender = dto.Gender,
                ContactInfo = dto.ContactInfo,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Runners.Add(runner);
            _context.SaveChanges();

            return Ok("Runner registered successfully.");
        }

        [HttpPost("register-organizer")]
        public IActionResult RegisterOrganizer(RegisterOrganizerDTO dto)
        {
            if (_context.Organizers.Any(o => o.Email == dto.Email))
            {
                return BadRequest("Email already exists.");
            }

            var organizer = new Organizer
            {
                Name = dto.Name,
                ContactInfo = dto.ContactInfo,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Organizers.Add(organizer);
            _context.SaveChanges();

            return Ok("Organizer registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var runner = _context.Runners
                .FirstOrDefault(r => r.Email == dto.Email);

            if (runner != null)
            {
                bool validPassword =
                    BCrypt.Net.BCrypt.Verify(
                        dto.Password,
                        runner.PasswordHash);

                if (validPassword)
                {
                    var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, runner.Name),
    new Claim(ClaimTypes.Email, runner.Email),
    new Claim(ClaimTypes.Role, "Runner")
};

                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal).Wait();

                    return Ok("Runner login successful.");
                }
            }

            var organizer = _context.Organizers
                .FirstOrDefault(o => o.Email == dto.Email);

            if (organizer != null)
            {
                bool validPassword =
                    BCrypt.Net.BCrypt.Verify(
                        dto.Password,
                        organizer.PasswordHash);

                if (validPassword)
                {
                    var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, organizer.Name),
    new Claim(ClaimTypes.Email, organizer.Email),
    new Claim(ClaimTypes.Role, "Organizer")
};

                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal).Wait();

                    return Ok("Organizer login successful.");
                }
            }

            return Unauthorized("Invalid email or password.");
        }
    }
}
