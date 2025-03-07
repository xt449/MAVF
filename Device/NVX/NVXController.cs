﻿using MAVF.API;
using MAVF.API.Device;
using MAVF.API.Device.Routing;
using Newtonsoft.Json;

namespace MAVF.Device.NVX
{
	[Device("nvx")]
	public class NVXController : IDevice, IRouteControl<NVXTransmitter, NVXReceiver>
	{
		public string Id { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<NVXTransmitter>))]
		public Dictionary<string, NVXTransmitter> Inputs { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<NVXReceiver>))]
		public Dictionary<string, NVXReceiver> Outputs { get; init; }

		Dictionary<NVXReceiver, NVXTransmitter> IRouteControl<NVXTransmitter, NVXReceiver>.Routes { get; } = new Dictionary<NVXReceiver, NVXTransmitter>();

		public void Initialize()
		{
			// Apply possible default routes here?
		}

		bool IRouteControl<NVXTransmitter, NVXReceiver>.ExecuteRoute(NVXTransmitter input, NVXReceiver output)
		{
			var endpoint = Outputs[output.Id];
			if (endpoint == null)
			{
				return false;
			}

			return endpoint.Route(input).Result;
		}
	}
}
