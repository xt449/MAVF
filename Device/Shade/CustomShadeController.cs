using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Shade;
using System.Text.Json.Serialization;

namespace MAVF.Device.TVTuner
{
	[Driver("customtshade")]
	public class CustomShadeController : AbstractNetworkDriver, IShadeControl
	{
		[JsonInclude]
		public readonly string requestShadesClose;

		[JsonInclude]
		public readonly string responseShadesHalf;

		[JsonInclude]
		public readonly string requestShadesOpen;

		public void ShadesClose()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestShadesClose);
			}
		}

		public void ShadesHalf()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(responseShadesHalf);
			}
		}

		public void ShadesOpen()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestShadesOpen);
			}
		}
	}
}
