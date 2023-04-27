using MILAV.API.Device;
using MILAV.API.Device.TVTuner;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.Device.TVTuner
{
    [Device("customtvtuner")]
    public class CustomChannelController : AbstractNetworkDevice, IChannelControl
    {
        [JsonProperty(Required = Required.Always)]
        public readonly string requestGetChannel;

        [JsonProperty(Required = Required.Always)]
        public readonly string responseGetChannel;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestSetChannel;

        public string? GetChannel()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestGetChannel);

                var match = Regex.Match(Connection.ReadASCII(), responseGetChannel);
                if (match.Success)
                {
                    return match.Value;
                }
            }

            return null;
        }

        public void SetChannel(string channel)
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(string.Format(requestSetChannel, channel));
            }
        }
    }
}
