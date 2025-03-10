using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Shade;
using System.Text.Json.Serialization;

namespace MAVF.Device.Shade
{
	[Driver("customtshade")]
	public class CustomShadeDriver : AbstractCommunicationDriver<CustomShadeDriver.DriverProperties>, IShadeControl
	{
		[JsonConstructor]
		public CustomShadeDriver(DriverProperties properties) : base(properties)
		{
		}

		public void ShadesClose()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestShadesClose);
			}
		}

		public void ShadesHalf()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.ResponseShadesHalf);
			}
		}

		public void ShadesOpen()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestShadesOpen);
			}
		}

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestShadesClose")]
			public required string RequestShadesClose { get; init; }

			[JsonPropertyName("responseShadesHalf")]
			public required string ResponseShadesHalf { get; init; }

			[JsonPropertyName("requestShadesOpen")]
			public required string RequestShadesOpen { get; init; }
		}
	}
}
