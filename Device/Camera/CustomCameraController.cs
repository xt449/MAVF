using MILAV.API.Device;
using MILAV.API.Device.Camera;
using Newtonsoft.Json;

namespace MILAV.Device.TVTuner
{
    public class CustomCameraController : AbstractNetworkDevice, ICameraControl
    {
        [JsonProperty(Required = Required.Always)]
        public readonly string requestPanUp;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestPanDown;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestPanLeft;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestPanRight;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestZoomIn;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestZoomOut;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestOpenLens;

        [JsonProperty(Required = Required.Always)]
        public readonly string requestCloseLens;

        public void PanUp()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestPanUp);
            }
        }

        public void PanDown()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestPanDown);
            }
        }

        public void PanLeft()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestPanLeft);
            }
        }

        public void PanRight()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestPanRight);
            }
        }

        public void ZoomIn()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestZoomIn);
            }
        }

        public void ZoomOut()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestZoomOut);
            }
        }

        public void OpenLens()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestOpenLens);
            }
        }

        public void CloseLens()
        {
            if (Connection.Connect())
            {
                Connection.WriteASCII(requestCloseLens);
            }
        }
    }
}
