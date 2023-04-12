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

        public AbstractDevice[] GetDevices()
        {
            return controller.GetDevices();
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

        public Input[]? GetDeviceInputsById(string id)
        {
            return controller.GetDeviceById(id)?.inputs;
        }

        public Output[]? GetDeviceOutputsById(string id)
        {
            return controller.GetDeviceById(id)?.outputs;
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

        // TVTuner

        // VideoController > VideoWall

        // VTC
    }
}
