using MAVF.API.Device;
using MAVF.API.Device.PDU;
using Newtonsoft.Json;

namespace MAVF.Device.USB
{
	[Device("custompdu")]
	public class CustomPDUController : AbstractNetworkDevice, IPDUControl
	{
		[JsonProperty(Required = Required.Always)]
		public readonly string requestTurnOffPower;

		[JsonProperty(Required = Required.Always)]
		public readonly string requestTurnOnPower;

		public Dictionary<string, int> Ports { get; init; }

		public void TurnPowerOff(int port)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(requestTurnOffPower, port));
			}
		}

		public void TurnPowerOn(int port)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(requestTurnOnPower, port));
			}
		}
	}
}
