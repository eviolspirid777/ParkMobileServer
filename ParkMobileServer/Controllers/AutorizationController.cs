using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using ParkMobileServer.DbContext;
using ParkMobileServer.Entities.Users;
using System.Data.Entity;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ParkMobileServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AutorizationController : ControllerBase
	{
		private readonly PostgreSQLDbContext _postgreSQLDbContext;
		private readonly IConfiguration _configuration;

		public AutorizationController
		(
			PostgreSQLDbContext postgreSQLDbContext,
			IConfiguration configuration
		)
		{
			_postgreSQLDbContext = postgreSQLDbContext;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(User user)
		{
			// Хеширование пароля
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
			_postgreSQLDbContext.Users.Add(user);
			await _postgreSQLDbContext.SaveChangesAsync();
			return Ok(user);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
		{
			var user = _postgreSQLDbContext.Users.FirstOrDefault(x => x.Username == login.Username);
			bool verify = false;
			if(user != null)
			{
				verify = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);
			}

			string secretKey = _configuration["JwtSecret"]; // Добавьте _configuration в конструктор контроллера
			if (string.IsNullOrEmpty(secretKey))
			{
				return StatusCode(500, "SecretKey is not set");
			}

			if (user == null || !verify)
			{
				return Unauthorized();
			}

			// Генерация токена (например, JWT)
			var token = GenerateToken(user, secretKey);
			return Ok(token);
		}

		private string GenerateToken(User user, string secretKey)
		{
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
			var secretKeySecurity = new SymmetricSecurityKey(secretKeyBytes);
			var credentials = new SigningCredentials(secretKeySecurity, SecurityAlgorithms.HmacSha256);

			// Укажите данные, которые будете кодировать
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			// Создайте токен
			var token = new JwtSecurityToken(
				issuer: "ParkMobileServer",
				audience: "ParkMobileAdmin",
				claims: claims,
				expires: DateTime.Now.AddMinutes(30), // Истечение через 30 минут
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
