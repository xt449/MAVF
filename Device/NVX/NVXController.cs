using MILAV.API;
using MILAV.API.Device;
using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.Device.NVX
{
    [Device("nvx")]
    public class NVXController : IDevice, IRouteControl<NVXInputOutput, NVXInputOutput>
    {
        public string Id { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<NVXInputOutput>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXInputOutput> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<NVXInputOutput>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, NVXInputOutput> Outputs { get; init; }

        Dictionary<NVXInputOutput, NVXInputOutput> IRouteControl<NVXInputOutput, NVXInputOutput>.routes { get; } = new Dictionary<NVXInputOutput, NVXInputOutput>();

        private readonly Dictionary<string, NVXEndpoint> endpoints = new Dictionary<string, NVXEndpoint>();

        public void Initialize()
        {
            foreach (var input in Inputs.Values)
            {
                if (!endpoints.ContainsKey(input.ip))
                {
                    endpoints.Add(input.ip, new NVXEndpoint(input));
                }
            }

            foreach (var output in Outputs.Values)
            {
                if (!endpoints.ContainsKey(output.ip))
                {
                    endpoints.Add(output.ip, new NVXEndpoint(output));
                }
            }
        }

        bool IRouteControl<NVXInputOutput, NVXInputOutput>.ExecuteRoute(NVXInputOutput input, NVXInputOutput output)
        {
            var endpoint = endpoints[output.ip];
            if(endpoint == null)
            {
                return false;
            }

            return endpoint.Route(input, output.port);
        }
    }
}
