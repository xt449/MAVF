using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Camera;
using System.Text.Json.Serialization;

namespace MAVF.Device.TVTuner
{
	public class CustomCameraController : AbstractNetworkDriver, ICameraControl
	{
		[JsonInclude]
		public readonly string requestPanUp;

		[JsonInclude]
		public readonly string requestPanDown;

		[JsonInclude]
		public readonly string requestPanLeft;

		[JsonInclude]
		public readonly string requestPanRight;

		[JsonInclude]
		public readonly string requestZoomIn;

		[JsonInclude]
		public readonly string requestZoomOut;

		[JsonInclude]
		public readonly string requestOpenLens;

		[JsonInclude]
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
