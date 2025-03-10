using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Camera;
using System.Text.Json.Serialization;

namespace MAVF.Device.Camera
{
	[Driver("customCamera")]
	public class CustomCameraDriver : AbstractCommunicationDriver<CustomCameraDriver.DriverProperties>, ICameraControl
	{
		[JsonConstructor]
		public CustomCameraDriver(DriverProperties properties) : base(properties)
		{
		}

		public void PanUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestPanUp);
			}
		}

		public void PanDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestPanDown);
			}
		}

		public void PanLeft()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestPanLeft);
			}
		}

		public void PanRight()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestPanRight);
			}
		}

		public void ZoomIn()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestZoomIn);
			}
		}

		public void ZoomOut()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestZoomOut);
			}
		}

		public void OpenLens()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestOpenLens);
			}
		}

		public void CloseLens()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestCloseLens);
			}
		}

		// Records

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestPanUp")]
			public required string RequestPanUp { get; init; }

			[JsonPropertyName("requestPanDown")]
			public required string RequestPanDown { get; init; }

			[JsonPropertyName("requestPanLeft")]
			public required string RequestPanLeft { get; init; }

			[JsonPropertyName("requestPanRight")]
			public required string RequestPanRight { get; init; }

			[JsonPropertyName("requestZoomIn")]
			public required string RequestZoomIn { get; init; }

			[JsonPropertyName("requestZoomOut")]
			public required string RequestZoomOut { get; init; }

			[JsonPropertyName("requestOpenLens")]
			public required string RequestOpenLens { get; init; }

			[JsonPropertyName("requestCloseLens")]
			public required string RequestCloseLens { get; init; }
		}
	}
}
