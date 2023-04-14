using Microsoft.AspNetCore.SignalR;
using MILAV.API;
using MILAV.API.Connection;
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

        public IEnumerable<AbstractDevice> GetDevices()
        {
            return controller.GetDevices().Values;
        }

        // Device

        public AbstractDevice? GetDeviceById(string id)
        {
            return controller.GetDeviceById(id);
        }

        // Should this be hidden?
        public string? GetDeviceIpById(string id)
        {
            return controller.GetDeviceById(id)?.ip;
        }

        // Should this be hidden?
        public int? GetDevicePortById(string id)
        {
            return controller.GetDeviceById(id)?.port;
        }

        // Should this be hidden?
        public Protocol? GetDeviceProtocolById(string id)
        {
            return controller.GetDeviceById(id)?.protocol;
        }

        public IEnumerable<Input>? GetDeviceInputsById(string id)
        {
            return controller.GetDeviceById(id)?.Inputs.Values;
        }

        public IEnumerable<Output>? GetDeviceOutputsById(string id)
        {
            return controller.GetDeviceById(id)?.Outputs.Values;
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

        public bool TryRoute(Input input, Output output)
        {
            if (output.device is IRouteControl controller)
            {
                return controller.Route(input, output);
            }

            return false;
        }

        public Input? GetRoute(Output output)
        {
            throw new NotImplementedException();
        }
    }
}
