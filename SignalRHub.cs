using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using MILAV.API;
using MILAV.API.Device;
using MILAV.API.Device.Routing;
using MILAV.API.Device.Video;
using MILAV.API.Layout;

namespace MILAV
{
    public class SignalRHub : Hub, IServerAPI
    {
        protected readonly Controller controller;

        public SignalRHub(Controller controller)
        {
            this.controller = controller;
        }

        // Devices

        public IEnumerable<IDevice> GetDevices()
        {
            return controller.GetDevices().Values;
        }

        // Device

        public IDevice? GetDeviceById(string deviceId)
        {
            return controller.GetDeviceById(deviceId);
        }

        // ControlState

        public string GetControlState()
        {
            return controller.GetControlState();
        }

        public async void SetControlState(string state)
        {
            controller.SetControlState(state);
            await Clients.All.SendAsync("AckSetControlState", state);
        }

        public async void ResetControlState()
        {
            var state = controller.GetDefaultControlState();
            controller.SetControlState(state);
            await Clients.All.SendAsync("AckSetControlState", state);
        }

        // Routing

        public IEnumerable<IInputOutput>? GetDeviceInputsById(string deviceId)
        {
            if (controller.GetDeviceById(deviceId) is IRouteControl<IInputOutput, IInputOutput> device)
            {
                return device.Inputs.Values;
            }

            return null;
        }

        public IEnumerable<IInputOutput>? GetDeviceOutputsById(string deviceId)
        {
            if (controller.GetDeviceById(deviceId) is IRouteControl<IInputOutput, IInputOutput> device)
            {
                return device.Outputs.Values;
            }

            return null;
        }

        public bool TryRoute(string deviceId, IInputOutput input, IInputOutput output)
        {
            var clientIp = Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            var user = controller.GetUserByIp(clientIp);
            if (user == null || !user.CanRouteInput(input) || !user.CanRouteOutput(output))
            {
                return false;
            }

            if (controller.GetDeviceById(deviceId) is IRouteControl<IInputOutput, IInputOutput> device)
            {
                return device.Route(input, output);
            }

            return false;
        }

        public IInputOutput? GetRoute(string deviceId, IInputOutput output)
        {
            if (controller.GetDeviceById(deviceId) is IRouteControl<IInputOutput, IInputOutput> device)
            {
                return device.GetRoute(output);
            }

            return null;
        }

        // Layouts

        public bool TryLayout(string deviceId, ILayout layout)
        {
            if (controller.GetDeviceById(deviceId) is ILayoutControl device)
            {
                device.SetLayout(layout);
                return true;
            }

            return false;
        }

        public ILayout? GetLayout(string deviceId)
        {
            if (controller.GetDeviceById(deviceId) is ILayoutControl device)
            {
                return device.GetLayout();
            }

            return null;
        }
    }
}
