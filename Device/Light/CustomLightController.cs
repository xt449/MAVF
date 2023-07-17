using MILAV.API.Device;
using MILAV.API.Device.Light;
using Newtonsoft.Json;

namespace MILAV.Device.TVTuner
{
	public class CustomLightController : AbstractNetworkDevice, ILightControl
	{
		[JsonProperty(Required = Required.Always)]
		public readonly string requestSetLightLevel;

		public void SetLightLevel(float lightLevel)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(requestSetLightLevel, lightLevel));
			}
		}
	}
}
