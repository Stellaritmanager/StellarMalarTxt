using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StellarBillingSystem_Malar.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly IConfiguration _config;
        private readonly BillingContext _db;

        public AuthController(IConfiguration config, BillingContext db)
        {
            _config = config;
            _db = db;
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromQuery] string username, [FromQuery] string password)
        {
            var user = _db.SHStaffAdmin.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("UserId", user.StaffID.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    

    public IActionResult Index()
        {
            return View();
        }
    }
}
