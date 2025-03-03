using MAVF.API;
using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Routing;
using MAVF.API.Device.Driver.USB;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.USB
{
	[Driver("customusb")]
	public class CustomUSBController : AbstractNetworkDriver, IUSBControl<InputOutputPort, InputOutputPort>
	{
		[JsonInclude]
		public readonly string requestSetRoute;

		[JsonInclude]
		public readonly string responseSetRoute;

		[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
		public Dictionary<string, InputOutputPort> Inputs { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<InputOutputPort>))]
		public Dictionary<string, InputOutputPort> Outputs { get; init; }

		Dictionary<InputOutputPort, InputOutputPort> IRouteControl<InputOutputPort, InputOutputPort>.Routes { get; } = new Dictionary<InputOutputPort, InputOutputPort>();

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
