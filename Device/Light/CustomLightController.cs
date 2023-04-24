using MILAV.API.Device;
using MILAV.API.Device.Light;
using Newtonsoft.Json;

namespace MILAV.Device.TVTuner
{
    public class CustomLightController : AbstractNetworkDevice, ILightControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetLightLevel;

        public void SetLightLevel(float lightLevel)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestSetLightLevel.Replace("$1", lightLevel.ToString()));
            }
        }
    }
}
