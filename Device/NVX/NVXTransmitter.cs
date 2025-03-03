using System.Text.Json;

namespace MAVF.Device.NVX
{
	public record NVXTransmitter : NVXEndpoint
	{
		public async Task<string> GetStreamLocation()
		{
			var response = await client.GetAsync($"/Device/StreamTransmit/Streams/{Port - 1}/StreamLocation");
			var body = await response.Content.ReadAsStringAsync();
			var json = JsonDocument.Parse(body).RootElement;

			return json.GetProperty("Device").GetProperty("StreamTransmit").GetProperty("Streams")[Port - 1].GetProperty("StreamLocation").GetString() ?? throw new Exception($"NVXEndpoint failed to get stream location address @ {client.BaseAddress}");
		}
	}
}
