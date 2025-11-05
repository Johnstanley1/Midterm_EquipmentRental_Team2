using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Midterm_EquipmentRental_Team2.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;


        public JWTService(IConfiguration configuration, AppDbContext context) 
        {
            _configuration = configuration; 
            _context = context;
        }

        public string GenerateToken(ClaimsPrincipal principal, TimeSpan? timeSpan = null ) {

            //map the logged -in User to a Customer(by same username) to embed CustomerId in claims
            var user = _context.Users.FirstOrDefault(u => u.Email == principal.FindFirstValue(ClaimTypes.Email));
            var customer = _context.Customers.FirstOrDefault(c => c.Email == user.Email);
            var customerId = customer?.Id ?? 0;

            var issuer = _configuration["Jwt: Issuer"];
            var audience = _configuration["Jwt: Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, principal.FindFirstValue(ClaimTypes.NameIdentifier)),
                new (ClaimTypes.Name, principal.Identity.Name),
                new (ClaimTypes.Email, principal.FindFirstValue(ClaimTypes.Email)),
                new Claim("CustomerId", customerId.ToString()) // used by rental endpoints as CustomerId
            };

            foreach (var role in principal.FindAll(ClaimTypes.Role).Select(c => c.Value).Distinct())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(timeSpan ?? TimeSpan.FromHours(1)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
