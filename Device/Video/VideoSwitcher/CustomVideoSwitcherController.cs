using MAVF.API;
using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Routing;
using MAVF.API.Device.Driver.Video.VideoSwitcher;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.Video.VideoSwitcher
{
	[Driver("customvideoswitcher")]
	public class CustomVideoSwitcherController : AbstractNetworkDriver, IVideoSwitcherControl<InputOutputPort, InputOutputPort>
	{
		[JsonInclude]
		public readonly string requestSetRoute;

		[JsonInclude]
		public readonly string responseSetRoute;

		[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
		public Dictionary<string, InputOutputPort> Inputs { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
		public Dictionary<string, InputOutputPort> Outputs { get; init; }

		public Dictionary<InputOutputPort, InputOutputPort> Routes { get; } = new Dictionary<InputOutputPort, InputOutputPort>();

		bool IRouteControl<InputOutputPort, InputOutputPort>.ExecuteRoute(InputOutputPort input, InputOutputPort output)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(requestSetRoute, input.Port, output.Port));

				return Regex.Match(Connection.ReadASCII(), responseSetRoute).Success;
			}

			return false;
		}
	}
}
