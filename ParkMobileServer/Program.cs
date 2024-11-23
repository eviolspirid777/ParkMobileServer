using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkMobileServer.DbContext;

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
				options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
			});

			builder.Services.AddSingleton<TelegramBot.TelegramBot>(provider =>
			{
				var botToken = "7566916254:AAG6ikx9G9a2ETL1lAEbZFWxXmhj7ylq_MY";
				var chatId = "481227813";
				return new TelegramBot.TelegramBot(botToken, chatId);
			});

			builder.Services.AddControllers();

			var app = builder.Build();

			app.UseCors(cors => cors
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}