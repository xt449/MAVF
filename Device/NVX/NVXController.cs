using MILAV.API;
using MILAV.API.Device;
using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.Device.NVX
{
    [Device("nvx")]
    public class NVXController : IDevice, IRouteControl<NVXEndpoint, NVXEndpoint>
    {
        public string Id { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<NVXEndpoint>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXEndpoint> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<NVXEndpoint>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXEndpoint> Outputs { get; init; }

        Dictionary<NVXEndpoint, NVXEndpoint> IRouteControl<NVXEndpoint, NVXEndpoint>.Routes { get; } = new Dictionary<NVXEndpoint, NVXEndpoint>();

        public void Initialize()
        {

        }

        bool IRouteControl<NVXEndpoint, NVXEndpoint>.ExecuteRoute(NVXEndpoint input, NVXEndpoint output)
        {
            var endpoint = Outputs[output.Id];
            if (endpoint == null)
            {
                return false;
            }

            return endpoint.Route(input, output.port).Result;
        }
    }
}
