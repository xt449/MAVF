using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.Device.NVX
{
    public class NVXEndpoint : IInputOutput
    {
        public string Id { get; init; }

        public IOType Type { get; init; }

        public string Group { get; init; }

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string ip;
    }
}
