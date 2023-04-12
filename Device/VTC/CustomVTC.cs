using MILAV.API.Device;
using MILAV.API.Device.TVTuner;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.Device.VTC
{
    [Device("customvtc")]
    public class CustomVTC : AbstractDevice, IChannelControl, IPowerControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestGetChannel;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseGetChannel;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetChannel;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestGetPower;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseGetPower;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetPower;

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
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestSetChannel.Replace("$1", channel));
            }
        }

        public bool GetPower()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestGetPower);

                var match = Regex.Match(Connection.ReadASCII(), responseGetPower);
                if (match.Success)
                {
                    return bool.Parse(match.Value);
                }
            }

            return false;
        }

        public void SetPower(bool state)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestSetPower.Replace("$1", state ? "1" : "0"));
            }
        }
    }
}
