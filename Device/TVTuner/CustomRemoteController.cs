using MILAV.API.Device.TVTuner;
using MILAV.API.Device;
using Newtonsoft.Json;
using System.Threading.Channels;
using System.Text.RegularExpressions;

namespace MILAV.Device.TVTuner
{
    public class CustomRemoteController : AbstractNetworkDevice, IRemoteControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetPowerOn;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetPowerOff;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestArrowDown;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestArrowLeft;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestArrowRight;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestArrowUp;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestBack;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestChannelDown;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestChannelUp;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestEnter;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestExit;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestGetPower;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseGetPower;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestGuide;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestMenu;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestVolumeDown;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestVolumeUp;

        public void SetPower(bool state)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(state ? requestSetPowerOn : requestSetPowerOff);
            }
        }

        public void ArrowDown()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestArrowDown);
            }
        }

        public void ArrowLeft()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestArrowLeft);
            }
        }

        public void ArrowRight()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestArrowRight);
            }
        }

        public void ArrowUp()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestArrowUp);
            }
        }

        public void Back()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestBack);
            }
        }

        public void ChannelDown()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestChannelDown);
            }
        }

        public void ChannelUp()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestChannelUp);
            }
        }

        public void Enter()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestEnter);
            }
        }

        public void Exit()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestExit);
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

        public void Guide()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestGuide);
            }
        }

        public void Menu()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestMenu);
            }
        }

        public void VolumeDown()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestVolumeDown);
            }
        }

        public void VolumeUp()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestVolumeUp);
            }
        }
    }
}
