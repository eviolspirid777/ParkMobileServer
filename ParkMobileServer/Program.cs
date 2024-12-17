using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkMobileServer.DbContext;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ParkMobileServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<PostgreSQLDbContext>(options =>
			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreWorkConnection"));
			});

			builder.Services.AddSingleton(provider =>
			{
				var botToken = "7566916254:AAG6ikx9G9a2ETL1lAEbZFWxXmhj7ylq_MY";
				return new TelegramBot.TelegramBot(botToken);
			});

			//builder.WebHost.UseUrls("http://*:3001");

			builder.Services.AddControllers();

			string secretKey = builder.Configuration["JwtSecret"]; // �������� JwtSecret � appsettings.json ��� ���������� ���������
			if (string.IsNullOrEmpty(secretKey))
			{
				throw new Exception("JwtSecret �� �����!");
			}
			var key = Encoding.UTF8.GetBytes(secretKey);
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = "ParkMobileServer", // ������� ��� issuer
					ValidAudience = "ParkMobileAdmin", // ������� ���� ���������
					IssuerSigningKey = new SymmetricSecurityKey(key)
					//��� �������� ������ �� ������ �������� Bearer 
				};
			});

			var app = builder.Build();

			app.UseCors(cors => cors
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}