using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Midterm_EquipmentRental_Team2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const string JwtSecret = "your_secret_key_here12345678901234"; // Use a secure key in production

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Customers.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        private string GenerateToken(Customer user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
