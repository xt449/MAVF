using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.Config
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class JsonConfiguration
    {
        [JsonProperty("debug")]
        public bool Debug { get; private set; }

        [JsonProperty("defaultState")]
        public string DefaultState { get; private set; }

        [JsonProperty("devices")]
        public AbstractDevice[] Devices { get; private set; }

        public static JsonConfiguration New()
        {
            return new JsonConfiguration()
            {
                Debug = false,
                DefaultState = "",
                Devices = new AbstractDevice[0],
            };
        }
    }
}
