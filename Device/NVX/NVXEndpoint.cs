using MAVF.API.Device.Driver.Routing;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace MAVF.Device.NVX
{
	public abstract class NVXEndpoint : InputOutputPort
	{
		[JsonInclude]
		public readonly string ip;

		[JsonInclude]
		public readonly string username;

		[JsonInclude]
		public readonly string password;

		protected readonly HttpClient client;

		public NVXEndpoint()
		{
			var clientHandler = new HttpClientHandler();
			clientHandler.CookieContainer = new CookieContainer();
			clientHandler.UseCookies = true;
			client = new HttpClient(clientHandler);
			client.BaseAddress = new Uri($"https://{ip}");

			if (!Authenticate().Result)
			{
				throw new Exception($"NVXEndpoint failed authentication @ {client.BaseAddress}");
			}
		}

		protected async Task<bool> Authenticate()
		{
			// No need to GET the userlogin
			//await client.GetAsync("userlogin.html");
			var response = await client.PostAsync("/userlogin.html", new StringContent($"login={username}&&passwd={password}", Encoding.UTF8, "text/plain"));
			return response?.IsSuccessStatusCode ?? false;
		}

		/*private async Task<string> GetStreamLocation()
        {
            var response = await client.GetAsync($"/Device/StreamTransmit/Streams/{port - 1}/StreamLocation");
            var body = await response.Content.ReadAsStringAsync();
            var json = JToken.Parse(body);

            return (string?)json["Device"]?["StreamTransmit"]?["Streams"]?[port - 1]?["StreamLocation"] ?? throw new Exception($"NVXEndpoint failed to get stream location address @ {client.BaseAddress}");
        }

        public async Task<bool> Route(NVXEndpoint input)
        {
            var response = await client.PostAsync($"/Device/StreamReceive/{port}/", new StringContent($"\"Device\":{{\"StreamReceive\":{{\"Streams\":[{{\"StreamLocation\":\"{input.streamLocation}\",\"Start\":true}}]}}}}", Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }*/
	}
}
