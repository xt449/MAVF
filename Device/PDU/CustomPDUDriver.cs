using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.PDU;
using System.Text.Json.Serialization;

namespace MAVF.Device.PDU
{
	[Driver("customPdu")]
	public class CustomPDUDriver : AbstractCommunicationDriver<CustomPDUDriver.DriverProperties>, IPDUControl
	{
		[JsonConstructor]
		public CustomPDUDriver(DriverProperties properties) : base(properties)
		{
		}

		public Dictionary<string, int> Ports => Properties.Ports;

		public void TurnPowerOff(int port)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(Properties.RequestTurnOffPower, port));
			}
		}

		public void TurnPowerOn(int port)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(Properties.RequestTurnOnPower, port));
			}
		}

		// Records

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("ports")]
			public required Dictionary<string, int> Ports { get; init; }

			[JsonPropertyName("requestTurnOffPower")]
			public required string RequestTurnOffPower { get; init; }

			[JsonPropertyName("requestTurnOnPower")]
			public required string RequestTurnOnPower { get; init; }
		}
	}
}
