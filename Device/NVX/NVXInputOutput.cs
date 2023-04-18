using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.Device.NVX
{
    public class NVXInputOutput : InputOutputPort
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string ip;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string username;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string password;
    }
}
