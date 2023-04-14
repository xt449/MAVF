using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using MILAV.API;
using MILAV.API.Device;

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

        public IEnumerable<Input>? GetDeviceInputsById(string deviceId)
        {
            if(controller.GetDeviceById(deviceId) is IRouteControl routing)
            {
                return routing.Inputs.Values;
            }

            return null;
        }

        public IEnumerable<Output>? GetDeviceOutputsById(string deviceId)
        {
            if (controller.GetDeviceById(deviceId) is IRouteControl routing)
            {
                return routing.Outputs.Values;
            }

            return null;
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

        public bool TryRoute(string deviceId, Input input, Output output)
        {
            if (input.type != output.type)
            {
                return false;
            }

            var clientIp = Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            var user = controller.GetUserByIp(clientIp);
            if (user == null || !user.CanRouteInput(input) || !user.CanRouteOutput(output))
            {
                return false;
            }

            if (controller.GetDeviceById(deviceId) is IRouteControl routing)
            {
                return routing.Route(input, output);
            }

            return false;
        }

        public Input? GetRoute(string deviceId, Output output)
        {
            throw new NotImplementedException();
        }
    }
}
