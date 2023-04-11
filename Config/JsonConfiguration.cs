using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.Config
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class JsonConfiguration
    {
        [JsonProperty("debug")]
        bool Debug { get; }

        [JsonProperty("defaultState")]
        string DefaultState { get; }

        [JsonProperty("devices")]
        AbstractDevice[] Devices { get; }
    }
}
