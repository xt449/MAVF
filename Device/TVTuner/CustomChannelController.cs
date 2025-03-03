using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.TVTuner;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.TVTuner
{
	[Driver("customtvtuner")]
	public class CustomChannelController : AbstractNetworkDriver, IChannelControl
	{
		[JsonInclude]
		public readonly string requestGetChannel;

		[JsonInclude]
		public readonly string responseGetChannel;

		[JsonInclude]
		public readonly string requestSetChannel;

		public string? GetChannel()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestGetChannel);

				var match = Regex.Match(Connection.ReadASCII(), responseGetChannel);
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
				Connection.WriteASCII(string.Format(requestSetChannel, channel));
			}
		}
	}
}
