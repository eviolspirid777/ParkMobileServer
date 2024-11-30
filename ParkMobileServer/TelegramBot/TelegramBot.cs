using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkMobileServer.TelegramBot
{
	public class TelegramBot
	{
		private readonly string _botToken;

		public TelegramBot(string botToken)
		{
			_botToken = botToken;
		}

		public async Task SendMessageAsync(string message)
		{
			using (var httpClient = new HttpClient())
			{
				var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
				//Me: 481227813
				//Emil: 643139754
				foreach (var element in new[ ] { "481227813", "643139754" })
				{
					var json = JsonConvert.SerializeObject(new
					{
						chat_id = element,
						text = message
					});

					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = await httpClient.PostAsync(url, content);
					response.EnsureSuccessStatusCode();
				}
			}
		}
	}
}
