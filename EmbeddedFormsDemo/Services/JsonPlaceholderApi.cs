using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using EmbeddedFormsDemo.Models;

namespace EmbeddedFormsDemo.Services
{
	public class JsonPlaceholderApi
	{
		private const string ApiRoot = "http://jsonplaceholder.typicode.com/";

		private readonly HttpClient client;
		private readonly Random random;

		public JsonPlaceholderApi()
		{
			client = new HttpClient { BaseAddress = new Uri(ApiRoot) };
			random = new Random();
		}

		public async Task<User> LoginAsync(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
				return null;

			// totally fake a user id from somewhere, ignoring the parameters
			var userId = random.Next(1, 11);

			var result = await client.GetStringAsync($"users/{userId}");
			return JsonConvert.DeserializeObject<User>(result);
		}
	}
}
