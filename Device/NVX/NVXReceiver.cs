﻿using System.Text;

namespace MAVF.Device.NVX
{
	public record NVXReceiver : NVXEndpoint
	{
		public async Task<bool> Route(NVXTransmitter input)
		{
			var response = await client.PostAsync($"/Device/StreamReceive/{Port}/", new StringContent($"\"Device\":{{\"StreamReceive\":{{\"Streams\":[{{\"StreamLocation\":\"{await input.GetStreamLocation()}\",\"Start\":true}}]}}}}", Encoding.UTF8, "application/json"));

			return response.IsSuccessStatusCode;
		}
	}
}
