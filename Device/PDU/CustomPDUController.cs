using MILAV.API.Device;
using MILAV.API.Device.PDU;
using Newtonsoft.Json;

namespace MILAV.Device.USB
{
    [Device("custompdu")]
    public class CustomPDUController : AbstractNetworkDevice, IPDUControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestTurnOffPower;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestTurnOnPower;

        public Dictionary<string, int> Ports { get; init; }

        public void TurnPowerOff(int port)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestTurnOffPower.Replace("$1", port.ToString()));
            }
        }

        public void TurnPowerOn(int port)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestTurnOnPower.Replace("$1", port.ToString()));
            }
        }
    }
}
