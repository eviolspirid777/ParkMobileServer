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
		private readonly string _chatId;

		public TelegramBot(string botToken, string chatId)
		{
			_botToken = botToken;
			_chatId = chatId;
		}

		public async Task SendMessageAsync(string message)
		{
			using (var httpClient = new HttpClient())
			{
				var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
				var json = JsonConvert.SerializeObject(new
				{
					chat_id = _chatId,
					text = message
				});

				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();
			}
		}
	}
}
