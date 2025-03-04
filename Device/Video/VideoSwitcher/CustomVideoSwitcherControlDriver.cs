using MAVF.API;
using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Routing;
using MAVF.API.Device.Driver.Video.VideoSwitcher;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.Video.VideoSwitcher
{
	[Driver("customVideoswitcher")]
	public class CustomVideoSwitcherControlDriver : AbstractCommunicationDriver<CustomVideoSwitcherControlDriver.DriverProperties>, IVideoSwitcherControl<InputOutputPort, InputOutputPort>
	{
		[JsonConstructor]
		public CustomVideoSwitcherControlDriver(DriverProperties properties) : base(properties)
		{
		}

		public Dictionary<string, InputOutputPort> Inputs => Properties.Inputs;

		public Dictionary<string, InputOutputPort> Outputs => Properties.Outputs;

		Dictionary<InputOutputPort, InputOutputPort> IRouteControl<InputOutputPort, InputOutputPort>.Routes { get; } = [];

		bool IRouteControl<InputOutputPort, InputOutputPort>.ExecuteRoute(InputOutputPort input, InputOutputPort output)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(Properties.RequestSetRoute, input.Port, output.Port));

				return Regex.Match(Connection.ReadASCII(), Properties.ResponseSetRoute).Success;
			}

			return false;
		}

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestSetRoute")]
			public required string RequestSetRoute { get; init; }

			[JsonPropertyName("responseSetRoute")]
			public required string ResponseSetRoute { get; init; }

			[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
			[JsonPropertyName("inputs")]
			public required Dictionary<string, InputOutputPort> Inputs { get; init; }

			[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
			[JsonPropertyName("outputs")]
			public required Dictionary<string, InputOutputPort> Outputs { get; init; }
		}
	}
}
