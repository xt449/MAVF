using Newtonsoft.Json.Linq;

namespace MILAV.Device.NVX
{
	public class NVXTransmitter : NVXEndpoint
	{
		public async Task<string> GetStreamLocation()
		{
			var response = await client.GetAsync($"/Device/StreamTransmit/Streams/{port - 1}/StreamLocation");
			var body = await response.Content.ReadAsStringAsync();
			var json = JToken.Parse(body);

			return (string?)json["Device"]?["StreamTransmit"]?["Streams"]?[port - 1]?["StreamLocation"] ?? throw new Exception($"NVXEndpoint failed to get stream location address @ {client.BaseAddress}");
		}
	}
}
