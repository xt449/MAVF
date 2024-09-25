using MAVF.API.Device;
using MAVF.API.Device.Shade;
using Newtonsoft.Json;

namespace MAVF.Device.TVTuner
{
	[Device("customtshade")]
	public class CustomShadeController : AbstractNetworkDevice, IShadeControl
	{
		[JsonProperty(Required = Required.Always)]
		public readonly string requestShadesClose;

		[JsonProperty(Required = Required.Always)]
		public readonly string responseShadesHalf;

		[JsonProperty(Required = Required.Always)]
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
