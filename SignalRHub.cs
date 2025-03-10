using MAVF.API;
using MAVF.API.Device.Driver.PDU;
using MAVF.API.Device.Driver.Routing;
using MAVF.API.Device.Driver.TVTuner;
using MAVF.API.Device.Driver.Video;
using MAVF.API.Layout;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace MAVF
{
	public class SignalRHub : Hub, IServerAPI
	{
		protected readonly Controller controller;

		public SignalRHub(Controller controller)
		{
			this.controller = controller;
		}

		// Devices

		public IEnumerable<API.Device.Device> GetDevices()
		{
			return controller.GetDevices().Values;
		}

		// Device

		public API.Device.Device? GetDeviceById(string deviceId)
		{
			return controller.GetDeviceById(deviceId);
		}

		// Mode

		public string GetMode()
		{
			return controller.GetMode();
		}

		public async void SetMode(string mode)
		{
			controller.SetMode(mode);
			await Clients.All.SendAsync("AckSetMode", mode);
		}

		public async void ResetMode()
		{
			var mode = controller.GetDefaultMode();
			controller.SetMode(mode);
			await Clients.All.SendAsync("AckSetMode", mode);
		}

		// Routing

		public IEnumerable<IInputOutput>? GetDeviceInputsById(string deviceId)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IRouteControl<IInputOutput, IInputOutput> device)
			{
				return device.Inputs.Values;
			}

			return null;
		}

		public IEnumerable<IInputOutput>? GetDeviceOutputsById(string deviceId)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IRouteControl<IInputOutput, IInputOutput> device)
			{
				return device.Outputs.Values;
			}

			return null;
		}

		public bool TrySetRoute(string deviceId, IInputOutput input, IInputOutput output)
		{
			var clientIp = Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
			var user = controller.GetUserByIp(clientIp);
			if (user == null || !user.CanRouteInput(input) || !user.CanRouteOutput(output))
			{
				return false;
			}

			if (controller.GetDeviceById(deviceId)?.Driver is IRouteControl<IInputOutput, IInputOutput> device)
			{
				return device.Route(input, output);
			}

			return false;
		}

		public IInputOutput? GetRoute(string deviceId, IInputOutput output)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IRouteControl<IInputOutput, IInputOutput> device)
			{
				return device.GetRoute(output);
			}

			return null;
		}

		// Layout

		public bool TrySetLayout(string deviceId, ILayout layout)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is ILayoutControl device)
			{
				device.SetLayout(layout);
				return true;
			}

			return false;
		}

		public ILayout? GetLayout(string deviceId)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is ILayoutControl device)
			{
				return device.GetLayout();
			}

			return null;
		}

		// TVTuner

		public bool TrySetChannel(string deviceId, string channel)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IChannelControl device)
			{
				device.SetChannel(channel);
				return true;
			}

			return false;
		}

		public string? GetChannel(string deviceId)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IChannelControl device)
			{
				return device.GetChannel();
			}

			return null;
		}

		// PDU

		public bool TryTurnPowerOn(string deviceId, int port)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IPDUControl device)
			{
				device.TurnPowerOn(port);
				return true;
			}

			return false;
		}

		public bool TryTurnPowerOff(string deviceId, int port)
		{
			if (controller.GetDeviceById(deviceId)?.Driver is IPDUControl device)
			{
				device.TurnPowerOff(port);
				return true;
			}

			return false;
		}
	}
}
