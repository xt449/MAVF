using MAVF.API.Device.Driver;
using MAVF.API.Device.Driver.TVTuner;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MAVF.Device.TVTuner
{
	public class CustomRemoteController : AbstractNetworkDriver, IRemoteControl
	{
		[JsonInclude]
		public readonly string requestSetPowerOn;

		[JsonInclude]
		public readonly string requestSetPowerOff;

		[JsonInclude]
		public readonly string requestArrowUp;

		[JsonInclude]
		public readonly string requestArrowDown;

		[JsonInclude]
		public readonly string requestArrowLeft;

		[JsonInclude]
		public readonly string requestArrowRight;

		[JsonInclude]
		public readonly string requestBack;

		[JsonInclude]
		public readonly string requestChannelDown;

		[JsonInclude]
		public readonly string requestChannelUp;

		[JsonInclude]
		public readonly string requestEnter;

		[JsonInclude]
		public readonly string requestExit;

		[JsonInclude]
		public readonly string requestGetPower;

		[JsonInclude]
		public readonly string responseGetPower;

		[JsonInclude]
		public readonly string requestGuide;

		[JsonInclude]
		public readonly string requestMenu;

		[JsonInclude]
		public readonly string requestVolumeDown;

		[JsonInclude]
		public readonly string requestVolumeUp;

		public void SetPower(bool state)
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(state ? requestSetPowerOn : requestSetPowerOff);
			}
		}

		public void ArrowUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestArrowUp);
			}
		}

		public void ArrowDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestArrowDown);
			}
		}

		public void ArrowLeft()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestArrowLeft);
			}
		}

		public void ArrowRight()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestArrowRight);
			}
		}

		public void Back()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestBack);
			}
		}

		public void ChannelDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestChannelDown);
			}
		}

		public void ChannelUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestChannelUp);
			}
		}

		public void Enter()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestEnter);
			}
		}

		public void Exit()
		{
			if (Connection.Connect())
			{
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
				Connection.WriteASCII(requestGuide);
			}
		}

		public void Menu()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestMenu);
			}
		}

		public void VolumeDown()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestVolumeDown);
			}
		}

		public void VolumeUp()
		{
			if (Connection.Connect())
			{
				Connection.WriteASCII(requestVolumeUp);
			}
		}
	}
}
