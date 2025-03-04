using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.TVTuner;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.TVTuner
{
	[Driver("customTvtuner")]
	public class CustomChannelControlDriver : AbstractCommunicationDriver<CustomChannelControlDriver.DriverProperties>, IChannelControl
	{
		[JsonConstructor]
		public CustomChannelControlDriver(DriverProperties properties) : base(properties)
		{
		}

		public string? GetChannel()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestGetChannel);

				var match = Regex.Match(Connection.ReadASCII(), Properties.ResponseGetChannel);
				if (match.Success)
				{
					return match.Value;
				}
			}

			return null;
		}

		public void SetChannel(string channel)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(Properties.RequestSetChannel, channel));
			}
		}

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestGetChannel")]
			public required string RequestGetChannel { get; init; }

			[JsonPropertyName("responseGetChannel")]
			public required string ResponseGetChannel { get; init; }

			[JsonPropertyName("requestSetChannel")]
			public required string RequestSetChannel { get; init; }
		}
	}
}
