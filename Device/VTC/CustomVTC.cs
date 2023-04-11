using MILAV.API.Device;
using MILAV.API.Device.TVTuner;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.Device.VTC
{
    [Device("customvtc")]
    public class CustomVTC : AbstractDevice, IChannelControl, IPowerControl
    {
        [JsonProperty("requestGetChannel")]
        public string GetChannelRequest { get; }

        [JsonProperty("responseGetChannel")]
        public string GetChannelResponse { get; }

        [JsonProperty("requestSetChannel")]
        public string SetChannelRequest { get; }

        [JsonProperty("requestGetPower")]
        public string GetPowerRequest { get; }

        [JsonProperty("responseGetPower")]
        public string GetPowerResponse { get; }

        [JsonProperty("requestSetPower")]
        public string SetPowerRequest { get; }

        public override void Validate() {
            base.Validate();

            if (GetChannelRequest == null) throw new JsonException("Device was deserialized with null 'requestGetChannel'");
            if (GetChannelResponse == null) throw new JsonException("Device was deserialized with null 'responseGetChannel'");
            if (SetChannelRequest == null) throw new JsonException("Device was deserialized with null 'requestSetChannel'");
            if (GetPowerRequest == null) throw new JsonException("Device was deserialized with null 'requestGetPower'");
            if (GetPowerResponse == null) throw new JsonException("Device was deserialized with null 'responseGetPower'");
            if (SetPowerRequest == null) throw new JsonException("Device was deserialized with null 'requestSetPower'");
        }

        public string? GetChannel()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(GetChannelRequest);

                var match = Regex.Match(Connection.ReadASCII(), GetChannelResponse);
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
                Connection.WriteASCII(SetChannelRequest.Replace("$1", channel));
            }
        }

        public bool GetPower()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(GetPowerRequest);

                var match = Regex.Match(Connection.ReadASCII(), GetPowerResponse);
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
                Connection.WriteASCII(SetPowerRequest.Replace("$1", state ? "1" : "0"));
            }
        }
    }
}
