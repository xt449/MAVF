using MAVF.API;
using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Routing;
using System.Text.Json.Serialization;

namespace MAVF.Device.NVX
{
	[Driver("nvx")]
	public class NVXDriver : AbstractCommunicationDriver<NVXDriver.DriverProperties>, IRouteControl<NVXTransmitter, NVXReceiver>
	{
		[JsonConstructor]
		public NVXDriver(DriverProperties properties) : base(properties)
		{
		}

		public Dictionary<string, NVXTransmitter> Inputs => Properties.Inputs;

		public Dictionary<string, NVXReceiver> Outputs => Properties.Outputs;

		Dictionary<NVXReceiver, NVXTransmitter> IRouteControl<NVXTransmitter, NVXReceiver>.Routes { get; } = [];

		bool IRouteControl<NVXTransmitter, NVXReceiver>.ExecuteRoute(NVXTransmitter input, NVXReceiver output)
		{
			var endpoint = Outputs[output.Id];
			if (endpoint == null)
			{
				return false;
			}

			return endpoint.Route(input).Result;
		}

		// Records

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
			[JsonPropertyName("inputs")]
			public required Dictionary<string, NVXTransmitter> Inputs { get; init; }

			[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
			[JsonPropertyName("outputs")]
			public required Dictionary<string, NVXReceiver> Outputs { get; init; }
		}
	}
}
