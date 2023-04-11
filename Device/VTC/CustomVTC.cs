using MILAV.API.Device;
using MILAV.API.Device.TVTuner;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.Device.VTC
{
    [Device("customvtc")]
    public class CustomVTC : AbstractDevice, IChannelControl, IPowerControl
    {
        [JsonProperty]
        public readonly string requestGetChannel;

        [JsonProperty]
        public readonly string responseGetChannel;

        [JsonProperty]
        public readonly string requestSetChannel;

        [JsonProperty]
        public readonly string requestGetPower;

        [JsonProperty]
        public readonly string responseGetPower;

        [JsonProperty]
        public readonly string requestSetPower;

        public override void Validate()
        {
            if (requestGetChannel == null) throw new JsonException("Device was deserialized with null 'requestGetChannel'");
            if (responseGetChannel == null) throw new JsonException("Device was deserialized with null 'responseGetChannel'");
            if (requestSetChannel == null) throw new JsonException("Device was deserialized with null 'requestSetChannel'");
            if (requestGetPower == null) throw new JsonException("Device was deserialized with null 'requestGetPower'");
            if (responseGetPower == null) throw new JsonException("Device was deserialized with null 'responseGetPower'");
            if (requestSetPower == null) throw new JsonException("Device was deserialized with null 'requestSetPower'");
        }

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
