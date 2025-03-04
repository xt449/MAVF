using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.TVTuner;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.TVTuner
{
	[Driver("customRemote")]
	public class CustomRemoteControlDriver : AbstractCommunicationDriver<CustomRemoteControlDriver.DriverProperties>, IRemoteControl
	{
		[JsonConstructor]
		public CustomRemoteControlDriver(DriverProperties properties) : base(properties)
		{
		}

		public void SetPower(bool state)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(state ? Properties.RequestSetPowerOn : Properties.RequestSetPowerOff);
			}
		}

		public void ArrowUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestArrowUp);
			}
		}

		public void ArrowDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestArrowDown);
			}
		}

		public void ArrowLeft()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestArrowLeft);
			}
		}

		public void ArrowRight()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestArrowRight);
			}
		}

		public void Back()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestBack);
			}
		}

		public void ChannelDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestChannelDown);
			}
		}

		public void ChannelUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestChannelUp);
			}
		}

		public void Enter()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestEnter);
			}
		}

		public void Exit()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestExit);
			}
		}

		public bool GetPower()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestGetPower);

				var match = Regex.Match(Connection.ReadASCII(), Properties.ResponseGetPower);
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
				Connection.WriteASCII(Properties.RequestGuide);
			}
		}

		public void Menu()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestMenu);
			}
		}

		public void VolumeDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestVolumeDown);
			}
		}

		public void VolumeUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(Properties.RequestVolumeUp);
			}
		}

		public record DriverProperties : CommunicationDriverProperties
		{
			[JsonPropertyName("requestSetPowerOn")]
			public required string RequestSetPowerOn { get; init; }

			[JsonPropertyName("requestSetPowerOff")]
			public required string RequestSetPowerOff { get; init; }

			[JsonPropertyName("requestArrowUp")]
			public required string RequestArrowUp { get; init; }

			[JsonPropertyName("requestArrowDown")]
			public required string RequestArrowDown { get; init; }

			[JsonPropertyName("requestArrowLeft")]
			public required string RequestArrowLeft { get; init; }

			[JsonPropertyName("requestArrowRight")]
			public required string RequestArrowRight { get; init; }

			[JsonPropertyName("requestBack")]
			public required string RequestBack { get; init; }

			[JsonPropertyName("requestChannelDown")]
			public required string RequestChannelDown { get; init; }

			[JsonPropertyName("requestChannelUp")]
			public required string RequestChannelUp { get; init; }

			[JsonPropertyName("requestEnter")]
			public required string RequestEnter { get; init; }

			[JsonPropertyName("requestExit")]
			public required string RequestExit { get; init; }

			[JsonPropertyName("requestGetPower")]
			public required string RequestGetPower { get; init; }

			[JsonPropertyName("responseGetPower")]
			public required string ResponseGetPower { get; init; }

			[JsonPropertyName("requestGuide")]
			public required string RequestGuide { get; init; }

			[JsonPropertyName("requestMenu")]
			public required string RequestMenu { get; init; }

			[JsonPropertyName("requestVolumeDown")]
			public required string RequestVolumeDown { get; init; }

			[JsonPropertyName("requestVolumeUp")]
			public required string RequestVolumeUp { get; init; }
		}
	}
}
