using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Light;
using System.Text.Json.Serialization;

namespace MAVF.Device.TVTuner
{
	public class CustomLightController : AbstractNetworkDriver, ILightControl
	{
		[JsonInclude]
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
