using MILAV.API.Device;
using MILAV.API.Device.Camera;
using Newtonsoft.Json;

namespace MILAV.Device.TVTuner
{
    public class CustomCameraController : AbstractNetworkDevice, ICameraControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestPanUp;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestPanDown;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestPanLeft;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestPanRight;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestZoomIn;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestZoomOut;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestOpenLens;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestCloseLens;

        public void PanUp()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestPanUp);
            }
        }

        public void PanDown()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestPanDown);
            }
        }

        public void PanLeft()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestPanLeft);
            }
        }

        public void PanRight()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestPanRight);
            }
        }

        public void ZoomIn()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestZoomIn);
            }
        }

        public void ZoomOut()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestZoomOut);
            }
        }

        public void OpenLens()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestOpenLens);
            }
        }

        public void CloseLens()
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestCloseLens);
            }
        }
    }
}
