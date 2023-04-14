using MILAV.API;
using MILAV.API.Device;
using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.Device.NVX
{
    public class NVXController : IDevice, IRouteControl<NVXEndpoint, NVXEndpoint>
    {
        public string Id { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXEndpoint> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXEndpoint> Outputs { get; init; }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        bool IRouteControl<NVXEndpoint, NVXEndpoint>.ExecuteRoute(NVXEndpoint input, NVXEndpoint output)
        {
            throw new NotImplementedException();
        }
    }
}
