﻿using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.Light;
using System.Text.Json.Serialization;

namespace MAVF.Device.Light
{
	[Driver("customLight")]
	public class CustomLightDriver : AbstractCommunicationDriver<CustomLightDriver.DriverProperties>, ILightControl
	{
		[JsonConstructor]
		public CustomLightDriver(DriverProperties properties) : base(properties)
		{
		}

		public void SetLightLevel(float lightLevel)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(string.Format(Properties.RequestSetLightLevel, lightLevel));
			}
		}

		// Records

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestSetLightLevel")]
			public required string RequestSetLightLevel { get; init; }
		}
	}
}
