using MAVF.API;
using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Routing;
using System.Text.Json.Serialization;

namespace MAVF.Device.NVX
{
	[Driver("nvx")]
	public class NVXController : IDriver, IRouteControl<NVXTransmitter, NVXReceiver>
	{
		public string Id { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<NVXTransmitter>))]
		public Dictionary<string, NVXTransmitter> Inputs { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<NVXReceiver>))]
		public Dictionary<string, NVXReceiver> Outputs { get; init; }

		Dictionary<NVXReceiver, NVXTransmitter> IRouteControl<NVXTransmitter, NVXReceiver>.Routes { get; } = new Dictionary<NVXReceiver, NVXTransmitter>();

		public object Properties { get; init; }

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
