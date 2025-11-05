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
        private readonly JWTService _jwtService;

        public AuthController(AppDbContext context, JWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }



        [HttpGet("login")]
        [Authorize] 
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                user = new User { Email = request.Email ?? "unknown", Role = "User", IsActive = true };
            }

            var token = _jwtService.GenerateToken(User, TimeSpan.FromHours(1));

            return Ok(new
            {
                token,
                user.Email,
                user.Role
            });
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
