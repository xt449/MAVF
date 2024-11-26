using MAVF.API;
using MAVF.API.Device;
using MAVF.API.Device.Routing;
using MAVF.API.Device.USB;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MAVF.Device.USB
{
	[Device("customusb")]
	public class CustomUSBController : AbstractNetworkDevice, IUSBControl<InputOutputPort, InputOutputPort>
	{
		[JsonProperty(Required = Required.Always)]
		public readonly string requestSetRoute;

		[JsonProperty(Required = Required.Always)]
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
				Connection.WriteASCII(string.Format(requestSetRoute, input.port, output.port));

				return Regex.Match(Connection.ReadASCII(), responseSetRoute).Success;
			}

			return false;
		}
	}
}
