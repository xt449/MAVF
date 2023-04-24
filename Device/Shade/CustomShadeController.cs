using MILAV.API.Device;
using MILAV.API.Device.Shade;
using Newtonsoft.Json;

namespace MILAV.Device.TVTuner
{
    [Device("customtshade")]
    public class CustomShadeController : AbstractNetworkDevice, IShadeControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestShadesClose;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseShadesHalf;

        [JsonProperty(Required = Required.DisallowNull)]
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
