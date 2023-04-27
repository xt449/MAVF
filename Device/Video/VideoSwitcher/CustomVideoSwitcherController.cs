using MILAV.API;
using MILAV.API.Device;
using MILAV.API.Device.Routing;
using MILAV.API.Device.Video.VideoSwitcher;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.Device.Video.VideoSwitcher
{
    [Device("customvideoswitcher")]
    public class CustomVideoSwitcherController : AbstractNetworkDevice, IVideoSwitcherControl<InputOutputPort, InputOutputPort>
    {
        [JsonProperty(Required = Required.Always)]
        public readonly string requestSetRoute;

        [JsonProperty(Required = Required.Always)]
        public readonly string responseSetRoute;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        public Dictionary<string, InputOutputPort> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        public Dictionary<string, InputOutputPort> Outputs { get; init; }

        Dictionary<InputOutputPort, InputOutputPort> IRouteControl<InputOutputPort, InputOutputPort>.Routes { get; } = new Dictionary<InputOutputPort, InputOutputPort>();

        bool IRouteControl<InputOutputPort, InputOutputPort>.ExecuteRoute(InputOutputPort input, InputOutputPort output)
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(string.Format(requestSetRoute, input.port, output.port));

                return Regex.Match(Connection.ReadASCII(), responseSetRoute).Success;
            }

            return false;
        }
    }
}
