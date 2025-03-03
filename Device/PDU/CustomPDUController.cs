using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.PDU;
using System.Text.Json.Serialization;

namespace MAVF.Device.USB
{
	[Driver("custompdu")]
	public class CustomPDUController : AbstractNetworkDriver, IPDUControl
	{
		[JsonInclude]
		public readonly string requestTurnOffPower;

		[JsonInclude]
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
