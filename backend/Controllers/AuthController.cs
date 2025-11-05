using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Services;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth.OAuth2;

/// <summary>
/// API Controller for managing authentication of users.
/// Provides login operations by tokenization.
/// </summary>

namespace Midterm_EquipmentRental_Team2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = "/api/auth/redirect"
            };
            return Challenge(props, "Google");
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = "http://localhost:4200/login"
            };
            return SignOut(props, CookieAuthenticationDefaults.AuthenticationScheme);
        }


        // Google redirects here after successful login
        [HttpGet("redirect")]
        [Authorize]
        public IActionResult Callback()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role) ?? "User";

            // Find user in DB
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                user = new User { Email = email ?? "unknown", Role = role };
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            // Redirect to frontend, passing role in query param
            var redirectUrl = $"http://localhost:4200/home?role={role}&email={email}";
            return Redirect(redirectUrl);
        }


        // private const string JwtSecret = "your-secret-key-goes-here0123456789"; // Use a secure key in production

        // generate login tokens by user
        // private string GenerateToken(User user)
        // {
        //     // map the logged-in User to a Customer (by same username) to embed CustomerId in claims
        //     var customer = _context.Customers.FirstOrDefault(c => c.Username == user.Username);
        //     var customerId = customer?.Id ?? 0;

        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, user.Username),
        //         new Claim(ClaimTypes.Role, user.Role),
        //         new Claim("UserId", customerId.ToString()) // used by rental endpoints as CustomerId
        //     };

        //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
        //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //     var token = new JwtSecurityToken(
        //         claims: claims,
        //         expires: DateTime.Now.AddMinutes(30),
        //         signingCredentials: creds
        //     );
        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }


        // login http post request
        // [HttpPost("login")]
        // public ActionResult Login([FromBody] LoginRequest request)
        // {
        //     var user = _context.Users
        //         .FirstOrDefault(u => 
        //             u.Username == request.Username && 
        //             u.Password == request.Password
        //         );

        //     if (user == null)
        //         return Unauthorized("Invalid username or password");

        //     var token = GenerateToken(user);
        //     return Ok(new 
        //     {
        //         token,
        //         user.Role,
        //         user.Username
        //     });
        // }
    }
}
